using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using libsvm;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect;
using System.Drawing;

namespace WindowsFormsApplication1
{
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

    public class CropImageData
    {
        public short[] pixels;
        public int height;
        public int width;

        public CropImageData() { }
        public CropImageData(short[] pixels, int height, int width)
        {
            this.pixels = pixels;
            this.height = height;
            this.width = width;
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

    public class FFImageData
    {
        public List<int> l;
        public HashSet<int> discovered;
        public int[] rect;

        public FFImageData()
        {
            this.l = new List<int>();
            this.discovered = new HashSet<int>();
        }
        public FFImageData(List<int> l, HashSet<int> discovered, int[] rect)
        {
            this.l = l;
            this.discovered = discovered;
            this.rect = rect;
        }
    }

    public class ImageClassifier
    {
        public event EventHandler<DepthFrameEventArgs> FrameReady;
        public event EventHandler<CategoryEventArgs> CategoryDetected;

        // Kinect Interface
        private KinectSensorChooser _chooser;
        private KinectSensor sensor;

        // OMP interface
        private bool started = false;
        private ImageFeature imgFeature;

        // SVM interface
        private SvmModelBuilder modelBuilder;

        private bool classifying = false;
        private Action initializeCallback = null;

        // imaging state
        private DepthFrame rawDepthFrame;
        private DepthFrame croppedFrame;
        private int category;
        private int cropStartX;
        private int cropStartY;

        // spread method
        private int SPREAD_METHOD = 1;

        // disable classifier
        public static bool CLASSIFY = false;

        public ImageClassifier()
        {
        }

        public DepthFrame RawDepthFrame
        {
            get { return this.rawDepthFrame; }
        }

        public DepthFrame CroppedFrame
        {
            get { return this.croppedFrame; }
        }
        
        public int Category
        {
            get { return this.category; }
        }

        public int CropStartX
        {
            get { return this.cropStartX; }
        }

        public int CropStartY
        {
            get { return this.cropStartY; }
        }

        #region Initialization

        public void BeginInitialize(Action initializeCallback)
        {
            this.initializeCallback = initializeCallback;
            Thread loaderThread = new Thread(this.InternalInitialize);
            loaderThread.Start();
        }

        void InternalInitialize()
        {
            // init svm
            this.LoadModel();

            // init omp
            if (CLASSIFY)
                this.imgFeature = new ImageFeature();

            // init kinect
            this.StartKinectSensor();

            if (this.initializeCallback != null)
            {
                this.initializeCallback();
            }
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

        void LoadModel()
        {
            this.modelBuilder = new SvmModelBuilder();

            string modelFileName = "model.svm";
            if (!this.modelBuilder.LoadFromFile(modelFileName))
            {
                // first time usage, train from feature file
                string problemFile = @"C:\Users\Haochen\Kinect\KinectCapstone\WindowsFormsApplication1\rgbdfea_depth_first2.mat";
                //label1.Text = "Training new model from " + problemFile;
                this.modelBuilder.TrainModel(problemFile);

                //Console.WriteLine("Trainning finished. Saving Model as {0}", modelFileName);
                this.modelBuilder.SaveModel(modelFileName);
            }
        }

        #endregion

        public void Start()
        {
            this.started = true;
        }

        public void Stop()
        {
            this.started = false;
        }

        private void CreateBitMapFromDepthFrame(DepthImageFrame frame)
        {
            if (this.started && frame != null)
            {
                // get image from frame
                short[] depthPixels = new short[frame.PixelDataLength];
                short[] secondDepthPixels = new short[frame.PixelDataLength];
                frame.CopyPixelDataTo(depthPixels);
                Array.Copy(depthPixels, secondDepthPixels, frame.PixelDataLength);
                // find closest point in depth frame (in millimeters)
                int closestIndex;
                short closest;

                // initialize flood fill data
                FFImageData floodFillData = new FFImageData();
                this.FindClosestPixel(depthPixels, new HashSet<int>(floodFillData.l), out closestIndex, out closest);
                int[] rect = new int[4];
                rect[0] = rect[2] = closestIndex % frame.Width;
                rect[1] = rect[3] = closestIndex / frame.Width;
                floodFillData.rect = rect;




                this.rawDepthFrame = new DepthFrame() { Pixels = depthPixels, Height = frame.Height, Width = frame.Width };                
                this.sensor.DepthStream.Range = closest < 1000 ? DepthRange.Near : DepthRange.Default;

                //Perform flood fill on the current frame and find the cropping area.
                int croppedWidth, croppedHeight;
                //short[] croppedDepthFrame = FloodFill(depthPixels, frame.Width, frame.Height, closestIndex, out croppedWidth, out croppedHeight, out this.cropStartX, out this.cropStartY);
                FloodFill(depthPixels, frame.Width, frame.Height, closestIndex, floodFillData);//, out croppedWidth, out croppedHeight, out this.cropStartX, out this.cropStartY);


                int secondClosestIndex;
                short secondClosest;
                this.FindClosestPixel(secondDepthPixels, new HashSet<int>(floodFillData.l), out secondClosestIndex, out secondClosest);
 
                if (Math.Abs(secondClosest - closest) < 80)
                    FloodFill(depthPixels, frame.Width, frame.Height, secondClosestIndex, floodFillData);


                this.cropStartX = floodFillData.rect[0];
                this.cropStartY = floodFillData.rect[1];
                CropImageData croppedDepthFrame = cropImage(floodFillData, frame.Width, depthPixels);
                //croppedWidth = croppedDepthFrame.width;
                //croppedHeight = croppedDepthFrame.height;
                this.croppedFrame = new DepthFrame() { Pixels = croppedDepthFrame.pixels, Height = croppedDepthFrame.height, Width = croppedDepthFrame.width };

                if (this.FrameReady != null)
                {
                    this.FrameReady(this, new DepthFrameEventArgs()
                    {
                        Frame = this.croppedFrame
                    });
                }

                if (!CLASSIFY)
                {
                    return;
                }
                if (this.croppedFrame.Width < 200 && this.croppedFrame.Height < 200)
                {
                    short[,] imageData = MatrixUtil.RawFrameTo2D(croppedDepthFrame.pixels, croppedDepthFrame.height, croppedDepthFrame.height);

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
        }

        private CropImageData cropImage(FFImageData floodFillData, int width, short[] rawDepth)
        {
            CropImageData cropData = new CropImageData();
            int startX = floodFillData.rect[0];
            int startY = floodFillData.rect[1];
            int endX = floodFillData.rect[2];
            int endY = floodFillData.rect[3];
            cropData.width = (endX - startX + 1);
            cropData.height = (endY - startY + 1);
            int croppedWidth = (endX - startX + 1);
            int croppedHeight = (endY - startY + 1);

            short[] filledCrop = new short[croppedHeight * croppedWidth];
            foreach (int i in floodFillData.l)
            {
                int x = i % width;
                int y = i / width;
                int offset = startY * width + startX;
                int transposedX = x - startX;
                int transposedY = y - startY;
                int transposedIndex = transposedX + transposedY * croppedWidth;
                filledCrop[transposedIndex] = rawDepth[i];
            }
            cropData.pixels = filledCrop;
            return cropData;
        }

        private void FindClosestPixel(short[] depthPixels, HashSet<int> ignorePixels, out int closestIndex, out short closest)
        {
            closestIndex = 0;
            closest = 2047;
            for (int i = 0; i < depthPixels.Length; ++i)
            {
                // discard the portion of the depth that contains only the player index
                short depth = (short)(depthPixels[i] >> DepthImageFrame.PlayerIndexBitmaskWidth);
                depthPixels[i] = depth;

                if (!ignorePixels.Contains(i) && depth > 200 && depth < closest)
                {
                    closest = depth;
                    closestIndex = i;
                }
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

        private const int MAX_SIZE = 20000;


        private void spread(int index, int origin, LinkedList<int> linked, HashSet<int> discovered, short[] rawDepth)
        {
            if (!discovered.Contains(index) && 0 <= index && index < rawDepth.Length)
            {
                if (linked.First == null) {
                    linked.AddFirst(index);
                    return;
                }
                for (LinkedListNode<int> it = linked.First; it != null; it = it.Next)
                {
                    if (Math.Abs((ushort)rawDepth[it.Value] - (ushort)rawDepth[origin])
                     > Math.Abs((ushort)rawDepth[index] - (ushort)rawDepth[origin]))
                    {
                        linked.AddBefore(it, index);
                        return;
                    }
                    if (it.Next == null)
                    {
                        linked.AddLast(index);
                        return;
                    }
                }
            }

        }


        // Perform flood fill algorithm on the current frame
        // width and height correspond to those of the current frame.
        private void FloodFill(
            short[] rawDepth,
            int width, 
            int height, 
            int closestIndex,
            FFImageData floodFillData)
        {
            Queue<int> q = new Queue<int>();
            List<int> l = floodFillData.l;
            HashSet<int> discovered = new HashSet<int>(floodFillData.l);
            int[] rect = floodFillData.rect;
            int count = 0;
            q.Enqueue(closestIndex);

            while (q.Count != 0 && count < MAX_SIZE)
            {
                int n = q.Dequeue();

                int x = n % width;
                int y = n / width;
                // check if the index is already discovered
                if (!discovered.Contains(n))
                {
                    // check if the index is within the range
                    if (0 <= x && x < width && 0 <= y && y < height)
                    {
                        
                        // check if the index is within the distance
                        if ((ushort)rawDepth[n] - (ushort)rawDepth[closestIndex] <= 200)
                        {
                            rect[0] = Math.Min(x, rect[0]);
                            rect[1] = Math.Min(y, rect[1]);

                            rect[2] = Math.Max(x, rect[2]);
                            rect[3] = Math.Max(y, rect[3]);
                            l.Add(n);

                            if (SPREAD_METHOD == 0)
                            {
                                // add node in each direction
                                if (!discovered.Contains(y * width + x + 1))
                                    q.Enqueue(y * width + x + 1);    // east

                                if (!discovered.Contains(y * width + x - 1))
                                    q.Enqueue(y * width + x - 1);    // west

                                if (!discovered.Contains((y + 1) * width + x))
                                    q.Enqueue((y + 1) * width + x);  // south

                                if (!discovered.Contains((y - 1) * width + x))
                                    q.Enqueue((y - 1) * width + x);  // north

                            } else if (SPREAD_METHOD == 1) {
                                LinkedList<int> linked = new LinkedList<int>();
                                spread(y * width + x + 1, n, linked, discovered, rawDepth);
                                spread(y * width + x - 1, n, linked, discovered, rawDepth);
                                spread((y + 1) * width + x, n, linked, discovered, rawDepth);
                                spread((y - 1) * width + x, n, linked, discovered, rawDepth);
                                for (LinkedListNode<int> it = linked.First; it != null; it = it.Next)
                                {
                                    q.Enqueue(it.Value);
                                }
                            }


                        }
                    }
                }
                discovered.Add(n);
                count++;
            }
            floodFillData.rect = rect;

        }
    }
}
