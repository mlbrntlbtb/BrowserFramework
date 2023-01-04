using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace VisionCRMLib.DlkControls
{
    [ControlType("Picker")]
    public class DlkPicker : DlkBaseControl
    {
        public DlkPicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private string mItemsXPath = ".//div[contains(@class, 'x-dataview-item')]";
        private string mSelectedItemXPath = ".//div[contains(@class,'x-item-selected')]/div[contains(@class,'x-picker-item')]";
        private string mPreviousItemXPath = ".//div[contains(@class,'x-item-selected')]/preceding-sibling::div[1]";
        private string mNextItemXPath = ".//div[contains(@class,'x-item-selected')]/following-sibling::div";
        //private List<IWebElement> mItems;

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                Thread.Sleep(1000);
                List<IWebElement> mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
                bool bToBeSelectedFound = false;
                bool bCurrentSelectedFound = false;
                int iCurrentIndex = 0;
                int iToBeSelectedIndex = 0;
                for (int i = 0; i < mItems.Count; i++)
                {
                    if (mItems[i].FindElement(By.CssSelector("div")).GetAttribute("innerHTML") == Value)
                    {
                        iToBeSelectedIndex = i;
                        bToBeSelectedFound = true;
                    }
                    if (mItems[i].GetAttribute("class").Contains("x-item-selected"))
                    {
                        iCurrentIndex = i;
                        bCurrentSelectedFound = true;
                    }
                    if (bToBeSelectedFound && bCurrentSelectedFound)
                    {
                        break;
                    }
                }
                if (!bToBeSelectedFound)
                {
                    throw new Exception("Select() failed. Unable to find value '" + Value + "'");
                }
                int iDiff = iToBeSelectedIndex - iCurrentIndex;
                if (iDiff < 0)
                {
                    for (int i = iDiff; i < 0; i++)
                    {
                        IWebElement previousElement = mElement.FindElement(By.XPath(mPreviousItemXPath));
                        DlkBaseControl previousItem = new DlkBaseControl("Previous", previousElement);
                        previousItem.Click();
                    }
                }
                else if (iDiff > 0)
                {
                    for (int i = iDiff; i > 0; i--)
                    {
                        IWebElement nextElement = mElement.FindElement(By.XPath(mNextItemXPath));
                        DlkBaseControl nextItem = new DlkBaseControl("Next", nextElement);
                        nextItem.Click();
                    }
                }


                //Refresh the selected item control
                Initialize();
                IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                ctlActualSelected.Click();
                DlkAssert.AssertEqual("Select", Value, ctlActualSelected.GetAttributeValue("innerHTML"));

            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement selectedItem = mElement.FindElement(By.XPath(mSelectedItemXPath));
                DlkBaseControl ctlActualSelected = new DlkBaseControl("ActualSelected", selectedItem);
                DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedValue, ctlActualSelected.GetValue());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
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

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bFound = false;

                string listItemXpath = ".//*[contains(@class,'picker-item')]";

                if (mElement.FindElements(By.XPath(listItemXpath)).Count > 0)
                {
                    IReadOnlyCollection<IWebElement> pickerItems = mElement.FindElements(By.XPath(listItemXpath));

                    bFound = pickerItems.Count(x => x.GetAttribute("textContent").ToLower().Trim() == Item.ToLower().Trim()) > 0;
                }

                DlkAssert.AssertEqual("VerifyAvailableInList()", Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

    }
}
