using CBILib.DlkUtility;
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
    [ControlType("ListView")]
    public class DlkListView : DlkBaseControl
    {
        private List<IWebElement> mListViewItems = new List<IWebElement>();

        public DlkListView(string ControlName, IWebElement ExistingWebElement) : base(ControlName, ExistingWebElement)
        {
        }

        public DlkListView(string ControlName, string SearchType, string SearchValue) : base(ControlName, SearchType, SearchValue)
        {
        }

        public DlkListView(string ControlName, string SearchType, string[] SearchValues) : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkListView(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) : base(ControlName, ExistingParentWebElement, CSSSelector)
        {
        }

        public DlkListView(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) : base(ControlName, ParentControl, SearchType, SearchValue)
        {
        }

        private void Initialize()
        {
            DlkCERCommon.WaitForPromptSpinner();
            FindElement();
            GetListViewItems();
            ScrollIntoViewUsingJavaScript(false);
        }

        private void GetListViewItems()
        {
            mListViewItems = new List<IWebElement>(mElement.FindElements(By.XPath(".//option")));
        }

        [Keyword("MultiSelect", new String[] { "1|text|Items To Select|Item1~Item2~Item3" })]
        public void MultiSelect(String Value)
        {
            try
            {
                Initialize();
                var values = Value.Split('~').ToList();

                foreach (var item in values)
                {
                    IWebElement foundItem = mListViewItems.SingleOrDefault(f => f.Text.Trim() == item.Trim());

                    if (foundItem != null)
                    {
                        foundItem.Click();
                    }
                    else
                    {
                        throw new Exception(item + " not found.");
                    }
                }
                DlkLogger.LogInfo("MultiSelect() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("MultiSelect() failed : " + e.Message, e);
            }
        }
    }
}
