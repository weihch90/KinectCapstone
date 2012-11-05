﻿using MathWorks.MATLAB.NET.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibOmp;
using csmatio.io;
using csmatio.types;
using libsvm;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            SvmModelBuilder modelBuilder = new SvmModelBuilder();

            string modelFileName = "model.svm";
            if (!modelBuilder.LoadFromFile(modelFileName))
            {
                // first time usage, train from feature file
                string problemFile = @"D:\UW\2012 Autumn\CSE 481\Kinect Capstone\WindowsFormsApplication1\WindowsFormsApplication1\rgbdfea_depth_first2.mat";
                Console.WriteLine("Training new model from {0}.", problemFile);
                modelBuilder.TrainModel(problemFile);

                Console.WriteLine("Trainning finished. Saving Model as {0}", modelFileName);
                modelBuilder.SaveModel(modelFileName);
            }

            Console.WriteLine("Model Ready.");

            ImageFeature imgFeature = new ImageFeature();

            short[,] imagePixels = new Png16Reader().Read("../../sampleImages/good/good_1/good_101_depthcrop.png");
            double[] vector = imgFeature.GenerateFeature(imagePixels);


            double category = svm.svm_predict(modelBuilder.GetModel(), MatrixUtil.DoubleToSvmNode(vector));

            Console.WriteLine(category);

            Console.ReadKey();
        }
    }
}
