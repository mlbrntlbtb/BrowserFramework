using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace StormTouchCRMLib.DlkControls
{
    [ControlType("LinkList")]
    public class DlkLinkList : DlkBaseControl
    {
        public DlkLinkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLinkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLinkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
        }
        
        [Keyword("Select", new String[] { "1|text|Value|ListItem" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();

                IWebElement mSelected = null;
                bool bFound = false;
                for (int i = 0; i < iFindElementDefaultSearchMax; i++)
                {
                    if (mElement.FindElements(By.XPath(".//*[contains(@class,'InfoLink')][contains(., '" + Item + "')]")).Count > 0)
                    {
                        mSelected = mElement.FindElement(By.XPath(".//*[contains(@class,'InfoLink')][contains(., '" + Item + "')]"));
                        bFound = true;
                        break;
                    }
                    else
                    {
                        if (mElement.FindElements(By.XPath(".//a[contains(.,'" + Item + "')]")).Count > 0)
                        {
                            mSelected = mElement.FindElement(By.XPath(".//a[contains(.,'" + Item + "')]"));
                            bFound = true;
                            break;
                        }
                        else if (mElement.FindElements(By.XPath(".//span[contains(.,'" + Item + "')]")).Count > 0)
                        {
                            mSelected = mElement.FindElement(By.XPath(".//span[contains(.,'" + Item + "')]"));
                            bFound = true;
                            break;
                        }
                        else if (mElement.FindElements(By.XPath("..//div[contains(@class, 'item')][contains(.,'" + Item + "')]")).Count > 0)
                        {
                            mSelected = mElement.FindElement(By.XPath(".//div[contains(@class, 'item')][contains(.,'" + Item + "')]"));
                            bFound = true;
                            break;
                        }
                    }                    
                    Thread.Sleep(1000);
                }

                if (!bFound)
                {
                    throw new Exception("Select() failed : Item not found");
                }

                DlkBaseControl mSelectedItem = new DlkBaseControl("Selected", mSelected);             
                mSelectedItem.Click();
                DlkLogger.LogInfo("Select() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyAvailableInList", new String[] { "1|text|SearchedText|SampleValue",
                                                  "2|text|VariableName|MyRow"})]
        public void VerifyAvailableInList(String ListItem, String ExpectedValue)
        {
            try
            {
                Initialize();
                bool bFound = false;
                for (int i = 0; i < iFindElementDefaultSearchMax; i++)
                {
                    if (mElement.FindElements(By.XPath(".//*[contains(@class,'InfoLink')][contains(., '" + ListItem + "')]")).Count > 0)
                    {
                        bFound = true;
                        break;
                    }
                    else
                    {
                        if (mElement.FindElements(By.XPath("./*[contains(.,'" + ListItem + "')]")).Count > 0)
                        {
                            bFound = true;
                            break;
                        }
                    }
                    Thread.Sleep(1000);
                }
                DlkAssert.AssertEqual("VerifyAvailableInList", Convert.ToBoolean(ExpectedValue), bFound);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                string actual = string.Empty;
                string expected = string.Empty;

                foreach (string expItm in Items.Split('~'))
                {
                    expected += DlkString.ReplaceCarriageReturn(expItm, "") + "~";
                }
                expected = expected.Trim('~');

                foreach (IWebElement elm in mElement.FindElements(By.XPath("./*")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                }
                actual = actual.Trim('~');

                DlkAssert.AssertEqual("VerifyList()", expected, actual);
                DlkLogger.LogInfo("VerifyList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

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
    }
}
