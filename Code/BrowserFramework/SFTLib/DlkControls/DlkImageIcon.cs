using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using SFTLib.DlkControls;
using SFTLib.DlkControls.Contract;
using SFTLib.DlkControls.Concrete.ComboBox;
using SFTLib.DlkUtility;

namespace SFTLib.DlkControls
{

    [ControlType("ImageIcon")]
    public class DlkImageIcon : DlkBaseControl
    {
        #region Constructor
        public DlkImageIcon(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkImageIcon(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkImageIcon(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                Initialize();

                String sIconClass = mElement.GetAttribute("class");
                String sIconSource = mElement.GetAttribute("src");
                DlkAssert.AssertEqual("VerifyExists() : ", Convert.ToBoolean(strExpectedValue), !sIconClass.ToLower().Contains("opaque") ? !sIconSource.ToLower().Contains("unsigned") : false);
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

    }
}
