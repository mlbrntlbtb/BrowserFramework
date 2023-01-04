using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
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
                ClickButton();
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickButtonIfExists")]
        public void ClickButtonIfExists()
        {
            try
            {
                if (Exists())
                {
                    ClickButton();
                }
                else
                {
                    DlkLogger.LogInfo("ClickButtonIfExists(): Button does not exist.");
                }

                DlkLogger.LogInfo("ClickButtonIfExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickButtonIfExists() failed : " + e.Message, e);
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

        [Keyword("VerifyButtonColor", new string[] { "1|text|ExpectedColor|Blue" })]
        public void VerifyButtonColor(string ExpectedColor)
        {
            try
            {
                Initialize();
                VerifyColor(ExpectedColor);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyButtonColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String strExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", strExpectedValue.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetButtonState", new string[] { "1|text|Variable Name|Sample Variable" })]
        public void GetButtonState(string VariableName)
        {
            try
            {
                Initialize();
                string status = bool.Parse(IsReadOnly()) ? "disabled" : "enabled";

                DlkVariable.SetVariable(VariableName, status);
                DlkLogger.LogInfo($"GetButtonState() : Successfully assigned status \"{status}\" to variable \"{VariableName}\".");
            }
            catch (Exception e)
            {
                throw new Exception("GetButtonState() failed : " + e.Message, e);
            }
        }
        #region private methods
        public void VerifyColor(string ExpectedColor)
        {
            try
            {
                string background = mElement.GetCssValue("background");
                string actualColor = "";

                if (string.IsNullOrEmpty(background))
                {
                    background = mElement.GetCssValue("background-image");
                }

                if (string.IsNullOrEmpty(background) || background == "none")
                {
                    background = mElement.GetCssValue("background-color");
                }

                if (string.IsNullOrEmpty(background) || background == "none")
                {
                    throw new Exception("Button color not found");
                }
                else
                {
                    background = background.Replace(" ", "");
                    if (background.Contains("rgb(8,107,38)") || background.Contains("rgb(152,202,72)"))
                    {
                        actualColor = "Green";
                    }
                    else if (background.Contains("rgb(4,50,91)") || background.Contains("rgb(76,94,155)") || background.Contains("rgb(96,114,138)"))
                    {
                        actualColor = "Blue";
                    }
                    else if (background.Contains("rgb(223,73,37)"))
                    {
                        actualColor = "Red";
                    }
                    else
                    {
                        throw new Exception($"Button color '{background}' not supported");
                    }
                    DlkAssert.AssertEqual("VerifyButtonColor()", ExpectedColor.ToLower(), actualColor.ToLower());
                }
            }
            catch
            {
                throw;
            }
        }

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

        /// <summary>
        /// Call this method to perform click on button
        /// </summary>
        private void ClickButton()
        {
            if (mControlName == "ChooseFile" || mControlName == "Browse") // Handle clicking of embedded Browse btn in FileUploadManager screen or Browse
            {
                if (DlkEnvironment.mIsMobileBrowser)
                {
                    Thread.Sleep(5000);
                    ClickUsingJavaScript(false);
                }
                else
                {
                    ClickByObjectCoordinates();
                }
            }
            else if (DlkEnvironment.mBrowser.ToLower() == "ie" || DlkEnvironment.mBrowser.ToLower() == "safari")
            {
                ClickUsingJavaScript(false);
            }
            else
            {
                Click(1);
            }
        }
        #endregion

    }
}
