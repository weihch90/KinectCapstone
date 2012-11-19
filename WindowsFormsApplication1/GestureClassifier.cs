using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using libsvm;

namespace GestureStudio
{
    public class CategoryEventArgs : EventArgs
    {
        public CategoryEventArgs()
        {
        }

        public int CategoryLabel
        {
            get;
            set;
        }
    }

    public class GestureClassifier
    {
        public event EventHandler<CategoryEventArgs> CategoryDetected;

        private ImageFeature imgFeature;
        private bool classifying = false;
        private int category;
        private bool initialized = false;

        // SVM interface
        private SvmModelBuilder modelBuilder;

        public int Category
        {
            get { return this.category; }
        }

        public GestureClassifier()
        {
        }

        public bool Initialized
        {
            get { return this.initialized; }
        }

        public void BeginInitialize(Action initializeCallback)
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                this.InternalInitialize(initializeCallback);
            });
        }

        void InternalInitialize(Action initializeCallback)
        {
            // init svm
            this.LoadModel();
            
            // init omp
            this.imgFeature = new ImageFeature(GestureStudio.GestureLib_DictionartyPath);

            this.initialized = true;
            if (initializeCallback != null)
            {
                initializeCallback();
            }
        }

        public void ClassifyImage(DepthFrame croppedFrame)
        {
            if (croppedFrame.Width < 200 && croppedFrame.Height < 200)
            {
                short[,] imageData = MatrixUtil.RawFrameTo2D(croppedFrame.Pixels, croppedFrame.Height, croppedFrame.Width);

                // queue classifer
                if (!this.classifying)
                {
                    this.classifying = true;
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Classify), imageData);
                }
            }
            else
            {
                // too big
                this.category = 0;
                if (this.CategoryDetected != null)
                {
                    this.CategoryDetected(this, new CategoryEventArgs() { CategoryLabel = this.category });
                }
            }
        }

        // Load in the prediction model. May take couple of seconds.
        void LoadModel()
        {
            this.modelBuilder = new SvmModelBuilder();

            String modelFileName = GestureStudio.ModelFileName;
            if (!this.modelBuilder.LoadFromFile(modelFileName))
            {
                // first time usage, train from feature file
                string problemFile = @"..\..\rgbdfea_depth_first_small_dict_threshold1500.mat";
                this.modelBuilder.TrainModel(problemFile);

                //Console.WriteLine("Training finished. Saving Model as {0}", modelFileName);
                this.modelBuilder.SaveModel(modelFileName);
            }
        }

        void Classify(object state)
        {
            short[,] imageData = (short[,])state;
            double[] feature = imgFeature.GenerateFeature(imageData);
            int label = (int)svm.svm_predict(modelBuilder.GetModel(), MatrixUtil.DoubleToSvmNode(feature));
            this.category = label;

            if (this.CategoryDetected != null)
            {
                this.CategoryDetected(this, new CategoryEventArgs() { CategoryLabel = this.category });
            }

            this.classifying = false;
        }
    }
}
