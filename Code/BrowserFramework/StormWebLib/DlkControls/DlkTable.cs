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
using StormWebLib.System;

namespace StormWebLib.DlkControls
{
    /// <summary>
    /// Navigator class for tables
    /// </summary>
    [ControlType("Table")]
    public class DlkTable : DlkBaseTable
    {
        #region PRIVATE VARIABLES
        private Table TableData;
        private TableControl table;

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
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            CreateTable();
            table = new TableControl(mElement);
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

        #region CLASSES
        public class Table
        {
            #region FIELDS
            public String mClass;
            public Dictionary<string, int> Columns;
            private IWebElement mTableElement;
            bool IsCurrentStormWeb = DlkEnvironment.mProductFolder.ToLower().Equals("stormweb") ? true : false;
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
                var xpath_RowToolsItems = ".//div[contains(@class, 'check-all')]";
                var xpath_columnHdr = ".//th[not(contains(@style,'display: none'))][not(contains(@class,'hidden'))]//div[contains(@class,'columnHdr')]";
                var xpath_parent_gridHdr = "./preceding-sibling::div[contains(@class,'gridHdr')]";

                if (Columns == null)
                {
                    Columns = new Dictionary<string, int>();

                    IList<IWebElement> mHeaders = new List<IWebElement>();

                    if (mTableElement.GetAttribute("class").Contains("rowTools"))
                    {
                        IWebElement mParentGrid = mTableElement.FindElement(By.XPath(xpath_parent_gridHdr));
                        if (mParentGrid.FindElements(By.XPath(xpath_RowToolsItems)).Count > 0)
                        {
                            mHeaders = mParentGrid.FindElements(By.XPath(xpath_RowToolsItems));
                        }
                    }
                    else if (mTableElement.FindElements(By.CssSelector("span.columnHdr")).Count() > 0)
                    {
                        mHeaders = mTableElement.FindElements(By.CssSelector("span.columnHdr"));
                    }
                    else if (mTableElement.FindElements(By.XPath(xpath_columnHdr)).Count() > 0)
                    {
                        mHeaders = mTableElement.FindElements(By.XPath(xpath_columnHdr));

                    }
                    else if (mTableElement.FindElements(By.XPath(xpath_parent_gridHdr)).Count > 0)
                    {
                        IWebElement mPrevGrid = mTableElement.FindElement(By.XPath(xpath_parent_gridHdr));
                        mHeaders = mPrevGrid.FindElements(By.XPath(xpath_columnHdr));
                    }
                    else
                    {
                        mHeaders = mTableElement.FindElements(By.CssSelector("th"));
                    }

                    //var mValidHeaders = mHeaders.Where(x => new DlkBaseControl("Header", x).GetValue().ToLower().Trim().Replace("\r\n", " ") != "");

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

            }

            private int GetColumnIndexByCellName(string sColumnHeader)
            {
                Columns = new Dictionary<string, int>();
                //check table headers first before checking table data
                IList<IWebElement> mHeaders = GetColumnHeaderElements();
                if (mHeaders.Count == 0)
                {
                    mHeaders = mTableElement.FindElements(By.XPath(".//table[not(contains(@class,'rowTools'))]//tbody//td"))
                            .Where(x => x.GetCssValue("display").Trim().ToLower() != "none")
                            .ToList();
                }
                string sName = "";
                int ColumnHeaderLength = sColumnHeader.Length;

                for (int i = 0; i < mHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[i]);
                    if (!String.IsNullOrEmpty(ctlHeader.GetAttributeValue("name")))
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

            private void ResizeArray(ref string[] array, int expectedSize)
            {
                while (array.Length < expectedSize)
                {
                    Array.Resize(ref array, array.Length + 1);
                    array[array.Length - 1] = "";
                }
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
                    IsLoadingScreenIsDisplayed(delay, i);
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
                    IsLoadingScreenIsDisplayed(delay, i);
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
            private void IsLoadingScreenIsDisplayed(string delay, int pressCount)
            {
                try
                {
                    IWebElement spinner;
                    double waitSeconds;
                    double.TryParse(delay, out waitSeconds);
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
                            //wait.Until(OpenQA.Selenium.Support.UI.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//*[text()='Getting more...']/../self::*[@style='display: block;']")));
                            // alternative for InvisibilityOfElementLocated() rather than installing new NuGet package
                            wait.Until(condition =>
                            {
                                try
                                {
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

            private IList<IWebElement> GetRowsInColumn(string ColumnHeader)
            {
                IList<IWebElement> rows = GetRows();
                IList<IWebElement> rowsFiltered = new List<IWebElement>();
                foreach (IWebElement row in rows)
                {
                    IList<IWebElement> columns = row.FindElements(By.TagName("td"));
                    foreach (IWebElement column in columns)
                    {
                        if (column.GetAttribute("name").ToLower().Trim() == ColumnHeader.ToLower().Trim())
                        {
                            rowsFiltered.Add(column);
                            break;
                        }
                    }
                }

                return rowsFiltered;
            }

            private string GetTextColValue(IWebElement Cell)
            {
                IWebElement mTextElm;

                try
                {
                    if (Cell.FindElements(By.CssSelector("input")).Count > 0)
                    {
                        mTextElm = Cell.FindElement(By.CssSelector("input"));
                    }
                    else if (Cell.FindElements(By.CssSelector("div.htmlInput")).Count > 0)
                    {
                        mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput"));
                    }
                    else
                    {
                        mTextElm = Cell;
                    }
                }
                catch
                {
                    throw new Exception("Cell not supported.");
                }
                DlkTextBox txtCell = new DlkTextBox("TextBox", mTextElm);
                return txtCell.GetValue();
            }

            private string GetLabelColValue(IWebElement Cell)
            {
                DlkLabel txtCell = new DlkLabel("Label", Cell);
                String ActValue = Regex.Replace(txtCell.GetValue(), @"<[^>]+>|&nbsp;", "").Trim();
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
                    try
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
                    catch
                    {
                        //get the text value of current cell
                        mTextElm = Cell;
                    }
                }

                txtCell = new DlkTextBox("TextBox", mTextElm);
                txtCell.VerifyText(ExpectedValue.Trim());

            }

            private void VerifyLabelColValue(IWebElement Cell, string ExpectedValue)
            {
                //DlkLabel txtCell = new DlkLabel("TextBox", Cell);
                //txtCell.VerifyText(ExpectedValue.Trim());

                string ActualValue = "";
                if (!Cell.Displayed)
                {
                    DlkBaseControl cell = new DlkBaseControl("Label", Cell);
                    cell.MouseOver();
                }

                if (Cell.GetAttribute("class").ToLower().Contains("gridinput"))
                {
                    ActualValue = new DlkBaseControl("Text", Cell).GetValue();
                }
                else if (Cell.GetAttribute("class").Contains("edit") && Cell.FindElements(By.TagName("input")).Count > 0)
                {
                    ActualValue = new DlkBaseControl("Text", Cell.FindElement(By.TagName("input"))).GetValue();
                }
                else
                {
                    ActualValue = Cell.Text.Trim();
                    if (ActualValue.Contains("\r\n"))
                    {
                        ActualValue = ActualValue.Replace("\r\n", "<br>");
                    }
                }
                DlkAssert.AssertEqual("VerifyLabelColValue : ", ExpectedValue, ActualValue);
            }

            private void VerifyImageColValue(IWebElement Cell, string ExpectedValue)
            {
                if (!Cell.Displayed)
                {
                    DlkBaseControl cell = new DlkBaseControl("Image", Cell);
                    cell.MouseOver();
                }

                IWebElement image = Cell.FindElements(By.XPath("./descendant::*[contains(@style,'image')]")).Count > 0 ?
                    Cell.FindElement(By.XPath(".//*[contains(@style,'image')]")) :
                    Cell.FindElement(By.XPath(".//*[contains(@class,'icon')]"));
                string ActualValue = image.GetAttribute("title");
                DlkAssert.AssertEqual("VerifyImageColValue : ", ExpectedValue, ActualValue);
            }

            private void VerifyTextColReadOnly(IWebElement Cell, string TrueOrFalse)
            {
                IWebElement mTextElm;
                try { mTextElm = Cell.FindElement(By.CssSelector("input")); }
                catch { mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput")); }
                DlkTextBox txtCell = new DlkTextBox("TextBox", mTextElm);
                txtCell.VerifyReadOnly(TrueOrFalse);
            }

            private void VerifyTextEditorReadOnly(IWebElement Cell, string TrueOrFalse)
            {
                IWebElement mEditorElm;
                try { mEditorElm = Cell.FindElement(By.XPath("./descendant::div[@class='text-editor-field']")); }
                catch { mEditorElm = Cell.FindElement(By.XPath("./descendant::div[contains(@class,'input-element html-input string_input')]")); }
                string ReadOnly = "true";
                if (mEditorElm.GetAttribute("contenteditable").ToLower() == "true")
                {
                    ReadOnly = "false";
                }
                DlkAssert.AssertEqual("VerifyReadOnly()", TrueOrFalse.ToLower(), ReadOnly.ToLower());
            }

            private void SetAndEnterTextColValue(IWebElement Cell, string Value)
            {
                IWebElement mTextElm;
                try { mTextElm = Cell.FindElement(By.CssSelector("input")); }
                catch { mTextElm = Cell.FindElement(By.CssSelector("div.htmlInput")); }

                //Clear the initial contents of the cell
                //In few instances, mElement.Clear() disables the control
                ClearField(mTextElm);

                //Check if ClearField is successful. If not, execute mElement.Clear
                if (!String.IsNullOrEmpty(mTextElm.GetAttribute("data-focus-value")) ||
                    !String.IsNullOrEmpty(mTextElm.GetAttribute("value")) ||
                    !String.IsNullOrEmpty(mTextElm.Text))
                {
                    mTextElm.Clear();

                    //If cell still contains values after clear, Select all values using Ctrl+A keys then send input
                    if (!String.IsNullOrEmpty(mTextElm.GetAttribute("data-focus-value")) ||
                        !String.IsNullOrEmpty(mTextElm.GetAttribute("value")) ||
                        !String.IsNullOrEmpty(mTextElm.Text))
                    {
                        char a = '\u0001'; //ASCII code for Ctrl + A
                        Actions actions = new Actions(DlkEnvironment.AutoDriver);
                        actions.SendKeys(mTextElm, Convert.ToString(a)).Perform();
                    }
                }

                mTextElm.SendKeys(Value);
                Thread.Sleep(3000);
                mTextElm.SendKeys(Keys.Enter);

                DlkLogger.LogInfo("Successfully executed SetAndEnterCellValue()");
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
                if (String.IsNullOrEmpty(Value))
                    cboCell.ClearField();
                else
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
                IWebElement mCheckBoxElm;
                try
                {
                    mCheckBoxElm = Cell.FindElement(By.CssSelector("span.gridCheckbox"));
                }
                catch
                {
                    mCheckBoxElm = Cell.FindElement(By.CssSelector("*.checkbox"));
                }
                DlkCheckBox chkCell = new DlkCheckBox("CheckBox", mCheckBoxElm);
                chkCell.VerifyValue(ExpectedValue);
            }

            private void VerifyCheckBoxColReadOnly(IWebElement Cell, string TrueOrFalse)
            {
                IWebElement mCheckBoxElm = Cell.FindElement(By.CssSelector("span.gridCheckbox"));
                DlkCheckBox chkCell = new DlkCheckBox("CheckBox", mCheckBoxElm);
                chkCell.VerifyReadOnly(TrueOrFalse);
            }

            private void VerifyRadioButtonColValue(IWebElement Cell, string ExpectedValue)
            {
                try
                {
                    IList<IWebElement> mRadioButtonElmList = Cell.FindElements(By.CssSelector("input.radio-group-field")).Where(item => item.Displayed).ToList();
                    if (mRadioButtonElmList.Count > 0)
                    {
                        foreach (IWebElement mRadioButtonElm in mRadioButtonElmList)
                        {
                            string ActualValue;
                            Boolean rdBtnCellState;
                            try
                            {
                                DlkRadioButton rdbtnCell = new DlkRadioButton("RadioButton", mRadioButtonElm);
                                rdBtnCellState = rdbtnCell.GetState();
                                if (rdBtnCellState.ToString().ToLower().Equals("true"))
                                {
                                    IWebElement mRadioButtonLbl = mRadioButtonElm.FindElement(By.XPath(".//following-sibling::span[@class='radio-label']"));
                                    ActualValue = mRadioButtonLbl.Text;
                                    DlkAssert.AssertEqual("VerifyCellValue(): ", ExpectedValue, ActualValue);
                                    break;
                                }
                            }
                            catch
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Radio button not found. ");
                    }

                }
                catch (Exception e)
                {
                    throw new Exception("VerifyCellValue() failed: " + e.Message);
                }
            }

            private void SetCheckBoxColValue(IWebElement Cell, string Value)
            {
                IWebElement mCheckBoxElm;
                try
                {
                    mCheckBoxElm = Cell.FindElement(By.CssSelector("span.gridCheckbox"));
                }
                catch
                {
                    mCheckBoxElm = Cell.FindElement(By.CssSelector("span.checkbox"));
                }
                DlkCheckBox chkCell = new DlkCheckBox("CheckBox", mCheckBoxElm);
                chkCell.ScrollIntoViewUsingJavaScript(true);
                chkCell.Set(Value);
            }

            private void SetRadioButtonColValue(IWebElement Cell, String Value)
            {
                try
                {
                    IList<IWebElement> mRadioButtonLbl = Cell.FindElements(By.XPath(".//span[contains(@class,'radio-label')][contains(.,'" + Value + "')]"));
                    if (mRadioButtonLbl.Count > 0)
                    {
                        IWebElement mRadioButtonElm = mRadioButtonLbl[0].FindElement(By.XPath(".//preceding-sibling::input[@class='radio-group-field']"));
                        DlkRadioButton rdbtnCell = new DlkRadioButton("RadioButton", mRadioButtonElm);
                        rdbtnCell.ScrollIntoViewUsingJavaScript(true);
                        rdbtnCell.Click();
                    }
                    else
                    {
                        throw new Exception("Value to set on radio button is incorrect or radio button has not been found.");
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("SetCellValue() failed: " + e.Message);
                }
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

            /// <summary>
            /// Used for cell of treeCol type
            /// </summary>
            /// <param name="Cell"></param>
            private void VerifyTreeColValue(IWebElement Cell, string ExpectedValue)
            {
                IWebElement treeValue = Cell.FindElement(By.XPath(".//div[contains(@class,'treeValue')]"));
                string ActualValue = treeValue.Text.Trim();
                if (ActualValue == String.Empty) // if HierarchyTable row is selected
                {
                    if (Cell.FindElements(By.TagName("input")).Count > 0)
                    {
                        IWebElement selectedTreeValue = Cell.FindElement(By.TagName("input"));
                        ActualValue = selectedTreeValue.GetAttribute("value");
                    }
                }
                if (ActualValue.Contains("\r\n"))
                {
                    ActualValue = ActualValue.Replace("\r\n", "<br>");
                }

                DlkAssert.AssertEqual("VerifyTreeColValue()", ExpectedValue, ActualValue);
            }


            /// <summary>
            /// Used for cell of html type
            /// </summary>
            /// <param name="Cell"></param>
            private void VerifyHtmlColValue(IWebElement Cell, string ExpectedValue)
            {
                IWebElement htmlValue;
                string ActualValue = "";

                if (Cell.FindElements(By.XPath(".//*")).Count > 0)
                {
                    htmlValue = Cell.FindElement(By.XPath(".//*"));
                    ActualValue = htmlValue.Text.Trim();
                }
                else
                {
                    ActualValue = Cell.GetAttribute("textContent");
                }

                if (ActualValue.Contains("\r\n"))
                {
                    ActualValue = ActualValue.Replace("\r\n", "<br>");
                }
                DlkAssert.AssertEqual("VerifyHtmlColValue()", ExpectedValue.Trim(), ActualValue.Trim());
            }

            /// <summary>
            /// Used for Textboxes where using Clear() would disable the control
            /// </summary>
            /// <param name="Cell"></param>
            /// <param name="Value"></param>
            private void SetTextWithoutClear(IWebElement Cell, string Value)
            {
                IWebElement mInput = Cell.FindElement(By.TagName("input"));
                mInput.SendKeys(Keys.Control + "a");
                DlkLogger.LogInfo("Highlighting current text..");
                Thread.Sleep(5000);// add a delay because it loads after focusing. it seems we cannot delete the current text while it is loading/searching for records. so we wait
                DlkLogger.LogInfo("Overwriting current text using new text: " + Value);
                mInput.SendKeys(Keys.Control + "a");
                mInput.SendKeys(Value);
                Thread.Sleep(5000);// add a delay because it loads after focusing.
            }

            /// <summary>
            /// Uses GetColumnIndexFromHeader or GetColumnIndexByCellName to determine the index of the column with the given ColumnHeader
            /// </summary>
            /// <param name="sColumnHeader"></param>
            /// <returns></returns>
            public int GetColumnIndex(string sColumnHeader)
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

                return iCol;
            }

            /// <summary>
            /// Alternative way to clear a field without Clear() 
            /// </summary>
            /// <param name="Field"></param>
            private void ClearField(IWebElement Field)
            {
                char a = '\u0001'; //ASCII code for Ctrl + A
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                actions.SendKeys(Field, Convert.ToString(a)).Perform();

                Thread.Sleep(1000);
                Field.SendKeys(Keys.Backspace);
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

            public void TryClickElement(DlkBaseControl bElement)
            {
                try
                {
                    bElement.ClickUsingJavaScript();
                }
               catch 
                {
                    //Some rows and table cells that contains child rows cannot be clicked using offsets 0,0 but expands when using offsets 5,5.
                    //Use Click Offset with offsets 1,1 instead. Observe if no adverse effects. 
                    if (bElement.GetAttributeValue("class").Contains("gotChildren") || bElement.GetAttributeValue("class").Contains("hasChildren")
                        && !bElement.GetAttributeValue("class").Contains("childRow"))
                    {
                        bElement.Click(0, 0);
                    }
                    else if (bElement.GetAttributeValue("class").Contains("treeCol") && !bElement.GetParent().GetAttribute("class").Contains("childRow"))
                    {
                        bElement.Click(1, 1);
                    }
                    else if (bElement.GetAttributeValue("class") ==""|| bElement.GetAttributeValue("class") == "newRow")
                    {
                        bElement.Click();
                    }
                    else
                    {
                        bElement.Click(5, 5); 
                    }
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
                else if (sClass.Contains("textCol") || sClass.Contains("text_field"))
                {
                    return GetTextColValue(mCell.mElement);
                }
                else if (sClass.Contains("html"))
                {
                    return GetTextColValue(mCell.mElement);
                }

                else if (sClass.Contains("lookup"))
                {
                    return GetTextColValue(mCell.mElement);
                }

                else if (sClass.Contains("ddwnCol"))
                {
                    return GetDDwnColValue(mCell.mElement);
                }
                else if (sClass.Contains("checkbox"))
                {
                    return GetCheckBoxColValue(mCell.mElement);
                }
                else if (sClass.Contains("buttonCol"))
                {
                    return GetTextColValue(mCell.mElement);
                }
                else if (sClass.Contains("treeCol") || sClass.Contains("right"))
                {
                    if (mCellElm.FindElements(By.TagName("input")).Count > 0)
                        return GetTextColValue(mCell.mElement);
                    else
                        return GetLabelColValue(mCell.mElement);
                }
                else if (sClass.Contains("Col"))
                {
                    return GetTextColValue(mCell.mElement);
                }
                else if (sClass.Contains("edit"))
                {
                    if (mCellElm.FindElements(By.TagName("input")).Count > 0)
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
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);

                if (mCellElm == null)
                    throw new Exception("SetCellValue() failed. Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                string sClass = mCell.GetAttributeValue("class");
                mCell.ScrollIntoViewUsingJavaScript(true);
                if (sClass != null && sClass.Contains("checkbox"))
                {
                    //Doing a click on checkbox alters the value and may affect other controls
                    SetCheckBoxColValue(mCell.mElement, sValue);
                }
                else
                {
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
                    else if (sClass.Contains("textCol") || sClass.Contains("htmlCol") || sClass.Contains("lookupCol") || sClass.Contains("emailCol"))
                    {
                        SetTextColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("ddwnCol") || sClass.Contains("popupmenu") || sClass.Contains("core_dropdown_field") && !sClass.Contains("core-component"))
                    {
                        SetDDwnColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("core_dropdown_field") && sClass.Contains("core-component"))
                    {
                        SetDDwnColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("object-"))
                    {
                        SetSelectColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("lookup2"))
                    {
                        SetTextWithoutClear(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("core_radio_group_field"))
                    {
                        SetRadioButtonColValue(mCell.mElement, sValue);
                    }
                    else if (sClass.Contains("core-component") && !sClass.Contains("edit"))
                    {
                        SetTextColValue(mCellElm, sValue);
                    }
                    else if (sClass.Contains("edit"))
                    {
                        DlkBaseControl input = new DlkBaseControl("Input", mCell, "xpath", ".//input");
                        if (!input.Exists())
                        {
                            var inputControl = mCellElm.FindElements(By.XPath(".//div[contains(@class,'text-editor')]")).Where(control => control.Displayed).Count() > 0
                                ? mCellElm.FindElement(By.XPath(".//div[contains(@class,'text-editor')]"))
                                : null;
                            // if inputControl is not text-editor
                            if (inputControl == null)
                            {
                                inputControl = mCellElm.FindElements(By.XPath(".//*[contains(@class,'input')]")).Where(control => control.Displayed).Count() > 0
                               ? mCellElm.FindElement(By.XPath(".//*[contains(@class,'input')]"))
                               : null;
                            }

                            input = new DlkBaseControl("Textarea in the table", inputControl);
                        }
                        if (input.GetAttributeValue("class").Contains("ddwn-input") ||
                            mCellElm.FindElements(By.XPath(".//*[contains(@class, 'ddwnArrow')]")).Count > 0)
                        {
                            SetDDwnColValue(mCell.mElement, sValue);
                        }
                        else if (input.GetAttributeValue("class").Contains("search-input"))
                        {
                            SetTextWithoutClear(mCell.mElement, sValue);
                        }
                        else if (input.GetAttributeValue("class").Contains("text-editor"))
                        {
                            SetTextWithEditButton(mCell.mElement, sValue);
                        }
                        //In hours grid in Timesheets, the cell retrieved by GetCell() is underneath a popup form. The popup form has the cell that accepts value from the UI.
                        else if (mTableElement.FindElements(By.XPath("//*[@id='popupForm']")).Count > 0)
                        {
                            SetTextColValue(mTableElement.FindElement(By.XPath("//*[@id='popupForm']//div[@id='cellBox']")), sValue);
                        }
                        else
                        {
                            SetTextColValue(mCell.mElement, sValue);
                        }
                    }
                }

                ClickBanner();
            }

            private void SetTextWithEditButton(IWebElement webElement, string sValue)
            {
                var input = webElement.FindElement(By.XPath(".//div[@contenteditable='true']"));
                input.Clear();
                input.SendKeys(sValue);
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
                mCell.ScrollIntoViewUsingJavaScript(true);
                TryClickElement(mCell);

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
                    //In hours grid in Timesheets, the cell retrieved by GetCell() is underneath a popup form. The popup form has the cell that accepts value from the UI.
                    if (mTableElement.FindElements(By.XPath("//*[@id='popupForm']")).Count > 0)
                    {
                        SetAndEnterTextColValue(mTableElement.FindElement(By.XPath("//*[@id='popupForm']//div[@id='cellBox']")), sValue);
                    }
                    else
                    {
                        SetAndEnterTextColValue(mCell.mElement, sValue);
                    }
                }
                else
                {
                    throw new Exception("Cell is unsupported.");
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
                mCell.ScrollIntoViewUsingJavaScript(true);
                string sClass = mCell.GetAttributeValue("class");
                if (sClass.Contains("buttonCol"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("textCol"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("lookup1") && sClass.Contains("ddwnCol"))
                {
                    VerifyComboBoxInTableCellValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("ddwnCol"))
                {
                    VerifyDDwnColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("core_dropdown"))
                {
                    IWebElement mDdwn = mCell.mElement.FindElements(By.XPath(".//div[contains(@class, 'dropdown-field-container')]")).Count > 0 ?
                        mCell.mElement.FindElement(By.XPath(".//div[contains(@class, 'dropdown-field-container')]")) : mCell.mElement;
                    VerifyDDwnColValue(mDdwn, sExpectedValue);
                }
                else if (sClass.Contains("checkbox") || sClass.Contains("selectboxCol"))
                {
                    VerifyCheckBoxColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("core_radio_group_field"))
                {
                    VerifyRadioButtonColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("lookupCol"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("null"))  //Reporting table in ngCRM
                {
                    VerifyLabelColValue(mCell.mElement, sExpectedValue);
                }
                else if (mCellElm.FindElements(By.CssSelector("div.attribute-icon")).Count > 0)
                {
                    VerifyImageColValue(mCell.mElement, sExpectedValue);
                }
                else if ((sClass.Contains("right")) || (sClass == ""))//table ngRP
                {
                    VerifyLabelColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("treeCol"))
                {
                    VerifyTreeColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("cell-html"))
                {
                    VerifyHtmlColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("wrap-text"))
                {
                    VerifyHtmlColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("infoBubble"))
                {
                    VerifyLabelColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("cellImage"))
                {
                    VerifyImageColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("emailCol"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("edit"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else if (sClass.Contains("Col"))
                {
                    VerifyTextColValue(mCell.mElement, sExpectedValue);
                }
                else
                {
                    VerifyLabelColValue(mCell.mElement, sExpectedValue);
                }
            }

            public void VerifyCellButtonDisplayed(int iRow, string sColumnHeader, string ExpectedValue)
            {
                bool actualValue = false, expectedValue = Convert.ToBoolean(ExpectedValue);

                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("VerifyCellButtonDisplayed() failed. Invalid row.");
                if (mCellElm.FindElements(By.XPath(".//span[contains(@class,'icon-recordarrow-left')]")).Count > 0 || mCellElm.FindElements(By.XPath(".//div[contains(@class,'icon')]")).Count > 0) // for indicators and icons
                {
                    actualValue = true;
                }
                else if (mCellElm.FindElements(By.XPath(".//input[contains(@class,'input-element')]")).Count > 0) // for clicked indicators, which replaces button with textbox
                {
                    actualValue = false;
                }
                else if (mCellElm.GetAttribute("class").Contains("Notes") || mCellElm.GetAttribute("name").Contains("NotesIcon"))
                {
                    if (mCellElm.GetAttribute("class").Contains("hasNotes"))
                    {
                        actualValue = true;
                    }
                    else
                    {
                        string buttonClass = mCellElm.FindElement(By.TagName("button")).GetAttribute("class");
                        string rowClass = mCellElm.FindElement(By.XPath(".//parent::tr")).GetAttribute("class");
                        if (!String.IsNullOrEmpty(buttonClass) && (rowClass.Contains("selected") || rowClass.Contains("hover")))
                        {
                            actualValue = true;
                        }
                    }
                }
                else if (mCellElm.FindElements(By.TagName("button")).Count > 0)
                {
                    IWebElement cellImage = mCellElm.FindElement(By.TagName("button"));
                    new DlkBaseControl("Cell", mCellElm).MouseOver();
                    string cellStyle = cellImage.GetAttribute("style");
                    if (cellStyle.Contains("background-position"))
                    {
                        string[] backgroundPos = cellStyle.Substring(cellStyle.IndexOf("background-position")).Split(new char[] { ':', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

                        if (backgroundPos[1] == "0px")
                            actualValue = false;
                        else
                            actualValue = true;
                    }
                    else if (cellStyle.Contains("display: none"))
                    {
                        actualValue = false;
                    }
                    else
                    {
                        actualValue = cellImage.Displayed;
                    }
                }
                else
                {
                    DlkLogger.LogInfo("Unable to find image/button.");
                    actualValue = false;
                }
                DlkAssert.AssertEqual("VerifyCellButtonDisplayed() : ", expectedValue, actualValue);
            }

            private void VerifyComboBoxInTableCellValue(IWebElement Cell, string ExpectedValue)
            {
                string ActualValue = "";
                var input = Cell.FindElement(By.XPath(".//input"));
                if (!input.Displayed)
                {
                    DlkBaseControl cell = new DlkBaseControl("Lookup1 tex", input);
                    cell.MouseOver();
                }

                if (input.GetAttribute("class").Equals("right edit"))
                {
                    ActualValue = new DlkBaseControl("Text", input.FindElement(By.XPath("./input"))).GetValue();
                }
                else if (input.GetAttribute("class").ToLower().Contains("gridinput"))
                {
                    ActualValue = new DlkBaseControl("Text", input).GetValue();
                }
                else
                {
                    ActualValue = input.Text.Trim();
                    if (ActualValue.Contains("\r\n"))
                    {
                        ActualValue = ActualValue.Replace("\r\n", "<br>");
                    }
                }
                DlkAssert.AssertEqual("VerifyLabelColValue : ", ExpectedValue, ActualValue);
            }

            public void VerifyCellValueContains(int iRow, string sColumnHeader, string sExpectedValue)
            {
                string cellValue = GetCellValue(iRow, sColumnHeader).ToLower();
                DlkAssert.AssertEqual("VerifyCellValueContains()", true, cellValue.Contains(sExpectedValue.ToLower()));
                DlkLogger.LogInfo("VerifyCellValueContains() successfully executed.");
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
                else if (sClass.Contains("cell-html"))
                {
                    VerifyTextEditorReadOnly(mCell.mElement, TrueOrFalse);
                }
                else if (sClass.Contains("Col"))
                {
                    VerifyTextColReadOnly(mCell.mElement, TrueOrFalse);
                }
            }

            public int GetRowWithColumnValue(string sColumnHeader, string sValue)
            {
                int i = 1;
                string cellValue = "";
                try
                {
                    while (cellValue != null)
                    {
                        cellValue = GetCellValue(i, sColumnHeader).Replace("\r\n", "");
                        if (cellValue == sValue)
                        {
                            return i;
                        }

                        i++;
                    }
                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            public int GetRowWithColumnValueContains(string sColumnHeader, string sValue)
            {
                int i = 1;
                if (sValue == "")
                {
                    sValue = null;
                }
                string cellValue = GetCellValue(i, sColumnHeader);
                try
                {
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
                catch (Exception)
                {
                    return -1;
                }
            }

            public void ClickRowButton(int iRow, string sButtonCaption)
            {
                DlkBaseControl ctlRowButton, ctlRow;
                IWebElement mRow = GetRow(iRow);
                ctlRow = new DlkBaseControl("Row", mRow);
                ctlRow.MouseOver();
                try
                {
                    IWebElement mRowButton = null;

                    if (mRow.FindElements(By.XPath(".//div[@class='gridImage'][contains(@style,'" + sButtonCaption + "')]")).Count > 0)
                    {
                        mRowButton = mRow.FindElement(By.XPath(".//div[@class='gridImage'][contains(@style,'" + sButtonCaption + "')]"));
                    }
                    else
                    {
                        mRowButton = mRow.FindElement(By.CssSelector("[title='" + sButtonCaption + "']"));
                        if (mRowButton.GetAttribute("class").ToLower().Contains("show-results"))
                            mRowButton = mRowButton.FindElement(By.XPath("./following-sibling::*[contains(@class,'tap-target')]"));
                    }
                    ctlRowButton = new DlkBaseControl("Row Button", mRowButton);
                    ctlRowButton.MouseOver();

                    if (mRowButton.GetAttribute("class").Contains("icons") || mRowButton.GetAttribute("class").Contains("gridInputBttn") || mRowButton.GetAttribute("class").Contains("clickable"))
                    {
                        TryClickElement(ctlRowButton);
                    }
                    else
                    {
                        ctlRowButton.Click();
                    }
                }
                catch
                {
                    throw new Exception("ClickRowButton() failed. Row button '" + sButtonCaption + "' not found.");
                }

            }


            public void VerifyRowButton(int iRow, string sButtonCaption, string TrueOrFalse)
            {


                Boolean bRowButtonExists = false;
                IWebElement mRow = GetRow(iRow);
                IWebElement mRowButton = null;


                try
                {
                    if ((mRow.FindElements(By.CssSelector("div.gridImage[title='" + sButtonCaption + "']")).Count > 0)
                       || (mRow.FindElements(By.XPath(".//button[@title='" + sButtonCaption + "']")).Count > 0))
                    {
                        //Verify if the button is displayed or not. If the button is not displayed, it will set to false.
                        mRowButton = mRow.FindElement(By.CssSelector("[title='" + sButtonCaption + "']"));
                        DlkBaseControl rowButton = new DlkBaseControl("Row Button", mRowButton);
                        rowButton.MouseOver();
                        if (mRowButton.Displayed)
                        {
                            bRowButtonExists = true;
                            DlkLogger.LogInfo("VerifyRowButton(): Row button found. Caption: [" + sButtonCaption + "]");
                        }

                        else
                        {
                            DlkLogger.LogInfo("VerifyRowButton(): Row button not found. Caption: [" + sButtonCaption + "]");
                        }

                    }
                    else
                    {
                        bRowButtonExists = false;
                        DlkLogger.LogInfo("VerifyRowButton(): Row button not found. Caption: [" + sButtonCaption + "]");
                    }
                    DlkAssert.AssertEqual("VerifyRowButton", Convert.ToBoolean(TrueOrFalse), bRowButtonExists);
                }
                catch (Exception ex)
                {
                    throw new Exception("VerifyRowButton() failed.", ex);
                }
            }

            public void ClickCellButton(int iRow, string sColumnHeader, string sButtonCaption)
            {
                try
                {
                    DlkBaseControl ctlCellButton;
                    IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                    DlkBaseControl ctlCell = new DlkBaseControl("Cell", mCellElm);
                    ctlCell.ScrollIntoViewUsingJavaScript(true);
                    ctlCell.MouseOver();
                    try
                    {
                        // IWebElement mCellButton = mCellElm.FindElement(By.Name(sButtonCaption));
                        IWebElement mCellButton = null;
                        if (mCellElm.FindElements(By.TagName("button")).Where(item => !item.GetAttribute("style").Contains("display: none")).Count() > 0)
                        {
                            mCellButton = mCellElm.FindElement(By.TagName("button"));
                        }
                        else if (mCellElm.FindElements(By.XPath(String.Format(".//*[@class='{0}']", sButtonCaption))).Where(item => item.Displayed).ToList().Count() > 0)
                        {
                            // find any element with matching class..
                            mCellButton = mCellElm.FindElement(By.XPath(String.Format(".//*[@class='{0}']", sButtonCaption)));
                        }
                        else if (mCellElm.FindElements(By.XPath(String.Format(".//*[@title='{0}']", sButtonCaption))).Where(item => item.Displayed).ToList().Count() > 0)
                        {
                            // find element with matching title
                            mCellButton = mCellElm.FindElement(By.XPath(String.Format(".//*[@title='{0}']", sButtonCaption)));
                        }
                        else
                        {
                            throw new NoSuchElementException();
                        }

                        ctlCellButton = new DlkBaseControl("Cell Button", mCellButton);
                        if (mCellButton.GetAttribute("class").Contains("icons") || mCellButton.GetAttribute("class").Contains("gridAddBttn") || mCellButton.GetAttribute("class").Contains("clickable"))
                        {
                            TryClickElement(ctlCellButton);
                        }
                        else
                        {
                            ctlCellButton.Click();
                        }

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
                catch (Exception ex)
                {
                    throw new Exception("ClickCellButton() failed. ", ex);
                }
            }

            public void VerifyCellButtonTooltip(int iRow, string sColumnHeader, string sButtonCaption)
            {
                try
                {
                    string ActualValue;
                    DlkBaseControl ctlCellButton;
                    IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                    DlkBaseControl ctlCell = new DlkBaseControl("Cell", mCellElm);
                    ctlCell.ScrollIntoViewUsingJavaScript(true);

                    IWebElement mCellButton;

                    if (mCellElm.FindElements(By.CssSelector("div.gridImage")).Count > 0)
                    {
                        mCellButton = mCellElm.FindElement(By.ClassName("gridImage"));
                    }
                    else if (mCellElm.FindElements(By.XPath("./following-sibling::td//div[@class='gridImage']")).Count > 0)
                    {
                        mCellButton = mCellElm.FindElement(By.XPath("./following-sibling::td//div[@class='gridImage']"));
                    }
                    else if (mCellElm.FindElements(By.XPath(".//*[contains(@class,'icon')]")).Count > 0)
                    {
                        mCellButton = mCellElm.FindElement(By.XPath(".//*[contains(@class,'icon')]"));
                    }
                    else
                    {
                        mCellButton = mCellElm.FindElement(By.TagName("button"));
                    }

                    ctlCellButton = new DlkBaseControl("Cell Button", mCellButton);
                    ctlCellButton.MouseOverOffset(0, 0);
                    ActualValue = ctlCellButton.GetAttributeValue("title");

                    DlkAssert.AssertEqual("VerifyCellButtonTooltip", sButtonCaption, ActualValue);

                }
                catch (Exception ex)
                {
                    throw new Exception("VerifyCellButtonTooltip() failed. ", ex);
                }
            }

            public void ClickCellTooltip(int iRow, string sColumnHeader)
            {

                DlkBaseControl ctlCellButton;
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                DlkBaseControl ctlCell = new DlkBaseControl("Cell", mCellElm);
                ctlCell.ScrollIntoViewUsingJavaScript(true);
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
                ctlCell.ScrollIntoViewUsingJavaScript(true);
                try
                {
                    if (mCellElm.FindElements(By.XPath(".//*[contains(@class,'cellLink')][contains(.,\"" + sLinkText + "\")]")).Count > 0)
                    {
                        mCellLink = mCellElm.FindElement((By.XPath(".//*[contains(@class,'cellLink')][contains(.,\"" + sLinkText + "\")]")));
                    }
                    else
                    {
                        mCellLink = mCellElm.FindElement(By.LinkText(sLinkText));
                    }
                    ctlCellLink = new DlkBaseControl("Cell Link", mCellLink);
                    ctlCellLink.Click();
                }
                catch
                {
                    mCellLink = mCellElm.FindElement(By.XPath(".//*[contains(@class,'infoBubble') or contains(@class, 'info-bubble')][contains(.,\"" + sLinkText + "\")]"));
                    ctlCellLink = new DlkBaseControl("Cell Link", mCellLink);
                    ctlCellLink.ScrollIntoViewUsingJavaScript(true);
                    ctlCellLink.Click();
                }
            }


            public void ClickCellLinkByColumnNumber(String RowNumber, String ColumnNumber, String LinkText)
            {
                try
                {
                    int targetColumnNumber = Convert.ToInt32(ColumnNumber) + 1;
                    int targetRowNumber = Convert.ToInt32(RowNumber) + 1;
                    IWebElement targetCell = null;


                    targetCell = mTableElement.FindElement(By.XPath(".//tr[" + targetRowNumber + "]/td[" + targetColumnNumber + "]"));


                    DlkBaseControl ctlCellLink;
                    DlkBaseControl ctlCell = new DlkBaseControl("Cell", targetCell);
                    ctlCell.ScrollIntoViewUsingJavaScript(true);
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

                if (mCellElm.Displayed == false)
                    mCell.ScrollIntoViewUsingJavaScript(true);

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

            public void VerifyTextHyperLink(int iRow, string sColumnHeader, string sExpectedValue, bool sTrueOrFalse)
            {
                Boolean bLinkExists = false;
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);
                if (mCellElm == null)
                    throw new Exception("Cell not found.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                if (mCellElm.FindElements(By.XPath(".//a[not(contains(@style, 'display: none'))]")).Count > 0)
                {
                    bLinkExists = true;
                }
                else if (mCellElm.FindElements(By.XPath(".//*[contains(@class, 'infoBubble')][not(contains(@style, 'display: none'))]")).Count > 0)
                {
                    bLinkExists = true;
                }

                DlkLogger.LogInfo("Checking if text is hyperlink...");
                DlkAssert.AssertEqual("VerifyTextHyperLink() HyperLink exists:", bLinkExists, sTrueOrFalse);
                DlkLogger.LogInfo("Checking if text is matching...");
                DlkAssert.AssertEqual("VerifyTextHyperLink() Text comparison:", mCell.GetValue().ToLower().Trim(), sExpectedValue.ToLower().Trim());
            }

            public void SelectRow(int iRow)
            {
                IWebElement mRow = GetRow(iRow);
                DlkBaseControl ctlRow = new DlkBaseControl("Row", mRow);
                ctlRow.ScrollIntoViewUsingJavaScript(true);
                try
                {
                    TryClickElement(ctlRow);
                }
                catch
                {
                    throw new Exception("SelectRow() failed. Row '" + iRow + "' not found.");
                }
            }

            public IWebElement GetCell(int iRow, string sColumnHeader)
            {
                int iCol = GetColumnIndex(sColumnHeader);
                IWebElement mRow = GetRow(iRow);
                if (mRow == null)
                {
                    return null;
                }
                else
                {
                    // add handlers for other HTML elements in different kinds of rows when necessary
                    IList<IWebElement> cells = mRow.FindElements(By.CssSelector("td")).Count > 0 ? mRow.FindElements(By.CssSelector("td")).Where(x => x.GetCssValue("display").ToLower().Trim() != "none").ToList() : null;

                    return cells[iCol];
                }
            }


            public IWebElement GetRow(int iRow)
            {
                try
                {
                    if (mTableElement.FindElements(By.XPath(".//div[contains(@class, 'rowTools')]")).Count > 0)
                    {
                        //Filter RowTools tables because they don't have gridBody in their element.
                        if (!(mTableElement.GetAttribute("class").Contains("Cols")))
                            mTableElement = mTableElement.FindElement(By.XPath(".//div[contains(@class, 'gridBody')]"));
                    }

                    List<IWebElement> tableRows = mTableElement.FindElements(By.XPath(".//tbody/tr[not(contains(@class,'hide'))][not(contains(@class,'filtered'))]")).ToList();
                    if (mTableElement.FindElements(By.XPath(".//tfoot/tr[not(contains(@class, 'hide'))]")).Count > 0)
                    {
                        tableRows.AddRange(mTableElement.FindElements(By.XPath(".//tfoot/tr[not(contains(@class, 'hide'))]")));
                    }

                    if (tableRows.Count > 0)
                    {
                        //If table is a treeview, remove all rows that are not displayed
                        if (tableRows[0].FindElements(By.ClassName("treeCol")).Count > 0)
                        {
                            tableRows = tableRows.Where(x => !x.GetAttribute("style").Contains("display: none")).ToList();
                        }

                        return tableRows[iRow - 1];
                    }
                    else
                    {
                        throw new Exception("GetRow() : no rows found in the table.");
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

            public IList<IWebElement> GetColumnHeaderElements()
            {
                //check for th first since this is the one we prominently use for headers
                IList<IWebElement> mHeaders = mTableElement.FindElements(By.XPath(".//th[not(contains(@style,'display: none'))]"));

                //If none found, try span.columnHdr
                if (mHeaders.Count == 0)
                {
                    mHeaders = mTableElement.FindElements(By.CssSelector("span.columnHdr"));
                }

                return mHeaders;
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

                    if (!mHeaders[i].Displayed)
                    {
                        ctlHeader.ScrollIntoViewUsingJavaScript(true);
                        int cellNum = i + 1;
                        try
                        {
                            IWebElement ctlBody = mTableElement.FindElement(By.XPath(".//descendant::*[contains(@class, 'gridBody')]//td[" + cellNum.ToString() + "]"));
                            DlkBaseControl ctlCell = new DlkBaseControl("Cell", ctlBody);
                            ctlCell.ScrollIntoViewUsingJavaScript(true);
                        }
                        catch
                        {
                            // do nothing
                        }
                        Thread.Sleep(1000);
                        if (!mHeaders[i].Displayed) continue;
                    }

                    IWebElement titleElement = mHeaders[i].FindElements(By.ClassName("columnHdr")).Count > 0 ?
                        mHeaders[i].FindElement(By.ClassName("columnHdr")) : mHeaders[i];

                    if (String.Equals(ctlHeader.GetAttributeValue("name"), ColumnHeader, StringComparison.InvariantCultureIgnoreCase))
                    {
                        bFound = true;
                        break;
                    }
                    else if (String.Equals(titleElement.GetAttribute("title"), ColumnHeader, StringComparison.InvariantCultureIgnoreCase))
                    {
                        bFound = true;
                        break;
                    }
                    else
                    {
                        string currHeader = ctlHeader.GetValue().Replace("\r\n", " ").Trim();
                        if (currHeader.Equals(ColumnHeader, StringComparison.InvariantCultureIgnoreCase))
                        {
                            bFound = true;
                            break;
                        }
                    }
                }
                return bFound;
            }

            public void VerifyColumnHeaders(String Items, String Attribute, Boolean IsContains = false)
            {
                IList<IWebElement> columnHeaders = GetColumnHeaderElements().Where(x => !x.GetAttribute("style").Contains("display: none")).Where(x => !x.GetAttribute("class").Contains("fake-column")).Where(x => !x.GetAttribute("class").ToLower().Contains("scrollhdr")).ToList();
                List<String> lsExpectedResults = Items.Split('~').ToList();

                //If scripter provided less expected result items than actual result items, throw an error
                if (!lsExpectedResults.Count.Equals(columnHeaders.Count))
                {
                    throw new Exception("Expected header count [" + lsExpectedResults.Count + "] is not equal to actual header count [" + columnHeaders.Count + "]");
                }

                Action<IList<IWebElement>, List<String>, String> CompareByAttribute = (listItems, expectedItems, attribute) =>
                {
                    int i = 0;
                    foreach (IWebElement columnHeader in listItems)
                    {
                        string columnHeaderValue = string.Empty;
                        string expectedValue = expectedItems.ElementAt(i).ToLower().Trim();

                        if (!String.IsNullOrEmpty(columnHeader.GetAttribute(attribute)))
                        {
                            columnHeaderValue = columnHeader.GetAttribute(attribute).ToLower().Trim();
                        }
                        else
                        {
                            columnHeaderValue = columnHeader.FindElement(By.XPath(".//*[contains(@class,'columnHdr')]")).GetAttribute(attribute).ToLower().Trim();
                        }

                        if (IsContains)
                        {
                            DlkLogger.LogInfo("Comparing:  Column Value [" + columnHeaderValue + "] contains Expected Value [" + expectedValue + "] ");
                            string newValue = columnHeaderValue;
                            //If string has numbers, remove numbers from actual header value
                            if (DlkString.HasNumericChar(columnHeaderValue))
                            {
                                newValue = Regex.Replace(columnHeaderValue, @"([\d+/])+([ ]?)", string.Empty);
                            }
                            if (!newValue.Contains(expectedValue))
                            {
                                throw new Exception("Column Value [" + columnHeaderValue + "] did not contain Expected Value [" + expectedValue + "].");
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("Comparing: Expected Value [" + expectedValue + "] - Column Value [" + columnHeaderValue + "]");
                            if (!columnHeaderValue.Equals(expectedValue))
                            {
                                throw new Exception("Expected Value [" + expectedValue + "] is not equal to Column Value [" + columnHeaderValue + "]");
                            }
                        }

                        i++;
                    }
                };

                switch (Attribute.ToLower())
                {
                    case "name":
                        DlkLogger.LogInfo("Checking the name attribute of column header...");
                        CompareByAttribute(columnHeaders, lsExpectedResults, "name");
                        break;
                    case "title":
                        DlkLogger.LogInfo("Checking the title attribute of column header...");
                        CompareByAttribute(columnHeaders, lsExpectedResults, "title");
                        break;
                    case "textcontent":
                        DlkLogger.LogInfo("Checking the textContent attribute of column header...");
                        CompareByAttribute(columnHeaders, lsExpectedResults, "textContent");
                        break;
                    default:
                        throw new Exception("Unsupported attribute. Supported attributes include name, title, and textcontent only.");
                }
            }

            public void VerifyColumnHeaderContains(String ExpectedValue, bool TrueOrFalse)
            {
                IList<IWebElement> columnHeaders = GetColumnHeaderElements().Where(x => !x.GetAttribute("style").Contains("display: none")).Where(x => !x.GetAttribute("class").Contains("fake-column")).ToList();
                bool bFound = false;

                foreach (IWebElement columnHeader in columnHeaders)
                {
                    string columnHeaderValue = string.Empty;
                    if (!String.IsNullOrEmpty(columnHeader.GetAttribute("name")))
                    {
                        columnHeaderValue = columnHeader.GetAttribute("name");
                    }
                    else if (!String.IsNullOrEmpty(columnHeader.GetAttribute("title")))
                    {
                        columnHeaderValue = columnHeader.GetAttribute("title");
                    }
                    else
                    {
                        columnHeaderValue = columnHeader.GetAttribute("textContent");
                    }

                    if (!String.IsNullOrEmpty(columnHeaderValue))
                    {
                        columnHeaderValue = DlkString.RemoveCarriageReturn(columnHeaderValue).ToLower().Trim();
                    }

                    DlkLogger.LogInfo("Comparing: Expected Value [" + ExpectedValue + "] - Column Value [" + columnHeaderValue + "]");
                    if (columnHeaderValue.Contains(ExpectedValue.ToLower().Trim()))
                    {
                        DlkLogger.LogInfo("Column found.");
                        bFound = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyColumnValueContains", TrueOrFalse, bFound);
            }

            public void ClickColumnHeader(String ColumnHeader)
            {
                try
                {
                    IWebElement columnHeader = GetColumnHeader(ColumnHeader);
                    new DlkBaseControl("Header", columnHeader).ScrollIntoViewUsingJavaScript(true);
                    columnHeader.Click();
                    Thread.Sleep(2000);
                    DlkLogger.LogInfo("ClickColumnHeader(): Successfully clicked column header [" + ColumnHeader + "]");
                }
                catch (Exception ex)
                {
                    DlkLogger.LogInfo(ex.Message);
                    throw new Exception("ClickColumnHeader(): Failed to click the column header [" + ColumnHeader + "]", ex);
                }
            }

            public IWebElement GetColumnHeader(String ColumnHeaderValue)
            {
                IList<IWebElement> columnHeaders = GetColumnHeaderElements();
                IWebElement columnFound = null;
                bool bFound = false;
                string columnHeaderValue = "";

                int curRetry = 0;
                int retryLimit = 3;
                while (!bFound && (curRetry++ <= retryLimit))
                {
                    foreach (IWebElement columnHeader in columnHeaders)
                    {
                        if (!String.IsNullOrEmpty(columnHeader.GetAttribute("name")))
                        {
                            columnHeaderValue = columnHeader.GetAttribute("name");
                        }
                        else if (!String.IsNullOrEmpty(columnHeader.GetAttribute("title")))
                        {
                            columnHeaderValue = columnHeader.GetAttribute("title");
                        }
                        else
                        {
                            columnHeaderValue = columnHeader.GetAttribute("textContent");
                        }

                        if (columnHeaderValue == ColumnHeaderValue)
                        {
                            DlkLogger.LogInfo("Column found [" + columnHeaderValue + "]");
                            bFound = true;
                            columnFound = columnHeader;
                            break;
                        }
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Unable to find the column [" + ColumnHeaderValue + "]");
                }
                return columnFound;
            }

            public bool IsColumnRequired(String ColumnHeader)
            {
                bool bRequired = false;

                IList<IWebElement> mHeaders = GetColumnHeaderElements();

                IWebElement mHeader = mHeaders[GetColumnIndex(ColumnHeader)];
                if (mHeader.GetAttribute("class").Contains("required"))
                {
                    bRequired = true;
                }
                else if (mHeader.FindElements(By.TagName("span")).Count > 0)
                {
                    bRequired = mHeader.FindElements(By.TagName("span")).Where(elem => elem.GetAttribute("class").Contains("required")).Count() > 0;
                }
                else if (IsCurrentStormWeb)
                {
                    bRequired = mHeader.FindElements(By.XPath(".//*[contains(@class,'show-required')]")).Count > 0;
                }
                else
                {
                    bRequired = mHeader.FindElements(By.XPath(".//*[contains(@class,'required')]")).Count > 0;
                }
                return bRequired;
            }

            public bool IsColumnFiltered(String ColumnHeader)
            {
                bool bFiltered = false;

                IList<IWebElement> mHeaders = GetColumnHeaderElements();
                IWebElement mHeader = mHeaders[GetColumnIndex(ColumnHeader)];
                try
                {
                    if (mHeader.GetAttribute("class").Contains("ui-state-filtered"))
                    {
                        bFiltered = true;
                    }
                    else if (mHeader.GetAttribute("class").Contains("filtered"))
                    {
                        bFiltered = true;
                    }
                }
                catch
                {
                    bFiltered = false;
                }

                return bFiltered;
            }

            public void SortColumn(String ColumnHeader, String SortOrder)
            {
                ColumnHeader = ColumnHeader.ToLower();
                SortOrder = SortOrder.ToLower();

                IList<IWebElement> mHeaders = GetColumnHeaderElements();
                DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[GetColumnIndex(ColumnHeader)]);

                //in case GetColumnHeaderElements() returned span element which is obstructed
                if (!ctlHeader.mElement.Displayed && ctlHeader.mElement.TagName.ToLower() != "th")
                    ctlHeader.mElement = ctlHeader.mElement.FindElement(By.XPath("./ancestor::th"));
                ctlHeader.ScrollIntoViewUsingJavaScript(true);
                if (ctlHeader != null && ctlHeader.mElement.Displayed)
                {
                    ctlHeader.Click();
                    Thread.Sleep(1000);

                    //in case GetColumnHeaderElements return the 'span' elements instead of the 'th'
                    string currOrderClass = ctlHeader.mElement.TagName != "span" ? ctlHeader.GetAttributeValue("class")
                            : ctlHeader.mElement.FindElement(By.XPath("./ancestor::th")).GetAttribute("class");

                    if (SortOrder == "descending")
                    {
                        if (currOrderClass.Contains("sortD"))
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
                        if (currOrderClass.Contains("sortA"))
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

                String ActSortOrder = "";
                IList<IWebElement> mHeaders = GetColumnHeaderElements();
                DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[GetColumnIndex(ColumnHeader)]);

                if (ctlHeader.mElement.Displayed)
                {
                    string ctlHeaderClass = ctlHeader.GetAttributeValue("class");
                    ActSortOrder = CheckSortIndicator(ctlHeaderClass);
                    if (ActSortOrder == "")
                    {
                        //find table head and check for sort indicator
                        DlkBaseControl tableHead = new DlkBaseControl("TableHead", ctlHeader.mElement.FindElement(By.XPath(".//ancestor::th[1]")));
                        string tableHeadClass = tableHead.GetAttributeValue("class");
                        ActSortOrder = CheckSortIndicator(tableHeadClass);
                    }

                    DlkAssert.AssertEqual("VerifySortColumn", SortOrder, ActSortOrder);
                }
                else
                {
                    throw new Exception("VerifySortColumn() failed: Column " + ColumnHeader + " was not found.");
                }
            }

            /// <summary>
            /// Checks sorting of an element
            /// </summary>
            /// <param name="headerClass">Class name of the element</param>
            /// <returns>Descending or Ascending Sort</returns>
            private string CheckSortIndicator(string headerClass)
            {
                string ActualSortOrder = "";
                if (headerClass.Contains("sortD"))
                {
                    ActualSortOrder = "descending";
                }
                else if (headerClass.Contains("sortA"))
                {
                    ActualSortOrder = "ascending";
                }
                return ActualSortOrder;
            }

            public void VerifyDateSort(String ColumnHeader, String SortOrder, String TrueOrFalse)
            {
                //input validation
                if (!(new string[] { "ascending", "descending", "closest", "farthest", "indeterminate" }).Contains(SortOrder.ToLower().Trim()))
                    throw new Exception("VerifyDateSort() : Invalid input parameter [SortOrder=" + SortOrder + "]");

                //get all rows in the column
                IList<IWebElement> rows = GetRowsInColumn(ColumnHeader);
                List<DateTime> dates = new List<DateTime>();

                /*The following foreach loop will populate the list of dates to be used in analyzing the sorting pattern.
                 * The list patterns may be sorted as "ascending", "descending", "closest", "farthest" or "indeterminate".
                 * This snippet will only be compatible with rows that contain controls with class 'eventTime'. 
                 * Aside from that, this will only accept values that will follow these formats:
                 * Format 1:
                 *      2:00 - 2:30 pm Tuesday,
                 *      May 17th, 2016 
                 * Format 2:
                 *      8:00 pm Tuesday, June 2nd, 2009 -
                 *      8:00 pm Saturday, June 12th, 2010 
                 * Format 3:
                 *      11:52 pm Thursday, November 8th, 2007  
                 * Format 4:
                 *      8:00 pm Tuesday, July 1st -
                 *      8:00 pm Thursday, July 31st, 2008
                 
                    Acceptable formats
                    Date: 
                        January 1st, 2004
                        January 1st 2004
                        Jan 1 2004
                        January 1 2004
                        January 1, 2004
                    Time: 
                        8:00 pm
                        8:00
                        16:00
                    Additional formats should be handled by additional regex
                 */
                string timeRegex = "\\b\\d+:\\d{2}";
                string dateRegex = "\\b([A-z]{3,9}\\s[0-9]{1,2}([A-z]{0}|[A-z]{2}))";
                string yearRegex = "\\b(\\d{4})";
                string meridianRegex = "\\bam|\\bpm";
                foreach (IWebElement row in rows)
                {
                    string eventTimeValue = new DlkBaseControl("Row", row.FindElement(By.XPath(".//*[@class='eventTime']"))).GetValue();

                    string eventTime = Regex.Match(eventTimeValue, timeRegex).Value + " " + Regex.Match(eventTimeValue, meridianRegex).Value;
                    string eventDay = Regex.Match(eventTimeValue, dateRegex).Value;
                    string eventYear = Regex.Match(eventTimeValue, yearRegex).Value;
                    if (eventTime != null && eventDay != null && eventYear != null)
                    {
                        string eventDate = eventDay + " " + eventYear + " " + eventTime;
                        if (DlkString.IsValidTime(eventDate))
                        {
                            DateTime eventTimeActualDate = Convert.ToDateTime(eventDate);
                            dates.Add(eventTimeActualDate);
                        }
                    }
                    else
                        throw new Exception("VerifyDateSort () : Invalid date format " + eventTimeValue + "");
                }

                string ActualSortOrder = "";

                var sAscending = dates.OrderBy(x => x);
                var sDescending = dates.OrderByDescending(x => x);
                DateTime now = DateTime.Now;
                var sClosest = dates.OrderBy(n => (now - n).Duration());
                var sFarthest = dates.OrderByDescending(n => (now - n).Duration());

                if (dates.SequenceEqual(sDescending))
                    ActualSortOrder = "descending";
                else if (dates.SequenceEqual(sAscending))
                    ActualSortOrder = "ascending";
                else if (dates.SequenceEqual(sClosest))
                    ActualSortOrder = "closest";
                else if (dates.SequenceEqual(sFarthest))
                    ActualSortOrder = "farthest";
                else
                    ActualSortOrder = "indeterminate";

                bool ActualValue = false;
                bool ExpectedValue = Convert.ToBoolean(TrueOrFalse);
                if (SortOrder.ToLower() == ActualSortOrder)
                {
                    ActualValue = true;
                }
                else
                {
                    ActualValue = false;
                }

                DlkAssert.AssertEqual("VerifyDateSort", ExpectedValue, ActualValue);
            }

            public void SetTextColValue(IWebElement Cell, string Value)
            {

                try
                {
                    IWebElement mTextElm = Cell.FindWebElementCoalesce(By.CssSelector("input")
                  , By.CssSelector("div.htmlInput")
                  , By.XPath(".//div[contains(@class,'input') and not(contains(@class,'icon')) and not(contains(@class,'html-container'))]")
                  , By.XPath(".//div[@contenteditable='true']"));

                    if (mTextElm != null)
                    {
                        //Clear the initial contents of the cell
                        //In few instances, mElement.Clear() disables the control
                        ClearField(mTextElm);

                        //If cell still contains values after clear, Select all values using Ctrl+A keys then send input
                        if (!String.IsNullOrEmpty(mTextElm.GetAttribute("data-focus-value")) ||
                            !String.IsNullOrEmpty(mTextElm.GetAttribute("value")) ||
                            !String.IsNullOrEmpty(mTextElm.Text))
                        {
                            char a = '\u0001'; //ASCII code for Ctrl + A
                            Actions actions = new Actions(DlkEnvironment.AutoDriver);
                            actions.SendKeys(mTextElm, Convert.ToString(a)).Perform();
                        }

                        //Set the value of the cell
                        if (!String.IsNullOrEmpty(Value))
                            mTextElm.SendKeys(Value);
                        else
                            mTextElm.SendKeys(Keys.Backspace);

                        //If table cell value has not been set or still null after sending values, 
                        //Check if parameter value is null, if not, send values again until value has beent set with max 3 tries.
                        //In few instances, values are not set on first try

                        if (Value != null || Value != "")
                        {
                            int curRetry = 0;
                            int retryLimit = 3;
                            while (++curRetry <= retryLimit)
                            {
                                if (String.IsNullOrEmpty(mTextElm.GetAttribute("data-focus-value")) &&
                                    String.IsNullOrEmpty(mTextElm.GetAttribute("value")) &&
                                    String.IsNullOrEmpty(mTextElm.Text))
                                {
                                    mTextElm.SendKeys(Value);
                                    Thread.Sleep(500);
                                    DlkLogger.LogInfo("Retrying to set values. Number of attempts: [" + curRetry + "]");
                                }
                            }
                        }

                    }
                    else
                    {
                        throw new Exception("Unsupported cell type or cell does not allow inputs.");
                    }
                }
                catch
                {
                    throw;
                }
            }

            /// <summary>
            /// Gets the Value of the specified Header
            /// </summary>
            /// <param name="columnHeader"></param>
            /// <param name="isActualValue"></param>
            /// <returns></returns>
            public string GetColumnHeaderValue(IWebElement columnHeader, bool isActualValue = false)
            {
                string columnHeaderValue;

                if (!String.IsNullOrEmpty(columnHeader.GetAttribute("name")) && !isActualValue)
                {
                    columnHeaderValue = columnHeader.GetAttribute("name");
                }
                else if (!String.IsNullOrEmpty(columnHeader.GetAttribute("title")) && !isActualValue)
                {
                    columnHeaderValue = columnHeader.GetAttribute("title");
                }
                else if (columnHeader.FindElements(By.CssSelector("div.columnHdr")).Count > 0 && !isActualValue)
                {
                    columnHeaderValue = columnHeader.FindElement(By.CssSelector("div.columnHdr")).GetAttribute("title");
                }
                else
                {
                    if (columnHeader.GetAttribute("class").Contains("checkbox"))
                    {
                        IWebElement checkBox = columnHeader.FindElement(By.CssSelector("span.checkbox"));
                        var cBox = new DlkCheckBox("CheckBox", checkBox);
                        columnHeaderValue = cBox.GetCheckedState().ToString();
                    }
                    else
                    {
                        if (IsCurrentStormWeb && columnHeader.FindElements(By.CssSelector("div.columnHdr")).Count > 0)
                        {
                            columnHeaderValue = columnHeader.FindElement(By.CssSelector("div.columnHdr")).GetAttribute("textContent");
                        }
                        else
                        {
                            columnHeaderValue = columnHeader.GetAttribute("textContent");
                        }
                    }

                }

                return columnHeaderValue;
            }

            // For Navigator PM grids
            //*********************************************************************************************************************
            //DlkTable mResourceTable = GetResourceTable(ParentResourceOrLevel, 1);  //Table1 - Resource table data
            //DlkTable mCalHeadersTable = GetResourceTable(ParentResourceOrLevel, 2); //Table2 - Calendar table Dates
            //DlkTable mCalDataTable = GetResourceTable(ParentResourceOrLevel, 3);    //Table 3 - Calendar table Hours

            public void ExpandAllRows()
            {
                /*The list below will contain all the conditions that define the html buttons
                 * that will included in the list of webelements to be processed in this keyword.
                 * Any new conditions that needs to be considered must be added in the list below using the proper and working xpath.
                */
                List<string> xpath_conditions = new List<string>();
                xpath_conditions.Add("[not(contains(@class,'cellIcon Inactive'))]");
                xpath_conditions.Add("[not(contains(translate(@title,'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'add assignments'))]");
                xpath_conditions.Add("[not(contains(translate(@title,'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'assign resources'))]");
                xpath_conditions.Add("[not(@name='Add')]");
                xpath_conditions.Add("[../*[not(contains(@class,'disabled delete'))][not(contains(@display,'none'))]]");
                xpath_conditions.Add("[not(contains(@class,'undefined'))]");
                xpath_conditions.Add("[not(contains(@class,'add-button'))]");
                xpath_conditions.Add("[not(contains(@class,'rowTools'))]");

                IList<IWebElement> mMatchImg = mTableElement.FindElements(By.XPath(".//button" + String.Join("", xpath_conditions.ToList())));
                IList<IWebElement> buttonsInTbl = new List<IWebElement>(mMatchImg);
                int i = 0;
                foreach (IWebElement buttonCheck in buttonsInTbl.ToList())
                {
                    i++;
                    bool btnDisplayed = buttonCheck.Displayed;

                    if (!btnDisplayed)
                        buttonsInTbl.Remove(buttonCheck);
                }

                if (buttonsInTbl.Count == 0)
                {
                    //for rows not using button to expand
                    buttonsInTbl = mTableElement.FindElements(By.XPath(".//span[contains(@class,'tree-wrap')]"));

                    if (buttonsInTbl.Count == 0)
                    {
                        //for ngRP
                        buttonsInTbl = mTableElement.FindElements(By.XPath(".//div[contains(@class,'treeIcon')][not(contains(@class,'info_bubble'))]"));
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
                                        mImgTmp.ScrollIntoViewUsingJavaScript(true);
                                        buttonsInTbl[j].Click();
                                        DlkLogger.LogInfo("Successfully expanded row " + j);
                                        Thread.Sleep(1000);
                                    }
                                }
                            }
                            catch
                            {
                                //object reference not set, refresh contents of buttonsInTbl
                                DlkStormWebFunctionHandler.WaitScreenGetsReady();
                                buttonsInTbl = mTableElement.FindElements(By.XPath(".//div[contains(@class,'treeIcon')]"));
                                DlkImage mImgTmp = new DlkImage("ExpandImageLink", buttonsInTbl[j]);
                                String mSrc = buttonsInTbl[j].GetAttribute("class");
                                if (mSrc.Contains("treeIcon"))
                                {
                                    if (!mSrc.Contains("expanded"))
                                    {
                                        mImgTmp.ScrollIntoViewUsingJavaScript(true);
                                        DlkStormWebFunctionHandler.WaitScreenGetsReady();
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
                                mImgTmp.ScrollIntoViewUsingJavaScript(true);
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

            public void VerifyRowHighlighted(String iRow, bool TrueOrFalse)
            {
                try
                {
                    IWebElement mRow = GetRow(Convert.ToInt32(iRow));
                    bool highlight = (mRow.GetAttribute("class").Contains("highlight")) ? true : false;
                    DlkAssert.AssertEqual("VerifyRowHighlighted", highlight, TrueOrFalse);
                }
                catch (Exception e)
                {
                    throw new Exception("VerifyRowHighlighted() failed : " + e.Message, e);
                }
            }
            public void VerifyRowSelected(String iRow, bool TrueOrFalse)
            {
                try
                {
                    IWebElement mRow = GetRow(Convert.ToInt32(iRow));
                    bool select = (mRow.GetAttribute("class").Contains("selected")) ? true : false;
                    DlkAssert.AssertEqual("VerifyRowSelected", select, TrueOrFalse);
                }
                catch (Exception e)
                {
                    throw new Exception("VerifyRowSelected() failed : " + e.Message, e);
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
                MatchValue = MatchValue.Replace("\r\n", "\n");

                for (int i = 1; i <= GetRowsForReporting().Count; i++)
                {
                    List<String> rData = GetRowData(i, true);
                    string rowFormat = rData[iCol].Replace("\r\n", "\n");
                    if (rowFormat.Contains(MatchValue)) //((rData.Contains(MatchValue)) || 
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

                if (TableRow != null)
                {
                    mAttrib = TableRow.GetAttribute(AttributeName);
                }
                else
                {
                    mAttrib = "none";
                }
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

                    if (mElm.GetAttribute("class") == "notes")
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

            public void SetRowToolsCheckBox(int iRow, string TrueOrFalse)
            {
                DlkBaseControl ctlRow;
                IWebElement mRow = GetRow(iRow);
                ctlRow = new DlkBaseControl("Row", mRow);
                ctlRow.MouseOver();
                SetCheckBoxColValue(mRow, TrueOrFalse);
            }

            public void VerifyRowToolsCheckBoxValue(int iRow, string TrueOrFalse)
            {
                DlkBaseControl ctlRow;
                IWebElement mRow = GetRow(iRow);
                ctlRow = new DlkBaseControl("Row", mRow);
                ctlRow.MouseOver();
                VerifyCheckBoxColValue(mRow, TrueOrFalse);
            }

            public int GetResourceRow(String ResourceName)
            {

                int iRow = GetRowByKey(1, ResourceName);
                return iRow;
            }

            public Boolean VerifySearchNoResults(int iRow, string sColumnHeader, string sValue, string sTrueOrFalse)
            {
                Boolean bNoResults = false;
                IWebElement mRowElm = GetRow(iRow);
                IWebElement mCellElm = GetCell(iRow, sColumnHeader);

                //If no row is selected in the table, the cells don't have a class (New UI - 11/09/2016)
                if (!mRowElm.GetAttribute("class").Contains("selected"))
                    mRowElm.Click();

                if (mCellElm == null)
                    throw new Exception("Invalid row.");

                DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                string sClass = mCell.GetAttributeValue("class");

                //Lookup cells will disable upon clearing text. Use SetTextWithoutClear.
                if (sClass.Contains("lookup"))
                {
                    SetTextWithoutClear(mCellElm, sValue);
                }
                else
                {
                    IWebElement mInput = mCellElm.FindElement(By.TagName("input"));
                    DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                    txtInput.SetTextOnly(sValue);
                    Thread.Sleep(3000);
                }

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                list.FindElement();
                if (list.Exists(1)) //if list appears, it means no match was found or multiple matches are found
                {
                    IWebElement noResults = list.mElement.FindElement(By.ClassName("no-results"));
                    bNoResults = noResults.Displayed ? true : false;
                }

                return bNoResults;
            }

            public void SetSpecialSelectionsItem(int iRow, string sColumnHeader, string Item)
            {
                try
                {
                    IWebElement mCellElm = GetCell(iRow, sColumnHeader);

                    if (mCellElm == null)
                        throw new Exception("SetCellSpecialSelectionsItem() failed. Invalid row.");

                    DlkBaseControl mCell = new DlkBaseControl("Cell", mCellElm);
                    string sClass = mCell.GetAttributeValue("class");
                    mCell.ScrollIntoViewUsingJavaScript(true);

                    DlkMultiselect multiCell = new DlkMultiselect("Multiselect", mCellElm);
                    multiCell.SelectSpecialSelectionsItem(Item);
                }
                catch (Exception e)
                {
                    throw new Exception("SetCellSpecialSelectionsItem() failed : " + e.Message, e);
                }

            }
            /// <summary>
            /// Click on banner to lose focus on table
            /// </summary>
            public void ClickBanner()
            {
                try
                {
                    DlkLogger.LogInfo("Performing Click on Banner to remove focus on table.");
                    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='banner']")));
                    bannerCtrl.Click();
                }
                catch
                {
                    //Do nothing -- there might be instances that setting a text or value would display a dialog message
                    //Placing a log instead for tracking
                    DlkLogger.LogInfo("Problem performing Click on Banner. Proceeding...");
                }
            }
            #endregion

            // End of Navigator PM grids
            //***********************************************************************************************************************
        }

        public class TableControl
        {
            internal IWebElement e;
            private Body body;
            public TableControl(IWebElement tableElement)
            {
                this.e = tableElement;
            }

            public GridActions Actions { get { return new GridActions(e.FindElement(By.XPath(".//*[contains(@class,'grid-actions')]"))); } }
            public Header Header { get { return new Header(e.FindElement(By.XPath(".//*[contains(@class,'gridHdr')]"))); } }
            public Body Body
            {
                get
                {
                    if (body == null) body = GetBody();//For Lazy Loading
                    return body;
                }
            }

            private Body GetBody()
            {
                if (e.GetAttribute("class").Contains("gridBody"))
                    return new Body(e);
                else
                    return new Body(e.FindElement(By.XPath(".//*[contains(@class,'gridBody')]")));
            }

            public LeftRowTools LeftRowTools { get { return new LeftRowTools(e.FindElement(By.XPath(".//*[contains(@class,'rowTools')][contains(@class,'left')]"))); } }
            public RightRowTools RightRowTools { get { return new RightRowTools(e.FindElement(By.XPath(".//*[contains(@class,'rowTools')][contains(@class,'right')]"))); } }
            public Footer Footer { get { return new Footer(e.FindElement(By.XPath(".//*[contains(@class,'gridFooter')]"))); } }

        }

        public class GridActions
        {
            public GridActions(IWebElement actionsElement)
            {
                e = actionsElement;
            }

            private IWebElement e;
        }

        public class Header
        {
            public Header(IWebElement headerElement)
            {
                e = headerElement;
            }

            private IWebElement e;

            public IWebElement FilterRow
            {
                get
                {
                    return e.FindElement(By.XPath(".//*[contains(@class,'filterRow')]"));
                }
            }
        }

        public class Body
        {
            public Body(IWebElement bodyElement)
            {
                e = bodyElement;
            }

            private IWebElement e;
            private IList<IWebElement> rows;
            private IList<IWebElement> columnHeaders;
            private IList<string> columnHeaderLabels;
            private IList<string> columnCellNames;

            public IList<IWebElement> Rows
            {
                get
                {
                    if (rows == null) rows = e.FindElements(By.CssSelector("table>tbody>tr"))
                        .Where(x => !x.GetAttribute("class").Contains("filtered"))
                        .ToList();//For Lazy Loading
                    return rows;
                }
            }

            public IList<IWebElement> ColumnHeaders
            {
                get
                {
                    if (columnHeaders == null) columnHeaders = GetColumnHeaders();//For Lazy Loading
                    return columnHeaders;
                }
            }

            private IList<IWebElement> GetColumnHeaders()
            {
                var mHeaders = new List<IWebElement>();
                var xpath_columnHdr = ".//th[not(contains(@style,'display: none'))][not(contains(@style,'display:none'))][not(contains(@class,'hidden'))]//*[contains(@class,'columnHdr')]";
                var xpath_parent_gridHdr = "./preceding-sibling::div[contains(@class,'gridHdr')]";

                if (e.FindElements(By.XPath(xpath_columnHdr)).Count() > 0)
                {
                    mHeaders = e.FindElements(By.XPath(xpath_columnHdr)).ToList();
                }
                else if (e.FindElements(By.XPath(xpath_parent_gridHdr)).Count > 0)
                {
                    IWebElement mPrevGrid = e.FindElement(By.XPath(xpath_parent_gridHdr));
                    mHeaders = mPrevGrid.FindElements(By.XPath(xpath_columnHdr)).ToList();
                }
                else
                {
                    mHeaders = e.FindElements(By.CssSelector("th")).ToList();
                }

                return mHeaders;
            }

            public IList<string> ColumnHeaderLabels
            {
                get
                {
                    if (columnHeaderLabels == null) columnHeaderLabels = GetColumnHeaderLabels();//For Lazy Loading
                    return columnHeaderLabels;
                }
            }

            private IList<string> GetColumnHeaderLabels()
            {
                List<string> columnHeaderLabels = new List<string>();
                var columnHeaders = ColumnHeaders;

                for (int i = 0; i < columnHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", columnHeaders[i]);
                    string sHeader = ctlHeader.GetAttributeValue("title").ToLower();
                    if (sHeader == "")
                    {
                        sHeader = ctlHeader.GetValue().ToLower().Trim();
                        if (sHeader.Contains("\r\n"))
                        {
                            sHeader = sHeader.Replace("\r\n", " ");
                        }

                    }
                    if (columnHeaderLabels.Where(x => x == sHeader).Count() < 1)
                        columnHeaderLabels.Add(sHeader);
                }
                return columnHeaderLabels;
            }

            public IList<string> ColumnCellNames
            {
                get
                {
                    if (columnCellNames == null) columnCellNames = GetColumnCellNames();//For Lazy Loading
                    return columnCellNames;
                }
            }

            private IList<string> GetColumnCellNames()
            {
                var row = Rows.FirstOrDefault();
                var columnHeaders = row.FindElements(By.CssSelector("td"))
                .Where(x => x.GetCssValue("display").ToLower().Trim() != "none").ToList();

                List<string> columnCellNames = new List<string>();

                for (int i = 0; i < columnHeaders.Count; i++)
                {
                    DlkBaseControl ctlHeader = new DlkBaseControl("Header", columnHeaders[i]);
                    string sHeader = ctlHeader.GetAttributeValue("name").ToLower();
                    if (sHeader == "")
                    {
                        sHeader = ctlHeader.GetValue().ToLower();
                        sHeader = DlkString.RemoveCarriageReturn(sHeader);
                    }
                    if (columnCellNames.Where(x => x == sHeader).Count() < 1)
                        columnCellNames.Add(sHeader);
                }
                return columnCellNames;
            }

            public IWebElement GetCell(int rowNumber, string sColumnHeader)
            {
                var row = Rows[rowNumber - 1];
                var rowCells = row.FindElements(By.CssSelector("td"))
                    .Where(x => x.GetCssValue("display").ToLower().Trim() != "none").ToList();
                return rowCells[GetColumnHeaderIndex(sColumnHeader)];
            }

            public int GetColumnHeaderIndex(string sColumnHeader)
            {
                int index = -1;
                string mColumnText = sColumnHeader.ToLower().Trim();
                var mColumnHeaders = ColumnHeaderLabels;

                //Check matching Column in Headers
                if (mColumnHeaders.Count(x => x == mColumnText) > 0)
                    index = ColumnHeaderLabels.IndexOf(mColumnText);
                else //Check matching Column in Names
                {
                    mColumnHeaders = ColumnCellNames;
                    index = mColumnHeaders.IndexOf(mColumnText);
                }

                return index;
            }

            public string GetCellValue(int rowNumber, string sColumnHeader)
            {
                string cellValue;
                IWebElement cell = GetCell(rowNumber, sColumnHeader);
                if (cell == null)
                    return null;

                DlkBaseControl Cell = new DlkBaseControl("Cell", cell);
                Cell.ScrollIntoViewUsingJavaScript();

                string sClass = cell.GetAttribute("class");
                string[] textBoxClasses = { "textCol", "text_field", "lookup", "buttonCol", "lookupCol", "emailCol", "right edit" };
                string[] comboBoxClasses = { "ddwnCol", "core_dropdown" };
                string[] htmlClasses = { "cell-html", "wrap-text" };
                string[] imageClasses = { "cellImage", "attribute-icon" };
                string[] checkBoxClasses = { "checkbox-cell", "checkboxCol", "checkbox", "selectboxCol" };

                if (sClass == "lookup2" || sClass.Contains("lookup1") && !sClass.Contains("ddwnCol"))
                {
                    cellValue = GetLabelColValue(cell);
                    
                }
                else if(comboBoxClasses.Any(x => sClass.Contains(x)))
                {
                    cellValue = GetDdownColValue(cell);
                }
                else if (textBoxClasses.Any(x => sClass.Contains(x)))
                {
                    cellValue = GetTextColValue(cell);
                }
                else if (htmlClasses.Any(x => sClass.Contains(x)))
                {
                    cellValue = GetHtmlColValue(cell);
                }
                else if (imageClasses.Any(x => sClass.Contains(x)))
                {
                    cellValue = GetImageColValue(cell);
                }
                else if (checkBoxClasses.Any(x => sClass.Contains(x)))
                {
                    cellValue = GetCheckBoxColValue(cell);
                }
                else if (sClass.Contains("core_radio_group_field"))
                {
                    cellValue = GetRadioButtonColValue(cell);
                }
                else if (sClass.Contains("treeCol"))
                {
                    cellValue = GetTreeColValue(cell);
                }
                else
                {
                    if (sClass.Contains("Col"))
                        cellValue = GetTextColValue(cell);
                    else if (cell.FindElements(By.XPath(".//*[contains(@class,'attribute-icon')]")).Count > 0)
                        cellValue = GetImageColValue(cell);
                    else
                        cellValue = GetLabelColValue(cell);
                }

                return cellValue;

            }

            public int GetRowWithColumnValue(string sColumnHeader, string sValue)
            {
                int i = 1;
                int filtered = 0;
                string cellValue = "";
                try
                {
                    while (cellValue != null)
                    {
                        cellValue = GetCellValue(i, sColumnHeader);
                        if (DlkString.RemoveCarriageReturn(cellValue) == sValue)
                        {

                            if (Rows[i - 1].GetAttribute("data-visible-before-search") == null)
                                return i - filtered;
                            else
                                //Subtract the preceeding rows that is not visible.
                                return i - Rows.Where((element) =>
                                {
                                    return Rows.IndexOf(element) < (i - 1) && !element.Displayed;
                                }).Count();
                        }
                        if (Rows[i - 1].GetAttribute("class").Contains("filtered"))
                            filtered++;
                        i++;
                    }
                    return -1;
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            private string GetTextColValue(IWebElement Cell)
            {
                DlkBaseControl cell = new DlkBaseControl("Cell", Cell);
                string cellValue;
                string xpath_TextBox2 = ".//div[contains(@class,'htmlInput')]";
                string xpath_TextBox3 = ".//*[contains(@class,'gridImage')]";

                IWebElement textBox = Cell.FindWebElementCoalesce(By.TagName("input"), By.XPath(xpath_TextBox2), By.XPath(xpath_TextBox3));
                Boolean IsXPathGridImage = new DlkBaseControl("Grid Image", cell, "XPATH", xpath_TextBox3).Exists(1) ? true : false;
                DlkTextBox TextBox = new DlkTextBox("TextBox", textBox);

                cellValue = IsXPathGridImage ? TextBox.GetAttributeValue("title") : TextBox.GetValue();
                return cellValue;
            }

            private string GetLabelColValue(IWebElement Cell)
            {
                DlkBaseControl cell = new DlkBaseControl("Cell", Cell);
                string cellValue;

                if (!Cell.Displayed)
                    cell.MouseOver();

                string iClass = cell.GetAttributeValue("class").ToLower();
                DlkBaseControl input = new DlkBaseControl("Input", cell, "XPATH", ".//input");
                IWebElement label = iClass.Contains("edit") ? input.Exists(1) ? input.mElement : null : null;
                cellValue = label != null ? new DlkLabel("Label", label).GetValue() : cell.GetValue();

                return DlkString.RemoveCarriageReturn(cellValue.Trim());
            }

            private string GetDdownColValue(IWebElement Cell)
            {
                DlkBaseControl cell = new DlkBaseControl("Cell", Cell);
                string cellValue = "";
                string xpath_DropDown = ".//*[contains(@class, 'dropdown-field-container')]";
                string xpath_DropDown2 = ".//*[contains(@class, 'inputContainer')]";
                string xpath_PopMenu = ".//*[contains(@class, 'popupmenu')]";
                string xpath_QuickEdit = ".//*[contains(@class,'quick_edit')]";
                string xpath_TagsText = "./following-sibling::*[@class='tagsinput']/*[@class='tag']//*[@class='tagstext']";
                string xpath_Content = ".//*[contains(@class,'content')]";


                IWebElement dDown = Cell.FindWebElementCoalesce(By.XPath(xpath_DropDown), By.XPath(xpath_DropDown2)) != null ? Cell.FindWebElementCoalesce(By.TagName("input")) :
                    Cell.FindWebElementCoalesce(By.XPath(xpath_PopMenu)) != null ? Cell.FindWebElementCoalesce(By.TagName("span")) :
                    Cell.FindWebElementCoalesce(By.XPath(xpath_QuickEdit)) != null ? Cell.FindWebElementCoalesce(By.XPath(xpath_Content)) : null;

                if (dDown.TagName == "input" && new DlkBaseControl("Tags Text", cell, "XPATH", xpath_TagsText).Exists(1))
                {
                    DlkLogger.LogInfo("Reading values of each tag in the control...");
                    foreach (IWebElement tag in dDown.FindElements(By.XPath(xpath_TagsText)))
                    {
                        DlkTextBox txtInput = new DlkTextBox("Input", tag);
                        cellValue += DlkString.RemoveCarriageReturn(txtInput.GetValue()).Trim() + "~";
                    }
                    cellValue = cellValue.Trim('~');
                }
                else
                {
                    cellValue = new DlkBaseControl("Drop Down", dDown).GetValue();
                }
                return cellValue;
            }

            private string GetCheckBoxColValue(IWebElement Cell)
            {
                string cellValue = "";
                string xpath_CheckBox = ".//span[contains(@class,'gridCheckbox')]";
                string xpath_CheckBox2 = ".//*[contains(@class,'checkbox')]";

                IWebElement checkBox = Cell.FindWebElementCoalesce(By.XPath(xpath_CheckBox), By.XPath(xpath_CheckBox2));

                if (checkBox != null)
                {
                    DlkCheckBox checkState = new DlkCheckBox("CheckBox", checkBox);
                    cellValue = checkState.GetCheckedState().ToString();
                }

                return cellValue;
            }

            private string GetRadioButtonColValue(IWebElement Cell)
            {
                string cellValue = "";
                Boolean rdBtnCellState;
                string css_InputRadio = "input.radio-group-field";
                string xpath_RadioLabel = ".//following-sibling::span[@class='radio-label']";

                IList<IWebElement> mRadioButtonElmList = Cell.FindElements(By.CssSelector(css_InputRadio))
                    .Where(item => item.Displayed).ToList();

                if (mRadioButtonElmList.Count > 0)
                {
                    foreach (IWebElement mRadioButtonElm in mRadioButtonElmList)
                    {
                        DlkRadioButton rdbtnCell = new DlkRadioButton("RadioButton", mRadioButtonElm);
                        rdBtnCellState = rdbtnCell.GetState();
                        if (rdBtnCellState.ToString().ToLower().Equals("true"))
                        {
                            IWebElement mRadioButtonLbl = mRadioButtonElm.FindElement(By.XPath(xpath_RadioLabel));
                            cellValue = mRadioButtonLbl.Text;
                            break;
                        }
                    }
                }
                return cellValue;
            }

            private string GetImageColValue(IWebElement Cell)
            {
                DlkBaseControl cell = new DlkBaseControl("Cell", Cell);
                string cellValue = "";
                string xpath_Image = ".//*[contains(@style,'image')]";
                string xpath_Icon = ".//*[contains(@class,'icon')]";

                if (!Cell.Displayed)
                    cell.MouseOver();

                IWebElement image = Cell.FindWebElementCoalesce(By.XPath(xpath_Image), By.XPath(xpath_Icon));
                cellValue = image.GetAttribute("title");

                return cellValue;
            }

            private string GetHtmlColValue(IWebElement Cell)
            {
                DlkBaseControl cell = new DlkBaseControl("Cell", Cell);
                DlkBaseControl control = new DlkBaseControl("Control", cell, "XPATH", ".//*");
                string cellValue;

                IWebElement html = control.Exists(1) ? control.mElement : null;
                cellValue = html != null ? new DlkBaseControl("HTML", html).GetValue().Trim() :
                    new DlkBaseControl("HTML", Cell).GetValue().Trim();

                return DlkString.RemoveCarriageReturn(cellValue);
            }

            private string GetTreeColValue(IWebElement Cell)
            {
                DlkBaseControl cell = new DlkBaseControl("Cell", Cell);
                string cellValue;
                string xpath_TreeCol = ".//div[contains(@class,'treeValue')]";
                DlkBaseControl treeCol = new DlkBaseControl("Tree Coll", cell, "XPATH", xpath_TreeCol);

                IWebElement treeColVal = treeCol.Exists(1) ? Cell.FindWebElementCoalesce(By.TagName("input"), By.TagName("span"), By.TagName("div")) : null;
                if (treeColVal.Text == "")
                {
                    cellValue = "";
                }
                else
                {
                    cellValue = treeColVal != null ? new DlkBaseControl("Column", treeColVal).GetValue().Trim() : null;
                }
                return DlkString.RemoveCarriageReturn(cellValue);
            }
        }

        public class LeftRowTools
        {
            public LeftRowTools(IWebElement leftRowToolsElement)
            {
                this.e = leftRowToolsElement;
            }

            private IWebElement e;
        }

        public class RightRowTools
        {
            public RightRowTools(IWebElement rightRowToolsElement)
            {
                e = rightRowToolsElement;
            }

            private IWebElement e;
        }

        public class Footer
        {
            public Footer(IWebElement footerElement)
            {
                e = footerElement;
            }

            private IWebElement e;
        }
        #endregion

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

        [Keyword("VerifyColumnHeaderContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnHeaderContains(String ColumnHeader, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool mExists;

                if (!Boolean.TryParse(TrueOrFalse, out mExists)) throw new Exception("Invalid TrueOrFalse value.");

                TableData.VerifyColumnHeaderContains(ColumnHeader, mExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnHeaders", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnHeaders(String ColumnHeaders, String Attribute)
        {
            try
            {
                Initialize();
                TableData.VerifyColumnHeaders(ColumnHeaders, Attribute);
                DlkLogger.LogInfo("VerifyColumnHeaders() successfully executed ");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaders() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnHeadersContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnHeadersContains(String ColumnHeaders, String Attribute)
        {
            try
            {
                Initialize();
                TableData.VerifyColumnHeaders(ColumnHeaders, Attribute, true);
                DlkLogger.LogInfo("VerifyColumnHeadersContains() successfully executed ");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeadersContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnRequired", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnRequired(String ColumnHeader, String TrueOrFalse)
        {
            Initialize();
            DlkAssert.AssertEqual("VerifyColumnRequired", Convert.ToBoolean(TrueOrFalse), TableData.IsColumnRequired(ColumnHeader));
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

        [Keyword("VerifyRowHighlighted", new String[] { "1|text|Row|1",
                                                    "2|text|ExpectedResult|True"})]
        public void VerifyRowHighlighted(String iRow, string TrueOrFalse)
        {
            Initialize();
            TableData.VerifyRowHighlighted(iRow, Convert.ToBoolean(TrueOrFalse));
        }

        [Keyword("VerifyRowSelected", new String[] { "1|text|Row|1",
                                                    "2|text|ExpectedResult|True"})]
        public void VerifyRowSelected(String iRow, string TrueOrFalse)
        {
            Initialize();
            TableData.VerifyRowSelected(iRow, Convert.ToBoolean(TrueOrFalse));
        }

        [Keyword("GetRowWithColumnValue", new String[] { "1|text|ColumnHeader|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetRowWithColumnValue(string ColumnHeader, string Value, string VariableName)
        {
            try
            {
                Initialize();
                int iRow = table.Body.GetRowWithColumnValue(ColumnHeader, Value);
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
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowWithColumnValueContains", new String[] { "1|text|ColumnHeader|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|MyRow"})]
        public void GetRowWithColumnValueContains(string ColumnHeader, string Value, string VariableName)
        {
            try
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
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnValueContains() failed : " + e.Message, e);
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
            int iRow = Convert.ToInt32(Row) + 1;
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

        [Keyword("VerifyRowExistsWithColumnValueContains", new String[] { "1|text|ColumnHeader|ID",
                                                                          "2|text|SearchedValue|1234",
                                                                          "3|text|ExpectedValue|TRUE"})]
        public void VerifyRowExistsWithColumnValueContains(string ColumnHeader, string Value, string TrueOrFalse)
        {
            bool bResult = false;
            Initialize();
            int iRow = TableData.GetRowWithColumnValueContains(ColumnHeader, Value);
            if (iRow > -1)
            {
                bResult = true;
            }

            DlkAssert.AssertEqual("VerifyRowExistsWithColumnValueContains", Convert.ToBoolean(TrueOrFalse), bResult);

        }

        [Keyword("VerifyValueExistsInColumnNumber", new String[] { "1|text|ColumnNumber|1",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|ExpectedValue|TRUE"})]
        public void VerifyValueExistsInColumnNumber(string ColumnNumber, string Value, string TrueOrFalse)
        {
            bool bResult = false;
            Initialize();

            int iRow = TableData.GetRowByColumnNumber(Convert.ToInt32(ColumnNumber), Value);
            if (iRow > -1)
            {
                bResult = true;
            }

            DlkAssert.AssertEqual("VerifyValueExistsInColumnNumber", Convert.ToBoolean(TrueOrFalse), bResult);
        }

        [Keyword("SetCellValue", new String[] { "1|text|Row|1",
                                                "2|text|ColumnHeader|Notes",
                                                "3|text|Value|sample text"})]
        public void SetCellValue(string Row, string ColumnHeader, string Value)
        {
            Initialize();
            TableData.SetCellValue(int.Parse(Row), ColumnHeader, Value);
        }

        [Keyword("SetCellSpecialSelectionsItem", new String[] { "1|text|Row|1",
                                                "2|text|ColumnHeader|Notes",
                                                "3|text|Item|sample text"})]
        public void SetCellSpecialSelectionsItem(string Row, string ColumnHeader, string Item)
        {
            Initialize();
            TableData.SetSpecialSelectionsItem(int.Parse(Row), ColumnHeader, Item);
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
                mCell.ScrollIntoViewUsingJavaScript(true);
                mCell.Click();

                //In hours grid in Timesheets, the cell retrieved by GetCell() is underneath a popup form. The popup form has the cell that accepts value from the UI.
                if (mElement.FindElements(By.XPath("//*[@id='popupForm']")).Where(x => x.Displayed).Count() > 0)
                {
                    TableData.SetTextColValue(mElement.FindElement(By.XPath("//*[@id='popupForm']//div[@id='cellBox']")), Text);
                }
                else
                {
                    TableData.SetTextColValue(mCell.mElement, Text);
                }
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
            try
            {
                int row;
                string ActualValue;

                if (!int.TryParse(Row, out row) || row == 0)
                    throw new Exception("VerifyCellValue() : Parameter supplied [Row] is not a valid input number");

                Initialize();
                ActualValue = table.Body.GetCellValue(row, ColumnHeader);
                DlkAssert.AssertEqual("VerifyCellValue()", ExpectedValue.ToLower(), ActualValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellButtonDisplayed", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes",
                                                   "3|text|ExpectedValue|sample text"})]
        public void VerifyCellButtonDisplayed(string Row, string ColumnHeader, string ExpectedValue)
        {
            try
            {
                Initialize();
                TableData.VerifyCellButtonDisplayed(int.Parse(Row), ColumnHeader, ExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellButtonDisplayed() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValueContains", new String[] { "1|text|Row|1",
                                                   "2|text|ColumnHeader|Notes",
                                                   "3|text|ExpectedValue|sample text"})]
        public void VerifyCellValueContains(string Row, string ColumnHeader, string ExpectedValue)
        {
            try
            {
                Initialize();
                TableData.VerifyCellValueContains(int.Parse(Row), ColumnHeader, ExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValueContains() failed : " + e.Message, e);
            }
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

            int row = 0;
            if (!Int32.TryParse(Row, out row))
                throw new FormatException("VerifyCellReadOnly() : Parameter supplied [Row] is not a valid integer");

            bool ExpectedResult = false;
            if (!bool.TryParse(TrueOrFalse, out ExpectedResult))
                throw new FormatException("VerifyCellReadOnly() : Parameter supplied [TrueOrFalse] is not a valid boolean");

            var cell = table.Body.GetCell(row, ColumnHeader);
            bool ActualResult = cell.GetAttribute("class").Contains("is-locked");

            //if false, use old functionality ------- TO DO: Check if all of these are still used
            if (!ActualResult)
            {
                DlkBaseControl mCell = new DlkBaseControl("Cell", cell);
                mCell.ScrollIntoViewUsingJavaScript();
                string sClass = mCell.GetAttributeValue("class");               

                string[] textBoxClasses = { "textCol", "core_text_field", "htmlCol", "lookupCol", "Col", "core_date_field", "edit" }; 
                string[] labelFormattingClasses = { "right", "center", "left", "infoBubble"};
                string[] checkBoxClasses = { "checkboxCol", "checkbox-cell" };

                if (textBoxClasses.Any(x => sClass.Contains(x)) && (!sClass.Contains("cell-html"))) //use TextBox VerifyReadOnly
                {
                    IWebElement mTextElm = cell.FindElements(By.CssSelector("input")).Count > 0 ?
                        cell.FindElement(By.CssSelector("input"))
                        : cell.FindElement(By.CssSelector("div.htmlInput"));

                    ActualResult = Convert.ToBoolean(new DlkTextBox("TextBox", mTextElm).IsReadOnly());
                }
                else if (sClass.Contains("ddwnCol") || sClass.Contains("core_dropdown_field")) //use ComboBox VerifyReadOnly
                {
                    ActualResult = new DlkComboBox("ComboBox", cell).IsReadOnly();
                }               
                else if (checkBoxClasses.Any(x => sClass.Contains(x))) //use CheckBox VerifyReadOnly
                {               
                    var cellElementLocked = cell.FindWebElementCoalesce(By.XPath(".//span[contains(@class,'locked')]"));                    
                    ActualResult = cellElementLocked != null ? true  
                          : Convert.ToBoolean(new DlkCheckBox("CheckBox", cell).IsReadOnly()); 
                }
                else if (sClass.Contains("cell-html")) //use TextEditor VerifyReadOnly
                {
                    IWebElement mEditorElm = cell.FindElements(By.XPath("./descendant::div[@class='text-editor-field']")).Count > 0 ?
                        cell.FindElement(By.XPath("./descendant::div[@class='text-editor-field']"))
                        : cell.FindElement(By.XPath("./descendant::div[contains(@class,'input-element html-input string_input')]"));

                    bool ReadOnly = true;
                    if (mEditorElm.GetAttribute("contenteditable").ToLower() == "true")
                    {
                        ReadOnly = false;
                    }
                    ActualResult = ReadOnly;
                }
                else if (mCell.Exists() && (String.IsNullOrEmpty(sClass) || labelFormattingClasses.Any(x => sClass.Contains(x)))) // For label type cells with null class element
                {
                    ActualResult = true;
                }
                else if (sClass.Contains("Image")) // For Images 
                {
                    ActualResult = true;
                }      
                else
                {
                    throw new Exception("VerifyCellReadOnly() : Class not supported.");
                }
            }

            DlkAssert.AssertEqual("VerifyCellReadOnly()", ExpectedResult, ActualResult);
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

        [Keyword("VerifyTextHyperLink", new String[] { "1|text|Row|1|True",
                                                  "2|text|ColumnHeader|Notes|True",})]
        public void VerifyTextHyperLink(string Row, string ColumnHeader, String ExpectedValue, String TrueOrFalse)
        {
            try
            {
                Initialize();
                TableData.VerifyTextHyperLink(int.Parse(Row), ColumnHeader, ExpectedValue, Convert.ToBoolean(TrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextHyperLink() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Row|1" })]
        public void VerifyRowCount(string Count)
        {
            try
            {
                int ExpectedCount = 0;
                if (!int.TryParse(Count, out ExpectedCount))
                    throw new FormatException("VerifyRowCount() : Parameter supplied [Count] is not a valid integer");

                Initialize();
                int ActualCount = table.Body.Rows.Count;

                DlkAssert.AssertEqual("VerifyRowCount()", ExpectedCount, ActualCount);
                DlkLogger.LogInfo("VerifyRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Row|1",
                                                "2|text|VariableName|RowCount"})]
        public void GetRowCount(string VariableName)
        {
            Initialize();
            int actRowCount = mElement.FindElements(By.XPath("./descendant::tbody/tr")).Count - 1;
            // int actRowCount = mElement.FindElements(By.CssSelector("table.bodyTable>tbody>tr:not(.hide_filtered)")).Count -1;
            DlkVariable.SetVariable(VariableName, actRowCount.ToString());
            DlkLogger.LogInfo("GetRowCount() successfully executed.");

        }

        [Keyword("GetActualRowCount", new String[] { "1|text|Row|1",
                                                "2|text|VariableName|RowCount"})]
        public void GetActualRowCount(string VariableName)
        {
            Initialize();
            int actRowCount = mElement.FindElements(By.XPath(".//table[not(@class='rowToolsTable')][not(@class='hdrTable')]/tbody/tr[not(contains(@class,'filtered'))]")).Count;
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
                    DlkBaseControl lstResults = new DlkBaseControl("ResultsList", "XPATH_DISPLAY", "//ul[contains(@class,'results')]");
                    //some tables have popupmenu dropdowns
                    DlkBaseControl popupLst = new DlkBaseControl("ResultsList", "XPATH_DISPLAY", "//ul[contains(@class,'popupmenu')]");
                    /* Click only drop-down button if results list not displayed */
                    if (!lstResults.Exists(1)) /* Is Nav style table dropdown list results NOT displayed? */
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
                    if (lstResults.Exists(1)) /* Nav style table dropdown results list */
                    {
                        DlkAssert.AssertEqual("VerifyItemInDropDownList()", bool.Parse(TrueOrFalse), mElement.FindElements(By.XPath(
                            "//ul[contains(@class,'results')]/descendant::div[normalize-space(@class)='search-name'][normalize-space(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'))='"
                            + Item.ToLower().Trim() + "']")).Count > 0);
                        targetCell.Click();
                        DlkLogger.LogInfo("VerifyItemInDropDownList() passed");
                    }
                    else if (popupLst.Exists(1)) /* Nav style table dropdown results list */
                    {
                        DlkAssert.AssertEqual("VerifyItemInDropDownList()", bool.Parse(TrueOrFalse), DlkEnvironment.AutoDriver.FindElements(By.XPath(
                            "//ul[contains(@class,'popupmenu')]//li[not(contains(@style, 'display: none'))]//span[normalize-space(translate(text(),'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'))='"
                            + Item.ToLower().Trim() + "']")).Count > 0);
                        targetCell.Click();
                        DlkLogger.LogInfo("VerifyItemInDropDownList() passed");
                    }
                    else /* CRM style table dropdown results list */
                    {
                        DlkAssert.AssertEqual("VerifyItemInDropDownList()", bool.Parse(TrueOrFalse),
                            mElement.FindElements(By.XPath("//div[contains(@class,'ddwnListContainer openedBelow')]/descendant::tr[@class='ddwnListItem']/td/div[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
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
                targetCell.Click();
                if (targetCell.GetAttribute("class").Contains("core_dropdown"))
                {
                    targetCell = targetCell.FindElement(By.XPath(".//div[contains(@class,'core-field')]"));
                }

                DlkComboBox cmb = new DlkComboBox("ComboBox", targetCell);
                cmb.VerifyList(Items);

                DlkLogger.LogInfo("VerifyDropDownList() passed");

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

                DlkBaseControl target = new DlkBaseControl("TargetCell", targetCell);
                target.ScrollIntoViewUsingJavaScript(true);
                DlkStormWebFunctionHandler.WaitScreenGetsReady();

                if (targetCell.GetAttribute("class").Equals("treeCol"))
                {
                    TableData.TryClickElement(target);
                }
                else
                {
                    target.Click();
                }

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
            int row;
            if (!int.TryParse(Row, out row) || row == 0)
                throw new Exception("ClickLineOption() : Parameter supplied [Row] is not a valid input number");

            String XPath_RowTools = "./descendant::table[@class='rowToolsTable']//tr[" + Row + "]";
            String XPath_RowTools2 = "./descendant::tbody//tr[" + Row + "]";
            String XPath_PopupMenuItem = ".//span[normalize-space(text())='" + Option.Trim() + "']";

            try
            {
                Initialize();
                ClickDropdownArrow(Row);
                Thread.Sleep(250);

                IWebElement targetCell = mElement.FindWebElementCoalesce(By.XPath(XPath_RowTools), By.XPath(XPath_RowTools2));

                if (targetCell == null)
                    throw new Exception("Target row cannot be found.");

                //if line option is still not clicked
                DlkBaseControl popMenu = new DlkBaseControl("PopUp", "CLASS_DISPLAY", "popupmenu");

                if (!popMenu.Exists(1))
                {
                    new DlkBaseControl("Cell", targetCell).Click();
                    ClickDropdownArrow(Row);
                    popMenu = new DlkBaseControl("PopUp", "CLASS_DISPLAY", "popupmenu");
                }

                if (!popMenu.Exists(1))
                    throw new Exception("Popup menu list cannot be found.");

                //allows reusability of code

                IWebElement itemOption = null;
                Action<IWebElement> clickOption = (ctrl) =>
                {
                    DlkLogger.LogInfo("Attempting to click option");
                    DlkBaseControl mOpt = new DlkBaseControl("Button", ctrl);
                    try
                    {
                        mOpt.Click(4, 5);
                    }
                    catch
                    {
                        mOpt.ClickUsingJavaScript();
                    }
                };

                try
                {
                    itemOption = popMenu.mElement.FindElements(By.XPath(XPath_PopupMenuItem)).Where(option => option.Displayed).First();
                    clickOption(itemOption);
                }
                catch
                {
                    // two tries, Delete seems to be 2 items
                    itemOption = popMenu.mElement.FindElements(By.XPath(XPath_PopupMenuItem)).Where(option => option.Displayed).Last();
                    clickOption(itemOption);
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
                String listVisibleOptions = String.Empty;
               var popUpSearch = "//*[contains(@class,'popupmenu')][contains(@style, 'display: block')][not(contains(@class,'action-bar'))]";
               var popUpSearch1 = "//div[contains(@class,'popupmenu')][contains(@style, 'display: block')][not(contains(@class,'action-bar'))]";

                if (DlkEnvironment.AutoDriver.FindElements(By.XPath(popUpSearch)).Count > 0)
                {
                    IWebElement popup = DlkEnvironment.AutoDriver.FindElement(By.XPath(popUpSearch));
                    if (popup.Text == "")
                    {
                        popup = DlkEnvironment.AutoDriver.FindElement(By.XPath(popUpSearch1));
                    }

                    ReadOnlyCollection<IWebElement> optionItems = popup.FindElements(By.XPath(".//span[contains(@class,'popupLabel')]"));
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
                    ClickDropdownArrow(Row);//close dropdown
                }
                else
                {
                    DlkLogger.LogInfo("No list popup is found.");
                    listVisibleOptions = string.Empty;
                }

                DlkAssert.AssertEqual("VerifyLineOptions", ListItems, listVisibleOptions);
                DlkLogger.LogInfo("VerifyLineOptions() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptions() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineOptionTooltip", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineOptionTooltip(String Row, String Option, String ExpectedValue)
        {
            try
            {
                Initialize();
                ClickDropdownArrow(Row);
                Thread.Sleep(250);

                Boolean bFound = false;
                IWebElement popup = DlkEnvironment.AutoDriver.FindElement(By.XPath("//ul[contains(@class,'popupmenu')][contains(@style, 'display: block')]"));
                IReadOnlyCollection<IWebElement> lstItems = popup.FindElements(By.XPath(".//span[contains(@class,'popupLabel')]"));

                for (int i = 0; i < lstItems.Count; i++)
                {
                    DlkBaseControl item = new DlkBaseControl("Item", lstItems.ElementAt(i));
                    if (item.GetValue() == Option)
                    {
                        item.MouseOver();
                        string ActualValue = item.GetAttributeValue("title").Trim();
                        DlkAssert.AssertEqual("VerifyLineOptionTooltip()", ExpectedValue.Trim(), ActualValue);
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("VerifyLineOptionTooltip() : Unable to find item [" + Option + "]");
                }

                DlkLogger.LogInfo("VerifyLineOptionTooltip() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptionTooltip() failed : " + e.Message, e);
            }
        }

        [Keyword("SetRowToolsCheckBox", new String[] { "1|text|Row|1",
                                                  "2|text|Value|TRUE"})]
        public void SetRowToolsCheckBox(string Row, string TrueOrFalse)
        {
            try
            {
                Initialize();
                TableData.SetRowToolsCheckBox(int.Parse(Row), TrueOrFalse);
            }
            catch (Exception e)
            {
                throw new Exception("SetRowToolsCheckBox() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowToolsCheckBoxValue", new String[] { "1|text|Row|1",
                                                  "2|text|Value|TRUE"})]
        public void VerifyRowToolsCheckBoxValue(string Row, string TrueOrFalse)
        {
            try
            {
                Initialize();
                TableData.VerifyRowToolsCheckBoxValue(int.Parse(Row), TrueOrFalse);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowToolsCheckBoxValue() failed : " + e.Message, e);
            }
        }

        public void ClickDropdownArrow(String LineIndex)
        {
            IList<IWebElement> mHeaders = mElement.FindElements(By.CssSelector("div.gridHdr"));
            String index;
            //+ 1 not required if header is in a separate table or is a rowTool 
            if (mHeaders.Count > 0)
            {
                index = LineIndex;
            }
            else if (mElement.GetAttribute("class").Contains("rowTools"))
            {
                index = LineIndex;
            }
            else if (mElement.FindElements(By.XPath(".//td[@name='DownArrow']")).Count > 0)
            {
                index = LineIndex;
            }
            else
            {
                index = (Convert.ToInt32(LineIndex) + 1).ToString();
            }
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[not(contains(@class,'hide'))][not(contains(@class,'filtered'))][" + index + "]"));
            int attempts = 0;
            if (DlkEnvironment.mBrowser.ToLower() != "firefox")
            {
                new DlkBaseControl("Row", targetRow).MouseOver();
            }
            try
            {
                string xpath_GridImage = ".//div[@name='RowTool']//div[@class='gridImage']";
                string xpath_DownArrow = ".//*[@name='DownArrow']//button";
                string xpath_TitleOptions = ".//button[contains(@title,'Options')]";
                string xpath_TitleOptions2 = "./ancestor::div[@class='leftPanel']/following-sibling::div[@class='rightPanel']/descendant::tbody/tr[" + index + "]//button[contains(@title,'Options')]";
                string xpath_TitleOptions3 = ".//div[contains(@class,'rowTools')]//tr[" + index + "]//button[contains(@title,'Options')]";

                IWebElement btn = targetRow.FindWebElementCoalesce(By.XPath(xpath_GridImage), By.XPath(xpath_DownArrow), By.XPath(xpath_TitleOptions),
                    By.XPath(xpath_TitleOptions2), By.XPath(xpath_TitleOptions3));

                if (btn == null)
                {
                    var btnElement = mElement.FindWebElementCoalesce(By.XPath(xpath_TitleOptions3));

                    if (btnElement == null)
                        throw new Exception("ClickDropdownArrow() failed: Unable to find arrow button.");
                    else
                        btn = btnElement;

                    //btn = btnElement ?? throw new Exception("ClickDropdownArrow() failed: Unable to find arrow button.");
                }

                while (attempts < 5 && btn != null)
                {
                    try
                    {
                        var ddArrow = new DlkBaseControl("targetButton", btn);
                        ddArrow.MouseOver();
                        TableData.TryClickElement(ddArrow);
                        Thread.Sleep(1000);
                        break;
                    }
                    catch
                    {
                        var ddArrow = new DlkBaseControl("targetButton", btn.FindElement(By.XPath("./parent::*")));
                        ddArrow.MouseOver();//hover container to show dropdown arrow
                        btn.Click();
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
            int i = 0; string iterationData = "";
            try
            {
                Initialize();
                List<String> lsExpectedResults = ExpectedResults.Split('~').ToList();

                //start from index 0 to avoid confusion on list index
                //adjustments made on functions that start with index 1 instead of 0 - GetCellValue, log messages
                //used TableRows to filter hidden rows since RowCount only returns total row count regardless if displayed or not
                //reverting to using RowCount and check if the row is displayed using the style attribute

                for (i = 0; i <= RowCount && i < lsExpectedResults.Count; i++)
                {
                    iterationData = lsExpectedResults[i];
                    string rowClass = TableData.GetRowAttribute((i + 1), "class");
                    string rowStyle = TableData.GetRowAttribute((i + 1), "style");
                    if (!rowClass.Equals("none") && !rowClass.Contains("hide") && !rowStyle.Contains("display: none"))
                    {
                        String ActResult = TableData.GetCellValue(i + 1, Col);
                        if (ActResult.Contains("\r\n"))
                        {
                            ActResult = ActResult.Replace("\r\n", "<br>");
                        }
                        if (ActResult != null)
                        {
                            DlkLogger.LogInfo("VerifyColumnData() process log: Found data [" + (i + 1).ToString() + "]: " + ActResult);
                            List<String> mRange = lsExpectedResults[i].Split(':').ToList();
                            if (mRange.Count == 1) // no range
                            {
                                double dResult = -1;
                                if (double.TryParse(mRange[0], out dResult)) // numerical comparison if applicable
                                {
                                    double dActResult = -1;
                                    if (double.TryParse(ActResult, out dActResult))
                                    {
                                        DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + (i + 1).ToString(), dResult, dActResult);
                                    }
                                    else // string compare
                                    {
                                        DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + (i + 1).ToString(), lsExpectedResults[i], ActResult.Trim());
                                    }
                                }
                                else // string compare
                                {
                                    DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + (i + 1).ToString(), lsExpectedResults[i], ActResult.Trim());
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
                        lsExpectedResults.Insert((i + 1), "");
                    }

                }
            }
            catch (ArgumentOutOfRangeException ix)
            {
                DlkLogger.LogInfo("VerifyColumnData() process log: Data not found [" + (i + 1).ToString() + "]: " + iterationData);
                throw ix;
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnData() failed." + ex.Message, ex);
            }
        }

        /// <summary>
        /// Verifies Column Data
        /// </summary>
        /// <param name="Col"></param> 
        /// <param name="ExpectedResult"></param>   
        /// <param name="IncludeInputValues"></param>       
        [Keyword("VerifyColumnDataContains", new String[] { "1|text|Expected Value|Sample Column Number",
                                                         "2|text|Expected Value|Sample Expected Value",
                                                               "3|text|Value|TRUE"})]
        public void VerifyColumnDataContains(String Col, String ExpectedResults)
        {
            int i = 0; string iterationData = "";
            try
            {
                Initialize();
                List<String> lsExpectedResults = ExpectedResults.Split('~').ToList();

                //start from index 0 to avoid confusion on list index
                //adjustments made on functions that start with index 1 instead of 0 - GetCellValue, log messages
                //used TableRows to filter hidden rows since RowCount only returns total row count regardless if displayed or not
                //reverting to using RowCount and check if the row is displayed using the style attribute

                for (i = 0; i <= RowCount && i < lsExpectedResults.Count; i++)
                {
                    iterationData = lsExpectedResults[i];
                    string rowClass = TableData.GetRowAttribute((i + 1), "class");
                    string rowStyle = TableData.GetRowAttribute((i + 1), "style");
                    if (!rowClass.Equals("none") && !rowClass.Contains("hide") && !rowStyle.Contains("display: none"))
                    {
                        String ActResult = TableData.GetCellValue(i + 1, Col);
                        if (ActResult.Contains("\r\n"))
                        {
                            ActResult = ActResult.Replace("\r\n", "<br>");
                        }
                        if (ActResult != null)
                        {
                            DlkLogger.LogInfo("VerifyColumnDataContains() process log: Found data [" + (i + 1).ToString() + "]: " + ActResult);
                            List<String> mRange = lsExpectedResults[i].Split(':').ToList();
                            if (mRange.Count == 1) // no range
                            {
                                DlkAssert.AssertEqual("Column compare. Col : " + Col + ", Row: " + (i + 1).ToString(), lsExpectedResults[i], ActResult, true);
                            }
                        }
                    }
                    else
                    {
                        lsExpectedResults.Insert((i + 1), "");
                    }

                }
            }
            catch (ArgumentOutOfRangeException ix)
            {
                DlkLogger.LogInfo("VerifyColumnDataContains() process log: Data not found [" + (i + 1).ToString() + "]: " + iterationData);
                throw ix;
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyColumnDataContains() failed." + ex.Message, ex);
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
            ColumnHeader = ColumnHeader.ToLower();
            SortOrder = SortOrder.ToLower();

            try
            {
                Initialize();
                IList<IWebElement> mHeaders = table.Body.ColumnHeaders;
                int mColIndex = table.Body.GetColumnHeaderIndex(ColumnHeader);
                DlkBaseControl ctlHeader = new DlkBaseControl("Header", mHeaders[mColIndex]);

                //in case GetColumnHeaderElements() returned span element which is obstructed
                if (!ctlHeader.mElement.Displayed && ctlHeader.mElement.TagName.ToLower() != "th")
                    ctlHeader.mElement = ctlHeader.mElement.FindElement(By.XPath("./ancestor::th"));
                ctlHeader.ScrollIntoViewUsingJavaScript(true);
                if (ctlHeader != null && ctlHeader.mElement.Displayed)
                {
                    ctlHeader.Click();
                    Thread.Sleep(1000);

                    //in case GetColumnHeaderElements return the 'span' elements instead of the 'th'

                    string currOrderClass = ctlHeader.mElement.GetAttribute("class").Contains("sort") ? ctlHeader.GetAttributeValue("class")
                            : ctlHeader.mElement.FindElement(By.XPath("./ancestor::th")).GetAttribute("class");

                    if (SortOrder == "descending")
                    {
                        if (currOrderClass.Contains("sortD"))
                        {
                            //do not re-sort, it's already Descending
                            DlkLogger.LogInfo("Column " + ColumnHeader + " is already in Descending order");
                        }
                        else
                        {
                            ctlHeader.Click();
                            DlkLogger.LogInfo("Column " + ColumnHeader + " re-sorted in Descending order");
                        }
                    }
                    else if (SortOrder == "ascending")
                    {
                        if (currOrderClass.Contains("sortA"))
                        {
                            DlkLogger.LogInfo("Column " + ColumnHeader + " is already in Ascending order");
                        }
                        else
                        {
                            ctlHeader.Click();
                            DlkLogger.LogInfo("Column " + ColumnHeader + " re-sorted in Ascending order");
                        }
                    }
                }
                else
                {
                    throw new Exception("SortColumn() failed: Column " + ColumnHeader + " was not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SortColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDateSort", new String[] { "1|text|Expected Value|Column Number",
                                                "2|text|Expected Value|Descending or Ascending",
                                                "3|text|Expected Value|True or False"})]
        public void VerifyDateSort(String ColumnName, String SortOrder, String TrueOrFalse)
        {
            try
            {
                Initialize();
                TableData.VerifyDateSort(ColumnName, SortOrder, TrueOrFalse);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDateSort() failed : " + e.Message, e);
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
                Boolean mExpectedResult = Boolean.Parse(ExpectedResult);
                Boolean? mActualResult = null;
                IWebElement mRow = TableData.GetRow(Int32.Parse(Row));
                IWebElement mCell = null;
                if (mRow.FindElements(By.XPath("./td[@class='Col resource-photo']//div[@class='gridImage']")).Where(item => item.Displayed).Count() > 0)
                {
                    mCell = mRow.FindElement(By.XPath("./td[@class='Col resource-photo']//div[@class='gridImage']"));
                }
                else if (mRow.FindElements(By.XPath(".//div[@class='treeImage']")).Where(item => item.Displayed).Count() > 0)
                {
                    mCell = mRow.FindElements(By.XPath(".//*[@src]")).Count > 0 ? mRow.FindElement(By.XPath(".//*[@src]"))
                        : mRow.FindElement(By.XPath(".//div[@class='treeImage']"));
                }
                else if (mRow.FindElements(By.XPath(".//*[contains(@class,'ag-photo-name-col')]/img")).Where(item => item.Displayed).Count() > 0)
                {
                    mCell = mRow.FindElement(By.XPath(".//*[contains(@class,'ag-photo-name-col')]/img"));
                }
                else if (mRow.FindElements(By.XPath(".//div[contains(@class,'photo-name')]//div[contains(@class,'no-image')]")).Where(item => item.Displayed).Count() > 0)
                {
                    DlkLogger.LogInfo("The element is a Table type");
                    mActualResult = false;
                    DlkLogger.LogInfo("No image was found");
                }
                else
                {
                    mActualResult = false;
                }

                if (mActualResult == null)
                {
                    if (mCell == null) throw new Exception("The table type is not yet supported.");

                    string mResourceImageURL = mCell.GetCssValue("background-image") != "none" ? mCell.GetCssValue("background-image") : mCell.GetAttribute("src");

                    mActualResult = !mResourceImageURL.ToLower().Contains(mDefaultResourceUrl.ToLower());

                    if (mResourceImageURL == "url(\"http://ashapp181vs/VisionRP/ngRP/images/RM_GenImage.png\")" || mResourceImageURL == "none")
                    {
                        mActualResult = false;
                    }
                }
                DlkAssert.AssertEqual("VerifyResourceImageExist():", mExpectedResult.ToString(), mActualResult.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyResourceImageExist() failed : " + ex.Message, ex);
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
            try
            {
                Initialize();
                int iRow = Convert.ToInt32(Row);
                List<String> lsRowData = GetDisplayedRowData(iRow, true);
                String RowData = "";
                foreach (var item in lsRowData)
                {
                    if (!String.IsNullOrWhiteSpace(item))
                    {
                        if (!String.IsNullOrWhiteSpace(RowData))
                        {
                            RowData += "~";
                        }
                        RowData += item.Trim().Replace("\r\n", " ").ToLower();
                    }
                }
                DlkAssert.AssertEqual("Row Data Compare: Row: " + iRow, ExpectedResults.ToLower(), RowData);

            }
            catch (Exception ex)
            {
                throw new Exception("VerifyRowData() failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Same function as DlkBaseTable.GetRowData but only returns the displayed data in the row
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private List<string> GetDisplayedRowData(int iRow, bool IncludeInputValues = false)
        {

            Initialize();
            List<String> rowData = new List<String>();
            IWebElement TableRow = mElement.FindElements(By.XPath(".//table[not(contains(@class,'rowTools'))][not(contains(@class,'hdrTable'))]//tr")).ToArray()[iRow - 1];

            // changed logic to check for "display: none" instead of using IWebElement.Displayed because they also want to verify rows that are "hidden" from their view
            IList<IWebElement> mElmCells = TableRow.FindElements(By.XPath(".//td")).Where(td => !td.GetAttribute("style").Contains("display: none")).ToList();
            if ((mElmCells == null) || (mElmCells.Count < 1))
            {
                mElmCells = TableRow.FindElements(By.XPath(".//th")).Where(td => !td.GetAttribute("style").Contains("display: none")).ToList();
            }
            foreach (IWebElement mCell in mElmCells)
            {
                new DlkBaseControl("Cell", mCell).ScrollIntoViewUsingJavaScript(true);
                bool includeInRowData = true;
                String mCellVal = mCell.Text.Trim();
                if (IncludeInputValues)
                {
                    if (mCellVal == "")
                    {
                        try
                        {
                            IWebElement mInputElm;
                            mInputElm = mCell.FindElement(By.TagName("input"));
                            mCellVal = mInputElm.GetAttribute("value");
                        }
                        catch
                        {
                            if (mCell.FindElements(By.XPath(".//div[contains(@class,'columnHdr')]")).Count() > 0)
                            {
                                IWebElement columnHeader = mCell.FindElement(By.XPath(".//div[contains(@class,'columnHdr')]"));
                                mCellVal = new DlkBaseControl("header that is not in view but not set to display: none", columnHeader).GetValue();
                            }
                            else
                            {
                                //no text value, do not include in row data
                                includeInRowData = false;
                            }
                        }
                    }
                }
                if (includeInRowData)
                    rowData.Add(mCellVal);
            }
            return rowData;
        }

        [Keyword("VerifyIconInDropdownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyIconInDropdownList(String ColumnHeader, String Row, String ItemName, String IconTitle, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement targetCell = TableData.GetCell(Convert.ToInt32(Row), ColumnHeader);
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                if (!list.Exists(1))
                {
                    targetCell.Click();
                    IWebElement mArrowDown = targetCell.FindElement(By.XPath(".//*[contains(@class,'tap-target')]"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }
                list.FindElement();
                IWebElement item = list.mElement.FindElement(By.XPath(".//span[text()='" + ItemName + "']"));
                bool iconExists = item.FindElements(By.XPath("./preceding-sibling::div[contains(@class, 'subTypeIcon')][@title='" + IconTitle + "']")).Count > 0;
                DlkAssert.AssertEqual("VerifyIconInDropdownList", Convert.ToBoolean(TrueOrFalse), iconExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIconInDropdownList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImageDimmed", new String[] { "1|text|Row|1",
                                                    "2|text|ExpectedResult|True"})]
        public void VerifyItemImageDimmed(String Row, String ExpectedResult)
        {
            try
            {
                Initialize();
                Boolean ExpectedValue = false;
                Boolean ActualValue;
                if (Boolean.TryParse(ExpectedResult, out ExpectedValue))
                {
                    IWebElement mRow = TableData.GetRow(Int32.Parse(Row));
                    DlkLogger.LogInfo("VerifyItemImageDimmed() Item row obtained.");
                    string sClass = mRow.GetAttribute("class").ToLower();

                    if (sClass.Contains("inactive") || sClass.Contains("disabled"))
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
                else if (sClass.Contains("over") || sClass.Contains("error"))
                {
                    ColorCode = "red";

                }
                else if ((!sClass.Contains("over")) && (!sClass.Contains("under")))
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

        /// <summary>
        /// Clicks the column header of the table that will match the supplied parameter
        /// </summary>
        /// <param name="ColumnHeader"></param>
        [Keyword("ClickColumnHeader")]
        public void ClickColumnHeader(String ColumnHeader)
        {
            Initialize();
            TableData.ClickColumnHeader(ColumnHeader);
        }

        [Keyword("VerifyItemInLineOption", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInLineOption(String Row, String Item, String TrueOrFalse)
        {
            try
            {
                bool ActualValue = false;
                string xpath_PopUpLabel = ".//li[not(contains(@class, 'disabled'))]//span[contains(@class,'popupLabel')]";
                Initialize();
                ClickDropdownArrow(Row);
                DlkBaseControl popup = new DlkBaseControl("PopUp", "CLASS_DISPLAY", "popupmenu");
                popup.FindElement();

                //get Enabled options
                ReadOnlyCollection<IWebElement> optionItems = popup.mElement.FindElements(By.XPath(xpath_PopUpLabel));
                foreach (IWebElement item in optionItems)
                {
                    if (item.Text.Trim() == Item)
                    {
                        ActualValue = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyItemInLineOption", Convert.ToBoolean(TrueOrFalse), ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInLineOption() failed : " + e.Message);
            }
        }

        [Keyword("VerifyHeaderButtonExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyHeaderButtonExists(String ColumnHeader, String TrueOrFalse)
        {
            try
            {
                bool ActualValue = false;
                Initialize();
                IWebElement columnHeader = TableData.GetColumnHeader(ColumnHeader);
                DlkBaseControl ctrlHeader = new DlkBaseControl("Header", columnHeader);
                ctrlHeader.MouseOver();

                if (columnHeader.FindElements(By.XPath(".//div[contains(@class, 'icon')]")).Count > 0)
                {
                    ActualValue = true;
                }

                DlkAssert.AssertEqual("VerifyHeaderButtonExists", Convert.ToBoolean(TrueOrFalse), ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeaderButtonExists() failed : " + e.Message);
            }
        }

        [Keyword("VerifyColumnHeaderValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyColumnHeaderValue(String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement columnHeader = TableData.GetColumnHeader(ColumnHeader);
                DlkAssert.AssertEqual("VerifyColumnHeaderValue", ExpectedValue.ToLower(), TableData.GetColumnHeaderValue(columnHeader, true).Trim().ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderValue() failed : " + e.Message);
            }
        }

        /// <summary>
        /// Keyword wil verify if search item doesn't have matching results. If no results were found, keyword returns true, otherwise returns false. This keyword is applicable only to search type dropdown cells.
        /// </summary>
        [Keyword("VerifySearchNoResults", new String[] { "1|text|Expected Value|ExampleValue" })]
        public void VerifySearchNoResults(String ColumnHeader, String Row, String SearchItem, String TrueOrFalse)
        {
            try
            {
                Initialize();
                var bIsNoResults = TableData.VerifySearchNoResults(int.Parse(Row), ColumnHeader, SearchItem, TrueOrFalse); // separated for readability
                DlkAssert.AssertEqual("VerifySearchNoResults()", Convert.ToBoolean(TrueOrFalse), bIsNoResults);
            }
            catch (Exception e)
            {
                throw new Exception("VerifySearchNoResults() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialList", new String[] { "1|text|ColumnHeader|Notes",
                                                    "2|text|Row|1",
                                                    "3|text|Expected|Item1~Item2" })]
        public void VerifyPartialList(String ColumnHeader, String Row, String Items)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(Row, out row)) throw new Exception("Row must be a valid number.");

                Initialize();
                IWebElement mCell = TableData.GetCell(row, ColumnHeader);
                DlkBaseControl tableCell = new DlkBaseControl("Cell", mCell);
                TableData.TryClickElement(tableCell);
                new DlkComboBox("ComboBox", mCell).VerifyPartialList(Items);
                DlkLogger.LogInfo("VerifyPartialList() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyFilterRowOpen", new String[] { "1|text|Value|True or False" })]
        public void VerifyFilterRowOpen(String TrueOrFalse)
        {
            try
            {
                Boolean ExpectedResult;
                Boolean ActualResult;
                if (!Boolean.TryParse(TrueOrFalse, out ExpectedResult))
                    throw new FormatException("VerifyFilterRowOpen() : Parameter supplied [TrueOrFalse] is not a valid boolean");

                Initialize();

                if (table.Header.FilterRow.Displayed)
                    ActualResult = table.Header.FilterRow.GetCssValue("display").ToLower().Trim() != "none" ? true : false;
                else
                    ActualResult = false;

                DlkAssert.AssertEqual("VerifyFilterRowOpen: ", ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyFilterRowOpen() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFilterRowOpen() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellInputMasked", new String[] { "1|text|Value|True or False" })]
        public void VerifyCellInputMasked(String Row, String ColumnHeader, String TrueOrFalse)
        {
            try
            {
                int row;
                Boolean ExpectedResult;
                Boolean ActualResult;

                if (!int.TryParse(Row, out row) || row == 0)
                    throw new Exception("VerifyCellInputMasked() : Parameter supplied [Row] is not a valid input number");

                if (!Boolean.TryParse(TrueOrFalse, out ExpectedResult))
                    throw new FormatException("VerifyCellInputMasked() : Parameter supplied [TrueOrFalse] is not a valid boolean");

                Initialize();
                IWebElement mCell = table.Body.GetCell(row, ColumnHeader);

                if (mCell.FindElements(By.XPath(".//input")).Count > 0)
                {
                    if (mCell.FindElements(By.XPath(".//input[contains(@type,'password')]")).Count > 0)
                        ActualResult = true;
                    else
                        ActualResult = false;
                }
                else
                    throw new Exception("Cell does not contain input type field");

                DlkAssert.AssertEqual("VerifyCellInputMasked: ", ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyCellInputMasked() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellInputMasked() failed : " + e.Message, e);
            }
        }

        [Keyword("FilterRowOpen", new String[] { "1|text|Value|SampleVar" })]
        public void FilterRowOpen(String VariableName)
        {
            try
            {
                Boolean ActualResult;

                Initialize();

                if (table.Header.FilterRow.Displayed)
                    ActualResult = table.Header.FilterRow.GetCssValue("display").ToLower().Trim() != "none" ? true : false;
                else
                    ActualResult = false;

                DlkVariable.SetVariable(VariableName, ActualResult.ToString());
                DlkLogger.LogInfo("FilterRowOpen() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("FilterRowOpen() failed : " + e.Message, e);
            }
        }

        [Keyword("ClearCellValue", new String[] { "1|text|Row|1",
                                                "2|text|ColumnHeader|Notes"})]
        public void ClearCellValue(string Row, string ColumnHeader)
        {
            Initialize();
            TableData.SetCellValue(int.Parse(Row), ColumnHeader, String.Empty);
        }
        #endregion
    }
}



