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
            this.fkeyCombo = new System.Windows.Forms.ComboBox();
            this.fkeyRadio = new System.Windows.Forms.RadioButton();
            this.newkeyRadio = new System.Windows.Forms.RadioButton();
            this.customKeyGroup = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.selectCommand = new System.Windows.Forms.ComboBox();
            this.customKeyGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlCheck
            // 
            this.ctrlCheck.AutoSize = true;
            this.ctrlCheck.Location = new System.Drawing.Point(257, 71);
            this.ctrlCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlCheck.Name = "ctrlCheck";
            this.ctrlCheck.Size = new System.Drawing.Size(48, 21);
            this.ctrlCheck.TabIndex = 0;
            this.ctrlCheck.Text = "Ctrl";
            this.ctrlCheck.UseVisualStyleBackColor = true;
            this.ctrlCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // shiftCheck
            // 
            this.shiftCheck.AutoSize = true;
            this.shiftCheck.Location = new System.Drawing.Point(320, 71);
            this.shiftCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.shiftCheck.Name = "shiftCheck";
            this.shiftCheck.Size = new System.Drawing.Size(55, 21);
            this.shiftCheck.TabIndex = 1;
            this.shiftCheck.Text = "Shift";
            this.shiftCheck.UseVisualStyleBackColor = true;
            // 
            // altCheck
            // 
            this.altCheck.AutoSize = true;
            this.altCheck.Location = new System.Drawing.Point(388, 71);
            this.altCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.altCheck.Name = "altCheck";
            this.altCheck.Size = new System.Drawing.Size(43, 21);
            this.altCheck.TabIndex = 2;
            this.altCheck.Text = "Alt";
            this.altCheck.UseVisualStyleBackColor = true;
            this.altCheck.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // keyInput
            // 
            this.keyInput.Location = new System.Drawing.Point(132, 68);
            this.keyInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.keyInput.MaxLength = 1;
            this.keyInput.Name = "keyInput";
            this.keyInput.Size = new System.Drawing.Size(100, 23);
            this.keyInput.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(268, 388);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(348, 388);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
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
            // selectGesture
            // 
            this.selectGesture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectGesture.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectGesture.FormattingEnabled = true;
            this.selectGesture.Location = new System.Drawing.Point(20, 86);
            this.selectGesture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectGesture.Name = "selectGesture";
            this.selectGesture.Size = new System.Drawing.Size(160, 24);
            this.selectGesture.TabIndex = 7;
            // 
            // selectApp
            // 
            this.selectApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectApp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.selectApp.FormattingEnabled = true;
            this.selectApp.Location = new System.Drawing.Point(20, 153);
            this.selectApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectApp.Name = "selectApp";
            this.selectApp.Size = new System.Drawing.Size(160, 24);
            this.selectApp.TabIndex = 8;
            this.selectApp.SelectedIndexChanged += new System.EventHandler(this.SelectAppIndex_Changed);
            // 
            // selectGestureLabel
            // 
            this.selectGestureLabel.AutoSize = true;
            this.selectGestureLabel.Location = new System.Drawing.Point(17, 63);
            this.selectGestureLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectGestureLabel.Name = "selectGestureLabel";
            this.selectGestureLabel.Size = new System.Drawing.Size(102, 17);
            this.selectGestureLabel.TabIndex = 9;
            this.selectGestureLabel.Text = "Select Gesture";
            // 
            // selectApplicationLabel
            // 
            this.selectApplicationLabel.AutoSize = true;
            this.selectApplicationLabel.Location = new System.Drawing.Point(16, 133);
            this.selectApplicationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.selectApplicationLabel.Name = "selectApplicationLabel";
            this.selectApplicationLabel.Size = new System.Drawing.Size(120, 17);
            this.selectApplicationLabel.TabIndex = 10;
            this.selectApplicationLabel.Text = "Select Application";
            // 
            // fkeyCombo
            // 
            this.fkeyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fkeyCombo.FormattingEnabled = true;
            this.fkeyCombo.Location = new System.Drawing.Point(132, 23);
            this.fkeyCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fkeyCombo.Name = "fkeyCombo";
            this.fkeyCombo.Size = new System.Drawing.Size(160, 24);
            this.fkeyCombo.TabIndex = 12;
            this.fkeyCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // fkeyRadio
            // 
            this.fkeyRadio.AutoSize = true;
            this.fkeyRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fkeyRadio.Location = new System.Drawing.Point(21, 23);
            this.fkeyRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fkeyRadio.Name = "fkeyRadio";
            this.fkeyRadio.Size = new System.Drawing.Size(73, 21);
            this.fkeyRadio.TabIndex = 13;
            this.fkeyRadio.TabStop = true;
            this.fkeyRadio.Text = "generic";
            this.fkeyRadio.UseVisualStyleBackColor = true;
            // 
            // newkeyRadio
            // 
            this.newkeyRadio.AutoSize = true;
            this.newkeyRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.newkeyRadio.Location = new System.Drawing.Point(23, 68);
            this.newkeyRadio.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.newkeyRadio.Name = "newkeyRadio";
            this.newkeyRadio.Size = new System.Drawing.Size(71, 21);
            this.newkeyRadio.TabIndex = 14;
            this.newkeyRadio.TabStop = true;
            this.newkeyRadio.Text = "custom";
            this.newkeyRadio.UseVisualStyleBackColor = true;
            // 
            // customKeyGroup
            // 
            this.customKeyGroup.BackColor = System.Drawing.SystemColors.Control;
            this.customKeyGroup.Controls.Add(this.fkeyRadio);
            this.customKeyGroup.Controls.Add(this.fkeyCombo);
            this.customKeyGroup.Controls.Add(this.newkeyRadio);
            this.customKeyGroup.Controls.Add(this.keyInput);
            this.customKeyGroup.Controls.Add(this.altCheck);
            this.customKeyGroup.Controls.Add(this.ctrlCheck);
            this.customKeyGroup.Controls.Add(this.shiftCheck);
            this.customKeyGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.customKeyGroup.Enabled = false;
            this.customKeyGroup.Location = new System.Drawing.Point(16, 266);
            this.customKeyGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.customKeyGroup.Name = "customKeyGroup";
            this.customKeyGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.customKeyGroup.Size = new System.Drawing.Size(452, 116);
            this.customKeyGroup.TabIndex = 15;
            this.customKeyGroup.TabStop = false;
            this.customKeyGroup.Text = "Custom Set Key Command";
            this.customKeyGroup.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 203);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Select Command";
            // 
            // selectCommand
            // 
            this.selectCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectCommand.Enabled = false;
            this.selectCommand.FormattingEnabled = true;
            this.selectCommand.Location = new System.Drawing.Point(21, 223);
            this.selectCommand.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectCommand.Name = "selectCommand";
            this.selectCommand.Size = new System.Drawing.Size(160, 24);
            this.selectCommand.TabIndex = 17;
            // 
            // KeyBindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 418);
            this.Controls.Add(this.selectCommand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customKeyGroup);
            this.Controls.Add(this.selectApplicationLabel);
            this.Controls.Add(this.selectGestureLabel);
            this.Controls.Add(this.selectApp);
            this.Controls.Add(this.selectGesture);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "KeyBindForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Binding";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.customKeyGroup.ResumeLayout(false);
            this.customKeyGroup.PerformLayout();
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
        private System.Windows.Forms.ComboBox fkeyCombo;
        private System.Windows.Forms.RadioButton fkeyRadio;
        private System.Windows.Forms.RadioButton newkeyRadio;
        private System.Windows.Forms.GroupBox customKeyGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox selectCommand;
    }
}