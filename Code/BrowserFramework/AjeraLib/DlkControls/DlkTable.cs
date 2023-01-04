using System;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace AjeraLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit;

        private string mstrHeader = ".//th";

        private List<IWebElement> mlstHeaders;
        private List<string> mlstHeaderTexts = new List<String>();

        #endregion

        #region CONSTRUCTORS
        public DlkTable(string ControlName, string SearchType, string SearchValue) 
            : base(ControlName, SearchType, SearchValue){}

        public DlkTable(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues){}

        public DlkTable(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}

        public DlkTable(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkTable(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector){}

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        #endregion

        #region KEYWORDS

        [Keyword("SelectColumnHeader", new[] { "1|text|Column Header|Line*" })]
        public virtual void SelectColumnHeader(String ColumnHeader)
        {
            try
            {
                bool bFound;
                IWebElement header;
                FindHeader(ColumnHeader, out bFound, out header);
                if (bFound == false)
                {
                    throw new Exception("Column header " + ColumnHeader + " not found");
                }
                header.Click();
                DlkLogger.LogInfo("Successfully executed SelectColumnHeader(): " + ColumnHeader);
            }
            catch (Exception e)
            {
                throw new Exception("SelectColumnHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExistsColumnHeader", new[] { "1|text|Column Header|Line*" })]
        public virtual void VerifyExistsColumnHeader(String ColumnHeader, String IsTrueOrFalse)
        {
            try
            {
                bool bFound;
                IWebElement header;
                FindHeader(ColumnHeader, out bFound, out header);
                DlkAssert.AssertEqual("VerifyExistsColumnHeader() : " + mControlName, Convert.ToBoolean(IsTrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistsColumnHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnCount", new[] { "10" })]
        public virtual void VerifyColumnCount(String ExpectedColCount)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyColumnCount() : " + mControlName, CountColumn(), Convert.ToInt32(ExpectedColCount));
                    
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new[] { "10" })]
        public virtual void VerifyRowCount(String ExpectedRowCount)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyRowCount() : " + mControlName, CountRow(), Convert.ToInt32(ExpectedRowCount));

            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new[] { "1|text|Column Header|Line*" })]
        public virtual void VerifyCellValue(String Row, String Column, String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyCellValue() : " + mControlName, ExpectedValue, GetCellValue(Row,Column));

            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableCell", new[] { "1|2" })]
        public virtual void ClickTableCell(String Row, String Column)
        {
            try
            {
                IWebElement cell;
                GetCell(out cell, Row, Column);
                if (cell != null)
                {
                    cell.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCell() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableCellButton", new[] { "1|2" })]
        public virtual void ClickTableCellButton(String Row, String Column)
        {
            try
            {
                IWebElement cell;
                GetCell(out cell, Row, Column);
                IWebElement button = cell.FindWebElementCoalesce(By.XPath("./ancestor::span[1]//following-sibling::span[contains(@class,'imagebutton')]"), 
                    By.XPath("./ancestor::span[contains(@class,'imagebutton')]"));
                new DlkImageButton("CellButton", button).Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTableCell", new[] { "1|text|Column Header|Line*" })]
        public virtual void SetTableCell(String Row, String Column, String Value)
        {
            try
            {
                IWebElement cell;
                GetCell(out cell, Row, Column);
                
                cell.Clear();
                if (!string.IsNullOrEmpty(Value))
                {
                    cell.SendKeys(Value);

                    if (cell.GetAttribute("value").ToLower() != Value.ToLower())
                    {
                        cell.Clear();
                        cell.SendKeys(Value);
                    }
                }
                DlkLogger.LogInfo("Successfully executed SetTableCell()");
            }
            catch (Exception e)
            {
                throw new Exception("SetTableCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectTableRow", new[] { "1" })]
        public virtual void SelectTableRow(String Row)
        {
             IWebElement cell;
            try
            {
               
                GetCell(out cell, Row, "1");
                if (cell != null)
                {
                    cell.Click();
                }
            }
            catch (Exception)
            {
                try
                {
                    GetCell(out cell, Row, "2");
                    if (cell != null)
                    {
                        cell.Click();
                    }
                }
                catch(Exception e)
                {
                    throw new Exception("SelectTableRow() failed : " + e.Message, e);
                }
                
            }
        }

        [Keyword("SelectCellByColumn", new[] { "1" })]
        public virtual void SelectCellByColumn(String ColumnHeader, String Value, String Instance)
        {
            try
            {
                Initialize();

                //Find Column Header Index
                bool bFound;
                int colIndex = GetColumnIndex(ColumnHeader, out bFound);

                if (bFound)
                {
                    //Find Column Cell Values
                    IList<IWebElement> columnValues  = GetColumn(colIndex, Value);

                    if (columnValues.Count > 0)
                    {

                        var cell = columnValues[0];
                        if (!string.IsNullOrEmpty(Instance))
                        {

                            int instanceCount = Convert.ToInt32(Instance);
                            if (instanceCount > 0 && instanceCount <= columnValues.Count)
                            {
                                cell = columnValues[instanceCount - 1];
                            }
                        }

                        cell.Click();
                    }
                }
                else
                {
                    throw new Exception("Column["+ ColumnHeader + "] not found in the table.");
                }


            }
            catch (Exception e)
            {
                throw new Exception("SelectCellByColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public void AssignValueToVariable(String Row, String Column, String VariableName)
        {
            try
            {
                Initialize();
                IWebElement cell;
                GetCell(out cell, Row, Column);
                DlkFunctionHandler.AssignToVariable(VariableName, new DlkBaseControl("TableCell", cell).GetValue());
                DlkLogger.LogInfo("AssignValueToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemFromTableCell", new String[] { "1|text|Column Header|Line*" })]
        public virtual void SelectItemFromTableCell(String Row, String Column, String Value)
        {
            int retryLimit = 3;
            try
            {
                //find table
                //DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                Initialize();
                DlkTable table = new DlkTable("Table", mElement);
                if (table.Exists())
                {
                    IWebElement cell;
                    table.GetCell(out cell, Row, Column);
                    DlkLogger.LogInfo("SelectItemFromTableCell() : Expanding dropdown in cell [" + Row + "," + Column + "].");
                    cell.Click();

                    IWebElement itemMenu;
                    IList<IWebElement> menuItems = null;
                    int currRetry = 0;
                    bool bFound = false;
                    string actualItems = string.Empty;

                    string x_popupMenuContents = "//*[contains(@class,'popupmenu-main')]";

                    //check if popup or dropdown
                    if (mElement.FindElements(By.XPath(x_popupMenuContents)).Count > 0)
                    {
                        //find popUp
                        itemMenu = mElement.FindElement(By.XPath(x_popupMenuContents));

                        //Look for items in the popupmenu/dropdown
                        menuItems = itemMenu.FindElements(By.CssSelector("div"));
                        while (++currRetry <= retryLimit && !bFound)
                        {
                            foreach (IWebElement aListItem in menuItems)
                            {
                                var dlkTreeItem = new DlkBaseControl("PopUp Item", aListItem);
                                if (currRetry <= 1)
                                {
                                    actualItems = actualItems + dlkTreeItem.GetValue() + " ";
                                }
                                if (dlkTreeItem.GetValue().ToLower() == Value.ToLower())
                                {
                                    dlkTreeItem.MouseOver();
                                    dlkTreeItem.Click();
                                    Thread.Sleep(1000);
                                    DlkLogger.LogInfo("SelectItemFromTableCell() : Successfully selected item [" + Value + "] from popup dropdown.");
                                    bFound = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //find contents of dropdown
                        SelectElement dropdown = new SelectElement(cell.FindElement(By.TagName("select")));
                        dropdown.SelectByText(Value);
                        cell.Click();

                        DlkLogger.LogInfo("SelectItemFromTableCell() : Successfully selected item [" + Value + "] from dropdown.");
                        bFound = true;
                    }

                    if (!bFound)
                    {
                        throw new Exception(Value + " not found in list. : Actual List = " + actualItems);
                    }
                }
                else
                {
                    throw new Exception(mControlName + " - Widget not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemFromTableCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectTableColumnHeader", new String[] { "Unapproved Time | Supervisor" })]
        public virtual void SelectTableColumnHeader(String ColumnHeader)
        {
            try
            {
                //find table
                //DlkTable table = new DlkTable("Table", "XPath", mSearchValues[0] + "//div[text()='" + WidgetCaption + "']/ancestor::div[1]/following-sibling::div[1]//table[@class='ax-axgrid']");
                Initialize();
                DlkTable table = new DlkTable("Table", mElement);
                if (table.Exists())
                {
                    bool bFound;
                    IWebElement header;
                    table.FindHeader(ColumnHeader, out bFound, out header);
                    if (bFound == false)
                    {
                        throw new Exception("Column header " + ColumnHeader + " not found");
                    }
                    else
                    {
                        DlkBaseControl headerCtrl = new DlkBaseControl("Header", header);
                        headerCtrl.ScrollIntoViewUsingJavaScript();
                        headerCtrl.Click();
                        DlkLogger.LogInfo("Successfully executed SelectColumnHeader(): " + ColumnHeader);
                    }
                }
                else
                {
                    throw new Exception(mControlName + " - Table not found");
                }

            }
            catch (Exception e)
            {
                throw new Exception("SelectTableColumnHeader() failed : " + e.Message, e);
            }
        }

        #endregion

        #region METHOD
        private void RefreshHeaders()
        {
            FindElement();
            mlstHeaders = new List<IWebElement>();
            mlstHeaderTexts = new List<String>();

            var mClassName = mElement.GetAttribute("class");
            if (!mClassName.ToLower().Equals("ax-axgrid"))
            {
                mstrHeader = ".//thead[contains(@class,'clone')]//th";
            }
            IList<IWebElement> headers  = mElement.FindElements(By.XPath(mstrHeader));

            foreach (IWebElement columnHeader in headers)
            {
                var header = columnHeader;
                if (header.GetAttribute("innerHTML").Contains("<span"))
                {
                    header = columnHeader.FindElement(By.CssSelector("span"));
                }

                if (header.GetCssValue("display") != "none" && columnHeader.GetCssValue("width") != "0px")
                {
                    mlstHeaders.Add(header);
                    mlstHeaderTexts.Add(GetColumnHeaderText(header));
                }

            }
        }

        public int CountColumn()
        {
            FindElement();
            //based on header since ajera's table may contain merged cells

            var mClassName = mElement.GetAttribute("class");
            if (!mClassName.ToLower().Equals("ax-axgrid"))
            {
                mstrHeader = "//thead[contains(@class,'clone')]//th";
            }
            IList<IWebElement> columns = mElement.FindElements(By.XPath(mstrHeader));

            int count = columns.Count(col => col.GetCssValue("display") != "none" && (col.GetCssValue("width") != "0px") && (col.GetCssValue("width") != "1px"));
            
            return count;
        }

        public int CountRow()
        {
            FindElement();
            IList<IWebElement> rows = mElement.FindElements(By.CssSelector("tbody>tr"));

            int count = rows.Count(row => row.GetCssValue("display") != "none" && row.GetCssValue("width") != "0px");
            return count;
        }

        public string GetCellValue(string row, string column)
        {
            FindElement();
            IWebElement cellInLocation = null;
            GetCell(out cellInLocation, row, column);
            return DlkString.ReplaceCarriageReturn(new DlkBaseControl("Cell", cellInLocation).GetValue(),"");
        }

        public void GetCell(out IWebElement cell, string row, string column)
        {
            Initialize();
            int rowNumber = Convert.ToInt32(row);
            int colNumber = Convert.ToInt32(column);
            colNumber = AdjustForColSpan(rowNumber, colNumber);

            cell = mElement.FindElement(By.XPath(".//tbody/tr[not(contains(@style,'none'))][" + rowNumber + "]/td[not(contains(@style,'none'))][" + colNumber + "]/div"));
          
            var tempCellContainerImg = cell.FindElements(By.CssSelector("img")).Where(x => x.Displayed);
            var tempCellContainerTextArea = cell.FindElements(By.CssSelector("textarea")).Where(x => x.Displayed);
            if (cell.TagName == "")
                cell = null;
            else
            {
                if (cell.FindElement(By.XPath("./..")).GetAttribute("innerHTML").Contains("<textarea") && (cell.FindElements(By.XPath("./preceding-sibling::textarea")).Count > 0))
                {
                    cell = cell.FindElement(By.XPath("./preceding-sibling::textarea"));
                }
                else if (cell.GetAttribute("innerHTML").Contains("<label"))
                {
                    cell = cell.FindElement(By.CssSelector("label"));
                }
                else if (cell.GetAttribute("innerHTML").Contains("<a"))
                {
                    cell = cell.FindElement(By.CssSelector("a"));
                }
                else if (cell.GetAttribute("innerHTML").Contains("<textarea") && (tempCellContainerTextArea.Count() > 0))
                {
                    cell = cell.FindElement(By.CssSelector("textarea"));
                }
                else if (cell.GetAttribute("innerHTML").Contains("<img") && (tempCellContainerImg.Count() > 0))
                {
                    cell = cell.FindElements(By.CssSelector("img")).Where(x => x.Displayed).First();
                }
                else if (cell.GetAttribute("innerHTML").Contains("<input"))
                {
                    cell = cell.FindElement(By.CssSelector("input"));
                }
                else if (cell.GetAttribute("innerHTML").Contains("<svg"))
                {
                    cell = cell.FindElement(By.CssSelector("svg"));
                }
            }
        }

        public IList<IWebElement> GetColumn(int colNumber, string value)
        {
            Initialize();
            IList<IWebElement> columnCells = new List<IWebElement>();
            columnCells =  mElement.FindElements(By.XPath(".//tbody//td[not(contains(@style,'none'))][" + colNumber + "]/div/*[text()='" + value +"']"));
            return columnCells;

        }

        public void FindHeader(string ColumnHeader, out bool bFound, out IWebElement header)
        {
            Initialize();
            RefreshHeaders();

            header = null;
            bFound = false;

            foreach (var hDrName in mlstHeaders)
            {
                if (Equals(ColumnHeader.ToLower(), GetColumnHeaderText(hDrName).ToLower()))
                {
                    bFound = true;
                    header = hDrName;
                    break;
                }
            }
        }

        private String GetColumnHeaderText(IWebElement columnHeader)
        {
            String headerText = "";
            headerText = new DlkBaseControl("ColumnHeader", columnHeader).GetValue();
            return DlkString.RemoveCarriageReturn(headerText.Trim());
        }

        private int GetColumnIndex(string columnHeader, out bool bFound)
        {
            RefreshHeaders();
            bFound = false;
            int columnCount = 0;

            foreach (var hDrName in mlstHeaders)
            {
                columnCount++;
                
                if (Equals(columnHeader.ToLower(), GetColumnHeaderText(hDrName).ToLower()))
                {
                    bFound = true;
                    return columnCount;
                }
                else if (Equals(columnHeader.ToLower(), GetColumnHeaderText(hDrName.FindElement(By.XPath("./ancestor::div/descendant::span[contains(@class,'header')]/label"))).ToLower()))
                {
                    bFound = true;
                    return columnCount;
                }
            }

            return 0;
        }

        private int AdjustForColSpan(int row, int colNumber)
        {
            //Get all cells for the row
            List<IWebElement> cells = mElement.FindElements(By.XPath(".//tbody/tr[not(contains(@style,'none'))][" + row + "]/td[not(contains(@style,'none'))]")).ToList();
            int colspan =0;

            //Check if there are cells that have colspan before our target column number.
            for(int i = 0; i < cells.Count ; i++)
            {
                if (i == colNumber - 1)
                {
                    break;
                }
                else if (cells[i].GetAttribute("colspan") != null)
                {
                    colspan += Convert.ToInt32(cells[i].GetAttribute("colspan")) - 1;
                }
                
            }
            return colNumber - colspan;
        }
        #endregion
    }

}
