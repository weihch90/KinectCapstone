using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestureStudio
{
    public partial class KeyBindForm : Form
    {
        public KeyBindForm()
        {
            InitializeComponent();
            setGestures();
            setApps();
            setAdditionalKeys();
        }

        public string getGestureName()
        {
            return (string)selectGesture.SelectedItem;
        }

        public string getAppName()
        {
            return (string)selectApp.SelectedItem;
        }

        public string getKeyBind()
        {

            if (this.customKeyGroup.Enabled)
            {
                if (newkeyRadio.Checked)
                {
                    if (keyInput.Text.Equals(""))
                        return null;
                    return (ctrlCheck.Checked ? "ctrl-" : "") +
                            (shiftCheck.Checked ? "shift-" : "") +
                            (altCheck.Checked ? "alt-" : "") +
                            keyInput.Text;
                }
                else if (fkeyRadio.Checked)
                    return (string)fkeyCombo.SelectedItem;
            }
            else
            {
                string appName = ((string)this.selectApp.SelectedItem).Trim();
                string command = ((string)this.selectCommand.SelectedItem).Trim();
                if (appName != "" && command != "")
                {
                    return command;
                }
            }
            return null;
        }

        private void setApps()
        {
            this.selectApp.Items.Clear();
            foreach (String app in KeyControls.getApplications())
            {
                this.selectApp.Items.Add(app);
            }
        }

        private void setGestures()
        {
            this.selectGesture.Items.Clear();
            foreach (GestureInfo gesture in Gestures.getGestures().Values)
            {
                this.selectGesture.Items.Add(gesture.getName());
            }
        }


        private void setCommands()
        {
            string appName = (string)this.selectApp.SelectedItem;
            this.selectCommand.Items.Clear();
            foreach (string command in KeyControls.getKeyMatches()[KeyControls.getAppId(appName)].Keys)
            {
                this.selectCommand.Items.Add(command);
            }
        }

        private void setAdditionalKeys()
        {
            foreach (String key in Control.KeysList)
            {
                this.fkeyCombo.Items.Add(key);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void setKeyLabel_Click(object sender, EventArgs e)
        {

        }

        private void SelectAppIndex_Changed(object sender, EventArgs e)
        {
            if ((string)this.selectApp.SelectedItem != "")
            {
                this.selectCommand.Enabled = true;
                this.setCommands();
            }
            else
            {
                this.selectCommand.Enabled = false;
            }
        }
    }
}
