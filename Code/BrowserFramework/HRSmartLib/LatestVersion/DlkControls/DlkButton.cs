using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using HRSmartLib.LatestVersion.DlkControls;
using HRSmartLib.DlkControls;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
             FindElement();
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "button" || mElement.TagName == "img")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }


        public void ClickFromIframe()
        {
            DlkCommon.DlkCommonFunction.ScrollIntoView(mElement);
            mElement.Click();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                DlkCommon.DlkCommonFunction.ScrollIntoView(mElement);
                if (mElement.GetAttribute("class") == "popupBtn")
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(.5);
                    }
                }
                else
                {                
                    Click(.5);
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Other element would receive the click"))
                {
                    IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    javascript.ExecuteScript("window.scrollBy(" + 0 + "," + 70 + ");");
                    DlkLogger.LogInfo("Adjusting control view to accurately click the element.");
                    ClickUsingJavaScript();
                }
                else if (!e.Message.ToLower().Contains("the http request to the remote webdriver server for url"))
                {
                    throw new Exception("Click() failed : " + e.Message, e);
                }
            }

        }

        [RetryKeyword("ClickRetryIfValidationFails")]
        public void ClickRetryIfValidationFails(string WizardStepOrHeaderName)
        {
            this.PerformAction(() =>
            {
                Click();
                string actualResult = DlkCommon.DlkCommonFunction.GetWizardStepOrHeader();
                DlkAssert.AssertEqual("ClickRetryIfValidationFails", WizardStepOrHeaderName, actualResult);
            }, new String[] { "retry" });
        }

        [RetryKeyword("ClickRetryIfValidationFailsAfterDelay")]
        public void ClickRetryIfValidationFailsAfterDelay(string WizardStepOrHeaderName, string Delay)
        {
            this.PerformAction(() =>
            {
                Click(Convert.ToInt16(Delay));
                string actualResult = DlkCommon.DlkCommonFunction.GetWizardStepOrHeader();
                DlkAssert.AssertEqual("ClickRetryIfValidationFails", WizardStepOrHeaderName, actualResult);
            }, new String[] { "retry" });
        }

        [RetryKeyword("ClickRetryIfDialogNotExist")]
        public void ClickRetryIfDialogNotExist()
        {
            this.PerformAction(() =>
                {
                    Click();
                    bool actualResult = DlkCommon.DlkCommonFunction.HasOpenMessageDialog();
                    DlkAssert.AssertEqual("ClickRetryIfDialogNotExist", true, actualResult);
                }, new String[] { "retry" });
        }

        [Keyword("DoubleClick")]
        public new void DoubleClick()
        {
            try
            {
                Initialize();
                base.DoubleClick();
            }
            catch(Exception ex)
            {
                throw new Exception("DoubleClick() execution failed. " + ex.Message, ex);
            }
        }

        public String GetText()
        {
            Initialize();
            String mText = "";
            mText = GetAttributeValue("value");
            if (string.IsNullOrEmpty(mText))
            {
                mText = GetValue().Trim();
            }
            return mText;
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActText = GetText();
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ActText);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyAttribute()", TrueOrFalse.ToLower(), ActValue.ToLower());
                //VerifyAttribute("readonly", strExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToggleExpanded", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyToggleExpanded(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool actualResult = false;
                bool foundToggle = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                List<string> toggleControls = new List<string>
                {
                    "./ancestor::div[@aria-expanded]",
                    "../../div[@class='form-filter']",
                    "../following-sibling::div[contains(@class,'filter')]",
                    "."
                };

                for (int i = 0; i < toggleControls.Count; i++)
                {
                    IList<IWebElement> toggleButton = mElement.FindElements(By.XPath(toggleControls[i]));
                    if (toggleButton.Count > 0)
                    {
                        string attribute = string.Empty;
                        if (toggleControls[i].Contains("aria-expanded"))
                        {
                            attribute = toggleButton[0].GetAttribute("aria-expanded") != null ? toggleButton[0].GetAttribute("aria-expanded") : string.Empty;
                        }
                        else if (toggleControls[i].Contains("form-filter"))
                        {
                            attribute = toggleButton[0].GetAttribute("style") != null ? toggleButton[0].GetAttribute("style") : string.Empty;
                            attribute = attribute.Contains("none") ? "False" : "True";
                        }
                        else if (toggleControls[i].Contains("following-sibling"))
                        {
                            attribute = toggleButton[0].GetAttribute("style") != null ? toggleButton[0].GetAttribute("style") : string.Empty;
                            attribute = attribute.Contains("none") ? "False" : "True";
                        }
                        else
                        {
                            attribute = toggleButton[0].GetAttribute("Title") != null ? toggleButton[0].GetAttribute("Title") : string.Empty;
                            attribute = attribute.Contains("Up") ? "True" : "False";
                        }

                        actualResult = Convert.ToBoolean(attribute);
                        foundToggle = true;
                    }

                    if (foundToggle)
                    {
                        break;
                    }
                }

                if (foundToggle)
                {
                    DlkAssert.AssertEqual("VerifyToggleExpanded()", expectedResult, actualResult);
                }
                else
                {
                    throw new Exception("Toggle not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToggleExpanded() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyToolTip", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyToolTip(String ToolTip)
        {
            try
            {
                Initialize();
                string expectedResult = ToolTip;
                string actualResult = string.Empty;
                List<string> searchToolTipList = new List<string>()
                {
                   "data-original-title",
                   "data-title"
                };

                IWebElement tooltipElement = mElement.FindElement(By.XPath(".//i[@data-toggle='tooltip'] | ./self::a[@data-toggle='tooltip']")); 
                foreach (string toolTip in searchToolTipList)
                {
                    if (string.IsNullOrEmpty(actualResult))
                    {
                        actualResult = tooltipElement.GetAttribute(toolTip) == null ? string.Empty : tooltipElement.GetAttribute(toolTip);
                    }
                    else
                    {
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyToolTip()", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyToolTip() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolTip() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                //Fail safe code for checking crashed site.
                DlkEnvironment.AutoDriver.FindElement(By.CssSelector("h1"));
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }
    }
}
