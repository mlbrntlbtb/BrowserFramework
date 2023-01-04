using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace SBCLib.DlkControls
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
        /// Verifies if control is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyItemReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemReadOnly(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement radioButton = GetItem(Item);
                DlkAssert.AssertEqual("VerifyItemReadOnly() : ", TrueOrFalse.ToLower(), new DlkBaseControl("Item", radioButton).IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyItemReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects a radiobutton in a given radiobutton group
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("Select", new String[] { "1|text|Expected Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
               Initialize();
                IWebElement radioButton = GetItem(Item);
                radioButton.Click();
                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectByIndex(String Index)
        {
            try
            {
                Initialize();
                int index;

                if (!int.TryParse(Index, out index))
                    throw new Exception($"Invalid index: {Index}");

                var radioButtons = mElement.FindElements(By.XPath(".//input"));

                if(radioButtons.Count == 0)
                    throw new Exception($"Radio buttons not found.");

                if (index > radioButtons.Count && index < 1)
                    throw new Exception($"Invalid index [{index}] should be within [1 - {radioButtons.Count}]");

                radioButtons[index - 1].Click();
                DlkLogger.LogInfo("SelectByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetOptionsCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetOptionsCount(String VariableName)
        {
            try
            {
                Initialize();

                var radioButtons = mElement.FindElements(By.XPath(".//input"));
                if (radioButtons.Count == 0)
                    throw new Exception($"Radio buttons not found.");

                DlkVariable.SetVariable(VariableName, radioButtons.Count.ToString());

                DlkLogger.LogInfo("GetOptionsCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetOptionsCount() failed : " + e.Message, e);
            }
        }
        
        [Keyword("VerifyOptionsCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyOptionsCount(String ExpectedCount)
        {
            try
            {
                bool validInt = int.TryParse(ExpectedCount, out int expectedCount);
                if (!validInt) throw new Exception($"ExpectedCount [{ExpectedCount}] should be a number.");

                Initialize();

                var radioButtons = mElement.FindElements(By.XPath(".//input"));
                if (radioButtons.Count == 0)
                    throw new Exception($"Radio buttons not found.");

                DlkAssert.AssertEqual("VerifyOptionsCount() : ", expectedCount, radioButtons.Count);
                DlkLogger.LogInfo("VerifyOptionsCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyOptionsCount() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods

        private IWebElement GetItem(String Text)
        {
            IWebElement element = null;
            element = mElement.FindWebElementCoalesce(By.XPath($".//input[@value='{Text}']"),
                    By.XPath($".//label[contains(.,'{ Text }')]/preceding-sibling::input"), 
                    By.XPath($".//label[contains(.,'{ Text }')]//ancestor::div[contains(@class,'formatDiv')]//preceding-sibling::label"),
                    By.XPath($".//parent::div//span[contains(@title,'{ Text }')]/preceding-sibling::input"),
                    By.XPath($".//span[contains(.,'{ Text }')]/preceding-sibling::div[contains(@class,'radio_manufacturer')]"),
                    By.XPath($".//li[contains(.,'{ Text }')]/input"),
                    By.XPath($".//span[contains(.,'{ Text }')]/preceding-sibling::input"));
            if (element == null)
            {
                throw new Exception($"RadioButton [{ Text }] not found.");
            }
            return element;
        }
        #endregion
    }
 }

