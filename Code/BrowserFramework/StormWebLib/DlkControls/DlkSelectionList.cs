using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using StormWebLib.System;
using System.Threading;
using System.Linq;
using CommonLib.DlkUtility;

namespace StormWebLib.DlkControls
{
    [ControlType("SelectionList")]
    public class DlkSelectionList : DlkBaseControl
    {

        #region CONSTANTS
        private const String CoreFieldClass = "core-field";
        private const int SHORT_WAIT = 1000;
        #endregion

        #region Private Variables
        private String mSelectionListType = "";
        #endregion

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
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();

            String mClass = this.mElement.GetAttribute("class");
            if (mClass.Contains(CoreFieldClass)) //"core-field"
            {
                mSelectionListType = CoreFieldClass;
            }
        }

        [Keyword("SelectItems", new String[] { "1|text|Items|Item1~Item2~Item3",
                                                "2|text|ExpectedValue|TRUE"})]
        public void SelectItems(String Items, String TrueOrFalse)
        {
            try
            {
                Initialize();
                Dictionary<String, Boolean> lItems = Items.Split('~')
                    .ToDictionary(item => item
                    , item => false); //Key as item | Value to indicate if the item is found
                foreach (String li in lItems.Keys.ToList())
                {
                    foreach (IWebElement elm in mElement.FindElements(By.TagName("li")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);

                        if (ctl.GetValue() == li)
                        {
                            var elm2 = elm.FindWebElementCoalesce(By.TagName("input"), By.ClassName("tree-checkbox"));
                            DlkCheckBox chk = new DlkCheckBox("Checkbox", elm2);
                            chk.Set(TrueOrFalse);
                            DlkAssert.AssertEqual("SelectItems()", Convert.ToBoolean(TrueOrFalse), chk.GetCheckedState());
                            lItems[li] = true; //Tag item as found
                            break;
                        }
                    }
                    if (lItems.Any(item => item.Value == false)) //For unfound item(s)
                    {
                        foreach (IWebElement elm in mElement.FindElements(By.TagName("div")))
                        {
                            DlkBaseControl ctl = new DlkBaseControl("element", elm);

                            if (ctl.GetValue().Contains(li))
                            {
                                var elm2 = elm.FindWebElementCoalesce(By.TagName("input"), By.XPath(".//*[contains(@class,'checkbox-icon')]"));
                                if (elm2 != null)
                                {
                                    DlkCheckBox chk = new DlkCheckBox("Checkbox", elm2);
                                    chk.Set(TrueOrFalse);
                                    DlkAssert.AssertEqual("SelectItems()", Convert.ToBoolean(TrueOrFalse), chk.GetCheckedState());
                                    lItems[li] = true; //Tag item as found
                                    break;
                                }
                            }
                        }
                    }

                    if (lItems.Any(item => item.Value == false))
                        throw new Exception(String.Format("SelectItems() : Control {0} not found", String.Join(", ", lItems
                            .Where(x => x.Value == false)
                            .Select(x => x.Key).ToList())));
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
                IList<IWebElement> elementList = mElement.FindElements(By.TagName("li"));
                if (elementList.Count < 1)
                {
                    elementList = mElement.FindElements(By.XPath("./div[contains(@class,'section-list')]"));
                }

                foreach (IWebElement elm in elementList)
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

        [Keyword("ExpandItemList", new String[] { "1|RowName" })]
        public void ExpandItemList(String ItemName)
        {
            try
            {
                Initialize();

                /* AJM [01/03/2018]: Readying a switch clause just in case multiple types of expandable SelectionList appear.
                 * For now, only existing example of this type is under Security > Roles.
                 */

                switch (mSelectionListType)
                {
                    case CoreFieldClass:
                        ExpandCoreSelectionList(ItemName);
                        break;
                    default:
                        throw new Exception("Unsupported SelectionList type.");
                }

                DlkLogger.LogInfo("ExpandItemList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandItemList() failed : " + e.Message, e);
            }
        }

        private void ExpandCoreSelectionList(String ItemName)
        {
            IWebElement item = mElement.FindElement(By.XPath(".//li[@data-key='" + ItemName + "']"));
            IWebElement treeBtn = null;
            new DlkBaseControl("Item", item).ScrollIntoViewUsingJavaScript();

            if (item.GetAttribute("class").Contains("has-subtree"))
            {
                treeBtn = item.FindElement(By.XPath(".//span[contains(@class,'icon toggle-children')]"));
                new DlkBaseControl("Button", treeBtn).Click();
                Thread.Sleep(SHORT_WAIT);
            }
            else
            {
                throw new Exception("Item has no chidren.");
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
