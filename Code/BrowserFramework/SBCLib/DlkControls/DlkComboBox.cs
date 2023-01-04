using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Collections.Generic;
using System.Linq;

namespace SBCLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        #region Constructors
        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IList<IWebElement> lstItems = null;
        private const string mListBoxXPath = ".//ul[@role='listbox']";
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        #region Keywords

        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
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
        /// Inputs text into a combobox control
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("SetText", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetText(String TextToEnter)
        {
            try
            {
                Initialize();
                IWebElement mTextbox = mElement.FindElement(By.TagName("input"));
                mTextbox.SendKeys(Keys.Control + "a");
                if (!string.IsNullOrEmpty(TextToEnter))
                {
                    mTextbox.SendKeys(TextToEnter);
                }
                DlkLogger.LogInfo("SetText() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies the value of  the control
        /// </summary>
        /// <param name="ExpectedText"></param>
        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyValue(String ExpectedText)
        {
            try
            {
                Initialize();
                IWebElement mTextbox = mElement.FindElement(By.TagName("input"));
                DlkAssert.AssertEqual("VerifyValue()", ExpectedText.Trim(' '), new DlkBaseControl(this.mControlName, mTextbox).GetValue().Trim(' '));
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Expected Value|SampleValue" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                FindComboBoxItems();
                if(lstItems != null)
                {
                    IWebElement option = GetItem(Value);
                    new DlkBaseControl("Option", option).ClickUsingJavaScript();
                }
                else
                {
                    throw new Exception("No combobox list open or found.");
                }
                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Expected Value|SampleValue" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                if (!Int32.TryParse(Index, out int index)) throw new Exception($"Index: [{Index}] is not a valid integer input.");
                Initialize();
                FindComboBoxItems();
                if (lstItems != null)
                {
                    IWebElement option = lstItems.ElementAt(index - 1);
                    new DlkBaseControl("Option", option).ClickUsingJavaScript();
                }
                else
                {
                    throw new Exception("No combobox list open or found.");
                }
                DlkLogger.LogInfo("SelectByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Clears text inside a combobox control
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("Clear", new String[] { "1|text|Expected Value|TRUE" })]
        public void Clear()
        {
            try
            {
                Initialize();
                IWebElement mTextbox = mElement.FindElement(By.TagName("input"));
                if (!String.IsNullOrWhiteSpace(mTextbox.GetAttribute("value"))) mTextbox.SendKeys(Keys.Control + "a" + Keys.Delete);
                DlkLogger.LogInfo("Successfully executed Clear()");
            }
            catch (Exception e)
            {
                throw new Exception("Clear() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private void FindComboBoxItems()
        {
            int maxRetry = 3;
            int retry = 0;
            do
            {
                //Click the textbox input to trigger the listbox options
                new DlkBaseControl("Textbox", mElement.FindElement(By.TagName("input"))).Click();
                IWebElement lstBox = mElement.FindElements(By.XPath(mListBoxXPath)).FirstOrDefault();
                if (lstBox != null)
                {
                    lstItems = lstBox.FindElements(By.TagName("li")).ToList();
                    break;
                }
                else
                {
                    DlkLogger.LogInfo("Listbox not found. Retrying...");
                    retry++;
                }
            } while (retry <= maxRetry);
        }
        private IWebElement GetItem(String Text)
        {
            IWebElement element = lstItems.Where(x => x.FindElement(By.XPath(".//span[@class='suggestions']")).Text.Trim(' ').Equals(Text)).FirstOrDefault();
            if (element == null) throw new Exception($"[{Text}] not found...");
            return element;
        }
        #endregion
    }
 }

