using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using SFTLib.DlkControls;
using SFTLib.DlkControls.Contract;
using SFTLib.DlkControls.Concrete.ComboBox;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;

namespace SFTLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        IComboBox comboBox;
        const int COUNTER = 0;
        const int SEARCH_MAX = 40;
        #region Constructors
        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue)
        {
        }

        public DlkComboBox(String ControlName, String SearchType, String SearchValue, Boolean VerifyAfterSelect)
            : base(ControlName, SearchType, SearchValue)
        {
        }

        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
        }

        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
        }
        #endregion
        public void Initialize()
        {
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            FindElement();
            comboBox = ComboBoxTypeFactory(mElement.GetAttribute("class"), mElement.GetAttribute("id"));
            if (DlkEnvironment.mBrowser.ToLower() == "ie") ScrollIntoView();
            else this.ScrollIntoViewUsingJavaScript();
        }

        private IComboBox ComboBoxTypeFactory(string className, string id)
        {
            // Including dictionaryCtrl id specific for SFT's login since it does not contain any class
            if (id == "dictionaryCtrl")
                return new DropDownComboBox(mElement, true);
            else if (className.Contains("dropDown", "comboBox") || mElement.TagName.ToLower() == "select")
                return new DropDownComboBox(mElement);
            else
                return new DefaultComboBox(mElement);
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                if (DlkEnvironment.mBrowser == "IE")
                    new DlkBaseControl("Button", mElement).ClickUsingJavaScript(false);
                else
                    mElement.Click();

                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("AssignValueToVariable")]
        public new void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                mElement = mElement.FindElement(By.XPath(".//input"));
                if (mElement == null)
                {
                    throw new Exception("ComboBox not found");
                }

                base.AssignValueToVariable(VariableName);
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (Convert.ToBoolean(TrueOrFalse) == true)
                    Initialize();

                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("Select")]
        public void Select(string Item)
        {
            try
            {
                Initialize();
                comboBox.Select(Item);
                Thread.Sleep(1000);
                DlkLogger.LogInfo("Select() successfully executed.");
                DlkSFTCommon.WaitForSpinner();
            }
            catch (StaleElementReferenceException)
            {
                DlkLogger.LogInfo("Stale Element. Retrying to select.");
                if (COUNTER <= SEARCH_MAX)
                    Select(Item);
                return;
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e); 
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("VerifyList", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                comboBox.VerifyList(Items);
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("VerifyText")]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                string actual = "";
                if (String.Equals(mElement.TagName, "select")) {
                    actual = mElement.FindElement(By.CssSelector("option:checked")).Text;
                } else {
                    actual = mElement.FindElement(By.XPath(".//input")).GetAttribute("value");
                }
                DlkAssert.AssertEqual("VerifyText()", ExpectedValue, actual);
                DlkLogger.LogInfo("VerifyText() passed");
            }
            catch(Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }
        [Keyword("SelectByIndex", new String[] { "1|text|IndexToSelect|Value" })]
        public void SelectByIndex(String IndexToSelect)
        {
            try
            {
                Initialize();
                int index = 0;
                if (!int.TryParse(IndexToSelect, out index))
                    throw new Exception("[" + IndexToSelect + "] is not a valid input for parameter Index.");

                Thread.Sleep(1000);
                comboBox.SelectByIndex(index);
                DlkSFTCommon.WaitForSpinner();
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String strExpectedValue)
        {
            try
            {
                Initialize();
                if (bool.TryParse(strExpectedValue, out bool result))
                {
                    bool bIsReadOnly = false;
                    if (mElement.GetAttribute("class").Contains("x-item-disabled"))
                    {
                        bIsReadOnly = true;
                    }
                    else if (mElement.FindElements(By.XPath("./ancestor::table[contains(@class, 'x-field ')]")).Any())
                    {
                        IWebElement parentField = mElement.FindElements(By.XPath("./ancestor::table[contains(@class, 'x-field ')]")).First();
                        bIsReadOnly = parentField.GetAttribute("class").Contains("x-item-disabled");
                    }
                    DlkAssert.AssertEqual("Verify read-only state for combobox: ", result, bIsReadOnly);
                }
                else
                {
                    throw new Exception("VerifyReadOnly() failed : value can only be True or False");
                }
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }
    }
}
