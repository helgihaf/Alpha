using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Drawing3D
{
    public struct Vertex
    {
        public static Vertex Zero = new Vertex();

        public float X;
        public float Y;
        public float Z;

        public Vertex(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vertex(Vertex copyFrom)
        {
            this.X = copyFrom.X;
            this.Y = copyFrom.Y;
            this.Z = copyFrom.Z;
        }

        public Vertex(Matrix matrix)
        {
            if (matrix.Columns == 3 && matrix.Rows == 1)
            {
                X = (float)matrix[0, 0];
                Y = (float)matrix[0, 1];
                Z = (float)matrix[0, 2];
            }
            else if (matrix.Columns == 1 && matrix.Rows == 3)
            {
                X = (float)matrix[0, 0];
                Y = (float)matrix[1, 0];
                Z = (float)matrix[2, 0];
            }
            else
            {
                throw new ArgumentException("Matrix must be 3x1 or 1x3");
            }
        }

        public void Add(Vertex other)
        {
            X += other.X;
            Y += other.Y;
            Z += other.Z;
        }

        public static Vertex operator +(Vertex a, Vertex b)
        {
            return new Vertex { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };
        }

        public static Vertex operator -(Vertex a, Vertex b)
        {
            return new Vertex { X = a.X - b.X, Y = a.Y - b.Y, Z = a.Z - b.Z };
        }

        /// <summary>
        /// The cross product of a and b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vertex operator *(Vertex a, Vertex b)
        {
            return new Vertex { X = a.Y * b.Z - a.Z * b.Y, Y = a.Z * b.X - a.X * b.Z, Z = a.X * b.Y - a.Y * b.X };
        }

        public Vertex ToInverse()
        {
            return new Vertex { X = -this.X, Y = -this.Y, Z = -this.Z };
        }

        public Matrix ToSingleRowMatrix()
        {
            return new Matrix(new double[,] { { X, Y, Z } });
        }
    }
}
