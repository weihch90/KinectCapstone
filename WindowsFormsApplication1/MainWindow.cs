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
            Dictionary<int, GestureInfo> gestures = Gestures.getGestures();
            // create table with correct size first
            this.gestureBindingsTable.RowCount = gestures.Keys.Count + 1;
            this.gestureBindingsTable.ColumnCount = Gestures.Applications.Length + 1;
            this.gestureBindingsTable.MinimumSize
                = new Size(gestureBindingsTable.ColumnCount * TableCellWidth, gestureBindingsTable.RowCount * TableCellHeight);
            int row = 0;
            
		    // set column width
            foreach (ColumnStyle style in gestureBindingsTable.ColumnStyles)
            {
                style.SizeType = SizeType.Absolute;
                style.Width = TableCellWidth;
            }

            // first row of the table
            for (int col = 1; col < gestureBindingsTable.ColumnCount; col++)
            {
                Label appName = new Label();
                appName.Text = Gestures.Applications[col - 1];
                gestureBindingsTable.Controls.Add(appName, col, row);
            }
            row++;

            // then assign values in to the table
            foreach (KeyValuePair<int, GestureInfo> gesturePairs in gestures)
            {
                Label gestureName = new Label();

                gestureName.Text = gesturePairs.Value.getName();
                gestureBindingsTable.Controls.Add(gestureName, 0, row);
                foreach (KeyValuePair<int, AppKeyInfo> commands in gesturePairs.Value.getAllCommands())
                {
                    Label key = new Label();
                    key.Text = commands.Value.ToString();
                    // save to the row associated with specific app index
                    gestureBindingsTable.Controls.Add(key, commands.Key + 1, row);
                }
                row++;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            // instance initialization requires UI thread, wait until load
            this.model = GestureModel.Instance;
            this.gestures = Gestures.Instance;
            this.loadTable();

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
                    return "Six";
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
                        gestureBindingsTable.Hide();
                        gestureBindingsTable.Controls.Clear();
                        loadTable();
                        gestureBindingsTable.Show();

                        //int rowIndex = Gestures.getGestureIndex(gestureName) + 1;
                        //gestureBindingsTable.RowStyles.RemoveAt(rowIndex);
                        
                        // adding new row
                        //GestureInfo gesture = Gestures.getGestures()[Gestures.getGestureId(gestureName)];
                        //RowStyle style = new RowStyle();
                        //gestureBindingsTable.RowStyles.Add(style);

                        /*Dictionary<int, AppKeyInfo> commands = gesture.getAllCommands();
                        for (int i = 1; i < gestureBindingsTable.ColumnCount; i++)
                        {
                            gestureBindingsTable.Controls.RemoveAt(i + rowIndex * gestureBindingsTable.ColumnCount);
                            Label key = new Label();
                            if (commands.ContainsKey(i))
                                key.Text = commands[i].toString();
                            else
                                key.Text = "";
                            // save to the row associated with specific app index
                            gestureBindingsTable.Controls.Add(key, i, rowIndex);
                        }
                        */

                        //gestureNameLabel.Text = gesture.getName();
                        //gestureBindingsTable.Controls.Add(gestureNameLabel, 0, rowIndex);

                        /*foreach (KeyValuePair<int, AppKeyInfo> commands in gesture.getAllCommands())
                        {
                            Label key = new Label();
                            key.Text = commands.Value.toString();
                            // save to the row associated with specific app index
                            gestureBindingsTable.Controls.Add(key, commands.Key + 1, rowIndex);
                        }
                        */

                    }
                    //label1.Text = mainForm.getKeyBind();
                }
                else
                {
                    //nothing was found
                }

            }
        }

        private void addTableRow(int index)
        {


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


    }
}
