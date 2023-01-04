using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSmartLib.DlkControls;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Wizard")]
    public class DlkWizard : DlkBaseControl
    {
        #region Declarations

        private const string ALL_STEPS_NAME = @".//li//p";

        #endregion

        #region Constructors

        public DlkWizard(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {

        }

        #endregion

        #region Properties
        #endregion

        #region Keywords

        [Keyword("ClickByStepNumber")]
        public void ClickByStepNumber(string StepNumber)
        {
            try
            {
                initialize();
                int stepNumber = int.MinValue;
                if (Int32.TryParse(StepNumber, out stepNumber))
                {
                    IWebElement element = mElement.FindElement(By.XPath("./li[" + stepNumber + "]/a"));
                    DlkButton buttonControl = new DlkButton("Button_Element", element);
                    buttonControl.Click();
                    //element.SendKeys(Keys.Enter);
                    DlkLogger.LogInfo("ClickByStepNumber() successfully executed.");
                }
                else
                {
                    throw new Exception("StepNumber : " + StepNumber + " is not an integer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ClickByStepNumber() execution failed. " + ex.Message, ex);
            }
        }

        [RetryKeyword("ClickRetryByStepNumber")]
        public void ClickRetryByStepNumber(string StepNumber)
        {
            this.PerformAction(() =>
                {
                    ClickByStepNumber(StepNumber);
                    string expectedResult = string.Concat("Step ", StepNumber);
                    string actualResult = getCurrentStep();
                    DlkAssert.AssertEqual("Verify_Current_Step : ", expectedResult, actualResult);
                }, new string[] { "retry" });
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

        [Keyword("VerifyCurrentStep")]
        public void VerifyCurrentStep(string StepNumber)
        {
            try
            {
                initialize();
                string expectedResult = string.Concat("Step ",StepNumber);
                string actualResult = getCurrentStep();
                DlkAssert.AssertEqual("Verify_Current_Step : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyCurrentStep() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyCurrentStep() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyStepsExists")]
        public void VerifyStepsExists(string StepsName, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool foundMatching = false;
                bool expectedResult = true;
                bool actualResult = true;
                bool shouldExists = Convert.ToBoolean(TrueOrFalse);
                string[] steps = StepsName.Split('~');
                string expectedStepName = string.Empty;

                IList<IWebElement> allStepsName = mElement.FindElements(By.XPath(ALL_STEPS_NAME));

                foreach (string stepName in steps)
                {
                    foundMatching = false;
                    expectedStepName = stepName;
                    foreach (IWebElement element in allStepsName)
                    {
                        if (element.Text.Equals(expectedStepName))
                        {
                            foundMatching = true;
                            break;
                        }
                    }

                    if ((shouldExists && !foundMatching) ||
                        (!shouldExists && foundMatching))
                    {
                        DlkLogger.LogInfo(string.Concat("Failed asserting : ", stepName));
                        actualResult = false;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyStepsExists : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyStepsExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyStepsExists() execution failed. " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private string getCurrentStep()
        {
            IList<IWebElement> currentSteps = mElement.FindElements(By.XPath("./li[contains(@class,'active')]/a/h4"));
            if (currentSteps.Count > 0)
            {
                DlkBaseControl currentStepControl = new DlkBaseControl("Current_Step : ", currentSteps[0]);
                return currentStepControl.GetValue().Trim();
            }

            return string.Empty;
        }

        #endregion
    }
}