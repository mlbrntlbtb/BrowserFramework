using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace ngCRMLib.DlkControls
{
    [ControlType("ScrollingList")]
    public class DlkScrollingList : DlkBaseControl
    {
        public DlkScrollingList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkScrollingList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkScrollingList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Index|1",
                                            "2|text|VariableName|Sample"})]
        public void AssignValueToVariable(String Index, String VariableName)
        {
            try
            {
                Initialize();
                IWebElement target = mElement.FindElement(By.XPath("./*[@class='results']/descendant::li[" + Index + "]"));
                DlkFunctionHandler.AssignToVariable(VariableName, new DlkBaseControl("Target", target).GetValue());
                DlkLogger.LogInfo("AssignValueToVariable() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignIndexToVariable", new String[] { "1|text|Value|ItenValue",
                                                         "2|text|VariableName|Sample"})]
        public void AssignIndexToVariable(String Value, String VariableName)
        {
            try
            {
                Initialize();
                for (int idx = 0; idx < mElement.FindElements(By.XPath("./*[@class='results']/descendant::li")).Count; idx++)
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", mElement.FindElements(By.XPath("./*[@class='results']/descendant::li"))[idx]);
                    if (ctl.GetValue() == Value)
                    {
                        DlkFunctionHandler.AssignToVariable(VariableName, idx.ToString());
                        DlkLogger.LogInfo("AssignIndexToVariable() successfully executed.");
                        return;
                    }
                }
                throw new Exception("Item not found in list.");
            }
            catch (Exception e)
            {
                throw new Exception("AssignIndexToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickItem", new String[] { "1|text|Item|Sample Item" })]
        public void ClickItem(String Item)
        {
            try
            {
                Initialize();
                IWebElement target = this.mElement.FindElement(By.XPath("./*[@class='results']/descendant::li[text()='" + Item + "']"));
                target.Click();
                DlkLogger.LogInfo("ClickItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItem() failed : " + e.Message, e);

            }
        }

        [Keyword("ClickItemSeries", new String[] { "1|text|Item|Sample Item" })]
        public void ClickItemSeries(String ItemSeries)
        {
            try
            {
                string[] items = ItemSeries.Split('~');
                for (int i = 0; i < items.Count(); i++)
                {
                    Initialize();
                    IWebElement target = null;
                    try { target = this.mElement.FindElement(By.XPath("./*[@class='results']/descendant::li[text()='" + items[i] + "']")); }
                    catch { target = this.mElement.FindElement(By.XPath("//descendant::li[text()='" + items[i] + "']")); }
                                       
                    new DlkBaseControl("item", target).Click(2);
                }
                DlkLogger.LogInfo("ClickItemSeries() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemSeries() failed : " + e.Message, e);

            }
        }

        [Keyword("ClickItemAtIndex", new String[] { "1|text|Index|1" })]
        public void ClickItemAtIndex(string Index)
        {
            try
            {
                Initialize();
                IWebElement target = this.mElement.FindElement(By.XPath("./*[@class='results']/descendant::li[" + Index + "]"));
                target.Click();
                DlkLogger.LogInfo("ClickItemAtIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("ClickItemAtIndex() failed : " + e.Message, e);

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

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Item|ItemToFind",
                                                         "2|text|ExpectedValue|TRUE"})]
        public void VerifyAvailableInList(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyAvailableInList()", bool.Parse(TrueOrFalse), 
                    this.mElement.FindElements(By.XPath("./*[@class='results']/descendant::li[text()='" + Item + "']")).Count > 0);
                DlkLogger.LogInfo("VerifyAvailableInList() passed.");
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
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./*[@class='results']/descendant::li")))
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

        [Keyword("VerifyValueAtIndex", new String[] { "1|text|Items|Item1~Item2~Item3" })]
        public void VerifyValueAtIndex(String Index, String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement target = mElement.FindElement(By.XPath("./*[@class='results']/descendant::li[" + Index + "]"));
                DlkAssert.AssertEqual("VerifyValueAtIndex()", ExpectedValue, new DlkBaseControl("TargetItem", target).GetValue());
                DlkLogger.LogInfo("VerifyValueAtIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValueAtIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemCount", new String[] { "1|text|Count|10" })]
        public void VerifyItemCount(String Count)
        {
            try
            {
                Initialize();
                int actualCount = mElement.FindElements(By.XPath("./*[@class='results']/descendant::li")).Count;
                if(actualCount < 0)
                {
                    actualCount = 0;
                }
                DlkAssert.AssertEqual("VerifyItemCount()", int.Parse(Count), actualCount);
                DlkLogger.LogInfo("VerifyItemCount() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
            }
        }
    }
}
