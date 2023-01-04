using CommonLib.DlkControls;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using MaconomyTouchLib.DlkControls;
using MaconomyTouchLib.System;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using TestIT.Sys;
using TestIT.TestRuns;

namespace TestIT.Envs
{
    public class EnvMaconomyTouch
    {
        private ObjectStoreHandler ObjectStoreHandler
        {
            get { return ObjectStoreHandler.Instance; }
        }

        #region FUNCTIONS
        public static void InitializeEnvironment()
        {
            /* Set Root Path based from executing binary and initialize directories */
            string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mRootPath = Directory.GetParent(binDir).FullName;
            while (new DirectoryInfo(mRootPath).GetDirectories()
                .Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                mRootPath = Directory.GetParent(mRootPath).FullName;
            }

            String mLibrary = Path.Combine(mRootPath + @"\Tools\TestRunner\" + GlobalProductRun.PRODUCT_LIBRARY);
            DlkEnvironment.InitializeEnvironment(GlobalProductRun.PRODUCT_NAME, mRootPath, mLibrary);
            DlkDynamicObjectStoreHandler.Instance.Initialize();
        }
        public static void Login(String ProductName, String Url, String User, String Password, String Database, String Pin)
        {
            DlkMaconomyTouchFunctionHandler.Login(Url, User, Password, Database, Pin);
        }
        #endregion

        #region ENVIRONMENTS
        public static Environment[] Environments = new Environment[]
        {
                new Environment()
                {
                    Id = "Default",
                    Url = "https://makapt9vs.ads.deltek.com/30_m20p100rest",
                    User = "UNIT TEST",
                    Password = "Ppuppumac!",
                    Database = "",
                    PIN = "167998",
                }
                
                // Copy and uncomment below to add new record. Do not delete this block, just copy.
                
                //,new MobileRecord()
                //{
                    //Id = "new",
                    //Url = "",
                    //User = "",
                    //Password = "",
                    //Database = "",
                    //PIN = "",
                //}
        };
        #endregion
    }

    public class Environment
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string PIN { get; set; }
    }
}
