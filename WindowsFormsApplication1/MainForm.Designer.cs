namespace WindowsFormsApplication1
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.filename = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fullFrameStream = new System.Windows.Forms.PictureBox();
            this.croppedFrameStream = new System.Windows.Forms.PictureBox();
            this.framesPerSecond = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fullFrameStream)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedFrameStream)).BeginInit();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.Location = new System.Drawing.Point(12, 498);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(800, 106);
            this.message.TabIndex = 0;
            this.message.Text = "";
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(662, 194);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 23);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "TakeImages";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // filename
            // 
            this.filename.Location = new System.Drawing.Point(662, 168);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(147, 20);
            this.filename.TabIndex = 6;
            this.filename.Text = "Gesture";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(777, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
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
            this.croppedFrameStream.Size = new System.Drawing.Size(150, 150);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 620);
            this.Controls.Add(this.framesPerSecond);
            this.Controls.Add(this.croppedFrameStream);
            this.Controls.Add(this.fullFrameStream);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filename);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.message);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fullFrameStream)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedFrameStream)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox message;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox filename;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox fullFrameStream;
        private System.Windows.Forms.PictureBox croppedFrameStream;
        private System.Windows.Forms.Label framesPerSecond;
    }
}

