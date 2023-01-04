using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Table")]
    public class DlkTable : DlkBaseControl
    {
        #region Constructors
        public DlkTable(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTable(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTable(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkTable(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion
        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        /// Verifies if Table exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
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
        [Keyword("VerifyCellValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellValue(String RowNumber, String ColumnNumber, String ExpectedCellValue)
        {
            try
            {
                Initialize();
                int rowIndex = Convert.ToInt32(RowNumber) - 1,
                    columnIndex = Convert.ToInt32(ColumnNumber) - 1;
                IReadOnlyCollection<IWebElement> tableRow = mElement.FindElements(By.XPath("./tbody/tr[@index='" + rowIndex + "']/td"));
                string cellValue = tableRow.ElementAt(columnIndex).Text;
                DlkAssert.AssertEqual("VerifyCellValue() : " + mControlName, cellValue, ExpectedCellValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        //Currently supports table in Settings_ManageUsers
        [Keyword("SelectRowAction", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectRowAction(String RowNumber, String Action)
        {
            try
            {
                Initialize();
                int rowIndex = Convert.ToInt32(RowNumber) - 1;
                int firstNRecordsInUpperScroll = 3; 
                if (rowIndex <= firstNRecordsInUpperScroll)
                {
                    // Scroll up then scroll to the right because scrollintoviewusingjs lacks
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.SendKeys(OpenQA.Selenium.Keys.PageUp).Build().Perform();
                    mAction.SendKeys(OpenQA.Selenium.Keys.Right).Build().Perform();
                    System.Threading.Thread.Sleep(2000);
                }
                IWebElement tableRow = mElement.FindElements(By.XPath("./tbody/tr[@index='" + rowIndex + "']")).FirstOrDefault();
                IWebElement rowAction = tableRow.FindElements(By.XPath("./td/button")).FirstOrDefault();
                // Click row action to open the popup
                rowAction.Click();
                // Find the specific action inside popover div the click
                mElement.FindElements(By.XPath("//div[contains(@class,'MuiPopover-paper')]//li[text()='" + Action + "']"))
                    .FirstOrDefault()
                    .Click();
                DlkLogger.LogInfo("SelectRowAction() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectRowAction() failed : " + e.Message, e);
            }
        }

        //SelectRowsPerPage

        #endregion
    }
}

