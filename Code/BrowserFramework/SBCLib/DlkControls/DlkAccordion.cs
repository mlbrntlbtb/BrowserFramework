using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;

namespace SBCLib.DlkControls
{
    [ControlType("Accordion")]
    public class DlkAccordion : DlkBaseControl
    {
        #region Constructors
        public DlkAccordion(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkAccordion(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkAccordion(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IList<IWebElement> lstHeaders = null;
        private const string mHeaderXpath = ".//div[contains(@class,'accordian_header')]";
        private const string mHeaderXpath2 = ".//h2[contains(@class,'SectionHead')]";
        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            FindHeaders();
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
        /// Expands an accordion item
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("Expand", new String[] { "1|text|Expected Value|TRUE" })]
        public void Expand(String Item)
        {
            try
            {
                Initialize();
                IWebElement headerItem = GetHeader(Item);
                Boolean bCurrentValue = GetExpandedState(headerItem);
                if (!bCurrentValue)
                {
                    new DlkBaseControl("Item", headerItem).ScrollIntoViewUsingJavaScript();
                    new DlkBaseControl("Item", headerItem).Click();
                    if (!GetExpandedState(headerItem)) { new DlkBaseControl("Item", headerItem).Click(); } //another click if first one doesn't take effect.

                }
                else
                {
                    DlkLogger.LogInfo("Item is already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("Expand() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Expand() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Collapses an accordion item
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("Collapse", new String[] { "1|text|Expected Value|TRUE" })]
        public void Collapse(String Item)
        {
            try
            {
                Initialize();
                IWebElement headerItem = GetHeader(Item);
                Boolean bCurrentValue = GetExpandedState(headerItem);
                if (bCurrentValue)
                {
                    new DlkBaseControl("Item", headerItem).ScrollIntoViewUsingJavaScript();
                    new DlkBaseControl("Item", headerItem).Click();
                    if (GetExpandedState(headerItem)) { new DlkBaseControl("Item", headerItem).Click(); } //another click if first one doesn't take effect.
                }
                else
                {
                    DlkLogger.LogInfo("Item is already in desired state. No action performed...");
                }
                DlkLogger.LogInfo("Collapse() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Collapse() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies the text of a given header
        /// </summary>
        /// <param name="Item"></param>
        [Keyword("VerifyHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyHeader(String LineIndex, String ExpectedText)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(LineIndex, out int idx)) throw new Exception($"LineIndex: [{LineIndex}] is not a valid integer input.");

                IWebElement headerItem = lstHeaders.ElementAt(idx-1);
                DlkAssert.AssertEqual("VerifyHeader():", ExpectedText.Trim(' '), headerItem.Text.Trim(' '));
                DlkLogger.LogInfo("VerifyHeader() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeader() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Private Methods
        private void FindHeaders()
        {
            lstHeaders = mElement.FindElements(By.XPath(mHeaderXpath)).Count > 0 ?
                mElement.FindElements(By.XPath(mHeaderXpath)).ToList() : 
                mElement.FindElements(By.XPath(mHeaderXpath2)).ToList();
        }

        private IWebElement GetHeader(String Text)
        {
            IWebElement element = lstHeaders.Where(x => DlkString.RemoveCarriageReturn(x.GetAttribute("textContent")).Trim(' ').Equals(Text)).FirstOrDefault();
            if (element == null) throw new Exception($"[{Text}] not found...");
            return element;
        }

        /// <summary>
        /// Returns true if the accordion is in expanded state.
        /// </summary>
        private Boolean GetExpandedState(IWebElement Item)
        {
            string classValue = string.Empty;
            switch(Item.TagName)
            {
                case "h2":
                    classValue = Item.GetAttribute("class");
                    break;
                case "div":
                    if (!String.IsNullOrEmpty(Item.GetAttribute("ng-click")))
                    {
                        IWebElement acc_data = Item.FindElement(By.XPath(".//following-sibling::div[contains(@class,'accordian_data')]"));
                        if (acc_data.Displayed) { classValue = "expanded"; }
                    }
                    else
                    {
                        classValue = Item.FindElement(By.TagName("a")).GetAttribute("class");
                    }
                    break;
                default:
                    throw new Exception("Element not supported.");
            }
            Boolean bCurrentVal = classValue.ToLower().Contains("expanded") ? true: false;
            string bState = bCurrentVal ? "expanded" : "collapsed";
            DlkLogger.LogInfo($"Accordion is in [{ bState }] state");
            return bCurrentVal;
        }

        #endregion
    }
 }

