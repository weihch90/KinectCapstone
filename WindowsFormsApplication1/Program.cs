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
        public const string GestureLib_DictionartyPath = @"DictionaryNormal.dic";
        public const string ModelFileDemo = @"model.svm";
        public const string ModelFileEmpty = @"model_empty.svm";
        public const string ModelFileNew = @"model_new.svm";
        public const string FeatureFileDemo = @"feature_10.mat";
        public const string FeatureFileEmpty = @"feature_empty.mat";
        public const string FeatureFileNew = @"feature_new.mat";
        public const string GesturesDataPathDemo = @"../../data/gesturesInfo.data";
        public const string GesturesDataPathEmpty = @"../../data/gesturesInfoEmpty.data";
        public const string GesturesDataPathNew = @"../../data/gesturesInfoNew.data";
        public const string SettingFile = @"../../data/settings.data";
        public const string KeySettingsFilePath = @"../../data/keySettings.data";
        public const string GestureImagePath = @"../../data/gestureImages";
        
        private const bool useNewDesign = true;
        public static bool GENERIC_GESTURES = true;
        public static bool DISPLAY_DETECTED_GESTURE_IMG = true;
        public static bool TABLE_DISPLAY_GESTURE_IMG = true;

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
