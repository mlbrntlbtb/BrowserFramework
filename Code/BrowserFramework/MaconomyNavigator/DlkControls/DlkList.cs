using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.Linq;

namespace MaconomyNavigatorLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkTextBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {

            FindElement();
        }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String Item)
        {
            try
            {
                Initialize();

                if (mElement.GetAttribute("class").Equals("dropdown-menu settings"))
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::li/a")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            ctl.Click();
                            DlkLogger.LogInfo("Successfully executed Select()");
                            return;
                        }
                    }
                }
                else
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::*[contains(@class,'ng-scope')]")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            ctl.Click();
                            DlkLogger.LogInfo("Successfully executed Select()");
                            return;
                        }
                    }
                }
                
                throw new Exception("Item not found in list");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword selects the n-th item in the notifications list where n is the non-zero index provided by the user.
        /// </summary>
        /// <param name="NonZeroIndex"></param>
        [Keyword("SelectByIndex", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectByIndex(String NonZeroIndex)
        {
            try
            {
                Initialize();
                //The Xpath for the notifications list: //span[contains(@class,'badge')]/following::ul[1]
                //Starting from the unordered list as the parent, find the specific <a> tag
                IWebElement listItem = mElement.FindElement(By.XPath(string.Format("./descendant::a[{0}]", NonZeroIndex)));
                listItem.Click();
                DlkLogger.LogInfo(string.Format("SelectByIndex() passed. Notification {0} was clicked.",NonZeroIndex));
                
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
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

        [Keyword("VerifyList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyList(string Items)
        {
            try
            {
                Initialize();

                string actual = "";
                string expected = "";

                // process expected
                foreach (string expItm in Items.Split('~'))
                {
                    expected += DlkString.ReplaceCarriageReturn(expItm, "") + "~";
                }
                expected = expected.Trim('~');

                if (mElement.GetAttribute("class").Equals("dropdown-menu settings"))
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::li/a")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                    }
                }
                else
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::*[contains(@class,'ng-scope')]")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        actual += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                    }
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

        [Keyword("VerifyCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyCount(string Count)
        {
            try
            {
                Initialize();
                int actual = 0;
                if (mElement.GetAttribute("class").Equals("dropdown-menu settings"))
                {
                    actual = mElement.FindElements(By.XPath("./descendant::li/a")).Count;
                }
                else
                {
                    actual = mElement.FindElements(By.XPath("./descendant::*[contains(@class,'ng-scope')]")).Count;
                }
                int expected = int.Parse(Count);

                DlkAssert.AssertEqual("VerifyCount()", expected, actual);
                DlkLogger.LogInfo("VerifyCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemInList(string Item, string TrueOrFalse)
        {
            //todo: verify item in list

            try
            {
                Initialize();

                bool actual = false;
                bool expected = bool.Parse(TrueOrFalse);

                //foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::li[@class='ng-scope']")))
                if (mElement.GetAttribute("class").Equals("dropdown-menu settings"))
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::li/a")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            actual = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::*[contains(@class,'ng-scope')]")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                        if (DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") == DlkString.ReplaceCarriageReturn(Item, ""))
                        {
                            actual = true;
                            break;
                        }
                    }
                }
                DlkAssert.AssertEqual("VerifyItemInList()", expected, actual);
                DlkLogger.LogInfo("VerifyItemInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialTextInList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPartialTextInList(string Item, string TrueOrFalse)
        {
            try
            {
                Initialize();

                bool actual = false;
                bool expected = bool.Parse(TrueOrFalse);

                var elementsList = mElement.GetAttribute("class").Equals("dropdown-menu settings") ? 
                    mElement.FindElements(By.XPath("./descendant::li/a")) : 
                    mElement.FindElements(By.XPath("./descendant::*[contains(@class,'ng-scope')]"));
                //look for any element in the list that contains the string Item
                actual = elementsList.Any(x => DlkString.ReplaceCarriageReturn(new DlkBaseControl("Item", x).GetValue(), "")
                    .Contains(DlkString.ReplaceCarriageReturn(Item, "")) );

                DlkAssert.AssertEqual("VerifyPartialTextInList()", expected, actual);
                DlkLogger.LogInfo("VerifyPartialTextInList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialTextInList() failed : " + e.Message, e);
            }
        }
    }
}
