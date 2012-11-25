namespace GestureStudio
{
    partial class KeyBindForm
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
            this.ctrlCheck = new System.Windows.Forms.CheckBox();
            this.shiftCheck = new System.Windows.Forms.CheckBox();
            this.altCheck = new System.Windows.Forms.CheckBox();
            this.keyInput = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.selectGesture = new System.Windows.Forms.ComboBox();
            this.selectApp = new System.Windows.Forms.ComboBox();
            this.selectGestureLabel = new System.Windows.Forms.Label();
            this.selectApplicationLabel = new System.Windows.Forms.Label();
            this.setKeyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlCheck
            // 
            this.ctrlCheck.AutoSize = true;
            this.ctrlCheck.Location = new System.Drawing.Point(15, 182);
            this.ctrlCheck.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlCheck.Name = "ctrlCheck";
            this.ctrlCheck.Size = new System.Drawing.Size(41, 17);
            this.ctrlCheck.TabIndex = 0;
            this.ctrlCheck.Text = "Ctrl";
            this.ctrlCheck.UseVisualStyleBackColor = true;
            this.ctrlCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // shiftCheck
            // 
            this.shiftCheck.AutoSize = true;
            this.shiftCheck.Location = new System.Drawing.Point(68, 182);
            this.shiftCheck.Margin = new System.Windows.Forms.Padding(2);
            this.shiftCheck.Name = "shiftCheck";
            this.shiftCheck.Size = new System.Drawing.Size(47, 17);
            this.shiftCheck.TabIndex = 1;
            this.shiftCheck.Text = "Shift";
            this.shiftCheck.UseVisualStyleBackColor = true;
            // 
            // altCheck
            // 
            this.altCheck.AutoSize = true;
            this.altCheck.Location = new System.Drawing.Point(129, 182);
            this.altCheck.Margin = new System.Windows.Forms.Padding(2);
            this.altCheck.Name = "altCheck";
            this.altCheck.Size = new System.Drawing.Size(38, 17);
            this.altCheck.TabIndex = 2;
            this.altCheck.Text = "Alt";
            this.altCheck.UseVisualStyleBackColor = true;
            this.altCheck.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // keyInput
            // 
            this.keyInput.Location = new System.Drawing.Point(184, 182);
            this.keyInput.Margin = new System.Windows.Forms.Padding(2);
            this.keyInput.MaxLength = 1;
            this.keyInput.Name = "keyInput";
            this.keyInput.Size = new System.Drawing.Size(76, 20);
            this.keyInput.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(184, 227);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(56, 19);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(245, 227);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(56, 19);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Set your key binding";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // selectGesture
            // 
            this.selectGesture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectGesture.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectGesture.FormattingEnabled = true;
            this.selectGesture.Location = new System.Drawing.Point(15, 70);
            this.selectGesture.Name = "selectGesture";
            this.selectGesture.Size = new System.Drawing.Size(121, 21);
            this.selectGesture.TabIndex = 7;
            // 
            // selectApp
            // 
            this.selectApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectApp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectApp.FormattingEnabled = true;
            this.selectApp.Location = new System.Drawing.Point(15, 124);
            this.selectApp.Name = "selectApp";
            this.selectApp.Size = new System.Drawing.Size(121, 21);
            this.selectApp.TabIndex = 8;
            // 
            // selectGestureLabel
            // 
            this.selectGestureLabel.AutoSize = true;
            this.selectGestureLabel.Location = new System.Drawing.Point(13, 51);
            this.selectGestureLabel.Name = "selectGestureLabel";
            this.selectGestureLabel.Size = new System.Drawing.Size(77, 13);
            this.selectGestureLabel.TabIndex = 9;
            this.selectGestureLabel.Text = "Select Gesture";
            // 
            // selectApplicationLabel
            // 
            this.selectApplicationLabel.AutoSize = true;
            this.selectApplicationLabel.Location = new System.Drawing.Point(12, 108);
            this.selectApplicationLabel.Name = "selectApplicationLabel";
            this.selectApplicationLabel.Size = new System.Drawing.Size(92, 13);
            this.selectApplicationLabel.TabIndex = 10;
            this.selectApplicationLabel.Text = "Select Application";
            // 
            // setKeyLabel
            // 
            this.setKeyLabel.AutoSize = true;
            this.setKeyLabel.Location = new System.Drawing.Point(13, 164);
            this.setKeyLabel.Name = "setKeyLabel";
            this.setKeyLabel.Size = new System.Drawing.Size(94, 13);
            this.setKeyLabel.TabIndex = 11;
            this.setKeyLabel.Text = "Set Key Command";
            // 
            // KeyBindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 257);
            this.Controls.Add(this.setKeyLabel);
            this.Controls.Add(this.selectApplicationLabel);
            this.Controls.Add(this.selectGestureLabel);
            this.Controls.Add(this.selectApp);
            this.Controls.Add(this.selectGesture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.keyInput);
            this.Controls.Add(this.altCheck);
            this.Controls.Add(this.shiftCheck);
            this.Controls.Add(this.ctrlCheck);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KeyBindForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Binding";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ctrlCheck;
        private System.Windows.Forms.CheckBox shiftCheck;
        private System.Windows.Forms.CheckBox altCheck;
        private System.Windows.Forms.TextBox keyInput;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox selectGesture;
        private System.Windows.Forms.ComboBox selectApp;
        private System.Windows.Forms.Label selectGestureLabel;
        private System.Windows.Forms.Label selectApplicationLabel;
        private System.Windows.Forms.Label setKeyLabel;
    }
}