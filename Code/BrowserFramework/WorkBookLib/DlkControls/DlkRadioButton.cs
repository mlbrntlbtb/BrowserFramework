using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using CommonLib.DlkUtility;
using System.Linq;
using WorkBookLib.DlkSystem;
using System.Threading;
using System.Text.RegularExpressions;
using System.ComponentModel;
using Newtonsoft.Json;

namespace WorkBookLib.DlkControls
{
    [ControlType("RadioButton")]
    public class DlkRadioButton : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkRadioButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkRadioButton(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkRadioButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE VARIABLES
        private static string radioItems1_XPath = ".//div[contains(@class,'iradio')]//ancestor::div[contains(@class,'GenericFormRow')]";
        private static string radioItems2_XPath = ".//div[contains(@class,'iradio')]//ancestor::div[contains(@class,'InputDiv')]";
        private static string radioItems3_XPath = ".//div[contains(@class,'iradio')]//ancestor::label[contains(@class,'GenericToolbarRadioButton')]";
        private static string radioItems4_XPath = ".//label[contains(@class,'GenericFlexRowLabel')]//following-sibling::div[contains(@class,'iradio')]";
        private static string radioItems5_XPath = ".//div[contains(@class,'iradio')]//ancestor::div[contains(@class,'GenericFlexRow')]/label";
        private static string radioItems6_XPath = ".//div[contains(@class,'iradio')]//ancestor::div[contains(@class,'SettingItem')]//label";
        private static string radioItems7_XPath = ".//label[contains(@class,'checkboxradio')]";
        private static string radioItems8_XPath = ".//div[contains(@class,'iradio')]//following-sibling::span";
        private static string radioItems9_XPath = ".//div[contains(@class,'iradio')]//following-sibling::label";
        private static string radioButton_XPath = ".//div[contains(@class,'iradio')]";
        private static int radioButtonCase = 0;
        private IList<IWebElement> mRadioItems;
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            DlkWorkBookFunctionHandler.WaitScreenGetsReady();
            FindElement();
            FindRadioItems();
        }

        private void FindRadioItems()
        {
            mRadioItems = mElement.FindWebElementsCoalesce(false, By.XPath(radioItems9_XPath), By.XPath(radioItems1_XPath), By.XPath(radioItems2_XPath), By.XPath(radioItems3_XPath), 
                By.XPath(radioItems4_XPath), By.XPath(radioItems5_XPath), By.XPath(radioItems6_XPath), By.XPath(radioItems7_XPath),By.XPath(radioItems8_XPath))
                .Where(x => x.Displayed).ToList();
            
            if (mRadioItems == null)
            {
                throw new Exception("Radio items not found.");
            }
           
            radioButtonCase = mElement.FindElements(By.XPath(radioItems4_XPath)).Count > 0 ||
                mElement.FindElements(By.XPath(radioItems7_XPath)).Count > 0 ||
                mElement.FindElements(By.XPath(radioItems9_XPath)).Count > 0 ? radioButtonCase = 1 :
                mElement.FindElements(By.XPath(radioItems8_XPath)).Count > 0 ? radioButtonCase = 2 :
                radioButtonCase = 0;
        }

        private string GetRadioItemsToString()
        {
            string radioItems = "";
            foreach (IWebElement radioItem in mRadioItems)
            {
                string currentRadioItem = null;

                if (mElement.FindElements(By.XPath(radioItems4_XPath)).Count > 0)
                {
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                    currentRadioItem = DlkString.RemoveCarriageReturn(jse.ExecuteScript("return arguments[0].nextSibling.nodeValue", radioItem)
                        .ToString().Trim().ToLower());
                }
                else
                {
                    currentRadioItem = DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "") == "" ?
                    DlkString.ReplaceCarriageReturn(radioItem.GetAttribute("title").Trim().ToLower(), "") :
                    DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "");
                }

                radioItems += currentRadioItem + "~";
            }
            return radioItems.Trim('~');
        }
        #endregion

        #region KEYWORDS

        [Keyword("VerifyRadioItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRadioItems(String ExpectedValue)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(ExpectedValue.Trim()))
                    throw new Exception("ExpectedValue cannot be empty.");

                DlkAssert.AssertEqual("VerifyRadioItems(): ", ExpectedValue.Trim(), GetRadioItemsToString());
                DlkLogger.LogInfo("VerifyRadioItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRadioItems() failed : " + e.Message, e);
            }
        }

        [Keyword("GetRadioItems", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetRadioItems(String VariableName)
        {
            try
            {
                Initialize();
                if (String.IsNullOrEmpty(VariableName))
                    throw new Exception("VariableName cannot be empty.");

                String ActualValue = GetRadioItemsToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetRadioItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetRadioItems() failed : " + e.Message, e);
            }
        }

        [Keyword("Select", new String[] { "1|text|Expected Value|TRUE" })]
        public void Select(String RadioItem)
        {
            try
            {
                Initialize();
                bool bFound = false;

                foreach (IWebElement radioItem in mRadioItems)
                {
                    string currentRadioItem = null;

                    if (mElement.FindElements(By.XPath(radioItems4_XPath)).Count > 0)
                    {
                        IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        currentRadioItem = DlkString.RemoveCarriageReturn(jse.ExecuteScript("return arguments[0].nextSibling.nodeValue", radioItem)
                            .ToString().Trim().ToLower());
                    }
                    else
                    {
                        currentRadioItem = DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(radioItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "");
                    }

                    if (currentRadioItem == RadioItem.ToLower())
                    {
                        IWebElement radioButton = null;

                        switch (radioButtonCase)
                        {
                            case 1:
                                radioButton = radioItem;
                                break;

                            case 2:
                                string radioButtonXpath = radioItems8_XPath + "[text()='" + RadioItem.ToUpper() + "']/preceding-sibling::div";
                                radioButton = mElement.FindElement(By.XPath(radioButtonXpath));
                                break;

                            default:
                                radioButton = radioItem.FindElement(By.XPath(radioButton_XPath));
                                break;
                        }

                        radioButton.Click();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + RadioItem.ToLower() + "] not found. Actual list: [" + GetRadioItemsToString() + "]");

                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectContains(String PartialRadioItem)
        {
            try
            {
                Initialize();
                bool bFound = false;

                foreach (IWebElement radioItem in mRadioItems)
                {
                    string currentRadioItem = null;

                    if (mElement.FindElements(By.XPath(radioItems4_XPath)).Count > 0)
                    {
                        IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        currentRadioItem = DlkString.RemoveCarriageReturn(jse.ExecuteScript("return arguments[0].nextSibling.nodeValue", radioItem)
                            .ToString().Trim().ToLower());
                    }
                    else
                    {
                        currentRadioItem = DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(radioItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "");
                    }

                    if (currentRadioItem.Contains(PartialRadioItem.ToLower()))
                    {
                        IWebElement radioButton = null;

                        switch (radioButtonCase)
                        {
                            case 1:
                                radioButton = radioItem;
                                break;

                            case 2:
                                string radioButtonXpath = radioItems8_XPath + "[text()='" + PartialRadioItem.ToUpper() + "']/preceding-sibling::div";
                                radioButton = mElement.FindElement(By.XPath(radioButtonXpath));
                                break;

                            default:
                                radioButton = radioItem.FindElement(By.XPath(radioButton_XPath));
                                break;
                        }

                        radioButton.Click();
                        bFound = true;
                        break;
                    }
                }
                
                if (!bFound)
                    throw new Exception("[" + PartialRadioItem.ToLower() + "] not found. Actual list: [" + GetRadioItemsToString() + "]");

                DlkLogger.LogInfo("SelectContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String RadioItem, String ExpectedValue)
        {
            try
            {
                Initialize();
                bool bFound = false;
                bool expectedValue = Convert.ToBoolean(ExpectedValue);
                bool actualValue = false;

                foreach (IWebElement radioItem in mRadioItems)
                {
                    string currentRadioItem = null;

                    if (mElement.FindElements(By.XPath(radioItems4_XPath)).Count > 0)
                    {
                        IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        currentRadioItem = DlkString.RemoveCarriageReturn(jse.ExecuteScript("return arguments[0].nextSibling.nodeValue", radioItem)
                            .ToString().Trim().ToLower());
                    }
                    else
                    {
                        currentRadioItem = DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(radioItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "");
                    }

                    if (currentRadioItem == RadioItem.ToLower())
                    {
                        IWebElement radioButton = null;

                        switch (radioButtonCase)
                        {
                            case 1:
                                radioButton = radioItem;
                                break;

                            case 2:
                                string radioButtonXpath = radioItems8_XPath + "[text()='" + RadioItem.ToUpper() + "']/preceding-sibling::div";
                                radioButton = mElement.FindElement(By.XPath(radioButtonXpath));
                                break;

                            default:
                                radioButton = radioItem.FindElement(By.XPath(radioButton_XPath));
                                break;
                        }

                        actualValue = radioButton.GetAttribute("class").Contains("checked");
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + RadioItem + "] not found. Actual list: [" + GetRadioItemsToString() + "]");

                DlkAssert.AssertEqual("VerifyValue(): ", expectedValue, actualValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetValue(String RadioItem, String VariableName)
        {
            try
            {
                Initialize();
                bool bFound = false;
                bool actualValue = false;

                foreach (IWebElement radioItem in mRadioItems)
                {
                    string currentRadioItem = null;

                    if (mElement.FindElements(By.XPath(radioItems4_XPath)).Count > 0)
                    {
                        IJavaScriptExecutor jse = (IJavaScriptExecutor)DlkEnvironment.AutoDriver;
                        currentRadioItem = DlkString.RemoveCarriageReturn(jse.ExecuteScript("return arguments[0].nextSibling.nodeValue", radioItem)
                            .ToString().Trim().ToLower());
                    }
                    else
                    {
                        currentRadioItem = DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "") == "" ?
                        DlkString.ReplaceCarriageReturn(radioItem.GetAttribute("title").Trim().ToLower(), "") :
                        DlkString.ReplaceCarriageReturn(radioItem.Text.Trim().ToLower(), "");
                    }

                    if (currentRadioItem == RadioItem.ToLower())
                    {
                        IWebElement radioButton = null;

                        switch (radioButtonCase)
                        {
                            case 1:
                                radioButton = radioItem;
                                break;

                            case 2:
                                string radioButtonXpath = radioItems8_XPath + "[text()='" + RadioItem.ToUpper() + "']/preceding-sibling::div";
                                radioButton = mElement.FindElement(By.XPath(radioButtonXpath));
                                break;

                            default:
                                radioButton = radioItem.FindElement(By.XPath(radioButton_XPath));
                                break;
                        }

                        actualValue = radioButton.GetAttribute("class").Contains("checked");
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    throw new Exception("[" + RadioItem + "] not found. Actual list: [" + GetRadioItemsToString() + "]");

                DlkVariable.SetVariable(VariableName, actualValue.ToString());
                DlkLogger.LogInfo("[" + actualValue.ToString() + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetValue() failed : " + e.Message, e);
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

        #endregion
    }
}
