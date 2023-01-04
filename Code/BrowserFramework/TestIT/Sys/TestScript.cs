using CommonLib.DlkSystem;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace TestIT.Sys
{
    public abstract class TestScript
    {
        private const char EMAIL_DELIMITER = ';';
        private static string LOGS_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs");
        private const string IMG_FILE_HEADER = "err_";
        public bool Result;
        public string Error = string.Empty;
        private string[] emailAddresses;
        private string name = "";

        public string StepNumber { get; set; } = "";

        public string Screen { get; set; } = "";

        public string Control { get; set; } = "";

        public string Keyword { get; set; } = "";

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
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
        
        public bool Run(TestRun Owner, Driver.Browser TargetBrowser, string TestEnvironment = "", string MobileID="", bool IncludeInResults = true, string ProductName = "", string TestPath = "")
        {
            Name = TestPath == "" ? this.GetType().Name : Path.GetFileNameWithoutExtension(TestPath);
            Result = false;
            mTargetBrowser = TargetBrowser;

            /* Track run number of owner run */
            if (TestSetup(TargetBrowser, MobileID, ProductName, out Error))
            {
                try
                {
                    Result = string.IsNullOrEmpty(TestEnvironment) ? TestExecute(out Error)
                        : TestExecute(out Error, TestEnvironment, TestPath);
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    Result = false;
                }

                if (!Result)
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
                XDocument input = XDocument.Load(GetTestResultsPath(TestPath));
                var elements = input.Descendants("step");
                foreach (var el in elements)
                {
                    if (el.Element("status").Value == "Failed")
                    {
                        StepNumber = el.Attribute("id").Value.ToString();
                        Screen = el.Element("screen").Value;
                        Control = el.Element("control").Value;
                        Keyword = el.Element("keyword").Value;
                        break;
                    }
                    else
                    {
                        StepNumber = "";
                        Screen = "";
                        Control = "";
                        Keyword = "";
                    }
                }

                Owner.TestManifest.Add(++(Owner.Test_CurrentRunNumber), this);
                Owner.Test_Total++; // this shouldn't be here, total should be determined before runtime
                if (Result)
                {
                    Owner.Test_Passed++;
                }
                else
                {
                    Owner.Test_Failed++;

                    if (Error.Contains("Control not found"))
                    {
                        Owner.Test_Failed_Control++;
                    }
                    else
                    {
                        Owner.Test_Failed_Keyword++;
                    } 
                }
            }
            return Result;
        }

        private bool TestSetup(Driver.Browser TargetBrowser, string MobileID, string ProductName, out string ErrorMessage)
        {
            StartTime = DateTime.Now;
            // Open Browser
            bool ret = true;
            ErrorMessage = string.Empty;
            try
            {
                Driver.SessionLogger.WriteLine("Running test: " + Name + "...");
                Driver.TargetBrowser = TargetBrowser;
                Driver.StartBrowser(false, MobileID, ProductName);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                Driver.SessionLogger.WriteLine(ErrorMessage);
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

        public virtual bool TestExecute(out string ErrorMessage, string TestEnvironment, string TestPath = "")
        {
            ErrorMessage = string.Empty;
            return true;
        }

        private string GetTestResultsPath(String TestPath)
        {
            String path = "";
            FileInfo[] drFiles = new DirectoryInfo(DlkEnvironment.mDirTestResultsCurrent).GetFiles("*" + Path.GetFileNameWithoutExtension(TestPath) + "*.*");
            foreach (FileInfo file in drFiles)
            {
                if(file.Extension == ".xml")
                path = file.FullName;
            }
            return path;
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
