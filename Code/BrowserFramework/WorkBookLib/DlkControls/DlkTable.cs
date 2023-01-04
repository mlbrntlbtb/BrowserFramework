using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Collections;
using OpenQA.Selenium.Support.UI;

namespace WorkBookLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        #region PRIVATE VARIABLES

        private IList<IWebElement> mColumnHeaders;
        private ArrayList mColumnNames = new ArrayList();
        private ArrayList mColumnNoOfScrolls = new ArrayList();
        private ArrayList mColumnIndexPerScroll = new ArrayList();
        //private bool mFailedHorizontalScroll = false;
        private int mScrollToColumn = 0;
        private IList<IWebElement> mTableRows;
        private static String mColumnContainerXPath = ".//*[@class='wj-colheaders']";
        private static String mColumnHeaderXPath = ".//*[contains(@class,'wj-header')]";
        private static String mColumnHeaderXPath2 = ".//*[contains(@role,'columnheader')]";
        private static bool mUsedTabToCell = false;
        private ArrayList mRowContents = new ArrayList();
        private ArrayList mRowNoOfScrolls = new ArrayList();
        private ArrayList mRowIndexPerScroll = new ArrayList();
        private int mScrollToRow = 0;
        private ArrayList mDdownContents = new ArrayList();
        private ArrayList mDdownNoOfScrolls = new ArrayList();
        private ArrayList mDdownIndexPerScroll = new ArrayList();
        private int mScrollToDdown = 0;
        private static String mRowContainerXPath = ".//*[@class='wj-cells']";
        private static String mRowXPath = ".//*[@class='wj-row'][not(descendant::div[@role='columnheader'])]";
        private static String mContextMenuXPath = "//*[contains(@id,'wbcontextmenu')][not(contains(@class,'Overlay'))][not(contains(@class,'hidden'))]";
        private static String mTableCellXPath = ".//div[@role='gridcell']";
        private static String mTableCellSelectedXPath = ".//div[@role='gridcell'][contains(@class,'wj-state-selected')] | .//div[@role='gridcell'][contains(@class,'wj-state-active')]";
        private static String mCellComboBoxList1XPath = "//div[contains(@class,'WijmoColumnDropDownListBox')][@role='listbox']";
        private static String mCellComboBoxList2XPath = "//div[contains(@class,'AutoCompleteColumnDropdown')]";
        private static String mCellComboBoxItem1XPath = ".//*[contains(@class,'wj-listbox-item')]";
        private static String mCellComboBoxItem2XPath = ".//*[contains(@class,'DropDownItem')]";
        private static String mTableContainerXPath = ".//div[@wj-part='root']";
        private string mControl = "";
        private const String mCellTextBox = "textbox";
        private const String mCellCheckBox = "checkbox";
        private const String mCellComboBox = "combobox";
        private const String mCellDatePicker = "datepicker";
        private const String mCellImage = "img";
        #endregion

        #region CONSTRUCTORS
        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize(bool getRowColumn = true)
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            ScrollReset();

            if (getRowColumn)
            {
                GetColumnHeaders();
                GetRows();
            }
            else
                GetRows();
        }

        public IWebElement GetCell(string LineIndex, string ColNameOrIndex, bool PartialColumn = false)
        {
            ScrollReset();
            IWebElement targetRow = GetTargetRow(LineIndex);
            IWebElement targetCell = null;
            mUsedTabToCell = false;

            int targetColumnNumber = 0;
            if (!int.TryParse(ColNameOrIndex, out targetColumnNumber))
            {
                if (!PartialColumn)
                    targetColumnNumber = GetColumnIndexFromName(ColNameOrIndex);
                else
                    targetColumnNumber = GetColumnIndexFromPartialName(ColNameOrIndex);
            }
            else
                targetColumnNumber = Convert.ToInt32(ColNameOrIndex) - 1;

            if (targetColumnNumber == -1)
                throw new Exception("Column '" + ColNameOrIndex + "' was not found.");

            //Check if column header has value for NoOfScrolls and IndexPerScroll
            if(mColumnNoOfScrolls.Count > 0)
            {
                targetCell = PressTabToCell(targetColumnNumber, Convert.ToInt32(LineIndex), targetRow);
                mUsedTabToCell = true;
            }
            else
            {
                List<IWebElement> targetCells = targetRow.FindElements(By.XPath(mTableCellXPath)).Where(x => x.Displayed).ToList();
                targetCell = targetCells.ElementAt(targetColumnNumber);
            }

            if (targetCell == null)
                throw new Exception("Cell not found.");

            return targetCell;
        }
        
        private string GetCellValue(string LineIndex, string ColNameOrIndex, bool PartialColumn = false)
        {
            string ActualValue = "";
            mControl = "";
            IWebElement targetCell = GetCell(LineIndex.ToString(), ColNameOrIndex, PartialColumn);

            if (targetCell.FindElements(By.XPath(".//input[@type='checkbox']")).Count > 0)
            {
                mControl = mCellCheckBox;
            }
            else if (targetCell.FindElements(By.TagName("img")).Count > 0)
            {
                mControl = mCellImage;
            }

            switch (mControl)
            {
                case mCellCheckBox:
                    DlkCheckBox checkBox = new DlkCheckBox("CheckBox Cell", targetCell);
                    ActualValue = checkBox.GetState().ToString();
                    break;
                case mCellImage:
                    IWebElement imageElm = targetCell.FindElement(By.TagName("img"));
                    string imageVal = imageElm.GetAttribute("data-tooltip") != null ?
                        imageElm.GetAttribute("data-tooltip").ToString() :
                        targetCell.GetAttribute("data-tooltip") != null ?
                        targetCell.GetAttribute("data-tooltip").ToString() : 
                        DlkString.RemoveCarriageReturn(targetCell.Text);
                    ActualValue = imageVal;
                    break;
                default:
                    ActualValue = DlkString.RemoveCarriageReturn(targetCell.Text);
                    break;
            }
            return ActualValue;
        }

        private string GetCellReadOnly(string LineIndex, string ColNameOrIndex)
        {
            string ActualValue = "";
            IWebElement targetCell = GetCell(LineIndex.ToString(), ColNameOrIndex);

            if (targetCell.FindElements(By.XPath(".//input[@type='checkbox']")).Count > 0)
            {
                mControl = mCellCheckBox;
            }

            switch (mControl)
            {
                case mCellCheckBox:
                    IWebElement targetCheckBox = targetCell.FindElement(By.TagName("input"));
                    ActualValue = new DlkBaseControl("Cell", targetCheckBox).IsReadOnly();
                    break;
                default:
                    ActualValue = new DlkBaseControl("Cell", targetCell).IsReadOnly();
                    break;
            }
            return ActualValue;
        }

        public void ClearField(IWebElement targetElement)
        {
            if (DlkEnvironment.mBrowser != "safari")
            {
                targetElement.SendKeys(Keys.Control + "a");
                targetElement.SendKeys(Keys.Delete);
            }
            else
            {
                targetElement.SendKeys(Keys.Command + "a");
                targetElement.SendKeys(Keys.Delete);
            }

        }
        #endregion

        #region PRIVATE METHODS

        private void GetColumnHeaders()
        {
            mColumnNames.Clear();
            mColumnNoOfScrolls.Clear();
            mColumnIndexPerScroll.Clear();
            mScrollToColumn = 0;

            IWebElement headerContainer = mElement.FindElement(By.XPath(mColumnContainerXPath)) != null ?
                mElement.FindElement(By.XPath(mColumnContainerXPath)) : throw new Exception("Table headers not found.");
            
            mColumnHeaders = headerContainer.FindElements(By.XPath(mColumnHeaderXPath))
                        .Where(x => x.Displayed).ToList().Count != 0 ?
                        headerContainer.FindElements(By.XPath(mColumnHeaderXPath)).Where(x => x.Displayed).ToList() :
                        mElement.FindElements(By.XPath(mColumnHeaderXPath2))
                        .Where(x => x.Displayed).ToList().Count != 0 ?
                        mElement.FindElements(By.XPath(mColumnHeaderXPath2)).Where(x => x.Displayed).ToList() :
                        mElement.FindElements(By.XPath(mColumnHeaderXPath2)).ToList();

            foreach (IWebElement col in mColumnHeaders)
            {
                string currentCol = DlkString.ReplaceCarriageReturn(col.Text.ToLower().Trim(), "");

                //Check column name in element's data tooltip
                if (String.IsNullOrEmpty(currentCol))
                {
                    currentCol = col.GetAttribute("data-tooltip") != null ?
                        DlkString.ReplaceCarriageReturn(col.GetAttribute("data-tooltip").ToLower().Trim(), "") : "";
                }
                
                //Check column name in element image's data tooltip
                if (String.IsNullOrEmpty(currentCol))
                {
                    IWebElement columnImage = col.FindElements(By.TagName("img")).Count > 0 ?
                        col.FindElement(By.TagName("img")) : null;
                    currentCol = columnImage != null ? columnImage.GetAttribute("data-tooltip") != null ?
                        DlkString.ReplaceCarriageReturn(columnImage.GetAttribute("data-tooltip").ToLower().Trim(), "") :
                        "" : "";
                }

                //Check column's value 
                if (String.IsNullOrEmpty(currentCol))
                {
                    currentCol = new DlkBaseControl("Target Column", col).GetValue().ToLower().Trim();
                }

                mColumnNames.Add(currentCol);
            }

            //Scroll to get all column headers that are hidden under horizontal scroll
            ScrollGetAllColumns(headerContainer);
        }

        private int GetColumnIndexFromName(string ColumnName)
        {
            if (mColumnNames.Count == 0)
                GetColumnHeaders();

            int index = -1;
            for (int i = 0; i < mColumnNames.Count; i++)
            {
                if (mColumnNames[i].ToString() == ColumnName.ToLower())
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private int GetColumnIndexFromPartialName(string ColumnName)
        {
            if (mColumnNames.Count == 0)
                GetColumnHeaders();

            int index = -1;
            for (int i = 0; i < mColumnNames.Count; i++)
            {
                if (mColumnNames[i].ToString().Contains(ColumnName.ToLower()))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void GetRows()
        {
            mRowContents.Clear();
            mRowNoOfScrolls.Clear();
            mRowIndexPerScroll.Clear();
            mScrollToRow = 0;

            IWebElement rowContainer = mElement.FindElement(By.XPath(mRowContainerXPath)) != null ?
                mElement.FindElement(By.XPath(mRowContainerXPath)) : throw new Exception("Rows not found.");

            mTableRows = rowContainer.FindElements(By.XPath(mRowXPath))
                .Where(x => x.Displayed && x.Enabled).ToList();

            foreach (IWebElement row in mTableRows)
            {
                string rowContent = DlkString.ReplaceCarriageReturn(row.Text.ToLower().Trim(), "") != "" ?
                    DlkString.ReplaceCarriageReturn(row.Text.ToLower().Trim(), "") :
                    DlkString.ReplaceCarriageReturn(new DlkBaseControl("Row", row).GetValue().ToLower().Trim(), "");

                mRowContents.Add(rowContent);
            }
            
            //Scroll to get all rows that are hidden under vertical scroll
            ScrollGetAllRows(rowContainer);
        }

        private IWebElement GetTargetRow(string LineIndex)
        {
            IWebElement targetRow = null;
            int targetRowNumber = Convert.ToInt32(LineIndex) - 1;
            //Check if all rows has value for NoOfScrolls and IndexPerScroll
            int mScrollsToGetToRow = 0;
            int mIndexPerScrollOfRow = 0;
            if (mRowNoOfScrolls.Count > 0)
            {
                mScrollsToGetToRow = Convert.ToInt32(mRowNoOfScrolls[targetRowNumber].ToString());
                mIndexPerScrollOfRow = Convert.ToInt32(mRowIndexPerScroll[targetRowNumber].ToString());
                targetRow = ScrollVerticalToRow(mScrollsToGetToRow, mIndexPerScrollOfRow);
            }
            else
            {
                GetRows();
                targetRow = mTableRows.ElementAt(Convert.ToInt32(LineIndex) - 1);
            }

            if (targetRow == null)
                throw new Exception("Row not found.");

            return targetRow;
        }

        private IWebElement GetTargetColumn(String ColNameOrIndex)
        {
            int targetColumnNumber = 0;
            if (!int.TryParse(ColNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(ColNameOrIndex);
            else
                targetColumnNumber = Convert.ToInt32(ColNameOrIndex) - 1;

            if (targetColumnNumber == -1)
                throw new Exception("Column '" + ColNameOrIndex + "' was not found.");

            IWebElement targetColumn = mColumnHeaders.ElementAt(targetColumnNumber);
            return targetColumn;
        }
        
        private void ScrollReset()
        {
            IWebElement targetTable = mElement.FindElements(By.XPath(mTableContainerXPath)).Count > 0 ?
                   mElement.FindElement(By.XPath(mTableContainerXPath)) : null;

            if (targetTable != null)
            {
                int retryTopScroll = 0;
                int maxRetryTopScroll = 5;
                int retryLeftScroll = 0;
                int maxRetryLeftScroll = 5;

                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

                int scrollYPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollTop", targetTable));
                int scrollXPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollLeft", targetTable));
                
                //Reset vertical scroll bar
                while (scrollYPos != 0)
                {
                    if (retryTopScroll >= maxRetryTopScroll)
                        throw new Exception("Vertical scroll bar cannot be reset");

                    jse.ExecuteScript("arguments[0].scrollTop = 0", targetTable);
                    scrollYPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollTop", targetTable));
                    retryTopScroll++;
                }

                //Reset horizontal scroll bar
                while (scrollXPos != 0)
                {
                    if (retryLeftScroll >= maxRetryLeftScroll)
                        throw new Exception("Horizontal scroll bar cannot be reset");

                    jse.ExecuteScript("arguments[0].scrollLeft = 0", targetTable);
                    scrollXPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollLeft", targetTable));
                    retryLeftScroll++;
                }
            }
        }

        private void ScrollGetAllRows(IWebElement rowContainer)
        {
            IWebElement targetTable = mElement.FindElements(By.XPath(mTableContainerXPath)).Count > 0 ?
                   mElement.FindElement(By.XPath(mTableContainerXPath)) : null;

            if (targetTable != null)
            {
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

                actions.MoveToElement(mElement).Click();

                int scrollHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollHeight", targetTable));
                int clientHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].clientHeight", targetTable));
                
                //Check if vertical scrollbar exists
                if (scrollHeight != clientHeight)
                {
                    //Set default values of no.of scrolls and index per scroll to rows
                    for (int r = 0; r <= mRowContents.Count - 1; r++)
                    {
                        mRowNoOfScrolls.Add(0);
                        mRowIndexPerScroll.Add(r);
                    }

                    int maxHeight = scrollHeight - clientHeight;
                    int topScrollValue = maxHeight < 100 ? maxHeight : 100;
                    int scrollYPos = 0;

                    jse.ExecuteScript("arguments[0].scrollTop = 0", targetTable);

                    while (scrollYPos <= maxHeight)
                    {
                        int checkLimit = scrollYPos + topScrollValue;
                        if (checkLimit > maxHeight)
                        {
                            jse.ExecuteScript("arguments[0].scrollTop = " + maxHeight.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling limit has been reached");

                            if(topScrollValue == 100)
                                mScrollToRow++;

                            //Re-initialize rows per scroll
                            mTableRows = rowContainer.FindElements(By.XPath(mRowXPath))
                                .Where(x => x.Displayed).ToList();
                            
                            for (int r = 0; r < mTableRows.Count; r++)
                            {
                                string currentRow = DlkString.ReplaceCarriageReturn(mTableRows[r].Text.ToLower().Trim(), "") != "" ?
                                    DlkString.ReplaceCarriageReturn(mTableRows[r].Text.ToLower().Trim(), "") :
                                    DlkString.ReplaceCarriageReturn(new DlkBaseControl("Row", mTableRows[r]).GetValue().ToLower().Trim(), "");

                                if (mRowContents.Contains(currentRow))
                                {
                                    int index = mRowContents.IndexOf(currentRow);

                                    mRowNoOfScrolls.RemoveAt(index);
                                    mRowIndexPerScroll.RemoveAt(index);

                                    mRowNoOfScrolls.Insert(index, mScrollToRow);
                                    mRowIndexPerScroll.Insert(index, r);
                                }
                                else
                                {
                                    mRowContents.Add(currentRow);
                                    mRowNoOfScrolls.Add(mScrollToRow);
                                    mRowIndexPerScroll.Add(r);
                                }
                            }
                            break;
                        }
                        else
                        {
                            jse.ExecuteScript("arguments[0].scrollTop += " + topScrollValue.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling down to reach all rows...");
                            mScrollToRow++;

                            //Re-initialize rows per scroll
                            mTableRows = rowContainer.FindElements(By.XPath(mRowXPath))
                                .Where(x => x.Displayed).ToList();
                            
                            for (int r = 0; r < mTableRows.Count; r++)
                            {
                                string currentRow = DlkString.ReplaceCarriageReturn(mTableRows[r].Text.ToLower().Trim(), "") != "" ?
                                    DlkString.ReplaceCarriageReturn(mTableRows[r].Text.ToLower().Trim(), "") :
                                    DlkString.ReplaceCarriageReturn(new DlkBaseControl("Row", mTableRows[r]).GetValue().ToLower().Trim(), "");

                                if (mRowContents.Contains(currentRow))
                                {
                                    int index = mRowContents.IndexOf(currentRow);

                                    mRowNoOfScrolls.RemoveAt(index);
                                    mRowIndexPerScroll.RemoveAt(index);

                                    mRowNoOfScrolls.Insert(index, mScrollToRow);
                                    mRowIndexPerScroll.Insert(index, r);
                                }
                                else
                                {
                                    mRowContents.Add(currentRow);
                                    mRowNoOfScrolls.Add(mScrollToRow);
                                    mRowIndexPerScroll.Add(r);
                                }
                            }
                        }
                            
                        scrollYPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollTop", targetTable));
                        Thread.Sleep(1000);
                    }

                    //Reset scroll back to default
                    mScrollToRow = 0;
                    jse.ExecuteScript("arguments[0].scrollTop = 0", targetTable);
                }
            }
            else
            {
                throw new Exception("Root element not found on current table");
            }
        }

        private void ScrollGetAllColumns(IWebElement headerContainer)
        {
            IWebElement targetTable = mElement.FindElements(By.XPath(mTableContainerXPath)).Count > 0 ?
                   mElement.FindElement(By.XPath(mTableContainerXPath)) : null;

            if (targetTable != null)
            {
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

                actions.MoveToElement(mElement).Click();
                
                int scrollWidth = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollWidth", targetTable));
                int clientWidth = Convert.ToInt32(jse.ExecuteScript("return arguments[0].clientWidth", targetTable));

                //Check if horizontal scrollbar exists
                if (scrollWidth != clientWidth)
                {
                    //Set default values of no.of scrolls and index per scroll to column headers
                    for (int c = 0; c <= mColumnNames.Count - 1; c++)
                    {
                        mColumnNoOfScrolls.Add(0);
                        mColumnIndexPerScroll.Add(c);
                    }
                    
                    int maxWidth = scrollWidth - clientWidth;
                    int leftScrollValue = maxWidth < 200 ? maxWidth : 200;
                    int scrollXPos = 0;

                    jse.ExecuteScript("arguments[0].scrollLeft = 0", targetTable);
                    
                    while (scrollXPos <= maxWidth)
                    {
                        int checkLimit = scrollXPos + leftScrollValue;
                        if (checkLimit > maxWidth)
                        {
                            jse.ExecuteScript("arguments[0].scrollLeft = " + maxWidth.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling limit has been reached");
                            mScrollToColumn++;

                            Thread.Sleep(1000);
                            //Re-initialize column headers per scroll
                            mColumnHeaders = headerContainer.FindElements(By.XPath(mColumnHeaderXPath))
                                .Where(x => x.Displayed).ToList();

                            for(int c=0; c < mColumnHeaders.Count; c++)
                            {
                                string currentCol = DlkString.ReplaceCarriageReturn(mColumnHeaders[c].Text.ToLower().Trim(), "");
                                
                                //Check column name in element's data tooltip
                                if (String.IsNullOrEmpty(currentCol))
                                {
                                    currentCol = mColumnHeaders[c].GetAttribute("data-tooltip") != null ?
                                        DlkString.ReplaceCarriageReturn(mColumnHeaders[c].GetAttribute("data-tooltip").ToLower().Trim(), "") : "";
                                }
                                
                                //Check column name in element image's data tooltip
                                if (String.IsNullOrEmpty(currentCol))
                                {
                                    IWebElement columnImage = mColumnHeaders[c].FindElements(By.TagName("img")).Count > 0 ?
                                        mColumnHeaders[c].FindElement(By.TagName("img")) : null;
                                    currentCol = columnImage != null ? columnImage.GetAttribute("data-tooltip") != null ?
                                        DlkString.ReplaceCarriageReturn(columnImage.GetAttribute("data-tooltip").ToLower().Trim(), "") :
                                        "" : "";
                                }
                                
                                if (!mColumnNames.Contains(currentCol) || String.IsNullOrEmpty(currentCol))
                                {
                                    mColumnNames.Add(currentCol);
                                    mColumnNoOfScrolls.Add(mScrollToColumn);
                                    mColumnIndexPerScroll.Add(c);
                                }
                            }
                            break;
                        }
                        else
                        {
                            jse.ExecuteScript("arguments[0].scrollLeft += " + leftScrollValue.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling right to reach all columns...");
                            mScrollToColumn++;

                            Thread.Sleep(1000);
                            //Re-initialize column headers per scroll
                            mColumnHeaders = headerContainer.FindElements(By.XPath(mColumnHeaderXPath))
                                .Where(x => x.Displayed).ToList();

                            for (int c = 0; c < mColumnHeaders.Count; c++)
                            {
                                string currentCol = DlkString.ReplaceCarriageReturn(mColumnHeaders[c].Text.ToLower().Trim(), "");

                                //Check column name in element's data tooltip
                                if (String.IsNullOrEmpty(currentCol))
                                    currentCol = mColumnHeaders[c].GetAttribute("data-tooltip") != null ?
                                        DlkString.ReplaceCarriageReturn(mColumnHeaders[c].GetAttribute("data-tooltip").ToLower().Trim(), "") : "";

                                //Check column name in element image's data tooltip
                                if (String.IsNullOrEmpty(currentCol))
                                {
                                    IWebElement columnImage = mColumnHeaders[c].FindElements(By.TagName("img")).Count > 0 ?
                                        mColumnHeaders[c].FindElement(By.TagName("img")) : null;
                                    currentCol = columnImage != null ? columnImage.GetAttribute("data-tooltip") != null ?
                                        DlkString.ReplaceCarriageReturn(columnImage.GetAttribute("data-tooltip").ToLower().Trim(), "") :
                                        "" : "";
                                }

                                if (!mColumnNames.Contains(currentCol) || String.IsNullOrEmpty(currentCol))
                                {
                                    mColumnNames.Add(currentCol);
                                    mColumnNoOfScrolls.Add(mScrollToColumn);
                                    mColumnIndexPerScroll.Add(c);
                                }
                            }
                        }
                        scrollXPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollLeft", targetTable));
                    }
                    //Reset scroll back to default
                    mScrollToColumn = 0;
                    jse.ExecuteScript("arguments[0].scrollLeft = 0", targetTable);
                }
            }
            else
            {
                throw new Exception("Root element not found on current table");
            }
        }

        private IWebElement ScrollVerticalToRow(int noOfScrolls, int indexPerScroll)
        {
            IWebElement targetRow = null;
            IWebElement targetTable = mElement.FindElements(By.XPath(mTableContainerXPath)).Count > 0 ?
                   mElement.FindElement(By.XPath(mTableContainerXPath)) : null;

            if (targetTable != null)
            {
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

                actions.MoveToElement(mElement).Click();

                int scrollHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollHeight", targetTable));
                int clientHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].clientHeight", targetTable));

                IWebElement rowContainer = mElement.FindElement(By.XPath(mRowContainerXPath)) != null ?
                               mElement.FindElement(By.XPath(mRowContainerXPath)) : throw new Exception("Rows not found.");

                //Check if vertical scrollbar exists
                if (scrollHeight != clientHeight)
                {
                    int maxHeight = scrollHeight - clientHeight;
                    int topScrollValue = maxHeight < 100 ? maxHeight : 100;
                    int scrollYPos = 0;

                    jse.ExecuteScript("arguments[0].scrollTop = 0", targetTable);

                    for (int s = 1; s <= noOfScrolls; s++)
                    {
                        int checkLimit = scrollYPos + topScrollValue;
                        if (checkLimit > maxHeight)
                        {
                            jse.ExecuteScript("arguments[0].scrollTop = " + maxHeight.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling limit has been reached");
                        }
                        else
                        {
                            jse.ExecuteScript("arguments[0].scrollTop += " + topScrollValue.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling down to reach all rows...");
                        }
                        
                        scrollYPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollTop", targetTable));
                        Thread.Sleep(1000);
                    }

                    List<IWebElement> targetRows = rowContainer.FindElements(By.XPath(mRowXPath))
                        .Where(x => x.Displayed).ToList();

                    targetRow = targetRows.ElementAt(indexPerScroll);
                }

                return targetRow;
            }
            else
            {
                throw new Exception("Root element not found on current table");
            }
        }

        private IWebElement ScrollHorizontalToCell(int noOfScrolls, int indexPerScroll, IWebElement targetRow)
        {
            IWebElement targetCell = null;
            IWebElement targetTable = mElement.FindElements(By.XPath(mTableContainerXPath)).Count > 0 ?
                   mElement.FindElement(By.XPath(mTableContainerXPath)) : null;

            if (targetTable != null)
            {
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

                actions.MoveToElement(mElement).Click();

                int scrollWidth = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollWidth", targetTable).ToString());
                int clientWidth = Convert.ToInt32(jse.ExecuteScript("return arguments[0].clientWidth", targetTable).ToString());

                //Check if horizontal scrollbar exists
                if (scrollWidth != clientWidth)
                {
                    int maxWidth = scrollWidth - clientWidth;
                    int leftScrollValue = maxWidth < 200 ? maxWidth : 200;
                    int scrollXPos = 0;
                    
                    jse.ExecuteScript("arguments[0].scrollLeft = 0", targetTable);

                    for (int s = 1; s <= noOfScrolls; s++)
                    {
                        int checkLimit = scrollXPos + leftScrollValue;
                        if (checkLimit > maxWidth)
                        {
                            jse.ExecuteScript("arguments[0].scrollLeft = " + maxWidth.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling limit has been reached");
                        }
                        else
                        {
                            jse.ExecuteScript("arguments[0].scrollLeft += " + leftScrollValue.ToString(), targetTable);
                            DlkLogger.LogInfo("Scrolling right to reach all cells...");
                        }
                        
                        scrollXPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollLeft", targetTable).ToString());
                        Thread.Sleep(1000);
                    }

                    List<IWebElement> targetCells = targetRow.FindElements(By.XPath(mTableCellXPath)).Where(x => x.Displayed).ToList();
                    targetCell = targetCells.ElementAt(indexPerScroll);
                }

                return targetCell;
            }
            else
            {
                throw new Exception("Root element not found on current table");
            }
        }

        private IWebElement PressTabToCell(int targetColumnNumber, int rowIndex, IWebElement targetRow)
        {
            IWebElement targetCell = null;
            IWebElement targetTable = mElement.FindElements(By.XPath(mTableContainerXPath)).Count > 0 ?
                   mElement.FindElement(By.XPath(mTableContainerXPath)) : null;

            if (targetTable != null)
            {
                //Retrieve target cell by clicking row header
                int targetRowIndex = 0;
                if (mRowNoOfScrolls.Count > 0)
                    targetRowIndex = Convert.ToInt32(mRowIndexPerScroll[rowIndex - 1].ToString()) + 1;
                else
                    targetRowIndex = rowIndex;

                string mRowHeaderXPath = ".//*[@class='wj-rowheaders']//div[@class='wj-row'][" + targetRowIndex.ToString() + "]//div[contains(@class,'wj-header')]";
                IWebElement rowHeader = mElement.FindElements(By.XPath(mRowHeaderXPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(mRowHeaderXPath)) : null;

                string mRowHeaderExpandCollapseXPath = ".//span[contains(@class,'wj-glyph')]";
                if (rowHeader != null)
                {
                    if (rowHeader.FindElements(By.XPath(mRowHeaderExpandCollapseXPath)).Count == 0)
                    {
                        //If row header is not visible after click, try moving to element before clicking
                        try
                        {
                            rowHeader.Click();
                        }
                        catch
                        {
                            Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                            mAction.MoveToElement(rowHeader).Click().Perform();

                            //Few instances where context menu appears while clicking row header click escape
                            string contextMenu_XPath = "//ul[contains(@class,'wbcontextmenu')]";
                            if(DlkEnvironment.AutoDriver.FindElements(By.XPath(contextMenu_XPath)).Count > 0)
                            {
                                IWebElement contextMenu = DlkEnvironment.AutoDriver.FindElement(By.XPath(contextMenu_XPath));
                                contextMenu.SendKeys(Keys.Escape);
                            }
                        }
                    }
                    else
                        DlkLogger.LogInfo("Row header contains expand/collapse icon, selecting first cell instead...");
                }
                else
                    throw new Exception("Row header not found");
                
                IList<IWebElement> allCellsFromRow = targetRow.FindElements(By.XPath(mTableCellXPath))
                            .Where(x => x.Displayed).ToList();

                //Get first cell and check if there is still no selected cell
                IWebElement checkFirstCell = allCellsFromRow.FirstOrDefault();
                bool firstCellIsClicked = checkFirstCell.GetAttribute("class").Contains("wj-state-selected");
                bool secondCellIsClicked = false;

                //Send tab keys for every column to navigate to desired cell
                int currentSelected = targetRow.FindElements(By.XPath(mTableCellSelectedXPath)).Count;
                IWebElement currentCell;

                for (int t = 0; t < mColumnNames.Count; t++)
                {
                    if ((currentSelected == 0 || !firstCellIsClicked) && !secondCellIsClicked)
                    {
                        IList<IWebElement> targetCells = targetRow.FindElements(By.XPath(mTableCellXPath))
                            .Where(x => x.Displayed).ToList();

                        if(targetColumnNumber == 0 || checkFirstCell.FindElements(By.XPath("./*[contains(@class,'Button')] | ./*[contains(@class,'button')]")).Count == 0)
                        {
                            //First cell is good to be clicked
                            IWebElement firstCell = targetCells.FirstOrDefault();
                            try
                            {
                                firstCell.Click();
                            }
                            catch
                            {
                                rowHeader.Click();
                            }
                            firstCellIsClicked = true;
                        }
                        else
                        {
                            //First cell has button that can be clicked, clicking second cell instead
                            IWebElement secondCell = allCellsFromRow.Count > 1 ? allCellsFromRow.ElementAt(1) 
                                : throw new Exception("Row header and 1st cell has button and there is no second cell. Selection cannot proceed.");
                            secondCell.Click();
                            secondCellIsClicked = true;
                            t++;
                        }
                        currentSelected = targetRow.FindElements(By.XPath(mTableCellSelectedXPath)).Count;
                        Thread.Sleep(1000);
                    }

                    if (t == targetColumnNumber)
                        break;

                    currentCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    currentCell.SendKeys(Keys.Tab);
                    Thread.Sleep(1000);
                    DlkLogger.LogInfo("Pressing tab from current cell...");
                }

                targetCell = targetRow.FindElements(By.XPath(mTableCellSelectedXPath)).Count > 0 ?
                    targetRow.FindElement(By.XPath(mTableCellSelectedXPath)) : null;

                //Retry getting target cell by clicking first cell 
                if(targetCell == null)
                {
                    IList<IWebElement> targetCells = targetRow.FindElements(By.XPath(mTableCellXPath))
                    .Where(x => x.Displayed).ToList();
                    IWebElement firstCell = targetCells.FirstOrDefault();

                    if (!firstCell.GetAttribute("class").Contains("wj-state-selected"))
                    {
                        try
                        {
                            firstCell.Click();
                        }
                        catch
                        {
                            rowHeader.Click();
                        }
                    }

                    //Send tab keys for every column to navigate to desired cell
                    for (int t = 0; t < mColumnNames.Count; t++)
                    {
                        if (t == targetColumnNumber)
                            break;

                        currentCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                        currentCell.SendKeys(Keys.Tab);
                        Thread.Sleep(1000);
                        DlkLogger.LogInfo("Pressing tab from current cell...");
                    }

                    targetCell = targetRow.FindElements(By.XPath(mTableCellSelectedXPath)).Count > 0 ?
                        targetRow.FindElement(By.XPath(mTableCellSelectedXPath)) : targetCells.LastOrDefault();
                }
                return targetCell;
            }
            else
            {
                throw new Exception("Root element not found on current table");
            }
        }

        private void ScrollGetAllDDownItems(IWebElement listContainer, IList<IWebElement> listItems)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

            //Check if dropdown list items generates only when scrolled
            if (listItems.Count > 0 & listContainer.GetAttribute("class").Contains("AutoComplete"))
            {
                string lastItemValue = listItems.LastOrDefault().Text;
                jse.ExecuteScript("arguments[0].scrollTop = 50", listContainer);
                Thread.Sleep(500);

                //Re-initialize items per scroll
                listItems = listContainer.FindElements(By.XPath(mCellComboBoxItem2XPath))
                    .Where(x => x.Displayed).ToList();
                string lastItemNewValue = listItems.LastOrDefault().Text;

                if (lastItemValue == lastItemNewValue)
                {
                    jse.ExecuteScript("arguments[0].scrollTop = 0", listContainer);
                    return;
                }
            }

            DlkLogger.LogInfo("Dropdown cell type is auto complete. Scrolling to reach all items... ");
            jse.ExecuteScript("arguments[0].scrollTop = 0", listContainer);
            foreach (IWebElement item in listItems)
            {
                string itemContent = DlkString.ReplaceCarriageReturn(item.Text.ToLower().Trim(), "") != "" ?
                    DlkString.ReplaceCarriageReturn(item.Text.ToLower().Trim(), "") :
                    DlkString.ReplaceCarriageReturn(new DlkBaseControl("List Item", item).GetValue().ToLower().Trim(), "");

                mDdownContents.Add(itemContent);
            }

            int scrollHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollHeight", listContainer));
            int clientHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].clientHeight", listContainer));

            //Check if vertical scrollbar exists
            if (scrollHeight != clientHeight)
            {
                //Set default values of no.of scrolls and index per scroll to rows
                for (int l = 0; l <= listItems.Count - 1; l++)
                {
                    mDdownNoOfScrolls.Add(0);
                    mDdownIndexPerScroll.Add(l);
                }

                int maxHeight = scrollHeight - clientHeight;
                int topScrollValue = maxHeight < 100 ? maxHeight : 100;
                int scrollYPos = 0;

                jse.ExecuteScript("arguments[0].scrollTop = 0", listContainer);

                while (scrollYPos <= maxHeight)
                {
                    int checkLimit = scrollYPos + topScrollValue;
                    if (checkLimit > maxHeight)
                    {
                        jse.ExecuteScript("arguments[0].scrollTop = " + maxHeight.ToString(), listContainer);
                        DlkLogger.LogInfo("Dropdown scrolling limit has been reached");

                        if (topScrollValue == 100)
                            mScrollToDdown++;

                        //Re-initialize items per scroll
                        listItems = listContainer.FindElements(By.XPath(mCellComboBoxItem2XPath))
                            .Where(x => x.Displayed).ToList();

                        for (int l = 0; l < listItems.Count; l++)
                        {
                            string currentItem = DlkString.ReplaceCarriageReturn(listItems[l].Text.ToLower().Trim(), "") != "" ?
                                DlkString.ReplaceCarriageReturn(listItems[l].Text.ToLower().Trim(), "") :
                                DlkString.ReplaceCarriageReturn(new DlkBaseControl("List Item", listItems[l]).GetValue().ToLower().Trim(), ""); ;

                            if (mDdownContents.Contains(currentItem))
                            {
                                int index = mDdownContents.IndexOf(currentItem);
                                mDdownNoOfScrolls.RemoveAt(index);
                                mDdownIndexPerScroll.RemoveAt(index);

                                mDdownNoOfScrolls.Insert(index, mScrollToDdown);
                                mDdownIndexPerScroll.Insert(index, l);
                            }
                            else
                            {
                                mDdownContents.Add(currentItem);
                                mDdownNoOfScrolls.Add(mScrollToDdown);
                                mDdownIndexPerScroll.Add(l);
                            }
                        }
                        break;
                    }
                    else
                    {
                        jse.ExecuteScript("arguments[0].scrollTop += " + topScrollValue.ToString(), listContainer);
                        DlkLogger.LogInfo("Scrolling down to reach all items...");
                        mScrollToDdown++;

                        //Re-initialize items per scroll
                        listItems = listContainer.FindElements(By.XPath(mCellComboBoxItem2XPath))
                            .Where(x => x.Displayed).ToList();

                        for (int l = 0; l < listItems.Count; l++)
                        {
                            string currentItem = DlkString.ReplaceCarriageReturn(listItems[l].Text.ToLower().Trim(), "") != "" ?
                                DlkString.ReplaceCarriageReturn(listItems[l].Text.ToLower().Trim(), "") :
                                DlkString.ReplaceCarriageReturn(new DlkBaseControl("List Item", listItems[l]).GetValue().ToLower().Trim(), "");

                            if (mDdownContents.Contains(currentItem))
                            {
                                int index = mDdownContents.IndexOf(currentItem);

                                mDdownNoOfScrolls.RemoveAt(index);
                                mDdownIndexPerScroll.RemoveAt(index);

                                mDdownNoOfScrolls.Insert(index, mScrollToDdown);
                                mDdownIndexPerScroll.Insert(index, l);
                            }
                            else
                            {
                                mDdownContents.Add(currentItem);
                                mDdownNoOfScrolls.Add(mScrollToDdown);
                                mDdownIndexPerScroll.Add(l);
                            }
                        }
                    }

                    scrollYPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollTop", listContainer));
                    Thread.Sleep(1000);
                }

                //Reset scroll back to default
                mScrollToDdown = 0;
                jse.ExecuteScript("arguments[0].scrollTop = 0", listContainer);
            }
        }

        private IWebElement ScrollVerticalToDDownItem(IWebElement listContainer, IList<IWebElement> listItems, string targetItem, bool partialCheck = false)
        {
            IWebElement targetDDownItem = null;
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

            //Check if dropdown list items generates only when scrolled
            if (listItems.Count > 0 & listContainer.GetAttribute("class").Contains("AutoComplete"))
            {
                string lastItemValue = listItems.LastOrDefault().Text;
                jse.ExecuteScript("arguments[0].scrollTop = 50", listContainer);
                Thread.Sleep(500);

                //Re-initialize items per scroll
                listItems = listContainer.FindElements(By.XPath(mCellComboBoxItem2XPath))
                    .Where(x => x.Displayed).ToList();
                string lastItemNewValue = listItems.LastOrDefault().Text;

                if (lastItemValue == lastItemNewValue)
                {
                    jse.ExecuteScript("arguments[0].scrollTop = 0", listContainer);
                    return null;
                }
            }

            DlkLogger.LogInfo("Dropdown cell type is auto complete. Scrolling to reach all items... ");
            jse.ExecuteScript("arguments[0].scrollTop = 0", listContainer);

            int targetIndex = 0;
            if (!partialCheck)
            {
                targetIndex = mDdownContents.Contains(targetItem.ToLower()) ? mDdownContents.IndexOf(targetItem.ToLower()) : throw new Exception("Target item: [" + targetItem + "] not found in list.");
            }
            else
            {
                bool partialValFound = false;
                foreach (var content in mDdownContents)
                {
                    if (content.ToString().Contains(targetItem.ToLower()))
                    {
                        targetIndex = mDdownContents.IndexOf(content);
                        partialValFound = true;
                        break;
                    }
                }

                if (!partialValFound)
                    throw new Exception("Target partial item: [" + targetItem + "] not found in list.");
            }
            int noOfScrolls = Convert.ToInt32(mDdownNoOfScrolls[targetIndex]);
            int indexPerScroll = Convert.ToInt32(mDdownIndexPerScroll[targetIndex]);
            int scrollHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollHeight", listContainer));
            int clientHeight = Convert.ToInt32(jse.ExecuteScript("return arguments[0].clientHeight", listContainer));

            //Check if vertical scrollbar exists
            if (scrollHeight != clientHeight)
            {
                int maxHeight = scrollHeight - clientHeight;
                int topScrollValue = maxHeight < 100 ? maxHeight : 100;
                int scrollYPos = 0;

                jse.ExecuteScript("arguments[0].scrollTop = 0", listContainer);

                for (int s = 1; s <= noOfScrolls; s++)
                {
                    int checkLimit = scrollYPos + topScrollValue;
                    if (checkLimit > maxHeight)
                    {
                        jse.ExecuteScript("arguments[0].scrollTop = " + maxHeight.ToString(), listContainer);
                        DlkLogger.LogInfo("Dropdown scrolling limit has been reached");
                    }
                    else
                    {
                        jse.ExecuteScript("arguments[0].scrollTop += " + topScrollValue.ToString(), listContainer);
                        DlkLogger.LogInfo("Scrolling down to reach all items...");
                    }

                    scrollYPos = Convert.ToInt32(jse.ExecuteScript("return arguments[0].scrollTop", listContainer));
                    Thread.Sleep(1000);
                }

                listItems = listContainer.FindElements(By.XPath(mCellComboBoxItem2XPath))
                            .Where(x => x.Displayed).ToList();

                targetDDownItem = listItems.ElementAt(indexPerScroll);
            }
            return targetDDownItem;
        }

        private void SetValueToCell(string LineIndex, string ColNameOrIndex, string Value, bool PartialColumn = false)
        {
            IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex, PartialColumn);

            if (targetCell.FindElements(By.XPath(".//input[@type='checkbox']")).Count > 0)
            {
                mControl = mCellCheckBox;
            }
            else
            {
                //Click cell to set focus to target cell
                int targetColumnNumber = 0;
                if (!int.TryParse(ColNameOrIndex, out targetColumnNumber))
                {
                    if (!PartialColumn)
                        targetColumnNumber = GetColumnIndexFromName(ColNameOrIndex);
                    else
                        targetColumnNumber = GetColumnIndexFromPartialName(ColNameOrIndex);
                }
                    

                //Avoid click cell for other cell since it already has current focus from pressing tab
                if (targetColumnNumber == 0 || !mUsedTabToCell)
                {
                    targetCell.Click();
                    Thread.Sleep(1000);
                    targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                }

                //Retrieve edit mode cell
                targetCell.Click();
                Thread.Sleep(1000);

                //Check if cell is Datepicker with DateColumn type
                if (targetCell.GetAttribute("class").Contains("DateColumn"))
                {
                    mControl = mCellDatePicker;
                }
                else
                {
                    targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                }

                targetCell = !targetCell.GetAttribute("class").Contains("wj-cell") ?
                    targetCell.FindElement(By.XPath(".//parent::div[@role='gridcell']")) : targetCell;
            }

            if (targetCell.FindElements(By.XPath(".//input[@type='text']")).Count > 0 ||
                targetCell.FindElements(By.XPath(".//input[@type='tel']")).Count > 0)
            {
                if (targetCell.GetAttribute("class").Contains("ColumnDropDownCell"))
                {
                    mControl = mCellComboBox;
                }
                else
                {
                    mControl = mCellTextBox;
                }
            }

            switch (mControl)
            {
                case mCellCheckBox:
                    DlkCheckBox checkBox = new DlkCheckBox("CheckBox Cell", targetCell);
                    checkBox.Set(Value);
                    break;
                case mCellTextBox:
                    IWebElement textBoxInput = targetCell.FindElement(By.TagName("input"));
                    DlkTextBox textBox = new DlkTextBox("TextBox Cell", textBoxInput);
                    if (!textBox.Exists(1))
                    {
                        IWebElement textBoxArea = targetCell.FindElements(By.XPath(".//textarea")).Count > 0 ?
                            targetCell.FindElement(By.XPath(".//textarea")) : throw new Exception("Text field not found");
                        textBoxArea.SendKeys(Value);
                        textBoxArea.SendKeys(Keys.Enter);
                    }
                    else
                    {
                        textBox.Set(Value);
                        textBox.mElement.SendKeys(Keys.Enter);
                    }
                    break;
                case mCellComboBox:
                    //Retrieve input field
                    IWebElement comboBoxInput = targetCell.FindElements(By.TagName("input")).Count > 0 ?
                        targetCell.FindElement(By.TagName("input")) : null;
                    if (comboBoxInput == null)
                        throw new Exception("Input field from target cell - combo box not found");

                    ClearField(comboBoxInput);
                    comboBoxInput.SendKeys(Value);

                    //Retrieve combo box list
                    IWebElement list = DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList1XPath)).Count > 0 ?
                        DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList1XPath)) :
                        DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList2XPath)).Count > 0 ?
                        DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList2XPath)) : null;

                    if (list == null)
                        throw new Exception("List from target cell - combo box not found");

                    //Retrieve combo box list items
                    bool iFound = false;
                    IList<IWebElement> items = list.FindElements(By.XPath(mCellComboBoxItem1XPath))
                        .Where(x => x.Displayed).ToList();

                    if (items.Count == 0)
                    {
                        items = list.FindElements(By.XPath(mCellComboBoxItem2XPath))
                        .Where(x => x.Displayed).ToList();
                    }

                    //Select item
                    foreach (IWebElement item in items)
                    {
                        string currentItem = new DlkBaseControl("Item", item).GetValue().Trim().ToLower();
                        if (currentItem == Value.ToLower())
                        {
                            item.Click();
                            DlkLogger.LogInfo("Selecting cell combo box item [" + Value + "] ...");
                            iFound = true;
                            break;
                        }
                    }

                    if (!iFound)
                        throw new Exception("Item not found from cell - combo box list");

                    break;
                case mCellDatePicker:
                    IWebElement datePickerInput = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                    DlkTextBox datePickBox = new DlkTextBox("DatePicker Iput", datePickerInput);
                    datePickBox.Set(Value);
                    datePickBox.mElement.SendKeys(Keys.Escape);
                    break;
                default:
                    mElement.SendKeys(Value);
                    break;
            }
        }
        
        private void SelectItemDDownValue(string lineIndex, string colNameOrIndex, string value)
        {
            IWebElement targetCell = GetCell(lineIndex, colNameOrIndex);

            //Click cell to set focus to target cell
            int targetColumnNumber = 0;
            if (!int.TryParse(colNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(colNameOrIndex);

            //Avoid click cell for other cell since it already has current focus from pressing tab
            if (targetColumnNumber == 0 || !mUsedTabToCell)
            {
                targetCell.Click();
                Thread.Sleep(1000);
                targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
            }

            //Retrieve edit mode cell
            targetCell.Click();
            Thread.Sleep(1000);

            targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();

            targetCell = !targetCell.GetAttribute("class").Contains("wj-cell") ?
                targetCell.FindElement(By.XPath(".//parent::div[@role='gridcell']")) : targetCell;

            if (!targetCell.GetAttribute("class").Contains("ColumnDropDownCell"))
            {
                throw new Exception("Cell is not a dropdown type. This is not yet supported.");
            }
            else
            {
                //Retrieve combo box list
                IWebElement list = DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList1XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList1XPath)) :
                    DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList2XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList2XPath)) : null;

                if (list == null)
                    throw new Exception("List from target cell - combo box not found");

                //Retrieve combo box list items
                bool iFound = false;
                IList<IWebElement> items = list.FindElements(By.XPath(mCellComboBoxItem1XPath))
                    .Where(x => x.Displayed).ToList();

                if (items.Count == 0)
                {
                    items = list.FindElements(By.XPath(mCellComboBoxItem2XPath))
                    .Where(x => x.Displayed).ToList();
                }

                //Dropdown list update where items are only generated when scrolled
                ScrollGetAllDDownItems(list, items);

                //Scroll to dropdown item in the list or select item directly if target item is null
                IWebElement targetItem = ScrollVerticalToDDownItem(list, items, value);
                
                if(targetItem == null)
                {
                    foreach (IWebElement item in items)
                    {
                        string currentItem = new DlkBaseControl("Item", item).GetValue().Trim().ToLower();
                        if (currentItem == value.ToLower())
                        {
                            item.Click();
                            DlkLogger.LogInfo("Selecting cell combo box item [" + value + "] ...");
                            iFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    targetItem.Click();
                    DlkLogger.LogInfo("Selecting cell combo box item [" + value + "] ...");
                    iFound = true;
                }

                if (!iFound)
                    throw new Exception("Item not found from cell - combo box list");
            }
        }

        private void SelectItemDDownValueContains(string lineIndex, string colNameOrIndex, string partialValue)
        {
            IWebElement targetCell = GetCell(lineIndex, colNameOrIndex);

            //Click cell to set focus to target cell
            int targetColumnNumber = 0;
            if (!int.TryParse(colNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(colNameOrIndex);

            //Avoid click cell for other cell since it already has current focus from pressing tab
            if (targetColumnNumber == 0 || !mUsedTabToCell)
            {
                targetCell.Click();
                Thread.Sleep(1000);
                targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
            }

            //Retrieve edit mode cell
            targetCell.Click();
            Thread.Sleep(1000);

            targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();

            targetCell = !targetCell.GetAttribute("class").Contains("wj-cell") ?
                targetCell.FindElement(By.XPath(".//parent::div[@role='gridcell']")) : targetCell;

            if (!targetCell.GetAttribute("class").Contains("ColumnDropDownCell"))
            {
                throw new Exception("Cell is not a dropdown type. This is not yet supported.");
            }
            else
            {
                //Retrieve combo box list
                IWebElement list = DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList1XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList1XPath)) :
                    DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList2XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList2XPath)) : null;

                if (list == null)
                    throw new Exception("List from target cell - combo box not found");

                //Retrieve combo box list items
                bool iFound = false;
                IList<IWebElement> items = list.FindElements(By.XPath(mCellComboBoxItem1XPath))
                    .Where(x => x.Displayed).ToList();

                if (items.Count == 0)
                {
                    items = list.FindElements(By.XPath(mCellComboBoxItem2XPath))
                    .Where(x => x.Displayed).ToList();
                }

                //Dropdown list update where items are only generated when scrolled
                ScrollGetAllDDownItems(list, items);

                //Scroll to dropdown item in the list or select item directly if target item is null
                IWebElement targetItem = ScrollVerticalToDDownItem(list, items, partialValue, true);

                if(targetItem == null)
                {
                    foreach (IWebElement item in items)
                    {
                        string currentItem = new DlkBaseControl("Item", item).GetValue().Trim().ToLower();
                        if (currentItem.Contains(partialValue.ToLower()))
                        {
                            item.Click();
                            DlkLogger.LogInfo("Selecting cell combo box item [" + partialValue + "] ...");
                            iFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    targetItem.Click();
                    DlkLogger.LogInfo("Selecting cell combo box item [" + partialValue + "] ...");
                    iFound = true;
                }
                

                if (!iFound)
                    throw new Exception("Item not found from cell - combo box list");
            }
        }

        private void SelectItemDDownByIndex(string lineIndex, string colNameOrIndex, int targetIndex, string itemIndex)
        {
            IWebElement targetCell = GetCell(lineIndex, colNameOrIndex);

            //Click cell to set focus to target cell
            int targetColumnNumber = 0;
            if (!int.TryParse(colNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(colNameOrIndex);

            //Avoid click cell for other cell since it already has current focus from pressing tab
            if (targetColumnNumber == 0 || !mUsedTabToCell)
            {
                targetCell.Click();
                Thread.Sleep(1000);
                targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
            }

            //Retrieve edit mode cell
            targetCell.Click();
            Thread.Sleep(1000);

            targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();

            targetCell = !targetCell.GetAttribute("class").Contains("wj-cell") ?
                targetCell.FindElement(By.XPath(".//parent::div[@role='gridcell']")) : targetCell;

            if (!targetCell.GetAttribute("class").Contains("ColumnDropDownCell"))
            {
                throw new Exception("Cell is not a dropdown type. This is not yet supported.");
            }
            else
            {
                //Retrieve combo box list
                IWebElement list = DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList1XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList1XPath)) :
                    DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList2XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList2XPath)) : null;

                if (list == null)
                    throw new Exception("List from target cell - combo box not found");

                //Retrieve combo box list items
                bool iFound = false;
                IList<IWebElement> items = list.FindElements(By.XPath(mCellComboBoxItem1XPath))
                    .Where(x => x.Displayed).ToList();

                if (items.Count == 0)
                {
                    items = list.FindElements(By.XPath(mCellComboBoxItem2XPath))
                    .Where(x => x.Displayed).ToList();
                }

                //Select item
                int currentIndex = 1;
                foreach (IWebElement item in items)
                {
                    if (currentIndex == targetIndex)
                    {
                        item.Click();
                        DlkLogger.LogInfo("Selecting cell combo box item index [" + itemIndex + "] ...");
                        iFound = true;
                        break;
                    }
                    else
                    {
                        currentIndex++;
                    }
                }

                if (!iFound)
                    throw new Exception("Item with index [" + itemIndex + "] not found from cell - combo box list");
            }
        }

        private void SetSelectItemDDownByIndex(string lineIndex, string colNameOrIndex, int targetIndex, string itemIndex, string setValue)
        {
            IWebElement targetCell = GetCell(lineIndex, colNameOrIndex);
            new DlkBaseControl("TargetCell", targetCell).ScrollIntoViewUsingJavaScript();
            //Click cell to set focus to target cell
            int targetColumnNumber = 0;
            if (!int.TryParse(colNameOrIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromName(colNameOrIndex);
            
            //Avoid click cell for other cell since it already has current focus from pressing tab
            if (targetColumnNumber == 0 || !mUsedTabToCell)
            {
                targetCell.Click();
                Thread.Sleep(1000);
            }
            
            //Retrieve edit mode cell
            targetCell.Click();
            Thread.Sleep(1000);
            
            if (!targetCell.GetAttribute("class").Contains("ColumnDropDownCell"))
            {
                throw new Exception("Cell is not a dropdown type. This is not yet supported.");
            }
            else
            {
                targetCell = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();

                if (!targetCell.TagName.Equals("input"))
                {
                    targetCell = !targetCell.GetAttribute("class").Contains("wj-cell") ?
                    targetCell.FindElement(By.XPath(".//parent::div[@role='gridcell']")) : targetCell;
                }
                
                IWebElement dropDownInput = !targetCell.TagName.Equals("input") ? targetCell.FindElement(By.TagName("input")) :
                    targetCell;

                DlkTextBox DropDownBox = new DlkTextBox("Dropdown TextBox", dropDownInput);
                DropDownBox.Set(setValue);
                Thread.Sleep(1000);
                
                //Retrieve combo box list
                IWebElement list = DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList1XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList1XPath)) :
                    DlkEnvironment.AutoDriver.FindElements(By.XPath(mCellComboBoxList2XPath)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(mCellComboBoxList2XPath)) : null;

                if (list == null)
                    throw new Exception("List from target cell - combo box not found");
                
                //Retrieve combo box list items
                bool iFound = false;
                IList<IWebElement> items = list.FindElements(By.XPath(mCellComboBoxItem1XPath))
                    .Where(x => x.Displayed).ToList();
                
                if (items.Count == 0)
                {
                    items = list.FindElements(By.XPath(mCellComboBoxItem2XPath))
                    .Where(x => x.Displayed).ToList();
                }
                
                //Select item
                int currentIndex = 1;
                foreach (IWebElement item in items)
                {
                    if (currentIndex == targetIndex)
                    {
                        item.Click();
                        Thread.Sleep(3000);
                        DlkLogger.LogInfo("Selecting cell combo box item index [" + itemIndex + "] ...");
                        iFound = true;
                        break;
                    }
                    else
                    {
                        currentIndex++;
                    }
                }

                if (!iFound)
                    throw new Exception("Item with index [" + itemIndex + "] not found from cell - combo box list");
            }
        }

        private void SelectRow(string Row, int row)
        {
            IWebElement targetRow = GetTargetRow(Row);
            if (targetRow == null)
                throw new Exception("Row not found.");

            int targetRowIndex = 0;
            if (mRowNoOfScrolls.Count > 0)
                targetRowIndex = Convert.ToInt32(mRowIndexPerScroll[row - 1].ToString()) + 1;
            else
                targetRowIndex = row;

            string mRowHeaderXPath = ".//*[@class='wj-rowheaders']//div[@class='wj-row'][" + targetRowIndex.ToString() + "]//div[contains(@class,'wj-header')]";
            IWebElement rowHeader = mElement.FindElements(By.XPath(mRowHeaderXPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mRowHeaderXPath)) : null;

            if (rowHeader != null)
            {
                rowHeader.Click();
                targetRow = GetTargetRow(Row);
                DlkLogger.LogInfo("Selecting target row's Row header...");
            }
            else
                throw new Exception("Row header not found");

            IList<IWebElement> targetCells = targetRow.FindElements(By.XPath(mTableCellXPath))
                .Where(x => x.Displayed).ToList();
            IWebElement firstCell = targetCells.FirstOrDefault();

            if (!firstCell.GetAttribute("class").Contains("wj-state-selected"))
                firstCell.Click();
        }
        
        private void SelectMultipleRows(List <IWebElement> targetRowHeaders, string []rowStrings)
        {
            int rowCount = targetRowHeaders.Count;
            Actions actions = new Actions(DlkEnvironment.AutoDriver);
            DlkLogger.LogInfo("Selecting rows... ");
            switch (rowCount)
            {
                case 1:
                    targetRowHeaders.FirstOrDefault().Click();
                    break;
                case 2:
                    actions.Click(targetRowHeaders[0]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[1]).KeyUp(Keys.Control)
                        .Build().Perform();
                    break;
                case 3:
                    actions.Click(targetRowHeaders[0]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[1]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[2]).KeyUp(Keys.Control)
                        .Build().Perform();
                    break;
                case 4:
                    actions.Click(targetRowHeaders[0]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[1]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[2]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[3]).KeyUp(Keys.Control)
                        .Build().Perform();
                    break;
                case 5:
                    actions.Click(targetRowHeaders[0]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[1]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[2]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[3]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[4]).KeyUp(Keys.Control)
                        .Build().Perform();
                    break;
                case 6:
                    actions.Click(targetRowHeaders[0]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[1]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[2]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[3]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[4]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[5]).KeyUp(Keys.Control)
                        .Build().Perform();
                    break;
                case 7:
                    actions.Click(targetRowHeaders[0]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[1]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[2]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[3]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[4]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[5]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[6]).KeyUp(Keys.Control)
                        .Build().Perform();
                    break;
                case 8:
                    actions.Click(targetRowHeaders[0]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[1]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[2]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[3]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[4]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[5]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[6]).KeyDown(Keys.Control)
                        .Click(targetRowHeaders[7]).KeyUp(Keys.Control)
                        .Build().Perform();
                    break;
                default:
                    throw new Exception("Invalid row count selection or selecting rows reached maximum limit. ");
            }

            foreach (string row in rowStrings)
            {
                DlkLogger.LogInfo("Selecting row [" + row + "]... ");
            }
        }

        private void SelectShiftMultipleRows(string startingRow, int firstRow, int lastRow)
        {
            SelectRow(startingRow, firstRow);
            IWebElement currentRow = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
            Actions actions = new Actions(DlkEnvironment.AutoDriver);

            DlkLogger.LogInfo("Selecting other rows using shift... ");
            if (firstRow < lastRow)
            {
                for (int r = 1; r < lastRow; r++)
                {
                    currentRow.SendKeys(Keys.Shift + Keys.ArrowDown);
                    DlkLogger.LogInfo("Pressing shift then down from current row... [" + r + "]");
                }
            }
            else if (firstRow > lastRow)
            {
                for (int r = firstRow; r > lastRow; r--)
                {
                    currentRow.SendKeys(Keys.Shift + Keys.ArrowUp);
                    DlkLogger.LogInfo("Pressing shift then up from current row... [" + r + "]");
                }
            }
        }
        #endregion

        #region KEYWORDS
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

        [Keyword("VerifyCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValue(String LineIndex, String ColNameOrIndex, String ExpectedValue)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(LineIndex, ColNameOrIndex);
                DlkAssert.AssertEqual("VerifyCellValue(): ", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValueWithColumnContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValueWithColumnContains(String LineIndex, String PartialColNameOrIndex, String ExpectedValue)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(LineIndex, PartialColNameOrIndex, true);
                DlkAssert.AssertEqual("VerifyCellValueWithColumnContains(): ", ExpectedValue, ActualValue);
                DlkLogger.LogInfo("VerifyCellValueWithColumnContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValueWithColumnContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellPartialValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellPartialValue(String LineIndex, String ColNameOrIndex, String ExpectedValue)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(LineIndex, ColNameOrIndex);
                DlkAssert.AssertEqual("VerifyCellPartialValue(): ", ExpectedValue, ActualValue, true);
                DlkLogger.LogInfo("VerifyCellPartialValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellPartialValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellReadOnly(String LineIndex, String ColNameOrIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");
                bool Expected;
                if(!Boolean.TryParse(TrueOrFalse, out Expected))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter TrueOrFalse.");

                string ActualValue = GetCellReadOnly(LineIndex, ColNameOrIndex);
                DlkAssert.AssertEqual("VerifyCellReadOnly(): ", TrueOrFalse.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyCellReadOnly() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCell(String LineIndex, String ColNameOrIndex)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkLogger.LogInfo("Clicking target cell...");
                targetCell.Click();
                DlkLogger.LogInfo("ClickCell() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValue(String LineIndex, String ColNameOrIndex, String Value)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                Initialize();
                SetValueToCell(LineIndex, ColNameOrIndex, Value);
                DlkLogger.LogInfo("SetCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCellValueWithColumnContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValueWithColumnContains(String LineIndex, String PartialColNameOrIndex, String Value)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                Initialize();
                SetValueToCell(LineIndex, PartialColNameOrIndex, Value, true);
                DlkLogger.LogInfo("SetCellValueWithColumnContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValueWithColumnContains() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemDropdownValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItemDropdownValue(String LineIndex, String ColNameOrIndex, String Value)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                Initialize();
                SelectItemDDownValue(LineIndex, ColNameOrIndex, Value);
                DlkLogger.LogInfo("SelectItemDropdownValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemDropdownValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemDropdownValueContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItemDropdownValueContains(String LineIndex, String ColNameOrIndex, String PartialValue)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                Initialize();
                SelectItemDDownValueContains(LineIndex, ColNameOrIndex, PartialValue);
                DlkLogger.LogInfo("SelectItemDropdownValueContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemDropdownValueContains() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemDropdownIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItemDropdownIndex(String LineIndex, String ColNameOrIndex, String ItemIndex)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                int targetIndex = 0;
                if (!int.TryParse(ItemIndex, out targetIndex) || targetIndex == 0)
                    throw new Exception("[" + ItemIndex + "] is not a valid input for parameter ItemIndex.");

                Initialize();
                SelectItemDDownByIndex(LineIndex, ColNameOrIndex, targetIndex, ItemIndex);
                DlkLogger.LogInfo("SelectItemDropdownIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemDropdownIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SetSelectItemDropdownIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetSelectItemDropdownIndex(String LineIndex, String ColNameOrIndex, String ValueToSet, String ItemIndex)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                int targetIndex = 0;
                if (!int.TryParse(ItemIndex, out targetIndex) || targetIndex == 0)
                    throw new Exception("[" + ItemIndex + "] is not a valid input for parameter ItemIndex.");

                Initialize();
                SetSelectItemDDownByIndex(LineIndex, ColNameOrIndex, targetIndex, ItemIndex, ValueToSet);
                DlkLogger.LogInfo("SetSelectItemDropdownIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetSelectItemDropdownIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetCellValue(String LineIndex, String ColNameOrIndex, String VariableName)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(LineIndex, ColNameOrIndex);
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellValueWithColumnContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetCellValueWithColumnContains(String LineIndex, String PartialColNameOrIndex, String VariableName)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(LineIndex, PartialColNameOrIndex, true);
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellValueWithColumnContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValueWithColumnContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRowCount(String RowCount)
        {
            try
            {
                Initialize(false);

                int row = 0;
                if (!int.TryParse(RowCount, out row))
                    throw new Exception("[" + RowCount + "] is not a valid input for parameter LineIndex.");

                if (mRowContents.Count == 0)
                {
                    Thread.Sleep(3000);
                    GetRows();
                }

                string ActualValue = mRowContents.Count.ToString();
                DlkAssert.AssertEqual("VerifyRowCount(): ", RowCount, ActualValue);
                DlkLogger.LogInfo("VerifyRowCount() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowCount(String VariableName)
        {
            try
            {
                Initialize(false);

                if (mRowContents.Count == 0)
                {
                    Thread.Sleep(3000);
                    GetRows();
                }
                string RowCount = mRowContents.Count.ToString();
                DlkVariable.SetVariable(VariableName, RowCount);
                DlkLogger.LogInfo("[" + RowCount + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetRowCount() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithColumnValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowWithColumnValue(String ColNameOrIndex, String Value, String VariableName)
        {
            try
            {
                Initialize();
                bool rFound = false;

                for(int r = 1; r <= mRowContents.Count; r++)
                {
                    string ActualValue = GetCellValue(r.ToString(), ColNameOrIndex);
                    if (ActualValue.ToLower() == Value.ToLower())
                    {
                        DlkVariable.SetVariable(VariableName, r.ToString());
                        DlkLogger.LogInfo("[" + r.ToString() + "] value set to Variable: [" + VariableName + "]");
                        rFound = true;
                        break;
                    }
                }

                if (!rFound)
                    throw new Exception("Row not found with value [" + Value + "]");

                DlkLogger.LogInfo("GetRowWithColumnValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithColumnPartialValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRowWithColumnPartialValue(String ColNameOrIndex, String Value, String VariableName)
        {
            try
            {
                Initialize();

                bool rFound = false;

                for (int r = 1; r <= mRowContents.Count; r++)
                {
                    string ActualValue = GetCellValue(r.ToString(), ColNameOrIndex);
                    if (ActualValue.ToLower().Contains(Value.ToLower()))
                    {
                        DlkVariable.SetVariable(VariableName, r.ToString());
                        DlkLogger.LogInfo("[" + r.ToString() + "] value set to Variable: [" + VariableName + "]");
                        rFound = true;
                        break;
                    }
                }

                if (!rFound)
                    throw new Exception("Row not found with value [" + Value + "]");

                DlkLogger.LogInfo("GetRowWithColumnPartialValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnPartialValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetCellReadOnly(String LineIndex, String ColNameOrIndex, String VariableName)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellReadOnly(LineIndex, ColNameOrIndex);
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellReadOnly() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellButton(String LineIndex, String ColNameOrIndex, String ButtonCaption)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                //Retrieve and click target cell
                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                targetCell.Click();

                //Retrieve target button
                String mButtonXPath = ".//*[contains(@class,'WijmoButtonColumnElement')][contains(@data-tooltip,'" + ButtonCaption + "')]";
                IWebElement targetButton = null;
                targetButton = targetCell.FindElements(By.XPath(mButtonXPath)).Count > 0 ?
                           targetCell.FindElement(By.XPath(mButtonXPath)) : null;

                if (targetButton == null)
                    throw new Exception("[" + ButtonCaption + "] button not found in the target cell.");
                else
                {
                    //Hover over target button and execute click
                    new DlkBaseControl("Target Button", targetButton).MouseOver();
                    targetButton.Click();
                    DlkLogger.LogInfo("[" + ButtonCaption + "] button has been clicked");
                    DlkLogger.LogInfo("ClickCellButton() passed.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContextMenuItem")]
        public void SelectContextMenuItem(String Item)
        {
            try
            {
                FindElement();
                //Perform righ click action on table
                Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                mAction.ContextClick(mElement).Perform();

                //Retrieve context menu element
                Thread.Sleep(2000);
                IWebElement contextMenu = DlkEnvironment.AutoDriver.FindElement(By.XPath(mContextMenuXPath));
                IList<IWebElement> contextMenuItems = contextMenu.FindElements(By.XPath(".//li[contains(@class,'item')]")).Where(x=>x.Displayed).ToList();

                //Select context menu item
                bool itemExist = false;

                if (!Item.Contains("~"))
                {
                    foreach (IWebElement item in contextMenuItems)
                    {
                        if (item.Text.ToLower().Contains(Item.ToLower()))
                        {
                            DlkBaseControl menuItem = new DlkBaseControl("Menu Item", item);
                            menuItem.MouseOver();
                            menuItem.ClickUsingJavaScript();
                            DlkLogger.LogInfo("[" + Item + "] context menu item has been clicked");
                            DlkLogger.LogInfo("SelectContextMenuItem() passed.");
                            itemExist = true;
                            break;
                        }
                    }
                }
                //else
                //{
                //    string[] Items = Item.Split('~');
                //    foreach (IWebElement item in contextMenuItems)
                //    {
                //        if (item.Text.ToLower().Contains(Items[0].ToLower()))
                //        {
                //            DlkBaseControl menuItem = new DlkBaseControl("Menu Item", item);
                //            menuItem.MouseOver();
                //            DlkLogger.LogInfo("[" + Items[0] + "] context menu item has been hovered");
                //            DlkLogger.LogInfo("SelectContextMenuItem() passed.");
                //        }
                //    }
                //}
                if (!itemExist)
                    throw new Exception("Context menu item [" + Item + "] not found.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContextMenuItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectRow", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectRow(String Row)
        {
            try
            {
                Initialize(false);

                int row = 0;
                if (!int.TryParse(Row, out row) || row == 0)
                    throw new Exception("[" + Row + "] is not a valid input for parameter LineIndex.");

                SelectRow(Row, row);
                DlkLogger.LogInfo("SelectRow() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectRow() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectMultipleRows", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectMultipleRows(String Rows)
        {
            try
            {
                Initialize(false);

                string[] rowSplit = Rows.Split('~');

                List<IWebElement> targetRowHeaders = new List<IWebElement>();
                foreach (string rowString in rowSplit)
                {
                    int row = 0;
                    if (!int.TryParse(rowString, out row) || row == 0)
                        throw new Exception("[" + rowString + "] is not a valid input for parameter Rows.");

                    string mRowHeaderXPath = ".//*[@class='wj-rowheaders']//div[@class='wj-row'][" + rowString.ToString() + "]//div[contains(@class,'wj-header')]";
                    IWebElement rowHeader = mElement.FindElements(By.XPath(mRowHeaderXPath)).Count > 0 ?
                        mElement.FindElement(By.XPath(mRowHeaderXPath)) : null;

                    if (rowHeader != null)
                        targetRowHeaders.Add(rowHeader);
                    else
                        throw new Exception("Row header not found");
                }

                SelectMultipleRows(targetRowHeaders, rowSplit);
                DlkLogger.LogInfo("SelectMultipleRows() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectMultipleRows() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectShiftMultipleRows", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectShiftMultipleRows(String Rows)
        {
            try
            {
                Initialize(false);

                string[] rowSplit = Rows.Split('~');
                string startRow = rowSplit.FirstOrDefault().ToString();
                string lastRow = rowSplit.LastOrDefault().ToString();

                int strRow = 0;
                if (!int.TryParse(startRow, out strRow) || strRow == 0)
                    throw new Exception("[" + startRow + "] is not a valid input for starting row index.");

                int lstRow = 0;
                if (!int.TryParse(lastRow, out lstRow) || lstRow == 0)
                    throw new Exception("[" + lastRow + "] is not a valid input for last row index.");

                if(strRow != lstRow)
                {
                    SelectShiftMultipleRows(startRow, strRow, lstRow);
                }
                else
                {
                    SelectRow(startRow, strRow);
                }

                DlkLogger.LogInfo("SelectShiftMultipleRows() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectShiftMultipleRows() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickRowHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickRowHeader(String Row)
        {
            try
            {
                Initialize(false);

                int row = 0;
                if (!int.TryParse(Row, out row) || row == 0)
                    throw new Exception("[" + Row + "] is not a valid input for parameter LineIndex.");

                IWebElement targetRow = GetTargetRow(Row);
                if (targetRow == null)
                    throw new Exception("Row not found.");

                int targetRowIndex = 0;
                if (mRowNoOfScrolls.Count > 0)
                    targetRowIndex = Convert.ToInt32(mRowIndexPerScroll[row - 1].ToString()) + 1;
                else
                    targetRowIndex = row;

                string mRowHeaderXPath = ".//*[@class='wj-rowheaders']//div[@class='wj-row'][" + targetRowIndex.ToString() + "]//div[contains(@class,'wj-header')]";
                IWebElement rowHeader = mElement.FindElements(By.XPath(mRowHeaderXPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(mRowHeaderXPath)) : null;

                if (rowHeader != null)
                {
                    rowHeader.Click();
                    DlkLogger.LogInfo("Clicking row header...");
                }
                else
                    throw new Exception("Row header not found");

                DlkLogger.LogInfo("ClickRowHeader() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickRowHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("SortColumn", new String[] { "1|text|Expected Value|TRUE" })]
        public void SortColumn(String ColNameOrIndex, String SortOrder)
        {
            try
            {
                Initialize();
                IWebElement targetColumn = GetTargetColumn(ColNameOrIndex);

                if (targetColumn == null)
                    throw new Exception("Target column not found");

                //Initial click to activate sort function of column header
                if (!targetColumn.GetAttribute("class").Contains("sort"))
                    new DlkBaseControl("Target Column", targetColumn).ClickUsingJavaScript();

                switch (SortOrder.ToLower())
                {
                    case "ascending":
                        if (targetColumn.GetAttribute("class").Contains("sort-asc"))
                            DlkLogger.LogInfo("Column already in ascending order. No change done.");
                        else
                        {
                            DlkLogger.LogInfo("Changing column order to ascending... ");
                            new DlkBaseControl("Target Column", targetColumn).ClickUsingJavaScript();
                            if (!targetColumn.GetAttribute("class").Contains("sort-asc"))
                                throw new Exception("Column order not changed.");
                        }
                        break;
                    case "descending":
                        if (targetColumn.GetAttribute("class").Contains("sort-desc"))
                            DlkLogger.LogInfo("Column already in descending order. No change done.");
                        else
                        {
                            DlkLogger.LogInfo("Changing column order to descending... ");
                            new DlkBaseControl("Target Column", targetColumn).ClickUsingJavaScript();
                            if (!targetColumn.GetAttribute("class").Contains("sort-desc"))
                                throw new Exception("Column order not changed.");
                        }
                        break;
                    default:
                        throw new Exception("Sort order must be ascending or descending only.");
                }
                DlkLogger.LogInfo("SortColumn() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SortColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnHeaderExist", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnHeaderExist(String ColumnHeader, String TrueOrFalse)
        {
            try
            {
                bool headerExist = false;
                string actualHeaderList = "";

                DlkWorkBookFunctionHandler.WaitScreenGetsReady();
                FindElement();
                ScrollReset();
                GetColumnHeaders();

                foreach (string columnText in mColumnNames)
                {
                    actualHeaderList += "~" + columnText;
                    if (columnText.ToLower().Equals(ColumnHeader.ToLower()))
                    {
                        DlkLogger.LogInfo("Column header [" + ColumnHeader + "] found in table headers.");
                        headerExist = true;
                        break;
                    }
                }

                if (!headerExist)
                    throw new Exception("Column header[" + ColumnHeader + "] not found in table headers. Actual table header: [" + actualHeaderList.Trim('~') + "]");

                DlkLogger.LogInfo("VerifyColumnHeaderExist() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderExist() failed : " + e.Message, e);
            }
        }

        [Keyword("GetColumnHeaderExist", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetColumnHeaderExist(String ColumnHeader, String VariableName)
        {
            try
            {
                bool headerExist = false;
                string actualHeaderList = "";

                DlkWorkBookFunctionHandler.WaitScreenGetsReady();
                FindElement();
                ScrollReset();
                GetColumnHeaders();

                foreach (string columnText in mColumnNames)
                {
                    actualHeaderList += "~" + columnText;
                    if (columnText.ToLower().Equals(ColumnHeader.ToLower()))
                    {
                        DlkLogger.LogInfo("Column header [" + ColumnHeader + "] found in table headers.");
                        headerExist = true;
                        break;
                    }
                }

                DlkVariable.SetVariable(VariableName, headerExist.ToString());
                DlkLogger.LogInfo("[" + headerExist.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetColumnHeaderExist() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetColumnHeaderExist() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnByIndex(String ColumnIndex, String ExpectedValue)
        {
            try
            {
                int targetColumn = 0;
                if (!int.TryParse(ColumnIndex, out targetColumn) || targetColumn == 0)
                    throw new Exception("[" + ColumnIndex + "] is not a valid input for parameter ColumnIndex.");

                DlkWorkBookFunctionHandler.WaitScreenGetsReady();
                FindElement();
                ScrollReset();
                GetColumnHeaders();

                string actualValue = mColumnNames[targetColumn - 1].ToString();
                DlkLogger.LogInfo("Column header value: [" + actualValue + "] retrieved from index [" + targetColumn + "]");

                DlkAssert.AssertEqual("VerifyColumnByIndex():", ExpectedValue.ToLower(), actualValue.ToLower());
                DlkLogger.LogInfo("VerifyColumnByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetColumnByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetColumnByIndex(String ColumnIndex, String VariableName)
        {
            try
            {
                int targetColumn = 0;
                if (!int.TryParse(ColumnIndex, out targetColumn) || targetColumn == 0)
                    throw new Exception("[" + ColumnIndex + "] is not a valid input for parameter ColumnIndex.");

                DlkWorkBookFunctionHandler.WaitScreenGetsReady();
                FindElement();
                ScrollReset();
                GetColumnHeaders();

                string actualValue = mColumnNames[targetColumn - 1].ToString();
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] value set to Variable: [" + VariableName + "]");

                DlkLogger.LogInfo("GetColumnByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetColumnByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SetHeaderCheckBoxValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetHeaderCheckBoxValue(String ColNameOrIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement targetColumn = GetTargetColumn(ColNameOrIndex);

                if (targetColumn == null)
                    throw new Exception("Target column not found");

                if (targetColumn.FindElements(By.XPath(".//input[@type='checkbox']")).Count > 0)
                {
                    DlkCheckBox checkBox = new DlkCheckBox("CheckBox Cell", targetColumn);
                    checkBox.Set(TrueOrFalse);
                }
                else
                    throw new Exception("This keyword only supports checkbox type headers.");

                DlkLogger.LogInfo("SetHeaderCheckBoxValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetHeaderCheckBoxValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowExistWithColumnValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRowExistWithColumnValue(String ColNameOrIndex, String Value, String ExpectedValue)
        {
            try
            {
                bool Expected;
                if (!Boolean.TryParse(ExpectedValue, out Expected))
                    throw new Exception("[" + ExpectedValue + "] is not a valid input for parameter ExpectedValue.");

                Initialize();
                bool rFound = false;

                for (int r = 1; r <= mRowContents.Count; r++)
                {
                    string ActualValue = GetCellValue(r.ToString(), ColNameOrIndex);
                    if (ActualValue.ToLower() == Value.ToLower())
                    {
                        rFound = true;
                        DlkLogger.LogInfo("[" + Value + "] value found at Row: [" + r.ToString() + "]");
                        break;
                    }
                }

                if (!rFound)
                    DlkLogger.LogInfo("[" + Value + "] not found in table. Row does not exist.");

                DlkAssert.AssertEqual("VerifyRowExistWithColumnValue", Expected, rFound);
                DlkLogger.LogInfo("VerifyRowExistWithColumnValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowExistWithColumnValue() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
