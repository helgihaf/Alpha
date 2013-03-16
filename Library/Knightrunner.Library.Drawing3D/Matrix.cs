using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.Library.Drawing3D
{
    //public class Matrix
    //{
    //    private float[,] matrix;

    //    public Matrix(int rows, int columns)
    //    {
    //        Rows = rows;
    //        Columns = columns;
    //        matrix = new float[rows, columns];
    //    }

    //    public Matrix(float[,] matrix)
    //    {
    //        this.matrix = matrix;
    //        Rows = matrix.GetLength(0);
    //        Columns = matrix.GetLength(1);
    //    }


    //    public int Rows { get; private set; }
    //    public int Columns { get; private set; }

    //    public float this[int row, int col]
    //    {
    //        get { return matrix[row, col]; }
    //        set { matrix[row, col] = value; }
    //    }


    //    public static Matrix operator *(Matrix a, Matrix b)
    //    {
    //        if (a.Columns != b.Rows)
    //            throw new ArgumentException("Cannot multply mismatching column and rows");

    //        Matrix result = new Matrix(a.Rows, a.Columns);
    //        for (int i = 0; i < a.Rows; i++)
    //        {
    //            for (int j = 0; j < a.Columns; j++)
    //            {
    //                result[i, j] = DoMultiply(a, b, i, j);
    //            }
    //        }

    //        return result;
    //    }

    //    private static float DoMultiply(Matrix a, Matrix b, int row, int col)
    //    {

    //        float value = 0;
    //        for (int i = 0; i < a.Columns; i++)
    //        {
    //            value += a[row, i] * b[col, i];
    //        }

    //        return value;
    //    }
    //}

    public class Matrix
    {
        private double[,] matrix;

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            matrix = new double[rows, columns];
        }

        public Matrix(double[,] matrix)
        {
            this.matrix = matrix;
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);
        }


        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public double this[int row, int col]
        {
            get { return matrix[row, col]; }
            set { matrix[row, col] = value; }
        }


        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.Columns != b.Rows)
                throw new ArgumentException("Cannot multply mismatching column and rows");

            Matrix result = new Matrix(a.Rows, a.Columns);
            for (int i = 0; i < a.Rows; i++)
            {
                for (int j = 0; j < a.Columns; j++)
                {
                    result[i, j] = DoMultiply(a, b, i, j);
                }
            }

            return result;
        }

        private static double DoMultiply(Matrix a, Matrix b, int row, int col)
        {
            double value = 0;
            for (int i = 0; i < a.Columns; i++)
            {
                value += a[row, i] * b[col, i];
            }

            return value;
        }
    }

}
