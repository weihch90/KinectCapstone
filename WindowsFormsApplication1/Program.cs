using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestureStudio
{
    static class GestureStudio
    {
        public const string GestureLib_DictionartyPath = @"Dictionary.dic";

        static MainForm mainForm;
        static LoadingWindow loadingWindow;
        static SynchronizationContext mainThreadContext;

        public static void DisplayLoadingWindow(string message)
        {
            mainThreadContext.Post((state) =>
                {
                    loadingWindow.LoaderMessage = message;
                    loadingWindow.Location = mainForm.Location;

                    loadingWindow.Show(mainForm);

                }, null);
        }

        public static void HideLoadingWindow()
        {
            mainThreadContext.Post((state) =>
                {
                    loadingWindow.Hide();
                }, null);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainForm = new MainForm();
            loadingWindow = new LoadingWindow();
            mainThreadContext = SynchronizationContext.Current;

            Application.Run(mainForm);
        }
    }
}
