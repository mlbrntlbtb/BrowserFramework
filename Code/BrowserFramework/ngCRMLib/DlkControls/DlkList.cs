using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace ngCRMLib.DlkControls
{
    [ControlType("List")]
    public class DlkList : DlkBaseControl
    {
        public DlkList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
   
        public void Initialize()
        {
            FindElement();
            
            String mClass = this.mElement.GetAttribute("class");   
        }

        [Keyword("AddItem", new String[] { "1|text|Item|NewItemToAdd" })]
        public void AddItem(String Item)
        {
            try
            {
                Initialize();

                IWebElement plusSign = mElement.FindElement(By.XPath("./descendant::span[text()='+']"));
                // Click plus sign
                new DlkBaseControl("AddButton", plusSign).Click();
                Thread.Sleep(3000); // 3 sec delay in between click and setting of value
                IWebElement selectedField = mElement.FindElement(By.XPath("./descendant::input[@class='org-level-item-name']"));
                selectedField.SendKeys(Item);
                selectedField.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("AddItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("AddItem() failed : " + e.Message, e);
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Index|1",
                                            "2|text|VariableName|Sample"})]
        public void AssignValueToVariable(String Index, String VariableName)
        {
            try
            {
                Initialize();
                IWebElement target = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/input[1]"));
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
                for (int idx = 0; idx < mElement.FindElements(By.XPath("./descendant::input")).Count; idx++)
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", mElement.FindElements(By.XPath("./descendant::input"))[idx]);
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

        [Keyword("EditItem", new String[] { "1|text|OldItemValue|OldValueOfItem",
                                            "2|text|NewItemValue|NewValueofItem"})]
        public void EditItem(String OldItemValue, String NewItemValue)
        {
            try
            {
                Initialize();

                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::input")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == OldItemValue)
                    {
                        ctl.ScrollIntoViewUsingJavaScript();
                        elm.Click();
                        elm.Clear();
                        elm.SendKeys(NewItemValue);
                        elm.SendKeys(Keys.Tab);
                        DlkLogger.LogInfo("EditItem() successfully executed.");
                        return;
                    }
                }
                //if that didn't work, try this
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::span")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == OldItemValue)
                    {
                        ctl.ScrollIntoViewUsingJavaScript();
                        elm.Click();
                        IWebElement input = mElement.FindElement(By.XPath("./descendant::input"));
                        input.Clear();
                        input.SendKeys(NewItemValue);
                        input.SendKeys(Keys.Tab);
                        DlkLogger.LogInfo("EditItem() successfully executed.");
                        return;
                    }
                }                
                throw new Exception("Item not found in list.");
            }
            catch (Exception e)
            {
                throw new Exception("EditItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SetItem", new String[] { "1|text|OldItemValue|OldValueOfItem"})]
        public void SetItem(String NewItemValue)
        {
            try
            {
                Initialize();
                foreach (IWebElement elm in mElement.FindElements(By.TagName("li")))
                {
                    if (elm.GetAttribute("data-row-disposition") == "new")
                    {
                        IWebElement mInput = elm.FindElement(By.XPath("./descendant::input"));
                        mInput.SendKeys(NewItemValue);
                        mInput.SendKeys(Keys.Enter);                     
                        DlkLogger.LogInfo("SetItem() successfully executed.");
                        return;
                     }                       
                    }
                }                          
               
            catch (Exception e)
            {
                throw new Exception("SetItem() failed : " + e.Message, e);
            }
        }

        [Keyword("EditItemAtIndex", new String[] { "1|text|Index|1",
                                                   "2|text|NewValue|NewValueofItem"})]
        public void EditItemAtIndex(String Index, String NewValue)
        {
            try
            {
                Initialize();
                IWebElement target = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/input[1]"));
                new DlkBaseControl("TargetItem", target).ScrollIntoViewUsingJavaScript();
                target.Click(); // not clicking causes validation error Is this a bug in product?
                target.Clear();
                target.SendKeys(NewValue);
                target.SendKeys(Keys.Tab);
                DlkLogger.LogInfo("EditItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("EditItemAtIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("DeleteItem", new String[] { "1|text|Value|ItemToDelete" })]
        public void DeleteItem(String Value)
        {
            try
            {
                Initialize();
                // needed to loop since value is not visible in DOM for newly added items
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::input")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == Value)
                    {
                        ctl.Click();
                        IWebElement target = ctl.mElement.FindElement(By.XPath("./following-sibling::div[1]"));
                        target.Click();
                        DlkLogger.LogInfo("DeleteItem() successfully executed.");
                        return;
                    }
                }
                //if that didn't work, try this
              //  foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::span")))
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::div")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == Value)
                    {
                        ctl.Click();
                        IWebElement target = ctl.mElement.FindElement(By.XPath("./following-sibling::span[2]"));
                        target.Click();
                        DlkLogger.LogInfo("DeleteItem() successfully executed.");
                        return;
                    }
                }
                    //if that didn't work, try this                   
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::div")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        if (ctl.GetValue() == Value)
                        {
                            ctl.MouseOver();
                            IWebElement target = ctl.mElement.FindElement(By.XPath("./following-sibling::span[2]"));
                            target.Click();
                            DlkLogger.LogInfo("DeleteItem() successfully executed.");
                            return;
                        }
                    }
                throw new Exception("Item not found in list.");
            }
            catch (Exception e)
            {
                throw new Exception("DeleteItem() failed : " + e.Message, e);
            }
        }

        [Keyword("DeleteItemAtIndex", new String[] { "1|text|Index|IndexOfItemToDelete" })]
        public void DeleteItemAtIndex(String Index)
        {
            try
            {
                Initialize();
                IWebElement inputField = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]"));
                new DlkBaseControl("TargetItem", inputField).Click();
                IWebElement target = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/div[1]"));
                target.Click();
                DlkLogger.LogInfo("DeleteItemAtIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("DeleteItemAtIndex() failed : " + e.Message, e);
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
                // needed to loop since value is not visible in DOM for newly added items
                bool actual = false;
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::input")))
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
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::input")))
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
                IWebElement inputField = mElement.FindElement(By.XPath("./ul[1]/li[" + Index + "]/input[1]"));
                DlkAssert.AssertEqual("VerifyValueAtIndex()", Value, new DlkBaseControl("TargetItem", inputField).GetValue());
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
                int actualCount = mElement.FindElements(By.XPath("./ul[1]/li")).Count - 1;
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
