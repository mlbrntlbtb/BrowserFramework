using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("DropDownList")]
    public class DlkDropDownList : DlkBaseControl
    {
        #region PRIVATE VARIABLES

        private string mDropDownListClass;
        private bool mIsDropDownOpen;
        private const String DROPDOWN_CLASS = "dm-dropdown";
        private const String AUTOCOMPLETE_CLASS = "dm-auto-complete";
        private const String FILTERTOGGLE_CLASS = "dm-filter-toggle";
        private const String DIV_CLASS = "div";
        private const String SPAN_CLASS = "span";
        private const String DXM_CLASS = "dxm";

        private const String DropDownClass = "dm-dropdown";
        private const String AutoCompleteClass = "dm-auto-complete";
        private const String FilterToggleClass = "dm-filter-toggle";
        private const String DivClass = "div";
        private const String SpanClass = "span";
        private const String DXMClass = "dxm";

        private IWebElement mDropDownMenu;
        private IWebElement mDropDownButton;

        #endregion

        #region CONSTRUCTORS
        public DlkDropDownList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropDownList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropDownList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize(bool openDropDownList = true)
        {

            FindElement();

            if(!mElement.Displayed)
                this.ScrollIntoViewUsingJavaScript();

            mDropDownListClass = GetDropDownListClass();

            if (openDropDownList)
            {
                mDropDownMenu = GetDropDownMenu();
                mDropDownButton = GetDropDownButton();
                OpenDropDownList();
            }
        }
        #endregion

        #region PRIVATE METHODS

        private string GetDropDownListClass()
        {
            string dropDownClass = mElement.TagName.ToLower();

            //Identify dropdownlist class
            if (dropDownClass.Contains(DIV_CLASS))
            {
                dropDownClass = mElement.GetAttribute("class") != null ?
                    mElement.GetAttribute("class").ToString().ToLower() : string.Empty;

                if (dropDownClass.Contains(DXM_CLASS))
                    dropDownClass = DXM_CLASS;
                else
                    dropDownClass = DIV_CLASS;
            }
            if (dropDownClass.Contains(DROPDOWN_CLASS))
            {
                dropDownClass = DROPDOWN_CLASS;
            }
            else if (dropDownClass.Contains(AUTOCOMPLETE_CLASS))
            {
                dropDownClass = AUTOCOMPLETE_CLASS;
            }
            else if (dropDownClass.Contains(FILTERTOGGLE_CLASS))
            {
                dropDownClass = FILTERTOGGLE_CLASS;
            }
            else if (dropDownClass.Contains(SPAN_CLASS))
            {
                dropDownClass = SPAN_CLASS;
            }

            return dropDownClass;
        }

        private IWebElement GetDropDownMenu()
        {
            IWebElement dropDownMenu;
            string default_DropDownMenu_XPath = ".//*[contains(@class,'dropdown-menu')]";
            string resultWrapper_DropDownMenu_XPath = ".//ancestor::*[contains(@class,'dropdown')]//*[contains(@class,'result-wrapper')]" +
                        " | .//ancestor::*[contains(@class,'dropdown')]//dm-result-wrapper";

            switch (mDropDownListClass)
            {
                case SPAN_CLASS:
                case DIV_CLASS:
                    dropDownMenu = mElement.FindElements(By.XPath(default_DropDownMenu_XPath)).Where(x => x.Displayed).FirstOrDefault();
                    break;
                case FILTERTOGGLE_CLASS:
                    dropDownMenu = mElement.FindElements(By.XPath(resultWrapper_DropDownMenu_XPath)).Where(x => x.Displayed).FirstOrDefault();
                    break;
                case DXM_CLASS:
                    dropDownMenu = null;
                    break;
                default:
                    dropDownMenu = mElement.FindElements(By.XPath(default_DropDownMenu_XPath)).Where(x => x.Displayed).FirstOrDefault();
                    break;
            }

            return dropDownMenu;
        }

        private IWebElement GetDropDownButton()
        {
            IWebElement dropDownButton;
            string default_DropDownButton_XPath = ".//*[contains(@class,'dropdown-toggle')]";
            switch (mDropDownListClass)
            {
                case SPAN_CLASS:
                case DIV_CLASS:
                    dropDownButton = mElement.FindWebElementCoalesce(By.TagName("button"), By.TagName("dm-filter-toggle"));
                    break;
                case FILTERTOGGLE_CLASS:
                    dropDownButton = mElement;
                    break;
                case DXM_CLASS:
                    dropDownButton = null;
                    break;
                default:
                    dropDownButton = mElement.FindElements(By.XPath(default_DropDownButton_XPath)).Where(x => x.Displayed).FirstOrDefault();
                    break;
            }

            return dropDownButton;
        }

        private void OpenDropDownList()
        {
            if (mDropDownMenu == null || !mDropDownMenu.Displayed)
            {
                DlkLogger.LogInfo("Opening dropdown menu... ");
                mDropDownButton.Click();
                mIsDropDownOpen = true;

                string default_DropDownMenu_XPath = ".//*[contains(@class,'dropdown-menu')]";
                string resultWrapper_DropdownMenu_XPath = "//div[contains(@class,'dropdown-menu')]//dm-result-wrapper";
                if (mDropDownMenu == null)
                {
                    mDropDownMenu = mElement.FindElements(By.XPath(default_DropDownMenu_XPath)).Where(x => x.Displayed).Any() ? mElement.FindElement(By.XPath(default_DropDownMenu_XPath)) : 
                        DlkEnvironment.AutoDriver.FindElements(By.XPath(resultWrapper_DropdownMenu_XPath)).Where(x=>x.Displayed).FirstOrDefault();
                }
            }
            WaitDropDownSpinner(); //Wait for dropdown menu spinner to finish
        }

        private void CloseDropDownList()
        {
            if(mIsDropDownOpen)
            {
                DlkLogger.LogInfo("Closing dropdown menu... ");
                mDropDownButton.Click();
            }
        }

        private void WaitDropDownSpinner()
        {
            int waitSpinner = 1;
            int waitSpinnerLimit = 61;
            string dropDownSpinner_XPath = "//*[@class='spinner']";
            bool isSpinnerDisplayed = true;

            while (waitSpinner != waitSpinnerLimit)
            {
                isSpinnerDisplayed = DlkEnvironment.AutoDriver.FindElements(By.XPath(dropDownSpinner_XPath)).Where(x => x.Displayed).Any();
                if (isSpinnerDisplayed)
                {
                    DlkLogger.LogInfo("Waiting for dropdown to finish loading in [" + waitSpinner.ToString() + "]s ...");
                    Thread.Sleep(1000);
                    waitSpinner++;
                }
                else
                    break;
            }

            if(isSpinnerDisplayed)
                throw new Exception("Waiting for dropdown to finish loading has reached it limit (60s).");
        }

        private void SelectItem(string item)
        {
            item = item.Trim();

            bool itemFound = false;
            switch (mDropDownListClass)
            {
                case AUTOCOMPLETE_CLASS:
                case FILTERTOGGLE_CLASS:

                    string dropDownMenuItems_XPath = ".//dm-filter-result//parent::div";
                    List<IWebElement> dropDownMenuItems = mDropDownMenu.FindElements(By.XPath(dropDownMenuItems_XPath)).Where(x => x.Displayed).ToList();
                    foreach (IWebElement dropDownItem in dropDownMenuItems)
                    {
                        DlkBaseControl targetItem = new DlkBaseControl("Target Item", dropDownItem);
                        string itemValue = DlkString.RemoveCarriageReturn(targetItem.GetValue().Trim().ToLower());

                        if (itemValue.Equals(item.ToLower()))
                        {
                            targetItem.Click();
                            DlkLogger.LogInfo("Selecting target item [" + item + "]...");
                            itemFound = true;
                            break;
                        }
                    }

                    break;
                case DROPDOWN_CLASS:
                case SPAN_CLASS:
                case DIV_CLASS:

                    string dropDownMenuItems_XPath1 = ".//*[contains(@class,'dropdown-menu')]//*[normalize-space()='" + item + "']";
                    string dropDownMenuItems_XPath2 = "//*[contains(@class,'dropdown open show')]//*[contains(@class,'dropdown-menu')]//*[normalize-space()='" + item + "']";
                    string dropDownMenuItems_XPath3 = "//*[contains(@class,'dropdown-menu show')]//*[normalize-space()='" + item + "']";
                    IWebElement dropDownMenuItem = mElement.FindWebElementCoalesce(By.XPath(dropDownMenuItems_XPath1), By.XPath(dropDownMenuItems_XPath2), By.XPath(dropDownMenuItems_XPath3));
                    if (dropDownMenuItem != null)
                    {
                        dropDownMenuItem.Click();
                        DlkLogger.LogInfo("Selecting target item [" + item + "]...");
                        itemFound = true;
                    }

                    break;
                case DXM_CLASS:
                    break;
                default:
                    throw new Exception("Dropdown list type not yet supported.");
            }

            if (!itemFound)
                throw new Exception("[" + item + "] not found in the dropdown list.");
        }

        private void SelectItemByIndex(int index)
        {
            string xpath_Results = "//*[contains(@class,'dropdown-menu')]//dm-filter-result/parent::div";
            string xpath_Results2 = ".//*[contains(@class,'dropdown-menu')]//*";
            string xpath_Results3 = "//*[contains(@class,'dropdown-menu')]//*[contains(@class,'result-wrapper')]//div";
            string xpath_Results4 = "//*[contains(@class,'dropdown-menu')]//dm-result-wrapper/div";
            string xpath_Results5 = "//*[contains(@class,'dropdown-menu')]//dm-result-wrapper//div[contains(@class,'dropdown-item')]";

            List<IWebElement> dropDownMenuItems;
            switch (mDropDownListClass)
            {
                case AUTOCOMPLETE_CLASS:
                case FILTERTOGGLE_CLASS:
                    dropDownMenuItems = mElement.FindWebElementsCoalesce(false, By.XPath(xpath_Results), By.XPath(xpath_Results3), By.XPath(xpath_Results4), By.XPath(xpath_Results5)).ToList();
                    break;
                case DROPDOWN_CLASS:
                case SPAN_CLASS:
                case DIV_CLASS:
                    dropDownMenuItems = mElement.FindWebElementsCoalesce(false, By.XPath(xpath_Results2), By.XPath(xpath_Results3), By.XPath(xpath_Results4), By.XPath(xpath_Results5)).ToList();
                    break;
                case DXM_CLASS:
                    dropDownMenuItems = null;
                    break;
                default:
                    throw new Exception("Dropdown list not yet supported.");
            }

            if (dropDownMenuItems != null && index <= dropDownMenuItems.Count)
            {
                DlkLogger.LogInfo("Selecting target item by index[" + index + "]...");
                dropDownMenuItems.ElementAt(index - 1).Click();
            }
            else
                throw new Exception("Item with index [" + index + "] not found in the dropdown list.");
        }

        private void SelectItemContains(string item)
        {
            item = item.Trim();

            bool itemFound = false;
            switch (mDropDownListClass)
            {
                case DROPDOWN_CLASS:
                case DIV_CLASS:
                case SPAN_CLASS:

                    string dDownMenu_XPath1 = ".//*[contains(@class,'dropdown-menu')]//*[contains(normalize-space(),'" + item + "')]";
                    string dDownMenu_XPath2 = "//*[contains(@class,'dropdown open show')]//*[contains(@class,'dropdown-menu')]//*[contains(normalize-space(),'" + item + "')]";
                    string dDownMenu_XPath3 = "//*[contains(@class,'dropdown-menu show')]//*[normalize-space()='" + item + "']";
                    IWebElement dDownMenu = mElement.FindWebElementCoalesce(By.XPath(dDownMenu_XPath1), By.XPath(dDownMenu_XPath2), By.XPath(dDownMenu_XPath3));
                    if (dDownMenu != null)
                    {
                        dDownMenu.Click();
                        DlkLogger.LogInfo("Selecting target item [" + item + "]...");
                        itemFound = true;
                    }
                    break;

                case AUTOCOMPLETE_CLASS:

                    string dDownAutoMenu_XPath = "//*[contains(@class,'dropdown-menu')]//dm-filter-result//parent::div";
                    IList<IWebElement> dropdownAutoMenuItems = DlkEnvironment.AutoDriver.FindElements(By.XPath(dDownAutoMenu_XPath)).Where(x => x.Displayed).ToList();
                    foreach (IWebElement elm in dropdownAutoMenuItems)
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        if (DlkString.RemoveCarriageReturn(ctl.GetValue().Trim()).Contains(item))
                        {
                            ctl.Click();
                            DlkLogger.LogInfo("Selecting target item [" + item + "]...");
                            itemFound = true;
                            break;
                        }
                    }
                    break;

                case FILTERTOGGLE_CLASS:

                    string dDownFilterMenu_XPath = "//.//parent::div/following-sibling::*[contains(@class,'dropdown')]//*[contains(@class,'result-wrapper')]//dm-filter-result//parent::div" +
                        " | //.//parent::div/following-sibling::*[contains(@class,'dropdown')]//dm-result-wrapper//dm-filter-result//parent::div";
                    IList<IWebElement> dropdownFilterMenuItems = DlkEnvironment.AutoDriver.FindElements(By.XPath(dDownFilterMenu_XPath)).Where(x => x.Displayed).ToList();
                    foreach (IWebElement elm in dropdownFilterMenuItems)
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        if (DlkString.RemoveCarriageReturn(ctl.GetValue().Trim()).Contains(item))
                        {
                            ctl.Click();
                            DlkLogger.LogInfo("Selecting target item [" + item + "]...");
                            itemFound = true;
                            break;
                        }
                    }
                    break;

                case DXM_CLASS:
                    break;

                default:
                    throw new Exception("Dropdown list type not yet supported.");
            }

            if (!itemFound)
                throw new Exception("[" + item + "] not found in the dropdown list.");
        }

        private IWebElement GetInputField()
        {
            IWebElement inputField = null;
            switch (mDropDownListClass)
            {
                case FILTERTOGGLE_CLASS:
                    string inputXPath1 = "./../input";
                    string inputXPath2 = "./../../input";
                    string inputXPath3 = "./../../../input";
                    inputField = mElement.FindWebElementCoalesce(By.XPath(inputXPath1), By.XPath(inputXPath2), By.XPath(inputXPath3));
                    break;
                default:
                    inputField = mElement.FindElements(By.TagName("input")).Where(x => x.Displayed).FirstOrDefault();
                    break;
            }
            return inputField;
        }

        private void SetTextValue(string text)
        {
            IWebElement inputField = GetInputField();

            if (inputField != null)
            {
                //clear input
                inputField.SendKeys(Keys.Control + "a");
                inputField.SendKeys(Keys.Backspace);
                DlkLogger.LogInfo("Clearing current value... ");

                //set
                inputField.SendKeys(text.Trim());
                DlkLogger.LogInfo("Setting text value [" + text + "]... ");
            }
            else
                throw new Exception("Input field not found on this drop down type.");
        }

        private string GetTextValue()
        {
            string actualValue = "";
            IWebElement inputField;
            switch (mDropDownListClass)
            {
                case FILTERTOGGLE_CLASS:
                    string filterInputField_XPath = "./following-sibling::input[@formcontrolname='searchField']";
                    inputField = mElement.FindElements(By.XPath(filterInputField_XPath)).Where(x => x.Displayed).FirstOrDefault();
                    actualValue = new DlkBaseControl("Target Input Field", inputField).GetValue();
                    break;
                default:
                    inputField = mElement.FindElements(By.TagName("input")).Where(x => x.Displayed).FirstOrDefault();
                    if(inputField != null)
                        actualValue = new DlkBaseControl("Target Input Field", inputField).GetValue();
                    else
                        actualValue = new DlkBaseControl("Target DropDownList", mElement).GetValue();
                    break;
            }
            return actualValue;
        }

        private string GetPlaceHolderValue()
        {
            string placeHolderValue = "";
            IWebElement inputField = GetInputField();
            if (inputField != null)
                placeHolderValue = new DlkBaseControl("Target Input Field", inputField).GetAttributeValue("placeholder");
            else
                throw new Exception("Input field not found on this drop down type.");
            return placeHolderValue;
        }

        private int GetItemCount()
        {
            int actualCount = 0;
            string dropDownMenuItem_XPath = "";
            switch (mDropDownListClass)
            {
                case DROPDOWN_CLASS:
                    dropDownMenuItem_XPath = ".//*[contains(@class,'dropdown-menu')]/*";
                    break;

                case DIV_CLASS:
                case SPAN_CLASS:
                    dropDownMenuItem_XPath = ".//*[contains(@class,'dropdown-menu')]//*[contains(@class,'dropdown-item')]";
                    break;

                case AUTOCOMPLETE_CLASS:
                    dropDownMenuItem_XPath = "//*[contains(@class,'dropdown-menu')]//dm-filter-result/parent::div";
                    break;

                case FILTERTOGGLE_CLASS:
                    dropDownMenuItem_XPath = "//ancestor::div[contains(@class,'dropdown')]//*[contains(@class,'dropdown-menu')]//dm-filter-result/parent::div";
                    break;

                case DXM_CLASS:
                    break;

                default:
                    throw new Exception("Dropdown list type not yet supported.");
            }

            actualCount = mElement.FindElements(By.XPath(dropDownMenuItem_XPath)).Where(x => x.Displayed).ToList().Count;
            return actualCount;
        }

        private List<IWebElement> GetDropDownMenuItems()
        {
            string dropDownMenuItem_XPath = "";
            List<IWebElement> dropDownMenuItems = null;
            switch (mDropDownListClass)
            {
                case DROPDOWN_CLASS:
                    dropDownMenuItem_XPath = ".//*[contains(@class,'dropdown-menu')]//*";
                    break;

                case DIV_CLASS:
                case SPAN_CLASS:
                    dropDownMenuItem_XPath = ".//*[contains(@class,'dropdown-menu')]//*[contains(@class,'dropdown-item')] | .//*[contains(@class,'dropdown-item')]";
                    break;

                case AUTOCOMPLETE_CLASS:
                    dropDownMenuItem_XPath = "//*[contains(@class,'dropdown-menu')]//dm-filter-result/parent::div | .//dm-filter-result//parent::div";
                    break;

                case FILTERTOGGLE_CLASS:
                    dropDownMenuItem_XPath = ".//ancestor::*[contains(@class,'dropdown')]//*[contains(@class,'result-wrapper')]//dm-filter-result//parent::div" +
                    " | .//ancestor::*[contains(@class,'dropdown')]//dm-result-wrapper//dm-filter-result//parent::div";
                    break;

                case DXM_CLASS:
                    break;

                default:
                    throw new Exception("Dropdown list type not yet supported.");
            }

            dropDownMenuItems = mElement.FindElements(By.XPath(dropDownMenuItem_XPath)).Where(x => x.Displayed).ToList();
            return dropDownMenuItems;
        }

        private string GetStringListItems()
        {
            string actualList = "";
            List<IWebElement> dropDownMenuItems = GetDropDownMenuItems();
            foreach (var item in dropDownMenuItems)
            {
                DlkBaseControl ctl = new DlkBaseControl("Item", item);
                actualList += DlkString.ReplaceCarriageReturn(ctl.GetValue(), " ") + "~";
            }
            actualList = actualList.Trim('~');
            return actualList;
        }

        private IWebElement GetItemInList(string item, bool isContains = false)
        {
            IWebElement targetItem = null;
            string expectedItem = DlkString.ReplaceCarriageReturn(item.ToLower(), " ");
            List<IWebElement> dropDownMenuItems = GetDropDownMenuItems();

            foreach (IWebElement elm in dropDownMenuItems)
            {
                DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                string actualItem = DlkString.ReplaceCarriageReturn(ctl.GetValue().ToLower(), " ");

                if (!isContains)
                {
                    if (actualItem.Equals(expectedItem))
                    {
                        targetItem = elm;
                        DlkLogger.LogInfo("Drop down item [" + item + "] found...");
                        break;
                    }
                }
                else
                {
                    if (actualItem.Contains(expectedItem))
                    {
                        targetItem = elm;
                        DlkLogger.LogInfo("Drop down item containing [" + item + "] found...");
                        break;
                    }
                }
            }
            return targetItem;
        }

        private bool VerifyItemInList(string item, bool isContains = false)
        {
            bool isExist = false;
            string expectedItem = DlkString.ReplaceCarriageReturn(item.ToLower(), " ");
            List<IWebElement> dropDownMenuItems = GetDropDownMenuItems();

            foreach (IWebElement elm in dropDownMenuItems)
            {
                DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                string actualItem = DlkString.ReplaceCarriageReturn(ctl.GetValue().ToLower(), " ");

                if (!isContains)
                {
                    if (actualItem.Equals(expectedItem))
                    {
                        isExist = true;
                        DlkLogger.LogInfo("Drop down item [" + item + "] found...");
                        break;
                    }
                }
                else
                {
                    if (actualItem.Contains(expectedItem))
                    {
                        isExist = true;
                        DlkLogger.LogInfo("Drop down item containing [" + item + "] found...");
                        break;
                    }
                }
            }
            return isExist;
        }

        private IWebElement GetItemByIndex(int index)
        {
            IWebElement targetItem;
            string xpath_Results = "//*[contains(@class,'dropdown-menu')]//dm-filter-result/parent::div";
            string xpath_Results2 = ".//*[contains(@class,'dropdown-menu')]//*";
            string xpath_Results3 = "//*[contains(@class,'dropdown-menu')]//*[contains(@class,'result-wrapper')]//div";
            string xpath_Results4 = "//*[contains(@class,'dropdown-menu')]//dm-result-wrapper/div";
            string xpath_Results5 = "//*[contains(@class,'dropdown-menu')]//dm-result-wrapper//div[contains(@class,'dropdown-item')]";

            List<IWebElement> dropDownMenuItems;
            switch (mDropDownListClass)
            {
                case AUTOCOMPLETE_CLASS:
                case FILTERTOGGLE_CLASS:
                    dropDownMenuItems = mElement.FindWebElementsCoalesce(false, By.XPath(xpath_Results), By.XPath(xpath_Results3), By.XPath(xpath_Results4), By.XPath(xpath_Results5)).ToList();
                    break;
                case DROPDOWN_CLASS:
                case SPAN_CLASS:
                case DIV_CLASS:
                    dropDownMenuItems = mElement.FindWebElementsCoalesce(false, By.XPath(xpath_Results2), By.XPath(xpath_Results3), By.XPath(xpath_Results4), By.XPath(xpath_Results5)).ToList();
                    break;
                case DXM_CLASS:
                    dropDownMenuItems = null;
                    break;
                default:
                    throw new Exception("Dropdown list not yet supported.");
            }

            if (dropDownMenuItems != null && index <= dropDownMenuItems.Count)
            {
                targetItem = dropDownMenuItems.ElementAt(index - 1);
                return targetItem;
            }
            else
                throw new Exception("Item with index [" + index + "] not found in the dropdown list.");
        }

        private string GetItemValueByIndex(int index)
        {
            IWebElement targetItem = GetItemByIndex(index);
            string actualValue = new DlkBaseControl("Target Item", targetItem).GetValue().Trim();
            return actualValue;
        }

        private bool GetItemReadOnlyState(string item)
        {
            IWebElement targetItem = GetItemInList(item, true);
            bool isItemReadOnly = Convert.ToBoolean(new DlkBaseControl("Target Item", targetItem).IsReadOnly());
            return isItemReadOnly;
        }

        private void ScrollUntilLast(int numberOfScrolls)
        {
            string scrollElement_XPath = "//*[contains(@class,'dropdown-menu')]//*[contains(@class,'result-wrapper')]" +
                " | //*[contains(@class,'dropdown-menu')]//dm-result-wrapper";
            IWebElement scrollElement = DlkEnvironment.AutoDriver.FindElements(By.XPath(scrollElement_XPath)).Where(x => x.Displayed).FirstOrDefault();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;

            if (scrollElement != null)
            {
                int scroll = 0;
                while (scroll != numberOfScrolls)
                {
                    scroll++;
                    DlkLogger.LogInfo("Executing scroll action [" + scroll + "] ...");
                    jse.ExecuteScript("arguments[0].scrollTop += 20000", scrollElement);
                    WaitDropDownSpinner();
                }
            }
            else
                throw new Exception("Drop down not supported with a scroll element");
        }

        private void InputConcurrentKey(string specialKey, string concurrentKey, IWebElement input)
        {
            switch (specialKey.ToLower())
            {
                case "ctrl":
                    if (concurrentKey.ToLower().Equals("tab"))
                        input.SendKeys(Keys.Control + Keys.Tab);
                    else
                        input.SendKeys(Keys.Control + concurrentKey.ToLower());
                    break;
                case "shift":
                    if (concurrentKey.ToLower().Equals("tab"))
                        input.SendKeys(Keys.Shift + Keys.Tab);
                    else
                        input.SendKeys(Keys.Shift + concurrentKey.ToLower());
                    break;
                case "alt":
                    if (concurrentKey.ToLower().Equals("tab"))
                        input.SendKeys(Keys.Alt + Keys.Tab);
                    else
                        input.SendKeys(Keys.Alt + concurrentKey.ToLower());
                    break;
                default:
                    throw new Exception("Special key not supported");
            }
        }

        #endregion

        #region KEYWORDS
        [Keyword("AssignValueToVariable", new String[] { "1|text|VariableName|SampleVar" })]
        public new void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize(false);
                string actualValue = GetTextValue();
                DlkVariable.SetVariable(VariableName, actualValue);
                DlkLogger.LogInfo("[" + actualValue + "] is assigned to Variable Name: [" + VariableName + "]");
                DlkLogger.LogInfo("AssignValueToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }
        
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize(false);
                IWebElement dropDownButton = GetDropDownButton();
                dropDownButton.Click();
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();
                SelectItem(Item);
                DlkLogger.LogInfo("Select() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                if (!int.TryParse(Index, out int index) || index == 0)
                    throw new Exception("[" + Index + "] is not a valid input for parameter Index.");

                Initialize();
                SelectItemByIndex(index);
                DlkLogger.LogInfo("SelectByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains", new String[] { "1|text|Value|SampleValue" })]
        public void SelectContains(String SearchItem)
        {
            try
            {
                Initialize();
                SelectItemContains(SearchItem);
                DlkLogger.LogInfo("SelectContains() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyList(string Items)
        {
            try
            {
                Initialize();
                string actualList = GetStringListItems();
                DlkAssert.AssertEqual("VerifyList() :", Items.ToLower(), actualList.ToLower());
                DlkLogger.LogInfo("VerifyList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("GetList", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetList(string VariableName)
        {
            try
            {
                Initialize();
                string actualList = GetStringListItems();
                DlkVariable.SetVariable(VariableName, actualList.ToLower());
                DlkLogger.LogInfo("[" + actualList + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineCount() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInList(string Item, string TrueOrFalse)
        {
            try
            {
                if (!bool.TryParse(TrueOrFalse, out bool expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = VerifyItemInList(Item);
                DlkAssert.AssertEqual("VerifyItemInList() :", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyItemInList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("VerifyContainsItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyContainsItemInList(string PartialItem, string TrueOrFalse)
        {
            try
            {
                if (!bool.TryParse(TrueOrFalse, out bool expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = VerifyItemInList(PartialItem, true);
                DlkAssert.AssertEqual("VerifyContainsItemInList() :", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyContainsItemInList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyContainsItemInList() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("VerifyCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCount(string Count)
        {
            try
            {
                if (!int.TryParse(Count, out int expectedCount) || expectedCount == 0)
                    throw new Exception("[" + Count + "] is not a valid input for parameter Count.");

                Initialize();
                int actualCount = GetItemCount();
                DlkAssert.AssertEqual("VerifyCount() :", expectedCount, actualCount);
                DlkLogger.LogInfo("VerifyCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCount() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("GetLineCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetLineCount(string VariableName)
        {
            try
            {
                Initialize();
                int actualCount = GetItemCount();
                DlkVariable.SetVariable(VariableName, actualCount.ToString());
                DlkLogger.LogInfo("[" + actualCount.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetLineCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineCount() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String Text)
        {
            try
            {
                Initialize(false);
                SetTextValue(Text);
                DlkLogger.LogInfo("SelectContains() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            DlkLogger.LogInfo("VerifyExists() passed");
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize(false);
                string actualValue = GetTextValue();
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue.ToLower(), actualValue.ToLower());
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPlaceholder", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyPlaceholder(String ExpectedValue)
        {
            try
            {
                Initialize(false);
                string placeHolderValue = GetPlaceHolderValue();
                DlkAssert.AssertEqual("VerifyPlaceholder()", ExpectedValue.ToLower(), placeHolderValue.ToLower());
                DlkLogger.LogInfo("VerifyPlaceholder() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPlaceholder() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                if (!bool.TryParse(TrueOrFalse, out bool expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                bool actualValue = Convert.ToBoolean(base.IsReadOnly());
                DlkAssert.AssertEqual("VerifyReadOnly()", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetReadOnlyState", new String[] { "1|text|VariableName|SampleVar" })]
        public void GetReadOnlyState(String VariableName)
        {
            try
            {
                string isReadOnly = base.IsReadOnly();
                DlkVariable.SetVariable(VariableName, isReadOnly);
                DlkLogger.LogInfo("[" + isReadOnly + "] assigned to Variable Name: [" + VariableName + "]");
                DlkLogger.LogInfo("GetReadOnlyState() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetReadOnlyState() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValueOfSelectedIndex", new String[] { "1|text|VariableName|SampleVar" })]
        public void GetValueOfSelectedIndex(String LineIndex, String VariableName)
        {
            try
            {
                if (!int.TryParse(LineIndex, out int index))
                    throw new Exception("[" + LineIndex + "] is not a valid input for parameter LineIndex.");

                Initialize();
                string actualValue = GetItemValueByIndex(index);
                DlkLogger.LogInfo("[" + actualValue + "] assigned to Variable Name: [" + VariableName + "]");
                DlkLogger.LogInfo("GetValueOfSelectedIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetValueOfSelectedIndex() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("GetItemReadOnlyState", new String[] { "1|text|VariableName|SampleVar" })]
        public void GetItemReadOnlyState(String PartialValueText, String VariableName)
        {
            try
            {
                Initialize();
                bool isItemReadOnly = GetItemReadOnlyState(PartialValueText);
                DlkLogger.LogInfo("[" + isItemReadOnly + "] assigned to Variable Name: [" + VariableName + "]");
                DlkLogger.LogInfo("GetItemReadOnlyState() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetItemReadOnlyState() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("ScrollUntilLast")]
        public void ScrollUntilLast(String NumberOfScrolls)
        {
            try
            {
                if (!int.TryParse(NumberOfScrolls, out int scroll) || scroll == 0)
                    throw new Exception("[" + NumberOfScrolls + "] is not a valid input for parameter NumberOfScrolls.");

                Initialize();
                ScrollUntilLast(scroll);
                DlkLogger.LogInfo("ScrollUntilLast() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ScrollUntilLast() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("EnterSimultaneousKeys")]
        public void EnterSimultaneousKeys(String CtrlShiftAlt, String ConcurrentKey)
        {
            try
            {
                Initialize(false);
                IWebElement inputField = GetInputField();
                InputConcurrentKey(CtrlShiftAlt, ConcurrentKey, inputField);
                DlkLogger.LogInfo("EnterSimultaneousKeys() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("EnterSimultaneousKeys() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactList(string Items)
        {
            try
            {
                Initialize();
                string actualList = GetStringListItems();
                DlkAssert.AssertEqual("VerifyExactList() :", Items, actualList);
                DlkLogger.LogInfo("VerifyExactList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactList() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("VerifyExactItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExactItemInList(string Item, string TrueOrFalse)
        {
            try
            {
                if (!bool.TryParse(TrueOrFalse, out bool expectedValue))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                Initialize();
                bool actualValue = VerifyItemInList(Item);
                DlkAssert.AssertEqual("VerifyExactItemInList() :", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyExactItemInList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactItemInList() failed : " + e.Message, e);
            }
            finally
            {
                CloseDropDownList();
            }
        }

        [Keyword("VerifyExactText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactText(String ExpectedValue)
        {
            try
            {
                Initialize(false);
                string actualValue = GetTextValue();
                DlkAssert.AssertEqual("VerifyExactText()", ExpectedValue, actualValue);
                DlkLogger.LogInfo("VerifyExactText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactPlaceholder", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactPlaceholder(String ExpectedValue)
        {
            try
            {
                Initialize(false);
                string placeHolderValue = GetPlaceHolderValue();
                DlkAssert.AssertEqual("VerifyExactPlaceholder()", ExpectedValue, placeHolderValue);
                DlkLogger.LogInfo("VerifyExactPlaceholder() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactPlaceholder() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
