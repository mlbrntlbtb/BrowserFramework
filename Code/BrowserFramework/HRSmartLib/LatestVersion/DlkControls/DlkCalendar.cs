using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Calendar")]
    public class DlkCalendar : DlkBaseControl
    {
        #region Declarations

        private const string CALENDAR_HEADER = @".//div[@class='cal-row-fluid cal-row-head']";
        private const string CALENDAR_ROW  = @".//div[contains(@class,'cal-row-fluid') and not(contains(@class,'cal-row-head'))]";
        private const string CALENDAR_ROW_COLUMN = @"./div[contains(@class,'content') or @data-cal-row]";
        private const string CALENDAR_ROW_COLUMN_ITEMS = @"./div[contains(@class,item)]";
        private const string CALENDAR_EVENT_CLICKABLE_ITEMS = @".//form[contains(@id,'delete_event_week_form')]//a";

        #endregion

        #region Constructors

        public DlkCalendar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDateToday")]
        public void VerifyDateToday(String Date)
        {
            try
            {
                initialize();
                string expectedResult = Convert.ToDateTime(Date).ToShortDateString();
                IList<IWebElement> dateTodayHeader = mElement.FindElements(By.XPath(@".//div[@class='cal-row-fluid cal-row-head']/div[contains(@class,'cal-day-today')]//span[@data-cal-date]"));
                if (dateTodayHeader.Count > 0)
                {
                    DlkBaseControl dateToday = new DlkBaseControl("Date_Header", dateTodayHeader[0]);
                    string actualResult = Convert.ToDateTime(dateTodayHeader[0].GetAttribute("data-cal-date")).ToShortDateString();
                    DlkAssert.AssertEqual("Date_Today", expectedResult, actualResult);
                }
                else
                {
                    throw new Exception("Could not find date today header.");
                }

                DlkLogger.LogInfo("VerifyRowColumnItemContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDateToday() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRowColumnItemContains")]
        public void VerifyRowColumnItemContains(string Row, string Column, string ItemText, string ItemIndex)
        {
            try
            {
                initialize();
                int rowIndex = Convert.ToInt16(Row) - 1;
                int columnIndex = Convert.ToInt16(Column) - 1;
                int itemIndex = Convert.ToInt16(ItemIndex) - 1;
                string expectedResult = ItemText;

                IWebElement rowColumnItem = getRowColumnItemByIndex(rowIndex, columnIndex, itemIndex);
                DlkBaseControl item = new DlkBaseControl("Row_Column_Item", rowColumnItem);
                string actualResult = item.GetValue().Replace("\r\n","\n");

                DlkAssert.AssertEqual("VerifyRowColumnItemContains", expectedResult, actualResult, true);
                DlkLogger.LogInfo("VerifyRowColumnItemContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowColumnItemContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyEventClickableItem")]
        public void VerifyEventClickableItem(string Row, string Column, string ItemTitle, string ItemIndex, string TrueOrFalse)
        {
            try
            {
                initialize();
                int rowIndex = Convert.ToInt16(Row) - 1;
                int columnIndex = Convert.ToInt16(Column) - 1;
                int itemIndex = Convert.ToInt16(ItemIndex) - 1;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;

                IWebElement rowColumnItem = getRowColumnItemByIndex(rowIndex, columnIndex, itemIndex);
                IList<IWebElement> eventClickableItems = rowColumnItem.FindElements(By.XPath(CALENDAR_EVENT_CLICKABLE_ITEMS));

                foreach (IWebElement clickableItem in eventClickableItems)
                {
                    String titleAttribute = clickableItem.GetAttribute("title");
                    if (titleAttribute != null &&
                        titleAttribute.Equals(ItemTitle))
                    {
                        actualResult = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyEventClickableItem", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyEventClickableItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyEventClickableItem() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickRowColumnItem")]
        public void ClickRowColumnItem(string Row, string Column, string ItemIndex, string ClickableIndex)
        {
            try
            {
                initialize();
                int rowIndex = Convert.ToInt16(Row) - 1;
                int columnIndex = Convert.ToInt16(Column) - 1;
                int itemIndex = Convert.ToInt16(ItemIndex) - 1;
                int clickableIndex = Convert.ToInt16(ClickableIndex) - 1;

                IWebElement rowColumnItem = getRowColumnItemByIndex(rowIndex, columnIndex, itemIndex);
                DlkCommon.DlkCommonFunction.ClickElementByIndex(rowColumnItem.FindElements(By.XPath(".//*")), clickableIndex);
                DlkLogger.LogInfo("ClickRowColumnItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickRowColumnItem() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickRowColumnItem")]
        public void DoubleClickRowColumnItem(string Row, string Column, string ItemIndex, string ClickableIndex)
        {
            try
            {
                initialize();
                int rowIndex = Convert.ToInt16(Row) - 1;
                int columnIndex = Convert.ToInt16(Column) - 1;
                int itemIndex = Convert.ToInt16(ItemIndex) - 1;
                int clickableIndex = Convert.ToInt16(ClickableIndex) - 1;

                IWebElement rowColumnItem = getRowColumnItemByIndex(rowIndex, columnIndex, itemIndex);
                DlkCommon.DlkCommonFunction.ClickElementByIndex(rowColumnItem.FindElements(By.XPath(".//*")), clickableIndex, true);
                DlkLogger.LogInfo("ClickRowColumnItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickRowColumnItem() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyHighlightedText")]
        public void VerifyHighlightedText(string Text)
        {
            try
            {
                initialize();
                List<IWebElement> highlightedCells = mElement.FindElements(By.XPath(".//td//span[@class='selected']")).ToList();
                bool highlighted = highlightedCells.Any(highlightedCell => highlightedCell.Text.Trim() == Text) ? true : false;

                DlkAssert.AssertEqual("VerifyHighlightedText", true, highlighted);
                DlkLogger.LogInfo("VerifyHighlightedText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHighlightedText() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private IWebElement getRowColumnItemByIndex (int rowIndex, int columnIndex, int itemIndex)
        {
            IList<IWebElement> rows = mElement.FindElements(By.XPath(CALENDAR_ROW));
            IWebElement rowColumn = rows[rowIndex].FindElements(By.XPath(CALENDAR_ROW_COLUMN))[columnIndex];
            IList<IWebElement> rowColumnItems = rowColumn.FindElements(By.XPath(CALENDAR_ROW_COLUMN_ITEMS));
            return rowColumnItems[itemIndex];
        }

        #endregion
    }
}
