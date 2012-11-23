using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace GestureStudio
{
    public class AppKeyInfo {
        private string command; // app command

        public AppKeyInfo(string command)
        {
            this.command = command;
        }

        public void setCommand(string command)
        {
            this.command = command;
        }

        public string getCommand()
        {
            return command;
        }

        public string toString()
        {
            return command;
        }

    }

    public class GestureInfo
    {
        private string name;
        private Dictionary<int, AppKeyInfo> appkeys;    //Dictionary<appId, keyCommands>

        public GestureInfo(string name)
        {
            this.name = name;
            this.appkeys = new Dictionary<int, AppKeyInfo>();
        }

        public void setAppCommand(int id, AppKeyInfo keyInfo)
        {
            if (appkeys.ContainsKey(id))
            {
                appkeys.Remove(id);
            }
            appkeys.Add(id, keyInfo);
        }

        public AppKeyInfo getAppKeyCommand(int id)
        {
            return appkeys[id];
        }

        public Dictionary<int, AppKeyInfo> getAllCommands()
        {
            return appkeys;
        }

        public void deleteAppCommand(int id)
        {
            appkeys.Remove(id);
        }

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }
    }

    /*
     * Singleton of Gestures
     * Cannot be inherited
     */
    public sealed class Gestures
    {
        private static Dictionary<int, GestureInfo> gestureList;  // dictionary<id, GestureInfo>

        private static Gestures instance;

        public static Gestures GetInstance()
        {
            if (instance == null)
            {
                instance = new Gestures();
            }
            return instance;
        }

        // private constructor
        private Gestures() {
            loadData(GestureStudio.Gestures_Data_Path);
        }

        public static void loadData(string path)
        {
            string[] lines = File.ReadAllLines(path);

            gestureList = new Dictionary<int, GestureInfo>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                string gestureId = line.Substring(0, line.IndexOf(":"));
                string name = line.Substring(line.IndexOf(":") + 1, line.IndexOf("{") - line.IndexOf(":") - 1);
                
                GestureInfo gesture = new GestureInfo(name);

                // if it is bined to specific key command to specific application
                if (line.IndexOf("{") != -1)
                {
                    string keyAssignment = line.Substring(line.IndexOf("{") + 1, line.IndexOf("}") - line.IndexOf("{") - 1);
                    string[] assignments = keyAssignment.Split(',');
                    // assign application key commands
                    for (int j = 0; j < assignments.Length; j++)
                    {
                        string assignment = assignments[j].Trim();
                        if (assignment != "")
                        {
                            string[] command = assignment.Split(':');
                            AppKeyInfo keyInfo = new AppKeyInfo(command[1]);
                            gesture.setAppCommand(int.Parse(command[0]), keyInfo);
                        }
                    }
                }

                gestureList.Add(int.Parse(gestureId), gesture);
            }
        }

        /*
         * data format
         * gestureId:GestureName{id:command,id:command,....,id:command}
         * gestureId:GestureName2{id:command,id:command,....,id:command}
         * gestureId:GestureName3
         * ...
         */
        public static void saveData(string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                // go over all the gestures
                foreach (KeyValuePair<int, GestureInfo> pair in gestureList)
                {
                    GestureInfo gesture = pair.Value;
                    string line = pair.Key + ":" + gesture.getName();
                    if (gesture.getAllCommands().Count > 0)
                    {
                        line += "{";
                        bool first = true;
                        // go over all the commands in the gesture
                        foreach (KeyValuePair<int, AppKeyInfo> commands in gesture.getAllCommands())
                        {
                            if (first)
                            {
                                line += commands.Key + ":" + commands.Value.toString();
                                first = false;
                            } else
                                line +=  "," + commands + ":" + commands.Value.toString();
                        }
                        line += "}";
                    }
                    file.WriteLine(line);
                }
            }
        }

        public static string getGestureName(int id)
        {
            if (gestureList.ContainsKey(id))
                return gestureList[id].getName();
            else
                return null;
        }


        /*
         * returns number of gestures
         */
        public static int getGesturesCount()
        {
            return gestureList.Count;
        }

        /*
         * Returns all the gestures
         */
        public static Dictionary<int, GestureInfo> getGestures()
        {
            return gestureList;
        }

        public static void addNewGesture(int id, string gestureName)
        {
            if (gestureList.ContainsKey(id))
            {
                gestureList.Remove(id);
            }
            gestureList.Add(id, new GestureInfo(gestureName));
        }

        public static bool containGestureId(int id)
        {
            return gestureList.ContainsKey(id);
        }

        public static AppKeyInfo getAppKeyForGesture(int gestureId, int appId)
        {
            if(containGestureId(gestureId))
                return gestureList[gestureId].getAppKeyCommand(appId);
            else
                return null;
        }

        public static void setAppKeyForGesture(int gestureId, int appId, string command)
        {
            if (containGestureId(gestureId))
            {
                gestureList[gestureId].setAppCommand(appId, new AppKeyInfo(command));
            }
        }

        public static void deleteAppKeyForGesture(int gestureId, int appId) {
            if (containGestureId(gestureId))
            {
                gestureList[gestureId].deleteAppCommand(appId);
            }
        }
    }
}
