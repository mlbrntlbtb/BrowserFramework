using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("MultiSelect")]
    public class DlkMultiSelect : DlkBaseControl
    {
        #region Declarations



        #endregion

        #region Constructors

        public DlkMultiSelect(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        public DlkMultiSelect(String ControlName, IWebElement existingWebElement)
            : base(ControlName, existingWebElement)
        {
            //Do Nothing.
        }

        #endregion

        #region Properties

        private bool IsFromIFrame
        {
            get
            {
                if (mSearchType.ToLower().Equals("iframe_xpath"))
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region Keywords

        [Keyword("Select")]
        public void Select(string Values)
        {
            if (Values.Equals(string.Empty))
            {
                DlkLogger.LogInfo("Skipping select since the data parameter is blank.");
            }
            else
            {
                try
                {
                    initialize();
                    string[] valueList = Values.Split('~');
                    IList<IWebElement> selectOptionList = mElement.FindElements(By.XPath(@".//option"));

                    for (int i = 0; i < valueList.Length; i++)
                    {
                        bool foundValue = false;
                        string textValue = valueList[i];
                        List<IWebElement> items = selectOptionList.Where(a => a.Text.Contains(textValue)).ToList();
                        foreach (IWebElement element in items)
                        {
                            DlkBaseControl control = new DlkBaseControl("Option", element);
                            string actualValue = control.GetValue().Replace("\r\n", string.Empty).Trim();
                            if (actualValue.Equals(textValue))
                            {
                                foundValue = true;
                                if (IsFromIFrame)
                                {
                                    element.Click();
                                }
                                else
                                {
                                    control.Click();
                                }
                                DlkLogger.LogInfo(textValue + " selected.");
                                break;
                            }
                        }

                        if (!foundValue)
                        {
                            throw new Exception(textValue + " not found.");
                        }
                    }

                    DlkLogger.LogInfo("Select( ) successfully executed.");
                }
                catch(Exception ex)
                {
                    throw new Exception("Select( ) execution failed : " + ex.Message, ex);
                }
            }
        }

        [Keyword("UnselectAll")]
        public void UnselectAll()
        {
            try
            {
                initialize();
                SelectElement selectedElement = new SelectElement(mElement);
                selectedElement.DeselectAll();

                DlkLogger.LogInfo("UnselectAll( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("UnselectAll( ) execution failed : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyItemSelected", new String[] { "1|text|Item|ItemToFind",
                                                         "2|text|ExpectedValue|TRUE"})]
        public void VerifyItemSelected(String Item, String TrueOrFalse)
        {
            try
            {
                initialize();
                string[] expectedResultsList = Item.Split('~');
                List<string> actualSelectedItems = getSelectedItems();
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                bool actualResult = false;

                foreach (string item in expectedResultsList)
                {
                    if (actualSelectedItems.Contains(item))
                    {
                        actualResult = true;
                    }
                    else
                    {
                        actualResult = false;
                    }
                    DlkAssert.AssertEqual("VerifyItemSelected() : " + item, expectedResult, actualResult);
                }

                DlkLogger.LogInfo("VerifyItemSelected() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemSelected() failed : " + e.Message, e);
            }
        }

        private List<string> getSelectedItems()
        {
            List<string> selectedItems = new List<string>();

            SelectElement selectedElement = new SelectElement(mElement);
            foreach (IWebElement element in selectedElement.AllSelectedOptions)
            {
                selectedItems.Add(element.Text);
            }

            return selectedItems;
        }

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string ItemCaption, string TrueOrFalse)
        {
            try
            {
                initialize();
                string expectedResult = ItemCaption;
                bool argument = Convert.ToBoolean(TrueOrFalse);
                bool foundOption = false;
                IList<IWebElement> selectOptionList = mElement.FindElements(By.XPath(@".//option"));

                for (int i = 0; i < selectOptionList.Count; i++)
                {
                    DlkBaseControl control = new DlkBaseControl("Option", selectOptionList[i]);
                    string actualResult = control.GetValue();
                    if (expectedResult.Equals(actualResult))
                    {
                        foundOption = true;
                        DlkLogger.LogInfo("Found option : " + actualResult);
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemExists", foundOption, argument ? true : false);
                DlkLogger.LogInfo("VerifyItemExists( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyItemExists() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("SelectByRow")]
        public void SelectByRow(string Rows)
        {
            try
            {
                initialize();
                string[] rowList = Rows.Split('~');
                IList<IWebElement> selectOptionList = mElement.FindElements(By.XPath(@".//option"));

                for (int i = 0; i < rowList.Length; i++)
                {
                    int row = Convert.ToInt32(rowList[i]) - 1;
                    DlkBaseControl control = new DlkBaseControl("Option", selectOptionList[row]);
                    control.Click();
                    DlkLogger.LogInfo(row + " selected.");
                }

                DlkLogger.LogInfo("SelectByRow( ) successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("SelectByRow() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyState")]
        public void VerifyState(String TrueOrFalse)
        {
            try
            {
                initialize();
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                string disabled = mElement.GetAttribute("disabled").Trim();
                if (disabled != null &&
                    disabled == "true")
                {
                    actualResult = false;
                }
                else
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("VerifyState : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyState() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyState() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                //Fail safe code for checking crashed site.
                DlkEnvironment.AutoDriver.FindElement(By.CssSelector("h1"));
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}
