using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Widget")]
    public class DlkWidget : DlkBaseControl
    {
        #region Constructors
        public DlkWidget(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkWidget(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkWidget(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }
        private const string GRIDITEM_XPATH = ".//div[contains(@data-testid, 'Contents')]/div[contains(@class,'MuiGrid-item')]";
        private const string WIDGETROW_FAVCONTACTS_XPATH = ".//div[@data-testid='favoriteContactsList']/div[contains(@class,'MuiGrid-item')]";
        private const string WIDGETROW_RECENTLYLISTED_XPATH = ".//div[contains(@data-testid, 'List')]/div[contains(@class,'MuiGrid-item')]";

        #region Keywords
        /// <summary>
        /// Assigns row value to var
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="RowNumber"></param>

        [Keyword("AssignRowValueToVariable")]
        public void AssignRowValueToVariable(String VariableName, String RowNumber)
        {
            try
            {
                Initialize();
                string mValue = GetRowValueByRowNumber(RowNumber);
                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo("AssignRowValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue.TrimEnd() + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignRowValueToVariable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Assigns column val to var
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="RowNumber"></param>
        /// <param name="ItemName"></param>
        [Keyword("AssignColumnValueToVariable")]
        public void AssignColumnValueToVariable(String VariableName, String RowNumber, String ItemName)
        {
            try
            {
                Initialize();
                string actualText = GetColumnValueByRowNumberAndItemName(RowNumber, ItemName).TrimEnd();
                DlkVariable.SetVariable(VariableName, actualText);
                DlkLogger.LogInfo("AssignColumnValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + actualText + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignColumnValueToVariable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies if Widget exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
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

        /// <summary>
        ///  Verifies widget header.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyHeader(String ExpectedText)
        {
            try
            {
                Initialize();
                string actualResult = DlkString.RemoveCarriageReturn(mElement.FindElement(By.XPath(".//*[contains(@class,'widgetTitle')]")).Text.Trim());
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n");

                DlkAssert.AssertEqual("VerifyHeader() : " + mControlName, textToVerify, actualResult);
                DlkLogger.LogInfo("VerifyHeader() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeader() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies widget content.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyContent", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyContent(String ExpectedText)
        {
            try
            {
                Initialize();
                string actualResult = DlkString.RemoveCarriageReturn(mElement.FindElement(By.XPath(".//a/span")).Text.Trim());
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n");

                DlkAssert.AssertEqual("VerifyContent() : " + mControlName, textToVerify, actualResult);
                DlkLogger.LogInfo("VerifyContent() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyContent() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies widget content list. Currently works in listing categories widget
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyContentList", new String[] { "1|text|Expected contents|List1~List2~List3" })]
        public void VerifyContentList(String ExpectedContents)
        {
            try
            {
                Initialize();
                IList<IWebElement> items = mElement.FindElements(By.XPath(".//div[contains(@class,'dvListingCategoryItem')]"));
                IList<string> actualContents = new List<string>();

                foreach (IWebElement item in items)
                {
                    actualContents.Add(DlkString.RemoveCarriageReturn(item.Text.Trim()));
                }

                string actualDelimitedContents = string.Join("~", actualContents);
                DlkAssert.AssertEqual("VerifyContentList() : " + mControlName, ExpectedContents, actualDelimitedContents);
                DlkLogger.LogInfo("VerifyContentList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyContentList() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks listing item in a widget
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("ClickListingCategoryItem", new String[] { "1|text|Item|Item1" })]
        public void ClickListingCategoryItem(String Item)
        {
            try
            {
                Initialize();
                IWebElement item = mElement.FindElements(By.XPath(".//div[contains(@class,'dvListingCategoryItem')]//span[text()='" + Item + "']")).FirstOrDefault();
                var dlkMenuItem = new DlkBaseControl("Widget Item", item);
                dlkMenuItem.FocusUsingJavaScript();
                dlkMenuItem.MouseOver();
                dlkMenuItem.Click();
                DlkLogger.LogInfo("ClickListingItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickListingItem() failed : " + e.Message, e);
            }
        }
        // the following keywords will only support RecentlyListed widget as of April2020

        /// <summary>
        /// Verifies row value
        /// Supported widget: RecentlyListed
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyRowValue", new String[] { "1|text|Item|Item1" })]
        public void VerifyRowValue(String RowNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                string actualValue = GetRowValueByRowNumber(RowNumber);
                DlkAssert.AssertEqual("VerifyContentList() : " + mControlName, ExpectedValue, actualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyContentList() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Verifies text 
        /// Specifically created for FavoriteContacts
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyTextByRowNumber", new String[] { "1|text|Item|Item1" })]

        public void VerifyTextByRowNumber(String RowNumber, String ExpectedValue)
        {
            try
            {
                string actualValue = mElement.FindElements(By.XPath("./div[" + RowNumber + "]")).FirstOrDefault().Text.Trim();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, actualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Verifies text by row and item name.
        /// Supported widget: Top 10 results
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ItemName"></param>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyTextByRecordNumberAndItemName", new String[] { "1|text|Item|Item1" })]

        public void VerifyTextByRecordNumberAndItemName(String RowNumber, String ItemName, String ExpectedText)
        {
            try
            {
                Initialize();
                string actualText = GetColumnValueByRowNumberAndItemName(RowNumber, ItemName);
                DlkAssert.AssertEqual("VerifyTextByRowAndItemName()", ExpectedText, actualText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextByRowAndItemName() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Works for Favorite contacts for now
        /// </summary>
        /// <param name="ExpectedValue"></param>
        /// 
        [Keyword("VerifyEntryExists", new String[] { "1|text|Item|Item1" })]

        public void VerifyEntryExists(string EntryValue, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement actualValue = null;
                bool bExists = false;
                switch (GetWidgetType())
                {
                    case "favoriteContacts":
                        actualValue = mElement.FindElements(By.XPath(".//div[text()='" + EntryValue + "']")).FirstOrDefault();
                        break;
                    default:
                        throw new Exception("Widget type not supported");
                } 
                if (actualValue.Displayed)
                    bExists = true;
                DlkAssert.AssertEqual("VerifyEntryExists() : ", Convert.ToBoolean(TrueOrFalse), bExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyEntryExists() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Verifies column value
        /// Supported widget: RecentlyListed
        /// </summary>
        /// <param name="ColumnNumber"></param>
        /// <param name="ExpectedValue"></param>

        [Keyword("VerifyColumnValue", new String[] { "1|text|Item|Item1" })]
        public void VerifyColumnValue(String ColumnNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                IList<IWebElement> items = mElement.FindElements(By.XPath(GRIDITEM_XPATH + "//div[contains(@class,'MuiGrid-item')][" + ColumnNumber + "]"));
                IList<string> actualContents = new List<string>();

                foreach (IWebElement item in items)
                {
                    actualContents.Add(DlkString.RemoveCarriageReturn(item.Text.Trim()));
                }

                string actualDelimitedContents = string.Join("~", actualContents);
                DlkAssert.AssertEqual("VerifyColumnValue() : " + mControlName, ExpectedValue, actualDelimitedContents);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnValue() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Clicks item by row number
        /// Supported widget: RecentlyListed
        /// </summary>
        /// <param name="RowNumber"></param>
        [Keyword("ClickItemByRowNumber", new String[] { "1|text|Item|Item1" })]
        public void ClickItemByRowNumber(String RowNumber)
        {
            try
            {
                Initialize();
                IWebElement item = null;
                int rowNumber = Int32.Parse(RowNumber) - 1;
                switch (GetWidgetType())
                {
                    case "favoriteContacts":
                        item = mElement.FindElements(By.XPath("./div")).ToList()[rowNumber];
                        break;
                    case "dvTopResultsComponent":
                        item = mElement.FindElements(By.XPath(".//a")).ToList()[rowNumber];
                        break;
                    default:
                        item = mElement.FindElements(By.XPath(".//div[contains(@data-testid, 'Contents')]/div[contains(@class,'MuiGrid-item')]")).ToList()[rowNumber];
                        break;
                }
                item.Click();
                DlkLogger.LogInfo("ClickItemByRowNumber() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemByRowNumber() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Hovers and verifies tool tip value
        /// Supported widget: Favorite contacts as of now
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="PartialRowValue"></param>
        /// <param name="ToolTipValue"></param>

        [Keyword("HoverAndVerifyToolTipValue", new String[] { "1|text|Item|Item1" })]
        public void HoverAndVerifyToolTipValue(String RowNumber, String PartialRowValue, String ToolTipValue)
        {
            try
            {
                Initialize();
                IWebElement rowToHover = null;
                IWebElement itemToHover = null;

                // Different support for diff widgets
                switch (GetWidgetType())
                {
                    // Find item to hover using the rowNumber and rowValueContains
                    case "favoriteContacts":
                        rowToHover = mElement.FindElements(By.XPath(WIDGETROW_FAVCONTACTS_XPATH + "[" + RowNumber + "]"))
                             .FirstOrDefault();
                        itemToHover = rowToHover.FindElements(By.XPath(".//*[contains(@class,'hoverTooltip') and contains(string(), '" + PartialRowValue + "')]"))
                            .FirstOrDefault();
                        break;
                    case "recentlyListed":
                        rowToHover = mElement.FindElements(By.XPath(WIDGETROW_RECENTLYLISTED_XPATH + "[" + RowNumber + "]"))
                             .FirstOrDefault();
                        itemToHover = rowToHover.FindElements(By.XPath(".//*[contains(@class,'hoverTooltip') and contains(string(), '" + PartialRowValue + "')]"))
                            .FirstOrDefault();
                        break;
                    default:
                        break;
                }

                // Hover on selected it
                new DlkBaseControl("ToolTip", itemToHover).MouseOver();
                // Get tooltip value 
                string actualToolTipValue = mElement.FindElements(By.XPath("//div[contains(@id,'mui-tooltip')]")).FirstOrDefault().Text;
                // Assert
                DlkAssert.AssertEqual("HoverAndVerifyToolTip() : " + mControlName, ToolTipValue, actualToolTipValue);
            }
            catch (Exception e)
            {
                throw new Exception("HoverAndVerifyToolTipValue() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private methods

        private string GetWidgetType()
        {
            string widgetType = null;
            switch (mElement.GetAttribute("data-testid"))
            {
                case "dvFavoriteContactsWidget":
                case null: // adding null for widgets specifically mapped as favcontacts_name, favcontacts_email as there is no regular pattern/data-testid
                    widgetType = "favoriteContacts";
                    break;
                case "dvRecentProductsWidget":
                    widgetType = "recentlyListed";
                    break;
                case "dvTopResultsComponent":
                    widgetType = "dvTopResultsComponent";
                    break;
                default:
                    widgetType = "N/A";
                    break;
            }
            return widgetType;
        }

        private string GetWidgetTextByRowNumber(string rowNumber)
        {
            string actualValue = null;
            string classAttribute = mElement.GetAttribute("class");

            if (classAttribute.Contains("favoriteContactItemFullNameLink"))
                actualValue = mElement.FindElements(By.XPath("(//*[contains(@class,'favoriteContactItemFullNameLink ')])[" + rowNumber + "]")).FirstOrDefault().Text.Trim();
            else if (classAttribute.Contains("favoriteContactItemRoleLink "))
                actualValue = mElement.FindElements(By.XPath("(//*[contains(@class,'favoriteContactItemRoleLink ')])[" + rowNumber + "]")).FirstOrDefault().Text.Trim();
            else if (classAttribute.Contains("favoriteContactItemEmailLink "))
                actualValue = mElement.FindElements(By.XPath("(//*[contains(@class,'favoriteContactItemEmailLink ')])[" + rowNumber + "]")).FirstOrDefault().Text.Trim();
            else if (classAttribute.Contains("favoriteContactItemPhoneContainer "))
                actualValue = mElement.FindElements(By.XPath("(//*[contains(@class,'favoriteContactItemPhoneContainer ')])[" + rowNumber + "]")).FirstOrDefault().Text.Trim();

            return actualValue;
        }

        private string GetRowValueByRowNumber(string RowNumber)
        {
            string actualValue = null;
            switch (GetWidgetType())
            {
                case "favoriteContacts":
                    actualValue = GetWidgetTextByRowNumber(RowNumber);
                    break;
                case "recentlyListed":
                    IList<IWebElement> items = mElement.FindElements(By.XPath(GRIDITEM_XPATH + "[" + RowNumber + "]//div[contains(@class,'MuiGrid-item')]"));
                    IList<string> actualContents = new List<string>();
                    foreach (IWebElement item in items)
                    {
                        actualContents.Add(DlkString.RemoveCarriageReturn(item.Text.Trim()));
                    }
                    actualValue = string.Join("~", actualContents).TrimEnd('~'); // trimming last delimeter since last element is an image not text (recently listed widgets)
                    break;
                default:
                    throw new Exception("Widget type not supported");
            }
            return actualValue;
        }

        private string GetColumnValueByRowNumberAndItemName(string RowNumber, string ItemName)
        {
            string rowText = null,
                      actualText = null,
                      columnName = null;
            switch (ItemName.Trim())
            {
                case "Last Updated":
                    columnName = "Last Updated: ";
                    rowText = mElement.FindElements(By.XPath("(//div[contains(@class,'Container')]//div[contains(@class,'dvTopResultsItemUpdateDate')])[" + RowNumber + "]")).FirstOrDefault().Text.Trim();
                    actualText = rowText.Substring(rowText.IndexOf(columnName) + columnName.Length);
                    break;
                case "State":
                    columnName = "State: ";
                    rowText = mElement.FindElements(By.XPath("(//div[contains(@class,'Container')]//div[contains(@class,'dvTopResultsItemState')])[" + RowNumber + "]")).FirstOrDefault().Text.Trim();
                    actualText = rowText.Substring(rowText.LastIndexOf(":") + 1).Trim();
                    break;
                case "Building Type":
                    columnName = "Building Type: ";
                    rowText = mElement.FindElements(By.XPath("(//div[contains(@class,'Container')]//div[contains(@class,'dvTopResultsItemBuildingType')])[" + RowNumber + "]")).FirstOrDefault().Text.Trim();
                    actualText = rowText.Substring(rowText.IndexOf(columnName) + columnName.Length);
                    break;
                case "Budget":
                    columnName = "Budget: ";
                    rowText = mElement.FindElements(By.XPath("(//div[contains(@class,'Container')]//div[contains(@class,'dvTopResultsItemConstructionBudget')])[" + RowNumber + "]")).FirstOrDefault().Text.Trim();
                    actualText = rowText.Substring(rowText.IndexOf(columnName) + columnName.Length);
                    break;
                default:
                    break;
            }
            return actualText;
        }
        #endregion
    }
}
