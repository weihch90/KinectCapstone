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
        private Gestures gestures;
        private bool disabled;
        private bool classifying;


        public MainWindow()
        {
            this.model = new GestureModel();
            gestures = Gestures.GetInstance();
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
                    // resize images in order to fit into picture box in the home tab
                    double croppedRatio_w_h = croppedFrame.Width / croppedFrame.Height;
                    
                    Bitmap fitFull = new Bitmap(fullFrame, this.mainWindow_full.Width, this.mainWindow_full.Height);
                    Bitmap fitCropped = new Bitmap(croppedFrame, this.mainWindow_cropped.Width, this.mainWindow_cropped.Height);
                    
                    this.mainWindow_full.Image = fitFull;
                    this.mainWindow_cropped.Image = fitCropped;
                    framesCount++;
                }, null);

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
                        this.mainWindow_status.Text = "Your Gesture: [" + Gestures.getGestureName(label) + "]";
                    else
                        this.mainWindow_status.Text = "Your Gesture: [" + LabelToString(label) + "]";
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

        public void Disable()
        {
            this.disabled = true;
        }

        public void Enable()
        {
            this.disabled = false;
        }


        // button controls

        // Home tab buttons
        /*
         * Start/Stop recognition
         */
        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (classifying)
            {
                this.model.Stop();
                this.controlButton.Text = "Start";
            }
            else
            {
                this.model.StartClassify();
                this.controlButton.Text = "Stop";
            }
            this.classifying = !this.classifying;

        }

        /*
         * Click on buttons on main window to go to different tabs
         */
        private void BindToApplications_Click(object sender, EventArgs e)
        {
            this.mainWindowTabs.SelectedTab = this.BindToApplications;
        }

        private void AddNewGesturesButton_Click(object sender, EventArgs e)
        {
            this.mainWindowTabs.SelectedTab = this.AddNewGestures;
        }

        private void TutorialButton_Click(object sender, EventArgs e)
        {
            this.mainWindowTabs.SelectedTab = this.tutorialTab;
        }
        
        // end Home tab buttons



        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void homeLabel_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
