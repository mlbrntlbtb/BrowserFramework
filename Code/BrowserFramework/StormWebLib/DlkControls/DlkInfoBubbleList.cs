using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using StormWebLib.System;

namespace StormWebLib.DlkControls
{
    [ControlType("InfoBubbleList")]
    public class DlkInfoBubbleList : DlkBaseControl
    {
        public DlkInfoBubbleList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkInfoBubbleList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkInfoBubbleList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("ClickItemAtIndex", new String[] { "1|text|Index|1" })]
        public void ClickItemAtIndex(string Index)
        {
            try
            {
                Initialize();
                IWebElement target = this.mElement.FindElement(By.XPath("./*[" + Index + "]"));
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
                bool duplicateExists = false;

                if (this.mElement.GetAttribute("class").Equals("results bubble"))
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath(".//div[@class='search-name linked-text']")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            duplicateExists = true;
                            break;
                        }
                    }
                    foreach (IWebElement elm in mElement.FindElements(By.XPath(".//div[@class='secondary-name']")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            duplicateExists = true;
                            break;
                        }
                    }
                }
                else if(this.mElement.FindElements(By.XPath("./*[text()='" + Item + "']")).Count > 0)
                {
                    duplicateExists = true;
                }
                
                DlkAssert.AssertEqual("VerifyAvailableInList()", bool.Parse(TrueOrFalse), duplicateExists);
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
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./*")))
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
        public void VerifyValueAtIndex(String Index, String Value)
        {
            try
            {
                Initialize();
                IWebElement target = mElement.FindElement(By.XPath("./*[" + Index + "]"));
                DlkAssert.AssertEqual("VerifyValueAtIndex()", Value, new DlkBaseControl("TargetItem", target).GetValue());
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
                int actualCount = mElement.FindElements(By.XPath("./*")).Count;
                if (actualCount < 0)
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
