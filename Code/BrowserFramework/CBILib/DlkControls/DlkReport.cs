using CBILib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("Report")]
    public class DlkReport : DlkBaseControl
    {
        #region Constructors
        public DlkReport(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkReport(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkReport(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if Report exists. Requires strExpectedValue - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement noRowsIndicator = mElement.FindElements(By.XPath(".//span[contains(text(),'No rows')]")).FirstOrDefault();
                IWebElement emptyReportIndication = mElement.FindElements(By.XPath(".//td[@specname='pageBody']/table|.//td[@class='pb']/table")).FirstOrDefault();
                bool exists = true;
                if (noRowsIndicator != null || emptyReportIndication == null)
                    exists = false;
                DlkAssert.AssertEqual("VerifyExists() : " + mControlName, Convert.ToBoolean(TrueOrFalse), exists);
                
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyFieldValue", new string[] { "1|text|Caption|Project ID" })]
        public void VerifyFieldValue(string Caption, string ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement field = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]")).FirstOrDefault();

                if (field != null)
                {
                    IWebElement value = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]//ancestor::td[1]/following-sibling::td[1]")).FirstOrDefault();
                    if (value != null)
                    {
                        DlkAssert.AssertEqual("VerifyFieldValue()", ExpectedValue, value.Text);
                        DlkLogger.LogInfo("VerifyFieldValue(): Passed");
                    }
                    else
                    {
                        throw new Exception($"'{Caption}' value not found.");
                    }
                }
                else
                    throw new Exception($"Field '{Caption}' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFieldValue() failed: " + e.Message, e);
            }
        }

        [Keyword("GetFieldValue", new string[] { "1|text|Caption|Project ID"})]
        public void GetFieldValue(string Caption, string VariableName)
        {
            try
            {
                Initialize();
                IWebElement field = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]")).FirstOrDefault();

                if (field != null)
                {
                    IWebElement value = mElement.FindElements(By.XPath($".//span[contains(string(),'{Caption.Trim()}')]//ancestor::td[1]/following-sibling::td[1]")).FirstOrDefault();

                    if (value != null)
                    {

                        DlkVariable.SetVariable(VariableName, value.Text);
                        DlkLogger.LogInfo("GetTableCellValue(): Passed");
                    }
                    else
                    {
                        throw new Exception($"'{Caption}' value not found.");
                    }
                }
                else
                    throw new Exception($"Field '{Caption}' not found.");
            }
            catch (Exception e)
            {
                throw new Exception("GetFieldValue() failed: " + e.Message, e);
            }
        }
        #endregion
    }
}
