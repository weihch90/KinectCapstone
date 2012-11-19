using csmatio.io;
using csmatio.types;
using libsvm;
using System.IO;
using System.Collections.Generic;
using RgbdfeaLib;
﻿using MathWorks.MATLAB.NET.Arrays;
using System.Linq;

namespace GestureStudio
{
    /// <summary>
    /// this is the SVM interface
    /// </summary>
    class SvmModelBuilder
    {
        private svm_model model;

        public SvmModelBuilder()
        {
            this.model = null;
        }

        public svm_model GetModel()
        {
            return this.model;
        }

        public bool LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                using (BinaryReader r = new BinaryReader(fs))
                {
                    this.model = new svm_model();

                    svm_parameter p = new svm_parameter();
                    p.C = r.ReadDouble();
                    p.cache_size = r.ReadDouble();
                    p.coef0 = r.ReadDouble();
                    p.degree = r.ReadDouble();
                    p.eps = r.ReadDouble();
                    p.gamma = r.ReadDouble();
                    p.kernel_type = r.ReadInt32();
                    p.nr_weight = r.ReadInt32();
                    p.nu = r.ReadDouble();
                    p.p = r.ReadDouble();
                    p.probability = r.ReadInt32();
                    p.shrinking = r.ReadInt32();
                    p.svm_type = r.ReadInt32();
                    p.weight = ReadDoubleArray(r);
                    p.weight_label = ReadIntArray(r);

                    this.model.param = p;
                    this.model.nr_class = r.ReadInt32();
                    this.model.l = r.ReadInt32();
                    this.model.SV = ReadSvmNodeArray(r);
                    this.model.sv_coef = ReadDouble2DArray(r);
                    this.model.rho = ReadDoubleArray(r);
                    this.model.probA = ReadDoubleArray(r);
                    this.model.probB = ReadDoubleArray(r);
                    this.model.label = ReadIntArray(r);
                    this.model.nSV = ReadIntArray(r);

                    return true;
                }
            }

            this.model = null;
            return false;
        }

        public void SaveModel(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            using (BinaryWriter w = new BinaryWriter(fs))
            {
                svm_parameter p = model.param;
                w.Write(p.C);
                w.Write(p.cache_size);
                w.Write(p.coef0);
                w.Write(p.degree);
                w.Write(p.eps);
                w.Write(p.gamma);
                w.Write(p.kernel_type);
                w.Write(p.nr_weight);
                w.Write(p.nu);
                w.Write(p.p);
                w.Write(p.probability);
                w.Write(p.shrinking);
                w.Write(p.svm_type);
                WriteArray(w, p.weight);
                WriteArray(w, p.weight_label);

                w.Write(model.nr_class);
                w.Write(model.l);
                WriteArray(w, model.SV);
                WriteArray(w, model.sv_coef);

                WriteArray(w, model.rho);
                WriteArray(w, model.probA);
                WriteArray(w, model.probB);

                WriteArray(w, model.label);
                WriteArray(w, model.nSV);
            }

            fs.Close();
        }

        private double[][] ReadDouble2DArray(BinaryReader reader)
        {
            bool isNull = !reader.ReadBoolean();
            if (isNull)
            {
                return null;
            }
            else
            {
                int length = reader.ReadInt32();
                double[][] array = new double[length][];
                for (int i = 0; i < length; i++)
                {
                    int sub_length = reader.ReadInt32();
                    array[i] = new double[sub_length];

                    for (int j = 0; j < sub_length; j++)
                    {
                        array[i][j] = reader.ReadDouble();
                    }
                }
                return array;
            }
        }

        private svm_node[][] ReadSvmNodeArray(BinaryReader reader)
        {
            bool isNull = !reader.ReadBoolean();
            if (isNull)
            {
                return null;
            }
            else
            {
                int length = reader.ReadInt32();
                svm_node[][] array = new svm_node[length][];
                for (int i = 0; i < length; i++)
                {
                    int sub_length = reader.ReadInt32();
                    array[i] = new svm_node[sub_length];

                    for (int j = 0; j < sub_length; j++)
                    {
                        svm_node node = new svm_node();
                        node.index = reader.ReadInt32();
                        node.value_Renamed = reader.ReadDouble();
                        array[i][j] = node;
                    }
                }
                return array;
            }
        }

        private int[] ReadIntArray(BinaryReader reader)
        {
            bool isNull = !reader.ReadBoolean();
            if (isNull)
            {
                return null;
            }
            else
            {
                int length = reader.ReadInt32();
                int[] array = new int[length];
                for (int i = 0; i < length; i++)
                {
                    array[i] = reader.ReadInt32();
                }
                return array;
            }
        }

        private double[] ReadDoubleArray(BinaryReader reader)
        {
            bool isNull = !reader.ReadBoolean();
            if (isNull)
            {
                return null;
            }
            else
            {
                int length = reader.ReadInt32();
                double[] array = new double[length];
                for (int i = 0; i < length; i++)
                {
                    array[i] = reader.ReadDouble();
                }
                return array;
            }
        }

        private void WriteArray(BinaryWriter writer, double[][] array)
        {
            if (array == null)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(array.Length);
                for (int i = 0; i < array.Length; ++i)
                {
                    writer.Write(array[i].Length);
                    for (int j = 0; j < array[i].Length; ++j)
                    {
                        writer.Write(array[i][j]);
                    }
                }
            }
        }

        private void WriteArray(BinaryWriter writer, svm_node[][] array)
        {
            if (array == null)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(array.Length);
                for (int i = 0; i < array.Length; ++i)
                {
                    writer.Write(array[i].Length);
                    for (int j = 0; j < array[i].Length; ++j)
                    {
                        writer.Write(array[i][j].index);
                        writer.Write(array[i][j].value_Renamed);
                    }
                }
            }
        }

        private void WriteArray(BinaryWriter writer, int[] array)
        {
            if (array == null)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(array.Length);
                for (int i = 0; i < array.Length; ++i)
                {
                    writer.Write(array[i]);
                }
            }
        }

        private void WriteArray(BinaryWriter writer, double[] array)
        {
            if (array == null)
            {
                writer.Write(false);
            }
            else
            {
                writer.Write(true);
                writer.Write(array.Length);
                for (int i = 0; i < array.Length; ++i)
                {
                    writer.Write(array[i]);
                }
            }
        }

        public void TrainModel(string featureFilePath)
        {
            MatFileReader mfr = new MatFileReader(featureFilePath);
            MLSingle ml = mfr.Content["rgbdfea"] as MLSingle;
            int[] dimension = ml.Dimensions;
            double[][] mlArray = MatrixUtil.Transpose(ml.GetArray());

            MLDouble label = mfr.Content["rgbdclabel"] as MLDouble;
            double[] labels = label.GetArray()[0];

            this.TrainModel(labels, mlArray);
        }

        public void TrainModel(double[] labels, double[][] mlArray)
        {
            SvmProblemBuilder builder = new SvmProblemBuilder(labels, mlArray);
            svm_problem problem = builder.CreateProblem();

            svm_parameter param = new svm_parameter()
            {
                svm_type = 0,
                kernel_type = 0,
                cache_size = 256,
                eps = 0.1,
                C = 1,
                nr_weight = 0,
                weight_label = null,
                weight = null
            };

            this.model = svm.svm_train(problem, param);
        }

        public void TrainModel(string featureFilePath, List<double[]> featureVector)
        {
            int dataLength = featureVector[0].Length;
            double[] featureData = MatrixUtil.FlattenMatrix(featureVector);
            MWCharArray rgbdFile = new MWCharArray(featureFilePath);
            MWNumericArray newFeature = new MWNumericArray(dataLength, featureVector.Count, featureData);
            rgbdfea rgbdfea = new rgbdfea();
            rgbdfea.appendFeature(rgbdFile, newFeature);

            TrainModel(featureFilePath);
        }
    }
}
