using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Grid")]
    public class DlkGrid : DlkBaseControl
    {
        #region Constructors
        public DlkGrid(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        private string gridType = null;
        private void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            gridType = GetGridType();
        }
        private const string xpathLowerCase = "translate(., 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')";

        #region Keywords
        /// <summary>
        /// Assigns cell value to variable
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>
        [Keyword("AssignCellValueToVariable")]
        public void AssignCellValueToVariable (String VariableName, String RowNumber, String ColumnNumber)
        {
            try
            {
                Initialize();
                string cellValue = GetCellValueByRowAndColumnNumber(RowNumber, ColumnNumber);
                DlkVariable.SetVariable(VariableName, cellValue);
                DlkLogger.LogInfo("AssignCellValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + cellValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignCellValueToVariable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies if Grid exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
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

        /// <summary>
        /// Verifies grid header text
        /// </summary>
        /// <param name="ExpectedHeaders"></param>

        [Keyword("VerifyGridHeaders", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void VerifyGridHeaders(String ExpectedHeaders)
        {
            try
            {
                Initialize();
                IList<IWebElement> headers = new List<IWebElement>();
                IList<string> actualHeaders = new List<string>();

                switch (GetGridType())
                {
                    case "muiGrid":
                        headers = mElement.FindElements(By.XPath(".//div[contains(@class,'MuiTableHead-root')]//span[not(img) and not(@data-testid='dvProjectSearchHeaderIconUpdatedDate')]"));
                        break;
                    case "dvList":
                        IWebElement muiDialog = mElement.FindElements(By.XPath("//div[contains(@class,'MuiDialog-paper')]")).LastOrDefault(); // using lastordefault to get latest dialog within nested dialogs
                        if (muiDialog != null) // determine if within dialog
                            headers = muiDialog.FindElements(By.XPath(".//div[contains(@data-testid,'Header') and not(contains(@data-testid,'HeaderIcon'))]"));
                        else
                            headers = mElement.FindElements(By.XPath("//div[contains(@data-testid,'Header') and not(contains(@data-testid,'HeaderIcon')) and not(contains(@data-testid,'appHeader'))]"));
                        break;
                    case "groupedRow":
                        headers = mElement.FindElements(By.XPath(".//*[@class='header']//*[@role='columnheader']//span[@class='hoverTooltip'] | //*[@class='header']//*[@role='columnheader']//*[@class='header-text']"));
                        break;
                    default:
                        break;
                }

                foreach (IWebElement item in headers)
                {
                    actualHeaders.Add(DlkString.RemoveCarriageReturn(item.Text.Trim()));
                }

                string actualDelimitedContents = string.Join("~", actualHeaders);
                DlkAssert.AssertEqual("VerifyGridHeaders() : " + mControlName, ExpectedHeaders, actualDelimitedContents);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyGridHeaders() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies grid title
        /// </summary>
        /// <param name="ExpectedText"></param>

        [Keyword("VerifyGridTitle", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void VerifyGridTitle(String ExpectedText)
        {
            try
            {
                Initialize();
                string actualText = null;
                switch (GetGridType())
                {
                    case "muiGrid":
                        throw new Exception("VerifyGridTitle() failed : Grid type not supported");
                    case "dvList":
                        actualText = mElement.FindElements(By.XPath(".//p[contains(@class,'MuiTypography-root')]")).FirstOrDefault().Text.Trim();
                        break;
                    default:
                        break;
                }

                DlkAssert.AssertEqual("VerifyGridTitle() : " + mControlName, ExpectedText, actualText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyGridTitle() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Clicks grid header based on grid header title
        /// </summary>
        /// <param name="GridHeaderTitle"></param>
        [Keyword("ClickGridHeader", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void ClickGridHeader(String GridHeaderTitle)
        {
            try
            {
                Initialize();
                IWebElement gridHeader = mElement.FindElements(By.XPath("//*[text()[" + xpathLowerCase + "='" + GridHeaderTitle.ToLower() + "']]"))
                        .FirstOrDefault();

                gridHeader.Click();
                DlkLogger.LogInfo("ClickGridHeader() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickGridHeader() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies cell value 
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>
        /// <param name="ExpectedValue"></param>

        [Keyword("VerifyCellValue", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void VerifyCellValue(String RowNumber, String ColumnNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                string cellValue = DlkString.ReplaceElipsesWithThreeDots(GetCellValueByRowAndColumnNumber(RowNumber, ColumnNumber));
                DlkAssert.AssertEqual("VerifyCellValue() : " + mControlName, ExpectedValue, cellValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies footer contents of a grid
        /// </summary>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyGridFooterText", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void VerifyGridFooterText(String ExpectedText)
        {
            try
            {
                Initialize();
                string footerText = null;
                if(gridType == "dvList")
                    footerText = mElement.FindElements(By.XPath("//*[@data-testid='dvListFooter']")).LastOrDefault().Text;
                else
                    footerText = mElement.FindElements(By.XPath(".//div[contains(@class,'MuiTableFooter-root')]")).FirstOrDefault().Text;

                DlkAssert.AssertEqual("VerifyGridFooterText() : " + mControlName, ExpectedText, footerText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyGridFooterText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// clicks grid row
        /// </summary>
        /// <param name="RowNumber"></param>
        [Keyword("ClickGridRow", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void ClickGridRow(String RowNumber)
        {
            try
            {
                Initialize();
                IWebElement gridRow = null;
                switch (GetGridType())
                {
                    case "muiGrid":
                        gridRow = mElement.FindElements(By.XPath(".//div[contains(@class,'MuiTableRow-hover')][" + RowNumber + "]")).FirstOrDefault();
                        break;
                    case "dvPartnerships":
                        gridRow = mElement.FindElements(By.XPath(".//h6[contains(@class,'partnershipType ')][" + RowNumber + "]")).FirstOrDefault();
                        break;
                    case "dvList":
                        gridRow = mElement.FindElements(By.XPath(".//div[contains(@class,'dvListRow')][" + RowNumber + "]")).FirstOrDefault();
                        break;
                    case "groupedRow":
                        gridRow = mElement.FindElements(By.XPath(".//*[@role='table']//*[@role='row' and contains(@class, 'grouped-row')]")).FirstOrDefault();
                        break;
                    default:
                        break;
                }
                gridRow.Click();
                DlkLogger.LogInfo("ClickGridRow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickGridRow() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Clicks grid cell by row number and column number
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>

        [Keyword("ClickGridCellByRowColumn", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void ClickGridCellByRowColumn(String RowNumber, String ColumnNumber)
        {
            try
            {
                Initialize();
                IWebElement gridRow = null;
                IWebElement gridCol = null;
                switch (GetGridType())
                {
                    case "muiGrid":
                        throw new Exception("ClickGridCellByRowColumn() failed : Grid type not supported");
                    case "dvList":
                        gridRow = mElement.FindElements(By.XPath(".//div[contains(@class,'dvListRow ')][" + RowNumber + "]")).FirstOrDefault();
                        gridCol = gridRow.FindElements(By.XPath(".//div[contains(@class,'MuiGrid-item')][" + ColumnNumber + "]//a")).FirstOrDefault();
                        break;
                    case "groupedRow":
                    case "dvLinks":
                        gridCol = GetCellByRowAndColumnNumber(RowNumber, ColumnNumber);
                        break;
                    default:
                        break;
                }
                gridCol.Click();
                DlkLogger.LogInfo("ClickGridCellByRowColumn() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickGridCellByRowColumn() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks download button - specific for dvDocument grid type
        /// </summary>
        /// <param name="RowNumber"></param>

        [Keyword("ClickDownloadButton", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void ClickDownloadButton(String RowNumber)
        {
            try
            {
                Initialize();
                IWebElement downloadButton = mElement.FindElements(By.XPath(".//button[@type='button'][" + RowNumber + "]")).FirstOrDefault();
                downloadButton.Click();
                DlkLogger.LogInfo("ClickDownloadButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickDownloadButton() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks content by row if the grid only has a single column - specific for dvLinks grid type but can be expanded
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>
        [Keyword("ClickContentByRow", new String[] { "1|text|ContentType|1",
                                                      "2|text|RowNumber|1" })]
        public void ClickContentByRow(String ContentType, String RowNumber)
        {
            try
            {
                Initialize();
                IWebElement contentToClick;

                switch(ContentType.ToLower())
                {
                    case "link":
                        contentToClick = mElement.FindElements(By.XPath(".//*[contains(@class, 'MuiLink')]//*[contains(@class, 'webLinkName')][" + RowNumber + "]")).FirstOrDefault();
                        break;
                    case "element":
                        contentToClick = mElement.FindElements(By.XPath($"(.//*[contains(@class, 'MuiPaper')]//a)[{ RowNumber }]")).FirstOrDefault();
                        break;
                    default:
                        contentToClick = null;
                        break;
                }

                if (contentToClick == null) throw new Exception($"{ContentType} not supported.");

                contentToClick.Click();
                DlkLogger.LogInfo("ClickContentByRow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickContentByRow() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Hovers and verify tooltip value. Supported only for dvDocuments grid type
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ToolTipValue"></param>

        [Keyword("HoverAndVerifyToolTipValue", new String[] { "1|text|Item|Item1" })]
        public void HoverAndVerifyToolTipValue(String RowNumber, String ToolTipValue)
        {
            try
            {
                Initialize();
                IWebElement itemToHover = null;
                var items = mElement.FindElements(By.XPath(".//*[contains(@class,'hoverTooltip')]")).ToList();
                itemToHover = items[Int32.Parse(RowNumber) - 1];
                // Hover on selected item
                new DlkBaseControl("ToolTip", itemToHover).MouseOver();
                // Get tooltip value 
                string actualToolTipValue = mElement.FindElements(By.XPath("//div[@role='tooltip']")).FirstOrDefault().Text;
                // Assert
                DlkAssert.AssertEqual("HoverAndVerifyToolTipValue() : " + mControlName, ToolTipValue, actualToolTipValue);
            }
            catch (Exception e)
            {
                throw new Exception("HoverAndVerifyToolTipValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Hovers and verify tooltip value on specific grid cell
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>
        /// <param name="ToolTipValue"></param>

        [Keyword("HoverAndVerifyToolTipValueByRowColumn", new String[] { "1|text|Row Number|1", "2|text|Column Number|1", "3|text|Tooltip Value|Value"})]
        public void HoverAndVerifyToolTipValueByRowColumn(String RowNumber, String ColumnNumber, String ToolTipValue)
        {
            try
            {
                Initialize();
                var cell = GetCellByRowAndColumnNumber(RowNumber, ColumnNumber);
                if (cell == null) throw new Exception("Cannot find specified cell.");
                new DlkBaseControl("Cell to hover", cell).ScrollIntoViewUsingJavaScript();

                var itemToHover = cell.FindElements(By.XPath(".//*[contains(@class,'hoverTooltip')]")).FirstOrDefault();
                if(itemToHover == null) throw new Exception("Cannot find tooltip to hover.");

                // Hover on selected item
                new DlkBaseControl("ToolTip", itemToHover).MouseOver();
                //add short delay if tooltip hasn't shown yet
                if (!DlkEnvironment.AutoDriver.FindElements(By.XPath(".//*[@role='tooltip']")).Any())
                {
                   Thread.Sleep(DlkEnvironment.mShortWaitMs);
                }
                // Get tooltip value 
                var tooltip = DlkEnvironment.AutoDriver.FindElements(By.XPath(".//*[@role='tooltip']")).FirstOrDefault();
                if (tooltip == null) throw new Exception("Cannot find tooltip after hovering.");

                // Assert
                DlkAssert.AssertEqual("HoverAndVerifyToolTipValueByRowColumn() : " + mControlName, ToolTipValue, tooltip.Text);
            }
            catch (Exception e)
            {
                throw new Exception("HoverAndVerifyToolTipValueByRowColumn() failed : " + e.Message, e);
            }
        }



        /// <summary>
        /// Verifies grid details header
        /// </summary>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyGridDetailsHeader", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void VerifyGridDetailsHeader(String ExpectedText)
        {
            try
            {
                Initialize();
                string header = null;

                if(mElement.GetAttribute("data-testid") == null) // Project details
                    header = mElement.FindElements(By.XPath(".//div[contains(@class,'MuiGrid-item')]")).FirstOrDefault().Text;
                else
                    header = mElement.FindElement(By.XPath(".//h2")).Text;

                DlkAssert.AssertEqual("VerifyGridDetailsHeader() : " + mControlName, ExpectedText, header);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyGridDetailsHeader() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Clicks button on specific cell
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>

        [Keyword("ClickGridCellButtonByRowColumn", new String[] { "1|text|Row Number|1",
                                                       "2|text|Column Number|1" })]
        public void ClickGridCellButtonByRowColumn(String RowNumber, String ColumnNumber)
        {
            try
            {
                Initialize();
                IWebElement cell = GetCellByRowAndColumnNumber(RowNumber, ColumnNumber);
                var cellButton = GetCellButton(cell);
                if (cellButton == null) throw new Exception("Could not find button in cell");
                cellButton.Click();
                DlkLogger.LogInfo("ClickGridCellButtonByRowColumn() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickGridCellButtonByRowColumn() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets checkbox value
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>
        [Keyword("SetCheckBoxByRowColumn", new String[] { "1|text|Row Number|1",
                                                       "2|text|Column Number|1" })]
        public void SetCheckBoxByRowColumn(String TrueOrFalse, String RowNumber, String ColumnNumber)
        {
            try
            {
                Initialize();
                var checkBox = GetCellByRowAndColumnNumber(RowNumber, ColumnNumber).FindElements(By.XPath(".//*[contains(@class, 'MuiCheckbox-root')]")).FirstOrDefault();
                if (checkBox == null) throw new Exception("Could not find checkbox in cell");

                new DlkCheckbox("Grid Checkbox", checkBox).Set(TrueOrFalse);
                DlkLogger.LogInfo("SetCheckBoxByRowColumn() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetCheckBoxByRowColumn() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets checkbox value
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        /// <param name="RowNumber"></param>
        /// <param name="ColumnNumber"></param>
        [Keyword("VerifyCheckBoxByRowColumn", new String[] { "1|text|Row Number|1",
                                                       "2|text|Column Number|1" })]
        public void VerifyCheckBoxByRowColumn(String IsChecked, String RowNumber, String ColumnNumber)
        {
            try
            {
                Initialize();
                var checkBox = GetCellByRowAndColumnNumber(RowNumber, ColumnNumber).FindElements(By.XPath(".//*[contains(@class, 'MuiCheckbox-root')]")).FirstOrDefault();
                if (checkBox == null) throw new Exception("Could not find checkbox in cell");

                new DlkCheckbox("Grid Checkbox", checkBox).VerifyState(IsChecked);
                DlkLogger.LogInfo("VerifyCheckBoxByRowColumn() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCheckBoxByRowColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyGridCellButtonByRowColumn", new String[] { "1|text|Row Number|1",
                                                       "2|text|Column Number|1" })]
        public void VerifyGridCellButtonByRowColumn(String RowNumber, String ColumnNumber, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement cell = GetCellByRowAndColumnNumber(RowNumber, ColumnNumber);
                var cellButton = GetCellButton(cell);

                DlkAssert.AssertEqual("VerifyGridCellButtonByRowColumn(): ", Convert.ToBoolean(TrueOrFalse), (cellButton != null && cellButton.Enabled));
                DlkLogger.LogInfo("VerifyGridCellButtonByRowColumn() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyGridCellButtonByRowColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyGroupedRowExists", new String[] { "1|text|Text To Verify|Sample Grid Text" })]
        public void VerifyGroupedRowExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                var groupedRow = mElement.FindElements(By.XPath(".//*[@role='table']//*[contains(@class, 'grouped-row')]")).FirstOrDefault();
                DlkAssert.AssertEqual("VerifyGroupedRowExists() : " + mControlName, Convert.ToBoolean(TrueOrFalse), groupedRow != null);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyGroupedRowExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private class
        private string GetGridType()
        {
            string type = null;
            if (mElement.FindElements(By.XPath(".//*[contains(@data-testid,'dvList')]")).FirstOrDefault(x => x.Displayed) != null ||
                    mElement.GetAttribute("class").Contains("dvList"))
                type = "dvList"; // as of June 2020, projectsearchdetails_dialog in home page is the basis
            else if (mElement.GetAttribute("data-testid").Contains("dvDocuments"))
                type = "dvDocuments";
            else if (mElement.GetAttribute("data-testid").Contains("dvPartnerships"))
                type = "dvPartnerships";
            else if (mElement.GetAttribute("data-testid").Contains("dvLinks"))
                type = "dvLinks";
            else if (mElement.FindElements(By.XPath(".//*[@role='table']//*[contains(@role,'row')] | .//*[@role='table']//*[@class='header']")).FirstOrDefault() != null)
                type = "groupedRow";
            else
                type = "muiGrid"; // as of June 2020, projectsearchdetails in home page is the basis
            return type;
        }

        private string GetCellValueByRowAndColumnNumber(string rowNumber, string columnNumber)
        {
            IWebElement gridRow = null;
            string cellValue = null;
            switch (gridType)
            {
                case "dvList":
                    gridRow = mElement.FindElements(By.XPath(".//div[contains(@class,'dvListRow')][" + rowNumber + "]")).FirstOrDefault();
                    cellValue = gridRow.FindElements(By.XPath("./div[" + columnNumber + "]")).FirstOrDefault().Text;
                    break;
                case "dvDocuments":
                    //gridRow = mElement.FindElements(By.XPath(".//h6[contains(@class,'documentType ')][" + rowNumber + "]")).FirstOrDefault();
                    var cells = mElement.FindElements(By.XPath(".//h6[contains(@class,'documentType')]")).ToList();
                    cellValue = cells[Int32.Parse(rowNumber) - 1].Text;
                    break;
                case "dvPartnerships":
                    //gridRow = mElement.FindElements(By.XPath(".//h6[contains(@class,'documentType ')][" + rowNumber + "]")).FirstOrDefault();
                    var cellPartnership = mElement.FindElements(By.XPath(".//h6[contains(@class,'partnershipType ')]")).ToList();
                    cellValue = cellPartnership[Int32.Parse(rowNumber) - 1].Text;
                    break;
                case "dvLinks":
                    cellValue = mElement.FindElements(By.XPath("(.//*[contains(@class, 'MuiTableRow-root')]//*[contains(@class, 'MuiTableCell')])[" + rowNumber + "]")).FirstOrDefault().Text;
                    break;
                case "groupedRow":
                    gridRow = mElement.FindElements(By.XPath("(.//*[contains(@role,'cell')]/parent::*[@role='row'])[" + rowNumber + "]")).FirstOrDefault();
                    cellValue = DlkString.RemoveCarriageReturn(gridRow.FindElements(By.XPath(".//*[@role='cell'][" + columnNumber + "]")).FirstOrDefault().Text.Trim());
                    break;
                default:
                    gridRow = mElement.FindElements(By.XPath(".//div[contains(@class,'MuiTableRow-hover')][" + rowNumber + "]")).FirstOrDefault();
                    cellValue = gridRow.FindElements(By.XPath(".//p[" + columnNumber + "]")).FirstOrDefault().Text;
                    break;
            }
            return cellValue.TrimEnd();
        }

        private IWebElement GetCellByRowAndColumnNumber(string rowNumber, string columnNumber)
        {
            IWebElement gridRow = null;
            IWebElement cell = null;
            switch (gridType)
            {
                case "dvList":
                    gridRow = mElement.FindElements(By.XPath(".//div[contains(@class,'dvListRow')][" + rowNumber + "]")).FirstOrDefault();
                    cell = gridRow.FindElements(By.XPath("./div[" + columnNumber + "]")).FirstOrDefault();
                    break;
                case "dvDocuments":
                    var cells = mElement.FindElements(By.XPath(".//h6[contains(@class,'documentType')]")).ToList();
                    cell = cells[Int32.Parse(rowNumber) - 1];
                    break;
                case "dvPartnerships":
                    var cellPartnership = mElement.FindElements(By.XPath(".//h6[contains(@class,'partnershipType ')]")).ToList();
                    cell = cellPartnership[Int32.Parse(rowNumber) - 1];
                    break;
                case "dvLinks":
                    cell = mElement.FindElements(By.XPath(".//*[contains(@class, 'MuiTableRow-root')]//*[contains(@class, 'MuiTableCell')][" + rowNumber + "]")).FirstOrDefault();
                    break;
                case "groupedRow":
                    gridRow = mElement.FindElements(By.XPath("(.//*[contains(@role,'cell')]/parent::*[@role='row'])[" + rowNumber + "]")).FirstOrDefault();
                    cell = gridRow.FindElements(By.XPath(".//*[@role='cell'][" + columnNumber + "]")).FirstOrDefault();
                    break;
                default:
                    gridRow = mElement.FindElements(By.XPath(".//div[contains(@class,'MuiTableRow-hover')][" + rowNumber + "]")).FirstOrDefault();
                    cell = gridRow.FindElements(By.XPath(".//p[" + columnNumber + "]")).FirstOrDefault();
                    break;
            }
            return cell;
        }

        private IWebElement GetCellButton(IWebElement cell)
        {
            IWebElement cellButton = null;

            switch (gridType)
            {
                case "dvDocuments":
                    cellButton = cell.FindElements(By.XPath("./ancestor::*[@role='cell']/following-sibling::*//button")).FirstOrDefault();
                    break;
                default:
                    cellButton = cell.FindElements(By.XPath(".//button")).FirstOrDefault();
                    break;
            }

            return cellButton;
        }

        #endregion
    }
}
