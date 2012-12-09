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

        private Stopwatch volumeTimer;
 

        private const int TableCellHeight = 30;
        private const int TableCellWidth = 100;
 
        public MainWindow()
        {
            this.disabled = false;
            this.classifying = false;
            InitializeComponent();
        }

        private void LoadTable()
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
                if (!gestureInfo[0].Equals("Noise"))
                {
                    int index = 1;
                    foreach (KeyValuePair<int, AppKeyInfo> command in gesturePair.Value.getAllCommands())
                    {
                        gestureInfo[index++] = command.Value.ToString();
                    }
                    gestureDataGridView.Rows.Add(gestureInfo);
                }
            }
        }

        private void LoadTutorial()
        {
            tutorialGridView.Rows.Clear();
            tutorialGridView.Columns.Clear();
            string[] imagePaths = Directory.GetFiles(GestureStudio.GestureImagePath);

            for (int i = 0; i < imagePaths.Length / 3; i++)
            {
                DataGridViewImageColumn ImageColumn = new System.Windows.Forms.DataGridViewImageColumn() { HeaderText = "Gesture" };
                DataGridViewTextBoxColumn TextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn() { HeaderText = "Name" };
                tutorialGridView.Columns.Add(ImageColumn);
                tutorialGridView.Columns.Add(TextColumn);
            }

            for (int i = 0; i < imagePaths.Length / 3; i++)
            {
                // Read in three images. Add to row.
                FileStream fs1 = File.OpenRead(imagePaths[i * 3]);
                byte[] by1 = new byte[fs1.Length];
                fs1.Read(by1, 0, by1.Length);
                String name1 = System.IO.Path.GetFileNameWithoutExtension(imagePaths[i * 3]);
                FileStream fs2 = File.OpenRead(imagePaths[i * 3 + 1]);
                byte[] by2 = new byte[fs2.Length];
                fs2.Read(by2, 0, by2.Length);
                String name2 = System.IO.Path.GetFileNameWithoutExtension(imagePaths[i * 3 + 1]);
                FileStream fs3 = File.OpenRead(imagePaths[i * 3 + 2]);
                byte[] by3 = new byte[fs3.Length];
                fs3.Read(by3, 0, by3.Length);
                String name3 = System.IO.Path.GetFileNameWithoutExtension(imagePaths[i * 3 + 2]);
                tutorialGridView.Rows.Add(by1, name1, by2, name2, by3, name3);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // instance initialization requires UI thread, wait until load
            this.model = GestureModel.Instance;
            this.gestures = Gestures.Instance;
            this.keyControls = KeyControls.Instance;
            this.controller = new Control();
            this.LoadTable();
            this.LoadTutorial();
            this.timer = new Stopwatch();
            this.volumeTimer = new Stopwatch();
            this.volumeTimer.Start();
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
                    if (croppedWidth > 10 && croppedHeight > 10 && croppedHeight < 200 && croppedHeight < 200) {
                        g.DrawRectangle(pen, startX + croppedWidth / 2, startY + croppedHeight / 2, 1, 1);
                        g.DrawRectangle(pen, startX, startY, croppedWidth, croppedHeight);
                    }
                }
                ctx.Post((o) =>
                {
                    Bitmap fitFull = new Bitmap(fullFrame, this.mainWindow_full.Width, this.mainWindow_full.Height);
                    Bitmap fitCropped;
                    if (GestureStudio.DISPLAY_DETECTED_GESTURE_IMG)
                    {


                    }
                    else
                    {
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

                        this.mainWindow_cropped.Image = fitCropped;
                    }




                    this.mainWindow_full.Image = fitFull;

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

                        if (this.timer.ElapsedMilliseconds > 0 && this.volumeTimer.ElapsedMilliseconds > 2500)
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
                            if (GestureStudio.DISPLAY_DETECTED_GESTURE_IMG) {
                                string img_path = GestureStudio.GestureImagePath + "/" + Gestures.getGestureName(maxLabel) + ".png";

                                Bitmap resized_img = null;
                                if (File.Exists(img_path))
                                {
                                    Bitmap img = new Bitmap(img_path);
                                    // resize

                                    resized_img = new Bitmap(img, this.mainWindow_cropped.Width, this.mainWindow_cropped.Height);
                                }

                                this.mainWindow_cropped.Image = resized_img;
                            }
                            // lookup which window is focused and find if it is in the gestures list
                            if (Gestures.getGestureName(maxLabel) != null && Gestures.getGestureName(maxLabel).ToLower() != "noise")
                            {

                                this.mainWindow_status.Text = "Your Gesture: [" + Gestures.getGestureName(maxLabel) + "]";
                                if (Gestures.getGestures()[maxLabel].getAllCommands().Count != 0)
                                    this.commandLabel.Text = "[" + Gestures.getGestures()[maxLabel].getAllCommands()[0].getCommand() + "]";
                                else
                                    this.commandLabel.Text = "[]";
                            }
                            else
                            {
                                this.mainWindow_status.Text = "Your Gesture: []";
                                this.commandLabel.Text = "[]";
                            }
                            // string focusedApp = ...
                            // int appId = Gestures.getAppId(focusedApp);
                            AppKeyInfo appInfo = Gestures.getAppKeyForGesture(maxLabel, 0 /*appId*/);
                            if (appInfo == null || KeyControls.getKeyMatches()[0/*appId*/] == null)
                                return;

                            string detectedCommand = KeyControls.getKeyMatches()[0][appInfo.getCommand()];
                            if (detectedCommand != null && !detectedCommand.Equals("f8") && !detectedCommand.Equals("f9"))
                            {
                                this.volumeTimer.Reset();
                                this.volumeTimer.Start();
                            }
                            this.controller.parseThenExecute(detectedCommand);
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
                case 6 :
                    return "Back";
                case 7:
                    return "Noise";
                case 8:
                    return "Paper";
                case 9:
                    return "Forward";
                case 10:
                    return "Rock on";
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
                this.mainWindow_status.Text = "Your Gesture: []";
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
                        LoadTable();
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
                    LoadTable();
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
                    LoadTable();
                    gestureDataGridView.Show();
                }
                catch (IOException)
                {
                }
            }

            this.model.Stop();
            this.controlButton.Text = "Start";
            this.mainWindow_status.Text = "Your Gesture: []";
            this.classifying = false;

        }

        private void gestureDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            gestureDataGridView.ClearSelection();
        }

        private void chooseModelButton_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            FD.Filter = "model files (*.svm)|*.svm|All files (*.*)|*.*";
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = System.IO.Path.GetFileName(FD.FileName);
                this.model.ModelFilePath = @fileToOpen;
                this.modelFileLabel.Text = fileToOpen;

                // Update gesture data based on model chosen.
                if (fileToOpen.Equals(GestureStudio.ModelFileNew))
                    Gestures.loadData(GestureStudio.GesturesDataPathNew);
                else if (fileToOpen.Equals(GestureStudio.ModelFileEmpty))
                    Gestures.loadData(GestureStudio.GesturesDataPathEmpty);
                else // Demo version
                    Gestures.loadData(GestureStudio.GesturesDataPathDemo);
            }
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            this.mainWindowTabs.SelectedTab = this.homeTab;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            Gestures.resetGestureInfo();
            Gestures.saveData();
            Gestures.loadData(Gestures.getPath());
            LoadTable();
        }
    }
}
