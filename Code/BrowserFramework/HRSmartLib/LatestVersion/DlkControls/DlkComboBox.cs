using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }

        #endregion

        #region Keywords

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            if (Value.Equals(string.Empty))
            {
                DlkLogger.LogInfo("Skipping select since the data parameter is blank.");
            }
            else
            {
                try
                {
                    initialize();
                    SelectElement comboBoxElement = new SelectElement(mElement);
                    comboBoxElement.SelectByText(Value);

                    //VerifyValue(Value);
                    DlkLogger.LogInfo("Successfully executed Select(). Control : " + mControlName + " : " + Value);
                }
                catch (Exception ex)
                {
                    throw new Exception("Selec( ) execution failed. : " + ex.Message, ex);
                }
            }
        }

        [Keyword("SelectByRow")]
        public void SelectByRow(string RowNumber)
        {
            if (RowNumber.Equals(string.Empty))
            {
                DlkLogger.LogInfo("Skipping select since the data parameter is blank.");
            }
            else
            {
                try
                {
                    initialize();
                    int rowIndex = Convert.ToInt32(RowNumber);
                    SelectElement comboBoxElement = new SelectElement(mElement);
                    comboBoxElement.SelectByIndex(rowIndex);

                    DlkLogger.LogInfo("Successfully executed SelectByRow(). Control : " + mControlName + " : Row Number : " + RowNumber);
                }
                catch (Exception ex)
                {
                    throw new Exception("SelectByRow( ) execution failed. : " + ex.Message, ex);
                }
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

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string Item, string TrueOrFalse)
        { 
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                StringBuilder actualText = new StringBuilder();
                IList<IWebElement> options = mElement.FindElements(By.XPath("./option"));
                foreach (IWebElement option in options)
                {
                    DlkBaseControl optionControl = new DlkBaseControl("Option Item : " + option.Text, option);
                    string optionValue = optionControl.GetValue().Trim();
                    actualText.AppendLine(optionValue);
                    if (optionValue.Equals(Item))
                    {
                        actualResult = true;
                        break;
                    }
                }

                DlkLogger.LogInfo("Actual Options : \n" + actualText.ToString());
                DlkAssert.AssertEqual("VerifyRowExists : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyItemExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyItemExists() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyState")]
        public void VerifyState(string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;

                string disabledAttribute = mElement.GetAttribute("disabled");
                if (!string.IsNullOrEmpty(disabledAttribute) &&
                    (disabledAttribute.Equals("true") ||
                     disabledAttribute.Equals("disabled")))
                {
                    actualResult = false;
                }
                else
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("VerifyState : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyState successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyState execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyText")]
        public void VerifyText(string ExpectedText)
        {
            try
            {
                initialize();
                SelectElement comboBoxElement = new SelectElement(mElement);
                DlkAssert.AssertEqual("VerifyText() : ", ExpectedText, comboBoxElement.SelectedOption.Text);
                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyText() execution failed. : " + ex.Message, ex);
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
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}
