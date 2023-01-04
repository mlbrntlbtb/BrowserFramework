using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace AjeraLib.DlkControls
{
    [ControlType("List")]
    class DlkList:DlkBaseControl
    {

        #region DECLARATIONS

        private Boolean IsInit;
        private int retryLimit = 3;
        private string mElementCOntainer = "div";

        #endregion

        #region COSNTRUCTORS

        public DlkList(string ControlName, string SearchType, string SearchValue) 
            : base(ControlName, SearchType, SearchValue){}

        public DlkList(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues){}

        public DlkList(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}

        public DlkList(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkList(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector){}

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
                mElementCOntainer = mSearchValues[0].Contains("table") ? "tr" : "div";
            }
        }

        #endregion

        #region KEYWORDS
        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();

                bool bFound = false;
                string actualItems = string.Empty;
                int curRetry = 0;

                while (++curRetry <= retryLimit && !bFound)
                {
                    IList<IWebElement> actionItems;
                    if (mSearchValues[0].Contains("table"))
                    {
                        actionItems = mElement.FindElements(By.XPath(".//div[contains(@class,'axgrid-cell')]//label"));
                    }
                    else
                    {
                        actionItems = mElement.FindElements(By.XPath("//span[contains(@class,'column-label')]"));
                        if (actionItems.Count < 1)
                        {
                            actionItems = mElement.FindElements(By.XPath("//span[contains(@class,'group-listrow')]"));
                        }
                    }

                    foreach (IWebElement aListItem in actionItems)
                    {
                        if (aListItem.Displayed)
                        {
                            var dlkMenuItem = new DlkBaseControl("List Item", aListItem);
                            actualItems = actualItems + dlkMenuItem.GetAttributeValue("innerHTML") + ", ";
                            if (dlkMenuItem.GetValue().ToLower().TrimStart() == Item.ToLower())
                            {
                                dlkMenuItem.ScrollIntoViewUsingJavaScript();
                                dlkMenuItem.MouseOverUsingJavaScript();
                                dlkMenuItem.ClickUsingJavaScript();
                                Thread.Sleep(1000);

                                bFound = true;
                                break;
                            }
                        }
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Select() failed. Control : " + mControlName + " : '" + Item +
                                            "' not found in list. : Actual List = " + actualItems);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExists(String Item, String IsTrueOrFalse)
        {
            try
            {
                FindElement();
                Initialize();
                
                bool bFound = false;
                string actualItems = string.Empty;
                int curRetry = 0;

                while (++curRetry <= retryLimit && !bFound)
                {                    
                    string className = mElement.GetAttribute("class");
                    IList<IWebElement> actionItems;
                    if (!className.ToLower().Contains("security"))
                    {
                        if (mSearchValues[0].Contains("table"))
                        {
                            actionItems = mElement.FindElements(By.XPath(".//div[contains(@class,'axgrid-cell')]//label"));
                        }
                        else
                        {
                            if (mElement.GetAttribute("class").ToLower().Contains("allfiltersrow"))
                            {
                                mElementCOntainer = "..//span";
                            }
                            else
                            {
                                mElementCOntainer = ".//span[contains(@class,'listrow')]";
                            }
                            actionItems = mElement.FindElements(By.XPath(mElementCOntainer));
                        }
                        foreach (IWebElement aListItem in actionItems)
                        {
                            if (aListItem.Displayed)
                            {
                                var dlkMenuItem = new DlkBaseControl("List Item", aListItem);
                                actualItems = actualItems + dlkMenuItem.GetValue() + " ";
                                if (dlkMenuItem.GetValue().ToLower().Trim() == Item.ToLower().Trim())
                                {
                                    bFound = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] stringSeparators = new string[] { "<br>" };
                        string[] listItems = mElement.Text.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string listItem in listItems)
                        {
                            if (String.Compare(listItem, Item) == 0)
                            {
                                bFound = true;
                                break;
                            }
                        }
                    }
                }

                if (bFound == Convert.ToBoolean(IsTrueOrFalse))
                {
                    DlkLogger.LogInfo("VerifyExists() passed: Actual = " + Convert.ToString(bFound) + " : Expected = " + IsTrueOrFalse);
                }
                else
                {
                    DlkLogger.LogInfo("VerifyExists() failed: Actual = " + Convert.ToString(bFound) + " : Expected = " + IsTrueOrFalse);
                    throw new Exception("VerifyExists() failed: Actual = " + Convert.ToString(bFound) + " : Expected = " + IsTrueOrFalse);
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SelectByIndex(String ItemIndex)
        {
            try
            {
                int iTargetItemPos = 0;

                Initialize();

                bool bFound = false;
                int curRetry = 0;
                int idx = 1;

                while (++curRetry <= retryLimit && !bFound)
                {
                    IList<IWebElement> actionItems;
                    if (mSearchValues[0].Contains("table"))
                    {
                        actionItems = mElement.FindElements(By.XPath(".//div[contains(@class,'axgrid-cell')]"));
                    }
                    else
                    {
                        actionItems = mElement.FindElements(By.XPath("//span[contains(@class,'column-label')]"));
                        if (actionItems.Count < 1)
                        {
                            actionItems = mElement.FindElements(By.XPath("//span[contains(@class,'group-listrow')]"));
                        }
                        if (actionItems.Count < 1)
                        {
                            actionItems = mElement.FindElements(By.XPath("//span[contains(@class,'listrow')]"));
                        }
                    }

                    if (ItemIndex == "last()")
                        iTargetItemPos = actionItems.Count;
                    else
                        iTargetItemPos = Convert.ToInt32(ItemIndex);

                    foreach (IWebElement aListItem in actionItems)
                    {
                        var dlkMenuItem = new DlkBaseControl("List Item", aListItem);
                        if (aListItem.Displayed)
                        {
                            if (idx == iTargetItemPos)
                            {
                                dlkMenuItem.MouseOver();
                                dlkMenuItem.Click();
                                Thread.Sleep(1000);

                                bFound = true;
                                break;
                            }
                            idx++;
                        }
                    }
                }
                if (!bFound)
                {
                    throw new Exception("SelectByIndex() failed. Control : " + mControlName + " : '" + iTargetItemPos +
                                       "' is out of bounds of the list count.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}
