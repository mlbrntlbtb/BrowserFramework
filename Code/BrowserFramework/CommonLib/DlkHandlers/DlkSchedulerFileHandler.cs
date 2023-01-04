using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    /// <summary>
    /// This class manages the schedule data
    /// </summary>
    public static class DlkSchedulerFileHandler
    {
        #region DECLARATIONS

        private static List<DlkScheduleRecord> mSchedules;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// the local file that contains the schedule
        /// </summary>
        public static String mScheduleFile
        {
            get
            {
                if (_ScheduleFile == null)
                {
                   
                    String mFile = Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), Environment.MachineName + ".xml");
                    mFile.Replace(" ", "");
                    _ScheduleFile = mFile;
                }
                return _ScheduleFile;
            }
            set
            {
                _ScheduleFile = value;
            }
        }
        private static String _ScheduleFile;

        public static List<DlkScheduleRecord> Schedules
        {
            get
            {
                if (mSchedules == null)
                {
                    mSchedules = LoadSchedule();
                }
                return mSchedules;
            }
        }

        #endregion

        #region METHODS

        public static void UpdateSchedule(List<DlkScheduleRecord> SchedRecs)
        {
            // create elements
            List<XElement> Elms = new List<XElement>();
            foreach (DlkScheduleRecord mRec in SchedRecs)
            {

                List<XElement> lstScripts = new List<XElement>();

                /* Assemble external script node */
                foreach (DlkExternalScript scr in mRec.ExternalScripts)
                {
                    XElement mScript = new XElement("Script", 
                        new XAttribute("path", scr.Path),
                        new XAttribute("order", scr.Order.ToString()),
                        new XAttribute("arguments", scr.Arguments),
                        new XAttribute("startin", scr.StartIn),
                        new XAttribute("wait", scr.WaitToFinish.ToString()),
                        new XAttribute("type", Enum.GetName(typeof(DlkExternalScriptType), scr.Type))
                        );
                    lstScripts.Add(mScript);
                }

                
                XElement mExternalScripts = new XElement("ExternalScripts", lstScripts);

                /* Assemble test suite node */
                XElement mTestSuite = new XElement("TestSuite",
                    new XAttribute("path", mRec.TestSuiteRelativePath),
                    new XAttribute("product", mRec.Product),
                    new XAttribute("library", mRec.Library)
                    );

                /* Assemble schedule node */
                XElement mSchedule = new XElement("Schedule",
                    new XAttribute("day", Enum.GetName(typeof(DayOfWeek), mRec.Day)),
                    new XAttribute("time", mRec.Time.ToShortTimeString()),
                    new XAttribute("order", mRec.Order.ToString()),
                    new XAttribute("email", mRec.Email),
                    new XAttribute("runstatus", mRec.RunStatus),
                    new XAttribute("sendEmailOnExecutionStart", mRec.SendEmailOnExecutionStart),
                    mTestSuite, mExternalScripts
                    );


                Elms.Add(mSchedule);
            }
            XElement ElmRoot = new XElement("Scheduler", Elms);
            XDocument xDoc = new XDocument(ElmRoot);

            if (File.Exists(mScheduleFile))
            {
                File.Delete(mScheduleFile);
            }
            xDoc.Save(mScheduleFile);
        }
        
        public static List<DlkScheduleRecord> LoadSchedule(bool GetFromFile=true)
        {
            /* If want to retrieve cached copy of schedules, then just return this */
            if (!GetFromFile)
            {
                return mSchedules;
            }

            /* scheduler file does not exist, create new */
            if (!File.Exists(mScheduleFile))
            {
                List<DlkScheduleRecord> recList = new List<DlkScheduleRecord>();
                UpdateSchedule(recList);
                return recList;
            }

            mSchedules = new List<DlkScheduleRecord>();

            XDocument mXml = null;
            mXml = XDocument.Load(mScheduleFile);

            /* scheduler file exists but has old schema */
            if (mXml.Descendants("Scheduler").Count() == 0)
            {
                File.Delete(mScheduleFile);
                List<DlkScheduleRecord> recList = new List<DlkScheduleRecord>();
                UpdateSchedule(recList);
                return recList;
            }

            var data = from doc in mXml.Descendants("Schedule")
                       select new
                       {
                           day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), doc.Attribute("day").Value),
                           time = DateTime.Parse(doc.Attribute("time").Value ?? null),
                           order = int.Parse(doc.Attribute("order").Value),
                           email = doc.Attribute("email").Value,
                           testsuite = doc.Element("TestSuite"),
                           externalscripts = doc.Element("ExternalScripts"),
                           runstatus = doc.Attribute("runstatus") != null ? Convert.ToBoolean(doc.Attribute("runstatus").Value) : true,
                           sendEmailOnExecutionStart = doc.Attribute("sendEmailOnExecutionStart") != null ? Convert.ToBoolean(doc.Attribute("sendEmailOnExecutionStart").Value) : false
                       };

            foreach (var val in data)
            {
                /* Iterate thru external scripts */
                List<DlkExternalScript> lstScr = new List<DlkExternalScript>();
                foreach (XElement scr in val.externalscripts.Elements())
                {
                    lstScr.Add(new DlkExternalScript(scr.Attribute("path").Value, int.Parse(scr.Attribute("order").Value), scr.Attribute("arguments").Value, scr.Attribute("startin").Value,
                        bool.Parse(scr.Attribute("wait").Value), (DlkExternalScriptType)Enum.Parse(typeof(DlkExternalScriptType), scr.Attribute("type").Value)));
                }

                DlkScheduleRecord mRec = new DlkScheduleRecord(val.day, val.time, val.order, val.email,
                    val.testsuite.Attribute("library").Value, val.testsuite.Attribute("product").Value, val.runstatus, val.sendEmailOnExecutionStart, val.testsuite.Attribute("path").Value,
                    lstScr);
                mSchedules.Add(mRec);
            }

            return mSchedules;
        }

        public static List<DlkScheduleRecord> GetListScheduledSuite()
        {
            List<DlkScheduleRecord>  CurrentSchedule = new List<DlkScheduleRecord>();
            bool hasNotElapsed = Schedules.Count(x => x.Day == DateTime.Now.DayOfWeek && x.Time.TimeOfDay >= DateTime.Now.TimeOfDay && x.Order==1 && x.RunStatus)>0;
            if (hasNotElapsed) {
                CurrentSchedule = Schedules.FindAll(x => x.Day == DateTime.Now.DayOfWeek && x.RunStatus).OrderBy(x => x.Order).ToList();
            }
            return CurrentSchedule;
        }

        public static List<DlkScheduleRecord> GetListScheduledSuite(DayOfWeek runDay)
        {
            List<DlkScheduleRecord> CurrentSchedule = Schedules.FindAll(x => x.Day == runDay && x.RunStatus).OrderBy(x => x.Order).ToList();
            return CurrentSchedule;
        }
        #endregion
    }
}