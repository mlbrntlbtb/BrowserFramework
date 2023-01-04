using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using GenerateDashboard.DlkRecords;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonLib.DlkUtility;

namespace GenerateDashboard.DlkCore
{
    static class DlkGenerateSuiteDetails
    {
        /// <summary>
        /// generate the suite details pages in xml format
        /// </summary>
        public static void Execute(List<DlkTestSuiteStatsRecord> LatestSuiteStatsRecords)
        {
            try
            {
                // create a Dashboard Suite Details xml for each suite result file
                int mId = 0;
                foreach (DlkTestSuiteStatsRecord mLatestSuiteRec in LatestSuiteStatsRecords)
                {
                    mId++;
                    List<XElement> mElms = new List<XElement>();

                    // load the suite result created by the execution engine and then add other details into a new file
                    string mResultsPath = mLatestSuiteRec.mTestSuiteResultPath;
                    List<DlkExecutionQueueRecord> mTestResults = DlkTestSuiteResultsFileHandler.GetResults(mLatestSuiteRec.mTestSuitePath, mLatestSuiteRec.mExecutionDate);
                    FileInfo[] resultFiles = new DirectoryInfo(mResultsPath).GetFiles("*.xml");
                    foreach (DlkExecutionQueueRecord mTestResult in mTestResults)
                    {
                        //String mFailedStepType = "";
                        String mFailedStepDetails = "";
                        string mFile = resultFiles.Where(x => x.Name == mTestResult.file).FirstOrDefault().FullName;
                        if (File.Exists(mFile))
                        {
                            DlkTest mTest = new DlkTest(mFile);
                            if (mTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONMSG").Count > 0)
                            {
                                mFailedStepDetails = mTest.mTestSetupLogMessages.FindAll(
                                    x => x.mMessageType == "EXCEPTIONMSG").First().mMessageDetails;
                            }
                            else
                            {
                                if (mTest.mTestFailedAtStep > 0)
                                {
                                    if (mTest.mTestSteps[mTest.mTestFailedAtStep - 1].mStepLogMessages.FindAll(x => x.mMessageType == "EXCEPTIONMSG").Count > 0)
                                    {
                                        mFailedStepDetails = mTest.mTestSteps[mTest.mTestFailedAtStep - 1].mStepLogMessages.FindAll(
                                        x => x.mMessageType == "EXCEPTIONMSG").First().mMessageDetails;
                                    }
                                }
                            }

                            /*Check Fail type */
                            //if (!String.IsNullOrEmpty(mFailedStepDetails))
                            //{
                            //    if (mFailedStepDetails.ToLower().Contains("verify"))
                            //        mFailedStepType = "TestStepAssertion";
                            //    else
                            //        mFailedStepType = "TestStepAction";
                            //}

                            //Retrieve test database and browser from saved logged messages
                            String mDatabase = "";
                            String mBrowser = "";
                            if (mTest.mTestSetupLogMessages.FindAll(x => x.mMessageType == "SYSTEMINFO").Count > 0)
                            {
                                mDatabase = mTest.mTestSetupLogMessages.FindAll(
                                    x => x.mMessageType == "SYSTEMINFO").Where(
                                    x => x.mMessageDetails.Contains("Login Database:")).First().mMessageDetails.
                                    Replace("Login Database: ", "");

                                mBrowser = mTest.mTestSetupLogMessages.FindAll(
                                    x => x.mMessageType == "SYSTEMINFO").Where(
                                    x => x.mMessageDetails.Contains("Browser:")).First().mMessageDetails.
                                    Replace("Browser: ", "");
                            }

                            XElement mElm = new XElement("testresult",
                            new XAttribute("testname", mTestResult.name),
                            new XAttribute("teststatus", mTestResult.teststatus),
                            new XAttribute("database", mDatabase),
                            new XAttribute("browser", mBrowser),
                            new XAttribute("testinstance", mTestResult.instance),
                            new XAttribute("testpath", mFile),
                            new XAttribute("teststart", mTest.mTestStart.ToString()),
                            new XAttribute("testend", mTest.mTestEnd.ToString()),
                            new XAttribute("duration", mTest.mTestElapsed),
                            new XAttribute("executiondate", mTestResult.executiondate),
                            new XAttribute("failedteststep", mTest.mTestFailedAtStep > 0 ? mTest.mTestFailedAtStep.ToString() : ""),
                            new XAttribute("failedteststepdetails", mFailedStepDetails)
                            );
                            mElms.Add(mElm);
                        }
                    }

                    XElement mRoot = new XElement("suitedetails",
                        new XAttribute("parent", "file:///" + Path.Combine(DlkEnvironment.mDirDataFrameworkDashboardRepositoryPublished.Replace("\\", "/"), "Summary.xml")),
                        new XElement("suiteinfo",
                            new XAttribute("suitename", mLatestSuiteRec.mTestSuiteName),
                            new XAttribute("suitepath", mLatestSuiteRec.mTestSuitePath.Replace(DlkEnvironment.mDirData, @"\")),
                            new XAttribute("suiteresultpath", mLatestSuiteRec.mTestSuiteResultPath),
                            new XAttribute("testcount", mLatestSuiteRec.mTestCount),
                            new XAttribute("testsexecuted", mLatestSuiteRec.mTestsExecuted),
                            new XAttribute("testspassed", mLatestSuiteRec.mTestsPassed),
                            new XAttribute("testsfailed", mLatestSuiteRec.mTestsFailed),
                            new XAttribute("testsfailedpercentage", mLatestSuiteRec.mTestsFailedPercentage),
                            new XAttribute("executiondate", DlkGenerateDashboard.GetDateStringFromPath(mLatestSuiteRec.mTestSuiteResultPath))),
                            new XElement("testresults", mElms)
                            );

                    XDocument mDoc = new XDocument(mRoot);
                    mDoc.AddFirst(new XProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + Path.Combine(DlkEnvironment.mDirProductsRoot, "Common\\Dashboard") + "\\SuiteDetails.xsl\""));
                    mDoc.Save(DlkEnvironment.mDirDataFrameworkDashboardRepositoryWorking + "File_" + mId + ".xml");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile("[Generate Dashboard] Error in creating suite detail file", ex);
            }
        }
    }
}