#define NATIVE_MAPPING

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;

namespace CPTouchLib.DlkControls
{
    [ControlType("Menu")]
    public class DlkMenu : DlkMobileControl
    {
#if NATIVE_MAPPING
        private const string XPATH_ITEMS = "//*[contains(@resource-id,'ext-deltekdropdownbutton')]";
#else
        private const string XPATH_ITEMS = "//*[contains(@id,'ext-deltekdropdownbutton')]";
#endif
        public DlkMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private List<DlkMobileControl> mItems = new List<DlkMobileControl>();

        private void Initialize()
        {
            FindElement();
            GetItems();
        }

        private void GetItems()
        {
            mElement.FindElements(By.XPath(mSearchValues.First() + XPATH_ITEMS)).ToList()
                .ForEach(x => mItems.Add(new DlkMobileControl("itm", x)));
        }

        [Keyword("Select")]
        public void Select(string ItemToSelect)
        {
            try
            {
                Initialize();
                var target = mItems.FirstOrDefault(x => GetItemText(x).Equals(ItemToSelect));
                if (target == null)
                {
                    throw new Exception("Target item not found");
                }
                target.Tap();
                DlkLogger.LogInfo("Successfully executed Select().");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex")]
        public void SelectByIndex(string OneBasedIndex)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mItems.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
                mItems[index - 1].Tap();
                DlkLogger.LogInfo("Successfully executed SelectByIndex().");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemCount")]
        public void VerifyItemCount(string ExpectedCount)
        {
            try
            {
                Initialize();
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                DlkAssert.AssertEqual("VerifyItemCount", expected, mItems.Count);
                DlkLogger.LogInfo("VerifyItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItems")]
        public void VerifyItems(string TildeBoundItems)
        {
            try
            {
                Initialize();
                string actual = string.Empty;
                try
                {
                    actual = string.Join("~", mItems.Select(x => GetItemText(x)));
                }
                catch (Exception e)
                {
                    throw new Exception("Unexpected control error encountered: " + e.Message);
                }
                DlkAssert.AssertEqual("VerifyItems", TildeBoundItems, actual);
                DlkLogger.LogInfo("Successfully executed VerifyItems().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        private string GetItemText(DlkMobileControl item)
        {
#if NATIVE_MAPPING
            var textChild = item.mElement.FindElements(By.XPath("./*/*")).FirstOrDefault();
            return textChild != null ? textChild.Text : string.Empty;
#else
            return item.mElement != null ? item.mElement.Text : string.Empty;
#endif
        }
    }
}
