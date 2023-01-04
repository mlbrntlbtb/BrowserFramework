using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ngCRMLib.DlkControls
{
    [ControlType("Multiselect")]
    public class DlkMultiselect : DlkBaseControl
    {
        public DlkMultiselect(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMultiselect(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMultiselect(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
   
        public void Initialize()
        {
            FindElement();
            
            String mClass = this.mElement.GetAttribute("class");    //class="navigator_ngcrm_search_list multiselect fluid"      
        }

        
        [Keyword("SetItem", new String[] { "1|text|Value"})]
        public void SetItem(String Value)
        {
            try
            {
                Initialize();
                
                foreach (IWebElement elm in mElement.FindElements(By.TagName("input")))
                {
                    if (elm.Displayed)
                    {
                        DlkTextBox txtInput = new DlkTextBox("Input", elm);
                        txtInput.ShowAutoComplete(Value);                   
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

        [Keyword("SelectItem", new String[] { "1|text|Value" })]
        public void SelectItem(String Value)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Value)) throw new Exception("Value must not be empty");

                Initialize();
                Thread.Sleep(5000);
                DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "result-items");
                IWebElement itm = null;
                try
                {
                    if (!list.Exists(1))
                    {
                        IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[@class='tap-target']"));
                        DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.Click();
                    }

                    list.FindElement();

                    itm = list.mElement.FindElement(By.XPath("./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + Value + "')]"));
                    Thread.Sleep(5000);
                }
                catch //the search item is not visible
                {
                    foreach (IWebElement elm in mElement.FindElements(By.TagName("input")))
                    {
                        if (elm.Displayed)
                        {
                            DlkTextBox txtInput = new DlkTextBox("Input", elm);
                            txtInput.Set(Value);
                            Thread.Sleep(3000);
                            list.FindElement();
                            itm = list.mElement.FindElement(By.XPath("./descendant::li[contains(@class,'search-result')]//div[contains(text(),'" + Value + "')]"));
                            break;
                        }
                    }

                }
                if (itm != null)
                {
                    DlkBaseControl ctlItem = new DlkBaseControl("Item", itm);
                    ctlItem.Click();
                }
                else
                {
                    throw new Exception("Item not found in list.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectItem() failed : " + e.Message, e);
            }
        }

        [Keyword("DeleteItem", new String[] { "1|text|Value|ItemToDelete" })]
        public void DeleteItem(String Value)
        {
            try
            {
                Initialize();
                foreach (IWebElement elm in mElement.FindElements(By.XPath(".//div[contains(@class,'tagstext')]")))
                {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        if (ctl.GetValue().Trim() == Value)
                        {                           
                            IWebElement target = ctl.mElement.FindElement(By.XPath("./following-sibling::a[1]"));
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

        [Keyword("VerifyItemSelected", new String[] { "1|text|Item|ItemToFind",
                                                         "2|text|ExpectedValue|TRUE"})]
        public void VerifyItemSelected(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool actual = false;
                foreach (IWebElement elm in mElement.FindElements(By.XPath(".//div[contains(@class,'tagstext')]")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue().Trim() == Item)
                    {
                        actual = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyItemSelected()", bool.Parse(TrueOrFalse), actual);
                DlkLogger.LogInfo("VerifyItemSelected() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemSelected() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyTooltipSelectedItem", new String[] { "1|text|Item|SelectedItem",
                                                         "2|text|ExpectedValue|Tooltip"})]
        public void VerifyTooltipSelectedItem(String Item, String Tooltip)
        {
            try
            {
                Initialize();
                string ActToolTip = "";
                foreach (IWebElement elm in mElement.FindElements(By.XPath(".//div[@class='tagstext']")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue().Trim() == Item)
                    {
                        ActToolTip = ctl.GetAttributeValue("title");
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyTooltipSelectedItem()", Tooltip, ActToolTip);
                DlkLogger.LogInfo("VerifyTooltipSelectedItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTooltipSelectedItem() failed : " + e.Message, e);
            }

        }


        [Keyword("VerifyList", new String[] { "1|text|Items|Item1~Item2~Item3" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();
                string actual = "";
                
                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[@class='tap-target']"));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();
                
                //DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "result-items");
                DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "results");

                list.FindElement();
                if(list.mElement.FindElements(By.XPath("./descendant::li")).Count > 0){
                    //verify result-items
                    foreach (IWebElement elm in list.mElement.FindElements(By.XPath("./descendant::li")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        actual += ctl.GetValue() + "~";
                    }
                }
                else
                {
                    //verify text
                    foreach (IWebElement div in list.mElement.FindElements(By.XPath("./descendant::div[not(contains(@style,'none'))]")))
                    {
                        foreach (IWebElement elm in div.FindElements(By.XPath("./descendant::p")))
                        {
                            DlkBaseControl ctl = new DlkBaseControl("element", elm);
                            actual += ctl.GetValue() + "~";
                        }
                    }
                }
                
                DlkAssert.AssertEqual("VerifyList()", Items, actual.Trim('~'));
                DlkLogger.LogInfo("VerifyList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySearchedList", new String[] { "1|text|Value|SearchedItem", 
                                                        "2|text|Items|Item1~Item2~Item3" })]
        public void VerifySearchedList(String SearchedValue, String Items)
        {
            try
            {
                Initialize();
                string actual = "";
                foreach (IWebElement elm in mElement.FindElements(By.TagName("input")))
                {
                    if (elm.Displayed)
                    {
                        DlkTextBox txtInput = new DlkTextBox("Input", elm);
                        txtInput.Set(SearchedValue);                                           
                     }                       
                    }                   
  
                DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "results");

                list.FindElement();
                if (list.mElement.FindElements(By.XPath("./descendant::li")).Count > 0)
                {
                    //verify result-items
                    foreach (IWebElement elm in list.mElement.FindElements(By.XPath("./descendant::li")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        actual += ctl.GetValue() + "~";
                    }
                }
                else
                {
                    //verify text
                    foreach (IWebElement div in list.mElement.FindElements(By.XPath("./descendant::div[not(contains(@style,'none'))]")))
                    {
                        foreach (IWebElement elm in div.FindElements(By.XPath("./descendant::p")))
                        {
                            DlkBaseControl ctl = new DlkBaseControl("element", elm);
                            actual += ctl.GetValue() + "~";
                        }
                    }
                }

                DlkAssert.AssertEqual("VerifySearchedList()", Items, actual.Trim('~'));
              
                DlkLogger.LogInfo("VerifySearchedList() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImageExists", new String[] { "1|text|Item",
                                                        "2|text|ExpectedValue|TRUE"})]  //verify if icon exists
        public void VerifyItemImageExists(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool actual = false;
                string icon = "";

                IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[@class='tap-target']"));
                DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                ctlArrowDown.Click();

                DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "results");

                list.FindElement();
                if (list.mElement.FindElements(By.XPath("./descendant::li")).Count > 0)
                {
                    foreach (IWebElement elm in list.mElement.FindElements(By.XPath("./descendant::li")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        if (ctl.GetValue() == Item)
                        {
                            icon = elm.FindElement(By.TagName("a")).GetAttribute("style");
                            if (icon != "") actual = true;
                            break;
                        }                  
                    }
                }
                DlkAssert.AssertEqual("VerifyItemSelected()", bool.Parse(TrueOrFalse), actual);
                DlkLogger.LogInfo("VerifyItemImageExists() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImageDimmed", new String[] { "1|text|Item",
                                                        "2|text|ExpectedValue|TRUE"})]  //verify if icon exists
        public void VerifyItemImageDimmed(String Item, String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool ActualValue;               

                bool ExpectedValue = false;
                if (Boolean.TryParse(TrueOrFalse, out ExpectedValue))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[@class='tap-target']"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();

                    DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "results");

                    list.FindElement();
                    if (list.mElement.FindElements(By.XPath("./descendant::li")).Count > 0)
                    {
                        foreach (IWebElement elm in list.mElement.FindElements(By.XPath("./descendant::li")))
                        {
                            DlkBaseControl ctl = new DlkBaseControl("element", elm);
                            if (ctl.GetValue() == Item)
                            {
                                string listItemClass = elm.GetAttribute("class");
                                if (listItemClass.ToLower().Contains("dimmed"))
                                    ActualValue = true;
                                else
                                    ActualValue = false;

                                DlkAssert.AssertEqual("VerifyItemImageDimmed()", ExpectedValue, ActualValue);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("VerifyItemImageDimmed() : Unable to find the contents of the list.");
                    }
                    DlkLogger.LogInfo("VerifyItemImageDimmed() successfully executed.");
                }
                else
                {
                    throw new Exception("VerifyItemImageDimmed() : Invalid input [" + TrueOrFalse + "]");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageDimmed() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemImageTooltip", new String[] { "1|text|Item",
                                                        "2|text|ExpectedValue|TRUE",
                                                        "2|text|ExpectedValue|TRUE"})]  //verify if icon exists
        public void VerifyItemImageTooltip(String Item, String ExpectedValue, String TrueOrFalse)
        {
            try
            {
                Initialize();
                string ActualValue;
                
                foreach (IWebElement elm in mElement.FindElements(By.TagName("input")))
                {
                    if (elm.Displayed)
                    {
                        DlkTextBox txtInput = new DlkTextBox("Input", elm);
                        
                        elm.Clear();
                        elm.SendKeys(Item);
                        Thread.Sleep(3000);
                        DlkLogger.LogInfo("VerifyItemImageTooltip() : search item successfully set.");
                        break;
                     }                       
                    
                }   

                    DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "results");

                    list.FindElement();
                    if (list.mElement.FindElements(By.XPath("./descendant::li")).Count > 0)
                    {
                        foreach (IWebElement elm in list.mElement.FindElements(By.XPath("./descendant::li")))
                        {
                            DlkBaseControl ctl = new DlkBaseControl("element", elm);
                            if (ctl.GetValue() == Item)
                            {
                                ctl.MouseOverOffset(0, 0);
                                string listItemTooltip = elm.GetAttribute("title");
                                ActualValue = listItemTooltip;

                                DlkAssert.AssertEqual("VerifyItemImageTooltip()", ExpectedValue, ActualValue);
                                break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("VerifyItemImageTooltip() : Unable to find the contents of the list.");
                    }
                    DlkLogger.LogInfo("VerifyItemImageTooltip() successfully executed.");
               
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageTooltip() failed : " + e.Message, e);
            }
        }
       
    //    [Keyword("VerifyItemCount", new String[] { "1|text|Count|10" })]
    //    public void VerifyItemCount(String Count)
    //    {
    //        try
    //        {
    //            Initialize();
    //            int actualCount = mElement.FindElements(By.XPath("./ul[1]/li")).Count - 1;
    //            if(actualCount < 0)
    //            {
    //                actualCount = 0;
    //            }
    //            DlkAssert.AssertEqual("VerifyItemCount()", int.Parse(Count), actualCount);
    //            DlkLogger.LogInfo("VerifyItemCount() successfully executed.");
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception("VerifyItemCount() failed : " + e.Message, e);
    //        }
    //    }
    }
}
