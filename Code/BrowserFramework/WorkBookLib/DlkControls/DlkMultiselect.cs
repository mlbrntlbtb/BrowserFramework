using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using WorkBookLib.DlkSystem;
using System.Threading;

namespace WorkBookLib.DlkControls
{
    [ControlType("Multiselect")]
    public class DlkMultiselect : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkMultiselect(String ControlName, String SearchType, String SearchValue)
          : base(ControlName, SearchType, SearchValue) { }
        public DlkMultiselect(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMultiselect(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkMultiselect(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES

        private static string mSelectListXPath = "//div[@id='select2-drop']//ul";
        private static string mSelectItemXPath = ".//li//div[@role='option']";
        private static string mSelectInputXPath = "//input[contains(@class,'select2-input')][contains(@class,'select2-focused')]";
        private static string mSelectItemTagsXPath = ".//li[contains(@class,'ResourceTag')]";
        private static string mSelectItemTagsCloseXPath = ".//a[contains(@class,'close')]";
        private static string mDropMaskXPath = "//*[contains(@id,'select2-drop-mask')][not((contains(@style,'display: none')))]";

        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            CloseDropdown();
            ClearTags();
        }

        public void ClearTags()
        {
            //Check if field has existing item tags
            IList<IWebElement> mSelectItemTags = mElement.FindElements(By.XPath(mSelectItemTagsXPath))
                .Where(x => x.Displayed).ToList();

            //Clear existing item tags
            while (mSelectItemTags.Count > 0)
            {
                IWebElement mSelectItemClose = mSelectItemTags.LastOrDefault().FindElements(By.XPath(mSelectItemTagsCloseXPath)).Count > 0 ?
                        mSelectItemTags.LastOrDefault().FindElement(By.XPath(mSelectItemTagsCloseXPath)) : null;

                string ItemValue = mSelectItemClose.Text;
                if (mSelectItemClose == null)
                    throw new Exception("[" + ItemValue + "] item tag does not contain a close button.");

                mSelectItemClose.Click();
                DlkLogger.LogInfo("Clearing [" + ItemValue + "] item tag");
                Thread.Sleep(2000);

                mSelectItemTags = mElement.FindElements(By.XPath(mSelectItemTagsXPath))
                .Where(x => x.Displayed).ToList();
            }
            
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
        }

        public void CloseDropdown()
        {
            if (DlkEnvironment.AutoDriver.FindElements(By.XPath(mDropMaskXPath)).Count > 0)
            {
                IWebElement activeElement = DlkEnvironment.AutoDriver.SwitchTo().ActiveElement();
                activeElement.SendKeys(Keys.Escape);
            }
        }

        public IWebElement GetMultiSelectInput()
        {
            IWebElement mSelectInput = null;
            int retry = 0;
            int retryLimit = 5;

            while (retry <= retryLimit && mSelectInput == null)
            {
                mElement.Click();
                DlkLogger.LogInfo("Finding Multiselect input field ...");
                mSelectInput = mElement.FindElements(By.XPath(mSelectInputXPath)).Count > 0 ?
                mElement.FindElement(By.XPath(mSelectInputXPath)) : null;
            }

            if (mSelectInput == null)
                throw new Exception("Multiselect input field not found.");

            return mSelectInput;
        }

        #endregion

        #region KEYWORDS
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

        [Keyword("SelectItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItems(String Items)
        {
            try
            {
                Initialize();

                bool mFound = false;
                string[] value = Items.Split('~');

                for (int v = 0; v < value.Count(); v++)
                {
                    string ActualItems = "";
                    mElement.Click(); //Open dropdown
                    IWebElement mSelectList = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSelectListXPath));
                    IList<IWebElement> mSelectItems = mSelectList.FindElements(By.XPath(mSelectItemXPath))
                        .Where(x => x.Displayed).ToList();

                    foreach (IWebElement item in mSelectItems)
                    {
                        DlkBaseControl mSelectItem = new DlkBaseControl("Item", item);
                        ActualItems = ActualItems + mSelectItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(mSelectItem.GetValue(), "").ToLower() == value[v].ToLower())
                        {
                            mSelectItem.Click();
                            mFound = true;
                            DlkLogger.LogInfo("Selecting item [" + value[v].ToString() + "] ...");
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    if (!mFound)
                    {
                        CloseDropdown();
                        throw new Exception("Control : " + mControlName + " : '" + value[v].ToString() + "' not found in current list. : Current List = " + ActualItems.Trim());
                    }
                }

                CloseDropdown();
                DlkLogger.LogInfo("SelectItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItems() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemsContain", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItemsContain(String PartialItems)
        {
            try
            {
                Initialize();

                bool mFound = false;
                string[] value = PartialItems.Split('~');

                for (int v = 0; v < value.Count(); v++)
                {
                    string ActualItems = "";
                    mElement.Click(); //Open dropdown
                    IWebElement mSelectList = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSelectListXPath));
                    IList<IWebElement> mSelectItems = mSelectList.FindElements(By.XPath(mSelectItemXPath))
                        .Where(x => x.Displayed).ToList();

                    foreach (IWebElement item in mSelectItems)
                    {
                        DlkBaseControl mSelectItem = new DlkBaseControl("Item", item);
                        ActualItems = ActualItems + mSelectItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(mSelectItem.GetValue(), "").ToLower().Contains(value[v].ToLower()))
                        {
                            mSelectItem.Click();
                            mFound = true;
                            DlkLogger.LogInfo("Selecting partial item [" + value[v].ToString() + "] ...");
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    if (!mFound)
                    {
                        CloseDropdown();
                        throw new Exception("Control : " + mControlName + " : '" + value[v].ToString() + "' not found in current list. : Current List = " + ActualItems.Trim());
                    }
                }

                CloseDropdown();
                DlkLogger.LogInfo("SelectItemsContain() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemsContain() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectItemsByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectItemsByIndex(String Index)
        {
            try
            {
                Initialize();

                bool mFound = false;
                string[] value = Index.Split('~');
                mElement.Click(); //Open dropdown

                for (int v = 0; v < value.Count(); v++)
                {
                    string ActualItems = "";
                    IWebElement mSelectList = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSelectListXPath));
                    IList<IWebElement> mSelectItems = mSelectList.FindElements(By.XPath(mSelectItemXPath))
                        .Where(x => x.Displayed).ToList();

                    int targetIndex = Convert.ToInt32(value[v]) - 1;
                    for (int i = 0; i < mSelectItems.Count; i++)
                    {
                        DlkBaseControl mSelectItem = new DlkBaseControl("Item", mSelectItems[i]);
                        string actualItem = mSelectItem.GetValue().Trim();
                        ActualItems = ActualItems + actualItem + "~";
                        if(targetIndex == i)
                        {
                            mSelectItem.Click();
                            mFound = true;
                            DlkLogger.LogInfo("Selecting item [" + actualItem + "] with index [" + value[v].ToString() + "] ...");
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    if (!mFound)
                    {
                        CloseDropdown();
                        throw new Exception("Control : " + mControlName + " : '" + value[v].ToString() + "' index not found in current list. : Current List = " + ActualItems.Trim());
                    }
                }

                CloseDropdown();
                DlkLogger.LogInfo("SelectItemsByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectItemsByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndSelectItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetAndSelectItems(String Items)
        {
            try
            {
                Initialize();

                IWebElement mSelectInput = GetMultiSelectInput();
                bool mFound = false;
                string[] value = Items.Split('~');

                for (int v = 0; v < value.Count(); v++)
                {
                    mSelectInput.SendKeys(value[v].ToString());

                    string ActualItems = "";
                    IWebElement mSelectList = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSelectListXPath));
                    IList<IWebElement> mSelectItems = mSelectList.FindElements(By.XPath(mSelectItemXPath))
                        .Where(x => x.Displayed).ToList();

                    foreach (IWebElement item in mSelectItems)
                    {
                        DlkBaseControl mSelectItem = new DlkBaseControl("Item", item);
                        ActualItems = ActualItems + mSelectItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(mSelectItem.GetValue(), "").ToLower() == value[v].ToLower())
                        {
                            mSelectItem.Click();
                            mFound = true;
                            DlkLogger.LogInfo("Selecting item [" + value[v].ToString() + "] ...");
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    if (!mFound)
                    {
                        CloseDropdown();
                        throw new Exception("Control : " + mControlName + " : '" + value[v].ToString() + "' not found in current list. : Current List = " + ActualItems.Trim());
                    }
                }

                CloseDropdown();
                DlkLogger.LogInfo("SetAndSelectItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndSelectItems() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndSelectItemsContain", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetAndSelectItemsContain(String PartialItems)
        {
            try
            {
                Initialize();

                IWebElement mSelectInput = GetMultiSelectInput();
                bool mFound = false;
                string[] value = PartialItems.Split('~');

                for (int v = 0; v < value.Count(); v++)
                {
                    mSelectInput.SendKeys(value[v].ToString());

                    string ActualItems = "";
                    IWebElement mSelectList = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSelectListXPath));
                    IList<IWebElement> mSelectItems = mSelectList.FindElements(By.XPath(mSelectItemXPath))
                        .Where(x => x.Displayed).ToList();

                    foreach (IWebElement item in mSelectItems)
                    {
                        DlkBaseControl mSelectItem = new DlkBaseControl("Item", item);
                        ActualItems = ActualItems + mSelectItem.GetValue() + "~";
                        if (DlkString.ReplaceCarriageReturn(mSelectItem.GetValue(), "").ToLower().Contains(value[v].ToLower()))
                        {
                            mSelectItem.Click();
                            mFound = true;
                            DlkLogger.LogInfo("Selecting partial item [" + value[v].ToString() + "] ...");
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    if (!mFound)
                    {
                        CloseDropdown();
                        throw new Exception("Control : " + mControlName + " : '" + value[v].ToString() + "' not found in current list. : Current List = " + ActualItems.Trim());
                    }
                }

                CloseDropdown();
                DlkLogger.LogInfo("SetAndSelectItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndSelectItems() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndSelectItemsByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetAndSelectItemsByIndex(String Items, String Index)
        {
            try
            {
                Initialize();

                IWebElement mSelectInput = GetMultiSelectInput();
                bool mFound = false;
                string[] value = Items.Split('~');

                for (int v = 0; v < value.Count(); v++)
                {
                    mSelectInput.SendKeys(value[v].ToString());

                    string ActualItems = "";
                    IWebElement mSelectList = DlkEnvironment.AutoDriver.FindElement(By.XPath(mSelectListXPath));
                    IList<IWebElement> mSelectItems = mSelectList.FindElements(By.XPath(mSelectItemXPath))
                        .Where(x => x.Displayed).ToList();

                    int targetIndex = Convert.ToInt32(Index) - 1;
                    for (int i = 0; i < mSelectItems.Count; i++)
                    {
                        DlkBaseControl mSelectItem = new DlkBaseControl("Item", mSelectItems[i]);
                        string actualItem = mSelectItem.GetValue().Trim();
                        ActualItems = ActualItems + actualItem + "~";
                        if (targetIndex == i)
                        {
                            mSelectItem.Click();
                            mFound = true;
                            DlkLogger.LogInfo("Selecting item [" + actualItem + "] with index [" + Index + "] ...");
                            Thread.Sleep(2000);
                            break;
                        }
                    }

                    if (!mFound)
                    {
                        CloseDropdown();
                        throw new Exception("Control : " + mControlName + " : '" + value[v].ToString() + "' index not found in current list. : Current List = " + ActualItems.Trim());
                    }
                }

                CloseDropdown();
                DlkLogger.LogInfo("SetAndSelectItemsByIndex() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndSelectItemsByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly() : ", ExpectedValue.ToLower(), ActualValue.ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();

                DlkBaseControl currentValue = new DlkBaseControl("Value", mElement);
                string ActualValue = DlkString.ReplaceCarriageReturn(currentValue.GetValue(), "");
                DlkAssert.AssertEqual("VerifyValue(): ", ExpectedValue, ActualValue);

                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
