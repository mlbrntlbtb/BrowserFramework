using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace CostpointLib.DlkFunctions
{
    [Component("AutoComplete")]
    public static class DlkAutoComplete
    {
        static DlkBaseControl m_List;
        static Dictionary<String, DlkBaseControl> m_Items;

        public static void Initialize()
        {
            m_List = new DlkBaseControl("AutoCompleteList", "ID", "fldAutoCompleteDiv");
            m_List.FindElement();
            m_Items = new Dictionary<string, DlkBaseControl>();
            foreach (IWebElement elem in m_List.mElement.FindElements(By.XPath(".//div")).Where(e => e.GetCssValue("display") != "none"))
            {
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
                DlkLogger.LogInfo("Successfully executed Select()");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
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
                ItemValue = DlkString.ReplaceElipsesWithThreeDots(ItemValue);
                DlkAssert.AssertEqual("VerifyAvailableInList()", Convert.ToBoolean(ExpectedValue), m_Items.ContainsKey(ItemValue));
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
