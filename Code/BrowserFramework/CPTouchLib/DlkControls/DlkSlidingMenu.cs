#define NATIVE_MAPPING

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("SlidingMenu")]
    public class DlkSlidingMenu : DlkMobileControl
    {
        private List<DlkMobileControl> mItems = new List<DlkMobileControl>();

        public DlkSlidingMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSlidingMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSlidingMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
            mItems = GetItems();
        }

        [Keyword("Select")]
        public void Select(string ItemToSelect)
        {
            try
            {
                Initialize();
                var target = mItems.FirstOrDefault(x => x.mControlName == ItemToSelect);
                if (target != null)
                {
                    target.Tap();
                    DlkLogger.LogInfo("Successfully executed Select().");
                    return;
                }
                throw new Exception("Item '" + ItemToSelect + "' not found");
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
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'. Keyword expects integer input");
                }
                DlkAssert.AssertEqual("VerifyItemCount", expected, mItems.Count);
                DlkLogger.LogInfo("Successfully executed VerifyItemCount().");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }

        [Keyword("Hide")]
        public void Hide()
        {
            try
            {
                Initialize();
#if NATIVE_MAPPING
                var header = new DlkMobileControl("header", "xpath", "//*[contains(@resource-id,'ext-deltekmenuheader-1')]");
#else
                var header = new DlkMobileControl("header", "XPATH_DISPLAY", "//*[contains(@id,'ext-deltekslidingmenubutton')]");
#endif
                if (header.Exists())
                {
                    header.Tap();
                    DlkLogger.LogInfo("Successfully executed Hide().");
                    return;
                }
                DlkLogger.LogInfo("Sliding menu is not displayed. Carrying on...");
            }
            catch (Exception e)
            {
                throw new Exception("Hide() failed : " + e.Message, e);
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
                    actual = string.Join("~", mItems.Select(x => x.mControlName));
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

        private List<DlkMobileControl> GetItems()
        {
            List<DlkMobileControl> ret = new List<DlkMobileControl>();
            try
            {
#if NATIVE_MAPPING
                var xpathItems = ".//*[contains(@resource-id,'item')]";
#else
                var xpathItems = ".//*[contains(@id,'deltekmenuitem')]|.//*[contains(@id,'deltekhelpitem')]";
#endif

                foreach (var itm in mElement.FindElements(By.XPath(xpathItems)).ToList().FindAll(x => x.Displayed))
                {
#if NATIVE_MAPPING
                    var txtHolder = itm.FindElements(By.XPath(".//*[last()]")).LastOrDefault();

                    if (txtHolder != null)
                    {
                        var txt = Regex.Replace(txtHolder.Text, @"[\d-]", string.Empty);
                        if (txt.Length >= 2 && char.IsUpper(txt[1]))
                        {
                            txt = txt.Substring(1);
                        }
                        ret.Add(new DlkMobileControl(txt, itm));
                    }
#else
                    ret.Add(new DlkMobileControl(itm.Text, itm));
#endif
                }
            }
            catch
            {
                DlkLogger.LogWarning("There was an error encountered getting menu items");
            }
            return ret;
        }
    }
}
