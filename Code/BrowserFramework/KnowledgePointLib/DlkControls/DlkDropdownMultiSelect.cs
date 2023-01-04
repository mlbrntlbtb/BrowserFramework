using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Threading;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("DropdownMultiSelect")]
    public class DlkDropdownMultiSelect : DlkBaseControl
    {
        private const string DropdownMultiSelectContainer = ".//div[contains(@class,'MuiGrid-grid-lg-3')]";
        private const int WAIT_TIME = 1500;
        #region Constructors
        public DlkDropdownMultiSelect(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropdownMultiSelect(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropdownMultiSelect(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public DlkDropdownMultiSelect(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion
        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        /// Verifies if DropdownMultiSelect exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>

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

        /// <summary>
        /// Verifies text
        /// </summary>
        /// <param name="strExpectedValue"></param>

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();
                mElement.Click();
                string actualResult = null;
                var selectedItem = mElement.FindElements(By.XPath(".//*[contains(@class,'MuiChip-label')]")).FirstOrDefault();
                if (ExpectedText == "" && selectedItem == null) // verifying empty result
                    actualResult = "";
                else
                {
                    actualResult = selectedItem.Text.Trim();
                    if (actualResult == null)
                        actualResult = mElement.Text.Trim();
                }
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText.Trim(), "\n");
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Verifies multiple selected items
        /// </summary>
        /// <param name="ExpectedValues"></param>
        [Keyword("VerifySelectedItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySelectedItems(String ExpectedValues)
        {
            try
            {
                Initialize();
                mElement.Click();
                string[] selectedItems = ExpectedValues.Split('~');
                List<IWebElement> items = mElement.FindElements(By.XPath(".//div[@role='button' and contains(@class,'deletable')]/span")).ToList();
                IList<string> actualItems = new List<string>();
                foreach (IWebElement item in items)
                {
                    actualItems.Add(DlkString.RemoveCarriageReturn(item.Text.Trim()));
                }
                string actualResult = string.Join("~", actualItems);
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedValues.Trim(), "\n");
                DlkAssert.AssertEqual("VerifySelectedItems() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedItems() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Selects multiple items
        /// </summary>
        /// <param name="MenuItems"></param>
        [Keyword("Select", new String[] { "1|text|Expected Value|TRUE" })]
        public void Select(String MenuItems)
        {
            try
            {
                Initialize();
                IWebElement dropdownInputField = mElement.FindElements(By.XPath(".//input")).FirstOrDefault();
                if (MenuItems.Contains("~"))
                {
                    string[] menuItems = MenuItems.Split('~');
                    foreach (string menuItem in menuItems)
                    {
                        dropdownInputField.SendKeys(menuItem);
                        Thread.Sleep(WAIT_TIME);
                        dropdownInputField.SendKeys(Keys.ArrowDown);
                        dropdownInputField.SendKeys(Keys.Enter);
                    }
                }
                else
                {
                    dropdownInputField.SendKeys(MenuItems);
                    Thread.Sleep(1500);
                    dropdownInputField.SendKeys(Keys.ArrowDown);
                    dropdownInputField.SendKeys(Keys.Enter);
                }

                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }
        /// <summary>
        /// Deselects item from a dropdown multiselect menu
        /// </summary>
        /// <param name="MenuItems"></param>

        [Keyword("Deselect", new String[] { "1|text|Expected Value|TRUE" })]
        public void Deselect(String MenuItems)
        {
            try
            {
                Initialize();
                IWebElement menuItemsToDeSelect;
                if (MenuItems.Contains("~"))
                {
                    var menuItems = MenuItems.Split('~');
                    foreach (string menuItem in menuItems)
                    {
                        if (ItemContainsEllipsis(menuItem))
                            menuItemsToDeSelect = mElement.FindElements(By.XPath(".//div[@role='button']")).ToList().Where(x => x.Text.Contains(menuItem.Remove(menuItem.Length - 3))).FirstOrDefault();
                        else
                            menuItemsToDeSelect = mElement.FindElements(By.XPath(".//div[@role='button']")).ToList().Where(x => x.Text.Contains(menuItem)).FirstOrDefault();

                        menuItemsToDeSelect.FindElements(By.XPath(".//*[contains(@class,'MuiChip-deleteIcon')]"))
                            .FirstOrDefault()
                            .Click();
                    }
                }
                else
                {
                    if (ItemContainsEllipsis(MenuItems))
                        menuItemsToDeSelect = mElement.FindElements(By.XPath(".//div[@role='button']")).ToList().Where(x => x.Text.Contains(MenuItems.Remove(MenuItems.Length - 3))).FirstOrDefault();
                    else
                        menuItemsToDeSelect = mElement.FindElements(By.XPath(".//div[@role='button']")).ToList().Where(x => x.Text.Contains(MenuItems)).FirstOrDefault();

                    menuItemsToDeSelect.FindElements(By.XPath(".//*[contains(@class,'MuiChip-deleteIcon')]"))
                        .FirstOrDefault()
                        .Click();
                }
                DlkLogger.LogInfo("Successfully executed Deselect()");
            }
            catch (Exception e)
            {
                throw new Exception("Deselect() failed : " + e.Message, e);
            }
        }


        #endregion

        #region
        private bool ItemContainsEllipsis(string item)
        {
            bool hasEllipsis = false;
            if (item.Substring(item.Length - 3) == "...")
                hasEllipsis = true;

            return hasEllipsis;
        }
        #endregion
    }
}
