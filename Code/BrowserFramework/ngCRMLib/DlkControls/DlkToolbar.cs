using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace ngCRMLib.DlkControls
{
    [ControlType("Toolbar")]
    public class DlkToolbar : DlkBaseControl
    {
        private ReadOnlyCollection<IWebElement> mButtons;
        private String mStrToolbarButtons = ".//*[contains(@class,'toolbar-icon')]";
        private String mStrImageButtons = ".//*[contains(@class,'centered pointer')]";
        private String mStrQuickEditorButtons = ".//*[@role='button']";
        private String mStrNavToolbarButtons = ".//*[contains(@class,'tool')]";
        private String mStrNavPMToolbarButtons = ".//*[contains(@class,'cmdBarIcon')]";

        public DlkToolbar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToolbar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
            GetButtons();

        }

        [Keyword("ClickToolbarButton", new String[] { "1|text|Button Caption|Save" })]
        public void ClickToolbarButton(String ButtonCaption)
        {
            bool bFound = false;

            try
            {
                Initialize();
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower())
                    {
                        DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                        btnToolbarButton.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Toolbar button '" + ButtonCaption + "' not found");
                }

                DlkLogger.LogInfo("Successfully executed ClickToolbarButton()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarButton() failed : " + e.Message, e);
            }
        }

        //public void VerifyToolbarButton(String pStrButtonCaption, String pStrProperty, String pStrExpectedValue)
        //{
        //}


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
                    if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower())
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

        [Keyword("VerifyToolbarButtonReadOnly", new String[] {"1|text|Button Caption|Save", 
                                                              "2|text|Expected Value|True (if button is expected to be read only)" })]
        public void VerifyToolbarButtonReadOnly(String ButtonCaption, String TrueOrFalse)
        {
            try
            {
                Initialize();
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower())
                    {
                        DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                        DlkAssert.AssertEqual("VerifyToolbarButtonReadOnly()", bool.Parse(TrueOrFalse), bool.Parse(btnToolbarButton.IsReadOnly()));
                        DlkLogger.LogInfo("VerifyToolbarButtonReadOnly() passed");
                        break;
                    }
                }
                
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolbarButtonReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("GetButtonState", new String[] { "1|text|Button Caption|Save", 
                                                    "2|text|VariableName|ButtonState"})]
        public void GetButtonState(String ButtonCaption, String VariableName)
        {         
            try
            {
                Initialize();
                string buttonState = string.Empty;

                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == ButtonCaption.ToLower())
                    {
                        DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                        string buttonReadOnly = btnToolbarButton.IsReadOnly();

                        if (buttonReadOnly == "true")
                        {
                            buttonState = "Disabled";
                        }
                        else if (buttonReadOnly == "false")
                        {
                            buttonState = "Enabled";
                        }

                        DlkVariable.SetVariable(VariableName, buttonState);
                        break;
                    }
                    buttonState = "Disabled";
                    DlkVariable.SetVariable(VariableName, buttonState);
                }

            }
            catch (Exception e)
            {
                throw new Exception("GetButtonState() failed : " + e.Message, e);
            }
        }

        private void GetButtons()
        {
            mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));
            if (mButtons.Count > 0)
            {
                return;
            }
            mButtons = mElement.FindElements(By.XPath(mStrImageButtons));
            if (mButtons.Count > 0)
            {
                return;
            }
            mButtons = mElement.FindElements(By.XPath(mStrQuickEditorButtons));
            if (mButtons.Count > 0)
            {
                return;
            }
            mButtons = mElement.FindElements(By.XPath(mStrNavToolbarButtons));
            if (mButtons.Count > 0)
            {
                return;
            }
            mButtons = mElement.FindElements(By.XPath(mStrNavPMToolbarButtons));
            if (mButtons.Count > 0)
            {
                return;
            }
        }
    }
}
