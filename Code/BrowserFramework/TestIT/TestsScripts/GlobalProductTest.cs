using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using MaconomyTouchLib.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TestIT.Envs;
using TestIT.Sys;

namespace TestIT.TestRuns
{
    public class GlobalProductTest : TestScript
    {
        private ObjectStoreHandler ObjectStoreHandler
        {
            get { return ObjectStoreHandler.Instance; }
        }

        public override bool TestExecute(out string ErrorMessage, string envID, string testPath)
        {
            ErrorMessage = string.Empty;
            bool ret = true;
            DlkEnvironment.SetContext("WEBVIEW");
            DlkTest mTest = null;

            Envs.Environment myEnv = EnvMaconomyTouch.Environments.First(x => x.Id == envID);
            //STEP 1 - ***LOGIN***
            EnvMaconomyTouch.Login(GlobalProductRun.PRODUCT_NAME, myEnv.Url, myEnv.User, myEnv.Password, myEnv.Database, myEnv.PIN);
            Thread.Sleep(5000);

            //load xml and loop over each step

            mTest = new DlkTest(testPath);
            mTest.UpdateTestStatus("failed");
            mTest.UpdateTestStart(DateTime.Now);
            mTest.UpdateTestInstanceExecuted(1);

            DlkData.SubstituteExecuteDataVariables(mTest);
            DlkData.SubstituteDataVariables(mTest);

            DateTime mStart = DateTime.Now;
            DateTime mEnd = DateTime.Now;

            foreach (var step in mTest.mTestSteps)
            {

                try
                {
                    if (step.mExecute.ToLower().Equals("true"))
                    {
                        String[] mTestParams = step.mParameterOrigString.Split(new[] { DlkTestStepRecord.globalParamDelimiter }, StringSplitOptions.None);

                        // make all variable substitutions
                        mTestParams = DlkVariable.SubstituteVariables(mTestParams);

                        if ((!string.IsNullOrEmpty(step.mScreen) && string.IsNullOrEmpty(step.mControl))
                            || (step.mScreen.ToLower() == "function") || (step.mControl.ToLower() == "function"))
                        {
                            mStart = DateTime.Now;
                            DlkMaconomyTouchFunctionHandler.ExecuteFunction(step.mScreen, step.mControl, step.mKeyword, mTestParams);
                            mEnd = DateTime.Now;
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            mStart = DateTime.Now;
                            DlkMaconomyTouchKeywordHandler.ExecuteKeyword(step.mScreen, step.mControl, step.mKeyword, mTestParams);
                            mEnd = DateTime.Now;
                            Thread.Sleep(2000);
                        }

                        mTest.UpdateTestStepExecutionData(Convert.ToInt32(step.mStepNumber), mStart, mEnd, "passed", new List<DlkLoggerRecord>());
                    }
                    else
                    {
                        mTest.UpdateTestStepExecutionData(Convert.ToInt32(step.mStepNumber), mStart, mEnd, "not run", new List<DlkLoggerRecord>());
                    }
                }
                catch (Exception ex)
                {
                    mTest.UpdateTestStepExecutionData(Convert.ToInt32(step.mStepNumber), mStart, mEnd, "failed", new List<DlkLoggerRecord>());
                    mTest.UpdateTestEnd(DateTime.Now);
                    mTest.WriteTestToTestResults();
                    throw new Exception(ex.InnerException.Message);
                }
            }

            mTest.UpdateTestEnd(DateTime.Now);
            mTest.WriteTestToTestResults();

            return ret;
        }

    }
}