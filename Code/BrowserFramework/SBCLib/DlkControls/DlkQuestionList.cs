using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace SBCLib.DlkControls
{
    [ControlType("QuestionList")]
    public class DlkQuestionList : DlkBaseControl
    {
        #region Constructors
        public DlkQuestionList(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkQuestionList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkQuestionList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IList<IWebElement> lstQItems = null;
        private IList<IWebElement> lstAnswers = null;
        private const string mQItemXpath = ".//div[contains(@class,'question_list_item')]";
        private const string mQItemAnswersXpath = ".//following-sibling::div[contains(@class,'question_panel')]//div[contains(@class,'answer_text')]";
        private const string mQItemActionXpath = ".//following-sibling::div[contains(@class,'question_panel')]//div[contains(@class,'project_action')]";
        private const string mContainerXpath = ".//ancestor::div[@id='rightPanelContainer']//div[contains(@class,'question_list_container')]";

        #endregion

        public void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
            FetchQuestionItems();
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bExpected = mElement.FindElement(By.XPath(mContainerXpath)).GetAttribute("class").Contains("disable"); 
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), bExpected);
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickQuestion", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickQuestion( String QuestionIndex)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(QuestionIndex, out int index)) throw new Exception($"Index: [{QuestionIndex}] is not a valid integer input.");
                if (index < 0) throw new Exception("Index value should not be less than 0");
                IWebElement qItem = lstQItems.ElementAt(index-1);
                new DlkBaseControl("Item", qItem).ScrollIntoViewUsingJavaScript();
                qItem.Click();
                DlkLogger.LogInfo("ClickQuestion() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickQuestion() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickQuestionAction", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickQuestionAction(String QuestionIndex, String Action)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(QuestionIndex, out int index)) throw new Exception($"Index: [{QuestionIndex}] is not a valid integer input.");
                if (index < 0) throw new Exception("Index value should not be less than 0");

                IWebElement qItem = lstQItems.ElementAt(index-1);
                IWebElement qAction = qItem.FindElements(By.XPath(mQItemActionXpath)).Where(x => x.Text.Equals(Action)).FirstOrDefault();
                if (qAction == null) throw new Exception($"Action [{Action}]  not found");
                new DlkBaseControl("Item", qAction).ScrollIntoViewUsingJavaScript();
                qAction.Click();
                DlkLogger.LogInfo("ClickQuestionAction() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickQuestionAction() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickAnswer", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickAnswer(String QuestionIndex, String AnswerIndex)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(QuestionIndex, out int index)) throw new Exception($"Index: [{QuestionIndex}] is not a valid integer input.");
                if (!Int32.TryParse(AnswerIndex, out int aindex)) throw new Exception($"Index: [{AnswerIndex}] is not a valid integer input.");
                if (index < 0 || aindex < 0) throw new Exception("Index value should not be less than 0");

                IWebElement qItem = lstQItems.ElementAt(index - 1);
                FetchAnswers(qItem);
                IWebElement qAnswer = lstAnswers.ElementAt(aindex - 1);
                ClickPseudoElement(qAnswer);
                DlkLogger.LogInfo("ClickAnswer() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickAnswer() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValueParam", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValueParam(String QuestionIndex, String AnswerIndex, String IsChecked)
        {
            try
            {
                Initialize();
                if (!Int32.TryParse(QuestionIndex, out int index)) throw new Exception($"Index: [{QuestionIndex}] is not a valid integer input.");
                if (!Int32.TryParse(AnswerIndex, out int aindex)) throw new Exception($"Index: [{AnswerIndex}] is not a valid integer input.");
                if (index < 0 || aindex < 0) throw new Exception("Index value should not be less than 0");

                IWebElement qItem = lstQItems.ElementAt(index - 1);
                FetchAnswers(qItem);
                IWebElement qAnswer = lstAnswers.ElementAt(aindex - 1);
                bool bExpected = qAnswer.GetAttribute("class").Contains("answered");
                DlkAssert.AssertEqual("VerifyValueParam()", Convert.ToBoolean(IsChecked), bExpected );
                DlkLogger.LogInfo("VerifyValueParam() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValueParam() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private void FetchQuestionItems()
        {
            lstQItems = mElement.FindElements(By.XPath(mQItemXpath)).ToList();
        }

        private void FetchAnswers(IWebElement Item)
        {
            lstAnswers = Item.FindElements(By.XPath(mQItemAnswersXpath)).ToList();
        }

        /// <summary>
        /// Click the estimated area of a pseudo-element item location
        /// </summary>
        /// <param name="Item"></param>
        private void ClickPseudoElement(IWebElement Item)
        {
            //Set offset values
            int INT_ELEMENT_PADDING = 5;
            int INT_OFFSET_Y = Item.Size.Height / 2;

            //Perform click
            Actions mAction = new Actions(DlkEnvironment.AutoDriver);
            mAction.MoveToElement(Item,INT_ELEMENT_PADDING, INT_OFFSET_Y).Click().Perform();
            Thread.Sleep(1000);
        }
        #endregion
    }
}
