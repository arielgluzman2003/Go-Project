using System;
using System.Collections.Generic;
using System.Drawing;

namespace Go.src.Model
{
    public class MonteCarlo
    {
        /// <summary>
        /// Reducing Function(int input) - returns > int output
        /// 
        /// Requirments:
        ///     output <= input
        ///     
        /// </summary>
        private Func<int, int> reducing_function;
        
        public MonteCarlo(Func<int, int> reducing_function)
        {
            this.reducing_function = reducing_function;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="id"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
 /*       public Board GenerateTree(Board board, int id, int depth)
        {
            if (depth == 0)
                return;


            Random random = new Random();
            int possible_moves_count = 0;
            //Get Amount of Clear Board Cells
            for (int x = 0; x < board.columns; x++)
            {
                for (int y = 0; y < board.rows; y++)
                {
                    if (board.Get(x, y) == Go.Properties.PlayerInfo.Default.NULLID)
                        possible_moves_count++;
                }
            }

            // Make Sure Board Has Move To Generate
            global::System.Diagnostics.Debug.Assert(possible_moves_count > 0);


            // Cut Moves By Some Value
            int moves_to_generate = reducing_function(possible_moves_count);

            // Set Of Random Moves with Length of 'moves_to_generate'
            HashSet<Point> moves = new HashSet<Point>();

            while (moves.Count <= moves_to_generate)
            {
                int rand_x = random.Next(board.columns);
                int rand_y = random.Next(board.rows);

                if (board.Get(rand_x, rand_y) == Go.Properties.PlayerInfo.Default.NULLID)
                {
                    if (!moves.Contains(new Point(rand_x, rand_y)))
                        moves.Add(new Point(rand_x, rand_y));
                }
            }



 */      // }
    }



}
