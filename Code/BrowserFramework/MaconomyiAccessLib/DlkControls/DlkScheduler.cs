using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Scheduler")]
    public class DlkScheduler : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private Boolean IsInit = false;

        //Scheduler Type
        private string mSchedDayClass = "dayview";
        private string mSchedWeekClass = "weekview";
        private string mSchedMonthClass = "monthview";
        private string mSchedClass;

        //Day Header
        private string mSchedDayHeaderCon_XPath = ".//div[contains(@class,'k-scheduler-header-wrap')]";
        private string mSchedDayHeaders_XPath = ".//th//span[contains(@class,'k-link')]";
        private IList<IWebElement> mSchedDayHeaders;

        //Time Header
        private string mSchedTimeHeaderCon_XPath = ".//div[contains(@class,'k-scheduler-content')]//preceding-sibling::div[contains(@class,'k-scheduler-times')]";
        private IList<IWebElement> mSchedTimeHeaders;

        //All Day
        private string mSchedAllDayCon_XPath = ".//table[contains(@class,'k-scheduler-header-all-day')]";
        private IList<IWebElement> mSchedAllDayRows;

        //Content Cells
        private string mSchedMainCon_XPath = ".//div[contains(@class,'k-scheduler-content')]";
        private string mSchedMainCon2_XPath = ".//div[contains(@class,'k-scheduler-header-wrap')]";
        private string mSchedRowsUpper_XPath = ".//tr[contains(@class,'k-middle-row')]";
        private string mSchedRowsLower_XPath = ".//tr[not(contains(@class,'k-middle-row'))]";
        private IWebElement mSchedMainCon;
        private IList<IWebElement> mSchedRows_Upper;
        private IList<IWebElement> mSchedRows_Lower;
        private bool IsContainerAllDay = false;

        //Value of Content Cells
        private string mSchedDayValues_XPath = ".//div[@daytimeviewitem]";
        private string mSchedWeekValues_XPath = ".//div[@daytimeviewitem]";
        private string mSchedMonthValues_XPath = ".//div[@monthviewitem]";
        private string mSchedCellValueTitle_XPath = ".//div[contains(@class,'title')]";

        //Line Options
        private string mSchedLineOptions_XPath = ".//*[contains(@class,'icon-rowtools')]";
        private string mSchedDropdownMenu_XPath = "//div[contains(@class,'dropdown open')]//div[contains(@class,'dropdown-menu')]";
        private string mSchedDropdownItem_XPath = ".//div[contains(@class,'dropdown-item')]";

        #endregion

        #region CONSTRUCTORS
        public DlkScheduler(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkScheduler(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkButton(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkScheduler(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                IsInit = true;

                mSchedClass = GetSchedClass();

                GetDayHeaders();
                GetMainContent();

                if (mSchedClass.Contains(mSchedDayClass) || mSchedClass.Contains(mSchedWeekClass))
                {
                    GetAllDay();
                    GetTimeHeaders();
                }
            }
        }

        public string GetSchedClass()
        {
            string currentClass = mElement.GetAttribute("class") != null ?
                mElement.GetAttribute("class").ToString() : throw new Exception("Sched class not found. ");
            if(currentClass.Contains(mSchedDayClass))
            {
                return mSchedDayClass;
            }
            else if (currentClass.Contains(mSchedWeekClass))
            {
                return mSchedWeekClass;
            }
            else if (currentClass.Contains(mSchedMonthClass))
            {
                return mSchedMonthClass;
            }
            else
                throw new Exception("Scheduler type not yet supported. ");
        }

        public void GetDayHeaders()
        {
            IWebElement mSchedDayHeaderCon = mElement.FindElements(By.XPath(mSchedDayHeaderCon_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mSchedDayHeaderCon_XPath)) : throw new Exception("Scheduler day header container not found.");
            mSchedDayHeaders = mSchedDayHeaderCon.FindElements(By.XPath(mSchedDayHeaders_XPath)).Count > 0 ?
               mSchedDayHeaderCon.FindElements(By.XPath(mSchedDayHeaders_XPath)) : mSchedDayHeaderCon.FindElements(By.TagName("th"));
        }

        public void GetAllDay()
        {
            IWebElement mSchedAllDayCon = mElement.FindElements(By.XPath(mSchedAllDayCon_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mSchedAllDayCon_XPath)) : throw new Exception("Scheduler all day container not found.");
            mSchedAllDayRows = mSchedAllDayCon.FindElements(By.TagName("tr")).Count > 0 ?
               mSchedAllDayCon.FindElements(By.TagName("tr")) : throw new Exception("Scheduler all day rows not found.");
        }

        public void GetTimeHeaders()
        {
            IWebElement mSchedTimeHeaderCon = mElement.FindElements(By.XPath(mSchedTimeHeaderCon_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mSchedTimeHeaderCon_XPath)) : throw new Exception("Scheduler time header container not found.");
            IList<IWebElement> mAllSchedTimeHeaders = mSchedTimeHeaderCon.FindElements(By.XPath(mSchedDayHeaders_XPath)).Count > 0 ?
               mSchedTimeHeaderCon.FindElements(By.XPath(mSchedDayHeaders_XPath)) : mSchedTimeHeaderCon.FindElements(By.TagName("th"));

            mSchedTimeHeaders = new List<IWebElement>();
            foreach (IWebElement timeHeader in mAllSchedTimeHeaders)
            {
                new DlkBaseControl("Time Header", timeHeader).ScrollIntoViewUsingJavaScript();
                if (!String.IsNullOrEmpty(timeHeader.Text))
                    mSchedTimeHeaders.Add(timeHeader);
            }
        }

        public void GetMainContent()
        {
            mSchedMainCon = mElement.FindElements(By.XPath(mSchedMainCon_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mSchedMainCon_XPath)) : throw new Exception("Scheduler cells container not found.");

            if (mSchedClass.Contains(mSchedDayClass) || mSchedClass.Contains(mSchedWeekClass))
            {
                mSchedRows_Upper = mSchedMainCon.FindElements(By.XPath(mSchedRowsUpper_XPath)).Count > 0 ?
                mSchedMainCon.FindElements(By.XPath(mSchedRowsUpper_XPath)) : throw new Exception("Scheduler upper cell rows not found.");
                mSchedRows_Lower = mSchedMainCon.FindElements(By.XPath(mSchedRowsLower_XPath)).Count > 0 ?
                mSchedMainCon.FindElements(By.XPath(mSchedRowsLower_XPath)) : throw new Exception("Scheduler lower cell rows not found.");
            }
        }

        public void ClickLineOption(string dateVal, string startTime, string endTime, string lineOption)
        {
            List<IWebElement> targetCells = GetTargetCells();

            bool tFound = false;
            foreach (IWebElement currentCell in targetCells)
            {
                string cellLabel = currentCell.GetAttribute("aria-label") != null ?
                    currentCell.GetAttribute("aria-label").ToString().Trim() :
                    throw new Exception("Attribute aria-label in target cell not found. ");
                string[] splitVal = cellLabel.Split(',');
                string[] dateTimeVal = splitVal.FirstOrDefault().Split(' ');
                string currentDateVal = dateTimeVal.FirstOrDefault().Trim();
                string currentTimeVal = splitVal.FirstOrDefault().Replace(currentDateVal, "").Trim();
                string currentStartTime = currentTimeVal.Split('–').FirstOrDefault().Trim().Replace(" ", "");
                string currentEndTime = currentTimeVal.Split('–').LastOrDefault().Trim().Replace(" ", "");
                string targetDateVal = Convert.ToDateTime(dateVal).ToString("M/d/y");
                string targetStartTimeVal = startTime.Replace(" ", "");
                string targetEndTimeVal = endTime.Replace(" ", "");

                if (IsContainerAllDay)
                {
                    //Cell label value is different if target cell is from all day container, date is only needed
                    string allDayDateVal = splitVal[1].Trim() + "," + splitVal[2].Trim();
                    currentDateVal = Convert.ToDateTime(allDayDateVal).ToString("M/d/y");
                    currentStartTime = String.Empty;
                    currentEndTime = String.Empty;
                    targetStartTimeVal = String.Empty;
                    targetEndTimeVal = String.Empty;
                }

                if (targetDateVal.Equals(currentDateVal) && targetStartTimeVal.Equals(currentStartTime) && targetEndTimeVal.Equals(currentEndTime))
                {
                    IWebElement lineOptions = currentCell.FindElements(By.XPath(mSchedLineOptions_XPath)).Count > 0 ?
                    currentCell.FindElement(By.XPath(mSchedLineOptions_XPath)) : throw new Exception("Line options not found on target cell. ");
                    lineOptions.Click();
                    Thread.Sleep(2000);

                    IWebElement dropDownMenu = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSchedDropdownMenu_XPath)).Count > 0 ?
                        DlkEnvironment.AutoDriver.FindElement(By.XPath(mSchedDropdownMenu_XPath)) : throw new Exception("Dropdown menu from line option not found. ");
                    List<IWebElement> dropdownItems = dropDownMenu.FindElements(By.XPath(mSchedDropdownItem_XPath)).Where(x => x.Displayed).ToList();

                    foreach (IWebElement dropDownItem in dropdownItems)
                    {
                        if (dropDownItem.Text.Trim().ToLower().Contains(lineOption.ToLower()))
                        {
                            DlkLogger.LogInfo("Selecting drop down item: [" + lineOption + "]... ");
                            dropDownItem.Click();
                            tFound = true;
                            break;
                        }
                    }
                    if (tFound)
                        break;
                }
            }

            if (!tFound)
                throw new Exception("No line option found on Date: [" + dateVal + "] and Time: [" + startTime + " - " + endTime + "]");

        }

        public IWebElement GetTargetTimeRow(string timeOrRowIndex, string upperOrLower)
        {
            //Check if timeOrRowIndex is already a ROW INDEX
            IWebElement targetRow = null;
            int targetRowNumber = 0;
            if (int.TryParse(timeOrRowIndex, out targetRowNumber))
            {
                targetRowNumber = targetRowNumber > 0 ? targetRowNumber - 1 :
                     throw new Exception("[" + timeOrRowIndex + "] is not a valid input for parameter TimeOrRowIndex.");
            }
            else
            {
                bool tFound = false;
                //Check if timeOrRowIndex is looking for ALL DAY
                if(timeOrRowIndex.ToLower().Equals("all day"))
                {
                    targetRow = mSchedAllDayRows.FirstOrDefault();
                    tFound = true;
                }
                else
                {
                    //Check if timrOrRowIndex is looking for TIME HEADER
                    int r = 0;
                    foreach (IWebElement timeHeader in mSchedTimeHeaders)
                    {
                        new DlkBaseControl("Time Header", timeHeader).ScrollIntoViewUsingJavaScript();
                        if (timeOrRowIndex.ToLower().Equals(timeHeader.Text.ToLower()))
                        {
                            targetRowNumber = r;
                            tFound = true;
                            break;
                        }
                        r++;
                    }
                }
                
                if (!tFound)
                    throw new Exception("[" + timeOrRowIndex + "] is not found. ");
            }

            //If timeOrRowIndex is ALL DAY, skip checking if row type is UPPER OR LOWER
            if(!timeOrRowIndex.ToLower().Equals("all day"))
            {
                //Check if row type is UPPER or LOWER
                switch (upperOrLower.ToLower())
                {
                    case "upper":
                        if (targetRowNumber >= mSchedRows_Upper.Count)
                            throw new Exception("Time row index [" + timeOrRowIndex + "] not found. ");
                        else
                            targetRow = mSchedRows_Upper.ElementAt(targetRowNumber);
                        break;
                    case "lower":
                        if (targetRowNumber >= mSchedRows_Lower.Count)
                            throw new Exception("Time row index [" + timeOrRowIndex + "] not found. ");
                        else
                            targetRow = mSchedRows_Lower.ElementAt(targetRowNumber);
                        break;
                    default:
                        throw new Exception("Invalid selection. Row type must only be upper or lower.");
                }
            }

            return targetRow;
        }

        public IWebElement GetTargetCell(string rowIndex, string dayOrColumnIndex, string upperOrLower)
        {
            //Get target column index of day
            int targetColumnNumber = 0;
            if (!int.TryParse(dayOrColumnIndex, out targetColumnNumber))
                targetColumnNumber = GetColumnIndexFromDay(dayOrColumnIndex);
            else
                targetColumnNumber = Convert.ToInt32(dayOrColumnIndex) - 1;

            if (targetColumnNumber == -1)
                throw new Exception("Day column [" + dayOrColumnIndex + "] not found. ");

            //Get target time row or month row
            IWebElement targetRow = null;
            if (mSchedClass.Equals(mSchedMonthClass))
            {
                targetRow = GetTargetMonthRow(rowIndex);
            }
            else if (mSchedClass.Contains(mSchedDayClass) || mSchedClass.Contains(mSchedWeekClass))
            {
                targetRow = GetTargetTimeRow(rowIndex, upperOrLower);
            }
            else
                throw new Exception("Scheduler type not yet supported. ");

            //Get target cell
            List<IWebElement> targetCells = targetRow.FindElements(By.TagName("td")).Where(x => x.Displayed).ToList();
             IWebElement targetCell = targetCells.ElementAt(targetColumnNumber);

            if (targetCell == null)
                throw new Exception("Cell not found. ");

            return targetCell;
        }

        public List <IWebElement> GetTargetCells()
        {
            List<IWebElement> targetCells;

            if (mSchedClass.Equals(mSchedDayClass))
                targetCells = mSchedMainCon.FindElements(By.XPath(mSchedDayValues_XPath)).Where(x => x.Displayed).ToList();
            else if (mSchedClass.Equals(mSchedWeekClass))
                targetCells = mSchedMainCon.FindElements(By.XPath(mSchedWeekValues_XPath)).Where(x => x.Displayed).ToList();
            else if (mSchedClass.Equals(mSchedMonthClass))
                targetCells = mSchedMainCon.FindElements(By.XPath(mSchedMonthValues_XPath)).Where(x => x.Displayed).ToList();
            else
                throw new Exception("Scheduler type not yet supported. ");

            if(targetCells.Count == 0)
            {
                //Cell contents are in all day container
                DlkLogger.LogInfo("No cells found in main container. Switching to header container... ");
                mSchedMainCon = mElement.FindElements(By.XPath(mSchedMainCon2_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mSchedMainCon2_XPath)) : throw new Exception("Scheduler cells header container not found.");

                if (mSchedClass.Equals(mSchedDayClass))
                    targetCells = mSchedMainCon.FindElements(By.XPath(mSchedDayValues_XPath)).Where(x => x.Displayed).ToList();
                else if (mSchedClass.Equals(mSchedWeekClass))
                    targetCells = mSchedMainCon.FindElements(By.XPath(mSchedWeekValues_XPath)).Where(x => x.Displayed).ToList();
                else if (mSchedClass.Equals(mSchedMonthClass))
                    targetCells = mSchedMainCon.FindElements(By.XPath(mSchedMonthValues_XPath)).Where(x => x.Displayed).ToList();

                IsContainerAllDay = true;
            }

            return targetCells;
        }

        public IWebElement GetTargetMonthRow(string rowIndex)
        {
            IWebElement targetRow = null;
            int targetRowNumber = 0;
            if (!int.TryParse(rowIndex, out targetRowNumber) || targetRowNumber == 0)
                throw new Exception("[" + rowIndex + "] is not a valid input for parameter RowIndex.");

            IList<IWebElement> targetRows = mSchedMainCon.FindElements(By.TagName("tr")).Where(x => x.Displayed).ToList();
            targetRow = targetRows.ElementAt(targetRowNumber - 1);

            return targetRow;
        }

        public string GetTargetCellValue(string dateVal, string startTime, string endTime)
        {
            List<IWebElement> targetCells = GetTargetCells();

            string targetCellValue = "";
            bool tFound = false;
            foreach(IWebElement currentCell in targetCells)
            {
                string cellLabel = currentCell.GetAttribute("aria-label") != null ?
                    currentCell.GetAttribute("aria-label").ToString().Trim() :
                    throw new Exception("Attribute aria-label in target cell not found. ");
                string []splitVal = cellLabel.Split(',');
                string []dateTimeVal = splitVal.FirstOrDefault().Split(' ');
                string currentDateVal = dateTimeVal.FirstOrDefault().Trim();
                string currentTimeVal = splitVal.FirstOrDefault().Replace(currentDateVal, "").Trim();
                string currentStartTime = currentTimeVal.Split('–').FirstOrDefault().Trim().Replace(" ","");
                string currentEndTime = currentTimeVal.Split('–').LastOrDefault().Trim().Replace(" ", "");
                string targetDateVal = Convert.ToDateTime(dateVal).ToString("M/d/y");
                string targetStartTimeVal = startTime.Replace(" ", "");
                string targetEndTimeVal = endTime.Replace(" ", "");

                if (IsContainerAllDay)
                {
                    //Cell label value is different if target cell is from all day container, date is only needed
                    string allDayDateVal = splitVal[1].Trim() + "," + splitVal[2].Trim();
                    currentDateVal = Convert.ToDateTime(allDayDateVal).ToString("M/d/y");
                    currentStartTime = String.Empty;
                    currentEndTime = String.Empty;
                    targetStartTimeVal = String.Empty;
                    targetEndTimeVal = String.Empty;
                }

                if (targetDateVal.Equals(currentDateVal) && targetStartTimeVal.Equals(currentStartTime) && targetEndTimeVal.Equals(currentEndTime))
                {
                    targetCellValue = !String.IsNullOrEmpty(splitVal.LastOrDefault().Trim()) ?
                        splitVal.LastOrDefault().Trim() : null;

                    if(targetCellValue == null)
                    {
                        IWebElement targetTitleValue = currentCell.FindElements(By.XPath(mSchedCellValueTitle_XPath)).Count > 0 ?
                        currentCell.FindElement(By.XPath(mSchedCellValueTitle_XPath)) : throw new Exception("Title not found on target cell value. ");
                        targetCellValue = targetTitleValue.GetAttribute("title") != null ? targetTitleValue.GetAttribute("title") 
                            : throw new Exception("Title value not found on title attribute target cell value. ");
                    }
                    tFound = true;
                    break;
                }
            }

            if (!tFound)
                throw new Exception("No target value found on Date: [" + dateVal + "] and Time: [" + startTime + " - " + endTime + "]");

            return targetCellValue;
        }

        public bool IsTargetCellHasValue(string dateVal, string startTime, string endTime)
        {
            List<IWebElement> targetCells = GetTargetCells();

            string targetCellValue = "";
            bool tFound = false;
            foreach (IWebElement currentCell in targetCells)
            {
                string cellLabel = currentCell.GetAttribute("aria-label") != null ?
                    currentCell.GetAttribute("aria-label").ToString().Trim() :
                    throw new Exception("Attribute aria-label in target cell not found. ");
                string[] splitVal = cellLabel.Split(',');
                string[] dateTimeVal = splitVal.FirstOrDefault().Split(' ');
                string currentDateVal = dateTimeVal.FirstOrDefault().Trim();
                string currentTimeVal = splitVal.FirstOrDefault().Replace(currentDateVal, "").Trim();
                string currentStartTime = currentTimeVal.Split('–').FirstOrDefault().Trim().Replace(" ", "");
                string currentEndTime = currentTimeVal.Split('–').LastOrDefault().Trim().Replace(" ", "");
                string targetDateVal = Convert.ToDateTime(dateVal).ToString("M/d/y");
                string targetStartTimeVal = startTime.Replace(" ", "");
                string targetEndTimeVal = endTime.Replace(" ", "");

                if (IsContainerAllDay)
                {
                    //Cell label value is different if target cell is from all day container, date is only needed
                    string allDayDateVal = splitVal[1].Trim() + "," + splitVal[2].Trim();
                    currentDateVal = Convert.ToDateTime(allDayDateVal).ToString("M/d/y");
                    currentStartTime = String.Empty;
                    currentEndTime = String.Empty;
                    targetStartTimeVal = String.Empty;
                    targetEndTimeVal = String.Empty;
                }

                if (targetDateVal.Equals(currentDateVal) && targetStartTimeVal.Equals(currentStartTime) && targetEndTimeVal.Equals(currentEndTime))
                {
                    targetCellValue = !String.IsNullOrEmpty(splitVal.LastOrDefault().Trim()) ?
                        splitVal.LastOrDefault().Trim() : null;

                    if (targetCellValue == null)
                    {
                        IWebElement targetTitleValue = currentCell.FindElements(By.XPath(mSchedCellValueTitle_XPath)).Count > 0 ?
                        currentCell.FindElement(By.XPath(mSchedCellValueTitle_XPath)) : throw new Exception("Title not found on target cell value. ");
                        targetCellValue = targetTitleValue.GetAttribute("title") != null ? targetTitleValue.GetAttribute("title")
                            : throw new Exception("Title value not found on title attribute target cell value. ");
                    }
                    tFound = true;
                    break;
                }
            }

            if (!tFound)
                DlkLogger.LogInfo("No target value found on Date: [" + dateVal + "] and Time: [" + startTime + " - " + endTime + "]");

            return tFound;
        }

        #endregion

        #region PRIVATE METHODS
        private int GetColumnIndexFromDay(string Day)
        {
            int index = -1;
            for (int i = 0; i < mSchedDayHeaders.Count; i++)
            {
                if (mSchedDayHeaders[i].Text.ToLower().Equals(Day.ToLower()))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        #endregion

        #region KEYWORDS
        [Keyword("DoubleClickTimeRow", new String[] { "" })]
        public void DoubleClickTimeRow(String TimeOrRowIndex, String UpperOrLower)
        {
            try
            {
                Initialize();
                IWebElement targetTimeRow = GetTargetTimeRow(TimeOrRowIndex, UpperOrLower);
                DlkLogger.LogInfo("Executing double click on target time row... ");
                Actions action = new Actions(DlkEnvironment.AutoDriver);
                action.DoubleClick(targetTimeRow).Perform();

                DlkLogger.LogInfo("DoubleClickTimeRow() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickTimeRow() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickCell", new String[] { "" })]
        public void DoubleClickCell(String TimeOrRowIndex, String DayOrColumnIndex, String UpperOrLower)
        {
            try
            {
                Initialize();
                IWebElement targetCell = GetTargetCell(TimeOrRowIndex, DayOrColumnIndex, UpperOrLower);
                DlkLogger.LogInfo("Executing double click on target cell... ");
                Actions action = new Actions(DlkEnvironment.AutoDriver);
                action.DoubleClick(targetCell).Perform();

                DlkLogger.LogInfo("DoubleClickCell() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickCell() failed : " + e.Message, e);
            }
        }

        [Keyword("DragAndDropCell")]
        public void DragAndDropCell(String DragRowIndex, String DragColumnIndex, String DragUpperOrLower, String DropRowIndex, String DropColumnIndex, String DropUpperOrLower)
        {
            try
            {
                Initialize();
                IWebElement dragCell = GetTargetCell(DragRowIndex, DragColumnIndex, DragUpperOrLower);
                IWebElement dropCell = GetTargetCell(DropRowIndex, DropColumnIndex, DropUpperOrLower);

                DlkLogger.LogInfo("Dragging target cell to drop area... ");
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                actions.DragAndDrop(dragCell, dropCell).Perform();
                DlkLogger.LogInfo("DragAndDropCell() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DragAndDropCell() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new String[] { "" })]
        public void VerifyCellValue(String Date, String StartTime, String EndTime, String ExpectedValue)
        {
            try
            {
                Initialize();

                DateTime targetDateVal;
                if (!DateTime.TryParse(Date, out targetDateVal))
                    throw new Exception("[" + Date + "] is not a valid input for parameter Date.");

                DateTime targetStartTimeVal;
                if (!DateTime.TryParse(StartTime, out targetStartTimeVal))
                    throw new Exception("[" + StartTime + "] is not a valid input for parameter StartTime.");

                DateTime targetEndTimeVal;
                if (!DateTime.TryParse(EndTime, out targetEndTimeVal))
                    throw new Exception("[" + EndTime + "] is not a valid input for parameter EndTime.");

                DlkAssert.AssertEqual("VerifyCellValue(): ", GetTargetCellValue(Date, StartTime, EndTime).ToLower(), ExpectedValue.ToLower());
                DlkLogger.LogInfo("VerifyCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellHasValue", new String[] { "" })]
        public void VerifyCellHasValue(String Date, String StartTime, String EndTime, String TrueOrFalse)
        {
            try
            {
                Initialize();

                DateTime targetDateVal;
                if (!DateTime.TryParse(Date, out targetDateVal))
                    throw new Exception("[" + Date + "] is not a valid input for parameter Date.");

                DateTime targetStartTimeVal;
                if (!DateTime.TryParse(StartTime, out targetStartTimeVal))
                    throw new Exception("[" + StartTime + "] is not a valid input for parameter StartTime.");

                DateTime targetEndTimeVal;
                if (!DateTime.TryParse(EndTime, out targetEndTimeVal))
                    throw new Exception("[" + EndTime + "] is not a valid input for parameter EndTime.");

                bool trueOrFalse;
                if (!Boolean.TryParse(TrueOrFalse, out trueOrFalse))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                DlkAssert.AssertEqual("VerifyCellHasValue(): ", IsTargetCellHasValue(Date, StartTime, EndTime), trueOrFalse);
                DlkLogger.LogInfo("VerifyCellHasValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellHasValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellValue", new String[] { "" })]
        public void GetCellValue(String Date, String StartTime, String EndTime, String VariableName)
        {
            try
            {
                Initialize();

                DateTime targetDateVal;
                if (!DateTime.TryParse(Date, out targetDateVal))
                    throw new Exception("[" + Date + "] is not a valid input for parameter Date.");

                DateTime targetStartTimeVal;
                if (!DateTime.TryParse(StartTime, out targetStartTimeVal))
                    throw new Exception("[" + StartTime + "] is not a valid input for parameter StartTime.");

                DateTime targetEndTimeVal;
                if (!DateTime.TryParse(EndTime, out targetEndTimeVal))
                    throw new Exception("[" + EndTime + "] is not a valid input for parameter EndTime.");

                string actualValue = GetTargetCellValue(Date, StartTime, EndTime);
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickLineOptions", new String[] { "" })]
        public void ClickLineOptions(String Date, String StartTime, String EndTime, String LineOption)
        {
            try
            {
                Initialize();

                DateTime targetDateVal;
                if (!DateTime.TryParse(Date, out targetDateVal))
                    throw new Exception("[" + Date + "] is not a valid input for parameter Date.");

                DateTime targetStartTimeVal;
                if (!DateTime.TryParse(StartTime, out targetStartTimeVal))
                    throw new Exception("[" + StartTime + "] is not a valid input for parameter StartTime.");

                DateTime targetEndTimeVal;
                if (!DateTime.TryParse(EndTime, out targetEndTimeVal))
                    throw new Exception("[" + EndTime + "] is not a valid input for parameter EndTime.");

                ClickLineOption(Date, StartTime, EndTime, LineOption);
                DlkLogger.LogInfo("ClickLineOptions() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickLineOptions() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    String tempValue = mElement.GetAttribute("class");
                    if (tempValue.Contains("disabled"))
                    {
                        DlkLogger.LogInfo("disabled");
                        ActValue = "true";
                    }
                }
                DlkAssert.AssertEqual("VerifyAttribute()", ExpectedValue.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
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
        #endregion

    }
}
