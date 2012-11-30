namespace GestureStudio
{
    partial class CollectImageForm
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
            this.fullBox = new System.Windows.Forms.PictureBox();
            this.croppedBox = new System.Windows.Forms.PictureBox();
            this.userNameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startCollectButton = new System.Windows.Forms.Button();
            this.imageCollectionStatus = new System.Windows.Forms.Label();
            this.gestureNameBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.fullBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedBox)).BeginInit();
            this.SuspendLayout();
            // 
            // fullBox
            // 
            this.fullBox.Location = new System.Drawing.Point(12, 7);
            this.fullBox.Name = "fullBox";
            this.fullBox.Size = new System.Drawing.Size(640, 480);
            this.fullBox.TabIndex = 0;
            this.fullBox.TabStop = false;
            // 
            // croppedBox
            // 
            this.croppedBox.Location = new System.Drawing.Point(668, 12);
            this.croppedBox.Name = "croppedBox";
            this.croppedBox.Size = new System.Drawing.Size(200, 200);
            this.croppedBox.TabIndex = 1;
            this.croppedBox.TabStop = false;
            // 
            // userNameBox
            // 
            this.userNameBox.Location = new System.Drawing.Point(667, 245);
            this.userNameBox.Name = "userNameBox";
            this.userNameBox.Size = new System.Drawing.Size(100, 20);
            this.userNameBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(664, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(667, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Gesture Name";
            // 
            // startCollectButton
            // 
            this.startCollectButton.Location = new System.Drawing.Point(667, 356);
            this.startCollectButton.Name = "startCollectButton";
            this.startCollectButton.Size = new System.Drawing.Size(196, 56);
            this.startCollectButton.TabIndex = 2;
            this.startCollectButton.Text = "Start";
            this.startCollectButton.UseVisualStyleBackColor = true;
            this.startCollectButton.Click += new System.EventHandler(this.StartCollectionButton_Click);
            // 
            // imageCollectionStatus
            // 
            this.imageCollectionStatus.AutoSize = true;
            this.imageCollectionStatus.Location = new System.Drawing.Point(667, 450);
            this.imageCollectionStatus.Name = "imageCollectionStatus";
            this.imageCollectionStatus.Size = new System.Drawing.Size(38, 13);
            this.imageCollectionStatus.TabIndex = 7;
            this.imageCollectionStatus.Text = "Ready";
            // 
            // gestureNameBox
            // 
            this.gestureNameBox.Location = new System.Drawing.Point(667, 302);
            this.gestureNameBox.Name = "gestureNameBox";
            this.gestureNameBox.Size = new System.Drawing.Size(100, 20);
            this.gestureNameBox.TabIndex = 1;
            // 
            // CollectImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 499);
            this.Controls.Add(this.gestureNameBox);
            this.Controls.Add(this.imageCollectionStatus);
            this.Controls.Add(this.startCollectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.userNameBox);
            this.Controls.Add(this.croppedBox);
            this.Controls.Add(this.fullBox);
            this.Name = "CollectImageForm";
            this.Text = "CollectImageForm";
            this.Load += new System.EventHandler(this.CollectImageForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fullBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.croppedBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox fullBox;
        private System.Windows.Forms.PictureBox croppedBox;
        private System.Windows.Forms.TextBox userNameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button startCollectButton;
        private System.Windows.Forms.Label imageCollectionStatus;
        private System.Windows.Forms.TextBox gestureNameBox;
    }
}