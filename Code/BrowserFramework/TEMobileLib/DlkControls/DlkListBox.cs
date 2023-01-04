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
    [ControlType("ListBox")]
    class DlkListBox : DlkBaseControl
    {
        public DlkListBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkListBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        
        public void Initialize()
        {
            FindElement();
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void SelectByIndex(String Index)
        {
            SelectItem("SelectByIndex()", Index, true, true);
        }

        [Keyword("Select", new String[] { "1|text|TextToSelect|Sample 1~Sample 2" })]
        public void Select(String TextToSelect)
        {
            SelectItem("Select()", TextToSelect, true, false);
        }

        [Keyword("UnselectByIndex", new String[] { "1|text|Index|1~3~5" })]
        public void UnselectByIndex(String Index)
        {
            SelectItem("UnselectByIndex()", Index, false, true);
        }

        [Keyword("Unselect", new String[] { "1|text|TextToUnselect|Sample 1~Sample 2" })]
        public void Unselect(String TextToUnselect)
        {
            SelectItem("Unselect()", TextToUnselect, false, false);
        }

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Item|Sample"})]
        public void VerifyAvailableInList(string Item)
        {
            try
            {
                Initialize();
                var itemXpath = string.Format("./descendant::div[text() = '{0}']", Item);
                var itemElement = mElement.FindElement(By.XPath(itemXpath));

                DlkLogger.LogInfo("VerifyAvailableInList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed. " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Items|Sample 1~Sample 2"})]
        public void VerifyList(string Items)
        {
            try
            {
                Initialize();
                var itemElements = mElement.FindElements(By.XPath("./descendant::div"));
                var elementText = string.Join("~", itemElements.Select(x => x.Text));
                DlkAssert.AssertEqual("VerifyList()", elementText, Items.Trim('~'));
                DlkLogger.LogInfo("VerifyList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed. " + e.Message, e);
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

        [Keyword("GetItemIndexWithValue", new String[] { "1|text|SearchedText|Sample Text", "2|text|VariableName|MyIndex" })]
        public void GetItemIndexWithValue(string Value, string VariableName)
        {
            try
            {
                Initialize();
                var itemXpath = string.Format("./descendant::div[text() = '{0}']", Value);
                var itemElement = mElement.FindElement(By.XPath(itemXpath));
                var index = mElement.FindElements(By.XPath("./descendant::div")).IndexOf(itemElement);

                DlkVariable.SetVariable(VariableName, (index + 1).ToString());
                DlkLogger.LogInfo("GetItemIndexWithValue() passed.");
            }
            catch(Exception e)
            {
                throw new Exception("GetItemIndexWithValue() failed. " + e.Message, e);
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


        #region private methods
        /// <summary>
        /// Select an item in the list
        /// </summary>
        /// <param name="Keyword">name of the keyword</param>
        /// <param name="InputString">input string parameter from the user</param>
        /// <param name="IsSelect">flag to select/unselect - true: select, false: unselect</param>
        /// <param name="IsByIndex">flag to select by index/by text - true: by index, false: by text</param>
        private void SelectItem(string Keyword, string InputString, bool IsSelect, bool IsByIndex)
        {
            String[] inputArray = InputString.Split('~');
            try
            {
                Initialize();
                for (int i = 0; i < inputArray.Length; i++)
                {
                    //select the correct item based on select type: by index/text
                    var itemXpath = string.Empty;
                    if (IsByIndex)
                        itemXpath = string.Format("./descendant::div[{0}]", inputArray[i]);
                    else
                        itemXpath = string.Format("./descendant::div[text() = '{0}']", inputArray[i]);

                    //get the element
                    var itemElement = mElement.FindElement(By.XPath(itemXpath));

                    //select item only if unselected, unselect only if selected
                    if ((IsSelect && itemElement.GetAttribute("sel") == "0")
                        || (!IsSelect && itemElement.GetAttribute("sel") == "1"))
                    {
                        itemElement.Click();
                    }

                    DlkLogger.LogInfo("Successfully executed " + Keyword);
                }
            }
            catch (Exception e)
            {
                throw new Exception(Keyword + " failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
