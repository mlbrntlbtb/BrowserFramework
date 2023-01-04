using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using CommonLib.DlkSystem;

namespace CommonLib.DlkHandlers
{
    public class DlkSchedulerSessionRecord
    {
        public DateTime datetime { get; set; }
        public String suitepath { get; set; }

        public DlkSchedulerSessionRecord(DateTime pDateTime, String pSuitePath)
        {
            datetime = pDateTime;
            suitepath = pSuitePath;
        }
    }

    public static class DlkSchedulerSessionHandler
    {
        private static String mSessionLogFile = "SessionLogs.xml";
        private static List<DlkSchedulerSessionRecord> mlstSchedulerSessions;

        public static void Initialize()
        {
            LoadSessionLog();            

        }

        public static void LoadSessionLog()
        {
            mlstSchedulerSessions = new List<DlkSchedulerSessionRecord>();
            String sessionLogFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), mSessionLogFile);
            if (File.Exists(sessionLogFilePath))
            {
                XDocument sessionLog = XDocument.Load(sessionLogFilePath);
                var data = from doc in sessionLog.Descendants("session")
                           select new
                           {
                               datetime = doc.Attribute("datetime").Value,
                               suitepath = doc.Attribute("suitepath").Value
                           };
                foreach (var val in data)
                {
                    mlstSchedulerSessions.Add(new DlkSchedulerSessionRecord(Convert.ToDateTime(val.datetime), val.suitepath));
                }
            }
            
        }

        public static void AddSession(DateTime datetime, String suitepath)
        {
            mlstSchedulerSessions.Add(new DlkSchedulerSessionRecord(datetime, suitepath));
        }

        public static Boolean IsSuiteExecutedToday(String suitepath)
        {
            Boolean result = false;
            //var data = from sessionRec in mlstSchedulerSessions
            //           where sessionRec.datetime.ToString("ddMMyyyy") == DateTime.Today.ToString("ddMMyyyy") && sessionRec.suitepath == suitepath
            //           select sessionRec;
            //if (data.Count() > 0)
            //{
            //    result = true;
            //}

            if (mlstSchedulerSessions.Count == 0)
            {
                return false;
            }

            DlkSchedulerSessionRecord lastSession = mlstSchedulerSessions.Last();
            if (lastSession.suitepath == suitepath && DateTime.Now.Subtract(lastSession.datetime).TotalHours < 24)
            {
                result = true;
            }

            return result;
        }

        public static List<DlkSchedulerSessionRecord> GetExecutedSuites(DateTime date)
        {
            List<DlkSchedulerSessionRecord> executedSuites = new List<DlkSchedulerSessionRecord>();
            var data = from sessionRec in mlstSchedulerSessions
                       where sessionRec.datetime.Date == date.Date
                       select sessionRec;

            foreach (var val in data)
            {
                executedSuites.Add(val);
            }

            return executedSuites;
        }

        public static void UpdateSessionLog()
        {
            List<XElement> sessions = new List<XElement>();
            foreach (DlkSchedulerSessionRecord sessionRec in mlstSchedulerSessions)
            {
                sessions.Add(new XElement("session",
                    new XAttribute("datetime", sessionRec.datetime.ToString()),
                    new XAttribute("suitepath", sessionRec.suitepath))
                    );
            }
            XElement sessionlogs = new XElement("sessionlogs", sessions);
            XDocument doc = new XDocument(sessionlogs);
            doc.Save(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), mSessionLogFile));
        }
    }
}
