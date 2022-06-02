using System;
using System.Collections.Generic;
using System.Drawing;


namespace Go.src.Model
{


    public class StaticEvaluation
    {
        BoardFunctions boardFunctions;

        public int stoneStrength { get; }
        public int influenceThreshold { get; }


        public StaticEvaluation(int stoneStrength, int influenceThreshold)
        {
            this.stoneStrength = stoneStrength;
            this.influenceThreshold = influenceThreshold;

            boardFunctions = new BoardFunctions();

        }


        public int Heuristic(Board board, int id)
        {
            // Validate Parameters
            global::System.Diagnostics.Debug.Assert(board != null);
            global::System.Diagnostics.Debug.Assert(id == Go.Properties.PlayerInfo.Default.PLAYER1ID || id == Go.Properties.PlayerInfo.Default.PLAYER2ID);

            int territory_balance = 0;
            int atari_stones_balance = 0;

            // Get Territory Of Both Sides
            (int t1, int t2) = GetTerritory(board);
            if (id == Go.Properties.PlayerInfo.Default.PLAYER1ID)
                territory_balance += t1 - t2;
            else
                territory_balance += t2 - t1;


            // Get Groups of Stones On Board
            HashSet<(HashSet<Point>, int)> Groups = boardFunctions.GetGroups(board);
            foreach ((HashSet<Point> group, int groupId) in Groups)
            {
                // If Group of Stones is in An Atari, meaning it has only 1 liberty left before it is captured
                // https://en.wikipedia.org/wiki/List_of_Go_terms#Atari

                if (IsAtari(board, group))
                {
                    if (groupId == id)
                        atari_stones_balance -= group.Count;
                    else if (groupId == boardFunctions.GetOpponentId(id))
                        atari_stones_balance += group.Count;
                }

                // If Group is One Liberty Away From State of Atari, 2 Liberties Before It's Captured
                if (boardFunctions.libertiesOfGroup(board, group) == 2)
                {
                    if (groupId == id)
                        atari_stones_balance -= group.Count / 2;
                    else if (groupId == boardFunctions.GetOpponentId(id))
                        atari_stones_balance += group.Count / 2;
                }

            }

            // 
            return territory_balance + atari_stones_balance * 2;
        }


        /// <summary>
        /// Determine Whether Group is in Mode of Atari or not 
        /// https://en.wikipedia.org/wiki/List_of_Go_terms#Atari
        /// </summary>
        /// <param name="board"></param>
        /// <param name="Group"></param>
        /// <returns></returns>
        public bool IsAtari(Board board, HashSet<Point> Group)
        {
            if (boardFunctions.libertiesOfGroup(board, Group) == 1)
                return true;
            return false;
        }

        /// <summary>
        /// Calculates Amount Of Empty Cells in Each Player's Territory
        /// 
        /// First Argument - Empty Cells in Player1ID's Territory
        /// Second Argument - Empty Cells in Player2ID's Territory
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns>
        /// 2 Sums of Territory, first for PLAYER1, second for PLAYER2
        /// </returns>
        public (int, int) GetTerritory(Board board)
        {
            int territoryCount1 = 0;
            int territoryCount2 = 0;

            for (int x = 0; x < board.columns; x++)
            {
                for (int y = 0; y < board.rows; y++)
                {
                    if (board.Get(x, y) == Go.Properties.PlayerInfo.Default.NULLID)
                    {
                        if (DetermineTerritory(board, x, y) == Go.Properties.PlayerInfo.Default.PLAYER1ID)
                            territoryCount1++;
                        else if (DetermineTerritory(board, x, y) == Go.Properties.PlayerInfo.Default.PLAYER2ID)
                            territoryCount2++;
                    }
                }
            }
            return (territoryCount1, territoryCount2);
        }


        /// <summary>
        /// Determines To Which Player Point is in it's territory
        /// </summary>
        /// <param name="board"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        /// A Player's ID, Either ID1 or ID2
        /// </returns>
        public int DetermineTerritory(Board board, int x, int y)
        {
            /* Territory Is Determined The following Way
             * result = ((influence1 - influence2) / (influence1 + influence2))^3 
             *  
             *  if result is + than ID1's territory
             *  else result is ID2's territory
             */

            (double influence1, double influence2) = AssessInfluence(board, x, y);
            if (Math.Pow((influence1 - influence2) / (influence1 + influence2), 3) > 0)
                return Go.Properties.PlayerInfo.Default.PLAYER1ID;
            else
                return Go.Properties.PlayerInfo.Default.PLAYER2ID;

        }
        double Influence(Board board, Point influencerStone, Point influencedStone)
        {
            /*
             * Influence Formula:
             *  I = 1/a * D * s
             *  
             *  I - is influence
             *  a - is stone attenuation
             *  D - is stone distance
             *  s - is stone strength constant
             */

            /* Out of threshold bounds - distance too large no influence */
            if (Distance(influencerStone, influencedStone) > influenceThreshold)
                return 0;

            int a;
            if ((a = Attenuation(board, influencerStone)) == 0)
                return (double)1 * Distance(influencerStone, influencedStone) * stoneStrength;
            

            return ((double)1 / a) * Distance(influencerStone, influencedStone) * stoneStrength;
        }

        /// <summary>
        /// Calculates Maximum Influence Of Each Player On A Cell/Intersection
        /// </summary>
        /// <param name="b"></param>
        /// <param name="intersection"></param>
        /// <returns>
        /// Argument One (Double) - Maximum Influence Of PLAYER1
        /// Argument Two (Double) - Maximum Influence Of PLAYER2
        /// </returns>
        (double, double) AssessInfluence(Board board, Point point)
        {
            /*  Assess Empty intersection's influence
             *  Run BFS up to 'influenceThreshold' levels, collect max influence
             *  
             */
            double maxID1Influence = 0;
            double maxID2Influence = 0;

            Queue<Point> level = new Queue<Point>();
            HashSet<Point> visited = new HashSet<Point>();

            level.Enqueue(point);
            visited.Add(point);
            while (level.Count > 0)
            {
                Point influencer = level.Dequeue();
                Point[] adjacent = board.GetAdjacent(influencer);
                for (int i = 0; i < adjacent.Length; i++)
                {
                    if (!visited.Contains(adjacent[i]))
                    {
                        visited.Add(adjacent[i]);

                        // Push Stones Into Queue Until Their Distance is 1 Before Threshold,
                        // Next Level Will Reach Threshold And End Search
                        if (Distance(point, adjacent[i]) < influenceThreshold)
                            level.Enqueue(adjacent[i]);

                        if (board.Get(adjacent[i]) == Go.Properties.PlayerInfo.Default.PLAYER1ID)
                        {
                            if (Influence(board, adjacent[i], point) > maxID1Influence)
                                maxID1Influence = Influence(board, adjacent[i], point);
                        }
                        if (board.Get(adjacent[i]) == Go.Properties.PlayerInfo.Default.PLAYER2ID)
                        {
                            if (Influence(board, adjacent[i], point) > maxID2Influence)
                                maxID2Influence = Influence(board, adjacent[i], point);
                        }
                    }
                }
            }

            return (maxID1Influence, maxID2Influence);
        }


        /// <summary>
        /// Calculate Distance Between Two Points
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        int Distance(int x0, int y0, int x1, int y1)
        {
            /* refrence : https://en.wikipedia.org/wiki/Hypotenuse#Calculating_the_hypotenuse*/
            /* √((x0-x1)**2 + (y0-y1)**2) = hypotenuse*/
            return (int)Math.Sqrt(Math.Pow((double)(x0 - x1), 2) + Math.Pow((double)(y0 - y1), 2));
        }

        /// <summary>
        /// Value Of Attenuation Drops The 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int Attenuation(Board board, int x, int y)
        {
            // Validate Input
            global::System.Diagnostics.Debug.Assert(x >= 0 && x < board.columns);
            global::System.Diagnostics.Debug.Assert(y >= 0 && y < board.rows);

            /*int val = 0;
            *//* center = board.columns/2, board.rows/2 *//*

            // Add Distance of X from Center to Val
            if (x > (board.columns / 2))
                val += x - (board.columns / 2);
            else
                val += (board.columns / 2) - x;

            // Add Distance of Y from Center to Val
            if (y > (board.rows / 2))
                val += y - (board.rows / 2);
            else
                val += (board.rows / 2) - y;

            return val;*/

            return Distance(board.rows / 2, board.columns / 2, x, y);
        }


        #region Useful Overrides

        (double, double) AssessInfluence(Board board, int x, int y)
        {
            return AssessInfluence(board, new Point(x, y));
        }
        int Attenuation(Board board, Point p)
        {
            return Attenuation(board, p.X, p.Y);
        }
        int Distance(Point p0, Point p1)
        {
            return Distance(p0.X, p0.Y, p1.X, p1.Y);
        }
        public int DetermineTerritory(Board board, Point cell)
        {
            return DetermineTerritory(board, cell.X, cell.Y);
        }

        #endregion

    }
}

