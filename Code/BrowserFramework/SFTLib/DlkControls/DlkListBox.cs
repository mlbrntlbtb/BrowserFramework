using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using SFTLib.DlkControls;
using SFTLib.DlkControls.Contract;
using SFTLib.DlkControls.Concrete.ComboBox;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;
using OpenQA.Selenium.Interactions;

namespace SFTLib.DlkControls {

    [ControlType("ListBox")]
    public class DlkListBox : DlkBaseControl {

        #region Constructors
        public DlkListBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) {
        }

        public DlkListBox(String ControlName, String SearchType, String SearchValue, Boolean VerifyAfterSelect)
            : base(ControlName, SearchType, SearchValue) {
        }

        public DlkListBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) {
        }

        public DlkListBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) {
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();

            if (DlkEnvironment.mBrowser.ToLower() == "edge")
                this.ScrollIntoViewUsingJavaScript();
        }
        #endregion

        #region Private Methods
        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
            this.ScrollIntoViewUsingJavaScript();
        }
        private List<IWebElement> GetItems(bool selectedOnly = false)
        {
            string itemXpath;
            Thread.Sleep(2000);
            if (mElement.GetAttribute("class").Contains("rich-shuttle"))
                itemXpath = ".//tr"; // for SFT version 1
            else
                itemXpath = ".//ul//li";  // for SFT version 2

            if (selectedOnly)
                return mElement.FindElements(By.XPath(itemXpath)).Where(item => item.GetAttribute("class").Contains("selected")).ToList();
            else
                return mElement.FindElements(By.XPath(itemXpath)).ToList();
        }
        #endregion

        #region Keywords
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (Convert.ToBoolean(TrueOrFalse) == true)
                    Initialize();

                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("MultiSelect")]
        public void MultiSelect(string Items)
        {
            try
            {
                Initialize();
                if (!Items.Contains("~"))
                    throw new Exception("Items must contain ( ~ ) as a delimiter.");
                List<String> ItemsToSelect = Items.Split('~').ToList<String>();
                foreach (String item in ItemsToSelect)
                {
                    var ListItem = GetItems().Where(li => li.Text.Trim() == item.Trim()).FirstOrDefault();
                    if (ListItem != null)
                        ListItem.DoubleClick(1);
                    else
                        throw new Exception(String.Format("Item {0} not found.", item));
                }
                Thread.Sleep(600);
                DlkLogger.LogInfo("MultiSelect() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("MultiSelect() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("Select")]
        public void Select(string Item) {
            try
            {
                Initialize();
                var itemToSelect = GetItems().Where(l => l.Text.Trim() == Item.Trim()).FirstOrDefault();
                if (itemToSelect != null)
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        itemToSelect.Click();
                        new Actions(DlkEnvironment.AutoDriver).DoubleClick(itemToSelect).Perform();
                    }
                    else
                    {
                        itemToSelect.DoubleClick(1);
                    }                    
                }
                else
                    throw new Exception(String.Format("Item {0} not found.", Item));

                Thread.Sleep(600);
                DlkLogger.LogInfo("Select() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("VerifyList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyList(String Items) {
            try {
                Initialize();
                String ActualList = String.Join("~", GetItems().Select(l => l.Text.Trim()));

                DlkAssert.AssertEqual("VerifyList() ListBox: ", ActualList, Items.Trim());

                DlkLogger.LogInfo("VerifyList() passed");
            } catch (Exception e) {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            } finally {
                Terminate();
            }
        }
        [Keyword("VerifySelectedText", new String[] { "1|text|Expected Item/s|-Select-~Expected~Items" })]
        public void VerifySelectedText(String ExpectedItems) {
            try
            {
                Initialize();
                String actual = String.Join("~", GetItems(true).Select(l => l.Text.Trim()));
                DlkAssert.AssertEqual("VerifySelectedText()", ExpectedItems.Trim(), actual);
                DlkLogger.LogInfo("VerifySelectedText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedText() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        #endregion
    }
}
