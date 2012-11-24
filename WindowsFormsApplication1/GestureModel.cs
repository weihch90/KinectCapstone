using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace GestureStudio
{
    public enum ProgramMode
    {
        Idle = 0,
        Classifying = 1,
        Learning = 2
    }

    /// <summary>
    /// Main model that controls the logic of Gesture Classifer
    /// </summary>
    public class GestureModel
    {
        private static GestureModel instance = null;

        public static GestureModel Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GestureModel();
                    instance.BeginInitialize();
                }

                return instance;
            }
        }

        /// <summary>
        /// Frame ready for view
        /// </summary>
        public event EventHandler<DepthFrameEventArgs> FrameReady;
        public event EventHandler<CategoryEventArgs> CategoryDetected;
        public event EventHandler ImageCollectionFinished;
        public event EventHandler NewModelReady;
        public event EventHandler<ModelEventArgs> StatusChanged;
        
        // Kinect Interface
        private KinectSensorChooser _chooser;
        private KinectSensor sensor;

        // image processors
        FloodFill floodFill;
        GestureClassifier classifier;
        GestureLearner trainer;

        // imaging state
        private DepthFrame rawDepthFrame;
        private DepthFrame croppedFrame;

        // mode
        private ProgramMode mode;
        
        private GestureModel()
        {  
        }

        /// <summary>
        /// Raw frame from Kinect sensor
        /// </summary>
        public DepthFrame RawDepthFrame
        {
            get { return this.rawDepthFrame; }
        }

        /// <summary>
        /// Cropped frame from kinect sensor
        /// </summary>
        public DepthFrame CroppedFrame
        {
            get { return this.croppedFrame; }
        }

        /// <summary>
        /// Upper left point of the cropped image, X
        /// </summary>
        public int CropStartX
        {
            get { return this.floodFill.CropStartX; }
        }

        /// <summary>
        /// Upper left point of the cropped image, Y
        /// </summary>
        public int CropStartY
        {
            get { return this.floodFill.CropStartY; }
        }
        
        public void BeginInitialize()
        {
            this.floodFill = new FloodFill();
            this.mode = ProgramMode.Idle;
            this.classifier = null;
            this.trainer = null;
            
            GestureStudio.DisplayLoadingWindow("Loading Kinect Sensor...");
            ThreadPool.QueueUserWorkItem((state) =>
            {
                // init kinect
                this.StartKinectSensor();
                GestureStudio.HideLoadingWindow();
                this.UpdateStatus("Ready");
            });
        }

        void StartKinectSensor()
        {
            this._chooser = new KinectSensorChooser();
            this._chooser.KinectChanged += (src, e) =>
            {
                this.StopKinectSensor(e.OldSensor);

                var newsensor = e.NewSensor;
                if (newsensor == null)
                {
                    return;
                }

                newsensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                newsensor.AllFramesReady += SensorAllFramesReady;

                try
                {
                    newsensor.Start();
                }
                catch (System.IO.IOException)
                {
                    //maybe another app is using Kinect 
                    this._chooser.TryResolveConflict();
                }
            };

            this._chooser.Start();

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }
        }

        void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (var frame = e.OpenDepthImageFrame())
            {
                this.CreateBitMapFromDepthFrame(frame);
            }
        }

        void StopKinectSensor(KinectSensor sensor)
        {
            if (sensor != null && sensor.IsRunning)
            {
                sensor.Stop();
                sensor.AudioSource.Stop();
            }
        }

        private void CreateBitMapFromDepthFrame(DepthImageFrame frame)
        {
            if (frame != null)
            {
                // get image from frame
                short[] depthPixels = new short[frame.PixelDataLength];
                short[] secondDepthPixels = new short[frame.PixelDataLength];
                frame.CopyPixelDataTo(depthPixels);
                Array.Copy(depthPixels, secondDepthPixels, frame.PixelDataLength);
                // find closest point in depth frame (in millimeters)

                this.rawDepthFrame = new DepthFrame() { Pixels = depthPixels, Height = frame.Height, Width = frame.Width};
                this.croppedFrame = this.floodFill.Process(depthPixels, frame.Height, frame.Width);
                this.sensor.DepthStream.Range = this.floodFill.ClosestDistance < 1000 ? DepthRange.Near : DepthRange.Default;
                
                if (this.FrameReady != null)
                {
                    this.FrameReady(this, new DepthFrameEventArgs()
                    {
                        Frame = this.croppedFrame
                    });
                }

                switch (this.mode)
                {
                    case ProgramMode.Classifying:
                        this.Classify();
                        break;
                    case ProgramMode.Learning:
                        this.Train();
                        break;
                }
            }
        }

        public void StartClassify()
        {
            this.mode = ProgramMode.Classifying;
        }

        public void StartLearning()
        {
            this.mode = ProgramMode.Learning;
        }

        public void Stop()
        {
            this.mode = ProgramMode.Idle;
            this.UpdateStatus("Ready");

            // reset classifier/trainer
            this.classifier = null;
            this.trainer = null;
        }

        private void Classify()
        {
            if (this.classifier == null)
            {
                this.classifier = new GestureClassifier();
                this.classifier.CategoryDetected += classifier_CategoryDetected;
                GestureStudio.DisplayLoadingWindow("Loading Image Classifier...");
                this.classifier.BeginInitialize(() =>
                    {
                        GestureStudio.HideLoadingWindow();
                    });            
            }

            if (this.classifier.Initialized)
            {
                this.UpdateStatus("Classifying");
                this.classifier.ClassifyImage(this.croppedFrame);
            }
            else
            {
                this.UpdateStatus("Loading Classifier...");
            }
        }

        void classifier_CategoryDetected(object sender, CategoryEventArgs e)
        {
            if (this.CategoryDetected != null)
            {
                this.CategoryDetected(sender, e);
            }
        }

        private void Train()
        {
            if (this.trainer == null)
            {
                this.trainer = new GestureLearner();
                this.trainer.ImageCollectionFinished += trainer_ImageCollectionFinished;
                this.trainer.NewModelReady += trainer_NewModelReady;
                GestureStudio.DisplayLoadingWindow("Loading Image Trainer...");
                this.trainer.BeginInitialize(() =>
                {
                    GestureStudio.HideLoadingWindow();
                });
            }

            if (this.trainer.Initialized)
            {
                if (!this.trainer.GestureDataReady)
                {
                    this.UpdateStatus("Collecting Gesture " + this.trainer.CurrentSampleCount + " of 50");
                    this.trainer.LearnGesture(this.croppedFrame);
                }
                else if (!this.trainer.ModelBuildStarted)
                {
                    this.UpdateStatus("Building Prediction Model...");

                    GestureStudio.DisplayLoadingWindow("Building Prediction Model...");
                    this.trainer.BuildModel(() =>
                    {
                        GestureStudio.HideLoadingWindow();
                    });
                }
                else
                {
                    // training finished
                    this.Stop();
                }
            }
            else
            {
                // waiting for init, do nothing
            }
        }
        
        void trainer_ImageCollectionFinished(object sender, EventArgs e)
        {
            if (this.ImageCollectionFinished != null)
            {
                this.ImageCollectionFinished(sender, null);
            }
        }

        void trainer_NewModelReady(object sender, EventArgs e)
        {
            if (this.NewModelReady != null)
            {
                this.NewModelReady(sender, null);
            }
        }

        void UpdateStatus(string status)
        {
            if (this.StatusChanged != null)
            {
                this.StatusChanged(this, new ModelEventArgs() { Status = status });
            }
        }
    }
    
    public class ModelEventArgs : EventArgs
    {

        public ModelEventArgs()
        {

        }

        public string Status
        {
            get;
            set;
        }
    }

    public class DepthFrameEventArgs : EventArgs
    {
        public DepthFrameEventArgs()
        {
        }

        public DepthFrame Frame
        {
            get;
            set;
        }
    }

    public class DepthFrame
    {
        public short[] Pixels { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Bitmap ToBitmap()
        {
            Bitmap image = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            byte[] croppedData = new byte[Width * Height * 4];
            int index = 0;
            for (int i = 0; i < Height; ++i)
            {
                for (int j = 0; j < Width; ++j)
                {
                    byte intensity = (byte)((Pixels[i * Width + j] + 1) & 0xFF);
                    croppedData[index++] = intensity;
                    croppedData[index++] = intensity;
                    croppedData[index++] = intensity;
                    croppedData[index++] = 0;
                }
            }

            var lockedBits = image.LockBits(new Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, image.PixelFormat);
            IntPtr ptr = lockedBits.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(croppedData, 0, ptr, croppedData.Length);
            image.UnlockBits(lockedBits);

            return image;
        }
    }
}
