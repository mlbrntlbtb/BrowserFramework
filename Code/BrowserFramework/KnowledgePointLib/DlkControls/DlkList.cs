using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;


namespace KnowledgePointLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        #region Constructors
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        #region Variables
        private string listType = null;
        #endregion

        private void Initialize()
        {
            FindElement();
            listType = GetListType();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if list exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
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

        /// <summary>
        /// Verifies the numner of items in the list. Requires expected item count.
        /// </summary>
        /// <param name="ExpectedCount"></param>
        [Keyword("VerifyItemCount", new String[] { "1|text|ExpectedCount|1~3~5" })]
        public void VerifyItemCount(string ExpectedCount)
        {
            try
            {
                Initialize();
                int.TryParse(ExpectedCount, out int expectedCount);
                var items = GetItems();
                if (items == null) throw new Exception("Cannot find items in list.");

                DlkAssert.AssertEqual("VerifyItemCount", items.Count, expectedCount);
                DlkLogger.LogInfo("Successfully executed VerifyItemCount().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies the item title text. Requires the item index and expected title text.
        /// </summary>
        /// <param name="ItemIndex"></param>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyItemText", new String[] { "1|text|ItemIndex|1~5", "2|text|ExpectedText|Value" })]
        public void VerifyItemText(string ItemIndex, string ExpectedText)
        {
            try
            {
                Initialize();
                int.TryParse(ItemIndex, out int index);
                var items = GetItems();
                if (items == null || items.Count <= 0) throw new Exception("Cannot find items in list.");

                var itemToCheck = items[index - 1].FindElements(By.XPath(GetContentXpath("title"))).FirstOrDefault();
                if(itemToCheck == null) throw new Exception("Cannot find list item to verify.");

                DlkAssert.AssertEqual("VerifyItemText", ExpectedText, itemToCheck.Text);
                DlkLogger.LogInfo("Successfully executed VerifyItemText().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies the item subtext. Requires the item index and expected subtext.
        /// </summary>
        /// <param name="ItemIndex"></param>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyItemSubText", new String[] { "1|text|ItemIndex|1~5", "2|text|ExpectedText|Value" })]
        public void VerifyItemSubText(string ItemIndex, string ExpectedText)
        {
            try
            {
                Initialize();
                int.TryParse(ItemIndex, out int index);
                var items = GetItems();
                if (items == null || items.Count <= 0) throw new Exception("Cannot find items in list.");

                var itemToCheck = items[index - 1].FindElements(By.XPath(GetContentXpath("SubText"))).FirstOrDefault();
                if (itemToCheck == null) throw new Exception("Cannot find list item to verify.");

                DlkAssert.AssertEqual("VerifyItemSubText", ExpectedText, DlkString.RemoveCarriageReturn(itemToCheck.Text));
                DlkLogger.LogInfo("Successfully executed VerifyItemSubText().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemSubText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if the item image exists. Requires the item index and the expected value (true or false).
        /// </summary>
        /// <param name="ItemIndex"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyItemImageExists", new String[] { "1|text|ItemIndex|1~5", "2|text|ExpectedText|Value" })]
        public void VerifyItemImageExists(string ItemIndex, string ExpectedValue)
        {
            try
            {
                Initialize();
                int.TryParse(ItemIndex, out int index);
                bool.TryParse(ExpectedValue, out bool expectedValue);
                var items = GetItems();
                if (items == null || items.Count <= 0) throw new Exception("Cannot find items in list.");

                var imageExists = items[index - 1].FindElements(By.XPath(GetContentXpath("image"))).Any();
                DlkAssert.AssertEqual("VerifyItemImageExists", expectedValue, imageExists);
                DlkLogger.LogInfo("Successfully executed VerifyItemImageExists().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageExists() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Selects an item using the item text.
        /// </summary>
        /// <param name="TextToSelect"></param>
        [Keyword("Select", new String[] { "1|text|TextToSelect|Value" })]
        public void Select(string TextToSelect)
        {
            try
            {
                Initialize();
                var listItems = GetItems();
                if (listItems == null || listItems.Count <= 0) throw new Exception("Cannot find items in list.");
                
                var itemToClick = listItems.FirstOrDefault(item => item.Text.ToLower().Contains(TextToSelect.ToLower()));
                
                if(itemToClick == null)
                {
                    throw new Exception("Cannot get item with text: " + TextToSelect);
                }    
                else
                {
                    itemToClick.Click();
                }

                DlkLogger.LogInfo("Successfully executed Select().");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects an item using the item index.
        /// </summary>
        /// <param name="Index"></param>
        [Keyword("SelectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                Initialize();
                int.TryParse(Index, out int index);
                var listItems = GetItems();
                if (listItems == null || listItems.Count <= 0) throw new Exception("Cannot find items in list.");

                listItems[index - 1].Click();
                DlkLogger.LogInfo("Successfully executed SelectByIndex()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }

        }

        /// <summary>
        /// Verifies if the buttons on the item exist. Multiple buttons may exist at a time on an item. 
        /// ButtonText is required to select a specific button to verify
        /// </summary>
        /// <param name="ItemIndex"></param>
        /// <param name="ButtonText"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyItemButtonExists", new String[] { "1|text|ItemIndex|1~5", "2|text|ButtonText|Value", "2|text|TrueOrFalse|Value" })]
        public void VerifyItemButtonExists(string ItemIndex, string ButtonText, string TrueOrFalse)
        {
            try
            {
                Initialize();
                int.TryParse(ItemIndex, out int index);
                bool.TryParse(TrueOrFalse, out bool expectedValue);

                var listItems = GetItems();
                if (listItems == null || listItems.Count <= 0) throw new Exception("Cannot find items in list.");

                var buttonExists = listItems[index - 1].FindElements(By.XPath(GetContentXpath(ButtonText))).Any();
                DlkAssert.AssertEqual("VerifyItemButtonExists", expectedValue, buttonExists);
                DlkLogger.LogInfo("Successfully executed VerifyItemButtonExists().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemButtonExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clicks on a button of a specific item.
        /// Multiple buttons may exist, button text is required.
        /// </summary>
        /// <param name="ItemIndex"></param>
        /// <param name="ButtonText"></param>
        [Keyword("ClickItemButton", new String[] { "1|text|ItemIndex|1~5", "2|text|ButtonText|Value" })]
        public void ClickItemButton(string ItemIndex, string ButtonText)
        {
            try
            {
                Initialize();
                int.TryParse(ItemIndex, out int index);

                var listItems = GetItems();
                if (listItems == null || listItems.Count <= 0) throw new Exception("Cannot find items in list.");

                var buttonToClick = listItems[index - 1].FindElements(By.XPath(GetContentXpath(ButtonText))).FirstOrDefault();
                if (buttonToClick == null) throw new Exception($"Cannot find '{ButtonText}' button.");

                buttonToClick.Click();
                DlkLogger.LogInfo("Successfully executed ClickItemButton().");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemButton() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Gets item index using text value inside the item.
        /// </summary>
        /// <param name="ItemText"></param>
        /// <param name="VariableName"></param>
        [Keyword("GetItemIndexWithValue", new String[] { "1|text|ItemText|Sample Text", "2|text|VariableName|Value" })]
        public void GetItemIndexWithValue(string ItemText, string VariableName)
        {
            try
            {
                Initialize();
                int index = 0;

                var listItems = GetItems();
                if (listItems == null || listItems.Count <= 0) throw new Exception("Cannot find items in list.");

                var item = listItems.FirstOrDefault(x => x.Text.ToLower().Contains(ItemText.ToLower()));
                if (item == null) throw new Exception($"Cannot find item with text: '{ItemText}'");
                index = listItems.IndexOf(item);

                DlkVariable.SetVariable(VariableName, (index + 1).ToString());
                DlkLogger.LogInfo("GetItemIndexWithValue() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetItemIndexWithValue() failed. " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies all items in list by title text. Requires item text with delimiter '~'
        /// </summary>
        /// <param name="Items"></param>
        [Keyword("VerifyList", new String[] { "1|text|Items|Sample 1~Sample 2" })]
        public void VerifyList(string Items)
        {
            try
            {
                Initialize();
                var items = GetItems();
                if (items == null || items.Count <= 0) throw new Exception("Cannot find items in list.");
                var itemTitles = items.Select(e => e.FindElements(By.XPath(GetContentXpath("Title"))).FirstOrDefault()).ToList();
                if (itemTitles == null || itemTitles.Count <= 0) throw new Exception("Cannot retrieve item list");

                var elementText = string.Join("~", itemTitles.Select(x => x.Text.Trim()));
                DlkAssert.AssertEqual("VerifyList()", Items.Trim('~'), elementText);
                DlkLogger.LogInfo("VerifyList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed. " + e.Message, e);
            }
        }

        #endregion

        #region Private Methods
        //There may be multiple types of lists (similar to grid)
        //Adding provision for future use.
        private string GetListType()
        {
            if(mElement.FindElements(By.XPath(".// *[@class='MuiGrid-root MuiGrid-item']")).Any())
            {
                listType = "gridList";
            }

            return listType;
        }

        private List<IWebElement> GetItems()
        {
            List<IWebElement> itemList;

            switch (listType)
            {
                case "gridList":
                    itemList = mElement.FindElements(By.XPath(".// *[@class='MuiGrid-root MuiGrid-item']")).ToList();
                    break;
                default:
                    itemList = null;
                    break;
            }
            return itemList;
        }

        //Item content xpaths might vary depending on the list type.
        //Added for future provisions.
        private string GetContentXpath(string contentType)
        {
            string contentXpath = string.Empty;
            switch (contentType.ToLower())
            {
                case ">":
                case "arrowright":
                    contentXpath = ".//*[@data-testid='projectTeamViewInfoArrow']/parent::div";
                    break;
                case "delete":
                    contentXpath = ".//*[@data-testid='deleteTeamMemberIcon']";
                    break;
                case "title":
                    contentXpath = ".//*[contains(@class, 'projectGroupLabel')]";
                    break;
                case "subtext":
                    contentXpath = ".//p";
                    break;
                case "image":
                    contentXpath = ".//*[@data-testid='profileAvatarIcon']";
                    break;
                default:
                    throw new Exception($"'{contentType}' is unsupported");
            }
            return contentXpath;
        }
        #endregion
    }
}
