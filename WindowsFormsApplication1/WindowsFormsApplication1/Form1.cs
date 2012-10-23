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
                //var bitmapImage = new Bitmap(frame.Width, frame.Height, PixelFormat.Format16bppRgb565);
                /*var bitmapImage = new Bitmap(frame.Width, frame.Height, PixelFormat.Format32bppRgb);
                var g = Graphics.FromImage(bitmapImage);
                g.Clear(Color.FromArgb(0, 34, 68));

                //Copy the depth frame data onto the bitmap  
                var _pixelData = new short[frame.PixelDataLength];
                frame.CopyPixelDataTo(_pixelData);
                BitmapData bmapdata = bitmapImage.LockBits(new Rectangle(0, 0, frame.Width,
                 frame.Height), ImageLockMode.WriteOnly, bitmapImage.PixelFormat);
                IntPtr ptr = bmapdata.Scan0;
                Marshal.Copy(_pixelData, 0, ptr, frame.Width * frame.Height);
                bitmapImage.UnlockBits(bmapdata);*/

                var bitmapImage = new Bitmap(frame.Width, frame.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                var g = Graphics.FromImage(bitmapImage);
                g.Clear(System.Drawing.Color.FromArgb(0, 34, 68));


                // Allocate space to put the depth pixels we'll receive
                short[] depthPixels = new short[frame.PixelDataLength];

                // Allocate space to put the color pixels we'll create
                byte[] colorPixels = new byte[frame.PixelDataLength * sizeof(int)];

                // This is the bitmap we'll display on-screen
                WriteableBitmap colorBitmap = new WriteableBitmap(frame.Width, frame.Height, 96.0, 96.0, PixelFormats.Bgr32, null);


                // Copy the pixel data from the image to a temporary array
                frame.CopyPixelDataTo(depthPixels);
                int rSize = 150;
                // Convert the depth to RGB
                int colorPixelIndex = 0;
                int index = 0;
                short closest = 2047;
                for (int i = 0; i < depthPixels.Length; ++i)
                {
                    // discard the portion of the depth that contains only the player index
                    short depth = (short)(depthPixels[i] >> DepthImageFrame.PlayerIndexBitmaskWidth);
                    if (depth < closest && depth > 300)
                    {
                        closest = depth;
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


                // Write the pixel data into our bitmap
                /*colorBitmap.WritePixels(
                    new Int32Rect(0, 0, colorBitmap.PixelWidth, colorBitmap.PixelHeight),
                    colorPixels,
                    colorBitmap.PixelWidth * sizeof(int),
                    0);*/

                BitmapData bmapdata = bitmapImage.LockBits(new Rectangle(0, 0, frame.Width,
                 frame.Height), ImageLockMode.WriteOnly, bitmapImage.PixelFormat);
                IntPtr ptr = bmapdata.Scan0;
                Marshal.Copy(colorPixels, 0, ptr, frame.Width * frame.Height * 4);
                bitmapImage.UnlockBits(bmapdata);

                var gobject = Graphics.FromImage(bitmapImage);
                
                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
                gobject.DrawRectangle(pen, (index % 640), (index / 480), 1, 1); 
                gobject.DrawRectangle(pen, (index % 640) - rSize/2, (index / 480) - rSize/2, rSize, rSize);


                // create a png bitmap encoder which knows how to save a .png file
                BitmapEncoder encoder = new PngBitmapEncoder();

                // create frame from the writable bitmap and add to encoder
                encoder.Frames.Add(BitmapFrame.Create(bmapdata));

                string time = System.DateTime.Now.ToString("hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);

                string myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                string path = Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

                // write the new file to disk
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        encoder.Save(fs);
                    }

                    this.statusBarText.Text = string.Format("{0} {1}", Properties.Resources.ScreenshotWriteSuccess, path);
                }
                catch (IOException)
                {
                    this.statusBarText.Text = string.Format("{0} {1}", Properties.Resources.ScreenshotWriteFailed, path);
                }

                

                return bitmapImage;
            }
            return null;
        }
    }
}
