using libsvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public static class MatrixUtil
    {
        public static T[][] Transpose<T>(T[][] input)
        {
            T[][] result = new T[input[0].Length][];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = new T[input.Length];
                for (int j = 0; j < input.Length; ++j)
                {
                    result[i][j] = input[j][i];
                }
            }

            return result;
        }

        public static svm_node[] DoubleToSvmNode(double[] data)
        {
            return data.Select((d, i) => d != 0 ?
                new svm_node()
                {
                    index = i,
                    value_Renamed = d
                } : null).Where(node => node != null).ToArray();
        }

        public static double[] FlattenMatrix(double[][] matrix, int height, int width)
        {
            double[] flatten = new double[height * width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    flatten[width * i + j] = matrix[i][j];
            return flatten;
        }

        public static T[,] RawFrameTo2D<T>(T[] matrix, int height, int width)
        {
            T[,] result = new T[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    result[i, j] = matrix[i * width + j];

            return result;
        }


        public static short[,] ShortToShort2D(short[] matrix, int height, int width)
        {
            short[,] result = new short[height,width];
            for(int i = 0; i < height; i++)
                for(int j = 0; j < width; j++)
                    result[i,j] = (short)matrix[i * width + j];
            return result;
        }
    }
}
