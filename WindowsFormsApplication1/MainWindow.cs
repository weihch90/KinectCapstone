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

namespace GestureStudio
{
    public partial class MainWindow : Form
    {
        public static double Width_To_Height_Ratio = 1;
        private int framesCount = 0;
        private GestureModel model;
        private Gestures gestures;
        private KeyControls keyControls;
        private Control controller;
        private Stopwatch timer; 
        private Dictionary<int, int> gestureCounts;
        private bool disabled;
        private bool classifying;

        private const int TableCellHeight = 30;
        private const int TableCellWidth = 100;
 
        public MainWindow()
        {
            this.disabled = false;
            this.classifying = false;
            InitializeComponent();


        }

        private void loadTable()
        {
            gestureDataGridView.Columns.Clear();
            Dictionary<int, GestureInfo> gestures = Gestures.getGestures();
            string[] applications = KeyControls.getApplications();
            
            // create table with correct size first
            gestureDataGridView.Columns.Add("Gesture\\App", "Gesture\\App");
            for (int i = 0; i < applications.Length; i++)
            {
                gestureDataGridView.Columns.Add(applications[i], applications[i]);
            }

            foreach (KeyValuePair<int, GestureInfo> gesturePair in gestures)
            {
                string[] gestureInfo = new string[gesturePair.Value.getAllCommands().Count + 1];
                gestureInfo[0] = gesturePair.Value.getName(); // gesture name
                int index = 1;
                foreach (KeyValuePair<int, AppKeyInfo> command in gesturePair.Value.getAllCommands())
                {
                    gestureInfo[index++] = command.Value.ToString();
                }
                gestureDataGridView.Rows.Add(gestureInfo);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // instance initialization requires UI thread, wait until load
            this.model = GestureModel.Instance;
            this.gestures = Gestures.Instance;
            this.keyControls = KeyControls.Instance;
            this.controller = new Control();
            this.loadTable();
            this.timer = new Stopwatch();
            this.gestureCounts = new Dictionary<int,int>();
           

            // direct to tutorial page if necessary
            string[] lines = File.ReadAllLines(GestureStudio.SettingFile);
            string[] directTutorial = lines[0].Split(':');
            if (directTutorial[0] == "directTutorial" && directTutorial[1] == "yes")
            {
                using (DirectToTutorialForm directForm = new DirectToTutorialForm())
                {
                    DialogResult result = directForm.ShowDialog();
                    if (DialogResult.Yes == result)
                    {
                        this.mainWindowTabs.SelectedTab = this.tutorialTab;
                    }
                    else if (DialogResult.No == result)
                    {
                        // don't show this dialog next time
                        if (directForm.isIgnoreChecked())
                        {
                            using (StreamWriter file = new StreamWriter(GestureStudio.SettingFile))
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append(directTutorial[0] + ":no");
                                file.WriteLine(sb.ToString());
                            }
                        }
                    }
                    else
                    {
                    }

                }
            }

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
                    {
                        if (!this.gestureCounts.ContainsKey(label))
                            this.gestureCounts.Add(label, 0);
                        this.gestureCounts[label]++;
                        
                        this.timer.Stop();
                        if (this.timer.ElapsedMilliseconds > 0)
                        {
                            this.timer.Reset();
                            int maxLabel = 2;
                            int maxCount = -1;
                            int countSum = 0;
                            foreach (int key in this.gestureCounts.Keys)
                            {
                                if (maxCount < this.gestureCounts[key])
                                {
                                    maxLabel = key;
                                    maxCount = this.gestureCounts[key];
                                    countSum += this.gestureCounts[key];
                                }
                            }
                            this.gestureCounts.Clear();
                                                        // lookup which window is focused and find if it is in the gestures list
                            this.mainWindow_status.Text = "Your Gesture: [" + Gestures.getGestureName(maxLabel) + "]";
                            // string focusedApp = ...
                            // int appId = Gestures.getAppId(focusedApp);
                            AppKeyInfo appInfo = Gestures.getAppKeyForGesture(maxLabel, 0 /*appId*/);
                            if (appInfo == null || KeyControls.getKeyMatches()[0/*appId*/] == null)
                                return;

                            
                            this.controller.parseThenExecute(KeyControls.getKeyMatches()[0][appInfo.getCommand()]);
                        }
                        this.timer.Start();
                        

                        
                    } 
                    else
                        this.mainWindow_status.Text = "Your Gesture: [" + LabelToString(label) + "]";
                }, args.CategoryLabel);
            };


        }

        private String LabelToString(int i)
        {
            switch (i)
            {
                case 1:
                    return "Thumbs Up";
                case 2:
                    return "L";
                case 3:
                    return "OK";
                case 4:
                    return "Thumbs Down";
                case 5:
                    return "C";
                case 6:
                    return "Noise";
                case 7:
                    return "Paper";
                case 8:
                    return "Rock";
                case 9:
                    return "Six";
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

        private void editApplicationButton_Click(object sender, EventArgs e)
        {

        }

        private void editBindingButton_Click(object sender, EventArgs e)
        {
            // returns a string form of the given keybinding or 
            // null if no text was inputted to the text box;
            //usage: 
            using (KeyBindForm keyForm = new KeyBindForm())
            {
                if (DialogResult.OK == keyForm.ShowDialog())
                {
                    string gestureName = keyForm.getGestureName();
                    string appName = keyForm.getAppName();

                    if (keyForm.getKeyBind() != null && keyForm.getKeyBind().Trim() != "" 
                        && keyForm.getGestureName() != null && keyForm.getAppName() != null)
                    {
                        Gestures.setAppKeyForGesture(gestureName, appName, keyForm.getKeyBind());
                        Gestures.saveData();
                        gestureDataGridView.Hide();
                        gestureDataGridView.Controls.Clear();
                        loadTable();
                        gestureDataGridView.Show();
                    }
                    //label1.Text = mainForm.getKeyBind();
                }
                else
                {
                    //nothing was found
                }

            }
        }

        private void chooseDataFile_Click(object sender, EventArgs e)
        {
            DialogResult result = chooseFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = chooseFileDialog.FileName;
                try
                {
                    Gestures.loadData(file);
                    gestureDataGridView.Hide();
                    gestureDataGridView.Controls.Clear();
                    loadTable();
                    gestureDataGridView.Show();
                }
                catch (IOException)
                {
                }
            }
        }


        private void collectDataButton_Click(object sender, EventArgs e)
        {
            using (CollectImageForm collectForm = new CollectImageForm())
            {
                if (DialogResult.OK == collectForm.ShowDialog())
                {
                    //label1.Text = mainForm.getKeyBind();
                }
                else
                {
                    //nothing was found
                }

            }
        }


        // end settings tab buttons

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

        private void chooseDataFileDialog_Ok(object sender, CancelEventArgs e)
        {

        }

        private void mainWindowTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainWindowTabs.SelectedIndex == 1)
            {
                try
                {
                    Gestures.loadData(Gestures.getPath());
                    gestureDataGridView.Hide();
                    gestureDataGridView.Controls.Clear();
                    loadTable();
                    gestureDataGridView.Show();
                }
                catch (IOException)
                {
                }
            }
        }

        private void gestureDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            gestureDataGridView.ClearSelection();
        }




    }
}
