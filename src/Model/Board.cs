/* Board.cs */


using System.Collections.Generic;
using System.Drawing;

namespace Go.src.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    public class Board
    {
        /// <summary>
        /// 
        /// </summary>
        private Graph<Point, int> board;
        public int rows { get; }
        public int columns { get; }

        /// <summary>
        /// Create Board. Represented By "Graph" of "Point"s type
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;

            board = new Graph<Point, int>();

            /* Generate Rows*Columns Vertices */
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                    board.AddVertex(new Point(x, y), Go.Properties.PlayerInfo.Default.NULLID);
            }


            /* For Every Vertex, Create Up-To 4 Outcoming Edges - LEFT, RIGHT, TOP, BOTTOM */
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    // LEFT
                    /* Check Wether Point is in bounds of Board */
                    if (inbound(x - 1, y))
                        board.AddEdge(new Edge<Point>(new Point(x, y),          // from
                                                      new Point(x - 1, y)));    // to
                    // RIGHT
                    /* Check Wether Point is in bounds of Board */
                    if (inbound(x + 1, y))
                        board.AddEdge(new Edge<Point>(new Point(x, y),          // from
                                                      new Point(x + 1, y)));    // to
                    // TOP
                    /* Check Wether Point is in bounds of Board */
                    if (inbound(x, y - 1))
                        board.AddEdge(new Edge<Point>(new Point(x, y),          // from
                                                      new Point(x, y - 1)));    // to
                    // BOTTOM
                    /* Check Wether Point is in bounds of Board */
                    if (inbound(x, y + 1))
                        board.AddEdge(new Edge<Point>(new Point(x, y),          // from
                                                      new Point(x, y + 1)));    // to

                }
            }

        }

        public Board(Board toCopy)
        {
            this.rows = toCopy.rows;
            this.columns = toCopy.columns;

            this.board = new Graph<Point, int>(toCopy.board);
        }

        public int Get(Point p)
        {
            return board.Get(p);
        }


        public void Set(Point p, int id)
        {
            board.Set(p, id);
        }



        /// <summary>
        /// Check if point - (x,y) is within bounderies of board.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>true if (x,y) within bounderies of board, false if not.</returns>
        public bool inbound(int x, int y)
        {
            return x >= 0 && y >= 0 && x < rows && y < columns;
        }
        public bool inbound(Point p)
        {
            return inbound(p.X, p.Y);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public uint GetLiberties(Point p)
        {
            uint liberties = 0;

            // LEFT
            if (inbound(p.X - 1, p.Y) &&
                board.Get(new Point(p.X - 1, p.Y)) == Go.Properties.PlayerInfo.Default.NULLID)
                liberties++;

            // RIGHT
            if (inbound(p.X + 1, p.Y) &&
                board.Get(new Point(p.X + 1, p.Y)) == Go.Properties.PlayerInfo.Default.NULLID)
                liberties++;

            // TOP
            if (inbound(p.X, p.Y - 1) &&
                board.Get(new Point(p.X, p.Y - 1)) == Go.Properties.PlayerInfo.Default.NULLID)
                liberties++;

            // BOTTOM
            if (inbound(p.X, p.Y + 1) &&
                board.Get(new Point(p.X, p.Y + 1)) == Go.Properties.PlayerInfo.Default.NULLID)
                liberties++;


            return liberties;
        }

        public Board CopyWith(int x, int y, int Id)
        {
            Board copy = new Board(this);
            copy.Set(x, y, Id);
            return copy;
        }


        public void SetAll(HashSet<Point> points, int id)
        {
            foreach (Point p in points)
                Set(p, id);
        }

        public Point[] GetAdjacent(Point p)
        {
            return board.GetAdjacent(p);
        }


        #region Useful Overrides
        public void Set(int x, int y, int id)
        {
            board.Set(new Point(x, y), id);
        }

        public int Get(int x, int y)
        {
            return board.Get(new Point(x, y));
        }
        public Board CopyWith(Point p, int Id)
        {
            return CopyWith(p.X, p.Y, Id);
        }
        #endregion




        public override string ToString()
        {
            int y;
            int x;
            string tostring = "";
            for (x = 0; x < columns; x++)
            {
                for (y = 0; y < rows - 1; y++)
                    tostring += Get(x, y).ToString() + ", ";
                tostring += Get(x, y).ToString() + "\n";
            }

            return tostring;
        }

    }
}
