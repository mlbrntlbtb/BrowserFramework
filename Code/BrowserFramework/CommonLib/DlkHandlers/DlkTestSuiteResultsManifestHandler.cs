using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    public static class DlkTestSuiteResultsManifestHandler
    {
        private static String ResultsManifest = DlkEnvironment.mDirSuiteResults + "SuiteResultsManifest.xml";
        private static List<DlkTestSuiteResultsManifestRecord> _ResultManifestRecords = LoadSuiteResultsManifestRecords();

        private static List<DlkTestSuiteResultsManifestRecord> LoadSuiteResultsManifestRecords()
        {
            ResultsManifest = DlkEnvironment.mDirSuiteResults + "SuiteResultsManifest.xml";
            List<DlkTestSuiteResultsManifestRecord> myResultManifestRecords = new List<DlkTestSuiteResultsManifestRecord>();
            CreateResultsManifest();
            try
            {
                XDocument DlkXml = XDocument.Load(ResultsManifest);
                var data = from doc in DlkXml.Descendants("suiteresult")
                            select new
                            {
                                suite = doc.Attribute("suite").Value,
                                suitepath = doc.Attribute("suitepath") != null ? doc.Attribute("suitepath").Value : "",
                                executiondate = doc.Attribute("executiondate").Value,
                                passed = doc.Attribute("passed").Value,
                                failed = doc.Attribute("failed").Value,
                                notrun = doc.Attribute("notrun").Value,
                                resultsdirectory = doc.Attribute("resultsdirectory") != null ? doc.Attribute("resultsdirectory").Value : ""
                            };
                foreach (var val in data)
                {
                    if (!string.IsNullOrEmpty(val.suitepath))
                    {
                        DlkTestSuiteResultsManifestRecord mRec = new DlkTestSuiteResultsManifestRecord(val.suite, val.suitepath, val.executiondate, int.Parse(val.passed), int.Parse(val.failed), int.Parse(val.notrun), val.resultsdirectory);
                        myResultManifestRecords.Add(mRec);
                    }
                }
            }
            catch
            {
                // if the results manifest is blank we end up here
            }
            return myResultManifestRecords;
        }

        public static List<DlkTestSuiteResultsManifestRecord> GetSuiteResultsManifestRecords()
        {
            return LoadSuiteResultsManifestRecords();
        }

        public static void AddSuiteResultsManifestRecord(DlkTestSuiteResultsManifestRecord rec)
        {
            //ResultManifestRecords.RemoveAll(r => rec.suite == r.suite && rec.executiondate == r.executiondate);
            _ResultManifestRecords.Add(rec);
            UpdateResultsManifest();
        }

        public static void DeleteResultsManifestRecord(DlkTestSuiteResultsManifestRecord rec)
        {
            if (_ResultManifestRecords.FindAll(x => x.IsEqual(rec)).Count > 0)
            {
                DlkTestSuiteResultsManifestRecord target = _ResultManifestRecords.FindAll(x => x.IsEqual(rec)).First();
                _ResultManifestRecords.Remove(target);
                UpdateResultsManifest();
            }
        }

        /// <summary>
        /// Creates an empty manifest or returns if one exists
        /// </summary>
        private static void CreateResultsManifest()
        {
            try
            {
                if (File.Exists(ResultsManifest))
                {
                    return;
                }

                XDocument xDoc = new XDocument(new XElement("resultsmanifest"));
                xDoc.Save(ResultsManifest);
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile(string.Format("[CreateResultsManifest] File {0} does not exist", ResultsManifest), ex);
            }
        }
        private static void UpdateResultsManifest()
        {
            
            //transform to xml
            List<XElement> mElms = new List<XElement>();
            foreach (DlkTestSuiteResultsManifestRecord tsm in _ResultManifestRecords)
            {
                XElement mElm = new XElement("suiteresult",
                    new XAttribute("suite", tsm.suite),
                    new XAttribute("suitepath", tsm.suitepath),
                    new XAttribute("executiondate", tsm.executiondate),
                    new XAttribute("passed", tsm.passed),
                    new XAttribute("failed", tsm.failed),
                    new XAttribute("notrun", tsm.notrun),
                    new XAttribute("resultsdirectory", tsm.resultsdirectory)
                    );
                mElms.Add(mElm);
            }

            XElement ElmRoot = new XElement("resultsmanifest", mElms);

            XDocument xDoc = new XDocument(ElmRoot);

            //save
            if (File.Exists(ResultsManifest))
            {
                File.Delete(ResultsManifest);
            }
            xDoc.Save(ResultsManifest);
        }

    }
}
