using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.src.Model
{

    public class Shape
    {
        private short data;
        public int rows { get; }
        public int columns { get; }
        public int score { get; }

        [JsonConstructor]
        public Shape(int score, int[,] shape)
        {
            rows = shape.GetLength(0);
            columns = shape.GetLength(1);

            System.Diagnostics.Debug.Assert(rows * columns <= 16);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    data <<= 1;
                    data += ((byte)shape[i, j]);
                }
            }

            for (int i = rows * columns - 1; i < 15; i++) data <<= 1;
        }
        
        public Shape(int rows, int columns, short data)
        {
            this.rows = rows;
            this.columns = columns;
            this.data = data;
        }

        public int Compare(Shape shape)
        {
            System.Diagnostics.Debug.Assert(shape != null);
            System.Diagnostics.Debug.Assert(shape.rows <= this.rows);
            System.Diagnostics.Debug.Assert(shape.columns <= this.columns);
            System.Diagnostics.Debug.Assert(shape.rows == this.rows || shape.columns == this.columns);

            short adjustedData = this.data;
            for (int i = shape.rows * shape.columns; i < this.rows * this.columns; i++) adjustedData <<= 1;


            int signCounter = 0;
            adjustedData ^= shape.data;

            while (adjustedData != 0)
            {
                if ((adjustedData & 0b1000000000000000) != 0)
                    signCounter++;
                adjustedData <<= 1;
            }

            return signCounter;
        }

        public override string ToString()
        {
            return Convert.ToString(data, 2).PadLeft(16, '0');
        }

        public void Replace(short data)
        {
            this.data = data; 
        }

    }

}
