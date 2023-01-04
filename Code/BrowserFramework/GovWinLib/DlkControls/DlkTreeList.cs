using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("TreeList")]
    public class DlkTreeList : DlkBaseControl
    {
        private String mstrListType;
        private IList<IWebElement> itemList;


        public DlkTreeList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTreeList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTreeList(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }


        public void Initialize()
        {
            RefreshList();
        }

        private void RefreshList()
        {
            FindElement();

            if ((mElement.GetAttribute("class")).ToLower().Contains("select2"))
            {
                mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@class,'select2-drop')]/ul"));
                if (mElement.Text == "")
                    mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@class,'select2-drop-active')]//ul[@class='select2-results']"));
                if(mElement.Text == "")
                    mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@id='select2-drop']//ul[@class='select2-results']"));
                    
            }

            if ((mElement.GetAttribute("tagName").ToLower() == "div"))
            {
                mElement = mElement.FindElement(By.XPath("./ul"));
            }

            if (mElement.GetAttribute("tagName").ToLower() == "ul")
            {
                mstrListType = "ul";
                itemList = mElement.FindElements(By.CssSelector("li"));
            }            

        }

        [RetryKeyword("VerifyInList", new String[] {   "1|text|Partial Text to Verify|Sample Value1",
                                                        "2|text|Expected Value (TRUE or FALSE)|TRUE"})]
        public void VerifyInList(String PartialTextToVerify, String TrueOrFalse)
        {
            String partialText = PartialTextToVerify;
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    bool isFound = false;

                    RefreshList();

                    switch (mstrListType)
                    {
                        case "ul":
                            foreach (IWebElement item in itemList)
                            {
                                DlkBaseControl itemControl = new DlkBaseControl("List Item", item);
                                isFound = (item.Text.Contains(partialText)) ? true : false;

                                if (isFound == true)
                                {
                                    break;
                                }

                            }
                            break;
                        default:
                            throw new Exception("VerifyInList() failed. List control of type '" + mstrListType + "' not supported.");
                    }

                    DlkAssert.AssertEqual("VerifyInList()", Convert.ToBoolean(expectedValue), isFound);
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String ExpectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                /*
                Boolean bExists = Exists();

                if (bExists == Convert.ToBoolean(ExpectedValue))
                {
                    DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bExists) + " : Expected = " + ExpectedValue);
                }
                else
                {
                    throw new Exception("VerifyExists() failed : Actual = " + Convert.ToString(bExists) + " : Expected = " + ExpectedValue));
                }*/
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

        [RetryKeyword("VerifyList", new String[] { "1|text|Expected Items|Sample Value1~Sample Value2~Sample Value3" })]
        public void VerifyList(String ExpectedItems)
        {
            String expectedItems = ExpectedItems;

            this.PerformAction(() =>
                {
                    String sActualListItems = "";

                    RefreshList();
                    switch (mstrListType)
                    {
                        case "ul":
                            Boolean bFirst = true;
                            foreach (IWebElement item in itemList)
                            {
                                DlkBaseControl itemControl = new DlkBaseControl("List Item", item);
                                if (!bFirst)
                                {
                                    sActualListItems = sActualListItems + "~" + itemControl.GetValue();
                                }
                                else
                                {
                                    sActualListItems = itemControl.GetValue();
                                    bFirst = false;
                                }
                            }
                            break;
                        default:
                            throw new Exception("VerifyList() failed. List control of type '" + mstrListType + "' not supported.");
                    }

                    DlkAssert.AssertEqual("VerifyList()", expectedItems.ToLower(), sActualListItems.ToLower());
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyPartialList", new String[] { "1|text|Expected Items|Sample Value1~Sample Value2~Sample Value3" })]
        public void VerifyPartialList(String ExpectedItems)
        {
            String expectedItems = ExpectedItems;

            this.PerformAction(() =>
                {
                    bool isExisting = true;

                    RefreshList();
                    switch (mstrListType)
                    {
                        case "ul":
                            {
                                #region Search if pattern exist
                                string[] searchPatterns = expectedItems.Split(new char[] { '~' });

                                List<String> text = new List<String>();

                                foreach (IWebElement item in itemList)
                                {
                                    text.Add((new DlkBaseControl("List Item", item)).GetValue());
                                }

                                int index = 0;
                                foreach (string searchPattern in searchPatterns.ToList())
                                {
                                    //perform forward search
                                    index = (text.FindIndex(index, s => s.Contains(searchPattern)));
                                    if (index < 0)
                                    {
                                        isExisting = false;
                                        throw new Exception(string.Format("VerifyPartialList() was unable to find {0}", searchPattern));
                                    }
                                }
                                #endregion
                            }
                            break;
                        default:
                            throw new Exception("VerifyList() failed. List control of type '" + mstrListType + "' not supported.");
                    }

                    if (isExisting)
                        DlkLogger.LogInfo("VerifyPartialList() succeeded.");
                }, new String[]{"retry"});               
        }

        [Keyword("Click")]
        public new void Click()
        {
            FindElement();
            base.Click();
            DlkLogger.LogInfo("Click() passed.");
        }

        [Keyword("ClickLinkWithText", new String[] {    "1|text|Click Row|O{Row} or Row Number",
                                                        "2|text|Link Containing Text|Sample Text"})]
        public void ClickLinkWithText(String rowNumber, String searchText)
        {
            RefreshList();            

            try{

                int iRow = Convert.ToInt32(rowNumber) - 1;

                if (iRow < 0)
                    throw new Exception("ClickLinkWithText() failed. Row should be greater than zero. ");
                
                DlkBaseControl rowItem = new DlkBaseControl("Row Item", itemList[iRow]);
                string searchPath = string.Format(".//*[contains(text(),'{0}')]", searchText);
                IWebElement linkWithtext = rowItem.mElement.FindElement(By.XPath(searchPath));
                if (linkWithtext != null)
                    linkWithtext.Click();
                else
                    throw new Exception(string.Format("ClickLinkWithText() failed. Unable to find link with text {0}", searchText));
            }
            catch(Exception e)
            {
                throw new Exception("ClickLinkWithText() failed. Unable to get cell with Row=" + rowNumber, e);                
            }

        }

        [Keyword("ClickLinkCheckbox", new String[] {    "1|text|Click Row|O{Row} or Row Number",
                                                        "2|text|Link Containing Text|Sample Text"})]
        public void ClickLinkCheckbox(String searchText)
        {
            int iRow = 0;
            RefreshList();

            try
            {

                for (int i = 0; i < itemList.Count; i++)
                {
                    DlkBaseControl itemCtrl = new DlkBaseControl("Row Item", itemList[i]);
                    if (itemCtrl.GetValue().Contains(searchText))
                    {
                        iRow = i;                        
                        break;
                    }
                }


                if (iRow < 0)
                    throw new Exception("ClickLinkWithText() failed. Row should be greater than zero. ");

                DlkBaseControl rowItem = new DlkBaseControl("Row Item", itemList[iRow]);
                string searchPath = string.Format("//span[contains(@class,'checkbox')]");
                IWebElement checkBoxByText = rowItem.mElement.FindElement(By.XPath(searchPath));
                if (checkBoxByText != null)
                    checkBoxByText.Click();
                else
                    throw new Exception(string.Format("ClickLinkCheckbox() failed. Unable to find link with text {0}", searchText));
            }
            catch (Exception e)
            {
                throw new Exception("ClickLinkCheckbox() failed. Unable to get cell with Row=" + iRow, e);
            }

        }

        [Keyword("ClickLinkWithRowIndex", new String[] {"1|text|Click Row|O{Row} or Row Number"})]
        public void ClickLinkWithRowIndex(String rowNumber)
        {
            RefreshList();

            try
            {

                int iRow = Convert.ToInt32(rowNumber) - 1;

                if (iRow < 0)
                    throw new Exception("ClickLinkWithRowIndex() failed. Row should be greater than zero. ");

                DlkBaseControl rowItem = new DlkBaseControl("Row Item", itemList[iRow]);
                rowItem.mElement.Click();
                
            }
            catch (Exception e)
            {
                throw new Exception("ClickLinkWithRowIndex() failed. Unable to get cell with Row=" + rowNumber, e);
            }

        }

        [RetryKeyword("VerifyLinkExistsInListRow", new String[] {  "1|text|Verify Link On Row|O{Row}",
                                                                    "2|text|Link Containing Text|Sample Text",
                                                                    "3|text|Expected Result|TRUE" })]
        public void VerifyLinkExistsInListRow(String VerifyLinkOnRow, String LinkContainingText, String TrueOrFalse)
        {
            String rowNumber = VerifyLinkOnRow;
            String searchText = LinkContainingText;
            String expectedResult = TrueOrFalse;

            this.PerformAction(() =>
                {
                    bool isFound = false;

                    RefreshList();
                    try
                    {
                        int iRow = Convert.ToInt32(rowNumber) - 1;

                        if (iRow < 0)
                            throw new Exception("ClickLinkWithText() failed. Row should be greater than zero. ");

                        DlkBaseControl rowItem = new DlkBaseControl("Row Item", itemList[iRow]);
                        string searchPath = string.Format("//a[contains(text(),'{0}')]", searchText);
                        IWebElement linkWithtext = rowItem.mElement.FindElement(By.XPath(searchPath));
                        isFound = (linkWithtext != null) ? true : false;
                        DlkAssert.AssertEqual("VerifyLinkExistsInListRow()", Convert.ToBoolean(expectedResult), isFound);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("ClickLinkWithText() failed. Unable to get cell with Row=" + rowNumber, e);
                    }
                }, new String[]{"retry"});
        }

        [Keyword("GetListRowWithText", new String[] { "1|text|Text|Sample Text",
                                                        "2|text|VariableName|MyListRow"})]
        public void GetListRowWithText(String searchText, String VariableName)
        {
            bool isFound = false;


            RefreshList();

            for (int i = 0; i < itemList.Count; i++)
            {
                DlkBaseControl itemCtrl = new DlkBaseControl("Row Item", itemList[i]);
                if (itemCtrl.GetValue().Contains(searchText))
                {
                    DlkVariable.SetVariable(VariableName,(i + 1).ToString());
                    isFound = true;
                    DlkLogger.LogInfo(string.Format("Successfully executed GetListRowWithText(). Found {0} in {1}th row", searchText, i));
                    break;
                }                
            }
            
            if(!isFound)
                throw new Exception(string.Format("GetListRowWithText() failed. Unable to find {0} in the list", searchText));
        }

    }
}

