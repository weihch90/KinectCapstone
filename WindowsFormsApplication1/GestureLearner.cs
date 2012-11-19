using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestureStudio
{
    public class GestureLearner
    {
        public event EventHandler ImageCollectionFinished;
        public event EventHandler NewModelReady;

        private const int SAMPLENUM = 50;

        // Feature vector interface
        private ImageFeature imgFeature;

        // SVM interface
        private SvmModelBuilder modelBuilder;

        private int currentSampleCount = 0;
        private bool initialized = false;
        private bool learning = false;
        private List<double[]> featureVector;
        private bool buildModelStarted = false;

        public GestureLearner()
        {
        }

        public bool Initialized
        {
            get { return this.initialized; }
        }

        public bool GestureDataReady
        {
            get { return this.currentSampleCount >= SAMPLENUM; }
        }

        public bool ModelBuildStarted
        {
            get { return this.buildModelStarted; }
        }

        public int CurrentSampleCount
        {
            get { return this.currentSampleCount; }
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
            modelBuilder = new SvmModelBuilder();
            this.featureVector = new List<double[]>();

            // init omp
            this.imgFeature = new ImageFeature(GestureStudio.GestureLib_DictionartyPath);

            // done initialization
            if (initializeCallback != null)
            {
                initializeCallback();
            }

            // display trainer form
            SynchronizationContext ctx = SynchronizationContext.Current;
            GestureStudio.DisplayTrainerForm(() =>
            {
                // count down finished, begin
                this.initialized = true;
            });
        }

        public void LearnGesture(DepthFrame croppedFrame)
        {
            if (croppedFrame.Width < 200 && croppedFrame.Height < 200 && croppedFrame.Width > 50 && croppedFrame.Height > 50)
            {
                short[,] imageData = MatrixUtil.RawFrameTo2D(croppedFrame.Pixels, croppedFrame.Height, croppedFrame.Width);

                // queue trainer
                if (!this.learning && !this.GestureDataReady)
                {
                    this.learning = true;
                    ThreadPool.QueueUserWorkItem((state) =>
                    {
                        this.Learn(imageData);
                    });
                }
            }
        }

        void Learn(short[,] imageData)
        {
            currentSampleCount++;

            double[] feature = imgFeature.GenerateFeature(imageData);
            featureVector.Add(feature);

            if (this.GestureDataReady)
            {
                this.ImageCollectionFinished(this, null);
            }

            this.learning = false;
        }

        public void BuildModel(Action buildCallBack)
        {
            // queue builder
            if (!this.buildModelStarted && this.GestureDataReady)
            {
                this.buildModelStarted = true;
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    this.Build(buildCallBack);
                });
            }
        }

        void Build(Action buildCallBack)
        {
            string problemFile = @"my_rgbdfea_depth_first.mat";
            this.modelBuilder.TrainModel(problemFile, featureVector);
            this.modelBuilder.SaveModel(GestureStudio.ModelFileName);

            // Model ready
            this.NewModelReady(this, null);
            if (buildCallBack != null)
            {
                buildCallBack();
            }
        }

    }
}
