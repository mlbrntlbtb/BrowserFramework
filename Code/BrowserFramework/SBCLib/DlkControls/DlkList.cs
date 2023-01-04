using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Linq;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace SBCLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {

        #region Constructors
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
        : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
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
        /// <param name="ExpectedValue"></param>
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
        /// Verify text of a list item with a given index
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyListItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyListItem(String Index, String ExpectedText)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(Index, out int index)) throw new Exception($"Index: [{Index}] is not a valid integer input.");
                string Actual = DlkString.RemoveCarriageReturn(mElement.FindElements(By.TagName("li")).ElementAt(index-1).Text.Trim(' '));
                string Expected = DlkString.RemoveCarriageReturn(ExpectedText.Trim(' '));
                DlkAssert.AssertEqual("VerifyListItem() : " + mControlName, Expected, Actual);
                DlkLogger.LogInfo("VerifyListItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyListItem() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Click a link inside a list item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("ClickItemLink", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickItemLink(String Index)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(Index, out int index)) throw new Exception($"Index: [{Index}] is not a valid integer input.");
                IWebElement item = mElement.FindElements(By.TagName("li")).ElementAt(index - 1);
                IWebElement link = item.FindElements(By.TagName("a")).FirstOrDefault();
                if (link == null) throw new Exception($"No link found at item [{index}]");
                new DlkLink("Link", link).Click();
                DlkLogger.LogInfo("ClickItemLink() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemLink() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Click a link inside a list item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("GetListItemText", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetListItemText(String Index, String VariableName)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(Index, out int index)) throw new Exception($"Index: [{Index}] is not a valid integer input.");
                string Text = DlkString.RemoveCarriageReturn(mElement.FindElements(By.TagName("li")).ElementAt(index - 1).Text.Trim(' '));
                DlkVariable.SetVariable(VariableName, Text);
                DlkLogger.LogInfo($"GetListItemText() passed. Variable:[{VariableName}], Value:[{Text}]");
            }
            catch (Exception e)
            {
                throw new Exception("GetListItemText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects a list item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("SelectItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItem(String Index)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(Index, out int index)) throw new Exception($"Index: [{Index}] is not a valid integer input.");
                IWebElement item = mElement.FindElements(By.TagName("li")).ElementAt(index - 1);
                IWebElement check = item.FindElements(By.XPath(".//div[contains(@class,'check')]")).FirstOrDefault();
                if (check == null) throw new Exception($"No selection found at item [{index}]");

                //Regular Click doesn't work here. Click by coordinates instead.
                int INT_ELEMENT_PADDING = check.Size.Width / 2;
                int INT_OFFSET_Y = 3;
                Actions mAction = new Actions(DlkEnvironment.AutoDriver);
                mAction.MoveToElement(check, INT_ELEMENT_PADDING, INT_OFFSET_Y).Click().Perform();
                Thread.Sleep(1000);
                DlkLogger.LogInfo("SelectItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItem() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects a list item
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifySelectedByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySelectedByIndex(String Index, String IsSelected)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(Index, out int index)) throw new Exception($"Index: [{Index}] is not a valid integer input.");
                IWebElement item = mElement.FindElements(By.TagName("li")).ElementAt(index - 1);
                IWebElement check = item.FindElements(By.XPath(".//div[contains(@class,'check')]")).FirstOrDefault();
                if (check == null) throw new Exception($"No selection found at item [{index}]");
                Boolean bChecked = check.GetAttribute("class").Contains("selected");
                DlkAssert.AssertEqual("VerifySelectedByIndex()", Convert.ToBoolean(IsSelected), bChecked);
                DlkLogger.LogInfo("VerifySelectedByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedByIndex() failed : " + e.Message, e);
            }
        }
        #endregion

    }
}
