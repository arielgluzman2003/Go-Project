/* BitBoard.cs */


using System;


namespace Go.src.Model
{

    public class BitBoard : IComparable
    {
        /// <summary>
        /// Bit-Board explained
        /// </summary>
        /// 

        public int rows { get; }
        public int columns { get; }

        private ulong board;         // 64 bits representing a board of max size 8X8

        public BitBoard(int rows, int columns)
        {
            #region input validation
            // Validating that size of 'board' (ulong) is not exceeded by the product of 'rows' X 'columns'
            global::System.Diagnostics.Debug.Assert(System.Runtime.InteropServices.Marshal.SizeOf(board) * 8 < rows * columns);
            #endregion
            this.rows = rows;
            this.columns = columns;
        }

        // Copy Constructor
        public BitBoard(BitBoard copy)
        {
            this.rows = copy.rows;
            this.columns = copy.columns;
            this.board = copy.board;
        }
        
        public BitBoard(int rows, int columns, ulong board): this(rows, columns)
        {
            this.board = board;
        }

        public BitBoard(bool[][] shape):this(shape.Length, shape[0].Length)
        {
            for(int x = 0; x < rows; x++)
            {
                for(int y = 0; y < columns; y++)
                {
                    Set(x, y, shape[x][y]);
                }
            }
        }

        /// <summary>
        /// Fetches value of cell/bit at coordinates (x,y).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>
        /// 1 if cell/bit is taken/set, 0 if not.
        /// </returns>
        public int Get(int x, int y)
        {
            /// y is rows, x is columns
            if ((board & GenerateMask(y, x)).Equals(0))
                return 0;
            else
                return 1;
        }

        /// <summary>
        /// Sets a value of cells/bit at coordinates (x,y).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="content">boolean value - true/false meaning to set 1 or 0.</param>
        /// <param name="leave_empty">*Unused</param>
        public void Set(int x, int y, bool content, int leave_empty = 0)
        {
            /// Bit is to be set 1
            if (content == true)
            {
                /// Bit is not already set 1
                if (Get(x, y) == 0)
                {
                    board |= GenerateMask(y, x);
                }
            }
            /// Bit is to be set 0
            else
            {
                /// Bit is not already set 0
                if (Get(x, y) == 1)
                {
                    board ^= GenerateMask(y, x);
                }
            }
        }

        /// <summary>
        /// Creates a mask with 1 bit set at (col,row)<br/>
        /// Example:<br/>
        ///     for a 4 by 4 board and col=2 row=1<br/>
        ///     generated mask is 0000001000000000....0<br/>
        ///     which looks like:<br/>
        ///
        ///             0  0  0  0<br/>
        ///             0  0  1  0<br/>
        ///             0  0  0  0<br/>
        ///             0  0  0  0
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>1 bit set mask as shown above.</returns>
        /// <exception cref="ArgumentException"></exception>
        private ulong GenerateMask(int row, int col)
        {
            // Validate Arguments are in bounds
            global::System.Diagnostics.Debug.Assert(row < 0 || row > this.rows || col < 0 || col > this.columns);
            
            // Fix Arguments
            row = Math.Abs(row) % this.rows;
            col = Math.Abs(col) % this.columns;


            /// To Shift Left = SIZE - (columns x row + column) - 1
            return (ulong)1 << 64 - (this.columns * row + col) - 1;
        }

        /// <summary>
        /// Merges Current BitBoard With Another BitBoard,<br/>
        /// Equivalent to OR operator or Union in Set-Theory
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Unioned Bit-Board</returns>
        public BitBoard Union(BitBoard other)
        {
            BitBoard union = new BitBoard(this);
            union.board |= other.board;
            return union;
        }

        /// <summary>
        /// Keeps only the mutualities between two bitboards,<br/>
        /// Equivalent to AND operator or Intersection in Set-Theory
        /// </summary>
        /// <param name="other"></param>
        /// <return>Intersectioned BitBoard</return>
        public BitBoard Intersection(BitBoard other)
        {
            BitBoard intersection = new BitBoard(this);
            intersection.board &= other.board;
            return intersection;
        }

        /// <summary>
        /// Union of two bit-boards with exclusion of the mutualities,<br/>
        /// Equivalent to X-OR Operator or SymmetricDifference in Set-Theory 
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Symmetrically Different Bit-Board</returns>
        public BitBoard SymmetricDifference(BitBoard other)
        {
            BitBoard symmetricDifference = new BitBoard(this);
            symmetricDifference.board ^= other.board;
            return symmetricDifference;
        }


        /// <summary>
        /// If All 'obj'/'other's Set Bits Match, Equal
        /// Get Difference from self to obj
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            // Can Only Compare With Another BitBoard
            global::System.Diagnostics.Debug.Assert(obj is BitBoard);

            int counter = 0;

            BitBoard other = (BitBoard)obj;

            ulong result = (this.board | other.board) - this.board;

            while(result > 0)
            {
                result >>= 1;
                counter++;
            }
            return counter;
        }

        #region ToString
        /// <summary>
        /// Get a board-print.
        /// </summary>
        /// <returns>
        /// String of board represented with 0s and 1s, 1 for taken cells 0 for non taken.
        /// </returns>
        public override string ToString()
        {
            return Convert.ToString((long)board, 2);
        }

        /// <summary>
        /// Get a visual board-print in a matrix-like representation.
        /// </summary>
        /// <returns>
        /// String of board represented with 0s and 1s, 1 for taken cells 0 for non taken.
        /// </returns>
        public string ToStringMatrix()
        {
            string s = "";
            int i, j;
            for (i = 0; i < columns; i++)
            {
                for (j = 0; j < rows; j++)
                {
                    s += Get(i, j) == 1 ? "1" : "0";
                    s += " ";
                }
                s += "\n";
            }
            return s;
        }

        
        #endregion

        #region Getters/Setters

        #endregion
    }
}