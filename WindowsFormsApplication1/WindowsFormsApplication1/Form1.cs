using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private Bitmap _bitmap;
        private ushort[] depthBitmap;
        private int snapshotCount = 0;
        private int index = 0; // index of the closest point
        private ushort[] convertedDepthPixels;
        private const int RSIZE = 150;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _chooser = new KinectSensorChooser();
            _chooser.KinectChanged += ChooserSensorChanged;
            _chooser.Start();
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
                for (int i = 0; i < depthPixels.Length; ++i)
                {
                    // discard the portion of the depth that contains only the player index
                    ushort depth = (ushort)(depthPixels[i] >> DepthImageFrame.PlayerIndexBitmaskWidth);
                    convertedDepthPixels[i] = depth;
                    if (depth < closest && depth > 300)
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

                // Save the depth info of the selected area into the array.
                depthBitmap = new ushort[RSIZE * RSIZE];
                int width = frame.Width;
                int height = frame.Height;
                int n = 0;
                if ((index / 480) - RSIZE / 2 >= 0 && (index % 640) - RSIZE / 2 >= 0 && (index / 480) + RSIZE / 2 <= height && (index % 640) + RSIZE / 2 <= width)
                {
                    for (int i = (index / 480) - RSIZE / 2; i < (index / 480) + RSIZE / 2; i++) //y, row number
                    {
                        for (int j = (index % 640) - RSIZE / 2; j < (index % 640) + RSIZE / 2; j++) //x, column number
                        {
                            if (convertedDepthPixels[i * width + j] - convertedDepthPixels[index] <= 200)
                                depthBitmap[n] = convertedDepthPixels[i * width + j];
                            else
                                depthBitmap[n] = 0;
                            n++;
                        }
                    }
                }

                Bitmap croppedImage = new Bitmap(RSIZE, RSIZE, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                byte[] croppedData = this.depthBitmap.SelectMany(s => new byte[] { (byte)(s >> 8), (byte)(s), 0, 0 }).ToArray();
                var lockedData = croppedImage.LockBits(new Rectangle(0, 0, RSIZE, RSIZE), ImageLockMode.WriteOnly, croppedImage.PixelFormat);
                IntPtr ptr = lockedData.Scan0;
                Marshal.Copy(croppedData, 0, ptr, croppedData.Length);
                croppedImage.UnlockBits(lockedData);
                pictureBox1.Image = croppedImage;

                BitmapData bmapdata = bitmapImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmapImage.PixelFormat);
                ptr = bmapdata.Scan0;
                Marshal.Copy(colorPixels, 0, ptr, width * height * 4);
                bitmapImage.UnlockBits(bmapdata);

                var gobject = Graphics.FromImage(bitmapImage);

                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
                gobject.DrawRectangle(pen, (index % 640), (index / 480), 1, 1);
                gobject.DrawRectangle(pen, (index % 640) - RSIZE / 2, (index / 480) - RSIZE / 2, RSIZE, RSIZE);
                
                return bitmapImage;
            }
            return null;
        }
    }
}