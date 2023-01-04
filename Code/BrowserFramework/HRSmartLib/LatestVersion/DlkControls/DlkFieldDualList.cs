using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("FieldDualList")]
    public class DlkFieldDualList : DlkBaseControl
    {
        #region Declarations
        string selectedContainer = ".//div[2]//ul";
        #endregion

        #region Constructors

        public DlkFieldDualList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {

        }

        public DlkFieldDualList(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {

        }

        #endregion

        #region Keywords
        [Keyword("AddSelectedItem")]
        public void AddSelectedItem(string Text)
        {
            try
            {
                initialize();
                List<IWebElement> currentAvailableFields = getAvailableFields();
                List<IWebElement> currentSelectedFields = getSelectedFields();
                IWebElement currentField = getFieldByText(Text, currentAvailableFields);

                if (currentField == null)
                {
                    throw new Exception("Field is not found: " + Text);
                }
                else
                {
                    if (currentSelectedFields != null)
                    {
                        IWebElement toLocation = currentSelectedFields[currentSelectedFields.Count - 1];
                        DlkCommon.DlkCommonFunction.DragAndDrop(currentField, toLocation, 0, 5);
                    }
                    else // selected field is still empty
                    {
                        IWebElement toLocation = mElement.FindElement(By.XPath(selectedContainer));
                        DlkCommon.DlkCommonFunction.DragAndDrop(currentField, toLocation);
                    }

                    DlkLogger.LogInfo("Successfully executed AddSelectedItem(): " + Text);
                }
            }   
            catch (Exception e)
            {
                throw new Exception("AddSelectedItem() failed : " + e.Message, e);
            }
        }

        [Keyword("RemoveSelectedItem")]
        public void RemoveSelectedItem(string Text)
        {
            try
            {
                initialize();
                List<IWebElement> currentAvailableFields = getAvailableFields();
                List<IWebElement> currentSelectedFields = getSelectedFields();
                IWebElement currentField = getFieldByText(Text, currentSelectedFields);

                if (currentField == null)
                {
                    throw new Exception("Field is not found: " + Text);
                }
                else
                {
                    IWebElement toLocation = currentAvailableFields[currentAvailableFields.Count - 1];

                    DlkCommon.DlkCommonFunction.DragAndDrop(currentField, toLocation, 0, 5);

                    DlkLogger.LogInfo("Successfully executed RemoveSelectedItem(): " + Text);
                }
            }
            catch (Exception e)
            {
                throw new Exception("RemoveSelectedItem() failed : " + e.Message, e);
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
        }

        private List<IWebElement> getAvailableFields()
        {
            List<IWebElement> availableFields = null;
            List<string> availableFieldsXpathLocator = new List<string> {
            ".//div[contains(@class,'draggable-available')]//ul//li",
            ".//div[1]//ul//li"
            };

            foreach (string locator in availableFieldsXpathLocator)
            {
                if (mElement.FindElements(By.XPath(locator)).Count > 0)
                {
                    availableFields = mElement.FindElements(By.XPath(locator)).ToList();
                    break;
                }
            }
            return availableFields;
        }

        private List<IWebElement> getSelectedFields()
        {
            List<IWebElement> selectedFields = null;
            List<string> selectedFieldsXpathLocator = new List<string> {
            ".//div[contains(@class,'draggable-selected')]//ul//li",
            ".//div[2]//ul//li"
            };

            foreach (string locator in selectedFieldsXpathLocator)
            {
                if (mElement.FindElements(By.XPath(locator)).Count > 0)
                {
                    selectedFields = mElement.FindElements(By.XPath(locator)).ToList();
                    break;
                }
            }
            return selectedFields;
        }

        private IWebElement getFieldByText(string text, List<IWebElement> fieldList)
        {
            IWebElement resultField = null;

            foreach (IWebElement field in fieldList)
            {
                string fieldText = "";
                if (field.FindElements(By.XPath(".//span")).Count > 0)
                {
                    fieldText = field.FindElement(By.XPath(".//span")).Text;
                }
                else
                {
                    DlkBaseControl currField = new DlkBaseControl("Control", field);
                    fieldText = currField.GetValue();
                }

                if (fieldText == text)
                {
                    return field;
                }
            }

            return resultField;
        }
        #endregion

        #region Properties
        #endregion
    }
}
