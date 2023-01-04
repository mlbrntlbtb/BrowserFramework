using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace METestHarness.Sys
{
    public abstract class TestScript
    {
        private static string LOGS_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs");
        private const string IMG_FILE_HEADER = "err_";
        public bool Result;
        public string Error = string.Empty;
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public DateTime StartTime;
        public DateTime EndTime;
        public TimeSpan Elapsed;

        private Driver.Browser mTargetBrowser;
        public string BrowserName
        {
            get
            {
                return Enum.GetName(typeof(Driver.Browser), mTargetBrowser);
            }
        }

        public bool Run(TestRun Owner, Driver.Browser TargetBrowser, string TestEnvironment="", bool IncludeInResults=true)
        {
            Result = false;
            mTargetBrowser = TargetBrowser;

            /* Track run number of owner run */
            if (TestSetup(TargetBrowser, out Error))
            {
                try
                {
                    Result = string.IsNullOrEmpty(TestEnvironment) ? TestExecute(out Error)
                        : TestExecute(out Error, TestEnvironment);
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    Result = false;
                }

                if(!Result)
                {
                    Directory.CreateDirectory(LOGS_PATH);
                    Driver.CaptureScreenshot(Path.Combine(LOGS_PATH, IMG_FILE_HEADER + GetDateTimeString(DateTime.Now) + ".png"));
                    Driver.SessionLogger.WriteLine(Error, Logger.MessageType.ERR);
                }

                if (!TestTearDown(Error))
                {
                    
                }                
            }

            if (IncludeInResults)
            {
                Owner.TestManifest.Add(++(Owner.Test_CurrentRunNumber), this);
                Owner.Test_Total++; // this shouldn't be here, total should be determined before runtime
                if (Result)
                {
                    Owner.Test_Passed++;
                }
                else
                {
                    Owner.Test_Failed++;
                }
            }
            return Result;
        }

        private bool TestSetup(Driver.Browser TargetBrowser, out string ErrorMessage)
        {
            StartTime = DateTime.Now;
            // Open Browser
            bool ret = true;
            ErrorMessage = string.Empty;
            try
            {
                Driver.SessionLogger.WriteLine("Running test: " + Name + "...");
                Driver.TargetBrowser = TargetBrowser;
                Driver.StartBrowser(false);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                ret = false;
            }
            return ret;
        }

        protected string TraceMessage(string CustomMessage, string ExceptionMessage, [CallerLineNumber] int LineNumber = 0)
        {
            return String.Format("[Ln {0}] {1} : {2}", LineNumber.ToString(), CustomMessage, ExceptionMessage);
        }

        public virtual bool TestExecute(out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            return true;
        }

        public virtual bool TestExecute(out string ErrorMessage, string TestEnvironment)
        {
            ErrorMessage = string.Empty;
            return true;
        }

        private string GetDateTimeString(DateTime inputString)
        {
            return string.Format("{0:yyyyMMddHHmmss}", inputString);
        }    

        private bool TestTearDown(string ErrorMessage)
        {
            bool ret = true;

            try
            {
                Driver.CloseSession();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                ret = false;
            }
            EndTime = DateTime.Now;
            Elapsed = EndTime - StartTime;
            return ret;
        }
    }
}
