using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkBaseControl
    {
        #region Constructors
        public DlkRadioButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRadioButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        private string radioButtonType = null;

        private void Initialize()
        {
            FindElement();
            radioButtonType = GetRadioButtonType();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if RadioButton exists. Requires strExpectedValue - can either be True or False
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
        [Keyword("Select", new String[] { "1|text|Text To Verify|Sample RadioButton Text" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                IWebElement radioButton = null;
                switch (radioButtonType)
                {
                    case "multipleConnection":
                        //xpath for iframe with no id
                        radioButton = mElement.FindElements(By.XPath($".//td[contains(text(),'{Value}')]/parent::tr//input")).FirstOrDefault();

                        //xpath for iframe with id
                        if(radioButton == null)
                            radioButton = mElement.FindElements(By.XPath(".//input[@_dispname='" + Value + "']")).FirstOrDefault();
                        break;
                    case "fromToRange":
                        radioButton = mElement.FindElements(By.XPath("./following-sibling::tr//span[contains(text(),'" + Value + "')]")).FirstOrDefault();
                        break;
                    case "defaultRadioButton":
                        radioButton = mElement.FindElements(By.XPath(".//div[@aria-label='" + Value + "']")).FirstOrDefault();

                        if (radioButton == null)
                        {
                            radioButton = GetRadioButtonNoBreakTag(Value);

                            if (radioButton == null && mElement.TagName == "input")
                                radioButton = mElement;
                        }
                        break;
                    default:
                        break;
                }
                radioButton.Click(); 
                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private methods
        private string GetRadioButtonType()
        {
            try
            {
                string type = null;
                if (mElement.GetAttribute("specname") == "selectDataSourceSignon" || 
                        mElement.FindElements(By.XPath("//input[@type='radio' and contains(@value,'dataSource')]")).Count > 0)  // Support for multiple database connection
                    type = "multipleConnection";
                else if (mElement.GetAttribute("prole") == "label") // For MRP Message Report
                    type = "fromToRange"; 
                else
                    type = "defaultRadioButton";
                return type;
            }
            catch
            {
                throw new Exception("Unsupported dropdown menu type");
            }
        }

        private IWebElement GetRadioButtonNoBreakTag(string value)
        {
            List<IWebElement> items = mElement.FindElements(By.XPath(".//div[@class='clsCheckBoxRow']")).ToList();

            foreach (var item in items)
            {
                if (item.Text == value)
                {
                    return item.FindElement(By.XPath("./div"));
                }
            }
            return null;
        }
        #endregion

    }
}
