﻿using MathWorks.MATLAB.NET.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csmatio.io;
using csmatio.types;
using System.Diagnostics;

namespace GestureStudio
{
    class ImageFeature
    {
        private double[][] dic;
        private int[] dict_dimension;
        LibOmp.LibOmp omp;

        public ImageFeature(string dictionaryPath)
        {
            this.dic = LoadDict(dictionaryPath);
            this.omp = new LibOmp.LibOmp();
        }

        private double[][] LoadDict(string dictionaryPath)
        {
            // Read in dictionary.
            MatFileReader mfr = new MatFileReader(dictionaryPath);
            MLDouble ml = mfr.Content["dic"] as MLDouble;
            this.dict_dimension = ml.Dimensions;
            return ml.GetArray();
        }

        // Generate the feature vector of the given image.
        public double[] GenerateFeature(short[,] pixelData)
        {
            // Initialize the parameters of feature (data).
            MWStructArray fea_first = new MWStructArray(1, 1, new string[] { "pixels", "maxsize" });
            fea_first.SetField("pixels", new MWNumericArray(pixelData)); //"../../sampleImages/good/good_1/good_100_depthcrop.png"
            fea_first.SetField("maxsize", 100);

            // Initialize the parameters of dictionary.
            MWStructArray dic_first = new MWStructArray(1, 1, new string[] { "dicsize", "patchsize", "samplenum", "dic" });
            dic_first.SetField("dicsize", 200);
            dic_first.SetField("patchsize", 16);
            dic_first.SetField("samplenum", 100);
            MWArray array = new MWNumericArray(this.dict_dimension[0], this.dict_dimension[1], MatrixUtil.FlattenMatrix(this.dic, this.dict_dimension[0], this.dict_dimension[1]));
            dic_first.SetField("dic", array);

            // Orthogonal matching pursuit encoder
            Stopwatch ompTimer = Stopwatch.StartNew();

            MWArray rgbdfea = omp.extract_feature(fea_first, dic_first);

            ompTimer.Stop();

            Array feature = rgbdfea.ToArray();
            Console.WriteLine("Feature Extraction Time: {0} ms", ompTimer.ElapsedMilliseconds);

            double[] feature_vector = new double[feature.Length];
            for (int i = 0; i < feature.Length; i++)
                feature_vector[i] = System.Convert.ToDouble(feature.GetValue(i, 0));

            return feature_vector;
        }
    }
}
