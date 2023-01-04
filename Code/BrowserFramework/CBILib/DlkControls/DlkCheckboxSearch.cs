using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CBILib.DlkControls;
using CBILib.DlkUtility;
using System.Threading;

namespace CBILib.DlkControls
{
    [ControlType("CheckboxSearch")]
    public class DlkCheckboxSearch : DlkBaseControl
    {
        #region Constructors
        public DlkCheckboxSearch(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckboxSearch(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckboxSearch(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            //WaitForCheckboxSearchReady();
            FindElement(); 
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords


        /// <summary>
        /// Verifies if button exists. Requires strExpectedValue - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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
        /// Selects CheckboxSearch
        /// </summary>
        /// <param name="CheckboxSearchPath"></param>
        [Keyword("SelectItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItem(String Keyword, String Items)
        {
            try
            {
                Initialize();

                IWebElement searchText = mElement.FindElements(By.XPath(".//input[@class='clsSearchText' or @class='clsSelectWithSearchSearchText']")).FirstOrDefault();
                bool isNewUI = false;

                if (searchText == null) //8035
                {
                    searchText = mElement.FindElements(By.XPath(".//input[@class='clsSwsEditBox']")).FirstOrDefault();
                    if (searchText == null)
                        throw new Exception("Cannot find search text.");
                }
                else
                    isNewUI = true;

                searchText.SendKeys(Keyword + Keys.Enter);

                // Wait for 3 seconds before page completes reloading the reinitialize to prevent stale error
                Thread.Sleep(3000); // To replac with wait for spinner
                Initialize();
                ScrollIntoViewUsingJavaScript(); // Since after entering the keyword, the page reloads
                IWebElement checkboxItem = null;
                if (Items != "")
                {
                    if (Items.Contains("~")) // Multiple items to select
                    {
                        string[] itemsToSelect = Items.Split('~');

                        if (isNewUI)
                        {
                            foreach (var item in itemsToSelect)
                            {
                                // Find item
                                checkboxItem = mElement.FindElements(By.XPath(".//div[contains(@class,'clsListViewCheckboxView')]//span[text()='" + item + "']/preceding-sibling::img[@class='clsLVCheckbox']")).FirstOrDefault();
                                if (checkboxItem != null)
                                    checkboxItem.Click();
                                else
                                    throw new Exception("Item does not exist.");
                            }
                        }
                        else
                        {
                            foreach (var item in itemsToSelect)
                            {
                                // Find item
                                checkboxItem = mElement.FindElements(By.XPath($".//option[contains(text(),'{item}')]")).FirstOrDefault();
                                if (checkboxItem != null)
                                {
                                    var mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                                    mAction.KeyDown(Keys.Shift).Click(checkboxItem).KeyUp(Keys.Shift).Perform();
                                }
                                else
                                    throw new Exception("Item does not exist.");
                            }
                        }
                    }
                    else
                    {
                        if (isNewUI)
                        {
                            var valueItems = Items.Split(' '); //fix for nbsp tag
                            var results = mElement.FindElements(By.XPath($".//div[contains(@class,'clsListViewCheckboxView')]//span[contains(text(),'{valueItems[0]}')]"));
                            checkboxItem = results.FirstOrDefault(f => f.Text.Contains(Items))?.FindElement(By.XPath("./preceding-sibling::img[contains(@class,'clsLVCheckbox')]"));
                        }
                        else
                            checkboxItem = mElement.FindElements(By.XPath($"..//option[contains(text(),'{Items}')]")).FirstOrDefault();
                        
                        if (checkboxItem != null)
                        {
                            checkboxItem.Click();
                        }
                        else
                            throw new Exception("Item does not exist.");
                    }

                    // Click add "->" button
                    IWebElement buttonAdd = mElement.FindElements(By.XPath(".//button/span[contains(text(),'Insert')]/..")).FirstOrDefault();

                    if (buttonAdd != null)
                        buttonAdd.Click();
                    else //added criteria for 1182 link
                        mElement.FindElement(By.XPath(".//button[@name='add']")).Click();
                }

            }
            catch (Exception e)
            {
                throw new Exception("SelectItem() failed : " + e.Message, e);
            }
        }


        #endregion

        #region Private Methods

        private void WaitForCheckboxSearchReady()
        {

            DlkBaseControl CheckboxSearchLoadingIdentifier = new DlkBaseControl("spinner", "xpath_display", "//table[contains(@class,'listControl ')]/tbody/tr"); //has value

            //if(mElement.FindElements(By.XPath("//table[contains(@class,'listControl ')]/tbody/tr")).Count() > 0)

            if (!CheckboxSearchLoadingIdentifier.Exists(1)) // does not exists, means no value
            {
                try
                {
                    while (!CheckboxSearchLoadingIdentifier.Exists(1))
                    {
                        DlkLogger.LogInfo("CheckboxSearch loading...");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (OpenQA.Selenium.StaleElementReferenceException)
                {
                    DlkLogger.LogInfo("CheckboxSearch finished loading.");
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                    return;
                }
            }
            DlkLogger.LogInfo("CheckboxSearch finished loading.");
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
        }

        #endregion

    }
}
