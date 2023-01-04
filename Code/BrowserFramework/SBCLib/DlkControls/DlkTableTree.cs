using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCLib.DlkControls
{
    [ControlType("TableTree")]
    class DlkTableTree : DlkBaseControl
    {
        #region Constructors
        public DlkTableTree(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkTableTree(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTableTree(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        private List<IWebElement> mColHeaders;
        private List<IWebElement> mRows;

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            InitializeColHeader();
            InitializeRows();
        }

        [Keyword("VerifyExists")]
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

        [Keyword("Expand")]
        public void Expand(String IndexPath)
        {
            try
            {
                Initialize();
                var IndexList = IndexPath.Split('~');

                List<IWebElement> Rows = mRows;

                foreach (var index in IndexList)
                {
                    if (Int32.TryParse(index, out int i) && i > mRows.Count)
                        throw new Exception($"Invalid Index [{index}]");

                    IWebElement Row = mRows.ElementAt(i - 1);

                    bool isLast = IndexList.Last() == index;

                    ExpandCollapseRow(Row, true);

                    if (!isLast) Rows = InitializeRows(Row);
                }

                DlkLogger.LogInfo("Expand() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Expand() failed : " + e.Message, e);
            }
        }

        [Keyword("Collapse")]
        public void Collapse(String IndexPath)
        {
            try
            {
                Initialize();
                var IndexList = IndexPath.Split('~');

                List<IWebElement> Rows = mRows;

                foreach (var index in IndexList)
                {
                    if (Int32.TryParse(index, out int i) && i > mRows.Count)
                        throw new Exception($"Invalid Index [{index}]");

                    IWebElement Row = mRows.ElementAt(i - 1);

                    bool isLast = IndexList.Last() == index;

                    ExpandCollapseRow(Row, isLast ? false : true);

                    if (!isLast) Rows = InitializeRows(Row);
                }
                

                DlkLogger.LogInfo("Collapse() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Collapse() failed : " + e.Message, e);
            }
        }

        


        #region Private Methods
        private int GetIndexOfColumnName(String ColName)
        {
            var col = mColHeaders.Where(c => c.Text.Trim() == ColName.Trim()).FirstOrDefault();
            if (col == null) throw new Exception($"Cannot find column header [{ColName}]");
            return mColHeaders.IndexOf(col);
        }

        private void ExpandCollapseRow(IWebElement Row, bool ExpandCollapseState = true)
        {
            var icon = Row.FindElements(By.XPath(".//span[contains(@class, 'divIcon')]")).FirstOrDefault();
            if (icon == null) throw new Exception("Cannot find icon to expand/collapse.");

            bool isExpanded = icon.GetAttribute("class").Contains("close");

            if ((!isExpanded && ExpandCollapseState) || (isExpanded && !ExpandCollapseState)) icon.Click();
        }

        private void InitializeColHeader()
        {
            //TableTree 1 //*[@id='projectNotes']
            //TableTree 2 //*[@id='registeredUserList']/div[2]/div
            var columns = mElement.FindElements(By.XPath("./table/thead/tr/th")).ToList();
            if (columns.Count < 1) throw new Exception("No column header found.");
            mColHeaders = columns.Where(c => !string.IsNullOrEmpty(c.Text.Trim())).ToList();
        }

        private List<IWebElement> InitializeRows(IWebElement element = null)
        {
            var rows = element == null 
                ? mElement.FindElements(By.XPath("./table/tbody/tr[not(contains(@class, 'header'))]/parent::tbody")).ToList() 
                : element.FindElements(By.XPath(".//table[contains(@class, 'taskGrid')]/tbody")).ToList();

            if (rows.Count < 1) throw new Exception("No rows found.");
            mRows = rows;
            return rows;
        }
        #endregion

    }
}
