using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// A schedule record
    /// </summary>
    public class DlkScheduleRecord
    {
        #region PUBLIC PROPERTIES
        public DayOfWeek Day { get; set; }
        public DateTime Time { get; set; }
        public int Order { get; set; }
        public string Email { get; set; }
        public string Library { get; set; }
        public string Product { get; set; }
        public string TestSuiteRelativePath { get; set; }
        public string ProductFolder { get; set; }
        public bool RunStatus { get; set; }
        public bool SendEmailOnExecutionStart { get; set; }
        public List<DlkExternalScript> ExternalScripts { get; set; }
        public String TestSuiteDisplay
        {
            get
            {
                return System.IO.Path.GetFileName(TestSuiteRelativePath) + " [" + Product + "]";
            }
        }
        #endregion

        #region PUBLIC METHODS
        public DlkScheduleRecord(DayOfWeek day, DateTime time, int order, string email, string library,
            string product, bool runstatus, bool sendEmailOnExecution, string testSuiteRelativePath, List<DlkExternalScript> externalScripts)
        {
            Day = day;
            Time = time;
            Order = order;
            Email = email;
            RunStatus = runstatus;
            SendEmailOnExecutionStart = sendEmailOnExecution;
            Library = System.IO.Path.GetFileName(library);
            Product = product;

            /* for backward compatibility where absolute paths where erroneously used */
            TestSuiteRelativePath = System.IO.Path.IsPathRooted(testSuiteRelativePath) && testSuiteRelativePath.Contains("BrowserFramework\\Products\\")
                ? testSuiteRelativePath.Substring(testSuiteRelativePath.IndexOf("BrowserFramework\\Products\\") + 26)
                : testSuiteRelativePath;

            ProductFolder = TestSuiteRelativePath.Contains("\\") ? TestSuiteRelativePath.Substring(0, TestSuiteRelativePath.IndexOf("\\")) : string.Empty;
            ExternalScripts = externalScripts;
        }

        public DlkScheduleRecord Clone()
        {
            return (DlkScheduleRecord)this.MemberwiseClone();
        }
        #endregion
    }
}