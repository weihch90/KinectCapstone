using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestureStudio
{
    public partial class DirectToTutorialForm : Form
    {
        public DirectToTutorialForm()
        {
            InitializeComponent();
        }

        public bool isIgnoreChecked()
        {
            return this.disableCheck.Checked;
        }
    }
}
