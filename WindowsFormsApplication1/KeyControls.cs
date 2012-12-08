using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GestureStudio
{
    class KeyControls
    {
        private static KeyControls instance;
        private static Dictionary<int, Dictionary<string, string>> keyMatches;
        private static string[] applications;
        public static KeyControls Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KeyControls();
                }

                return instance;
            }
        }

        public static Dictionary<int, Dictionary<string, string>> getKeyMatches()
        {
            return keyMatches;
        }

        public static string[] getApplications()
        {
            return applications;
        }

        public static int getAppId(string appName)
        {
            for (int i = 0; i < applications.Length; i++)
                if (applications[i] == appName)
                    return i;
            return -1;
        }

        private KeyControls()
        {
            loadData();
        }

        // file format
        // WMP, PP
        // appId:command:key
        // ...
        private static void loadData()
        {
            string[] lines = File.ReadAllLines(GestureStudio.KeySettingsFilePath);
            keyMatches = new Dictionary<int, Dictionary<string, string>>();
            string[] apps = lines[0].Split(',');
            applications = new string[apps.Length];

            // assign all the available applications
            for (int i = 0; i < apps.Length; i++)
            {
                applications[i] = apps[i].Trim();
                keyMatches.Add(i, new Dictionary<string, string>());
            }

            // go over all the key commands

            for (int i = 1; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(':');
                keyMatches[int.Parse(temp[0])].Add(temp[1], temp[2]);
            }

        }

    }

}
