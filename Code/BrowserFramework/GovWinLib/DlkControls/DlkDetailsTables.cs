using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("DetailsTables")]
    public class DlkDetailsTables : DlkBaseControl
    {
        private String mstrFormTablesXpath = ".//table[contains(@class,'formTable')]|.//table[contains(@class,'chartTable')]|./table";
        private List<DlkTable> mlstFormTables;
        Dictionary<String, DlkCheckBox> chkElems;

        public DlkDetailsTables(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDetailsTables(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDetailsTables(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public void Initialize()
        {                
            FindElement();
            RefreshFormTables();
        }

        [Keyword("RefreshDetailTables")]
        public void RefreshFormTables()
        {
            FindElement();
            mlstFormTables = new List<DlkTable>();
            IList<IWebElement> lstFormTablesElements;

            if (mElement.TagName.Equals("table"))
            {
                lstFormTablesElements = mElement.FindElements(By.XPath("."));
            }
            else
            {
                if (mControlName.Equals("ComparePricing_Content"))
                {
                    lstFormTablesElements = mElement.FindElements(By.Id("CompareContent"));
                }
                if (mControlName.Equals("ContractSummary_Table"))
                {
                    lstFormTablesElements = mElement.FindElements(By.XPath("."));
                }
                else
                {
                    lstFormTablesElements = mElement.FindElements(By.XPath(mstrFormTablesXpath));
                }
            }

            foreach (IWebElement formTableElement in lstFormTablesElements)
            {
                mlstFormTables.Add(new DlkTable("FormTable", formTableElement));
            }

            DlkLogger.LogInfo("Successfully executed RefreshDetailTables()");
        }
        
        [Keyword("GetDetailInfo", new String[] { "1|text|Detail Caption|GovWin IQ Task Order ID"})]
        public void GetDetailInfo(String DetailCaption, String VariableName)
        {
            Boolean bFound = false;

            Initialize();

            foreach (DlkTable formTable in mlstFormTables)
            {
                if (formTable.ColumnHeaderExists(DetailCaption))
                {
                    formTable.GetTableRowWithDetailHeader(DetailCaption, "GetDetailInfoRow");
                    String GetDetailInfoRow = DlkVariable.GetVariable("O{GetDetailInfoRow}");
                    formTable.GetTableCellValue(GetDetailInfoRow, DetailCaption, VariableName);
                    bFound = true;
                }
            }

            if (!bFound)
            {
                throw new Exception("GetDetailInfo() failed. Detail Caption '" + DetailCaption + "' not found.");
            }
        }

        [Keyword("SetDetailInfo", new String[] { "1|text|Detail Caption|GovWin IQ Task Order ID",
                                                        "2|text|Value|Sample Value" })]
        public void SetDetailInfo(String DetailCaption, String Value)
        {
            Boolean bFound = false;

            Initialize();

            foreach (DlkTable formTable in mlstFormTables)
            {
                DlkLogger.LogInfo(formTable.GetTableColumnHeaders());
                if (formTable.ColumnHeaderExists(DetailCaption))
                {
                    formTable.GetTableRowWithDetailHeader(DetailCaption, "SetDetailInfoRow");
                    String SetDetailInfoRow = DlkVariable.GetVariable("O{SetDetailInfoRow}");
                    formTable.SetTableCellValue(SetDetailInfoRow, DetailCaption, Value);
                    bFound = true;
                }
            }

            if (!bFound)
            {
                throw new Exception("SetDetailInfo() failed. Detail Caption '" + DetailCaption + "' not found.");
            }
        }

        [Keyword("ClickDetailInfo", new String[] { "1|text|Detail Caption|GovWin IQ Task Order ID" })]
        public void ClickDetailInfo(String DetailCaption)
        {
            Boolean bFound = false;

            Initialize();

            foreach (DlkTable formTable in mlstFormTables)
            {
                if (formTable.ColumnHeaderExists(DetailCaption))
                {
                    formTable.GetTableRowWithDetailHeader(DetailCaption, "ClickDetailInfoRow");
                    String ClickDetailInfoRow = DlkVariable.GetVariable("O{ClickDetailInfoRow}");
                    formTable.ClickTableCellLink(ClickDetailInfoRow, DetailCaption);
                    bFound = true;
                }
            }

            if (!bFound)
            {
                throw new Exception("ClickDetailInfo() failed. Detail Caption '" + DetailCaption + "' not found.");
            }
        }

        [Keyword("ClickDetailInfoLink", new String[] { "1|text|Detail Caption|GovWin IQ Task Order ID",
                                                       "2|text|Link Text|View Org Chart" })]
        public void ClickDetailInfoLink(String DetailCaption, String LinkText)
        {            
            Boolean bFound = false;

            Initialize();

            foreach (DlkTable formTable in mlstFormTables)
            {
                if (formTable.ColumnHeaderExists(DetailCaption))
                {
                    formTable.GetTableRowWithDetailHeader(DetailCaption, "DetailInfoRow");
                    String DetailInfoRow = DlkVariable.GetVariable("O{DetailInfoRow}");
                    formTable.ClickTableCellLinkWithText(DetailInfoRow, DetailCaption, LinkText.Trim());
                    bFound = true;
                    break;
                }
            }

            if (!bFound)
            {
                throw new Exception("ClickDetailInfoLink() failed. Detail Caption '" + DetailCaption + "' not found.");
            }
        }

        [Keyword("ClickDetailInfoButton", new String[] { "1|text|Detail Caption|Comments",
                                                         "2|text|Button Text|Edit"})]
        public void ClickDetailInfoButton(String DetailCaption, String ButtonText)
        {
            Boolean bFound = false;

            Initialize();

            foreach (DlkTable formTable in mlstFormTables)
            {
                if (formTable.ColumnHeaderExists(DetailCaption))
                {
                    formTable.GetTableRowWithDetailHeader(DetailCaption, "DetailInfoRow");
                    String DetailInfoRow = DlkVariable.GetVariable("O{DetailInfoRow}");
                    formTable.ClickTableCellButtonWithText(DetailInfoRow, DetailCaption, ButtonText);
                    bFound = true;
                    break;
                }
            }

            if (!bFound)
            {
                throw new Exception("ClickDetailInfoButton() failed. Detail Caption '" + DetailCaption + "' not found.");
            }         
        }

        [Keyword("SelectDetailInfoComboBox", new String[] { "1|text|Detail Caption|Comments",
                                                         "2|text|Expected Text|a"})]
        public void SelectDetailInfoComboBox(String DetailCaption, String ButtonText)
        {
            Boolean bFound = false;

            Initialize();

            foreach (DlkTable formTable in mlstFormTables)
            {
                if (formTable.ColumnHeaderExists(DetailCaption))
                {
                    formTable.GetTableRowWithDetailHeader(DetailCaption, "DetailInfoRow");
                    String DetailInfoRow = DlkVariable.GetVariable("O{DetailInfoRow}");
                    formTable.ClickTableCellComboBox(DetailInfoRow, DetailCaption, ButtonText);
                    bFound = true;
                    break;
                }
            }

            if (!bFound)
            {
                throw new Exception("SelectDetailInfoComboBox() failed. Detail Caption '" + DetailCaption + "' not found.");
            }
        }
        #region Verify methods
        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    VerifyExists(Convert.ToBoolean(expectedValue));
                }, new String[] { "retry" });
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyDetailInfo", new String[] {   "1|text|Detail Caption|GovWin IQ Task Order ID",
                                                            "2|text|Expected Value|1234"})]
        public void VerifyDetailInfo(String DetailCaption, String ExpectedValue)
        {
            String detailCaption = DetailCaption;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { detailCaption, expectedValue };

            this.PerformAction(() =>
            {
                Boolean bFound = false;

                Initialize();

                foreach (DlkTable formTable in mlstFormTables)
                {                    
                    if (formTable.ColumnHeaderExists(detailCaption))
                    {
                        formTable.GetTableRowWithColumnValue(detailCaption, expectedValue, "VerifyPairDetailInfoRow");
                        String VerifyPairDetailInfoRow = DlkVariable.GetVariable("O{VerifyPairDetailInfoRow}");
                        formTable.VerifyTableCellValue(VerifyPairDetailInfoRow, detailCaption, expectedValue);
                        bFound = true;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("VerifyDetailInfo() failed. Detail Caption '" + detailCaption + "' not found.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyDetailInfoCaptionWithoutWhiteSpace", new String[] {   "1|text|Detail Caption|GovWin IQ Task Order ID",
                                                            "2|text|Expected Value|1234"})]
        public void VerifyDetailInfoCaptionWithoutWhiteSpace(String DetailCaption, String ExpectedValue)
        {
            String detailCaption = DetailCaption;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { detailCaption, expectedValue };

            this.PerformAction(() =>
            {
                Boolean bFound = false;

                Initialize();

                foreach (DlkTable formTable in mlstFormTables)
                {
                    if (formTable.ColumnHeaderExistsNoWhiteSpace(detailCaption))
                    {
                        formTable.GetTableRowWithDetailHeader(DetailCaption, "DetailInfoRow");
                        String DetailInfoRow = DlkVariable.GetVariable("O{DetailInfoRow}");
                        formTable.VerifyTableCellValue(DetailInfoRow, detailCaption, expectedValue);
                        bFound = true;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("VerifyDetailInfoCaptionWithoutWhiteSpace() failed. Detail Caption '" + detailCaption + "' not found.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyPartialDetailInfo", new String[] {   "1|text|Partial Detail Caption|GovWin IQ Task Order ID",
                                                            "2|text|Expected Value|1234"})]
        public void VerifyPartialDetailInfo(String PartialDetailCaption, String ExpectedValue)
        {
            String detailCaption = PartialDetailCaption;
            String expectedValue = ExpectedValue;

            String[] inputs = new String[] { detailCaption, expectedValue };

            this.PerformAction(() =>
            {
                Boolean bFound = false;

                Initialize();

                foreach (DlkTable formTable in mlstFormTables)
                {
                    if (formTable.ColumnHeaderExists(detailCaption))
                    {
                        formTable.GetTableRowWithDetailHeader(PartialDetailCaption, "DetailInfoRow");
                        String DetailInfoRow = DlkVariable.GetVariable("O{DetailInfoRow}");
                        formTable.VerifyTableCellValueContains(DetailInfoRow, detailCaption, expectedValue);
                        bFound = true;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("VerifyPartialDetailInfo() failed. Detail Caption '" + detailCaption + "' not found.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyCaptionsExist", new String[] { "1|text|Expected (TRUE or FALSE)|TRUE" })]
        public void VerifyCaptionsExist(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                Boolean bFound = false;

                Initialize();

                foreach (DlkTable formTable in mlstFormTables)
                {
                    if (formTable.ContainsColumnHeaders())
                    {
                        bFound = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyCaptionsExist", Convert.ToBoolean(expectedValue), bFound);
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyCaptionExist", new String[] { "1|text|Detail Caption|GovWin IQ Task Order ID",
                                                            "2|text|Expected (TRUE or FALSE)|TRUE" })]
        public void VerifyCaptionExist(String DetailCaption, String TrueOrFalse)
        {
            String detailCaption = DetailCaption;
            String expectedValue = TrueOrFalse;

            String[] inputs = new String[] { detailCaption, expectedValue };


            this.PerformAction(() =>
            {
                Boolean bFound = false;

                Initialize();

                foreach (DlkTable formTable in mlstFormTables)
                {
                    if (formTable.ColumnHeaderExists(detailCaption))
                    {
                        bFound = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyCaptionExist", Convert.ToBoolean(expectedValue), bFound);
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyPairDetailInfo", new String[] {   "1|text|Detail Caption|GovWin IQ Task Order ID",
                                                                "2|text|Expected Value|1234"})]
        public void VerifyPairDetailInfo(String DetailCaption, String ExpectedValue)
        {
            String detailCaption = DetailCaption;
            String expectedValue = ExpectedValue;

            this.PerformAction(() =>
            {
                Boolean bFound = false;

                Initialize();

                //string id = Guid.NewGuid().ToString();
                //DlkEnvironment.CurrentStepOutput = id;

                foreach (DlkTable formTable in mlstFormTables)
                {
                    if (formTable.ColumnHeaderExists(detailCaption))
                    {
                        formTable.GetTableRowWithColumnValue(detailCaption, expectedValue, "VerifyPairDetailInfoRow");
                        bFound = true;
                    }
                }

                //DlkEnvironment.OutputData.Remove(id);

                if (!bFound)
                {
                    throw new Exception("VerifyDetailInfo() failed. Detail Caption '" + detailCaption + "' not found.");
                }
            }, new String[] { "retry" });
        }

        [Keyword("VerifyDetailInfoIsEmpty", new String[] {   "1|text|Detail Caption|GovWin IQ Task Order ID",
                                                                "2|text|Expected Value|TRUE"})]
        public void VerifyDetailInfoIsEmpty(String DetailCaption, String ExpectedValue)
        {
            Initialize();
            String headerTitle = DetailCaption;
            bool expectedResult = Convert.ToBoolean(ExpectedValue);
            bool actualResult = Convert.ToBoolean(ExpectedValue) ? false : true;

            String[] inputs = new String[] { headerTitle, expectedResult.ToString() };

            this.PerformAction(() =>
            {
                Initialize();             

                foreach (DlkTable formTable in mlstFormTables)
                {
                    if (formTable.ColumnHeaderExists(headerTitle))
                    {
                        actualResult = formTable.checkContentIsEmpty(0, headerTitle);
                    }
                }

                DlkAssert.AssertEqual("VerifyDetailInfoIsEmpty", Convert.ToBoolean(expectedResult), actualResult);
            }, new String[] { "retry" });
        }
        #endregion

        [Keyword("SelectCheckBox", new String[] { "1|text|Check Box Number (1,2,3,..)|1",
                                                    "2|text|Selected|TRUE"})]
        public virtual void SelectCheckBox(String CheckBoxNum, String isChecked)
        {

            try
            {
                bool expectedResult = Convert.ToBoolean(isChecked);
                bool actualResult = expectedResult ? false : true;
                int iIndex = Int32.Parse(CheckBoxNum) - 1;

                while (!this.Exists())
                {
                    Thread.Sleep(-1);
                }

                mapCheckBox();

                if (iIndex >= chkElems.Count)
                {
                    throw new Exception("SelectCheckBox() failed. Index is greater than the number of result items in the result list.");
                }
                else
                {
                    DlkCheckBox chkSelected = chkElems.ElementAt(iIndex).Value;
                    chkSelected.Set("TRUE");
                    actualResult = chkSelected.mElement.Selected;
                    DlkLogger.LogInfo("Successfully executed SelectCheckBox().");
                }

                DlkAssert.AssertEqual("SelectCheckBox", expectedResult, actualResult);

            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
        }

        private void mapCheckBox()
        {
            int elemInd = 1;
            IList<IWebElement> lstElem = DlkEnvironment.AutoDriver.FindElements(By.TagName("input"));
            chkElems = new Dictionary<string, DlkCheckBox>();

            int lstElemCount = lstElem.Count;
            int indx = 0;
            foreach (IWebElement elem in lstElem)
            {


                var inputType = elem.GetAttribute("class");

                if (inputType.Equals("chbx"))
                {
                    chkElems.Add(elemInd.ToString(), new DlkCheckBox("chkBox" + elemInd, elem));
                    elemInd++;
                }

                indx++;

            }

        }

        bool threadStat = true;
        private void verifyModalWindowExist()
        {
            while (threadStat)
            {

                if (this.Exists())
                {
                    threadStat = false;
                    mapCheckBox();
                }


            }
        }
    }
}

