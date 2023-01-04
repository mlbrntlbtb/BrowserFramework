using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace WorkBookLib.DlkControls
{
    [ControlType("Form")]
    public class DlkForm : DlkBaseControl
    {
        public DlkForm(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkForm(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkForm(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkForm(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("BringToFront")]
        public void BringToFront()
        {
            try
            {
                Initialize();
                mElement = mElement.FindElement(By.XPath("../.."));
                int zIndexValue = Convert.ToInt32(mElement.GetCssValue("z-index")) + 2;
                IJavaScriptExecutor ex = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                string convertedValue = zIndexValue.ToString();
                ex.ExecuteScript("arguments[0].setAttribute('style', 'z-index:" + convertedValue + "')", mElement);
                DlkLogger.LogInfo("BringToFront() passed");
            }
            catch (Exception e)
            {
                throw new Exception("BringToFront() failed : " + e.Message, e);
            }
        }

        [Keyword("MaximizeForm")]
        public void MaximizeForm()
        {
            try
            {
                Initialize();
                if (mElement.FindElements(By.XPath("/following-sibling::div[@class = 'ToolButtons']")).Count > 0)
                {
                    mElement = mElement.FindElement(By.XPath("/following-sibling::div[@class = 'ToolButtons']//div[contains(@class, 'MaximizeDialog')]//*[name()='svg']"));
                }
                Click(4.5);
                DlkLogger.LogInfo("MaximizeForm() passed");
            }
            catch (Exception e)
            {
                throw new Exception("MaximizeForm() failed : " + e.Message, e);
            }
        }

        [Keyword("Close")]
        public void Close()
        {
            try
            {
                Initialize();
                String keyPress = Keys.Alt + "w" + Keys.Alt;
                DlkEnvironment.AutoDriver.FindElement(By.XPath("//html")).SendKeys(keyPress);
                DlkLogger.LogInfo("Close() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Close() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
    }
}