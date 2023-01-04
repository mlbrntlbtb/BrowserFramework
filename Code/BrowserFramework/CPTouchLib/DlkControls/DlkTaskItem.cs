#define NATIVE_MAPPING
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;

namespace CPTouchLib.DlkControls
{
    [ControlType("TaskItem")]
    public class DlkTaskItem : DlkMobileControl
    {
#if NATIVE_MAPPING
        private const string STR_XPATH_APPROVE_TS_CONTAINER = "//*[contains(@resource-id,'approveTimesheets')]";
        private const string STR_XPATH_BADGE_CONTAINER = "//*[contains(@resource-id,'approveTaskBadge')]";
#else
        private const string STR_XPATH_APPROVE_TS_CONTAINER = "//*[contains(@id,'approveTimesheets')]";
        private const string STR_XPATH_BADGE_CONTAINER = "//*[contains(@id,'approveTaskBadge')]";
#endif

        public DlkTaskItem(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTaskItem(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTaskItem(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Tap")]
        public new void Tap()
        {
            try
            {
                FindElement();
                base.Tap();
                DlkLogger.LogInfo("Successfully executed Tap().");
            }
            catch (StaleElementReferenceException)
            {
                Tap();
            }
            catch (Exception e)
            {
                throw new Exception("Tap() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
                var approveTS = mElement.FindElements(By.XPath("(" + mSearchValues.First() 
                    + STR_XPATH_APPROVE_TS_CONTAINER + "/*)[1]/*")).FirstOrDefault();
#else
                 var approveTS = mElement.FindElements(By.XPath(mSearchValues.First() 
                    + STR_XPATH_APPROVE_TS_CONTAINER + "//*[contains(@class, 'x-label')]/span")).FirstOrDefault();
#endif
                if (approveTS == null)
                {
                    throw new Exception("Cannot get text because text field is null");
                }
                DlkAssert.AssertEqual("VerifyText(): " + mControlName, ExpectedValue, approveTS.Text);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyBadge", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyBadge(String ExpectedValue)
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
                var badge = mElement.FindElements(By.XPath("(" + mSearchValues.First()
                    + STR_XPATH_BADGE_CONTAINER + "/*/*)[1]")).FirstOrDefault();
#else
                var badge = mElement.FindElements(By.XPath(mSearchValues.First()
                    + STR_XPATH_BADGE_CONTAINER + "//*[contains(@class, 'smallbadge')]")).FirstOrDefault();
#endif
                if (badge == null)
                {
                    throw new Exception("Cannot get badge value because badge field is null");
                }
                DlkAssert.AssertEqual("VerifyBadge():" + mControlName, ExpectedValue, badge.Text);
                DlkLogger.LogInfo("VerifyBadge() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyBadge() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
    }
}
