using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Diagnostics;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;

namespace CommonLib.DlkHandlers
{
    public static class DlkSessionInfoFileHandler
    {
        private static String SessionInfoFile = "";
        private static XDocument DlkXml;

        public static String User = "";
        public static DateTime LastLogin;
        public static Boolean bErrorEncountered = false;

        public static void Initialize()
        {
            SessionInfoFile = DlkEnvironment.mDirTools + @"SessionInfo.xml";
            if (File.Exists(SessionInfoFile))
            {
                DlkXml = XDocument.Load(SessionInfoFile);
                SetVars();
            }
        }
        public static void Initialize(String UserName)
        {
            User = UserName;
            bErrorEncountered = false;
            LastLogin = DateTime.Now;
            UpdateAndWrite(User);
            SessionInfoFile = DlkEnvironment.mDirTools + @"SessionInfo.xml";
            DlkXml = XDocument.Load(SessionInfoFile);
            SetVars();
        }

        public static void LogErrorInSession(Boolean bErrorFound)
        {
            bErrorEncountered = bErrorFound;
            UpdateAndWrite(User);
            SetVars();
        }

        public static Boolean IsLoginNeeded(String UserName)
        {
            DlkLogger.LogInfo("LastUser: " + User + ", LastLogin: " + LastLogin + ", CurrentUser: " + UserName);
            Boolean bIsLoginNeeded = true;
            if (User == "")
            {
                return bIsLoginNeeded;
            }
            if (User != UserName)
            {
                return bIsLoginNeeded;
            }
            if (LastLogin == null)
            {
                return bIsLoginNeeded;
            }

            if (bErrorEncountered)
            {
                return bIsLoginNeeded;
            }

            /* Commented out. Not applicable for batch runs. Since last login time is not indicator of session in-activity */
            //DateTime dtTwoHoursAgo = DateTime.Now;
            //dtTwoHoursAgo = dtTwoHoursAgo.Subtract(new TimeSpan(2, 0, 0));
            //if (LastLogin <= dtTwoHoursAgo)
            //{
            //    return bIsLoginNeeded;
            //}

            bIsLoginNeeded = false;
            return bIsLoginNeeded;
        }
        private static void SetVars()
        {
            var data = from doc in DlkXml.Descendants("sessioninfo")
                       select new
                       {
                           user = doc.Element("user").Value,
                           lastlogin = doc.Element("lastlogin").Value,
                           errorencountered = doc.Element("errorencountered").Value
                       };
            foreach (var val in data)
            {
                User = val.user;

                // set default if needed
                if ((val.lastlogin == null) || (val.lastlogin == ""))
                {
                    DateTime dtYesterday = DateTime.Now;
                    dtYesterday = dtYesterday.Subtract(new TimeSpan(1, 0, 0, 0));
                    LastLogin = dtYesterday;
                }
                else
                {
                    LastLogin = Convert.ToDateTime(val.lastlogin);
                }

                if ((val.errorencountered == null) || (val.errorencountered == ""))
                {
                    bErrorEncountered = false;
                }
                else
                {
                    bErrorEncountered = Convert.ToBoolean(val.errorencountered);
                }
            }
        }
        private static void UpdateAndWrite(String UserEmail)
        {
            DateTime dtNow = DateTime.Now;

            // create Doc
            XElement ElmSessionInfo = new XElement("sessioninfo",
                new XElement("user", User),
                new XElement("lastlogin", LastLogin.ToString()),
                new XElement("errorencountered", Convert.ToString(bErrorEncountered))
                );

            XDocument xDoc = new XDocument(ElmSessionInfo);
            if (File.Exists(SessionInfoFile))
            {
                FileInfo mFileInfo = new FileInfo(SessionInfoFile);
                mFileInfo.IsReadOnly = false;
                File.Delete(SessionInfoFile);
            }
            xDoc.Save(SessionInfoFile);
        }
    }
}

