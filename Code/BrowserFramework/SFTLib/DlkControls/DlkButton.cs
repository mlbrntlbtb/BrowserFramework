using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using SFTLib.DlkControls;
using SFTLib.DlkUtility;
using System.Threading;
using OpenQA.Selenium.Interactions;
using CommonLib.DlkUtility;

namespace SFTLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        #region Constructors
        public DlkButton(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize(int timeout = 0)
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();

            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            if (timeout == 0)
                FindElement();
            else
                FindElement(timeout);

            DlkEnvironment.mSwitchediFrame = true;
        }
        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                myClick(4.5);

                DlkLogger.LogInfo("Click() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("ClickButtonIfExists", new String[] { "1|text|Button Name|New" })]
        public void ClickButtonIfExists()
        {
            try
            {
                Initialize(2);
                myClick(4.5);
                DlkLogger.LogInfo("Successfully executed ClickButtonIfExists()");
            }
            catch (Exception)
            {
                DlkLogger.LogInfo("ClickButtonIfExists() : " + mControlName + " does not exist.");
                return;
            }
        }


        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedText, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                if (Convert.ToBoolean(strExpectedValue))
                {
                    Initialize();
                    this.ScrollIntoViewUsingJavaScript();
                    DlkEnvironment.mSwitchediFrame = false;
                }
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String strExpectedValue)
        {
            try
            {
                Initialize();
                if (bool.TryParse(strExpectedValue, out bool result))
                {
                    bool bIsReadOnly = false;
                    if (mElement.GetAttribute("class").Contains("x-btn-disabled"))
                    {
                        bIsReadOnly = true;
                    }
                    else if (mElement.FindElements(By.XPath("./ancestor::div[contains(@class, 'x-btn ')]")).Any())
                    {
                        IWebElement parentButton = mElement.FindElements(By.XPath("./ancestor::div[contains(@class, 'x-btn ')]")).First();
                        bIsReadOnly = parentButton.GetAttribute("class").Contains("x-btn-disabled");
                    }
                    DlkAssert.AssertEqual("Verify read-only state for button: ", result, bIsReadOnly);
                }
                else
                {
                    throw new Exception("VerifyReadOnly() failed : value can only be True or False");
                }
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("BrowseButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void BrowseButton(String filePath)
        {
            try
            {
                Initialize();
                mElement.SendKeys(filePath);
                DlkLogger.LogInfo("BrowseButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("BrowseButton() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        private String GetText()
        {
            FindElement();
            String mText = GetAttributeValue("value");

            //this is to handle other button controls with text as element's text
            if (string.IsNullOrEmpty(mText))
            {
                mText = CleanString(mElement.Text);
            }

            return mText;
        }

        private String CleanString(String TextToClean)
        {
            string ret = string.Empty;

            ret = DlkString.ReplaceCarriageReturn(TextToClean, "");
            ret = DlkString.NormalizeNonBreakingSpace(ret);
            ret = DlkString.UnescapeXML(ret);

            return ret.Trim();
        }

        public void myClick(double dbSecToWait)
        {
            try
            {
                if (DlkEnvironment.mBrowser == "IE")
                {
                    DlkSFTCommon.WaitForScreenToLoad();
                    if (GetValue().ToLower().Contains("delete") 
                        || (mElement.GetAttribute("src") != null && mElement.GetAttribute("src").ToLower().Contains("delete")) //checks for delete buttons that do not have "delete" as value
                        || (mElement.GetAttribute("id") != null && mElement.GetAttribute("id").ToLower().Contains("delete"))) 
                    {
                        Thread.Sleep(1000);
                        mElement.Click();//JS click causes timeouts on all delete buttons.
                    }
                    else
                        ClickUsingJavaScript(false);
                }
                else  if(DlkEnvironment.mBrowser == "Edge")
                {
                    ClickUsingJavaScript(false);
                }
                else
                {
                    ScrollIntoViewUsingJavaScript();
                    mElement.Click();
                }
            }
            catch (Exception e)
            {
                //Known issue with Chrome driver. When an element overlaps the desired element to be clicked, it will render the desired element to be unclickable.
                if (e.Message.Contains("Element is not clickable at point"))
                {
                    Click(5, 5);
                }
                else
                {
                    throw e;
                }
            }
            int iWait = Convert.ToInt32(dbSecToWait * 1000);
            Thread.Sleep(iWait);
        }
    }
}
