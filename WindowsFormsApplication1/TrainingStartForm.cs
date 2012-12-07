using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GestureStudio
{
    public partial class TrainingStartForm : Form
    {
        public TrainingStartForm()
        {
            InitializeComponent();
        }

        public Action StartCallback
        {
            get;
            set;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            SynchronizationContext ctx = SynchronizationContext.Current;

            // Save the new gesture name to gestureInfoNew.data
            string name = gestureName.Text;
            Gestures.addNewGesture(name);
            Gestures.saveData(GestureStudio.GesturesDataPathNew);
            Gestures.loadData(GestureStudio.GesturesDataPathNew);

            // Count down 5.
            int countDown = 5;
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (o, src) =>
                {
                    countDown--;
                    ctx.Post((state) =>
                        {
                            this.countDownLabel.Text = countDown.ToString();
                        }, null);

                    if (countDown == 0)
                    {
                        this.OnStart();
                        timer.Stop();
                    }
                };

            timer.Start();
        }

        void OnStart()
        {
            if (this.StartCallback != null)
            {
                this.StartCallback();
            }
        }
    }
}
