/* BoardFunctions.cs */

using System;
using System.Collections.Generic;
using System.Drawing;


namespace Go.src.Model
{

    public class BoardFunctions
    {
        public HashSet<(HashSet<Point>, int)> CheckCaptures(Board board)
        {
            HashSet<(HashSet<Point>, int)> Captured = new HashSet<(HashSet<Point>, int)> ();

            for (int x = board.columns - 1; x >= 0; x--)
            {
                for (int y = board.rows - 1; y >= 0; y--)
                {
                    HashSet<Point> involvedVertices;
                    if (Surrounded(board, x, y, out involvedVertices))
                    {
                        Captured.Add((filter(board, involvedVertices, board.Get(x, y)), board.Get(x, y)));
                        board.SetAll(filter(board, involvedVertices, board.Get(x, y)), Go.Properties.PlayerInfo.Default.NULLID);
                    }
                }
            }
            
            return Captured;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool isSuicideMove(Board board, Point p)
        {
            HashSet<Point> involvedVertices;
            int playerId = board.Get(p);
            int opponentId = GetOpponentId(playerId);
            bool result = Surrounded(board, p, out involvedVertices);
            if (result == false)
                return false;
            else
            {
                foreach (Point vertex in involvedVertices)
                {
                    if (board.Get(vertex) == opponentId)
                    {
                        result = Surrounded(board, vertex);
                        if (result == true)
                            return false;
                    }
                }
                return true;
            }

        }

        public int libertiesOfGroup(Board board, HashSet<Point> Group)
        {
            HashSet<Point> liberties = new HashSet<Point>();

            foreach (Point vertex in Group)
            {
                foreach(Point adjacent in board.GetAdjacent(vertex))
                {
                    if(board.Get(adjacent) == Go.Properties.PlayerInfo.Default.NULLID)
                    {
                        if(!liberties.Contains(adjacent))
                            liberties.Add(adjacent);
                    }
                }
            }

            return liberties.Count;
        }

        public BitBoard GetFrame(Board board, int x, int y, int width, int height, int id)
        {
            BitBoard frame = new BitBoard(height, width);
            for(int cell_x = x; cell_x <= x + width; cell_x++)
            {
                for(int cell_y = y; cell_y <= y + height; cell_y++)
                    frame.Set(cell_x, cell_y, board.Get(cell_x, cell_y) == id);                    
            }

            return frame;
        }

        /// <summary>
        /// Finds Out If Point 'start' is Surrounded by Opponent Stones.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="start"></param>
        /// <param name="visited">[Out] Set Of Stones Which Were Involved in Search</param>
        /// <returns>
        /// True if Stone 'start' is Surrounded,
        /// False if not.
        /// </returns>
        public bool Surrounded(Board board, Point start, out HashSet<Point> visited)
        {
            /*
             *  Implements BFS, 
             *      Visited Stones Will Be Returned Unfiltered
             *      Returns False:
             *          When Reached Friendly Adjacent Stone
             *          Or When Reached Empty Adjacent Stone
             *      
             *      Returns True:
             *          When None Of False-Returning Conditions Have Been Met
             */
            int startID = board.Get(start);
            int opponentID = GetOpponentId(startID);
            visited = new HashSet<Point>();
            Queue<Point> level = new Queue<Point>();
            visited.Add(start);
            level.Enqueue(start);
            while (level.Count != 0)
            {
                Point next = level.Dequeue();
                foreach (Point p in board.GetAdjacent(next))
                {
                    if (!visited.Contains(p))
                    {
                        visited.Add(p);
                        if (board.Get(p) == startID)
                        {
                            if (board.GetLiberties(p) == 0)
                                level.Enqueue(p);
                            else
                                return false;
                        }
                        else if (board.Get(p) == Go.Properties.PlayerInfo.Default.NULLID)
                        {
                            return false;
                        }
                        /*  */
                        else if (board.Get(p) == opponentID)
                        {
                            continue;
                        }
                    }
                }
            }

            return true;
        }

        int findLongestWall(Board board, int id)
        {
            #region validate input
            if (id != Go.Properties.PlayerInfo.Default.PLAYER1ID && id != Go.Properties.PlayerInfo.Default.PLAYER2ID)
                throw new Exception();
            #endregion

            for (int x = 0; x < board.columns; x++)
            {
                for (int y = 0; y < board.rows; y++)
                {

                }
            }

            return 0;
        }

        public int GetOpponentId(int id)
        {
            /* If ID is of 'Player 1' return ID of 'Player 2' */
            if (id == Go.Properties.PlayerInfo.Default.PLAYER1ID)
                return Go.Properties.PlayerInfo.Default.PLAYER2ID;

            /* If ID is of 'Player 2' return ID of 'Player 1' */
            else if (id == Go.Properties.PlayerInfo.Default.PLAYER2ID)
                return Go.Properties.PlayerInfo.Default.PLAYER1ID;

            /* If no ID matched, return ID of Empty-Cell */
            else
                return Go.Properties.PlayerInfo.Default.PLAYER2ID;

        }



        public bool isCellEmpty(Board board, Point move)
        {
            return board.Get(move) != Go.Properties.PlayerInfo.Default.NULLID;
        }

        public bool cellIn(IEnumerable<Point> group, Point move)
        {
            foreach (Point cell in group)
            {
                if (move.Equals(cell))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Filters Any Cells With ID Different to 'id'
        /// </summary>
        /// <param name="board"></param>
        /// <param name="rawSet"></param>
        /// <param name="id"></param>
        /// <returns>New HashSet Equal or Smaller Than 'rawSet', exclusive to 'id'.</returns>
        public HashSet<Point> filter(Board board, HashSet<Point> rawSet, int id)
        {
            HashSet<Point> filtered = new HashSet<Point>();
            foreach (Point p in rawSet)
            {
                if (board.Get(p) == id)
                    filtered.Add(p);
            }
            return filtered;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public HashSet<(HashSet<Point>, int)> GetGroups(Board board)
        {
            /*
             *
             */

            HashSet<(HashSet<Point>, int)> Groups = new HashSet<(HashSet<Point>, int)>();
            for(int x = 0; x < board.columns; x++)
            {
                for(int y = 0; y < board.rows; y++)
                {
                    if(board.Get(x,y) != Go.Properties.PlayerInfo.Default.NULLID)
                    {
                        HashSet<Point> Group = GetGroup(board, x, y);
                        Groups.Add((Group, board.Get(x, y)));
                        board.SetAll(Group, Go.Properties.PlayerInfo.Default.NULLID);
                    }
                }
            }

            return Groups;
        }

        /// <summary>
        /// Get Group That Starts in 'Cell',
        /// Group (Official 'Go' Term is "String") is a Group of 
        /// Adjacent Stones That Share the Same ID, i.e. Are All White Or Are All Black.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public HashSet<Point> GetGroup(Board board, Point cell)
        {
            /*
             * Simple BFS Which Checks Returns Adjacent Friendly Stones, i.e. A Group (Official 'Go' Term is "String")
             */

            int id = board.Get(cell);
            global::System.Diagnostics.Debug.Assert(id != Go.Properties.PlayerInfo.Default.NULLID);
            int opponentId = GetOpponentId(id);
            HashSet<Point> Group = new HashSet<Point>();

            Queue<Point> level = new Queue<Point>();
            HashSet<Point> visited = new HashSet<Point>();
            level.Enqueue(cell);
            visited.Add(cell);
            while(level.Count > 0)
            {
                Point next = level.Dequeue();
                Group.Add(next);
                Point[] adjacent = board.GetAdjacent(next);
                foreach (Point adjacentPoint in adjacent)
                {
                    if (!visited.Contains(adjacentPoint))
                    {
                        visited.Add(adjacentPoint);
                        if (board.Get(adjacentPoint) == id)
                            level.Enqueue(adjacentPoint);
                    }
                }
            }

            return Group;
        }



        #region Useful Overrides

        public bool Surrounded(Board board, int x, int y, out HashSet<Point> territory)
        {
            return Surrounded(board, new Point(x, y), out territory);
        }

        public bool Surrounded(Board board, Point start)
        {
            HashSet<Point> involvedVertices = new HashSet<Point>();
            return Surrounded(board, start, out involvedVertices);
        }

        public HashSet<Point> GetGroup(Board board, int x, int y)
        {
            return GetGroup(board, new Point(x,y));
        }

        #endregion

    }
}
