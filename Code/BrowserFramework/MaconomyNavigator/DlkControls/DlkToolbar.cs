using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;


namespace MaconomyNavigatorLib.DlkControls
{
    [ControlType("Toolbar")]
    public class DlkToolbar : DlkBaseControl
    {
        private ReadOnlyCollection<IWebElement> mButtons;
        private String mStrToolbarButtons = ".//*[contains(@class,'toolbar-icon')]";
        private String mStrImageButtons = ".//*[contains(@class,'centered pointer')]";

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

        public void VerifyToolbarButton(String pStrButtonCaption, String pStrProperty, String pStrExpectedValue)
        {
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

        //[Keyword("VerifyToolbarButtonReadOnly", new String[] {"1|text|Button Caption|Save", 
        //                                                      "2|text|Expected Value|True (if button is expected to be read only)" })]
        //public void VerifyToolbarButtonReadOnly(String ButtonCaption, String ExpectedValue)
        //{
        //    bool bFound = false;
        //    bool bReadOnly = false;
        //    String[] arrInputString = ButtonCaption.Split('~');

        //    try
        //    {
        //        Initialize();


        //        DlkLogger.LogInfo("VerifyToolbarButtonReadOnly() passed");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyToolbarButtonReadOnly() failed : " + e.Message, e);
        //    }
        //}

        private void GetButtons()
        {
            mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));

            if (mButtons.Count > 0)
            {
                return;
            }
            mButtons = mElement.FindElements(By.XPath(mStrImageButtons));
        }
    }
}
