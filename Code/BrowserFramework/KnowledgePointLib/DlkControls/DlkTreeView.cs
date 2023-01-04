using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using KnowledgePointLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("TreeView")]
    public class DlkTreeView : DlkBaseControl
    {
        #region Constructors
        public DlkTreeView(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTreeView(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTreeView(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        private string treeViewType = null;
        private const string cardViewXpath = ".//*[contains(@class, 'elementItemCard')]";
        private const string defaultViewXpath = "./ul[@role='tree']/li";

        private void Initialize()
        {
            FindElement();
            GetTreeViewType();
            DlkKnowledgePointCommon.WaitForScreenToLoad();
            DlkKnowledgePointCommon.WaitForLoadingSpinnerToFinish();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if TreeView exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyExists", new String[] { "1|text|TrueOrFalse|TRUE" })]
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
        /// Verifies row value of a treeview
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyRowValue", new String[] { "1|text|RowNumber|Value", "2|text|ExpectedValue|TRUE" })]
        public void VerifyRowValue(String RowNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                int.TryParse(RowNumber, out int rowNum);
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                string actualRowValue = GetTreeViewItems()[rowNum - 1].Text.Trim();
                DlkAssert.AssertEqual("VerifyRowValue() : " + mControlName, ExpectedValue, actualRowValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowValue() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies row value of a treeview
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyRowTitle", new String[] { "1|text|RowNumber|Value", "2|text|ExpectedValue|TRUE" })]
        public void VerifyRowTitle(String RowNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                int.TryParse(RowNumber, out int rowNum);
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                var item = GetTreeViewItems()[rowNum - 1].FindElements(By.XPath(".//span[1]")).FirstOrDefault();
                if (item == null) throw new Exception("Cannot find item with row number: " + RowNumber);

                string actualRowValue = item.Text.Trim();
                DlkAssert.AssertEqual("VerifyRowTitle() : " + mControlName, ExpectedValue, actualRowValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowTitle() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies row value of a treeview
        /// </summary>
        /// <param name="RowNumber"></param>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyRowSubText", new String[] { "1|text|RowNumber|Value", "2|text|ExpectedValue|TRUE" })]
        public void VerifyRowSubText(String RowNumber, String ExpectedValue)
        {
            try
            {
                Initialize();
                int.TryParse(RowNumber, out int rowNum);
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                var item = GetTreeViewItems()[rowNum - 1].FindElements(By.XPath(".//span[2]")).FirstOrDefault();
                if (item == null) throw new Exception("Cannot find item with row number: " + RowNumber);
                string actualRowValue = item.Text.Trim();
                DlkAssert.AssertEqual("VerifyRowSubText() : " + mControlName, ExpectedValue, actualRowValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRowSubText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies content of specific treeview item like a list
        /// (For default treeview)
        /// </summary>
        /// <param name="ItemNumber"></param>
        /// <param name="ExpectedContents"></param>
        [Keyword("VerifyTreeContents", new String[] { "1|text|ItemNumber|Value", "2|text|ExpectedContents|TRUE" })]
        public void VerifyTreeContents(String ItemNumber, String ExpectedContents)
        {
            try
            {
                Initialize();
                int.TryParse(ItemNumber, out int itemNum);
                IList<IWebElement> contents = new List<IWebElement>();
                IList<string> actualContents = new List<string>();

                contents = treeViewType == "cardView" ? GetTreeViewItems()[itemNum - 1].FindElements(By.XPath(".//span")).ToList() : GetTreeViewItems()[itemNum - 1].FindElements(By.XPath(".//div[contains(@data-testid,'sectionDetailContainerLabel')]")).ToList();

                foreach (IWebElement item in contents)
                {
                    actualContents.Add(DlkString.RemoveCarriageReturn(item.Text.Trim()));
                }

                string actualDelimitedContents = string.Join("~", actualContents);
                DlkAssert.AssertEqual("VerifyTreeContents() : " + mControlName, ExpectedContents, actualDelimitedContents);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTreeContents() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies content of specific treeview item like a list
        /// (For default treeview)
        /// </summary>
        /// <param name="ExpectedContents"></param>
        [Keyword("VerifyTreeViewItems", new String[] { "1|text|ItemNumber|Value", "2|text|ExpectedContents|TRUE" })]
        public void VerifyTreeViewItems(String ItemNumber, String ExpectedContents)
        {
            try
            {
                Initialize();
                int.TryParse(ItemNumber, out int itemNum);
                IList<IWebElement> contents = new List<IWebElement>();
                IList<string> actualContents = new List<string>();

                contents = treeViewType == "cardView" ? GetTreeViewItems()[itemNum - 1].FindElements(By.XPath(".//span")).ToList() : GetTreeViewItems()[itemNum - 1].FindElements(By.XPath(".//div[contains(@data-testid,'sectionDetailContainerLabel')]")).ToList();

                foreach (IWebElement item in contents)
                {
                    actualContents.Add(DlkString.RemoveCarriageReturn(item.Text.Trim()));
                }

                string actualDelimitedContents = string.Join("~", actualContents);
                DlkAssert.AssertEqual("VerifyTreeViewItems() : " + mControlName, ExpectedContents, actualDelimitedContents);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTreeViewItems() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Selects tree view item by text
        /// </summary>
        /// <param name="ItemText"></param>
        [Keyword("SelectByText", new String[] { "1|text|ItemText|TRUE" })]
        public void SelectByText(String ItemText)
        {
            try
            {
                Initialize();
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                var tvItem = GetTreeViewItems().FirstOrDefault(item => item.Text.Contains(ItemText));
                if (tvItem == null) throw new Exception("Cannot find item with value: " + ItemText);

                tvItem.Click();
                DlkLogger.LogInfo("SelectByText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects tree view item by index
        /// </summary>
        /// <param name="Index"></param>
        [Keyword("SelectByIndex", new String[] { "1|text|ItemText|TRUE" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                Initialize();
                int.TryParse(Index, out int index);
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                var itemToSelect = GetTreeViewItems()[index - 1];

                itemToSelect.Click();
                DlkLogger.LogInfo("SelectByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Collapses tree view item by text
        /// </summary>
        /// <param name="ItemText"></param>
        [Keyword("CollapseItemByText", new String[] { "1|text|ItemText|TRUE" })]
        public void CollapseItemByText(String ItemText)
        {
            try
            {
                Initialize();
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                var tvItem = GetTreeViewItems().FirstOrDefault(item => item.Text.Contains(ItemText));
                if (tvItem == null) throw new Exception("Cannot find item with value: " + ItemText);

                var collapser = tvItem.FindElements(By.XPath(".//*[@data-testid='ExpandMoreIcon']")).FirstOrDefault();
                collapser.Click();
                
                DlkLogger.LogInfo("CollapseItemByText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseItemByText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Collapses tree view item by text
        /// </summary>
        /// <param name="Index"></param>
        [Keyword("CollapseItemByIndex", new String[] { "1|text|ItemText|TRUE" })]
        public void CollapseItemByIndex(String Index)
        {
            try
            {
                Initialize();
                int.TryParse(Index, out int index);
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                var tvItem = GetTreeViewItems()[index - 1];
                
                var collapser = tvItem.FindElements(By.XPath(".//*[@data-testid='ExpandMoreIcon']")).FirstOrDefault();
                collapser.Click();

                DlkLogger.LogInfo("CollapseItemByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseItemByIndex() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verify tree view item exists
        /// </summary>
        /// <param name="ItemText"></param>
        [Keyword("VerifyItemAvailable", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemAvailable(String ItemText, String IsItemAvailable)
        {
            try
            {
                Initialize();
                bool.TryParse(IsItemAvailable, out bool expectedValue);
                if (GetTreeViewItems() == null) throw new Exception("Cannot find items in tree view.");

                var test = GetTreeViewItems();
                var isAvailable = GetTreeViewItems().Any(item => item.Text.Contains(ItemText) && item.Displayed);

                DlkAssert.AssertEqual(("VerifyItemAvailable() with item text "+ ItemText +": ") + mControlName, expectedValue, isAvailable);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemAvailable() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods

        private string GetTreeViewType()
        {
            if(mElement.FindElements(By.XPath(cardViewXpath)).Any())
            {
                treeViewType = "cardView";
            }
            else
            {
                treeViewType = "default";
            }
            return treeViewType;
        }

        private List<IWebElement> GetTreeViewItems()
        {
            return mElement.FindElements(By.XPath(treeViewType == "cardView" ? cardViewXpath : defaultViewXpath)).Where(elem => elem.Displayed).ToList();
        }
        #endregion
    }
}
