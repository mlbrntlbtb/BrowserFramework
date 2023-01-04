using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;
using MaconomyiAccessLib.DlkSystem;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {

        #region PRIVATE VARIABLES

        private Boolean IsInit = false;
        private string mTableClass;
        private List<IWebElement> mColumnHeaders;
        private List<IWebElement> mTableRows;
        private const String Results = "result-wrapper";
        private const String DDown = "dropdown-menu";
        private const String DXGV_CLASS = "dxgv";
        private IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

        #endregion

        #region CONSTRUCTORS

        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                mTableClass = GetTableClass();
                mColumnHeaders = GetColumnHeaders();
                mTableRows = GetRows();
                IsInit = true;
            }
        }

        public string GetCellValue(int LineIndex, string ColNameOrIndex)
        {
            string actualValue = "";
            IWebElement targetCell = GetCell(LineIndex.ToString(), ColNameOrIndex);

            switch (mTableClass)
            {
                case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only
                    string DXGV_checkBoXPath = ".//*[contains(@class,'CheckBox')]";
                    string DXGV_checkBoxValueXPath = ".//*[contains(@class,'Checked')]";
                    //Check for checkbox field
                    if(targetCell.FindElements(By.XPath(DXGV_checkBoXPath)).Count > 0)
                        actualValue = targetCell.FindElements(By.XPath(DXGV_checkBoxValueXPath)).Count > 0 ? "True" : "False";
                    //Default field
                    else
                        actualValue = GetDefaultCellValue(targetCell);
                    break;
                default:
                    string checkBoxXPath = ".//*[contains(@class,'boolean')] | .//*[contains(@class, 'checkbox boolean')]";
                    string checkBoxValueXPath = ".//*[contains(@class,'fa-check')] | .//*[contains(@class, 'checked')]";
                    string textBoxTagName = "input";

                    //Check for checkbox field
                    if (targetCell.FindElements(By.XPath(checkBoxXPath)).Count > 0)
                        actualValue = targetCell.FindElements(By.XPath(checkBoxValueXPath)).Count > 0 ? "True" : "False";
                    //Check for textbox field
                    else if (targetCell.FindElements(By.TagName(textBoxTagName)).Count > 0)
                        actualValue = DlkString.RemoveCarriageReturn(new DlkBaseControl("Input", targetCell.FindElement(By.TagName(textBoxTagName))).GetValue());
                    //Default field
                    else
                        actualValue = GetDefaultCellValue(targetCell);
                    break;
            }
            return actualValue;
        }

        public IWebElement GetCell(string LineIndex, string ColNameOrIndex)
        {
            //get target row
            if (!int.TryParse(LineIndex, out int targetRowNumber) || targetRowNumber == 0)
                throw new Exception("Line Index [" + LineIndex + "] not valid.");

            IWebElement targetRow = mTableRows.Count > 0 ? mTableRows.ElementAt(targetRowNumber - 1)
                : throw new Exception("Rows not found.");

            //get target column number from column headers
            if (!int.TryParse(ColNameOrIndex, out int targetColumnNumber))
                targetColumnNumber = GetColumnNumberFromName(ColNameOrIndex);

            if (targetColumnNumber == -1)
                throw new Exception("Column '" + ColNameOrIndex + "' was not found.");

            List<IWebElement> targetCells;
            IWebElement targetCell = null;
            string DEFAULT_CellsXPath1 = "./descendant::td/dm-edit-cell-wrapper//ancestor::td[@kendogridcell]"; //cells on edit mode and non edit mode
            string DEFAULT_CellsXPath2 = "./descendant::td/dm-presentation-cell//ancestor::td[@kendogridcell]"; //cells on non-edit mode
            string DXGV_CellsXPath = ".//td[contains(@class,'dxgv')][not(contains(@class,'dxgvDetailButton'))]";

            switch (mTableClass)
            {
                case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only
                    targetCells = targetRow.FindElements(By.XPath(DXGV_CellsXPath)).Where(x => x.Displayed).ToList();
                    targetCell = targetCells.ElementAt(targetColumnNumber - 1);
                    break;

                default:
                    targetCells = targetRow.FindElements(By.XPath(DEFAULT_CellsXPath1)).ToList();
                    targetCells.AddRange(targetRow.FindElements(By.XPath(DEFAULT_CellsXPath2)));

                    new DlkBaseControl("FirstCol", targetCells.FirstOrDefault()).ScrollIntoViewUsingJavaScript();

                    targetCell = targetCells.ElementAt(targetColumnNumber - 1);
                    if (targetCell == null)
                        throw new Exception("Cell not found.");

                    //Set focus to target cell then re-initialize
                    if (targetCell.FindElements(By.TagName("dm-presentation-cell")).Count > 0)
                    {
                        ClickUsingJavaScriptThenSelenium(targetCell);
                        targetRow = null;
                        targetCell = null;
                        targetCells.Clear();
                        Thread.Sleep(2000);
                        mTableRows = GetRows();
                        targetRow = mTableRows.Count > 0 ? mTableRows.ElementAt(targetRowNumber - 1)
                        : throw new Exception("Rows not found.");

                        targetCells = targetRow.FindElements(By.XPath(DEFAULT_CellsXPath1)).ToList();
                        targetCells.AddRange(targetRow.FindElements(By.XPath(DEFAULT_CellsXPath2)));
                        targetCell = targetCells.ElementAt(targetColumnNumber - 1);
                    }
                    break;
            }

            if (targetCell == null)
                throw new Exception("Cell not found.");

            return targetCell;
        }

        public void SetValueToCell(String ColNameOrIndex, String LineIndex, String Value)
        {
            IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

            if (!targetCell.Displayed)
            {
                ClickUsingJavaScriptThenSelenium(targetCell);
                Thread.Sleep(1000);
            }

            switch (mTableClass)
            {
                case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only
                    string DXGV_checkBoXPath = ".//*[contains(@class,'CheckBox')]";
                    string DXGV_checkBoxButtonXPath = ".//span[contains(@class,'CheckBox')]";
                    string DXGV_checkBoxValueXPath = ".//*[contains(@class,'Checked')]";

                    //Check for checkbox field
                    if (targetCell.FindElements(By.XPath(DXGV_checkBoXPath)).Where(x=>x.Displayed).Any())
                    {
                        bool expectedValue = Convert.ToBoolean(Value);
                        bool actualValue = targetCell.FindElements(By.XPath(DXGV_checkBoxValueXPath)).Where(x => x.Displayed).Any();
                        if ((!expectedValue & actualValue) || (expectedValue & !actualValue))
                        {
                            IWebElement targetCheckBox = targetCell.FindElement(By.XPath(DXGV_checkBoxButtonXPath));
                            targetCheckBox.Click();
                        }
                    }
                    //Default field
                    else
                    {
                        targetCell.Click(); //Select target cell
                        IWebElement targetInput = targetCell.FindElements(By.TagName("input")).Where(x => x.Displayed).Any() ?
                            targetCell.FindElement(By.TagName("input")) : throw new Exception("Input field from target cell not found.");

                        targetInput.Clear(); //Clear input field
                        Thread.Sleep(1000);
                        targetCell.Click();
                        targetInput.SendKeys(Value); //Set value to cell
                    }
                    break;
                default:
                    string cellCheckBox_XPath = ".//dm-edit-cell//div[contains(@class,'checkbox')] | .//dm-check";
                    string cellCheckBoxButton_XPath = ".//dm-edit-cell//div[contains(@class,'checkbox')]//button | .//button[contains(@class,'check')]";
                    string cellCheckBoxChecked_XPath = "./descendant::button[contains(@class,'fa fw fa-check checked')] | ./descendant::button//i[contains(@class,'check')] | ./descendant::button[contains(@class,'checked')]";

                    //Check for checkbox field
                    if (targetCell.FindElements(By.XPath(cellCheckBox_XPath)).Where(x => x.Displayed).Any())
                    {
                        bool expectedValue = Convert.ToBoolean(Value);
                        bool actualValue = targetCell.FindElements(By.XPath(cellCheckBoxChecked_XPath)).Where(x => x.Displayed).Any();
                        if ((!expectedValue & actualValue) || (expectedValue & !actualValue))
                        {
                            IWebElement targetCheckBox = targetCell.FindElement(By.XPath(cellCheckBoxButton_XPath));
                            ClickUsingJavaScriptThenSelenium(targetCheckBox);
                        }
                    }
                    //Default field
                    else
                    {
                        IWebElement targetInputControl = null;
                        if (DlkEnvironment.mBrowser.ToLower().Equals("safari"))
                        {
                            bool bOk = false;
                            int attempts = 0;
                            while (!bOk && attempts < 10)
                            {
                                try
                                {
                                    attempts++;
                                    DlkLogger.LogInfo("Attempting to set cell value... Attempt: " + attempts.ToString());
                                    Initialize();
                                    targetCell = GetCell(LineIndex, ColNameOrIndex);
                                    targetCell.Click();
                                    Thread.Sleep(1000);

                                    // need to re-initialize, DOM changes upon click
                                    Initialize();
                                    targetCell = GetCell(LineIndex, ColNameOrIndex);
                                    targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));
                                    targetInputControl.Clear();
                                    bOk = true;
                                }
                                catch
                                {
                                    /* if clear fails on first try, the element is no longer attached to DOM. Try again */
                                }
                            }
                        }

                        string cellInput_XPath = "./descendant::input";
                        string cellTextArea_XPath = "./descendant::textarea";

                        targetInputControl = targetCell.FindElements(By.XPath(cellInput_XPath)).Where(x => x.Displayed).Any() ?
                            targetCell.FindElement(By.XPath(cellInput_XPath)) :
                            targetCell.FindElements(By.XPath(cellTextArea_XPath)).Where(x => x.Displayed).Any() ?
                            targetCell.FindElement(By.XPath(cellTextArea_XPath)) :
                            throw new Exception("Target input field not found.");

                        DlkBaseControl targetCtl = new DlkBaseControl("TargetControl", targetInputControl);
                        try
                        {
                            targetInputControl.Clear();
                        }
                        catch
                        {
                            targetInputControl.Clear();
                        }

                        targetInputControl.SendKeys(Value);

                        /* checking */
                        if (targetCtl.GetValue() != Value)
                        {
                            targetInputControl.Clear();
                            targetInputControl.SendKeys(Value);
                        }

                        /* Insert pause so value won't become blank */
                        Thread.Sleep(1000);
                    }
                    break;
            }
           
        }

        public void OpenDropdownList(IWebElement targetCell, DlkBaseControl results)
        {
            string xpath_Ddown = "./descendant::*[contains(@class,'dropdown')]";
            if (targetCell.FindElements(By.XPath(xpath_Ddown)).Count == 0)
                throw new Exception("Cell is not a dropdown type.");

            int attempts = 0;
            string xpath_Button1 = "./descendant::dm-filter-toggle";
            string xpath_Button2 = "./descendant::a[contains(@class,'icon-droparrow')]";
            string xpath_Button3 = "./descendant::a[1]";

            IWebElement button = targetCell.FindWebElementCoalesce(By.XPath(xpath_Button1), By.XPath(xpath_Button2), By.XPath(xpath_Button3));
            if (button == null)
                throw new Exception("Button not found.");

            while (!results.Exists(1) && attempts < 3)
            {
                DlkLogger.LogInfo("Opening drop down list on [" + ++attempts + "] attempt(s) ...");
                button.Click();
                Thread.Sleep(2000);
                WaitDropdownSpinner(targetCell);
            }

            if (results.Exists(1))
                DlkLogger.LogInfo("OpenDropDownList() passed.");
            else
                throw new Exception("Drop down list has not been opened.");
        }

        public void CloseDropdownList(IWebElement targetCell, DlkBaseControl results)
        {
            string xpath_Button1 = "./descendant::dm-filter-toggle";
            string xpath_Button2 = "./descendant::a[contains(@class,'icon-droparrow')]";
            string xpath_Button3 = "./descendant::a[1]";

            IWebElement button = targetCell.FindWebElementCoalesce(By.XPath(xpath_Button1), By.XPath(xpath_Button2), By.XPath(xpath_Button3));
            if (button == null)
                throw new Exception("Button not found.");

            if (results.Exists(1))
            {
                button.Click();
                DlkLogger.LogInfo("CloseDropdownList() passed.");
            }
        }

        public void WaitDropdownSpinner(IWebElement targetCell)
        {
            string xpath_Spinner = "./descendant::div[contains(@class,'spinner')]";

            IWebElement spinner = targetCell.FindElements(By.XPath(xpath_Spinner)).Count > 0 ?
                targetCell.FindElement(By.XPath(xpath_Spinner)) : null;
            if (spinner == null)
                throw new Exception("Spinner not found.");

            int sleep = 1;
            while (spinner.GetAttribute("style").Contains("visibility: visible"))
            {
                if (sleep < 21)
                {
                    DlkLogger.LogInfo("Waiting for spinner to finish loading in [" + sleep.ToString() + "] second(s) ...");
                    Thread.Sleep(1000);
                    sleep++;
                }
                else
                    throw new Exception("Waiting for dropdown to finish loading has reached it limit (20s).");
            }
        }

        public void ClickUsingJavaScriptThenSelenium(IWebElement targetControl)
        {
            //Solution for Issues when using ClickUsingJavaScript function
            DlkBaseControl TargetControl = new DlkBaseControl("Target Control", targetControl);
            try
            {
                TargetControl.ClickUsingJavaScript();
            }
            catch (Exception)
            {
                if(targetControl.FindElements(By.TagName("i")).Count > 0 || targetControl.FindElements(By.TagName("button")).Count > 0)
                {
                    Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.MoveToElement(targetControl, 1, 1).Click().Perform();
                }
                else
                {
                    targetControl.Click();
                }
            }
        }

        public List <IWebElement> GetFilterCells()
        {
            List<IWebElement> filterCells;
            IWebElement filterRow;
            string DEFAULT_tableHeader_XPath = ".//div[contains(@class,'k-grid-header')]/table";
            string DEFAULT_filterRow_XPath = ".//tr[contains(@class,'k-filter-row')]";
            string DEFAULT_filterCell_XPath = ".//td[@kendogridfiltercell]//dm-list-view-filter";

            string DXGV_filterRow_XPath = ".//tr[contains(@class,'dxgvFilterRow_DevEx')] ";
            string DXGV_filterCell_XPath = ".//td[contains(@class,'dxgv')]";

            switch (mTableClass)
            {
                case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only
                    filterRow = mElement.FindElements(By.XPath(DXGV_filterRow_XPath)).Any() ?
                       mElement.FindElement(By.XPath(DXGV_filterRow_XPath)) : throw new Exception("Filter row not found.");
                    filterCells = filterRow.FindElements(By.XPath(DXGV_filterCell_XPath)).ToList();
                    break;
                default:
                    IWebElement tableHeader = mElement.FindElement(By.XPath(DEFAULT_tableHeader_XPath));
                    filterRow = tableHeader.FindElements(By.XPath(DEFAULT_filterRow_XPath)).Any() ?
                        tableHeader.FindElement(By.XPath(DEFAULT_filterRow_XPath)) : throw new Exception("Filter row not found.");
                    filterCells = filterRow.FindElements(By.XPath(DEFAULT_filterCell_XPath)).ToList();
                    break;
            }
            return filterCells;
        }

        public void SetValueToHeaderFilter(String ColNameOrIndex, String FilterValue, String FieldValue)
        {
            if (!int.TryParse(ColNameOrIndex, out int colNumber))
                colNumber = GetColumnNumberFromName(ColNameOrIndex);

            if (colNumber < 0)
                throw new Exception("Column header [" + ColNameOrIndex + "] not found. ");

            List<IWebElement> filterCells = GetFilterCells();

            if(colNumber > filterCells.Count)
                throw new Exception("Column headers does not match with filter fields.");

            IWebElement targetFilterCell = filterCells.ElementAt(colNumber - 1);
            switch (mTableClass)
            {
                case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only
                    string valueToFilter = !String.IsNullOrEmpty(FilterValue) ? FilterValue : FieldValue;

                    //Set value for textbox field
                    IWebElement inputField = targetFilterCell.FindElement(By.TagName("input"));
                    inputField.Clear();
                    DlkMaconomyiAccessFunctionHandler.WaitIframeLoading();

                    //Filter cells will be reset after clear
                    filterCells = GetFilterCells(); 
                    targetFilterCell = filterCells.ElementAt(colNumber - 1);
                    inputField = targetFilterCell.FindElement(By.TagName("input"));
                    inputField.SendKeys(valueToFilter);
                    break;

                default:
                    string dDownFilter_XPath = ".//div[contains(@dm-data-id,'dm-list-view-operator')]";
                    string mainField_XPath = ".//div[contains(@class,'input-group left') or contains(@class,'input-group right')]";
                    string mainField_dDown_XPath = ".//parent::div[contains(@container,'body')][contains(@class,'dropdown')]";

                    IWebElement dDownFilterField = targetFilterCell.FindElements(By.XPath(dDownFilter_XPath)).Any() ?
                        targetFilterCell.FindElement(By.XPath(dDownFilter_XPath)) : null;
                    IWebElement mainField = targetFilterCell.FindElements(By.XPath(mainField_XPath)).Any() ?
                        targetFilterCell.FindElement(By.XPath(mainField_XPath)) : throw new Exception("Main filter field not found.");

                    //Set value for dropdown filter field
                    if (!String.IsNullOrEmpty(FilterValue))
                    {
                        if (dDownFilterField != null)
                        {
                            DlkDropDownList dDownFilter = new DlkDropDownList("Target Dropdown", dDownFilterField);
                            dDownFilter.Select(FilterValue);
                            DlkLogger.LogInfo("Dropdown filter value set to : [" + FilterValue + "]");
                        }
                        else
                        {
                            DlkLogger.LogInfo("No dropdown filter found for this cell");
                        }
                    }
                    else
                    {
                        DlkLogger.LogInfo("No dropdown filter value set.");
                    }

                    //Set value for main filter field

                    string mainField_DDown_Class_XPath = ".//ancestor::*[contains(@dm-data-id,'dm-list-view-dropdown-input')]";
                    string mainField_Date_Class_XPath = ".//ancestor::*[contains(@dm-data-id,'dm-list-view-date-input')]";
                    string mainField_TxtBox_Class_XPath = ".//ancestor::*[contains(@dm-data-id,'dm-list-view-simple-input')]";

                    //Check if main filter field is a dropdown
                    if (mainField.FindElements(By.XPath(mainField_DDown_Class_XPath)).Any())
                    {
                        IWebElement mainDropdownField = mainField.FindElement(By.XPath(mainField_dDown_XPath));
                        DlkDropDownList maindDownField = new DlkDropDownList("Target Dropdown", mainDropdownField);
                        maindDownField.Select(FieldValue);
                    }
                    //Check if main filter field is a textbox or date
                    else if (mainField.FindElements(By.XPath(mainField_TxtBox_Class_XPath)).Any() ||
                        mainField.FindElements(By.XPath(mainField_Date_Class_XPath)).Any())
                    {
                        IWebElement mainTextBoxField = mainField.FindElement(By.TagName("input"));
                        DlkTextBox mainTBoxField = new DlkTextBox("Target TextBox", mainTextBoxField);
                        mainTBoxField.SetAndEnter(FieldValue);
                    }
                    DlkLogger.LogInfo("Main field value set to : [" + FieldValue + "]");
                    break;
            }
        }

        public string GetValueHealderFilter(String ColNameOrIndex)
        {
            if (!int.TryParse(ColNameOrIndex, out int colNumber))
                colNumber = GetColumnNumberFromName(ColNameOrIndex);

            if (colNumber < 0)
                throw new Exception("Column header [" + ColNameOrIndex + "] not found. ");

            List<IWebElement> filterCells = GetFilterCells();
            string actualValue = "";

            if (colNumber > filterCells.Count)
                throw new Exception("Column headers does not match with filter fields.");

            IWebElement targetFilterCell = filterCells.ElementAt(colNumber - 1); ;
            switch (mTableClass)
            {
                case DXGV_CLASS:
                    // Get value for textbox field
                    IWebElement inputField = targetFilterCell.FindElement(By.TagName("input"));
                    actualValue = inputField.Text.Trim();
                    break;
                default:
                    string chipListContainer_XPath = ".//mat-chip-list";
                    string chipItem_XPath = ".//mat-basic-chip";

                    IWebElement targetChipList = targetFilterCell.FindElements(By.XPath(chipListContainer_XPath)).Any() ?
                        targetFilterCell.FindElement(By.XPath(chipListContainer_XPath)) : throw new Exception("Chip list container not found.");
                    List<IWebElement> chipItems = targetChipList.FindElements(By.XPath(chipItem_XPath)).ToList();

                    if (chipItems.Count > 0)
                    {
                        foreach (IWebElement currentItem in chipItems)
                        {
                            string currentValue = currentItem.Text.Trim();
                            actualValue = String.IsNullOrEmpty(actualValue) ? currentValue :
                                actualValue + "~" + currentValue;
                        }
                    }
                    break;
            }
            return actualValue;
        }

        #endregion

        #region PRIVATE METHODS

        private string GetTableClass()
        {
            string tableClass = mElement.GetAttribute("class") != null ?
                mElement.GetAttribute("class").ToString().ToLower() : string.Empty;

            //Identify table class
            if (tableClass.Contains(DXGV_CLASS))
                tableClass = DXGV_CLASS;

            return tableClass;
        }

        private List<IWebElement> GetColumnHeaders()
        {
            List<IWebElement> columnHeaders;
            string DEFAULT_columnHeaderContainerXPath = ".//div[contains(@class,'k-grid-header')]/table";
            string DGXV_columnHeaderContainerXPath = ".//tr[contains(@id,'HeadersRow')]";

            IWebElement tableHeader = mElement.FindWebElementCoalesce(By.XPath(DEFAULT_columnHeaderContainerXPath), By.XPath(DGXV_columnHeaderContainerXPath));

            if (tableHeader == null)
                throw new Exception("Table header container not found");

            string DEFAULT_columnHeadersXPath1 = "./descendant::th//dm-table-header";
            string DEFAULT_columnHeadersXPath2 = "./descendant::th//dm-adv-search-heading//div[contains(@class,'title')]";
            string DEFAULT_columnHeadersXPath3 = "./descendant::th//div[contains(@class,'heading')]";
            string DGXV_columnHeadersXPath = ".//td[contains(@class,'dxgvHeader')][contains(@id,'col')]";

            switch (mTableClass)
            {
                case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only
                    columnHeaders = tableHeader.FindElements(By.XPath(DGXV_columnHeadersXPath)).Where(x => x.Displayed).ToList();
                    break;
                
                default:
                    columnHeaders = tableHeader.FindElements(By.XPath(DEFAULT_columnHeadersXPath1)).Where(x => x.Displayed).ToList();
                    columnHeaders = columnHeaders.Any() ? columnHeaders : tableHeader.FindElements(By.XPath(DEFAULT_columnHeadersXPath2)).Where(x => x.Displayed).ToList();
                    columnHeaders = columnHeaders.Any() ? columnHeaders : tableHeader.FindElements(By.XPath(DEFAULT_columnHeadersXPath3)).ToList(); //Include hidden columns for this column header type
                    break;
            }

            if (columnHeaders.Count == 0)
                throw new Exception("Table headers not found");

            return columnHeaders;
        }

        private List<IWebElement> GetRows()
        {
            List<IWebElement> tableRows;
            string DEFAULT_RowsXPath = "./descendant::tbody/tr[not(contains(@class,'k-grid-norecords'))][not(contains(@class,'calendar'))]";
            string DXGV_RowsXPath = ".//tr[contains(@class,'dxgvDataRow')]";

            switch (mTableClass)
            {
                case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only
                    tableRows = mElement.FindElements(By.XPath(DXGV_RowsXPath)).Where(x => x.Displayed).ToList();
                    break;
                default:
                    tableRows = mElement.FindElements(By.XPath(DEFAULT_RowsXPath)).Where(x => x.Displayed).ToList();
                    break;
            }
            return tableRows;
        }

        private string GetDefaultCellValue(IWebElement targetCell)
        {
            string actualValue;
            actualValue = DlkString.RemoveCarriageReturn(targetCell.Text.Trim()) != null ?
                            DlkString.RemoveCarriageReturn(targetCell.Text.Trim()) :
                           !String.IsNullOrEmpty(new DlkBaseControl("Targete Cell", targetCell).GetValue().Trim()) ?
                           new DlkBaseControl("Targete Cell", targetCell).GetValue().Trim() :
                           string.Empty;
            return actualValue;
        }

        private int GetColumnNumberFromName(string ColumnName)
        {
            int ret = -1;
            for (int i = 0; i < mColumnHeaders.Count; i++)
            {
                DlkBaseControl targetColumn = new DlkBaseControl("Column", mColumnHeaders[i]);
                targetColumn.ScrollIntoViewUsingJavaScript();

                string columnText = mColumnHeaders[i].GetAttribute("title") != null ? !String.IsNullOrEmpty(mColumnHeaders[i].GetAttribute("title")) ?
                    mColumnHeaders[i].GetAttribute("title").Trim() : targetColumn.mElement.Text.Trim() : targetColumn.mElement.Text.Trim();
                if (columnText.ToLower() == ColumnName.ToLower())
                {
                    ret = i + 1;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// This function is for retrieving column numbers without header names but images
        /// </summary>
        private int GetColNumberFromImageName(string ColumnName)
        {
            string colName = ColumnName.ToLower();
            int ret = -1;
            for (int i = 0; i < mColumnHeaders.Count; i++)
            {
                if (mColumnHeaders[i].GetAttribute("source").ToLower().Contains(colName))
                {
                    ret = i + 1;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Opens the Dropdown list in LineOptions
        /// </summary>
        /// <param name="LineIndex"></param>
        private void ClickLineOption(String LineIndex)
        {
            string targetRow_XPath = "./descendant::tbody/tr[" + LineIndex + "]/td[contains(@class,'line-action')]";
            string targetLineButton_XPath = ".//*[contains(@class,'command dropdown')] | .//*[contains(@class,'icon-rowtools')]";

            IWebElement targetRow = mElement.FindElements(By.XPath(targetRow_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(targetRow_XPath)) : throw new Exception("Target row not found.");
            IWebElement lineBtn = targetRow.FindElements(By.XPath(targetLineButton_XPath)).Count > 0 ?
                targetRow.FindElement(By.XPath(targetLineButton_XPath)) : throw new Exception("Target line button not found.");
            DlkBaseControl targetRowCtl = new DlkBaseControl("ActionBtn", targetRow);
            DlkBaseControl lineBtnCtl = new DlkBaseControl("ActionBtn", lineBtn);
            targetRowCtl.ScrollIntoViewUsingJavaScript();
            lineBtn.Click();
            Thread.Sleep(1000);
        }

        private IWebElement GetLineOption(String LineOption)
        {
            IWebElement mLineOption = null;
            if (DlkEnvironment.AutoDriver.FindElements(By.TagName("bs-dropdown-container")).Count > 0)
            {
                IWebElement mListOptions = DlkEnvironment.AutoDriver.FindElement(By.TagName("bs-dropdown-container"));

                if (mListOptions.FindElements(By.XPath(".//button[contains(text(),'" + LineOption + "')]/parent::div")).Count > 0)
                {
                    mLineOption = mListOptions.FindElement(By.XPath(".//button[contains(text(),'" + LineOption + "')]/parent::div"));
                }
            }
            if (mLineOption == null)
                throw new Exception("Line option [" + LineOption + "] not found.");

            return mLineOption;
        }

        /// <summary>
        /// Opens the Attachment list in Attachments
        /// </summary>
        /// <param name="LineIndex"></param>
        private void ClickAttachmentIcon(String LineIndex)
        {
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            int attempts = 0;
            new DlkBaseControl("FirstCol", firstCol).ScrollIntoViewUsingJavaScript();
            firstCol.Click();

            //reassign targetrow; DOM changes upon click
            targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement btn = targetRow.FindElement(By.XPath(".//div[@class='dropdown']//a[1]"));
            while (attempts < 5)
            {
                try
                {
                    new DlkBaseControl("targetButton", btn).ScrollIntoViewUsingJavaScript();
                    btn.Click();
                    break;
                }
                catch (Exception)
                {
                    //do nothing
                }
                attempts++;
            }
        }

        /// <summary>
        /// Closes dropdown list in LineOptions
        /// </summary>
        private void CloseLineOptions(String LineIndex)
        {
            IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            new DlkBaseControl("FirstCol", firstCol).ScrollIntoViewUsingJavaScript();
            firstCol.Click();
        }

        private void ClickExpandCollapse(String LineIndex, String ExpandCollapse)
        {
            int rowIndex = Convert.ToInt32(LineIndex) - 1;

            if(mTableRows.Count > 0)
            {
                IWebElement targetRow = mTableRows.ElementAt(rowIndex);
                bool isTargetRowExpandCollapse = false;
                switch (mTableClass)
                {
                    case DXGV_CLASS: //Modify if element is under DXGV TABLE CLASS only

                        string dx_ExpandCollapseRow_XPath = ".//img[contains(@class,'Collapse') or contains(@class,'Expand')]";
                        isTargetRowExpandCollapse = targetRow.FindElements(By.XPath(dx_ExpandCollapseRow_XPath)).Where(x => x.Displayed).Any();
                        if (isTargetRowExpandCollapse)
                        {
                            IWebElement targetButton = targetRow.FindElement(By.XPath(dx_ExpandCollapseRow_XPath));

                            //Verify and click expand or collapse button by class
                            string targetClass = ExpandCollapse.ToLower().Equals("expand") || ExpandCollapse.ToLower().Equals("collapse") ?
                                ExpandCollapse.ToLower() : throw new Exception("Input [" + ExpandCollapse + "] is not valid ExpandCollapse parameter. Select expand or collapse only");

                            //Get target button class or alt state
                            string buttonClassAlt = !String.IsNullOrEmpty(targetButton.GetAttribute("class")) ?
                                targetButton.GetAttribute("class").ToLower().Trim() :
                                !String.IsNullOrEmpty(targetButton.GetAttribute("alt")) ?
                                targetButton.GetAttribute("alt").ToLower().Trim() :
                                 throw new Exception("Button state on target row not found");

                            //Identifty button state
                            bool isButtonStateIdentified = (buttonClassAlt.Contains("expand") || buttonClassAlt.Contains("collapse"));

                            if (!isButtonStateIdentified)
                                throw new Exception("Expand/Collapse button not identified on target row.");

                            //Expand or collapse target row
                            if (!buttonClassAlt.Contains(targetClass)) //Expanded - collapse / Collapsed - expand.
                                targetButton.Click();
                            else
                                DlkLogger.LogInfo("Target row is already in [" + ExpandCollapse + "] state.");
                        }

                        break;

                    default:

                        //Get target row and target button
                        isTargetRowExpandCollapse = targetRow.GetAttribute("class").ToLower().Contains("master");
                        if (isTargetRowExpandCollapse)
                        {
                            IWebElement targetButton = targetRow.FindElements(By.TagName("a")).Count > 0 ?
                                targetRow.FindElement(By.TagName("a")) : throw new Exception("Expand/Collapse button not on target row found.");

                            //Verify and click expand or collapse button by class
                            string expandClass = "k-plus";
                            string collapseClass = "k-minus";
                            string targetClass = ExpandCollapse.ToLower().Equals("expand") ? expandClass :
                                ExpandCollapse.ToLower().Equals("collapse") ? collapseClass :
                                throw new Exception("Input [" + ExpandCollapse + "] is not valid ExpandCollapse parameter. Select expand or collapse only");

                            //Get target button class or title state
                            string buttonClassTitle = !String.IsNullOrEmpty(targetButton.GetAttribute("class")) ?
                                targetButton.GetAttribute("class").ToLower().Trim() :
                                !String.IsNullOrEmpty(targetButton.GetAttribute("title")) ?
                                targetButton.GetAttribute("title").ToLower().Trim() :
                                 throw new Exception("Button state on target row not found");

                            //Identifty button state
                            bool isButtonStateIdentified = (buttonClassTitle.Contains(expandClass) || buttonClassTitle.Contains(collapseClass)
                                || buttonClassTitle.Contains("expand") || buttonClassTitle.Contains("collapse"));

                            if (!isButtonStateIdentified)
                                throw new Exception("Expand/Collapse button not identified on target row.");

                            //Expand or collapse target row
                            if (buttonClassTitle.Contains(targetClass) || buttonClassTitle.Contains(ExpandCollapse.ToLower()))
                                targetButton.Click();
                            else
                                DlkLogger.LogInfo("Target row is already in [" + ExpandCollapse + "] state.");
                        }

                        break;
                }
                if (!isTargetRowExpandCollapse)
                    throw new Exception("Target row does not contain expand/collapse button.");
            }
            else
                throw new Exception("No table rows found.");
        }

        private void SetDetailValue(String LineIndex, String Field)
        {
            //Get target row and target field
            string targetRow_XPath = "./descendant::tbody/tr[" + LineIndex + "][contains(@class,'k-detail-row')]";
            IWebElement targetRow = mElement.FindElements(By.XPath(targetRow_XPath)).Count > 0 ?
                mElement.FindElement(By.XPath(targetRow_XPath)) : throw new Exception("Target row does not contain details field.");

            IWebElement targetField = targetRow.FindElements(By.TagName("a")).Count > 0 ?
                targetRow.FindElement(By.TagName("a")) : throw new Exception("Expand/Collapse button not found.");
        }

        private DlkBaseControl GetResultsList()
        {
            string resultsXPath = "//dm-result-wrapper | //*[contains(@class,'result-wrapper')]";
            DlkBaseControl results = new DlkBaseControl("Results", "XPATH_DISPLAY", resultsXPath);
            return results;
        }

        private IList <IWebElement> GetDropdownListItems(DlkBaseControl results)
        {
            IList<IWebElement> resultItems = results.mElement.FindElements(By.XPath(".//dm-filter-result | .//p"))
                           .Where(x => x.Displayed).ToList();

            return resultItems != null ? resultItems : throw new Exception("Result items not found.");
        } 

        private string GetDropdownListItemsString(IList<IWebElement> resultItems, bool lowerCase = true)
        {
            string dropdownListItems = "";

            foreach (IWebElement elm in resultItems)
            {
                string actualItem = DlkString.ReplaceCarriageReturn(new DlkBaseControl("Element", elm).GetValue(), "").Trim();
                dropdownListItems += actualItem + "~";
            }

            dropdownListItems = lowerCase ? dropdownListItems.ToLower() : dropdownListItems;
            return dropdownListItems.Trim('~');
        }

        private void SelectItemDropdownList(IList<IWebElement> resultItems, string Item, bool Partial = false)
        {
            bool iFound = false;
            foreach (IWebElement elm in resultItems)
            {
                string ActualItem = DlkString.ReplaceCarriageReturn(new DlkBaseControl("Element", elm).GetValue(), "").Trim().ToLower();
                string ExpectedItem = DlkString.ReplaceCarriageReturn(Item, "").Trim().ToLower();

                DlkLogger.LogInfo("Actual Item: [" + ActualItem + "]");

                if (!Partial)
                {
                    if (ActualItem == ExpectedItem)
                    {
                        elm.Click();
                        iFound = true;
                        DlkLogger.LogInfo("Selecting Item: [" + ActualItem + "] ...");
                        break;
                    }
                }
                else //Select item in dropdown list with partial value
                {
                    if (ActualItem.Contains(ExpectedItem))
                    {
                        elm.Click();
                        iFound = true;
                        DlkLogger.LogInfo("Selecting Item: [" + ActualItem + "] ...");
                        break;
                    }
                }
            }

            if (!iFound)
                throw new Exception("Item [" + Item + "] not found in list.");
        }

        private bool VerifyItemDropdownList(IList<IWebElement> resultItems, string Item, bool lowerCase = true, bool Partial = false)
        {
            bool actualResult = false;
            foreach (IWebElement elm in resultItems)
            {
                string ActualItem = DlkString.ReplaceCarriageReturn(new DlkBaseControl("Element", elm).GetValue(), "").Trim();
                string ExpectedItem = DlkString.ReplaceCarriageReturn(Item, "").Trim();

                //Convert values to lowercase if true
                ActualItem = lowerCase ? ActualItem.ToLower() : ActualItem;
                ExpectedItem = lowerCase ? ExpectedItem.ToLower() : ExpectedItem;

                DlkLogger.LogInfo("Actual Item: [" + ActualItem + "]");

                if (!Partial)
                {
                    if (ActualItem == ExpectedItem)
                    {
                        actualResult = true;
                        break;
                    }
                }
                else //Verify item in dropdown list with partial value
                {
                    if (ActualItem.Contains(ExpectedItem))
                    {
                        actualResult = true;
                        break;
                    }
                }
            }
            return actualResult;
        }

        private void InputConcurrentKey(int rowNum, int columnNum, string specialKey, string concurrentKey, IWebElement cell)
        {
            try
            {
                Initialize();
                //this works and with less code than using Actions.Sendkeys(keys.ctrl).sendkeys(concurrentkey).Perform();
                switch (specialKey.ToLower())
                {
                    case "ctrl":
                        if (concurrentKey.ToLower().Equals("tab"))
                            cell.SendKeys(Keys.Control + Keys.Tab);
                        else
                            cell.SendKeys(Keys.Control + concurrentKey.ToLower());
                        break;
                    case "shift":
                        if (concurrentKey.ToLower().Equals("tab"))
                            cell.SendKeys(Keys.Shift + Keys.Tab);
                        else
                            cell.SendKeys(Keys.Shift + concurrentKey.ToLower());
                        break;
                    case "alt":
                        if (concurrentKey.ToLower().Equals("tab"))
                            cell.SendKeys(Keys.Alt + Keys.Tab);
                        else
                            cell.SendKeys(Keys.Alt + concurrentKey.ToLower());
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region KEYWORDS

        [Keyword("AssignLineIndexToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public void AssignLineIndexToVariable(String ColumnName, String CellValue, String VariableName)
        {
            try
            {
                Initialize();
                bool comparison = false;
                int i = 1;
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");
                }

                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::tbody/tr")))
                {
                    IWebElement targetCell = elm.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]"));
                    
                    if (targetCell.GetAttribute("class").Contains("string") & !(targetCell.FindElements(By.XPath("./descendant::div[@class='open']")).Count > 0))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", targetCell.FindElement(By.XPath("./descendant::input[2]")));
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue().ToLower(), "") == DlkString.ReplaceCarriageReturn(CellValue.ToLower(), ""))
                        { comparison = true; }
                    }
                    else
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", targetCell.FindElement(By.XPath("./descendant::input[1]")));
                        if (DlkString.ReplaceCarriageReturn(ctl.GetAttributeValue("value").ToLower(), "") == DlkString.ReplaceCarriageReturn(CellValue.ToLower(), ""))
                        { comparison = true; }
                    }
                    
                    if (comparison)
                    {
                        DlkFunctionHandler.AssignToVariable(VariableName, i.ToString());
                        DlkLogger.LogInfo("AssignLineIndexToVariable() successfully executed.");
                        return;
                    }
                    i++;
                }
                throw new Exception("Cell with '" + CellValue + "' was not found.");

            }
            catch (Exception e)
            {
                throw new Exception("AssignLineIndexToVariable() failed : " + e.Message, e);
            }
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

        [Keyword("SetCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValue(String ColNameOrIndex, String LineIndex, String Value)
        {
            try
            {
                Initialize();
               
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                SetValueToCell(ColNameOrIndex, LineIndex, Value);
                DlkLogger.LogInfo("SetCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetFieldValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetFieldValue(String LineIndex, String Field, String Value)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                FindElement();

                string targetFieldName = Field.ToLower().Trim();
                string detailRows_XPath = ".//tr[contains(@class,'k-master-row')][" + LineIndex + "]/following-sibling::tr[contains(@class,'k-detail-row')][1]";
                string fieldHasInputValue_XPath = ".//span[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'" 
                    + targetFieldName + "')]//ancestor::dm-label-block//following-sibling::div";
                string fieldNoneValue_XPath = ".//span[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'" 
                    + targetFieldName + "')]//ancestor::dm-label-block//following-sibling::dm-none-value";

                //Get target detail row
                IWebElement targetRow = mElement.FindElements(By.XPath(detailRows_XPath)).Count > 0 ? mElement.FindElement(By.XPath(detailRows_XPath))
                    : throw new Exception("Target detail row not found.");

                //Get target field
                IWebElement targetField = targetRow.FindElements(By.XPath(fieldHasInputValue_XPath)).Count > 0 ?
                    targetRow.FindElement(By.XPath(fieldHasInputValue_XPath)) :
                    targetRow.FindElements(By.XPath(fieldNoneValue_XPath)).Count > 0 ?
                    targetRow.FindElement(By.XPath(fieldNoneValue_XPath)) :
                    throw new Exception("Target field from detail row not found.");

                //Check if field is textbox or label
                IWebElement targetInput = targetField.FindElements(By.TagName("input")).Count > 0 ?
                    targetField.FindElement(By.TagName("input")) : throw new Exception("Target field is a label and does not contain an input field.");

                //Set value to input field
                targetInput.Clear();
                Thread.Sleep(1000);
                targetInput.SendKeys(Value);
                Thread.Sleep(1000);
                DlkLogger.LogInfo("SetFieldValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetFieldValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetFieldValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetFieldValue(String LineIndex, String Field, String VariableName)
        {
            try
            {
                int row = 0;
                if (!int.TryParse(LineIndex, out row) || row == 0)
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                FindElement();

                string targetFieldName = Field.ToLower().Trim();
                string detailRows_XPath = ".//tr[contains(@class,'k-master-row')][" + LineIndex + "]/following-sibling::tr[contains(@class,'k-detail-row')][1]";
                string fieldHasInputValue_XPath = ".//span[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'"
                    + targetFieldName + "')]//ancestor::dm-label-block//following-sibling::div";
                string fieldNoneValue_XPath = ".//span[contains(translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'),'"
                    + targetFieldName + "')]//ancestor::dm-label-block//following-sibling::dm-none-value";

                //Get target detail row
                IWebElement targetRow = mElement.FindElements(By.XPath(detailRows_XPath)).Count > 0 ? mElement.FindElement(By.XPath(detailRows_XPath))
                    : throw new Exception("Target detail row not found.");

                //Get target field
                IWebElement targetField = targetRow.FindElements(By.XPath(fieldHasInputValue_XPath)).Count > 0 ?
                    targetRow.FindElement(By.XPath(fieldHasInputValue_XPath)) :
                    targetRow.FindElements(By.XPath(fieldNoneValue_XPath)).Count > 0 ?
                    targetRow.FindElement(By.XPath(fieldNoneValue_XPath)) :
                    throw new Exception("Target field from detail row not found.");

                //Check if field is textbox or label
                IWebElement targetInput = targetField.FindElements(By.TagName("input")).Count > 0 ?
                    targetField.FindElement(By.TagName("input")) :
                    targetField.FindElements(By.TagName("span")).Count > 0 ?
                    targetField.FindElement(By.TagName("span")) :
                    throw new Exception("Target field input or label not found.");

                //Get value to input field
                string actualValue = targetInput.Text.Trim();
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] stored to variable name: [" + VariableName + "]");
                DlkLogger.LogInfo("GetFieldValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetFieldValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCellValueAndEnter", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCellValueAndEnter(String ColNameOrIndex, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                SetValueToCell(ColNameOrIndex, LineIndex, Value);
                mElement.SendKeys(Keys.Enter);
                Thread.Sleep(3000);
                DlkLogger.LogInfo("SetCellValueAndEnter() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetCellValueAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("SendBackspaceToCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void SendBackspaceToCell(String ColNameOrIndex, String LineIndex, String NumberOfTimes)
        {
            try
            {
                Initialize();

                int counter = 0;
                int maxLoop = Convert.ToInt32(NumberOfTimes);

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                targetCell.Click();

                /* if click fails on first try, the element is no longer attached to DOM. Try again */
                IWebElement targetInputControl = null;
                targetInputControl = targetCell.FindElement(By.TagName("input"));

                while (counter < maxLoop)
                {
                    targetInputControl.SendKeys(Keys.Backspace);
                    counter++;
                }
                DlkLogger.LogInfo("SendBackspaceToCell() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SendBackspaceToCell() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new String[] { "ColNameOrIndex|1|Expected Value" })]
        public void VerifyCellValue(String ColNameOrIndex, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(row, ColNameOrIndex);

                bool BooleanValue;
                if (Boolean.TryParse(ActualValue, out BooleanValue))
                    DlkAssert.AssertEqual("VerifyCellValue(): ", Convert.ToBoolean(Value), BooleanValue);
                else
                    DlkAssert.AssertEqual("VerifyCellValue(): ", Value.ToLower(), ActualValue.ToLower());

                DlkLogger.LogInfo("VerifyCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValueContains", new String[] { "ColNameOrIndex|1|Expected Value" })]
        public void VerifyCellValueContains(String ColNameOrIndex, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(row, ColNameOrIndex);

                bool BooleanValue;
                if (Boolean.TryParse(ActualValue, out BooleanValue))
                    DlkAssert.AssertEqual("VerifyCellValueContains(): ", Convert.ToBoolean(Value), BooleanValue);
                else
                    DlkAssert.AssertEqual("VerifyCellValueContains(): ", Value.ToLower(), ActualValue.ToLower(), true);

                DlkLogger.LogInfo("VerifyCellValueContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValueContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellErrorMessage", new String[] { "ColNameOrIndex|1|Expected Value" })]
        public void VerifyCellErrorMessage(String ColNameOrIndex, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                IWebElement errorMsg = targetCell.FindElement(By.XPath(".//dm-error"));

                DlkAssert.AssertEqual("VerifyCellErrorMessage()", Value.ToLower(), DlkString.RemoveCarriageReturn(new DlkBaseControl("ErrorMessage", errorMsg).GetValue()).ToLower());
                DlkLogger.LogInfo("VerifyCellErrorMessage() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellErrorMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellCheck", new String[] { "ColNameOrIndex|1||TRUE" })]
        public void VerifyCellCheck(String ColNameOrIndex, String LineIndex, String TrueOrFalse = "")
        {
            try
            {
                Initialize();
                bool isChecked = false;

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

                string cellXPath1 = ".//*[contains(@class,'boolean')]";
                string checkedXPath1 = ".//*[contains(@class,'fa-check')]";
                string cellXPath2 = ".//div[contains(@class,'checkbox')]";
                string checkedXPath2 = ".//*[contains(@class, 'checked')]";

                if (targetCell.FindElements(By.XPath(cellXPath1)).Count > 0 && targetCell.FindElements(By.XPath(checkedXPath1)).Count > 0)
                    isChecked = true;
                else if(targetCell.FindElements(By.XPath(cellXPath2)).Count > 0 && targetCell.FindElements(By.XPath(checkedXPath2)).Count > 0)
                    isChecked = true;
                
                DlkAssert.AssertEqual("VerifyCellCheck()", Convert.ToBoolean(TrueOrFalse), isChecked);
                DlkLogger.LogInfo("VerifyCellCheck() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellCheck() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextboxIsVisible", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTextboxIsVisible(String ColNameOrIndex, String LineIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bfound = false;

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

                if (targetCell.GetAttribute("class").Contains("boolean"))
                {
                    // do nothing
                }
                else if (targetCell.FindElements(By.XPath(".//*[1]")).Count > 0)
                {
                    IWebElement targetInputControl = targetCell.FindElement(By.XPath("./descendant::input[1]"));

                    if (targetInputControl.GetAttribute("class").Contains("ng-valid"))
                    {
                        bfound = true;
                    }

                }
                DlkAssert.AssertEqual("VerifyTextboxIsVisible()", bool.Parse(TrueOrFalse), bfound);
                DlkLogger.LogInfo("VerifyTextboxIsVisible() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextboxIsVisible() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellButton(String ColNameOrIndex, String LineIndex, String ButtonNameOrBlank, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bfound = false;

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

                string buttonXPath = "";

                switch (ButtonNameOrBlank.ToLower())
                {
                    // add new case for new buttons 
                    case "calendar":
                        buttonXPath = ".//a[contains(@class,'calendar')] | ./descendant::img[contains(@src,'calendar')]";
                        break;
                    case "check":
                        buttonXPath = ".//button[contains(@class,'check')]";
                        break;
                    case "openoutside":
                        buttonXPath = ".//*[contains(@class,'openoutside')]";
                        break;
                    case "approve":
                        buttonXPath = ".//*[contains(@class,'thumbs-up')]";
                        break;
                    case "reject":
                        buttonXPath = ".//*[contains(@class,'thumbs-down')]";
                        break;
                    case "delete":
                        buttonXPath = "./descendant::a[contains(@title,'Delete')]/..";
                        break;
                    case "reopen":
                        buttonXPath = "./descendant::a[contains(@title,'Reopen')]/..";
                        break;
                    case "find":
                        buttonXPath = "./descendant::i[contains(@class,'lookup')]/..";
                        break;
                    case "favorite":
                        buttonXPath = "./descendant::i[contains(@class,'fa-star')]/..";
                        break;
                    case "edit":
                        buttonXPath = "./descendant::a[contains(@title,'Edit')]";
                        break;
                    case "submit":
                        buttonXPath = "./descendant::a[contains(@title,'Submit')]/..";
                        break;
                    default:
                        throw new Exception("[" + ButtonNameOrBlank + "] button does not exist.");
                }

                if (targetCell.FindElements(By.XPath(buttonXPath)).Count > 0)
                    bfound = true;

                DlkAssert.AssertEqual("VerifyCellButton()", bool.Parse(TrueOrFalse), bfound);
                DlkLogger.LogInfo("VerifyCellButton() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineCount(String LineCount)
        {
            try
            {
                Initialize();
                int actRowCount = mTableRows.Count;
                DlkAssert.AssertEqual("VerifyLineCount()", int.Parse(LineCount), actRowCount);
                DlkLogger.LogInfo("VerifyLineCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineCount() failed : " + e.Message, e);
            }
        }

        [Keyword("ShowFindDialog", new String[] { "1|text|Expected Value|TRUE" })]
        public void ShowFindDialog(String ColNameOrIndex, String LineIndex)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                IWebElement button = targetCell.FindElement(By.XPath("./descendant::a[@class='ng-scope']"));
                button.Click();
                DlkLogger.LogInfo("ShowFindDialog() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ShowFindDialog() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLine", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectLine(String LineIndex)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string xpath_TargetRow = "./descendant::tbody/tr[" + LineIndex + "][not(contains(@class,'k-grid-norecords'))][not(contains(@class,'calendar'))]";
                IWebElement targetRow = mElement.FindElements(By.XPath(xpath_TargetRow)).Count > 0 ?
                    mElement.FindElement(By.XPath(xpath_TargetRow)) : null;

                if (targetRow == null)
                    throw new Exception("Row not found.");
                else
                {
                    try
                    {
                        ClickUsingJavaScriptThenSelenium(targetRow);
                    }
                    catch
                    {
                        string xpath_TargetRowCell = "./descendant::tbody/tr[" + LineIndex + "][not(contains(@class,'k-grid-norecords'))][not(contains(@class,'calendar'))]/td";
                        IWebElement targetRowCell = mElement.FindElements(By.XPath(xpath_TargetRowCell)).Count > 0 ?
                            mElement.FindElement(By.XPath(xpath_TargetRowCell)) : null;

                        if (targetRowCell == null)
                            throw new Exception("Row with first cell not found.");

                        targetRowCell.Click();
                    }
                }

                DlkLogger.LogInfo("SelectLine() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectLine() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineButtons", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineButtons(String LineIndex, String ExpectedValues)
        {
            try
            {
                Initialize();
                //find the row that the QE needs to check the line options
                IWebElement targetRow = mElement.FindElement(By.XPath(string.Format("./descendant::tbody/tr[{0}]/td[1]", LineIndex)));
                //highlight the row by clicking on it
                DlkLogger.LogInfo(string.Format("Attempting to click row number {0}", LineIndex));
                targetRow.Click();
                DlkLogger.LogInfo(string.Format("Row number {0} was clicked", LineIndex));
                Thread.Sleep(2000);//to give time for the buttons to load.

                //do: check if the line options exist
                ReadOnlyCollection<IWebElement> lineOptions = targetRow.FindElements(By.XPath("../descendant::li"));
                if (lineOptions.Count > 0)
                {
                    DlkLogger.LogInfo(string.Format("VerifyIfLineOptionsExist() successfully executed. {0} line option(s) found."
                        , lineOptions.Count.ToString()));
                }
                else
                {
                    throw new Exception(string.Format("Line option(s) on line number {0} were not found", LineIndex));
                }
                //if the line options exist, do: comparison of actual and expected
                string[] expected = ExpectedValues.Split('~');
                int loopCounter = 0;
                //for each <li> element, go to its corresponding <a> tags to compare the actual values with the expected values
                foreach (var item in lineOptions)
                {
                    string lineOptionTitleAttributeValue = item.FindElement(By.XPath("./a")).GetAttribute("Title");
                    DlkLogger.LogInfo(string.Format("Comparing [{0}] and [{1}]", expected[loopCounter].ToLower(), lineOptionTitleAttributeValue.ToLower()));
                    DlkAssert.AssertEqual("VerifyIfLineOptionsExist", expected[loopCounter].ToLower(), lineOptionTitleAttributeValue.ToLower());
                    DlkLogger.LogInfo(string.Format("[{0}] and [{1}] are equal", expected[loopCounter].ToLower(), lineOptionTitleAttributeValue.ToLower()));
                    loopCounter++;
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfLineOptionsExist() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickLineActions", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickLineActions(String LineIndex, String Option)
        {
            try
            {
                Initialize();
                ClickLineOption(LineIndex);
                Thread.Sleep(3000);

                IWebElement list = null;
                DlkBaseControl listCtl = new DlkBaseControl("List", list);

                IWebElement option;
                string lineOptionMenu_TagName = "bs-dropdown-container";
                string lineOptionMenu_XPath = ".//span[contains(@class,'open active')] | //div[contains(@class,'dropdown-menu show')]";

                if (DlkEnvironment.AutoDriver.FindElements(By.TagName(lineOptionMenu_TagName)).Count > 0)
                {
                    list = DlkEnvironment.AutoDriver.FindElement(By.TagName(lineOptionMenu_TagName));

                    if (list.FindElements(By.XPath(".//button[contains(text(),'" + Option + "')]/parent::div")).Count > 0)
                    {
                        option = list.FindElement(By.XPath(".//button[contains(text(),'" + Option + "')]/parent::div"));
                        DlkBaseControl optBtn = new DlkBaseControl("List", option);
                        optBtn.Click();
                    }
                    else
                    {
                        throw new Exception("ClickLineActions() failed : " + Option + "not found.");
                    }
                }
                else if (mElement.FindElements(By.XPath(lineOptionMenu_XPath)).Count > 0)
                {
                    list = mElement.FindElement(By.XPath(lineOptionMenu_XPath));

                    if (list.FindElements(By.XPath(".//button[contains(text(),'" + Option + "')]/parent::div")).Count > 0)
                    {
                        option = list.FindElement(By.XPath(".//button[contains(text(),'" + Option + "')]/parent::div"));
                        DlkBaseControl optBtn = new DlkBaseControl("List", option);
                        optBtn.Click();
                    }
                    else
                    {
                        throw new Exception("ClickLineActions() failed : " + Option + "not found.");
                    }
                }
                else
                {
                    throw new Exception("ClickLineActions() failed : Line option container not yet supported");
                }

                DlkLogger.LogInfo("ClickLineActions() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickLineActions() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickLineExpandCollapse", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickLineExpandCollapse(String LineIndex, String ExpandCollapse)
        {
            try
            {
                if (!int.TryParse(LineIndex, out int row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                Initialize();
                ClickExpandCollapse(LineIndex, ExpandCollapse);
                DlkLogger.LogInfo("ClickLineExpandCollapse() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickLineExpandCollapse() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickAttachmentOption", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickAttachmentOption(String LineIndex, String Option)
        {
            try
            {
                Initialize();
                ClickAttachmentIcon(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));

                IWebElement opt = null;
                // two tries, Delete seems to be 2 items
                try
                {
                    DlkLogger.LogInfo("Attempting to click first instance of option");
                    opt = targetRow.FindElements(By.XPath("./descendant::a[text()='" + Option + "']")).First();
                    opt.Click();
                }
                catch
                {
                    DlkLogger.LogInfo("Attempting to click last instance of option");
                    opt = targetRow.FindElements(By.XPath("./descendant::a[text()='" + Option + "']")).Last();
                    opt.Click();
                }

                DlkLogger.LogInfo("ClickAttachmentOption() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickAttachmentOption() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellLink(String LineIndex, String ColNameOrIndex)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                string xpath_CellLink = ".//span[contains(@class,'dropdown-toggle')]";

                if (targetCell.FindElements(By.XPath(xpath_CellLink)).Count == 0)
                    throw new Exception("Link not found.");

                IWebElement targetLink = targetCell.FindElement(By.XPath(xpath_CellLink));
                targetLink.Click();
                DlkLogger.LogInfo("ClickCellLink() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellLink() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickCellButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCellButton(String LineIndex, String ColNameOrIndex, String ButtonNameOrBlank)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

                string buttonXPath = "";
                string buttonXPath2 = ".//button";

                IWebElement targetButton = null;
                switch (ButtonNameOrBlank.ToLower())
                {
                    // add new case for new buttons 
                    case "calendar":
                        buttonXPath = ".//a[contains(@class,'calendar')]";
                        break;
                    case "check":
                        buttonXPath = ".//button[contains(@class,'check')]";
                        break;
                    case "openoutside":
                        buttonXPath = ".//*[contains(@class,'openoutside')]";
                        break;
                    case "approve":
                        buttonXPath = ".//*[contains(@class,'thumbs-up')]";
                        break;
                    case "reject":
                        buttonXPath = ".//*[contains(@class,'thumbs-down')]";
                        break;
                    default:
                        buttonXPath = ".//i";
                        break;
                }

                targetButton = targetCell.FindElements(By.XPath(buttonXPath)).Count > 0 ?
                           targetCell.FindElement(By.XPath(buttonXPath)) : null;

                if (targetButton == null)
                {
                    targetButton = targetCell.FindElements(By.XPath(buttonXPath2)).Count > 0 ?
                           targetCell.FindElement(By.XPath(buttonXPath2)) : null;
                }

                if (targetButton == null)
                    throw new Exception("[" + ButtonNameOrBlank + "] button not found in the target cell.");
                else
                {
                    targetButton.Click();
                    DlkLogger.LogInfo("[" + ButtonNameOrBlank + "] button has been clicked");
                    DlkLogger.LogInfo("ClickCellButton() passed.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickCellButton() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectCell(String LineIndex, String ColNameOrIndex)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                targetCell.Click();

                /* if click fails on first try, the element is no longer attached to DOM. Try again */
                IWebElement targetInputControl = null;
                targetInputControl = targetCell.FindElement(By.TagName("input"));
                DlkBaseControl targetCtl = new DlkBaseControl("TargetControl", targetInputControl);
                try
                {
                    targetCtl.Click();
                }

                catch
                {
                    targetCtl.Click();
                }

                DlkLogger.LogInfo("SelectCell() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCell() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnHeaderSortType", new String[] { "1|text|Col1|Col1~Col2~Col3", "1|text|Asc|Asc~Desc~Asc" })]
        public void VerifyColumnHeaderSortType(String ColumnName, String AscOrDesc)
        {
            try
            {
                Initialize();

                List<string> colNames = ColumnName.Split('~').ToList();
                List<string> sorting = AscOrDesc.Split('~').ToList();

                List<string> actualColNamesAndSorting = new List<string>();
                List<string> expectedColNamesAndSorting = new List<string>();

                if (colNames.Count != sorting.Count)
                    throw new Exception("Column names count should be equal to AscOrDesc count.");

                for (int i = 0; i < colNames.Count; i++)
                {
                    expectedColNamesAndSorting.Add($"{colNames[i]}/{sorting[i].ToLower()}{(colNames.Count == 1 ? "" : "/" + (i + 1))}");

                    for (int j = 0; j < mColumnHeaders.Count; j++)
                    {
                        IWebElement targetColumn = mColumnHeaders[j];
                        string columnHeader = !String.IsNullOrEmpty(targetColumn.Text) ?
                                                targetColumn.Text.Trim() :
                                                new DlkBaseControl("Target Column", targetColumn).GetValue().Trim();

                        if (columnHeader == colNames[i])
                        {
                            var sortElements = targetColumn.FindElements(By.XPath(".//following-sibling::span"));
                            int sortIndex;
                            string sortType = "asc";
                            string sortClass = sortElements[0].GetAttribute("class");

                            if (colNames.Count == 1) //use to verify single column sorting only
                            {
                                sortIndex = -1;                                
                            }
                            else
                            {
                                if (sortElements != null && sortElements.Count > 1)
                                {
                                    sortIndex = int.Parse(sortElements[1].Text);
                                }
                                else
                                    break;
                            }

                            if (sortClass.Contains("desc"))
                                sortType = "desc";

                            actualColNamesAndSorting.Add($"{columnHeader}/{sortType}{(sortIndex == -1 ? "" : "/" + sortIndex)}");
                            break;
                        }
                    }
                }

                DlkAssert.AssertEqual("VerifyColumnHeaderSortType()", expectedColNamesAndSorting.ToArray(), actualColNamesAndSorting.ToArray());
                DlkLogger.LogInfo("VerifyColumnHeaderSortType() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnHeaderSortType() failed : " + e.Message, e);
            }
        }

        [Keyword("DragColumn", new String[] { "text|ColumnA", "text|ColumnB" })]
        public void DragColumn(String FromColumn, String ToColumn)
        {
            try
            {
                Initialize();

                IWebElement getTargetColumn(string columnName) 
                {
                    for (int i = 0; i < mColumnHeaders.Count; i++)
                    {
                        IWebElement targetColumn = mColumnHeaders[i];
                        string columnHeader = !string.IsNullOrEmpty(targetColumn.Text) ? targetColumn.Text.Trim() :
                                                new DlkBaseControl("Target Column", targetColumn).GetValue().Trim();

                        if (columnHeader == columnName)
                            return targetColumn.FindElement(By.XPath(".//ancestor::th"));
                    }
                    return null;
                }

                IWebElement fromCol = getTargetColumn(FromColumn);
                IWebElement toCol = getTargetColumn(ToColumn);

                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.DragAndDrop(fromCol, toCol).Release().Build().Perform();
                DlkLogger.LogInfo("DragColumn() Passed");
            }
            catch (Exception e)
            {
                throw new Exception("DragColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("SortColumn", new String[] { "1|text|Col1|Col1~Col2~Col3", "1|text|Asc|Asc~Desc~Asc" })]
        public void SortColumn(String ColumnName, String AscOrDesc)
        {
            try
            {
                Initialize();

                List<string> colNames = ColumnName.Split('~').ToList();
                List<string> sorting = AscOrDesc.Split('~').ToList();

                if (colNames.Count != sorting.Count)
                    throw new Exception("Column names count should be equal to AscOrDesc count.");

                for (int i = 0; i < colNames.Count; i++)
                {
                    for (int j = 0; j < mColumnHeaders.Count; j++)
                    {
                        IWebElement targetColumn = mColumnHeaders[j];
                        string columnHeader = !String.IsNullOrEmpty(targetColumn.Text) ?
                                                targetColumn.Text.Trim() :
                                                new DlkBaseControl("Target Column", targetColumn).GetValue().Trim();

                        if (columnHeader == colNames[i])
                        {
                            var sortElements = targetColumn.FindElements(By.XPath(".//following-sibling::span"));
                            string sortClass = sortElements[0].GetAttribute("class");

                            if (sorting[i].ToLower() == "desc")
                            {
                                if (string.IsNullOrEmpty(sortClass))
                                    targetColumn.Click();
                            }
                            else //asc
                            {
                                if (sortClass.Contains("desc"))
                                    targetColumn.Click();
                            }
                            Thread.Sleep(300);
                            targetColumn.Click();

                            DlkLogger.LogInfo($"SortColumn() successfully clicked column {colNames[i]} sort by {sorting[i]}");
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("SortColumn() failed : " + e.Message, e);
            }
        }

        [Keyword("ResetTableSort")]
        public void ResetTableSort()
        {
            try
            {
                Initialize();
                IWebElement getCloseButton() => mElement.FindElements(By.XPath(".//div[contains(@class,'header-actions reset')]/button")).FirstOrDefault();
                IWebElement resetSortButton = getCloseButton();
                int retry = 1;
                bool success = false;

                while (retry <= 3)
                {
                    resetSortButton.Click();
                    Thread.Sleep(300);
                    resetSortButton = getCloseButton();
                    if (resetSortButton == null)
                    {
                        success = true;
                        break;
                    }

                    DlkLogger.LogInfo($"ResetTableSort() :  Failed to click close button. Retry {retry}");
                    retry++;
                }

                if (success)
                    DlkLogger.LogInfo("ResetTableSort() successfully executed.");
                else
                    throw new Exception("Failed to click ResetSortButton in table.");
            }

            catch (Exception e)
            {
                throw new Exception("ResetTableSort() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTableColumnHeaderText")]
        public void VerifyTableColumnHeaderText(String ColumnIndex, String ExpectedResult)
        {
            try
            {
                Initialize();

                int index = 0;
                if (!int.TryParse(ColumnIndex, out index))
                    throw new Exception("[" + ColumnIndex + "] is not a valid input for parameter ColumnIndex.");

                int totalCount = mColumnHeaders.Count;
                if (index > totalCount || index < 1)
                    throw new Exception("Column index [" + index.ToString() + "] is not within the range of total columns [" + totalCount.ToString() + "]");

                IWebElement targetColumn = mColumnHeaders.ElementAt(index - 1);
                string ActualResult = !String.IsNullOrEmpty(targetColumn.Text) ?
                    targetColumn.Text.Trim() : new DlkBaseControl("Target Column", targetColumn).GetValue().Trim();

                DlkAssert.AssertEqual("VerifyTableColumnHeaderText()", ExpectedResult.ToLower(), ActualResult.ToLower());
                DlkLogger.LogInfo("VerifyTableColumnHeaderText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableColumnHeaderText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowHeader")]
        public void VerifyRowHeader(String RowIndex, String ExpectedResult)
        {
            try
            {
                Initialize();

                IWebElement tableRowHeader = mElement.FindElement(By.XPath(string.Format("../../div[contains(@class,'ng-scope')]/table[contains(@class,'hover')]/descendant::td[contains(@class,'ng-binding') and contains(@style,'uppercase')][{0}]", RowIndex)));

                var tableRowName = tableRowHeader.Text;
                if (tableRowName != null)
                {
                    DlkAssert.AssertEqual("VerifyRowHeader()", ExpectedResult.ToLower()
                    , tableRowName.ToLower());
                    DlkLogger.LogInfo("VerifyRowHeader() successfully executed.");
                }
                else
                {
                    throw new Exception(tableRowName + " is NULL");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowHeader() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyLineOptionsListExists")]
        public void VerifyLineOptionsListExists(String LineIndex, String TrueOrFalse)
        {

            try
            {
                Initialize();
                ClickLineOption(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                DlkAssert.AssertEqual("VerifyLineOptionsListExists()", Convert.ToBoolean(TrueOrFalse), targetRow.FindElements(By.XPath("./descendant::*[@class='dropdown-menu dropdown-menu-right line-action']")).Count > 0);
                DlkLogger.LogInfo("VerifyLineOptionsListExists() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptionsListExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineOptions")]
        public void VerifyLineOptions(String LineIndex, String ListItems)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", DDown);
                if (!list.Exists(1))
                {
                    ClickLineOption(LineIndex);
                    Thread.Sleep(5000);
                }

                list.FindElement();

                String listVisibleOptions = "";
                ReadOnlyCollection<IWebElement> optionItems = list.mElement.FindElements(By.XPath("./descendant::div[contains(@class,'dropdown-item')]"));
                foreach (IWebElement opt in optionItems)
                {
                    if (string.IsNullOrEmpty(opt.Text) || string.IsNullOrEmpty(opt.Text.Trim()))
                    {
                        continue;
                    }
                    listVisibleOptions += opt.Text + "~";
                }
                listVisibleOptions = listVisibleOptions.Trim('~');
                DlkAssert.AssertEqual("VerifyLineOptions", ListItems.ToLower(), listVisibleOptions.ToLower());
                /* Close the the dropdown list */

                CloseLineOptions(LineIndex);
                DlkLogger.LogInfo("VerifyLineOptions() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptions() failed : " + e.Message, e);
            }
        }
        
        [Keyword("VerifyLineOptionVisible")]
        public void VerifyLineOptionVisible(String LineIndex, String ItemName, String TrueOrFalse)
        {
            try
            {
                Initialize();
                ClickLineOption(LineIndex);
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                DlkAssert.AssertEqual("VerifyLineOptionVisible()", Convert.ToBoolean(TrueOrFalse), targetRow.FindElement(By.XPath("./descendant::a[text()='" + ItemName + "'][contains(@class,'ng-binding')]")).Text.ToLower().Equals(ItemName.ToLower()));
                DlkLogger.LogInfo("VerifyLineOptionVisible() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptionVisible() failed : " + e.Message, e);
            }
        }
        
        [Keyword("VerifyLineOptionDisabled")]
        public void VerifyLineOptionDisabled(String LineIndex, String ItemName, String TrueOrFalse)
        {
            try
            {
                Initialize();
                ClickLineOption(LineIndex);

                string targetRow_XPath = "./ descendant::tbody / tr[" + LineIndex + "]";
                string targetLineOption_XPath = "./descendant::a[text()='" + ItemName + "'][contains(@class,'ng-binding')]";
                IWebElement targetRow = mElement.FindElement(By.XPath(targetRow_XPath));

                //get the lineoption based on input text or by seperate dropdown menu
                IWebElement mLineOption = targetRow.FindElements(By.XPath(targetLineOption_XPath)).Count > 0 ?
                   targetRow.FindElement(By.XPath(targetLineOption_XPath)) : GetLineOption(ItemName);
                //throw exception if line option is not visible
                if (!mLineOption.Text.ToLower().Equals(ItemName.ToLower()))
                {
                    throw new Exception("Line option is not visible.");
                }
                //check if line option is disabled by checking for class name "disabled"
                var isDisabled = mLineOption.GetAttribute("class").Contains("disabled");
                DlkAssert.AssertEqual("VerifyLineOptionDisabled()", Convert.ToBoolean(TrueOrFalse), isDisabled);
                DlkLogger.LogInfo("VerifyLineOptionDisabled() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineOptionDisabled() failed : " + e.Message, e);
            }
        }

        [Keyword("DisplayLineDetails", new String[] { "1|text|Expected Value|TRUE" })]
        public void DisplayLineDetails(String LineIndex)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));
                IWebElement btn = targetRow.FindElement(By.XPath("./descendant::*[@class='icon-dots']"));
                new DlkBaseControl("targetButton", btn).ScrollIntoViewUsingJavaScript();
                btn.Click();
                DlkLogger.LogInfo("DisplayLineDetails() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("DisplayLineDetails() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineDetailsHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineDetailsHeader(String LineIndex, String TrueOrFalse)
        {
            //todo: verify label

            try
            {
                Initialize();
                IWebElement popOver = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]/*[@class='popover']"));
                IWebElement header = popOver.FindElement(By.XPath("./descendant::h4[@class='ng-binding']"));
                DlkAssert.AssertEqual("VerifyLineDetailsHeader()", Convert.ToBoolean(TrueOrFalse), header.Text);
                DlkLogger.LogInfo("VerifyLineDetailsHeader() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineDetailsHeader() failed : " + e.Message, e);
            }
        }
        
        [Keyword("VerifyItemInTextBoxList")]
        public void VerifyItemInTextBoxList(String LineIndex, String ColNameOrIndex, String ItemToBeVerified, String TrueOrFalse)
        {
            Initialize();
            //get specific cell
            int row = 0;
            if (!int.TryParse(LineIndex, out row))
                throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

            IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

            IWebElement targetInput = targetCell.FindElement(By.TagName("input"));
            bool found = false;
            //wait for dropdown suggestions/autocomplete feature
            Thread.Sleep(2000);
            //gets all of the dropdown items
            foreach (IWebElement dropDownItem in targetInput.FindElements(By.XPath("./parent::div//div[@class='result']/descendant::div[@class='row ng-scope']")))
            {
                if (dropDownItem.FindElements(By.XPath("./div/p")).Count > 0)
                {
                    //go to next item
                    if (found)
                    {
                        break;
                    }
                    //check each <p> tag if it contains the desired text
                    foreach (var p in dropDownItem.FindElements(By.XPath("./div/p")))
                    {
                        DlkLogger.LogInfo("Looking at text.. " + p.Text);
                        if (p.Text.Trim().ToLower().Equals(ItemToBeVerified.ToLower()))
                        {
                            found = true;
                            DlkLogger.LogInfo("Found item " + p.Text);
                            break;
                        }
                        else
                        {
                            //go to next
                            continue;
                        }
                    }
                }
            }
            if (!found)
            {
                throw new Exception("Item was not found.");
            }
            else
            {
                DlkLogger.LogInfo("VerifyItemInTextBoxList() passed");
            }
        }

        [Keyword("GetLineIndexWithColumnValue", new String[] { "1|text|ColNameOrIndex|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|LineIndex"})]
        public void GetLineIndexWithColumnValue(string ColumnName, string Value, string VariableName)
        {
            try
            {
                Initialize();
                int totalRowCount = mTableRows.Count;
                int lineIndex = -1;
                for (int l = 1; l <= totalRowCount; l++)
                {
                    string cellValue = GetCellValue(l, ColumnName);
                    if (cellValue == Value)
                    {
                        lineIndex = l;
                        break;
                    }
                }

                if (lineIndex == -1)
                    throw new Exception("No line index retrieved with the specified value [" + Value + "]");

                DlkVariable.SetVariable(VariableName, lineIndex.ToString());
                DlkLogger.LogInfo("Line Index: [" + lineIndex.ToString() + "] stored to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetRowWithColumnValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineIndexWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLineIndexWithColumnValueContains", new String[] { "1|text|ColNameOrIndex|ID",
                                                         "2|text|SearchedValue|1234",
                                                         "3|text|VariableName|LineIndex"})]
        public void GetLineIndexWithColumnValueContains(string ColumnName, string Value, string VariableName)
        {
            try
            {
                Initialize();
                int totalRowCount = mTableRows.Count;
                int lineIndex = -1;
                for (int l = 1; l <= totalRowCount; l++)
                {
                    string cellValue = GetCellValue(l, ColumnName);
                    if (cellValue.ToLower().Contains(Value.ToLower()))
                    {
                        lineIndex = l;
                        DlkLogger.LogInfo("Expected Value to Contain: [" + Value + "] Actual Value: [" + cellValue + "]");
                        break;
                    }
                }

                if (lineIndex == -1)
                    throw new Exception("No line index retrieved with the specified value [" + Value + "]");

                DlkVariable.SetVariable(VariableName, lineIndex.ToString());
                DlkLogger.LogInfo("Line Index: [" + lineIndex.ToString() + "] stored to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetLineIndexWithColumnValueContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineIndexWithColumnValueContains() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLineCount", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void GetLineCount(string VariableName)
        {
            try
            {
                Initialize();
                int actRowCount = mTableRows.Count;
                string count = actRowCount.ToString(); 
                DlkVariable.SetVariable(VariableName, count);
                DlkLogger.LogInfo("[" + count + "] stored to variable name: [" + VariableName + "]");
                DlkLogger.LogInfo("GetLineCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineCount() failed : " + e.Message, e);
            }

        }

        [Keyword("GetCellValue", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void GetCellValue(String ColNameOrIndex, String LineIndex, String VariableName)
        {
            try
            {

                Initialize();
                string cellValue = GetCellValue(Convert.ToInt32(LineIndex), ColNameOrIndex);
                DlkVariable.SetVariable(VariableName, cellValue);
                DlkLogger.LogInfo("Value: [" + cellValue + "] stored to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }

        }

        [Keyword("EnterSimultaneousKeys", new String[] { "1|text|Row|1", 
                                                "2|text|VariableName|RowCount"})]
        public void EnterSimultaneousKeys(String ColNameOrIndex, String LineIndex, String CtrlShiftAlt, String ConcurrentKey)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                int columnNum = 0;
                if (!int.TryParse(ColNameOrIndex, out columnNum))
                    columnNum = GetColumnNumberFromName(ColNameOrIndex);
                else
                    columnNum = Convert.ToInt32(ColNameOrIndex);

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                targetCell.Click();

                var cell = targetCell.FindElement(By.TagName("input"));
                InputConcurrentKey(row, columnNum, CtrlShiftAlt, ConcurrentKey, cell);
                DlkLogger.LogInfo("EnterSimultaneousKeys() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("EnterSimultaneousKeys() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyCellReadonly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellReadonly(String ColNameOrIndex, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();
                Value = Value.ToLower();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                targetCell.Click();
                
                if (targetCell.FindElements(By.XPath(".//dm-edit-cell//div[@class='checkbox']/*")).Count > 0)
                {
                    IWebElement check = targetCell.FindElement(By.XPath(".//button[contains(@class,'fa-check')]"));
                    DlkAssert.AssertEqual("VerifyCellReadonly()", Value.ToLower(), new DlkBaseControl("Target", check).IsReadOnly().ToLower());
                }
                else if (targetCell.FindElements(By.XPath(".//dm-edit-cell//div[@class='input-group']/*")).Count > 0)
                {
                    targetCell = targetCell.FindElement(By.XPath(".//dm-edit-cell//div[@class='input-group']/*"));
                    DlkAssert.AssertEqual("VerifyCellReadonly()", Value.ToLower(), new DlkBaseControl("Target", targetCell).IsReadOnly().ToLower());
                }
                else
                {
                    DlkAssert.AssertEqual("VerifyCellReadonly()", Value.ToLower(), new DlkBaseControl("Target", targetCell).IsReadOnly().ToLower());

                }
                DlkLogger.LogInfo("VerifyCellReadonly() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellReadonly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetCellReadOnlyState", new String[] { "1|text|VariableName|SampleVar" })]
        public void GetCellReadOnlyState(String LineIndex, String ColNameOrIndex, String VariableName)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

                string state = targetCell.FindElements(By.TagName("div"))
                    .Where(x => x.GetAttribute("readonly") != null)
                    .ToList().Count > 0 ? "True" : "False";

                DlkVariable.SetVariable(VariableName, state);
                DlkLogger.LogInfo("[" + state + "] assigned to Variable Name: [" + VariableName + "]");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellReadOnlyState() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignRowCellValuesToVariable", new String[] { "1|text|Row|1",
                                                "2|text|VariableName|RowCount"})]
        public void AssignRowCellValuesToVariable(String LineIndex, String VariableName)
        {
            try
            {
                Initialize();
                string allCellsValue = "";
                for(int c=1; c < mColumnHeaders.Count + 1; c++)
                {
                    string cellValue = GetCellValue(Convert.ToInt32(LineIndex), c.ToString());
                    allCellsValue = allCellsValue == "" ? allCellsValue = cellValue : allCellsValue + "~" + cellValue;
                }
                DlkVariable.SetVariable(VariableName, allCellsValue);
                DlkLogger.LogInfo("[" + allCellsValue + " ] Value stored to Variable Name: [" + VariableName + "]");
                DlkLogger.LogInfo("AssignRowCellValuesToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignRowCellValuesToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignColumnCellValuesToVariable", new String[] { "1|text|ColNameOrIndex|1",
                                                "2|text|VariableName|ColValue"})]
        public void AssignColumnCellValuesToVariable(String ColNameOrIndex, String VariableName)
        {
            try
            {
                Initialize();
                string allCellsValue = "";
                int actRowCount = mTableRows.Count;
                for (int r = 1; r < actRowCount + 1; r++)
                {
                    string cellValue = GetCellValue(r, ColNameOrIndex);
                    allCellsValue = allCellsValue == "" ? allCellsValue = cellValue : allCellsValue + "~" + cellValue;
                }
                DlkVariable.SetVariable(VariableName, allCellsValue);
                DlkLogger.LogInfo("[" + allCellsValue + " ] Value stored to Variable Name: [" + VariableName + "]");
                DlkLogger.LogInfo("AssignColumnCellValuesToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignColumnCellValuesToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContainsInAllCell", new String[] { "1|text|ColNameOrIndex|1",
                                                "2|text|VariableName|ColValue"})]
        public void VerifyTextContainsInAllCell(String ColNameOrIndex, String LineIndex, String PartialText)
        {
            try
            {
                Initialize();
                Boolean ActualResult = false;
                if(!String.IsNullOrWhiteSpace(LineIndex))
                {
                    for (int c = 1; c < mColumnHeaders.Count + 1; c++)
                    {
                        string cellValue = GetCellValue(Convert.ToInt32(LineIndex), c.ToString());
                        if (cellValue.ToLower().Contains(PartialText.ToLower()))
                        {
                            DlkLogger.LogInfo("[" + PartialText.ToLower() + "] has matched the cell value [" + cellValue.ToLower() + "] from row [" + LineIndex + "]");
                            ActualResult = true;
                            break;
                        }
                    }
                }

                if(!String.IsNullOrWhiteSpace(ColNameOrIndex))
                {
                    int actRowCount = mTableRows.Count;
                    for (int r = 1; r < actRowCount + 1; r++)
                    {
                        string cellValue = GetCellValue(r, ColNameOrIndex);
                        if (cellValue.ToLower().Contains(PartialText.ToLower()))
                        {
                            DlkLogger.LogInfo("[" + PartialText.ToLower() + "] has matched the cell value [" + cellValue.ToLower() + "] from column [" + ColNameOrIndex + "]");
                            ActualResult = true;
                            break;
                        }
                    }
                }

                if (ActualResult == false)
                    throw new Exception("No cell value has matched [" + PartialText.ToLower() + "]");

                DlkLogger.LogInfo("VerifyTextContainsInAllCell() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContainsInAllCell() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLineDoubleClick", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectLineDoubleClick(String LineIndex)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string xpath_TargetRow = "./descendant::tbody/tr[" + LineIndex + "][not(contains(@class,'k-grid-norecords'))][not(contains(@class,'calendar'))]";
                IWebElement targetRow = mElement.FindElements(By.XPath(xpath_TargetRow)).Count > 0 ?
                    mElement.FindElement(By.XPath(xpath_TargetRow)) : null;

                if (targetRow == null)
                    throw new Exception("Row not found.");
                else
                {
                    Actions actions = new Actions(DlkEnvironment.AutoDriver);
                    actions.DoubleClick(targetRow).Build().Perform();
                }
                DlkLogger.LogInfo("SelectLineDoubleClick() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectLineDoubleClick() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactCellValue", new String[] { "ColNameOrIndex|1|Expected Value" })]
        public void VerifyExactCellValue(String ColNameOrIndex, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(row, ColNameOrIndex);

                bool BooleanValue;
                if (Boolean.TryParse(ActualValue, out BooleanValue))
                    DlkAssert.AssertEqual("VerifyExactCellValue(): ", Convert.ToBoolean(Value), BooleanValue);
                else
                    DlkAssert.AssertEqual("VerifyExactCellValue(): ", Value, ActualValue);

                DlkLogger.LogInfo("VerifyExactCellValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactCellValueContains", new String[] { "ColNameOrIndex|1|Expected Value" })]
        public void VerifyExactCellValueContains(String ColNameOrIndex, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                string ActualValue = GetCellValue(row, ColNameOrIndex);

                bool BooleanValue;
                if (Boolean.TryParse(ActualValue, out BooleanValue))
                    DlkAssert.AssertEqual("VerifyExactCellValueContains(): ", Convert.ToBoolean(Value), BooleanValue);
                else
                    DlkAssert.AssertEqual("VerifyExactCellValueContains(): ", Value, ActualValue, true);

                DlkLogger.LogInfo("VerifyExactCellValueContains() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactCellValueContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactCellErrorMessage", new String[] { "ColNameOrIndex|1|Expected Value" })]
        public void VerifyExactCellErrorMessage(String ColNameOrIndex, String LineIndex, String Value = "")
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                IWebElement errorMsg = targetCell.FindElement(By.XPath(".//dm-error"));

                DlkAssert.AssertEqual("VerifyExactCellErrorMessage()", Value, DlkString.RemoveCarriageReturn(new DlkBaseControl("ErrorMessage", errorMsg).GetValue()));
                DlkLogger.LogInfo("VerifyExactCellErrorMessage() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactCellErrorMessage() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactLineButtons", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactLineButtons(String LineIndex, String ExpectedValues)
        {
            try
            {
                Initialize();
                //find the row that the QE needs to check the line options
                IWebElement targetRow = mElement.FindElement(By.XPath(string.Format("./descendant::tbody/tr[{0}]/td[1]", LineIndex)));
                //highlight the row by clicking on it
                DlkLogger.LogInfo(string.Format("Attempting to click row number {0}", LineIndex));
                targetRow.Click();
                DlkLogger.LogInfo(string.Format("Row number {0} was clicked", LineIndex));
                Thread.Sleep(2000);//to give time for the buttons to load.

                //do: check if the line options exist
                ReadOnlyCollection<IWebElement> lineOptions = targetRow.FindElements(By.XPath("../descendant::li"));
                if (lineOptions.Count > 0)
                {
                    DlkLogger.LogInfo(string.Format("VerifyExactLineButtons() successfully executed. {0} line option(s) found."
                        , lineOptions.Count.ToString()));
                }
                else
                {
                    throw new Exception(string.Format("Line option(s) on line number {0} were not found", LineIndex));
                }
                //if the line options exist, do: comparison of actual and expected
                string[] expected = ExpectedValues.Split('~');
                int loopCounter = 0;
                //for each <li> element, go to its corresponding <a> tags to compare the actual values with the expected values
                foreach (var item in lineOptions)
                {
                    string lineOptionTitleAttributeValue = item.FindElement(By.XPath("./a")).GetAttribute("Title");
                    DlkLogger.LogInfo(string.Format("Comparing [{0}] and [{1}]", expected[loopCounter], lineOptionTitleAttributeValue));
                    DlkAssert.AssertEqual("VerifyExactLineButtons", expected[loopCounter], lineOptionTitleAttributeValue);
                    DlkLogger.LogInfo(string.Format("[{0}] and [{1}] are equal", expected[loopCounter], lineOptionTitleAttributeValue));
                    loopCounter++;
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactLineButtons() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactTableColumnHeaderText")]
        public void VerifyExactTableColumnHeaderText(String ColumnIndex, String ExpectedResult)
        {
            try
            {
                Initialize();

                int index = 0;
                if (!int.TryParse(ColumnIndex, out index))
                    throw new Exception("[" + ColumnIndex + "] is not a valid input for parameter ColumnIndex.");

                int totalCount = mColumnHeaders.Count;
                if (index > totalCount || index < 1)
                    throw new Exception("Column index [" + index.ToString() + "] is not within the range of total columns [" + totalCount.ToString() + "]");

                string ActualResult = mColumnHeaders.ElementAt(index - 1).Text;

                DlkAssert.AssertEqual("VerifyExactTableColumnHeaderText()", ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyExactTableColumnHeaderText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactTableColumnHeaderText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactRowHeader")]
        public void VerifyExactRowHeader(String RowIndex, String ExpectedResult)
        {
            try
            {
                Initialize();

                IWebElement tableRowHeader = mElement.FindElement(By.XPath(string.Format("../../div[contains(@class,'ng-scope')]/table[contains(@class,'hover')]/descendant::td[contains(@class,'ng-binding') and contains(@style,'uppercase')][{0}]", RowIndex)));

                var tableRowName = tableRowHeader.Text;
                if (tableRowName != null)
                {
                    DlkAssert.AssertEqual("VerifyExactRowHeader()", ExpectedResult, tableRowName);
                    DlkLogger.LogInfo("VerifyExactRowHeader() successfully executed.");
                }
                else
                {
                    throw new Exception(tableRowName + " is NULL");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactRowHeader() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyExactLineOptions")]
        public void VerifyExactLineOptions(String LineIndex, String ListItems)
        {
            try
            {
                Initialize();
                IWebElement targetRow = mElement.FindElement(By.XPath("./descendant::tbody/tr[" + LineIndex + "]"));

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", DDown);
                if (!list.Exists(1))
                {
                    ClickLineOption(LineIndex);
                    Thread.Sleep(5000);
                }

                list.FindElement();

                String listVisibleOptions = "";
                ReadOnlyCollection<IWebElement> optionItems = list.mElement.FindElements(By.XPath("./descendant::div[contains(@class,'dropdown-item')]"));
                foreach (IWebElement opt in optionItems)
                {
                    if (string.IsNullOrEmpty(opt.Text) || string.IsNullOrEmpty(opt.Text.Trim()))
                    {
                        continue;
                    }
                    listVisibleOptions += opt.Text + "~";
                }
                listVisibleOptions = listVisibleOptions.Trim('~');
                DlkAssert.AssertEqual("VerifyExactLineOptions", ListItems, listVisibleOptions);
                /* Close the the dropdown list */

                CloseLineOptions(LineIndex);
                DlkLogger.LogInfo("VerifyExactLineOptions() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactLineOptions() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactItemInTextBoxList")]
        public void VerifyExactItemInTextBoxList(String LineIndex, String ColNameOrIndex, String ItemToBeVerified, String TrueOrFalse)
        {
            ////div[contains(@class,'table-body')]//table[@class='row ng-scope']
            Initialize();
            //get specific cell
            int row = 0;
            if (!int.TryParse(LineIndex, out row))
                throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

            IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);

            IWebElement targetInput = targetCell.FindElement(By.TagName("input"));
            bool found = false;
            //wait for dropdown suggestions/autocomplete feature
            Thread.Sleep(2000);
            //gets all of the dropdown items
            foreach (IWebElement dropDownItem in targetInput.FindElements(By.XPath("./parent::div//div[@class='result']/descendant::div[@class='row ng-scope']")))
            {
                if (dropDownItem.FindElements(By.XPath("./div/p")).Count > 0)
                {
                    //go to next item
                    if (found)
                    {
                        break;
                    }
                    //check each <p> tag if it contains the desired text
                    foreach (var p in dropDownItem.FindElements(By.XPath("./div/p")))
                    {
                        DlkLogger.LogInfo("Looking at text.. " + p.Text);
                        if (p.Text.Trim().Equals(ItemToBeVerified))
                        {
                            found = true;
                            DlkLogger.LogInfo("Found item " + p.Text);
                            break;
                        }
                        else
                        {
                            //go to next
                            continue;
                        }
                    }
                }
            }
            if (!found)
            {
                throw new Exception("Item was not found.");
            }
            else
            {
                DlkLogger.LogInfo("VerifyExactItemInTextBoxList() passed");
            }
        }

        [Keyword("VerifyExactTextContainsInAllCell", new String[] { "1|text|ColNameOrIndex|1",
                                                "2|text|VariableName|ColValue"})]
        public void VerifyExactTextContainsInAllCell(String ColNameOrIndex, String LineIndex, String PartialText)
        {
            try
            {
                Initialize();
                Boolean ActualResult = false;
                if (!String.IsNullOrWhiteSpace(LineIndex))
                {
                    for (int c = 1; c < mColumnHeaders.Count + 1; c++)
                    {
                        string cellValue = GetCellValue(Convert.ToInt32(LineIndex), c.ToString());
                        if (cellValue.Contains(PartialText))
                        {
                            DlkLogger.LogInfo("[" + PartialText + "] has matched the cell value [" + cellValue + "] from row [" + LineIndex + "]");
                            ActualResult = true;
                            break;
                        }
                    }
                }

                if (!String.IsNullOrWhiteSpace(ColNameOrIndex))
                {
                    int actRowCount = mTableRows.Count;
                    for (int r = 1; r < actRowCount + 1; r++)
                    {
                        string cellValue = GetCellValue(r, ColNameOrIndex);
                        if (cellValue.Contains(PartialText))
                        {
                            DlkLogger.LogInfo("[" + PartialText + "] has matched the cell value [" + cellValue + "] from column [" + ColNameOrIndex + "]");
                            ActualResult = true;
                            break;
                        }
                    }
                }

                if (ActualResult == false)
                    throw new Exception("No cell value has matched [" + PartialText + "]");

                DlkLogger.LogInfo("VerifyExactTextContainsInAllCell() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactTextContainsInAllCell() failed : " + e.Message, e);
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

        #region HEADER FILTER KEYWORDS

        [Keyword("GetHeaderFilterValue")]
        public void GetHeaderFilterValue(String ColNameOrIndex, String VariableName)
        {
            try
            {
                Initialize();
                string actualValue = GetValueHealderFilter(ColNameOrIndex);
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("Header filter value: [" + actualValue + "] set to variable: [" + VariableName + "].");
                DlkLogger.LogInfo("GetHeaderFilterValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetHeaderFilterValue() failed : " + e.Message, e);
            }
        }

        [Keyword("RemoveHeaderFilterValue")]
        public void RemoveHeaderFilterValue(String ColNameOrIndex, String ValueToRemove)
        {
            try
            {
                Initialize();

                if (!int.TryParse(ColNameOrIndex, out int colNumber))
                    colNumber = GetColumnNumberFromName(ColNameOrIndex);

                if (colNumber < 0)
                    throw new Exception("Column header [" + ColNameOrIndex + "] not found. ");

                List<IWebElement> filterCells = GetFilterCells();
                string chipListContainer_XPath = ".//mat-chip-list";
                string chipItem_XPath = ".//mat-basic-chip";
                string chipCloseIcon_XPath = ".//mat-icon";

                if (colNumber <= filterCells.Count)
                {
                    IWebElement targetFilterCell = filterCells.ElementAt(colNumber - 1);
                    IWebElement targetChipList = targetFilterCell.FindElements(By.XPath(chipListContainer_XPath)).Any() ?
                        targetFilterCell.FindElement(By.XPath(chipListContainer_XPath)) : throw new Exception("Chip list container not found.");
                    List<IWebElement> chipItems = targetChipList.FindElements(By.XPath(chipItem_XPath)).ToList();

                    if (chipItems.Count > 0)
                    {
                        int v = 0;
                        string[] valuesToRemove = ValueToRemove.Split('~');
                        bool vFound = false;
                        foreach (IWebElement currentItem in chipItems)
                        {
                            if (v >= valuesToRemove.Count())
                                break;

                            string currentValue = currentItem.Text.Trim();
                            if (currentValue.Equals(valuesToRemove[v].ToString()))
                            {
                                IWebElement chipCloseIcon = currentItem.FindElements(By.XPath(chipCloseIcon_XPath)).Any() ?
                                    currentItem.FindElement(By.XPath(chipCloseIcon_XPath)) : throw new Exception("Chip close icon not found on filter value: [" + currentValue + "].");
                                chipCloseIcon.Click();
                                DlkLogger.LogInfo("Removing filter value: [" + currentValue + "]...");
                                Thread.Sleep(500);
                                vFound = true;
                            }
                            else
                            {
                                DlkLogger.LogInfo("Filter value : [" + currentValue + "] does not match with value to remove: [" + valuesToRemove[v].ToString() + "]...");
                            }
                            v++;
                        }

                        if (!vFound)
                            throw new Exception("No filter value found with value to remove: [" + ValueToRemove + "].");
                    }
                    else
                    {
                        DlkLogger.LogInfo("Column header filter : [" + ColNameOrIndex + "] is empty. No value removed.");
                    }
                }
                else
                {
                    throw new Exception("Column headers does not match with filter fields.");
                }
                DlkLogger.LogInfo("RemoveHeaderFilterValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("RemoveHeaderFilterValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetHeaderFilter")]
        public void SetHeaderFilter(String ColNameOrIndex, String FilterValue, String FieldValue)
        {
            try
            {
                Initialize();
                SetValueToHeaderFilter(ColNameOrIndex, FilterValue, FieldValue);
                DlkLogger.LogInfo("SetHeaderFilter() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetHeaderFilter() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyHeaderFilterValue")]
        public void VerifyHeaderFilterValue(String ColNameOrIndex, String ExpectedValue)
        {
            try
            {
                Initialize();
                string actualValue = GetValueHealderFilter(ColNameOrIndex);
                DlkLogger.LogAssertion("VerifyHeaderFilterValue(): ", ExpectedValue, actualValue);
                DlkLogger.LogInfo("VerifyHeaderFilterValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeaderFilterValue() failed : " + e.Message, e);
            }
        }

        #endregion

        #region CELL DROPDOWN KEYWORDS

        [Keyword("DropDownScrollUntilLast", new String[] { "1|2|10|2" })]
        public void DropDownScrollUntilLast(String ColNameOrIndex, String LineIndex, String NumberOfScrolls)
        {
            try
            {
                Initialize();
                if (!int.TryParse(LineIndex, out int row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                if (!int.TryParse(NumberOfScrolls, out int scroll))
                    throw new Exception("[" + NumberOfScrolls + "] is not a valid input for parameter NumberOfScrolls.");

                if (scroll <= 0)
                    throw new Exception("Value for number of scroll(s) should be at least (1) one.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                string xpath_Scroll = "//*[contains(@class,'dropdown-menu')]//*[contains(@class,'result-wrapper')] | //*[contains(@class,'dropdown-menu')]//dm-result-wrapper";
                IWebElement scrollElm = DlkEnvironment.AutoDriver.FindElements(By.XPath(xpath_Scroll)).Count > 0 ?
                    DlkEnvironment.AutoDriver.FindElement(By.XPath(xpath_Scroll)) : null;

                if (scrollElm == null)
                    throw new Exception("Drop down not supported with a scroll element");

                int n = 0;
                while (n != scroll)
                {
                    n++;
                    DlkLogger.LogInfo("Executing scroll action [" + n.ToString() + "] ...");
                    jse.ExecuteScript("arguments[0].scrollTop += 20000", scrollElm);
                    WaitDropdownSpinner(targetCell);
                }

                DlkLogger.LogInfo("DropDownScrollUntilLast() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DropDownScrollUntilLast() failed : " + e.Message, e);
            }
        }

        [Keyword("ShowDropdownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void ShowDropdownList(String ColNameOrIndex, String LineIndex)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);
                DlkLogger.LogInfo("ShowDropdownList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ShowDropdownList() failed : " + e.Message, e);
            }
        }

        [Keyword("GetDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetDropDownList(String ColNameOrIndex, String LineIndex, String VariableName)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);
                string actualItems = GetDropdownListItemsString(resultItems);

                DlkVariable.SetVariable(VariableName, actualItems);
                DlkLogger.LogInfo("[" + actualItems + "] stored to variable name: [" + VariableName + "]");
                CloseDropdownList(targetCell, results);

                DlkLogger.LogInfo("GetDropDownList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLineCountDropDown", new String[] { "text|1|SampleVar" })]
        public void GetLineCountDropDown(String ColumnName, String LineIndex, String VariableName)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColumnName);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);

                int resultCount = resultItems.Count;

                DlkVariable.SetVariable(VariableName, resultCount.ToString());
                DlkLogger.LogInfo("[" + resultCount.ToString() + "] stored to variable name: [" + VariableName + "]");

                CloseDropdownList(targetCell, results);
                DlkLogger.LogInfo("GetLineCountDropDown() passed.");

            }
            catch (Exception e)
            {
                throw new Exception("GetLineCountDropDown() failed: " + e.Message, e);
            }
        }

        [Keyword("GetItemDropDownByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetItemDropDownByIndex(String ColNameOrIndex, String LineIndex, String ItemIndex, String VariableName)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                int index = 0;
                if (!int.TryParse(ItemIndex, out index) || index == 0)
                    throw new Exception("[" + ItemIndex + "] is not a valid input for parameter ItemIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);

                if (index > resultItems.Count())
                    throw new Exception("[" + ItemIndex + "] ItemIndex exceeds the total items on the list.");

                IWebElement targetItem = resultItems[index - 1];
                string actualValue = DlkString.ReplaceCarriageReturn(new DlkBaseControl("Element", targetItem).GetValue(), "").Trim().ToLower();
               
                CloseDropdownList(targetCell, results);

                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] stored to variable name: [" + VariableName + "]");
                DlkLogger.LogInfo("GetItemDropDownByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemDropDownByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectCellValueFromList", new String[] { "1|text|Value|SampleValue" })]
        public void SelectCellValueFromList(String ColNameOrIndex, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);
                SelectItemDropdownList(resultItems, Value); 
               
                DlkLogger.LogInfo("Successfully executed SelectCellValueFromList()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCellValueFromList() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectCellValueFromListContains", new String[] { "1|text|Value|SampleValue" })]
        public void SelectCellValueFromListContains(String ColNameOrIndex, String LineIndex, String Value)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);
                SelectItemDropdownList(resultItems, Value, true);

                DlkLogger.LogInfo("Successfully executed SelectCellValueFromListContains()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectCellValueFromListContains() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemDropDownByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItemDropDownByIndex(String ColNameOrIndex, String LineIndex, String ItemIndex)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                int index = 0;
                if (!int.TryParse(ItemIndex, out index) || index == 0)
                    throw new Exception("[" + ItemIndex + "] is not a valid input for parameter ItemIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);

                if (index > resultItems.Count())
                    throw new Exception("[" + ItemIndex + "] ItemIndex exceeds the total items on the list.");

                try
                {
                    resultItems[index - 1].Click();
                }
                catch
                {
                    IWebElement targetItem = resultItems[index - 1].FindElement(By.XPath(".//parent::div"));
                    new DlkBaseControl("TargetItem", targetItem).ScrollIntoViewUsingJavaScript();
                    Actions actions = new Actions(DlkEnvironment.AutoDriver);
                    actions.Click(targetItem).Build().Perform();
                }

                DlkLogger.LogInfo("Click() successfully executed.");

                CloseDropdownList(targetCell, results);
                DlkLogger.LogInfo("SelectItemDropDownByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemDropDownByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDropDownListCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDropDownListCount(String ColNameOrIndex, String LineIndex, String Count)
        {
            try
            {
                Initialize();
                int actualListCount = 0;

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);
                actualListCount = resultItems.Count;

                CloseDropdownList(targetCell, results);
                DlkAssert.AssertEqual("VerifyDropDownListCount()", int.Parse(Count), actualListCount);
                DlkLogger.LogInfo("VerifyDropDownListCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDropDownListCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInDropDownList(String ColumnName, String LineIndex, String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                bool trueOrfalse;
                if (!Boolean.TryParse(TrueOrFalse, out trueOrfalse))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                IWebElement targetCell = GetCell(LineIndex, ColumnName);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);

                bool ActualResult = VerifyItemDropdownList(resultItems, Item);
                bool ExpectedResult = bool.Parse(TrueOrFalse);

                CloseDropdownList(targetCell, results);
                DlkAssert.AssertEqual("VerifyItemInDropDownList()", ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyItemInDropDownList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactItemInDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactItemInDropDownList(String ColumnName, String LineIndex, String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                bool trueOrfalse;
                if (!Boolean.TryParse(TrueOrFalse, out trueOrfalse))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                IWebElement targetCell = GetCell(LineIndex, ColumnName);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);

                bool ActualResult = VerifyItemDropdownList(resultItems, Item, false);
                bool ExpectedResult = bool.Parse(TrueOrFalse);

                CloseDropdownList(targetCell, results);
                DlkAssert.AssertEqual("VerifyExactItemInDropDownList()", ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyExactItemInDropDownList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactItemInDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyIfDropdownListContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyIfDropdownListContains(String ColumnName, String LineIndex, String Contains)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColumnName);

                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);

                Boolean hasMatched = VerifyItemDropdownList(resultItems, Contains, true, true);

                CloseDropdownList(targetCell, results);

                if (hasMatched)
                    DlkLogger.LogInfo("VerifyIfDropdownListContains() successfully executed.");
                else
                    throw new Exception("No item from dropdown contains text " + Contains);

            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfDropdownListContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactDropdownListContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactDropdownListContains(String ColumnName, String LineIndex, String Contains)
        {
            try
            {
                Initialize();

                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColumnName);

                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);

                Boolean hasMatched = VerifyItemDropdownList(resultItems, Contains, false, true);

                CloseDropdownList(targetCell, results);

                if (hasMatched)
                    DlkLogger.LogInfo("VerifyExactDropdownListContains() successfully executed.");
                else
                    throw new Exception("No item from dropdown contains text " + Contains);

            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactDropdownListContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDropDownList(String ColNameOrIndex, String LineIndex, String Items)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);
                string actualItems = GetDropdownListItemsString(resultItems);

                DlkAssert.AssertEqual("VerifyDropDownList()", Items.ToLower(), actualItems);
                CloseDropdownList(targetCell, results);
                DlkLogger.LogInfo("VerifyDropDownList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDropDownList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactDropDownList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactDropDownList(String ColNameOrIndex, String LineIndex, String Items)
        {
            try
            {
                Initialize();
                int row = 0;
                if (!int.TryParse(LineIndex, out row))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                IWebElement targetCell = GetCell(LineIndex, ColNameOrIndex);
                DlkBaseControl results = GetResultsList();
                OpenDropdownList(targetCell, results);

                IList<IWebElement> resultItems = GetDropdownListItems(results);
                string actualItems = GetDropdownListItemsString(resultItems, false);

                DlkAssert.AssertEqual("VerifyExactDropDownList()", Items, actualItems);
                CloseDropdownList(targetCell, results);
                DlkLogger.LogInfo("VerifyExactDropDownList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactDropDownList() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
