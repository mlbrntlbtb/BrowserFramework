using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using TEMobileLib.DlkUtility;

namespace TEMobileLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        private String mstrVisibleCalBtnXPATH = "//input[contains(@id,'basicField')]/ancestor::td/span[@class='fCalBtn' and not(contains(@style,'display: none;'))]";

        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "button" || GetAttributeValue("type") == "button" || mElement.TagName == "img")
            {
                return true;
            }
            else if(mElement.TagName == "span")
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::span[@type='button']"));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {

            FindElement();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                /* JPV: Appium does not trigger non-standard events such as touchend (Login button). Workaround is to trigger handler manually */
                /* Another possibity is to tap via native view, but computing coordinates within a Safari webbrowser is flaky */
                base.Click();
                if (!DlkTEMobileCommon.IsElementStale(this))//check if element is in DOM first before verifying JS attributes
                {
                    if (mElement.GetAttribute("ontouchend") != null)
                    {
                        IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        javascript.ExecuteScript("var e = new Event('touchend'); arguments[0].dispatchEvent(e)", mElement);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("ClickWhileLoading")]
        public void ClickWhileLoading()
        {
            try
            {
                Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickWhileLoading() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedText, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                if (mControlName == "Find_CriteriaValueShowCalendar") // Check for Calendar button in Query dialog which is a special case
                {
                    Initialize();
                    Boolean ActValue = false;
                    if (!mElement.Displayed)
                    {
                        if (mElement.FindElements(By.XPath(mstrVisibleCalBtnXPATH)).Count > 0)
                        {
                            ActValue = true;
                            DlkAssert.AssertEqual("VerifyExists() : " + mControlName, Convert.ToBoolean(strExpectedValue), ActValue);
                        }
                        else
                        {
                            ActValue = false;
                            DlkAssert.AssertEqual("VerifyExists() : " + mControlName, Convert.ToBoolean(strExpectedValue), ActValue);
                        }
                        DlkLogger.LogInfo("VerifyExists() passed");
                    }
                }            
                else
                {
                    base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String strExpectedValue)
        {
            try
            {
                DlkEnvironment.mIsMobile = true;
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", strExpectedValue.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("NativeClick")]
        public void NativeClick()
        {
            try
            {
                DlkEnvironment.mIsMobile = true;
                DlkEnvironment.SetContext("NATIVE");
                Initialize();
                mElement.Click();
                DlkLogger.LogInfo("NativeClick() passed");
            }
            catch (Exception e)
            {
                throw new Exception("NativeClick() failed : " + e.Message, e);
            }
            finally
            {
                DlkEnvironment.SetContext("WEBVIEW");
                DlkEnvironment.mIsMobile = false;
            }
        }

        [Keyword("NativeGetExists")]
        public void NativeGetExists(String VariableName)
        {
            try
            {
                DlkEnvironment.mIsMobile = true;
                DlkEnvironment.SetContext("NATIVE");
                string existsValue = Exists().ToString();
                DlkVariable.SetVariable(VariableName, existsValue);
                DlkLogger.LogInfo("NativeGetExists() passed : value " + existsValue + " assigned to variable " + VariableName);
            }
            catch (Exception e)
            {
                throw new Exception("NativeGetExists() failed : " + e.Message, e);
            }
            finally
            {
                DlkEnvironment.SetContext("WEBVIEW");
                DlkEnvironment.mIsMobile = false;
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        #region private methods
        private String GetText()
        {
            Initialize();
            String mText = GetAttributeValue("value");

            //this is to handle other button controls with text as element's text
            if (string.IsNullOrEmpty(mText))
            {
                mText = mElement.Text;
            }
                
            return mText;
        }

        #endregion

    }
}
