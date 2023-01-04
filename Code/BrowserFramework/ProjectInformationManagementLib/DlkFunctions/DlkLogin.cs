using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using ProjectInformationManagementLib.System;
using System.Threading;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace ProjectInformationManagementLib.DlkFunctions
{
    [Component("Login")]
    public static class DlkLogin
    {
        /// <summary>
        /// Performs a login to ProjectInformationManagement
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        public static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;

            // use the object store definitions 
            if (User != "")
            {
                DlkProjectInformationManagementKeywordHandler.ExecuteKeyword("Login", "Username", "Set", new String[] { User });
                DlkProjectInformationManagementKeywordHandler.ExecuteKeyword("Login", "Password", "Set", new String[] { Password });
                if (!String.IsNullOrEmpty(Database) && !String.IsNullOrWhiteSpace(Database))
                {
                    DlkProjectInformationManagementKeywordHandler.ExecuteKeyword("Login", "Database", "Select", new String[] { Database });
                }
                Thread.Sleep(250);
                DlkProjectInformationManagementKeywordHandler.ExecuteKeyword("Login", "Login", "Click", new String[] { "" });
                Thread.Sleep(5000);

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
