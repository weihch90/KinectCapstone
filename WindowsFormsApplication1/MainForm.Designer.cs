namespace GestureStudio
{
    partial class MainForm
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
            this.message = new System.Windows.Forms.RichTextBox();
            this.fullFrameStream = new System.Windows.Forms.PictureBox();
            this.croppedFrameStream = new System.Windows.Forms.PictureBox();
            this.framesPerSecond = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.trainingModeButton = new System.Windows.Forms.RadioButton();
            this.classifyModeButton = new System.Windows.Forms.RadioButton();
            this.mainStatusDisplay = new System.Windows.Forms.StatusStrip();
            this.modelStatusDisplay = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fullFrameStream)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedFrameStream)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.mainStatusDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.Location = new System.Drawing.Point(12, 498);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(819, 38);
            this.message.TabIndex = 0;
            this.message.Text = "";
            // 
            // fullFrameStream
            // 
            this.fullFrameStream.Location = new System.Drawing.Point(12, 12);
            this.fullFrameStream.Name = "fullFrameStream";
            this.fullFrameStream.Size = new System.Drawing.Size(640, 480);
            this.fullFrameStream.TabIndex = 8;
            this.fullFrameStream.TabStop = false;
            // 
            // croppedFrameStream
            // 
            this.croppedFrameStream.Location = new System.Drawing.Point(662, 12);
            this.croppedFrameStream.Name = "croppedFrameStream";
            this.croppedFrameStream.Size = new System.Drawing.Size(169, 200);
            this.croppedFrameStream.TabIndex = 9;
            this.croppedFrameStream.TabStop = false;
            // 
            // framesPerSecond
            // 
            this.framesPerSecond.AutoSize = true;
            this.framesPerSecond.Location = new System.Drawing.Point(659, 449);
            this.framesPerSecond.Name = "framesPerSecond";
            this.framesPerSecond.Size = new System.Drawing.Size(35, 13);
            this.framesPerSecond.TabIndex = 10;
            this.framesPerSecond.Text = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stopButton);
            this.groupBox1.Controls.Add(this.startButton);
            this.groupBox1.Controls.Add(this.trainingModeButton);
            this.groupBox1.Controls.Add(this.classifyModeButton);
            this.groupBox1.Location = new System.Drawing.Point(662, 218);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 92);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program Mode";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(87, 63);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(6, 63);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // trainingModeButton
            // 
            this.trainingModeButton.AutoSize = true;
            this.trainingModeButton.Location = new System.Drawing.Point(6, 42);
            this.trainingModeButton.Name = "trainingModeButton";
            this.trainingModeButton.Size = new System.Drawing.Size(93, 17);
            this.trainingModeButton.TabIndex = 1;
            this.trainingModeButton.TabStop = true;
            this.trainingModeButton.Text = "Training Mode";
            this.trainingModeButton.UseVisualStyleBackColor = true;
            // 
            // classifyModeButton
            // 
            this.classifyModeButton.AutoSize = true;
            this.classifyModeButton.Location = new System.Drawing.Point(6, 19);
            this.classifyModeButton.Name = "classifyModeButton";
            this.classifyModeButton.Size = new System.Drawing.Size(90, 17);
            this.classifyModeButton.TabIndex = 0;
            this.classifyModeButton.TabStop = true;
            this.classifyModeButton.Text = "Classify Mode";
            this.classifyModeButton.UseVisualStyleBackColor = true;
            // 
            // mainStatusDisplay
            // 
            this.mainStatusDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelStatusDisplay});
            this.mainStatusDisplay.Location = new System.Drawing.Point(0, 547);
            this.mainStatusDisplay.Name = "mainStatusDisplay";
            this.mainStatusDisplay.Size = new System.Drawing.Size(841, 22);
            this.mainStatusDisplay.TabIndex = 12;
            this.mainStatusDisplay.Text = "Ready...";
            // 
            // modelStatusDisplay
            // 
            this.modelStatusDisplay.Name = "modelStatusDisplay";
            this.modelStatusDisplay.Size = new System.Drawing.Size(85, 17);
            this.modelStatusDisplay.Text = "Model Status...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 569);
            this.Controls.Add(this.mainStatusDisplay);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.framesPerSecond);
            this.Controls.Add(this.croppedFrameStream);
            this.Controls.Add(this.fullFrameStream);
            this.Controls.Add(this.message);
            this.Name = "MainForm";
            this.Text = "Display";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fullFrameStream)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedFrameStream)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.mainStatusDisplay.ResumeLayout(false);
            this.mainStatusDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox message;
        private System.Windows.Forms.PictureBox fullFrameStream;
        private System.Windows.Forms.PictureBox croppedFrameStream;
        private System.Windows.Forms.Label framesPerSecond;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RadioButton trainingModeButton;
        private System.Windows.Forms.RadioButton classifyModeButton;
        private System.Windows.Forms.StatusStrip mainStatusDisplay;
        private System.Windows.Forms.ToolStripStatusLabel modelStatusDisplay;
    }
}

