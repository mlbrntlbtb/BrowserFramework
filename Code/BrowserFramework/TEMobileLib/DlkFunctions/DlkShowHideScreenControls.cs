using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TEMobileLib.DlkControls;

namespace TEMobileLib.DlkFunctions
{
    [Component("ShowHideScreenControls")]
    public static class DlkShowHideScreenControls
    {
        static IList<IWebElement> mlstRowElements;

        [Keyword("SetAlwaysHideValue", new String[] {"1|text|Row|RowValue", 
                                                    "2|text|Value|True"})]
        public static void SetAlwaysHideValue(String RowValue, String IsChecked)
        {
            Boolean bFound = false;

            try
            {
                GetTableRows();

                foreach (IWebElement row in mlstRowElements)
                {
                    string sColName = GetRowText(row);
                    Boolean bIsChecked = Convert.ToBoolean(IsChecked);

                    if (sColName == RowValue)
                    {
                        //Retrieve col_id which is the unique id for the row 
                        string colID = row.GetAttribute("col_id");
                        DlkCheckBox chkBoxControl = new DlkCheckBox("AlwaysHideCheckBox", "XPATH", (string.Format("//*[@id='tCMgrTableHide']/div[@class='tCMgrBody']/div[@col_id='{0}']/div[@class='tCMgrBodyCol5']/input[@class='tCMgrBodyInput']", colID)));
                        Boolean bActValue = chkBoxControl.GetCheckedState();

                        if (bIsChecked != bActValue)
                        {
                            chkBoxControl.Click();
                        }
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Row '" + RowValue + "' not found.");
                }
                DlkLogger.LogInfo("Successfully executed SetAlwaysHideValue()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAlwaysHideValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAlwaysHideValue", new String[] {"1|text|Row|RowValue", 
                                                    "2|text|Expected Value|True"})]
        public static void VerifyAlwaysHideValue(String RowValue, String ExpectedValue)
        {
            Boolean bFound = false;

            try
            {
                GetTableRows();

                foreach (IWebElement row in mlstRowElements)
                {
                    string sColName = GetRowText(row);

                    if (sColName == RowValue)
                    {
                        //Retrieve col_id which is the unique id for the row 
                        string colID = row.GetAttribute("col_id");
                        DlkCheckBox chkBoxControl = new DlkCheckBox("AlwaysHideCheckBox", "XPATH", (string.Format("//*[@id='tCMgrTableHide']/div[@class='tCMgrBody']/div[@col_id='{0}']/div[@class='tCMgrBodyCol5']/input[@class='tCMgrBodyInput']", colID)));
                        Boolean ActualValue = chkBoxControl.GetCheckedState();

                        //Perform verify
                        DlkAssert.AssertEqual("VerifyAlwaysHideValue()", Convert.ToBoolean(ExpectedValue), ActualValue);
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Row '" + RowValue + "' not found.");
                }
                DlkLogger.LogInfo("Successfully executed VerifyAlwaysHideValue()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAlwaysHideValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableRowExist", new String[] {"1|text|Row|RowValue", 
                                                    "2|text|Expected Value|True"})]
        public static void VerifyTableRowExist(String RowValue, String ExpectedValue)
        {
            Boolean bFound = false;

            try
            {
                GetTableRows();

                foreach (IWebElement row in mlstRowElements)
                {
                    string sColName = GetRowText(row);

                    if (sColName == RowValue)
                    {
                        bFound = true;
                        break;
                    }
                }
                if (bFound != Convert.ToBoolean(ExpectedValue))
                {
                    throw new Exception("Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(bFound) + "'");
                }
                DlkLogger.LogInfo("Successfully executed VerifyTableRowExist() : Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(bFound) + "'");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableRowExist() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Retrieve all table rows
        /// </summary>
        private static void GetTableRows()
        {
            mlstRowElements = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@id='tCMgrTableHide']/div[@class='tCMgrBody']/div[@class='tCMgrBodyRow']"));
        }

        /// <summary>
        /// Retrieves the table row text value
        /// </summary>
        /// <param name="columnHeader">Column element</param>
        /// <returns>Text value of the Column element</returns>
        private static String GetRowText(IWebElement columnHeader)
        {
            string headerText = "";
            headerText = new DlkBaseControl("ColumnHeader", columnHeader).GetValue();
            return DlkString.RemoveCarriageReturn(headerText.Trim()).Replace("/ ", "/");
        }
    }
}
