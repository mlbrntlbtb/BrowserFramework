using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Threading;
using WorkBookLib.DlkSystem;
using System.Text.RegularExpressions;

namespace WorkBookLib.DlkControls
{
    [ControlType("Toolbar")]
    public class DlkToolbar : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkToolbar(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkToolbar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToolbar(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkToolbar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PRIVATE VARIABLES
        private List<IWebElement> mButtons;
        private String mToolbarButtonsXPath = ".//*[contains(@class,'MenuButton')]";
        private String mToolbarButtonsXPath2 = ".//*[contains(@class,'MenuItem')][not(contains(@class,'MenuItemsContainer'))]";
        private String mToolbarButtonsXPath3 = ".//*[@title]";
        private String mToolbarArrowUpXPath = ".//*[contains(@class,'ScrollArrow vertical')]";
        private String mToolbarArrowDownXPath = ".//*[contains(@class,'ScrollArrow Last vertical')]";
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            GetToolbarButtons();
        }

        public string GetControlValue(IWebElement item, String controlName) {
            String value = "";

            value = new DlkBaseControl(controlName, item).GetValue().Trim();
            if (!new Regex("[@#^\":{}|<>=]").IsMatch(value) && !double.TryParse(value , out double x)) {
                return value;
            }
            value = item.GetAttribute("title");
            if ((value != "") && (value != null)) {
                return value;
            }
            value = item.GetAttribute("placeholder");
            if ((value != "") && (value != null)) {
                return value;
            }
            return "";
        }

        #endregion

        #region PRIVATE METHODS
        private void GetToolbarButtons()
        {
            mButtons = mElement.FindWebElementsCoalesce(false, By.XPath(mToolbarButtonsXPath), By.XPath(mToolbarButtonsXPath2), By.XPath(mToolbarButtonsXPath3))
                .Where(x => x.Displayed).ToList();
               
            if (mButtons.Count > 0)
                return;
            else
                throw new Exception("Toolbar buttons not found.");
        }
        
        private string GetToolbarButtonsToString() {
            String buttons = "";
            foreach (IWebElement item in mButtons) {
                buttons += "~" + GetControlValue(item, "mToolbarItem");
            }
            return buttons.Trim('~');
        }

        private void SelectToolbarButton(string ButtonCaption)
        {
            bool bFound = false;

            foreach (IWebElement aButton in mButtons)
            {
                if (aButton.GetAttribute("title").ToLower().Contains(ButtonCaption.ToLower()) ||
                    aButton.Text.ToLower().Contains(ButtonCaption.ToLower()))
                {
                    DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                    btnToolbarButton.ClickUsingJavaScript();
                    bFound = true;
                    break;
                }
            }

            if (!bFound)
            {
                IWebElement ArrowUp = mElement.FindElements(By.XPath(mToolbarArrowUpXPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(mToolbarArrowUpXPath)) : null;
                IWebElement ArrowDown = mElement.FindElements(By.XPath(mToolbarArrowDownXPath)).Count > 0 ?
                    mElement.FindElement(By.XPath(mToolbarArrowDownXPath)) : null;

                if (ArrowUp != null & ArrowDown != null)
                {
                    string firstToolbar = mButtons[0].GetAttribute("title").ToLower();
                    string currentFirstToolbar = "";

                    //Verify toolbar buttons using arrow up
                    while (currentFirstToolbar != firstToolbar)
                    {
                        ArrowUp.Click();
                        Thread.Sleep(1000);
                        firstToolbar = mButtons[0].GetAttribute("title").ToLower();
                        GetToolbarButtons();
                        currentFirstToolbar = mButtons[0].GetAttribute("title").ToLower();
                        if (currentFirstToolbar != firstToolbar)
                        {
                            foreach (IWebElement aButton in mButtons)
                            {
                                if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower() ||
                                    aButton.Text.ToLower().Contains(ButtonCaption.ToLower()))
                                {
                                    DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                                    btnToolbarButton.ClickUsingJavaScript();
                                    bFound = true;
                                    break;
                                }
                            }
                        }
                        if (bFound)
                            break;
                    }

                    //Verify toolbar buttons using arrow down
                    if (!bFound)
                    {
                        currentFirstToolbar = "";
                        while (currentFirstToolbar != firstToolbar)
                        {
                            ArrowDown.Click();
                            Thread.Sleep(1000);
                            firstToolbar = mButtons[0].GetAttribute("title").ToLower();
                            GetToolbarButtons();
                            currentFirstToolbar = mButtons[0].GetAttribute("title").ToLower();
                            if (currentFirstToolbar != firstToolbar)
                            {
                                foreach (IWebElement aButton in mButtons)
                                {
                                    if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower() ||
                                        aButton.Text.ToLower().Contains(ButtonCaption.ToLower()))
                                    {
                                        DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                                        btnToolbarButton.ClickUsingJavaScript();
                                        bFound = true;
                                        break;
                                    }
                                }
                            }
                            if (bFound)
                                break;
                        }
                    }
                }
                else
                {
                    throw new Exception("Toolbar button ['" + ButtonCaption + "'] not found");
                }
            }

            if (!bFound)
            {
                throw new Exception("[" + ButtonCaption + "] not found. Actual toolbar items: " + GetToolbarButtonsToString());
            }
        }

        private void SelectToolbarButtonByIndex(int index)
        {
            bool bFound = false;

            int currentIndex = 0;
            int targetIndex = index - 1;
            foreach (IWebElement aButton in mButtons)
            {
                if(currentIndex == targetIndex)
                {
                    DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                    btnToolbarButton.ClickUsingJavaScript();
                    bFound = true;
                    break;
                }
                currentIndex++;
            }

            if (!bFound)
            {
                throw new Exception("Button with index [" + index.ToString() + "] not found. Actual toolbar count: " + mButtons.Count.ToString());
            }
        }
        #endregion

        #region KEYWORDS


        [Keyword("VerifyToolbarItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyToolbarItems(String ExpectedValue) {
            try {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                DlkAssert.AssertEqual("VerifyToolbarItems(): ", ExpectedValue.Trim(), GetToolbarButtonsToString());
                DlkLogger.LogInfo("VerifyToolbarItems() passed");
            } catch (Exception e) {
                throw new Exception("VerifyToolbarItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetToolbarItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetToolbarItems(String VariableName) {
            try {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetToolbarButtonsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetToolbarItems() passed");
            } catch (Exception e) {
                throw new Exception("GetToolbarItems() failed : " + e.Message, e);
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

        [Keyword("ClickToolbarButton", new String[] { "1|text|Button Caption|Save" })]
        public void ClickToolbarButton(String ButtonCaption)
        {
            try
            {
                Initialize();
                SelectToolbarButton(ButtonCaption);
                DlkLogger.LogInfo("Successfully executed ClickToolbarButton()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarButton() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickToolbarButtonByIndex", new String[] { "1|text|Button Index|1" })]
        public void ClickToolbarButtonByIndex(String ButtonIndex)
        {
            try
            {
                if (!int.TryParse(ButtonIndex, out int index) || index == 0)
                    throw new Exception("[" + ButtonIndex + "] is not a valid input for parameter ButtonIndex.");

                Initialize();
                SelectToolbarButtonByIndex(index);
                DlkLogger.LogInfo("Successfully executed ClickToolbarButtonByIndex()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarButtonByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToolbarButtonExist", new String[] {"1|text|Button Caption|Save",
                                                              "2|text|Expected Value|True " })]
        public void VerifyToolbarButtonExist(String ButtonCaption, String TrueOrFalse)
        {
            bool bFound = false;

            try
            {
                Initialize();

                //mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower() ||
                        aButton.Text.ToLower().Contains(ButtonCaption.ToLower()))
                    {
                        if (aButton.Displayed)
                        {
                            bFound = true;
                        }
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyToolbarButtonExist()", Convert.ToBoolean(TrueOrFalse), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolbarButtonExist() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
