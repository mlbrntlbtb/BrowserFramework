using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace SBCLib.DlkControls
{
    [ControlType("SustainabilityGrid")]
    public class DlkSustainabilityGrid : DlkBaseControl
    {
        #region Constructors
        public DlkSustainabilityGrid(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSustainabilityGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSustainabilityGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
            FetchTopics();
        }

        #region Declarations
        private IList<IWebElement> lstTopics = null;
        private const string mTopicListXpath = ".//aside[@class='as_viewtopic']//div[contains(@id,'topic_')]";
        private const string mUpdateTopicXpath = ".//div[contains(@class,'update_topic_table')]//a[contains(@class,'update_topic_link')]";

        #endregion

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

        /// <summary>
        /// Click the update topic button given the index
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("UpdateTopic", new String[] { "1|text|Expected Value|TRUE" })]
        public void UpdateTopic(String RowIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Index: [{RowIndex}] is not a valid integer input.");
                IWebElement element = mElement.FindElements(By.XPath(mUpdateTopicXpath)).ElementAt(rowindex - 1);
                if (element == null) throw new Exception($"Button at index[{RowIndex}] is not found.");
                if (element.GetAttribute("class").ToLower().Contains("disabled")) throw new Exception($"Button at index[{RowIndex}] is currently disabled. Cannot perform click. ");
                element.Click();
                DlkLogger.LogInfo("UpdateTopic() passed");
            }
            catch (Exception e)
            {
                throw new Exception("UpdateTopic() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Select the topic given the index
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("SelectTopic", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectTopic(String RowIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Index: [{RowIndex}] is not a valid integer input.");
                IWebElement element = lstTopics.ElementAt(rowindex - 1);
                if (element == null) throw new Exception($"Topic at index[{RowIndex}] is not found.");
                element.Click();
                DlkLogger.LogInfo("SelectTopic() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectTopic() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Click the cell given the index
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("ClickCell", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickCell(String RowIndex, String ColIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Row Index: [{RowIndex}] is not a valid integer input.");
                if (!Int32.TryParse(ColIndex, out int colindex)) throw new Exception($"Col Index: [{ColIndex}] is not a valid integer input.");

                string gridRowXpath = ".//div[@class='grid_view_container']//div[@id='iso_systemsgird']";
                string gridItemXpath = ".//li[contains(@class,'ng-scope')]/div";

                //Click cell
                IWebElement row = mElement.FindElements(By.XPath(gridRowXpath)).ElementAt(rowindex -1);
                IWebElement target = row.FindElements(By.XPath(gridItemXpath)).ElementAt(colindex - 1);
                new DlkBaseControl("cell", target).ClickUsingJavaScript(false);
                DlkLogger.LogInfo("ClickCell() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickCell() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// erifies if the cell is selected or not
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyCellSelected", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCellSelected(String RowIndex, String ColIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Row Index: [{RowIndex}] is not a valid integer input.");
                if (!Int32.TryParse(ColIndex, out int colindex)) throw new Exception($"Col Index: [{ColIndex}] is not a valid integer input.");

                string gridRowXpath = ".//div[@class='grid_view_container']//div[@id='iso_systemsgird']";
                string gridItemXpath = ".//li[contains(@class,'ng-scope')]/div";

                //Click cell
                IWebElement row = mElement.FindElements(By.XPath(gridRowXpath)).ElementAt(rowindex - 1);
                IWebElement target = row.FindElements(By.XPath(gridItemXpath)).ElementAt(colindex - 1);
                Boolean ActValue = target.GetAttribute("class").Contains("green");
                DlkAssert.AssertEqual("VerifyCellSelected()", Convert.ToBoolean(TrueOrFalse), ActValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCellSelected() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Select the topic given the index in SingleColumnView
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("SingleColumnClick", new String[] { "1|text|Expected Value|TRUE" })]
        public void SingleColumnClick(String RowIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(RowIndex, out int rowindex)) throw new Exception($"Index: [{RowIndex}] is not a valid integer input.");
                string singleRowXpath = ".//div[@class='single_view_container']//div[@class='single_view_row']";
                IWebElement element = mElement.FindElements(By.XPath(singleRowXpath)).ElementAt(rowindex - 1).FindElement(By.XPath(".//a[contains(@id,'lnkSingleViewRow')]"));
                element.Click();
                DlkLogger.LogInfo("SingleColumnClick() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SingleColumnClick() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Select the sustainability given the index in SingleColumnView
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("SingleColumnSelectSustainability", new String[] { "1|text|Expected Value|TRUE" })]
        public void SingleColumnSelectSustainability(String ColIndex)
        {
            try
            {
                Initialize();
                //Guard clauses
                if (!Int32.TryParse(ColIndex, out int colindex)) throw new Exception($"Col Index: [{ColIndex}] is not a valid integer input.");
                string singleColXpath = ".//div[contains(@id,'singleViewTabContent')]//li/div";
                IWebElement element = mElement.FindElements(By.XPath(singleColXpath)).Where(x => x.Displayed).ElementAt(colindex - 1);
                element.Click();
                DlkLogger.LogInfo("SingleColumnSelectSustainability() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SingleColumnSelectSustainability() failed : " + e.Message, e);
            }
        }

        #endregion


        #region Private Methods
        private void FetchTopics()
        {
            lstTopics = mElement.FindElements(By.XPath(mTopicListXpath)).ToList();
        }
        
        #endregion
    }
}
