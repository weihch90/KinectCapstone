﻿using MathWorks.MATLAB.NET.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibOmp;
using csmatio.io;
using csmatio.types;

namespace WindowsFormsApplication1
{
    class ImageFeature
    {
        private MWArray dic;

        public ImageFeature()
        {
            MatFileReader mfr = new MatFileReader(@"C:\Users\Administrator\Kinect\homp_release\rgbd_dic_16x16_depth.mat");
            MLDouble ml = mfr.Content["dic"] as MLDouble;
            int[] dimension = ml.Dimensions;
            double[][] mlArray = ml.GetArray();
            MWArray array = new MWNumericArray(dimension[0], dimension[1], flatenArray(mlArray, dimension[0], dimension[1]));
            this.dic = array;
        }


        public void createImageFeature() 
        {
            MWArray myArray = new MWNumericArray(25, 25);
            MWStructArray fea_first = new MWStructArray(1, 1, new string[] { "feapath", "type", "maxsize", "savedir" });
            MWStructArray dic_first = new MWStructArray(1, 1, new string[] { "dicsize", "patchsize", "samplenum", "dic" });
            MWStructArray encoder_first = new MWStructArray(1, 1, new string[] { "coding", "pooling", "sparsity" });
            MWStructArray fea_final = new MWStructArray(1, 1, new string[] { "feapath" });
            MWStructArray encoder_final = new MWStructArray(1, 1, new string[] { "coding", "pooling", "patchsize" });
            MWCellArray feapath1 = new MWCellArray("../../sampleImages/good/good_1/good_1_depthcrop.png");
            MWCellArray feapath2 = new MWCellArray("./features/rgbdhomp_ksvd_first_16x16_fea_first/000001.mat");


            fea_first["feapath", 1] = "sampleImages/good/good_1/good_100_depthcrop.png"; // relative to bin/Debug/
            fea_first["type", 1] = "depth";
            fea_first["maxsize", 1] = 150;
            fea_first["savedir", 1] = "./features/rgbdhomp_ksvd_first_16x16_fea_first.type/";

            dic_first["dicsize", 1] = 500;
            dic_first["patchsize", 1] = 16;
            dic_first["samplenum", 1] = 100;
            dic_first["dic", 1] = this.dic;

            encoder_first["coding", 1] = "omp";
            encoder_first["pooling", 1] = 4;
            encoder_first["sparsity", 1] = 4;

            fea_final["feapath", 1] = feapath2;

            encoder_final["coding", 1] = "omp";
            encoder_final["pooling", 1] = new MWNumericArray(1, 3, new int[]{1, 2, 3});
            encoder_final["patchsize", 1] = 1;

            LibOmp.LibOmp omp = new LibOmp.LibOmp();

            omp.omp_pooling_layer1_batch(fea_first, dic_first, encoder_first);
            MWArray rgbdfea = omp.omp_pooling_final_batch_single(fea_final, encoder_final); // 2D ushort array
            //ushort[] fea_vector = new ushort[rgbdfea.Dimensions[0]];
            Array fea_vector = rgbdfea.ToArray();
            for (int i = 0; i < 100; i++)
                for(int j = 0; j < rgbdfea.Dimensions[1]; j++)
                    Console.WriteLine(fea_vector.GetValue(i, j));
        }

        private double[] flatenArray(double[][] array, int height, int width)
        {
            double[] flaten = new double[height * width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    flaten[width*i+j] = array[i][j];
            return flaten;
        }

    }
}
