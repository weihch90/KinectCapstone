using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace GestureStudio
{
    public class Control
    {
        public static string[] KeysList = {"f1", "f2", "f3", "f4", "f5", "f6", "f7", "f8", "f9", "f10", "f11", "f12"};
        public static ushort[] ConversionList = {0x36, 0x37, 0x38, 0x39, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47};
        
        public Dictionary<string, ushort> keyDict;
        int buffer;
        bool firstRun;
        Stopwatch stopWatch;
        
        public Control(int delaybuffer = 700)
        {
            // http://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
            stopWatch = new Stopwatch();
            buffer = delaybuffer;
            firstRun = true;

            keyDict = new Dictionary<string, ushort>();
            for (int i = 0; i < KeysList.Length; i++)
            {
                keyDict.Add(KeysList[i], ConversionList[i]);
            }
        }

        public void startApp(string name, string args = "")
        {
            if (checkBuffer())
                Process.Start(name, args);
            stopWatch.Start();
        }

        public bool parseThenExecute(string command, int delay = 100, string app = "wmplayer") {
            if (command == null)
                return false;

            char press = (char) 0;
            ushort[] codes = new ushort[1];
            if (keyDict.ContainsKey(command))
            {
                codes[0] = keyDict[command];
            }
            else
            {
                string[] commands = command.Split('-');
                int len = commands.Length;
                if (len == 0)
                    return false;

                int codeslen = len - 1;
                codes = new ushort[codeslen];
                for (int i = 0; i < codeslen; i++)
                {
                    ushort code = stringToHexCode(commands[i]);
                    if (code == 0)
                        return false;
                    codes[i] = code;
                }

                press = commands[len - 1][0];
            }
            
            if (!focusApp(app))
                return false;

            System.Threading.Thread.Sleep(delay);
            holdOptionsThenPress(codes, press);

            return true;
        }

        private ushort stringToHexCode(string key)
        {
            if (key.Equals("ctrl"))
                return (ushort) 0x1d;
            if (key.Equals("alt"))
                return (ushort) 0x38;
            if (key.Equals("shift"))
                return (ushort) 0x2a;
            
            return 0;
        }

        public bool ctrlShiftThenPress(char ch, int delay = 100, string app = "wmplayer")
        {
            if (!focusApp(app))
                return false;

            System.Threading.Thread.Sleep(delay);
            holdOptionsThenPress(new ushort[] { 0x1d, 0x2a}, ch);

            return true;
        }

        public bool ctrlThenPress(char ch, int delay = 100, string app = "wmplayer")
        {
            if (!focusApp(app))
                return false;

            System.Threading.Thread.Sleep(delay);
            holdOptionsThenPress(new ushort[] {0x1d}, ch);

            return true;
        }

        public bool shiftThenPress(char ch, int delay = 100, string app = "wmplayer")
        {
            if (!focusApp(app))
                return false;

            System.Threading.Thread.Sleep(delay);
            holdOptionsThenPress(new ushort[] { 0x2a }, ch);

            return true;
        }

        // checks to see that another command wasn't issued recently.
        private bool checkBuffer()
        {
            stopWatch.Stop();
            if (firstRun || stopWatch.ElapsedMilliseconds > buffer) // another command has not been issued too soon
            {
                firstRun = false;
                stopWatch.Reset();
                return true;
            }
            return false;
        }

        // holds down the keys specified in array, presses the key ch, then releases the keys.
        public void holdOptionsThenPress(ushort[] options, char ch) {
            for (int i = 0; i < options.Length; i++) {
                KeyDown(options[i]);
                System.Threading.Thread.Sleep(50);
            }
            PressKey(ch, true);
            System.Threading.Thread.Sleep(100);
            PressKey(ch, false);
            for (int i = options.Length - 1; i > - 1; i--) {
                System.Threading.Thread.Sleep(50);
                KeyUp(options[i]);
            }
            stopWatch.Start();
        }
        
        private bool focusApp(string app)
        {
            if (!checkBuffer())
            {
                stopWatch.Start();
                return false;
            }
               
            Process[] processes = Process.GetProcessesByName(app);

            if (processes.Length == 0)
            {
                stopWatch.Start();
                return false;
            }
            IntPtr WindowHandle = processes[0].MainWindowHandle;

            WMP.SwitchWindow(WindowHandle);
            return true;
        }

        public bool volumeUp() {
            /* investigate why this doesn't work... */
            if (!checkBuffer())
            {
                stopWatch.Start();
                return false;
            }
            System.Threading.Thread.Sleep(100);
            KeyDown(0xAF);
            System.Threading.Thread.Sleep(100);
            KeyUp(0xAF);
            stopWatch.Start();
            return true;
        }

        // press or release a key ch based on the boolean press
        private void PressKey(char ch, bool press)
        {
            byte vk = WMP.VkKeyScan(ch);
            ushort scanCode = (ushort)WMP.MapVirtualKey(vk, 0);

            if (press)
                KeyDown(scanCode);
            else
                KeyUp(scanCode);
        }

        // presses a key down
        private void KeyDown(ushort scanCode)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = WMP.INPUT_KEYBOARD;
            inputs[0].ki.dwFlags = 0 | WMP.KEYEVENTF_SCANCODE;
            inputs[0].ki.wScan = (ushort)(scanCode & 0xff);

            uint intReturn = WMP.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
            if (intReturn != 1)
            {
                throw new Exception("Could not send key: " + scanCode);
            }
        }

        // releases a key
        private void KeyUp(ushort scanCode)
        {
            INPUT[] inputs = new INPUT[1];
            inputs[0].type = WMP.INPUT_KEYBOARD;
            inputs[0].ki.wScan = scanCode;
            inputs[0].ki.dwFlags = WMP.KEYEVENTF_KEYUP | WMP.KEYEVENTF_SCANCODE;
            uint intReturn = WMP.SendInput(1, inputs, System.Runtime.InteropServices.Marshal.SizeOf(inputs[0]));
            if (intReturn != 1)
            {
                throw new Exception("Could not send key: " + scanCode);
            }
        }

    }

    public class WMP
    {


        /// <summary>
        /// 
        /// </summary>
        public const uint WM_KEYDOWN = 0x100;

        /// <summary>
        /// 
        /// </summary>
        public const uint WM_KEYUP = 0x101;

        /// <summary>
        /// 
        /// </summary>
        public const uint WM_LBUTTONDOWN = 0x201;

        /// <summary>
        /// 
        /// </summary>
        public const uint WM_LBUTTONUP = 0x202;

        public const uint WM_CHAR = 0x102;

        /// <summary>
        /// 
        /// </summary>
        public const int MK_LBUTTON = 0x01;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_RETURN = 0x0d;

        public const int VK_ESCAPE = 0x1b;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_TAB = 0x09;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_LEFT = 0x25;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_UP = 0x26;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_RIGHT = 0x27;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_DOWN = 0x28;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_F5 = 0x74;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_F6 = 0x75;

        /// <summary>
        /// 
        /// </summary>
        public const int VK_F7 = 0x76;

        /// <summary>
        /// The GetForegroundWindow function returns a handle to the foreground window.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
          IntPtr hProcess,
          IntPtr lpBaseAddress,
          [Out()] byte[] lpBuffer,
          int dwSize,
          out int lpNumberOfBytesRead
         );

        public static void SwitchWindow(IntPtr windowHandle)
        {
            if (GetForegroundWindow() == windowHandle)
                return;

            IntPtr foregroundWindowHandle = GetForegroundWindow();
            uint currentThreadId = GetCurrentThreadId();
            uint temp;
            uint foregroundThreadId = GetWindowThreadProcessId(foregroundWindowHandle, out temp);
            AttachThreadInput(currentThreadId, foregroundThreadId, true);
            SetForegroundWindow(windowHandle);
            AttachThreadInput(currentThreadId, foregroundThreadId, false);

            while (GetForegroundWindow() != windowHandle)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="lpszClass"></param>
        /// <param name="lpszWindow"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern byte VkKeyScan(char ch);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IntPtr FindWindow(string name)
        {
            Process[] procs = Process.GetProcesses();

            foreach (Process proc in procs)
            {
                if (proc.MainWindowTitle == name)
                {
                    return proc.MainWindowHandle;
                }
            }

            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int MakeLong(int low, int high)
        {
            return (high << 16) | (low & 0xffff);
        }

        [DllImport("User32.dll")]
        public static extern uint SendInput(uint numberOfInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] input, int structSize);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();

        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;
        public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const uint KEYEVENTF_KEYUP = 0x0002;
        public const uint KEYEVENTF_UNICODE = 0x0004;
        public const uint KEYEVENTF_SCANCODE = 0x0008;
        public const uint XBUTTON1 = 0x0001;
        public const uint XBUTTON2 = 0x0002;
        public const uint MOUSEEVENTF_MOVE = 0x0001;
        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const uint MOUSEEVENTF_XDOWN = 0x0080;
        public const uint MOUSEEVENTF_XUP = 0x0100;
        public const uint MOUSEEVENTF_WHEEL = 0x0800;
        public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
        public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        int dx;
        int dy;
        uint mouseData;
        uint dwFlags;
        uint time;
        IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        uint uMsg;
        ushort wParamL;
        ushort wParamH;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT
    {
        [FieldOffset(0)]
        public int type;
        [FieldOffset(8)] //*
        public MOUSEINPUT mi;
        [FieldOffset(8)] //*
        public KEYBDINPUT ki;
        [FieldOffset(8)] //*
        public HARDWAREINPUT hi;
    }
}
