using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GestureStudio
{
    public partial class MainWindow : Form
    {
        private int framesCount = 0;
        private GestureModel model;
        private bool disabled;
        private bool classifying;


        public MainWindow()
        {
            this.model = new GestureModel();
            Gestures gestures = Gestures.GetInstance();
            this.disabled = false;
            this.classifying = false;
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SynchronizationContext ctx = SynchronizationContext.Current;

            this.model.FrameReady += (s, args) =>
            {
                if (disabled)
                {
                    return;
                }
                /*
                Bitmap fullFrame = this.model.RawDepthFrame.ToBitmap();
                Bitmap croppedFrame = this.model.CroppedFrame.ToBitmap();

                using (Graphics g = Graphics.FromImage(fullFrame))
                {
                    int startX = this.model.CropStartX;
                    int startY = this.model.CropStartY;
                    int croppedWidth = this.model.CroppedFrame.Width;
                    int croppedHeight = this.model.CroppedFrame.Height;

                    Pen pen = new System.Drawing.Pen(System.Drawing.Color.Red, 5);
                    g.DrawRectangle(pen, startX + croppedWidth / 2, startY + croppedHeight / 2, 1, 1);
                    g.DrawRectangle(pen, startX, startY, croppedWidth, croppedHeight);
                }
                ctx.Post((o) =>
                {
                    this.fullFrameStream.Image = fullFrame;
                    this.croppedFrameStream.Image = croppedFrame;
                    framesCount++;
                }, null);
                 */
            };

            this.model.CategoryDetected += (s, args) =>
            {
                if (disabled)
                {
                    return;
                }

                ctx.Post((o) =>
                {
                    int label = (int)o;
                    if (GestureStudio.GENERIC_GESTURES)
                        this.currentGesture.Text = "Your Gesture: [" + Gestures.getGestureName(label) + "]";
                    else
                        this.currentGesture.Text = "Your Gesture: [" + LabelToString(label) + "]";
                }, args.CategoryLabel);
            };

            /*
            this.model.ImageCollectionFinished += (s, args) =>
            {
                this.model.Stop();
                ctx.Post((o) =>
                {
                    this.message.Text = "Image collection finished. Building new prediction model now...";
                }, null);
            };
            */
            /*
            this.model.NewModelReady += (s, args) =>
            {
                ctx.Post((o) =>
                {
                    this.message.Text = "New prediction model ready.";
                }, null);
            };
             */
            /*
            this.model.StatusChanged += (s, args) =>
            {
                ctx.Post((o) =>
                {
                    this.modelStatusDisplay.Text = args.Status;
                }, null);
            };
            */
            /*
            System.Timers.Timer fpsCounter = new System.Timers.Timer(1000);
            fpsCounter.AutoReset = true;
            fpsCounter.Elapsed += (src, args) =>
            {
                if (disabled)
                {
                    return;
                }
                ctx.Post((o) =>
                {
                    this.framesPerSecond.Text = "FPS = " + framesCount;
                    framesCount = 0;
                }, null);
            };

            fpsCounter.Start();
            */
            this.model.BeginInitialize();
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
                case 7:
                    return "Circle";
                case 8:
                    return "Stop";
            }
            return "";
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (classifying)
            {
                classifying = false;
                this.model.Stop();
                this.controlButton.Text = "Start";
            }
            else
            {
                classifying = true;
                this.model.StartClassify();
                this.controlButton.Text = "Stop";
            }
                
        }

        public void Disable()
        {
            this.disabled = true;
        }

        public void Enable()
        {
            this.disabled = false;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BindToApplications_Click(object sender, EventArgs e)
        {
            this.mainWindowTabs.SelectedTab = this.BindToApplications;
        }

        private void AddNewGesturesButton_Click(object sender, EventArgs e)
        {
            this.mainWindowTabs.SelectedTab = this.AddNewGestures;
        }

    }
}
