using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SBCLib.DlkControls
{
    [ControlType("TreeList")]
    public class DlkTreeList : DlkBaseControl
    {
        #region Constructors
        public DlkTreeList(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkTreeList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTreeList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IList<IWebElement> lstItems = null;
        private const string mTreeItemXpath = ".//li[@role='presentation']";
        private const string mTreeItemExpanderXpath = ".//i[contains(@class,'jstree-ocl')]";

        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            FindTreeItems();
        }

        #region Keywords

        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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
        /// Expands a treelist item
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("ExpandItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void ExpandItem(String Item)
        {
            try
            {
                Initialize();
                IWebElement treeItem = GetItem(Item);
                Boolean bCurrentValue = GetExpandedState(treeItem);
                if (!bCurrentValue)
                {
                    treeItem.FindElement(By.XPath(mTreeItemExpanderXpath)).Click();
                }
                else
                {
                    DlkLogger.LogInfo("Item is already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("ExpandItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandItem() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Collapses a treelist item
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("CollapseItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void CollapseItem(String Item)
        {
            try
            {
                Initialize();
                IWebElement treeItem = GetItem(Item);
                Boolean bCurrentValue = GetExpandedState(treeItem);
                if (bCurrentValue)
                {
                    treeItem.FindElement(By.XPath(mTreeItemExpanderXpath)).Click();
                }
                else
                {
                    DlkLogger.LogInfo("Item is already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("CollapseItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseItem() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets the value of an item's checkbox
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("SetItemCheckbox", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetItemCheckbox(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                if (!Boolean.TryParse(TrueOrFalse, out Boolean bValue)) throw new Exception($"Value: [{TrueOrFalse}] is invalid. True Or False values are only accepted.");
                IWebElement treeItem = GetItem(Item);
                Boolean bCurrentValue = GetItemCheckboxState(treeItem);
                if (bCurrentValue != bValue)
                {
                    int maxRetry = 3;
                    for(int i=1; i<= maxRetry; i++)
                    {
                        IWebElement checkbox = treeItem.FindElement(By.TagName("a"));
                        new DlkBaseControl(Item, checkbox).Click();
                        Thread.Sleep(1000);
                        bCurrentValue = GetItemCheckboxState(treeItem);
                        if (bCurrentValue == bValue)
                        {
                            break;
                        }
                        DlkLogger.LogInfo($"Click failed. Retrying.... Attempts: {i}");
                        if (i == maxRetry)
                        {
                            throw new Exception("Maximum number of failed attempts reached.");
                        }
                    }
                }
                else
                {
                    DlkLogger.LogInfo("Checkbox already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("SetItemCheckbox() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetItemCheckbox() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Sets the value of an item's checkbox
        /// </summary>
        /// <param name="Item"></param>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyItemCheckbox", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemCheckbox(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                if (!Boolean.TryParse(TrueOrFalse, out Boolean bValue)) throw new Exception($"Value: [{TrueOrFalse}] is invalid. True Or False values are only accepted.");
                IWebElement treeItem = GetItem(Item);
                DlkAssert.AssertEqual("", bValue, GetItemCheckboxState(treeItem));
                DlkLogger.LogInfo("VerifyItemCheckbox() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCheckbox() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private void FindTreeItems()
        {
            lstItems = mElement.FindElements(By.XPath(mTreeItemXpath)).ToList();
        }

        private IWebElement GetItem(String Text)
        {
            IWebElement element = lstItems.Where(x => x.FindElement(By.TagName("a")).Text.Trim(' ').Equals(Text)).FirstOrDefault();
            if (element == null) throw new Exception($"[{Text}] not found...");
            return element;
        }

        // <summary>
        /// Returns true if the checkbox is in checked state.
        /// SBCTreelist checkbox doesnt have checked or selected attribute. This is the only way to check if the checkbox is clicked.
        /// </summary>
        private Boolean GetItemCheckboxState(IWebElement Item)
        {
            Initialize();
            Boolean bCurrentVal = Item.FindElement(By.TagName("a")).GetAttribute("class").ToLower().Contains("jstree-clicked") ? true :
                                    Item.FindElement(By.XPath(".//i[contains(@class,'jstree-checkbox')]")).GetAttribute("class").ToLower().Contains("jstree-undetermined");
            string bState = bCurrentVal ? "checked" : "unchecked";
            DlkLogger.LogInfo($"TreeItem is in [{ bState }] state");
            return bCurrentVal;
        }

        /// <summary>
        /// Returns true if the tree-item is in expanded state.
        /// </summary>
        private Boolean GetExpandedState(IWebElement Item)
        {
            Initialize();
            Boolean bCurrentVal = Convert.ToBoolean(Item.GetAttribute("aria-expanded"));
            string bState = bCurrentVal ? "expanded" : "collapsed";
            DlkLogger.LogInfo($"TreeItem is in [{ bState }] state");
            return bCurrentVal;
        }
        #endregion
    }
 }

