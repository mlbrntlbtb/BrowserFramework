using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using WorkBookLib.DlkSystem;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;

namespace WorkBookLib.DlkControls
{
    [ControlType("SideBar")]
    public class DlkSideBar : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private IList<IWebElement> mSidebarItems;
        private IList<IWebElement> mSidebarHeaders;
        private static string mTabItemsXPath = ".//*[contains(@class,'listboxItem')]";
        private static string mTabItems2XPath = ".//li[contains(@class,'Entry Clickable')]";
        private static string mHeaderTabItemsXPath = ".//*[contains(@class,'ListBoxGroupHeader')]//span[contains(@class,'headerText')]";
        private static string mHeaderTabItems2XPath = ".//*[contains(@class,'ListBoxGroupHeader')]//div[contains(@class,'headerContainer')]";
        private static string mTabExpandXPath = ".//ancestor::li[contains(@class,'ListBoxGroupHeader')]//div[contains(@class,'Plus')]";
        
        #endregion

        #region CONSTRUCTORS
        public DlkSideBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSideBar(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            GetSideBarItems();
        }
        //public string GetControlValue(IWebElement item, String controlName) {
        //    String value = "";

        //    value = new DlkBaseControl(controlName, item).GetValue().Trim();
        //    if (!new Regex("[@#^\":{}|<>=]").IsMatch(value) && !double.TryParse(value, out double x)) {
        //        return value;
        //    }
        //    value = item.GetAttribute("title");
        //    if ((value != "") && (value != null)) {
        //        return value;
        //    }
        //    value = item.GetAttribute("placeholder");
        //    if ((value != "") && (value != null)) {
        //        return value;
        //    }
        //    return "";
        //}
        #endregion

        #region PRIVATE METHODS
        private void OpenAllSidebarHeader() {
            foreach (IWebElement header in mSidebarHeaders) {
                DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0) {
                    mSideBarHeader.Click();
                    GetSideBarItems();
                }
            }
        }

        private void GetSideBarItems()
        {
            mSidebarItems = mElement.FindElements(By.XPath(mTabItemsXPath)).Count > 0 ?
                mElement.FindElements(By.XPath(mTabItemsXPath))
                .Where(x => x.Displayed).ToList() :
                mElement.FindElements(By.XPath(mTabItems2XPath)).Count > 0 ?
                mElement.FindElements(By.XPath(mTabItems2XPath))
                .Where(x => x.Displayed).ToList() :
                mElement.FindElements(By.TagName("li"))
                .Where(x => x.Displayed).ToList();
            mSidebarHeaders = mElement.FindElements(By.XPath(mHeaderTabItemsXPath))
                .Where(x => x.Displayed).ToList();

            if(mSidebarHeaders.Count == 0)
            {
                mSidebarHeaders = mElement.FindElements(By.XPath(mHeaderTabItems2XPath))
               .Where(x => x.Displayed).ToList();
            }
        }

        private string GetSideBarItemsToString() {
            String sideBarItems = "";

            foreach (IWebElement sideBarItem in mSidebarItems) 
            {
                string currentItemValue = !String.IsNullOrEmpty(sideBarItem.Text.Trim()) ?
                    sideBarItem.Text.Trim() : new DlkBaseControl("SideBar Item", sideBarItem).GetValue();
                sideBarItems += "~" + DlkString.RemoveCarriageReturn(currentItemValue);
            }
            return sideBarItems.Trim('~');
        }

        private void ClickItemButton(IWebElement sideBarItemElement, String buttonCaption)
        {
            string targetButtonXPath = ".//*[contains(@title,'" + buttonCaption + "')]";
            IWebElement targetButton = sideBarItemElement.FindElements(By.XPath(targetButtonXPath)).Count > 0 ?
                sideBarItemElement.FindElement(By.XPath(targetButtonXPath)) : throw new Exception("Button [" + buttonCaption + "] not found.");
            targetButton.Click();
            DlkLogger.LogInfo("Executing click on button [" + buttonCaption + "]... ");
        }
        #endregion

        #region KEYWORDS

        [Keyword("VerifySidebarItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySidebarItems(String ExpectedValue) {
            try {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                OpenAllSidebarHeader();
                DlkAssert.AssertEqual("VerifySidebarItems(): ", ExpectedValue.Trim(), GetSideBarItemsToString());
                DlkLogger.LogInfo("VerifySidebarItems() passed");
            } catch (Exception e) {
                throw new Exception("VerifySidebarItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetSidebarItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetSidebarItems(String VariableName) {
            try {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                OpenAllSidebarHeader();
                String ActualValue = GetSideBarItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetSidebarItems() passed");
            } catch (Exception e) {
                throw new Exception("GetSidebarItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetSidebarItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetSidebarItemCount(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                OpenAllSidebarHeader();
                int ActualValue = mSidebarHeaders.Count;
                DlkVariable.SetVariable(VariableName, ActualValue.ToString());
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetSidebarItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetSidebarItems() failed : " + e.Message, e);
            }
        }

        [Keyword("Select")]
        public void Select(String Item)
        {
            try
            {
                Boolean iFound = false;
                string ActualSideBarItems = "";

                Initialize();

                if (!Item.Contains('~'))
                {
                    foreach (IWebElement item in mSidebarItems)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                        ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (currentItem == Item.ToLower())
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(item).Click().Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Selecting sidebar item [" + Item + "] ...");
                            break;
                        }
                    }
                }
                else
                {
                    string[] items = Item.Split('~');
                    foreach (IWebElement header in mSidebarHeaders)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                        ActualSideBarItems = ActualSideBarItems + mSideBarHeader.GetValue() + "~";

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (currentHeader == items[0].ToLower())
                        {
                            if(mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header [" + items[0] + "] ...");
                                GetSideBarItems();
                            }else
                                DlkLogger.LogInfo("Sidebar header [" + items[0] + "] already opened ...");

                            foreach (IWebElement item in mSidebarItems)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                                ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                                string currentItem2 = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();
                                if (currentItem2 == items[1].ToLower())
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(item).Perform();
                                    item.Click();
                                    iFound = true;
                                    DlkLogger.LogInfo("Selecting sidebar item [" + items[1] + "] ...");
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("Select() failed. '" + Item + "' not found. Actual sidebar items: " + ActualSideBarItems);
                }

                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains")]
        public void SelectContains(String PartialValue)
        {
            try
            {
                Boolean iFound = false;
                string ActualSideBarItems = "";

                Initialize();

                if (!PartialValue.Contains('~'))
                {
                    foreach (IWebElement item in mSidebarItems)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                        ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (currentItem.Contains(PartialValue.ToLower()))
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(item).Click().Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Selecting sidebar item [" + PartialValue + "] ...");
                            break;
                        }
                    }
                }
                else
                {
                    string[] items = PartialValue.Split('~');
                    foreach (IWebElement header in mSidebarHeaders)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                        ActualSideBarItems = ActualSideBarItems + mSideBarHeader.GetValue() + "~";

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (currentHeader.Contains(items[0].ToLower()))
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header [" + items[0] + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + items[0] + "] already opened ...");

                            foreach (IWebElement item in mSidebarItems)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                                ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                                string currentItem2 = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();
                                if (currentItem2.Contains(items[1].ToLower()))
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(item).Click().Perform();
                                    iFound = true;
                                    DlkLogger.LogInfo("Selecting sidebar item [" + items[1] + "] ...");
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("SelectContains() failed. '" + PartialValue + "' not found. Actual sidebar items: " + ActualSideBarItems);
                }

                DlkLogger.LogInfo("SelectContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex")]
        public void SelectByIndex(String Index)
        {
            try
            {
                Boolean iFound = false;
                
                Initialize();

                if (!Index.Contains('~'))
                {
                    int index = Convert.ToInt32(Index) - 1;

                    for(int i=0; i < mSidebarItems.Count; i++)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (i == index)
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(mSidebarItems[i]).Click().Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Selecting sidebar item index [" + Index + "] with value: [" + currentItem + "] ...");
                            break;
                        }
                    }
                }
                else
                {
                    string[] indexes = Index.Split('~');
                    int headerIndex = Convert.ToInt32(indexes[0]) - 1;
                    int itemIndex = Convert.ToInt32(indexes[1]) - 1;

                    for (int h = 0; h < mSidebarHeaders.Count; h++)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", mSidebarHeaders[h]);

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (h == headerIndex)
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header index [" + indexes[0].ToString() + "] with value: [" + currentHeader + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + currentHeader + "] already opened ...");
                            
                            for (int i = 0; i < mSidebarItems.Count; i++)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                                string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                                if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                                    String.IsNullOrEmpty(currentItem))
                                    currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                        DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                                if (i == itemIndex)
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(mSidebarItems[i]).Click().Perform();
                                    iFound = true;
                                    DlkLogger.LogInfo("Selecting sidebar item index [" + indexes[1].ToString() + "] with value: [" + currentItem + "] ...");
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("SelectByIndex() failed. Item with index [" + Index + "] not found. Total sidebar items [" + mSidebarItems.Count.ToString() + "]");
                }

                DlkLogger.LogInfo("SelectByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickItemButtonByIndex")]
        public void ClickItemButtonByIndex(String ButtonCaption, String Index)
        {
            try
            {
                Boolean iFound = false;

                Initialize();

                if (!Index.Contains('~'))
                {
                    int index = Convert.ToInt32(Index) - 1;

                    for (int i = 0; i < mSidebarItems.Count; i++)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (i == index)
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(mSidebarItems[i]).Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Item index [" + Index + "] with value: [" + currentItem + "] found...");
                            ClickItemButton(mSidebarItems[i], ButtonCaption);
                            break;
                        }
                    }
                }
                else
                {
                    string[] indexes = Index.Split('~');
                    int headerIndex = Convert.ToInt32(indexes[0]) - 1;
                    int itemIndex = Convert.ToInt32(indexes[1]) - 1;

                    for (int h = 0; h < mSidebarHeaders.Count; h++)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", mSidebarHeaders[h]);

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (h == headerIndex)
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header index [" + indexes[0].ToString() + "] with value: [" + currentHeader + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + currentHeader + "] already opened ...");

                            for (int i = 0; i < mSidebarItems.Count; i++)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                                string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                                if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                                    String.IsNullOrEmpty(currentItem))
                                    currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                        DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                                if (i == itemIndex)
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(mSidebarItems[i]).Perform();
                                    iFound = true;
                                    DlkLogger.LogInfo("Item index [" + indexes[1].ToString() + "] with value: [" + currentItem + "] found...");
                                    ClickItemButton(mSidebarItems[i], ButtonCaption);
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("ClickItemButtonByIndex() failed. Item with index [" + Index + "] not found. Total sidebar items [" + mSidebarItems.Count.ToString() + "]");
                }

                DlkLogger.LogInfo("ClickItemButtonByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemButtonByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickItemButtonByValue")]
        public void ClickItemButtonByValue(String ButtonCaption, String PartialValue)
        {
            try
            {
                Boolean iFound = false;
                string ActualSideBarItems = "";

                Initialize();

                if (!PartialValue.Contains('~'))
                {
                    foreach (IWebElement item in mSidebarItems)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                        ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (currentItem.Contains(PartialValue.ToLower()))
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(item).Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Sidebar item: [" + PartialValue + "] found...");
                            ClickItemButton(item, ButtonCaption);
                            break;
                        }
                    }
                }
                else
                {
                    string[] items = PartialValue.Split('~');
                    foreach (IWebElement header in mSidebarHeaders)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                        ActualSideBarItems = ActualSideBarItems + mSideBarHeader.GetValue() + "~";

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (currentHeader.Contains(items[0].ToLower()))
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header [" + items[0] + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + items[0] + "] already opened ...");

                            foreach (IWebElement item in mSidebarItems)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                                ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                                string currentItem2 = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();
                                if (currentItem2.Contains(items[1].ToLower()))
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(item).Perform();
                                    iFound = true;
                                    DlkLogger.LogInfo("Sidebar item: [" + items[1] + "] found...");
                                    ClickItemButton(item, ButtonCaption);
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("ClickItemButtonByValue() failed. '" + PartialValue + "' not found. Actual sidebar items: " + ActualSideBarItems);
                }

                DlkLogger.LogInfo("ClickItemButtonByValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemButtonByValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetIndexByItemValue")]
        public void GetIndexByItemValue(String VariableName, String ItemValue)
        {
            try
            {
                Boolean iFound = false;
                string ActualSideBarItems = "";

                Initialize();

                if (!ItemValue.Contains('~'))
                {
                    int itemIndex = 0;
                    foreach (IWebElement item in mSidebarItems)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                        ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (currentItem == ItemValue.ToLower())
                        {
                            itemIndex++;
                            DlkVariable.SetVariable(VariableName, itemIndex.ToString());
                            DlkLogger.LogInfo("[" + itemIndex.ToString() + "] index value set to Variable: [" + VariableName + "]");
                            iFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    string[] items = ItemValue.Split('~');
                    int headerIndex = 0;
                    foreach (IWebElement header in mSidebarHeaders)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                        ActualSideBarItems = ActualSideBarItems + mSideBarHeader.GetValue() + "~";

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (currentHeader == items[0].ToLower())
                        {
                            headerIndex++;
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header [" + items[0] + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + items[0] + "] already opened ...");

                            int itemIndex = 0;
                            foreach (IWebElement item in mSidebarItems)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                                ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                                string currentItem2 = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();
                                if (currentItem2 == items[1].ToLower())
                                {
                                    itemIndex++;
                                    DlkVariable.SetVariable(VariableName, headerIndex.ToString() + "~" + itemIndex.ToString());
                                    DlkLogger.LogInfo("[" + itemIndex.ToString() + "] index value set to Variable: [" + VariableName + "]");
                                    iFound = true;
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("GetIndexByItemValue() failed. '" + ItemValue + "' not found. Actual sidebar items: " + ActualSideBarItems);
                }

                DlkLogger.LogInfo("GetIndexByItemValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetIndexByItemValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetIndexByPartialItemValue")]
        public void GetIndexByPartialItemValue(String VariableName, String PartialValue)
        {
            try
            {
                Boolean iFound = false;
                string ActualSideBarItems = "";

                Initialize();

                if (!PartialValue.Contains('~'))
                {
                    int itemIndex = 0;
                    foreach (IWebElement item in mSidebarItems)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                        ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (currentItem.Contains(PartialValue.ToLower()))
                        {
                            itemIndex++;
                            DlkVariable.SetVariable(VariableName, itemIndex.ToString());
                            DlkLogger.LogInfo("[" + itemIndex.ToString() + "] index value set to Variable: [" + VariableName + "]");
                            iFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    string[] items = PartialValue.Split('~');
                    int headerIndex = 0;
                    foreach (IWebElement header in mSidebarHeaders)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                        ActualSideBarItems = ActualSideBarItems + mSideBarHeader.GetValue() + "~";

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (currentHeader.Contains(items[0].ToLower()))
                        {
                            headerIndex++;
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header [" + items[0] + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + items[0] + "] already opened ...");

                            int itemIndex = 0;
                            foreach (IWebElement item in mSidebarItems)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                                ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                                string currentItem2 = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();
                                if (currentItem2.Contains(items[1].ToLower()))
                                {
                                    itemIndex++;
                                    DlkVariable.SetVariable(VariableName, headerIndex.ToString() + "~" + itemIndex.ToString());
                                    DlkLogger.LogInfo("[" + itemIndex.ToString() + "] index value set to Variable: [" + VariableName + "]");
                                    iFound = true;
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("GetIndexByPartialItemValue() failed. '" + PartialValue + "' not found. Actual sidebar items: " + ActualSideBarItems);
                }

                DlkLogger.LogInfo("GetIndexByPartialItemValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetIndexByPartialItemValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetSidebarItemByIndex")]
        public void GetSidebarItemByIndex(String VariableName, String Index)
        {
            try
            {
                Boolean iFound = false;

                Initialize();

                if (!Index.Contains('~'))
                {
                    int index = Convert.ToInt32(Index) - 1;

                    for (int i = 0; i < mSidebarItems.Count; i++)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (i == index)
                        {
                            string currentSidebarItemValue = !String.IsNullOrEmpty(mSidebarItems[i].Text.Trim()) ?
                                    mSidebarItems[i].Text.Trim() : new DlkBaseControl("SideBar Item", mSidebarItems[i]).GetValue();

                            DlkVariable.SetVariable(VariableName, DlkString.RemoveCarriageReturn(currentSidebarItemValue));
                            DlkLogger.LogInfo("[" + currentSidebarItemValue + "] value set to Variable: [" + VariableName + "]");
                            iFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    string[] indexes = Index.Split('~');
                    int headerIndex = Convert.ToInt32(indexes[0]) - 1;
                    int itemIndex = Convert.ToInt32(indexes[1]) - 1;

                    for (int h = 0; h < mSidebarHeaders.Count; h++)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", mSidebarHeaders[h]);

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (h == headerIndex)
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header index [" + headerIndex.ToString() + "] with value: [" + currentHeader + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + currentHeader + "] already opened ...");

                            for (int i = 0; i < mSidebarItems.Count; i++)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                                string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                                if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                                    String.IsNullOrEmpty(currentItem))
                                    currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                        DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                                if (i == itemIndex)
                                {
                                    string currentSidebarItemValue = !String.IsNullOrEmpty(mSidebarItems[i].Text.Trim()) ?
                                        mSidebarItems[i].Text.Trim() : new DlkBaseControl("SideBar Item", mSidebarItems[i]).GetValue();

                                    DlkVariable.SetVariable(VariableName, DlkString.RemoveCarriageReturn(currentSidebarItemValue));
                                    DlkLogger.LogInfo("[" + currentSidebarItemValue + "] value set to Variable: [" + VariableName + "]");
                                    iFound = true;
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("GetSidebarItemByIndex() failed. Item with index [" + Index + "] not found. Total sidebar items [" + mSidebarItems.Count.ToString() + "]");
                }

                DlkLogger.LogInfo("GetSidebarItemByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySidebarItemByIndex")]
        public void VerifySidebarItemByIndex(String ExpectedValue, String Index)
        {
            try
            {
                Boolean iFound = false;

                Initialize();

                if (!Index.Contains('~'))
                {
                    int index = Convert.ToInt32(Index) - 1;

                    for (int i = 0; i < mSidebarItems.Count; i++)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (i == index)
                        {
                            string currentSidebarItemValue = !String.IsNullOrEmpty(mSidebarItems[i].Text.Trim()) ?
                                    mSidebarItems[i].Text.Trim() : new DlkBaseControl("SideBar Item", mSidebarItems[i]).GetValue();

                            DlkAssert.AssertEqual("VerifySidebarItemByIndex() :", ExpectedValue.ToLower(), DlkString.RemoveCarriageReturn(currentSidebarItemValue).ToLower());
                            iFound = true;
                            break;
                        }
                    }
                }
                else
                {
                    string[] indexes = Index.Split('~');
                    int headerIndex = Convert.ToInt32(indexes[0]) - 1;
                    int itemIndex = Convert.ToInt32(indexes[1]) - 1;

                    for (int h = 0; h < mSidebarHeaders.Count; h++)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", mSidebarHeaders[h]);

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (h == headerIndex)
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header index [" + headerIndex.ToString() + "] with value: [" + currentHeader + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + currentHeader + "] already opened ...");

                            for (int i = 0; i < mSidebarItems.Count; i++)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                                string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                                if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                                    String.IsNullOrEmpty(currentItem))
                                    currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                        DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                                if (i == itemIndex)
                                {
                                    string currentSidebarItemValue = !String.IsNullOrEmpty(mSidebarItems[i].Text.Trim()) ?
                                    mSidebarItems[i].Text.Trim() : new DlkBaseControl("SideBar Item", mSidebarItems[i]).GetValue();

                                    DlkAssert.AssertEqual("VerifySidebarItemByIndex() :", ExpectedValue.ToLower(), DlkString.RemoveCarriageReturn(currentSidebarItemValue).ToLower());
                                    iFound = true;
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("VerifySidebarItemByIndex() failed. Item with index [" + Index + "] not found. Total sidebar items [" + mSidebarItems.Count.ToString() + "]");
                }

                DlkLogger.LogInfo("VerifySidebarItemByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickItem")]
        public void DoubleClickItem(String Item)
        {
            try
            {
                Boolean iFound = false;
                string ActualSideBarItems = "";
                
                Initialize();

                if (!Item.Contains('~'))
                {
                    foreach (IWebElement item in mSidebarItems)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                        ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";


                        if (currentItem == Item.ToLower())
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(item).Click().Perform();
                            action.DoubleClick(item).Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Selecting sidebar item [" + Item + "] ...");
                            break;
                        }
                    }
                }
                else
                {
                    string[] items = Item.Split('~');
                    foreach (IWebElement header in mSidebarHeaders)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                        ActualSideBarItems = ActualSideBarItems + mSideBarHeader.GetValue() + "~";

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (currentHeader == items[0].ToLower())
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header [" + items[0] + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + items[0] + "] already opened ...");

                            foreach (IWebElement item in mSidebarItems)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                                ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                                string currentItem2 = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();
                                if (currentItem2 == items[1].ToLower())
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(item).Click().Perform();
                                    action.DoubleClick(item).Perform();
                                    iFound = true;
                                    DlkLogger.LogInfo("Selecting sidebar item [" + items[1] + "] ...");
                                    break;
                                }
                            }
                        }
                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("DoubleClickItem() failed. '" + Item + "' not found. Actual sidebar items: " + ActualSideBarItems);
                }

                DlkLogger.LogInfo("DoubleClickItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickItem() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickItemContains")]
        public void DoubleClickItemContains(String PartialValue)
        {
            try
            {
                Boolean iFound = false;
                string ActualSideBarItems = "";

                Initialize();

                if (!PartialValue.Contains('~'))
                {
                    foreach (IWebElement item in mSidebarItems)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                        ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";


                        if (currentItem.Contains(PartialValue.ToLower()))
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(item).Click().Perform();
                            action.DoubleClick(item).Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Selecting sidebar item [" + PartialValue + "] ...");
                            break;
                        }
                    }
                }
                else
                {
                    string[] items = PartialValue.Split('~');
                    foreach (IWebElement header in mSidebarHeaders)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", header);
                        ActualSideBarItems = ActualSideBarItems + mSideBarHeader.GetValue() + "~";

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (currentHeader.Contains(items[0].ToLower()))
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header [" + items[0] + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + items[0] + "] already opened ...");

                            foreach (IWebElement item in mSidebarItems)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", item);
                                ActualSideBarItems = ActualSideBarItems + mSideBarItem.GetValue() + "~";

                                string currentItem2 = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();
                                if (currentItem2.Contains(items[1].ToLower()))
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(item).Click().Perform();
                                    action.DoubleClick(item).Perform();
                                    iFound = true;
                                    DlkLogger.LogInfo("Selecting sidebar item [" + items[1] + "] ...");
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("DoubleClickItemContains() failed. '" + PartialValue + "' not found. Actual sidebar items: " + ActualSideBarItems);
                }

                DlkLogger.LogInfo("DoubleClickItemContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickItemContains() failed : " + e.Message, e);
            }
        }

        [Keyword("DoubleClickItemByIndex")]
        public void DoubleClickItemByIndex(String Index)
        {
            try
            {
                Boolean iFound = false;

                Initialize();

                if (!Index.Contains('~'))
                {
                    int index = Convert.ToInt32(Index) - 1;

                    for (int i = 0; i < mSidebarItems.Count; i++)
                    {
                        DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                        string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                        if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                            String.IsNullOrEmpty(currentItem))
                            currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                        if (i == index)
                        {
                            Actions action = new Actions(DlkEnvironment.AutoDriver);
                            action.MoveToElement(mSidebarItems[i]).Click().Perform();
                            action.DoubleClick(mSidebarItems[i]).Perform();
                            iFound = true;
                            DlkLogger.LogInfo("Selecting sidebar item index [" + index + "] with value: [" + currentItem + "] ...");
                            break;
                        }
                    }
                }
                else
                {
                    string[] indexes = Index.Split('~');
                    int headerIndex = Convert.ToInt32(indexes[0]) - 1;
                    int itemIndex = Convert.ToInt32(indexes[1]) - 1;

                    for (int h = 0; h < mSidebarHeaders.Count; h++)
                    {
                        DlkBaseControl mSideBarHeader = new DlkBaseControl("Header", mSidebarHeaders[h]);

                        string currentHeader = DlkString.RemoveCarriageReturn(mSideBarHeader.GetValue()).ToLower().Trim();
                        if (h == headerIndex)
                        {
                            if (mSideBarHeader.mElement.FindElements(By.XPath(mTabExpandXPath)).Count > 0)
                            {
                                mSideBarHeader.Click();
                                DlkLogger.LogInfo("Selecting sidebar header index [" + headerIndex.ToString() + "] with value: [" + currentHeader + "] ...");
                                GetSideBarItems();
                            }
                            else
                                DlkLogger.LogInfo("Sidebar header [" + currentHeader + "] already opened ...");

                            for (int i = 0; i < mSidebarItems.Count; i++)
                            {
                                DlkBaseControl mSideBarItem = new DlkBaseControl("Item", mSidebarItems[i]);

                                string currentItem = DlkString.RemoveCarriageReturn(mSideBarItem.GetValue()).ToLower().Trim();

                                if (mSideBarItem.mElement.GetAttribute("class").Contains("Entry Clickable") ||
                                    String.IsNullOrEmpty(currentItem))
                                    currentItem = mSideBarItem.mElement.GetAttribute("data-display") != null ?
                                        DlkString.RemoveCarriageReturn(mSideBarItem.mElement.GetAttribute("data-display").ToLower().Trim()) : "";

                                if (i == itemIndex)
                                {
                                    Actions action = new Actions(DlkEnvironment.AutoDriver);
                                    action.MoveToElement(mSidebarItems[i]).Click().Perform();
                                    action.DoubleClick(mSidebarItems[i]).Perform();
                                    iFound = true;
                                    DlkLogger.LogInfo("Selecting sidebar item index [" + itemIndex.ToString() + "] with value: [" + currentItem + "] ...");
                                    break;
                                }
                            }
                        }

                        if (iFound)
                            break;
                    }
                }

                if (!iFound)
                {
                    throw new Exception("DoubleClickItemByIndex() failed. Item with index [" + Index + "] not found. Total sidebar items [" + mSidebarItems.Count.ToString() + "]");
                }

                DlkLogger.LogInfo("DoubleClickItemByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("DoubleClickItemByIndex() failed : " + e.Message, e);
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
