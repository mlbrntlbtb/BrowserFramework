using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("ListBox")]
    public class DlkListBox : DlkBaseControl
    {
        private List<IWebElement> mListItems = new List<IWebElement>();
        private string mListType = "li";

        #region Constructor
        public DlkListBox(string ControlName, string SearchType, string SearchValue) : base(ControlName, SearchType, SearchValue) { }
        public DlkListBox(string ControlName, IWebElement ExistingWebElement) : base(ControlName, ExistingWebElement) { }
        public DlkListBox(string ControlName, string SearchType, string[] SearchValues) : base(ControlName, SearchType, SearchValues) { }
        public DlkListBox(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) : base(ControlName, ExistingParentWebElement, CSSSelector) { }
        public DlkListBox(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion

        private void Initialize()
        {
            FindElement();
            GetListItems();
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

        [Keyword("VerifyItemExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemExists(string Item, string ExpectedValue)
        {
            try
            {
                Initialize();
                bool found = false;

                foreach (var item in mListItems)
                {
                    if (item.Text == Item)
                    {
                        found = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemExists()", bool.Parse(ExpectedValue), found);
                DlkLogger.LogInfo("VerifyItemExists() Passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() failed : " + e.Message, e);
            }
        }

        [Keyword("Click", new String[] { "1|text|Item|Sample Value" })]
        public void Click(string Item)
        {
            try
            {
                Initialize();
                bool found = false;

                foreach (var item in mListItems)
                {
                    if (item.Text == Item)
                    {
                        var listItem = new DlkBaseControl("ListItem", item);
                        listItem.ClickByObjectCoordinates();
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new Exception($"List item '{Item}' not found.");

                DlkLogger.LogInfo("Click() Passed.");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("RightClick", new String[] { "1|text|Item|Sample Value" })]
        public void RightClick(string Item)
        {
            try
            {
                Initialize();
                bool found = false;

                foreach (var item in mListItems)
                {
                    if (item.Text == Item)
                    {
                        var mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                        mAction.MoveToElement(item).ContextClick().Build().Perform();
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new Exception($"List item '{Item}' not found.");

                DlkLogger.LogInfo("RightClick() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("RightClick() failed : " + e.Message, e);
            }
        }

        private void GetListItems()
        {
            mListItems = mElement.FindElements(By.XPath("./ul//li")).ToList();

            if (mListItems?.Count == 0)
            {
                mListItems = mElement.FindElements(By.XPath(".//*[name()='text']")).ToList();

                if (mListItems.Count == 0)
                    throw new Exception("List items not found");
                else
                    mListType = "text";
            }
        }
    }
}
