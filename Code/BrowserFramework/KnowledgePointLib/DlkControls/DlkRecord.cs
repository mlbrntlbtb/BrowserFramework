using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Record")]
    public class DlkRecord : DlkBaseControl
    {
        #region Constructors
        public DlkRecord(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRecord(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRecord(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Keywords
        /// <summary>
        ///  Verifies if Record control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse">True if record area exists, false if not</param>
        [Keyword("VerifyExists", new String[] { "1|text|TrueOrFalse|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies number of records
        /// </summary>
        /// <param name="ExpectedCount">Expected number of records (0, 1, etc.)</param>
        [Keyword("VerifyRecordCount", new String[] { "1|text|ExpectedCount|Value" })]
        public void VerifyRecordCount(String ExpectedCount)
        {
            try
            {
                Initialize();
                int.TryParse(ExpectedCount, out int expectedCount);
                var records = GetRecordItems(mElement);

                DlkAssert.AssertEqual("VerifyRecordCount(): ", expectedCount, records.Count);
                DlkLogger.LogInfo("VerifyRecordCount() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRecordCount() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Expands a specific record using its full text as value.
        /// </summary>
        /// <param name="RecordText">Full text of a specific record item</param>
        [Keyword("ExpandRecordByText", new String[] { "1|text|RecordText|Value" })]
        public void ExpandRecordByText(String RecordText)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var expandButton = records.FirstOrDefault(record => record.Text.ToLower().Trim() == RecordText.ToLower().Trim()).FindElement(By.XPath(".//*[contains(@data-testid, 'expand')]"));
                expandButton.Click();
                DlkLogger.LogInfo("ExpandRecordByTitle() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandRecordByTitle() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Expands a specific record using partial text as value.
        /// </summary>
        /// <param name="PartialText">Partial text of a specific record item</param>
        [Keyword("ExpandRecordByPartialText", new String[] { "1|text|PartialText|Value" })]
        public void ExpandRecordByPartialText(String PartialText)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var expandButton = records.FirstOrDefault(record => record.Text.ToLower().Trim().Contains(PartialText.ToLower().Trim())).FindElement(By.XPath(".//*[contains(@data-testid, 'expand')]"));
                expandButton.Click();
                DlkLogger.LogInfo("ExpandRecordByPartialText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandRecordByPartialText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies number of records
        /// </summary>
        /// <param name="RecordIndex">Index of a specific record item. Starts with 1</param>
        [Keyword("ExpandRecordByIndex", new String[] { "1|text|Index|1" })]
        public void ExpandRecordByIndex(String RecordIndex)
        {
            try
            {
                Initialize();
                int.TryParse(RecordIndex, out int index);
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var expandButton = records[index - 1].FindElement(By.XPath(".//*[contains(@data-testid, 'expand')]"));
                expandButton.Click();
                DlkLogger.LogInfo("ExpandRecordByIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandRecordByIndex() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Collapses a specific record using title as value.
        /// </summary>
        /// <param name="RecordText">Full text of a specific record item</param>
        [Keyword("CollapseRecordByText", new String[] { "1|text|RecordText|Value" })]
        public void CollapseRecordByText(String RecordText)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var collapseButton = records.FirstOrDefault(record => DlkString.RemoveCarriageReturn(record.Text.ToLower().Trim()) == RecordText.ToLower().Trim()).FindElement(By.XPath(".//*[contains(@data-testid, 'collapse')]"));
                collapseButton.Click();
                DlkLogger.LogInfo("CollapseRecordByText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseRecordByText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Collapses a specific record using partial text as value.
        /// </summary>
        /// <param name="PartialText">Partial text of a specific record item</param>
        [Keyword("CollapseRecordByPartialText", new String[] { "1|text|PartialText|Value" })]
        public void CollapseRecordByPartialText(String PartialText)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var collapseButton = records.FirstOrDefault(record => DlkString.RemoveCarriageReturn(record.Text.ToLower().Trim()).Contains(PartialText.ToLower().Trim())).FindElement(By.XPath(".//*[contains(@data-testid, 'collapse')]"));
                collapseButton.Click();
                DlkLogger.LogInfo("CollapseRecordByPartialText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseRecordByPartialText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Collapses a specific record using index as value.
        /// </summary>
        /// <param name="RecordIndex">Index of a specific record item. Starts with 1</param>
        [Keyword("CollapseRecordByIndex", new String[] { "1|text|RecordIndex|1" })]
        public void CollapseRecordByIndex(String RecordIndex)
        {
            try
            {
                Initialize();
                int.TryParse(RecordIndex, out int index);
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var collapseButton = records[index - 1].FindElement(By.XPath(".//*[contains(@data-testid, 'collapse')]"));
                collapseButton.Click();
                DlkLogger.LogInfo("CollapseRecordByIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseRecordByIndex() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks on show/hide button by title.
        ///  Show/Hide Record button could not be mapped separately.
        /// </summary>
        /// <param name="RecordText">Full text of a specific record item</param>
        [Keyword("ClickShowHideRecordByText", new String[] { "1|text|RecordText|Value" })]
        public void ClickShowHideRecordByText(String RecordText)
        {
            try
            {
                Initialize();
                
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var showHideButton = records.FirstOrDefault(record => DlkString.RemoveCarriageReturn(record.Text.ToLower().Trim()) == RecordText.ToLower().Trim()).FindElement(By.XPath(".//*[@data-testid='dvTemplateHovered']//*[contains(@class, 'MuiGrid-item')][1]"));

                showHideButton.Click();
                DlkLogger.LogInfo("ClickShowHideRecordByText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickShowHideRecordByText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks on show/hide button by partial text.
        ///  Show/Hide Record button could not be mapped separately.
        /// </summary>
        /// <param name="PartialText">Partial text of a specific record item</param>
        [Keyword("ClickShowHideRecordByPartialText", new String[] { "1|text|PartialText|Value" })]
        public void ClickShowHideRecordByPartialText(String PartialText)
        {
            try
            {
                Initialize();

                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var showHideButton = records.FirstOrDefault(record => DlkString.RemoveCarriageReturn(record.Text.ToLower().Trim()).Contains(PartialText.ToLower().Trim())).FindElement(By.XPath(".//*[@data-testid='dvTemplateHovered']//*[contains(@class, 'MuiGrid-item')][1]"));

                showHideButton.Click();
                DlkLogger.LogInfo("ClickShowHideRecordByPartialText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickShowHideRecordByPartialText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks on show/hide button by index.
        ///  Show/Hide Record button could not be mapped separately.
        /// </summary>
        /// <param name="RecordIndex">Index of a specific record item. Starts with 1</param>
        [Keyword("ClickShowHideRecordByIndex", new String[] { "1|text|RecordIndex|1" })]
        public void ClickShowHideRecordByIndex(String RecordIndex)
        {
            try
            {
                Initialize();
                int.TryParse(RecordIndex, out int index);
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var showHideButton = records[index - 1].FindElement(By.XPath(".//*[@data-testid='dvTemplateHovered']//*[contains(@class, 'MuiGrid-item')][1]"));
                showHideButton.Click();
                DlkLogger.LogInfo("ClickShowHideRecordByIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickShowHideRecordByIndex() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks on the Add button using the record text.
        /// </summary>
        /// <param name="RecordText">Full text of a specific record item</param>
        [Keyword("ClickAddByRecordText", new String[] { "1|text|RecordText|Value" })]
        public void ClickAddByRecordText(String RecordText)
        {
            try
            {
                Initialize();

                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var showHideButton = records.FirstOrDefault(record => DlkString.RemoveCarriageReturn(record.Text.ToLower().Trim()) == RecordText.ToLower().Trim()).FindElement(By.XPath(".//*[@data-testid='dvTemplateHovered']//*[contains(@class, 'MuiGrid-item')][2]"));

                showHideButton.Click();
                DlkLogger.LogInfo("ClickAddByRecordText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickAddByRecordText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks on the Add button using partial record text.
        /// </summary>
        /// <param name="PartialRecordText">Partial text of a specific record item</param>
        [Keyword("ClickAddByPartialText", new String[] { "1|text|PartialRecordText|value" })]
        public void ClickAddByPartialText(String PartialRecordText)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var showHideButton = records.FirstOrDefault(record => DlkString.RemoveCarriageReturn(record.Text.ToLower().Trim()).Contains(PartialRecordText.ToLower().Trim())).FindElement(By.XPath(".//*[@data-testid='dvTemplateHovered']//*[contains(@class, 'MuiGrid-item')][2]"));

                showHideButton.Click();
                DlkLogger.LogInfo("ClickAddByPartialText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickAddByPartialText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Clicks on the Add button using record index.
        /// </summary>
        /// <param name="RecordIndex">Index of a specific record item. Starts with 1</param>
        [Keyword("ClickAddByRecordIndex", new String[] { "1|text|RecordIndex|1" })]
        public void ClickAddByRecordIndex(String RecordIndex)
        {
            try
            {
                Initialize();
                int.TryParse(RecordIndex, out int index);
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var showHideButton = records[index - 1].FindElement(By.XPath(".//*[@data-testid='dvTemplateHovered']//*[contains(@class, 'MuiGrid-item')][2]"));
                showHideButton.Click();
                DlkLogger.LogInfo("ClickAddByRecordIndex() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickAddByRecordIndex() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Gets a record index by partial text. Ideal for record items with very long text.
        /// </summary>
        /// <param name="PartialText">Partial text of a specific record item</param>
        /// <param name="VariableName">Variable to save the record index in</param>
        [Keyword("GetRecordIndexByPartialText", new String[] { "1|text|PartialText|Value", "1|text|VariableName|Value" })]
        public void GetRecordIndexByPartialText(String PartialText, String VariableName)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var record = records.FirstOrDefault(elem => DlkString.RemoveCarriageReturn(elem.Text.ToLower().Trim()).Contains(PartialText.ToLower().Trim()));
                if (record == null) throw new Exception("Cannot find record that contains '" + PartialText + "'");

                var index = records.IndexOf(record) + 1;
                DlkVariable.SetVariable(VariableName, index.ToString());
                DlkLogger.LogInfo("GetRecordIndexByPartialText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRecordIndexByPartialText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Gets a record index by text. 
        /// </summary>
        /// <param name="RecordText">Full text of a specific record item</param>
        /// <param name="VariableName">Variable to save the record index in</param>
        [Keyword("GetRecordIndexByText", new String[] { "1|text|ExpectedCount|TRUE", "1|text|VariableName|TRUE" })]
        public void GetRecordIndexByText(String RecordText, String VariableName)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var record = records.FirstOrDefault(elem => DlkString.RemoveCarriageReturn(elem.Text.ToLower().Trim()) == RecordText.ToLower().Trim());
                if (record == null) throw new Exception("Cannot find record that contains '" + RecordText + "'");

                var index = records.IndexOf(record) + 1;
                DlkVariable.SetVariable(VariableName, index.ToString());
                DlkLogger.LogInfo("GetRecordIndexByText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetRecordIndexByText() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies if record exists using text.
        /// </summary>
        /// <param name="RecordText">Full text of a specific record item</param>
        [Keyword("VerifyRecord", new String[] { "1|text|RecordText|Value" })]
        public void VerifyRecord(String RecordText)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var record = records.First(elem => DlkString.RemoveCarriageReturn(elem.Text.ToLower().Trim()) == RecordText.ToLower().Trim());
                
                DlkLogger.LogInfo("VerifyRecord() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRecord() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies if record exists using partial text.
        /// </summary>
        /// <param name="PartialText">Partial text of a specific record item</param>
        [Keyword("VerifyRecordByPartialText", new String[] { "1|text|PartialText|Value" })]
        public void VerifyRecordByPartialText(String PartialText)
        {
            try
            {
                Initialize();
                var records = GetRecordItems(mElement);
                if (records == null || records.Count <= 0) throw new Exception("Cannot find records on the screen.");

                var record = records.First(elem => DlkString.RemoveCarriageReturn(elem.Text.ToLower().Trim()).Contains(PartialText.ToLower().Trim()));
                DlkLogger.LogInfo("VerifyRecordByPartialText() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRecordByPartialText() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private methods
        private void Initialize()
        {
            FindElement();
            ScrollIntoViewUsingJavaScript();
        }

        //Added element parameter. Might be reused to find subitems/subrecords under existing records in the future.
        private List<IWebElement> GetRecordItems(IWebElement element)
        {
            return element.FindElements(By.XPath(".//*[@data-testid='templateItem']")).ToList();
        }
        #endregion
    }
}
