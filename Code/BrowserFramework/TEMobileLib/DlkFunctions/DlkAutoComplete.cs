using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace TEMobileLib.DlkFunctions
{
    [Component("AutoComplete")]
    public static class DlkAutoComplete
    {
        static DlkBaseControl m_List;
        static Dictionary<String, DlkBaseControl> m_Items;

        private static String XPath_MOBILE_OK = "//form//*[@id='mbOk']";
        private static String XPath_MOBILE_CANCEL = "//*[@id='subtaskBtn']//*[@id='bCan']";

        public static void Initialize()
        {
            m_List = new DlkBaseControl("AutoCompleteList", "ID", "fldAutoCompleteDiv");
            m_List.FindElement();
            m_Items = new Dictionary<string, DlkBaseControl>();
            foreach (IWebElement elem in m_List.mElement.FindElements(By.XPath(".//div[contains(@class, 'fldAutoCItem') or contains(@class, 'fldAutoCEItem')]")))
            {
                if (!elem.Displayed) break;

                DlkBaseControl itemToAdd = new DlkBaseControl("Item", elem);
                m_Items.Add(itemToAdd.GetValue(), itemToAdd);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public static void Select(String sSelectedItem)
        {
            try
            {
                Initialize();
                sSelectedItem = DlkString.ReplaceElipsesWithThreeDots(sSelectedItem);
                if (!m_Items.Keys.Contains(sSelectedItem))
                {
                    throw new Exception("Item '" + sSelectedItem + "' not found.");
                }
                m_Items[sSelectedItem].Click();
                if(!sSelectedItem.ToLower().Equals("more values..."))
                {
                    var mobileOK = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_OK)).FirstOrDefault();
                    Thread.Sleep(500);
                    mobileOK.Click();
                }
                
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("Cancel", new String[] { "1|text|Value|TRUE" })]
        public static void Cancel()
        {
            try
            {
                var lkpCancel = DlkEnvironment.AutoDriver.FindElements(By.XPath(XPath_MOBILE_CANCEL)).FirstOrDefault();
                if (lkpCancel == null) throw new Exception("Cancel button of lookup not found.");
                Thread.Sleep(500);
                lkpCancel.Click();
                DlkLogger.LogInfo("Successfully executed Cancel()");
            }
            catch (Exception e)
            {
                throw new Exception("Cancel() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public static void VerifyExists(String strExpectedValue)
        {
            try
            {
                Initialize();
                m_List.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Item|Sample item",
                                                         "2|text|Expected Value|TRUE"})]
        public static void VerifyAvailableInList(String ItemValue, String ExpectedValue)
        {
            try
            {
                Initialize();
                bool acualResult;
                if (m_Items.Count == 0 && ItemValue == "no values found") acualResult = true;
                else acualResult = m_Items.ContainsKey(DlkString.ReplaceElipsesWithThreeDots(ItemValue));

                DlkAssert.AssertEqual("VerifyAvailableInList()", Convert.ToBoolean(ExpectedValue), acualResult);
                DlkLogger.LogInfo("VerifyAvailableInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception ("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public static void VerifyList(String ExpectedValues)
        {
            try
            {
                Initialize();
                ExpectedValues = DlkString.ReplaceElipsesWithThreeDots(ExpectedValues);
                string[] input = ExpectedValues.Split('~');

                DlkLogger.LogInfo("Expected num items = " + input.Length + " : Actual num items = "
                    + m_Items.Count);
                if (input.Length != m_Items.Count)
                {
                    throw new Exception ("Expected number of items does not match actual number of items.");
                }

                int idx = 0;
                foreach (KeyValuePair<string, DlkBaseControl> kvp in m_Items)
                {
                    DlkLogger.LogInfo("Expected Item " + idx + " = " + input[idx] + " : Actual Item  " + idx + " = " + kvp.Key);
                    if (kvp.Key != input[idx])
                    {
                        throw new Exception ("Item on index " + idx + " does not match.");
                    }
                    idx++;
                }
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception ("VerifyList() failed : " + e.Message, e);
            }
        }   
    }
}
