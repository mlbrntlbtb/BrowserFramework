using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        #region Constructors
        public DlkMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private const string SIDEBAR_WRAPPER = "//div[contains(@class,'docked sidebarNav')]";

        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if label exists. Requires TrueOrFalse - can either be True or False
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
        /// Verifies label's content. Requires ExpectedText parameter
        /// </summary>
        /// <param name="TextToVerify"></param>

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedText)
        {
            try
            {
                Initialize();

                string actualResult = DlkString.ReplaceCarriageReturn(mElement.Text.Trim(), "\n").ToUpper();
                string textToVerify = DlkString.ReplaceCarriageReturn(ExpectedText, "\n").ToUpper();

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, actualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects a menu item
        /// </summary>
        /// <param name="SelectMenuItem"></param>

        [Keyword("SelectMenuItem")]
        public void SelectMenuItem(String MenuItem)
        {
            try
            {
                Initialize();

                if (MenuItem.Contains("~")) // has sidebar submenu; currently supports two levels of select only
                {
                    var menuItems = MenuItem.Split('~');
                    mElement.FindElement(By.XPath(".//*[text()='" + menuItems[0] + "']")).Click(); // click the parent
                    mElement.FindElement(By.XPath(".//*[text()='" + menuItems[1] + "']")).Click(); // click the child
                }
                else
                {
                    mElement.FindElement(By.XPath(".//*[text()='" + MenuItem + "']")).Click();
                }
                DlkLogger.LogInfo("SelectMenuItem() passed");
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Menu item not found");
            }
            catch (Exception e)
            {
                throw new Exception("SelectMenuItem() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
