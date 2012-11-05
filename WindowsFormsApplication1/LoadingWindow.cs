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

namespace WindowsFormsApplication1
{
    public partial class LoadingWindow : Form
    {
        bool initialized = false;
        ImageClassifier classifier;

        public LoadingWindow()
        {
            InitializeComponent();
            var ctx = SynchronizationContext.Current;

            this.classifier = new ImageClassifier();
            classifier.BeginInitialize(() =>
                {
                    this.initialized = true;
                    ctx.Post((o) =>
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            this.Close();
                        }, null);
                });
        }

        public ImageClassifier GetClassifier()
        {
            if (this.initialized)
            {
                return this.classifier;
            }
            else
            {
                return null;
            }
        }
    }
}
