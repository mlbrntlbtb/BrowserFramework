using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using AjeraLib.DlkSystem;

namespace AjeraLib.DlkControls
{
    [ControlType("Expander")]
    class DlkExpander:DlkAjeraBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit;
        private List<DlkBaseControl> mlstHeader;
        private int retryLimit = 3;
        #endregion

        #region CONSTRUCTORS

        public DlkExpander(string ControlName, string SearchType, string SearchValue) 
            : base(ControlName, SearchType, SearchValue){}

        public DlkExpander(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues){}

        public DlkExpander(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}

        public DlkExpander(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkExpander(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector){}


        public void Initialize(string Header = "")
        {
            if (!IsInit)
            {
                mlstHeader = new List<DlkBaseControl>();
                FindElement();
                FindHeader(Header);
                IsInit = true;
            }
        }
       
        #endregion

        #region KEYWORDS

        [Keyword("Expand", new String[] { "HeaderName" })]
        public void Expand(String Header)
        {
            try
            {

                DlkBaseControl expanderButton = new DlkBaseControl(string.Empty, string.Empty, string.Empty);
                string elementContainer;

                if (mControlName.ToLower().Equals("chevron"))
                {
                    elementContainer = mSearchValues[0] + "//div[text()='" + Header +
                        "']//following-sibling::div[contains(@class,'chrome-menu-button')]//img[@src='Images/svg/icon/chevron-down.svg']";
                }
                else if (mControlName.ToLower().Equals("tabgroups"))
                {
                    elementContainer = mSearchValues[0] + "//div[text()='" + Header +
                        "']//following-sibling::div[contains(@class,'ax-main-header-tab-group-menu-button-position')]//img[@src='Images/svg/icon/chevron-down.svg']";
                }
                else
                {
                    elementContainer = mSearchValues[0] + "//label[text()='" + Header + "']//ancestor::div[1]";

                    try
                    {
                        //special scenario -- img or button changes to blue icon/ changes style when trying to click/hover
                        DlkBaseControl blueHoverButton = new DlkBaseControl("Expander Button", "XPATH", elementContainer + "//img[@src='Images/svg/icon/blue/arrow-right3.svg']");
                        blueHoverButton.FindElement();
                        elementContainer = elementContainer + "//img[@src='Images/svg/icon/arrow-right3.svg']/ancestor::span[1]";
                    }
                    catch
                    {
                        elementContainer = elementContainer + "//img[@src='Images/svg/icon/arrow-right3.svg']";
                    }
                }

                expanderButton = new DlkBaseControl("ExpanderButton", "XPATH_DISPLAY", elementContainer);

                int currRetry = 0;
                bool bFound = false;
                
                while (++currRetry <= retryLimit && !bFound)
                {
                    try
                    {
                        expanderButton.FindElement();
                        if (expanderButton.Exists())
                        {
                            expanderButton.MouseOver();
                            if (DlkEnvironment.mBrowser.ToLower() == "ie")
                            {
                                expanderButton.ClickUsingJavaScript();
                            }
                            else
                            {
                                expanderButton.Click(4.5);
                            }
                            Thread.Sleep(1000);
                            bFound = true;
                        }
                    }
                    catch (Exception)
                    {
                        IWebElement expanderElement = expanderButton.GetParent().FindElement(By.XPath(expanderButton.mSearchValues[0]));
                        new DlkBaseControl("expanderElement", expanderElement).ScrollIntoViewUsingJavaScript();
                        bFound = false;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Control : " + mControlName + "'s expander not found or header was already in expanded state.");
                }

            }
            catch (Exception e)
            {
                throw new Exception("Expand() failed : " + e.Message, e);
            }
        }

        [Keyword("Collapse", new String[] { "HeaderName" })]
        public void Collapse(String Header)
        {
            try
            {

                DlkBaseControl expanderButton = new DlkBaseControl(string.Empty, string.Empty, string.Empty);
                string elementContainer;

                if (mControlName.ToLower().Equals("chevron"))
                {
                    elementContainer =  mSearchValues[0] + "//div[contains(@class,'menu-detached')]//img[@src='Images/svg/icon/chevron-up.svg']";
                }
                else if (mControlName.ToLower().Equals("tabgroups"))
                {
                    elementContainer = mSearchValues[0] + "//div[text()='" + Header +
                        "']//following-sibling::div[contains(@class,'ax-main-header-tab-group-menu-button-position')]//img[@src='Images/svg/icon/chevron-up.svg']";
                }
                else
                {
                    elementContainer = mSearchValues[0] + "//label[text()='" + Header + "']//ancestor::div[1]";

                    try
                    {
                        //special scenario -- img or button changes to blue icon/ changes style when trying to click/hover
                         DlkBaseControl blueHoverButton = new DlkBaseControl("Expander Button", "XPATH", elementContainer +"//img[@src='Images/svg/icon/blue/arrow-down2.svg']");
                         blueHoverButton.FindElement();
                         elementContainer = elementContainer + "//img[@src='Images/svg/icon/arrow-down2.svg']/ancestor::span[1]";
                    }
                    catch
                    {
                        elementContainer = elementContainer + "//img[@src='Images/svg/icon/arrow-down2.svg']";
                    }
                }

               expanderButton = new DlkBaseControl("ExpanderButton", "XPATH_DISPLAY", elementContainer);

                int currRetry = 0;
                bool bFound = false;

                while (++currRetry <= retryLimit && !bFound)
                {
                    try
                    {
                        expanderButton.FindElement();
                        if (expanderButton.Exists())
                        {
                            expanderButton.MouseOver();
                            //expanderButton.Highlight(true);
                            if (DlkEnvironment.mBrowser.ToLower() == "ie")
                            {
                                expanderButton.ClickUsingJavaScript();
                            }
                            else
                            {
                                expanderButton.Click(4.5);
                            }
                            Thread.Sleep(1000);
                            bFound = true;
                        }
                    }
                    catch (Exception)
                    {
                        bFound = false;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Control : " + mControlName + "'s expander not found or header was already in collapsed state.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Collapse() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItem", new String[] { "HeaderName|ItemName" })]
        public void SelectItem(String Header, String Item)
        {
            try
            
            {
                Initialize(Header);
               // Expand(Header);

                int currRetry = 0;
                Boolean bFound = false;
                string actualItems = string.Empty;

                var actionItems = GetItems(Header);

                while (++currRetry <= retryLimit && !bFound)
                {
                    foreach (IWebElement aListItem in actionItems)
                    {
                        var dlkTreeItem = new DlkBaseControl("Expander Item", aListItem);
                        actualItems = actualItems + dlkTreeItem.GetValue() + " ";
                        if (dlkTreeItem.GetValue().ToLower() == Item.ToLower())
                        {
                            dlkTreeItem.MouseOverUsingJavaScript();
                            dlkTreeItem.ClickUsingJavaScript();
                            Thread.Sleep(1000);

                            bFound = true;
                            break;
                        }
                    }
                }

                if (!bFound)
                {
                    throw new Exception( Item + " not found in list. : Actual List = " + actualItems);
                }

            }
            catch (Exception e)
            {
                throw new Exception("SelectItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectHeader", new String[] { "HeaderName|ItemName" })]
        public void SelectHeader(String Header)
        {
            try
            {
                int currRetry = 0;
                Boolean bFound = false;
                FindElement();

                IList<IWebElement> headerList;
                var headerContainter = string.Empty;
                if (mControlName.ToLower().Equals("chevron"))
                {
                    headerContainter = mSearchValues[0] + "//div[contains(text(),'" + Header + "')]";
                }
                else if (mControlName.ToLower().Equals("columns"))
                {
                    headerContainter = mSearchValues[0] + "/..//span[contains(@class,'ax-uicontrol-imagebutton-padding')]//label[text()='" + Header + "']";
                }
                else
                {
                    headerContainter = mSearchValues[0] + "//label[text()='" + Header + "']";
                }

                headerList = mElement.FindElements(By.XPath(headerContainter));
                while (++currRetry <= retryLimit && !bFound)
                {
                    foreach (IWebElement header in headerList)
                    {
                        DlkBaseControl headerBaseControl = new DlkBaseControl("Expander", header);
                        headerBaseControl.MouseOver();
                        header.Click();
                        Thread.Sleep(1000);

                        bFound = true;
                        break;

                    }
                }
                

                if (!bFound)
                {
                    throw new Exception("SelectHeader() : Header [" + Header + "] not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectItem() failed : " + e.Message, e);
            }
        }

        //this will select the item by description

        [Keyword("SelectByDescription", new String[] { "HeaderName|ItemName" })]
        public void SelectByDescription(String Header, String Description)
        {
            try
            {
                Initialize(Header);

                int currRetry = 0;
                Boolean bFound = false;
                string actualItems = string.Empty;

                var actionItems = GetItems(Header);

                while (++currRetry <= retryLimit && !bFound)
                {
                    foreach (IWebElement aListItem in actionItems)
                    {
                        string itemTitle = aListItem.FindElement(By.XPath(".//div[@class='ax-gallerylist-item-description']")).Text;
                        if (itemTitle == Description)
                        {
                            var dlkTreeItem = new DlkBaseControl("Expander Item", aListItem);
                            dlkTreeItem.MouseOver();
                            dlkTreeItem.Click();
                            Thread.Sleep(1000);

                            bFound = true;
                            break;
                        }
                    }
                }

                if (!bFound)
                {
                    throw new Exception("SelectByDescription() failed: Description not found in list.");
                }

            }
            catch (Exception e)
            {
                throw new Exception("SelectByDescription() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByTitle", new String[] { "HeaderName|Title" })]
        public void SelectByTitle(String Header, String Title)
        {
            try
            {
                Initialize(Header);

                if (!IsExpanded(Header))
                {
                    Expand(Header);
                }

                int currRetry = 0;
                Boolean bFound = false;

                var actionItems = GetItems(Header);

                while (++currRetry <= retryLimit && !bFound)
                {
                    foreach (IWebElement aListItem in actionItems)
                    {
                        string currTitle = aListItem.FindElement(By.XPath(".//div[@class='ax-gallerylist-item-title']")).Text;
                        currTitle = currTitle.Remove(currTitle.LastIndexOf(' '));

                        if (currTitle == Title)
                        {
                            var dlkTreeItem = new DlkBaseControl("Expander Item", aListItem);
                            dlkTreeItem.MouseOver();
                            dlkTreeItem.Click();
                            Thread.Sleep(1000);

                            bFound = true;
                            break;
                        }
                        
                    }
                }

                if (!bFound)
                {
                    throw new Exception(Title + " not found in list.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectByTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "HeaderName|ItemName" })]
        public void SelectByIndex(String Header, String Index)
        {
            try
            {
                Initialize(Header);

                IList<IWebElement> itemList = new List<IWebElement>();
                int index = int.Parse(Index) - 1;
                var actionItems = GetItems(Header);
                var dlkTreeItem = new DlkBaseControl("Expander Item", actionItems[index]);
                
                dlkTreeItem.MouseOver();
                dlkTreeItem.Click();
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemExists", new String[] { "HeaderName|ItemName" })]
        public void VerifyItemExists(String Header, String Item, String IsTrueOrFalse)
        {
            try
            {
                Initialize(Header);
                if(!IsExpanded(Header))
                {
                    Expand(Header);
                }

                int currRetry = 0;
                Boolean bFound = false;
                string actualItems = string.Empty;

                var actionItems = GetItems(Header);

                while (++currRetry <= retryLimit && !bFound)
                {
                    foreach (IWebElement aListItem in actionItems)
                    {
                        var dlkTreeItem = new DlkBaseControl("Expander Item", aListItem);
                        actualItems = actualItems + dlkTreeItem.GetValue() + " ";
                        if (dlkTreeItem.GetValue().ToLower() == Item.ToLower())
                        {
                            bFound = true;
                            break;
                        }
                    }
                }

                if (IsExpanded(Header))
                {
                    Collapse(Header);
                }
                if (bFound == Convert.ToBoolean(IsTrueOrFalse))
                {
                    DlkLogger.LogInfo("VerifyItemExists() passed: Actual = " + Convert.ToString(bFound) + " : Expected = " + IsTrueOrFalse);
                }
                else
                {
                    DlkLogger.LogInfo("VerifyItemExists() failed: Actual = " + Convert.ToString(bFound) + " : Expected = " + IsTrueOrFalse);
                    throw new Exception("VerifyItemExists() failed. Control : " + mControlName + " : '" + Item +
                                       "' not found in list. : Actual List = " + actualItems);
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "TRUE|FALSE" })]
        public void VerifyExists(String Header, String IsTrueOrFalse)
        {
            try
            {

                DlkBaseControl expanderButton = new DlkBaseControl(string.Empty, string.Empty, string.Empty);
                DlkBaseControl collapserButton = new DlkBaseControl(string.Empty, string.Empty, string.Empty);
                if (mControlName.ToLower().Equals("chevron"))
                {

                    expanderButton = new DlkBaseControl("ExpanderButton", "XPATH",
                        mSearchValues[0] + "//div[text()='" + Header +
                        "']//following-sibling::div[contains(@class,'chrome-menu-button')]//img[@src='Images/svg/icon/chevron-down.svg']");
                    collapserButton = new DlkBaseControl("ExpanderButton", "XPATH",
                       mSearchValues[0] + "//div[contains(text(),'" + Header +
                       "')]//following-sibling::div[contains(@class,'chrome-menu-button')]//img[@src='Images/svg/icon/chevron-up.svg']");

                }
                else if (mControlName.ToLower().Equals("tabgroups"))
                {
                    expanderButton = new DlkBaseControl("ExpanderButton", "XPATH",
                        mSearchValues[0] + "//div[text()='" + Header +
                        "']//following-sibling::div[contains(@class,'ax-main-header-tab-group-menu-button-position')]");
                    collapserButton = new DlkBaseControl("ExpanderButton", "XPATH",
                       mSearchValues[0] + "//div[contains(text(),'" + Header +
                       "')]//following-sibling::div[contains(@class,'ax-main-header-tab-group-menu-button-position')]");
                }
                else
                {
                    expanderButton = new DlkBaseControl("ExpanderButton", "XPATH", mSearchValues[0] + "//label[text()='" + Header + "']//ancestor::div[1]//img[@src='Images/svg/icon/arrow-right3.svg']/ancestor::span[1]");
                    collapserButton = new DlkBaseControl("ExpanderButton", "XPATH", mSearchValues[0] + "//label[text()='" + Header + "']//ancestor::div[1]//img[@src='Images/svg/icon/arrow-down2.svg']/ancestor::span[1]");

                }

                if (expanderButton.Exists())
                {
                    expanderButton.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                }
                else
                {
                    collapserButton.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                    //if (collapserButton.Exists())
                    //{
                    //    Collapse(Header);
                    //}
                }
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyByDescription", new String[] { "HeaderName|ItemName" })]
        public void VerifyByDescription(String Category, String Title, String Description, String IsTrueOrFalse)
        {
            try
            {
                Initialize(Category);
                string actualItem = string.Empty;
                var actionItems = GetItems(Category);
                foreach (IWebElement aListItem in actionItems)
                {
                    string itemTitle = aListItem.FindElement(By.XPath(".//div[@class='ax-gallerylist-item-title']")).Text;
                    if (itemTitle == Title)
                    {
                        actualItem = aListItem.FindElement(By.XPath(".//following-sibling::div[@class='ax-gallerylist-item-description']")).Text;
                        Boolean actualValue = Convert.ToBoolean(actualItem.Equals(Description));
                        DlkAssert.AssertEqual("VerifyByDescription(): ", Convert.ToBoolean(IsTrueOrFalse), actualValue);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyByDescription() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyByTitle", new String[] { "HeaderName|Title" })]
        public void VerifyByTitle(String Header, String Title, String IsTrueOrFalse)
        {
            try
            {
                Initialize(Header);
                if (!IsExpanded(Header))
                {
                    Expand(Header);
                }

                int currRetry = 0;
                Boolean bFound = false;
                string actualTitles = string.Empty;

                var actionItems = GetItems(Header);

                while (++currRetry <= retryLimit && !bFound)
                {
                    foreach (IWebElement aListItem in actionItems)
                    {
                        string currTitle = aListItem.FindElement(By.XPath(".//div[@class='ax-gallerylist-item-title']")).Text;
                        
                        
                        //Remove the widget count
                        currTitle = currTitle.Remove(currTitle.LastIndexOf(' '));
                        actualTitles += currTitle + "; ";
                        if (currTitle.Trim().ToLower() == Title.Trim().ToLower())
                        {
                            bFound = true;
                            break;
                        }
                            
                    }
                }

                if (bFound == Convert.ToBoolean(IsTrueOrFalse))
                {
                    DlkLogger.LogInfo("VerifyByTitle() passed: Actual = " + Convert.ToString(bFound) + " : Expected = " + IsTrueOrFalse);
                }
                else
                {
                    DlkLogger.LogInfo("VerifyByTitle() failed: Actual = " + Convert.ToString(bFound) + " : Expected = " + IsTrueOrFalse);
                    throw new Exception("VerifyByTitle() failed. Control : " + mControlName + " : '" + Title +
                                       "' not found in list. : Actual List = " + actualTitles);
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyByTitle() failed : " + e.Message, e);
            }
        }


        #endregion

        #region METHODS

        private void FindHeader(string Header)
        {
            IList<IWebElement> lstHeaderElements;
            var headerContainter = string.Empty;
            if (mControlName.ToLower().Equals("chevron"))
            {
                headerContainter = mSearchValues[0] + "//div[contains(text(),'" + Header + "')]";
            }
            else
            {
                headerContainter = mSearchValues[0] + "//label[text()='" + Header + "']";
            }

            lstHeaderElements = mElement.FindElements(By.XPath(headerContainter));

            foreach (IWebElement widgetElement in lstHeaderElements.Where(item => !string.IsNullOrEmpty(item.Text)))
            {
                mlstHeader.Add(new DlkBaseControl("Expander Header", widgetElement));
            }
        }

        private IList<IWebElement> GetItems(string header)
        {
            const string tableExpanderClass = "parentofgroup";
            string childPos = "1";
            IList<IWebElement> actionItems = null;

            try
            {
                IWebElement actionMenu;
                string elementContainer;
                if (mSearchValues[0].Contains(tableExpanderClass))
                {
                    //Expander in Table
                    elementContainer = mSearchValues[0] + "//label[text()='" + header + "']/ancestor::tr";
                    actionMenu = mElement.FindElement(By.XPath(elementContainer));
                    var mClassname = actionMenu.GetAttribute("class");
                    childPos = mClassname.Substring(mClassname.LastIndexOf(tableExpanderClass) + (tableExpanderClass + "-").Length, 1);
                }
                else if (mControlName.ToLower().Equals("chevron"))
                {
                    //expander as menu
                    elementContainer = mSearchValues[0] + "//div[contains(@class,'ax-corner-tl ax-widget-chrome-menu-detached')]";
                }
                else if (mControlName.ToLower().Equals("tabgroups"))
                {
                    //expander as menu
                    elementContainer = mSearchValues[0] + "//div[contains(@class,'ax-main-header-tab-group-menu-button-position')]/preceding-sibling::div[contains(@class,'ax-main-header-tab-group-menu-position')]";
                }
                else
                {
                    // common expander
                    elementContainer = mSearchValues[0] + "//label[text()='" + header + "']/ancestor::div[1]/following-sibling::div";
                
                }

                actionMenu = mElement.FindElement(By.XPath(elementContainer));
                if (!actionMenu.Displayed)
                {
                    mElms = DlkEnvironment.AutoDriver.FindElements(By.XPath(elementContainer));
                    foreach (IWebElement mElm in mElms)
                    {
                        if (mElm.GetCssValue("display") != "none")
                        {
                            actionMenu = mElm;
                            break;
                        }
                    }
                }
                
                actionItems = actionMenu.FindElements(mSearchValues[0].Contains(tableExpanderClass)
                    ? By.XPath("./following-sibling::tr[contains(@class,'childofgroup-" + childPos + "')]/td[2]//label")
                    : By.CssSelector("div"));

                if (mControlName.ToLower().Equals("availabletasklist"))
                {
                    for (int i = 0; i <= actionItems.Count; i++)
                    {
                        if (actionItems[i].Text == "")
                        {
                            actionItems = actionMenu.FindElements(By.XPath(".//span[contains(@class,'column-wrapper')]//ul//li[contains(@class,'menu')]"));
                            break;
                        }
                    }
                }

                if (mControlName.ToLower().Equals("galleryitem"))
                {
                        actionItems = actionMenu.FindElements(By.XPath(".//div[contains(@id, 'galleryitem')]"));
                }
            }
            catch 
            {
                
            }
            
            return actionItems;
        }

        private bool IsExpanded(string Header)
        {
            DlkBaseControl expanderButton = new DlkBaseControl(string.Empty, string.Empty, string.Empty);
            string elementContainer;

            if (mControlName.ToLower().Equals("chevron"))
            {
                elementContainer = mSearchValues[0] + "//div[text()='" + Header +
                    "']//following-sibling::div[contains(@class,'chrome-menu-button')]//img[@src='Images/svg/icon/chevron-down.svg']";
            }
            else if (mControlName.ToLower().Equals("tabgroups"))
            {
                elementContainer = mSearchValues[0] + "//div[text()='" + Header +
                    "']//following-sibling::div[contains(@class,'ax-main-header-tab-group-menu-button-position')]//img[@src='Images/svg/icon/chevron-down.svg']";
            }
            else
            {
                elementContainer = mSearchValues[0] + "//label[text()='" + Header + "']//ancestor::div[1]";

                try
                {
                    //special scenario -- img or button changes to blue icon/ changes style when trying to click/hover
                    DlkBaseControl blueHoverButton = new DlkBaseControl("Expander Button", "XPATH", elementContainer + "//img[@src='Images/svg/icon/blue/arrow-right3.svg']");
                    blueHoverButton.FindElement();
                    elementContainer = elementContainer + "//img[@src='Images/svg/icon/arrow-right3.svg']/ancestor::span[1]";
                }
                catch
                {
                    elementContainer = elementContainer + "//img[@src='Images/svg/icon/arrow-right3.svg']";
                }
            }

            expanderButton = new DlkBaseControl("ExpanderButton", "XPATH", elementContainer);

            int currRetry = 0;
         
            while (++currRetry <= retryLimit)
            {
                if (expanderButton.Exists(1))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
