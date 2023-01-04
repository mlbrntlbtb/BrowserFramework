using CommonLib.DlkHandlers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestRunner.Common
{
    static class DlkEditorWindowHandler
    {
        private const int DEFAULT_MAX_EDITOR_WINDOWS = 4;

        #region USER32 API Initialization
        /// <summary>
        /// Enumerate scheduler child windows
        /// </summary>
        /// <param name="dwThreadId">scheduler process ID</param>
        /// <param name="lpfn">returned child window</param>
        /// <param name="lParam">value to be passed to the callback function.</param>
        /// <returns>true=if child window is found;flase=if not</returns>
        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        /// <summary>
        /// Checks if child window is open
        /// </summary>
        /// <param name="hWnd">child handle to window</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(int hWnd);

        /// <summary>
        /// Get Scheduler Test Editor window title
        /// </summary>
        /// <param name="hWnd">child handle to window</param>
        /// <param name="title">code to get window title</param>
        /// <param name="size">window title size</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetWindowText(int hWnd, StringBuilder title, int size);

        /// <summary>
        /// Send close command to scheduler child window dialog
        /// </summary>
        /// <param name="hWnd">child handle to window</param>
        /// <param name="Msg">close command</param>
        /// <param name="wParam">default 0</param>
        /// <param name="lParam">default 0</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Checks if child window dialog is closed
        /// </summary>
        /// <param name="hWnd">child handle to window</param>
        /// <returns>true if executed else false</returns>
        [DllImport("user32.dll")]
        private static extern bool CloseWindow(IntPtr hWnd);

        /// <summary>
        /// Brings window to front
        /// </summary>
        /// <param name="hWnd">child handle to window</param>
        /// <returns>true if executed else false</returns>
        [DllImport("user32.dll", SetLastError = false)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Show window if minimized
        /// </summary>
        /// <param name="hWnd">child handle to window</param>
        /// <param name="cmd">9 = Restore window</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmd);

        /// <summary>
        /// Checks if window is mminimized
        /// </summary>
        /// <param name="hWnd">child window handle</param>
        /// <returns>true if minimized/shown</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        /// <summary>
        /// delegate to enumerate advance test scheduler child windows
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);
        #endregion

        /// <summary>
        /// Performs a check if a Test Editor script is already opened
        /// </summary>
        /// <param name="ScriptPath">test script path</param>
        /// <returns>true if found, else false</returns>
        public static bool IsEditorScriptOpened(string ScriptPath, bool BringToFront = true)
        {
            foreach (KeyValuePair<string,IntPtr> win in GetActiveTEWindows("testrunner","scheduler"))
            {
                if (win.Key.Contains(ScriptPath))
                {
                    if (BringToFront)
                    {
                        if (IsIconic(win.Value))
                            ShowWindow(win.Value, 9);
                        else
                            SetForegroundWindow(win.Value);
                    }
                    
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the number of active Test Editor Windows have reached maximum allowed count
        /// </summary>
        /// <returns></returns>
        public static bool IsActiveEditorsMaxed(bool IsScheduler = false)
        {
            int maxEditorWindows = DEFAULT_MAX_EDITOR_WINDOWS;
            int.TryParse(DlkConfigHandler.GetConfigValue("maxeditorwindows"), out maxEditorWindows);
            if(maxEditorWindows <= 0)
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_MAX_EDITOR_INVALID);
                return true;
            }
            if (GetActiveTEWindows("testrunner", "scheduler").Count == maxEditorWindows)
            {
                DlkUserMessages.ShowWarning(string.Format(DlkUserMessages.WRN_MAX_EDITOR_WINDOWS, maxEditorWindows.ToString(), IsScheduler ? "Scheduler" : "Test Runner" ),"Editor Limit Reached");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if advance test scheduler Test Editor window dialog are closed
        /// </summary>
        /// <returns>true=if all test scheduler test editor window are closed;false=if not closed</returns>
        public static bool TestEditorWindowClosed(List<IntPtr> activeWindows)
        {
            uint WM_CLOSE = 0x10;
            bool childNotClosed;

            foreach (IntPtr child in activeWindows)
            {
                SendMessage(child, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                childNotClosed = CloseWindow(child);

                if (childNotClosed)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Retrieves a list of currently active Test Editor windows
        /// </summary>
        /// <param name="windowNames">testrunner or scheduler</param>
        /// <returns>dictionary for test editor path and handle</returns>
        public static Dictionary<string, IntPtr> GetActiveTEWindows(params string[] windowNames)
        {
            Dictionary<string, IntPtr> windows = new Dictionary<string, IntPtr>();

            foreach (var windowName in windowNames)
            {
                Process process = Process.GetProcesses().FirstOrDefault(f => f.ProcessName.ToLower() == windowName);
                if (process != null)
                {
                    foreach (ProcessThread processThread in process.Threads)
                    {
                        EnumThreadWindows(processThread.Id, (hWnd, lParam) =>
                        {
                            //Check if Window is Visible or not.
                            if (!IsWindowVisible((int)hWnd))
                                return true;

                            //Get the Window's Title.
                            StringBuilder title = new StringBuilder(256);
                            GetWindowText((int)hWnd, title, 256);

                            //Check if child window title contains Test Editor
                            if (title.Length > 0 && title.ToString().Contains("Test Editor") && !windows.ContainsKey(title.ToString()))
                            {
                                windows.Add(title.ToString(), hWnd);
                            }

                            return true;
                        }, IntPtr.Zero);
                    }
                }
            }

            return windows;
        }
    }
}
