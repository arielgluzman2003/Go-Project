/* GraphicBoard.cs */

using System.Drawing;
using System;
using Newtonsoft.Json;

namespace Go.src.View
{


    /// <summary>
    /// The Settings of each board are stored inside a JSON file called "/BoardPositions.json"
    /// These values are Deserialized into an object GraphicBoard
    /// 
    /// GraphicBoard can take these values and produce calculations
    /// of cell position in actual pixel coordinated on the screen
    /// 
    /// The Serialization takes place in "/Program.cs" using Newtonsoft.Json library.
    /// </summary>
    public class GraphicBoard
    {
        public string id { get; }
        public int rows { get; }
        public int columns { get; }
        private int[] initialPosition;
        private int horizontalSpacing;
        private int verticalSpacing;
        

        [JsonConstructor]
        public GraphicBoard(string id, int[] initialPosition, int horizontalSpacing, int verticalSpacing, int rows, int columns)

        {
            #region Input Validation
            if (initialPosition.Length != 2)
                throw new JsonException(String.Format(""));
            #endregion

            this.id = id;
            this.initialPosition = initialPosition;
            this.horizontalSpacing = horizontalSpacing;
            this.verticalSpacing = verticalSpacing;
            this.rows = rows;
            this.columns = columns;
        }


        /// <summary>
        /// Converts Raw Pixel Coordiantes Into Cell (row, columns)
        /// </summary>
        /// <param name="p">Raw Pixel Coordinates Recieved From a Mouse Click Event.</param>
        /// <returns>Cell in Board</returns>
        public Point GetBoardPosition(Point p)
        {
            int x = p.X / horizontalSpacing;
            int y = p.Y / verticalSpacing;

            return new Point(x, y);
        }

        /// <summary>
        /// Takes Cell in Board and Produces Pixel Coordinates in Board.
        /// </summary>
        /// <param name="p">Cell in Board</param>
        /// <returns>Pixel Coordiantes in Board.</returns>
        public Point GetCoordinates(int x, int y)
        {
            return new Point(initialPosition[0]/2 + (horizontalSpacing * x),
                initialPosition[1]/2 + (verticalSpacing * y));
        }

        public Point GetCoordiantes(Point p)
        {
            return GetCoordinates(p.X, p.Y);
        }

        public override string ToString()
        {
            return String.Format("rows: {0:d}, columns: {1:d}\ninitialPosition: {2:d}, {3:d}\nhorizontalSpacing: {4:d}\nverticalSpacing: {5:d}",
                rows, columns, initialPosition[0], initialPosition[1], horizontalSpacing, verticalSpacing);

        }
    }
}
