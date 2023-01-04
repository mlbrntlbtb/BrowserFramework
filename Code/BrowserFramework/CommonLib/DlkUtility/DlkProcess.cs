using System;
using System.Diagnostics;

namespace CommonLib.DlkUtility
{
    public static class DlkProcess
    {
        /// <summary>
        /// Checks if the specific process is running
        /// </summary>
        /// <param name="ProcessName">Name of the process</param>
        /// <param name="contains">checker if you want the specific process name or part of the process name</param>
        /// <returns>returns true if the process exists and false if not</returns>
        public static bool IsProcessRunning(string ProcessName, bool Contains = true)
        {
            Boolean bIsRunning = false;
            Process[] processes = Process.GetProcesses();
            
            foreach (Process process in processes)
            {
                if (Contains ? process.ProcessName.ToLower().Contains(ProcessName.ToLower()) : process.ProcessName.ToLower().Equals(ProcessName.ToLower()))
                {
                    string pr = process.ProcessName;
                    bIsRunning = true;
                    break;
                }
            }
            return bIsRunning;
        }

        /// <summary>
        /// Runs a process. Use a timeout of 0 seconds to skip timeout logic
        /// </summary>
        /// <param name="Filename"></param>
        /// <param name="Arguments"></param>
        /// <param name="WorkingDir"></param>
        /// <param name="bHideWin"></param>
        /// <param name="iTimeoutSec"></param>
        public static void RunProcess(String Filename, String Arguments, String WorkingDir, Boolean bHideWin, int iTimeoutSec)
        {
            Process p = new Process();
            p.StartInfo.FileName = Filename;
            p.StartInfo.Arguments = Arguments;
            p.StartInfo.WorkingDirectory = WorkingDir;
            p.StartInfo.UseShellExecute = true;
            if (bHideWin)
            {
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            p.Start();
            if (iTimeoutSec > 0)
            {
                p.WaitForExit(iTimeoutSec * 1000);
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
        }

        // Runs a process. Modified to either wait or kill process after specific time.
        public static Process RunProcess(String Filename, String Arguments, String WorkingDir, Boolean bHideWin, Boolean bWait)
        {
            Process p = new Process();
            p.StartInfo.FileName = Filename;
            p.StartInfo.Arguments = Arguments;
            p.StartInfo.WorkingDirectory = WorkingDir;
            p.StartInfo.UseShellExecute = true;
            if (bHideWin)
            {
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            p.Start();

            if (bWait)
            {
                p.WaitForExit();
            }

            return p;
        }
    }
}
