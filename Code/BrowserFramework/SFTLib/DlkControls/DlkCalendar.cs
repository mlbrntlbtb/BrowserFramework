using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using SFTLib.DlkSystem;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;
using SFTLib.DlkUtility;

namespace SFTLib.DlkControls
{
    [ControlType("Calendar")]
    public class DlkCalendar : DlkBaseControl
    {
        const int COUNTER = 0;
        const int SEARCH_MAX = 40;
        #region Constructors
        public DlkCalendar(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkCalendar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCalendar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            this.ScrollIntoViewUsingJavaScript(true); // JPV: negate effect on IE, check if this will affect test scripts
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                DlkSFTCommon.ScrollToElement(this);
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
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
        [Keyword("SetDate")]
        public void SetDate(String value)
        {
            try
            {
                Initialize();
                mElement = !String.Equals(mElement.TagName, "input") ? mElement.FindElement(By.XPath(@".//input")) : mElement;
                Thread.Sleep(500);
                mElement.SendKeys(Keys.Control + "a");
                mElement.SendKeys(Keys.Backspace);
                mElement.SendKeys(value);
                Thread.Sleep(600);
                DlkLogger.LogInfo("Successfully executed SetDate()");
                DlkSFTCommon.WaitForSpinner();
            }
            catch (StaleElementReferenceException)
            {
                DlkLogger.LogInfo("Stale Element. Retrying to set.");
                if (COUNTER <= SEARCH_MAX)
                    SetDate(value);
                return;
            }
            catch (Exception e)
            {
                throw new Exception("SetDate() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }
    }
}
