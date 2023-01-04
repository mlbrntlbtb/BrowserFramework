using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        private Boolean mblnHasEvent = false;
        private String mstrListType;
        private SelectElement mobjSelectElement;


        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            RefreshList();
        }

        private void RefreshList()
        {
            FindElement();
            if (mElement.GetAttribute("tagName").ToLower() == "select" || mElement.GetAttribute("class").Contains("select"))
            {
                mstrListType = "select";
                RefreshSelectList();
            }

        }

        private void RefreshSelectList()
        {
            mobjSelectElement = new SelectElement(mElement);
            String onchangeEvent = GetAttributeValue("onchange");
            if (onchangeEvent != null && onchangeEvent != "")
            {
                mblnHasEvent = true;
            }

        }

        [Keyword("Select", new String[] { "1|text|Item Text|Sample Value"})]
        public void Select(String ItemText)
        {
            RefreshList();
            switch (mstrListType)
            {
                case "select":
                    if (mblnHasEvent)
                    {
                        DlkEnvironment.CaptureUrl();
                    }
                    mobjSelectElement.DeselectAll();
                    mobjSelectElement.SelectByText(ItemText);
                    if (mblnHasEvent)
                    {
                        DlkEnvironment.WaitUrlUpdate();
                    }
                    break;
                default:
                    throw new Exception("Select() failed. List control of type '" + mstrListType + "' not supported.");
            }
            DlkLogger.LogInfo("Successfully executed Select() : " + ItemText);
        }

        [Keyword("SelectMultiple", new String[] { "1|text|Item Texts|Sample Value1~Sample Value2~Sample Value3" })]
        public void SelectMultiple(String ItemTexts)
        {
            RefreshList();
            switch (mstrListType)
            {
                case "select":
                    if (mblnHasEvent)
                    {
                        DlkEnvironment.CaptureUrl();
                    }
                    mobjSelectElement.DeselectAll();
                    foreach (String item in ItemTexts.Split('~'))
                    {
                        mobjSelectElement.SelectByText(item);
                    }
                    if (mblnHasEvent)
                    {
                        DlkEnvironment.WaitUrlUpdate();
                    }
                    break;
                default:
                    throw new Exception("SelectMultiple() failed. List control of type '" + mstrListType + "' not supported.");
            }
            DlkLogger.LogInfo("Successfully executed SelectMultiple() : " + ItemTexts);
        }

        #region Verify methods
        [RetryKeyword("VerifyInList", new String[] {   "1|text|List Item|Sample Value1",
                                                        "2|text|Expected Value (TRUE or FALSE)|TRUE"})]
        public void VerifyInList(String ListItem, String TrueOrFalse)
        {
            String listItem = ListItem;
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Boolean bFound = false;

                    RefreshList();

                    switch (mstrListType)
                    {
                        case "select":
                            foreach (IWebElement itemElement in mobjSelectElement.Options)
                            {
                                DlkBaseControl itemControl = new DlkBaseControl("List Item", itemElement);
                                if (listItem == itemControl.GetValue())
                                {
                                    bFound = true;
                                    break;
                                }
                            }
                            break;
                        default:
                            throw new Exception("VerifyInList() failed. List control of type '" + mstrListType + "' not supported.");
                    }

                    DlkAssert.AssertEqual("VerifyInList() " + mControlName, Convert.ToBoolean(expectedValue), bFound);
                }, new String[]{"retry"});
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
                        case "select":
                            Boolean bFirst = true;
                            foreach (IWebElement itemElement in mobjSelectElement.Options)
                            {
                                DlkBaseControl itemControl = new DlkBaseControl("List Item", itemElement);
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

                    DlkAssert.AssertEqual("VerifyList() " + mControlName, expectedItems.ToLower(), sActualListItems.ToLower());
                }, new String[]{"retry"});

        }

        [RetryKeyword("VerifySelected", new String[] { "1|text|Expected Values|Sample Value1~Sample Value2~Sample Value3" })]
        public void VerifySelected(String ExpectedValues)
        {
            String expectedValues = ExpectedValues;

            this.PerformAction(() =>
            {
                String sActualSelectedItems = "";

                RefreshList();
                switch (mstrListType)
                {
                    case "select":
                        Boolean bFirst = true;
                        foreach (IWebElement itemElement in mobjSelectElement.AllSelectedOptions)
                        {
                            DlkBaseControl itemControl = new DlkBaseControl("Selected Item", itemElement);
                            if (!bFirst)
                            {
                                sActualSelectedItems = sActualSelectedItems + "~" + itemControl.GetValue();
                            }
                            else
                            {
                                sActualSelectedItems = itemControl.GetValue();
                                bFirst = false;
                            }
                        }
                        break;
                    default:
                        throw new Exception("VerifySelected() failed. List control of type '" + mstrListType + "' not supported.");
                }

                DlkAssert.AssertEqual("VerifySelected() ", expectedValues.ToLower(), sActualSelectedItems.ToLower());
            }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyIsMultiSelect", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyIsMultiSelect(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Initialize();

                    String multipleAttribute = GetAttributeValue("multiple");
                    DlkAssert.AssertEqual("VerifyIsMultiSelect()", Convert.ToBoolean(expectedValue), Convert.ToBoolean(multipleAttribute));
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    /*Boolean bExists = base.Exists();

                    if (bExists == Convert.ToBoolean(expectedValue))
                    {
                        DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bExists) + " : Expected = " + expectedValue);
                    }
                    else
                    {
                        throw new Exception("VerifyExists() failed : Actual = " + Convert.ToString(bExists) + " : Expected = " + expectedValue));
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

        [RetryKeyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE",
                                                        "2|text|VariableName|IfExists"})]
        public void GetVerifyExists(String TrueOrFalse, String VariableName)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Boolean bExists = base.Exists();

                    DlkVariable.SetVariable(VariableName, Convert.ToString(bExists == Convert.ToBoolean(expectedValue)));
                }, new String[] { "retry" });
        }
        #endregion
    }
}

