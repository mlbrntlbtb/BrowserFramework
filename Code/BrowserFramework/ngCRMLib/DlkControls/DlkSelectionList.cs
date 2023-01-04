using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;

namespace ngCRMLib.DlkControls
{
    [ControlType("SelectionList")]
    public class DlkSelectionList : DlkBaseControl
    {
        public DlkSelectionList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSelectionList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSelectionList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public new bool VerifyControlType()
        {
            FindElement();
            if (GetAttributeValue("type") == "checkbox")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("SelectItems", new String[] { "1|text|Items|Item1~Item2~Item3",
                                                "2|text|ExpectedValue|TRUE"})]
        public void SelectItems(String Items, String TrueOrFalse)  
        {
            try
            {
                Initialize();
                String[] lItem = Items.Split('~');
                foreach (String li in lItem)
                {

                    foreach (IWebElement elm in mElement.FindElements(By.TagName("li")))
                        {
                            
                            DlkBaseControl ctl = new DlkBaseControl("element", elm);

                            if (ctl.GetValue() == li)
                            {
                                IWebElement elm2 = elm.FindElement(By.TagName("input"));
                                DlkCheckBox chk = new DlkCheckBox("Checkbox", elm2);
                                chk.Set(TrueOrFalse);
                                DlkAssert.AssertEqual("SelectItems()", Convert.ToBoolean(TrueOrFalse), chk.GetCheckedState());
                                break;
                            }
                        }
                    //if that didn't work, try this

                    foreach (IWebElement elm in mElement.FindElements(By.TagName("div")))
                    {

                        DlkBaseControl ctl = new DlkBaseControl("element", elm);

                        if (ctl.GetValue().Contains(li))
                        {
                            IWebElement elm2 = elm.FindElement(By.TagName("input"));
                            DlkCheckBox chk = new DlkCheckBox("Checkbox", elm2);
                            chk.Set(TrueOrFalse);
                            DlkAssert.AssertEqual("SelectItems()", Convert.ToBoolean(TrueOrFalse), chk.GetCheckedState());
                            break;
                        }
                    }

                }
                DlkLogger.LogInfo("SelectItems() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("UnselectItems() failed : " + e.Message, e);
            }
        }

        //[Keyword("UnselectItems", new String[] { "1|text|Count|10" })]
        //public void UnselectItems(String Items)
        //{
        //    try
        //    {
        //        Initialize();
        //        string bIsChecked = "False";
        //        String[] lItem = Items.Split('~');
        //        foreach (String li in lItem)
        //        {
        //            try
        //            {
        //                IWebElement elm = mElement.FindElement(By.XPath("//div[@class='iq-opp-new-records']/descendant::div[@class='search-name'][contains(text(),'" + li + "')]/parent::*[1]/preceding-sibling::*[@class='chkSelect']"));
        //                DlkCheckBox chk = new DlkCheckBox("Checkbox", elm);
        //                chk.Set(bIsChecked);
        //                DlkAssert.AssertEqual("UnselectItems()", Convert.ToBoolean(bIsChecked), chk.GetCheckedState());
        //            }
        //            catch (Exception e)
        //            {
        //                throw new Exception("UnselectItems() failed : " + e.Message, e);
        //            }
        //        }
        //        DlkLogger.LogInfo("UnselectItems() successfully executed.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("UnselectItems() failed : " + e.Message, e);
        //    }
        //}

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

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Item|ItemToFind",
                                                         "2|text|ExpectedValue|TRUE"})]
        public void VerifyAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                // needed to loop since value is not visible in DOM for newly added items
                bool actual = false;
                //  foreach (IWebElement elm in mElement.FindElements(By.XPath("//div[@class='iq-opp-new-records']/descendant::div[@class='search-name']")))
                IList<IWebElement> elementsList = new List<IWebElement>();

                if (mElement.FindElements(By.TagName("li")).Count > 0)
                    elementsList = mElement.FindElements(By.TagName("li"));
                else if (mElement.FindElements(By.TagName("input")).Count > 0)
                    elementsList = mElement.FindElements(By.TagName("input"));
                else
                    throw new Exception("Unable to find contents of the list.");

                foreach (IWebElement elm in elementsList)
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == Item)
                    {
                        actual = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyAvailableInList()", bool.Parse(TrueOrFalse), actual);
                DlkLogger.LogInfo("VerifyAvailableInList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyList", new String[] { "1|text|Items|Item1~Item2~Item3" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                string actual = "";
              //  foreach (IWebElement elm in mElement.FindElements(By.XPath("//div[@class='iq-opp-new-records']/descendant::div[@class='search-name']")))
                foreach (IWebElement elm in mElement.FindElements(By.TagName("li")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    actual += ctl.GetValue() + "~";
                }
                DlkAssert.AssertEqual("VerifyList()", Items, actual.Trim('~'));
                DlkLogger.LogInfo("VerifyList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        //[Keyword("VerifyValueAtIndex", new String[] { "1|text|Items|Item1~Item2~Item3" })]
        //public void VerifyValueAtIndex(String Index, String Value)
        //{
        //    try
        //    {
        //        Initialize();
        //        IWebElement inputField = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/input[1]"));
        //        DlkAssert.AssertEqual("VerifyValueAtIndex()", Value, new DlkBaseControl("TargetItem", inputField).GetValue());
        //        DlkLogger.LogInfo("VerifyValueAtIndex() successfully executed.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyValueAtIndex() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("VerifyItemCount", new String[] { "1|text|Count|10" })]
        //public void VerifyItemCount(String Count)
        //{
        //    try
        //    {
        //        Initialize();
        //        int actualCount = mElement.FindElements(By.XPath("./ul[1]/li")).Count - 1;
        //        if(actualCount < 0)
        //        {
        //            actualCount = 0;
        //        }
        //        DlkAssert.AssertEqual("VerifyItemCount()", int.Parse(Count), actualCount);
        //        DlkLogger.LogInfo("VerifyItemCount() successfully executed.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyItemCount() failed : " + e.Message, e);
        //    }
        //}

        
    }
}
