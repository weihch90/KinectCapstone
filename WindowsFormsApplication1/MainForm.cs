using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GestureStudio
{
    public partial class MainForm : Form
    {
        public static double Width_To_Height_Ratio = 1;
        private int framesCount = 0;
        private GestureModel model;
        private bool disabled;

        public MainForm()
        {
            this.disabled = false;
            InitializeComponent();
        }

        public void Disable()
        {
            this.disabled = true;
        }

        public void Enable()
        {
            this.disabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // instance initialization requires UI thread, wait until load
            this.model = GestureModel.Instance;

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
                    
                    ctx.Post((o) => {
                        Bitmap fitFull = new Bitmap(fullFrame, this.fullFrameStream.Width, this.fullFrameStream.Height);
                        Bitmap fitCropped;

                        // make sure the cropped image has area
                        if (croppedFrame.Height > 0 && croppedFrame.Width > 0)
                        {
                            // resize images in order to fit into picture box in the home tab
                            double croppedRatio_w_h = (double)croppedFrame.Width / croppedFrame.Height;
                            if (croppedRatio_w_h > Width_To_Height_Ratio)  // cropped image is long in horizontal
                            {
                                fitCropped = new Bitmap(croppedFrame, this.croppedFrameStream.Width, (int)(this.croppedFrameStream.Width / croppedRatio_w_h));
                            }
                            else  // cropped image is long in vertical
                            {
                                fitCropped = new Bitmap(croppedFrame, (int)(this.croppedFrameStream.Height * croppedRatio_w_h), this.croppedFrameStream.Height);
                            }
                        }
                        else
                            fitCropped = null;

                        this.croppedFrameStream.Image = fitCropped;
                        this.fullFrameStream.Image = fitFull;

                        framesCount++;
                    }, null);
                };

            this.model.ImageCollectionFinished += (s, args) =>
                {
                    this.model.Stop();
                    ctx.Post((o) =>
                    {
                        this.message.Text = "Image collection finished. Building new prediction model now...";
                    }, null);
                };

            this.model.NewModelReady += (s, args) =>
                {
                    ctx.Post((o) =>
                    {
                        this.message.Text = "New prediction model ready.";
                    }, null);
                };

            this.model.StatusChanged += (s, args) =>
                {
                    ctx.Post((o) =>
                        {
                            this.modelStatusDisplay.Text = args.Status;
                            this.message.Text = args.Status;
                        }, null);
                };

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
        }
                
        private void startButton_Click(object sender, EventArgs e)
        {
            /*if (this.classifyModeButton.Checked)
            {
                this.model.StartClassify();
            }
            else if (this.trainingModeButton.Checked)
            {
                this.model.StartLearning();
            }
            else
            {
                MessageBox.Show("Pick a program mode to run.");
            }*/
            this.model.StartLearning();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.model.Stop();
        }

        private void chooseFeatureFileButton_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            FD.Filter = "feature files (*.mat)|*.mat|All files (*.*)|*.*";
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = System.IO.Path.GetFileName(FD.FileName);
                this.featureFilePath.Text = fileToOpen;
                this.model.FeatureFilePath = @fileToOpen;
            }
        }
    }
}