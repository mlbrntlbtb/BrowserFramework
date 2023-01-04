using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("RadioButtonSearch")]
    public class DlkRadioButtonSearch : DlkBaseControl
    {
        #region Constructors
        public DlkRadioButtonSearch(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButtonSearch(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRadioButtonSearch(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if RadioButtonSearch exists. Requires strExpectedValue - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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
        /// Selects item in radio button
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("Select", new String[] { "1|text|Text To Verify|Sample RadioButtonSearch Text" })]
        public void Select(String Keyword, String Value)
        {
            try
            {
                Initialize();
                //Input keyword first
                mElement.FindElements(By.XPath(".//input[@class='clsSearchText']|.//input[@class='clsSelectWithSearchSearchText']"))
                    .FirstOrDefault()
                    .SendKeys(Keyword + Keys.Enter);
                // Wait for 3 seconds before page completes reloading the reinitialize to prevent stale error
                Thread.Sleep(3000); // To replace with wait for spinner
                Initialize();
                if (Value != "")
                {
                    IWebElement radioButtonItem = mElement.FindElements(By.XPath("//span[contains(text(),'" + Value + "')]/preceding-sibling::img[contains(@class,'clsLVRadio')]")).FirstOrDefault();
                    if (radioButtonItem != null)
                        radioButtonItem.Click();
                    else
                        throw new Exception("Item does not exist.");
                }
                else
                {
                    // Do nothing. Just input the keyword
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        #endregion


    }
}
