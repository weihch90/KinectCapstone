using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoadingWindow loadingWindow = new LoadingWindow();
            loadingWindow.ShowDialog();
            ImageClassifier classifer = loadingWindow.GetClassifier();
            if (classifer != null)
            {
                MainForm mainForm = new MainForm(classifer);
                Application.Run(mainForm);
            }
        }
    }
}
