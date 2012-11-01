using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private KinectSensorChooser _chooser;
        private KinectSensor sensor;
        private Bitmap _bitmap;
        private ushort[] depthBitmap;
        private int snapshotCount = 0;
        private int index = 0; // index of the closest point

        private ushort[] convertedDepthPixels;
        private const int RSIZE = 150;
        private Bitmap croppedImage;
        
        private const int MAXEDGE = 200;
        private const int MAX_SIZE = 10000;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _chooser = new KinectSensorChooser();
            _chooser.KinectChanged += ChooserSensorChanged;
            _chooser.Start();
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

        }

        void ChooserSensorChanged(object sender, KinectChangedEventArgs e)
        {
            var old = e.OldSensor;
            StopKinect(old);

            var newsensor = e.NewSensor;
            if (newsensor == null)
            {
                return;
            }

            newsensor.SkeletonStream.Enable();
            newsensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            newsensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
            newsensor.AllFramesReady += SensorAllFramesReady;

            try
            {
                newsensor.Start();
                rtbMessages.Text = "Kinect Started" + "\r";
            }
            catch (System.IO.IOException)
            {
                rtbMessages.Text = "Kinect Not Started" + "\r";
                //maybe another app is using Kinect 
                _chooser.TryResolveConflict();
            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    sensor.Stop();
                    sensor.AudioSource.Stop();
                }
            }
        }

        void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            SensorDepthFrameReady(e);
            video.Image = _bitmap;
        }


        void SensorDepthFrameReady(AllFramesReadyEventArgs e)
        {
            // if the window is displayed, show the depth buffer image 
            if (WindowState != FormWindowState.Minimized)
            {
                using (var frame = e.OpenDepthImageFrame())
                {
                    _bitmap = CreateBitMapFromDepthFrame(frame);
                }
            }
        }

        private Bitmap CreateBitMapFromDepthFrame(DepthImageFrame frame)
        {
            if (frame != null)
            {
                var bitmapImage = new Bitmap(frame.Width, frame.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                var g = Graphics.FromImage(bitmapImage);
                g.Clear(System.Drawing.Color.FromArgb(0, 34, 68));

                // Allocate space to put the depth pixels we'll receive
                short[] depthPixels = new short[frame.PixelDataLength];
                convertedDepthPixels = new ushort[frame.PixelDataLength];

                // Allocate space to put the color pixels we'll create
                byte[] colorPixels = new byte[frame.PixelDataLength * sizeof(int)];

                // This is the bitmap we'll display on-screen
                //WriteableBitmap depthBitmap = new WriteableBitmap(frame.Width, frame.Height, 96.0, 96.0, PixelFormats.Gray16, null);


                // Copy the pixel data from the image to a temporary array
                frame.CopyPixelDataTo(depthPixels);
                // Convert the depth to RGB
                int colorPixelIndex = 0;
                ushort closest = 2047;
                int width = frame.Width;
                int height = frame.Height;
                
                for (int i = 0; i < depthPixels.Length; ++i)
                {
                    // discard the portion of the depth that contains only the player index
                    ushort depth = (ushort)(depthPixels[i] >> DepthImageFrame.PlayerIndexBitmaskWidth);
                    convertedDepthPixels[i] = depth;
                    if (depth < closest && depth > 200)
                    {
                        closest = depth;
                        //rtbMessages.Text = depth.ToString();
                        index = i;
                    }

                    // to convert to a byte we're looking at only the lower 8 bits
                    // by discarding the most significant rather than least significant data
                    // we're preserving detail, although the intensity will "wrap"
                    // add 1 so that too far/unknown is mapped to black
                    byte intensity = (byte)((depth + 1) & byte.MaxValue);

                    // Write out blue byte
                    colorPixels[colorPixelIndex++] = intensity;

                    // Write out green byte
                    colorPixels[colorPixelIndex++] = intensity;

                    // Write out red byte                        
                    colorPixels[colorPixelIndex++] = intensity;

                    // We're outputting BGR, the last byte in the 32 bits is unused so skip it
                    // If we were outputting BGRA, we would write alpha here.
                    ++colorPixelIndex;
                }
                if (this.raw_depth_to_meters(index) < 1) {
                    this.sensor.DepthStream.Range = DepthRange.Near;
                    this.label1.Text = "Near";
                } else {
                    this.sensor.DepthStream.Range = DepthRange.Default;
                    this.label1.Text = "Default";
                }

                Queue<int> q = new Queue<int>();
                List<int> l = new List<int>();
                HashSet<int> discovered = new HashSet<int>();
                q.Enqueue(index);

                int[] rect = new int[4];
                rect[0] = rect[2] = index % width;
                rect[1] = rect[3] = index / width;

                while(q.Count != 0 && l.Count < MAX_SIZE) {
                    int n = q.Dequeue();

                    int x = n % width;
                    int y = n / width;
                    // check if the index is already discovered
                    if (!discovered.Contains(n)) {
                        // check if the index is within the range
                        if (0 <= x && x < width && 0 <= y && y < height) {
                            // check if the index is within the distance
                            if (convertedDepthPixels[y * width + x] - convertedDepthPixels[index] <= 200)
                            {
                                rect[0] = Math.Min(x, rect[0]);
                                rect[1] = Math.Min(y, rect[1]);

                                rect[2] = Math.Max(x, rect[2]);
                                rect[3] = Math.Max(y, rect[3]);
                                l.Add(n);

                                // add node in each direction
                                if (!discovered.Contains(y * width + x + 1))
                                    q.Enqueue(y * width + x + 1);    // east

                                if (!discovered.Contains(y * width + x - 1))
                                    q.Enqueue(y * width + x - 1);    // west
                            
                                if (!discovered.Contains((y + 1) * width + x))
                                    q.Enqueue((y + 1) * width + x);  // south

                                if (!discovered.Contains((y - 1) * width + x))
                                    q.Enqueue((y - 1) * width + x);  // north

                            }
                        }
                    }
                    discovered.Add(n);

                }



                // Save the depth info of the selected area into the array.




                /*Queue<int> q = new Queue<int>();

                for (int i = 0; i < height; i++) //y, row 
                    for (int j = 0; j < width; j++) //x, column
                        // check if the point is within the maximum range of the box
                        if (Math.Abs(j - xIndex) < MAXEDGE && Math.Abs(i - yIndex) < MAXEDGE)
                            if (convertedDepthPixels[i * width + j] - convertedDepthPixels[index] <= 200)
                                q.Enqueue(i * width + j);

                int[] rect = new int[4];
                int tmp = q.Peek();
                rect[0] = rect[2] = tmp % width;
                rect[1] = rect[3] = tmp / width;
            
                foreach (int i in q)
                {
                   int x = i % width;
                   int y = i / width;
                   rect[0] = Math.Min(x, rect[0]);
                   rect[1] = Math.Min(y, rect[1]);

                   rect[2] = Math.Max(x, rect[2]);
                   rect[3] = Math.Max(y, rect[3]);
                }
                */

                int startX = rect[0];
                int startY = rect[1];
                int endX = rect[2];
                int endY = rect[3];

                int croppedWidth = (endX - startX + 1);
                int croppedHeight = (endY - startY + 1);
                
                //depthBitmap = new ushort[Math.Min(croppedHeight * croppedWidth, 4 * MAXEDGE * MAXEDGE)];


                depthBitmap = new ushort[croppedHeight * croppedWidth];
                foreach (int i in l)
                {
                    int x = i % width;
                    int y = i / width;
                    int offset = startY * width + startX;
                    int transposedX = x - startX;
                    int transposedY = y - startY;
                    int transposedIndex = transposedX + transposedY * croppedWidth;
                    depthBitmap[transposedIndex] = convertedDepthPixels[i];
                }
                
                /*
                for (int i = startY; i < endY; i++) //y, row number
                {
                    for (int j = startX; j < endX; j++) //x, column number
                    {
                        if (convertedDepthPixels[i * width + j] - convertedDepthPixels[index] <= 200)
                            depthBitmap[n] = convertedDepthPixels[i * width + j];
                        else
                            depthBitmap[n] = 0;
                        n++;
                    }
                }
                */




                // Save the array containing depth info to a new bitmap with 32bpp rgb format. Details saved on green channel, highlight saved on blue channel.
                // Display it on the pictureBox.
                croppedImage = new Bitmap(croppedWidth, croppedHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                byte[] croppedData = this.depthBitmap.SelectMany(s => new byte[] { (byte)(s >> 8), (byte)(s), 0, 0 }).ToArray();
                var lockedData = croppedImage.LockBits(new Rectangle(0, 0, croppedWidth, croppedHeight), ImageLockMode.WriteOnly, croppedImage.PixelFormat);
                IntPtr ptr = lockedData.Scan0;
                Marshal.Copy(croppedData, 0, ptr, croppedData.Length);
                croppedImage.UnlockBits(lockedData);
                pictureBox1.Image = croppedImage;

                // DepthImage displayed on the main window.
                BitmapData bmapdata = bitmapImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmapImage.PixelFormat);
                ptr = bmapdata.Scan0;
                Marshal.Copy(colorPixels, 0, ptr, width * height * 4);
                bitmapImage.UnlockBits(bmapdata);

                // Draw the selected region out.
                var gobject = Graphics.FromImage(bitmapImage);

                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
                gobject.DrawRectangle(pen, startX + croppedWidth / 2, startY + croppedHeight / 2, 1, 1);
                gobject.DrawRectangle(pen, startX, startY, croppedWidth, croppedHeight);

                // If checkbox checked, save image to the folder.
                CheckState state = checkBox1.CheckState;
                switch (state)
                {  
                    case CheckState.Indeterminate:
                        break;
                    case CheckState.Unchecked:
                        break;
                    case CheckState.Checked:
                        {
                            saveImage();
                            break;
                        }
                }

                return bitmapImage;
            }
            return null;
        }

        private void saveImage()
        {
            snapshotCount++;
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            String name = filename.Text;
            String path = "C:\\Users\\Administrator\\Kinect\\KinectCapstone\\sampleImages\\" + name + "\\" + name + "_2\\" + name + "_" + snapshotCount + ".png";
            croppedImage.Save(@path, GetEncoder(ImageFormat.Png), encoderParameters);
        }

        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private float raw_depth_to_meters(int raw_depth)
        {
            if (raw_depth < 2047)
            {
                return (float)(1.0 / (raw_depth * -0.0030711016 + 3.3309495161));
            }
            return 0;
        }


        private void WindowMP()
        {
            Process[] processes = Process.GetProcessesByName("media player");

            if (processes.Length == 0)
                return;

            IntPtr WindowHandle = processes[0].MainWindowHandle;


        }

    }
}