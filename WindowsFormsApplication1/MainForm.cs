using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GestureStudio
{
    public partial class MainForm : Form
    {
        private int framesCount = 0;
        private GestureModel model;
        private bool disabled;

        public MainForm()
        {
            this.model = new GestureModel();
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
                        this.fullFrameStream.Image = fullFrame;
                        this.croppedFrameStream.Image = croppedFrame;
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
                    this.message.Text = LabelToString(label);
                }, args.CategoryLabel);
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
        
        private void startButton_Click(object sender, EventArgs e)
        {
            if (this.classifyModeButton.Checked)
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
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.model.Stop();
        }
    }
}