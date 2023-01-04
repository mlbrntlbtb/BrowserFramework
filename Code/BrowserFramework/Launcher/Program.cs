using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Launcher
{
    static class Program
    {
        #region DECLARATIONS

        static readonly string PATH = Path.Combine(RootPath, @"Tools\TestRunner\TestRunner.exe");
        static readonly string LOG_FOLDER = Path.Combine(RootPath, @"Tools\TestRunner\logs\");
        static readonly string MAIN_CONFIG = Path.Combine(RootPath, @"Products\Common\Configs\config.xml");

        /* Hidden Directories */
        static readonly string HIDDEN_DIR_TOOLS_TR = Path.Combine(RootPath, @"Tools\TestRunner");
        static readonly string HIDDEN_DIR_TOOLS_SELENIUM = Path.Combine(RootPath, @"Tools\Selenium");
        static readonly string HIDDEN_DIR_TOOLS_EXTLIB = Path.Combine(RootPath, @"Tools\ExternalLib");
        static readonly string HIDDEN_DIR_PRODUCTS = Path.Combine(RootPath, @"Products");
        static readonly string HIDDEN_DIR_PRODUCTS_COMMON = Path.Combine(RootPath, @"Products\Common");
        /* end of hidden directories */

        static int DEFAULT_LOAD_TIMEOUT = 10;
        #endregion

        #region PROPERTIES

        public static bool AutoAgentRunning { get; set; }

        public static string SessionLogFile { get; set; }

        static string RootPath
        {
            get
            {
                string binDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string rootPath = Directory.GetParent(binDir).FullName;
                while (new DirectoryInfo(rootPath)
                    .GetDirectories().Count(x => x.FullName.Contains("Products")) == 0)
                {
                    rootPath = Directory.GetParent(rootPath).FullName;
                }
                return rootPath;
            }
        }

        #endregion

        #region MAIN

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                HideDirectories();
                InitializeLaunch();
                ProcessStartInfo si = new ProcessStartInfo
                {
                    FileName = PATH
                };
                Process.Start(si);
            }
            catch (Exception ex)
            {
                LogToFile("Test Runner encountered an unexpected error. See program logs for details.", ex);
            }

        }

        #endregion

        #region METHODS

        static void InitializeLaunch()
        {
            bool initialLaunch;
            Boolean.TryParse(GetConfigValue("initiallaunch"), out initialLaunch);

            if (!initialLaunch)
            {
                bool sourceControlEnabled;
                Boolean.TryParse(GetConfigValue("sourcecontrolenabled"), out sourceControlEnabled);

                bool getLatestOnLaunch;
                Boolean.TryParse(GetConfigValue("getlatestversiononlaunch"), out getLatestOnLaunch);

                if (sourceControlEnabled && getLatestOnLaunch)
                {
                    GetLatestFiles();
                }
            }
        }

        static void HideDirectories()
        {
            try
            {
                string[] dirsToHide = { HIDDEN_DIR_TOOLS_TR };
                string[] dirsToDelete = { HIDDEN_DIR_TOOLS_SELENIUM, HIDDEN_DIR_TOOLS_EXTLIB };

                /* Hide unneccessary Products folder except Common, will be hidden later */
                foreach (string subDir in Directory.GetDirectories(HIDDEN_DIR_PRODUCTS))
                {
                    if (subDir == HIDDEN_DIR_PRODUCTS_COMMON)
                    {
                        HideDirectory(subDir);
                        continue;
                    }
                    HideDirectory(Path.Combine(subDir, "Framework\\RemoteBrowsers"));
                    HideDirectory(Path.Combine(subDir, "Framework\\Configs"));
                    HideDirectory(Path.Combine(subDir, "Framework\\Scheduler"), true, true);
                }

                /* Hide Tools\\TestRunner and Products\\Common */
                foreach (string dir in dirsToHide)
                {
                    HideDirectory(dir);
                }

                /* Delete unnecces folders */
                foreach (string dir in dirsToDelete)
                {
                    HideDirectory(dir, true, true);
                }
            }
            catch
            {
                /* Do nothing */
            }
        }

        /// <summary>
        /// Hide/delete target directory
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hide"></param>
        /// <param name="deleteIfNotReadOnly"></param>
        static void HideDirectory(string path, bool hideIfNotReadOnly = true, bool deleteIfNotReadOnly = false)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new DirectoryInfo(path);

                    if (!di.GetFiles("*.*", SearchOption.AllDirectories).Any(x => x.Attributes.HasFlag(FileAttributes.ReadOnly)))
                    {
                        if (deleteIfNotReadOnly)
                        {
                            Directory.Delete(path, true);
                            return;
                        }
                        if (hideIfNotReadOnly)
                        {
                            if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                            {
                                //Add Hidden flag    
                                di.Attributes |= FileAttributes.Hidden;
                            }
                        }
                        else
                        {
                            di.Attributes |= FileAttributes.Normal;
                        }
                    }
                }
            }
            catch
            {
                /* Do nothing */
            }
        }

        static string GetSourceControl()
        {
            string teamFoundation = string.Empty;
            string vs = Environment.GetEnvironmentVariable("VS120COMNTOOLS");

            if (string.IsNullOrEmpty(vs))
            {
                vs = Environment.GetEnvironmentVariable("VS100COMNTOOLS");
            }

            if (vs != null && File.Exists(Path.Combine(vs, @"..\IDE\TF.exe")))
                teamFoundation = Path.Combine(vs, @"..\IDE\TF.exe");

            return teamFoundation;
        }

        static void GetLatestFiles()
        {
            try
            {
                //Get latest files only when test scheduler is not running
                if (!IsProcessRunning("TestRunnerScheduler"))
                {
                    KillRunningProcesses();

                    //Get Latest Files
                    if (File.Exists(Path.Combine(RootPath, @"Tools\TestRunner\SourceControl.bat")))
                    {
                        RunProcess(Path.Combine(RootPath, @"Tools\TestRunner\SourceControl.bat"),
                            "get /recursive /force " + Path.Combine(RootPath, @"Tools\TestRunner"),
                            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            false,
                            300);
                    }
                    else if (!String.IsNullOrEmpty(GetSourceControl()))
                    {
                        RunProcess(GetSourceControl(),
                            "get /recursive /force " + Path.Combine(RootPath, @"Tools\TestRunner"),
                            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            false,
                            300);
                    }

                    ReRunExistingProcesses();
                }
            }
            catch
            {
                // throw
            }
        }

        private static void KillRunningProcesses()
        {
            if (IsProcessRunning("TestRunner"))
            {
                KillProcessByName("TestRunner");
            }

            AutoAgentRunning = false;
            if (IsProcessRunning("AutomationAgent"))
            {
                AutoAgentRunning = true;
                KillProcessByName("AutomationAgent");
            }
        }

        #endregion

        #region LOGS

        private static void InitializeLogger()
        {
            if (!Directory.Exists(LOG_FOLDER))
            {
                Directory.CreateDirectory(LOG_FOLDER);
            }
            SessionLogFile = Path.Combine(LOG_FOLDER + GetDateAsText("file") + ".log");
        }

        public static void LogToFile(String message, Exception exceptionObject = null)
        {
            InitializeLogger();
            if (exceptionObject != null)
            {
                File.WriteAllLines(SessionLogFile, new[] { GetDateAsText("long") + @"| " + message,
                GetDateAsText("long") + @"| " + exceptionObject.Source, 
                GetDateAsText("long") + @"| " + exceptionObject.Message, 
                GetDateAsText("long") + @"| " + exceptionObject.StackTrace });
            }
            else
            {
                File.WriteAllLines(SessionLogFile, new[] { GetDateAsText("long") + @"| " + message });
            }
        }

        public static String GetDateAsText(String formatType)
        {
            String sDate;
            switch (formatType.ToLower())
            {
                
                case "file":
                    sDate = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                    break;
                case "long":
                    sDate = string.Format("{0:yyyy-MM-dd hh:mm:ss tt}", DateTime.Now);
                    break;
                default:
                    sDate = string.Format("{0:yyyy-MM-dd hh:mm:ss tt}", DateTime.Now);
                    break;
            }
            return sDate;
        }

        #endregion 

        #region PROCESSES

        public static void RunProcess(String filename, String arguments, String workingDir, Boolean bHideWin, int iTimeoutSec)
        {
            Process p = new Process
            {
                StartInfo =
                {
                    FileName = filename,
                    Arguments = arguments,
                    WorkingDirectory = workingDir,
                    UseShellExecute = true
                }
            };
            if (bHideWin)
            {
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
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

        private static void KillProcessByName(string processName)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName == processName)
                {
                    try
                    {
                        process.Close();
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }

            if (IsProcessRunning(processName))
            {
                //process was not closed
                try
                {
                    processes = Process.GetProcesses();

                    foreach (Process process in processes)
                    {
                        if (process.ProcessName == processName)
                        {
                            process.Kill();
                        }
                    }
                }
                catch
                {
                    LogToFile("Couldn't kill process: " + processName);
                }
            }
        }

        public static bool IsProcessRunning(string processName)
        {
            Boolean bIsRunning = false;
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName.ToLower().Contains(processName.ToLower()))
                {
                    if (FileVersionInfo.GetVersionInfo(process.MainModule.FileName).FileDescription.ToLower().Contains("launcher"))
                    {
                        break;
                    }
                    bIsRunning = true;
                    break;
                }
            }
            return bIsRunning;
        }

        private static void ReRunExistingProcesses()
        {
            if (AutoAgentRunning)
                RunProcess(Path.Combine(RootPath, @"Tools\TestRunner\AutomationAgent.exe"), "",
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), true, -1);
        }

        #endregion

        #region CONFIGS
        /// <summary>
        /// Get Value of a specific node from config file
        /// </summary>
        /// <param name="node">Specific config node</param>
        /// <returns>Node value</returns>
        public static string GetConfigValue(string node)
        {
            string nodeVal = string.Empty;
            
            if (File.Exists(MAIN_CONFIG))
            {
                XDocument configFile = LoadConfigFile(MAIN_CONFIG, DEFAULT_LOAD_TIMEOUT);
                if (!ConfigExists(node, MAIN_CONFIG))
                {
                    UpdateConfigValue("initiallaunch", "true", MAIN_CONFIG);
                    configFile = LoadConfigFile(MAIN_CONFIG, DEFAULT_LOAD_TIMEOUT);
                }
                var item = configFile.Root.Element(node);
                if (item != null)
                {
                    nodeVal = item.Value;
                }
            }
            return nodeVal;
        }

        /// <summary>
        /// Create config file
        /// </summary>
        /// <param name="FilePath">Path of Config File</param>
        public static void CreateConfigFile(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                Save(FilePath);
            }
        }

        /// <summary>
        /// Save Config File
        /// </summary>
        /// <param name="path">Path of Config File</param>
        /// <param name="input">XDocument containing contents for config file</param>
        /// <param name="timeout">Retry Limit</param>
        private static void SaveConfigFile(string path, XDocument input, int timeout)
        {
            if (input == null)
                return;
            for (int i = 1; i < timeout; i++)
            {
                try
                {
                    input.Save(path);
                    break;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Saves config file with default xml node
        /// </summary>
        /// <param name="ConfigPath">Path of Config File</param>
        public static void Save(String ConfigPath)
        {
            XElement config = new XElement("config");
            XDocument xDoc = new XDocument(config);
            SaveConfigFile(ConfigPath, xDoc, DEFAULT_LOAD_TIMEOUT);
        }

        /// <summary>
        /// Updates specific element in a config file
        /// </summary>
        /// <param name="Node">Element Name</param>
        /// <param name="NodeValue">Element Value</param>
        private static void UpdateConfigValue(string Node, string NodeValue, string MainConfig)
        {
            CreateConfigFile(MainConfig);
            var configFile = LoadConfigFile(MainConfig, DEFAULT_LOAD_TIMEOUT);
            var element = configFile.Root.Element(Node);

            if (element != null)
            {
                element.Value = NodeValue;
            }
            else
            {
                //create the child node on XML
                XElement childNode = new XElement(Node)
                {
                    Value = NodeValue
                };
                configFile.Root.Add(childNode);
            }
            SaveConfigFile(MainConfig, configFile, DEFAULT_LOAD_TIMEOUT);
        }

        /// <summary>
        /// Check if Config Exists
        /// </summary>
        /// <param name="Node">Specific config node</param>
        /// <param name="mainConfig">File Path of Config File</param>
        /// <returns></returns>
        public static bool ConfigExists(string Node, string mainConfig)
        {
            bool nodeExists = false;
            if (File.Exists(mainConfig))
            {
                var configFile = LoadConfigFile(mainConfig, DEFAULT_LOAD_TIMEOUT);
                var item = configFile.Root.Element(Node);
                if (item != null)
                {
                    nodeExists = true;
                }
            }
            return nodeExists;
        }

        /// <summary>
        /// Loads config file
        /// </summary>
        /// <param name="path">Path of Config File</param>
        /// <param name="timeout">Retry Limit</param>
        /// <returns></returns>
        private static XDocument LoadConfigFile(string path, int timeout)
        {
            XDocument ret = null;
            for (int i = 1; i < timeout; i++)
            {
                try
                {
                    ret = XDocument.Load(path);
                    break;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    if (e.Message.Equals("Root element is missing."))
                    {
                        ReCreateConfigFile(path);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Create a clean copy of config file
        /// </summary>
        private static void ReCreateConfigFile(string mainConfig)
        {
            if (File.Exists(mainConfig))
            {
                File.Delete(mainConfig);
            }
            CreateConfigFile(mainConfig);
        }

        #endregion

    }
}
