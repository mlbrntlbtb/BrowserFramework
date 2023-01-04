using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Grid")]
    public class DlkGrid : DlkBaseControl
    {

        #region PRIVATE VARIABLES
        
        private Boolean IsInit = false;
        private ReadOnlyCollection<IWebElement> mColumnHeaders;

        #endregion

        #region CONSTRUCTORS

        public DlkGrid(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                FindColumnHeaders();
                IsInit = true;
            }
        }

        #endregion

        #region PRIVATE METHODS

        private string GetCellValue(String RowName, string ColumnName)
        {
            Initialize();
            string ret = "";
            IWebElement targetRow = null;
            try
            {
                targetRow = mElement.FindElement(By.XPath("./descendant::th[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='" + RowName.ToLower() + "']/ancestor::tr[1]"));
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return null;
            }

            int targetColumnNumber = GetColumnNumberFromName(ColumnName);

            IWebElement firstCol = targetRow.FindElement(By.XPath("./descendant::td[1]"));
            //firstCol.Click();
            new DlkBaseControl("FirstCol", firstCol).MouseOver();
            IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/p[contains(@class,'ng-binding')]"));
            ret = new DlkBaseControl("Target", targetCell).GetValue();

            return ret;
        }

        private void FindColumnHeaders()
        {
            List<IWebElement> collection = new List<IWebElement>();
            //foreach (IWebElement elm in mElement.FindElements(By.XPath("./preceding::thead/descendant::th/div")))
            foreach (IWebElement elm in mElement.FindElements(By.XPath("./preceding::div[contains(@class,'table-header')]//table//thead//th")))
            {
                if (!string.IsNullOrEmpty(elm.Text))
                {
                    collection.Add(elm);
                }
            }
            mColumnHeaders = new ReadOnlyCollection<IWebElement>(collection);
        }

        private int GetColumnNumberFromName(string ColumnName)
        {

            int ret = -1;
            bool bFound = false;
            int colNum = 0;
            int gridColumnCount = GetColumnCount();
            for (int i = 0; i < mColumnHeaders.Count; i++)
            {
                if (new DlkBaseControl("Header", mColumnHeaders[i]).GetValue().ToLower() == ColumnName.ToLower())
                {
                    colNum = i;
                    bFound = true;
                    break;
                }
            }

            if (bFound)
            {
                ret = gridColumnCount - (mColumnHeaders.Count - colNum);
            }

            return ret;
            //old code
            //int ret = -1;
            //int gridColumnCount = GetColumnCount();
            //int startIndex = mColumnHeaders.Count - gridColumnCount;
            //for (int i = startIndex; i < mColumnHeaders.Count; i++)
            //{
            //    if (new DlkBaseControl("Header", mColumnHeaders[i]).GetValue().ToLower() == ColumnName.ToLower())
            //    {
            //        ret = i + 1;
            //        break;
            //    }
            //}
            //return ret - startIndex;
        }

        private int GetColumnCount()
        {
            return mElement.FindElements(By.XPath("./tr[1]/descendant::*[contains(@class,'ng-binding')]")).Count;
        }

        private string GetGridDisplayState()
        {
            Initialize();
            var collapseElements = mElement.FindElements(By.XPath(".//a[contains(@class,'chevron-down')]"));
            var expandElements = mElement.FindElements(By.XPath(".//a[contains(@class,'chevron-up')]"));

            if (collapseElements.Count > 0 && collapseElements.First().Displayed) //if grid has the collapse button it means it is expanded
            {
                return "expanded";
            }
            else if (expandElements.Count > 0 && expandElements.First().Displayed) //if grid has the expand button it means it is collapse
            {
                return "collapsed";
            }
            else
            {
                throw new Exception("Cannot find the control.");
            }
        }

        #endregion

        #region KEYWORDS

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

        [Keyword("Expand", new String[] { "1|text|Expected Value|TRUE" })]
        public void Expand()
        {
            try
            {
                Initialize();
                var collapseElements = mElement.FindElements(By.XPath(".//a[contains(@class,'chevron-down')]"));
                var expandElements = mElement.FindElements(By.XPath(".//a[contains(@class,'chevron-up')]"));

                if (expandElements.Count > 0 && expandElements.First().Displayed)
                {
                    IWebElement chevronDown = expandElements.First();
                    chevronDown.Click();
                    DlkLogger.LogInfo("Expand() successfully executed.");
                }
                else if (collapseElements.Count > 0)
                {
                    DlkLogger.LogInfo("Grid is already expanded.");
                }
                else
                {
                    throw new Exception("Cannot find expand button.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Expand() failed : " + e.Message, e);
            }
        }

        [Keyword("Collapse", new String[] { "1|text|Expected Value|TRUE" })]
        public void Collapse()
        {
            try
            {
                Initialize();
                var collapseElements = mElement.FindElements(By.XPath(".//a[contains(@class,'chevron-down')]"));
                var expandElements = mElement.FindElements(By.XPath(".//a[contains(@class,'chevron-up')]"));

                if (collapseElements.Count > 0 && collapseElements.First().Displayed)
                {
                    IWebElement chevronUp = collapseElements.First();
                    chevronUp.Click();
                    DlkLogger.LogInfo("Collapse() successfully executed.");
                }
                else if (expandElements.Count > 0)
                {
                    DlkLogger.LogInfo("Grid is already collapsed.");
                }
                else
                {
                    throw new Exception("Cannot find collapse button.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Collapse() failed : " + e.Message, e);
            }
        }

        [Keyword("GetState", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetState(String VariableName)
        {
            try
            {
                String ActualState = GetGridDisplayState();
                DlkFunctionHandler.AssignToVariable(VariableName, ActualState);
                DlkLogger.LogInfo("GetState() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyState() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyState", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyState(String ExpectedState)
        {
            try
            {
                String ActualState = GetGridDisplayState();
                DlkAssert.AssertEqual("VerifyState()", ExpectedState.ToLower(), ActualState);
                DlkLogger.LogInfo("VerifyState() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyState() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValue(String RowName, String ColumnName, String Value)
        {
            try
            {
                Initialize();
                // stramgely displayed row text is not in same case as in HTML code
                IWebElement targetRow = mElement.FindElement(By.XPath(
                    "./descendant::th[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                    + RowName.ToLower() + "']/ancestor::tr[1]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");

                }
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/p[contains(@class,'ng-binding')]"));

                DlkAssert.AssertEqual("VerifyCellValue()", Value.ToLower(), new DlkBaseControl("Target", targetCell).GetValue().ToLower());
                DlkLogger.LogInfo("VerifyCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowHeader")]
        public void VerifyRowHeader(String RowIndex, String ExpectedResult)
        {
            try
            {
                Initialize();

                IWebElement tableRowHeader = mElement.FindElement(By.XPath(string.Format("../../../div[contains(@class,'ng-scope')]/table[contains(@class,'ng-scope')]/descendant::th[contains(@class,'ng-binding')][{0}]", RowIndex)));

                var tableRowName = tableRowHeader.Text;
                if (tableRowName != null)
                {
                    DlkAssert.AssertEqual("VerifyRowHeader()", ExpectedResult.ToLower(), tableRowName.ToLower());
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

        [Keyword("GetCellValue", new String[] { "1|text|Row|1",
                                                "2|text|VariableName|RowCount"})]
        public void GetCellValue(String RowName, String ColumnName, String VariableName)
        {
            try
            {

                Initialize();
                string cellValue = GetCellValue(RowName, ColumnName);
                DlkVariable.SetVariable(VariableName, cellValue);
                DlkLogger.LogInfo("GetCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetCellValue() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyExactCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactCellValue(String RowName, String ColumnName, String Value)
        {
            try
            {
                Initialize();
                // stramgely displayed row text is not in same case as in HTML code
                IWebElement targetRow = mElement.FindElement(By.XPath(
                    "./descendant::th[translate(text(), 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz')='"
                    + RowName.ToLower() + "']/ancestor::tr[1]"));
                int targetColumnNumber = GetColumnNumberFromName(ColumnName);

                // check
                if (targetColumnNumber == -1)
                {
                    throw new Exception("Column '" + ColumnName + "' was not found.");

                }
                IWebElement targetCell = targetRow.FindElement(By.XPath("./descendant::td[" + targetColumnNumber + "]/p[contains(@class,'ng-binding')]"));

                DlkAssert.AssertEqual("VerifyExactCellValue()", Value, new DlkBaseControl("Target", targetCell).GetValue());
                DlkLogger.LogInfo("VerifyExactCellValue() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactCellValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactRowHeader")]
        public void VerifyExactRowHeader(String RowIndex, String ExpectedResult)
        {
            try
            {
                Initialize();

                IWebElement tableRowHeader = mElement.FindElement(By.XPath(string.Format("../../../div[contains(@class,'ng-scope')]/table[contains(@class,'ng-scope')]/descendant::th[contains(@class,'ng-binding')][{0}]", RowIndex)));

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
