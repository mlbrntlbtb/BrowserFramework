using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjectInformationManagementLib.DlkSystem;
using OpenQA.Selenium;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("BrowserWindow")]
    public class DlkBrowserWindow : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType = false;
        #endregion

        #region CONSTRUCTOR
        public DlkBrowserWindow(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBrowserWindow(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkBrowserWindow(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkBrowserWindow(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Contains("iframe"))
            {
                _iframeSearchType = true;
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|title|sample title" })]
        public void VerifyTitle(string Title)
        {
            try
            { 
                Initialize();
                string actualTitle = DlkEnvironment.AutoDriver.Title;
                bool isEqual = DlkAssert.Equals(Title, actualTitle);

                if(isEqual)
                {
                    DlkLogger.LogInfo("VerifyTitle() passed");
                }                
                else
                {
                    throw new Exception(string.Format("VerifyTitle() actual:{0} compare:{1}", actualTitle, Title));
                }

                if(DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
                {
                    DlkEnvironment.AutoDriver.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
    }
}
