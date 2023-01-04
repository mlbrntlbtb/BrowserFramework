using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMobileLib.DlkControls
{
    [ControlType("WebGrid")]
    public class DlkWebGrid : DlkBaseControl
    {
        public DlkWebGrid(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkWebGrid(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkWebGrid(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        string xpathId = "//span[@id='savedQueries']/span[@name='{0}']";
        string xpathName = "//span[@id='savedQueries']/span[@class='mCBTitle'][text()='{0}']";
        string xpathGetValue = "//span[@id='savedQueries']/span[@name='{0}']/following-sibling::span[@class='mCBTitle' and @foc=1]";

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("SelectSavedQueryItem", new String[] { "1|text|QueryId|Test1" })]
        public void SelectSavedQueryItem(String QueryId)
        {
            try
            {
                Initialize();

                string xpath = string.Format(xpathId, QueryId);
                DlkBaseControl btnControl = new DlkBaseControl("QueryId", mElement.FindElement(By.XPath(xpath)));
                btnControl.Click();

                DlkLogger.LogInfo("SelectSavedQueryItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectSavedQueryItem() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCellValue", new String[] { "1|text|ColumnHeader|Id or Name",
                                                   "2|text|ExpectedValue|Qry1"})]
        public void VerifyCellValue(String ColumnHeader, String ExpectedValue)
        {
            try
            {
                Initialize();

                string xpath = "";
                string actualValue = "";

                if (ColumnHeader.ToLower() == "id")
                {
                    xpath = string.Format(xpathId, ExpectedValue);
                }
                else if (ColumnHeader.ToLower() == "name")
                {
                    xpath = string.Format(xpathName, ExpectedValue);
                }

                DlkBaseControl control = new DlkBaseControl("ExpectedValue", mElement.FindElement(By.XPath(xpath)));
                actualValue = control.GetValue();
                DlkAssert.AssertEqual("VerifyCellValue()", ExpectedValue, actualValue);
                DlkLogger.LogInfo("VerifyCellValue() passed");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to locate element"))
                {
                    throw new Exception(string.Format("VerifyCellValue() failed : {0} cannot be found on column {1}", ExpectedValue, ColumnHeader));
                }
                else
                {
                    throw new Exception("VerifyCellValue() failed : " + e.Message, e);
                }
            }
        }

        [Keyword("GetCellValue", new String[] { "1|text|QueryId|TEST",
                                                   "2|text|ColumnHeader|Name",
                                                   "3|text|VariableName|myVar"})]
        public void GetCellValue(String QueryId, String ColumnHeader, String VariableName)
        {
            DlkBaseControl control = null;
            DlkBaseControl controlValue = null;

            try
            {
                Initialize();

                string xpath = "";

                switch(ColumnHeader.ToLower())
                {
                    case "id":
                        xpath = string.Format(xpathId, QueryId);
                        control = new DlkBaseControl("VariableName", mElement.FindElement(By.XPath(xpath)));
                        DlkVariable.SetVariable(VariableName, control.GetValue());
                        break;
                    case "name":
                        xpath = string.Format(xpathId, QueryId);
                        control = new DlkBaseControl("VariableName", mElement.FindElement(By.XPath(xpath)));
                        control.Click();

                        xpath = string.Format(xpathGetValue, QueryId);
                        controlValue = new DlkBaseControl("VariableName", mElement.FindElement(By.XPath(xpath)));
                        DlkVariable.SetVariable(VariableName, controlValue.GetValue());
                        break;
                }

                DlkLogger.LogInfo("GetCellValue() passed");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to locate element"))
                {
                    throw new Exception(string.Format("GetCellValue() failed : Query Id {0} does not exist on the list", QueryId));
                }
                else
                {
                    throw new Exception("GetCellValue() failed : " + e.Message, e);
                }
            }
        }

        [Keyword("VerifyWebGridRowExistWithColumnValue", new String[] { "1|text|ColumnHeader|Name",
                                                   "2|text|Value|myQry",
                                                   "3|text|ExpectedValue|True or False"})]
        public void VerifyWebGridRowExistWithColumnValue(String ColumnHeader, String Value, String ExpectedValue)
        {
            bool isExist = false;

            try
            {
                Initialize();

                string xpath = "";
                string actualValue = "";

                if (ColumnHeader.ToLower() == "id")
                {
                    xpath = string.Format(xpathId, Value);
                }
                else if (ColumnHeader.ToLower() == "name")
                {
                    xpath = string.Format(xpathName, Value);
                }

                DlkBaseControl control = new DlkBaseControl("ExpectedValue", mElement.FindElement(By.XPath(xpath)));
                actualValue = control.GetValue();
                if(actualValue == Value)
                {
                    isExist = true;
                }

                if(isExist == Convert.ToBoolean(ExpectedValue))
                {
                    DlkLogger.LogInfo("Successfully executed VerifyWebGridRowExistWithColumnValue() : Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(isExist) + "'");
                }
                else
                {
                    throw new Exception("VerifyWebGridRowExistWithColumnValue() failed : Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(isExist) + "'");
                }

                DlkLogger.LogInfo("VerifyWebGridRowExistWithColumnValue() passed");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unable to locate element"))
                {
                    if (isExist == Convert.ToBoolean(ExpectedValue))
                    {
                        DlkLogger.LogInfo("Successfully executed VerifyWebGridRowExistWithColumnValue() : Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(isExist) + "'");
                        DlkLogger.LogInfo("VerifyWebGridRowExistWithColumnValue() passed");
                    }
                    else
                    {
                        throw new Exception("VerifyWebGridRowExistWithColumnValue() failed : Expected Value = '" + ExpectedValue + "' , Actual Value = '" + Convert.ToString(isExist) + "'");
                    }
                }
                else
                {
                    throw new Exception("VerifyWebGridRowExistWithColumnValue() failed : " + e.Message, e);
                }
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }
    }
}
