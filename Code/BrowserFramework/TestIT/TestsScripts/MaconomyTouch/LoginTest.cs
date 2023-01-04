using CommonLib.DlkSystem;
using MaconomyTouchLib.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestIT.Envs;
using TestIT.Sys;
using TestIT.TestRuns;

namespace TestIT.Tests.MaconomyTouch
{
    public class LoginTest : TestScript
    {
        private ObjectStoreHandler ObjectStoreHandler
        {
            get { return ObjectStoreHandler.Instance; }
        }

        public override bool TestExecute(out string ErrorMessage, string envID, string TestPath)
        {
            ErrorMessage = string.Empty;
            bool ret = true;
            DlkEnvironment.SetContext("WEBVIEW");

            try
            {
                Envs.Environment myEnv = EnvMaconomyTouch.Environments.First(x => x.Id == envID);
                //STEP 1 - ***LOGIN***
                EnvMaconomyTouch.Login(GlobalProductRun.PRODUCT_NAME, myEnv.Url, myEnv.User, myEnv.Password, myEnv.Database, myEnv.PIN);
                Thread.Sleep(5000);

                //STEP 2
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Main", "ShowMenu", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 3
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("SlidingMenu", "MileageSheet", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 4
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Mileage Sheet", "MileageSheetsListDetails", "GetRowCount", new String[] { "RowCount" });
                Thread.Sleep(1000);

                //STEP 5
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Mileage Sheet", "MileageSheetsListDetails", "SelectByRow", new String[] { "2" });
                Thread.Sleep(1000);

                /* -- LOGOUT -- */

                //STEP 6
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("Main", "ShowMenu", "Click", new String[] { "" });
                Thread.Sleep(1000);

                //STEP 7
                DlkMaconomyTouchKeywordHandler.ExecuteKeyword("SlidingMenu", "Log Out", "Click", new String[] { "" });
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}
