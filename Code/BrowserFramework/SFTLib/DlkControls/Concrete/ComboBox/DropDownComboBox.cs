using SFTLib.DlkControls.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Reflection;
using CommonLib.DlkControls;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using CommonLib.DlkSystem;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace SFTLib.DlkControls.Concrete.ComboBox
{
    public class DropDownComboBox : IComboBox
    {
        IWebElement webElement;
        bool fromLogin;



        public DropDownComboBox(IWebElement webElement, bool fromLogin = false)
        {
            this.webElement = webElement;
            this.fromLogin = fromLogin;
        }
        public void Select(string item)
        {
            try
            {
                List<IWebElement> options;
                IWebElement itemToSelect;
                var browser = DlkEnvironment.mBrowser.ToLower();

                string selectedItem = webElement.ExecJSWithStringReturnValue(@"
                    return arguments[0].options[arguments[0].selectedIndex].text
                ");

                if (selectedItem == item)
                    return;

                if (!fromLogin)
                    webElement.Click();

                options = webElement.FindElements(By.XPath(@".//option")).ToList();

                itemToSelect = options.Where(x => x.Text.Contains(item)).FirstOrDefault();

                if (itemToSelect != null)
                {
                    if (browser != "ie")
                        new DlkBaseControl("Cell", itemToSelect).ScrollIntoViewUsingJavaScript();

                    itemToSelect.Click();
                }
                else
                    throw new Exception(String.Format("Item {0} not found.", item));

                options = webElement.FindElements(By.XPath(@".//option")).ToList();

                if ((browser == "edge" || browser == "ie") && options.FirstOrDefault() != null && options.FirstOrDefault().Displayed)
                {
                    // this will close the Dropdown menu if its still open
                    if (browser == "ie")
                    {
                        var label = webElement.FindElements(By.XPath(".//parent::td//preceding-sibling::td[1]")).FirstOrDefault();
                        Thread.Sleep(500);
                        label.Click();
                    } else 
                        webElement.Click();
                }
                
            }
            catch (StaleElementReferenceException)
            {
                throw new StaleElementReferenceException();
            }
            catch (Exception exception)
            {
                throw new Exception(String.Format("Select() failed : {0} StackTrace : {1}", exception.Message, exception.StackTrace));
            }
        }


        public void VerifyList(string items)
        {
            try
            {
                webElement.Click();
                Thread.Sleep(1000);
                var list = webElement.FindElements(By.XPath(".//option")).GetWebElementsValues().Where(opt => opt != "");
                var itemsToVerify = String.Join("~", list);
                webElement.Click();
                DlkAssert.AssertEqual("VerifyList() List: ", itemsToVerify, items);
            }
            catch (Exception exception)
            {
                throw new Exception(String.Format("VerifyList() failed : {0} StackTrace : {1}", exception.Message, exception.StackTrace));
            }
        }
        public void SelectByIndex(int index)
        {
            try
            {
                bool isFound = false;
                
                webElement.Click();
                var options = webElement.FindElements(By.XPath(@".//option")).Where(x => x.Displayed).ToList();

                if (options.Count == 0)
                    throw new Exception("No options found in combo box");

                foreach(var option in options)
                {
                    if (options.IndexOf(option) + 1 == index)
                    {
                        new DlkBaseControl("ComboBox option", option).ScrollIntoViewUsingJavaScript();
                        var optionText = option.Text; //get option name to select
                        option.Click();
                        DlkLogger.LogInfo("Successfully executed SelectByIndex(). Value: " + optionText + " was selected from the list.");
                        isFound = true;
                        break;
                    }
                }
                
                if(!isFound)
                    throw new Exception(String.Format("Item index {0} not found.", index));
            }
            catch (Exception exception)
            {
                throw new Exception(String.Format("SelectByIndex failed : {0} StackTrace : {1}", exception.Message, exception.StackTrace));
            }
            
        }
           
    }
}
