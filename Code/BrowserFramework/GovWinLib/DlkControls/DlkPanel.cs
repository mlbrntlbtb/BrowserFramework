using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{

    [ControlType("Panel")]
    public class DlkPanel : DlkBaseControl
    {
        private Boolean IsInit = false;
        public DlkPanel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPanel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPanel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkPanel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        
        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        [Keyword("ClickLink")]
        public void ClickLink()
        {
            Initialize();
            DlkLink panelBodyLink = new DlkLink("Panel Body Link", this, "XPATH", "./following-sibling::div[1]/.//a");
            if (panelBodyLink.Exists(1))
            {
                panelBodyLink.Click();
            }
            else
            {
                throw new Exception("ClickLink() failed. The panel does not have a link.");
            }
        }

        private String TrimValue(String sValue)
        {
            sValue = sValue.Replace("\n", " ");
            sValue = sValue.Replace("\r", " ");
            while (sValue.Contains("  "))
            {
                sValue = sValue.Replace("  ", " ");
            }
            return sValue.Trim();
        }

        #region Verify methods
        [RetryKeyword("VerifyPartialText", new String[] { "1|text|Expected Value|Overview" })]
        public void VerifyPartialText(String PartialText)
        {
            this.PerformAction(() =>
            {
                Initialize();
                String strExpected = PartialText;
                bool actualResult = false;

                if (mElement.Text.Contains(strExpected))
                    actualResult = true;

                DlkAssert.AssertEqual("VerifyPartialText()", true, actualResult);
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                VerifyExists(Convert.ToBoolean(expectedValue));
            }, new String[]{"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyHeaderText", new String[] { "1|text|Expected Value|Sample text" })]
        public void VerifyHeaderText(String HeaderText)
        {
            String expectedValue = HeaderText;

            this.PerformAction(() =>
            {
                Initialize();
                string actualValue = GetValue().Split("\r\n".ToCharArray()).Last();
                DlkAssert.AssertEqual("VerifyHeaderText()", expectedValue.TrimStart(' ').TrimEnd(' '),actualValue.TrimStart(' ').TrimEnd(' '));
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyText", new String[] { "1|text|Expected Value|Sample text" })]
        public void VerifyText(String ExpectedValue)
        {
            String expectedValue = ExpectedValue;

            this.PerformAction(() =>
            {
                Initialize();
                DlkBaseControl panelBody = new DlkBaseControl("Panel Body", this, "XPATH", "./following-sibling::div[1]");
                if (panelBody.Exists(1))
                {
                    String ActualValue = TrimValue(panelBody.GetValue());
                    DlkAssert.AssertEqual("VerifyText()", expectedValue, ActualValue);
                }
                else
                {
                    throw new Exception("VerifyText() failed. This panel does not have a text body.");
                }
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyList", new String[] { "1|text|Expected Bullet Texts|Text1~Text2~Text3" })]
        public void VerifyList(String ExpectedBulletTexts)
        {
            String expectedValue = ExpectedBulletTexts;

            this.PerformAction(() =>
            {
                Initialize();
                IList<IWebElement> listItems = mElement.FindElements(By.XPath("./following-sibling::div[1]/ul/li"));
                String sActualValue = "";
                foreach (IWebElement item in listItems)
                {
                    DlkBaseControl itemControl = new DlkBaseControl("List Item", item);
                    if (sActualValue != "")
                    {
                        sActualValue = sActualValue + "~";
                    }
                    sActualValue = sActualValue + TrimValue(itemControl.GetValue());
                }

                DlkAssert.AssertEqual("VerifyList()", expectedValue.ToLower(), sActualValue.ToLower());
            }, new String[]{"retry"});
        }

        #endregion
    }
}

