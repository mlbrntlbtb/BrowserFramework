using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CommonLib.DlkHandlers
{
    public static class DlkSourceControlHandler
    {
        #region DECLARATIONS
        public const string FILE_SRC_CONTROL = "SourceControl.bat";
        public const string FILE_SRC_CONTROL_LOG = "SourceControl.log";
        #endregion

        #region PROPERTIES

        public static bool SourceControlEnabled
        {
            get
            {
#if DEBUG
                bool sourceControlEnabled;
                Boolean.TryParse(DlkConfigHandler.GetConfigValue("sourcecontrolenabled"), out sourceControlEnabled);
                return sourceControlEnabled;
#else
                return false;
#endif
            }
            set
            {
                try
                {
                    DlkConfigHandler.UpdateConfigValue("sourcecontrolenabled", value.ToString());
                }
                catch
                {
                    // do nothing
                }
            }
        }

        /// <summary>
        /// Flag that checks if source control is supported for machine
        /// </summary>
        public static bool SourceControlSupported
        {
            get
            {
#if DEBUG
                /* check for source tool */
                if (!File.Exists(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), DlkSourceControlHandler.FILE_SRC_CONTROL)))
                {
                    return false;
                }

                /* VS 2017 */
                string vs = Environment.GetEnvironmentVariable("TF2017");
                if (!string.IsNullOrEmpty(vs) && File.Exists(System.IO.Path.Combine(vs, "TF.exe")))
                {
                    return true;
                }

                /* VS 2013 */
                vs = Environment.GetEnvironmentVariable("VS120COMNTOOLS");
                if (!string.IsNullOrEmpty(vs) && File.Exists(System.IO.Path.Combine(vs, @"..\IDE\TF.exe")))
                {
                    return true;
                }

                /* VS 2010 */
                vs = Environment.GetEnvironmentVariable("VS100COMNTOOLS");
                if (!string.IsNullOrEmpty(vs) && File.Exists(System.IO.Path.Combine(vs, @"..\IDE\TF.exe")))
                {
                    return true;
                }
#endif
                return false;
            }
        }


        public static bool IsFileUnderSourceControl(string FilePath)
        {
            FileInfo fi = new FileInfo(FilePath);
            return fi.IsReadOnly;
        }
#endregion

#region METHODS

        private static void ExecuteCommand(String Command, String Options, String Path)
        {
            int iTimeout = 60;
            Process p = new Process();
            p.StartInfo.FileName = FILE_SRC_CONTROL;
            p.StartInfo.Arguments = Command + " " + Options + " \"" + Path + "\"";
            //p.StartInfo.WorkingDirectory = DlkEnvironment.DirRoot;
            p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit(iTimeout * 1000);
            if (!p.HasExited)
            {
                p.Kill();
            }
        }

        private static void ExecuteCommand(String Command, String Options, String[] Arguments)
        {
            int iTimeout = 60;
            Process p = new Process();
            p.StartInfo.FileName = FILE_SRC_CONTROL;
            p.StartInfo.Arguments = Command + " " + Options;
            foreach (string args in Arguments)
            {
                p.StartInfo.Arguments += " " + "\"" + args + "\"";
            }
            //p.StartInfo.WorkingDirectory = DlkEnvironment.DirRoot;
            p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit(iTimeout * 1000);
            if (!p.HasExited)
            {
                p.Kill();
            }
        }

        public static bool Add(string Path, string CommandOptions)
        {
            try
            {
                ExecuteCommand("add", CommandOptions, Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(string Path, string CommandOptions)
        {
            try
            {
                ExecuteCommand("delete", CommandOptions, Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckIn(string Path, string CommandOptions)
        {
            try
            {
                ExecuteCommand("checkin", CommandOptions, Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckOut(string Path, string CommandOptions)
        {
            try
            {
                ExecuteCommand("checkout", CommandOptions, Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets history of file and dumps to SourceControl.log
        /// </summary>
        /// <param name="Path">Path of file under source control</param>
        /// <param name="CommandOptions">Command options (if any)</param>
        /// <returns>TRUE if successful, FALSE otherwise</returns>
        public static bool History(string Path, string CommandOptions)
        {
            try
            {
                ExecuteCommand("history", CommandOptions, Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsMapped(string Path, string CommandOptions)
        {
            try
            {
                ExecuteCommand("workfold", CommandOptions, Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool GetFiles(string Path, string CommandOptions)
        {
            try
            {
                ExecuteCommand("get", CommandOptions, Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool MapFolder(string LocalPath, string ServerPath, string CommandOptions)
        {
            try
            {
                if (CommandOptions.Length > 0)
                {
                    CommandOptions += " ";
                }
                CommandOptions = "/map";
                ExecuteCommand("workfold", CommandOptions, new string[] { ServerPath, LocalPath });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool UnmapFolder(string LocalPath, string CommandOptions)
        {
            try
            {
                if (CommandOptions.Length > 0)
                {
                    CommandOptions += " ";
                }
                CommandOptions = "/unmap";
                ExecuteCommand("workfold", CommandOptions, LocalPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

#endregion


    }
}
