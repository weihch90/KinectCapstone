namespace WindowsFormsApplication2
{
    partial class KeyBindForm
    {
        // returns a string form of the given keybinding or 
        // null if no text was inputted to the text box;
        /* usage: 
          using (KeyBindForm kbForm = new KeyBindForm())
            {
                if (DialogResult.OK == kbForm.ShowDialog())
                {
                    label1.Text = kbForm.getKeyBind();
                }
                else
                {
                    //nothing was found
                }

            }
        */
        public string getKeyBind()
        {
            if (keyInput.Text.Equals(""))
                return null;

            return (ctrlCheck.Checked ? "ctrl-" : "") +
                        (shiftCheck.Checked ? "shift-" : "") +
                        (altCheck.Checked ? "alt-" : "") +
                        keyInput.Text;
        }
        
        
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlCheck
            // 
            this.ctrlCheck.AutoSize = true;
            this.ctrlCheck.Location = new System.Drawing.Point(19, 65);
            this.ctrlCheck.Name = "ctrlCheck";
            this.ctrlCheck.Size = new System.Drawing.Size(51, 21);
            this.ctrlCheck.TabIndex = 0;
            this.ctrlCheck.Text = "Ctrl";
            this.ctrlCheck.UseVisualStyleBackColor = true;
            this.ctrlCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // shiftCheck
            // 
            this.shiftCheck.AutoSize = true;
            this.shiftCheck.Location = new System.Drawing.Point(94, 65);
            this.shiftCheck.Name = "shiftCheck";
            this.shiftCheck.Size = new System.Drawing.Size(58, 21);
            this.shiftCheck.TabIndex = 1;
            this.shiftCheck.Text = "Shift";
            this.shiftCheck.UseVisualStyleBackColor = true;
            // 
            // altCheck
            // 
            this.altCheck.AutoSize = true;
            this.altCheck.Location = new System.Drawing.Point(174, 65);
            this.altCheck.Name = "altCheck";
            this.altCheck.Size = new System.Drawing.Size(46, 21);
            this.altCheck.TabIndex = 2;
            this.altCheck.Text = "Alt";
            this.altCheck.UseVisualStyleBackColor = true;
            this.altCheck.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // keyInput
            // 
            this.keyInput.Location = new System.Drawing.Point(245, 65);
            this.keyInput.MaxLength = 1;
            this.keyInput.Name = "keyInput";
            this.keyInput.Size = new System.Drawing.Size(100, 22);
            this.keyInput.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(239, 118);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;

            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(329, 118);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Set your key binding";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 153);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.keyInput);
            this.Controls.Add(this.altCheck);
            this.Controls.Add(this.shiftCheck);
            this.Controls.Add(this.ctrlCheck);
            this.Name = "Form2";
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
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
    }
}