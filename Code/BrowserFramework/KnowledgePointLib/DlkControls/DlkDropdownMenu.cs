using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("DropdownMenu")]
    public class DlkDropdownMenu : DlkBaseControl
    {
        #region Constructors
        public DlkDropdownMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropdownMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropdownMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private const string popOverContainer = "//div[contains(@class,'MuiPopover-root popOverMenu')]//ul";
        private const string popOverContainerRounded = "//div[contains(@class,'MuiPopover-paper')]//ul";
        private const string autoCompleteContainer = "//div[contains(@class,'MuiAutocomplete-paper')]//ul";
        private const string autoCompleteOpenButton = ".//following-sibling::div[contains(@class,'MuiAutocomplete')]";
        private string dropdownMenuType = null;

        #endregion
        private void Initialize()
        {
            FindElement();
            dropdownMenuType = GetDropDownMenuType();
        }

        #region Keywords


        [Keyword("AssignValueToVariable")]
        public new void AssignValueToVariable(String VariableName)
        {
            Initialize();
            string mValue = null;
            switch (dropdownMenuType)
            {
                case "autoCompleteList":
                    mValue = DlkString.ReplaceCarriageReturn(mElement.GetAttribute("value").Trim(), "\n");
                    break;
                case "autoCompleteComboBox":
                    mValue = DlkString.ReplaceCarriageReturn(mElement.FindElements(By.XPath(".//input")).FirstOrDefault().GetAttribute("value").Trim(), "\n");
                    if (mValue == "")
                        mValue = DlkString.ReplaceCarriageReturn(mElement.FindElements(By.XPath(".//span")).FirstOrDefault().Text.Trim(), "\n");
                    break;
                case "popOverRounded":
                    mValue = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "\n");
                    break;
                default:
                    mValue = DlkString.ReplaceCarriageReturn(mElement.GetAttribute("value").Trim(), "\n");
                    break;
            }

            DlkVariable.SetVariable(VariableName, mValue.TrimEnd());
            DlkLogger.LogInfo("AssignValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
        }

        /// <summary>
        /// Clicks an element
        /// </summary>

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                Click(4.5);

                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Selects an item inside the dropdown menu with the given value
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("Select", new String[] { "1|text|Expected Value|SampleValue" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                ShowDropdownMenuList();
                GetDropdownMenuItem(Value).Click();
                DlkLogger.LogInfo("Select() passed");
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Select() failed. Item: '" + Value + "' does exist in the list.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if control exists. Requires MenuPath and TrueOrFalse - can either be True or False.  
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyItemExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemExists(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                ShowDropdownMenuList();
                IWebElement dropdownMenuItem = GetDropdownMenuItem(Item);
                bool itemExists = true;
                if (dropdownMenuItem == null)
                    itemExists = false;
                DlkAssert.AssertEqual("VerifyItemExists() : " + mControlName, Convert.ToBoolean(TrueOrFalse), itemExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() failed : " + e.Message, e);
            }
        }


        /// <summary>
        ///  Verifies if an element exists. Requires TrueOrFalse - can either be True or False
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
        /// Verifies if control is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower(), base.IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies text of an element
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyText", new String[] { "1|text|Expected Value|Text" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();
                string actualResult = null;
                switch (dropdownMenuType)
                {
                    case "autoCompleteList":
                        actualResult = DlkString.ReplaceCarriageReturn(mElement.GetAttribute("value").Trim(), "\n");
                        break;
                    case "autoCompleteComboBox":
                        actualResult = DlkString.ReplaceCarriageReturn(mElement.FindElements(By.XPath(".//input")).FirstOrDefault().GetAttribute("value").Trim(), "\n");
                        if (actualResult == "")
                            actualResult = DlkString.ReplaceCarriageReturn(mElement.FindElements(By.XPath(".//span")).FirstOrDefault().Text.Trim(), "\n");
                        break;
                    case "popOverRounded":
                        var hasLabel = mElement.FindElements(By.XPath(".//label[@data-shrink='true']")).FirstOrDefault();
                        if (hasLabel != null)
                            actualResult = mElement.FindElements(By.XPath(".//div[@aria-haspopup='listbox']")).FirstOrDefault().Text.Trim();
                        else
                            actualResult = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "\n");
                        break;
                    default:
                        actualResult = DlkString.ReplaceCarriageReturn(mElement.GetAttribute("value").Trim(), "\n");
                        break;
                }
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n");
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Gets dropdown menu type
        /// </summary>
        /// <returns></returns>
        private string GetDropDownMenuType()
        {
            try
            {
                string type = null;
                if (mElement.FindElements(By.XPath("./parent::*[contains(@class, 'CardHeader-action')]")).Any())
                    type = "cardButtonMenu";
                else if (mElement.TagName.Equals("button")) // mapped as button
                    type = "popOver";
                else if (mElement.GetAttribute("aria-autocomplete") == "list") // mapped as input. elements that enables autocomplete
                    type = "autoCompleteList";
                else if (mElement.TagName.Equals("input"))
                    type = "dropdownTransformsToInput"; // For elements that are dropdown menu when editable, but on some forms appear as input when disabled (Screen: AccountSettings)
                else if (mElement.GetAttribute("class").Contains("MuiAutocomplete-hasPopupIcon") && mElement.GetAttribute("role") == "combobox") //almost same as combobox; appears in projectoverviewdate
                    type = "autoCompleteComboBox";
                else if (mElement.GetAttribute("role") == "combobox")
                    type = "comboBox";
                // mapped as div where text value (ex. View) is located || /div/div -> second level in DOM for specific dropdownmenu
                else if (mElement.GetAttribute("aria-haspopup") == "listbox" || mElement.FindElements(By.XPath("./div/div")).FirstOrDefault().GetAttribute("aria-haspopup") == "listbox")
                    type = "popOverRounded";
                return type;
            }
            catch
            {
                throw new Exception("Unsupported dropdown menu type");
            }
        }

        private IWebElement GetDropdownMenuItem(string dropdownValue)
        {
            IWebElement item;

            switch (dropdownMenuType)
            {
                case "popOver":
                    item = mElement.FindElements(By.XPath(popOverContainer + "//span[text()='" + dropdownValue + "']")).FirstOrDefault();
                    break;
                case "popOverRounded":
                    item = mElement.FindElements(By.XPath(popOverContainerRounded + "//li[text()='" + dropdownValue + "']")).FirstOrDefault();
                    break;
                case "comboBox":
                    item = mElement.FindElements(By.XPath("//div[@role='listbox']//p[text()='" + dropdownValue + "']")).FirstOrDefault();
                    break;
                case "autoCompleteComboBox":
                    // instead of showing dropdown menu, will search instead
                    var input = mElement.FindElement(By.XPath(".//input"));
                    input.SendKeys(Keys.Control + "a" + Keys.Backspace); // clear input 
                    input.SendKeys(@dropdownValue); // then search for value
                    item = mElement.FindElement(By.XPath("//*[contains(@class,'MuiAutocomplete-listbox')]//*[text()='" + dropdownValue + "']"));
                    break;
                case "dropdownTransformsToInput":
                    mElement.SendKeys(Keys.Control + "a" + Keys.Backspace); // clear input 
                    mElement.SendKeys(dropdownValue); // then search for value
                    Thread.Sleep(DlkEnvironment.mMediumWaitMs);

                    int count = 0;
                    var dropdownToInputXpath = By.XPath("//*[contains(@class,'MuiAutocomplete-listbox')]//*[text()='" + dropdownValue + "']");

                    while (!mElement.FindElements(dropdownToInputXpath).Any(elem => elem.Displayed) && count < 5)
                    {
                        Thread.Sleep(DlkEnvironment.mMediumWaitMs);
                        count++;
                    }

                    item = mElement.FindElement(dropdownToInputXpath);
                    break;
                case "autoCompleteList":
                    item = mElement.FindElements(By.XPath(autoCompleteContainer + "//li[text()='" + dropdownValue + "']")).FirstOrDefault();
                    if (item == null)
                        item = mElement.FindElements(By.XPath(autoCompleteContainer + "//li//p[text()='" + dropdownValue + "']")).FirstOrDefault();
                    break;
                case "cardButtonMenu":
                    item = mElement.FindElements(By.XPath("//*[@role='menuitem' and contains(text(), '" + dropdownValue + "')]")).FirstOrDefault();
                        break;
                default:
                    item = null;
                    break;
            }
            return item;
        }

        /// <summary>
        /// Shows dropdown menu list by clicking the button wrapper
        /// </summary>
        private void ShowDropdownMenuList()
        {
            if (!IsDropdownMenuOpen()) // if dropdown menu is closed, open/click the menu
            {
                switch (dropdownMenuType)
                {
                    case "autoCompleteList":
                        mElement.FindElement(By.XPath(autoCompleteOpenButton)).Click();
                        break;
                    case "comboBox":
                        mElement.FindElement(By.XPath(".//button[contains(@class,'popupIndicator')]")).Click();
                        break;
                    case "autoCompleteComboBox":
                        // do nothing
                        break;
                    case "popOver":
                    case "popOverRounded":
                    case "dropdownTransformsToInput":
                        Click(4.5);
                        break;
                    default:
                        break;
                }
            }
            else // if its already open
            {
                DlkLogger.LogInfo("Dropdown menu is already open"); // do nothing, just log info
            }

        }
        private bool IsDropdownMenuOpen()
        {
            bool isOpen = false;

            switch (dropdownMenuType)
            {
                case "autoCompleteList":
                    string menuIndicator = mElement.FindElement(By.XPath(autoCompleteOpenButton + "//button[contains(@class,'MuiAutocomplete-popupIndicator')]")).GetAttribute("aria-label");
                    if (menuIndicator == "Close")
                        isOpen = true;
                    else
                        isOpen = false;
                    break;
                case "comboBox":
                    string comboBoxIndicator = mElement.GetAttribute("aria-expanded");
                    if (comboBoxIndicator == "true")
                        isOpen = true;
                    else
                        isOpen = false;
                    break;
                case "autoCompleteComboBox":
                    break;
                case "popOver":
                case "popOverRounded":
                    if (mElement.FindElements(By.XPath(popOverContainer)).Count > 0 || mElement.FindElements(By.XPath(popOverContainerRounded)).Count > 0)
                        isOpen = true;
                    else
                        isOpen = false;
                    break;
                default:
                    break;
            }
            return isOpen;
        }

        #endregion
    }
}