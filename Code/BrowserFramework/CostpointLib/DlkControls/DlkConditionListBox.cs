using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CostpointLib.DlkControls
{
    [ControlType("ConditionListBox")]
    class DlkConditionListBox : DlkBaseControl
    {
        private IList<IWebElement> mlstRows;
        private String mstrRowXPATH = "./div[@class='smplLstBxItm']";

        public DlkConditionListBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkConditionListBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
            GetRows();
        }

        #region Keywords

        [Keyword("Select", new String[] { "1|text|TextToSelect|Project - Sort Ascending" })]
        public void Select(String TextToSelect)
        {
            Initialize();

            try
            {
                foreach (IWebElement row in mlstRows)
                {
                    string strRowValue = row.Text.Trim();

                    if (strRowValue == TextToSelect)
                    {
                        row.Click();
                        DlkLogger.LogInfo("Select() successfully executed.");
                        return;
                    }
                }
                throw new Exception("Select() failed. Condition '" + TextToSelect + "' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed. " + e.Message, e);
            }
        }

        [Keyword("SelectByRowIndex", new String[] { "1|text|Index|1" })]
        public void SelectByRowIndex(String Index)
        {
            int idx = 0;
            Initialize();

            try
            {
                foreach (IWebElement row in mlstRows)
                {
                    string strRowIndex = row.GetAttribute("name").Trim();
                    idx = Convert.ToInt32(Index);
                    idx = idx - 1;
                    if (strRowIndex == idx.ToString())
                    {
                        row.Click();
                        DlkLogger.LogInfo("SelectByRowIndex() successfully executed.");
                        return;
                    }
                }
                throw new Exception("SelectByRowIndex() failed. Row '" + Index + "' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByRowIndex() failed. " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|ExpectedValue|TRUE" })]
        public void VerifyExists(String StrExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(StrExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValueOfSelectedCondition", new String[] { "1|text|ExpectedValue|Account - Sort Ascending" })]
        public void VerifyValueOfSelectedCondition(String StrExpectedValue)
        {
            string StrActualValue = string.Empty;
            Initialize();

            try
            {
                foreach (IWebElement row in mlstRows)
                {
                    string strSelected = row.GetAttribute("foc");
                    if (strSelected == "1")
                    {
                        StrActualValue = row.Text.Trim();
                        break;
                    }                    
                }
                DlkAssert.AssertEqual("VerifyValueOfSelectedCondition()", StrExpectedValue, StrActualValue);
                DlkLogger.LogInfo("VerifyValueOfSelectedCondition() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValueOfSelectedCondition() failed : " + e.Message, e);
            }
        }


        #endregion

        #region Private Methods
        /// <summary>
        /// Retrieves all the rows in the Sort conditions list
        /// </summary>
        private void GetRows()
        {
            mlstRows = mElement.FindElements(By.XPath(mstrRowXPATH));
        }

        #endregion
    }
}
