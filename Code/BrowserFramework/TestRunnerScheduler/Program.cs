using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using CommonLib.DlkUtility;

namespace TestRunnerScheduler
{
    class Program
    {
        #region DECLARATIONS
        private static string mRootPath = string.Empty;
        private static bool mRunNow;
        private static string mRunDay;
        #endregion

        #region METHODS
        static void Main(string[] args)
        {
            try
            {
                InitializeExecutionVariables(args);

                DateTime mLast = DateTime.Now;
                DlkSchedulerSessionHandler.Initialize();

                string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                mRootPath = Directory.GetParent(binDir).FullName;
                while (new DirectoryInfo(mRootPath).GetDirectories()
                    .Where(x => x.FullName.Contains("Products")).Count() == 0)
                    {
                        mRootPath = Directory.GetParent(mRootPath).FullName;
                    }

                DlkTestRunnerSettingsHandler.Initialize(mRootPath);

                // execute the schedule in an infinte loop
                int iSleep = 30000;
                Boolean bSleepMsg = false;
                List<DlkScheduleRecord> mNext = null;
                if (!mRunNow)
                {
                    mNext = DlkSchedulerFileHandler.GetListScheduledSuite();
                    while (true)
                    {
                        if (mLast.DayOfWeek != DateTime.Now.DayOfWeek && mNext.Count < 1) //condition for next-day schedules
                        {
                            mLast = DateTime.Now;
                            mNext = DlkSchedulerFileHandler.GetListScheduledSuite();
                        }
                        if (mNext.Count < 1) //condition for scheduler state
                        {
                            if (!bSleepMsg)
                            {
                                System.Console.WriteLine("No suites scheduled for the remainder of today. Sleeping...");
                                bSleepMsg = true;
                            }
                            Thread.Sleep(iSleep);
                            continue;
                        }
                        else
                        {
                            bSleepMsg = false;
                            if (mNext.First().Order <= 1)
                            {
                                System.Console.WriteLine("Waiting for next scheduled task: [" + mNext.First().Time.ToString() + "]: "
                                    + mNext.First().Product + " : " + mNext.First().TestSuiteRelativePath);
                                DateTime mCurrent = DateTime.Now;
                                TimeSpan mTimeToWait = mNext.First().Time.TimeOfDay - mCurrent.TimeOfDay;
                                Stopwatch mWatch = new Stopwatch();
                                mWatch.Start();
                                while (true)
                                {
                                    if (TimeSpan.FromMilliseconds(mWatch.ElapsedMilliseconds) > mTimeToWait)
                                    {
                                        break;
                                    }
                                    Thread.Sleep(iSleep);
                                }
                                mWatch.Stop();
                            }
                            TestExecute(mNext);
                        }
                    }
                }
                else
                {
                    DayOfWeek pRunDay = DateTime.Now.DayOfWeek;
                    if (Enum.TryParse(mRunDay,true,out pRunDay))
                    {
                        mNext = DlkSchedulerFileHandler.GetListScheduledSuite(pRunDay);
                        while (mNext.Count > 0)
                        {                            
                            TestExecute(mNext, DateTime.Now.ToString());
                        }
                    }
                    else
                    {
                        DisplayHelp();
                    }
                }
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile("Test Scheduler encountered an unexpected error. See program logs for details.", e);
            }
        }

        private static void TestExecute(List<DlkScheduleRecord> mNext, string scheduledStart = "")
        {
            string mProductFolder = mNext.First().ProductFolder;
            string mLibrary = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), mNext.First().Library);


            if (mNext.First().SendEmailOnExecutionStart)
            {
                DlkTestRunnerApi.SetupPreEmailNotification(mNext.First());
            }
            Console.WriteLine("Running pre-execution scripts...");
            DlkTestRunnerApi.RunPreScripts(mNext.First(), mRootPath);
            Console.WriteLine("Running pre-execution scripts...DONE");
            DlkEnvironment.InitializeEnvironment(mProductFolder, mRootPath, mLibrary);
            DlkTestRunnerApi.SetupEmailNotification(mNext.First());

            if (string.IsNullOrEmpty(scheduledStart))
            {
                DateTime today = DateTime.Now;  //date is always today  / execution date
                DateTime startScheduleDateTime =
                    new DateTime(today.Year, today.Month, today.Day, mNext.First().Time.Hour, mNext.First().Time.Minute,
                        mNext.First().Time.Second);
                scheduledStart = startScheduleDateTime.ToString();
            }
            RunTest(Path.Combine(DlkEnvironment.mDirProductsRoot, mNext.First().TestSuiteRelativePath), scheduledStart);
            Console.WriteLine("Running post-execution scripts...");
            DlkTestRunnerApi.RunPostScripts(mNext.First(), mRootPath);
            Console.WriteLine("Running post-execution scripts...DONE");
            mNext.RemoveAt(0);

            DeleteNotificationFile(DlkEnvironment.mDirTestSuite);
        }

        /// <summary>
        /// Initializes the variables to be used by the program during the execution
        /// </summary>
        private static void InitializeExecutionVariables(string[] args)
        {
            try
            {
                string paramRunNow = GetParameterValue(args, "/r");
                mRunNow = paramRunNow == "" ? false : Convert.ToBoolean(paramRunNow);
                //mRunNow = Convert.ToBoolean(GetParameterValue(args, "/r"));
                mRunDay = GetParameterValue(args, "/d");
            }
            catch
            {
                DisplayHelp();
            }
        }

        /// <summary>
        /// Returns the specific parameters passed through command line arguments
        /// </summary>
        private static string GetParameterValue(string[] args, string identifier)
        {
            try
            {
                var argIndex = Array.FindIndex(args, x => x == identifier.ToLower());
                return args[argIndex + 1];
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Displays help menu in case incorrect arguments were supplied
        /// </summary>
        private static void DisplayHelp()
        {
            Console.WriteLine("Test Runner Scheduler by MakatiAutomation");
            //Console.WriteLine("Version: " + STR_VERSION);
            //Console.WriteLine("Last Modified by: " + STR_LAST_MODIFIED_INITIALS);
            Console.WriteLine();
            Console.WriteLine("TESTRUNNERSCHEDULER [/D] [/R]");
            Console.WriteLine("   /D          Day of the week whose schedules will be executed.");
            Console.WriteLine("   /R          Indicates whether to wait for the execution time or to start the execution now.");
            Console.WriteLine();
            Console.WriteLine("EXAMPLES");
            Console.WriteLine("   TestRunnerScheduler /r \"false\"");
            Console.WriteLine("   TestRunnerScheduler /r \"true\" /d \"Monday\"");
        }

        static void RunTest(string TestPath, string ScheduledStart)
        {
            Console.Write("Running '" + Path.GetFileName(TestPath) +  "' test suite...");
            DlkTestRunnerApi.ExecuteScheduledTest(TestPath, ScheduledStart);
            Console.WriteLine("DONE");
        }

        static void RunProgram(DlkExternalScript script)
        {
            try
            {
                string startDir = script.StartIn;
                if (String.IsNullOrWhiteSpace(startDir))
                {
                    startDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                }
                Console.Write("Executing " + script.Name + " ...");
                DlkProcess.RunProcess(script.Path, script.Arguments, startDir, false, script.WaitToFinish);
                Console.WriteLine("DONE");
            }
            catch
            {

            }
        }

        static void DeleteNotificationFile(string TestPath)
        {
            if (File.Exists(Path.Combine(TestPath, "TestSuiteNotification.html")))
            {
                File.Delete((Path.Combine(TestPath, "TestSuiteNotification.html")));
            }
        }

        #endregion
    }
}
