using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using StormWebLib.System;


namespace StormWebLib.DlkControls
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
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
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
                        btnToolbarButton.ClickUsingJavaScript();
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

        [Keyword("SelectToolbarOption", new String[] { "1|text|Button Caption|Font Name", 
                                                    "2|text|Option|Arial"})]
        public void SelectToolbarOption(String ComboBoxCaption, String Option)
        {
            Initialize();
            try
            {
                IWebElement comboBox = null;
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == ComboBoxCaption.ToLower() &&
                        aButton.FindElements(By.XPath("./span[contains(@class, 'combo_text')]")).Count > 0)
                    {
                        DlkBaseControl btnToolbarButton = new DlkBaseControl("ToolbarButton", aButton);
                        btnToolbarButton.Click();
                        DlkLogger.LogInfo("Successfully clicked '" + ComboBoxCaption + "'");

                        comboBox = aButton;
                        break;
                    }
                }

                if (comboBox == null) throw new Exception("Unable to find ComboBox '" + Option + "'");

                DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath("//iframe[@class='cke_panel_frame']")));

                string optionXpath = String.Format(".//div[@role='listbox'][@title='{0}']//a[@title='{1}']", ComboBoxCaption, Option);
                var mOption = DlkEnvironment.AutoDriver.FindElement(By.XPath(optionXpath));
                mOption.Click();
                DlkLogger.LogInfo("Successfully clicked '" + mOption.Text + "'");
            }
            catch (Exception e)
            {
                throw new Exception("SelectToolbarOption() failed : " + e.Message, e);
            }
            finally
            {
                //Revert to the default frame
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                Thread.Sleep(500);
            }
        }


        [Keyword("VerifyValue", new String[] { "1|text|Button Caption|Font Name", 
                                                    "2|text|ExpectedValue|Arial"})]
        public void VerifyValue(String ComboBoxCaption, String ExpectedValue)
        {
            Initialize();
            try
            {
                string ActualValue = null;
                bool bFound = false;
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.GetAttribute("title").ToLower() == ComboBoxCaption.ToLower())
                    {
                        if(aButton.GetAttribute("class").Contains("cke_button_on"))
                        {
                            ActualValue = Boolean.TrueString;
                        }
                        else if(aButton.GetAttribute("class").Contains("cke_button_off"))
                        {
                            ActualValue = Boolean.FalseString;
                        }
                        else
                        {
                            DlkBaseControl ctrl = new DlkBaseControl("ToolbarCtrl", aButton);
                            ActualValue = ctrl.GetValue().Trim();
                        }
                        bFound = true;
                        break;
                    }
                }

                if (!bFound) throw new Exception("Unable to find toolbar control.");

                DlkAssert.AssertEqual("VerifyValue()", ExpectedValue, ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
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
