using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;

namespace GestureStudio
{
    public class AppKeyInfo {
        private string command; // app command, we can user other ways to store commands

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

        public override string ToString()
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
            if (appkeys.ContainsKey(id))
                return appkeys[id];
            else
                return null;
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
        private static string dataPath = GestureStudio.Gestures_Data_Path;
        public static int NOISE_LABEL = 6;

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
            loadData(GestureStudio.Gestures_Data_Path);
        }

        public static string getPath()
        {
            return dataPath;
        }

        public static void changeDataFile(string path)
        {
            dataPath = path;
        }

        public static void loadData(string path)
        {
            changeDataFile(path);
            string[] lines = File.ReadAllLines(path);

            gestureList = new Dictionary<int, GestureInfo>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                string gestureId = line.Substring(0, line.IndexOf(":"));
                string name;

                if(line.IndexOf("{") == -1)
                    name = line.Substring(line.IndexOf(":") + 1, line.Length - line.IndexOf(":") - 1);
                else
                    name = line.Substring(line.IndexOf(":") + 1, line.IndexOf("{") - line.IndexOf(":") - 1);
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

        public static void saveData()
        {
            saveData(dataPath);
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
                    StringBuilder sb = new StringBuilder();
                    sb.Append(pair.Key + ":" + gesture.getName());
                    if (gesture.getAllCommands().Count > 0)
                    {
                        sb.Append("{");
                        bool first = true;
                        // go over all the commands in the gesture
                        foreach (KeyValuePair<int, AppKeyInfo> commands in gesture.getAllCommands())
                        {
                            if (first)
                            {
                                sb.Append(commands.Key + ":" + commands.Value.ToString());
                                first = false;
                            }
                            else
                                sb.Append("," + commands.Key + ":" + commands.Value.ToString());
                        }
                        sb.Append("}");
                    }
                    file.WriteLine(sb.ToString());
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
         * return gestureId of given gesture name
         */
        public static int getGestureId(string name)
        {
            foreach (int i in gestureList.Keys)
            {
                if (gestureList[i].getName() == name)
                {
                    return i;
                }
            }

            return -1;
        }

        /*
         * return gestureId of given gesture name
         */
        public static int getGestureIndex(string name)
        {
            int count = 0;
            foreach (int i in gestureList.Keys)
            {
                if (gestureList[i].getName() == name)
                {
                    return count;
                }
                count++;
            }

            return -1;
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

        public static void addNewGesture(string gestureName)
        {
            int id = gestureList.Count + 1;
            addNewGesture(id, gestureName);
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



        public static void setAppKeyForGesture(string gestureName, string appName, string command)
        {
            setAppKeyForGesture(gestureName, appName, new AppKeyInfo(command));
        }

        public static void setAppKeyForGesture(string gestureName, string appName, AppKeyInfo keyInfo)
        {
            int gestureId =getGestureId(gestureName);
            int appId = KeyControls.getAppId(appName);
            if (gestureId != -1 && appId != -1)
                gestureList[gestureId].setAppCommand(appId, keyInfo);
        }

        public static void setAppKeyForGesture(int gestureId, int appId, string command)
        {
            setAppKeyForGesture(gestureId, appId, new AppKeyInfo(command));
        }
        
        public static void setAppKeyForGesture(int gestureId, int appId, AppKeyInfo keyInfo)
        {
            if (containGestureId(gestureId))
            {
                gestureList[gestureId].setAppCommand(appId, keyInfo);
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
