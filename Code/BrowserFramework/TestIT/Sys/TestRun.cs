using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace TestIT.Sys
{
    public abstract class TestRun
    {
        private const char EMAIL_DELIMITER = ';';
        private const string HEADER = "==================================================";
        private const string SUBHEADER = "--------------------------------------------------";
        private const string TEST_RUN = "TestRun\t\t: ";
        private const string TEST_PASSED = "Passed\t\t: ";
        private const string TEST_FAILED = "Failed\t\t: ";
        private const string TEST_TOTAL = "Total\t\t: ";
        private const string ELAPSED = "Elapsed\t\t: ";
        private const string LAST_CONTROL = "Control\t\t: ";
        private const string LAST_KEYWORD = "Keyword\t\t: ";
        private const string RESULT = "Result\t\t: ";
        private const string EMAIL_SMTP_HOST = "smtp.deltek.com";
        private const string EMAIL_SENDER = "TestIT@deltek.com";
        private const int EMAIL_SMTP_PORT = 25;
        private const string EMAIL_SMTP_USER = "Random@Deltek.com";
        private const string EMAIL_SMTP_PASS = "1234";
        private const string EMAIL_SUBJECT_HEADER = "[TestIT] Results Summary: ";

        public DateTime StartTime;
        public DateTime EndTime;
        public TimeSpan Elapsed;
        public int Test_Total;
        public int Test_Passed;
        public int Test_Failed;
        public int Test_Failed_Control;
        public int Test_Failed_Keyword;
        public int Test_CurrentRunNumber;
        private Mailer Emailer = new Mailer(EMAIL_SMTP_HOST, EMAIL_SMTP_PORT, EMAIL_SMTP_USER, EMAIL_SMTP_PASS);
        private string name = "";

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

        public string StepNumber { get; set; }
        public string Screen { get; set; }
        public string Control { get; set; }
        public string Keyword { get; set; }

        public string Result
        {
            get
            {
                if (Test_Passed > 0)
                {
                    return("Passed");
                }
                else
                {
                    return("Failed");
                }
            }
        }

        public int PassRate
        {
            get
            {
                if (Test_Total > 0)
                {
                    decimal passed = Test_Passed;
                    decimal total = Test_Total;
                    return decimal.ToInt32((passed / total) * 100);
                }
                else
                {
                    return 0;
                }
            }
        }

        public Dictionary<int, TestScript> TestManifest = new Dictionary<int, TestScript>();
        public List<string> ResultLogs = new List<string>();

        private string[] emailAddresses;
        private List<String> testPath;

        public TestRun(List<String> TestPath,string EmailAddresses)
        {
            testPath = TestPath;
            emailAddresses = EmailAddresses.Split(EMAIL_DELIMITER);
        }

        public int Run()
        {
            //Name = testPath != null ? this.GetType().Name : Path.GetFileNameWithoutExtension(testPath);
            SetUp();
            ExecuteTests(testPath);
            TearDown();

            return PassRate;
        }
        
        private void SetUp()
        {
            StartTime = DateTime.Now;
            Driver.SessionLogger.WriteLine("Initializing test run...\n");
        }

        public abstract void ExecuteTests(List<String> testPaths);

        private void TearDown()
        {
            EndTime = DateTime.Now;
            Elapsed = EndTime - StartTime;
            string strElapsed = Elapsed.ToString().Substring(0, Elapsed.ToString().LastIndexOf('.'));
            Driver.SessionLogger.WriteLine("\nTerminating test run...\n");

            // Collate stats
            CacheAndDisplayLogs(HEADER);
            CacheAndDisplayLogs(LAST_CONTROL + Control);
            CacheAndDisplayLogs(LAST_KEYWORD + Keyword);
            CacheAndDisplayLogs(RESULT + Result);
            //CacheAndDisplayLogs(TEST_PASSED + Test_Passed);
            //CacheAndDisplayLogs(TEST_FAILED + Test_Failed);
            //CacheAndDisplayLogs(TEST_TOTAL + Test_Total);
            CacheAndDisplayLogs(ELAPSED + strElapsed);
            CacheAndDisplayLogs(HEADER);

            // Email
            if (emailAddresses != null && emailAddresses.Length > 0)
            {
                // Assemble email. Text for now
                // send email
                string summaryFile = Guid.NewGuid().ToString() + ".html";
                try
                {
                    string product = DlkEnvironment.mDirProduct.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last();
                    string subject = EMAIL_SUBJECT_HEADER + product;
                    string errorMessage;
                    if (Emailer.SendMail(subject, EMAIL_SENDER, emailAddresses, CreateEmailBody(summaryFile), true, out errorMessage))
                    {
                        Driver.SessionLogger.WriteLine("\nResult notificiation email sent!");
                    }
                    else
                    {
                        throw new Exception(errorMessage);
                    }
                }
                catch (Exception e)
                {
                    CacheAndDisplayLogs("\n" + e.Message);
                }
                finally
                {
                    string fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), summaryFile);
                    if (File.Exists(fullPath))
                        File.Delete(fullPath);
                }
            }
        }

        private string CreateEmailBody(string summaryFile)
        {
            String placeholder = String.Empty;
            return new ReportBuilder().CreateHTMLReportBody(this, summaryFile);
        }

        private void CacheAndDisplayLogs(string Msg)
        {
            Driver.SessionLogger.WriteLine(Msg);
            ResultLogs.Add(Msg);
        }
    }
}
