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
        public const string ModelFileName = @"model_new.svm";
        public const string ProblemFile = @"rgbdfea_depth_first_small_dict_threshold1500.mat";
        public const string FeatureFile = @"my_rgbdfea_depth_first.mat";
        public const string Gestures_Data_Path = @"../../data/gesturesInfo.data";
        public const string SettingFile = @"../../data/settings.data";
        public const string KeySettingsFile_Path = @"../../data/keySettings.data";
        
        private const bool useNewDesign = true;
        public static bool GENERIC_GESTURES = true;

        static MainForm mainForm;
        static MainWindow mainWindow;
        static LoadingWindow loadingWindow;
        static TrainingStartForm trainerForm;
        static SynchronizationContext mainThreadContext;

        public static void DisplayTrainerForm(Action startCallback)
        {
            trainerForm.StartCallback = () =>
                {
                    if (startCallback != null)
                    {
                        startCallback();
                    }

                    mainThreadContext.Post((state) =>
                        {
                            trainerForm.Hide();
                        }, null);
                };
            
            mainThreadContext.Post((state) =>
                {
                    if (useNewDesign)
                        trainerForm.Show(mainWindow);
                    else
                        trainerForm.Show(mainForm);
                }, null);
        }
        
        public static void DisplayLoadingWindow(string message)
        {
            mainThreadContext.Post((state) =>
                {
                    loadingWindow.LoaderMessage = message;
                    if (useNewDesign)
                    {
                        mainWindow.Disable();
                        loadingWindow.Show(mainWindow);
                    }
                    else
                    {
                        mainForm.Disable();
                        loadingWindow.Show(mainForm);
                    }
                }, null);
        }

        public static void HideLoadingWindow()
        {
            mainThreadContext.Post((state) =>
                {
                    if (useNewDesign)
                    {
                        mainWindow.Enable();
                        loadingWindow.Hide();
                    }
                    else
                    {
                        mainForm.Enable();
                        loadingWindow.Hide();
                    }
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
            if (useNewDesign)
            {
                mainWindow = new MainWindow();
            }
            else
            {
                mainForm = new MainForm();
            }
            loadingWindow = new LoadingWindow();
            trainerForm = new TrainingStartForm();

            mainThreadContext = SynchronizationContext.Current;

            if (useNewDesign)
                Application.Run(mainWindow);
            else
                Application.Run(mainForm);
        }
    }
}
