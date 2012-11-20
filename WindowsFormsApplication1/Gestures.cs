using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GestureStudio
{
    public class AppKeyInfo {
        private int id; // app id
        private string command; // app command

        public AppKeyInfo(int id, string command)
        {
            this.id = id;
            this.command = command;
        }
    }

    public class GestureInfo
    {
        private string name;
        private int id;
        private List<AppKeyInfo> appkeys;

        public GestureInfo(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.appkeys = new List<AppKeyInfo>();
        }
    }

    /*
     * Singleton of Gestures
     * Cannot be inherited
     */
    public sealed class Gestures
    {
 
        private const string DATA_FILE_PATH = @"data/gesturesInfo.data";
        private const int ARRAY_EXPAND_SIZE = 2;

        private static string[] gestureNames;
        private static string[][] gestureKeys;
        private static int gestureCount;

        private static Gestures instance;

        public static Gestures Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Gestures();
                }
                return instance;
            }

        }

        // private constructor
        private Gestures() {
            loadData(DATA_FILE_PATH);
        }

        public static void loadData(string path)
        {
            string[] lines = File.ReadAllLines(path);
            gestureCount = lines.Length;
            gestureNames = new string[lines.Length + ARRAY_EXPAND_SIZE];
            gestureKeys = new string[lines.Length + ARRAY_EXPAND_SIZE][];
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string name = line.Substring(0, line.IndexOf(":"));
                // if it is bined to specific key command to specific application
                if (line.IndexOf(":") + 1 != line.Length)
                {
                    string keyAssignment = line.Substring(line.IndexOf("{") + 1, line.IndexOf("}") - line.IndexOf("{") - 1);
                    string[] assignments = keyAssignment.Split(',');
                    gestureKeys[i] = new string[assignments.Length];
                    // assign application key commands
                    for (int j = 0; j < assignments.Length; j++)
                    {
                        if (assignments[j] != "")
                            gestureKeys[i][j] = assignments[j];
                        else
                            gestureKeys[i][j] = null;
                    }

                }
                gestureNames[i] = name;
            }
        }

        /*
         * data format
         * GestureName:{command,command,....,command}
         * GestureName2:{command,command,....,command}
         * ...
         */
        public static void saveData(string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                for (int i = 0; i < gestureCount; i++)
                {
                    string line = gestureNames[i] + ":";
                    if (gestureKeys != null && gestureKeys.Length != 0)
                    {
                        line += ":{";
                        for (int j = 0; j < gestureKeys[i].Length; j++)
                        {    
                            line += gestureKeys[i][j] + ",";
                        }
                        line += "}";
                    }
                    file.WriteLine(line);
                }
            }
        }

        public static string getGestureName(int index)
        {
            if (index > gestureCount || index < 0) {
                return null;
            }
            return gestureNames[index];
        }

        public static void addNewGesture(string gestureName)
        {
            if (gestureCount >= gestureNames.Length)
            {
                // extend gestureName array
                string[] furtherNames = new string[ARRAY_EXPAND_SIZE];
                List<string> listNames = gestureNames.ToList<string>();
                listNames.Add(gestureName);
                listNames.AddRange(furtherNames);
                gestureNames = listNames.ToArray();

                //extend gestureKeys array
                List<string[]> listKeys = gestureKeys.ToList<string[]>();
                for (int i = 0; i < ARRAY_EXPAND_SIZE; i++)
                {
                    listKeys.Add(new string[ARRAY_EXPAND_SIZE]);
                }
                gestureKeys = listKeys.ToArray();
            }
            else
            {
                gestureNames[gestureCount] = gestureName;
            }
            gestureCount++;
        }

        public static bool containGestureName(string gestureName)
        {
            for (int i = 0; i < gestureCount; i++)
            {
                if (gestureName.Equals(gestureNames[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static string getAppKeyForGesture(int gestureIndex, int appIndex)
        {
            if (0 <= appIndex && 0 <= gestureIndex && gestureCount > gestureIndex 
                && gestureKeys[gestureIndex] != null && gestureKeys[gestureIndex].Length > appIndex)
            {
                return gestureKeys[gestureIndex][appIndex];
            }
            return null;
        }

        public static void setAppKeyForGesture(int gestureIndex, int appIndex, string command)
        {
            if (0 <= appIndex && 0 <= gestureIndex && gestureCount > gestureIndex 
                && gestureKeys[gestureIndex].Length > appIndex)
            {
                // if no command set for this gesture.
                if (gestureKeys[gestureIndex] == null)
                {
                    gestureKeys[gestureIndex] = new string[appIndex + 1];
                }
                gestureKeys[gestureIndex][appIndex] = command;
                
            }
        }

        public static void deleteAppKeyForGesture(int gestureIndex, int appIndex) {
            if (0 <= appIndex && 0 <= gestureIndex && gestureCount > gestureIndex 
                && gestureKeys[gestureIndex].Length > appIndex)
            {
                gestureKeys[gestureIndex][appIndex] = null;
            }

        }
    }
}
