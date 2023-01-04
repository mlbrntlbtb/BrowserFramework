using System;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;

namespace SBCLib.DlkControls
{
    [ControlType("OutputDocument")]
    public class DlkOutputDocument : DlkBaseControl
    {
        #region Constructors
        public DlkOutputDocument(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkOutputDocument(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkOutputDocument(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IList<IWebElement> lstItems = null;
        private const string mOutputItemXpath = ".//div[@class='output_document_project']";
        private const string mNameXpath = ".//span[contains(@class,'output_document_name')][contains(@data-bind,'FullFileName')]";
        private const string mDateXpath = ".//span[contains(@class,'output_document_date')]";
        private const string mActionXpath = ".//span[contains(@class,'output_document_action')]";

        #endregion

        private void Initialize()
        {
            FindElement();
            FindItems();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if control exists. Requires TrueOrFalse - can either be True or False
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

        [Keyword("VerifyName", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyName(String Index, String ExpectedText)
        {
            try
            {
                Initialize();
                IWebElement outputItem = GetItemByIndex(Index);
                String ActualValue = outputItem.FindElement(By.XPath(mNameXpath)).GetAttribute("textContent");
                DlkAssert.AssertEqual("VerifyName() : " + mControlName, ExpectedText.Trim(), ActualValue.Trim());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyName() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDate", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyDate(String Index, String ExpectedText)
        {
            try
            {
                Initialize();
                IWebElement outputItem = GetItemByIndex(Index);
                String ActualValue = outputItem.FindElement(By.XPath(mDateXpath)).GetAttribute("textContent");
                DlkAssert.AssertEqual("VerifyDate() : " + mControlName, ExpectedText.Trim(), ActualValue.Trim());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDate() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickAction", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickAction(String Index, String Action)
        {
            try
            {
                Initialize();
                IWebElement outputItem = GetItemByIndex(Index);
                IWebElement actionItem = outputItem.FindElements(By.XPath(mActionXpath)).Where(x => x.Displayed).FirstOrDefault().FindElements(By.TagName("a")).Where(x => x.Text.Equals(Action)).FirstOrDefault();
                if (actionItem == null) throw new Exception($"Action [{Action}] not found");
                actionItem.Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickAction() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Assigns the supplied variable name to whatever GetValue() returns
        /// </summary>
        /// <param name="VariableName"></param>
        [Keyword("AssignValueToVariable")]
        public void AssignValueToVariable(string Index, string VariableName)
        {
            try
            {
                Initialize();
                IWebElement outputItem = GetItemByIndex(Index);
                String txtValue = DlkString.ReplaceCarriageReturn( outputItem.Text, " ").TrimEnd();
                if (string.IsNullOrEmpty(txtValue))
                {
                    DlkVariable.SetVariable(VariableName, string.Empty);
                }
                else
                {
                    DlkLogger.LogInfo("AssignValueToVariable()", mControlName, $"Variable:[{VariableName}], Value:[{txtValue}].");
                }
                DlkLogger.LogInfo("Successfully executed AssignValueToVariable().");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }


        /// <summary>
        /// Assigns the supplied variable name to whatever GetValue() returns
        /// </summary>
        /// <param name="VariableName"></param>
        [Keyword("AssignPartialValueToVariable")]
        public void AssignPartialValueToVariable(string Index, string VariableName, string StartIndex, string Length)
        {
            try
            {
                Initialize();
                IWebElement outputItem = GetItemByIndex(Index);
                String txtValue = DlkString.ReplaceCarriageReturn(outputItem.Text, " ").TrimEnd();
                if (string.IsNullOrEmpty(txtValue))
                {
                    DlkVariable.SetVariable(VariableName, string.Empty);
                }
                else
                {
                    string mValue = txtValue.Substring(int.Parse(StartIndex), int.Parse(Length));
                    DlkVariable.SetVariable(VariableName, mValue );
                    DlkLogger.LogInfo("AssignValueToVariable()", mControlName, $"Variable:[{VariableName}], Value:[{mValue}].");
                }
                DlkLogger.LogInfo("Successfully executed AssignPartialValueToVariable().");
            }
            catch (Exception e)
            {
                throw new Exception("AssignPartialValueToVariable() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private void FindItems()
        {
            lstItems = mElement.FindElements(By.XPath(mOutputItemXpath)).ToList();
        }

        private IWebElement GetItemByIndex(String Index)
        {
            if (!Int32.TryParse(Index, out int index)) throw new Exception($"Index: [{Index}] is not a valid integer input.");
            IWebElement element = lstItems.ElementAt(index -1);
            return element;
        }
        #endregion
    }
}
