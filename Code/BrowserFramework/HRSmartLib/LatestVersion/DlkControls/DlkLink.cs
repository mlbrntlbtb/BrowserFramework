using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using HRSmartLib.DlkControls;
using System.Threading;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Link")]
    public class DlkLink : CommonLib.DlkControls.DlkBaseControl
    {
        #region Declarations

        #endregion

        #region Constructors

        public DlkLink(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        public DlkLink(String ControlName, IWebElement ExistingElement)
            : base(ControlName, ExistingElement)
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                initialize();
                DlkCommon.DlkCommonFunction.ScrollIntoView(mElement);
                Thread.Sleep(500);
                base.Click(1.5);
                DlkLogger.LogInfo("Successfully executed Click()");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("StandardClick")]
        public void StandardClick()
        {
            try
            {
                initialize();
                mElement.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE or FALSE" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                initialize();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                if (!ExpectedValue.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(ExpectedValue));
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
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTooltipContent")]
        public void VerifyTooltipContent(string Content)
        {
            try
            {
                initialize();
                string actualResult = mElement.GetAttribute("data-content") == null ? string.Empty : mElement.GetAttribute("data-content").Trim();
                DlkAssert.AssertEqual("Verify Tooltip Content : ", Content, actualResult);
                DlkLogger.LogInfo("VerifyTooltipContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTooltipContent() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTooltipTitle")]
        public void VerifyTooltipTitle(string Content)
        {
            try
            {
                initialize();
                string actualResult = mElement.GetAttribute("data-original-title") == null ? string.Empty : mElement.GetAttribute("data-original-title").Trim();
                DlkAssert.AssertEqual("Verify Tooltip Title : ", Content, actualResult);
                DlkLogger.LogInfo("VerifyTooltipTitle() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTooltipTitle() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyLinkPopover")]
        public void VerifyLinkPopover(string Text)
        {
            try
            {
                initialize();
                string actualResult = "";
                mElement.Click();

                initialize(); //initialize again since attribute needed is added after clicking
                if (mElement.GetAttribute("aria-describedby").Contains("popover"))
                {
                    actualResult = mElement.GetAttribute("data-bs-content").Replace("<br />", "~");
                }

                DlkAssert.AssertEqual("VerifyLinkPopover(): ", Text, actualResult);
                DlkLogger.LogInfo("VerifyLinkPopover() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyLinkPopover() execution failed. : " + ex.Message, ex);
            }
        }


        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }


        public bool IsAttributeExists(string attributeName, IWebElement element = null)
        {
            if (element == null)
            {
                if (mElement == null)
                {
                    FindElement();
                }
                element = mElement;
            }
            try
            {
                string attributeValue = element.GetAttribute(attributeName);

                if (attributeValue == null)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
