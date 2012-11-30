using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using System.Threading.Tasks;
using System.Windows;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;


namespace GestureStudio
{
    public partial class CollectImageForm : Form
    {
        private int framesCount = 0;
        private GestureModel model;
        private int snapshotCount = 0;
        private Bitmap originalFullFrame;
        private Bitmap croppedFrame;
        private bool collecting;
        private Stopwatch timer; 

        private const bool SAVE_FULL_IMG = true;
        private const int MILISEC = 1000;
        private const int COUNT_TIMER = 5;
        private const int CAPTURE_FREQ = 20;
        private const int MAX_CAPTURES = 15;
        private const string PROJECT_HOME_PATH = @"..\\..\\..\\";


        public CollectImageForm()
        {
            this.collecting = false;
            InitializeComponent();
        }

        private void StartCollectionButton_Click(object sender, EventArgs e)
        {
            if (!collecting)
            {
                this.collecting = true;
                this.startCollectButton.Text = "Stop";
                this.timer.Reset();
                this.timer.Start();
            }
            else
            {
                this.collecting = false;
                this.startCollectButton.Text = "Start";
                this.snapshotCount = 0;
            }
        }

        private void CollectImageForm_Load(object sender, EventArgs e)
        {
            // instance initialization requires UI thread, wait until load
            this.model = GestureModel.Instance;
            this.timer = new Stopwatch();

            SynchronizationContext ctx = SynchronizationContext.Current;

            this.model.FrameReady += (s, args) =>
            {

                Bitmap fullFrame = this.model.RawDepthFrame.ToBitmap();
                originalFullFrame = (Bitmap)fullFrame.Clone();
                croppedFrame = this.model.CroppedFrame.ToBitmap();
                using (Graphics g = Graphics.FromImage(fullFrame))
                {
                    int startX = this.model.CropStartX;
                    int startY = this.model.CropStartY;
                    int croppedWidth = this.model.CroppedFrame.Width;
                    int croppedHeight = this.model.CroppedFrame.Height;

                    System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
                    g.DrawRectangle(pen, startX + croppedWidth / 2, startY + croppedHeight / 2, 1, 1);
                    g.DrawRectangle(pen, startX, startY, croppedWidth, croppedHeight);

                }

                ctx.Post((o) =>
                {
                    this.fullBox.Image = fullFrame;
                    this.croppedBox.Image = croppedFrame;
                    this.timer.Stop();

                    if (collecting)
                    {
                        if (this.timer.ElapsedMilliseconds < COUNT_TIMER * MILISEC)
                        {
                            this.imageCollectionStatus.Text = "Count: " + (COUNT_TIMER - this.timer.ElapsedMilliseconds / MILISEC);
                            this.timer.Start();
                            return;
                        }
                        this.imageCollectionStatus.Text = "Collecting Images (" + (snapshotCount / CAPTURE_FREQ) + "/" + MAX_CAPTURES + ")";
                        saveImage();

                        // once it gets to max captures, stop capture images.
                        if (snapshotCount / CAPTURE_FREQ > MAX_CAPTURES)
                        {
                            this.collecting = false;
                            this.startCollectButton.Text = "Start";
                            this.imageCollectionStatus.Text = "Finished image Collection";
                            this.timer.Reset();
                            this.snapshotCount = 0;
                        }
                    }
                    else
                    {
                        this.imageCollectionStatus.Text = "Ready to Collect Images";
                    }
                    this.timer.Start();
                    framesCount++;
                }, null);
            };
        }

        private void saveImage()
        {
            snapshotCount++;
            if (snapshotCount % CAPTURE_FREQ == 0 && snapshotCount / CAPTURE_FREQ <= MAX_CAPTURES)
            {
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                String name = this.gestureNameBox.Text;
                if (this.userNameBox.Text == null || this.userNameBox.Text.Trim() == "")
                    return;
                String userName = this.userNameBox.Text.Trim().Replace(' ', '_');
                String cropped_dir_path = PROJECT_HOME_PATH + "croppedImages";
                String full_dir_path = PROJECT_HOME_PATH + "fullImages";
                if (!Directory.Exists(cropped_dir_path))
                    Directory.CreateDirectory(cropped_dir_path);

                if (!Directory.Exists(full_dir_path))
                    Directory.CreateDirectory(full_dir_path);

                cropped_dir_path += "\\" + name;
                full_dir_path += "\\" + name;

                if (!Directory.Exists(cropped_dir_path))
                    Directory.CreateDirectory(cropped_dir_path);

                if (!Directory.Exists(full_dir_path))
                    Directory.CreateDirectory(full_dir_path);

                cropped_dir_path += "\\" + name + "_" + userName;
                full_dir_path += "\\" + name + "_" + userName;

                if (!Directory.Exists(cropped_dir_path))
                    Directory.CreateDirectory(cropped_dir_path);

                if (!Directory.Exists(full_dir_path))
                    Directory.CreateDirectory(full_dir_path);

                String cropped_file_path = cropped_dir_path + "\\" + name + "_" + snapshotCount + ".png";
                String full_file_path = full_dir_path + "\\" + name + "_" + snapshotCount + ".png";


                while (File.Exists(cropped_file_path))
                    cropped_file_path = cropped_dir_path + "\\" + name + "_" + ++snapshotCount + ".png";

                while (File.Exists(full_file_path))
                    full_file_path = full_dir_path + "\\" + name + "_" + ++snapshotCount + ".png";

                croppedFrame.Save(cropped_file_path, GetEncoder(ImageFormat.Png), encoderParameters);
                if (SAVE_FULL_IMG)
                {
                    originalFullFrame.Save(full_file_path, GetEncoder(ImageFormat.Png), encoderParameters);
                }
            }
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
    }
}
