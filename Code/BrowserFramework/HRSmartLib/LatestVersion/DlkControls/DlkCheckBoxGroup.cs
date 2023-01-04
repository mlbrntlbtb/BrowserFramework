using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("CheckBoxGroup")]
    public class DlkCheckBoxGroup : DlkBaseControl
    {
        #region Declarations
        private string viewMoreXpath = ".//div[contains(@class,'toggle-filter')]";
        #endregion

        #region Constructors

        public DlkCheckBoxGroup(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {

        }

        public DlkCheckBoxGroup(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {

        }

        #endregion

        #region Keywords
        [Keyword("SetCheckBoxByIndex")]
        public void SetCheckBoxByIndex(string Index, string IsChecked)
        {
            if (IsChecked.Equals(string.Empty))
            {
                DlkLogger.LogInfo("Skipping set since the data parameter is blank.");
            }
            else
            {
                try
                {
                    initialize();
                    bool bIsChecked = Convert.ToBoolean(IsChecked);
                    IWebElement CurrentCheckBox = getCheckBoxByIndex(Index);

                    DlkCheckBox checkbox = new DlkCheckBox("Checkbox", CurrentCheckBox);
                    checkbox.Set(IsChecked);

                    DlkLogger.LogInfo("Successfully executed SetCheckBoxByIndex(): " + mControlName);
                }
                catch (Exception ex)
                {
                    throw new Exception("SetCheckBoxByIndex( ) failed : " + ex.Message, ex);
                }
            }
        }

        [Keyword("SetCheckBoxByText")]
        public void SetCheckBoxByText(string Text, string IsChecked)
        {
            if (IsChecked.Equals(string.Empty))
            {
                DlkLogger.LogInfo("Skipping set since the data parameter is blank.");
            }
            else
            {
                try
                {
                    initialize();
                    bool bIsChecked = Convert.ToBoolean(IsChecked);
                    IWebElement CurrentCheckBox = getCheckBoxByText(Text);

                    DlkCheckBox checkbox = new DlkCheckBox("Checkbox", CurrentCheckBox);
                    checkbox.Set(IsChecked);

                    DlkLogger.LogInfo("Successfully executed SetCheckBoxByText(): " + mControlName);
                }
                catch (Exception ex)
                {
                    throw new Exception("SetCheckBoxByText( ) failed : " + ex.Message, ex);
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

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyCheckBoxTextByIndex")]
        public void VerifyCheckBoxTextByIndex(string Index, string Text)
        {
            try
            {
                initialize();
                IWebElement CurrentCheckBox = getCheckBoxByIndex(Index);
                DlkLabel parentLabelControl = new DlkLabel("Parent Label", CurrentCheckBox.FindElement(By.XPath("./..")));
                string actual = parentLabelControl.GetValue().ToLower().Trim();

                DlkAssert.AssertEqual("VerifyText() : ", Text.ToLower(), actual);
                DlkLogger.LogInfo("VerifyText() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyText() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyCheckBoxValueByIndex")]
        public void VerifyCheckBoxValueByIndex(string Index, string TrueOrFalse)
        {
            try
            {
                initialize();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                IWebElement CurrentCheckBox = getCheckBoxByIndex(Index);
                bool actualResult = CurrentCheckBox.Selected;

                DlkAssert.AssertEqual("VerifyCheckBoxValueByIndex() : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyCheckBoxValueByIndex() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyValue() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ActValue.ToLower());
        }
        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();

            // automatically expand items
            List<IWebElement> ViewMoreButtonResult = mElement.FindElements(By.XPath(viewMoreXpath)).ToList();

            if (ViewMoreButtonResult.Count > 0)
            {
                if (ViewMoreButtonResult[0].Text.Contains("View More") || ViewMoreButtonResult[0].Text.Contains("Show More"))
                {
                    ViewMoreButtonResult[0].Click();
                }
            }
        }

        private IWebElement getCheckBoxByIndex(string index)
        {
            List<IWebElement> checkBoxes = mElement.FindElements(By.XPath(".//input[@type='checkbox']")).ToList();
            IWebElement cb = checkBoxes[Convert.ToInt16(index) - 1];
           
            return cb;
        }

        private IWebElement getCheckBoxByText(string label)
        {
            List<IWebElement> labelElements = mElement.FindElements(By.XPath("//label[text()='"+ label +"']")).ToList();
            IWebElement cb = null;

            if (labelElements.Count > 0)
            {
                cb = labelElements[0].FindElement(By.XPath(".//input[@type='checkbox']"));
            }

            return cb;
        }
        #endregion

        #region Properties
        #endregion
    }
}
