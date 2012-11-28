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

            return null;
        }

        private void setApps()
        {
            foreach (String app in Gestures.Applications)
            {
                this.selectApp.Items.Add(app);
            }
        }

        private void setGestures()
        {
            foreach (GestureInfo gesture in Gestures.getGestures().Values)
            {
                this.selectGesture.Items.Add(gesture.getName());
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
    }
}
