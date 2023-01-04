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

namespace DCOLib.DlkControls
{
    public class DlkComboBoxList : DlkBaseControl
    {
        private String mStrListItemDesc = "option";
        private IList<IWebElement> mItemElements;

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
            return DlkString.RemoveCarriageReturn(allItems);
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
    }
}
