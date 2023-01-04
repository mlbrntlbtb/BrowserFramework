using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBCLib.DlkControls
{
    [ControlType("CheckBoxList")]
    public class DlkCheckBoxList : DlkBaseControl
    {
        #region Constructors
        public DlkCheckBoxList(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBoxList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBoxList(String ControlName, IWebElement ExistingWebElement)
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


        
        [Keyword("SetAllCheckboxValues", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetAllCheckboxValues(String TrueOrFalse)
        {
            try
            {
                Initialize();
                var checkBoxList = mElement.FindElements(By.XPath("./li/div//input")).ToList();
                if (checkBoxList.Count < 1) throw new Exception("No CheckBoxList found.");

                foreach(var checkbox in checkBoxList)
                {
                    new DlkCheckBox("checkbox", checkbox).SetValue(TrueOrFalse);
                }

                DlkLogger.LogInfo("SetAllCheckboxValues() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetAllCheckboxValues() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCheckboxValues", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetCheckboxValues(String ListOfNameOrIndex, String TrueOrFalse)
        {
            try
            {
                Initialize();

                var checkboxNamesOrIndexes = ListOfNameOrIndex.Trim().Split('~').ToList();

                foreach(var checkBoxNameOrIndex in checkboxNamesOrIndexes)
                {
                    var checkBox = GetByNameOrIndex(checkBoxNameOrIndex);
                    new DlkCheckBox("checkbox", checkBox).SetValue(TrueOrFalse);
                }

                DlkLogger.LogInfo("SetCheckboxValues() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetCheckboxValues() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods

        private IWebElement GetByNameOrIndex(String NameOrIndex)
        {
            IWebElement checkBox;
            int index;

            var checkBoxList = mElement.FindElements(By.XPath("./li/div//label")).ToList();
            if (checkBoxList.Count < 1) throw new Exception("No CheckBoxList found.");

            if (Int32.TryParse(NameOrIndex, out index))
            {
                if (index > checkBoxList.Count) throw new Exception($"Index [{index}] is greater than the actual checkbox count [{checkBoxList.Count}].");
                checkBox = checkBoxList.ElementAt(index - 1);
            }
            else
                checkBox = checkBoxList.Where(cb => cb.Text.Replace(System.Environment.NewLine, " ").Trim() == NameOrIndex.Trim()).FirstOrDefault();

            if (checkBox == null) throw new Exception($"No checkbox found at [{NameOrIndex}]");

            return checkBox;
        }
        #endregion
    }
}
