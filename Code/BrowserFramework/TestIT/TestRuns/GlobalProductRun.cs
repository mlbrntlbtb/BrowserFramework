using System;
using System.Collections.Generic;
using System.Threading;
using TestIT.Envs;
using TestIT.Sys;

namespace TestIT.TestRuns
{ 
    public class GlobalProductRun : TestRun
    {
        public static string PRODUCT_NAME = "MaconomyTouch";
        public static Driver.Browser BROWSER_TYPE = Driver.Browser.MOBILE;
        public static string ENVIRONMENT = "Default";
        public static string EMULATOR_NAME = "TESTIT_Nexus6PEmulator_Nougat7";
        public static bool INCLUDE_IN_RESULTS = true;
        public static string PRODUCT_LIBRARY = "MaconomyTouchLib.dll";

        public GlobalProductRun(List<String> testPath, string emailAddresses) : base(testPath, emailAddresses) { }

        private ObjectStoreHandler ObjectStoreHandler
        {
            get { return ObjectStoreHandler.Instance; }
        }

        public override void ExecuteTests(List<String> TestPaths)
        {
            foreach(string path in TestPaths)
            {
                try
                {
                    ObjectStoreHandler.ProductName = GlobalProductRun.PRODUCT_NAME;
                    ObjectStoreHandler.InitializeForUnitTest();
                    Thread.Sleep(2000);
                    while (ObjectStoreHandler.StillLoading)
                    {
                        Thread.Sleep(2000);
                    }
                    EnvMaconomyTouch.InitializeEnvironment();
                    new GlobalProductTest().Run(this, GlobalProductRun.BROWSER_TYPE,
                                                      GlobalProductRun.ENVIRONMENT,
                                                      GlobalProductRun.EMULATOR_NAME,
                                                      GlobalProductRun.INCLUDE_IN_RESULTS,
                                                      GlobalProductRun.PRODUCT_NAME,
                                                      path);
                }
                catch 
                {
                    //proceed to next test
                }
            }
        }
    }
}
