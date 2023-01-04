#define ATK_RELEASE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Collections;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;

namespace CommonLib.DlkHandlers
{

    /// <summary>
    /// This class manages the data for a test
    /// </summary>
    public class DlkTest
    {
        /// <summary>
        /// the test steps for the test - includes execution details
        /// </summary>
        public List<DlkTestStepRecord> mTestSteps
        {
            get
            {
                return _TestSteps;
            }
            set
            {
                _TestSteps = value;
            }
        }

        /// <summary>
        /// returns only test steps that were executed
        /// </summary>
        public List<DlkTestStepRecord> mExecutedTestSteps
        {
            get
            {
                List<DlkTestStepRecord> ret = new List<DlkTestStepRecord>();
                foreach (DlkTestStepRecord step in mTestSteps)
                {
                    if (step.mStepStatus.ToLower() == "passed" || step.mStepStatus.ToLower() == "failed")
                    {
                        ret.Add(step);
                    }
                    else
                    {
                        //do nothing
                        //break;
                    }
                }
                return ret;
            }
        }

        private List<DlkTestStepRecord> _TestSteps;

        private DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public String mTestPath
        {
            get
            {
                return _TestPath;
            }
            set
            {
                _TestPath = value;
            }
        }
        private String _TestPath;

        public String mFileName
        {
            get
            {
                return Path.GetFileName(mTestPath);
            }

        }

        /// <summary>
        /// The name of the test
        /// </summary>
        public String mTestName
        {
            get
            {
                return _TestName;
            }
            set
            {
                _TestName = value;
            }
        }
        private String _TestName;

        /// <summary>
        /// The desciption of the test
        /// </summary>
        public String mTestDescription
        {
            get
            {
                return _TestDescription;
            }
            set
            {
                _TestDescription = value;
            }
        }
        private String _TestDescription;

        /// <summary>
        /// The author of the test
        /// </summary>
        public String mTestAuthor
        {
            get
            {
                return _TestAuthor;
            }
            set
            {
                _TestAuthor = value;
            }
        }
        private String _TestAuthor;
        
        /// <summary>
        /// status of the test
        /// </summary>
        public String mTestStatus
        {
            get
            {
                return _TestStatus;
            }
            set
            {
                _TestStatus = value;
            }
        }
        private String _TestStatus;

        /// <summary>
        /// when the test started
        /// </summary>
        public DateTime mTestStart
        {
            get
            {
                return _TestStart;
            }
        }
        private DateTime _TestStart;

        /// <summary>
        /// when the test ended
        /// </summary>
        public DateTime mTestEnd
        {
            get
            {
                return _TestEnd;
            }
        }
        private DateTime _TestEnd;

        public String mTestElapsed
        {
            get
            {
                return _TestElapsed;
            }
        }
        private String _TestElapsed;

        /// <summary>
        /// This is the iteration of the test that was executed
        /// </summary>
        public int mTestInstanceExecuted
        {
            get
            {
                return _TestInstanceExecuted;
            }
            set
            {
                _TestInstanceExecuted = value;
            }
        }
        private int _TestInstanceExecuted;

        /// <summary>
        /// which step the test failed during
        /// </summary>
        public int mTestFailedAtStep
        {
            get
            {
                return _TestFailedAtStep;
            }
        }
        private int _TestFailedAtStep =0;

#if ATK_RELEASE
        /// <summary>
        /// Determines if the test execution should continue after encountering an error. Turned off by default.
        /// </summary>
        public bool mContinueOnError
        {
            get { return _ContinueOnError; }
            set { _ContinueOnError = value; }
        }
        private bool _ContinueOnError = false;
#else       
#endif



        /// <summary>
        /// These are the messages logged during test setup
        /// </summary>
        public List<DlkLoggerRecord> mTestSetupLogMessages
        {
            get
            {
                return _TestSetupLogMessages;
            }
        }
        /// <summary>
        /// Writes .txt file to identify the test result as AdHoc run.
        /// </summary>
        internal void WriteAdHocFile()
        {
            try
            {
                FileInfo fi = new FileInfo(mTestPath);
                _Identifier = string.IsNullOrEmpty(DlkEnvironment.mCurrentTestIdentifier) ? "" : DlkEnvironment.mCurrentTestIdentifier;

                string path = DlkEnvironment.mDirTestResultsCurrent + Path.GetFileNameWithoutExtension(fi.Name) + "_"
                    + mTestInstanceExecuted + ".txt";

                int copy = 0;
                while (File.Exists(path))
                {
                    string tmp = DlkEnvironment.mDirTestResultsCurrent + Path.GetFileNameWithoutExtension(fi.Name) + "_"
                        + mTestInstanceExecuted + "_" + ++copy + ".txt";
                    if (File.Exists(tmp))
                        path = tmp;
                }
                File.WriteAllText(path, "");
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile("[Write AdHoc Test Result] Error in writing test result file.", ex);
            }
        }

        private List<DlkLoggerRecord> _TestSetupLogMessages;
        
        /// <summary>
        /// These are the messages logged during test teardown
        /// </summary>
        public List<DlkLoggerRecord> mTestTeardownLogMessages
        {
            get
            {
                return _TestTeardownLogMessages;
            }
        }
        private List<DlkLoggerRecord> _TestTeardownLogMessages;


        /// <summary>
        /// This is the loaded xml for the test
        /// </summary>
        private XDocument mXml;

        /// <summary>
        /// Number of instances of a test
        /// </summary>
        public int mInstanceCount
        {
            get
            {
                return _InstanceCount;
            }
            set
            {
                _InstanceCount = value;
            }
        }

        private int _InstanceCount = 0;

        public string mIdentifier
        {
            get
            {
                return _Identifier;
            }
            set
            {
                _Identifier = value;
            }
        }

        private string _Identifier;

        public DlkData Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        private DlkData _data = null;

        private List<DlkTestLinkRecord> _links = new List<DlkTestLinkRecord>();
        private List<DlkTag> _tags = new List<DlkTag>();

        /// <summary>
        /// Local/Network file links or HTTP Uri strings attached to test
        /// </summary>
        public List<DlkTestLinkRecord> mLinks
        {
            get
            {
                return _links;
            }
            set
            {
                _links = value;
            }
        }

        /// <summary>
        /// Tags attached to test
        /// </summary>
        public List<DlkTag> mTags
        {
            get
            {
                return _tags;
            }
            set
            {
                _tags = value;
            }
        }

        /// <summary>
        /// Create a DlkTest object by passing in the path to the test file
        /// </summary>
        /// <param name="TestFilePath">test file path</param>
        /// <param name="IsNewFile">boolean value that determines if the Test object should be created as new or not</param>
        public DlkTest(String TestFilePath, bool IsNewFile = false)
        {
            _TestPath = TestFilePath; // note: we don't read the path that is in a results file... we always set it to what we are reading

            // load the XML
            mXml = XDocument.Load(TestFilePath, LoadOptions.PreserveWhitespace);

            _TestSteps = new List<DlkTestStepRecord>();

            // set the root test elements
            var dataroot = from doc in mXml.Descendants("test")
                       select new
                       {
                           name = doc.Element("name").Value,
                           description = doc.Element("description").Value,
                           author = (String)doc.Element("author") ?? "",
                           identifier = (String)doc.Element("identifier") ?? "",
                           instance = (String)doc.Element("instance") ?? "",
#if ATK_RELEASE
                           continueonerror = (String)doc.Element("continueonerror") ?? "",
#else
#endif
                           status = !IsNewFile ? (String)doc.Element("status") ?? "" : "",
                           stepfailed = !IsNewFile ? (String)doc.Element("stepfailed") ?? "" : "",
                           start = !IsNewFile ? (String)doc.Element("start") ?? "" : "",
                           end = !IsNewFile ? (String)doc.Element("end") ?? "" : "",
                           elapsed = !IsNewFile ? (String)doc.Element("elapsed") ?? "" : ""
                       };
            foreach (var val in dataroot)
            {
                _TestName = val.name;
                _TestDescription = val.description;
                _TestAuthor = val.author;
                _TestStatus = val.status;
                _Identifier = val.identifier;
#if ATK_RELEASE
                _ContinueOnError = String.IsNullOrWhiteSpace(val.continueonerror) ? false : Convert.ToBoolean(val.continueonerror);
#else
#endif
                if (val.instance == "")
                {
                    _TestInstanceExecuted = -1;
                }
                else
                {
                    _TestInstanceExecuted = Convert.ToInt32(val.instance);
                }

                if (val.stepfailed == "")
                {
                    _TestFailedAtStep = 0;
                }
                else
                {
                    _TestFailedAtStep = Convert.ToInt32(val.stepfailed);
                }

                if (val.start == "")
                {
                    _TestStart = new DateTime();
                }
                else
                {
                    _TestStart = DateTime.Parse(val.start);
                }

                if (val.end == "")
                {
                    _TestEnd = new DateTime();
                }
                else
                {
                    _TestEnd = DateTime.Parse(val.end);
                }

                _TestElapsed = val.elapsed;
            }

            // set the test setup log msgs
            _TestSetupLogMessages = new List<DlkLoggerRecord>();
            if (!IsNewFile)
            {
                var datasetuplgmsg = from doc in mXml.Descendants("testsetup")
                                     select new
                                     {
                                         logmessages = doc.Element("logmessages").Descendants()
                                     };
                foreach (var val in datasetuplgmsg)
                {
                    foreach (XElement mElm in val.logmessages)
                    {
                        DateTime mMessageDateTime = DateTime.Parse(mElm.Attribute("MessageDateTime").Value.ToString());
                        String mMessageType = mElm.Attribute("MessageType").Value.ToString();
                        String mMessageDetails = mElm.Attribute("MessageDetails").Value.ToString();
                        DlkLoggerRecord mLogRec = new DlkLoggerRecord(mMessageDateTime, mMessageType, mMessageDetails);
                        _TestSetupLogMessages.Add(mLogRec);
                    }
                }
            }

            // set the test teardown log msgs
            _TestTeardownLogMessages = new List<DlkLoggerRecord>();
            if (!IsNewFile)
            {
                var datateardwnlgmsg = from doc in mXml.Descendants("testteardown")
                                        select new
                                        {
                                            logmessages = doc.Element("logmessages").Descendants()
                                        };
                foreach (var val in datateardwnlgmsg)
                {
                    foreach (XElement mElm in val.logmessages)
                    {
                        DateTime mMessageDateTime = DateTime.Parse(mElm.Attribute("MessageDateTime").Value.ToString());
                        String mMessageType = mElm.Attribute("MessageType").Value.ToString();
                        String mMessageDetails = mElm.Attribute("MessageDetails").Value.ToString();
                        DlkLoggerRecord mLogRec = new DlkLoggerRecord(mMessageDateTime, mMessageType, mMessageDetails);
                        _TestSetupLogMessages.Add(mLogRec);
                    }
                }
            }

            /* Data */
            string datapath = Path.Combine(Path.GetDirectoryName(_TestPath), Path.GetFileNameWithoutExtension(_TestPath) + ".trd");
            _data = new DlkData(datapath);
            _InstanceCount = _data.Records.Any() ? (_data.Records.First().Values.Any() ? _data.Records.First().Values.Count : 1) : 1;

            // add test steps
            var datastep = from doc in mXml.Descendants("step")
                       select new
                       {
                           stepid = doc.Attribute("id").Value,
                           execute = doc.Element("execute").Value,
                           screen = doc.Element("screen").Value,
                           control = doc.Element("control").Value,
                           keyword = doc.Element("keyword").Value,
                           parameters = doc.Element("parameters").Descendants(),
                           delay = doc.Element("delay").Value,

                           status = !IsNewFile ? (String)doc.Element("status") ?? "" : "Not run",
                           start = !IsNewFile ? (String)doc.Element("start") ?? "" : "",
                           end = !IsNewFile ? (String)doc.Element("end") ?? "" : "",
                           elapsed = !IsNewFile ? (String)doc.Element("elapsed") ?? "" : "",

                           logmessages = doc.Element("logmessages").Descendants()
                       };
            foreach (var val in datastep)
            {
                DlkTestStepRecord mRec = new DlkTestStepRecord();
                mRec.mStepNumber = Convert.ToInt32(val.stepid);  
              
                if(val.execute == "true")
                {
                    mRec.mExecute = bool.TrueString;
                }
                else if (val.execute == "false")
                {
                    mRec.mExecute = bool.FalseString;
                }
                else
                {
                    mRec.mExecute = val.execute;
                }
                      
                mRec.mScreen = val.screen;
                mRec.mControl = val.control;
                mRec.mKeyword = val.keyword;
                mRec.mStepDelay = Convert.ToInt32(val.delay);

                mRec.mStepStatus = val.status;
                if (val.start != "")
                {
                    mRec.mStepStart = DateTime.Parse(val.start);
                }
                if (val.end != "")
                {
                    mRec.mStepEnd = DateTime.Parse(val.end);
                }
                mRec.mStepElapsedTime = val.elapsed;

                /* step parmeters: 1 step parameter for each test instance */
                mRec.mParameters = new List<String>();
                if (val.parameters != null && val.parameters.Any())
                {
                    for (int idx = 0; idx < _InstanceCount; idx++)
                    {
                        string parameter = "";
                        if (val.parameters.Count() == 1 || idx + 1 > val.parameters.Count())
                            parameter = val.parameters.First().Value.ToString();
                        else
                            parameter = val.parameters.ElementAt(idx).Value.ToString();
                        //Backward Compatibility
                        string mControl = "";
                        if (mRec.mControl == "Function")
                        {
                            mControl = "Function";
                        }
                        else if (mRec.mControl == "")
                        {
                            mControl = mRec.mScreen;
                        }
                        else
                        {
                            mControl = DlkDynamicObjectStoreHandler.GetControlType(mRec.mScreen, mRec.mControl);
                        }
                        int keywordParamCount = DlkAssemblyKeywordHandler.GetControlKeywordParameters(mControl, mRec.mKeyword).Count;

                        //Fact: There is no old param with both new delimiter(q7*;) and |
                        //if new delimiter is already used and | is part of paramValue it will not go here
                        if (keywordParamCount > 1 && parameter.Contains('|') && !parameter.Contains(DlkTestStepRecord.globalParamDelimiter))
                        {
                            parameter = parameter.Replace("|", DlkTestStepRecord.globalParamDelimiter);
                        }
                        mRec.mParameters.Add(parameter);
                    }
                }

                List<DlkLoggerRecord> mLogRecs = new List<DlkLoggerRecord>();
                if (!IsNewFile)
                {
                    foreach (XElement mElm in val.logmessages)
                    {
                        DateTime mMessageDateTime = DateTime.Parse(mElm.Attribute("MessageDateTime").Value.ToString());
                        String mMessageType = mElm.Attribute("MessageType").Value.ToString();
                        String mMessageDetails = mElm.Attribute("MessageDetails").Value.ToString();
                        DlkLoggerRecord mLogRec = new DlkLoggerRecord(mMessageDateTime, mMessageType, mMessageDetails);
                        mLogRecs.Add(mLogRec);
                    }
                }
                mRec.mStepLogMessages = mLogRecs;
                _TestSteps.Add(mRec);
            }


            /* Get attached links if any */
            var links = from doc in mXml.Descendants("link")
                           select new
                           {
                               id = doc.Attribute("id").Value,
                               name = doc.Attribute("name").Value,
                               link = doc.Attribute("path").Value
                           };

            foreach (var val in links)
            {
                _links.Add(new DlkTestLinkRecord(val.id, val.name, val.link));
            }

            /* Get tags if any */
            var tags = from doc in mXml.Descendants("tag")
                       select new
                       {
                           id = doc.Attribute("id").Value,
                           name = doc.Attribute("name")!= null ? doc.Attribute("name").Value : "",
                       };

            List<DlkTag> allTags = DlkTag.LoadAllTags();
            foreach (var val in tags)
            {
                var tg = allTags.FirstOrDefault(x => x.Id == val.id);
                if(tg != null)
                {
                    _tags.Add(new DlkTag(tg.Id, tg.Name, tg.Conflicts, tg.Description));
                }
                else
                {
                    _tags.Add(new DlkTag(val.id, val.name, "", ""));
                }
            }

            if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct)
            {
                DecryptParameters();
            }

            ConvertTestStepScreens();
        }

        /// <summary>
        /// Convert loaded test steps screen if show app name is enabled
        /// </summary>
        private void ConvertTestStepScreens()
        {
            if (DlkEnvironment.IsShowAppNameProduct && DlkEnvironment.IsShowAppNameEnabled())
            {
                foreach (DlkTestStepRecord step in mTestSteps)
                {
                    if (step.mScreen == "")
                        continue;

                    int index = DlkDynamicObjectStoreHandler.Screens.IndexOf(step.mScreen);
                    //screen name yet converted to appid when add hoc run is cancelled before it reaches the login or during loading of browser
                    if (index == -1)
                        break;

                    step.mScreen = DlkDynamicObjectStoreHandler.Alias[index];     
                }
            }
        }

        /// <summary>
        /// Decrypt password masked enabled parameters in DlkTest object
        /// </summary>
        private void DecryptParameters()
        {
            string decryptedText = "";
            foreach (DlkTestStepRecord _mStep in mTestSteps)
            {
                DlkPasswordControl control = DlkPasswordMaskedRecord.GetMatchedControl(_mStep);
                if (control != null)
                {
                    DlkUtility.DlkDecryptionHelper utility = new DlkUtility.DlkDecryptionHelper();
                    string[] arrParameters = null;
                    _mStep.mPasswordParameters = new List<string>();
                    
                    for (int i = 0; i < _mStep.mParameters.Count(); i++)
                    {
                        arrParameters = _mStep.mParameters[i].Split(new string[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);
                        for (int arrIndex = 0; arrIndex < arrParameters.Count(); arrIndex++)
                        {
                            DlkPasswordParameter _control = control.Parameters.SingleOrDefault(s => s.Index == arrIndex);

                            if (_control != null)
                            {
                                string originalText = arrParameters[arrIndex];
                                string passwordText = "";
                                if ((!DlkData.IsDataDrivenParam(originalText) && utility.IsDecrpytable(originalText)) ||
                                    (!DlkData.IsOutPutVariableParam(originalText) && utility.IsDecrpytable(originalText)) ||
                                    (!DlkData.IsGlobalVarParam(originalText) && utility.IsDecrpytable(originalText)))
                                {
                                    decryptedText = utility.DecryptByteArrayToString(Convert.FromBase64String(originalText));
                                    arrParameters[arrIndex] = decryptedText;
                                    passwordText = decryptedText;
                                }
                                else
                                    passwordText = originalText;

                                if (i == 0) //get decrypted parameter in 1st iteration only
                                    _mStep.mPasswordParameters.Add(passwordText);
                            }
                            else
                            {
                                if (i == 0) //get decrypted parameter in 1st iteration only
                                    _mStep.mPasswordParameters.Add("");
                            }
                        }

                        _mStep.mParameters[i] = string.Join(DlkTestStepRecord.globalParamDelimiter,arrParameters);
                    }
                }
            }
        }

        /// <summary>
        /// Checks parameter if masked.. parameter can be masked in test editor only
        /// </summary>
        /// <param name="parameter">keyword parameter</param>
        /// <returns>true/false</returns>
        private bool IsMaskedText(string parameter)
        {
            int count = 0;
            foreach (char c in parameter.ToCharArray())
            {
                if (c.ToString() == DlkPasswordMaskedRecord.PasswordChar)
                    count++;
            }
            return count == parameter.Length;
        }

        public static bool IsValidTest(String TestFilePath)
        {
            bool bValid = false;
            try
            {
                // load the XML
                XDocument myXml = XDocument.Load(TestFilePath);

                // set the root test elements
                var dataroot = from doc in myXml.Descendants("test")
                               select new
                               {
                                   name = doc.Element("name").Value,
                                   description = doc.Element("description").Value,

                                   instance = (String)doc.Element("instance") ?? "",
                                   status = (String)doc.Element("status") ?? "",
                                   stepfailed = (String)doc.Element("stepfailed") ?? "",
                                   start = (String)doc.Element("start") ?? "",
                                   end = (String)doc.Element("end") ?? "",
                                   elapsed = (String)doc.Element("elapsed") ?? ""
                               };
                foreach (var val in dataroot)
                {
                    if (val.instance != null)
                    {
                        bValid = true;
                    }
                }
            }
            catch
            {
                bValid = false;
            }
            return bValid;
        }

        /// <summary>
        /// used to update (in memory) the test status; note we want this to be failed until a test completes successfully and can be marked passed
        /// </summary>
        /// <param name="Status"></param>
        public void UpdateTestStatus(String Status)
        {
            _TestStatus = Status;
        }

        /// <summary>
        /// used to update (in memory) the test with the specified instance
        /// </summary>
        /// <param name="Instance"></param>
        public void UpdateTestInstanceExecuted(int Instance)
        {
            _TestInstanceExecuted = Instance;
        }

        /// <summary>
        /// Update (in memory) the test setup log details
        /// </summary>
        /// <param name="LoggerRecords"></param>
        public void UpdateTestSetupLogMessages(List<DlkLoggerRecord> LoggerRecords)
        {
            _TestSetupLogMessages = LoggerRecords;
        }

        /// <summary>
        /// Update (in memory) the test teardown log details
        /// </summary>
        /// <param name="LoggerRecords"></param>
        public void UpdateTestTeardownLogMessages(List<DlkLoggerRecord> LoggerRecords)
        {
            _TestTeardownLogMessages = LoggerRecords;
        }

        /// <summary>
        /// Updates (in memory) the test steps execution data
        /// </summary>
        /// <param name="iStep"></param>
        /// <param name="LoggerRecords"></param>
        public void UpdateTestStepExecutionData(
            int iStep, DateTime StepStart, DateTime StepEnd,
            String StepStatus,
            List<DlkLoggerRecord> LoggerRecords)
        {
            for (int i = 0; i < _TestSteps.Count; i++)
            {
                if (iStep == _TestSteps[i].mStepNumber)
                {
                    _TestSteps[i].mStepStatus = StepStatus;
                    _TestSteps[i].mStepLogMessages = LoggerRecords;
                    _TestSteps[i].mStepStart = StepStart;
                    _TestSteps[i].mStepEnd = StepEnd;

                    TimeSpan ts = StepEnd - StepStart;
                    _TestSteps[i].mStepElapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                    break;
                }
            }
        }

        /// <summary>
        /// Updates the start date
        /// </summary>
        /// <param name="TestStart"></param>
        public void UpdateTestStart(DateTime TestStart)
        {
            _TestStart = TestStart;
        }

        /// <summary>
        /// Updates the start end
        /// </summary>
        /// <param name="TestStart"></param>
        public void UpdateTestEnd(DateTime TestEnd)
        {
            _TestEnd = TestEnd;
            TimeSpan ts = mTestEnd - mTestStart;
            _TestElapsed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        /// <summary>
        /// Updates the step that the test failed during
        /// </summary>
        /// <param name="TestStepNumber"></param>
        public void UpdateTestFailedAtStep(int TestStepNumber)
        {
            _TestFailedAtStep = TestStepNumber;
        }

        /// <summary>
        /// Writes the test stored in memory to the workinf folder
        /// </summary>
        public void WriteTestToTestResults()
        {
            string path = "";
            try
            {
                FileInfo fi = new FileInfo(mTestPath);
                //string uniqueid = string.IsNullOrEmpty(DlkEnvironment.mCurrentTestIdentifier) ? "" : "_" + DlkEnvironment.mCurrentTestIdentifier;
                _Identifier = string.IsNullOrEmpty(DlkEnvironment.mCurrentTestIdentifier) ? "" : DlkEnvironment.mCurrentTestIdentifier;

                path = DlkEnvironment.mDirTestResultsCurrent + Path.GetFileNameWithoutExtension(fi.Name) + "_"
                    + mTestInstanceExecuted + Path.GetExtension(fi.Name);

                int copy = 0;
                while (File.Exists(path))
                {
                    path = DlkEnvironment.mDirTestResultsCurrent + Path.GetFileNameWithoutExtension(fi.Name) + "_"
                        + mTestInstanceExecuted + "_" + ++copy + Path.GetExtension(fi.Name);
                }
                ConvertTestStepScreens();
                WriteTest(path, true);
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile($"[Write Test Result] Error in writing test result file for '{path}'.", ex);
            }
        }

        /// <summary>
        /// Writes the test stored in memory to the specified path
        /// </summary>
        /// <param name="TestPath"></param>
        public void WriteTest(String TestPath, Boolean OverwriteIfExists)
        {
            try
            {
                if (File.Exists(TestPath))
                {
                    if (OverwriteIfExists)
                    {
                        FileInfo fi = new FileInfo(TestPath);
                        fi.IsReadOnly = false;
                        File.Delete(TestPath);
                    }
                    else
                    {
                        throw new Exception("File exists. Please delete before writing or instruct to overwrite. File: " + TestPath);
                    }
                }

                // create Test Setup log msgs
                List<XElement> mRecs = new List<XElement>();
                foreach (DlkLoggerRecord mLogMsg in mTestSetupLogMessages)
                {
                    XElement mElm = new XElement("logmessage",
                        new XAttribute("MessageDateTime", mLogMsg.mMessageDateTime.ToString()),
                        new XAttribute("MessageType", mLogMsg.mMessageType),
                        new XAttribute("MessageDetails", mLogMsg.mMessageDetails));
                    mRecs.Add(mElm);
                }
                XElement mElmTestSetupLogMsgs = new XElement("testsetup",
                        new XElement("logmessages", mRecs)
                        );


                // create Test Teardown log msgs
                mRecs = new List<XElement>();
                foreach (DlkLoggerRecord mLogMsg in mTestTeardownLogMessages)
                {
                    XElement mElm = new XElement("logmessage",
                        new XAttribute("MessageDateTime", mLogMsg.mMessageDateTime.ToString()),
                        new XAttribute("MessageType", mLogMsg.mMessageType),
                        new XAttribute("MessageDetails", mLogMsg.mMessageDetails));
                    mRecs.Add(mElm);
                }
                XElement mElmTestTeardownLogMsgs = new XElement("testteardown",
                        new XElement("logmessages", mRecs)
                        );


                // create TestSteps
                mRecs = new List<XElement>();
                List<XElement> mLgRecs = new List<XElement>();
                List<XElement> mParamRecs = new List<XElement>();
                
                foreach (DlkTestStepRecord ts in mTestSteps)
                {
                    mLgRecs = new List<XElement>();
                    foreach (DlkLoggerRecord mLogMsg in ts.mStepLogMessages)
                    {
                        XElement mElm = new XElement("logmessage",
                            new XAttribute("MessageDateTime", mLogMsg.mMessageDateTime.ToString()),
                            new XAttribute("MessageType", mLogMsg.mMessageType),
                            new XAttribute("MessageDetails", mLogMsg.mMessageDetails));
                        mLgRecs.Add(mElm);
                    }
                    XElement mLogMsgs = new XElement("logmessages", mLgRecs);

                    mParamRecs = new List<XElement>();
                    string mScreen = ts.mScreen;
                    if (DlkEnvironment.IsShowAppNameProduct && !String.IsNullOrEmpty(ts.mScreen))
                    {
                        int index = DlkDynamicObjectStoreHandler.Alias.IndexOf(ts.mScreen);

                        if (index > -1)
                            mScreen = DlkDynamicObjectStoreHandler.Screens[index];
                    }

                    //Encrypt password masked enabled parameters for specific products
                    if (DlkPasswordMaskedRecord.IsPasswordMaskedProduct && ts.mPasswordParameters != null)
                    {
                        string[] _mParam;
                        DlkUtility.DlkEncryptionHelper utility = new DlkUtility.DlkEncryptionHelper();

                        for (int index = 0; index < ts.mParameters.Count(); index++)
                        {
                            _mParam = ts.mParameters[index].Split(new string[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);

                            for (int i = 0; i < ts.mPasswordParameters.Count(); i++)
                            {
                                if ((DlkPasswordMaskedRecord.IsMaskedParameter(ts, i) && !DlkData.IsDataDrivenParam(_mParam[i])) ||
                                    (DlkPasswordMaskedRecord.IsMaskedParameter(ts, i) && !DlkData.IsOutPutVariableParam(_mParam[i])) ||
                                    (DlkPasswordMaskedRecord.IsMaskedParameter(ts, i) && !DlkData.IsGlobalVarParam(_mParam[i])))
                                {
                                    if (!IsMaskedText(_mParam[i]))
                                        _mParam[i] = Convert.ToBase64String(utility.EncryptStringToByteArray(_mParam[i]));

                                    else
                                        _mParam[i] = Convert.ToBase64String(utility.EncryptStringToByteArray(ts.mPasswordParameters[i]));
                                }
                                else
                                    _mParam[i] = _mParam[i];
                            }
                            XElement mElm = new XElement("parameter", string.Join(DlkTestStepRecord.globalParamDelimiter, _mParam));
                            mParamRecs.Add(mElm);
                        }
                    }
                    else
                    {
                        foreach (String mParam in ts.mParameters)
                        {
                            XElement mElm = new XElement("parameter", mParam);
                            mParamRecs.Add(mElm);
                        }
                    }

                    XElement resultrow = new XElement("step",
                        new XAttribute("id", ts.mStepNumber.ToString()),
                        new XElement("execute", ts.mExecute.ToString()),
                        new XElement("screen", mScreen),
                        new XElement("control", ts.mControl),
                        new XElement("keyword", ts.mKeyword),
                        new XElement("parameters", mParamRecs),
                        new XElement("delay", ts.mStepDelay.ToString()),
                        new XElement("status", ts.mStepStatus),
                        new XElement("logmessages", mLgRecs),
                        new XElement("start", ts.mStepStart.ToString()),
                        new XElement("end", ts.mStepEnd.ToString()),
                        new XElement("elapsed", ts.mStepElapsedTime)
                        );
                    mRecs.Add(resultrow);
                }
                XElement mElmSteps = new XElement("steps", mRecs);


                /* Write Test Link if any */
                List<XElement> lstLinksNode = new List<XElement>();

                foreach (DlkTestLinkRecord lnk in mLinks)
                {
                    XElement linknode = new XElement("link",
                        new XAttribute("id", lnk.Id),
                        new XAttribute("name", lnk.DisplayName),
                        new XAttribute("path", lnk.LinkPath)
                        );
                    lstLinksNode.Add(linknode);
                }

                XElement mElmsLinks = new XElement("links", lstLinksNode);

                /* Write Tags if any */
                List<XElement> lstTagsNode = new List<XElement>();

                foreach (DlkTag tag in mTags)
                {
                    XElement tagnode = new XElement("tag",
                        new XAttribute("id", tag.Id),
                        new XAttribute("name", tag.Name)
                        );
                    lstTagsNode.Add(tagnode);
                }
                XElement mElmsTags = new XElement("tags", lstTagsNode);

                XElement ElmRoot = new XElement("test",
                    new XElement("name", mTestName),
                    new XElement("file", mTestPath),
                    new XElement("identifier", mIdentifier),
                    new XElement("description", mTestDescription),
                    new XElement("author", mTestAuthor),
                    new XElement("instance", mTestInstanceExecuted.ToString()),
#if ATK_RELEASE
                new XElement("continueonerror", mContinueOnError.ToString()),
#else
#endif
                new XElement("status", mTestStatus),
                    new XElement("stepfailed", mTestFailedAtStep.ToString()),
                    new XElement("start", mTestStart.ToString()),
                    new XElement("end", mTestEnd.ToString()),
                    new XElement("elapsed", mTestElapsed),
                    mElmTestSetupLogMsgs,
                    mElmTestTeardownLogMsgs,
                    mElmsLinks,
                    mElmsTags,
                    mElmSteps
                    );
                XDocument xDoc = new XDocument(ElmRoot);

                xDoc.Save(TestPath);
                _data.Save(Path.Combine(Path.GetDirectoryName(TestPath), Path.GetFileNameWithoutExtension(TestPath) + ".trd"));
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile($"[Write Test Result] Error in writing test result file for '{TestPath}'.", ex);
            }
        }

        public string GetOriginalTestPath()
        {
            XDocument doc = XDocument.Load(_TestPath);
            var test = from itm in doc.Descendants("test")
                       select new
                       {
                           file = itm.Element("file").Value
                       };
            return test.First().file;
        }
    }

    /// <summary>
    /// Class to compare Tests based on path
    /// </summary>
    public class DlkTestCompare : IEqualityComparer<DlkTest>
    {
        public bool Equals(DlkTest x, DlkTest y)
        {
            if (x != null && y != null)
            {
                return x.mTestPath == y.mTestPath;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(DlkTest obj)
        {
            return 0;
        }
    }

}
