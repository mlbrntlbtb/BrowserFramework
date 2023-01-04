using CBILib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("ListBoxSearch")]
    public class DlkListBoxSearch : DlkBaseControl
    {
        public DlkListBoxSearch(string ControlName, IWebElement ExistingWebElement) : base(ControlName, ExistingWebElement)
        {
        }

        public DlkListBoxSearch(string ControlName, string SearchType, string SearchValue) : base(ControlName, SearchType, SearchValue)
        {
        }

        public DlkListBoxSearch(string ControlName, string SearchType, string[] SearchValues) : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkListBoxSearch(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) : base(ControlName, ExistingParentWebElement, CSSSelector)
        {
        }

        public DlkListBoxSearch(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) : base(ControlName, ParentControl, SearchType, SearchValue)
        {
        }

        private void Initialize()
        {
            DlkCERCommon.WaitForPromptSpinner();
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("InsertItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void InsertItems(String Keyword, String Items)
        {
            try
            {
                Initialize();
                //Input keyword first
                mElement.FindElements(By.XPath(".//input[@class='clsSwsEditBox']"))
                    .FirstOrDefault()
                    .SendKeys(Keyword + Keys.Enter);
                // Wait for 3 seconds before page completes reloading the reinitialize to prevent stale error
                Thread.Sleep(3000);
                Initialize();
                if (Items != "")
                {
                    List<IWebElement> availableItems = mElement.FindElements(By.XPath("(.//select[contains(@multiple,'multiple')])[1]//option")).ToList();
                    string[] splitItems = Items.Split('~');

                    foreach (var item in splitItems)
                    {
                        IWebElement selectedItem = availableItems.FirstOrDefault(f => f.Text.Contains(item));
                        if (selectedItem != null)
                            selectedItem.Click();
                        else
                            throw new Exception($"Item {item} not found.");
                    }

                    IWebElement insertButton = mElement.FindElements(By.XPath(".//span[contains(text(),'Insert')]/parent::button")).FirstOrDefault();

                    if (insertButton != null)
                        insertButton.Click();
                    else
                        throw new Exception("Insert button not found..");
                }
                DlkLogger.LogInfo("InsertItems() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("InsertItems() failed : " + e.Message, e);
            }
        }

        [Keyword("RemoveItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void RemoveItems(String Items)
        {
            try
            {
                Initialize();
                if (Items != "")
                {
                    List<IWebElement> availableItems = mElement.FindElements(By.XPath("(.//select[contains(@multiple,'multiple')])[2]//option")).ToList();
                    string[] splitItems = Items.Split('~');

                    foreach (var item in splitItems)
                    {
                        IWebElement selectedItem = availableItems.FirstOrDefault(f => f.Text.Contains(item));
                        if (selectedItem != null)
                            selectedItem.Click();
                        else
                            throw new Exception($"Item {item} not found.");
                    }

                    IWebElement removeButton = mElement.FindElements(By.XPath("//span[contains(text(),'Remove')]/parent::button")).FirstOrDefault();

                    if (removeButton != null)
                        removeButton.Click();
                    else
                        throw new Exception("Remove button not found..");
                }
                DlkLogger.LogInfo("RemoveItems() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("RemoveItems() failed : " + e.Message, e);
            }
        }
    }
}
