using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Interactions;
using System.Reflection.Emit;

namespace TEMobileLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        private const string SCRIPT_SHEET_NAME = "Script";
        private const string SCRIPT_OUTPUT_COLUMN_NAME = "Output";
        private const char DEFAULT_DELIMETER = '~';
        private const int ROW_HEADER_SELECTOR_COL_INDEX = -1;
        private const int HORIZONTAL_SCROLL_OFFSET = 3;
        private const int VERTICAL_SCROLL_OFFSET = 2;

        private String mstrHeaderClass = "hdcll";
        private String mstrRowClass = "dRw";
        private String mstrClickCellCommentOkButtonXPATH = "//input[@id='expandoOK']";
        private String mstrClickCellCommentCancelButtonXPATH = "//input[@id='expandoCancel']";
        private String mstrTableCheckbox = "./ancestor::form[1]/descendant::*[@id='selectAllImg']";
        private IList<string> mlstHeaderTexts;
        private List<IWebElement> mlstHeaders;
        private IList<IWebElement> mlstRows;
        private IWebElement mHScroll;
        private IWebElement mVScroll;

        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
            FindScrollBars();
        }

        [Keyword("GetTableRowWithColumnValue", new String[] {"1|text|Column Header|Line*", 
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public void GetTableRowWithColumnValue(String ColumnHeader, String Value, String VariableName)
        {
            try
            {
                Initialize();
                var row = GetRowsWithColumnValue(ColumnHeader, Value);

                if (row.value == null)
                    throw new Exception($"No row found with given ColumnHeader [{ColumnHeader}] and Value [{Value}].");

                DlkVariable.SetVariable(VariableName, row.index.ToString());
                ResetTableSelected();
                DlkLogger.LogInfo("Successfully executed GetTableRowWithColumnValue(). Value : " + row.index.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("GetTableRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableCellByRowColumn", new String[] {"1|text|Row|O{Row}", 
                                                            "2|text|Column Header|Line*"})]
        public void ClickTableCellByRowColumn(String Row, String ColumnHeader)
        {
            try
            {
                Initialize();

                int cellIndex = GetColumnIndexByHeaderName(ColumnHeader, false);
                var row = SetandGetSelectedRowByIndex(Convert.ToInt32(Row), true);
                var cell = GetCellByIndex(row, cellIndex);
                if (cell.value == null) throw new Exception("Cannot find cell.");

                cell.value.Click();
                DlkLogger.LogInfo("Successfully executed ClickTableCellByRowColumn()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCellByRowColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCurrSelectedRowCellByColumn", new String[] {"1|text|Row|O{Row}",
                                                            "2|text|Column Header|Line*"})]
        public void ClickCurrSelectedRowCellByColumn(String ColumnHeader)
        {
            try
            {
                Initialize();

                int cellIndex = GetColumnIndexByHeaderName(ColumnHeader, false);
                var rowContainers = GetRowContainers();
                var row = rowContainers.FirstOrDefault(r => r.GetAttribute("hilite").Contains("1"));
                var cell = GetCellByIndex(row, cellIndex);
                if (cell.value == null) throw new Exception("Cannot find cell.");

                cell.value.Click();
                DlkLogger.LogInfo("Successfully executed ClickCurrSelectedRowCellByColumn()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCurrSelectedRowCellByColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickTableRowHeader", new String[] { "1|text|Row|O{Row}" })]
        public void DoubleClickTableRowHeader(String Row)
        {
            try
            {
                Initialize();

                IWebElement rowHeader = GetRowHeaderCell(Convert.ToInt32(Row));
                if (rowHeader == null)throw new Exception("Row header cell not found");
                
                var browser = DlkEnvironment.mBrowser.ToLower();

                browser = browser.Contains("android") ? "android"
                    : browser.Contains("ios") ? "ios"
                    : browser;

                switch (browser.ToLower())
                {
                    case "chrome":
                    case "android":
                        DlkBaseControl ctlRowHeader = new DlkBaseControl("RowHeader", rowHeader);
                        ctlRowHeader.DoubleClick();
                        break;
                    case "safari":
                    case "ios":
                    case "iphone":
                        try
                        {
                            rowHeader.Click();
                            rowHeader.Click();
                        }
                        catch { }
                        break;
                    default:
                        throw new Exception($"Browser [{browser}] is not yet supported.");
                }

                DlkLogger.LogInfo("Successfully executed DoubleClickTableRowHeader()");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickTableRowHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableCheckBox", new String[] { "" })]
        public void ClickTableCheckBox()
        {
            try
            {
                Initialize();

                IWebElement tableCheckBox = mElement.FindElement(By.XPath(mstrTableCheckbox));
                tableCheckBox.Click();
                DlkLogger.LogInfo("Successfully executed Click()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCheckBox() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTableCellValue", new String[] {"1|text|Row|O{Row}", 
                                                    "2|text|Column Header|Line*",
                                                    "3|text|Value|Sample Value"})]
        public void SetTableCellValue(String Row, String ColumnHeader, String Value)
        {
            try
            {
                Initialize();
                ResetHScroll();
                int intColIndex = GetColumnIndexByHeaderName(ColumnHeader, false, false);
                if (intColIndex != -1)
                {
                    var row = SetandGetSelectedRowByIndex(Convert.ToInt32(Row), true);
                    IWebElement cell = GetCellByIndex(row, intColIndex).value;
                    IWebElement inputControl = GetInputControl(cell);
                    if (inputControl == null)
                    {
                        throw new Exception("Cell does not contain an input control");
                    }
                    else
                    {
                        String strInputControlClass = inputControl.GetAttribute("class").ToLower();
                        String strInputControlID = inputControl.GetAttribute("id");

                        switch (strInputControlClass)
                        {
                            case "tdfro":
                            case "tdfrq":
                            case "tdf":
                            case "tdfrqnum":
                            case "tdfronum":
                            case "tdfnum":
                                while (!inputControl.Displayed)
                                {
                                    HScrollSingleRight();
                                }
                                DlkTextBox textCell = new DlkTextBox("Text Cell", this, "ID", strInputControlID);
                                try
                                {
                                    if (cell.FindElements(By.XPath("./following-sibling::*[1]")).Count == 0)
                                    {
                                        textCell.Set(Value);
                                        break;
                                    }
                                }
                                catch
                                {
                                    // do nothing
                                }
                                try
                                {
                                    textCell.Set(Value);
                                }
                                catch (Exception e)
                                {
                                    if (e.Message.Contains("element click intercepted"))
                                    {
                                        DlkLogger.LogInfo("Cell might only be partially visible - retrying Set() after scrolling");
                                        HScrollSingleRight();
                                        textCell.Set(Value);
                                    }
                                }
                                break;
                            case "tcb":
                                DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", "ID", strInputControlID);
                                chkCell.Set(Value);
                                break;
                            case "tccbt":
                            case "tccbtb":
                                if (inputControl.FindElements(By.XPath("./following-sibling::span")).Any())
                                {
                                    IWebElement cboArrow = inputControl.FindElement(By.XPath("./following-sibling::span"));
                                    while (!cboArrow.Displayed)
                                    {
                                        HScrollSingleRight();
                                    }
                                }
                                DlkComboBox cboCell = new DlkComboBox("Combo Cell", inputControl);
                                cboCell.SelectDropdownValue(Value, false);
                                break;
                            default:
                                throw new Exception("Cell input control = '" + strInputControlClass + "' is not recognized");
                        }
                        ResetHScroll();
                        DlkLogger.LogInfo("Successfully executed SetTableCellValue()");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SetTableCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellValue", new String[] {"1|text|Row|O{Row}", 
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|Sample Value"})]
        public void VerifyTableCellValue(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();
                int intColIndex = GetColumnIndexByHeaderName(ColumnHeader, false, false);
                if (intColIndex != -1)
                {
                    var row = SetandGetSelectedRowByIndex(Convert.ToInt32(Row), true);
                    IWebElement cell = GetCellByIndex(row, intColIndex).value;
                    IWebElement inputControl = GetInputControl(cell);
                    if (inputControl == null)
                    {
                        throw new Exception("Cell does not contain an input control");
                    }
                    else
                    {
                        String strInputControlClass = inputControl.GetAttribute("class").ToLower();
                        String strInputControlID = inputControl.GetAttribute("id");

                        switch (strInputControlClass)
                        {
                            case "tdfro":
                            case "tdfrq":
                            case "tdf":
                            case "tdfrqnum":
                            case "tdfronum":
                            case "tdfnum":
                            case "cllro":
                                DlkTextBox textCell = new DlkTextBox("Text Cell", inputControl);
                                textCell.VerifyText(ExpectedValue);                                              
                                break;

                            case "tcb":
                                DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", "ID", strInputControlID);
                                chkCell.VerifyValue(ExpectedValue);
                                break;

                            case "tccbt":
                            case "tccbtb":
                                DlkComboBox cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID);
                                cboCell.VerifyValue(ExpectedValue);
                                break;
                            default:
                                throw new Exception("Cell input control = '" + strInputControlClass + "' is not recognized");
                        }
                        ResetHScroll();
                        DlkLogger.LogInfo("Successfully executed VerifyTableCellValue()");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellValue() failed : " + e.Message, e);
            }
        }

        

        [Keyword("VerifyRowExistWithColumnValue")]
        public void VerifyRowExistWithColumnValue(String ColumnHeader, String Value, String ExpectedValue)
        {
            try
            {
                Initialize();
                var row = GetRowsWithColumnValue(ColumnHeader, Value);
                var isFound = row.value != null;

                DlkAssert.AssertEqual("VerifyRowExistWithColumnValue():", Convert.ToBoolean(ExpectedValue), isFound);

                DlkLogger.LogInfo("Successfully executed VerifyRowExistWithColumnValue()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowExistWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowExistWithMultipleColumnValue")]
        public void VerifyRowExistWithMultipleColumnValue(String ColumnHeaders, String Values, String VariableName)
        {
            
        }

        [Keyword("VerifyTableRowExistWithColumnValue", new String[] {"1|text|Column Header|Sample Column Header",
                                                                    "2|text|Value|Sample Value",
                                                                    "3|text|Expected Value|True or False"})]
        public void VerifyTableRowExistWithColumnValue(String ColumnHeader, String Value, String ExpectedValue)
        {
            try
            {
                Initialize();
                var rows = GetRowContainers(false, true);
                if (rows.Count() == 0)
                {
                    DlkAssert.AssertEqual("VerifyTableRowExistWithColumnValue():", Convert.ToBoolean(ExpectedValue), false);
                    DlkLogger.LogInfo("Successfully executed VerifyTableRowExistWithColumnValue()");
                    return;
                }
                var row = GetRowsWithColumnValue(ColumnHeader, Value);
                var isFound = row.value != null;

                DlkAssert.AssertEqual("VerifyTableRowExistWithColumnValue():", Convert.ToBoolean(ExpectedValue), isFound);

                DlkLogger.LogInfo("Successfully executed VerifyTableRowExistWithColumnValue()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableRowExistWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableRowCount", new String[] { "1|text|Expected Row Count|0" })]
        public void VerifyTableRowCount(String ExpectedRowCount)
        {
            try
            {
                int iExpRowCount = Convert.ToInt32(ExpectedRowCount);
                int iActRowCount = 0;
                Initialize();
                bool isEmpty = iExpRowCount == 0;
                var rowContainers = GetRowContainers(isEmpty:isEmpty);

                if (!isEmpty)
                {
                    ResetTableSelected(rowContainers);
                    string selected = string.Empty;
                    bool isLast = false;

                    while (!isLast)
                    {
                        // Will get current selected row
                        var selectedRow = rowContainers.FirstOrDefault(r => r.GetAttribute("hilite").Contains("1"));
                        if (selectedRow == null) throw new Exception("Selected row not found.");
                        var currSelected = selectedRow.Text;
                        // If true, means end of table row
                        if (selected == currSelected) isLast = true;
                        else
                        {
                            iActRowCount++;
                            selected = currSelected;
                        }
                        // Move to next row
                        var browser = DlkEnvironment.mBrowser;
                        if (browser.ToLower().Contains("android")) browser = "android";
                        else if (browser.ToLower().Contains("ios")) browser = "ios";
                        switch (browser.ToLower())
                        {
                            case "safari":
                            case "ios":
                                rowContainers.ElementAt(0).SendKeys(Keys.ArrowDown);
                                break;
                            case "chrome":
                            case "android":
                                new Actions(DlkEnvironment.AutoDriver)
                                    .MoveToElement(rowContainers.ElementAt(0))
                                    .SendKeys(Keys.ArrowDown).Perform();
                                break;

                            default:
                                throw new Exception($"Browser [{browser}] is not yet supported.");
                        }

                    }
                }
                                
                DlkAssert.AssertEqual("VerifyTableRowCount() : Table Row Count", iExpRowCount, iActRowCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExist", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExist(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExist() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExist() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellReadOnly", new String[] {"1|text|Row|O{Row}", 
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|TRUE"})]
        public void VerifyTableCellReadOnly(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
                    String cellClass = cell.GetAttribute("class").ToString().ToLower();
                    DlkAssert.AssertEqual("VerifyTableCellReadOnly()", Convert.ToBoolean(ExpectedValue), cellClass.Equals("cllro"));
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellReadOnly() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyTableColumnHeaders", new String[] { "1|text|Expected header texts|Header1~Header2~Header3" })]
        public void VerifyTableColumnHeaders(String ExpectedHeaders)
        {
            String strActualHeaders = String.Empty;
            String strDelimeter = String.Empty;
            List<IWebElement> mlstHeadersBeforeScroll;
            IList<string> mlstHeadersFullList;
            String sHeader = "";
            String sHeaderFinal = "";

            mlstHeadersFullList = new List<String>();

            try
            {
                Initialize();
                if (mHScroll != null)
                {
                    //reset horizontal scroll bar to make sure we cover all headers
                    HScrollStart();
                }
                RefreshHeaders();
                mlstHeadersBeforeScroll = mlstHeaders;

                foreach (IWebElement hdrName in mlstHeadersBeforeScroll)
                {
                    String headerText = GetColumnHeaderText(hdrName);
                    if (headerText.Contains("<div"))
                    {
                        headerText = RemoveHTMLTagsInHeader(headerText);
                    }
                    mlstHeadersFullList.Add(headerText);
                }

                if (mHScroll != null)
                {
                    //Scroll to view the hidden columns and append the newly visible ones to the main list
                    if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
                    {
                        IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                        IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                        DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                        double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                        double trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                        double rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());

                        while (mlstHeaderTexts.Contains(GetColumnHeaderText(mlstHeaders[mlstHeaders.Count - 1])) && trackLeft + trackWidth + 3 < rightBtnLeft)
                        {
                            rightBtnControl.Click();
                            RefreshHeaders();
                            rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                            trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                            trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                            rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());

                            foreach (IWebElement hdrName in mlstHeaders)
                            {
                                sHeader = GetColumnHeaderText(hdrName);
                                sHeaderFinal = SplitCamelCaseInHeader(sHeader);
                                if (sHeaderFinal.Contains("<div"))
                                {
                                    sHeaderFinal = RemoveHTMLTagsInHeader(sHeaderFinal);
                                }
                                if (mlstHeadersFullList[mlstHeadersFullList.Count - 1] == sHeaderFinal.Replace("*", ""))
                                {
                                    mlstHeadersFullList[mlstHeadersFullList.Count - 1] = sHeaderFinal;
                                }
                                else if (!mlstHeadersFullList.Contains(sHeaderFinal))
                                {
                                    mlstHeadersFullList.Add(sHeaderFinal);
                                }
                            }
                        }
                    }
                }
                
                foreach (IWebElement hdrName in mlstHeaders)
                {
                    sHeader = GetColumnHeaderText(hdrName);
                    sHeaderFinal = SplitCamelCaseInHeader(sHeader);
                    if (sHeaderFinal.Contains("<div"))
                    {
                        sHeaderFinal = RemoveHTMLTagsInHeader(sHeaderFinal);
                    }
                    if (mlstHeadersFullList[mlstHeadersFullList.Count - 1] == sHeaderFinal.Replace("*", ""))
                    {
                        mlstHeadersFullList[mlstHeadersFullList.Count - 1] = sHeaderFinal;
                    }
                    else if (!mlstHeadersFullList.Contains(sHeaderFinal))
                    {
                        mlstHeadersFullList.Add(sHeaderFinal);
                    }
                }

                foreach (String hdr in mlstHeadersFullList)
                {
                    strActualHeaders += (strDelimeter + hdr);
                    strDelimeter = DEFAULT_DELIMETER.ToString();
                }

                DlkAssert.AssertEqual("VerifyTableColumnHeaders()", ExpectedHeaders.ToLower(), strActualHeaders.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableColumnHeaders() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellList", new String[] {"1|text|Row|O{Row}", 
                                                      "2|text|Column Header|Line*",
                                                      "3|text|Expected Values|Item1~Item2~Item3"})]
        public void VerifyTableCellList(String Row, String ColumnHeader, String ExpectedValues)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);

                if (intColIndex != -1)
                {
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex));
                    String strInputControlClass = inputControl.GetAttribute("class").ToLower();
                    String strInputControlID = inputControl.GetAttribute("id");

                    if (inputControl == null || !(strInputControlClass.Equals("tccbtb")))
                    {
                        throw new Exception("Cell does not contain a list");
                    }

                    DlkComboBox cboCell = new DlkComboBox("Combo Cell", this, "ID", strInputControlID);
                    String comboBoxItems = string.Empty;
                    cboCell.DisplayComboBoxList(cboCell.GetText(), 3);
                    comboBoxItems = cboCell.ComboBoxList.GetAllItemsWithDelimiter();
                    cboCell.CollapseDropDownList();
                    DlkAssert.AssertEqual("VerifyTableCellList()", ExpectedValues, DlkString.NormalizeNonBreakingSpace(comboBoxItems));
                    DlkLogger.LogInfo("VerifyTableCellList() passed");
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInTableCellList", new String[] {"1|text|Row|O{Row}", 
                                                                 "2|text|Column Header|Line*",
                                                                 "3|text|Searched Value|Sample Value",
                                                                 "4|text|Expected Value|TRUE"})]
        public void VerifyAvailableInTableCellList(String Row, String ColumnHeader, String SearchedValue, String ExpectedValue)
        {
            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);

                if (intColIndex != -1)
                {
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex));
                    String strInputControlClass = inputControl.GetAttribute("class").ToLower();
                    String strInputControlID = inputControl.GetAttribute("id");

                    if (inputControl == null || !(strInputControlClass.Equals("tccbtb")))
                    {
                        throw new Exception("Cell does not contain a list");
                    }

                    DlkComboBox cboCell = new DlkComboBox("Combo Cell", this, "ID", strInputControlID);
                    String comboBoxItems = string.Empty;
                    cboCell.DisplayComboBoxList(cboCell.GetText(), 3);
                    comboBoxItems = cboCell.ComboBoxList.GetAllItemsWithDelimiter();
                    cboCell.CollapseDropDownList();
                    DlkAssert.AssertEqual("VerifyAvailableInTableCellList()", Convert.ToBoolean(ExpectedValue),
                        DlkString.NormalizeNonBreakingSpace(comboBoxItems).Split('~').Contains(SearchedValue));
                    DlkLogger.LogInfo("VerifyAvailableInTableCellList() passed");
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInTableCellList() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTableRowWithMultipleColumnValues", new String[] {"1|text|Column Headers|Header1~Header2~Header3", 
                                                                      "2|text|Values|Value1~Value2~Value3",
                                                                        "3|text|VariableName|MyRow"})]
        public void GetTableRowWithMultipleColumnValues(String ColumnHeaders, String Values, String VariableName)
        {
            try
            {
                Initialize();
                var row = GetRowsWithColumnValue(ColumnHeaders, Values);

                if (row.value == null) 
                    throw new Exception($"No row found with given ColumnHeaders [{ColumnHeaders}] and Values [{Values}].");

                DlkVariable.SetVariable(VariableName, row.index.ToString());
                DlkLogger.LogInfo("Successfully executed GetTableRowWithMultipleColumnValues()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowExistWithMultipleColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableCellButtonByRowColumn", new String[] {"1|text|Row|O{Row}", 
                                                                  "2|text|Column Header|Line*",
                                                                  "3|text|Button name|Expand"})]
        public void ClickTableCellButtonByRowColumn(String Row, String ColumnHeader, String ButtonName)
        {
            try
            {
                Initialize();
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex));

                    if (inputControl == null)
                    {
                        throw new Exception("Cell does not contain an input control");
                    }
                    inputControl.Click();
                    IWebElement target = inputControl.FindWebElementCoalesce(By.XPath("./following-sibling::*[contains(@title, '" + ButtonName + "')]"),
                        By.XPath("./following-sibling::*[contains(@class, '" + ButtonName + "')]"));

                    if (target == null)
                    {
                        throw new Exception("Control = '" + ButtonName + "' not found within table cell");
                    }
                    target.Click();
                    DlkLogger.LogInfo("Successfully executed ClickTableCellButtonByRowColumn()");
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCellButtonByRowColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("SetTableCellCommentPopupValue", new String[] { "1|text|Comment|Sample Comment" })]
        public void SetTableCellCommentPopupValue(String sCommentText)
        {
            try
            {
                DlkTextArea txtTableComment = new DlkTextArea("TextAreaComment", "ID", "expandoEdit");
                txtTableComment.Set(sCommentText);
            }
            catch (Exception e)
            {
                throw new Exception("SetTableCellCommentPopupValue() failed " + e.Message, e);
            }
        }

        [Keyword("ClickTableCellCommentPopupButton", new String[] { "3|text|Button Name|Ok or Cancel" })]
        public void ClickTableCellCommentPopupButton(String sButtonCaption)
        {
            try
            {
                Initialize();
                DlkBaseControl btnControl = null;
                if (sButtonCaption.ToLower() == "ok")
                {
                    btnControl = new DlkBaseControl("OK Button", mElement.FindElement(By.XPath(mstrClickCellCommentOkButtonXPATH)));
                }
                else if (sButtonCaption.ToLower() == "cancel")
                {
                    btnControl = new DlkBaseControl("Cancel Button", mElement.FindElement(By.XPath(mstrClickCellCommentCancelButtonXPATH)));
                }

                if (!btnControl.Exists() || btnControl == null)
                {
                    throw new Exception("Button control: " + sButtonCaption + " not found");
                }
                else
                {
                    btnControl.Click();
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCellCommentPopupButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellCommentPopupValue", new String[] { "1|text|Comment|Sample Comment" })]
        public void VerifyTableCellCommentPopupValue(String sCommentText)
        {
            try
            {
                DlkTextArea txtTableComment = new DlkTextArea("TextAreaComment", "ID", "expandoEdit");
                txtTableComment.VerifyText(sCommentText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellCommentPopupValue() failed " + e.Message, e);
            }
        }

        [Keyword("ClickTableRowHeaderCellButton", new String[] { "1|text|Row|O{Row}",
                                                                "2|text|Button name|Sample Comment"})]
        public void ClickTableRowHeaderCellButton(String Row, String ButtonName)
        {
            try
            {
                Initialize();

                IWebElement rowHeader = GetRowHeaderCell(Convert.ToInt32(Row));
                if (rowHeader == null)
                {
                    throw new Exception("Row header cell not found");
                }
                rowHeader.Click();

                IWebElement inputControl = GetInputControl(rowHeader);
                DlkBaseControl inputCtrl = new DlkBaseControl("Comment", inputControl);
                if (inputControl == null)
                {
                    throw new Exception("Cell does not contain an input control");
                }
                inputCtrl.ClickAndHold();

                if (inputControl.FindElements(By.XPath("./following-sibling::*[contains(@title, '" + ButtonName + "') or contains(@class, '" + ButtonName + "')]")).Count < 1)
                {
                    throw new Exception("Control = '" + ButtonName + "' not found within table cell");
                }


                DlkButton btnTextBoxButton = new DlkButton("TextBoxButton",
                    inputControl.FindElements(By.XPath("./following-sibling::*[contains(@title, '" + ButtonName + "') or contains(@class, '" + ButtonName + "')]")).First());

                if (!btnTextBoxButton.Exists())
                {
                    throw new Exception("Control = '" + ButtonName + "' not found within table cell");
                }
                btnTextBoxButton.Click();
                DlkLogger.LogInfo("Successfully executed ClickTableRowHeaderCellButton()");

            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeaderCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableRowHeader", new String[] { "1|text|Row|O{Row}" })]
        public void ClickTableRowHeader(String Row)
        {
            try
            {
                Initialize();
                var rowContainers = GetRowContainers();
                var selectedRow = SetandGetSelectedRowByIndex(Convert.ToInt32(Row), false, rowContainers);
                var rowHeader = selectedRow.FindElements(By.XPath("./*[contains(@class, 'cllfrst')]")).FirstOrDefault(rh => rh.Displayed);
                if (rowHeader == null) throw new Exception("Row header cell not found");
                new DlkBaseControl("RowHeader", rowHeader).ClickUsingJavaScript(false);
                DlkLogger.LogInfo("Successfully executed ClickTableRowHeader()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTableRowHeaderButton", new String[] { "1|text|Row|O{Row}" })]
        public void ClickTableRowHeaderButton(String Row)
        {
            try
            {
                if (!int.TryParse(Row, out int rowIndex)) throw new Exception($"The value of row [{Row}] is invalid.");

                Initialize();
                var rowContainers = GetRowContainers();
                var selectedRow = SetandGetSelectedRowByIndex(rowIndex, false, rowContainers);
                var rowHeaderButton = selectedRow.FindElements(By.XPath("./*[contains(@class, 'cllfrst')]/button")).FirstOrDefault(rh => rh.Displayed);
                if (rowHeaderButton == null) throw new Exception("Row header Button not found");

                new DlkBaseControl("RowHeaderButton", rowHeaderButton).ClickUsingJavaScript(false);
                DlkLogger.LogInfo("Successfully executed ClickTableRowHeaderButton()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeaderButton() failed : " + e.Message, e);
            }
        }

        [Keyword("RightClickColumnHeader", new String[] { "1|text|ColumnName|ColumnName" })]
        public void RightClickColumnHeader(String ColumnName)
        {
            try {
                Initialize();
                RefreshHeaders();
                bool hasColumn = false;
                for (int i = 0; i < mlstHeaders.Count; i++)
                {
                    if (ColumnName == GetColumnHeaderText(mlstHeaders[i]))
                    {
                        hasColumn = true;
                        IWebElement columnHeader = mlstHeaders[i];
                        Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                        mAction.ContextClick(columnHeader);
                        mAction.Perform();
                        DlkLogger.LogInfo("Successfully executed RightClickColumnHeader(): " + ColumnName);
                        break;
                    }
                }
                if (hasColumn == false)
                {
                    throw new Exception("Column header " + ColumnName + " not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTableCellValue", new String[] {"1|text|Row|O{Row}", 
                                                     "2|text|Column Header|Account*",
                                                     "3|text|VariableName|MyCell"})]
        public void GetTableCellValue(String Row, String ColumnHeader, String VariableName)
        {
            String cellValue = String.Empty;

            try
            {
                Initialize();

                int intColIndex = GetColumnIndexByHeaderName(ColumnHeader, false);
                if (intColIndex != -1)
                {
                    var row = SetandGetSelectedRowByIndex(Convert.ToInt32(Row), true);
                    IWebElement cell = GetCellByIndex(row, intColIndex).value;
                    IWebElement inputControl = GetInputControl(cell);
                    if (inputControl == null)
                    {
                        throw new Exception("Cell does not contain an input control");
                    }
                    else
                    {
                        String strInputControlClass = inputControl.GetAttribute("class").ToLower();
                        String strInputControlID = inputControl.GetAttribute("id");

                        switch (strInputControlClass)
                        {
                            case "tdfro":
                            case "tdfrq":
                            case "tdf":
                            case "tdfrqnum":
                            case "tdfronum":
                            case "tdfnum":
                                DlkTextBox textCell = new DlkTextBox("Text Cell", this, "ID", strInputControlID);
                                cellValue = textCell.GetValue();
                                break;
                            case "tcb":
                                DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", "ID", strInputControlID);
                                cellValue = chkCell.GetValue();
                                break;
                            case "tccbt":
                            case "tccbtb":
                                DlkComboBox cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID);
                                cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID, false);
                                cellValue = cboCell.GetText();
                                break;
                            default:
                                throw new Exception("Cell input control = '" + strInputControlClass + "' is not recognized");
                        }
                        DlkVariable.SetVariable(VariableName, cellValue);
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
                ResetHScroll();
                DlkLogger.LogInfo("Successfully executed GetTableCellValue(). Value : " + cellValue);
            }
            catch (Exception e)
            {
                throw new Exception("GetTableCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyHighlightedRowValue", new String[] {"1|text|Column Header|Account", 
                                                        "2|text|Expected Value|01200-010"})]
        public void VerifyHighlightedRowValue(String ColumnHeader, String ExpectedValue)
        {
            int intColIndex = -1;

            try
            {
                Initialize();
                bool bContinue = true;

                intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    VScrollStart("existing");
                    VScrollStart("new");

                    RefreshRows();
                    int i = 0;
                    while (bContinue)
                    {
                        for (i = 0; i < mlstRows.Count; i++)
                        {
                            string rowStyle = mlstRows[i].GetAttribute("style");

                            // Checks if current row is highlighted. If yes, proceed to get the cell to verify the value
                            if (rowStyle.Contains("border-color: rgb(0, 140, 186)"))
                            {
                                bContinue = false;
                                IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(i + 1), intColIndex));
                                if (inputControl == null)
                                {
                                    throw new Exception("Cell does not contain an input control");
                                }
                                else
                                {
                                    String strInputControlClass = inputControl.GetAttribute("class").ToLower();
                                    String strInputControlID = inputControl.GetAttribute("id");

                                    switch (strInputControlClass)
                                    {
                                        case "tdfro":
                                        case "tdfrq":
                                        case "tdf":
                                        case "tdfrqnum":
                                        case "tdfronum":
                                        case "tdfnum":
                                            DlkTextBox textCell = new DlkTextBox("Text Cell", inputControl);
                                            textCell.VerifyText(ExpectedValue);
                                            break;

                                        case "tcb":
                                            DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", "ID", strInputControlID);
                                            chkCell.VerifyValue(ExpectedValue);
                                            break;

                                        case "tccbt":
                                        case "tccbtb":
                                            DlkComboBox cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID);
                                            cboCell.VerifyValue(ExpectedValue);
                                            break;
                                        default:
                                            throw new Exception("Cell input control = '" + strInputControlClass + "' is not recognized");
                                    }
                                    DlkLogger.LogInfo("Successfully executed VerifyHighlightedRowValue()");
                                }
                            }
                            else
                                continue;
                        }
                        if (bContinue)
                        {
                            if (VScrollEnd("existing") && VScrollEnd("new"))
                            {
                                bContinue = false;
                            }
                            else
                            {
                                VScrollDown("existing");
                                VScrollDown("new");
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHighlightedRowValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetColumnHeaderValue", new String[] {"1|text|ColumnIndex|0", 
                                                       "2|text|VariableName|MyVar"})]
        public void GetColumnHeaderValue(String ColumnIndex, String VariableName)
        {
            try
            {
                int iColIndex = Convert.ToInt32(ColumnIndex);
                Initialize();
                RefreshHeaders();
                if (iColIndex >= mlstHeaderTexts.Count)
                {
                    throw new Exception("Column index " + iColIndex + " out of bounds of header indices");
                }
                DlkVariable.SetVariable(VariableName, mlstHeaderTexts[iColIndex]);
                DlkLogger.LogInfo("Successfully executed GetColumnHeaderValue(). Value obtained: " + mlstHeaderTexts[iColIndex]);
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        #region Private Methods

        private Row GetRowsWithColumnValue(String ColumnHeaders, String Values)
        {
            try
            {
                IWebElement Row = null;

                var colIndexes = GetColumnIndexByHeaderName(ColumnHeaders.Split(DEFAULT_DELIMETER).ToList(), true);

                var values = Values.Split(DEFAULT_DELIMETER);

                if (values.Count() != colIndexes.Count) throw new Exception("ColumnHeaders and Values should have the same item count.");

                ResetTableSelected();
                var rowContainers = GetRowContainers(true);
                var RCCount = rowContainers.Count;

                bool isFound = false;
                string selected = string.Empty;
                int rowIndex = 0;
                List<string> rowsContent = new List<string>();

                while (!isFound)
                {
                    rowContainers = GetRowContainers();
                    RCCount = rowContainers.Count;
                    // Will get current selected row
                    var selectedRow = rowContainers.FirstOrDefault(r => r.GetAttribute("hilite").Contains("1"));
                    if (selectedRow == null) throw new Exception("Selected row not found.");
                    var currSelected = selectedRow.Text;

                    // If true, means end of table row
                    if (selected == currSelected && rowIndex > 0) break;
                    // else, set new selected row
                    else selected = currSelected;

                    List<string> currenttRowsContent = new List<string>();

                    // search if values exist in current set of rows
                    foreach (var rc in rowContainers)
                    {
                        if (!rowsContent.Contains(rc.Text) || rowsContent == null) rowIndex++;
                        currenttRowsContent.Add(rc.Text);

                        List<bool> matchFound = new List<bool>();
                        foreach (var ci in colIndexes.Select((value, index) => new { index, value }))
                        {
                            var cell = GetCellByIndex(rc, ci.value);
                            matchFound.Add(cell.value.Text.Trim() == values.ElementAt(ci.index));
                            if(cell.isHScrollChanged) ResetHScroll();
                        }
                        if (!matchFound.Contains(false))
                        {
                            Row = rc;
                            isFound = true;
                            break;
                        }
                        
                    };

                    rowsContent = currenttRowsContent;

                    // Move to next set of rows
                    if (!isFound) MoveSendKeys(rowContainers, isZeroBasedIndex: true);
                }
                
                if (!isFound) DlkLogger.LogInfo($"No row found with given column values [{Values}].");

                return new Row { value = Row, index = rowIndex };
            }
            catch (Exception e)
            {
                throw new Exception($"GetRowsWithColumnValue(): {e.Message}");
            }
        }

        public List<IWebElement> GetRowContainers(bool toPositionLast = false, bool isEmpty = false)
        {
            try
            {
                var rowContainers = mElement.FindElements(By.XPath(".//*[@id='NEW' or @id='EXSTNG']/div")).Where(s => s.Displayed).ToList();
                if (!isEmpty && rowContainers.Count < 1) throw new Exception("No rows found.");

                // Try to cllick any header to set focus on current table
                var header = mElement.FindElements(By.XPath(".//*[contains(@class, 'hdcll')][2]")).FirstOrDefault(s => s.Displayed);
                if (header != null) header.Click();

                if (toPositionLast) MoveSendKeys(rowContainers);

                return rowContainers;
            }
            catch (Exception e)
            {
                throw new Exception($"GetRowContainers(): {e.Message}");
            }
        }
        private void MoveSendKeys(List<IWebElement> rowContainers, bool isDown = true, bool isZeroBasedIndex = false, int rowIndex = -1)
        {
            var browser = DlkEnvironment.mBrowser;
            var RCCount = rowContainers.Count;
            var keyValue = isDown ? Keys.ArrowDown : Keys.ArrowUp;

            if (browser.ToLower().Contains("android")) browser = "android";
            else if (browser.ToLower().Contains("ios")) browser = "ios";

            switch (browser.ToLower())
            {
                case "chrome":
                case "android":
                    var move = new Actions(DlkEnvironment.AutoDriver).MoveToElement(rowContainers.ElementAt(0));
                    if (rowIndex > -1)
                        for (var i = 0; i < rowIndex - 1; i++) move.SendKeys(keyValue);
                    else if (!isZeroBasedIndex) 
                        for (var i = 1; i < RCCount; i++) move.SendKeys(keyValue);
                    else 
                        foreach (var row in rowContainers) move.SendKeys(keyValue);
                    move.Perform();
                    break;
                case "safari":
                case "ios":
                case "iphone":
                    if (rowIndex > -1)
                        for (var i = 0; i < rowIndex - 1; i++) rowContainers.ElementAt(0).SendKeys(keyValue);
                    else if (!isZeroBasedIndex) 
                        for (var i = 1; i < RCCount; i++) rowContainers.ElementAt(0).SendKeys(keyValue);
                    else 
                        foreach (var row in rowContainers) rowContainers.ElementAt(0).SendKeys(keyValue);
                    break;
                default:
                    throw new Exception($"Browser [{browser}] is not yet supported.");
            }
        }

        public IWebElement SetandGetSelectedRowByIndex(int rowIndex, bool skipRowReset, List<IWebElement> rowContainers = null)
        {
            try
            {
                if (!skipRowReset)
                {
                    ResetTableSelected();
                }
                if(rowContainers == null) rowContainers = GetRowContainers();
                MoveSendKeys(rowContainers, rowIndex: rowIndex);
                var selectedRow = rowContainers.FirstOrDefault(r => r.GetAttribute("hilite").Contains("1"));
                return selectedRow;
            }
            catch (Exception e)
            {
                throw new Exception($"SetSelectedRowByIndex(): {e.Message}");
            }
        }

        public void ResetTableSelected(List<IWebElement> rowContainers = null)
        {
            try
            {
                if (rowContainers == null) rowContainers = GetRowContainers();

                bool isFirst = false;
                string selected = string.Empty;
                while (!isFirst)
                {
                    // Will get current selected row
                    var selectedRow = rowContainers.FirstOrDefault(r => r.GetAttribute("hilite").Contains("1"));
                    if (selectedRow == null) throw new Exception("Selected row not found.");
                    var currSelected = selectedRow.Text;
                    // If true, means end of table row
                    if (selected == currSelected) break;
                    // else, set new selected row
                    else selected = currSelected;
                    // Move to next set of rows
                    MoveSendKeys(rowContainers, false);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ResetTableSelected(): {e.Message}");
            }
        }

        public Cell GetCellByIndex(IWebElement row, int cellIndex)
        {
            try
            {
                IWebElement cell = null;
                bool isHScrollChanged = false;

                var rightButton = mElement.FindElements(By.XPath("./ancestor::form//*[@id='hScrCnt']//*[@id='hp2']")).FirstOrDefault();
                if (rightButton == null) throw new Exception("Cannot find right scroll button");

                var cells = row.FindElements(By.XPath($".//*[contains(@class, 'cll') and not(contains(@class, 'cllfrst')) and not(contains(@style, 'display: none;'))]")).ToList();
                if (cells.Count < 1) throw new Exception($"Cells not found.");

                if(cellIndex >= cells.Count)
                {
                    var firstRowCells = mElement.FindElements(By.XPath($".//*[@id='row0']//*[contains(@class, 'cll') and not(contains(@class, 'cllfrst'))]")).ToList();
                    if (firstRowCells.Count < 1) throw new Exception($"firstRowCells not found.");

                    while (!IsInteractable(firstRowCells.ElementAt(cellIndex), false))
                    {
                        isHScrollChanged = true;
                        rightButton.Click();
                    }

                    cells = row.FindElements(By.XPath($".//*[contains(@class, 'cll') and not(contains(@class, 'cllfrst'))]")).ToList();

                    cell = cells.LastOrDefault();
                }
                else
                {
                    while (cellIndex != cells.Count)
                    {
                        if (cells.ElementAt(cellIndex).Displayed)
                        {
                            cell = cells.ElementAt(cellIndex);
                            break;
                        }
                        else
                        {
                            cellIndex++;
                        }
                    }
                }
                
                return new Cell { value = cell, isHScrollChanged = isHScrollChanged };
            }
            catch (Exception e)
            {
                throw new Exception($"GetCellByIndex(): {e.Message}");
            }
        }

        private void FindScrollBars()
        {
            mHScroll = mElement.FindElements(By.XPath("./..//preceding-sibling::div[@id='hScrCnt']")).FirstOrDefault(el => el.Displayed);
            mVScroll = mElement.FindElements(By.XPath("./..//preceding-sibling::div[@id='vScrCnt']")).FirstOrDefault(el => el.Displayed);
        }

        private int GetColumnIndexByHeader(String sColumnHeader)
        {
            int index = -1;
            Boolean bContinue = true;

            HScrollStart();
            RefreshHeaders();
            while (bContinue && index == -1)
            {
                for (int i = 0; i < mlstHeaders.Count; i++)
                {
                    string currentHeader = GetColumnHeaderText(mlstHeaders[i]).Trim('*').Trim();
                    if (sColumnHeader.Trim('*').Trim() == currentHeader)
                    {
                        index = i;
                        if (i == mlstHeaders.Count - 1 && mlstHeaders.Count > 1 && mHScroll.GetCssValue("visibility") != "hidden")
                        {
                            if (HScrollEnd())
                            {
                                break;
                            }
                            HScrollSingleRight();
                            index--;
                            if ((i == mlstHeaders.Count - 1))
                            {
                                i = -1;
                                continue;
                            }
                        }
                        break;
                    }
                }
                if (HScrollEnd() || index > -1)
                {
                    bContinue = false;
                }
                else
                {
                    HScrollRight(GetColumnHeaderText(mlstHeaders[mlstHeaders.Count - 1]));
                }

            }
            return index;
        }
        private int GetColumnIndexByHeaderName(String sColumnHeader, bool hasMultipleColumnValues, bool resetScroll = true)
        {
            int index = GetColumnIndexByHeaderName(new List<String> { sColumnHeader }, hasMultipleColumnValues, resetScroll).FirstOrDefault();
            return index;
        }

        private List<int> GetColumnIndexByHeaderName(List<String> sColumnHeaders, bool hasMultipleColumnValues, bool resetScroll = true)
        {
            try
            {
                var columnHeaders = sColumnHeaders.Select(ch => new TableHeader{ value = ch.Trim('*').Trim(), isFound = false }).ToList();

                List<IWebElement> headers = new List<IWebElement>();
                bool HScrollDidChange = false;

                var rightButton = mElement.FindElements(By.XPath("./ancestor::form//*[@id='hScrCnt']//*[@id='hp2']")).FirstOrDefault();
                if (rightButton == null) throw new Exception("Cannot find right scroll button");

                List<string> headerTextList = new List<string>();

                string headerFullText = string.Empty;

                do
                {
                    headers = mElement.FindElements(By.XPath(".//*[@id='tHdr']//*[contains(@class, 'hdcll') and not(contains(@class, 'hdcllfrst'))]")).ToList();
                    if (headers.Count < 1) throw new Exception("Cannot find table headers");

                    var currHeaderFullText = mElement.FindElements(By.XPath(".//*[@id='tHdr']")).FirstOrDefault().Text;
                    if (headerFullText == currHeaderFullText) break;
                    else headerFullText = currHeaderFullText;

                    foreach (var header in headers)
                    {
                        var headerText = Regex.Replace(header.Text.Trim('*').Trim(), @"\r\n?|\n", " ");
                        if (!headerTextList.Contains(headerText) && !string.IsNullOrEmpty(headerText))
                        {
                            headerTextList.Add(headerText);
                            var colheader = columnHeaders
                                .Where(ch => ch.value == headerText)
                                .ToList();

                            if(colheader.Count > 0) colheader.ForEach(ch => ch.isFound = true );
                            while (!IsInteractable(header, false))
                            {
                                HScrollDidChange = true;
                                rightButton.Click();
                            }
                        }
                    }
                    if (columnHeaders.Select(ch => ch.isFound).Contains(false))
                    {
                        HScrollDidChange = true;
                        rightButton.Click();
                        DlkLogger.LogInfo("Right scroll button clicked.");
                        if (!hasMultipleColumnValues)
                        {
                            headerTextList.Clear();
                        }
                    }

                } while (columnHeaders.Select(ch => ch.isFound).Contains(false));

                if (columnHeaders.Select(ch => ch.isFound).Contains(false)) throw new Exception($"Cannot find all headers [{string.Join(", ", sColumnHeaders)}].");

                List<int> headerIndexList = columnHeaders.Select(ch => headerTextList.IndexOf(ch.value)).ToList();

                if (resetScroll && HScrollDidChange)
                {
                    ResetHScroll();
                }
                return headerIndexList;
            }
            catch (Exception e)
            {
                throw new Exception($"GetRowsWithColumnValue(): {e.Message}");
            }
        }

        private void ResetHScroll()
        {
            DlkLogger.LogInfo("Resetting scroll bar...");
            FindElement();
            var leftButton = mElement.FindElements(By.XPath("./ancestor::form//*[@id='hScrCnt']//*[@id='hp1']")).FirstOrDefault();
            if (leftButton == null || !leftButton.Displayed) return;

            string fullHeaderText = string.Empty;
            bool isFirst = false;
            while (!isFirst)
            {
                var header = mElement.FindElements(By.XPath(".//*[@id='tHdr']")).FirstOrDefault();
                var currFullHeaderText = header.Text.Trim();
                if (fullHeaderText == currFullHeaderText) isFirst = true;
                else
                {
                    fullHeaderText = currFullHeaderText;
                    leftButton.Click();
                }
            }
            DlkLogger.LogInfo("Resetting scroll bar: Done");
        }

        private bool IsInteractable(IWebElement element, bool willClick = true)
        {
            try
            {
                if(willClick) element.Click();
                var isDisplayed = element.Displayed;
                return isDisplayed;
            }
            catch { return false; }
        }

        private void RefreshHeaders()
        {
            FindElement();

            mlstHeaders = new List<IWebElement>();
            mlstHeaderTexts = new List<String>();

            IList<IWebElement> headers = mElement.FindElements(By.ClassName(mstrHeaderClass));
            foreach (IWebElement columnHeader in headers)
            {
                DlkBaseControl hdr = new DlkBaseControl("Header", columnHeader);
                if (columnHeader.GetCssValue("display") != "none")
                {
                    mlstHeaders.Add(columnHeader);
                    mlstHeaderTexts.Add(GetColumnHeaderText(columnHeader));
                }
            }
        }

        private void RefreshRows()
        {
            FindElement();
            mlstRows = mElement.FindElements(By.ClassName(mstrRowClass)).Where(row => row.Displayed).ToList();
        }

        private String GetColumnHeaderText(IWebElement columnHeader)
        {
            String headerText = "";
            headerText = new DlkBaseControl("ColumnHeader", columnHeader).GetValue();
            return DlkString.RemoveCarriageReturn(headerText.Trim()).Replace("/ ", "/"); // trim of space after slash for some headers
        }

        private void VScrollStart(String RowType)
        {
            String vscrollID = "";
            String upBtnID = "";
            if (mVScroll.GetCssValue("visibility") != "hidden")
            {
                IWebElement vScrollElement = null;
                if (RowType.ToLower() == "existing")
                {
                    vscrollID = "vScroll1O";
                    upBtnID = "vp1O";
                }
                else if (RowType.ToLower() == "new")
                {
                    vscrollID = "vScroll1N";
                    upBtnID = "vp1N";
                }

                vScrollElement = mVScroll.FindElement(By.Id(vscrollID));

                if (vScrollElement.GetCssValue("visibility") != "hidden")
                {
                    IWebElement track = vScrollElement.FindElement(By.XPath("./descendant::div[@class='track']"));
                    IWebElement upScrollButton = vScrollElement.FindElement(By.Id(upBtnID));
                    DlkBaseControl upBtnControl = new DlkBaseControl("Up Button", upScrollButton);
                    bool isTrackHiiden = !track.Displayed;
                    Boolean bContinue = true;
                    while (bContinue)
                    {
                        if (isTrackHiiden)
                        {
                            string trackStyle = track.GetAttribute("style");
                            new DlkBaseControl("", track).SetAttribute("style", trackStyle.Replace("none", "block"));
                        }
                        int trackTop = Convert.ToInt32(track.GetCssValue("top").Replace("px", "").Trim());
                        int upBtnTop = Convert.ToInt32(upScrollButton.GetCssValue("top").Replace("px", "").Trim());
                        int upBtnHeight = Convert.ToInt32(upScrollButton.GetCssValue("height").Replace("px", "").Trim());
                        if (upBtnTop + upBtnHeight < trackTop)
                        //if (upBtnTop + upBtnHeight + 2 < trackTop)
                        {

                            upBtnControl.Click();
                        }
                        else
                        {
                            bContinue = false;
                        }
                    }
                }
            }
        }

        private void ManualDown()
        {
            var tableRows = mElement.FindElements(By.XPath("//*[contains(@id, 'row')]")).Where(tr => tr.Displayed);
            try
            {
                foreach (var row in tableRows) 
                {
                    row.Click();
                    //mElement.SendKeys(Keys.Down);
                    var Rows = mElement.FindElements(By.XPath("//*[contains(@id, 'dTbl')]")).FirstOrDefault(tr => tr.Displayed);
                    //IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    //js.ExecuteScript("var evt = new KeyboardEvent('keydown', {'keyCode':34, 'which':34}); window.dispatchEvent (evt);");
                    IJavaScriptExecutor js = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    js.ExecuteScript("" +
                        "var event = document.createEvent('HTMLEvents');" +
                        "event.initEvent('keypress', true, false);" +
                        "event.keyCode = 40;" +
                        "document.dispatchEvent(event);");
                };
            }
            catch(Exception e) { var err = e; }
            
        }

        private void VScrollDown(String RowType)
        {
            String vscrollID = "";
            String downBtnID = "";
            String recordCSS = "";
            if (mVScroll.GetCssValue("visibility") != "hidden")
            {
                IWebElement vScrollElement = null;
                if (RowType.ToLower() == "existing")
                {
                    vscrollID = "vScroll1O";
                    downBtnID = "vp2O";
                    recordCSS = "div#EXSTNG>div";
                }
                else if (RowType.ToLower() == "new")
                {
                    vscrollID = "vScroll1N";
                    downBtnID = "vp2N";
                    recordCSS = "div#NEW>div";
                }

                vScrollElement = mVScroll.FindElement(By.Id(vscrollID));

                if (vScrollElement.GetCssValue("visibility") != "hidden")
                {
                    IList<IWebElement> rows = mElement.FindElements(By.CssSelector(recordCSS));
                    IWebElement track = vScrollElement.FindElement(By.CssSelector("div>div.track"));
                    bool isTrackHiiden = !track.Displayed;
                    IWebElement downScrollButton = vScrollElement.FindElement(By.Id(downBtnID));
                    DlkBaseControl dwnBtnControl = new DlkBaseControl("DownBtn", downScrollButton);
                    if (isTrackHiiden)
                    {
                        string trackStyle = track.GetAttribute("style");
                        new DlkBaseControl("", track).SetAttribute("style", trackStyle.Replace("none", "block"));
                    }
                    int trackTop = Convert.ToInt32(track.GetCssValue("top").Replace("px", "").Trim());
                    int trackHeight = Convert.ToInt32(track.GetCssValue("height").Replace("px", "").Trim());
                    int downBtnTop = Convert.ToInt32(downScrollButton.GetCssValue("top").Replace("px", "").Trim());
                    if (trackTop + trackHeight + 2 < downBtnTop)
                    {
                        for (int i = 1; i < rows.Count; i++)
                        {
                            try
                            {
                                dwnBtnControl.Click();
                            }
                            catch (InvalidOperationException) // catch exception when button unclickable due to load time
                            {
                                //Costpoint.WaitLoadingFinished(Costpoint.IsCurrentComponentModal());
                                if (dwnBtnControl.Exists())
                                {
                                    dwnBtnControl.Click();
                                }
                                else
                                {
                                    throw;
                                }
                            }

                        }
                    }


                }
            }
        }

        private bool VScrollEnd(String RowType)
        {
            String vscrollID = "";
            String downBtnID = "";
            if (mVScroll.GetCssValue("visibility") != "hidden")
            {
                IWebElement vScrollElement = null;
                if (RowType.ToLower() == "existing")
                {
                    vscrollID = "vScroll1O";
                    downBtnID = "vp2O";
                }
                else if (RowType.ToLower() == "new")
                {
                    vscrollID = "vScroll1N";
                    downBtnID = "vp2N";
                }

                vScrollElement = mVScroll.FindElement(By.Id(vscrollID));

                if (vScrollElement.GetCssValue("visibility") != "hidden")
                {
                    DlkBaseControl vscroll = new DlkBaseControl("Vertical Scroll", vScrollElement);
                    IWebElement track = vScrollElement.FindElement(By.CssSelector("div>div.track"));
                    bool isTrackHiiden = !track.Displayed;
                    IWebElement downScrollButton = vScrollElement.FindElement(By.Id(downBtnID));

                    if (isTrackHiiden)
                    {
                        string trackStyle = track.GetAttribute("style");
                        new DlkBaseControl("", track).SetAttribute("style", trackStyle.Replace("none", "block"));
                    }

                    int trackTop = Convert.ToInt32(track.GetCssValue("top").Replace("px", "").Trim());
                    int trackHeight = Convert.ToInt32(track.GetCssValue("height").Replace("px", "").Trim());
                    int downBtnTop = Convert.ToInt32(downScrollButton.GetCssValue("top").Replace("px", "").Trim());
                    if (trackTop + trackHeight + VERTICAL_SCROLL_OFFSET < downBtnTop)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void HScrollStart()
        {
            if (mHScroll.GetCssValue("visibility") != "hidden" &&  mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                IWebElement leftScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp1"));
                DlkBaseControl leftBtnControl = new DlkBaseControl("Left Button", leftScrollButton);
                Boolean bContinue = true;
                while (bContinue)
                {
                    double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                    double leftBtnLeft = Convert.ToDouble(leftScrollButton.GetCssValue("left").Replace("px", "").Trim());
                    double leftBtnWidth = Convert.ToDouble(leftScrollButton.GetCssValue("width").Replace("px", "").Trim());
                    if (leftBtnLeft + leftBtnWidth + HORIZONTAL_SCROLL_OFFSET < trackLeft)
                    {
                        leftBtnControl.Click();
                    }
                    else
                    {
                        bContinue = false;
                    }
                }
            }
        }

        private void HScrollRight()
        {
            if (mHScroll.GetCssValue("visibility") != "hidden")
            {
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                double trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                double rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());
                if (trackLeft + trackWidth + 1 < rightBtnLeft)
                {
                    for (int i = 1; i < mlstHeaders.Count; i++)
                    {
                        rightBtnControl.Click();
                    }
                    RefreshHeaders();
                }
            }
        }

        private void HScrollRight(String LastVisibleHeaderText)
        {
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                double trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                double rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());

                string leftStyleOfRightButton = string.Empty;

                while (mlstHeaderTexts.Contains(LastVisibleHeaderText) && trackLeft + trackWidth + 1 < rightBtnLeft)
                {
                    rightBtnControl.Click();
                    RefreshHeaders();
                    rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                    trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                    trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                    rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());
                    trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());

                    string currentLeftStyleOfRightButton = rightScrollButton.GetCssValue("left");
                    if (leftStyleOfRightButton == currentLeftStyleOfRightButton) break;
                    else leftStyleOfRightButton = currentLeftStyleOfRightButton;
                }
            }
        }

        private void HScrollSingleRight()
        {
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);

                rightBtnControl.Click();
                RefreshHeaders();
            }
        }

        private void HScrollBigSingleRight()
        {
            if (mHScroll.GetCssValue("visibility") != "hidden")
            {
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);

                rightBtnControl.Click(-2, 0);
                RefreshHeaders();
            }
        }

        private bool HScrollEnd()
        {
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                DlkBaseControl hscroll = new DlkBaseControl("Horizontal Scroll", mHScroll);
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                double trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                double rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());
                if (trackLeft + trackWidth + HORIZONTAL_SCROLL_OFFSET < rightBtnLeft)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private IWebElement GetCell(int iRow, int iColumn)
        {
            IWebElement cell = null;
            RefreshRows();
            if (mlstRows.Count < iRow || iRow <= 0)
            {
                return cell;
            }
            IWebElement row = mlstRows.ElementAt(iRow - 1);
            IList<IWebElement> cells = new List<IWebElement>();
                var rowChildren = row.FindElements(By.XPath("./*"));
                foreach (IWebElement item in rowChildren)
                {
                    // do not include first cell and un-displayed cells
                    if (item.GetAttribute("class") != "cllfrst" && item.GetCssValue("display") != "none")
                    {
                            cells.Add(item);
                    }
                }
            cell = cells.ElementAt(iColumn);
            return cell;
        }

        private IWebElement GetRowHeaderCell(int iRow)
        {
            IWebElement hdr = null;
            RefreshRows();
            if (mlstRows.Count < iRow || iRow <= 0)
            {
                return hdr;
            }
            IWebElement row = mlstRows[iRow - 1];
            hdr = row.FindElements(By.ClassName("cllfrst")).Count > 0 ? row.FindElements(By.ClassName("cllfrst")).First() : null;
            return hdr;
        }

        private IWebElement GetInputControl(IWebElement cell)
        {
            if (cell == null)
            {
                return null;
            }

            IList<IWebElement> cellInputs = cell.FindElements(By.CssSelector("input"));
            if (cellInputs.Count > 0)
            {
                return cellInputs.First();
            }
            else if (cell.FindElements(By.CssSelector("div")).Count > 0)
            {
                return cell.FindElement(By.CssSelector("div"));
            }
            else if (cell.FindElements(By.CssSelector("span")).Count > 0)
            {
                cellInputs = cell.FindElements(By.CssSelector("span"));
                if (cellInputs.Count > 0)
                {
                    return cellInputs.First();
                }
            }
            else if (cell.FindElements(By.CssSelector("textarea")).Count > 0) // added for 7.0 support
            {
                cellInputs = cell.FindElements(By.CssSelector("textarea"));
                if (cellInputs.Count > 0)
                {
                    return cellInputs.First();
                }
            }
            return null;

        }

        private string SplitCamelCaseInHeader(String strToFormat)
        {
            return Regex.Replace(strToFormat, "(?<=[a-z])(?=[A-Z])", " ");
        }

        private string RemoveHTMLTagsInHeader(String strToFormat)
        {
            return Regex.Replace(strToFormat, @"<[^>]+>|&nbsp;", "").Trim();
        }

        #endregion

    }

    class Row
    {
        public int index { get; set; }
        public IWebElement value { get; set; }
    }

    public class Cell
    {
        public IWebElement value { get; set; }
        public bool isHScrollChanged { get; set; }
    }

    class TableHeader
    {
        public string value { get; set; }
        public bool isFound { get; set; }
    }
}
