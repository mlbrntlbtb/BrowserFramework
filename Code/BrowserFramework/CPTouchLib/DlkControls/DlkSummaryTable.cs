#define NATIVE_MAPPING

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("SummaryTable")]
    public class DlkSummaryTable : DlkMobilePicker
    {
#if NATIVE_MAPPING
        private const string STR_XPATH_HEADERS = "//*[string-length(@resource-id)!=0][string-length(@text)!=0]";
#else
        private const string STR_XPATH_HEADERS = "//*[contains(@class,'detailHeaderSummary')]//span";
#endif
#if NATIVE_MAPPING
        private const string STR_XPATH_ROWS = "//*[contains(@resource-id,'ext-simplelistitem')]";
#else
        private const string STR_XPATH_ROWS = ".//*[contains(@id,'ext-simplelistitem')]";
#endif

        public DlkSummaryTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSummaryTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSummaryTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private List<SummaryListItem> mRows = new List<SummaryListItem>();

        [Keyword("SelectRowWithColumnValue", new String[] { "1|text|Value|SampleValue" })]
        public void SelectRowWithColumnValue(string ColumnName, string Value, string ExactMatch, string OneBasedIndex)
        {
            try
            {
                Initialize();
                bool exact;
                if (!bool.TryParse(ExactMatch, out exact))
                {
                    exact = true;
                }
                var matches = new List<SummaryListItem>();
                if (exact)
                {
                    for (int i = 0; i < mRows.Count; i++)
                    {
                        if (mRows[i].Values.ContainsKey(ColumnName))
                        {
                            if (mRows[i].Values[ColumnName] == Value)
                            {
                                matches.Add(mRows[i]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < mRows.Count; i++)
                    {
                        if (mRows[i].Values.ContainsKey(ColumnName))
                        {
                            if (mRows[i].Values[ColumnName].Contains(Value))
                            {
                                matches.Add(mRows[i]);
                            }
                        }
                    }
                }
                if (!matches.Any())
                {
                    throw new Exception("Cannot locate target row");
                }
                int index;
                if (!int.TryParse(OneBasedIndex, out index) || index > matches.Count || index < 1)
                {
                    index = 1;
                }
                matches[index - 1].ScrollIntoView();
                matches[index - 1].Tap();
                DlkLogger.LogInfo("SelectRowWithColumnValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectRowWithMultipleColumnValues", new String[] { "1|text|Value|SampleValue" })]
        public void SelectRowWithMultipleColumnValues(string TildeBoundCols, string TildeSeparatedVal, string ExactMatch, string OneBasedIndex)
        {
            try
            {
                Initialize();
                bool exact;
                if (!bool.TryParse(ExactMatch, out exact))
                {
                    exact = true;
                }
                var matches = new List<SummaryListItem>();
                var colNames = TildeBoundCols.Split('~');
                var colVals = TildeSeparatedVal.Split('~');
                if (colNames.Count() != colVals.Count())
                {
                    throw new Exception("List size mismatch between column names and column values");
                }
                if (exact)
                {
                    for (int i = 0; i < mRows.Count; i++)
                    {
                        var isMatch = false;
                        for (int j = 0; j < colNames.Count(); i++)
                        {
                            if (mRows[i].Values[colNames[j]] != colVals[j])
                            {
                                isMatch = false;
                                break;
                            }
                            isMatch = true;
                        }
                        if (isMatch)
                        {
                            matches.Add(mRows[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < mRows.Count; i++)
                    {
                        var isMatch = false;
                        for (int j = 0; j < colNames.Count(); i++)
                        {
                            if (!mRows[i].Values[colNames[j]].Contains(colVals[j]))
                            {
                                isMatch = false;
                                break;
                            }
                            isMatch = true;
                        }
                        if (isMatch)
                        {
                            matches.Add(mRows[i]);
                        }
                    }
                }
                if (!matches.Any())
                {
                    throw new Exception("Cannot locate target row");
                }
                int index;
                if (!int.TryParse(OneBasedIndex, out index) || index > matches.Count || index < 1)
                {
                    index = 1;
                }
                matches[index - 1].Tap();
                DlkLogger.LogInfo("SelectRowWithMultipleColumnValues() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectRowWithMultipleColumnValues() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectRowByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SelectRowByIndex(string OneBasedIndex)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mRows.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
                mRows[index - 1].Tap();
                DlkLogger.LogInfo("SelectRowByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectRowByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnValue", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyColumnValue(string RowIndex, string ColumnName, string ValueToVerify)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(RowIndex, out index))
                {
                    throw new Exception("Invalid index: '" + RowIndex + "'");
                }
                if (index < 1 || index > mRows.Count)
                {
                    throw new Exception("Index out of item range: '" + RowIndex + "'");
                }
                if (!mRows[index - 1].Values.ContainsKey(ColumnName))
                {
                    throw new Exception("Cannot locate column '" + ColumnName + "' value for target row");
                }
                DlkAssert.AssertEqual("VerifyColumnValue", ValueToVerify, mRows[index - 1].Values[ColumnName]);
                DlkLogger.LogInfo("VerifyColumnValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnValue() failed : " + e.Message, e);
            }
        }

        //[Keyword("GetRowWithMultipleColumnValues", new String[] { "1|text|Value|SampleValue" })]
        //public void GetRowWithMultipleColumnValues(string TildeSeparatedColumns, string TildeSeparatedValues, string InstanceToSelect, string Variable)
        //{
        //    try
        //    {
        //        DlkLogger.LogInfo("GetRowWithMultipleColumnValues() successfully executed.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("GetRowWithMultipleColumnValues() failed : " + e.Message, e);
        //    }
        //}

        [Keyword("GetRowWithColumnValue", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowWithColumnValue(string ColumnName, string Value, string ExactMatch, string OneBasedIndex, string Variable)
        {
            try
            {
                Initialize();
                bool exact;
                if (!bool.TryParse(ExactMatch, out exact))
                {
                    exact = true;
                }
                var matches = new List<SummaryListItem>();
                if (exact)
                {
                    for (int i = 0; i < mRows.Count; i++)
                    {
                        if (mRows[i].Values.ContainsKey(ColumnName))
                        {
                            if (mRows[i].Values[ColumnName] == Value)
                            {
                                matches.Add(mRows[i]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < mRows.Count; i++)
                    {
                        if (mRows[i].Values.ContainsKey(ColumnName))
                        {
                            if (mRows[i].Values[ColumnName].Contains(Value))
                            {
                                matches.Add(mRows[i]);
                            }
                        }
                    }
                }
                if (!matches.Any())
                {
                    throw new Exception("Cannot locate target row");
                }
                int index;
                if (!int.TryParse(OneBasedIndex, out index) || index > matches.Count || index < 1)
                {
                    index = 1;
                }
                DlkVariable.SetVariable(Variable, (mRows.IndexOf(matches[index - 1]) + 1).ToString());
                DlkLogger.LogInfo("GetRowWithColumnValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowWithColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetColumnValue", new String[] { "1|text|Value|SampleValue" })]
        public void GetColumnValue(string RowIndex, string ColumnName, string Variable)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(RowIndex, out index))
                {
                    throw new Exception("Invalid index: '" + RowIndex + "'");
                }
                if (index < 1 || index > mRows.Count)
                {
                    throw new Exception("Index out of item range: '" + RowIndex + "'");
                }
                if (!mRows[index - 1].Values.ContainsKey(ColumnName))
                {
                    throw new Exception("Cannot locate column '" + ColumnName + "' value for target row");
                }
                DlkVariable.SetVariable(Variable, mRows[index - 1].Values[ColumnName]);
                DlkLogger.LogInfo("GetColumnValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetColumnValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowCount(string Variable)
        {
            try
            {
                Initialize();
                DlkVariable.SetVariable(Variable, mRows.Count.ToString());
                DlkLogger.LogInfo("GetRowCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowCount", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyRowCount(string ExpectedCount)
        {
            try
            {
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                Initialize();
                DlkAssert.AssertEqual("VerifyRowCount", expected, mRows.Count);
                DlkLogger.LogInfo("VerifyRowCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowChargeTotal", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyRowChargeTotal(string RowIndex, string ChargeCode, string ExpectedValue)
        {
            try
            {
                DlkLogger.LogInfo("VerifyRowChargeTotal() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowChargeTotal() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRowChargeTotal", new String[] { "1|text|Value|SampleValue" })]
        public void GetRowChargeTotal(string RowIndex, string ChargeCode, string Variable)
        {
            try
            {
                DlkLogger.LogInfo("GetRowChargeTotal() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRowChargeTotal() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(string TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        private void Initialize()
        {
#if NATIVE_MAPPING
            DlkEnvironment.SetContext("NATIVE");
#endif
            FindElement();
            var hdrs = GetColumnHeaders();
            GetRows(hdrs);
        }

        private void GetRows(List<string> headers)
        {
              mElement.FindElements(By.XPath(STR_XPATH_ROWS)).ToList()
                .ForEach(x => mRows.Add(new SummaryListItem(new DlkMobileControl("itm", x), headers)));
            mRows = mRows.FindAll(x => x.Values.Any());
        }

        private List<string> GetColumnHeaders()
        {
            List<string> ret = new List<string>();
            //return mElement.FindElements(By.XPath(mSearchValues.First() + STR_XPATH_HEADERS)).ToList().Select(x => x.Text).ToList();
            foreach (var itm in mElement.FindElements(By.XPath(STR_XPATH_HEADERS)))
            {
                if (!string.IsNullOrEmpty(itm.Text.Trim()))
                {
                    ret.Add(itm.Text);
                }
            }
            return ret;
        }
    }

    public class SummaryListItem
    {
#if NATIVE_MAPPING
        private const string STR_XPATH_VALID_HEADERS = "//*[string-length(@text)!=0]";
#else
        private const string STR_XPATH_VALID_HEADERS = ".//div[contains(@class,'summary')]";
#endif
        private DlkMobileControl mControl;
        private List<string> mHeaders;
        public SummaryListItem(DlkMobileControl control, List<string> headers)
        {
            mControl = control;
            mHeaders = headers;
            PopulateValues();
        }
        public Dictionary<string, string> Values { get; private set; } = new Dictionary<string, string>();

        private void PopulateValues()
        {
            var valContainers = mControl.mElement.FindElements(By.XPath(STR_XPATH_VALID_HEADERS));
            if (valContainers.Count != mHeaders.Count)
            {
                return;
            }
            for (int i=0; i < valContainers.Count; i++)
            {
                Values.Add(mHeaders[i], valContainers[i].Text);
            }
        }

        public void Tap()
        {
            mControl.Tap();
        }

        public void ScrollIntoView()
        {
            mControl.ScollIntoViewUsingWebView();
        }
    }
}
