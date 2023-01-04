using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace TM1WebLib.DlkControls
{
    public class DlkComboBoxList : DlkBaseControl
    {
        private String mStrListItemDesc = "div";
        private IList<IWebElement> mItemElements;
        private Dictionary<String, int> dictItemPos;

        public DlkComboBoxList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBoxList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBoxList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            CustomFindElement();
        }

        private void CustomFindElement()
        {
            FindElement();
            mItemElements = mElement.FindElements(By.CssSelector(mStrListItemDesc));
        }

        private void CreateItemPosDict()
        {
            int intCounter = 1;

            dictItemPos = new Dictionary<string, int>();
            DlkLogger.LogInfo("count: " + Convert.ToString(mItemElements.Count));
            foreach (IWebElement aListItem in mItemElements)
            {
                DlkBaseControl dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                dictItemPos.Add(dlkComboItem.GetValue().ToLower(), intCounter);
                intCounter++;
            }
        }

        public int GetItemPosition(String itemText, bool IsStrict, out string ActualValue)
        {
            Initialize();
            int pos = 1;
            if (IsStrict)
            {
                foreach (IWebElement aListItem in mItemElements)
                {
                    DlkBaseControl dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                    ActualValue = dlkComboItem.GetValue();
                    if (DlkString.NormalizeNonBreakingSpace(dlkComboItem.GetValue()).ToLower() == itemText.ToLower())
                    {
                        return pos;
                    }
                    pos++;
                }
            }
            else
            {
                foreach (IWebElement aListItem in mItemElements)
                {
                    DlkBaseControl dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                    ActualValue = dlkComboItem.GetValue();
                    if (DlkString.NormalizeNonBreakingSpace(dlkComboItem.GetValue()).ToLower().Contains(itemText.ToLower()))
                    {
                        return pos;
                    }
                    pos++;
                }
            }
            ActualValue = string.Empty;
            return 0;
        }

        public String GetAllItems()
        {
            String allItems = "";
            Initialize();
            foreach (IWebElement aListItem in mItemElements)
            {
                DlkBaseControl dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                allItems = allItems + dlkComboItem.GetValue() + " ";

            }
            return allItems;
        }

        public String GetAllItemsWithDelimiter()
        {
            String allItems = "";
            int itemIdx = 0;
            int elemIdx = mItemElements.Count() - 1;
            Initialize();
            foreach (IWebElement aListItem in mItemElements)
            {
                DlkBaseControl dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                itemIdx = mItemElements.IndexOf(aListItem);
                if (itemIdx == elemIdx)
                {
                    allItems = allItems + dlkComboItem.GetValue();
                }
                else
                {
                    allItems = allItems + dlkComboItem.GetValue() + "~";
                }
            }
            return allItems;
        }

        public void Select(String Value)
        {
            Initialize();
            Boolean bFound = false;
            String actualItems = "";
            DlkBaseControl dlkComboItem;

            mItemElements = mElement.FindElements(By.CssSelector(mStrListItemDesc));
            foreach (IWebElement aListItem in mItemElements)
            {
                dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                actualItems = actualItems + dlkComboItem.GetValue() + " ";
                if (dlkComboItem.GetValue().ToLower() == Value.ToLower())
                {
                    //if (DlkEnvironment.TestBrowser.ToLower() == "ie")
                    //{
                    //    dlkComboItem.MouseOver();
                    //    dlkComboItem.mElement.SendKeys(Keys.Enter);
                    //}
                    //else
                    //{
                    dlkComboItem.Click();
                    //}

                    bFound = true;
                    break;
                }

            }
            if (!bFound)
            {
                throw new Exception ("Select() failed. Control : " + mControlName + " : '" + Value +
                                        "' not found in list. : Actual List = " + actualItems);
            }
        }

        //Select that accepts an integer value parameter
        public void Select(int itemIdx)
        {
            Initialize();
            Boolean bFound = false;
            DlkBaseControl dlkComboItem;
            int idx = 1;

            mItemElements = mElement.FindElements(By.CssSelector(mStrListItemDesc));
            foreach (IWebElement aListItem in mItemElements)
            {            
                dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                if (aListItem.Text != "")
                {
                    if (idx == itemIdx)
                    {
                        dlkComboItem.Click();
                        bFound = true;
                        break;
                    }
                    idx++;
                }
            }
            if (!bFound)
            {
                throw new Exception("SelectByIndex() failed. Control : " + mControlName + " : '" + itemIdx +
                                        "' is out of bounds of the list count.");
            }
        }

        public string SelectItem(String Value, bool IsStrict = true)
        {
            string actualValue = string.Empty; // return is critical for non-strict comparisons
            Initialize();
            try
            {
                string searchString = IsStrict ? "./descendant::div[text()='" + Value + "']" : "./descendant::div[contains(text(),'" + Value + "')]";
                DlkComboBoxListItem ctl = new DlkComboBoxListItem("ItemToSelect", this, "xpath", searchString);
                ctl.Select();
                actualValue = DlkString.UnescapeXML(DlkString.NormalizeNonBreakingSpace(ctl.GetValue()));
            }
            catch
            {
                DlkLogger.LogInfo("SelectItem() : Unable to select item from combobox dropdown list.");
            }
            return actualValue;
        }

        //Without verify
        public void SelectItemWithoutVerify(String Value, bool IsStrict = true)
        {
            Initialize();
            try
            {
                string searchString = IsStrict ? "./descendant::div[text()='" + Value + "']" : "./descendant::div[contains(text(),'" + Value + "')]";
                DlkComboBoxListItem ctl = new DlkComboBoxListItem("ItemToSelect", this, "xpath", searchString);
                ctl.Select();

            }
            catch
            {
                DlkLogger.LogInfo("SelectItem() : Unable to select item from combobox dropdown list.");
            }

        }
    }

    public class DlkComboBoxListItem : DlkBaseControl
    {
        private bool IsInit = false;

        public DlkComboBoxListItem(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBoxListItem(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBoxListItem(String ControlName, IWebElement ExistingParentWebElement, String CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }
        public DlkComboBoxListItem(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkComboBoxListItem(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }


        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
            }
            IsInit = true;
        }

        private void MouseOverComboBox()
        {
            MouseOver();
        }

        private new void Click(int offsetX, int offsetY)
        {
            try
            {
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.MoveToElement(mElement, offsetX, offsetY).Click().Perform();
                DlkLogger.LogInfo("Successfully executed Click(). offsetX: " + offsetX.ToString() + ", offsetY: " + offsetY.ToString() + ", Control: " + mControlName);
            }
            catch (Exception e)
            {
                throw new Exception (e.Message + "\n" + e.StackTrace);
            }
        }

        private void GetElementLocationOnScreenComboBox()
        {
            GetElementLocationOnScreen(true);
        }

        public void Select()
        {
            Initialize();
            //this.MouseOver();
            this.Click();
        }
    }
}
