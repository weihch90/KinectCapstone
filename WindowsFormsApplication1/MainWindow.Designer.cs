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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Home = new System.Windows.Forms.TabPage();
            this.AddNewGestures = new System.Windows.Forms.TabPage();
            this.BindToApplications = new System.Windows.Forms.TabPage();
            this.homeLabel = new System.Windows.Forms.Label();
            this.addGestureButton = new System.Windows.Forms.Button();
            this.bindToApplicationButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.currentGesture = new System.Windows.Forms.Label();
            this.controlButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.Home.SuspendLayout();
            this.BindToApplications.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Home);
            this.tabControl1.Controls.Add(this.AddNewGestures);
            this.tabControl1.Controls.Add(this.BindToApplications);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(595, 375);
            this.tabControl1.TabIndex = 0;
            // 
            // Home
            // 
            this.Home.Controls.Add(this.controlButton);
            this.Home.Controls.Add(this.currentGesture);
            this.Home.Controls.Add(this.bindToApplicationButton);
            this.Home.Controls.Add(this.addGestureButton);
            this.Home.Controls.Add(this.homeLabel);
            this.Home.Location = new System.Drawing.Point(4, 22);
            this.Home.Name = "Home";
            this.Home.Padding = new System.Windows.Forms.Padding(3);
            this.Home.Size = new System.Drawing.Size(587, 349);
            this.Home.TabIndex = 0;
            this.Home.Text = "Home";
            this.Home.UseVisualStyleBackColor = true;
            // 
            // AddNewGestures
            // 
            this.AddNewGestures.Location = new System.Drawing.Point(4, 22);
            this.AddNewGestures.Name = "AddNewGestures";
            this.AddNewGestures.Padding = new System.Windows.Forms.Padding(3);
            this.AddNewGestures.Size = new System.Drawing.Size(587, 349);
            this.AddNewGestures.TabIndex = 1;
            this.AddNewGestures.Text = "Add New Gestures";
            this.AddNewGestures.UseVisualStyleBackColor = true;
            this.AddNewGestures.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // BindToApplications
            // 
            this.BindToApplications.Controls.Add(this.button1);
            this.BindToApplications.Controls.Add(this.tableLayoutPanel1);
            this.BindToApplications.Location = new System.Drawing.Point(4, 22);
            this.BindToApplications.Name = "BindToApplications";
            this.BindToApplications.Padding = new System.Windows.Forms.Padding(3);
            this.BindToApplications.Size = new System.Drawing.Size(587, 349);
            this.BindToApplications.TabIndex = 2;
            this.BindToApplications.Text = "Bind To Applications";
            this.BindToApplications.UseVisualStyleBackColor = true;
            this.BindToApplications.Click += new System.EventHandler(this.BindToApplications_Click);
            // 
            // homeLabel
            // 
            this.homeLabel.AutoSize = true;
            this.homeLabel.Font = new System.Drawing.Font("Times New Roman", 35.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeLabel.Location = new System.Drawing.Point(105, 29);
            this.homeLabel.Name = "homeLabel";
            this.homeLabel.Size = new System.Drawing.Size(374, 53);
            this.homeLabel.TabIndex = 0;
            this.homeLabel.Text = "Gesture Controller";
            this.homeLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // addGestureButton
            // 
            this.addGestureButton.Location = new System.Drawing.Point(114, 246);
            this.addGestureButton.Name = "addGestureButton";
            this.addGestureButton.Size = new System.Drawing.Size(138, 61);
            this.addGestureButton.TabIndex = 2;
            this.addGestureButton.Text = "Add New Gestures";
            this.addGestureButton.UseVisualStyleBackColor = true;
            // 
            // bindToApplicationButton
            // 
            this.bindToApplicationButton.Location = new System.Drawing.Point(341, 246);
            this.bindToApplicationButton.Name = "bindToApplicationButton";
            this.bindToApplicationButton.Size = new System.Drawing.Size(138, 61);
            this.bindToApplicationButton.TabIndex = 3;
            this.bindToApplicationButton.Text = "Bind To Applications";
            this.bindToApplicationButton.UseVisualStyleBackColor = true;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(460, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "New Bind";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // currentGesture
            // 
            this.currentGesture.AutoSize = true;
            this.currentGesture.Location = new System.Drawing.Point(7, 323);
            this.currentGesture.Name = "currentGesture";
            this.currentGesture.Size = new System.Drawing.Size(81, 13);
            this.currentGesture.TabIndex = 4;
            this.currentGesture.Text = "Your Gesture: []";
            this.currentGesture.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // controlButton
            // 
            this.controlButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controlButton.Location = new System.Drawing.Point(227, 158);
            this.controlButton.Name = "controlButton";
            this.controlButton.Size = new System.Drawing.Size(138, 61);
            this.controlButton.TabIndex = 1;
            this.controlButton.Text = "Start";
            this.controlButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 379);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.tabControl1.ResumeLayout(false);
            this.Home.ResumeLayout(false);
            this.Home.PerformLayout();
            this.BindToApplications.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Home;
        private System.Windows.Forms.TabPage AddNewGestures;
        private System.Windows.Forms.TabPage BindToApplications;
        private System.Windows.Forms.Label homeLabel;
        private System.Windows.Forms.Button bindToApplicationButton;
        private System.Windows.Forms.Button addGestureButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label currentGesture;
        private System.Windows.Forms.Button controlButton;
    }
}