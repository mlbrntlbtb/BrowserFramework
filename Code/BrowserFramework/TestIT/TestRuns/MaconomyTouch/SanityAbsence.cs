using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using TestIT.Envs;
using TestIT.Sys;
using TestIT.Tests.MaconomyTouch;
using TestIT.TestsScripts.MaconomyTouch;

namespace TestIT.TestRuns
{
    class SanityAbsence: TestRun
    {
        public SanityAbsence(List<String> testPath, string emailAddresses) : base(testPath, emailAddresses) { }

        private ObjectStoreHandler ObjectStoreHandler
        {
            get { return ObjectStoreHandler.Instance; }
        }

        public override void ExecuteTests(List<String> TestPaths)
        {
            foreach (string path in TestPaths)
            {
                ObjectStoreHandler.ProductName = GlobalProductRun.PRODUCT_NAME;
                ObjectStoreHandler.InitializeForUnitTest();
                Thread.Sleep(2000);
                while (ObjectStoreHandler.StillLoading)
                {
                    Thread.Sleep(2000);
                }
                EnvMaconomyTouch.InitializeEnvironment();
                new SanityAbsenceTest().Run(this, GlobalProductRun.BROWSER_TYPE,
                                                  GlobalProductRun.ENVIRONMENT,
                                                  GlobalProductRun.EMULATOR_NAME,
                                                  GlobalProductRun.INCLUDE_IN_RESULTS,
                                                  GlobalProductRun.PRODUCT_NAME);

            }
        }
    }
}
