using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using CostpointLib.DlkControls;

namespace CostpointLib.DlkFunctions
{
    [Component("ArrangeTableColumns")]
    public static class DlkArrangeTableColumns
    {
        static IList<IWebElement> mlstRowElements;

        [Keyword("SelectRow", new String[] {"1|text|Row|RowValue", 
                                                    "2|text|Value|Project*"})]
        public static void SelectRow(String RowValue)
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
                        DlkBaseControl selectedRow = new DlkBaseControl("Width TextBox", "XPATH", (string.Format("//*[@id='tCMgrTableMove']/div[@class='tCMgrBody']/div[@col_id='{0}']/div[@class='tCMgrBodyCol3']/input[@class='tCMgrBodyInput']", colID)));
                        selectedRow.Click();

                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Row '" + RowValue + "' not found.");
                }
                DlkLogger.LogInfo("Successfully executed SelectRow()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectRow() failed : " + e.Message, e);
            }
        }

        [Keyword("SetWidth", new String[] {"1|text|Row|RowValue", 
                                                    "2|text|Value|100"})]
        public static void SetWidth(String RowValue, String TextToEnter)
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
                        DlkTextBox inputControl = new DlkTextBox("Width TextBox", "XPATH", (string.Format("//*[@id='tCMgrTableMove']/div[@class='tCMgrBody']/div[@col_id='{0}']/div[@class='tCMgrBodyCol3']/input[@class='tCMgrBodyInput']", colID)));
                        inputControl.Set(TextToEnter);

                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Row '" + RowValue + "' not found.");
                }
                DlkLogger.LogInfo("Successfully executed SetWidth()");
            }
            catch (Exception e)
            {
                throw new Exception("SetWidth() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyWidth", new String[] {"1|text|Row|RowValue", 
                                                    "2|text|Expected Value|100"})]
        public static void VerifyWidth(String RowValue, String ExpectedValue)
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
                        DlkTextBox inputControl = new DlkTextBox("Width TextBox", "XPATH", (string.Format("//*[@id='tCMgrTableMove']/div[@class='tCMgrBody']/div[@col_id='{0}']/div[@class='tCMgrBodyCol3']/input[@class='tCMgrBodyInput']", colID)));
                        String ActualValue = inputControl.GetAttributeValue("value");

                        //Perform verify
                        DlkAssert.AssertEqual("VerifyWidth()", ExpectedValue, ActualValue);
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Row '" + RowValue + "' not found.");
                }
                DlkLogger.LogInfo("Successfully executed VerifyWidth()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyWidth() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Retrieve all table rows
        /// </summary>
        private static void GetTableRows()
        {
            mlstRowElements = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@id='tCMgrTableMove']/div[@class='tCMgrBody']/div[@class='tCMgrBodyRow']"));
        }

        /// <summary>
        /// Retrieves the table row text value
        /// </summary>
        /// <param name="columnHeader">Column element</param>
        /// <returns>Text value of the Column element</returns>
        private static String GetRowText(IWebElement columnHeader)
        {
            string headerText = "";
            char[] charsToTrim = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ' '};

            headerText = new DlkBaseControl("ColumnHeader", columnHeader).GetValue();
            return headerText.TrimStart(charsToTrim); //To remove the sequence number from the header text
        }
    }
}
