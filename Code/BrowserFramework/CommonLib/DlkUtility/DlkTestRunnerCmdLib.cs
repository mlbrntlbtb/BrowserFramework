using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLib.DlkUtility
{
    public static class DlkTestRunnerCmdLib
    {
        private const int INT_ARG_IDX_FILEPATH = 0;
        private const int INT_ARG_IDX_LIBRARY = 1;
        private const int INT_ARG_IDX_PRODUCT = 2;
        private const int INT_ARG_IDX_EMAIL = 3;
        private const int INT_ARG_IDX_RUNNUMBER = 4;
        private const int INT_ARG_IDX_STARTEMAIL = 5;
        private const int INT_ARG_IDX_PRESCRIPT = 6;
        private const int INT_ARG_IDX_POSTSCRIPT = 7;
        private const int INT_ARG_IDX_RUNONLYFAILED = 8;

        static string RootPath;

        public static bool DoNotKillDriversOnTearDown
        {
            set
            {
                DlkEnvironment.mDoNotKillDriversOnTearDown = value;
            }
        }


        #region methods
        public static bool Run(DlkTestRunnerCmdLibExecutionArgs args)
        {
            bool displayHelp = true;

            var filePath = args.FilePath;
            //validations - fail if suite do not exist
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Suite path not found.");
            }

            //initialize variables
            RootPath = GetRootPath();

            //Find the product folder based on path of xml
            var productFolder = GetProductFolder(filePath);

            bool hasInvalidEmail;
            var validatedEmail = GetValidEmails(args.EmailDistro, out hasInvalidEmail);

            if (!string.IsNullOrEmpty(args.Library) && !string.IsNullOrEmpty(args.Product) && !hasInvalidEmail)
            {
                //Find library in current folder
                var libraryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), args.Library);
                //initialize environment needed to run the test
                DlkEnvironment.InitializeEnvironment(productFolder, RootPath, libraryPath);

                /* Set current application based on product folder. TEMP only, Ideal to pass App ID to DlkTestRunnerCmdLib in args 
                and set it there. Some keywords rely on version of application stored on INIT, thats why we need this call */
                DlkTestRunnerSettingsHandler.Initialize(RootPath);
                DlkTestRunnerSettingsHandler.ApplicationUnderTest = DlkTestRunnerSettingsHandler.ApplicationList.Find(
                    x => x.ProductFolder == productFolder);

                //Run validations
                ValidateTests(filePath);

                //Setup for email notif
                List<DlkExternalScript> scripts = new List<DlkExternalScript>();
                scripts.AddRange(ConvertToExternalScripts(args.PreScript, DlkExternalScriptType.PreExecutionScript));
                scripts.AddRange(ConvertToExternalScripts(args.PostScript, DlkExternalScriptType.PostExecutionScript));

                DlkScheduleRecord sched = new DlkScheduleRecord(DayOfWeek.Monday, DateTime.Now, 1, validatedEmail, args.Library, args.Product, true, args.SendPreEmail, filePath, scripts);
                DlkTestRunnerApi.SetupEmailNotification(sched, args.ConsiderDefaultEmail);
                if (args.SendPreEmail)
                {
                    DlkTestRunnerApi.SetupPreEmailNotification(sched, args.ConsiderDefaultEmail);
                }

                DlkTestRunnerApi.RunPreScripts(sched, RootPath);

                //Setup overrides when requested 
                if (!string.IsNullOrEmpty(args.BrowserOverride))
                {
                    DlkTestRunnerApi.SetupOverrideBrowser(args.BrowserOverride);
                }
                if (args.LoginConfigOverride != null)
                {
                    DlkTestRunnerApi.SetupOverrideLoginConfig(args.LoginConfigOverride.mID, args.LoginConfigOverride.mUrl, args.LoginConfigOverride.mUser, args.LoginConfigOverride.mPassword, args.LoginConfigOverride.mDatabase,
                                                                args.LoginConfigOverride.mPin, args.LoginConfigOverride.mMetaData);
                }
                if (!string.IsNullOrEmpty(args.LoginConfigID))
                {
                    DlkLoginConfigRecord loginConfigOverride = DlkLoginConfigHandler.GetLoginConfigInfo(DlkEnvironment.mLoginConfigFile, args.LoginConfigID);
                    DlkTestRunnerApi.SetupOverrideLoginConfig(loginConfigOverride.mID, loginConfigOverride.mUrl, loginConfigOverride.mUser, loginConfigOverride.mPassword, loginConfigOverride.mDatabase,
                                                                loginConfigOverride.mPin, loginConfigOverride.mMetaData);
                }
                
                //Running the test suite
                RunTestSuite(filePath, args.ScheduledStart, args.RunNumber, args.NumberOfRuns, args.RerunFailedTestsOnly);

                DlkTestRunnerApi.RunPostScriptsWithoutShare(sched, RootPath);
                displayHelp = false;
            }

            if (hasInvalidEmail)
            {
                Console.WriteLine("Test Runner Cmd stopped. The supplied email argument has an invalid address.");
                displayHelp = false;
            }

            return displayHelp;
        }

        public static bool RunLegacySchedulerFile(string[] args)
        {
            bool displayHelp = true;
            DayOfWeek dayOfWeek;

            var filePath = args[0];
            var dayOfWeekArg = GetArg("/day", "/d");

            if (Enum.TryParse(dayOfWeekArg, true, out dayOfWeek))
            {
                //initialize file path
                DlkSchedulerFileHandler.mScheduleFile = filePath;
                //get list of schedule and run each test
                var scheduleList = DlkSchedulerFileHandler.GetListScheduledSuite(dayOfWeek);
                foreach (var schedule in scheduleList)
                {
                    RunSchedule(schedule);
                }
                displayHelp = false;
            }

            return displayHelp;
        }

        public static void Stop()
        {
            // Set abortion flag to true - cancel all test in suite run
            DlkTestRunnerApi.StopExecution();
        }

        public static void ResetFlags()
        {
            DlkTestRunnerApi.mAbortionPending = false;
            DlkTestRunnerApi.ResetConfigOverrides();
        }

        /// <summary>
        /// Log exception messages to a file on the current directory
        /// </summary>
        /// <param name="e">the exception details to log</param>
        public static void LogException(Exception e)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DlkString.GetDateAsText("file") + ".log");
            File.WriteAllLines(path, new string[] { DlkString.GetDateAsText("long") + "| " + "Test Scheduler encountered an unexpected error.",
                    DlkString.GetDateAsText("long") + "| " + e.Source, 
                    DlkString.GetDateAsText("long") + "| " + e.Message, 
                    DlkString.GetDateAsText("long") + "| " + e.StackTrace });
        }

        /// <summary>
        /// Get the product folder name based on absolute path
        /// </summary>
        /// <param name="absolutePath">the absolute path of the test suite</param>
        /// <returns></returns>
        public static string GetProductFolder(string absolutePath)
        {
            const string product = "Products\\";
            const string project = "BrowserFramework\\";
            string result = string.Empty;

            if (absolutePath.Contains(product))
            {
                //cut off all the parts before products
                result = absolutePath.Substring(absolutePath.IndexOf(product) + product.Length);
                //cut off all the parts after the product
                result = result.Remove(result.IndexOf("\\"));
            }
            else
            {
                result = absolutePath.Substring(absolutePath.IndexOf(project) + project.Length);
                //cut off all the parts after the product
                result = result.Remove(result.IndexOf("\\"));
            }
            return result;
        }

        /// <summary>
        /// Convert external scripts from string to DlkExternalScript
        /// </summary>
        /// <param name="scripts"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<DlkExternalScript> ConvertToExternalScripts(string scripts, DlkExternalScriptType type)
        {
            int idx = 0;
            List<DlkExternalScript> ExternalSctipts = new List<DlkExternalScript>();
            foreach (string s in scripts.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] attributes = s.Split(new string[] { "<" }, StringSplitOptions.None);
                ExternalSctipts.Add(new DlkExternalScript(idx, type)
                {
                    Path = attributes[0],
                    Arguments = attributes[1],
                    StartIn = attributes[2],
                    WaitToFinish = bool.Parse(attributes[3])
                });
                idx++;
            }
            return ExternalSctipts;
        }

        /// <summary>
        /// basic suite validations for error handling - throw exception to stop test from continuing
        /// </summary>
        /// <returns></returns>
        private static void ValidateTests(string suitePath)
        {
            //validations - fail if all test is missing
            List<DlkExecutionQueueRecord> input = DlkTestSuiteXmlHandler.Load(suitePath);
            bool testExist = false;
            foreach (DlkExecutionQueueRecord record in input)
            {
                string file = Path.Combine(record.folder.Trim('\\'), record.file);
                if (File.Exists(Path.Combine(DlkEnvironment.mDirTests, file)))
                {
                    testExist = true;
                    break;
                }
            }
            if (!testExist)
            {
                throw new ArgumentException("All tests in suite cannot be found.");
            }
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
        /// Method used to get one of the argument supplied. First parameter have precedence over the second.
        /// </summary>
        /// <param name="arg">First argument string</param>
        /// <param name="alternativeArg">Second argument string</param>
        /// <returns></returns>
        public static string GetArg(string arg, string alternativeArg)
        {
            return !string.IsNullOrEmpty(DlkArgs.GetArg(arg)) ? DlkArgs.GetArg(arg) : DlkArgs.GetArg(alternativeArg);
        }

        /// <summary>
        /// Sanitize the email string and return only valid emails
        /// </summary>
        /// <param name="rawEmailString">email string of emails seperated by semicolon</param>
        /// <param name="hasInvalidEmail">output boolean that notifies if there is an invalid email</param>
        /// <returns></returns>
        private static string GetValidEmails(string rawEmailString, out bool hasInvalidEmail)
        {
            String validEmails = string.Empty;
            hasInvalidEmail = false;

            foreach (string email in rawEmailString.Split(';'))
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (DlkString.IsValidEmail(email))
                    {
                        validEmails += email + ";";
                    }
                    else
                    {
                        hasInvalidEmail = true;
                    }
                }
            }

            return validEmails;
        }

        /// <summary>
        /// Run the schedule file with all the necessary steps (email and scripts)
        /// </summary>
        /// <param name="schedule"></param>
        private static void RunSchedule(DlkScheduleRecord schedule)
        {
            string mProductFolder = schedule.ProductFolder; //****
            string mLibrary = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), schedule.Library);

            //pre-run processes
            if (schedule.SendEmailOnExecutionStart)
            {
                DlkTestRunnerApi.SetupPreEmailNotification(schedule);
            }
            Console.WriteLine("Running pre-execution scripts...");
            DlkTestRunnerApi.RunPreScripts(schedule, RootPath);
            Console.WriteLine("Running pre-execution scripts...DONE");
            //initialize
            DlkEnvironment.InitializeEnvironment(mProductFolder, RootPath, mLibrary);
            DlkTestRunnerApi.SetupEmailNotification(schedule);
            //run test
            RunTestSuite(Path.Combine(DlkEnvironment.mDirProductsRoot, schedule.TestSuiteRelativePath), scheduledStart: DateTime.Now.ToString(CultureInfo.InvariantCulture),
                runNumber: 1, numberOfRuns: 1, runOnlyFailedTests: false);
            //post-run processes
            Console.WriteLine("Running post-execution scripts...");
            DlkTestRunnerApi.RunPostScripts(schedule, RootPath);
            Console.WriteLine("Running post-execution scripts...DONE");
            DeleteNotificationFile(DlkEnvironment.mDirTestSuite);
        }

        /// <summary>
        /// Execute the test suite
        /// </summary>
        /// <param name="absolutePath"></param>
        private static void RunTestSuite(string absolutePath, string scheduledStart, int runNumber, int numberOfRuns, bool runOnlyFailedTests)
        {
            Console.Write("Running test suite...");
            DlkTestRunnerApi.ExecuteScheduledTest(absolutePath, scheduledStart, runNumber, numberOfRuns, runOnlyFailedTests);
            Console.WriteLine("DONE");
        }

        /// <summary>
        /// Delete notification file if it exist
        /// </summary>
        /// <param name="testPath"></param>
        private static void DeleteNotificationFile(string testPath)
        {
            if (File.Exists(Path.Combine(testPath, "TestSuiteNotification.html")))
            {
                File.Delete((Path.Combine(testPath, "TestSuiteNotification.html")));
            }
        }
        #endregion
    }

    public class DlkTestRunnerCmdLibExecutionArgs
    {
        public string FilePath = string.Empty;
        public string Library = string.Empty;
        public string Product = string.Empty;
        public int RunNumber = 0;
        public string EmailDistro = string.Empty;
        public bool SendPreEmail = false;
        public string PreScript = string.Empty;
        public string PostScript = string.Empty;
        public bool RerunFailedTestsOnly = false;
        public string BrowserOverride = string.Empty;
        public string LoginConfigID = string.Empty;
        public DlkLoginConfigRecord LoginConfigOverride = null;
        public int NumberOfRuns = 1;
        public string ScheduledStart = string.Empty;

        public bool ConsiderDefaultEmail = true;

        public string ScheduleId = string.Empty;
        public string MachineName = string.Empty;
        public int ArgumentsCount = 0; //used to detect argument counts converted from executionString

        public DlkTestRunnerCmdLibExecutionArgs()
        {
        }

        public DlkTestRunnerCmdLibExecutionArgs(string[] args)
        {
            int runNumber = 1;
            bool sendPreEmail = false;
            bool rerunFailedTests = false;

            //Read the xml input to determine if suite or scheduler
            var libraryArg = DlkTestRunnerCmdLib.GetArg("/library", "/l");
            var productArg = DlkTestRunnerCmdLib.GetArg("/product", "/p");
            var emailArg = DlkTestRunnerCmdLib.GetArg("/email", "/e");
            var preEmailArg = DlkTestRunnerCmdLib.GetArg("/emailpre", "/ep");
            var preExScriptArg = DlkTestRunnerCmdLib.GetArg("/preScripts", "/pres");
            var postExScriptArg = DlkTestRunnerCmdLib.GetArg("postScripts", "/posts");
            var rerunFailedTestsArg = DlkTestRunnerCmdLib.GetArg("postScripts", "/posts");
            var browserArg = DlkTestRunnerCmdLib.GetArg("/browser", "/b");
            var environmentArg = DlkTestRunnerCmdLib.GetArg("/environment", "/en");

            //parse items
            bool.TryParse(preEmailArg, out sendPreEmail);
            bool.TryParse(rerunFailedTestsArg, out rerunFailedTests);

            FilePath = args[0];
            Library = libraryArg;
            Product = productArg;
            RunNumber = runNumber;
            EmailDistro = emailArg;
            SendPreEmail = sendPreEmail;
            PreScript = preExScriptArg;
            PostScript = postExScriptArg;
            RerunFailedTestsOnly = rerunFailedTests;
            ScheduledStart = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            BrowserOverride = browserArg;
            LoginConfigID = environmentArg;
        }

        public DlkTestRunnerCmdLibExecutionArgs(string executionString)
        {
            const int ScheduleIdIndex = 1;
            const int ServerNameIndex = 2;
            const int FilePathIndex = 3;
            const int LibraryIndex = 4;
            const int ProductIndex = 5;
            const int RunNumberIndex = 6;
            const int EmailDistroIndex = 7;
            const int SendPreEmailIndex = 8;
            const int PreScriptIndex = 9;
            const int PostScriptIndex = 10;
            const int RerunFailedTestsOnlyIndex = 11;
            const int BrowserOverrideIndex = 12;
            const int LoginConfigOverrideIndex = 13;
            const int NumberOfRunsIndex = 14;
            const int ScheduledStartIndex = 15;

            int runNumber = 1;
            bool sendPreEmail = false;
            bool rerunFailedTests = false;
            int numberOfRuns = 1;

            var arguments = executionString.Split('|');
            ArgumentsCount = arguments.Count();

            //parse arguments
            int.TryParse(arguments[RunNumberIndex], out runNumber);
            bool.TryParse(arguments[SendPreEmailIndex], out sendPreEmail);
            bool.TryParse(arguments[RerunFailedTestsOnlyIndex], out rerunFailedTests);
            int.TryParse(arguments[NumberOfRunsIndex], out numberOfRuns);

            DlkLoginConfigRecord loginConfig = null;
            if(!string.IsNullOrEmpty(arguments[LoginConfigOverrideIndex]))
            {
                var splitItems = arguments[LoginConfigOverrideIndex].Split('~');
                loginConfig = new DlkLoginConfigRecord(splitItems[0], splitItems[1], splitItems[2], splitItems[3], splitItems[4],
                                                                                splitItems[5], string.Empty, splitItems[6]);
            }

            ScheduleId = arguments[ScheduleIdIndex];
            MachineName = arguments[ServerNameIndex];
            FilePath = arguments[FilePathIndex];
            Library = arguments[LibraryIndex];
            Product = arguments[ProductIndex];
            RunNumber = runNumber;
            EmailDistro = arguments[EmailDistroIndex];
            SendPreEmail = sendPreEmail;
            PreScript = arguments[PreScriptIndex];
            PostScript = arguments[PostScriptIndex];
            RerunFailedTestsOnly = rerunFailedTests;
            BrowserOverride = arguments[BrowserOverrideIndex];
            LoginConfigOverride = loginConfig;
            NumberOfRuns = numberOfRuns;
            ScheduledStart = arguments[ScheduledStartIndex];
        }

        public string CreateSchedulerExecutionString()
        {
            var loginConfigOverride = LoginConfigOverride != null ?
                string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}", LoginConfigOverride.mID, LoginConfigOverride.mUrl, LoginConfigOverride.mUser, LoginConfigOverride.mPassword, LoginConfigOverride.mDatabase, LoginConfigOverride.mPin, LoginConfigOverride.mMetaData) :
                string.Empty;

            return string.Format("execute|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}",
                                    ScheduleId, 
                                    MachineName,
                                    FilePath,
                                    Library,
                                    Product,
                                    RunNumber, 
                                    EmailDistro,
                                    SendPreEmail,
                                    PreScript,
                                    PostScript, 
                                    RerunFailedTestsOnly,
                                    BrowserOverride,
                                    loginConfigOverride,
                                    NumberOfRuns, 
                                    ScheduledStart);
        }
    }
}
