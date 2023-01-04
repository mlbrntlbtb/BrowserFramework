using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using HRSmartLib.System;
using System.Threading;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using HRSmartLib.DlkSystem;

namespace HRSmartLib.LatestVersion.DlkFunctions
{
     [Component("Login")]
    public static class DlkLogin
    {
        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        public static void Login(String Url, String User, String Password, String Database)
        {
            if (!Url.Equals(string.Empty))
            {
                DlkEnvironment.AutoDriver.Url = Url;
            }

            // use the object store definitions 
            if (User != "")
            {
                IKeywordHandler handler = DlkCommon.DlkCommonFunction.GetKeywordHandler();

                handler.ExecuteKeyword("Login", "User", "Set", new String[] { User });
                handler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                //DlkGovWinKeywordHandler.ExecuteKeyword("Login", "Database", "Select", new String[] { Database });
                Thread.Sleep(250);
                handler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                

                //TODO: wait to check successful login
                //Wait for Sidebar to appear (denotes that the main page has loaded)
                //DlkObjectStoreFileControlRecord osRec = DlkDynamicObjectStoreHandler.GetControlRecord("Main", "SideBar");
                //DlkSideBar sideBar = new DlkSideBar(osRec.mKey, osRec.mSearchMethod, osRec.mSearchParameters);
                //if (!sideBar.Exists(60))
                //{
                //    DlkLogger.LogWarning("Login() : Main page not loaded within timeout.");
                //}
                //else
                //{
                //    DlkLogger.LogInfo("Login() passed.");
                //}

            }
        }

    }
}
