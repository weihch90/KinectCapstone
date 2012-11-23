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
        public static double Width_To_Height_Ratio = 1;
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
            loadTable();
        }

        private void loadTable()
        {
            
            string[] gesture_names = { "here", "are", "the", "gesture", "names", "six" };
            string[][] key_binding_test = { new string[] {"a", "b"}, 
                                       new string[] {"y"},
                                        new string[] {"z", "x"},
                                        new string [] {"test", "data"},
                                        new string [] {"gangnam", "style"},
                                        new string [] {"herp", "derp"}};


            for (int j = 0; j < gesture_names.Length; j++)
            {
                Label l = new Label();
                l.Text = gesture_names[j];
                l.Size = new Size(200, 30);
                gestureBindingsTable.Controls.Add(l, 0, j);
            }
            for (int row = 0; row < gesture_names.Length; row++)
            {
                for (int col = 0; col < key_binding_test[row].Length; col++)
                {
                    Label l = new Label();
                    l.Text = key_binding_test[row][col];
                    l.Size = new Size(200, 30);
                    gestureBindingsTable.Controls.Add(l, col + 1, row);
                }
            }
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
                    Bitmap fitFull = new Bitmap(fullFrame, this.mainWindow_full.Width, this.mainWindow_full.Height);
                    Bitmap fitCropped;

                    // make sure the cropped image has area
                    if (croppedFrame.Height > 0 && croppedFrame.Width > 0)
                    {
                        // resize images in order to fit into picture box in the home tab
                        double croppedRatio_w_h = (double)croppedFrame.Width / croppedFrame.Height;
                        if (croppedRatio_w_h > Width_To_Height_Ratio)  // cropped image is long in horizontal
                        {
                            fitCropped = new Bitmap(croppedFrame, this.mainWindow_cropped.Width, (int)(this.mainWindow_cropped.Width / croppedRatio_w_h));
                        }
                        else  // cropped image is long in vertical
                        {
                            fitCropped = new Bitmap(croppedFrame, (int)(this.mainWindow_cropped.Height * croppedRatio_w_h), this.mainWindow_cropped.Height);
                        }
                    }
                    else
                        fitCropped = null;

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
            this.mainWindowTabs.SelectedTab = this.settingsTab;
        }

        private void TutorialButton_Click(object sender, EventArgs e)
        {
            this.mainWindowTabs.SelectedTab = this.tutorialTab;
        }
        
        // end Home tab buttons

        // Settings tab buttons



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

        private void addGestureButton_Click(object sender, EventArgs e)
        {
            // returns a string form of the given keybinding or 
            // null if no text was inputted to the text box;
            //usage: 
            using (MainForm mainForm = new MainForm())
            {
                if (DialogResult.OK == mainForm.ShowDialog())
                {
                    //label1.Text = mainForm.getKeyBind();
                }
                else
                {
                    //nothing was found
                }

            }
            
        }


    }
}
