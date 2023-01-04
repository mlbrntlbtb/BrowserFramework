using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using System.IO;

namespace CommonLib.DlkHandlers
{
    public static class DlkTestSuiteXmlHandler
    {
        private static List<DlkExecutionQueueRecord> mTestSuiteRecs;
        private static String mTestSuite;
        private static XDocument DlkXml;
        public static Boolean IsScriptMissing = false;
        private const string sCollChangedMsg = "Collection was modified; enumeration operation may not execute";
        public static List<DlkExecutionQueueRecord> mMissingRecs { get; set; }

        /// <summary>
        /// This will create a saved suite file. It assumes the calling code has determined if a save can be performed
        /// </summary>
        /// <param name="TestSuite">the full filepath to save to</param>
        /// <param name="Recs">a list of records which we will transalate to xml</param>
        public static void Save(String TestSuite, DlkTestSuiteInfoRecord TestSuiteInfo, List<DlkExecutionQueueRecord> Recs)
        {
            mTestSuite = TestSuite;
            mTestSuiteRecs = Recs;

            List<XElement> tests = new List<XElement>();


            foreach (DlkExecutionQueueRecord sr in mTestSuiteRecs)
            {
                tests.Add(new XElement("test",
                    new XAttribute("identifier", sr.identifier),
                    new XAttribute("row", sr.testrow),
                    new XAttribute("folder", sr.folder),
                    new XAttribute("name", sr.name),
                    new XAttribute("description", sr.description),
                    new XAttribute("file", sr.file),
                    new XAttribute("testid", sr.instance),
                    new XAttribute("teststatus", sr.teststatus),
                    new XAttribute("duration", sr.duration),
                    new XAttribute("environment", sr.environment),
                    new XAttribute("browser", sr.Browser.Name),
                    new XAttribute("keepopen", sr.keepopen ?? "false"),
                    new XAttribute("executiondate", sr.executiondate),
                    new XAttribute("assigned", sr.assigned),
                    new XAttribute("execute", sr.execute),
                    new XAttribute("dependent", sr.dependent),
                    new XAttribute("executedependencyresult", sr.executedependencyresult),
                    new XAttribute("executedependencytestrow", sr.executedependencytestrow),
                    new XAttribute("totalsteps", sr.totalsteps)
                    )
                    );
            }

            List<XElement> lstLinksNode = new List<XElement>();

            foreach (DlkSuiteLinkRecord lnk in TestSuiteInfo.mLinks)
            {
                XElement linknode = new XElement("link",
                    new XAttribute("id", lnk.Id),
                    new XAttribute("name", lnk.DisplayName),
                    new XAttribute("path", lnk.LinkPath)
                    );
                lstLinksNode.Add(linknode);
            }

            XElement mElmsLinks = new XElement("links", lstLinksNode);

            List<XElement> lstTagsNode = new List<XElement>();

            foreach (DlkTag tag in TestSuiteInfo.Tags)
            {
                XElement tagnode = new XElement("tag",
                    new XAttribute("id", tag.Id),
                    new XAttribute("name", tag.Name)
                    );
                lstTagsNode.Add(tagnode);
            }

            XElement mElmsTags = new XElement("tags", lstTagsNode);

            XElement mGlobalVar = TestSuiteInfo.GlobalVar == String.Empty ?
                new XElement("globalvar") : new XElement("globalvar",
                new XAttribute("name", TestSuiteInfo.GlobalVar)
                );

            XElement suite = new XElement("suite", 
                new XAttribute("testbrowser",TestSuiteInfo.Browser),
                new XAttribute("environment", TestSuiteInfo.EnvID),
                new XAttribute("language", TestSuiteInfo.Language),
                new XAttribute("email", TestSuiteInfo.Email),
                new XAttribute("description", TestSuiteInfo.Description),
                new XAttribute("owner", TestSuiteInfo.Owner),
                tests,
                mElmsLinks,
                mElmsTags,
                mGlobalVar
                );
            
            XDocument xDoc = new XDocument(suite);
            xDoc.Save(mTestSuite);
        }

        /// <summary>
        /// Reads a xml file and returns the records. Assumes the caller has already verified the file exists.
        /// </summary>
        /// <param name="TestSuite">a fullpath filename to read</param>
        /// <param name="dirTests">a fullpath of test folder</param>
        /// <returns>a list of records</returns>
        public static List<DlkExecutionQueueRecord> Load(String TestSuite, string dirTests = "")
        {
            if(string.IsNullOrEmpty(dirTests))
            {
                dirTests = DlkEnvironment.mDirTests;
            }

            mTestSuiteRecs = new List<DlkExecutionQueueRecord>();
            List<DlkExecutionQueueRecord> _TestSuiteRecs = new List<DlkExecutionQueueRecord>();
            mTestSuiteRecs = LoadPartial(TestSuite, dirTests);

            /* fetch test info from testfile not from xml */
            IsScriptMissing = false;
            mMissingRecs = new List<DlkExecutionQueueRecord>();
            foreach(DlkExecutionQueueRecord eqr in mTestSuiteRecs ){
                try
                {
                    DlkTest test = new DlkTest(Path.Combine(dirTests, eqr.folder.Trim('\\'), eqr.file));
                    eqr.name = test.mTestName;
                    eqr.file = Path.GetFileName(test.mTestPath);
                    eqr.description = test.mTestDescription;
                    if (Convert.ToInt32(eqr.instance) > test.mInstanceCount)
                    {
                        throw new Exception();
                    }
                    _TestSuiteRecs.Add(eqr);
                }
                catch
                {
                    IsScriptMissing = true;
                    mMissingRecs.Add(eqr);
                }                
            }

            /* */
            mTestSuiteRecs.Clear();
            mTestSuiteRecs = _TestSuiteRecs.ToList();

            return mTestSuiteRecs;
        }

        public static List<DlkExecutionQueueRecord> LoadPartial(String TestSuite, string dirTests = "")
        {
            try
            {
                if (string.IsNullOrEmpty(dirTests))
                {
                    dirTests = DlkEnvironment.mDirTests;
                }

                List<DlkExecutionQueueRecord> suiteRecs = new List<DlkExecutionQueueRecord>();
                DlkXml = XDocument.Load(TestSuite);

                var data = from doc in DlkXml.Descendants("test")
                           select new
                           {
                               identifier = doc.Attribute("identifier").Value,
                               row = doc.Attribute("row").Value,
                               folder = doc.Attribute("folder").Value,
                               name = doc.Attribute("name").Value,
                               description = doc.Attribute("description").Value,
                               keepopen = doc.Attribute("keepopen") == null ? "false" : doc.Attribute("keepopen").Value,
                               file = doc.Attribute("file").Value,
                               testid = doc.Attribute("testid").Value,
                               environment = doc.Attribute("environment").Value,
                               browser = doc.Attribute("browser").Value,
                               teststatus = doc.Attribute("teststatus").Value,
                               duration = doc.Attribute("duration").Value,
                               executiondate = doc.Attribute("executiondate").Value,
                               assigned = doc.Attribute("assigned") == null ? "" : doc.Attribute("assigned").Value,
                               execute = doc.Attribute("execute") == null ? "True" : doc.Attribute("execute").Value,
                               dependent = doc.Attribute("dependent") == null ? "False" : doc.Attribute("dependent").Value,
                               executedependencyresult = doc.Attribute("executedependencyresult") == null ? "" : doc.Attribute("executedependencyresult").Value,
                               executedependencytestrow = doc.Attribute("executedependencytestrow") == null ? "" : doc.Attribute("executedependencytestrow").Value,
                               totalsteps = doc.Attribute("totalsteps") == null ? "--" : doc.Attribute("totalsteps").Value
                           };
                    foreach (var val in data)
                    {
                        string location = Path.Combine(dirTests.Trim('\\') + val.folder, val.file);
                        var executedSteps = "0/0";
                        if (File.Exists(location))
                        {
                            DlkTest test = new DlkTest(location);
                            test.mTestInstanceExecuted = Convert.ToInt32(val.testid);
                            DlkData.SubstituteExecuteDataVariables(test);
                            executedSteps = "0/" + test.mTestSteps.FindAll(x => x.mExecute.ToLower() == "true").Count;
                        }

                        DlkExecutionQueueRecord eqr = new DlkExecutionQueueRecord(val.identifier,
                            val.row, executedSteps, val.teststatus, val.folder, val.name, val.description, val.file, val.testid,
                            val.environment, val.browser, val.keepopen, val.duration, val.executiondate, val.execute, val.dependent,
                            val.executedependencyresult, val.executedependencytestrow, "", val.totalsteps
                            );
                        suiteRecs.Add(eqr);
                    }

                /* Fill dependency information */
                foreach (DlkExecutionQueueRecord eqr in suiteRecs)
                {
                    if (bool.Parse(eqr.dependent) && suiteRecs.FindAll(x => x.testrow == eqr.executedependencytestrow).Count > 0)
                    {
                        eqr.executedependency = suiteRecs.FindAll(x => x.testrow == eqr.executedependencytestrow).First();
                    }
                }

                return suiteRecs;
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Contains(sCollChangedMsg))
                {
                    return LoadPartial(TestSuite, dirTests);
                }
                throw;
            }
        }

        /// <summary>
        /// Get both test suite info and contained records 
        /// </summary>
        /// <param name="TestSuite">Suite Path</param>
        /// <param name="ExecutedSteps">string to set as executed steps (for PERF gain only)</param>
        /// <returns>Tuple of suite info and records</returns>
        public static Tuple<DlkTestSuiteInfoRecord, List<DlkExecutionQueueRecord>> LoadSuiteInfoAndRecords(string TestSuite, string ExecutedSteps)
        {
            XDocument xdoc = XDocument.Load(TestSuite);
            DlkTestSuiteInfoRecord info = GetTestSuiteInfo(TestSuite, xdoc);
            List<DlkExecutionQueueRecord> recs = LoadPartial(TestSuite, ExecutedSteps, xdoc);
            return new Tuple<DlkTestSuiteInfoRecord, List<DlkExecutionQueueRecord>>(info, recs);
        }

        /// <summary>
        /// Overloaded method that will be called by Test Library
        /// </summary>
        /// <param name="TestSuite">Testsuite file</param>
        /// <param name="ExecutedSteps">This will have a fixed value to lessen processing time</param>
        /// <returns>list of DlkExecutionQueueRecord</returns>
        public static List<DlkExecutionQueueRecord> LoadPartial(String TestSuite, string ExecutedSteps, XDocument PreFetchedDoc = null)
        {
            List<DlkExecutionQueueRecord> suiteRecs = new List<DlkExecutionQueueRecord>();
            DlkXml = PreFetchedDoc == null ? XDocument.Load(TestSuite) : PreFetchedDoc;

            var data = from doc in DlkXml.Descendants("test")
                       select new
                       {
                           identifier = doc.Attribute("identifier").Value,
                           row = doc.Attribute("row").Value,
                           folder = doc.Attribute("folder").Value,
                           name = doc.Attribute("name").Value,
                           description = doc.Attribute("description").Value,
                           keepopen = doc.Attribute("keepopen") == null ? "false" : doc.Attribute("keepopen").Value,
                           file = doc.Attribute("file").Value,
                           testid = doc.Attribute("testid").Value,
                           environment = doc.Attribute("environment").Value,
                           browser = doc.Attribute("browser").Value,
                           teststatus = doc.Attribute("teststatus").Value,
                           duration = doc.Attribute("duration").Value,
                           executiondate = doc.Attribute("executiondate").Value,
                           assigned = doc.Attribute("assigned") == null ? "" : doc.Attribute("assigned").Value,
                           execute = doc.Attribute("execute") == null ? "True" : doc.Attribute("execute").Value,
                           dependent = doc.Attribute("dependent") == null ? "False" : doc.Attribute("dependent").Value,
                           executedependencyresult = doc.Attribute("executedependencyresult") == null ? "" : doc.Attribute("executedependencyresult").Value,
                           executedependencytestrow = doc.Attribute("executedependencytestrow") == null ? "" : doc.Attribute("executedependencytestrow").Value,
                           totalsteps = doc.Attribute("totalsteps") == null ? "--" : doc.Attribute("totalsteps").Value
                       };
            foreach (var val in data)
            {
                DlkExecutionQueueRecord eqr = new DlkExecutionQueueRecord(val.identifier,
                    val.row, ExecutedSteps, val.teststatus, val.folder, val.name, val.description, val.file, val.testid,
                    val.environment, val.browser, val.keepopen, val.duration, val.executiondate, val.execute, val.dependent,
                    val.executedependencyresult, val.executedependencytestrow,"", val.totalsteps
                    );
                suiteRecs.Add(eqr);
            }

            /* Fill dependency information */
            foreach (DlkExecutionQueueRecord eqr in suiteRecs)
            {
                if (bool.Parse(eqr.dependent) && suiteRecs.FindAll(x => x.testrow == eqr.executedependencytestrow).Count > 0)
                {
                    eqr.executedependency = suiteRecs.FindAll(x => x.testrow == eqr.executedependencytestrow).First();
                }
            }

            return suiteRecs;
        }

        public static bool IsValidFormat(String TestSuite)
        {
            bool isValid = false;
            try
            {
                DlkXml = XDocument.Load(TestSuite);
                var data = from doc in DlkXml.Descendants("test")
                           select new
                           {
                               identifier = doc.Attribute("identifier").Value,
                               row = doc.Attribute("row").Value,
                               folder = doc.Attribute("folder").Value,
                               name = doc.Attribute("name").Value,
                               description = doc.Attribute("description").Value,
                               file = doc.Attribute("file").Value,
                               testid = doc.Attribute("testid").Value,
                               environment = doc.Attribute("environment").Value,
                               browser = doc.Attribute("browser").Value,
                               teststatus = doc.Attribute("teststatus").Value,
                               duration = doc.Attribute("duration").Value,
                               executiondate = doc.Attribute("executiondate").Value,
                               assigned = doc.Attribute("assigned") == null ? "" : doc.Attribute("assigned").Value
                           };
                foreach (var val in data)
                {
                    if (val.identifier != null)
                    {
                        isValid = true;
                    }
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Reads a xml file and returns the execution info record.
        /// </summary>
        /// <param name="TestSuite">a fullpath filename to read</param>
        /// <returns>DlkTestSuiteInfoRecord object</returns>
        public static DlkTestSuiteInfoRecord GetTestSuiteInfo(String TestSuite, XDocument PreFetchedDoc = null)
        {
            DlkXml = PreFetchedDoc == null ? XDocument.Load(TestSuite) : PreFetchedDoc;
            if (DlkXml.Root.Name == "suite")
            {
                var data = from doc in DlkXml.Descendants("suite")
                           select new
                           {
                               browser = doc.Attribute("testbrowser").Value,
                               environment = doc.Attribute("environment").Value,
                               language = doc.Attribute("language").Value,
                               email = doc.Attribute("email")!=null?doc.Attribute("email").Value:"",
                               description = doc.Attribute("description")!=null?doc.Attribute("description").Value:"",
                               owner = doc.Attribute("owner") != null ? doc.Attribute("owner").Value : ""
                           };

                var links = from doc in DlkXml.Descendants("link")
                            select new
                            {
                                id = doc.Attribute("id").Value,
                                name = doc.Attribute("name").Value,
                                link = doc.Attribute("path").Value
                            };

                List<DlkSuiteLinkRecord> _links = new List<DlkSuiteLinkRecord>();
                foreach (var val in links)
                {
                    _links.Add(new DlkSuiteLinkRecord(val.id, val.name, val.link));
                }

                var tags = from doc in DlkXml.Descendants("tag")
                            select new
                            {
                                id = doc.Attribute("id").Value,
                                name = doc.Attribute("name") != null ? doc.Attribute("name").Value : ""
                            };

                List<DlkTag> _tags = new List<DlkTag>();

                List<DlkTag> allTags = DlkTag.LoadAllTags();
                foreach (var val in tags)
                {
                    var tg = allTags.FirstOrDefault(x => x.Id == val.id);
                    if (tg != null)
                    {
                        _tags.Add(new DlkTag(tg.Id, tg.Name, tg.Conflicts, tg.Description));
                    }
                    else
                    {
                        _tags.Add(new DlkTag(val.id, val.name, "", ""));
                    }
                }

                var globalVar = from doc in DlkXml.Descendants("globalvar")
                            select new
                            {
                                name = doc.Attribute("name") != null ? doc.Attribute("name").Value : ""
                            };

                return new DlkTestSuiteInfoRecord(data.First().browser, data.First().environment, data.First().language, _tags, data.First().email, data.First().description, _links, data.First().owner, globalVar.Any() ? globalVar.First().name : "");
            }
            //For backward compatibility
            else
            {
                return new DlkTestSuiteInfoRecord("Firefox", "", "EN", new List<DlkTag>(), "", "", new List<DlkSuiteLinkRecord>(), "", "");
            }
        }

        public static DlkExecutionQueueRecord GetExecutionQueueRecordByScriptTestID(String testid)
        {
            DlkExecutionQueueRecord executionrecord = null;
            foreach (DlkExecutionQueueRecord rec in mTestSuiteRecs)
            {
                if (rec.identifier == testid)
                {
                    executionrecord = rec;
                    break;
                }
            }
            return executionrecord;
        }

        /// <summary>
        /// Infers suite path from Name of suite
        /// </summary>
        /// <param name="SuiteName">Name of suite</param>
        /// <returns>Inferred suite path from file name</returns>
        public static string GetTestSuitePathFromName(string SuiteName)
        {
            string[] hits = Directory.GetFiles(DlkEnvironment.mDirTestSuite, SuiteName.Replace(".xml", "") + ".xml", SearchOption.AllDirectories);
            /* Just return first hit if found, empty string if not */
            return hits.DefaultIfEmpty(string.Empty).First();
        }

    }
}
