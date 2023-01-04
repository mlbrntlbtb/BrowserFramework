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
using CostpointLib.DlkRecords;

namespace CostpointLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        private const string ORANGE_RGB = "color: rgb(242, 119, 42)";
        private const string ROW_SELECTED_CLASS = "sel";
        private const string SCRIPT_SHEET_NAME = "Script";
        private const string SCRIPT_OUTPUT_COLUMN_NAME = "Output";
        private const char DEFAULT_DELIMETER = '~';
        private const int ROW_HEADER_SELECTOR_COL_INDEX = -1;
        private const int HORIZONTAL_SCROLL_OFFSET = 3;
        private const int VERTICAL_SCROLL_OFFSET = 2;

        private String mstrHeaderClass = "hdcll";
        private String mstrRowClass = "dRw";
        private String mstrNewRowsXPATH = "./descendant::div[@class='tblbdy' and @id='NEW']//div[@class='dRw']";
        private String mstrNewRowsAndHeadersXPATH = "./descendant::div[@class='tblbdy' and @id='NEW']|./descendant::div[@class='tblbdy' and @id='tHdr']";
        private String mstrHScrollBarXPATH = "./ancestor::div[@class='tblvw']/div[@id='hScrCnt']";
        private String mstrVScrollBarXPATH = "./ancestor::div[@class='tblvw']/div[@class='vScrCnt']";
        private String mstrClickCellCommentOkButtonXPATH = "//input[@id='expandoOK']";
        private String mstrClickCellCommentCancelButtonXPATH = "//input[@id='expandoCancel']";
        private String mstrTableCheckbox = "./ancestor::form[1]/descendant::*[@id='selectAllImg']";
        private string mstrMessageArea = "//div[contains(@class,'msg') and contains(@style,'visible')]";
        private double mLeftTrack = -1;
        private IList<string> mlstHeaderTexts;
        private List<IWebElement> mlstHeaders;
        private IList<IWebElement> mlstRows;
        private IWebElement mHScroll;
        private IWebElement mVScroll;
        private bool isControlAndClick;

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
            int intColIndex = -1;
            int iActRowCount = 0;
            int tempScrollTop = 0;
            int scrollTop = 0;
            int tempYLocation = 0;
            int actualYLocation = 0;
            int retryCount = 0;

            try
            {
                Initialize();
                intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    if (ScrollUp())
                    {
                        tempYLocation = GetRowYLocation();
                        while ((tempYLocation == 0 || tempYLocation != actualYLocation) || scrollTop != tempScrollTop || retryCount > 0)
                        {
                            iActRowCount++;
                            if (iActRowCount > 1)
                                tempYLocation = actualYLocation;

                            tempScrollTop = scrollTop;         
                            
                            IWebElement row = mElement.FindElements(By.ClassName(mstrRowClass))
                                    .Where(w => w.GetAttribute("hilite") == "1").FirstOrDefault()
                                    .FindElements(By.XPath("./*"))
                                    .Where(item => item.GetAttribute("class") != "cllfrst" && item.GetCssValue("display") != "none")
                                    .ElementAt(intColIndex);

                            DlkBaseControl cellControl = new DlkBaseControl("Cell", GetInputControl(row));
                            string cellValue = cellControl.GetValue();

                            if (cellControl.GetAttributeValue("class") == "tCB")
                            {
                                cellValue = cellControl.GetAttributeValue("checked") == null ? "false" : cellControl.GetAttributeValue("checked").ToLower();
                                Value = Value.ToLower();
                            }
                            if (cellValue.Equals(Value)) // means found
                            {
                                DlkPreviousControlRecord.SetCurrentControl(iActRowCount.ToString());
                                DlkVariable.SetVariable(VariableName, iActRowCount.ToString());
                                DlkLogger.LogInfo("Successfully executed GetTableRowWithColumnValue()");
                                break;
                            }
                            else //handle vertical scroll (retry count for stack scroll is set to 15)
                            {
                                ScrollTable(scrollUp: false);
                                RefreshRows();
                                actualYLocation = GetRowYLocation();

                                if (mVScroll.GetCssValue("visibility") != "hidden")
                                {
                                    scrollTop = int.Parse(GetVerticalScrollTopLocation());

                                    if (tempYLocation == actualYLocation)
                                    {
                                        if (scrollTop == tempScrollTop && retryCount < 15)
                                            retryCount++;
                                        else if (scrollTop != tempScrollTop)
                                        {
                                            retryCount = 0;
                                        }
                                        else if (retryCount == 15)
                                        {
                                            throw new Exception("Value = '" + Value + "' under Column = '" + ColumnHeader + "' not found in table");
                                        }
                                    }
                                }
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    if (ScrollUp())
                    {
                        DlkBaseControl cellControl = new DlkBaseControl("Cell", GetCell(Convert.ToInt32(Row), intColIndex, true));
                        cellControl.Click();
                    }
                    else
                    {
                        throw new Exception($"ClickTableCellByRowColumn() failed : No table row(s) found");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed ClickTableCellByRowColumn()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableCellByRowColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickTableRowHeader", new String[] { "1|text|Row|O{Row}" })]
        public void DoubleClickTableRowHeader(String Row)
        {
            try
            {
                Initialize();
                DlkPreviousControlRecord.SetCurrentControl(Row);
                IWebElement rowHeader = GetRowHeaderCell(Convert.ToInt32(Row));
                if (rowHeader == null)
                {
                    throw new Exception("Row header cell not found");
                }
                DlkBaseControl ctlRowHeader = new DlkBaseControl("RowHeader", rowHeader);

                ctlRowHeader.DoubleClick();
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                RefreshRows();
                ScrollUp();
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                string getHighlightedRowId() => mElement.FindElements(By.ClassName(mstrRowClass)).Where(w => w.GetAttribute("hilite") == "1").FirstOrDefault()?.GetAttribute("id");

                if (intColIndex != -1)
                {                    
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex, true);
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
                                string rowId = getHighlightedRowId();
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

                                textCell.Set(Value, false);
                                
                                RefreshRows();
                                if (rowId != getHighlightedRowId())
                                {
                                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                                    mAction.KeyDown(Keys.Shift).SendKeys(Keys.Tab).KeyUp(Keys.Shift).Build().Perform();
                                }
                                break;
                            case "tcb":
                                DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", "ID", strInputControlID);
                                Boolean bCurrentVal = Convert.ToBoolean(chkCell.GetAttributeValue("checked"));

                                if (bool.TryParse(Value, out bool result) && result != bCurrentVal)
                                    chkCell.Click();
                                break;
                            case "tccbt":
                            case "tccbtb":
                                DlkComboBox cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID);
                                try
                                {
                                    if (cell.FindElements(By.XPath("./following-sibling::*[1]")).Count == 0)
                                    {
                                        cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID, true);
                                    }
                                }
                                catch
                                {
                                    // do nothing
                                }
                                cboCell.SelectDropdownValue(Value, false);
                                break;
                            default:
                                throw new Exception("Cell input control = '" + strInputControlClass + "' is not recognized");
                        }                        
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex, true));
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

        [Keyword("VerifyTableRowExistWithColumnValue", new String[] {"1|text|Column Header|Sample Column Header",
                                                                    "2|text|Value|Sample Value",
                                                                    "3|text|Expected Value|True or False"})]
        public void VerifyTableRowExistWithColumnValue(String ColumnHeader, String Value, String ExpectedValue)
        {
            bool blnFound = false;
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
                    while (bContinue)
                    {
                        for (int i = 0; i < mlstRows.Count; i++)
                        {
                            DlkBaseControl cellControl = new DlkBaseControl("Cell", GetInputControl(GetCell(i + 1, intColIndex)));
                            if (cellControl.GetValue() == Value)
                            {

                                blnFound = true;
                                bContinue = false;
                                break;
                            }
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

                    if (blnFound == Convert.ToBoolean(ExpectedValue))
                    {
                        DlkLogger.LogInfo("Successfully executed VerifyTableRowExistWithColumnValue() : Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(blnFound) + "'");
                    }
                    else
                    {
                        throw new Exception("Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(blnFound) + "'");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableRowExistWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableRowCount", new String[] { "1|text|Expected Row Count|0" })]
        public void VerifyTableRowCount(String ExpectedRowCount)
        {
            int iActRowCount;
            try
            {
                int iExpRowCount = Convert.ToInt32(ExpectedRowCount);
                iActRowCount = GetTableRows();
                DlkAssert.AssertEqual("VerifyTableRowCount() : Table Row Count", iExpRowCount, iActRowCount);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTableRowCount", new String[] { "1|text|VariableName|RowCount" })]
        public void GetTableRowCount(string VariableName)
        {
            int iActRowCount;
            try
            {
                iActRowCount = GetTableRows();
                DlkVariable.SetVariable(VariableName, iActRowCount.ToString());
                DlkLogger.LogInfo("Successfully executed GetTableRowCount()");
            }
            catch (Exception e)
            {
                throw new Exception("GetTableRowCount() failed : " + e.Message, e);
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex, true);
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
            Dictionary<string,string> mlstHeadersFullList;
            String sHeader = "";
            String sHeaderFinal = "";
            String sKey = "";

            mlstHeadersFullList = new Dictionary<string,string>();

            try
            {
                Initialize();
                //reset horizontal scroll bar to make sure we cover all headers
                HScrollStart();
                RefreshHeaders();
                mlstHeadersBeforeScroll = mlstHeaders;

                foreach (IWebElement hdrName in mlstHeadersBeforeScroll)
                {
                    String headerText = GetColumnHeaderText(hdrName);
                    if (headerText.Contains("<div"))
                    {
                        headerText = RemoveHTMLTagsInHeader(headerText);
                    }
                    mlstHeadersFullList.Add(hdrName.GetAttribute("id"), headerText);
                }

                //Scroll to view the hidden columns and append the newly visible ones to the main list
                if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
                {
                    IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                    DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                    rightBtnControl.Click();
                    double trackRightLastPosition = -1;

                    while (true)
                    {
                        FindScrollBars();
                        IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                        double trackRight = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                        RefreshHeaders();                        
                        foreach (IWebElement hdrName in mlstHeaders)
                        {
                            sHeader = GetColumnHeaderText(hdrName);
                            sKey = hdrName.GetAttribute("id");
                            sHeaderFinal = SplitCamelCaseInHeader(sHeader);
                            
                            if (sHeaderFinal.Contains("<div"))
                            {
                                sHeaderFinal = RemoveHTMLTagsInHeader(sHeaderFinal);
                            }

                            if (mlstHeadersFullList.LastOrDefault().Value == sHeaderFinal.Replace("*", ""))
                            {
                                mlstHeadersFullList[sKey] = sHeaderFinal;
                            }
                            else if (!mlstHeadersFullList.ContainsKey(sKey))
                            {
                                mlstHeadersFullList.Add(sKey, sHeaderFinal);
                            }
                        }

                        if (trackRight != trackRightLastPosition)
                        {
                            trackRightLastPosition = trackRight;
                            rightBtnControl.Click();
                        }
                        else
                            break;
                    }
                }

                strActualHeaders = string.Join(DEFAULT_DELIMETER.ToString(), 
                                        ExpectedHeaders.Split(DEFAULT_DELIMETER).ToList().FindAll(header =>
                                        {
                                            string keyResult = mlstHeadersFullList.FirstOrDefault(colHeader => colHeader.Value.ToLower() == header.ToLower()).Key??null;
                                            if (keyResult != null)
                                            {
                                                mlstHeadersFullList.Remove(keyResult);
                                                return true;
                                            }
                                            return false;
                                        }));

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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex, true));
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex, true));
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
                String[] lstHdrs = ColumnHeaders.Split(DEFAULT_DELIMETER);
                String[] lstValues = Values.Split(DEFAULT_DELIMETER);
                int iActRowCount = 0;
                int tempScrollTop = 0;
                int scrollTop = 0;
                int tempYLocation = 0;
                int actualYLocation = 0;
                int retryCount = 0;

                if (lstHdrs.Count() != lstValues.Count())
                {
                    throw new Exception("Column Headers count = " + lstHdrs.Count() + " not equal to Values count = " + lstValues.Count());
                }

                Initialize();
                RefreshRows();
                if (ScrollUp())
                {
                    tempYLocation = GetRowYLocation();

                    while ((tempYLocation == 0 || tempYLocation != actualYLocation) || scrollTop != tempScrollTop || retryCount > 0)
                    {                        
                        iActRowCount++;
                        if (iActRowCount > 1)
                            tempYLocation = actualYLocation;

                        tempScrollTop = scrollTop;                       

                        int inputIndex = -1;
                        for (inputIndex = 0; inputIndex < lstHdrs.Length; inputIndex++)
                        {
                            int intColIndex = -1;
                            RefreshRows(true);
                            intColIndex = GetColumnIndexByHeader(lstHdrs[inputIndex]); // call this just to check if valid header
                            RefreshRows();
                            if (intColIndex != -1)
                            {
                                IWebElement row = mElement.FindElements(By.ClassName(mstrRowClass))
                                        .Where(w => w.GetAttribute("hilite") == "1").FirstOrDefault()
                                        .FindElements(By.XPath("./*"))
                                        .Where(item => item.GetAttribute("class") != "cllfrst" && item.GetCssValue("display") != "none")
                                        .ElementAt(intColIndex);

                                DlkBaseControl cellControl = new DlkBaseControl("Cell", GetInputControl(row));
                                string cellValue = cellControl.GetValue();
                                // if input control is checkbox
                                if (cellControl.GetAttributeValue("class") == "tCB")
                                {
                                    cellValue = cellControl.GetAttributeValue("checked") == null ? "false" : cellControl.GetAttributeValue("checked").ToLower();
                                    lstValues[inputIndex] = lstValues[inputIndex].ToLower();
                                }
                                if (!(cellValue.Equals(lstValues[inputIndex])))
                                {
                                    inputIndex = -1;
                                    break;
                                }
                            }
                            else
                            {
                                throw new Exception("Column '" + lstHdrs[inputIndex] + "' not found");
                            }
                        }
                        if (inputIndex == lstHdrs.Length) // means found
                        {
                            DlkPreviousControlRecord.SetCurrentControl(iActRowCount.ToString());
                            DlkVariable.SetVariable(VariableName, iActRowCount.ToString());
                            DlkLogger.LogInfo("Successfully executed GetTableRowWithMultipleColumnValues()");
                            break;
                        }
                        else //handle vertical scroll (retry count for stack scroll is set to 15)
                        { 
                            ScrollTable(scrollUp: false);
                            actualYLocation = GetRowYLocation();

                            if (mVScroll.GetCssValue("visibility") != "hidden")
                            {
                                scrollTop = int.Parse(GetVerticalScrollTopLocation());

                                if (tempYLocation == actualYLocation)
                                {
                                    if (scrollTop == tempScrollTop && retryCount < 15)
                                        retryCount++;
                                    else if (scrollTop != tempScrollTop)
                                    {
                                        retryCount = 0;
                                    }
                                    else if (retryCount == 15)
                                    {
                                        throw new Exception("Values = '" + Values + "' under Columns = '" + ColumnHeaders + "' not found in table");
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    throw new ArgumentException("GetTableRowWithMultipleColumnValues() failed : No row(s) found in table");                
            }
            catch (Exception e)
            {
                throw new Exception("GetTableRowWithMultipleColumnValues() failed : " + e.Message, e);
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
                RefreshRows();
                DlkPreviousControlRecord.SetCurrentControl(Row);
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex, true));

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

        [Keyword("GetTableCellCommentPopupValue", new String[] { "1|text|VariableName|SampleVar"})]
        public void GetTableCellCommentPopupValue(String sVariableName)
        {
            try
            {
                DlkTextArea txtTableComment = new DlkTextArea("TextAreaComment", "ID", "expandoEdit");
                String commentValue = DlkString.ReplaceCarriageReturn(txtTableComment.GetValue(), "\n");
                DlkVariable.SetVariable(sVariableName, commentValue);
                DlkLogger.LogInfo("Successfully executed GetTableCellCommentPopupValue()");
            }
            catch (Exception e)
            {
                throw new Exception("GetTableCellCommentPopupValue() failed " + e.Message, e);
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                RefreshRows();
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                RefreshRows();
                isControlAndClick = true;
                //IWebElement rowHeader = GetCell(Convert.ToInt32(Row), ROW_HEADER_SELECTOR_COL_INDEX);
                IWebElement rowHeader = GetRowHeaderCell(Convert.ToInt32(Row));
                if (rowHeader == null)
                {
                    throw new Exception("Row header cell not found");
                }
                DlkBaseControl ctlRowHeader = new DlkBaseControl("RowHeader", rowHeader);

                try
                {
                    //scroll table into view to avoid the issue of clicking row element not in view
                    ScrollIntoViewUsingJavaScript();
                }
                catch (Exception)
                {
                    DlkLogger.LogInfo("ClickTableRowHeader Scroll into view exception");
                }

                ControlAndClick(rowHeader);
                DlkLogger.LogInfo("Successfully executed ClickTableRowHeader()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeader() failed : " + e.Message, e);
            }
            finally
            {
                isControlAndClick = false;
            }
        }
        [Keyword("RightClickColumnHeader", new String[] { "1|text|ColumnName|ColumnName" })]
        public void RightClickColumnHeader(String ColumnName)
        {
            try
            {
                Initialize();
                int colIndex = GetColumnIndexByHeader(ColumnName);

                if (colIndex > -1)
                {
                    IWebElement columnHeader = mlstHeaders[colIndex];
                    if (DlkEnvironment.mBrowser == "Firefox")
                    {
                        PerformRightClickUsingJavaScript(columnHeader);
                    }
                    else
                    {
                        OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                        mAction.ContextClick(columnHeader);
                        mAction.Perform();
                    }
                    DlkLogger.LogInfo("Successfully executed RightClickColumnHeader(): " + ColumnName);
                }
                else
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
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex, true);
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
                DlkLogger.LogInfo("Successfully executed GetTableCellValue()");
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

        [Keyword("RightClickTableRowHeader", new String[] { "1|text|Row|O{Row}" })]
        public void RightClickTableRowHeader(String Row)
        {
            try
            {
                Initialize();
                IWebElement rowHeader = GetRowHeaderCell(Convert.ToInt32(Row));
                if (rowHeader == null)
                    throw new ArgumentException("Row header cell not found");

                try
                {
                    //scroll table into view to avoid the issue of clicking row element not in view
                    ScrollIntoViewUsingJavaScript();
                }
                catch (Exception)
                {
                    DlkLogger.LogInfo("RightClickTableRowHeader Scroll into view exception");
                }

                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.ContextClick(rowHeader);
                mAction.Perform();
                DlkLogger.LogInfo("Successfully executed RightClickTableRowHeader()");
            }
            catch (Exception e)
            {
                throw new Exception("RightClickTableRowHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellFontColor", new String[] {"1|text|Row|O{Row}",
                                                     "2|text|Column Header|Account*",
                                                     "3|text|ExpectedColor|Orange"})]
        public void VerifyCellFontColor(String Row, String ColumnHeader, String ExpectedColor)
        {
            try
            {
                Initialize();
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex, true);
                    IWebElement inputControl = GetInputControl(cell);
                    if (inputControl == null)
                    {
                        throw new Exception("Cell does not contain an input control");
                    }
                    else
                    {
                        String strInputControlClass = inputControl.GetAttribute("class").ToLower();
                        String strInputControlID = inputControl.GetAttribute("id");
                        String actualColor = "Default";
                        switch (strInputControlClass)
                        {
                            case "tdfro":
                            case "tdfrq":
                            case "tdf":
                            case "tdfrqnum":
                            case "tdfronum":
                            case "tdfnum":
                            case "tcb":
                            case "tccbt":
                            case "tccbtb":
                                DlkBaseControl tableCell = new DlkBaseControl("Cell", "ID", strInputControlID);
                                string style = tableCell.GetAttributeValue("style");

                                if (style.Contains(ORANGE_RGB))
                                    actualColor = "Orange";

                                DlkAssert.AssertEqual("VerifyCellFontColor()", ExpectedColor.ToLower(), actualColor.ToLower());
                                break;
                            default:
                                throw new Exception("Cell input control = '" + strInputControlClass + "' is not recognized");
                        }

                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed VerifyCellFontColor()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellFontColor() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyColumnHeaderExists", new String[] { "1|text|ExpectedHeaders|Header1~Header2", "2|text|VariableName|MyVar" })]
        public void GetVerifyColumnHeaderExists(string ExpectedHeaders, string VariableName)
        {
            String strActualHeaders = String.Empty;
            String strDelimeter = String.Empty;
            List<IWebElement> mlstHeadersBeforeScroll;
            Dictionary<string, string> mlstHeadersFullList;
            String sHeader = "";
            String sHeaderFinal = "";
            String sKey = "";

            mlstHeadersFullList = new Dictionary<string, string>();

            try
            {
                Initialize();
                //reset horizontal scroll bar to make sure we cover all headers
                HScrollStart();
                RefreshHeaders();
                mlstHeadersBeforeScroll = mlstHeaders;

                foreach (IWebElement hdrName in mlstHeadersBeforeScroll)
                {
                    String headerText = GetColumnHeaderText(hdrName);
                    if (headerText.Contains("<div"))
                    {
                        headerText = RemoveHTMLTagsInHeader(headerText);
                    }
                    mlstHeadersFullList.Add(hdrName.GetAttribute("id"), headerText);
                }

                //Scroll to view the hidden columns and append the newly visible ones to the main list
                if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
                {
                    IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                    DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                    rightBtnControl.Click();
                    double trackRightLastPosition = -1;

                    while (true)
                    {
                        FindScrollBars();
                        IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                        double trackRight = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                        RefreshHeaders();
                        foreach (IWebElement hdrName in mlstHeaders)
                        {
                            sHeader = GetColumnHeaderText(hdrName);
                            sKey = hdrName.GetAttribute("id");
                            sHeaderFinal = SplitCamelCaseInHeader(sHeader);

                            if (sHeaderFinal.Contains("<div"))
                            {
                                sHeaderFinal = RemoveHTMLTagsInHeader(sHeaderFinal);
                            }

                            if (mlstHeadersFullList.LastOrDefault().Value == sHeaderFinal.Replace("*", ""))
                            {
                                mlstHeadersFullList[sKey] = sHeaderFinal;
                            }
                            else if (!mlstHeadersFullList.ContainsKey(sKey))
                            {
                                mlstHeadersFullList.Add(sKey, sHeaderFinal);
                            }
                        }

                        if (trackRight != trackRightLastPosition)
                        {
                            trackRightLastPosition = trackRight;
                            rightBtnControl.Click();
                        }
                        else
                            break;
                    }
                }

                strActualHeaders = string.Join(DEFAULT_DELIMETER.ToString(),
                                        ExpectedHeaders.Split(DEFAULT_DELIMETER).ToList().FindAll(header =>
                                        {
                                            string keyResult = mlstHeadersFullList.FirstOrDefault(colHeader => colHeader.Value.ToLower() == header.ToLower()).Key ?? null;
                                            if (keyResult != null)
                                            {
                                                mlstHeadersFullList.Remove(keyResult);
                                                return true;
                                            }
                                            return false;
                                        }));

                bool areEqual = ExpectedHeaders.ToLower() == strActualHeaders.ToLower();
                DlkVariable.SetVariable(VariableName, areEqual.ToString());
                DlkLogger.LogInfo("GetVerifyColumnHeaderExists() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyColumnHeaderExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellColor", new String[] {"1|text|Row|O{Row}",
                                                     "2|text|Column Header|Account*",
                                                     "3|text|ExpectedColor|Orange|Default"})]
        public void VerifyCellColor(String Row, String ColumnHeader, String ExpectedColor)
        {
            try
            {
                Initialize();
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex, true);
                    IWebElement inputControl = GetInputControl(cell);
                    if (inputControl == null)
                    {
                        throw new Exception("Cell does not contain an input control");
                    }
                    else
                    {
                        String actualColor = "Default";
                        string cellStyle = cell.GetAttribute("style");

                        if (cellStyle.Contains(ORANGE_RGB))
                            actualColor = "Orange";

                        DlkAssert.AssertEqual("VerifyCellColor()", ExpectedColor.ToLower(), actualColor.ToLower());
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed VerifyCellColor()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableCellButtonExists", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Button Name|Phone",
                                                        "4|text|Expected Value|TRUE" })]
        public void VerifyTableCellButtonExists(String Row, String ColumnHeader, String ButtonName, String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex, true);
                    string ActualValue = string.Empty;

                    switch (ButtonName.ToLower())
                    {
                        case "phone":
                            ActualValue = GetButtonDisplayStyle(cell, "tPhoneBtn");
                            break;
                        case "email":
                            ActualValue = GetButtonDisplayStyle(cell, "tEmailBtn");
                            break;
                        default:
                            throw new Exception("Cell button = '" + ButtonName + "' is not recognized");
                    }

                    DlkAssert.AssertEqual("VerifyTableCellButtonExists()", ExpectedValue.ToLower(), ActualValue.ToLower());
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellButtonExists() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyTableCellButtonReadOnly", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Button Name|Phone",
                                                        "4|text|Expected Value|TRUE" })]
        public void VerifyTableCellButtonReadOnly(String Row, String ColumnHeader, String ButtonName, String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkPreviousControlRecord.SetCurrentControl(Row);
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                RefreshRows();
                if (intColIndex != -1)
                {
                    ScrollUp();
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex, true);
                    string ActualValue = string.Empty;

                    switch (ButtonName.ToLower())
                    {
                        case "phone":
                            ActualValue = GetButtonDisplayStyle(cell, "tPhoneBtn");
                            break;
                        case "email":
                            ActualValue = GetButtonDisplayStyle(cell, "tEmailBtn");
                            break;
                        default:
                            throw new Exception("Cell button = '" + ButtonName + "' is not recognized");
                    }

                    DlkAssert.AssertEqual("VerifyTableCellButtonReadOnly()", ExpectedValue.ToLower(), ActualValue.ToLower());
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableCellButtonReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Retrieves the button display style
        /// </summary>
        /// <param name="cell">current table cell</param>
        /// <param name="buttonClass">button class</param>
        /// <returns>True if the button exists/is displayed and False if not</returns>
        private string GetButtonDisplayStyle(IWebElement cell, string buttonClass)
        {
            string btnDisplayed = string.Empty;
            string btnStyle = string.Empty;

            IWebElement textIconButton = cell.FindElement(By.XPath("./span[@class='"+ buttonClass +"']"));
            btnStyle = textIconButton.GetAttribute("style");

            if (btnStyle.Contains("display: inline"))
            {
                btnDisplayed = "True";
            }
            else
            {
                btnDisplayed = "False";
            }
            return btnDisplayed;
        }


        #region Private Methods
        private void FindScrollBars()
        {
            mHScroll = mElement.FindElement(By.XPath(mstrHScrollBarXPATH));
            mVScroll = mElement.FindElement(By.XPath(mstrVScrollBarXPATH));
        }

        private int GetColumnIndexByHeader(String sColumnHeader)
        {
            int index = -1;
            Boolean bContinue = true;

            if (ErrorMessageExists())
                ScrollToEndOfPage();

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
                            if (!HScrollSingleRight())
                                break;

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
                    RefreshHeaders();
                    HScrollRight(GetColumnHeaderText(mlstHeaders[mlstHeaders.Count - 1]));
                }

            }

            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                mLeftTrack = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
            }

            return index;
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

        private void RefreshRows(bool forceHeaders = false)
        {
            FindElement();
            if (mElement.FindElements(By.XPath(mstrNewRowsXPATH)).Any() && !forceHeaders)
            {
                mElement = mElement.FindElement(By.XPath(mstrNewRowsAndHeadersXPATH));
            }
            mlstRows = mElement.FindElements(By.ClassName(mstrRowClass));
        }

        private String GetColumnHeaderText(IWebElement columnHeader)
        {
            var header = new DlkBaseControl("ColumnHeader", columnHeader);
            String headerText = DlkEnvironment.mBrowser.ToLower() == "safari" ? header.GetAttributeValue("innerText")
                : header.GetValue();
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
                    int tempTrackTop = -1;
                    Boolean bContinue = true;
                    while (bContinue)
                    {
                        if (isTrackHiiden)
                        {
                            string trackStyle = track.GetAttribute("style");
                            new DlkBaseControl("", track).SetAttribute("style", trackStyle.Replace("none", "block"));
                        }
                        int trackTop = Convert.ToInt32(track.GetCssValue("top").Replace("px", "").Trim());
                        if (trackTop != tempTrackTop)
                        {
                            tempTrackTop = trackTop;
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
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement leftScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp1"));
                DlkBaseControl leftBtnControl = new DlkBaseControl("Left Button", leftScrollButton);
                double trackLeftLastPosition = -1;
                while (true)
                {
                    FindScrollBars();
                    IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                    leftBtnControl.Click();
                    double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                    if (trackLeftLastPosition != trackLeft)
                        trackLeftLastPosition = trackLeft;
                    else
                        break;
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

                while (mlstHeaderTexts.Contains(LastVisibleHeaderText) && trackLeft + trackWidth + 1 < rightBtnLeft)
                {
                    rightBtnControl.Click();
                    RefreshHeaders();
                    rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                    trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                    trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                    rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());
                }
            }
        }
        
        /// <summary>
        /// Returns true if right scroll button was successfully clicked or right scroll button not exists
        /// </summary>
        /// <returns></returns>
        private bool HScrollSingleRight()
        {
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                double getTrackLeft() => Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                double lastPosition = getTrackLeft();                
                rightBtnControl.Click();

                if (lastPosition == getTrackLeft())
                    return false;

                RefreshHeaders();                
            }
            return true;
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

        private IWebElement GetCell(int iRow, int iColumn, bool locateRow = false)
        {
            IWebElement cell = null;
            IWebElement row = null;

            if (locateRow)
            {
                IWebElement getHighlightedRow() => mElement.FindElements(By.ClassName(mstrRowClass)).Where(w => w.GetAttribute("hilite") == "1").FirstOrDefault();

                if (!DlkPreviousControlRecord.IsRepeatedRow)
                {
                    string rowID = getHighlightedRow()?.GetAttribute("id");
                    int i = 1;
                    while (i < iRow)
                    {
                        ScrollTable(scrollUp: false);
                        if (rowID == getHighlightedRow().GetAttribute("id"))
                        {
                            ReturnTrack();
                            break;
                        }
                        i++;
                    }
                }

                row = getHighlightedRow();
            }
            else
            {
                RefreshRows();
                if (mlstRows.Count < iRow || iRow <= 0)
                    return cell;

                row = mlstRows[iRow - 1];
            }

            IList<IWebElement> cells = new List<IWebElement>();

            foreach (IWebElement item in row.FindElements(By.XPath("./*")))
            {
                // do not include first cell and un-displayed cells
                if (item.GetAttribute("class") != "cllfrst" && item.GetCssValue("display") != "none")
                {
                    cells.Add(item);
                }
            }
            cell = cells[iColumn];
            return cell;
        }

        /// <summary>
        /// Scroll to previous track. Call this method when focus of cell is untintentionally moved to required field during Scroll down 
        /// </summary>
        private void ReturnTrack()
        {
            try
            {
                if (!(mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed))
                    return;

                HScrollStart();
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                double getTrackLeft()
                {
                    if (double.TryParse(track.GetCssValue("left").Replace("px", "").Trim(), out double result))
                        return result;
                    else
                        return mLeftTrack; //if track left returns "Auto" stop scrolling
                }

                while (mLeftTrack != getTrackLeft())
                {
                    rightBtnControl.Click();
                }
                RefreshHeaders();
                RefreshRows();
            }
            catch (ElementClickInterceptedException) when (ErrorMessageExists())
            {
                ScrollToEndOfPage();
            }
        }

        private IWebElement GetRowHeaderCell(int iRow)
        {
            IWebElement hdr = null;
            if (ScrollUp())
            {               
                if (!DlkPreviousControlRecord.IsRepeatedRow)
                {
                    int currentRow = 1;
                    while (currentRow != iRow)
                    {
                        ScrollTable(scrollUp: false);
                        currentRow++;
                    }
                }

                IWebElement row = mElement.FindElements(By.ClassName(mstrRowClass)).Where(w => w.GetAttribute("hilite") == "1").FirstOrDefault();                
                hdr = row.FindElements(By.ClassName("cllfrst")).Count > 0 ? row.FindElements(By.ClassName("cllfrst")).First() : null;
                return hdr;
            }
            else
                throw new ArgumentException("GetRowHeaderCell() failed : No row(s) found in table");
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

        /// <summary>
        /// Performs a right click (mouse click) on the specified table column element
        /// </summary>
        /// <param name="elem">table column element where the right click will be executed</param>
        private void PerformRightClickUsingJavaScript(IWebElement columnElem)
        {
            IJavaScriptExecutor jse = DlkEnvironment.AutoDriver as IJavaScriptExecutor;
            String javaScript = "var evt = document.createEvent('MouseEvents');"
                + "var RIGHT_CLICK_BUTTON_CODE = 2;"
                + "evt.initMouseEvent('contextmenu', true, true, window, 1, 0, 0, MouseEvent.clientX, MouseEvent.clientY, false, false, false, false, RIGHT_CLICK_BUTTON_CODE, null);"
                + "arguments[0].dispatchEvent(evt)";

            jse.ExecuteScript(javaScript, columnElem);
        }

        /// <summary>
        /// Get Y axis location for selected row in table
        /// </summary>
        /// <returns></returns>
        private int GetRowYLocation()
        {
            IWebElement element = mElement.FindElements(By.ClassName(mstrRowClass)).Where(w => w.GetAttribute("hilite") == "1").FirstOrDefault();
            return element == null ? -1 : element.Location.Y;
        }

        /// <summary>
        /// Scroll table up or down
        /// </summary>
        /// <param name="scrollUp">true if you want to start from first row</param>
        private void ScrollTable(bool scrollUp)
        {
            OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
            if (DlkEnvironment.mIsMobileBrowser)
            {
                IWebElement basePage = DlkEnvironment.AutoDriver.FindElement(By.XPath("//*"));
                basePage.SendKeys(scrollUp ? Keys.Up : Keys.Down);
            }
            else
            {
                mAction.SendKeys(scrollUp ? Keys.Up : Keys.Down).Build().Perform();
            }

            while (DlkEnvironment.AutoDriver.FindElements(By.XPath("//span[@class='pleaseWaitImage' and contains(@style,'hidden')]")).FirstOrDefault() == null)
            {
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// Get top location of table vertical scroll bar
        /// </summary>
        /// <returns>null or top location value</returns>
        private string GetVerticalScrollTopLocation()
        {
            string[] verticalScrollClass = new string[]
            {
                "trackO", //CP7
                "trackN" //CP8
            };
            string result = null;
            foreach (var item in verticalScrollClass)
            {
                if (int.TryParse(mVScroll.FindElement(By.Id(item)).GetCssValue("top").Replace("px", ""), out int i))
                    result = i.ToString();
            }
            return result;
        }

        /// <summary>
        /// Set selected row to first row. returns false if theres no row found
        /// </summary>
        /// <returns></returns>
        private bool ScrollUp()
        {
            if (!DlkPreviousControlRecord.IsRepeatedRow)
            {
                int tempLocationY = 0;
                int actualLocationY = 0;
                int attempt = 0;
                while (attempt != 3) //retry for loading modal blocking table scroll
                {
                    try
                    {
                        VScrollStart("existing");
                        VScrollStart("new");
                        IWebElement mRow = mElement.FindElements(By.ClassName(mstrRowClass)).FirstOrDefault();
                        IWebElement mRowHeader = mRow.FindElements(By.ClassName("cllfrst")).FirstOrDefault();
                        string rowSelected = null;
                        void rowHeaderClick()
                        {
                            if (isControlAndClick) //sendkey control + click for ClickTableRowHeader keyword      
                            {
                                rowSelected = mRowHeader.GetAttribute(ROW_SELECTED_CLASS);
                                ControlAndClick(mRowHeader);
                            }
                            else
                                mRowHeader.Click(); //table keywords except ClickTableRowHeader

                            while (mRowHeader.GetAttribute(ROW_SELECTED_CLASS) != rowSelected) //retain value
                                ControlAndClick(mRowHeader);
                        };

                        if (mRowHeader == null)
                            mRow.Click();
                        else if (mRowHeader != null && mRow != null)
                        {
                            if (!(mRowHeader.FindElements(By.TagName("button")).FirstOrDefault().GetAttribute("style").ToString().Replace(" ", "").Contains("display:block;")))
                                rowHeaderClick();
                            else
                            {
                                if (mControlName == "ChargeTreeTable")
                                {
                                    try
                                    {
                                        //this works for ADMCHGTREE screen but not working for some TE screens
                                        (mRow.FindElements(By.XPath("./child::div[not(contains(@class,'cllfrst'))][1]")).FirstOrDefault() ?? mRow).Click();
                                    }
                                    catch
                                    {
                                        //do nothing
                                    }
                                }
                                else
                                    mRow.Click(); //throws no error, but not actually clicks row for ADMCHGTREE
                            }
                        }
                        else
                            rowHeaderClick();

                        break;
                    }
                    catch(Exception ex)
                    {
                        if (ex is ElementClickInterceptedException)
                        {
                            if (ErrorMessageExists())
                                ScrollToEndOfPage();
                            else
                                ScrollIntoViewUsingJavaScript();
                        }

                        attempt++;
                        DlkLogger.LogInfo($"ScrollUp() : Retry count {attempt}");
                        Thread.Sleep(500);
                    }
                }

                while (tempLocationY == 0 || tempLocationY != actualLocationY)
                {
                    tempLocationY = actualLocationY;
                    ScrollTable(scrollUp: true);
                    actualLocationY = GetRowYLocation();

                    if (actualLocationY == -1)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if required field error message is displayed
        /// </summary>
        /// <returns>True if message area required field error is displayed</returns>
        private bool ErrorMessageExists()
        {
            IWebElement messageArea = DlkEnvironment.AutoDriver.FindElements(By.XPath(mstrMessageArea)).FirstOrDefault();
            if (messageArea != null)
                return messageArea.Text.ToLower().Contains("error");
            else
                return false;
        }

        /// <summary>
        /// Scroll to end of page if required field error message is displayed
        /// </summary>
        private void ScrollToEndOfPage()
        {
            IJavaScriptExecutor javascript = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
            javascript.ExecuteScript("arguments[0].scrollIntoView();", mElement);
            javascript.ExecuteScript("window.scrollBy(0,document.body.scrollHeight)");            
        }

        /// <summary>
        /// Get table row count
        /// Used in keywords: GetTableRowCount and VerifyTableRowCount
        /// </summary>
        /// <returns>Table row count</returns>
        private int GetTableRows()
        {
            try
            {
                int iActRowCount = 0;
                int tempScrollTop = 0;
                int scrollTop = 0;
                int tempYLocation = 0;
                int actualYLocation = 0;
                int retryCount = 0;
                int stackCount = 0;

                Initialize();
                RefreshRows();
                if (ScrollUp())
                {
                    tempYLocation = GetRowYLocation();
                    while ((tempYLocation == 0 || tempYLocation != actualYLocation) || scrollTop != tempScrollTop || retryCount > 0)
                    {
                        iActRowCount++;
                        if (iActRowCount > 1)
                            tempYLocation = actualYLocation;

                        tempScrollTop = scrollTop;
                        ScrollTable(scrollUp: false);
                        actualYLocation = GetRowYLocation();
                        if (mVScroll.GetCssValue("visibility") != "hidden")
                        {
                            scrollTop = int.Parse(GetVerticalScrollTopLocation());

                            if (tempYLocation == actualYLocation)
                            {
                                if (scrollTop == tempScrollTop && retryCount < 15)
                                    retryCount++;
                                else if (scrollTop != tempScrollTop)
                                {
                                    stackCount = retryCount;
                                    retryCount = 0;
                                }
                                else if (retryCount == 15)
                                {
                                    iActRowCount -= (retryCount - stackCount);
                                    break;
                                }
                            }
                        }
                    }
                }
                return iActRowCount;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Used for multi-selection of rows in table
        /// </summary>
        /// <param name="element">row header element</param>
        private void ControlAndClick(IWebElement element)
        {
            if (DlkEnvironment.mIsMobileBrowser)
            {
                string javaScript = "var selattr = document.createAttribute('sel'); selattr.value='1'; arguments[0].setAttributeNode(selattr);";
                if (element.GetAttribute("sel") == "1")
                {
                    javaScript = "arguments[0].removeAttribute('sel');";
                }
                IJavaScriptExecutor jse = DlkEnvironment.AutoDriver as IJavaScriptExecutor;
                jse.ExecuteScript(javaScript, element);
            }
            else
            {
                new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver)
                .KeyDown(Keys.Control)
                .Click(element)
                .KeyUp(Keys.Control)
                .Perform();
            }
            Thread.Sleep(DlkEnvironment.mBrowser.ToLower() == "firefox" ? 100 : 200);
        }
        #endregion

    }
}
