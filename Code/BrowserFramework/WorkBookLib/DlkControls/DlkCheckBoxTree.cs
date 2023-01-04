using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace WorkBookLib.DlkControls
{
    [ControlType("CheckBoxTree")]
    public class DlkCheckBoxTree : DlkBaseControl
    {

        #region PRIVATE VARIABLES
        private static String mTreeContainer_XPath = ".//ul[contains(@class,'jstree-children')]";
        private static String mTreeItem_XPath = "./li[contains(@role,'treeitem')]";
        private static String mTreeIcon_XPath = "./i[contains(@class,'jstree-icon')]";
        private static String mTreeAnchor_XPath = "./a[contains(@class,'jstree-anchor')]";
        private static String mTreeCheckBox_XPath = "./i[contains(@class,'jstree-checkbox')]";

        #endregion

        #region CONSTRUCTORS
        public DlkCheckBoxTree(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBoxTree(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBoxTree(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkCheckBoxTree(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
        }

        public IWebElement GetTreeContainer(IWebElement treeParent)
        {
            IWebElement treeContainer = treeParent.FindElements(By.XPath(mTreeContainer_XPath)).Count > 0 ?
                treeParent.FindElement(By.XPath(mTreeContainer_XPath)) : throw new Exception("Tree container not found.");
            return treeContainer;
        }

        public IList<IWebElement> GetTreeItems(IWebElement container)
        {
            IList<IWebElement> treeItems = container.FindElements(By.XPath(mTreeItem_XPath)).Where(x => x.Displayed).ToList();
            return treeItems != null ? treeItems : throw new Exception("Tree items not found.");
        }

        public void OpenAllTreeItems(IList <IWebElement> treeItems)
        {
            foreach (IWebElement treeItem in treeItems)
            {
                bool isOpen = treeItem.GetAttribute("class").Contains("jstree-open");
                if (!isOpen)
                {
                    IWebElement treeIcon = treeItem.FindElements(By.XPath(mTreeIcon_XPath)).Count > 0 ?
                        treeItem.FindElement(By.XPath(mTreeIcon_XPath)) : null;

                    if (treeIcon != null)
                        treeIcon.Click();
                    else
                        DlkLogger.LogInfo("Tree icon is already open or not found. No click action done.");
                }
            }
        }

        public IList<IWebElement> GetTreeAnchors(IWebElement treeItem)
        {
            IList<IWebElement> treeAnchors = treeItem.FindElements(By.XPath(mTreeAnchor_XPath)).Where(x => x.Displayed).ToList();
            return treeAnchors != null ? treeAnchors : throw new Exception("Tree anchors not found.");
        }

        public IWebElement GetNodeCheckBox(IWebElement treeNode)
        {
            IWebElement itemCheckBox = treeNode.FindElements(By.XPath(mTreeCheckBox_XPath)).Count > 0 ?
                treeNode.FindElement(By.XPath(mTreeCheckBox_XPath)) : throw new Exception("Checkbox item not found.");
            return itemCheckBox;
        }

        public bool GetState(IWebElement treeAnchor)
        {
            bool currentState = treeAnchor.GetAttribute("class").Contains("jstree-clicked");
            return currentState;
        }

        public bool SetSingleTreeItem(IList <IWebElement> mTreeItems, string Item, bool expectedValue)
        {
            bool tFound = false;
            foreach (IWebElement treeItem in mTreeItems)
            {
                IList<IWebElement> mTreeAnchors = GetTreeAnchors(treeItem);
                foreach (IWebElement treeAnchor in mTreeAnchors)
                {
                    string currentItem = treeAnchor.Text.Trim().ToLower();
                    if (currentItem.Equals(Item.ToLower()))
                    {
                        IWebElement mCheckBoxItem = GetNodeCheckBox(treeAnchor);
                        if (expectedValue != GetState(treeAnchor))
                        {
                            DlkLogger.LogInfo("Clicking checkbox item... ");
                            mCheckBoxItem.Click();
                        }
                        else
                        {
                            DlkLogger.LogInfo("Checkbox item has same current state. No click action done.");
                        }
                        tFound = true;
                        break;
                    }
                }
            }
            if (!tFound)
                throw new Exception("Item [" + Item + "] not found in the CheckBox Tree.");
            return tFound;
        }

        public bool GetSingleTreeItemState(IList<IWebElement> mTreeItems, string Item, string VariableName)
        {
            bool tFound = false;
            foreach (IWebElement treeItem in mTreeItems)
            {
                IList<IWebElement> mTreeAnchors = GetTreeAnchors(treeItem);
                foreach (IWebElement treeAnchor in mTreeAnchors)
                {
                    string currentItem = treeAnchor.Text.Trim().ToLower();
                    if (currentItem.Equals(Item.ToLower()))
                    {
                        IWebElement mCheckBoxItem = GetNodeCheckBox(treeAnchor);
                        string actualValue = GetState(treeAnchor).ToString();
                        DlkVariable.SetVariable(VariableName, actualValue);
                        DlkLogger.LogInfo("[" + actualValue + "] value set to Variable: [" + VariableName + "]");
                        tFound = true;
                        break;
                    }
                }
            }
            if (!tFound)
                throw new Exception("Item [" + Item + "] not found in the CheckBox Tree.");
            return tFound;
        }

        public bool VerifySingleTreeItemState(IList<IWebElement> mTreeItems, string Item, string expectedValue)
        {
            bool tFound = false;
            foreach (IWebElement treeItem in mTreeItems)
            {
                IList<IWebElement> mTreeAnchors = GetTreeAnchors(treeItem);
                foreach (IWebElement treeAnchor in mTreeAnchors)
                {
                    string currentItem = treeAnchor.Text.Trim().ToLower();
                    if (currentItem.Equals(Item.ToLower()))
                    {
                        IWebElement mCheckBoxItem = GetNodeCheckBox(treeAnchor);
                        string actualValue = GetState(treeAnchor).ToString();
                        DlkAssert.AssertEqual("VerifyItemState(): ", expectedValue, actualValue);
                        tFound = true;
                        break;
                    }
                }
            }
            if (!tFound)
                throw new Exception("Item [" + Item + "] not found in the CheckBox Tree.");
            return tFound;
        }

        public string GetTreeItemList(IList <IWebElement> mTreeItems)
        {
            string actualTreeItemList = "";
            foreach (IWebElement treeItem in mTreeItems)
            {
                IList<IWebElement> mTreeAnchors = GetTreeAnchors(treeItem);
                int treeAnchorCount = 1;
                foreach (IWebElement treeAnchor in mTreeAnchors)
                {
                    string currentTreeAnchor = treeAnchor.Text.Trim();
                    actualTreeItemList = String.IsNullOrEmpty(actualTreeItemList) ? actualTreeItemList + "(" + treeAnchorCount.ToString() + ") " + currentTreeAnchor :
                        actualTreeItemList + Environment.NewLine + "(" + treeAnchorCount.ToString() + ") " + currentTreeAnchor;

                    if (treeItem.FindElements(By.XPath(mTreeContainer_XPath)).Count > 0)
                    {
                        IWebElement treeChildrenContainer = GetTreeContainer(treeItem);
                        IList<IWebElement> treeChildrenItems = GetTreeItems(treeChildrenContainer);
                        foreach (IWebElement treeChildrenItem in treeChildrenItems)
                        {
                            IList<IWebElement> mTreeChildrenAnchors = GetTreeAnchors(treeChildrenItem);
                            foreach (IWebElement treeChildrenAnchor in mTreeChildrenAnchors)
                            {
                                string currentTreeChildrenAnchor = treeChildrenAnchor.Text.Trim();
                                actualTreeItemList += "~" + currentTreeChildrenAnchor;
                            }
                        }
                    }
                    treeAnchorCount++;
                }
            }
            return actualTreeItemList;
        }
        #endregion

        #region KEYWORDS

        [Keyword("SetItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetItem(String Item, String TrueOrFalse)
        {
            try
            {
                bool expectedValue;
                if (!Boolean.TryParse(TrueOrFalse, out expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                IWebElement mTreeContainer = GetTreeContainer(mElement);
                IList <IWebElement> mTreeItems = GetTreeItems(mTreeContainer);
                OpenAllTreeItems(mTreeItems);

                if (!Item.Contains('~'))
                {
                    SetSingleTreeItem(mTreeItems, Item, expectedValue);
                }
                else
                {
                    string [] Items = Item.Split('~');
                    bool tFound = false;
                    foreach (IWebElement treeItem in mTreeItems)
                    {
                        IList<IWebElement> mTreeAnchors = GetTreeAnchors(treeItem);
                        foreach (IWebElement treeAnchor in mTreeAnchors)
                        {
                            string currentItem = treeAnchor.Text.Trim().ToLower();
                            if (currentItem.Equals(Items[0].ToLower()))
                            {
                                if(treeItem.FindElements(By.XPath(mTreeContainer_XPath)).Count > 0)
                                {
                                    IWebElement treeChildrenContainer = GetTreeContainer(treeItem);
                                    IList<IWebElement> treeChildrenItems = GetTreeItems(treeChildrenContainer);
                                    foreach (IWebElement treeChildrenItem in treeChildrenItems)
                                    {
                                        tFound = SetSingleTreeItem(treeChildrenItems, Items[1], expectedValue);
                                        if (tFound)
                                            break;
                                    }
                                }
                            }
                            if (tFound)
                                break;
                        }
                    }
                    if (!tFound)
                        throw new Exception("Item [" + Item + "] not found in the CheckBox Tree.");
                }

                DlkLogger.LogInfo("SetItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetItem() failed : " + e.Message, e);
            }
        }

        [Keyword("GetItemState", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetItemState(String Item, String VariableName)
        {
            try
            {
                Initialize();
                IWebElement mTreeContainer = GetTreeContainer(mElement);
                IList<IWebElement> mTreeItems = GetTreeItems(mTreeContainer);
                OpenAllTreeItems(mTreeItems);

                if (!Item.Contains('~'))
                {
                    GetSingleTreeItemState(mTreeItems, Item, VariableName);
                }
                else
                {
                    string[] Items = Item.Split('~');
                    bool tFound = false;
                    foreach (IWebElement treeItem in mTreeItems)
                    {
                        IList<IWebElement> mTreeAnchors = GetTreeAnchors(treeItem);
                        foreach (IWebElement treeAnchor in mTreeAnchors)
                        {
                            string currentItem = treeAnchor.Text.Trim().ToLower();
                            if (currentItem.Equals(Items[0].ToLower()))
                            {
                                if (treeItem.FindElements(By.XPath(mTreeContainer_XPath)).Count > 0)
                                {
                                    IWebElement treeChildrenContainer = GetTreeContainer(treeItem);
                                    IList<IWebElement> treeChildrenItems = GetTreeItems(treeChildrenContainer);
                                    foreach (IWebElement treeChildrenItem in treeChildrenItems)
                                    {
                                        tFound = GetSingleTreeItemState(treeChildrenItems, Items[1], VariableName);
                                        if (tFound)
                                            break;
                                    }
                                }
                            }
                            if (tFound)
                                break;
                        }
                    }
                    if (!tFound)
                        throw new Exception("Item [" + Item + "] not found in the CheckBox Tree.");
                }

                DlkLogger.LogInfo("GetItemState() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetItemState() failed : " + e.Message, e);
            }
        }

        [Keyword("GetTreeItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetTreeItems(String VariableName)
        {
            try
            {
                Initialize();
                IWebElement mTreeContainer = GetTreeContainer(mElement);
                IList<IWebElement> mTreeItems = GetTreeItems(mTreeContainer);
                OpenAllTreeItems(mTreeItems);

                string actualTreeListItems = GetTreeItemList(mTreeItems);
                DlkVariable.SetVariable(VariableName, actualTreeListItems);
                DlkLogger.LogInfo("[" + actualTreeListItems + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetTreeItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetTreeItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTreeItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTreeItems(String ExpectedTreeItems)
        {
            try
            {
                Initialize();
                IWebElement mTreeContainer = GetTreeContainer(mElement);
                IList<IWebElement> mTreeItems = GetTreeItems(mTreeContainer);
                OpenAllTreeItems(mTreeItems);

                string actualTreeListItems = GetTreeItemList(mTreeItems);
                DlkAssert.AssertEqual("VerifyTreeItems(): ", ExpectedTreeItems, actualTreeListItems);
                DlkLogger.LogInfo("VerifyTreeItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemState", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemState(String Item, String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement mTreeContainer = GetTreeContainer(mElement);
                IList<IWebElement> mTreeItems = GetTreeItems(mTreeContainer);
                OpenAllTreeItems(mTreeItems);

                if (!Item.Contains('~'))
                {
                    VerifySingleTreeItemState(mTreeItems, Item, ExpectedValue);
                }
                else
                {
                    string[] Items = Item.Split('~');
                    bool tFound = false;
                    foreach (IWebElement treeItem in mTreeItems)
                    {
                        IList<IWebElement> mTreeAnchors = GetTreeAnchors(treeItem);
                        foreach (IWebElement treeAnchor in mTreeAnchors)
                        {
                            string currentItem = treeAnchor.Text.Trim().ToLower();
                            if (currentItem.Equals(Items[0].ToLower()))
                            {
                                if (treeItem.FindElements(By.XPath(mTreeContainer_XPath)).Count > 0)
                                {
                                    IWebElement treeChildrenContainer = GetTreeContainer(treeItem);
                                    IList<IWebElement> treeChildrenItems = GetTreeItems(treeChildrenContainer);
                                    foreach (IWebElement treeChildrenItem in treeChildrenItems)
                                    {
                                        tFound = VerifySingleTreeItemState(treeChildrenItems, Items[1], ExpectedValue);
                                        if (tFound)
                                            break;
                                    }
                                }
                            }
                            if (tFound)
                                break;
                        }
                    }
                    if (!tFound)
                        throw new Exception("Item [" + Item + "] not found in the CheckBox Tree.");
                }

                DlkLogger.LogInfo("VerifyItemState() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemState() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}