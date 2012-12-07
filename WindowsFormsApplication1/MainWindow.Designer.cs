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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tutorialTab = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.gestureDataGridView = new System.Windows.Forms.DataGridView();
            this.chooseDataFileButton = new System.Windows.Forms.Button();
            this.addGestureButton = new System.Windows.Forms.Button();
            this.setKeyBind = new System.Windows.Forms.Button();
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

            this.gestureDataGridView = new System.Windows.Forms.DataGridView();
            this.chooseModelButton = new System.Windows.Forms.Button();
            this.tutorialTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.settingsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gestureDataGridView)).BeginInit();
            this.homeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_cropped)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainWindow_full)).BeginInit();
            this.mainWindowTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tutorialTab
            // 
            this.tutorialTab.Controls.Add(this.pictureBox3);
            this.tutorialTab.Location = new System.Drawing.Point(4, 25);
            this.tutorialTab.Name = "tutorialTab";
            this.tutorialTab.Padding = new System.Windows.Forms.Padding(3);
            this.tutorialTab.Size = new System.Drawing.Size(613, 466);
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
            this.settingsTab.Controls.Add(this.gestureDataGridView);
            this.settingsTab.Controls.Add(this.chooseDataFileButton);
            this.settingsTab.Controls.Add(this.addGestureButton);
            this.settingsTab.Controls.Add(this.setKeyBind);
            this.settingsTab.Location = new System.Drawing.Point(4, 25);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(613, 466);
            this.settingsTab.TabIndex = 2;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            this.settingsTab.Click += new System.EventHandler(this.BindToApplications_Click);
            // 
            // gestureDataGridView
            // 
            this.gestureDataGridView.AllowUserToAddRows = false;
            this.gestureDataGridView.AllowUserToDeleteRows = false;
            this.gestureDataGridView.AllowUserToResizeColumns = false;
            this.gestureDataGridView.AllowUserToResizeRows = false;
            this.gestureDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gestureDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gestureDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gestureDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gestureDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gestureDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gestureDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.gestureDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gestureDataGridView.Enabled = false;
            this.gestureDataGridView.Location = new System.Drawing.Point(27, 24);
            this.gestureDataGridView.Name = "gestureDataGridView";
            this.gestureDataGridView.RowHeadersVisible = false;
            this.gestureDataGridView.Size = new System.Drawing.Size(418, 359);
            this.gestureDataGridView.TabIndex = 5;
            this.gestureDataGridView.SelectionChanged += new System.EventHandler(this.gestureDataGridView_SelectionChanged);
            // 
            // chooseDataFileButton
            // 
            this.chooseDataFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseDataFileButton.Location = new System.Drawing.Point(293, 401);
            this.chooseDataFileButton.Name = "chooseDataFileButton";
            this.chooseDataFileButton.Size = new System.Drawing.Size(152, 51);
            this.chooseDataFileButton.TabIndex = 4;
            this.chooseDataFileButton.Text = "Choose Data File";
            this.chooseDataFileButton.UseVisualStyleBackColor = true;
            this.chooseDataFileButton.Click += new System.EventHandler(this.chooseDataFile_Click);
            // 
            // addGestureButton
            // 
            this.addGestureButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addGestureButton.Location = new System.Drawing.Point(27, 401);
            this.addGestureButton.Name = "addGestureButton";
            this.addGestureButton.Size = new System.Drawing.Size(127, 51);
            this.addGestureButton.TabIndex = 1;
            this.addGestureButton.Text = "New Gesture";
            this.addGestureButton.UseVisualStyleBackColor = true;
            this.addGestureButton.Click += new System.EventHandler(this.addGestureButton_Click);
            // 
            // setKeyBind
            // 
            this.setKeyBind.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setKeyBind.Location = new System.Drawing.Point(160, 401);
            this.setKeyBind.Name = "setKeyBind";
            this.setKeyBind.Size = new System.Drawing.Size(127, 51);
            this.setKeyBind.TabIndex = 3;
            this.setKeyBind.Text = "Edit Binding";
            this.setKeyBind.UseVisualStyleBackColor = true;
            this.setKeyBind.Click += new System.EventHandler(this.editBindingButton_Click);
            // 
            // homeTab
            // 
            this.homeTab.Controls.Add(this.chooseModelButton);
            this.homeTab.Controls.Add(this.mainWindow_cropped);
            this.homeTab.Controls.Add(this.mainWindow_full);
            this.homeTab.Controls.Add(this.tutorialButton);
            this.homeTab.Controls.Add(this.label1);
            this.homeTab.Controls.Add(this.controlButton);
            this.homeTab.Controls.Add(this.mainWindow_status);
            this.homeTab.Controls.Add(this.settingsButton);
            this.homeTab.Controls.Add(this.homeLabel);
            this.homeTab.Location = new System.Drawing.Point(4, 25);
            this.homeTab.Name = "homeTab";
            this.homeTab.Padding = new System.Windows.Forms.Padding(3);
            this.homeTab.Size = new System.Drawing.Size(613, 466);
            this.homeTab.TabIndex = 0;
            this.homeTab.Text = "Home";
            this.homeTab.UseVisualStyleBackColor = true;
            // 
            // mainWindow_cropped
            // 
            this.mainWindow_cropped.BackColor = System.Drawing.Color.Transparent;
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
            this.tutorialButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(173, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 17);
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
            this.mainWindow_status.Size = new System.Drawing.Size(49, 17);
            this.mainWindow_status.TabIndex = 4;
            this.mainWindow_status.Text = "Ready";
            // 
            // settingsButton
            // 
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.mainWindowTabs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainWindowTabs.Location = new System.Drawing.Point(1, 1);
            this.mainWindowTabs.Name = "mainWindowTabs";
            this.mainWindowTabs.SelectedIndex = 0;
            this.mainWindowTabs.Size = new System.Drawing.Size(621, 495);
            this.mainWindowTabs.TabIndex = 0;
            this.mainWindowTabs.SelectedIndexChanged += new System.EventHandler(this.mainWindowTabs_SelectedIndexChanged);
            // 
            // chooseFileDialog
            // 
            this.chooseFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.chooseDataFileDialog_Ok);
            // 
<<<<<<< HEAD
=======
            // gestureDataGridView
            // 
            this.gestureDataGridView.AllowUserToAddRows = false;
            this.gestureDataGridView.AllowUserToDeleteRows = false;
            this.gestureDataGridView.AllowUserToResizeColumns = false;
            this.gestureDataGridView.AllowUserToResizeRows = false;
            this.gestureDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gestureDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gestureDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gestureDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gestureDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.gestureDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gestureDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.gestureDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gestureDataGridView.Enabled = false;
            this.gestureDataGridView.Location = new System.Drawing.Point(27, 24);
            this.gestureDataGridView.Name = "gestureDataGridView";
            this.gestureDataGridView.RowHeadersVisible = false;
            this.gestureDataGridView.Size = new System.Drawing.Size(418, 359);
            this.gestureDataGridView.TabIndex = 5;
            this.gestureDataGridView.SelectionChanged += new System.EventHandler(this.gestureDataGridView_SelectionChanged);
            // 
            // chooseModelButton
            // 
            this.chooseModelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chooseModelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseModelButton.Location = new System.Drawing.Point(210, 368);
            this.chooseModelButton.Name = "chooseModelButton";
            this.chooseModelButton.Size = new System.Drawing.Size(155, 60);
            this.chooseModelButton.TabIndex = 10;
            this.chooseModelButton.Text = "Choose Model";
            this.chooseModelButton.UseVisualStyleBackColor = true;
            this.chooseModelButton.Click += new System.EventHandler(this.chooseModelButton_Click);
            // 
>>>>>>> 3699b6908eca9c94fc93dee65d4e3278e62e437c
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
            ((System.ComponentModel.ISupportInitialize)(this.gestureDataGridView)).EndInit();
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
        private System.Windows.Forms.Button addGestureButton;
        private System.Windows.Forms.Button chooseDataFileButton;
        private System.Windows.Forms.OpenFileDialog chooseFileDialog;
        private System.Windows.Forms.DataGridView gestureDataGridView;
        private System.Windows.Forms.Button chooseModelButton;

    }
}