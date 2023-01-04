using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using CommonLib.DlkSystem;

namespace CommonLib.DlkHandlers
{
    public static class DlkErrorReportsHandler
    {
        public static void CreateErrorReport(String FilePath, List<DlkErrorRecord> ErrorLog)
        {
            try
            {
                List<XElement> errInst = new List<XElement>();

                foreach (DlkErrorRecord el in ErrorLog)
                {
                    errInst.Add(new XElement("errorlog",
                        new XAttribute("instance", el.Instance),
                        new XAttribute("errorInfo", el.Logs)
                        )
                        );
                }
                XElement errorReport = new XElement("reportsummary",
                    errInst
                    );

                XDocument xDoc = new XDocument(errorReport);
                xDoc.Save(FilePath);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile("Test Runner encountered an unexpected error. See program logs for details.", ex);
            }
        }

    }
}