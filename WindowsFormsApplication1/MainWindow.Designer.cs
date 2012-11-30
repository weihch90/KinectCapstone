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
            this.tutorialTab = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.chooseDataFileButton = new System.Windows.Forms.Button();
            this.editAppButton = new System.Windows.Forms.Button();
            this.addGestureButton = new System.Windows.Forms.Button();
            this.setKeyBind = new System.Windows.Forms.Button();
            this.gestureBindingsTable = new System.Windows.Forms.TableLayoutPanel();
            this.homeTab = new System.Windows.Forms.TabPage();
            this.mainWindow_cropped = new System.Windows.Forms.PictureBox();
            this.mainWindow_full = new System.Windows.Forms.PictureBox();
            this.tutorialButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.controlButton = new System.Windows.Forms.Button();
            this.mainWindow_status = new System.Windows.Forms.Label();
            this.settingsButton = new System.Windows.Forms.Button();
            this.homeLabel = new System.Windows.Forms.Label();
            this.mainWindowTabs = new System.Windows.Forms.TabControl();
            this.chooseFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.collectDataButton = new System.Windows.Forms.Button();
            this.tutorialTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.settingsTab.SuspendLayout();
            this.homeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_cropped)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_full)).BeginInit();
            this.mainWindowTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tutorialTab
            // 
            this.tutorialTab.Controls.Add(this.pictureBox3);
            this.tutorialTab.Location = new System.Drawing.Point(4, 22);
            this.tutorialTab.Name = "tutorialTab";
            this.tutorialTab.Padding = new System.Windows.Forms.Padding(3);
            this.tutorialTab.Size = new System.Drawing.Size(613, 469);
            this.tutorialTab.TabIndex = 3;
            this.tutorialTab.Text = "Tutorial";
            this.tutorialTab.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(38, 39);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(528, 395);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.collectDataButton);
            this.settingsTab.Controls.Add(this.chooseDataFileButton);
            this.settingsTab.Controls.Add(this.editAppButton);
            this.settingsTab.Controls.Add(this.addGestureButton);
            this.settingsTab.Controls.Add(this.setKeyBind);
            this.settingsTab.Controls.Add(this.gestureBindingsTable);
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(613, 469);
            this.settingsTab.TabIndex = 2;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            this.settingsTab.Click += new System.EventHandler(this.BindToApplications_Click);
            // 
            // chooseDataFileButton
            // 
            this.chooseDataFileButton.Location = new System.Drawing.Point(324, 401);
            this.chooseDataFileButton.Name = "chooseDataFileButton";
            this.chooseDataFileButton.Size = new System.Drawing.Size(93, 51);
            this.chooseDataFileButton.TabIndex = 4;
            this.chooseDataFileButton.Text = "Choose Data File";
            this.chooseDataFileButton.UseVisualStyleBackColor = true;
            this.chooseDataFileButton.Click += new System.EventHandler(this.chooseDataFile_Click);
            // 
            // editAppButton
            // 
            this.editAppButton.Location = new System.Drawing.Point(126, 400);
            this.editAppButton.Name = "editAppButton";
            this.editAppButton.Size = new System.Drawing.Size(93, 52);
            this.editAppButton.TabIndex = 2;
            this.editAppButton.Text = "Edit Application";
            this.editAppButton.UseVisualStyleBackColor = true;
            this.editAppButton.Click += new System.EventHandler(this.editApplicationButton_Click);
            // 
            // addGestureButton
            // 
            this.addGestureButton.Location = new System.Drawing.Point(27, 401);
            this.addGestureButton.Name = "addGestureButton";
            this.addGestureButton.Size = new System.Drawing.Size(93, 51);
            this.addGestureButton.TabIndex = 1;
            this.addGestureButton.Text = "New Gesture";
            this.addGestureButton.UseVisualStyleBackColor = true;
            this.addGestureButton.Click += new System.EventHandler(this.addGestureButton_Click);
            // 
            // setKeyBind
            // 
            this.setKeyBind.Location = new System.Drawing.Point(225, 401);
            this.setKeyBind.Name = "setKeyBind";
            this.setKeyBind.Size = new System.Drawing.Size(93, 51);
            this.setKeyBind.TabIndex = 3;
            this.setKeyBind.Text = "Edit Binding";
            this.setKeyBind.UseVisualStyleBackColor = true;
            this.setKeyBind.Click += new System.EventHandler(this.editBindingButton_Click);
            // 
            // gestureBindingsTable
            // 
            this.gestureBindingsTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.gestureBindingsTable.ColumnCount = 1;
            this.gestureBindingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.gestureBindingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gestureBindingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 134F));
            this.gestureBindingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gestureBindingsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.gestureBindingsTable.Location = new System.Drawing.Point(44, 40);
            this.gestureBindingsTable.Name = "gestureBindingsTable";
            this.gestureBindingsTable.RowCount = 1;
            this.gestureBindingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.gestureBindingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.gestureBindingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.gestureBindingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.gestureBindingsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.gestureBindingsTable.Size = new System.Drawing.Size(76, 31);
            this.gestureBindingsTable.TabIndex = 0;
            this.gestureBindingsTable.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // homeTab
            // 
            this.homeTab.Controls.Add(this.mainWindow_cropped);
            this.homeTab.Controls.Add(this.mainWindow_full);
            this.homeTab.Controls.Add(this.tutorialButton);
            this.homeTab.Controls.Add(this.label1);
            this.homeTab.Controls.Add(this.controlButton);
            this.homeTab.Controls.Add(this.mainWindow_status);
            this.homeTab.Controls.Add(this.settingsButton);
            this.homeTab.Controls.Add(this.homeLabel);
            this.homeTab.Location = new System.Drawing.Point(4, 22);
            this.homeTab.Name = "homeTab";
            this.homeTab.Padding = new System.Windows.Forms.Padding(3);
            this.homeTab.Size = new System.Drawing.Size(613, 469);
            this.homeTab.TabIndex = 0;
            this.homeTab.Text = "Home";
            this.homeTab.UseVisualStyleBackColor = true;
            // 
            // mainWindow_cropped
            // 
            this.mainWindow_cropped.BackColor = System.Drawing.Color.Black;
            this.mainWindow_cropped.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mainWindow_cropped.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainWindow_cropped.Location = new System.Drawing.Point(449, 111);
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
            this.mainWindow_full.Location = new System.Drawing.Point(49, 111);
            this.mainWindow_full.Name = "mainWindow_full";
            this.mainWindow_full.Size = new System.Drawing.Size(360, 240);
            this.mainWindow_full.TabIndex = 8;
            this.mainWindow_full.TabStop = false;
            // 
            // tutorialButton
            // 
            this.tutorialButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.tutorialButton.Location = new System.Drawing.Point(498, 367);
            this.tutorialButton.Name = "tutorialButton";
            this.tutorialButton.Size = new System.Drawing.Size(71, 60);
            this.tutorialButton.TabIndex = 7;
            this.tutorialButton.Text = "Tutorial";
            this.tutorialButton.UseVisualStyleBackColor = true;
            this.tutorialButton.Click += new System.EventHandler(this.TutorialButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Control your applications with hand gestures";
            // 
            // controlButton
            // 
            this.controlButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.controlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlButton.Location = new System.Drawing.Point(49, 368);
            this.controlButton.Name = "controlButton";
            this.controlButton.Size = new System.Drawing.Size(155, 60);
            this.controlButton.TabIndex = 0;
            this.controlButton.Text = "Start";
            this.controlButton.UseVisualStyleBackColor = true;
            this.controlButton.Click += new System.EventHandler(this.ControlButton_Click);
            // 
            // mainWindow_status
            // 
            this.mainWindow_status.AutoSize = true;
            this.mainWindow_status.Location = new System.Drawing.Point(22, 442);
            this.mainWindow_status.Name = "mainWindow_status";
            this.mainWindow_status.Size = new System.Drawing.Size(38, 13);
            this.mainWindow_status.TabIndex = 4;
            this.mainWindow_status.Text = "Ready";
            // 
            // settingsButton
            // 
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.settingsButton.Location = new System.Drawing.Point(421, 368);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(71, 60);
            this.settingsButton.TabIndex = 3;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.BindToApplications_Click);
            // 
            // homeLabel
            // 
            this.homeLabel.AutoSize = true;
            this.homeLabel.Font = new System.Drawing.Font("Times New Roman", 43.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeLabel.Location = new System.Drawing.Point(129, 13);
            this.homeLabel.Name = "homeLabel";
            this.homeLabel.Size = new System.Drawing.Size(373, 67);
            this.homeLabel.TabIndex = 5;
            this.homeLabel.Text = "Gesture Studio";
            this.homeLabel.Click += new System.EventHandler(this.homeLabel_Click);
            // 
            // mainWindowTabs
            // 
            this.mainWindowTabs.Controls.Add(this.homeTab);
            this.mainWindowTabs.Controls.Add(this.settingsTab);
            this.mainWindowTabs.Controls.Add(this.tutorialTab);
            this.mainWindowTabs.Location = new System.Drawing.Point(1, 1);
            this.mainWindowTabs.Name = "mainWindowTabs";
            this.mainWindowTabs.SelectedIndex = 0;
            this.mainWindowTabs.Size = new System.Drawing.Size(621, 495);
            this.mainWindowTabs.TabIndex = 0;
            // 
            // chooseFileDialog
            // 
            this.chooseFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.chooseDataFileDialog_Ok);
            // 
            // collectDataButton
            // 
            this.collectDataButton.Location = new System.Drawing.Point(423, 400);
            this.collectDataButton.Name = "collectDataButton";
            this.collectDataButton.Size = new System.Drawing.Size(93, 52);
            this.collectDataButton.TabIndex = 5;
            this.collectDataButton.Text = "Collect Data";
            this.collectDataButton.UseVisualStyleBackColor = true;
            this.collectDataButton.Click += new System.EventHandler(this.collectDataButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 497);
            this.Controls.Add(this.mainWindowTabs);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tutorialTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.settingsTab.ResumeLayout(false);
            this.homeTab.ResumeLayout(false);
            this.homeTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_cropped)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_full)).EndInit();
            this.mainWindowTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tutorialTab;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Button setKeyBind;
        private System.Windows.Forms.TableLayoutPanel gestureBindingsTable;
        private System.Windows.Forms.TabPage homeTab;
        private System.Windows.Forms.PictureBox mainWindow_cropped;
        private System.Windows.Forms.PictureBox mainWindow_full;
        private System.Windows.Forms.Button tutorialButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button controlButton;
        private System.Windows.Forms.Label mainWindow_status;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Label homeLabel;
        private System.Windows.Forms.TabControl mainWindowTabs;
        private System.Windows.Forms.Button editAppButton;
        private System.Windows.Forms.Button addGestureButton;
        private System.Windows.Forms.Button chooseDataFileButton;
        private System.Windows.Forms.OpenFileDialog chooseFileDialog;
        private System.Windows.Forms.Button collectDataButton;

    }
}