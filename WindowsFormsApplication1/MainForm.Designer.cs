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
            this.startButton = new System.Windows.Forms.Button();
            this.mainStatusDisplay = new System.Windows.Forms.StatusStrip();
            this.modelStatusDisplay = new System.Windows.Forms.ToolStripStatusLabel();
            this.framesPerSecond = new System.Windows.Forms.ToolStripStatusLabel();
            this.chooseFeatureFileButton = new System.Windows.Forms.Button();
            this.featureFilePath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.homeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fullFrameStream)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedFrameStream)).BeginInit();
            this.mainStatusDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.message.Enabled = false;
            this.message.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.Location = new System.Drawing.Point(441, 348);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(169, 102);
            this.message.TabIndex = 0;
            this.message.Text = "";
            // 
            // fullFrameStream
            // 
            this.fullFrameStream.Location = new System.Drawing.Point(15, 164);
            this.fullFrameStream.Name = "fullFrameStream";
            this.fullFrameStream.Size = new System.Drawing.Size(400, 285);
            this.fullFrameStream.TabIndex = 8;
            this.fullFrameStream.TabStop = false;
            this.fullFrameStream.Click += new System.EventHandler(this.fullFrameStream_Click);
            // 
            // croppedFrameStream
            // 
            this.croppedFrameStream.Location = new System.Drawing.Point(441, 164);
            this.croppedFrameStream.Name = "croppedFrameStream";
            this.croppedFrameStream.Size = new System.Drawing.Size(120, 120);
            this.croppedFrameStream.TabIndex = 9;
            this.croppedFrameStream.TabStop = false;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(441, 298);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(120, 38);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // mainStatusDisplay
            // 
            this.mainStatusDisplay.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelStatusDisplay,
            this.framesPerSecond});
            this.mainStatusDisplay.Location = new System.Drawing.Point(0, 475);
            this.mainStatusDisplay.Name = "mainStatusDisplay";
            this.mainStatusDisplay.Size = new System.Drawing.Size(622, 22);
            this.mainStatusDisplay.TabIndex = 12;
            this.mainStatusDisplay.Text = "Ready...";
            // 
            // modelStatusDisplay
            // 
            this.modelStatusDisplay.Name = "modelStatusDisplay";
            this.modelStatusDisplay.Size = new System.Drawing.Size(85, 17);
            this.modelStatusDisplay.Text = "Model Status...";
            // 
            // framesPerSecond
            // 
            this.framesPerSecond.Name = "framesPerSecond";
            this.framesPerSecond.Size = new System.Drawing.Size(26, 17);
            this.framesPerSecond.Text = "FPS";
            // 
            // chooseFeatureFileButton
            // 
            this.chooseFeatureFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseFeatureFileButton.Location = new System.Drawing.Point(262, 121);
            this.chooseFeatureFileButton.Name = "chooseFeatureFileButton";
            this.chooseFeatureFileButton.Size = new System.Drawing.Size(156, 23);
            this.chooseFeatureFileButton.TabIndex = 13;
            this.chooseFeatureFileButton.Text = "Choose Feature File";
            this.chooseFeatureFileButton.UseVisualStyleBackColor = true;
            this.chooseFeatureFileButton.Click += new System.EventHandler(this.chooseFeatureFileButton_Click);
            // 
            // featureFilePath
            // 
            this.featureFilePath.AutoSize = true;
            this.featureFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.featureFilePath.Location = new System.Drawing.Point(12, 121);
            this.featureFilePath.Name = "featureFilePath";
            this.featureFilePath.Size = new System.Drawing.Size(179, 17);
            this.featureFilePath.TabIndex = 14;
            this.featureFilePath.Text = "Default: feature_empty.mat";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(179, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Control your applications with hand gestures";
            // 
            // homeLabel
            // 
            this.homeLabel.AutoSize = true;
            this.homeLabel.Font = new System.Drawing.Font("Times New Roman", 43.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeLabel.Location = new System.Drawing.Point(135, 19);
            this.homeLabel.Name = "homeLabel";
            this.homeLabel.Size = new System.Drawing.Size(373, 67);
            this.homeLabel.TabIndex = 15;
            this.homeLabel.Text = "Gesture Studio";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(622, 497);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.homeLabel);
            this.Controls.Add(this.featureFilePath);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.chooseFeatureFileButton);
            this.Controls.Add(this.mainStatusDisplay);
            this.Controls.Add(this.croppedFrameStream);
            this.Controls.Add(this.fullFrameStream);
            this.Controls.Add(this.message);
            this.Name = "MainForm";
            this.Text = "Training Mode";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fullFrameStream)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedFrameStream)).EndInit();
            this.mainStatusDisplay.ResumeLayout(false);
            this.mainStatusDisplay.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void fullFrameStream_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.RichTextBox message;
        private System.Windows.Forms.PictureBox fullFrameStream;
        private System.Windows.Forms.PictureBox croppedFrameStream;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.StatusStrip mainStatusDisplay;
        private System.Windows.Forms.ToolStripStatusLabel modelStatusDisplay;
        private System.Windows.Forms.Button chooseFeatureFileButton;
        private System.Windows.Forms.Label featureFilePath;
        private System.Windows.Forms.ToolStripStatusLabel framesPerSecond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label homeLabel;
    }
}

