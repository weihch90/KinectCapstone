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



namespace GestureStudio
{
    public partial class MainForm : Form
    {
        private int framesCount = 0;
        private GestureModel model;

        public MainForm()
        {
            this.model = new GestureModel();
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

            SynchronizationContext ctx = SynchronizationContext.Current;

            this.model.FrameReady += (s, args) =>
                {
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
                    ctx.Post((o) =>
                    {
                        int label = (int)o;
                        this.message.Text = LabelToString(label);
                    }, args.CategoryLabel);
                };

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
                MessageBox.Show("Pick a program mode to run");
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.model.Stop();
        }
    }
}