using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using System.Threading;
using System.Diagnostics;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("DocSearchResultTable")]
    public class DlkDocSearchResultTable : DlkDetailsTables
    {
        Dictionary<String, DlkRadioButton> rdoElems;
        Dictionary<String, DlkCheckBox> chkElems;
        Dictionary<String, DlkButton> headerElems;
        Dictionary<String, IList<IWebElement>> dataElems;
        IList<IWebElement> mRows;
        IList<IWebElement> mHeaders;
        protected List<DlkHeaderGroup> mlstHeaderGroups;
        protected List<DlkRow> mlstRows;
        protected String mstrTableType = "";
        String mOutputVar = "";

        public DlkDocSearchResultTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDocSearchResultTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDocSearchResultTable(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


        //Implement Code, then change type on LeadsAlert.cs

        public new void Initialize()
        {
            FindElement();
        }

        bool threadStat = true;

        [Keyword("ClickTableCellLink", new String[] {"1|text|Row|O{Row}",
                                                    "2|text|Header|Document Title"})]
        public void ClickTableCellLink(String Row, String Header)
        {
            try
            {
                int iRow = Convert.ToInt32(Row);

                while (!this.Exists())
                    Thread.Sleep(-1);

                mapHeaders();
                mapData();

                int iCol = 0;

                for (int i = 0; i < headerElems.Count; i++)
                {
                    if (headerElems.ElementAt(i).Value.mElement.Text.Trim().Equals(Header))
                    {
                        iCol = i;
                        break;
                    }
                }

                IWebElement cell = (IWebElement)dataElems.ElementAt(iRow - 1).Value.ElementAt(iCol);
                IList<IWebElement> links = cell.FindElements(By.CssSelector("a"));
                DlkLink cellLink = new DlkLink("Cell Link", links.First());
                cellLink.Click();                

                DlkLogger.LogInfo("Successfully executed Click().");
            }
            catch (InvalidOperationException invalid)
            {
                DlkLogger.LogError(new Exception(string.Format("Exception of type {0} caught in button Click() method.", invalid.GetType())));
            }


           

        }

        [Keyword("SortHeader", new String[] { "1|text|Header Number(1,2,3,..)|1" })]
        public void SortHeader(String HeaderNumber)
        {
            bool actualResult = false;
            int iIndex = Int32.Parse(HeaderNumber) - 1;

            while (!this.Exists())
                Thread.Sleep(-1);

            mapHeaders();

            if (iIndex >= headerElems.Count)
            {
                DlkLogger.LogError(new Exception("SortHeader() failed. Index is greater than the number of result items in the result list."));
            }
            else
            {
                DlkButton headerSelected = headerElems.ElementAt(iIndex).Value;
                headerSelected.Click();
                actualResult = true;
                DlkLogger.LogInfo("Successfully executed SortHeader().");
            }

            DlkAssert.AssertEqual("SortHeader", true, actualResult);
        }

        [Keyword("VerifyHeaderName", new String[] { "1|text|Expected Header Name|Name", "2|text|Header Number(1,2,3,..)|1" })]
        public void verifyHeaderName(String HeaderName, String HeaderNumumber)
        {
            bool actualResult = false;
            int iIndex = Int32.Parse(HeaderNumumber) - 1;

            while (!this.Exists())
                Thread.Sleep(-1);

            mapHeaders();

            if (iIndex >= headerElems.Count)
            {
                DlkLogger.LogError(new Exception("VerifyHeaderName() failed. Index is greater than the number of result items in the result list."));
            }
            else
            {
                DlkButton headerSelected = headerElems.ElementAt(iIndex).Value;
                if(HeaderName.Trim().Equals(headerSelected.mElement.Text))
                {
                    actualResult = true;
                    DlkLogger.LogInfo("Successfully executed VerifyHeaderName().");
                }
            }

            DlkAssert.AssertEqual("VerifyHeaderName", true, actualResult);
        }

        [Keyword("VerifyDataPerRow", new String[] { "1|text|Row|O{Row}", "2|text|Header/Field|Document Title", "3|text|Expected Text|ABC" })]
        public void VerifyDataPerRow(String Row, String Header, String ExpectedValue)
        {            
            while (!this.Exists())
                Thread.Sleep(-1);

            mapHeaders();
            mapData();

            int col = 0;

            for (int i = 0; i < headerElems.Count; i++)
            {
                if (headerElems.ElementAt(i).Value.mElement.Text.Trim().Equals(Header))
                {
                    col = i;
                    break;
                }
            }

            String actualData = dataElems.ElementAt(Int32.Parse(Row) - 1).Value.ElementAt(col).Text;

            DlkAssert.AssertEqual("VerifyDataPerRow", ExpectedValue, actualData);
        }

        [Keyword("SelectCheckBox", new String[] { "1|text|Check Box Number (1,2,3,..)|1",
                                                    "2|text|Selected|TRUE"})]
        public override void SelectCheckBox(String CheckBoxNum, String isChecked)
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
                    DlkLogger.LogError(new Exception("SelectCheckBox() failed. Index is greater than the number of result items in the result list."));
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
                DlkLogger.LogError(ex);
            }
        }

        [Keyword("SelectRadioButton", new String[]{"1|text|Radio Button Number (1,2,3,..)|1"})]
        public void SelectRadioButton(String RadioButtonIndex)
        {

            try
            {
                bool actualResult = false;
                int iIndex = Int32.Parse(RadioButtonIndex) - 1;

                while (!this.Exists())
                {
                    Thread.Sleep(-1);
                }

                mapRdoButtons();                

                if (iIndex >= rdoElems.Count)
                {
                    DlkLogger.LogError(new Exception("SelectRadioButton() failed. Index is greater than the number of result items in the result list."));
                }
                else
                {
                    DlkRadioButton rdoSelected = rdoElems.ElementAt(iIndex).Value;
                    rdoSelected.Select("TRUE");
                    actualResult = rdoSelected.mElement.Selected;
                    DlkLogger.LogInfo("Successfully executed SelectRadioButton().");
                }
                
                DlkAssert.AssertEqual("SelectRadioButton", true, actualResult);

            }
            catch (ArgumentNullException ex)
            {
                DlkLogger.LogError(ex);
            }
            
             
        }

        private void verifyModalWindowExist()
        {
            while (threadStat)
            {

                if (this.Exists())
                {
                    threadStat = false;
                    mapRdoButtons();
                }


            }
        }

        private void mapCheckBox()
        {
            int elemInd = 1;
            IList<IWebElement> lstElem = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchValues[0] + "//input"));
            chkElems = new Dictionary<string, DlkCheckBox>();

            int lstElemCount = lstElem.Count;
            int indx = 0;
            foreach (IWebElement elem in lstElem)
            {


                var inputType = elem.GetAttribute("type");

                if (inputType.Equals("checkbox"))
                {
                    chkElems.Add(elemInd.ToString(), new DlkCheckBox("chkBox" + elemInd, elem));
                    elemInd++;
                }

                indx++;

            }

        }

        private void mapRdoButtons()
        {
            int elemInd = 1;
            IList<IWebElement> lstElem = DlkEnvironment.AutoDriver.FindElements(By.TagName("input"));
            rdoElems = new Dictionary<string, DlkRadioButton>();

            int lstElemCount = lstElem.Count;
            int indx = 0;
            foreach (IWebElement elem in lstElem)
            {              
                var inputType = elem.GetAttribute("type");
                var inputName = elem.GetAttribute("name");

                var getType = elem.GetType();
                if (inputType.Equals("radio"))
                {
                    rdoElems.Add(elemInd.ToString(), new DlkRadioButton("rdoButton" + elemInd, elem));
                    elemInd++;
                }

                indx++;

            }

        }


        private void mapHeaders()
        {
            int elemInd = 1;
            String strSearch = "//div[@id='tabAddDocSearch']//table[@id='docSearchResults']//thead//tr//th";
            if(mControlName.Equals("LeadDetails_AddEditPrimaryNIGPCodeModalWindow_SearchResults_Table"))
            {
                strSearch = "//div[@id='nigpCodesSearchResultsTable_wrapper']//table//tr/th";
            }
            if (mControlName.Equals("Opportunities_Result"))
            {
                strSearch = "//tbody/tr/td[contains(@class,'resultsColumnHeader')]";
            }
            if (mControlName.Equals("AddNewSubContractor_SearchExistingSubContractors_SearchTable"))
            {
                strSearch = "//table[@id='pscDatatableSubContractorSearchResultsList']//th";
            }
            IList<IWebElement> lstElem = DlkEnvironment.AutoDriver.FindElements(By.XPath(strSearch));

            if (lstElem.Count == 0)
            {
                strSearch = "//div[@class='dataTables_scrollHead']//table//th";
                lstElem = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchValues[0] + strSearch));
            }
            mHeaders = lstElem;
            headerElems = new Dictionary<string, DlkButton>();

            foreach (IWebElement header in lstElem)
            {
                //IWebElement header = DlkEnvironment.AutoDriver.FindElement(By.XPath("/div"));
                headerElems.Add(elemInd.ToString(), new DlkButton("header"+elemInd, header));
                elemInd++;
            }
        }

        private void mapData()
        {
            int elemInd = 1;
            String strSearch = "//tbody[@id='searchResultsBody']//tr";
            if (mControlName.Equals("LeadDetails_AddEditPrimaryNIGPCodeModalWindow_SearchResults_Table"))
            {
                strSearch = "//table[@id='nigpCodesSearchResultsTable']/tbody/tr";
            }
            if (mControlName.Equals("Opportunities_Result"))
            {
                strSearch = "//tbody/tr[contains(@class,'resultsDataRow')]";
            }
            if (mControlName.Equals("AddNewSubContractor_SearchExistingSubContractors_SearchTable"))
            {
                strSearch = "//table[@id='pscDatatableSubContractorSearchResultsList']//tbody/tr";
            }
            IList<IWebElement> lstElem = DlkEnvironment.AutoDriver.FindElements(By.XPath(strSearch));

            if (lstElem.Count == 0)
            {
                strSearch = "//div[@class='dataTables_scrollBody']//tbody/tr";
                lstElem = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchValues[0] + strSearch));
            }

            mRows = lstElem;
            dataElems = new Dictionary<string,IList<IWebElement>>();

            foreach (IWebElement row in lstElem)
            {
                IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                dataElems.Add(elemInd.ToString(), cells);
                elemInd++;
            }

        }

        private void writeOutput(int i)
        {
            try
            {
                DlkVariable.SetVariable(mOutputVar, Convert.ToString(i + 1));                
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("Exception:" + e.Message);
                throw e;
            }
        }

        private void writeOutput(String str)
        {
            try
            {
                DlkVariable.SetVariable(mOutputVar, str);                
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("Exception:" + e.Message);
                throw e;
            }
        }

        private bool getRow(String sColumnHeader, String sValue, String sVariableName)
        {

            mOutputVar = sVariableName;

            String methodName = new StackFrame(1, true).GetMethod().Name;

            int col = 0;

            for (int i = 0; i < headerElems.Count; i++)
            {
                if (headerElems.ElementAt(i).Value.mElement.Text.Trim().Equals(sColumnHeader))
                {
                    col = i;
                    break;
                }
            }

            for (int i = 0; i < dataElems.Count; i++)
            {
                switch (methodName.ToLower())
                {
                    case "gettablerowwithcolumnvalue":
                        if (dataElems.ElementAt(i).Value.ElementAt(col).Text.Equals(sValue))
                        {
                            writeOutput(i);
                            return true;
                        }
                        break;
                    case "gettablerowwithpartialcolumnvalue":
                        if (dataElems.ElementAt(i).Value.ElementAt(col).Text.Contains(sValue))
                        {
                            writeOutput(i);
                            return true;
                        }
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        [Keyword("GetTableRowWithColumnValue", new String[] {"1|text|Column Header|Line*", 
                                                            "2|text|Value|1",
                                                            "3|text|VariableName|myRow"})]
        public virtual void GetTableRowWithColumnValue(String ColumnHeader, String Value, String VariableName)
        {
            Initialize();
            bool blnFound = false;

            while (!this.Exists())
            {
                Thread.Sleep(-1);
            }

            mapHeaders();
            mapData();

            blnFound = getRow(ColumnHeader, Value, VariableName);

            if (blnFound)
            {
                DlkLogger.LogInfo("Successfully executed GetTableRowWithColumnValue().");
            }
            else
            {
                DlkLogger.LogError(new Exception("GetTableRowWithColumnValue() failed. Value '" + Value + "' under Column '" + ColumnHeader + "' not found in table."));
            }

        }

        [Keyword("GetTableRowWithPartialColumnValue", new String[] {"1|text|Column Header|Line*", 
                                                            "2|text|Partial Value|1",
                                                            "3|text|VariableName|MyRow"})]
        public virtual void GetTableRowWithPartialColumnValue(String ColumnHeader, String Value, String VariableName)
        {
            bool blnFound = false;

            while (!this.Exists())
            {
                Thread.Sleep(-1);
            }

            mapHeaders();
            mapData();

            blnFound = getRow(ColumnHeader, Value, VariableName);

            if (blnFound)
            {
                DlkLogger.LogInfo("Successfully executed GetTableRowWithPartialColumnValue().");
            }
            else
            {
                DlkLogger.LogError(new Exception("GetTableRowWithPartialColumnValue() failed. Value '" + Value + "' under Column '" + ColumnHeader + "' not found in table."));
            }

        }

        [Keyword("GetTableCellValue", new String[] { "1|text|Row|O{Row}", "2|text|Header/Field|Document Title", "3|text|VariableName,MyValue"})]
        public void GetTableCellValue(String Row, String Header, String VariableName)
        {
            bool actual = false;

            mOutputVar = VariableName;
            
            while (!this.Exists())
                Thread.Sleep(-1);

            mapHeaders();
            mapData();

            int col = 0;

            for (int i = 0; i < headerElems.Count; i++)
            {
                if (headerElems.ElementAt(i).Value.mElement.Text.Trim().Equals(Header))
                {
                    col = i;
                    break;
                }
            }

            String actualData = dataElems.ElementAt(Int32.Parse(Row) - 1).Value.ElementAt(col).Text;
            if (!actualData.Equals(null))
                actual = true;

            writeOutput(actualData);

            DlkAssert.AssertEqual("GetTableCellValue", true, actual);
        }

        [Keyword("CheckRowIfReadOnly", new String[] {"1|text|Row|O{Row}",
                                                        "2|text|Expected|TRUE"})]
        public virtual void CheckRowIfReadOnly(String Row, String TrueOrFalse)
        {
            bool isReadOnly = Convert.ToBoolean(TrueOrFalse) ? false : true;
            int iRow = Convert.ToInt32(Row) - 1;

            while (!this.Exists())
            {
                Thread.Sleep(-1);
            }

            mapHeaders();
            mapData();

            String rowClass = mRows.ElementAt(iRow).GetAttribute("class");
            if (rowClass.Contains("inactive"))
                isReadOnly = true;
            else
                isReadOnly = false;

            DlkAssert.AssertEqual("CheckRowIfReadOnly", Convert.ToBoolean(TrueOrFalse), isReadOnly);           

        }

        [RetryKeyword("VerifyTableColumnHeaders", new String[] { "1|text|Expected header texts|Header1~Header2~Header3" })]
        public virtual void VerifyTableColumnHeaders(String ExpectedHeaders)
        {
            String expectedHeaders = ExpectedHeaders;
            bool actualResult = true;

            List<String> headers = new List<string>();

            this.PerformAction(() =>
                {
                    foreach (string str in expectedHeaders.Split('~'))
                    {
                        headers.Add(str);
                    }

                    mapHeaders();
                    mapData();

                    foreach (IWebElement caption in mHeaders)
                    {
                        if (!headers.Contains(caption.Text))
                            actualResult = false;
                    }

                    DlkAssert.AssertEqual("VerifyTableColumnHeaders ", true, actualResult);
                }, new String[] { "retry" });

        }

    }
}
