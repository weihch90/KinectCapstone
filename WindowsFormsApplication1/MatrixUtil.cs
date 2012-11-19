using libsvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureStudio
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

        public static double[] FlattenMatrix(List<double[]> list)
        {
            int width = list.Count; // 50
            int height = list[0].Length; // 2800

            double[] flatten = new double[height * width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    flatten[i * width + j] = list[j][i];

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

        public static double[][] AppendToMatrix(double[][] matrix, List<double[]> data)
        {
            double[][] result = new double[matrix.Length + data.Count][];
            int dataLength = data.ElementAt(0).Length;
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = new double[dataLength];
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    result[i][j] = matrix[i][j];
                }
            }

            for(int i = 0; i < data.Count; i++)
            {
                result[i + matrix.Length] = new double[dataLength];
                for(int j = 0; j < dataLength; j++)
                {
                    double[] vector = data.ElementAt(i);
                    result[i + matrix.Length][j] = vector[j];
                }
            }
            return result;
        }

        public static double[] EnlargeLabelArray(double[] label, int addOn)
        {
            double[] result = new double[label.Length + addOn];
            for (int i = 0; i < label.Length; i++)
                result[i] = label[i];

            double newLabel = label[label.Length - 1] + 1;
            for (int i = label.Length; i < result.Length; i++)
                result[i] = newLabel;

            return result;
        }
    }
}
