using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using System.Text.RegularExpressions;

namespace TEMobileLib.DlkControls
{
    [ControlType("MultiPartTable")]
    public class DlkMultiPartTable : DlkBaseControl
    {
        private const string SCRIPT_SHEET_NAME = "Script";
        private const string SCRIPT_OUTPUT_COLUMN_NAME = "Output";
        private const char DEFAULT_DELIMETER = '~';
        private const int ROW_HEADER_SELECTOR_COL_INDEX = -1;
        private const int HORIZONTAL_SCROLL_OFFSET = 3;
        private const int VERTICAL_SCROLL_OFFSET = 2;

        private String mstrHeaderClass = "hdcll";
        private String mstrRowClass = "dRw";
        private String mstrTotalRowClass = "dTotRw";
        private String mstrMultiPartTableRightCellXPATH = "./ancestor::div[@class='tblvw']/div[@class='rightFenceCnt']";
        private String mstrMultiPartTableRightBottomCellXPATH = "./ancestor::div[@class='tblvw']/div[@class='rightFenceTotalCnt']";
        private String mstrCurrentRowXPATH = "./descendant::div[@class='dRw' and contains(@style,'3px')]";
        private String mstrHScrollBarXPATH = "./ancestor::div[@class='tblvw']/div[@id='hScrCnt']";
        private String mstrLeftHScrollBarXPATH = "./ancestor::div[@class='tblvw']/div[@id='hLScrCnt']";
        private String mstrVScrollBarXPATH = "./ancestor::div[@class='tblvw']/div[@class='vScrCnt']";
        private String mstrMultiPartTableRightXPATH = "./ancestor::div[@class='tblvw']/div[@class='rightFenceHdr']";
        private String mstrMultiPartTableBottomXPATH = "./ancestor::div[@class='tblvw']/descendant::div[@class='tbltotals']";
        private String mstrClickCellCommentOkButtonXPATH = "//input[@id='expandoOK']";
        private String mstrClickCellCommentCancelButtonXPATH = "//input[@id='expandoCancel']";
        private String mstrTableCheckbox = "./ancestor::form[1]/descendant::*[@id='selectAllImg']";
        private IList<string> mlstHeaderTexts;
        private List<IWebElement> mlstHeaders;
        private IList<IWebElement> mlstRows;
        private IWebElement mHScroll;
        private IWebElement mVScroll;

        public DlkMultiPartTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMultiPartTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMultiPartTable(String ControlName, IWebElement ExistingWebElement)
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
                    int i = 0;
                    while (bContinue)
                    {
                        for (i = 0; i < mlstRows.Count; i++)
                        {
                            DlkBaseControl cellControl = new DlkBaseControl("Cell", GetInputControl(GetCell(i + 1, intColIndex)));
                            String cellValue = cellControl.GetValue();
                            // if input control is checkbox
                            if (cellControl.GetAttributeValue("class") == "tCB")
                            {
                                cellValue = cellControl.GetAttributeValue("checked") == null ? "false" : cellControl.GetAttributeValue("checked").ToLower();
                                Value = Value.ToLower();
                            }
                            if (cellValue.Equals(Value))
                            {
                                DlkVariable.SetVariable(VariableName, (i + 1).ToString());
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

                    if (blnFound)
                    {
                        DlkLogger.LogInfo("Successfully executed GetTableRowWithColumnValue()");
                    }
                    else
                    {
                        throw new Exception("Value = '" + Value + "' under Column = '" + ColumnHeader + "' not found in table");
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

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    DlkBaseControl cellControl = new DlkBaseControl("Cell", GetCell(Convert.ToInt32(Row), intColIndex));
                    cellControl.Click();
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

                IWebElement rowHeader = GetRowHeaderCell(Convert.ToInt32(Row));
                if (rowHeader == null)
                {
                    throw new Exception("Row header cell not found");
                }
                DlkBaseControl ctlRowHeader = new DlkBaseControl("RowHeader", rowHeader);

                ctlRowHeader.DoubleClick();;
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

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
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
                                IList<IWebElement> txtElements = cell.FindElements(By.XPath("./following-sibling::*[1]"));
                                if (txtElements.Count == 0)
                                {
                                    textCell.Set(Value);
                                    break;
                                }
                                if (intColIndex == 0)
                                {
                                    textCell.Set(Value);
                                }
                                else
                                {
                                    textCell.Set(Value);
                                }
                                break;
                            case "tcb":
                                DlkCheckBox chkCell = new DlkCheckBox("CheckBox Cell", "ID", strInputControlID);
                                chkCell.Set(Value);
                                break;
                            case "tccbt":
                            case "tccbtb":
                                DlkComboBox cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID);
                                IList<IWebElement> cboElements = cell.FindElements(By.XPath("./following-sibling::*[1]"));
                                if (cboElements.Count == 0)
                                {
                                    cboCell = new DlkComboBox("Combo Cell", "ID", strInputControlID, true);
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
                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex));
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

        [Keyword("VerifyRightTableCellValue", new String[] {"1|text|Row|O{Row}", 
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|Sample Value"})]
        public void VerifyRightTableCellValue(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();
                int intColIndex = GetColumnIndexByHeaderRightMultiPart(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement inputControl = GetInputControl(GetCellRightMultiPart(Convert.ToInt32(Row), intColIndex));
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
                            case "totval":
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
                        DlkLogger.LogInfo("Successfully executed VerifyRightTableCellValue()");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRightTableCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyBottomTableCellValue", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Column Header|Line*",
                                                        "3|text|Expected Value|Sample Value"})]
        public void VerifyBottomTableCellValue(String Row, String ColumnHeader, String ExpectedValue)
        {
            try
            {
                bool bIsRightTable = false;
                Initialize();
                int intColIndex = GetColumnIndexByHeaderRightMultiPart(ColumnHeader); // look for header in right multipart table first - it has fewer columns
                if (intColIndex != -1)
                {
                    bIsRightTable = true;
                }
                else // look for header in main multipart table
                {
                    intColIndex = GetColumnIndexByHeader(ColumnHeader);
                }
                if (intColIndex != -1)
                {
                    IWebElement inputControl = GetInputControl(bIsRightTable ? GetCellRightMultiPart(Convert.ToInt32(Row), intColIndex, true)
                        : GetCellBottomMultiPart(Convert.ToInt32(Row), intColIndex));
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
                            case "totval":
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
                        DlkLogger.LogInfo("Successfully executed VerifyBottomTableCellValue()");
                    }
                }
                else
                {
                    throw new Exception("Column '" + ColumnHeader + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyBottomTableCellValue() failed : " + e.Message, e);
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
            try
            {
                int iExpRowCount = Convert.ToInt32(ExpectedRowCount);
                int iActRowCount = 0;
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);

                Initialize();

                VScrollStart("existing");
                VScrollStart("new");

                RefreshRows();
                while (!(VScrollEnd("existing") && VScrollEnd("new")))
                {
                    for (int i = 0; i < mlstRows.Count; i++)
                    {
                        // To handle some grids that load after scroll down especially in Chrome browser
                        if (DlkEnvironment.mBrowser.ToLower() == "chrome")
                        {
                            Thread.Sleep(500);
                            mAction.SendKeys(OpenQA.Selenium.Keys.Down).Build().Perform();
                            iActRowCount++;
                        }
                        else
                        {
                            mAction.SendKeys(OpenQA.Selenium.Keys.Down).Build().Perform();
                            iActRowCount++;
                        }
                    }
                    VScrollDown("existing");
                    VScrollDown("new");
                }

                if (mlstRows.Count > 0)
                {
                    int iTerminalRowIndex = Convert.ToInt32(mElement.FindElements(By.XPath(mstrCurrentRowXPATH))[0].GetAttribute("id").Substring(3));

                    iActRowCount++; // Add 1 to count currently selected row, succeeding count will increment on mouse down key press
                    while (iTerminalRowIndex < mlstRows.Count - 1)
                    {
                        // To handle some grids that load after scroll down especially in Chrome browser
                        if (DlkEnvironment.mBrowser.ToLower() == "chrome")
                        {
                            Thread.Sleep(1000);
                            mAction.SendKeys(OpenQA.Selenium.Keys.Down).Build().Perform();
                            iTerminalRowIndex++;
                            iActRowCount++;
                        }
                        else
                        {
                            mAction.SendKeys(OpenQA.Selenium.Keys.Down).Build().Perform();
                            iTerminalRowIndex++;
                            iActRowCount++;
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
            try
            {
                String strActualHeaders = String.Empty;
                String strDelimeter = String.Empty;
                List<IWebElement> mlstHeadersBeforeScroll;
                IList<string> mlstHeadersFullList;
                String sHeader = "";
                String sHeaderFinal = "";
                mlstHeadersFullList = new List<String>();

                Initialize();
                //reset horizontal scroll bar to make sure we cover all headers
                HScrollMultiPartStart();
                RefreshHeaders();
                mlstHeadersBeforeScroll = mlstHeaders; // get first header before scroll
                String headerText = GetColumnHeaderText(mlstHeadersBeforeScroll.First());
                if (headerText.Contains("<div"))
                {
                    headerText = RemoveHTMLTagsInHeader(headerText);
                }
                mlstHeadersFullList.Add(headerText);
                //Scroll to view the hidden columns and append the newly visible ones to the main list
                mHScroll = mElement.FindElement(By.XPath(mstrLeftHScrollBarXPATH)); // Scroll left table first
                if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
                {
                    IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                    IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hpL2"));
                    DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                    double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                    double trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                    double rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());

                    while (mlstHeaderTexts.Contains(GetColumnHeaderText(mlstHeaders[mlstHeaders.Count - 1])) && trackLeft + trackWidth + 3 < rightBtnLeft)
                    {
                        rightBtnControl.Click();
                        RefreshHeaders();
                        rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hpL2"));
                        trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                        trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                        rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());

                        sHeader = GetColumnHeaderText(mlstHeaders.First());
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
                mHScroll = mElement.FindElement(By.XPath(mstrHScrollBarXPATH));
                if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
                {
                    IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                    IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                    DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                    double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                    double trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                    double rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());

                    while (mlstHeaderTexts.Contains(GetColumnHeaderText(mlstHeaders[mlstHeaders.Count - 1])) && trackLeft + trackWidth + 1 < rightBtnLeft)
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
                String[] lstHdrs = ColumnHeaders.Split(DEFAULT_DELIMETER);
                String[] lstValues = Values.Split(DEFAULT_DELIMETER);

                if (lstHdrs.Count() != lstValues.Count())
                {
                    throw new Exception("Column Headers count = " + lstHdrs.Count() + " not equal to Values count = " + lstValues.Count());
                }

                Initialize();
                VScrollStart("existing");
                VScrollStart("new");
                RefreshRows();

                bool bContinue = true;

                while (bContinue)
                {
                    for (int rowIndex = 1; rowIndex <= mlstRows.Count; rowIndex++)
                    {
                        int inputIndex = 0;
                        for (inputIndex = 0; inputIndex < lstHdrs.Count(); inputIndex++)
                        {
                            int intColIndex = -1;
                            intColIndex = GetColumnIndexByHeader(lstHdrs[inputIndex]); // call this just to check if valid header
                            if (intColIndex != -1)
                            {
                                DlkBaseControl cellControl = new DlkBaseControl("Cell", GetInputControl(GetCell(rowIndex, intColIndex)));
                                String cellValue = cellControl.GetValue();
                                // if input control is checkbox
                                if (cellControl.GetAttributeValue("class") == "tCB")
                                {
                                    cellValue = cellControl.GetAttributeValue("checked") == null ? "false" : cellControl.GetAttributeValue("checked").ToLower();
                                    lstValues[inputIndex] = lstValues[inputIndex].ToLower();
                                }
                                if (!(cellValue.Equals(lstValues[inputIndex])))
                                {
                                    break;
                                }
                            }
                            else
                            {
                                throw new Exception("Column '" + lstHdrs[inputIndex] + "' not found");
                            }
                        }


                        if (inputIndex == lstHdrs.Count()) // means found
                        {
                            DlkVariable.SetVariable(VariableName, rowIndex.ToString());
                            DlkLogger.LogInfo("Successfully executed GetTableRowWithMultipleColumnValues()");
                            return;
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

                throw new Exception("Values = '" + Values + "' under Columns = '" + ColumnHeaders + "' not found in table");
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
                if (intColIndex != -1)
                {
                    IWebElement inputControl = GetInputControl(GetCell(Convert.ToInt32(Row), intColIndex));

                    if (inputControl == null)
                    {
                        throw new Exception("Cell does not contain an input control");
                    }
                    inputControl.Click();

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

                rowHeader.Click();
                DlkLogger.LogInfo("Successfully executed ClickTableRowHeader()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTableRowHeader() failed : " + e.Message, e);
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
                        OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
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

                int intColIndex = GetColumnIndexByHeader(ColumnHeader);
                if (intColIndex != -1)
                {
                    IWebElement cell = GetCell(Convert.ToInt32(Row), intColIndex);
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
                                        case "totval":
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
        private void FindScrollBars()
        {
            mHScroll = mElement.FindElement(By.XPath(mstrHScrollBarXPATH));
            mVScroll = mElement.FindElement(By.XPath(mstrVScrollBarXPATH));
        }

        private int GetColumnIndexByHeader(String sColumnHeader)
        {
            int index = -1;
            Boolean bContinue = true;

            HScrollMultiPartStart();
            RefreshHeaders();
            while (bContinue && index == -1)
            {
                for (int i = 0; i < mlstHeaders.Count; i++)
                {
                    string currentHeader = GetColumnHeaderText(mlstHeaders[i]).Trim('*').Trim();
                    if (sColumnHeader.Trim('*').Trim() == currentHeader)
                    {
                        index = i;
                        if (i == mlstHeaders.Count - 1 && mHScroll.GetCssValue("visibility") != "hidden")
                        {
                            HScrollSingleMultiPartRight();
                            index--;
                            if ((i == mlstHeaders.Count - 1) && (HScrollEnd()))
                            {
                                index++;
                            }
                        }
                        break;
                    }
                }
                if (HScrollMultiPartEnd() || index > -1)
                {
                    bContinue = false;
                }
                else
                {
                    HScrollMultiPartRight(GetColumnHeaderText(mlstHeaders[mlstHeaders.Count - 1]));
                }
            }
            return index;
        }

        private int GetColumnIndexByHeaderRightMultiPart(String sColumnHeader)
        {
            int index = -1;
            RefreshHeadersRight();
            for (int i = 0; i < mlstHeaders.Count; i++)
            {
                string currentHeader = GetColumnHeaderText(mlstHeaders[i]).Trim('*').Trim();
                if (sColumnHeader.Trim('*').Trim() == currentHeader)
                {
                    index = i;
                    break;
                }
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

        private void RefreshHeadersRight()
        {
            FindElement();

            mlstHeaders = new List<IWebElement>();
            mlstHeaderTexts = new List<String>();
            
            IList<IWebElement> headers = mElement.FindElements(By.XPath(mstrMultiPartTableRightXPATH));
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
            mlstRows = mElement.FindElements(By.ClassName(mstrRowClass));
        }

        private void RefreshRowsRightMultiPart(bool isBottomRightOnly)
        {
            FindElement();
            IWebElement rightCellElement = mElement.FindElement(By.XPath(mstrMultiPartTableRightCellXPATH));
            mlstRows = rightCellElement.FindElements(By.ClassName(mstrRowClass));
            rightCellElement = mElement.FindElement(By.XPath(mstrMultiPartTableRightBottomCellXPATH));
            IList<IWebElement> mlstRightBottomRows = rightCellElement.FindElements(By.ClassName(mstrTotalRowClass));
            mlstRows = isBottomRightOnly ? mlstRightBottomRows : mlstRows.Concat(mlstRightBottomRows).ToList();
        }

        private void RefreshRowsBottomMultiPart()
        {
            FindElement();
            IWebElement mBottomTable = mElement.FindElement(By.XPath(mstrMultiPartTableBottomXPATH));
            mlstRows = mBottomTable.FindElements(By.ClassName(mstrTotalRowClass));
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
                    Boolean bContinue = true;
                    while (bContinue)
                    {
                        int trackTop = Convert.ToInt32(track.GetCssValue("top").Replace("px", "").Trim());
                        int upBtnTop = Convert.ToInt32(upScrollButton.GetCssValue("top").Replace("px", "").Trim());
                        int upBtnHeight = Convert.ToInt32(upScrollButton.GetCssValue("height").Replace("px", "").Trim());
                        if (upBtnTop + upBtnHeight < trackTop)
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
                    IWebElement downScrollButton = vScrollElement.FindElement(By.Id(downBtnID));
                    DlkBaseControl dwnBtnControl = new DlkBaseControl("DownBtn", downScrollButton);
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
                    IWebElement downScrollButton = vScrollElement.FindElement(By.Id(downBtnID));
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

        private void HScrollMultiPartStart()
        {
            mHScroll = mElement.FindElement(By.XPath(mstrLeftHScrollBarXPATH));
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                IWebElement leftScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hpL1"));
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
            mHScroll = mElement.FindElement(By.XPath(mstrHScrollBarXPATH));
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
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

        private void HScrollMultiPartRight(String LastVisibleHeaderText)
        {
            string scrollButtonCSS = "div>*#hpL2";
            if (HScrollLeftTableEnd()) // scroll right table this time
            {
                mHScroll = mElement.FindElement(By.XPath(mstrHScrollBarXPATH));
                scrollButtonCSS = "div>*#hp2";
            }
            else
            {
                mHScroll = mElement.FindElement(By.XPath(mstrLeftHScrollBarXPATH));
            }
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector(scrollButtonCSS));
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                double trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                double trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                double rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());

                while (mlstHeaderTexts.Contains(LastVisibleHeaderText) && trackLeft + trackWidth + 1 < rightBtnLeft)
                {
                    rightBtnControl.Click();
                    RefreshHeaders();
                    rightScrollButton = mHScroll.FindElement(By.CssSelector(scrollButtonCSS));
                    trackLeft = Convert.ToDouble(track.GetCssValue("left").Replace("px", "").Trim());
                    trackWidth = Convert.ToDouble(track.GetCssValue("width").Replace("px", "").Trim());
                    rightBtnLeft = Convert.ToDouble(rightScrollButton.GetCssValue("left").Replace("px", "").Trim());
                }
            }
        }

        private void HScrollSingleMultiPartRight()
        {
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                IWebElement rightScrollButton;
                if (HScrollLeftTableEnd())
                {
                    mHScroll = mElement.FindElement(By.XPath(mstrHScrollBarXPATH));
                    rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hp2"));
                }
                else
                {
                    rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hpL2"));
                }
                DlkBaseControl rightBtnControl = new DlkBaseControl("Right Button", rightScrollButton);
                rightBtnControl.Click();
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

        private bool HScrollLeftTableEnd()
        {
            mHScroll = mElement.FindElement(By.XPath(mstrLeftHScrollBarXPATH));
            if (mHScroll.GetCssValue("visibility") != "hidden" && mHScroll.FindElement(By.CssSelector("div>div.track")).Displayed == true)
            {
                DlkBaseControl hscroll = new DlkBaseControl("Horizontal Scroll", mHScroll);
                IWebElement track = mHScroll.FindElement(By.CssSelector("div>div.track"));
                IWebElement rightScrollButton = mHScroll.FindElement(By.CssSelector("div>*#hpL2"));
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

        private bool HScrollMultiPartEnd()
        {
            bool LeftEnd = HScrollLeftTableEnd();
            if (!LeftEnd)
            {
                return false;
            }
            bool RightEnd = false;
            mHScroll = mElement.FindElement(By.XPath(mstrHScrollBarXPATH));
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
                    RightEnd = true;
                    return (LeftEnd && RightEnd);
                }
            }
            else
            {
                RightEnd = true;
                return (LeftEnd && RightEnd);
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
            IWebElement row = mlstRows[iRow - 1];
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

        private IWebElement GetCellRightMultiPart(int iRow, int iColumn, bool isBottomRightOnly = false)
        {
            IWebElement cell = null;
            RefreshRowsRightMultiPart(isBottomRightOnly);
            if (mlstRows.Count < iRow || iRow <= 0)
            {
                return cell;
            }
            IWebElement row = mlstRows[iRow - 1];
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

        private IWebElement GetCellBottomMultiPart(int iRow, int iColumn)
        {
            IWebElement cell = null;
            RefreshRowsBottomMultiPart();
            if (mlstRows.Count < iRow || iRow <= 0)
            {
                return cell;
            }
            IWebElement row = mlstRows[iRow - 1];
            IList<IWebElement> cells = new List<IWebElement>();
            foreach (IWebElement item in row.FindElements(By.XPath("./*")))
            {
                // do not include first cell and un-displayed cells
                if (item.GetAttribute("cmt") != "1" && item.GetCssValue("display") != "none")
                {
                    cells.Add(item);
                }
            }
            cell = cells[iColumn];
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
            else if (cell.FindElements(By.ClassName("totVal")).Count > 0)
            {
                cellInputs = cell.FindElements(By.ClassName("totVal"));
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
}
