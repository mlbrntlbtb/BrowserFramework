using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AjeraTimeLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using CommonLib.DlkUtility;

namespace AjeraTimeLib.DlkControls
{
    [ControlType("Listbox")]
    public class DlkListbox : DlkAjeraTimeBaseControl
    {
        #region DECLARATIONS
        private int retryLimit = 3;
        #endregion

        #region CONSTRUCTORS
        public DlkListbox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkListbox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public DlkListbox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public DlkListbox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (mElement == null)
            {
                FindElement();
            }
        }

        #endregion

        #region KEYWORDS
        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Text" })]
        public void VerifyText(string ItemIndex, string ExpectedValue)
        {
            try
            {
                String ActualResult = "";

                Initialize();
                ActualResult = GetItemAtIndex(ItemIndex);

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue.Trim(), ActualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Item Index|1" })]
        public void SelectByIndex(String ItemIndex)
        {
            try
            {
                int iTargetItemPos = 0;

                Initialize();

                bool bFound = false;
                int curRetry = 0;
                
                while (++curRetry <= retryLimit && !bFound)
                {
                    int idx = 1;
                    IList<IWebElement> actionItems;
                    actionItems = mElement.FindElements(By.TagName("li"));

                    if (ItemIndex == "last")
                        iTargetItemPos = actionItems.Count;
                    else if (ItemIndex == "first")
                        iTargetItemPos = idx;
                    else
                        iTargetItemPos = Convert.ToInt32(ItemIndex);

                    foreach (IWebElement aListItem in actionItems)
                    {
                        var dlkMenuItem = new DlkBaseControl("List Item", aListItem);
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
                if (!bFound)
                {
                    throw new Exception("SelectByIndex() failed. Control : " + mControlName + " : '" + iTargetItemPos +
                                       "' cannot be found in the list.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemContains", new String[] { "1|text|Text To Verify|Sample Text" })]
        public void VerifyItemContains(string ItemIndex, string ExpectedValue)
        {
            try
            {
                string ActualResult = "";
                Initialize();

                ActualResult = GetItemAtIndex(ItemIndex);

                DlkAssert.AssertEqual("VerifyItemContains() : " + mControlName, true, ActualResult.Contains(ExpectedValue.Trim()));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemContains() failed : " + e.Message, e);
            }
        }

        [Keyword("GetFullText", new String[] { "1|text|Value|SampleValue" })]
        public void GetFullText(String RowIndex, String VariableName)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                String ActValue = "";
                for (int i = 0; i < this.iFindElementDefaultSearchMax; i++)
                {
                    try
                    {
                        mSelected = mElement.FindElements(By.TagName("li")).ElementAt(int.Parse(RowIndex) - 1);
                        if (mSelected != null)
                        {
                            break;
                        }
                    }
                    catch (OpenQA.Selenium.NoSuchElementException)
                    {
                    }
                    Thread.Sleep(1000);
                }
                DlkBaseControl ctlItem = new DlkBaseControl("Item", mSelected);
                ctlItem.ScrollIntoViewUsingJavaScript();

                ActValue = ctlItem.GetValue();
                DlkVariable.SetVariable(VariableName, FormatString(ActValue));
                DlkLogger.LogInfo("GetFullText() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetFullText() failed : " + e.Message, e);
            }
        }

        [Keyword("GetIndexContains", new String[] { "1|text|Value|ItenValue",
                                                    "2|text|VariableName|Sample"})]
        public void GetIndexContains(String PartialText, String VariableName)
        {
            try
            {
                Initialize();
                Boolean bFound = false;
                mElementList = mElement.FindElements(By.TagName("li"));
                for (int i = 0; i < mElementList.Count(); i++)
                {
                    if (mElementList[i].Text.Contains(PartialText))
                    {
                        DlkFunctionHandler.AssignToVariable(VariableName, (i + 1).ToString());
                        DlkLogger.LogInfo("GetIndexContains() successfully executed.");
                        bFound = true;
                        break;
                    }
                }
                if(bFound == false)
                {
                throw new Exception("GetIndexContains() failed: Item not found in list.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("GetIndexContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChecked", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyChecked(String ItemIndex, String IsChecked)
        {
            try
            {
                Boolean bIsChecked = Convert.ToBoolean(IsChecked);
                Initialize();
                IWebElement mSelected = GetElementAtIndex(ItemIndex);
    
                Boolean bCurrentValue = mSelected.FindElement(By.TagName("span")).Displayed;
                DlkAssert.AssertEqual("VerifyChecked() : " + mControlName, bIsChecked, bCurrentValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChecked() failed : " + e.Message, e);
            }
        }
        #endregion 

        #region METHODS
        private static String FormatString(String InputString)
        {
            String sResult = "";
            String[] items;

            items = InputString.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length > 1)
            {
                int maxVal = items.Length - 1;

                for (int i = 0; i < maxVal; i++)
                {
                    sResult += items[i].Trim() + ";";
                }

                sResult += items[maxVal].Trim();
            }
            else
            {
                sResult = items[0].Trim();
            }

            return sResult;
        }

        public String GetItemAtIndex(string ItemIndex)
        {
            String actualResult = "";
            IWebElement mSelected = null;

            mElementList = mElement.FindElements(By.TagName("li"));
            if (Convert.ToInt32(ItemIndex) - 1 > mElementList.Count)
            {
                throw new Exception("'" + ItemIndex + "' is out of bounds.");
            }

            mSelected = mElementList.ElementAt(Convert.ToInt32(ItemIndex) - 1);

            actualResult = mSelected.Text.Trim();
            if(actualResult.Contains("\r\n\r\n"))
            {
                actualResult = actualResult.Replace("\r\n\r\n", ";");
            }
            else if (actualResult.Contains("\r\n"))
            {
                actualResult = actualResult.Replace("\r\n", ";");
            }
            return actualResult;
        }

        public IWebElement GetElementAtIndex(string ItemIndex)
        {
         
            IWebElement mSelected = null;

            mElementList = mElement.FindElements(By.TagName("li"));
            if (Convert.ToInt32(ItemIndex) - 1 > mElementList.Count)
            {
                throw new Exception("'" + ItemIndex + "' is out of bounds.");
            }

            mSelected = mElementList.ElementAt(Convert.ToInt32(ItemIndex) - 1);

            return mSelected;
        }
        #endregion
    }
}