using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRSmartLib.DlkControls;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("Wizard")]
    public class DlkWizard : DlkBaseControl
    {
        #region Declarations


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
                    element.SendKeys(Keys.Enter);
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
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
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

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private string getCurrentStep()
        {
            IWebElement currentStepElement = mElement.FindElement(By.XPath("./li[@class='active wizard_step_current']/a/h4"));
            DlkBaseControl currentStepControl = new DlkBaseControl("Current_Step : ", currentStepElement);
            return currentStepControl.GetValue().Trim();
        }

        #endregion
    }
}