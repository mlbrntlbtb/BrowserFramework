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
    [ControlType("CheckboxList")]
    public class DlkCheckboxList : DlkBaseControl
    {
        private bool isNewUI = false;
        private const string oldUIXpath = ".//div[contains(@class,'clsCheckBoxRow')]//label[text()='{0}']/..//input";
        private const string newUIXpath = ".//div[contains(@class,'clsListViewCheckboxView')]//span[text()='{0}']/preceding-sibling::img[contains(@class,'clsLVCheckbox')]";
        private IList<IWebElement> mItems;
        private ListType listType = ListType.NotSupported;

        #region Constructors
        public DlkCheckboxList(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckboxList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckboxList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            GetListItems();

            if (listType == ListType.NotSupported)
                throw new Exception("List type is not supported.");
        }

        #region Keywords

        /// <summary>>
        /// Sets the value of a CheckboxList to checked or unchecked. Requires TrueOrFalse - can either be True or False
        /// </summary>

        [Keyword("Set")]
        public void Set(String IsChecked, String Values)
        {
            try
            {
                Initialize();
                IWebElement checkboxItem = null;

                List<string> itemsToSel = Values.Split('~').ToList();
                List<string> itemsfound = new List<string>();

                switch (listType)
                {
                    case ListType.Reporting:
                        foreach (string checkBoxItemText in mItems.Select(s => s.Text).ToList())
                        {
                            bool currentValChecked = GetCheckedState(checkBoxItemText, null);

                            if (isNewUI)
                                checkboxItem = mElement.FindElements(By.XPath(string.Format(newUIXpath, checkBoxItemText))).FirstOrDefault();
                            else
                                checkboxItem = mElement.FindElements(By.XPath(string.Format(oldUIXpath, checkBoxItemText))).FirstOrDefault();

                            if (!itemsToSel.Contains(checkBoxItemText) && currentValChecked)
                            {
                                checkboxItem.Click();
                            }
                            else if (itemsToSel.Contains(checkBoxItemText) && currentValChecked != bool.Parse(IsChecked))
                            {
                                checkboxItem.Click();
                                itemsfound.Add(checkBoxItemText);
                            }
                        }
                        break;
                    case ListType.FilterDialog:
                        foreach (IWebElement item in mItems)
                        {
                            bool currentValChecked = GetCheckedState(null, item);
                            if (itemsToSel.Contains(item.Text) && currentValChecked != bool.Parse(IsChecked))
                            {
                                new DlkBaseControl("CheckBox", item).ScrollIntoViewUsingJavaScript();
                                item.Click();
                                itemsfound.Add(item.Text);
                            }
                        }
                        break;
                }


                if (itemsfound.Count != itemsToSel.Count)
                {
                    throw new Exception($"Item(s) not found {string.Join("~", itemsToSel.Except(itemsfound).ToArray())}");
                }

                DlkLogger.LogInfo("Set() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if CheckboxList exists. Requires strExpectedValue - can either be True or False
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

        #endregion

        #region Private Methods

        private void GetListItems()
        {
            mItems = new List<IWebElement>();

            /*reporting*/
            foreach (IWebElement item in mElement.FindElements(By.XPath(".//table//tr")))
            {
                mItems.Add(item);
            }
            if (mItems.Count > 0)
                listType = ListType.Reporting;

            /*filter dialog*/
            foreach (IWebElement item in mElement.FindElements(By.XPath(".//div[contains(@class,'listItem-wrapper')]")))
            {
                mItems.Add(item);
            }
            if (mItems.Count > 0)
                listType = ListType.FilterDialog;

        }

        private bool GetCheckedState(string CheckBoxItem, IWebElement element)
        {            
            bool isChecked = false;

            switch (listType)
            {
                case ListType.Reporting:
                    var checkBox = mElement.FindElements(By.XPath(string.Format(newUIXpath, CheckBoxItem))).FirstOrDefault();
                    if (checkBox != null)
                    {
                        isNewUI = true;
                        isChecked = checkBox.GetAttribute("class").Contains("checked");
                    }
                    else
                    {
                        checkBox = mElement.FindElements(By.XPath(string.Format(oldUIXpath, CheckBoxItem))).FirstOrDefault();

                        isChecked = bool.TryParse(checkBox.GetAttribute("checked"), out bool res) ? res : false;
                    }
                    break;
                case ListType.FilterDialog:
                    var checkbox = element.FindElement(By.XPath(".//input[@type='checkbox']"));
                    isChecked = bool.TryParse(checkbox.GetAttribute("checked"), out bool chkState) ? chkState : false;
                    break;
            }

            return isChecked;
        }
        #endregion

        enum ListType
        {
            Reporting,
            FilterDialog,
            NotSupported
        }
    }
}
