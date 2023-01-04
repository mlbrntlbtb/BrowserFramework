using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        #region Declarations

        #endregion

        #region Constructors

        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            initialize();
        }

        #endregion

        #region Keywords

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

        [Keyword("VerifyRowCount")]
        public void VerifyRowCount(string RowCount)
        {
            try
            {
                int expectedResult = Convert.ToInt32(RowCount);
                int actualResult = getRows().Count;
                DlkAssert.AssertEqual("VerifyRowCount : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() execution failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string ItemText, string TrueOrFalse)
        {
            try
            {
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;
                IList<IWebElement> rows = getRows();
                foreach (IWebElement row in rows)
                {
                    DlkBaseControl rowControl = new DlkBaseControl(row.Text, row);
                    if (rowControl.GetValue().Trim().Equals(ItemText))
                    {
                        actualResult = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemExists : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyItemExists() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyItemExists() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("ClickByRow")]
        public void ClickByRow(string Row)
        {
            try
            {
                bool hasExpectedElement = false;
                IWebElement rowElement = getRow(Row);
                IList<IWebElement> rowElements = rowElement.FindElements(By.XPath(".//*"));
                foreach (IWebElement element in rowElements)
                {
                    switch (element.TagName.Trim())
                    {
                        case "button":
                        case "a":
                            {
                                hasExpectedElement = true;
                                element.SendKeys(Keys.Enter);
                                break;
                            }
                        case "input":
                            {
                                if (element.GetAttribute("type") != null &&
                                    element.GetAttribute("type").Trim().Equals("checkbox"))
                                {
                                    hasExpectedElement = true;
                                    DlkCheckBox checkBoxControl = new DlkCheckBox("CheckBox_Element : " + element.Text, element);
                                    checkBoxControl.Click();
                                }
                                else if (element.GetAttribute("type") != null &&
                                         element.GetAttribute("type").Equals("radio"))
                                {
                                    hasExpectedElement = true;
                                    DlkRadioButton radioButtonControl = new DlkRadioButton("RadioButton_Element : " + element.Text, element);
                                    radioButtonControl.Click();
                                }
                                break;
                            }
                    }

                    if (hasExpectedElement)
                    {
                        DlkLogger.LogInfo("ClickByRow() successfully executed.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ClickByRow() execution failed. " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private IList<IWebElement> getRows()
        {
            return mElement.FindElements(By.XPath("./li | ./div"));
        }

        private IWebElement getRow(string row)
        {
            return mElement.FindElement(By.XPath("./li[" + row + "]"));
        }

        #endregion
    }
}
