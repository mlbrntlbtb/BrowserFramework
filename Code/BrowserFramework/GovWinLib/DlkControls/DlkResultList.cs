using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using GovWinLib.DlkUtility;

namespace GovWinLib.DlkControls
{
    [ControlType("ResultList")]
    public class DlkResultList : DlkBaseControl
    {
        private String mstrNextPageLinkText = "[Next]";
        private String mstrResultItemsXpath = "//div[contains(@class,'resultDisplayRow')]";
        private List<DlkResultItem> mlstResultItems;
        private DlkResultItem mobjActiveResultItem;


        public DlkResultList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkResultList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkResultList(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


        public void Initialize()
        {
            FindElement();
            mobjActiveResultItem = null;
        }

        public void RefreshResultItems()
        {
            Initialize();
            mlstResultItems = new List<DlkResultItem>();
            IList<IWebElement> lstResultItemsElements = mElement.FindElements(By.XPath(mstrResultItemsXpath));
            foreach (IWebElement resultItemElement in lstResultItemsElements)
            {
                mlstResultItems.Add(new DlkResultItem("ResultItem", resultItemElement));
            }
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    VerifyExists(Convert.ToBoolean(expectedValue));
                }, new String[]{"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [Keyword("FindResultItemByIndex", new String[] { "1|text|Index|1" })]
        public void FindResultItemByIndex(String ResultItemIndex)
        {
            //Index parameter is 1 based
            int iIndex = Convert.ToInt32(ResultItemIndex) - 1;
            mobjActiveResultItem = null;
            RefreshResultItems();
            if (iIndex >= mlstResultItems.Count)
            {
                throw new Exception("FindResultItemByIndex() failed. Index is greater than the number of result items in the result list.");
            }
            else
            {
                mobjActiveResultItem = mlstResultItems[iIndex];
                DlkLogger.LogInfo("Successfully executed FindResultItemByIndex().");
            }
        }


        [Keyword("ClickResultItemToggleButton", new String[] { "1|text|Index|1" })]
        public void ClickResultItemToggleButton(String ResultItemIndex)
        {
            //Index parameter is 1 based
            int iIndex = Convert.ToInt32(ResultItemIndex) - 1;
            mobjActiveResultItem = null;
            RefreshResultItems();
            if (iIndex >= mlstResultItems.Count)
            {
                throw new Exception("ClickResultItemToggleButton() failed. Index is greater than the number of result items in the result list.");
            }
            else
            {
                
                this.PerformAction(() =>
                {
                    DlkLink toggleButton = new DlkLink("toggle", mlstResultItems[iIndex], "XPATH", ".//a[contains(.,'Expand/Collapse')]");

                    toggleButton.Click();
                    /*String strToggleClass = toggleButton.GetAttribute("class");
                    if (strToggleClass.Equals(mElement.GetAttribute("class")))
                        throw new Exception("Unable to click toggle button");*/
                }, new String[] { "retry" });
                
                DlkLogger.LogInfo("Successfully executed ClickResultItemToggleButton().");
            }
        }

        [Keyword("VerifyHighlightedKeyword", new String[] { "1|text|Index|1", "2|text|Keyword|Bureau" })]
        public void VerifyHighlightedKeyword(String ResultItemIndex, String Keyword)
        {
            //Index parameter is 1 based
            int iIndex = Convert.ToInt32(ResultItemIndex) - 1;
            mobjActiveResultItem = null;
            RefreshResultItems();
            if (iIndex >= mlstResultItems.Count)
            {
                throw new Exception("FindResultItemByIndex() failed. Index is greater than the number of result items in the result list.");
            }
            else
            {
                mobjActiveResultItem = mlstResultItems[iIndex];
                String highlightedKey = mobjActiveResultItem.mElement.FindElement(By.ClassName("CesResultHighlight")).Text;
                DlkAssert.AssertEqual("VerifyHighlightedKeyword()", Keyword.ToLower(), highlightedKey.ToLower());
            }
        }

        [Keyword("VerifyCaption", new String[] { "1|text|Index|1", "2|text|Caption|Mark as", "3|text|Expected|TRUE" })]
        public void VerifyCaption(String ResultItemIndex, String Caption, String TrueOrFalse)
        {
            int iIndex = Convert.ToInt32(ResultItemIndex) - 1;
            String caption = Caption;
            bool expectedResult = Convert.ToBoolean(TrueOrFalse);
            bool actualResult = expectedResult? false:true;

            RefreshResultItems();

            String entry = mlstResultItems[iIndex].GetValue();

            if (entry.Contains(caption))
            {
                actualResult = true;
            }

            //IList<IWebElement> contents = mlstResultItems[iIndex].FindElements(By.XPath(".//div[@class='clearfix'//ul[@class='programInfo'//li"));

            //foreach (IWebElement content in contents)
            //{
            //    if (content.Text.Contains(caption))
            //        actualResult = true;
            //}

            DlkAssert.AssertEqual("VerifyCaption()", expectedResult, actualResult);

        }

        [RetryKeyword("VerifyChartExists", new String[] { "1|text|Index|1", "2|text|ExpectedResult|TRUE" })]
        public void VerifyChartExists(String ResultItemIndex, String TrueOrFalse)
        {
            int iIndex = Convert.ToInt32(ResultItemIndex);
            bool expectedResult = Convert.ToBoolean(TrueOrFalse);
            bool actualResult = expectedResult ? false : true;

            RefreshResultItems();

            IWebElement contents = mlstResultItems[iIndex].GetParent().FindElement(By.XPath(".//div[contains(@id,'highcharts')]"));

            if (contents != null)
                actualResult = true;

            DlkAssert.AssertEqual("VerifyChartExists()", expectedResult, actualResult);
        }

        private void FindResultItemLoop(String sItemTitle)
        {
            Boolean bContinue = true;

            while (bContinue)
            {
                try
                {
                    mobjActiveResultItem = null;
                    while (bContinue)
                    {
                        RefreshResultItems();
                        foreach (DlkResultItem resultItem in mlstResultItems)
                        {
                            if (sItemTitle.ToLower() == resultItem.GetItemTitle().ToLower())
                            {
                                DlkLogger.LogInfo("Successfully executed FindResultItem(). '" + sItemTitle + "' found in list.");
                                mobjActiveResultItem = resultItem;
                                bContinue = false;
                                break;
                            }
                        }

                        if (bContinue)
                        {
                            IList<IWebElement> lstNextPage = mElement.FindElements(By.LinkText(mstrNextPageLinkText));
                            if (lstNextPage.Count == 0)
                            {
                                bContinue = false;
                                throw new Exception("FindResultItem() failed. Unable to find '" + sItemTitle + "' in list.");
                            }
                            else
                            {
                                DlkLink NextPage = new DlkLink("Next Page", lstNextPage.First());
                                NextPage.Click();
                            }
                        }
                    }
                }
                catch (StaleElementReferenceException s)
                {
                    DlkLogger.LogInfo(s.Message);
                    DlkLogger.LogInfo("Retrying FindResultItem...");
                    bContinue = true;
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("Element is no longer attached to the DOM"))
                    {
                        DlkLogger.LogInfo(e.Message);
                        DlkLogger.LogInfo("Element is no longer attached to the DOM. " + "Retrying FindResultItem...");
                        bContinue = true;
                    }
                    else
                    {
                        DlkLogger.LogInfo("Exception type: " + e.GetType());
                        throw e;
                    }
                }
            }
        }

        private String TrimValue(String sValue)
        {
            sValue = sValue.Replace("\n", " ");
            sValue = sValue.Replace("\r", " ");
            while (sValue.Contains("  "))
            {
                sValue = sValue.Replace("  ", " ");
            }
            return sValue.Trim();
        }

        [RetryKeyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String TextToVerify)
        {
            String textToVerify = TrimValue(TextToVerify);

            this.PerformAction(() =>
            {
                FindElement();
                String ActualResult = "";

                // Below style does not work on IE
                switch (GetAttributeValue("tagName").ToLower())
                {
                    case "h1":
                        ActualResult = TrimValue(GetValue());
                        IList<IWebElement> aElements = mElement.FindElements(By.CssSelector("a"));
                        foreach (IWebElement aElement in aElements)
                        {
                            DlkBaseControl linkControl = new DlkBaseControl("Link", aElement);
                            ActualResult = ActualResult.Replace(linkControl.GetValue(), "");
                        }
                        ActualResult = ActualResult.Trim();
                        break;
                    default:
                        ActualResult = mElement.Text.Trim();
                        break;
                }

                ActualResult = TrimValue(ActualResult);
                bool boolActual = ActualResult.Contains(textToVerify) ? true : false;

                DlkAssert.AssertEqual("VerifyTextContains()", true, boolActual);
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify)
        {
            String textToVerify = TrimValue(TextToVerify);

            this.PerformAction(() =>
            {
                FindElement();
                String ActualResult = "";

                // Below style does not work on IE
                switch (GetAttributeValue("tagName").ToLower())
                {
                    case "h1":
                        ActualResult = TrimValue(GetValue());
                        IList<IWebElement> aElements = mElement.FindElements(By.CssSelector("a"));
                        foreach (IWebElement aElement in aElements)
                        {
                            DlkBaseControl linkControl = new DlkBaseControl("Link", aElement);
                            ActualResult = ActualResult.Replace(linkControl.GetValue(), "");
                        }
                        ActualResult = ActualResult.Trim();
                        break;
                    default:
                        ActualResult = mElement.Text.Trim();
                        break;
                }

                ActualResult = TrimValue(ActualResult);

                DlkAssert.AssertEqual("VerifyText()", textToVerify, ActualResult);
            }, new String[]{"retry"});
        }

        [Keyword("FindResultItem", new String[] { "1|text|Result Item Title|Sample Title" })]
        public void FindResultItem(String ItemTitle)
        {
            Action<string> method = new Action<string>(FindResultItemLoop);
            DlkMethodCallHelper.CallWithTimeout(method, 60, ItemTitle);
        }

        [RetryKeyword("VerifyResultItemExists", new String[] { "1|text|Result Item Title|Sample Title",
                                                          "2|text|Expected Result|TRUE"})]
        public void VerifyResultItemExists(String ResultItemTitle, String TrueOrFalse)
        {
            String itemTitle = ResultItemTitle;
            String expectedResult = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Boolean bFound = false;
                    Boolean bContinue = true;
                    mobjActiveResultItem = null;

                    RefreshResultItems();
                    IList<IWebElement> lstFirstPage = mElement.FindElements(By.LinkText("1"));
                    if (lstFirstPage.Count > 0)
                    {
                        DlkLink FirstPage = new DlkLink("First Page", lstFirstPage.First());
                        FirstPage.Click();
                    }

                    while (bContinue)
                    {
                        RefreshResultItems();
                        foreach (DlkResultItem resultItem in mlstResultItems)
                        {
                            if (itemTitle.ToLower() == resultItem.GetItemTitle().ToLower())
                            {
                                DlkLogger.LogInfo("Successfully executed FindResultItem(). '" + itemTitle + "' found in list.");
                                mobjActiveResultItem = resultItem;
                                bFound = true;
                                break;

                            }
                        }

                        if (!bFound)
                        {
                            IList<IWebElement> lstNextPage = mElement.FindElements(By.LinkText(mstrNextPageLinkText));
                            if (lstNextPage.Count == 0)
                            {
                                bContinue = false;
                            }
                            else
                            {
                                DlkLink NextPage = new DlkLink("Next Page", lstNextPage.First());
                                NextPage.Click();
                            }
                        }
                        else
                        {
                            bContinue = false;
                        }
                    }

                    DlkAssert.AssertEqual("VerifyResultItemExists()", Convert.ToBoolean(expectedResult), bFound);
                }, new String[]{"retry"});

        }

        [Keyword("ClickResultItem")]
        public void ClickResultItem(String ResultItemIndex)
        {
            FindResultItemByIndex(ResultItemIndex);
            if (mobjActiveResultItem != null)
            {
                mobjActiveResultItem.ClickItem();
            }
            else
            {
                throw new Exception("ClickResultItem() failed. No active result item selected. Please add FindResultItem keyword prior to this step.");
            }
        }

        [Keyword("GetResultItemTitle", new String [] {"1|text|VariableName|MyTitle"})]
        public void GetResultItemTitle(String ResultItemIndex, String VariableName)
        {
            FindResultItemByIndex(ResultItemIndex);
            if (mobjActiveResultItem != null)
            {
                DlkVariable.SetVariable(VariableName, mobjActiveResultItem.GetItemTitle());
            }
            else
            {
                throw new Exception("GetResultItemTitle() failed. No active result item selected. Please add FindResultItem keyword prior to this step.");            
            }
        }

        [RetryKeyword("VerifyResultItemDetail", new String[] { "1|text|Detail Caption|Task Order #",
                                                          "2|text|Expected Value|1234"})]
        public void VerifyResultItemDetail(String ResultItemIndex, String DetailCaption, String ExpectedValue)
        {
            FindResultItemByIndex(ResultItemIndex);
            String detailCaption = DetailCaption;
            String expectedValue = ExpectedValue;

             this.PerformAction(() =>
                 {
                     if (mobjActiveResultItem != null)
                     {
                         DlkAssert.AssertEqual("VerifyResultItemDetail()", expectedValue, mobjActiveResultItem.GetItemDetail(detailCaption));
                     }
                     else
                     {
                         throw new Exception("VerifyResultItemDetail() failed. No active result item selected. Please add FindResultItem keyword prior to this step.");
                     }
                 }, new String[]{"retry"});
        }

        [RetryKeyword("ClickResultItemDetail", new String[] { "1|text|Detail Caption|Task Order #",
                                                          "2|text|Expected Value|1234"})]
        public void ClickResultItemDetail(String ResultItemIndex, String DetailCaption)
        {
            FindResultItemByIndex(ResultItemIndex);
            String detailCaption = DetailCaption;

            this.PerformAction(() =>
            {
                if (mobjActiveResultItem != null)
                {
                    mobjActiveResultItem.GetItemDetailLink(detailCaption).Click();
                }
                else
                {
                    throw new Exception("ClickResultItemDetail() failed. No active result item selected. Please add FindResultItem keyword prior to this step.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyResultItemDetailTitle", new String[] { "1|text|Detail Caption Title|Title"})]
        public void VerifyResultItemDetailTitle(String ResultItemIndex, String CaptionTitle)
        {
            FindResultItemByIndex(ResultItemIndex);
            String expectedValue = CaptionTitle;

            this.PerformAction(() =>
            {
                if (mobjActiveResultItem != null)
                {
                    DlkAssert.AssertEqual("VerifyResultItemDetailTitle()", expectedValue, mobjActiveResultItem.GetItemDetailTitle(expectedValue));
                }
                else
                {
                    throw new Exception("VerifyResultItemDetailTitle() failed. No active result item selected. Please add FindResultItem keyword prior to this step.");
                }
            }, new String[]{"retry"});
        }

        [Keyword("MarkResultItem", new String[] { "1|text|Mark Value (0,1,2,3,4,5,hide)|hide"})]
        public void MarkResultItem(String ResultItemIndex, String Value)
        {
            FindResultItemByIndex(ResultItemIndex);
            if (mobjActiveResultItem != null)
            {
                mobjActiveResultItem.MarkItem(Value);
            }
            else
            {
                throw new Exception("MarkResultItem() failed. No active result item selected. Please add FindResultItem keyword prior to this step.");
            }
        }

        [RetryKeyword("VerifyMarkValue", new String[] { "1|text|Mark Value (0,1,2,3,4,5,hide)|hide" })]
        public void VerifyMarkValue(String ResultItemIndex, String Value)
        {
            FindResultItemByIndex(ResultItemIndex);
            if (mobjActiveResultItem != null)
            {
                DlkAssert.AssertEqual("VerifyMarkValue", Value, mobjActiveResultItem.GetMarkItem().GetValue());
            }
            else
            {
                throw new Exception("VerifyMarkValue() failed. No active result item selected. Please add FindResultItem keyword prior to this step.");
            }
        }
    }
}

