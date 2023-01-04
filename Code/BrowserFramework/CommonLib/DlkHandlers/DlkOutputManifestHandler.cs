using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;


namespace CommonLib.DlkHandlers
{
    /// <summary>
    /// Class to read/write into results manifest file
    /// </summary>
    public static class DlkOutputManifestHandler
    {
        #region PRIVATE MEMBERS
        private static string TestManifestFile = Path.Combine(DlkEnvironment.mDirTestResults, DlkEnvironment.STR_TEST_RESULTS_MANIFEST_FILE);
        private const double DBL_DATE_EXPIRATION_DAYS = -60;
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Get last execution output of a test file (if any)
        /// </summary>
        /// <param name="Path">Absolute local path of test</param>
        /// <returns>Last output value for input test path</returns>
        public static string GetLastOutput(string Path)
        {
            string ret = string.Empty;

            if (!File.Exists(TestManifestFile))
            {
                File.WriteAllText(TestManifestFile, "<output />");
            }

            XDocument doc = XDocument.Load(TestManifestFile, LoadOptions.PreserveWhitespace);
            var data = from itm in doc.Descendants("test")
                       select new
                       {
                           path = itm.Attribute("path").Value.ToString(),
                           value = itm.Value.ToString()
                       };

            var rec = data.FirstOrDefault(x => x.path == Path);
            if (rec != null)
            {
                ret = rec.value;
            }
            return ret;
        }

        /// <summary>
        /// Write into manifest file with updated output
        /// Only output info not older than 60 days are written into manifest
        /// </summary>
        /// <param name="Path">Path of test file</param>
        /// <param name="Value">Output value</param>
        public static void UpdateOutputManifest(string Path, string Value)
        {
            try
            {
                if (!File.Exists(TestManifestFile))
                {
                    File.WriteAllText(TestManifestFile, "<output />");
                }

                XDocument doc = XDocument.Load(TestManifestFile, LoadOptions.PreserveWhitespace);
                var data = from itm in doc.Descendants("test")
                           select new
                           {
                               path = itm.Attribute("path").Value.ToString(),
                               value = itm.Value.ToString(),
                               date = DateTime.Parse(itm.Attribute("date").Value)
                           };
                List<XElement> recs = new List<XElement>();
                foreach (var record in data.ToList().FindAll(x => x.date >= DateTime.Today.AddDays(DBL_DATE_EXPIRATION_DAYS)))
                {
                    recs.Add(new XElement("test", record.value,
                        new XAttribute("path", record.path),
                        new XAttribute("date", record.date.ToShortDateString())
                        ));
                }

                var target = recs.FirstOrDefault(x => x.Attribute("path").Value.ToString() == Path);
                if (target == null)
                {
                    recs.Add(new XElement("test", Value,
                        new XAttribute("path", Path),
                        new XAttribute("date", DateTime.Today.ToShortDateString())
                        ));
                }
                else
                {
                    target.Value = Value;
                    target.SetAttributeValue("date", DateTime.Today.ToShortDateString());
                }
                XElement root = new XElement("output", recs);

                XmlWriterSettings xws = new XmlWriterSettings();
                xws.NewLineHandling = NewLineHandling.Entitize;

                using (XmlWriter xw = XmlWriter.Create(TestManifestFile, xws))
                {
                    new XDocument(root).Save(xw);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile("[Write Output Manifest] Error in writing output manifest", ex);
            }
        }
        #endregion
    }
}
