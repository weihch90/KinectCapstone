using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestureStudio
{
    public partial class LoadingWindow : Form
    {
        public string LoaderMessage
        {
            get
            {
                return this.loaderMessage.Text;
            }

            set
            {
                this.loaderMessage.Text = value;
            }
        }

        public LoadingWindow()
        {
            InitializeComponent();
        }
    }
}
