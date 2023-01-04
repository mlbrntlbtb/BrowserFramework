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
    /// <summary>
    /// Div tag that house both Available Items table and Selected Items table.
    /// </summary>
    [ControlType("DualList")]
    public class DlkDualList : DlkBaseControl
    {
        #region Declarations

        private const string ROW_BUTTON = @"./td[@class='actions_td']/a";
        private const string ROW_ITEM_GROUP = "complexduallist_category_header_row bg-muted";
        private IWebElement _availableTableList = null;
        private IWebElement _selectedTableList = null;

        #endregion

        #region Constructors

        public DlkDualList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            initialize();
        }

        public DlkDualList(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {
            initialize();
        }

        #endregion

        #region Keywords

        [Keyword("SelectByRow")]
        public void SelectByRow(string Row)
        {
            try
            {
                //zero based row index.
                //int rowIndex = Convert.ToInt32(Row) - 1;
                performButtonClickByRow(Row, _availableTableList);
                DlkLogger.LogInfo("SelectByRow( ) execution passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("SelectByRow( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("DeselectByRow")]
        public void DeselectByRow(string Row)
        {
            try
            {
                //zero based row index.
                //int rowIndex = Convert.ToInt32(Row) - 1;
                performButtonClickByRow(Row, _selectedTableList);
                DlkLogger.LogInfo("Deselect( ) execution passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Deselect( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("SetCellValue")]
        public void SetCellValue(string Row, string Column, string Value, string Table)
        {
            try
            {
                int columnIndex = -1;
                IWebElement table = getTable(Table);
                IWebElement tableRow = getTableRowByRow(Convert.ToInt32(Row) - 1, table);

                if (!Int32.TryParse(Column, out columnIndex))
                {
                    columnIndex = getColumnIndexByHeader(Column);
                }

                bool hasExpectedElement = false;
                IList<IWebElement> rowColumnElementList = tableRow.FindElements(By.XPath(@"./td[" + columnIndex + "]//*"));
                foreach (IWebElement element in rowColumnElementList)
                {
                    hasExpectedElement = false;
                    switch (element.TagName)
                    {
                        case "input" :
                        {
                            if (element.GetAttribute("type").Trim().Equals("text"))
                            {
                                hasExpectedElement = true;
                                element.Clear();
                                element.SendKeys(Keys.Control + "a"); 
                                element.SendKeys(Keys.Delete);
                                element.SendKeys(Value);
                                //DlkTextBox textBoxControl = new DlkTextBox("Column : " + Column, element);
                                //textBoxControl.Set(Value);
                            }
                            break;
                        }
                    }

                    if (hasExpectedElement)
                    {
                        break;
                    }
                }

                if (!hasExpectedElement)
                {
                    DlkLogger.LogError(new Exception("SetByRowColumn( ) execution failed. Expected element to Set( ) not found."));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SetByRowColumn( ) execution failed. : " + ex.Message, ex);
            }
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
        #endregion

        #region Methods

        private IWebElement getTable(string table)
        {
            switch (table.ToLower())
            {
                case "selected":
                    {
                        return _selectedTableList;
                    }
                case "available":
                    {
                        return _availableTableList;
                    }
            }

            return null;
        }

        private int getColumnIndexByHeader(string Column)
        {
            throw new NotImplementedException();
        }

        private void initialize()
        {
            FindElement();
            _availableTableList = mElement.FindElement(By.XPath(@".//table[@class='table']"));
            _selectedTableList = mElement.FindElement(By.XPath(@".//table[@class='table complexduallist_selectedtable']"));
        }

        private IList<IWebElement> getRowItemsFromTableList(IWebElement list)
        {
            IList<IWebElement> rowItemList = list.FindElements(By.XPath(@"./tbody/tr[@class!='" + ROW_ITEM_GROUP + "']"));
            return rowItemList;
        }

        private IWebElement getTableRowByRow(int row, IWebElement table)
        {
            IList<IWebElement> rowList = getRowList(table);

            if (row < rowList.Count)
            {
                return rowList[row];
            }
            else
            {
                throw new Exception("No Rows Found.");
            }
        }

        /*private void collapsedGroup(IWebElement rowItemElement, IWebElement table)
        {
            IList<IWebElement> rowGroupList = table.FindElements(By.XPath(@"./tbody/tr[@class='" + ROW_ITEM_GROUP + "']"));

            foreach (IWebElement rowGroup in rowGroupList)
            {
                IWebElement rowGroupButton = rowGroup.FindElement(By.XPath(@"./td/a"));
                string rowGroupButtonId = rowGroupButton.GetAttribute("id").Trim();

                if (rowItemElement.GetAttribute("class").Trim().Contains(rowGroupButtonId))
                {
                    DlkBaseControl rowGroupButtonControl = new DlkBaseControl("Row Group " + rowItemElement.Text, rowGroupButton);
                    rowGroupButtonControl.Click();
                    DlkLogger.LogInfo("Successfully collapsed group " + rowGroupButtonControl.GetValue().Trim());
                    break;
                }
            }
        }*/

        private void performButtonClickByRow(string row, IWebElement table)
        {
            string[] rows = row.Split('~');
            int rowIndex = Convert.ToInt32(rows[0]) - 1;
            IList<IWebElement> rowItemList = getRowList(table);

            if (rowIndex < rowItemList.Count)
            {
                IWebElement rowItemElement = rowItemList[rowIndex];

                if (rowItemElement.GetAttribute("class") != null &&
                    rowItemElement.GetAttribute("class").Trim().Equals(ROW_ITEM_GROUP))
                {
                    IWebElement rowGroup = rowItemElement.FindElement(By.XPath("./td/a"));
                    DlkLink rowGroupControl = new DlkLink("Row_Group : " + rowGroup.Text, rowGroup);
                    string rowGroupButtonId = string.Concat(rowGroup.GetAttribute("id").Trim(), " ");
                    //rowGroupControl.Click();
                    rowGroup.SendKeys(Keys.Enter);

                    performButtonClickByRow(rowItemElement.FindElement(By.XPath("../tr[contains(@class,'" + rowGroupButtonId + "') and not(contains(@style,'none'))][" + rows[1] + "]")));
                    //rowGroupControl.Click();
                    rowGroup.SendKeys(Keys.Enter);
                }
                else
                {
                    performButtonClickByRow(rowItemElement);
                }
            }
            else
            {
                throw new Exception("Item not existing.");
            }
        }

        private void performButtonClickByRow(IWebElement rowItemElement)
        {
            //DlkBaseControl addControl = new DlkBaseControl("Row_Button : " + rowItemElement.Text, rowItemElement.FindElement(By.XPath(ROW_BUTTON)));
            IWebElement addControl = rowItemElement.FindElement(By.XPath(ROW_BUTTON));
            bool isAddDisabled = addControl.GetAttribute("class").Contains("disabled") ? true : false;

            if (isAddDisabled)
            {
                DlkLogger.LogError(new Exception("Add is Disabled."));
            }
            else
            {
                addControl.Click();
            }
        }

        private IList<IWebElement> getRowList(IWebElement table)
        {
            return table.FindElements(By.XPath(@"./tbody/tr[not(contains(@style,'none'))]"));

            /*foreach (IWebElement element in rowList)
            {
                if (element.GetAttribute("class") != null &&
                    element.GetAttribute("class").Trim().Equals(ROW_ITEM_GROUP))
                {
                    //we do not want to add this into the list.
                }
                else
                {
                    filteredRowList.Add(element);
                }
            }

            return filteredRowList;*/
        }

        #endregion
    }
}
