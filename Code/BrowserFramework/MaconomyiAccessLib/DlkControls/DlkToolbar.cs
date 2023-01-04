using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Toolbar")]
    public class DlkToolbar : DlkBaseControl
    {

        #region PRIVATE VARIABLES

        private ReadOnlyCollection<IWebElement> mButtons;
        private String mStrToolbarButtons = ".//*[contains(@class,'toolbar-icon')]";
        private String mStrImageButtons = ".//*[contains(@class,'centered pointer')]";

        #endregion

        #region CONSTRUCTORS

        public DlkToolbar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToolbar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            GetButtons();

        }

        public void GetButtons()
        {
            mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));

            if (mButtons.Count > 0)
            {
                return;
            }
            mButtons = mElement.FindElements(By.XPath(mStrImageButtons));
        }

        #endregion

        #region KEYWORDS

        [Keyword("ClickToolbarButton", new String[] { "1|text|Button Caption|Save" })]
        public void ClickToolbarButton(String pStrButtonCaption)
        {
            bool bFound = false;

            try
            {
                Initialize();
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == pStrButtonCaption.ToLower())
                    {
                        DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                        btnToolbarButton.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Toolbar button '" + pStrButtonCaption + "' not found");
                }

                DlkLogger.LogInfo("Successfully executed ClickToolbarButton()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToolbarButtonExist", new String[] {"1|text|Button Caption|Save",
                                                              "2|text|Expected Value|True " })]
        public void VerifyToolbarButtonExist(String ButtonCaption, String ExpectedValue)
        {
            bool bFound = false;

            try
            {
                Initialize();

                mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower())
                    {
                        bFound = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyToolbarButtonExist()", Convert.ToBoolean(ExpectedValue), bFound);
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

        #endregion

    }
}
