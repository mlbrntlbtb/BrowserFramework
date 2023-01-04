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
    [ControlType("TreeView")]
    public class DlkTreeView : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private static String mFirstTreeContainerXPath = ".//ul[contains(@class,'jstree-children')]";
        private static String mFirstTreeItemsXPath = "./li[contains(@role,'treeitem')]/a[contains(@class,'jstree-anchor')]";
        private static String mFirstCheckClosedXPath = "./parent::li[contains(@class,'jstree-closed')]";
        private static String mSecondTreeContainerXPath = "./following-sibling::ul[contains(@class,'jstree-children')]";
        private static String mSecondTreeItemsXPath = "./li[contains(@role,'treeitem')]/a[contains(@class,'jstree-anchor')]";
        private static String mSecondCheckClosedXPath = "./parent::li[contains(@class,'jstree-closed')]";
        private static String mThirdTreeContainerXPath = "./following-sibling::ul[contains(@class,'jstree-children')]";
        private static String mThirdTreeItemsXPath = "./li[contains(@role,'treeitem')]/a[contains(@class,'jstree-anchor')]";
        private static String mThirdCheckClosedXPath = "./parent::li[contains(@class,'jstree-closed')]";

        #endregion

        #region CONSTRUCTORS
        public DlkTreeView(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTreeView(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTreeView(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTreeView(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
        }

        public IWebElement GetFirstTreeContainer()
        {
            IWebElement firstTreeContainer = mElement.FindElements(By.XPath(mFirstTreeContainerXPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mFirstTreeContainerXPath)) : throw new Exception("First level tree container not found.");

            return firstTreeContainer;
        }

        public IWebElement GetSecondTreeContainer(IWebElement firstTreeItem)
        {
            IWebElement secondTreeContainer = firstTreeItem.FindElements(By.XPath(mSecondTreeContainerXPath)).Count > 0 ?
                firstTreeItem.FindElement(By.XPath(mSecondTreeContainerXPath)) : throw new Exception("Second level tree container not found.");

            return secondTreeContainer;
        }

        public IWebElement GetThirdTreeContainer(IWebElement secondTreeItem)
        {
            IWebElement thirdTreeContainer = secondTreeItem.FindElements(By.XPath(mThirdTreeContainerXPath)).Count > 0 ?
                secondTreeItem.FindElement(By.XPath(mThirdTreeContainerXPath)) : throw new Exception("Third level tree container not found.");

            return thirdTreeContainer;
        }

        public IList <IWebElement> GetFirstTreeItems(IWebElement firstTreeContainer)
        {
            IList<IWebElement> firstTreeItems = firstTreeContainer.FindElements(By.XPath(mFirstTreeItemsXPath))
                .Where(x => x.Displayed).ToList();

            return firstTreeItems;
        }

        public IList<IWebElement> GetSecondTreeItems(IWebElement secondTreeContainer)
        {
            IList<IWebElement> secondTreeItems = secondTreeContainer.FindElements(By.XPath(mSecondTreeItemsXPath))
                .Where(x => x.Displayed).ToList();

            return secondTreeItems;
        }

        public IList<IWebElement> GetThirdTreeItems(IWebElement thirdTreeContainer)
        {
            IList<IWebElement> thirdTreeItems = thirdTreeContainer.FindElements(By.XPath(mThirdTreeItemsXPath))
                .Where(x => x.Displayed).ToList();

            return thirdTreeItems;
        }

        public bool CheckFirstTreeItemClosed(IWebElement firstTreeItem)
        {
            bool isClosed = firstTreeItem.FindElements(By.XPath(mFirstCheckClosedXPath)).Count > 0;
            return isClosed;
        }

        public bool CheckSecondTreeItemClosed(IWebElement secondTreeItem)
        {
            bool isClosed = secondTreeItem.FindElements(By.XPath(mSecondCheckClosedXPath)).Count > 0;
            return isClosed;
        }

        public bool CheckThirdTreeItemClosed(IWebElement thirdTreeItem)
        {
            bool isClosed = thirdTreeItem.FindElements(By.XPath(mThirdCheckClosedXPath)).Count > 0;
            return isClosed;
        }
        #endregion

        #region KEYWORDS

        [Keyword("Select")]
        public void Select(String TreeItem)
        {
            try
            {
                Boolean tFound = false;
                Initialize();

                string[] treeItems = TreeItem.Split('~');

                //Retrieve and select FIRST level of Tree items
                IWebElement firstTreeContainer = GetFirstTreeContainer();
                IList<IWebElement> firstTreeItems = GetFirstTreeItems(firstTreeContainer);

                foreach (IWebElement firstTreeItem in firstTreeItems)
                {
                    string firstTreeItemValue = !String.IsNullOrEmpty(firstTreeItem.Text) ? firstTreeItem.Text.Trim() :
                        new DlkBaseControl("Tree Item", firstTreeItem).GetValue().Trim();

                    if (treeItems[0].ToLower().Equals(firstTreeItemValue.ToLower()))
                    {
                        if (CheckFirstTreeItemClosed(firstTreeItem))
                        {
                            firstTreeItem.Click(); //Click item if not closed
                            DlkLogger.LogInfo("Clicking first level item: [" + firstTreeItemValue + "] ...");
                        }
                        else
                        {
                            DlkLogger.LogInfo("First level item: [" + firstTreeItemValue + "] is already opened.");
                        }

                        if (treeItems.Length >= 2)
                        {
                            //Retrieve and select SECOND level of Tree items
                            IWebElement secondTreeContainer = GetSecondTreeContainer(firstTreeItem);
                            IList<IWebElement> secondTreeItems = GetSecondTreeItems(secondTreeContainer);

                            foreach (IWebElement secondTreeItem in secondTreeItems)
                            {
                                string secondTreeItemValue = !String.IsNullOrEmpty(secondTreeItem.Text) ? secondTreeItem.Text.Trim() :
                                new DlkBaseControl("Tree Item", secondTreeItem).GetValue().Trim();

                                if (treeItems[1].ToLower().Equals(secondTreeItemValue.ToLower()))
                                {
                                    if (CheckSecondTreeItemClosed(secondTreeItem))
                                    {
                                        secondTreeItem.Click(); //Click item if not closed
                                        DlkLogger.LogInfo("Clicking second level item: [" + secondTreeItemValue + "] ...");
                                    }
                                    else
                                    {
                                        DlkLogger.LogInfo("Second level item: [" + secondTreeItemValue + "] is already opened.");
                                    }

                                    if (treeItems.Length >= 3)
                                    {
                                        //Retrieve and select THIRD level of Tree items
                                        IWebElement thirdTreeContainer = GetThirdTreeContainer(secondTreeItem);
                                        IList<IWebElement> thirdTreeItems = GetThirdTreeItems(thirdTreeContainer);

                                        foreach (IWebElement thirdTreeItem in thirdTreeItems)
                                        {
                                            string thirdTreeItemValue = !String.IsNullOrEmpty(thirdTreeItem.Text) ? thirdTreeItem.Text.Trim() :
                                            new DlkBaseControl("Tree Item", thirdTreeItem).GetValue().Trim();

                                            if (treeItems[2].ToLower().Equals(thirdTreeItemValue.ToLower()))
                                            {
                                                thirdTreeItem.Click();
                                                DlkLogger.LogInfo("Clicking third level item: [" + thirdTreeItemValue + "] ...");
                                                tFound = true;
                                                break;
                                                //End of Tree View Level
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tFound = true;
                                        break;
                                    }
                                }
                                if (tFound)
                                    break;
                            }
                        }
                        else
                        {
                            tFound = true;
                            break;
                        }
                    }
                    if (tFound)
                        break;
                }

                if (!tFound)
                {
                    throw new Exception("Select() failed. [" + TreeItem + "] not found in Tree view list.");
                }

                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains")]
        public void SelectContains(String TreeItem)
        {
            try
            {
                Boolean tFound = false;
                Initialize();

                string[] treeItems = TreeItem.Split('~');

                //Retrieve and select FIRST level of Tree items
                IWebElement firstTreeContainer = GetFirstTreeContainer();
                IList<IWebElement> firstTreeItems = GetFirstTreeItems(firstTreeContainer);

                foreach (IWebElement firstTreeItem in firstTreeItems)
                {
                    string firstTreeItemValue = !String.IsNullOrEmpty(firstTreeItem.Text) ? firstTreeItem.Text.Trim() :
                        new DlkBaseControl("Tree Item", firstTreeItem).GetValue().Trim();

                    if (firstTreeItemValue.ToLower().Contains(treeItems[0].ToLower()))
                    {
                        if (CheckFirstTreeItemClosed(firstTreeItem))
                        {
                            firstTreeItem.Click(); //Click item if not closed
                            DlkLogger.LogInfo("Clicking first level item: [" + firstTreeItemValue + "] ...");
                        }
                        else
                        {
                            DlkLogger.LogInfo("First level item: [" + firstTreeItemValue + "] is already opened.");
                        }

                        if (treeItems.Length >= 2)
                        {
                            //Retrieve and select SECOND level of Tree items
                            IWebElement secondTreeContainer = GetSecondTreeContainer(firstTreeItem);
                            IList<IWebElement> secondTreeItems = GetSecondTreeItems(secondTreeContainer);

                            foreach (IWebElement secondTreeItem in secondTreeItems)
                            {
                                string secondTreeItemValue = !String.IsNullOrEmpty(secondTreeItem.Text) ? secondTreeItem.Text.Trim() :
                                new DlkBaseControl("Tree Item", secondTreeItem).GetValue().Trim();

                                if (secondTreeItemValue.ToLower().Contains(treeItems[1].ToLower()))
                                {
                                    if (CheckSecondTreeItemClosed(secondTreeItem))
                                    {
                                        secondTreeItem.Click(); //Click item if not closed
                                        DlkLogger.LogInfo("Clicking second level item: [" + secondTreeItemValue + "] ...");
                                    }
                                    else
                                    {
                                        DlkLogger.LogInfo("Second level item: [" + secondTreeItemValue + "] is already opened.");
                                    }

                                    if (treeItems.Length >= 3)
                                    {
                                        //Retrieve and select THIRD level of Tree items
                                        IWebElement thirdTreeContainer = GetThirdTreeContainer(secondTreeItem);
                                        IList<IWebElement> thirdTreeItems = GetThirdTreeItems(thirdTreeContainer);

                                        foreach (IWebElement thirdTreeItem in thirdTreeItems)
                                        {
                                            string thirdTreeItemValue = !String.IsNullOrEmpty(thirdTreeItem.Text) ? thirdTreeItem.Text.Trim() :
                                            new DlkBaseControl("Tree Item", thirdTreeItem).GetValue().Trim();

                                            if (thirdTreeItemValue.ToLower().Contains(treeItems[2].ToLower()))
                                            {
                                                thirdTreeItem.Click();
                                                DlkLogger.LogInfo("Clicking third level item: [" + thirdTreeItemValue + "] ...");
                                                tFound = true;
                                                break;
                                                //End of Tree View Level
                                            }
                                        }
                                    }
                                    else
                                    {
                                        tFound = true;
                                        break;
                                    }
                                }
                                if (tFound)
                                    break;
                            }
                        }
                        else
                        {
                            tFound = true;
                            break;
                        }
                    }
                    if (tFound)
                        break;
                }

                if (!tFound)
                {
                    throw new Exception("SelectContains() failed. [" + TreeItem + "] not found in Tree view list.");
                }

                DlkLogger.LogInfo("SelectContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
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