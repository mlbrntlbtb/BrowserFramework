using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TRDiagnosticsCore;
using TRDiagnosticsCore.Tests;
using TRDiagnosticsCore.Utility;

namespace TRDiagnostics
{
    class Program
    {
        private static Mutex myMutex;
        private static bool IsNewInstance = false;
        public static string TRPath;
        public static string DirRunArg;
        public static List<string> testRunnerPaths;

        static void Main(string[] args)
        {
            DisplayHeader();

            //Only one instance should run at any given time to prevent multiple diagnostic checks 
            if (CheckInstance())
            {
                Console.WriteLine("The system detected another instance of Test Runner Diagnostics is running.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            string optionArg;
            string pathArg;

            if (args.Count() == 3)
            {
                optionArg = args[0].ToString();
                DirRunArg = args[1].ToString();
                pathArg = args[2].ToString();
                DiagnosticTestOptions(optionArg, pathArg);
            }
            else if (args.Count() == 2)
            {
                optionArg = args[0].ToString();
                pathArg = args[1].ToString();
                DirRunArg = "0";
                DiagnosticTestOptions(optionArg, pathArg);
            }
            else if (args.Count() == 1)
            {
                optionArg = args.FirstOrDefault();
                DiagnosticTestOptions(optionArg);
            }
            else
            {
                //Invalid diagnostic test option
                DisplayInvalidArgument();
                DisplayHelp();
                return;
            }
        }

        /// <summary>
        /// Handles test options for specific diagnostic test
        /// </summary>
        private static void DiagnosticTestOptions(string optionArg = "", string pathArg = "")
        {
            try
            {
                //Diagnostic test option
                switch (optionArg.ToLower().Trim())
                {
                    case "/?":
                    case "/h":
                    case "-h":
                    case "help":
                        //Help function
                        if(!String.IsNullOrEmpty(pathArg))
                            DisplayInvalidArgument();

                        DisplayHelp();
                        break;
                    case "/f":
                    case "-f":
                    case "full":
                        //Check test runner path input
                        TRPath = CheckTestRunnerInput(pathArg);

                        //Selects full diagnostic test
                        if (!String.IsNullOrEmpty(TRPath))
                            RunFullDiagnosticTest();
                        else
                            DisplayProvidePath();
                        break;
                    case "/b":
                    case "-b":
                    case "browser":
                        //Check test runner path input
                        TRPath = CheckTestRunnerInput(pathArg);

                        //Selects browser diagnostic test
                        if (!String.IsNullOrEmpty(TRPath))
                            RunBrowserTest();
                        else
                            DisplayProvidePath();
                        break;
                    case "/d":
                    case "-d":
                    case "dir":
                        //Check test runner path input
                        TRPath = CheckTestRunnerInput(pathArg);

                        //Selects core files diagnostic test
                        if (!String.IsNullOrEmpty(TRPath))
                            RunCoreFileTest();
                        else
                            DisplayProvidePath();
                        break;
                    case "/sf":
                    case "-sf":
                    case "sharing":
                        //Check test runner path input
                        TRPath = CheckTestRunnerInput(pathArg);

                        //Selects shared folders diagnostic test
                        if (!String.IsNullOrEmpty(TRPath))
                            RunShareFolderTest();
                        else
                            DisplayProvidePath();
                        break;
                    case "/sys":
                    case "-sys":
                    case "system":
                        //Check test runner path input
                        TRPath = CheckTestRunnerInput(pathArg);

                        //Selects system requirements diagnostic test
                        if (!String.IsNullOrEmpty(TRPath))
                            RunSystemReqTest();
                        else
                            DisplayProvidePath();
                        break;
                    case "/sp":
                    case "-sp":
                    case "searchpath":
                        //Searchs for available valid test runner paths
                        if (!String.IsNullOrEmpty(pathArg))
                        {
                            DisplayInvalidArgument();
                            DisplayHelp();
                        }
                        else
                            SearchTestRunnerPaths();
                        break;
                    default:
                        //Invalid diagnostic test option
                        DisplayInvalidArgument();
                        DisplayHelp();
                        break;
                }
            }
            catch(Exception e)
            {
                //Run test selection again when error is encountered
                SaveErrorLogs(e.ToString(), "Something went wrong while selecting diagnostic test.");
            }
        }

        /// <summary>
        /// Runs browser test instance
        /// </summary>
        private static void RunBrowserTest()
        {
            //Run browser test instance
            BrowserTest browserTest = new BrowserTest();
            browserTest.Run(TRPath, true);
            //Save logs after running browser test
            SaveLogs(browserTest.DiagnosticLogger.Logs, browserTest.Recommendations, browserTest.TestName, false);
            GenerateHTMLReport(browserTest.DiagnosticLogger.Logs, browserTest.Recommendations, browserTest.TestName);
        }

        /// <summary>
        /// Runs core files test instance
        /// </summary>
        private static void RunCoreFileTest()
        {
            //Run core files test instance
            CoreFilesTest coreFileTest = new CoreFilesTest();
            coreFileTest.Run(TRPath, true, DirRunArg);
            //Save logs after running core file test
            SaveLogs(coreFileTest.DiagnosticLogger.Logs, coreFileTest.Recommendations, coreFileTest.TestName,false);
            GenerateHTMLReport(coreFileTest.DiagnosticLogger.Logs, coreFileTest.Recommendations, coreFileTest.TestName);
        }

        /// <summary>
        /// Runs shared folders test instance
        /// </summary>
        private static void RunShareFolderTest()
        {
            //Run shared folder test instance
            ShareFolderTest shareFolderTest = new ShareFolderTest();
            shareFolderTest.Run(TRPath, true);
            //Save logs after running shared folder test
            SaveLogs(shareFolderTest.DiagnosticLogger.Logs, shareFolderTest.Recommendations, shareFolderTest.TestName, false);
            GenerateHTMLReport(shareFolderTest.DiagnosticLogger.Logs, shareFolderTest.Recommendations, shareFolderTest.TestName);
        }

        /// <summary>
        /// Runs system requirement test instance
        /// </summary>
        private static void RunSystemReqTest()
        {
            //Run system requirement test instance
            SystemReqTest systemReqTest = new SystemReqTest();
            systemReqTest.Run(TRPath, true);
            //Save logs after running system requirement test
            SaveLogs(systemReqTest.DiagnosticLogger.Logs, systemReqTest.Recommendations, systemReqTest.TestName, false);
            GenerateHTMLReport(systemReqTest.DiagnosticLogger.Logs, systemReqTest.Recommendations, systemReqTest.TestName);
        }

        /// <summary>
        /// Runs full diagnostic test instance
        /// </summary>
        private static void RunFullDiagnosticTest()
        {
            //Run full diagnostic test instance
            SystemReqTest systemReqTest = new SystemReqTest();
            CoreFilesTest coreFileTest = new CoreFilesTest();
            BrowserTest browserTest = new BrowserTest();
            ShareFolderTest shareFolderTest = new ShareFolderTest();

            systemReqTest.Run(TRPath, true);
            coreFileTest.Run(TRPath, true, DirRunArg);
            browserTest.Run(TRPath, true);
            shareFolderTest.Run(TRPath, true);

            List<TestRecord> testRecord = new List<TestRecord>();

            // System Rquirements Test
            testRecord.Add(new TestRecord
            {
                TestName = systemReqTest.TestName,
                Logs = systemReqTest.DiagnosticLogger.Logs,
                Reco = systemReqTest.Recommendations
            });

            // Core File Test
            testRecord.Add(new TestRecord
            {
                TestName = coreFileTest.TestName,
                Logs = coreFileTest.DiagnosticLogger.Logs,
                Reco = coreFileTest.Recommendations
            });

            // Browser Test
            testRecord.Add(new TestRecord
            {
                TestName = browserTest.TestName,
                Logs = browserTest.DiagnosticLogger.Logs,
                Reco = browserTest.Recommendations
            });

            // Shared Folder Test
            testRecord.Add(new TestRecord
            {
                TestName = shareFolderTest.TestName,
                Logs = shareFolderTest.DiagnosticLogger.Logs,
                Reco = shareFolderTest.Recommendations
            });

            //Save logs after running browser test
            testRecord.ForEach(ftr =>
            {
                bool appendLogs = false;
                if (testRecord.First() != ftr) appendLogs = true;
                SaveLogs(ftr.Logs, ftr.Reco, ftr.TestName, appendLogs);
            });
            GenerateHTMLReport(testRecord);
        }

        /// <summary>
        /// Displays the header when the console is launched for the first time
        /// </summary>
        private static void DisplayHeader()
        {
            ConsolePrinter.PrintDiagnosticHeader();
        }

        /// <summary>
        /// Displays the help info
        /// </summary>
        private static void DisplayHelp()
        {
            ConsolePrinter.PrintHeader();
            ConsolePrinter.PrintLine("Description:");
            ConsolePrinter.PrintLine("\t This tool will allow you to perform simple diagnostic checks on Deltek Test Runner. To perform");
            ConsolePrinter.PrintLine("a diagnostic check, please specify the diagnostic check and full path to your Test Runner location.");

            ConsolePrinter.PrintLine();
            ConsolePrinter.PrintLine();
            ConsolePrinter.PrintLine("Command \t\t Details");
            ConsolePrinter.PrintLine("help, /h, /?, -h \t Display information for all available commands. \n");
            ConsolePrinter.PrintLine("searchpath, /sp, -sp \t Displays all the valid Test Runner paths in your machine. You may use this command if you");
            ConsolePrinter.PrintLine("\t\t\t are unsure of the full path of your Test Runner location. The search may take up to 5 minutes");
            ConsolePrinter.PrintLine("\t\t\t or more depending on the size of the disk. \n");
            ConsolePrinter.PrintLine("full, /f, -f \t\t Run all diagnostic tests. \n");
            ConsolePrinter.PrintLine("browser, /b, -b \t Run checks on installed browsers and identify compatibility with installed webdrivers. \n");
            ConsolePrinter.PrintLine("dir, /d, -d \t\t Run checks on important files and directories required by Test Runner. The check may take up");
            ConsolePrinter.PrintLine("\t\t\t to 5 minutes or more depending on the size of the directory.\n");
            ConsolePrinter.PrintLine("\t\t\t It has an optional parameter to exclude folders to check. \n\t\t\t 1 - Test Script, 2 - Test Script Result, 3 - Test Suite and 4 - Test Suite Result \n ");
            ConsolePrinter.PrintLine("sharing, /sf, -sf \t Run checks on shared folder settings to allow correct email links. \n");
            ConsolePrinter.PrintLine("system, /sys, -sys \t Run checks on the machine if it meets the minimum system requirements defined in the Test");
            ConsolePrinter.PrintLine("\t\t\t Runner User Guide.");

            ConsolePrinter.PrintLine();
            ConsolePrinter.PrintLine("Examples:");
            ConsolePrinter.PrintLine("TRDiagnostics help");
            ConsolePrinter.PrintLine("TRDiagnostics searchpath");
            ConsolePrinter.PrintLine("TRDiagnostics full \"C:/Deltek/TestRunner/BrowserFramework/Tools/TestRunner\"");
            ConsolePrinter.PrintLine("TRDiagnostics /b \"C:/Deltek/TestRunner/BrowserFramework/Tools/TestRunner\"");
            ConsolePrinter.PrintLine("TRDiagnostics -d 1234 \"C:/Deltek/TestRunner/BrowserFramework/Tools/TestRunner\"");
            ConsolePrinter.PrintLine("TRDiagnostics sharing \"C:/Deltek/TestRunner/BrowserFramework/Tools/TestRunner\"");
            ConsolePrinter.PrintLine("TRDiagnostics /sys \"C:/Deltek/TestRunner/BrowserFramework/Tools/TestRunner\"");
            ConsolePrinter.PrintHeader();
        }

        /// <summary>
        /// Displays the invalid argument message
        /// </summary>
        private static void DisplayInvalidArgument()
        {
            ConsolePrinter.PrintLine("Invalid argument. Please see the help guide below for the list of available options:");
        }

        /// <summary>
        /// Calls save diagnostic logs function then prints ouput to Console
        /// </summary>
        /// <returns></returns>
        private static void SaveLogs(List<LogRecord> logs, List<RecommendedItem> recommendations, string testName, bool appendLogs)
        {
            try
            {
                bool isLogsAppended;
                string saveLogsPath = "";
                SaveLogsHandler.SaveLogs(TRPath, logs, recommendations, testName, appendLogs, out saveLogsPath, out isLogsAppended);
                if (!isLogsAppended)
                {
                    ConsolePrinter.PrintLine("\nDiagnostic test completed.", "Diagnostic test logs saved at: <" + saveLogsPath + ">");
                    ConsolePrinter.PrintHeader();
                }
            }
            catch(Exception e)
            {
                string errorMessage = "\nSomething went wrong while saving diagnostic logs. Diagnostics logs not saved.";
                ConsolePrinter.PrintLine(errorMessage);
                SaveErrorLogs(e.ToString(), errorMessage);
            }
        }

        /// <summary>
        /// Generates Html Report 
        /// </summary>
        /// <returns></returns>
        private static void GenerateHTMLReport(List<TestRecord> testRecords)
        {
            string FileOutput = string.Empty;
            try
            {
                FileOutput = SaveLogsHandler.GetLogFilePath(TRPath, true);
                SaveLogsHandler.GenerateHTMLReport(FileOutput, testRecords);
                ConsolePrinter.PrintLine($"Diagnostic test report saved at: <{FileOutput}>");
                ConsolePrinter.PrintHeader();
            }
            catch (DirectoryNotFoundException dne)
            {
                string errorMessage = $"\nDirectory not found ({FileOutput}). Diagnostics report not saved.";
                ConsolePrinter.PrintLine(errorMessage);
                SaveErrorLogs(dne.ToString(), errorMessage);
            }
            catch (UnauthorizedAccessException uae)
            {
                string errorMessage = $"\nUnauthorized Access on ({FileOutput}). Diagnostics report not saved.";
                ConsolePrinter.PrintLine(errorMessage);
                SaveErrorLogs(uae.ToString(), errorMessage);
            }
            catch (Exception e)
            {
                string errorMessage = "\nSomething went wrong while generating diagnostic report. Diagnostics report not saved.";
                ConsolePrinter.PrintLine(errorMessage);
                SaveErrorLogs(e.ToString(), errorMessage);
            }
        }
        private static void GenerateHTMLReport(List<LogRecord> logs, List<RecommendedItem> recommendations, string testName)
        {
            var testRecords = new List<TestRecord>();
            testRecords.Add((new TestRecord
            {
                TestName = testName,
                Logs = logs,
                Reco = recommendations
            }));
            GenerateHTMLReport(testRecords);
        }

        /// <summary>
        /// Calls save error logs function then prints ouput to Console
        /// </summary>
        /// <returns></returns>
        private static void SaveErrorLogs(string errorException, string errorMessage)
        {
            bool isErrorSaved = false;
            string errorLogsPath = "";
            isErrorSaved = SaveLogsHandler.SaveErrorLogs(TRPath, errorException, errorMessage, out errorLogsPath);
            if (isErrorSaved)
            {
                ConsolePrinter.PrintHeader();
                ConsolePrinter.PrintLine("\n" + errorMessage + " See error logs for details", "Error logs saved at: <" + errorLogsPath + ">");
            }
            else
            {
                ConsolePrinter.PrintLine("\n" + errorMessage);
            }
        }

        /// <summary>
        /// Checks for existing instance
        /// </summary>
        /// <returns></returns>
        private static bool CheckInstance()
        {
            myMutex = new Mutex(true, "TRDiagnostics", out IsNewInstance);
            if (!IsNewInstance)
            {
                IsNewInstance = true;
            }
            else
            {
                IsNewInstance = false;
            }
            return IsNewInstance;
        }

        /// <summary>
        /// Displays all valid test runner test directories
        /// </summary>
        private static void SearchTestRunnerPaths()
        {
            try
            {
                string selectedDrive = "";
                //Check for multiple drives and selection prompt
                selectedDrive = SelectDrive();

                if (String.IsNullOrEmpty(selectedDrive))
                    return;

                //Retrieve available browser framework paths
                testRunnerPaths = new List<string>();
                testRunnerPaths = RootPathHandler.GetBrowserFrameworkPath(selectedDrive, true);

                if (testRunnerPaths.Count == 0)
                {
                    //No valid test runner directory found
                    ConsolePrinter.PrintLine("No valid Test Runner directory installed in Drive [" + selectedDrive + "].");
                    Environment.Exit(0);
                }
                else if (testRunnerPaths.Count > 0)
                {
                    //Select prompt for multiple browser framework directories
                    ConsolePrinter.PrintLine("Valid Test Runner directories in Drive [" + selectedDrive + "]:");

                    //Display all available browser framework directories
                    for (int p = 1; p <= testRunnerPaths.Count; p++)
                    {
                        ConsolePrinter.PrintLine("[" + p + "] <" + testRunnerPaths[p - 1].ToString() + ">");
                    }
                    ConsolePrinter.PrintHeader();
                    Environment.Exit(0);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                ConsolePrinter.PrintLine("\nAccess to selected drive is denied.");
            }
            catch (Exception e)
            {
                SaveErrorLogs(e.ToString(), "\nSomething went wrong while searching for valid Test Runner directories.");
            }
        }

        /// <summary>
        /// Selection for drive(s) available
        /// </summary>
        private static string SelectDrive()
        {
            //Retrieve all available drives
            string driveSelected = "";
            var availableDrives = DriveInfo.GetDrives();
            if (availableDrives.Count() > 1)
            {
                //Select prompt for multiple drives detected
                ConsolePrinter.PrintLine("Multiple drives detected. Press the number of the drive you wish to use:");

                //Displays all available drives
                for (int d = 1; d <= availableDrives.Count(); d++)
                {
                    ConsolePrinter.PrintLine("[" + d + "] <" + availableDrives[d - 1].ToString() + ">");
                }

                //User selects target drive
                int driveIndex = 0;
                if (!int.TryParse(Console.ReadLine(), out driveIndex) || driveIndex > availableDrives.Count() || driveIndex == 0)
                {
                    ConsolePrinter.PrintHeader();
                    ConsolePrinter.PrintLine("Invalid option selected. Try again.");
                    Environment.Exit(0);
                }
                else
                {
                    var selectedDrive = availableDrives[driveIndex - 1];
                    if(selectedDrive.DriveType == DriveType.CDRom)
                    {
                        ConsolePrinter.PrintLine("Selected drive is an optical disc drive [CD/DVD-ROM]. Access denied.");
                        Environment.Exit(0);
                    }
                    else if (selectedDrive.DriveType == DriveType.Removable)
                    {
                        ConsolePrinter.PrintLine("Selected drive is a removable storage device. Access denied.");
                        Environment.Exit(0);
                    }
                    else
                    {
                        driveSelected = selectedDrive.ToString();
                    }
                }

                ConsolePrinter.PrintLine("Drive: <" + driveSelected + "> selected.");
                ConsolePrinter.PrintHeader();
            }
            else
            {
                //Single instance of drive found
                driveSelected = availableDrives.FirstOrDefault().ToString();
            }
            return driveSelected;
        }

        /// <summary>
        /// Checks test runner directory path if valid
        /// </summary>
        private static string CheckTestRunnerInput(string testRunnerPath)
        {
            if (testRunnerPath.Equals("."))
            {
                testRunnerPath = Directory.GetCurrentDirectory();
            }

            if (File.Exists(Path.Combine(testRunnerPath, "TestRunner.exe")))
            {
                ConsolePrinter.PrintLine("Directory selected: <" + testRunnerPath + ">");
            }
            else
            {
                ConsolePrinter.PrintLine("You have entered an invalid Test Runner directory.");
                Environment.Exit(0);
            }
            return testRunnerPath;
        }

        /// <summary>
        /// Display provide test runner path and help
        /// </summary>
        private static void DisplayProvidePath()
        {
            ConsolePrinter.PrintLine("Please provide a valid Test Runner directory. See help guide below:");
            DisplayHelp();
        }
    }
}
