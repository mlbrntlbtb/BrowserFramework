using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;

namespace ngCRMLib.DlkControls
{
    /// <summary>
    /// Navigator class for tables
    /// </summary>
    [ControlType("Table")]
    public class DlkTable : DlkBaseTable
    {
        #region FIELDS
        private Table TableData;
        #endregion

        #region CONSTRUCTOR
        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue)
        {
        }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }
        #endregion

        #region PUBLIC METHODS
        public new void Initialize()
        {
            FindElement();
            CreateTable();
        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (this.mElement.GetAttribute("class").ToLower().Contains("navigator_grid"))
            {
                return true;
            }
            else
            {
                try
                {
                    IWebElement parentElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'navigator_grid')]"));
                    return true;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }

            }
        }

        public new void AutoCorrectSearchMethod(ref string SearchType, ref string SearchValue)
        {
            try
            {
                DlkBaseControl mCorrectControl = new DlkBaseControl("Table", "", "");
                bool mAutoCorrect = false;

                VerifyControlType();
                IWebElement parentTable = mElement.FindElement(By.XPath("./ancestor::div[contains(@class, 'navigator_grid')]"));
                mCorrectControl = new DlkBaseControl("CorrectControl", parentTable);
                mAutoCorrect = true;

                if (mAutoCorrect)
                {
                    String mId = mCorrectControl.GetAttributeValue("id");
                    String mName = mCorrectControl.GetAttributeValue("name");
                    String mClassName = mCorrectControl.GetAttributeValue("class");
                    if (mId != null && mId != "")
                    {
                        SearchType = "ID";
                        SearchValue = mId;
                    }
                    else if (mName != null && mName != "")
                    {
                        SearchType = "NAME";
                        SearchValue = mName;
                    }
                    else if (mClassName != null && mClassName != "")
                    {
                        SearchType = "CLASSNAME";
                        SearchValue = mClassName.Split(' ').First();
                    }
                    else
                    {
                        SearchType = "XPATH";
                        SearchValue = mCorrectControl.FindXPath();
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region PRIVATE METHODS
        private void CreateTable()
        {
            string sClass = GetAttributeValue("class").ToLower();
            if (sClass.Contains("navigator_grid"))
            {
                sClass = "navigator_grid";
            }

            TableData = new Table(sClass, mElement);
        }
        #endregion

        public class Table
        {
            #region FIELDS
            public String mClass;
            public Dictionary<string, int> Columns;
            private IWebElement mTableElement;
            internal IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            #endregion

            #region CONSTRUCTOR
            public Table(String sClass, IWebElement TableElement)
            {
                mClass = sClass;
                mTableElement = TableElement;
            }
            #endregion

            #region PRIVATE METHODS

            private int GetColumnIndexFromHeader(string sColumnHeader)
            {
                if (Columns == null)
                {
                    Columns = new Dictionary<string, int>();
                    IList<IWebElement> mHeaders = mTableElement.FindElements(By.CssSelector("span.columnHdr"));

                    //some tables don't have span.columnHdr, use the generic th instead
                    if (mHeaders.Count == 0)
                    {
                        mHeaders = mTableElement.FindElements(By.CssSelector("th"));
                    }

                    for (int i = 0; i < mHeaders.Count; i++)
                    {
                        DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                        string sHeader = ctlHeader.GetAttributeValue("title").ToLower();
                        if (sHeader == "")
                        {
                            sHeader = ctlHeader.GetValue().ToLower().Trim();
                            if (sHeader.Contains("\r\n"))
                            {
                                sHeader = sHeader.Replace("\r\n", " ");
                            }

                        }
                        if (!Columns.ContainsKey(sHeader))
                            Columns.Add(sHeader, i);
                    }

                }

                string mColumnText = sColumnHeader.ToLower().Trim();
                return Columns[mColumnText];


                //if (Columns.ContainsKey(mColumnText))
                //{
                //    return Columns[mColumnText];
                //}
                //else
                //{
                //    return -1;
                //    //throw new Exception("GetColumnIndexFromHeader() failed. Invalid column header '" + sColumnHeader + "'");
                //}

            }

            private int GetColumnIndexByCellName(string sColumnHeader)
            {
                Columns = new Dictionary<string, int>();
                IList<IWebElement> mHeaders = mTableElement.FindElements(By.TagName("td"));
                string sName = "";
                int ColumnHeaderLength = sColumnHeader.Length;

                for (int i = 0; i < mHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    if (!ctlHeader.GetAttributeValue("style").Contains("display: none;"))
                    {
                        string sHeader = ctlHeader.GetAttributeValue("name").ToLower();
                        int HeaderLength = sHeader.Length;
                        if (HeaderLength >= ColumnHeaderLength)
                        {
                            sName = sHeader.Substring(0, ColumnHeaderLength);
                        }
                        else
                        {
                            sName = sHeader;
                        }

                        if (!Columns.ContainsKey(sName))
                            Columns.Add(sName, i);
                    }

                    /* if found matching column break the loop */
                    if (sName.ToLower() == sColumnHeader.ToLower())
                    {
                        break;
                    }
                }

                string mColumnText = sColumnHeader.ToLower();


                if (Columns.ContainsKey(mColumnText))
                {
                    return Columns[mColumnText];
                }
                else
                {
                    throw new Exception("GetColumnIndexFromHeader() failed. Invalid column header '" + sColumnHeader + "'");
                }

            }

            private int GetRowCount()
            {
                //return GetRows().Count;
                IList<IWebElement> mRows = mTableElement.FindElements(By.CssSelector("table.bodyTable>tbody>tr"));
                if (mRows == null)
                {
                    mRows = mTableElement.FindElements(By.CssSelector("table>tbody>tr"));
                }
                List<IWebElement> mVisibleRows = new List<IWebElement>();
                for (int i = 0; i < mRows.Count; i++)
                {
                    DlkBaseControl row = new DlkBaseControl("Row", mRows[i]);
                    if (!row.GetAttributeValue("class").Contains("hide"))
                        mVisibleRows.Add(mRows[i]);
                }

                return mVisibleRows.Count;
            }

            /// <summary>
            /// handler for resource view 3 grids with 1 scrollbar
            /// </summary>
            /// <param name="scrollcount"></param>
            /// <param name="pageDirection"></param>
            /// <param name="delay"></param>
            private void ScrollDiv(int scrollcount, string pageDirection, string delay)
            {
                string JavaScriptScrollValue = "500";
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                //moves to the div containing the table scrollbar, this will throw an exception if the find element fails.
                actions.MoveToElement(mTableElement.FindElement(By.XPath("../../*[@class='rightPanel']//*[@class='rowTools']"))).Perform();
                for (int i = 1; i <= scrollcount; i++)
                {
                    IsLoadingScreenIsDisplayed(delay,i);
                    //resource view handler, (3 grids as 1)
                    var x = mTableElement.FindElement(By.XPath("../../*[@class='rightPanel']//*[@class='rowTools']"));
                    switch (pageDirection.ToLower())
                    {
                        case "up":
                            jse.ExecuteScript("arguments[0].scrollTop -= " + JavaScriptScrollValue, x);
                            break;
                        case "down":
                            jse.ExecuteScript("arguments[0].scrollTop += " + JavaScriptScrollValue, x);
                            //actions.MoveToElement(x).MoveByOffset(elementWidthInPixels, elementHeightInPixels / 2).ClickAndHold().MoveByOffset(0, 100).Release().Perform();
                            break;
                        default:
                            throw new Exception("Invalid direction");
                    }
                }
            }       

            private void ScrollTable(int scrollcount, string pageDirection, string delay)
            {
                
                string JavaScriptScrollValue = "500";
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                IWebElement gridBody = null;
                //if the element includes the table headers
                if (mTableElement.FindElements(By.XPath("../../descendant::*[@class='gridBody ']")).Count > 0)
                {
                    //then get just the body.
                    gridBody = mTableElement.FindElement(By.XPath("../../descendant::*[@class='gridBody ']"));
                }
                actions.MoveToElement(mTableElement).Click();
                for (int i = 1; i <= scrollcount; i++)
                {
                   IsLoadingScreenIsDisplayed(delay,i);
                   /*for scrolling, I think that this is also an option:
                    * ((OpenQA.Selenium.Remote.RemoteWebDriver)DlkEnvironment.AutoDriver).ExecuteScript("scroll(0,20000)");
                      or this:
                    * actions.MoveToElement(mTableElement).MoveByOffset(-5, 10).ClickAndHold().MoveByOffset(0, 100).Release().Perform();
                    */
                   switch (pageDirection.ToLower())
                   {
                       case "up":
                           jse.ExecuteScript("arguments[0].scrollTop -= " + JavaScriptScrollValue, gridBody != null ? gridBody : mTableElement);
                           break;
                       case "down":
                           jse.ExecuteScript("arguments[0].scrollTop += " + JavaScriptScrollValue, gridBody != null ? gridBody : mTableElement);
                           break;
                       default:
                           throw new Exception("Invalid direction");
                   }   
                }
            }

            //keep scrolling until paging occurs
            private void IsLoadingScreenIsDisplayed(string delay,int pressCount)
            {
                try
                {
                    IWebElement spinner;
                    double waitSeconds;
                    double.TryParse(delay,out waitSeconds);
                    int curRetry = 0;
                    int retryLimit = 3;
                    while (curRetry++ <= retryLimit)
                    {
                        //gets the div containing the 'Getting more...' part
                        spinner = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[text()='Getting more...']/../self::*[@style='display: block;']"));
                        if (spinner.Displayed)
                        {
                            //wait for a maximum of X seconds depending on the delay set by the user.
                            var wait = (new OpenQA.Selenium.Support.UI.WebDriverWait(DlkEnvironment.AutoDriver, TimeSpan.FromSeconds(waitSeconds)));

                            wait.IgnoreExceptionTypes(new Type[] { typeof(NoSuchElementException) });
                            //waits until paging is finished
                            //Workaround for using Selenium ExpectedConditions since it is now obselete
                            wait.Until(condition =>
                                {
                                    try
                                    {
                                        spinner = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*[text()='Getting more...']/../self::*[@style='display: block;']"));
                                        return !spinner.Displayed;
                                    }
                                    catch (StaleElementReferenceException)
                                    {
                                        return true;
                                    }
                                    catch (NoSuchElementException)
                                    {
                                        return true;
                                    }
                                });

                        }
                    }
                    Thread.Sleep(500);//split second delay to make it humanly possible to see the scrolling
                }
                catch (Exception e)
                {
                    if (!(e is NoSuchElementException))
                    {
                        throw e;
                    }   
                }
            }
           

            private IList<IWebElement> GetRows()
            {
                return mTableElement.FindElements(By.CssSelector("table.bodyTable>tbody>tr"));
            }

            //For Reporting table
            private IList<IWebElement> GetRowsForReporting()
            {
                return mTableElement.FindElements(By.CssSelector("table>tbody>tr"));
            }

            private List<string> GetNavigatorGridColumnHeaders()
            {
                List<string> mColumnHeaders = new List<string>();

                IList<IWebElement> mHeaders = mTableElement.FindElements(By.CssSelector("span.columnHdr"));
                for (int i = 0; i < mHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    mColumnHeaders.Add(ctlHeader.GetAttributeValue("title").ToLower() + "~");
                }

                return mColumnHeaders;
            }

            private string GetTextColValue(IWebElement Cell)
            {
                IWebElement mTextElm;
                try { mTextElm = Cell.FindElement(By.CssSelector("input")); }
                catch { mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput")); }
                DlkTextBox txtCell = new DlkTextBox("TextBox", mTextElm);
                return txtCell.GetValue();
            }

            private string GetLabelColValue(IWebElement Cell)
            {
                DlkLabel txtCell = new DlkLabel("Label", Cell);
                String ActValue = txtCell.GetValue().Trim();
                if (ActValue.Contains("\r\n"))
                {
                    ActValue = ActValue.Replace("\r\n", "<br>");
                }
                return ActValue;
            }

            private void VerifyTextColValue(IWebElement Cell, string ExpectedValue)
            {
                IWebElement mTextElm;
                DlkTextBox txtCell;
                try { mTextElm = Cell.FindElement(By.CssSelector("input")); }
                catch
                {

                    try { mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput")); }

                    catch
                    {
                        mTextElm = Cell.FindElement(By.ClassName("gridImage")); //to handle images title verification
                        txtCell = new DlkTextBox("TextBox", mTextElm);
                        txtCell.VerifyAttribute("title", ExpectedValue);
                        return;
                    }
                }

                txtCell = new DlkTextBox("TextBox", mTextElm);
                txtCell.VerifyText(ExpectedValue.Trim());

            }

            private void VerifyLabelColValue(IWebElement Cell, string ExpectedValue)
            {
                DlkLabel txtCell = new DlkLabel("TextBox", Cell);
                txtCell.VerifyText(ExpectedValue.Trim());
            }

            private void VerifyTextColReadOnly(IWebElement Cell, string TrueOrFalse)
            {
                IWebElement mTextElm;
                try { mTextElm = Cell.FindElement(By.CssSelector("input")); }
                catch { mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput")); }
                DlkTextBox txtCell = new DlkTextBox("TextBox", mTextElm);
                txtCell.VerifyReadOnly(TrueOrFalse);
            }

            private void SetAndEnterTextColValue(IWebElement Cell, string Value)
            {
                IWebElement mTextElm;
                try { mTextElm = Cell.FindElement(By.CssSelector("input")); }
                catch { mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput")); }
                DlkTextBox txtCell = new DlkTextBox("TextBox", mTextElm);
                txtCell.SetAndEnter(Value);
            }

            private void PressTabTextCol(IWebElement Cell)
            {
                IWebElement mTextElm;
                try { mTextElm = Cell.FindElement(By.CssSelector("input")); }
                catch { mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput")); }
                DlkTextBox txtCell = new DlkTextBox("TextBox", mTextElm);
                txtCell.PressTab();
            }

            private string GetDDwnColValue(IWebElement Cell)
            {
                DlkComboBox cboCell = new DlkComboBox("ComboBox", Cell);
                return cboCell.GetValue();
            }

            private void VerifyDDwnColValue(IWebElement Cell, string ExpectedValue)
            {
                DlkComboBox cboCell = new DlkComboBox("ComboBox", Cell);
                cboCell.VerifyValue(ExpectedValue);
            }

            private void VerifyDDwnColReadOnly(IWebElement Cell, string TrueOrFalse)
            {
                DlkComboBox cboCell = new DlkComboBox("ComboBox", Cell);
                cboCell.VerifyReadOnly(TrueOrFalse);
            }

            private void SetDDwnColValue(IWebElement Cell, string Value)
            {
                DlkComboBox cboCell = new DlkComboBox("ComboBox", Cell);
                cboCell.Select(Value);
            }

            private void SetSelectColValue(IWebElement Cell, string Value)
            {
                IWebElement mSelect = Cell.FindElement(By.TagName("select"));
                DlkComboBox cboCell = new DlkComboBox("ComboBox", mSelect);
                cboCell.Select(Value);
            }

            private void SetMultiSelectColValue(IWebElement Cell, string Value)
            {
                DlkMultiselect multiCell = new DlkMultiselect("Multiselect", Cell);
                multiCell.SetItem(Value);
            }

            private void DeleteMultiSelectColValue(IWebElement Cell, string Value)
            {
                DlkMultiselect multiCell = new DlkMultiselect("Multiselect", Cell);
                multiCell.DeleteItem(Value);
            }

            private void ClickDDwnColLink(IWebElement Cell, string Value)
            {
                DlkComboBox cboCell = new DlkComboBox("ComboBox", Cell);
                cboCell.ClickTableLink(Value);
            }

            private void VerifyDDwnColLinkExists(IWebElement Cell, string Value)
            {
                DlkComboBox cboCell = new DlkComboBox("ComboBox", Cell);
                cboCell.VerifyTableLinkExists(Value);
            }

            private string GetCheckBoxColValue(IWebElement Cell)
            {
                IWebElement mCheckBoxElm = Cell.FindElement(By.CssSelector("span.gridCheckbox"));
                DlkCheckBox chkCell = new DlkCheckBox("CheckBox", mCheckBoxElm);
                return Convert.ToString(chkCell.GetCheckedState());
            }

            private void VerifyCheckBoxColValue(IWebElement Cell, string ExpectedValue)
            {
                IWebElement mCheckBoxElm = Cell.FindElement(By.CssSelector("span.gridCheckbox"));
                DlkCheckBox chkCell = new DlkCheckBox("CheckBox", mCheckBoxElm);
                chkCell.VerifyValue(ExpectedValue);
            }

            private void VerifyCheckBoxColReadOnly(IWebElement Cell, string TrueOrFalse)
            {
                IWebElement mCheckBoxElm = Cell.FindElement(By.CssSelector("span.gridCheckbox"));
                DlkCheckBox chkCell = new DlkCheckBox("CheckBox", mCheckBoxElm);
                chkCell.VerifyReadOnly(TrueOrFalse);
            }

            private void SetCheckBoxColValue(IWebElement Cell, string Value)
            {
                IWebElement mCheckBoxElm = Cell.FindElement(By.CssSelector("span.gridCheckbox"));
                DlkCheckBox chkCell = new DlkCheckBox("CheckBox", mCheckBoxElm);
                chkCell.Set(Value);
            }

            private List<String> GetRowData(int iRow, Boolean IncludeInputValues)
            {
                // Initialize();
                List<String> rowData = new List<String>();
                IWebElement TableRow;
                TableRow = GetRow(iRow);
                if (TableRow == null)
                {
                    TableRow = GetReportingRow(iRow);
                }


                // IWebElement TableRow = mElmTableRows[iRow];
                IList<IWebElement> mElmCells = TableRow.FindElements(By.TagName("td"));
                if ((mElmCells == null) || (mElmCells.Count < 1))
                {
                    mElmCells = TableRow.FindElements(By.TagName("th"));
                }
                foreach (IWebElement mCell in mElmCells)
                {
                    String mCellVal = mCell.Text.Trim();
                    if (IncludeInputValues)
                    {
                        if ((mCellVal == "") || (mCellVal == "undefined"))
                        {
                            IWebElement mInputElm;
                            try
                            {

                                mInputElm = mCell.FindElement(By.TagName("input"));
                                mCellVal = mInputElm.GetAttribute("value");
                            }
                            catch
                            {
                                //nothing
                            }
                        }
                    }
                    rowData.Add(mCellVal);
                }
                return rowData;
            }

            private DlkTable GetResourceTable(String ParentResourceOrLevel, int iTableNo)
            {

                DlkTable mResourceTable = null;
                int iRow = GetRowByKey(0, ParentResourceOrLevel);
                iRow++;

                IWebElement mRow = GetRow(iRow);
                String mRowClass = "";

                for (int i = iRow; i < GetRows().Count; i++)
                {
                    mRowClass = GetRowAttribute(i, "class");
                    if (mRowClass.ToLower() == "externalcontainerrow")
                    {
                        IWebElement mElm = mRow.FindElements(By.TagName("table"))[iTableNo];
                        mResourceTable = new DlkTable("ResourceRowTable", mElm);
                        break;
                    }
                }
                return mResourceTable;
            }
            #endregion

            #region PUBLIC METHODS
            public void Scroll(int scrollcount, String direction, string delay)
            {
                try
                {
                    if (mTableElement != null && scrollcount != 0)
                    {
                        if (mTableElement.FindElements(By.XPath("../../*[@class='rightPanel']//*[@class='rowTools']")).Count > 0)
                        {
                            ScrollDiv(scrollcount, direction, delay);
                        }
                        else
                        {
                            ScrollTable(scrollcount, direction, delay);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Scroll() Failed. " + e.Message);
                }
            }

            public string GetCellValue(int iRow, string sColumnHeader)
            {
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    return null;

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                string sClass = mCell.GetAttributeValue("class");
                if (sClass.Contains("roleDdwnCol"))
                {

                }
                else if (sClass.Contains("textCol"))
                {
                    return GetTextColValue(mCell.mElement);
                }
                else if (sClass.Contains("htmlCol"))
                {
                    return GetTextColValue(mCell.mElement);
                }

                else if (sClass.Contains("lookupCol"))
                {
                    return GetTextColValue(mCell.mElement);
                }

                else if (sClass.Contains("ddwnCol"))
                {
                    return GetDDwnColValue(mCell.mElement);
                }
                else if (sClass.Contains("checkboxCol"))
                {
                    return GetCheckBoxColValue(mCell.mElement);
                }
                else if (sClass.Contains("lookupCol"))
                {
                    return GetTextColValue(mCell.mElement);
                }
                else if (sClass.Contains("buttonCol"))
                {
                    return GetTextColValue(mCell.mElement);
                }
                else if (sClass.Contains("treeCol"))
                {
                    if(mCellElm.FindElements(By.TagName("input")).Count > 0)
                        return GetTextColValue(mCell.mElement);
                    else
                        return GetLabelColValue(mCell.mElement);
                }
                else    //Reporting table in ngCRM
                {
                    return GetLabelColValue(mCell.mElement);
                }
                return null;
            }

            public void SetCellValue(int iRow, string sColumnHeader, string sValue)
            {
                IWebElement mRowElm = GetRow(iRow);
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);

                //If no row is selected in the table, the cells don't have a class (New UI - 11/09/2016)
                if (!mRowElm.GetAttribute("class").Contains("selected"))
                    mRowElm.Click();

                if (mCellElm == null)
                    throw new Exception("SetCellValue() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                string sClass = mCell.GetAttributeValue("class");

                if (sClass.Contains("checkbox"))
                {
                    //Doing a click on checkbox alters the value and may affect other controls
                    SetCheckBoxColValue(mCell.mElement, sValue);
                }
                else
                {
                    //clicking the cell to get the correct class
                    //since there are scenarios when the class of the cell changes when clicked
                    mCell.Click();
                    sClass = mCell.GetAttributeValue("class");

                    if ((sClass.Contains("lookup")) && (sClass.Contains("ddwnCol")))
                    {
                        try
                        {
                            SetTextWithoutClear(mCell.mElement, sValue);
                        }
                        catch
                        {
                            mCell.Click();
                            SetTextColValue(mCell.mElement, sValue);
                        }
                    }
                    else if ((sClass.Contains("ddwnCol")) && (sClass.Contains("multiselect")))
                    {
                        SetMultiSelectColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("textCol"))
                    {
                        SetTextColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("htmlCol"))
                    {
                        SetTextColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("ddwnCol"))
                    {
                        SetDDwnColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("object-"))
                    {
                        SetSelectColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("lookupCol"))
                    {
                        SetTextColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("emailCol"))
                    {
                        SetTextColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("edit"))
                    {
                        SetDDwnColValue(mCell.mElement, sValue);
                    }
                }
            }

            /// <summary>
            /// Used for Textboxes where using Clear() would disable the control
            /// </summary>
            /// <param name="Cell"></param>
            /// <param name="Value"></param>
            private void SetTextWithoutClear(IWebElement Cell, string Value)
            {
                IWebElement mInput;
                try { mInput = Cell.FindElement(By.CssSelector("input")); }
                catch { mInput = Cell.FindElement(By.CssSelector("div.htmlInput")); }

                mInput.SendKeys(Keys.Control + "a");
                DlkLogger.LogInfo("Highlighting current text..");
                Thread.Sleep(5000);// add a delay because it loads after focusing. it seems we cannot delete the current text while it is loading/searching for records. so we wait
                DlkLogger.LogInfo("Overwriting current text using new text: " + Value);
                mInput.SendKeys(Value);
                Thread.Sleep(5000);// add a delay because it loads after focusing.
            }

            public void DeleteCellValue(int iRow, string sColumnHeader, string sValue)
            {
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("DeleteCellValue() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                string sClass = mCell.GetAttributeValue("class");
                if ((sClass.Contains("ddwnCol")) && (sClass.Contains("multiselect"))) 
                {
                    DeleteMultiSelectColValue(mCell.mElement, sValue);
                }
                else 
                {
                    //for future use
                }
            }           

            public void SetAndEnterCellValue(int iRow, string sColumnHeader, string sValue)
            {
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("SetCellValue() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                mCell.Click();
                string sClass = mCell.GetAttributeValue("class");
                if ((sClass.Contains("lookup")) && (sClass.Contains("ddwnCol")))
                {
                    SetAndEnterTextColValue(mCell.mElement, sValue);                   
                }
                else if ((sClass.Contains("ddwnCol")) && (sClass.Contains("multiselect")))
                {
                    SetMultiSelectColValue(mCell.mElement, sValue);
                }
                else if (sClass.Contains("textCol"))
                {
                    SetAndEnterTextColValue(mCell.mElement, sValue);
                }
                else if (sClass.Contains("htmlCol"))
                {
                    SetAndEnterTextColValue(mCell.mElement, sValue);
                }
                else if (sClass.Contains("ddwnCol"))
                {
                    SetDDwnColValue(mCell.mElement, sValue);
                }
                else if (sClass.Contains("checkboxCol"))
                {
                    SetCheckBoxColValue(mCell.mElement, sValue);
                }
                else if (sClass.Contains("lookupCol"))
                {
                    SetAndEnterTextColValue(mCell.mElement, sValue);
                }
                else if (sClass.Contains("emailCol"))
                {
                    SetAndEnterTextColValue(mCell.mElement, sValue);
                }
                else if (sClass.Contains("edit"))
                {
                    SetDDwnColValue(mCell.mElement, sValue);
                }
            }

            public void PressTab(int iRow, string sColumnHeader)
            {
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("SetCellValue() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                mCell.Click();
                string sClass = mCell.GetAttributeValue("class");
                if (sClass.Contains("roleDdwnCol"))
                {

                }
                else if (sClass.Contains("textCol"))
                {
                    PressTabTextCol(mCell.mElement);
                }
                else if (sClass.Contains("htmlCol"))
                {
                    PressTabTextCol(mCell.mElement);
                }
                else if (sClass.Contains("ddwnCol"))
                {
                   //do nothing
                }
                else if (sClass.Contains("checkboxCol"))
                {
                    //do nothing
                }
                else if (sClass.Contains("lookupCol"))
                {
                    PressTabTextCol(mCell.mElement);
                }
            }

            public void VerifyCellValue(int iRow, string sColumnHeader, string sExpectedValue)
            {                
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("VerifyCellValue() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                string sClass = mCell.GetAttributeValue("class");
                if (sClass.Contains("buttonCol"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("textCol")) 
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("ddwnCol"))
                {
                    VerifyDDwnColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("checkboxCol"))
                {
                    VerifyCheckBoxColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("lookupCol"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("null"))  //Reporting table in ngCRM
                {
                    VerifyLabelColValue(mCell.mElement, sExpectedValue);
                }
                else if ((sClass.Contains("right")) || (sClass == ""))//table ngRP
                {
                    VerifyLabelColValue(mCell.mElement, sExpectedValue);
                }

            }

            public void VerifyCellReadOnly(int iRow, string sColumnHeader, string TrueOrFalse)
            {
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("VerifyCellValue() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                string sClass = mCell.GetAttributeValue("class");
                if (sClass.Contains("roleDdwnCol"))
                {

                }
                else if (sClass.Contains("textCol"))
                {
                    VerifyTextColReadOnly(mCell.mElement, TrueOrFalse);
                }
                else if (sClass.Contains("htmlCol"))
                {
                    VerifyTextColReadOnly(mCell.mElement, TrueOrFalse);
                }
                else if (sClass.Contains("ddwnCol"))
                {
                    VerifyDDwnColReadOnly(mCell.mElement, TrueOrFalse);
                }
                else if (sClass.Contains("checkboxCol"))
                {
                    VerifyCheckBoxColReadOnly(mCell.mElement, TrueOrFalse);
                }
                else if (sClass.Contains("lookupCol"))
                {
                    VerifyTextColReadOnly(mCell.mElement, TrueOrFalse);
                }

            }

            public int GetRowWithColumnValue(string sColumnHeader, string sValue)
            {
                int i = 0;
                if (sValue == "")
                {
                    sValue = null;
                }
                string cellValue = GetCellValue(i, sColumnHeader);
                while (cellValue != null)
                {
                    if (cellValue == sValue)
                    {
                        return i;

                    }

                    i++;
                    cellValue = GetCellValue(i, sColumnHeader);
                }
                return -1;
            }

            public int GetRowWithColumnValueContains(string sColumnHeader, string sValue)
            {
                int i = 0;
                if (sValue == "")
                {
                    sValue = null;
                }
                string cellValue = GetCellValue(i, sColumnHeader);
                while (cellValue != null)
                {
                    if (cellValue.Contains(sValue))
                    {
                        return i;

                    }

                    i++;
                    cellValue = GetCellValue(i, sColumnHeader);
                }

                return -1;
            }

            public void ClickRowButton(int iRow, string sButtonCaption)
            {
                DlkBaseControl ctlRowButton;
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);
                ctlRow.MouseOver();

                //Have to comment out this new code as it doesn't work with CRM/Configuration tables
                //MouseOver should be on first column on the row since there are tables with rows that are expanded
                //IWebElement firstTD = mRow.FindElement(By.XPath("./*[1]"));
                //DlkBaseControl ctlTD = new DlkBaseControl("Row", firstTD);

                //ctlTD.MouseOver();
                

                try
                {
                    IWebElement mRowButton = null;
                    switch (sButtonCaption.ToLower())
                    {
                        case "receiptdelete":
                            mRowButton = mRow.FindElement(By.XPath(".//div[@class='gridImage'][contains(@style,'ReceiptDelete')]"));
                            break;
                        case "receiptview":
                            mRowButton = mRow.FindElement(By.XPath(".//div[@class='gridImage'][contains(@style,'ReceiptView')]"));
                            break;
                        case "options":
                            mRowButton = mRow.FindElement(By.CssSelector("[title='" + sButtonCaption + "']"));
                            break;
                        case "delete":
                            mRowButton = mRow.FindElement(By.CssSelector("[title='" + sButtonCaption + "']"));
                            break;                            
                        default:
                            mRowButton = mRow.FindElement(By.CssSelector("div.gridImage[title='" + sButtonCaption + "']"));
                            break;
                    }

                    ctlRowButton = new DlkBaseControl("Row Button", mRowButton);
                    
                }
                catch
                {
                    throw new Exception("ClickRowButton() failed. Row button '" + sButtonCaption + "' not found.");
                }

                ctlRowButton.MouseOverOffset(0, 0);
                Thread.Sleep(1000);
                ctlRowButton.Click();
            }


            public void VerifyRowButton(int iRow, string sButtonCaption, string TrueOrFalse)
            {
                
                
                Boolean bRowButtonExists = false;
                IWebElement mRow = GetRow(iRow);
             
                
                try
                {
                    if ((mRow.FindElements(By.CssSelector("div.gridImage[title='" + sButtonCaption + "']")).Count > 0)
                       || (mRow.FindElements(By.XPath(".//button[@title='" + sButtonCaption + "']")).Count > 0))

                    {
                        //Verify if the button is displayed or not. If the button is not displayed, it will set to false.
                        IReadOnlyCollection<IWebElement> mRowButtons = mRow.FindElements(By.XPath("./descendant::*[@title='" + sButtonCaption + "']"));

                        foreach (IWebElement rowButton in mRowButtons)
                        { 
                            if (rowButton.Displayed)
                            {
                                bRowButtonExists = true;
                                DlkLogger.LogInfo("VerifyRowButton(): Row button found. Caption: [" + sButtonCaption + "]");
                                break;
                            }
                        }                    
                    }

                    if (bRowButtonExists)
                    {
                        DlkAssert.AssertEqual("VerifyRowButton", Convert.ToBoolean(TrueOrFalse), bRowButtonExists);
                    }
                    else
                    {
                        DlkLogger.LogInfo("VerifyRowButton(): Row button not found. Caption: [" + sButtonCaption + "]");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("VerifyRowButton() failed.",ex);
                }
            }

            public void ClickCellButton(int iRow, string sColumnHeader, string sButtonCaption)
            {

                DlkBaseControl ctlCellButton;
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                DlkBaseControl ctlCell = new DlkBaseControl("Cell", mCellElm);
                ctlCell.ScrollIntoViewUsingJavaScript();
                ctlCell.MouseOver();
                try
                {
                   // IWebElement mCellButton = mCellElm.FindElement(By.Name(sButtonCaption));
                    IWebElement mCellButton = mCellElm.FindElement(By.TagName("button"));
                    ctlCellButton = new DlkBaseControl("Cell Button", mCellButton);
                    ctlCellButton.Click();
                }
                catch (NoSuchElementException)
                {
                    throw new Exception("ClickCellButton() failed. Cell button '" + sButtonCaption + "' not found.");
                }
                catch
                {
                    // Ignore. For chrome, sometimes 1 click "ctlCell.Click()" is enough to perform desired click
                }
            }

            public void VerifyCellButtonTooltip(int iRow, string sColumnHeader, string sButtonCaption)
            {
                string ActualValue;
                try
                {
                    DlkBaseControl ctlCellButton;
                    IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                    DlkBaseControl ctlCell = new DlkBaseControl("Cell", mCellElm);
                    ctlCell.ScrollIntoViewUsingJavaScript();

                    IWebElement mCellButton = mCellElm.FindElement(By.TagName("button"));
                    ctlCellButton = new DlkBaseControl("Cell Button", mCellButton);
                    ctlCellButton.MouseOverOffset(0, 0);
                    ActualValue = ctlCellButton.GetAttributeValue("title");
                }

                catch (Exception ex)
                {
                    throw new Exception("VerifyCellButtonTooltip() failed. ", ex);
                }

                DlkAssert.AssertEqual("VerifyCellButtonTooltip", sButtonCaption, ActualValue);
            }

            public void ClickCellTooltip(int iRow, string sColumnHeader)
            {

                DlkBaseControl ctlCellButton;
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                DlkBaseControl ctlCell = new DlkBaseControl("Cell", mCellElm);
                ctlCell.ScrollIntoViewUsingJavaScript();      
                try
                {
                    IList<IWebElement> mCellButton = mCellElm.FindElements(By.TagName("span"));
                    foreach (IWebElement mElm in mCellButton)
                        if (mElm.GetAttribute("class").Contains("toolTipButton"))
                        {
                            ctlCellButton = new DlkBaseControl("Cell Button", mElm);
                            ctlCellButton.Click();
                            break;
                        }
                }
                catch (NoSuchElementException)
                {
                    throw new Exception("ClickCellTooltip() failed. Cell button not found.");
                }
            }

            public void ClickCellLink(int iRow, string sColumnHeader, string sLinkText)
            {

                DlkBaseControl ctlCellLink;
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                IWebElement mCellLink = null;
                DlkBaseControl ctlCell = new DlkBaseControl("Cell", mCellElm);
                ctlCell.ScrollIntoViewUsingJavaScript();
                try{
                    if (mCellElm.FindElements(By.XPath(".//*[contains(@class,'cellLink')][contains(.,'" + sLinkText + "')]")).Count > 0)
                    {
                        mCellLink = mCellElm.FindElement((By.XPath(".//*[contains(@class,'cellLink')][contains(.,'" + sLinkText + "')]")));
                    }else{
                        mCellLink = mCellElm.FindElement(By.LinkText(sLinkText));
                    }
                    ctlCellLink = new DlkBaseControl("Cell Link", mCellLink);
                    ctlCellLink.Click();
                }
                catch
                {
                    mCellLink = mCellElm.FindElement(By.XPath(".//*[contains(@class,'infoBubble')][contains(.,'" + sLinkText + "')]"));
                    ctlCellLink = new DlkBaseControl("Cell Link", mCellLink);
                    ctlCellLink.Click();
                }
            }
           

           public void ClickCellLinkByColumnNumber(String RowNumber, String ColumnNumber, String LinkText)
           {           
                try
                {
                    int targetColumnNumber = Convert.ToInt32(ColumnNumber)+1;
                    int targetRowNumber = Convert.ToInt32(RowNumber) +1;
                    IWebElement targetCell = null;


                    targetCell = mTableElement.FindElement(By.XPath(".//tr[" + targetRowNumber + "]/td[" + targetColumnNumber + "]"));
               

                    DlkBaseControl ctlCellLink;
                    DlkBaseControl ctlCell = new DlkBaseControl("Cell", targetCell);
                    ctlCell.ScrollIntoViewUsingJavaScript();
                    ctlCell.Click();
                    try
                    {
                       
                        IWebElement mCellLink = null;
                        if (targetCell.FindElements(By.XPath(".//*[contains(@class,'cellLink')]")).Count > 0)
                        {
                            mCellLink = targetCell.FindElement((By.XPath(".//*[contains(@class,'cellLink')]")));
                        }
                        else
                        {
                            mCellLink = targetCell.FindElement(By.LinkText(LinkText));
                        }
                        ctlCellLink = new DlkBaseControl("Cell Link", mCellLink);
                        ctlCellLink.Click();
                    }
                    catch (NoSuchElementException)
                    {
                        throw new Exception("ClickCellLinkByColumnNumber() failed. Cell link '" + LinkText + "' not found.");
                    }
                    catch
                    {
                        // Ignore. For chrome, sometimes 1 click "ctlCell.Click()" is enough to perform desired click
                    }
                    DlkLogger.LogInfo("ClickCellLink() successfully executed.");
                }
                catch (Exception e)
                {
                    throw new Exception("ClickCellLink() failed : " + e.Message, e);
                }
           }

            public void ClickDropDownLink(int iRow, string sColumnHeader, string sLinkCaption)
            {
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("ClickDropDownLink() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                mCell.Click();

                ClickDDwnColLink(mCell.mElement, sLinkCaption);

            }

            public void VerifyDropDownLinkExists(int iRow, string sColumnHeader, string sExpectedValue)
            {
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("ClickDropDownLink() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                mCell.Click();

                VerifyDDwnColLinkExists(mCell.mElement, sExpectedValue);

            }

            public void SelectRow(int iRow)
            {
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);
                ctlRow.ScrollIntoViewUsingJavaScript();
                try
                {
                    ctlRow.ClickUsingJavaScript();
                    //ctlRow.Click();
                    //ctlRow.ClickAndHold();
                }
                catch
                {
                    throw new Exception("SelectRow() failed. Row '" + iRow + "' not found.");
                }


            }

            public IWebElement GetCell(int iRow, string sColumnHeader)
            {
                int iCol = -1;
                try 
                { 
                    iCol = GetColumnIndexFromHeader(sColumnHeader.ToLower());                
                }
                catch 
                {
                    iCol = GetColumnIndexByCellName(sColumnHeader); 
                }

                IWebElement mRow = GetRow(iRow);
                if (mRow == null)
                {
                    return null;
                }
                else
                {
                    IList<IWebElement> cells = mRow.FindElements(By.CssSelector("td"));
                    return cells[iCol];
                }
            }


            public IWebElement GetRow(int iRow)
            {

                try
                {
                    try
                    {
                        try
                        {

                            IWebElement mRow = mTableElement.FindElement(By.CssSelector("table.bodyTable>tbody>tr:not([class~='hide']):nth-child(" + (iRow + 1).ToString() + ")"));
                            return mRow;
                        }
                        catch
                        {
                            IWebElement mRow = mTableElement.FindElement(By.CssSelector("table>tbody>tr:not([class~='hide']):nth-child(" + (iRow + 1).ToString() + ")"));
                            int i = 2;
                            while (mRow.Displayed == false)
                            {
                                mRow = mTableElement.FindElement(By.CssSelector("table>tbody>tr:not([class~='hide']):nth-child(" + (iRow + i).ToString() + ")"));
                                i++;
                            }
                            return mRow;
                        }
                    }
                    catch
                    {
                        IWebElement mRow = mTableElement.FindElement(By.XPath("./descendant::table/tbody/tr[not(contains(@class,'hide'))][" + (iRow).ToString() + "]"));
                        return mRow;
                    }
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return null;

                }
            }

            public IWebElement GetReportingRow(int iRow)
            {

                try
                {
                    IWebElement mRow = mTableElement.FindElement(By.CssSelector("table>tbody>tr:not([class~='hide']):nth-child(" + (iRow + 1).ToString() + ")"));
                    return mRow;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return null;
                }

            }

           
            public List<string> GetColumnHeaders()
            {
                switch (mClass)
                {
                    case "navigator_grid":
                        return GetNavigatorGridColumnHeaders();
                }

                return null;
            }

            public bool VerifyColumnHeader(String ColumnHeader)
            {
                bool bFound = false;
                IList<IWebElement> mHeaders = mTableElement.FindElements(By.CssSelector("span.columnHdr"));

                //some tables don't have span.columnhdr, in those cases use generic th instead
                if (mHeaders.Count == 0)
                {
                    mHeaders = mTableElement.FindElements(By.CssSelector("th"));
                }
                for (int i = 0; i < mHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    IWebElement ctlParent1 = ctlHeader.GetParent();
                    DlkBaseControl ctlParent = new DlkBaseControl("Parent", ctlParent1);
                    IWebElement ctlParent2 = ctlParent.GetParent();
                    string strStyle = ctlParent2.GetAttribute("style");
                    string strStyle1 = ctlHeader.GetAttributeValue("style");
                    if (ctlHeader.GetAttributeValue("title") != "")
                    {
                        if ((ctlHeader.GetAttributeValue("title").ToLower().Equals(ColumnHeader.ToLower())) && !(strStyle.Contains("display: none")))
                        {
                            bFound = true;
                            break; 
                        }
                    }
                    else
                    {
                        if ((ctlHeader.GetAttributeValue("textContent").ToLower().Equals(ColumnHeader.ToLower())) && !(strStyle1.Contains("display: none")))
                        {
                            bFound = true;
                            break;
                        }
                    }

                }
                return bFound;
            }

            public bool IsColumnRequired(String ColumnHeader)
            {
                bool bRequired = false;

                IList<IWebElement> mHeaders = mTableElement.FindElements(By.CssSelector("span.columnHdr"));
                for (int i = 0; i < mHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    if (ctlHeader.GetAttributeValue("title").ToLower().Equals(ColumnHeader.ToLower()))
                    {
                        try 
                        { 
                        
                          if (mHeaders[i].FindElements(By.TagName("span"))[1].GetAttribute("class").Contains("required"))
                        {
                            bRequired = true;
                            break;
                        }
                            }
                        catch
                        {
                            bRequired = false;
                            break;
                        }
                    }
                }

                return bRequired;
            }
                 
            public bool IsColumnFiltered(String ColumnHeader)
            {
                bool bFiltered = false;

                IList<IWebElement> mHeaders = mTableElement.FindElements(By.CssSelector("th"));
                for (int i = 0; i < mHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    if (ctlHeader.mElement.Text.Trim().ToLower().Equals(ColumnHeader.ToLower()))
                    {
                        try
                        {
                            if (ctlHeader.mElement.GetAttribute("class").Contains("ui-state-filtered"))
                            {
                                bFiltered = true;
                                break;
                            }
                        }
                        catch
                        {
                            bFiltered = false;
                            break;
                        }
                    }
                }

                return bFiltered;
            }

            public void SortColumn(String ColumnHeader, String SortOrder)
            {
                ColumnHeader = ColumnHeader.ToLower();
                SortOrder = SortOrder.ToLower();
                bool bFound = false;
                IList<IWebElement> mHeaders = mTableElement.FindElements(By.TagName("th"));
                DlkBaseControl ctlHeader =  new DlkBaseControl("Header", null);

                for (int i = 0; i < mHeaders.Count; i++)
                {
                    ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    try
                    {
                        if (ctlHeader.GetAttributeValue("name").ToLower().Contains(ColumnHeader)) 
                        {
                            bFound = true;
                            break;
                        }
                        else if (mHeaders[i].Text.ToLower().Contains(ColumnHeader))
                        {
                            bFound = true;
                            break;
                        }
                    }
                    catch
                    {
                        //if name attribute doesn't exist, use this instead
                        if (ctlHeader.GetAttributeValue("textContent").ToLower().Contains(ColumnHeader))
                        {
                            bFound = true;
                            break;
                        }
                    }
                }

                if (bFound)
                {
                    ctlHeader.Click();
                    if (SortOrder == "descending")
                    {
                        if (ctlHeader.GetAttributeValue("class").Contains("sortD"))
                        {
                            //do not re-sort, it's already Descending
                            DlkLogger.LogInfo("Column " + ColumnHeader + "is already in Descending order");
                        }
                        else
                        {
                            ctlHeader.Click();
                            DlkLogger.LogInfo("Column " + ColumnHeader + "re-sorted in Descending order");
                        }
                    }
                    else if (SortOrder == "ascending")
                    {
                        if (ctlHeader.GetAttributeValue("class").Contains("sortA"))
                        {
                            DlkLogger.LogInfo("Column " + ColumnHeader + "is already in Ascending order");
                        }
                        else
                        {
                            ctlHeader.Click();
                            DlkLogger.LogInfo("Column " + ColumnHeader + "re-sorted in Ascending order");
                        }
                    }
                }
                else
                {
                    throw new Exception("SortColumn() failed: Column " + ColumnHeader + " was not found.");
                }
            }

            public void VerifySortColumn(String ColumnHeader, String SortOrder)
            {
                ColumnHeader = ColumnHeader.ToLower();
                SortOrder = SortOrder.ToLower();
                bool bFound = false;
                String ActSortOrder = "";
                IList<IWebElement> mHeaders = mTableElement.FindElements(By.TagName("th"));
                DlkBaseControl ctlHeader = new DlkBaseControl("Header", null);

                for (int i = 0; i < mHeaders.Count; i++)
                {
                    ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    try
                    {
                        if (ctlHeader.GetAttributeValue("name").ToLower().Contains(ColumnHeader))
                        {
                            bFound = true;
                            break;
                        }
                        else if (mHeaders[i].Text.ToLower().Contains(ColumnHeader))
                        {
                            bFound = true;
                            break;
                        }
                    }
                    catch
                    {
                        //if name attribute doesn't exist, use this instead
                        if (ctlHeader.GetAttributeValue("textContent").ToLower().Contains(ColumnHeader))
                        {
                            bFound = true;
                            break;
                        }
                    }
                }

                if (bFound)
                {
                   
                        if (ctlHeader.GetAttributeValue("class").Contains("sortD"))
                        {
                            ActSortOrder = "descending";
                        }
                        else if (ctlHeader.GetAttributeValue("class").Contains("sortA"))
                        {
                            ActSortOrder = "ascending";
                        }

                DlkAssert.AssertEqual("VerifySortColumn", SortOrder, ActSortOrder);
                }
                else
                {
                    throw new Exception("VerifySortColumn() failed: Column " + ColumnHeader + " was not found.");
                }
            }
       
            public void SetTextColValue(IWebElement Cell, string Value)
            {
                IWebElement mTextElm;
                try {mTextElm = Cell.FindElement(By.CssSelector("input"));}
                catch {mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput"));}
               
                mTextElm.Clear();
                //mTextElm.SendKeys(Keys.Control + "A");
                mTextElm.SendKeys(Value);
                Thread.Sleep(3000);
                DlkLogger.LogInfo("Column Value successfully set to [" + Value + "]");
            }

            // For Navigator PM grids
            //*********************************************************************************************************************
            //DlkTable mResourceTable = GetResourceTable(ParentResourceOrLevel, 1);  //Table1 - Resource table data
            //DlkTable mCalHeadersTable = GetResourceTable(ParentResourceOrLevel, 2); //Table2 - Calendar table Dates
            //DlkTable mCalDataTable = GetResourceTable(ParentResourceOrLevel, 3);    //Table 3 - Calendar table Hours
            
            public void ExpandAllRows()
            {
                IList<IWebElement> mMatchImg = mTableElement.FindElements(By.TagName("button"));
                IList<IWebElement> buttonsInTbl = new List<IWebElement>(mMatchImg);
                foreach (IWebElement buttonCheck in buttonsInTbl.ToList())
                {
                    if (buttonCheck.GetAttribute("class").Contains("cellIcon Inactive"))
                        buttonsInTbl.Remove(buttonCheck);
                    if(buttonCheck.FindElement(By.XPath("..")).GetAttribute("class").Contains("disabled delete"))
                        buttonsInTbl.Remove(buttonCheck);
                    if (buttonCheck.FindElement(By.XPath("..")).GetCssValue("display").Contains("none"))
                        buttonsInTbl.Remove(buttonCheck);
                    if (buttonCheck.GetAttribute("title").ToLower() == "add assignments")
                        buttonsInTbl.Remove(buttonCheck);
                    if (buttonCheck.GetAttribute("title").ToLower() == "assign resources")
                        buttonsInTbl.Remove(buttonCheck);
                    if (!buttonCheck.Displayed)
                        buttonsInTbl.Remove(buttonCheck);
                }

                if (buttonsInTbl.Count == 0)
                {
                    //for rows not using button to expand
                    buttonsInTbl = mTableElement.FindElements(By.XPath(".//span[contains(@class,'tree-wrap')]"));

                    if (buttonsInTbl.Count == 0)
                    {
                        //for ngRP
                        buttonsInTbl = mTableElement.FindElements(By.XPath(".//div[contains(@class,'treeIcon')]"));
                        for (int j = 0; j < buttonsInTbl.Count; j++)
                        {
                            try
                            {
                                DlkImage mImgTmp = new DlkImage("ExpandImageLink", buttonsInTbl[j]);
                                String mSrc = buttonsInTbl[j].GetAttribute("class");
                                if (mSrc.Contains("treeIcon"))
                                {
                                    //if (!mSrc.Contains("expanded") && !mSrc.Contains("noChildren"))
                                    if (!mSrc.Contains("expanded"))
                                    {
                                        mImgTmp.ScrollIntoViewUsingJavaScript();
                                        buttonsInTbl[j].Click();
                                        DlkLogger.LogInfo("Successfully expanded row " + j);
                                        Thread.Sleep(1000);
                                    }
                                }
                            }
                            catch
                            {
                                //object reference not set, refresh contents of buttonsInTbl

                                buttonsInTbl = mTableElement.FindElements(By.XPath(".//div[contains(@class,'treeIcon')]"));
                                DlkImage mImgTmp = new DlkImage("ExpandImageLink", buttonsInTbl[j]);
                                String mSrc = buttonsInTbl[j].GetAttribute("class");
                                if (mSrc.Contains("treeIcon"))
                                {
                                    if (!mSrc.Contains("expanded"))
                                    {
                                        mImgTmp.ScrollIntoViewUsingJavaScript();
                                        buttonsInTbl[j].Click();
                                        DlkLogger.LogInfo("Successfully clicked link with src: " + mSrc);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //for CRM reporting table
                        for (int j = 0; j < buttonsInTbl.Count; j++)
                        {
                            DlkImage mImgTmp = new DlkImage("ExpandImageLink", buttonsInTbl[j]);
                            String mSrc = buttonsInTbl[j].GetAttribute("class");
                            if (mSrc.Contains("tree-wrap"))
                            {
                                if (!mSrc.Contains("minus"))
                                {
                                    mImgTmp.ScrollIntoView();
                                    buttonsInTbl[j].Click();
                                    DlkLogger.LogInfo("Successfully clicked link with src: " + mSrc);
                                }
                            }
                        }
                    }                   
                }
                else
                {
                    for (int j = 0; j < buttonsInTbl.Count; j++)
                    {
                        DlkImage mImgTmp = new DlkImage("ExpandImageLink", buttonsInTbl[j]);
                        String mSrc = buttonsInTbl[j].GetAttribute("class");
                        if (mSrc.Contains("treeBttn"))
                        {
                            if (!mSrc.Contains("expanded"))
                            {
                                mImgTmp.ScrollIntoView();
                                buttonsInTbl[j].Click();
                                DlkLogger.LogInfo("Successfully clicked link with src: " + mSrc);
                            }
                        }
                        if (mSrc.Contains("treeIcon"))
                        {
                            //if (!mSrc.Contains("expanded") && !mSrc.Contains("noChildren"))
                            if (!mSrc.Contains("expanded"))
                            {
                                mImgTmp.ScrollIntoViewUsingJavaScript();
                                buttonsInTbl[j].Click();
                                DlkLogger.LogInfo("Successfully expanded row " + j);
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
                
            }

            public void VerifyResourceToolTip(String iRow, String sToolTipText)
            {                
                try
                {
                    IWebElement mRow = GetRow(Convert.ToInt32(iRow));
                    string iconXpath = "//div[contains(@class,'treeIcon noChildren')]";

                    DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow.FindElement(By.XPath(iconXpath)));
                    ctlRow.MouseOverOffset(0, 0);

                    IWebElement mRowIcon = mRow.FindElement(By.XPath(iconXpath));
                    
                    
                    string toolTip = mRowIcon.GetAttribute("title");
                    DlkAssert.AssertEqual("VerifyResourceTooltip", sToolTipText, toolTip);
                }
                catch (Exception e)
                {
                    throw new Exception("VerifyResourceTooltip() failed : " + e.Message, e);
                }
            }
          
            public int GetRowByKey(int iCol, String MatchValue)
            {

                int iRow = -1;

                for (int i = 0; i < GetRows().Count; i++)
                {
                    List<String> rData = GetRowData(i, true);
                    if (rData.Contains(MatchValue))
                    {
                        if (rData[iCol].Contains(MatchValue))
                        {
                            iRow = i;
                            break;
                        }
                    }
                }
                return iRow;
            }

            public int GetRowByColumnNumber(int iCol, String MatchValue)
            {

                int iRow = -1;

                for (int i = 0; i < GetRowsForReporting().Count; i++)
                {
                    List<String> rData = GetRowData(i, true);
                    if (rData[iCol].Contains(MatchValue)) //((rData.Contains(MatchValue)) || 
                    {
                    
                       // if (rData[iCol].Contains(MatchValue))
                      //  {
                            iRow = i;
                            break;
                      //  }
                    }
                }
                return iRow;
            }

            public String GetRowAttribute(int iRow, String AttributeName)
            {
                String mAttrib = "";

               // IWebElement TableRow = mElmTableRows[iRow];

                IWebElement TableRow = GetRow(iRow);

                mAttrib = TableRow.GetAttribute(AttributeName);
                return mAttrib;
            }

           // Resource View button to select Calendar or Summary View for the Resources
            public void ClickResourceViewButton(String ParentResourceOrLevel)
            {

                DlkBaseControl ctlRowButton;
                int iRow = GetRowByKey(0, ParentResourceOrLevel);
                iRow++;
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);
                IList<IWebElement> mRowButton = mRow.FindElements(By.TagName("button"));
                foreach (IWebElement mElm in mRowButton)
                    if (mElm.GetAttribute("class").Contains("viewSwitch")) 
                    {
                        ctlRowButton = new DlkBaseControl("Row Button", mElm);
                        ctlRowButton.Click();
                        break;
                    }
            }

            // Add Resource button on Project Management grids
            public void ClickAddResource(String ParentResourceOrLevel)
            {

                DlkBaseControl ctlRowButton;
                int iRow = GetRowByKey(0, ParentResourceOrLevel);
                iRow++;
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);
                IList<IWebElement> mRowButton = mRow.FindElements(By.TagName("button"));
                foreach (IWebElement mElm in mRowButton)
                    if (mElm.GetAttribute("class").Equals("addResource"))
                    {
                        ctlRowButton = new DlkBaseControl("Row Button", mElm);
                        ctlRowButton.Click();
                        break;
                    }
            }

            //Resource Grid Tooltip button on the Project Management Grids
            public void ClickResourceToolTip(String ParentResourceOrLevel)
            {

                DlkBaseControl ctlRowButton;
                int iRow = GetRowByKey(0, ParentResourceOrLevel);
                iRow++;
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);
                IList<IWebElement> mRowButton = mRow.FindElements(By.TagName("div"));
                foreach (IWebElement mElm in mRowButton)

                    if (((mElm.GetAttribute("class").Contains("toolTipButton")) && (mElm.GetAttribute("style").Equals("display: block;"))) || (mElm.GetAttribute("class").Contains("resourceGridTooltip")))
                        {

                            ctlRowButton = new DlkBaseControl("Row Button", mElm);
                            ctlRowButton.Click();
                            break;

                        }                       
            }

            public DlkTextBox GetResourceNotesTextBox(String ParentResourceOrLevel)
            {
                int iRow = GetRowByKey(0, ParentResourceOrLevel);
                iRow++;
              
                DlkTextBox mNotes = null;
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);
                IList<IWebElement> mRowNotes = mRow.FindElements(By.TagName("div"));
                foreach (IWebElement mElm in mRowNotes)

                    if (mElm.GetAttribute("class") =="notes")
                    {

                        mNotes = new DlkTextBox("Notes Text Box", mElm);
                        break;

                    }                   
                
                //for (int i = iRow; i < RowCount; i++)
                //{
                //    mRowClass = this.tblLaborPlanningData.GetRowAttribute(i, "class");

                //    if (mRowClass.ToLower() == "externalcontainerrow")
                //    {
                //        IWebElement mElm = TableRows[i].FindElement(By.ClassName("notesDiv"));
                //        mNotes = new DlkTextBox("Notes Text Box", mElm);
                //        break;
                //    }
                //}
                return mNotes;
            }
            
            //Resource Row
            public void ClickResourceRowButton(String ParentResourceOrLevel, String ResourceName, String ButtonName)
            {
             
               int iRow = GetRowByKey(1, ResourceName);
                
                DlkBaseControl ctlRowButton;
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);               
                try
                {
                    IWebElement mRowButton = mRow.FindElement(By.CssSelector("td[name='" + ButtonName + "']"));
                    
                    ctlRowButton = new DlkBaseControl("Row Button", mRowButton);
                }
                catch
                {
                    throw new Exception("ClickRowButton() failed. Row button '" + ButtonName + "' not found.");
                }

                
                ctlRowButton.Click();
            }

            public int GetResourceRow(String ResourceName)
            {

                int iRow = GetRowByKey(1, ResourceName);                 
                return iRow;
            }
#endregion

            // End of Navigator PM grids
            //***********************************************************************************************************************
        }

        #region KEYWORDS
        //Keywords
        [Keyword("VerifyColumnHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnHeader(String ColumnHeader, String TrueOrFalse)
        {
            Initialize();
            //string ActualHeader = Convert.ToString(TableData.GetColumnHeaders());
            //ActualHeader = ActualHeader.Trim('~');
            DlkAssert.AssertEqual("VerifyColumnHeader", Convert.ToBoolean(TrueOrFalse), TableData.VerifyColumnHeader(ColumnHeader));
        }

        [Keyword("VerifyColumnRequired", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnRequired(String ColumnHeader, String TrueOrFalse)
        {
            Initialize();
            DlkAssert.AssertEqual("VerifyColumnRequired", Convert.ToBoolean(TrueOrFalse), TableData.IsColumnRequired(ColumnHeader) );
        }

        [Keyword("VerifyColumnFiltered", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnFiltered(String ColumnHeader, String TrueOrFalse)
        {
            Initialize();
            DlkAssert.AssertEqual("VerifyColumnFiltered", Convert.ToBoolean(TrueOrFalse), TableData.IsColumnFiltered(ColumnHeader));
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

        [Keyword("GetRowWithColumnValue", new String[] { "1|text|ColumnHeader|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetRowWithColumnValue(string ColumnHeader, string Value, string VariableName)
        {
            Initialize();
            int iRow = TableData.GetRowWithColumnValue(ColumnHeader, Value);
            if (iRow > -1)
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                DlkLogger.LogInfo("GetRowWithColumnValue() passed.");
            }
            else
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                //throw new Exception("GetRowWithColumnValue() failed. Unable to find row.");
            }
        }

        [Keyword("GetRowWithColumnValueContains", new String[] { "1|text|ColumnHeader|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetRowWithColumnValueContains(string ColumnHeader, string Value, string VariableName)
        {
            Initialize();
            int iRow = TableData.GetRowWithColumnValueContains(ColumnHeader, Value);
            if (iRow > -1)
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                DlkLogger.LogInfo("GetRowWithColumnValueContains() passed.");
            }
            else
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                //throw new Exception("GetRowWithColumnValueContains() failed. Unable to find row.");
            }
        }

        [Keyword("GetRowByColumnNumber", new String[] { "1|text|Resource name|Resource Name", 
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetRowByColumnNumber(string ColumnNum, string Value, string VariableName)
        {
            Initialize();
            int iColumnNum = Convert.ToInt32(ColumnNum);
            int iRow = TableData.GetRowByColumnNumber(iColumnNum, Value);
            if (iRow > -1)
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                DlkLogger.LogInfo("GetResourceRow() passed.");
            }
            else
            {
                //DlkVariable.SetVariable(VariableName, iRow.ToString());
                throw new Exception("GetResourceRow() failed. Unable to find row.");
            }
        }
        


        [Keyword("GetCopiedRowNum", new String[] { "1|text|Row|RowNum",
                                                         "2|text|VariableName|MyRow"})]
        public void GetCopiedRowNum(string Row, string VariableName)
        {
            Initialize();
            int iRow = Convert.ToInt32(Row) +1;           
            if (iRow > -1)
            {
                DlkVariable.SetVariable(VariableName, iRow.ToString());
                DlkLogger.LogInfo("GetCopiedRowNum passed.");
            }
            else
            {
                throw new Exception("GetCopiedRowNum failed. Unable to find row.");
            }
        }

        [Keyword("VerifyRowExistsWithColumnValue", new String[] { "1|text|ColumnHeader|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|ExpectedValue|TRUE"})]
        public void VerifyRowExistsWithColumnValue(string ColumnHeader, string Value, string TrueOrFalse)
        {
            bool bResult = false;
            Initialize();
            int iRow = TableData.GetRowWithColumnValue(ColumnHeader, Value);
            if (iRow > -1)
            {
                bResult = true;
            }

            DlkAssert.AssertEqual("VerifyRowExistsWithColumnValue", Convert.ToBoolean(TrueOrFalse), bResult);

        }

        [Keyword("SetCellValue", new String[] { "1|text|Row|1",
                                                "2|text|ColumnHeader|Notes",
                                                "3|text|Value|sample text"})]
        public void SetCellValue(string Row, string ColumnHeader, string Value)
        {
            Initialize();
            TableData.SetCellValue(int.Parse(Row), ColumnHeader, Value);
        }

        [Keyword("DeleteCellValue", new String[] { "1|text|Row|1",
                                                "2|text|ColumnHeader|Notes",
                                                "3|text|Value|sample text"})]
        public void DeleteCellValue(string Row, string ColumnHeader, string Value)
        {
            Initialize();
            TableData.DeleteCellValue(int.Parse(Row), ColumnHeader, Value);
        }

        [Keyword("SetCellText", new String[] { "1|text|Row|1",
                                        "2|text|ColumnHeader|Notes",
                                        "3|text|Text|sample text"})]
        public void SetCellText(string Row, string ColumnHeader, string Text)
        {
            try
            {
                Initialize();
                IWebElement mCellElm = TableData.GetCell(int.Parse(Row), ColumnHeader);

                if (mCellElm == null)
                    throw new Exception("SetCellText() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                mCell.Click();
                TableData.SetTextColValue(mCell.mElement, Text);
            }
            catch (Exception e)
            {
                throw new Exception("SetCellText() failed : " + e.Message, e);

            }
        }

        [Keyword("SetAndEnterCellValue", new String[] { "1|text|Row|1",
                                                "2|text|ColumnHeader|Notes",
                                                "3|text|Value|sample text"})]
        public void SetAndEnterCellValue(string Row, string ColumnHeader, string Value)
        {
            Initialize();
            TableData.SetAndEnterCellValue(int.Parse(Row), ColumnHeader, Value);
        }

        [Keyword("PressTab", new String[] { "1|text|Row|1",
                                                "2|text|ColumnHeader|Notes"})]
        public void PressTab(string Row, string ColumnHeader)
        {
            Initialize();
            TableData.PressTab(int.Parse(Row), ColumnHeader);
        }

        [Keyword("VerifyCellValue", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes",
                                                   "3|text|ExpectedValue|sample text"})]
        public void VerifyCellValue(string Row, string ColumnHeader, string ExpectedValue)
        {           
            Initialize();
            TableData.VerifyCellValue(int.Parse(Row), ColumnHeader, ExpectedValue);
        }

        [Keyword("VerifyCellEmpty", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes"})]
        public void VerifyCellEmpty(string Row, string ColumnHeader)
        {            
            Initialize();
            TableData.VerifyCellValue(int.Parse(Row), ColumnHeader, "");
        }


        [Keyword("VerifyCellReadOnly", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes",
                                                   "3|text|ExpectedValue|sample text"})]
        public void VerifyCellReadOnly(string Row, string ColumnHeader, string TrueOrFalse)
        {
            Initialize();
            TableData.VerifyCellReadOnly(int.Parse(Row), ColumnHeader, TrueOrFalse);
        }

        [Keyword("ClickRowButton", new String[] { "1|text|Row|1",
                                                  "2|text|ButtonCaption|Delete"})]
        public void ClickRowButton(string Row, string Caption)
        {
            Initialize();
            TableData.ClickRowButton(int.Parse(Row), Caption);
        }

        [Keyword("VerifyRowButton", new String[] { "1|text|Row|1",
                                                  "2|text|ButtonCaption|Delete",
                                                    "3|text|ExpectedValue|sample text"})]
        public void VerifyRowButton(string Row, string Caption, string TrueOrFalse)
        {
            Initialize();
            TableData.VerifyRowButton(int.Parse(Row), Caption, TrueOrFalse);
        }


        [Keyword("ClickCellButton", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes",
                                                  "2|text|ButtonCaption|Delete"})]
        public void ClickCellButton(string Row, string ColumnHeader, string Caption)
        {
            Initialize();
            TableData.ClickCellButton(int.Parse(Row), ColumnHeader, Caption);
        }

        [Keyword("VerifyCellButtonTooltip", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes",
                                                  "2|text|Tooltip|Delete"})]
        public void VerifyCellButtonTooltip(string Row, string ColumnHeader, string Tooltip)
        {
            Initialize();
            TableData.VerifyCellButtonTooltip(int.Parse(Row), ColumnHeader, Tooltip);
        }

        [Keyword("ClickCellTooltip", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes"})]
        public void ClickCellTooltip(string Row, string ColumnHeader)
        {
            Initialize();
            TableData.ClickCellTooltip(int.Parse(Row), ColumnHeader);
        }

        [Keyword("ClickCellLink", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes",
                                                  "2|text|LinkText|Delete"})]
        public void ClickCellLink(string Row, string ColumnHeader, string LinkText)
        {
            Initialize();
            TableData.ClickCellLink(int.Parse(Row), ColumnHeader, LinkText);
        }


        [Keyword("ClickCellLinkByColumnNumber", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellLinkByColumnNumber(String RowNumber, String ColumnNumber, String LinkText)
        {
            Initialize();
            TableData.ClickCellLinkByColumnNumber(RowNumber, ColumnNumber, LinkText);

        }
           
       

        [Keyword("SelectRow", new String[] { "1|text|Row|1" })]
        public void SelectRow(string Row)
        {
            Initialize();
            TableData.SelectRow(int.Parse(Row));
        }

        [Keyword("ClickDropDownLink", new String[] { "1|text|Row|1",
                                                  "2|text|ColumnHeader|Notes",})]
        public void ClickDropDownLink(string Row, string ColumnHeader, string LinkCaption)
        {
            Initialize();
            TableData.ClickDropDownLink(int.Parse(Row), ColumnHeader, LinkCaption);
        }

        [Keyword("VerifyDropDownLinkExists", new String[] { "1|text|Row|1|True",
                                                  "2|text|ColumnHeader|Notes|True",})]
        public void VerifyDropDownLinkExists(string Row, string ColumnHeader, String TrueOrFalse)
        {
            Initialize();
            TableData.VerifyDropDownLinkExists(int.Parse(Row), ColumnHeader, TrueOrFalse);
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Row|1" })]
        public void VerifyRowCount(string Count)
        {
            try
            {
                Initialize();
                int actRowCount = mElement.FindElements(By.XPath("./descendant::tbody/tr")).Count;
                DlkAssert.AssertEqual("VerifyRowCount()", int.Parse(Count), actRowCount);
                DlkLogger.LogInfo("VerifyRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void GetRowCount( string VariableName)
        {
           Initialize();
           int actRowCount = mElement.FindElements(By.XPath("./descendant::tbody/tr")).Count -1;
          // int actRowCount = mElement.FindElements(By.CssSelector("table.bodyTable>tbody>tr:not(.hide_filtered)")).Count -1;
           DlkVariable.SetVariable(VariableName, actRowCount.ToString());
           DlkLogger.LogInfo("GetRowCount() successfully executed.");
            
        }

        [Keyword("GetActualRowCount", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void GetActualRowCount(string VariableName)
        {
            Initialize();
            int actRowCount = mElement.FindElements(By.XPath("./descendant::tbody/tr")).Count;
            DlkVariable.SetVariable(VariableName, actRowCount.ToString());
            DlkLogger.LogInfo("GetActualRowCount() successfully executed.");

        }

        [Keyword("VerifyItemInDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInDropDownList(String ColumnHeader, String Row, String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement targetCell = TableData.GetCell(Convert.ToInt32(Row), ColumnHeader);

                if (targetCell.FindElements(By.XPath("./descendant::button[1]")).Count > 0)
                {
                    IWebElement mDropDownButton = targetCell.FindElement(By.XPath("./descendant::button[1]"));
                  //  string actual = "";
                    DlkBaseControl lstResults = new DlkBaseControl("ResultsList", "XPATH_DISPLAY", "//ul[normalize-space(@class)='results dropdown-style']");
                    /* Click only drop-down button if results list not displayed */
                    if (!lstResults.Exists()) /* Is Nav style table dropdown list results NOT displayed? */
                    {
                        /* Is CRM style table dropdown list results displayed */
                        if (mElement.FindElements(By.XPath("//div[@class='ddwnListContainer openedBelow']/descendant::td[@name='Desc']")).Count > 0)
                        {
                            // Do nothing
                        }
                        else /* No results list displayed -> click dropd down button */
                        {
                            targetCell.Click();
                            mDropDownButton.Click();
                            Thread.Sleep(3000);
                        }
                    }
                    if (lstResults.Exists()) /* Nav style table dropdown results list */
                    {
                        DlkAssert.AssertEqual("VerifyItemInDropDownList()", bool.Parse(TrueOrFalse), mElement.FindElements(By.XPath(
                            "//ul[normalize-space(@class)='results dropdown-style']/descendant::div[normalize-space(@class)='search-name'][translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                            + Item.ToLower() + "']")).Count > 0);
                        mDropDownButton.Click();
                        DlkLogger.LogInfo("VerifyItemInDropDownList() passed");
                    }
                    else /* CRM style table dropdown results list */
                    {
                        DlkAssert.AssertEqual("VerifyItemInDropDownList()", bool.Parse(TrueOrFalse), 
                            mElement.FindElements(By.XPath("//div[@class='ddwnListContainer openedBelow']/descendant::tr[@class='ddwnListItem']/td[@name='Desc']/div[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='" 
                            + Item.ToLower() + "']")).Count > 0);
                        mDropDownButton.Click();
                        DlkLogger.LogInfo("VerifyItemInDropDownList() passed");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDropDownList(String ColumnHeader, String Row, String Items)
        {
            try
            {
                Initialize();
                IWebElement targetCell = TableData.GetCell(Convert.ToInt32(Row), ColumnHeader);

                if (targetCell.FindElements(By.XPath("./descendant::button[1]")).Count > 0)
                {
                    IWebElement mDropDownButton = targetCell.FindElement(By.XPath("./descendant::button[1]"));
                    string actual = "";
                    DlkBaseControl lstResults = new DlkBaseControl("ResultsList", "XPATH_DISPLAY", "//ul[normalize-space(@class)='results dropdown-style']");
                    /* Click only drop-down button if results list not displayed */
                    if (!lstResults.Exists()) /* Is Nav style table dropdown list results NOT displayed? */
                    {
                        /* Is CRM style table dropdown list results displayed */
                        if (mElement.FindElements(By.XPath("//div[@class='ddwnListContainer openedBelow']/descendant::td[@name='Desc']")).Count > 0)
                        {
                            // Do nothing
                        }
                        else /* No results list displayed -> click dropd down button */
                        {
                            targetCell.Click();
                            mDropDownButton.Click();
                            Thread.Sleep(3000);
                        }
                    }
                    
                    if (lstResults.Exists()) /* Nav style table dropdown results list */
                    {
                        foreach (IWebElement elm in lstResults.mElement.FindElements(By.XPath("./descendant::li")))
                        {
                            actual += new DlkBaseControl("Text", elm.FindElement(By.XPath("./descendant::div[normalize-space(@class)='search-name']"))).GetValue().ToUpper() + "~";
                        }
                    }
                    else /* CRM style table dropdown results list */
                    {
                        foreach (IWebElement elm in mElement.FindElements(By.XPath("//div[@class='ddwnListContainer openedBelow']/descendant::tr[@class='ddwnListItem']/td[@name='Desc']")))
                        {
                            actual += new DlkBaseControl("Text", elm.FindElement(By.XPath("./div[1]"))).GetValue().ToUpper() + "~";
                        }
                    }
                    actual = actual.Trim('~');
                    DlkAssert.AssertEqual("VerifyDropDownList()", DlkString.ReplaceCarriageReturn(Items.ToUpper(), ""), DlkString.ReplaceCarriageReturn(actual, ""));
                    mDropDownButton.Click();
                    DlkLogger.LogInfo("VerifyDropDownList() passed");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCell(String Row, String ColumnHeader)
        {
            try
            {
                Initialize();
                IWebElement targetCell = TableData.GetCell(Convert.ToInt32(Row), ColumnHeader);
                //IWebElement targetLink = targetCell.FindElement(By.XPath("./descendant::div[@class='inputContainer']/input[1]"));
                new DlkBaseControl("TargetCell", targetCell).Click();
                DlkLogger.LogInfo("ClickCell() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCell() failed : " + e.Message, e);
            }
        }


        [Keyword("ClickLineOption", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickLineOption(String Row, String Option)
        {
            try
            {
                Initialize();
                ClickDropdownArrow(Row);
                Thread.Sleep(250);
                //IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));

                IWebElement opt = null;
                IWebElement popup = DlkEnvironment.AutoDriver.FindElement(By.ClassName("popupmenuBody"));
                // two tries, Delete seems to be 2 items
                try
                {
                    DlkLogger.LogInfo("Attempting to click first instance of option");
                    opt = popup.FindElements(By.XPath("//span[text()='" + Option + "']")).First();
                    opt.Click();
                }
                catch
                {
                    DlkLogger.LogInfo("Attempting to click last instance of option");
                    opt = popup.FindElements(By.XPath("//span[text()='" + Option + "']")).Last();
                    opt.Click();
                }

                DlkLogger.LogInfo("ClickLineOption() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickLineOption() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyLineOptions")]
        public void VerifyLineOptions(String Row, String ListItems)
        {
            try
            {
                Initialize();
                ClickDropdownArrow(Row);
                Thread.Sleep(2000);
                //IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement popup = DlkEnvironment.AutoDriver.FindElement(By.ClassName("popupmenuBody"));
                String listVisibleOptions = "";
                ReadOnlyCollection<IWebElement> optionItems = popup.FindElements(By.XPath("//span[contains(@class,'popupLabel')]"));
                if (optionItems.Count == 0)
                {
                    Thread.Sleep(1000);
                }

                foreach (IWebElement opt in optionItems)
                {
                    if (string.IsNullOrEmpty(opt.Text) || string.IsNullOrEmpty(opt.Text.Trim()))
                    {
                        continue;
                    }
                    listVisibleOptions += opt.Text + "~";
                }
                listVisibleOptions = listVisibleOptions.Trim('~');
                DlkAssert.AssertEqual("VerifyLineOptions", ListItems, listVisibleOptions);
                DlkLogger.LogInfo("VerifyLineOptions() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptions() failed : " + e.Message, e);
            }
        }

        public void ClickDropdownArrow(String LineIndex)
        {
            String index = (Convert.ToInt32(LineIndex) + 1).ToString();
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + index + "]"));
            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            int attempts = 0;
            new DlkBaseControl("FirstCol", firstCol).MouseOver();
            firstCol.Click();

            //reassign targetrow; DOM changes upon click   
            try
            {
                targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + index + "]"));
                IWebElement btn = targetRow.FindElement(By.XPath("./descendant::div[@class='gridImage']"));
                while (attempts < 5)
                {
                    try
                    {
                        new DlkBaseControl("targetButton", btn).MouseOver();
                        btn.Click();
                        break;
                    }
                    catch
                    {
                        //do nothing
                    }
                    attempts++;
                }
            }
            catch
            {
                //nothing
            }
        }

        /// <summary>
        /// Verifies Column Data
        /// </summary>
        /// <param name="Col"></param> 
        /// <param name="ExpectedResult"></param>   
        /// <param name="IncludeInputValues"></param>       
        [Keyword("VerifyColumnData", new String[] { "1|text|Expected Value|Sample Column Number",
                                                         "2|text|Expected Value|Sample Expected Value",
                                                               "3|text|Value|TRUE"})]
        public void VerifyColumnData(String Col, String ExpectedResults)
        {
            try
            {
                Initialize();
                List<String> lsExpectedResults = ExpectedResults.Split('~').ToList();
                for (int i = 0; i < RowCount - 1; i++)
                {
                    if (!TableData.GetRowAttribute(i, "class").Contains("hide"))
                    {
                        String ActResult = TableData.GetCellValue(i, Col);
                        if (ActResult.Contains("\r\n"))
                        {
                            ActResult = ActResult.Replace("\r\n", "<br>");
                        }
                        if (ActResult != null)
                        {
                            DlkLogger.LogInfo("VerifyColumnData() process log: Found data [" + i.ToString() + "]: " + ActResult);
                            List<String> mRange = lsExpectedResults[i].Split(':').ToList();
                            if (mRange.Count == 1) // no range
                            {
                                double dResult = -1;                                
                                if (double.TryParse(mRange[0], out dResult)) // numerical comparison if applicable
                                {
                                    double dActResult = -1;
                                    if (double.TryParse(ActResult, out dActResult))
                                    {
                                        DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + i.ToString(), dResult, dActResult);
                                    }
                                    else // string compare
                                    {
                                        DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + i.ToString(), lsExpectedResults[i], ActResult);
                                    }
                                }
                                else // string compare
                                {
                                    DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + i.ToString(), lsExpectedResults[i], ActResult);
                                }
                            }
                            else //we have a range
                            {
                                //double dLower = double.Parse(mRange[0], System.Globalization.NumberStyles.Currency);
                                //double dUpper = double.Parse(mRange[1], System.Globalization.NumberStyles.Currency);
                                //double dActResult = double.Parse(ActResult, System.Globalization.NumberStyles.Currency);

                                //DlkAssert.AssertWithinRange("Column compare with range validation.", dLower, dUpper, dActResult);
                            }
                        }
                    }
                    else
                    {
                        lsExpectedResults.Insert(i, "");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnData() failed." ,ex);
            }
        }

         /// <summary>
        /// Verifies Column Data
        /// </summary>
        /// <param name="Col"></param> 
        /// <param name="ExpectedResult"></param>   
        /// <param name="IncludeInputValues"></param>       
        [Keyword("AssignCellValueToVariable", new String[] { "1|text|Expected Value|Sample Column Number",
                                                         "2|text|Expected Value|Sample Expected Value",
                                                               "3|text|Value|TRUE"})]
        public void AssignCellValueToVariable(String ColumnHeader, String Row, String VariableName)
        {
            try
            {
                Initialize();
                int i = -1;
                if (!Int32.TryParse(Row, out i))
                {
                    throw new Exception("AssignCellValueToVariable() : Invalid input for Row number [" + Row + "]");
                }
                string cellValue = TableData.GetCellValue(i, ColumnHeader);
                DlkLogger.LogInfo("GetCellValue(): Cell value obtained. [" + cellValue + "]");
                DlkVariable.SetVariable(VariableName, cellValue);
                DlkLogger.LogInfo("AssignCellValueToVariable() passed.");

            }
            catch (Exception ex)
            {
                DlkLogger.LogInfo("SetResourceImageExist() failed : " + ex.Message);
                throw new Exception("SetResourceImageExist() failed : " + ex.Message, ex);
            }
        }

        [Keyword("SortColumn", new String[] { "1|text|Expected Value|Column Name",
                                                "2|text|Expected Value|Descending or Ascending"})]
        public void SortColumn(String ColumnHeader, String SortOrder)
        {
            try
            {
                Initialize();
                TableData.SortColumn(ColumnHeader, SortOrder);
            }
            catch (Exception e)
            {
                throw new Exception("SortColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySortColumn", new String[] { "1|text|Expected Value|Column Name",
                                                "2|text|Expected Value|Descending or Ascending"})]
        public void VerifySortColumn(String ColumnHeader, String SortOrder)
        {
            try
            {
                Initialize();
                TableData.VerifySortColumn(ColumnHeader, SortOrder);
            }
            catch (Exception e)
            {
                throw new Exception("VerifySortColumn() failed : " + e.Message, e);
            }
        }

        //For Navigator Labor Grid
        //***************************************************************************************
        [Keyword("ExpandAllRows")]
        public void ExpandAllRows()
        {
            Initialize();
            TableData.ExpandAllRows();
        }

        // Will work for Delete Resource
        [Keyword("ClickResourceImage", new String[] { "1|text|Expected Value|Resource Name",
                                                "2|text|Expected Value|ImageName"})]
        public void ClickResourceImage(String Parent, String ResourceName, String ImageName)
        {
            Initialize();
            TableData.ClickResourceRowButton(Parent, ResourceName, ImageName);
        }

        // Resource View button to select Calendar or Summary View for the Resources on the Labor Grid
        [Keyword("ClickResourceViewButton", new String[] { "1|text|Expected Value|Level" })]
        public void ClickResourceViewButton(String ResourceParent)
        {
            Initialize();
            TableData.ClickResourceViewButton(ResourceParent);
           
        }

        //Add Resource button on the Project Management Grids
        [Keyword("ClickAddResource", new String[] { "1|text|Expected Value|Level" })]
        public void ClickAddResource(String ResourceParent)
        {
            Initialize();
            TableData.ClickAddResource(ResourceParent);

        }

        //Resource Grid Tooltip button on the Project Management Grids
        [Keyword("ClickResourceToolTip", new String[] { "1|text|Expected Value|Level" })]
        public void ClickResourceToolTip(String ResourceParent)
        {
            Initialize();
            TableData.ClickResourceToolTip(ResourceParent);

        }

        //Resource Grid Notes text box on the Project Management Grids
        [Keyword("SetResourceNotes", new String[] { "1|text|Expected Value|Level",
                                                    "2|text|Expected Value|Notes"})]
        public void SetResourceNotes(String ResourceParent, String Notes)
        {
            Initialize();
            DlkTextBox TextBox = TableData.GetResourceNotesTextBox(ResourceParent);
            TextBox.Set(Notes);
        }

        [Keyword("VerifyResourceNotes", new String[] { "1|text|Expected Value|Level",
                                                    "2|text|Expected Value|Notes"})]
        public void VerifyResourceNotes(String ResourceParent, String Notes)
        {
            Initialize();
            DlkTextBox TextBox = TableData.GetResourceNotesTextBox(ResourceParent);
            TextBox.VerifyText(Notes);
        }

        [Keyword("VerifyResourceImageExist", new String[] { "1|text|Row|1",
                                                    "2|text|ExpectedResult|True"})]
        public void VerifyResourceImageExist(String Row, String ExpectedResult)
        {
            try
            {
                Initialize();
                string mDefaultResourceUrl = "images/RM_EmpImage.png";
                string mDimmedResourceUrl = "images/RM_GenImage.png";
                Boolean mExpectedResult = Boolean.Parse(ExpectedResult);

                IWebElement mRow = TableData.GetRow(Int32.Parse(Row));
                IWebElement mCell = mRow.FindElement(By.XPath("./td[@class='Col resource-photo']//div[@class='gridImage']"));
                
                string mResourceImageURL = mCell.GetCssValue("background-image");
                
                Boolean mActualResult = !mResourceImageURL.ToLower().Contains(mDefaultResourceUrl.ToLower());

                if (mResourceImageURL.ToLower().Contains(mDimmedResourceUrl.ToLower()))
                {
                    mActualResult = false;
                }
                DlkAssert.AssertEqual("VerifyResourceImageExist():", mExpectedResult, mActualResult);
            }
            catch (Exception ex)
            {
                DlkLogger.LogInfo("SetResourceImageExist() failed : " + ex.Message);
                throw new Exception("SetResourceImageExist() failed : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyResourceTooltip", new String[] { "1|text|Row|1",
                                                    "2|text|ExpectedResult|True"})]
        public void VerifyResourceTooltip(String iRow, string sToolTipText)
        {
            Initialize();
            TableData.VerifyResourceToolTip(iRow, sToolTipText);
        }

        [Keyword("VerifyRowData", new String[] { "1|text|Expected Value|Row Number",
                                                         "2|text|Expected Value|Expected Value~Expected Value"})]
        public void VerifyRowData(String Row, String ExpectedResults)
        {
            Initialize();
            int iRow = Convert.ToInt32(Row);
            List<String> lsRowData = GetRowData(iRow, true);
            String RowData = String.Join("~", lsRowData.ToArray());
            if (RowData.Contains("\r\n"))
            {
                RowData = RowData.Replace("\r\n", "<br>");
            }
            DlkAssert.AssertEqual("Row Data Compare: Row: " + iRow, ExpectedResults, RowData);

            //List<String> lsExpectedResults = ExpectedResults.Split('~').ToList();
           // List<String> lsActResults = GetRowData(iRow, true);
           // DlkAssert.AssertEqual("Row Data Compare: Row: " + iRow, lsExpectedResults, lsActResults);
        }

        [Keyword("VerifyItemImageDimmed", new String[] { "1|text|Row|1",
                                                    "2|text|ExpectedResult|True"})]
        public void VerifyItemImageDimmed(String Row, String ExpectedResult)
        {
            try
            {
                Initialize();
                Boolean ExpectedValue=false;
                Boolean ActualValue;
                if (Boolean.TryParse(ExpectedResult, out ExpectedValue))
                {
                    IWebElement mRow = TableData.GetRow(Int32.Parse(Row));
                    DlkLogger.LogInfo("VerifyItemImageDimmed() Item row obtained." );

                    if (mRow.GetAttribute("class").ToLower().Contains("inactive"))
                        ActualValue = true;
                    else
                        ActualValue = false;
                    DlkAssert.AssertEqual("VerifyItemImageDimmed():", ExpectedValue, ActualValue);
                }
                else
                {
                    throw new Exception("VerifyItemImageDimmed() failed. Invalid input [" + ExpectedResult + "]");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogInfo("VerifyItemImageDimmed() failed : " + ex.Message);
                throw new Exception("VerifyItemImageDimmed() failed : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyCellColor", new String[] { "1|text|Row|1",
                                                    "2|text|ColumnHeader|Notes",            
                                                    "3|text|Expected|Color" })]
        public void VerifyCellColor(string Row, string ColumnHeader, String Color)
        {
            String ColorCode = "";
    
            try
            {
               Initialize();
               IWebElement mCellElm = TableData.GetCell(int.Parse(Row), ColumnHeader);
                if (mCellElm == null)
                    throw new Exception("SetCellText() failed. Invalid row.");
                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);

                string sClass = mCell.GetAttributeValue("class");
                
                if (sClass.Contains("under"))
                {
                    ColorCode = "yellow";
                    
                }
                else if (sClass.Contains("over"))
                {
                    ColorCode = "red";
                  
                }
                else if ((!sClass.Contains("over"))&&(!sClass.Contains("under")))
                {
                    ColorCode = "gray";
                }

                DlkAssert.AssertEqual("VerifyColor", Color.ToLower(), ColorCode);
                DlkLogger.LogInfo("Successfully executed VerifyColor()");
            }
            catch (Exception e)
            {
                DlkLogger.LogError(e);
                throw new Exception("VerifyColor() failed : " + e.Message, e);
            }
        }

        //Scroll up or down grid
        [Keyword("ScrollGridUpOrDown")]
        public void ScrollGridUpOrDown(String NumberOfScrolls, String Direction, String Delay)
        {
            try
            {
                int numberOfScrolls;
                int.TryParse(NumberOfScrolls, out numberOfScrolls);
                Initialize();
                TableData.Scroll(numberOfScrolls, Direction.ToLower(), Delay);
                DlkLogger.LogInfo(string.Format("Successfully scrolled {1} {0} times", NumberOfScrolls, Direction.ToLower()));
            }
            catch (Exception e)
            {
                DlkLogger.LogError(e);
                throw new Exception("ScrollGridUpOrDown() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}

