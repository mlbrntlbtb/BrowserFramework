using SFTLib.DlkControls.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Reflection;
using System.Threading;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;

namespace SFTLib.DlkControls.Concrete.ComboBox
{
    public class DefaultComboBox : IComboBox
    {
        IWebElement webElement;
        public DefaultComboBox(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        public void Select(string item)
        {
            try
            {
                ToggleDropdown();
                Thread.Sleep(1000);
                var comboBoxItems = webElement.FindElements(By.XPath("//*[contains(@class,'x-boundlist x-boundlist-floating') and not(contains(@style,'display: none'))]//li"));
                var itemToSelect = comboBoxItems.Where(x => x.Text.Trim() == item.Trim()).FirstOrDefault();

                if (itemToSelect != null)
                    itemToSelect.ClickUsingJS();
                else
                    throw new Exception(String.Format("Item {0} not found.", item));

            }
            catch (Exception exception)
            {
                throw new Exception(String.Format("Select failed : {0} StackTrace : {1}", exception.Message, exception.StackTrace));
            }
        }
        public void VerifyList(string items)
        {
            try
            {
                ToggleDropdown();
                var list = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[contains(@class,'x-boundlist-floating') and not(contains(@style,'display: none'))]//li"))
                    .Where(x => !String.IsNullOrEmpty(x.Text.Trim()))
                    .Select(x => x.Text.Trim()).ToList();
                var itemsToVerify = String.Join("~", list);

                ToggleDropdown();

                DlkAssert.AssertEqual("VerifyList() List: ", itemsToVerify, items);
            }
            catch (Exception exception)
            {
                throw new Exception(String.Format("Select failed : {0} StackTrace : {1}", exception.Message, exception.StackTrace));
            }
        }
        public void SelectByIndex(int index)
        {
            try
            {
                bool isFound = false;

                ToggleDropdown();
                Thread.Sleep(1000);
                var comboBoxItems = webElement.FindElements(By.XPath("//*[contains(@class,'x-boundlist x-boundlist-floating') and not(contains(@style,'display: none'))]//li"));
                var options = comboBoxItems.Where(x => x.Displayed).ToList();

                if (options.Count == 0)
                    throw new Exception("No options found in combo box");

                foreach (var option in options)
                {
                    if (options.IndexOf(option) + 1 == index)
                    {
                        new DlkBaseControl("ComboBox option", option).ScrollIntoViewUsingJavaScript();
                        var optionText = option.Text; //get option name to select
                        option.Click();
                        DlkSFTCommon.WaitForSpinner();
                        DlkLogger.LogInfo("Successfully executed SelectByIndex(). Value: " + optionText + " was selected from the list.");
                        isFound = true;
                        break;
                    }
                }

                if (!isFound)
                    throw new Exception(String.Format("Item index {0} not found.", index));
            }
            catch (Exception exception)
            {
                throw new Exception(String.Format("SelectByIndex failed : {0} StackTrace : {1}", exception.Message, exception.StackTrace));
            }
        }
        #region PRIVATE METHODS
        /// <summary>
        /// Gets the current active/selected item in the menu list
        /// </summary>
        /// <returns>IWebElement Menu Item</returns>
        private IWebElement GetSelectedComboBoxItem()
        {
            var highlightedComboBoxItems = webElement.FindElements(By.XPath("//*[contains(@class,'x-boundlist x-boundlist-floating') and not(contains(@style,'display: none'))]//li[contains(@class, 'item-over')]")).Where(obj => obj.Displayed).ToList();

            if (highlightedComboBoxItems.Count > 0) return highlightedComboBoxItems.FirstOrDefault();
            else throw new Exception("Unable to retrieve highlighted combo box option.");
        }

        private void ToggleDropdown()
        {
            DlkSFTCommon.WaitForScreenToLoad();
            var arrowDownButton = webElement.FindElements(By.XPath(".//*[contains(@class,'form-arrow-trigger') and @role='button']")).FirstOrDefault();
            if (arrowDownButton != null)
            {
                arrowDownButton.ClickUsingJS();
                Thread.Sleep(1000);
            }
            else throw new Exception("ComboBox dropdown button not found.");
        }
        #endregion
    }
}
