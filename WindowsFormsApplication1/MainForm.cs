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
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using System.Threading;



namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        private int snapshotCount = 0;
        private ImageClassifier classifer;
        private int framesCount = 0;

        public MainForm(ImageClassifier classifer)
        {
            this.classifer = classifer;
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            SynchronizationContext ctx = SynchronizationContext.Current;

            this.classifer.FrameReady += (s, args) =>
                {
                    Bitmap fullFrame = this.classifer.RawDepthFrame.ToBitmap();
                    Bitmap croppedFrame = this.classifer.CroppedFrame.ToBitmap();
                    using (Graphics g = Graphics.FromImage(fullFrame))
                    {
                        int startX = this.classifer.CropStartX;
                        int startY = this.classifer.CropStartY;
                        int croppedWidth = this.classifer.CroppedFrame.Width;
                        int croppedHeight = this.classifer.CroppedFrame.Height;

                        Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
                        g.DrawRectangle(pen, startX + croppedWidth / 2, startY + croppedHeight / 2, 1, 1);
                        g.DrawRectangle(pen, startX, startY, croppedWidth, croppedHeight);
                    }
                    
                    ctx.Post((o) => {
                        this.fullFrameStream.Image = fullFrame;
                        this.croppedFrameStream.Image = croppedFrame;
                        framesCount++;
                    }, null);
                };

            this.classifer.CategoryDetected += (s, args) =>
                {
                    ctx.Post((o) =>
                    {
                        int label = (int)o;
                        this.message.Text = LabelToString(label);
                    }, args.CategoryLabel);
                };

            this.classifer.Start();
            System.Timers.Timer fpsCounter = new System.Timers.Timer(1000);
            fpsCounter.AutoReset = true;
            fpsCounter.Elapsed += (src, args) =>
                {
                    ctx.Post((o) =>
                    {
                        this.framesPerSecond.Text = "FPS = " + framesCount;
                        framesCount = 0;
                    }, null);
                };

            fpsCounter.Start();
        }
                
        private String LabelToString(int i)
        {
            switch (i)
            {
                case 1:
                    return "Good";
                case 2:
                    return "Noise";
                case 3:
                    return "OK";
                case 4:
                    return "Paper";
                case 5:
                    return "Rock";
                case 6:
                    return "Scissor";
            }
            return "";
        }

        //private void TakeSnapshot()
        //{
        //    // If checkbox checked, save image to the folder.
        //    CheckState state = checkBox1.CheckState;
        //    switch (state)
        //    {
        //        case CheckState.Indeterminate:
        //            break;
        //        case CheckState.Unchecked:
        //            break;
        //        case CheckState.Checked:
        //            {
        //                saveImage();
        //                break;
        //            }
        //    }
        //}


        //private void saveImage()
        //{
        //    snapshotCount++;
        //    EncoderParameters encoderParameters = new EncoderParameters(1);
        //    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
        //    String name = filename.Text;
        //    String path = "C:\\Users\\Administrator\\Kinect\\KinectCapstone\\sampleImages\\" + name + "\\" + name + "_2\\" + name + "_" + snapshotCount + ".png";
        //    croppedImage.Save(@path, GetEncoder(ImageFormat.Png), encoderParameters);
        //}

        //public static ImageCodecInfo GetEncoder(ImageFormat format)
        //{

        //    ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

        //    foreach (ImageCodecInfo codec in codecs)
        //    {
        //        if (codec.FormatID == format.Guid)
        //        {
        //            return codec;
        //        }
        //    }
        //    return null;
        //}
    }
}