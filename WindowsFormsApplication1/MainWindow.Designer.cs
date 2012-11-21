namespace GestureStudio
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainWindowTabs = new System.Windows.Forms.TabControl();
            this.Home = new System.Windows.Forms.TabPage();
            this.mainWindow_cropped = new System.Windows.Forms.PictureBox();
            this.mainWindow_full = new System.Windows.Forms.PictureBox();
            this.tutorialButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.controlButton = new System.Windows.Forms.Button();
            this.mainWindow_status = new System.Windows.Forms.Label();
            this.bindToApplicationButton = new System.Windows.Forms.Button();
            this.addGestureButton = new System.Windows.Forms.Button();
            this.homeLabel = new System.Windows.Forms.Label();
            this.AddNewGestures = new System.Windows.Forms.TabPage();
            this.stopTrain = new System.Windows.Forms.Button();
            this.startTrain = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BindToApplications = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tutorialTab = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.mainWindowTabs.SuspendLayout();
            this.Home.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_cropped)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_full)).BeginInit();
            this.AddNewGestures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.BindToApplications.SuspendLayout();
            this.tutorialTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // mainWindowTabs
            // 
            this.mainWindowTabs.Controls.Add(this.Home);
            this.mainWindowTabs.Controls.Add(this.AddNewGestures);
            this.mainWindowTabs.Controls.Add(this.BindToApplications);
            this.mainWindowTabs.Controls.Add(this.tutorialTab);
            this.mainWindowTabs.Location = new System.Drawing.Point(1, 1);
            this.mainWindowTabs.Name = "mainWindowTabs";
            this.mainWindowTabs.SelectedIndex = 0;
            this.mainWindowTabs.Size = new System.Drawing.Size(817, 504);
            this.mainWindowTabs.TabIndex = 0;
            // 
            // Home
            // 
            this.Home.Controls.Add(this.mainWindow_cropped);
            this.Home.Controls.Add(this.mainWindow_full);
            this.Home.Controls.Add(this.tutorialButton);
            this.Home.Controls.Add(this.label1);
            this.Home.Controls.Add(this.controlButton);
            this.Home.Controls.Add(this.mainWindow_status);
            this.Home.Controls.Add(this.bindToApplicationButton);
            this.Home.Controls.Add(this.addGestureButton);
            this.Home.Controls.Add(this.homeLabel);
            this.Home.Location = new System.Drawing.Point(4, 22);
            this.Home.Name = "Home";
            this.Home.Padding = new System.Windows.Forms.Padding(3);
            this.Home.Size = new System.Drawing.Size(809, 478);
            this.Home.TabIndex = 0;
            this.Home.Text = "Home";
            this.Home.UseVisualStyleBackColor = true;
            // 
            // mainWindow_cropped
            // 
            this.mainWindow_cropped.BackColor = System.Drawing.Color.Black;
            this.mainWindow_cropped.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mainWindow_cropped.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainWindow_cropped.Location = new System.Drawing.Point(542, 111);
            this.mainWindow_cropped.Name = "mainWindow_cropped";
            this.mainWindow_cropped.Size = new System.Drawing.Size(120, 120);
            this.mainWindow_cropped.TabIndex = 9;
            this.mainWindow_cropped.TabStop = false;
            // 
            // mainWindow_full
            // 
            this.mainWindow_full.BackColor = System.Drawing.Color.Black;
            this.mainWindow_full.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mainWindow_full.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainWindow_full.Location = new System.Drawing.Point(158, 111);
            this.mainWindow_full.Name = "mainWindow_full";
            this.mainWindow_full.Size = new System.Drawing.Size(360, 240);
            this.mainWindow_full.TabIndex = 8;
            this.mainWindow_full.TabStop = false;
            // 
            // tutorialButton
            // 
            this.tutorialButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tutorialButton.Location = new System.Drawing.Point(627, 369);
            this.tutorialButton.Name = "tutorialButton";
            this.tutorialButton.Size = new System.Drawing.Size(71, 59);
            this.tutorialButton.TabIndex = 7;
            this.tutorialButton.Text = "Tutorial";
            this.tutorialButton.UseVisualStyleBackColor = true;
            this.tutorialButton.Click += new System.EventHandler(this.TutorialButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Control your applications with hand gestures";
            // 
            // controlButton
            // 
            this.controlButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.controlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlButton.Location = new System.Drawing.Point(127, 369);
            this.controlButton.Name = "controlButton";
            this.controlButton.Size = new System.Drawing.Size(155, 59);
            this.controlButton.TabIndex = 0;
            this.controlButton.Text = "Start";
            this.controlButton.UseVisualStyleBackColor = true;
            this.controlButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // mainWindow_status
            // 
            this.mainWindow_status.AutoSize = true;
            this.mainWindow_status.Location = new System.Drawing.Point(22, 447);
            this.mainWindow_status.Name = "mainWindow_status";
            this.mainWindow_status.Size = new System.Drawing.Size(38, 13);
            this.mainWindow_status.TabIndex = 4;
            this.mainWindow_status.Text = "Ready";
            // 
            // bindToApplicationButton
            // 
            this.bindToApplicationButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bindToApplicationButton.Location = new System.Drawing.Point(484, 368);
            this.bindToApplicationButton.Name = "bindToApplicationButton";
            this.bindToApplicationButton.Size = new System.Drawing.Size(137, 60);
            this.bindToApplicationButton.TabIndex = 3;
            this.bindToApplicationButton.Text = "Bind To Applications";
            this.bindToApplicationButton.UseVisualStyleBackColor = true;
            this.bindToApplicationButton.Click += new System.EventHandler(this.BindToApplications_Click);
            // 
            // addGestureButton
            // 
            this.addGestureButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.addGestureButton.Location = new System.Drawing.Point(323, 368);
            this.addGestureButton.Name = "addGestureButton";
            this.addGestureButton.Size = new System.Drawing.Size(155, 60);
            this.addGestureButton.TabIndex = 2;
            this.addGestureButton.Text = "Add New Gestures";
            this.addGestureButton.UseVisualStyleBackColor = true;
            this.addGestureButton.Click += new System.EventHandler(this.AddNewGesturesButton_Click);
            // 
            // homeLabel
            // 
            this.homeLabel.AutoSize = true;
            this.homeLabel.Font = new System.Drawing.Font("Times New Roman", 43.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeLabel.Location = new System.Drawing.Point(215, 13);
            this.homeLabel.Name = "homeLabel";
            this.homeLabel.Size = new System.Drawing.Size(373, 67);
            this.homeLabel.TabIndex = 5;
            this.homeLabel.Text = "Gesture Studio";
            this.homeLabel.Click += new System.EventHandler(this.homeLabel_Click);
            // 
            // AddNewGestures
            // 
            this.AddNewGestures.Controls.Add(this.stopTrain);
            this.AddNewGestures.Controls.Add(this.startTrain);
            this.AddNewGestures.Controls.Add(this.pictureBox2);
            this.AddNewGestures.Controls.Add(this.pictureBox1);
            this.AddNewGestures.Location = new System.Drawing.Point(4, 22);
            this.AddNewGestures.Name = "AddNewGestures";
            this.AddNewGestures.Padding = new System.Windows.Forms.Padding(3);
            this.AddNewGestures.Size = new System.Drawing.Size(809, 478);
            this.AddNewGestures.TabIndex = 1;
            this.AddNewGestures.Text = "Add New Gestures";
            this.AddNewGestures.UseVisualStyleBackColor = true;
            this.AddNewGestures.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // stopTrain
            // 
            this.stopTrain.Location = new System.Drawing.Point(606, 274);
            this.stopTrain.Name = "stopTrain";
            this.stopTrain.Size = new System.Drawing.Size(195, 59);
            this.stopTrain.TabIndex = 3;
            this.stopTrain.Text = "Stop";
            this.stopTrain.UseVisualStyleBackColor = true;
            // 
            // startTrain
            // 
            this.startTrain.Location = new System.Drawing.Point(606, 209);
            this.startTrain.Name = "startTrain";
            this.startTrain.Size = new System.Drawing.Size(195, 59);
            this.startTrain.TabIndex = 2;
            this.startTrain.Text = "Start";
            this.startTrain.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(606, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 200);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BindToApplications
            // 
            this.BindToApplications.Controls.Add(this.button1);
            this.BindToApplications.Controls.Add(this.tableLayoutPanel1);
            this.BindToApplications.Location = new System.Drawing.Point(4, 22);
            this.BindToApplications.Name = "BindToApplications";
            this.BindToApplications.Padding = new System.Windows.Forms.Padding(3);
            this.BindToApplications.Size = new System.Drawing.Size(809, 478);
            this.BindToApplications.TabIndex = 2;
            this.BindToApplications.Text = "Bind To Applications";
            this.BindToApplications.UseVisualStyleBackColor = true;
            this.BindToApplications.Click += new System.EventHandler(this.BindToApplications_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(460, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "New Bind";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(54, 38);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 188);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tutorialTab
            // 
            this.tutorialTab.Controls.Add(this.pictureBox3);
            this.tutorialTab.Location = new System.Drawing.Point(4, 22);
            this.tutorialTab.Name = "tutorialTab";
            this.tutorialTab.Padding = new System.Windows.Forms.Padding(3);
            this.tutorialTab.Size = new System.Drawing.Size(809, 478);
            this.tutorialTab.TabIndex = 3;
            this.tutorialTab.Text = "Tutorial";
            this.tutorialTab.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(41, 37);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(639, 367);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 508);
            this.Controls.Add(this.mainWindowTabs);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainWindowTabs.ResumeLayout(false);
            this.Home.ResumeLayout(false);
            this.Home.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_cropped)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_full)).EndInit();
            this.AddNewGestures.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.BindToApplications.ResumeLayout(false);
            this.tutorialTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainWindowTabs;
        private System.Windows.Forms.TabPage Home;
        private System.Windows.Forms.TabPage AddNewGestures;
        private System.Windows.Forms.TabPage BindToApplications;
        private System.Windows.Forms.Label homeLabel;
        private System.Windows.Forms.Button bindToApplicationButton;
        private System.Windows.Forms.Button addGestureButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label mainWindow_status;
        private System.Windows.Forms.Button controlButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button startTrain;
        private System.Windows.Forms.Button stopTrain;
        private System.Windows.Forms.TabPage tutorialTab;
        private System.Windows.Forms.Button tutorialButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox mainWindow_cropped;
        private System.Windows.Forms.PictureBox mainWindow_full;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}