using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using GovWinLib.System;
using System.Threading;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace GovWinLib.DlkFunctions
{
     [Component("Login")]
    public static class DlkLogin
    {
        public static DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        /// <summary>
        /// Performs a login to navigator
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Password"></param>
        /// <param name="Database"></param>
        public static void Login(String Url, String User, String Password, String Database)
        {
            DlkEnvironment.AutoDriver.Url = Url;
            String Screen = "Login";
            // use the object store definitions 
            if (User != "")
            {
                // Identify if environment is internal or external since they have diff login behaviors
                if (Url.ToLower().Contains("internal."))
                {
                    // Click AlternateInternalLogin link to toggle username/password fields
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "AlternateInternalLogin", "Click", new String[] { "" });
                    // Type username and password then click the login button
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "User", "Set", new String[] { User });
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "Password", "Set", new String[] { Password });
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "Login", "Click", new String[] { "" });
                }
                else // For staging or External
                {
                    // Sets username first the clicks the "Login" Button
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "User", "Set", new String[] { User });
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "Login", "Click", new String[] { "" });
                    // Redirects to the next page where password textbox will be visible, then click the login button
                    DlkObjectStoreFileControlRecord osRec = DlkDynamicObjectStoreHandler.GetControlRecord(Screen, "Password");
                    DlkBaseTextBox passwordControl = new DlkBaseTextBox(osRec.mKey, osRec.mSearchMethod, osRec.mSearchParameters);
                    if (!passwordControl.Exists(60))
                    {
                        DlkLogger.LogWarning("Login() : Main page not loaded within timeout.");
                    }
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "Password", "Set", new String[] { Password });
                    DlkGovWinKeywordHandler.ExecuteKeyword(Screen, "Login", "Click", new String[] { "" });
                }
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
