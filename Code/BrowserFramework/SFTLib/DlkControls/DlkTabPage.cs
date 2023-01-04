using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using SFTLib.DlkControls;
using SFTLib.DlkUtility;

namespace SFTLib.DlkControls
{
    [ControlType("TabPage")]
    public class DlkTabPage : DlkBaseControl
    {
        public DlkTabPage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTabPage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTabPage(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

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
        private void WaitUntilTabContentLoad()
        {
            DlkBaseControl tabContent = new DlkBaseControl("TabContent", "XPATH", $"//td[@id='{mElement.GetAttribute("id").Replace("_lbl", "")}']");
            try
            {
                tabContent.FindElement();
            }
            catch (Exception e)
            {
                throw new Exception($"WaitUntilContentLoad(): {e}");
            }
        }

        private String GetText()
        {
            FindElement();
            String mText = GetAttributeValue("value");

            //this is to handle other controls with text as element's text
            if (string.IsNullOrEmpty(mText))
            {
                mText = mElement.Text;
            }

            return mText;
        }

        [Keyword("SelectTab")]
        public void SelectTab()
        {
            try
            {
                Initialize();
                Thread.Sleep(2000);
                if (DlkEnvironment.mBrowser == "IE" && mElement.GetAttribute("class").Contains("rich-tab"))
                {
                    DlkEnvironment.mSwitchediFrame = false;
                    ClickByObjectCoordinates();
                }
                else if (DlkEnvironment.mBrowser == "IE")
                    ClickUsingJavaScript(false);
                else
                    mElement.Click();
                
                DlkSFTCommon.WaitForScreenToLoad();
                DlkSFTCommon.WaitForSpinner();
                //if (mElement.GetAttribute("class").Contains("rich-tab"))
                //    WaitUntilTabContentLoad();
                DlkLogger.LogInfo("Successfully executed SelectTab()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectTab() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                if (Convert.ToBoolean(ExpectedValue))
                {
                    Initialize();
                    ScrollIntoViewUsingJavaScript();
                    DlkEnvironment.mSwitchediFrame = false;
                }

                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
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

        [Keyword("VerifyTabName", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTabName(String ExpectedText)
        {
            try
            {
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify name for Tab: " + mControlName, ExpectedText, ActText);
                DlkLogger.LogInfo("VerifyTabName() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabName() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
    }
}
