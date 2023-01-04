using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace SchedulingAgent
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (Process.GetProcessesByName("SchedulingAgent").Length <= 1)
                {
                    bool isShareProducts = !(args != null && args.Any() && args[0] == "false");
                    Server.Start(isShareProducts);
                }
                else
                {
                    DlkLogger.LogInfo("An existing Scheduling Agent already exists. Cannot run multiple agents in one machine.");
                }
            }
            catch (Exception e)
            {                                                                               
                DlkTestRunnerCmdLib.LogException(e);
            }
        }
    }

    public static class Server
    {
        #region DECLARATIONS
        private const string StatusReady = "ready";
        private const int ArgumentsCount = 12;

        private static FileCopy FileToCopy;
        private static string Status = StatusReady;
        private static string ServerName = string.Empty;
        private static string SuiteUid = string.Empty;
        private static string SuitePath = string.Empty;

        private static Thread executionThread;
        private static Machine thisMachine;
        #endregion

        #region METHODS

        /// <summary>
        /// Start the UdpClient that listens to commands
        /// </summary>
        public static void Start(bool isShareProducts)
        {
            //share product folders
            if (isShareProducts)
            {
                ShareProductsFolder();
            }

            //initialize thread
            executionThread = new Thread(() => { });
            //initialize machine
            thisMachine = new Machine();
            thisMachine.MonitorMemoryUsage();

            //Start server
            IPEndPoint ipEndPoint = null;
            UdpClient udpClient = new UdpClient(2056);
            Console.WriteLine("SchedulingAgent Server started. Servicing on port 2056");

            while (true)
            {
                try
                {
                    // receive command
                    string command = Encoding.ASCII.GetString(udpClient.Receive(ref ipEndPoint));

                    Console.WriteLine("Command: " + command);
                    string returnString = RunCommand(command);
                    Console.WriteLine("Response: " + returnString);

                    //return message to sender
                    byte[] returnBytes = Encoding.ASCII.GetBytes(returnString);
                    udpClient.Send(returnBytes, returnBytes.Length, ipEndPoint);
                }
                catch (Exception e)
                {
                    DlkTestRunnerCmdLib.LogException(e);
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// process the command from controller and produce the return string
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private static string RunCommand(string command)
        {
            string returnString = " ";

            // Perform action based on command
            if (command == "alive")
            {
                returnString = "true";
            }
            else if (command == "status")
            {
                returnString = Status;

                //when warning and filetocopy is not null, it means it errored while trying to copy
                if (FileToCopy != null && Status.StartsWith("Warning"))
                {
                    try
                    {
                        CopyFiles();
                        Status = StatusReady;
                    }
                    catch (Exception ex)
                    {
                        Status = "Warning: " + ex.Message + " Check controller machine for possible network or sharing issues.";
                        //DlkTestRunnerCmdLib.LogException(ex);
                    }
                }

                returnString += string.Format("|{0}|{1}|{2}|{3}|{4}|{5}", thisMachine.RunTime.ToString(@"dd\.hh\:mm\:ss"), thisMachine.OperatingSystem,
                    thisMachine.FreeSpace, thisMachine.TotalMemory, thisMachine.PeakMemoryAndDate, ServerName);
            }
            else if (command.StartsWith("execute"))
            {
                returnString = Status == StatusReady ?
                                ExecuteCommand(new DlkTestRunnerCmdLibExecutionArgs(command)) :
                                "Error|Cannot execute at this time.";
            }
            else if (command.StartsWith("stop"))
            {
                if (executionThread.IsAlive)
                {
                    //executionThread.Abort();
                    DlkTestRunnerCmdLib.Stop();
                    returnString = "Cancelling";
                }
            }
            else if (command.StartsWith("getlatest"))
            {
                if (!executionThread.IsAlive)
                {
                    Process p = new Process();
                    p.StartInfo.FileName = "SchedulerGetLatest.bat";
                    p.StartInfo.Arguments = command.Split('|')[1];
                    p.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    p.Start();

                    returnString = "getting latest";
                }
                else
                {
                    Status = "ERROR|Get Latest failed.";
                }
            }
            else
            {
                returnString = "ERROR|Unsupported command: " + command;
            }

            return returnString;
        }

        /// <summary>
        /// Setup execute command and call executetest 
        /// </summary>
        /// <param name="executeArgs"></param>
        /// <returns>return state of execute</returns>
        private static string ExecuteCommand(DlkTestRunnerCmdLibExecutionArgs executeArgs)
        {
            string returnString = string.Empty;
            if (executeArgs.ArgumentsCount >= ArgumentsCount)
            {
                SuiteUid = executeArgs.ScheduleId;
                ServerName = executeArgs.MachineName;

                //modify SuitePath for current directory structure
                string abPath = executeArgs.FilePath;
                string curPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                abPath = abPath.Substring(abPath.LastIndexOf("BrowserFramework\\Products"));
                curPath = curPath.Substring(0, curPath.LastIndexOf("BrowserFramework"));
                SuitePath = curPath + abPath; //absolute path of suite
                executeArgs.FilePath = SuitePath;

                //execute the test
                ExecuteTest(executeArgs);
                returnString = "executing";
            }
            else
            {
                returnString = "ERROR|Invalid arguments for execute.";
            }

            return returnString;
        }

        /// <summary>
        /// Get tfs latest files
        /// </summary>
        private static void GetLatestProductFolder()
        {
            string projectFolderName = DlkTestRunnerCmdLib.GetProductFolder(SuitePath);
            string pathToSync = SuitePath.Substring(0, SuitePath.IndexOf(projectFolderName + "\\Suites") + projectFolderName.Length);

            DlkSourceControlHandler.GetFiles(pathToSync, "/recursive /overwrite");
        }

        /// <summary>
        /// Get the root path of testrunner framework
        /// </summary>
        /// <returns></returns>
        private static string GetRootPath()
        {
            var binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rootPath = Directory.GetParent(binDir).FullName;
            while (new DirectoryInfo(rootPath).GetDirectories()
                .Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                rootPath = Directory.GetParent(rootPath).FullName;
            }
            return rootPath;
        }

        /// <summary>
        /// Executes the test in a bgworker
        /// </summary>
        /// <param name="arguments"></param>
        private static void ExecuteTest(DlkTestRunnerCmdLibExecutionArgs arguments)
        {
            if (!executionThread.IsAlive)
            {
                executionThread = new Thread(() => { ExecutionThread_DoWork(arguments); });
                executionThread.Start();
            }         
        }

        /// <summary>
        /// Copy result files to server/controller computer
        /// </summary>
        private static void CopyResultFiles()
        {
            string suiteName = Path.GetFileNameWithoutExtension(SuitePath);
            string projectFolderName = DlkTestRunnerCmdLib.GetProductFolder(SuitePath);

            //GET SOURCE FOLDER - LOCAL PATH
            string sourceFilePath = SuitePath.Substring(0, SuitePath.IndexOf(projectFolderName + "\\Suites") + projectFolderName.Length + 1)
                + "Framework\\SuiteResults\\" + suiteName;
            var folders = new DirectoryInfo(sourceFilePath).GetDirectories();
            var sourceFolder = folders.First(y => y.CreationTime == folders.Max(x => x.CreationTime));

            //SETUP TARGET - NETWORK PATH
            //check if we need to create folders on network path for files to copy to
            var targetPath = string.Format("\\\\{0}\\BrowserFramework\\{1}\\Framework\\SuiteResults\\{2}\\{3}", 
                ServerName, projectFolderName, suiteName, sourceFolder.Name);

            //COPY
            //get files to copy from source folder
            string[] files = Directory.GetFiles(sourceFolder.FullName);
            //create a copy of the file in case copy fails
            FileToCopy = new FileCopy() { Files = files, TargetPath = targetPath };
            CopyFiles();

            Console.WriteLine("Info: Results copy completed.");
        }

        /// <summary>
        /// Try to copy over files that failed to copy previously
        /// </summary>
        private static void CopyFiles()
        {
            // Get a unique folder name if it exist
            int counter = 1;
            while (Directory.Exists(FileToCopy.TargetPath))
            {
                FileToCopy.TargetPath = FileToCopy.TargetPath + "(" + counter++ + ")";
            }
            DirectoryInfo di = Directory.CreateDirectory(FileToCopy.TargetPath);

            // Copy the files to the newly created folder.
            foreach (string s in FileToCopy.Files)
            {
                // Use static Path methods to extract only the file name from the path.
                var fileName = Path.GetFileName(s);
                var destFile = Path.Combine(FileToCopy.TargetPath, fileName);
                File.Copy(s, destFile, true);
            }

            FileToCopy = null;
        }

        /// <summary>
        /// reset variables
        /// </summary>
        private static void ResetValues()
        {
            SuiteUid = string.Empty;
            ServerName = string.Empty;
            SuitePath = string.Empty;
            Status = StatusReady;
        }

        /// <summary>
        /// Share product folders - for controller to see testresults image
        /// </summary>
        private static void ShareProductsFolder()
        {
            try
            {
                string productPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                productPath = productPath.Substring(0, productPath.LastIndexOf("BrowserFramework") + ("BrowserFramework").Length) + "\\Products";

                Process p = new Process();
                p.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SharedFolderFull.bat");
                p.StartInfo.Arguments = productPath;
                p.StartInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.Verb = "runas"; //request admin rights
                p.Start();
                p.WaitForExit(300000);
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                DlkTestRunnerCmdLib.LogException(ex);
            }
        }

        #endregion

        #region THREADS

        /// <summary>
        /// bgDowork executing the test
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ExecutionThread_DoWork(DlkTestRunnerCmdLibExecutionArgs arguments)
        {
            //set status to suite Id
            Status = SuiteUid;

            try
            {
                //get latest product folder
#if DEBUG
                GetLatestProductFolder();
                Console.WriteLine("Info: Product folder synced.");
#endif
                //agents do not need to include default emails from testrunner settings file. this will be included by controller.
                arguments.ConsiderDefaultEmail = false;
                //run test
                DlkTestRunnerCmdLib.Run(arguments);

                //reset flags
                DlkTestRunnerCmdLib.ResetFlags();

                //copy files
                try
                {
                    //copy results here
                    CopyResultFiles();
                    ResetValues();

                    Console.WriteLine("Info: Execution Complete.");
                }
                catch (Exception ex)
                {
                    Status = "Warning: " + ex.Message + " Check controller machine for possible network or sharing issues.";
                    DlkTestRunnerCmdLib.LogException(ex);
                }
            }
            catch (Exception ex)
            {
                ResetValues();
                Status = "Error: " + ex.Message;
                DlkTestRunnerCmdLib.LogException(ex);
            }
        }
    }

        #endregion


    public class FileCopy
    {
        public string[] Files;
        public string TargetPath;
    }
}
