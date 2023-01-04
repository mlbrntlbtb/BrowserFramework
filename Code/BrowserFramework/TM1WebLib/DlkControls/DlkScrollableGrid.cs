using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using TM1WebLib.DlkUtility;
using TM1WebLib.DlkControls;


namespace TM1WebLib.DlkControls
{
    [ControlType("ScrollableGrid")]
    public class DlkScrollableGrid : DlkBaseControl
    {
        private List<DlkScrollableGridHeader> mHeaders = new List<DlkScrollableGridHeader>();
        private IWebElement mTopRightPanel;
        private IWebElement mTopLeftPanel;
        private IWebElement mBottomLeftPanel;
        private IWebElement mBottomRightPanel;
        private IWebElement mTopLeftBody;
        private IWebElement mTopRightBody;
        private IWebElement mBottomLeftBody;
        private IWebElement mBottomRightBody;

        private int topLeftHeaderCount = 0;


        public DlkScrollableGrid(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkScrollableGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkScrollableGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
            InitializePanels();
            InitializeTables();
            mHeaders = GetHeaders();
        }

        private void InitializeTables()
        {
            mTopLeftBody = mTopLeftPanel.FindElement(By.XPath("./descendant::tbody[1]"));
            mTopRightBody = mTopRightPanel.FindElement(By.XPath("./descendant::tbody[1]"));
            mBottomLeftBody = mBottomLeftPanel.FindElement(By.XPath("./descendant::tbody[1]"));
            mBottomRightBody = mBottomRightPanel.FindElement(By.XPath("./descendant::tbody[1]"));
        }

        private void InitializePanels()
        {
            DlkBaseControl topPanel = new DlkBaseControl("topPanel", "CLASS_DISPLAY", "topSheetPanel");
            topPanel.FindElement();
            mTopLeftPanel = topPanel.mElement.FindElement(By.XPath("./div[1]"));
            mTopRightPanel = topPanel.mElement.FindElement(By.XPath("./div[2]"));

            DlkBaseControl bottomPanel = new DlkBaseControl("bottomPanel", "CLASS_DISPLAY", "bottomSheetPanel");
            bottomPanel.FindElement();
            mBottomLeftPanel = bottomPanel.mElement.FindElement(By.XPath("./div[@class='bottomScrollablePanel']/div[1]"));
            mBottomRightPanel = bottomPanel.mElement.FindElement(By.XPath("./div[@class='bottomScrollablePanel']/div[2]"));
        }

        private List<DlkScrollableGridHeader> GetHeaders()
        {
            List<DlkScrollableGridHeader> ret = new List<DlkScrollableGridHeader>();
            /* Get headers in left top panel */
            foreach (IWebElement topLeftRow in mTopLeftBody.FindElements(By.XPath("./descendant::tr")))
            {
                if (string.IsNullOrEmpty(topLeftRow.Text))
                {
                    continue;
                }
                foreach(IWebElement topLeftCol in topLeftRow.FindElements(By.XPath("./descendant::td")))
                {
                    string txt = topLeftCol.GetAttribute("textContent");
                    if (!string.IsNullOrEmpty(txt))
                    {
                        ret.Add(
                            new DlkScrollableGridHeader
                            {
                                HeaderText = txt,
                                SubHeaderText = string.Empty,//empty for now
                                HeaderQuadrant = Quadrant.TopLeft
                            }
                        );
                        topLeftHeaderCount++;
                    }
                }
                break;
            }

            /* Get headers in right top panel */
            foreach (IWebElement topRightCol in mElement.FindElements(By.XPath("./td")))
            {
                string txt = topRightCol.GetAttribute("textContent");
                //if (string.IsNullOrEmpty(txt))
                //{
                //    continue;
                //}
                ret.Add(
                    new DlkScrollableGridHeader
                    {
                        HeaderText = string.IsNullOrEmpty(txt) ? "BLANK_COL" : txt,
                        SubHeaderText = string.Empty,//empty for now
                        HeaderQuadrant = Quadrant.TopRight
                    }
                );
            }

            return ret;
        }

        //[Keyword("GetRowWithColumnValue")]
        //public void GetRowWithColumnValue(string ColumnName, string CellValue, string VariableName)
        //{
        //    try
        //    {
        //        DlkLogger.LogInfo("Successfully executed Set()");

        //    }
        //    catch(Exception e)
        //    {
        //        throw new Exception("GetRowWithColumnValue() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("GetRowWithMultipleColumnValues")]
        //public void GetRowWithMultipleColumnValues(string ColumnNames, string CellValues, string VariableName)
        //{
        //    try
        //    {
        //        DlkLogger.LogInfo("Successfully executed Set()");

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("GetRowWithMultipleColumnValues() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("SetCellValue")]
        //public void SetCellValue(string RowNumber, string ColumnName)
        //{
        //    try
        //    {
        //        DlkLogger.LogInfo("Successfully executed Set()");

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("SetCellValue() failed : " + e.Message, e);
        //    }
        //}

        [Keyword("VerifyCellValue")]
        public void VerifyCellValue(string RowNumber, string ColumnName, string ExpectedValue)
        {
            try
            {
                Initialize();
                DlkScrollableGridHeader targetCol = mHeaders.Find(x => x.HeaderText == ColumnName);

                if (targetCol == null)
                {
                    throw new Exception("Target column '" + ColumnName + "' not found");
                }

                int iColumn = -1;
                int iRowNumber; // 1 based

                if (!int.TryParse(RowNumber, out iRowNumber))
                {
                    throw new Exception("RowNumber variable '" + RowNumber + "' is not numeric");
                }

                if (targetCol.HeaderQuadrant == Quadrant.TopLeft)
                {
                    iColumn = mHeaders.IndexOf(targetCol); // 0 based
                    if (iColumn < 0)
                    {
                        throw new Exception("There was a problem accessing target column");
                    }
                    IWebElement targetRow = mBottomLeftBody.FindElement(By.XPath("./descendant::tr[" + RowNumber + "]"));
                    IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    string actVal = targetCell.GetAttribute("textContent") == null ? string.Empty : targetCell.GetAttribute("textContent").Trim();
                    DlkAssert.AssertEqual("VerifyCellValue()", ExpectedValue, actVal);
                }
                else
                {
                    iColumn = mHeaders.IndexOf(targetCol) - mHeaders.FindAll(x => x.HeaderQuadrant == Quadrant.TopLeft).Count; // 0 based
                    if (iColumn < 0)
                    {
                        throw new Exception("There was a problem accessing target column");
                    }
                    IWebElement targetRow = mBottomRightBody.FindElement(By.XPath("./descendant::tr[" + RowNumber + "]"));
                    IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    string actVal = targetCell.GetAttribute("textContent") == null ? string.Empty : targetCell.GetAttribute("textContent").Trim();
                    DlkAssert.AssertEqual("VerifyCellValue()", ExpectedValue, actVal);
                }

                DlkLogger.LogInfo("VerifyCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectCells")]
        public void SelectCells(string ColumnName, string FirstCell, string LastCell)
        {
            try
            {
                Initialize();
                DlkScrollableGridHeader targetCol = mHeaders.Find(x => x.HeaderText == ColumnName);

                if (targetCol == null)
                {
                    throw new Exception("Target column '" + ColumnName + "' not found");
                }

                int iColumn = -1;
                int iFirstCell; // 1 based
                int iLastCell; // 1 based

                if (!int.TryParse(FirstCell, out iFirstCell))
                {
                    throw new Exception("FirstCell variable '" + FirstCell + "' is not numeric");
                }

                if (!int.TryParse(LastCell, out iLastCell))
                {
                    throw new Exception("LastCell variable '" + LastCell + "' is not numeric");
                }

                if (targetCol.HeaderQuadrant == Quadrant.TopLeft)
                {
                    iColumn = mHeaders.IndexOf(targetCol); // 0 based
                    if (iColumn < 0)
                    {
                        throw new Exception("There was a problem accessing target column");
                    }
                    IWebElement targetRow = mBottomLeftBody.FindElement(By.XPath("./descendant::tr[" + FirstCell + "]"));
                    IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    string actVal = targetCell.GetAttribute("textContent") == null ? string.Empty : targetCell.GetAttribute("textContent").Trim();
                    IWebElement endRow = mBottomLeftBody.FindElement(By.XPath("./descendant::tr[" + LastCell + "]"));
                    IWebElement endCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    targetCell.Click();
                    Thread.Sleep(500);
                    OpenQA.Selenium.Interactions.Actions shiftAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    OpenQA.Selenium.Interactions.Actions downAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    targetCell.Click();
                    shiftAction = shiftAction.KeyDown(Keys.Shift);
                    shiftAction.Perform();
                    for (int num = 0; num < iLastCell; num++)
                    {
                        downAction = downAction.SendKeys(Keys.ArrowDown);
                        downAction.Perform();
                        Thread.Sleep(1600);
                    }
                    shiftAction = shiftAction.KeyUp(Keys.Shift);
                    shiftAction.Perform();
                }
                else
                {
                    iColumn = mHeaders.IndexOf(targetCol) - mHeaders.FindAll(x => x.HeaderQuadrant == Quadrant.TopLeft).Count; // 0 based
                    if (iColumn < 0)
                    {
                        throw new Exception("There was a problem accessing target column");
                    }
                    IWebElement targetRow = mBottomRightBody.FindElement(By.XPath("./descendant::tr[" + FirstCell + "]"));
                    IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    OpenQA.Selenium.Interactions.Actions rightAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    rightAction = rightAction.SendKeys(Keys.Right);
                    if (!targetCell.Displayed)
                    {
                        IWebElement startingRow = mBottomRightBody.FindElement(By.XPath("./descendant::tr[1]"));
                        IWebElement startingCell = startingRow.FindElement(By.XPath("./descendant::td[1]"));
                        IWebElement lastCell = startingCell;
                        for (int rowNum = 1; targetCell.Displayed != true; rowNum++)
                        {
                            startingCell = startingRow.FindElement(By.XPath("./descendant::td[" + rowNum + "]"));
                            if (!startingCell.Displayed)
                            {
                                rowNum--;
                                //startingCell = startingRow.FindElement(By.XPath("./descendant::td[" + rowNum + "]"));
                                //startingCell.Click();
                                //startingCell.Click();
                                //startingCell.SendKeys(Keys.ArrowLeft);
                                //mBottomRightPanel.SendKeys(Keys.ArrowRight);
                                //mBottomRightPanel.SendKeys(Keys.ArrowRight);
                                IWebElement scrollElement = mElement.FindElement(By.XPath("/descendant::*[@id='dijit_layout_TabContainer_2']"));
                                OpenQA.Selenium.Interactions.Actions builder = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                                builder.MoveToElement(scrollElement, 1400, 670).Click().Build().Perform();
                                Thread.Sleep(3000);
                                continue;
                            }
                            startingCell.SendKeys(Keys.ArrowRight);
                            Thread.Sleep(1000);
                            lastCell = startingCell;
                        }
                        lastCell.Click();
                        startingCell.SendKeys(Keys.ArrowRight);
                        Thread.Sleep(1000);
                    }
                    IWebElement endRow = mBottomRightBody.FindElement(By.XPath("./descendant::tr[" + LastCell + "]"));
                    IWebElement endCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    targetCell.Click();
                    Thread.Sleep(500);
                    OpenQA.Selenium.Interactions.Actions shiftAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    OpenQA.Selenium.Interactions.Actions downAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    targetCell.Click();
                    shiftAction = shiftAction.KeyDown(Keys.Shift);
                    shiftAction.Perform();
                    for (int num = 0; num < iLastCell-1; num++)
                    {
                        downAction = downAction.SendKeys(Keys.ArrowDown);
                        downAction.Perform();
                        Thread.Sleep(1000);
                    }
                    shiftAction = shiftAction.KeyUp(Keys.Shift);
                    shiftAction.Perform();
                }
                Thread.Sleep(2000);
                DlkLogger.LogInfo("SelectCells() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCells() failed : " + e.Message, e);
            }
        }

        [Keyword("RightClickCell")]
        public void RightClickCell(string ColumnName, string RowNumber)
        {
            try
            {
                Initialize();
                DlkScrollableGridHeader targetCol = mHeaders.Find(x => x.HeaderText == ColumnName);

                if (targetCol == null)
                {
                    throw new Exception("Target column '" + ColumnName + "' not found");
                }

                int iColumn = -1;
                int iRowNumber; // 1 based

                if (!int.TryParse(RowNumber, out iRowNumber))
                {
                    throw new Exception("RowNumber variable '" + RowNumber + "' is not numeric");
                }

                if (targetCol.HeaderQuadrant == Quadrant.TopLeft)
                {
                    iColumn = mHeaders.IndexOf(targetCol); // 0 based
                    if (iColumn < 0)
                    {
                        throw new Exception("There was a problem accessing target column");
                    }
                    IWebElement targetRow = mBottomLeftBody.FindElement(By.XPath("./descendant::tr[" + RowNumber + "]"));
                    IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction = mAction.ContextClick(targetCell);
                    mAction.Perform();
                    Thread.Sleep(1000);
                    mAction.Perform();
                }
                else
                {
                    iColumn = mHeaders.IndexOf(targetCol) - mHeaders.FindAll(x => x.HeaderQuadrant == Quadrant.TopLeft).Count; // 0 based
                    if (iColumn < 0)
                    {
                        throw new Exception("There was a problem accessing target column");
                    }
                    IWebElement targetRow = mBottomRightBody.FindElement(By.XPath("./descendant::tr[" + RowNumber + "]"));
                    IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + (iColumn + 1).ToString() + "]"));
                    if (!targetCell.Displayed)
                    {
                        mBottomRightPanel.SendKeys(Keys.PageUp);
                        Thread.Sleep(1000);
                    }
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction = mAction.ContextClick(targetCell);
                    mAction.Perform();
                    Thread.Sleep(1000);
                    mAction.Perform();
                }
                Thread.Sleep(2000);
                DlkLogger.LogInfo("RightClickCell() passed");
            }
            catch (Exception e)
            {
                throw new Exception("RightClickCell() failed : " + e.Message, e);
            }
        }

        //[Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        //public void VerifyExists(String strExpectedValue)
        //{
        //    try
        //    {
        //        base.VerifyExists(Convert.ToBoolean(strExpectedValue));
        //        DlkLogger.LogInfo("VerifyExists() passed");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyExists() failed : " + e.Message, e);
        //    }
        //}
    }

    public class DlkScrollableGridHeader
    {
        public string HeaderText { get; set; }
        public string SubHeaderText { get; set; }
        public Quadrant HeaderQuadrant { get; set; }
    }

    public enum Quadrant
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
}
