using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("ColorPalette")]
    public class DlkColorPalette : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkColorPalette(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkColorPalette(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkColorPalette(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkColorPalette(String ControlName, IWebElement ExistingWebElement)
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


        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();

                this.MouseOver();
                Click(5, 5);

                DlkLogger.LogInfo("Successfully executed Click().");
            }
            catch (InvalidOperationException invalid)
            {
                //InvalidOperationCan be due to alert dialog from WaitUrlUpdate call above
                if (invalid.Message.Contains("Modal dialog present"))
                {
                    if (DlkAlert.DoesAlertExist(3))
                    {
                        DlkAlert.Accept();
                        Click();
                    }
                }
                else
                {
                    throw new Exception(string.Format("Exception of type {0} caught in button Click() method.", invalid.GetType()));
                }
            }
            catch (Exception e)
            {
                throw e;

            }
        }

        [Keyword("SetColor", new String[] { "1|text|Color Code|#ffffff" })]
        public void SetColor(String ColorCode)
        {
            try
            {
                Initialize();
                IWebElement txtColorCode = mElement.FindElement(By.ClassName("sp-input smallest"));
                txtColorCode.Click();
                txtColorCode.Clear();
                txtColorCode.SendKeys(ColorCode);
                //DlkLogger.LogDebug("SetColor() : control name = '" + mControlName + "' , text to enter = '" + strColorCode + "'");
                DlkLogger.LogInfo("Successfully executed SetColor()");
            }
            catch (Exception e)
            {
                throw e;                
            }
        }

        [Keyword("VerifyColor", new String[] { "1|text|Expected|#ffffff" })]
        public void VerifyColor(String ColorCode)
        {
            try
            {
                Initialize();
                IWebElement txtColorCode = mElement.FindElement(By.ClassName("sp-input smallest"));
                DlkAssert.AssertEqual("VerifyColor", ColorCode.ToLower(), txtColorCode.Text.ToLower());
                DlkLogger.LogInfo("Successfully executed VerifyColor()");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String strExpectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                try
                {
                    base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                catch (Exception e)
                {
                    throw e;
                }
            }, new String[] { "retry" });
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

    }

}
