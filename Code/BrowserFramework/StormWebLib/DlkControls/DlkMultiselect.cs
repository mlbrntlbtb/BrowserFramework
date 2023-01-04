using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StormWebLib.System;
using CommonLib.DlkUtility;


namespace StormWebLib.DlkControls
{
    [ControlType("Multiselect")]
    public class DlkMultiselect : DlkBaseControl
    {
        #region  Constants

        private const String spSelections = ".//li[contains(@class,'special-selections')]"; //special selections
        private const String spOperators = ".//*[@class='special-operators']/li"; //special operators
        private const String mArrowDownPath = ".//span[@class='tap-target']";
        
        #endregion

        #region Constructors
        public DlkMultiselect(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMultiselect(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMultiselect(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Public Functions
        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();            
            
            String mClass = this.mElement.GetAttribute("class");    //class="navigator_ngcrm_search_list multiselect fluid"      
        }
        #endregion

        #region Private Functions
        private void SetValue(IWebElement element, string value)
        {
            element.SendKeys(value);
            Thread.Sleep(3000);
            DlkLogger.LogInfo("Successfully entered value [" + value + "]");
        }

        private void DisplayUndisplayDropDown()
        {
            DlkButton mArrowDown = null;
            if (mElement.FindElements(By.XPath(mArrowDownPath)).Count > 0)
            {
                mArrowDown = new DlkButton("Button", mElement.FindElement(By.XPath(mArrowDownPath)));
            }
            else
            {
                mArrowDown = new DlkButton("Button", mElement.FindElement(By.XPath(".//following-sibling::span[@class='tap-target']")));
            }

            if (mArrowDown.Exists(1))
            {
                try
                {
                    mArrowDown.ClickUsingJavaScript();
                }
                catch
                {
                    DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                    if (!list.Exists(1))
                    {
                        mArrowDown.Click();
                    }
                }
                Thread.Sleep(2000);
            }
        }

        private void ClickSpecialOptions(IWebElement specialSelectionElement)
        {
            if (specialSelectionElement != null)
            {
                DlkBaseControl specialSelection = new DlkBaseControl("Button", specialSelectionElement);
                if (specialSelection.Exists(1))
                {
                    specialSelection.Click();
                    Thread.Sleep(5000);
                }
            }
        }

        #endregion

        #region Keywords
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                base.Click();
                DlkLogger.LogInfo("Click() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        //Sets text only. No enter or tab behavior
        [Keyword("SetText", new String[] { "1|text|Value" })]
        public void SetText(String Value)
        {
            try
            {
                Initialize();
                string multiselect_xpath = ".//descendant::input[contains(@class,'multiselect')]";
                string textbox_xpath = ".//descendant::input";
                IWebElement txtbox = null;

                if (mElement.FindElements(By.XPath(multiselect_xpath)).Count > 0)
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath(multiselect_xpath)))
                    {
                        if (elm.Displayed)
                        {
                            txtbox = elm;
                            break;
                        }
                    }
                }
                else if (mElement.FindElements(By.XPath(textbox_xpath)).Count > 0)
                {
                    txtbox = mElement.FindElement(By.XPath(textbox_xpath));
                }
                else
                {
                    throw new Exception("SetText() : Unable to find the input control.");
                }

                SetValue(txtbox, Value);
                DlkLogger.LogInfo("SetItem() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetText() failed : " + e.Message, e);
            }
        }

        [Keyword("SetItem", new String[] { "1|text|Value"})]
        public void SetItem(String Value)
        {
            try
            {
                Initialize();

                string multiselect_xpath = ".//descendant::input[contains(@class,'multiselect')]";
                string textbox_xpath = ".//descendant::input";

                if (mElement.FindElements(By.XPath(multiselect_xpath)).Count > 0)
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath(multiselect_xpath)))
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
                else if (mElement.FindElements(By.XPath(textbox_xpath)).Count > 0)
                {
                    IWebElement txtbox = mElement.FindElement(By.XPath(textbox_xpath));
                    SetValue(txtbox, Value);                    
                }
                else
                {
                    throw new Exception("SetItem() : Unable to find the input control.");
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
                Initialize();
                Thread.Sleep(5000);
                DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "result-items");
                
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = mElement.FindElements(By.XPath(".//span[@class='tap-target']")).Count > 0 ? 
                        mElement.FindElement(By.XPath(".//span[@class='tap-target']")) :
                        mElement.FindElement(By.XPath(".//following-sibling::*[contains(@class,'tap-target')]"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                }

                list.FindElement();
                IWebElement itm = null;
                try
                {
                    itm = list.mElement.FindElement(By.XPath("./descendant::li[contains(@class,'search-result')]//div[normalize-space(text()) = '" + Value + "']"));
                    Thread.Sleep(5000);
                }
                catch //the search item is not visible
                {
                    foreach (IWebElement elm in mElement.FindElements(By.TagName("input")))
                        {
                            if (elm.Displayed)
                            {
                                DlkTextBox txtInput = new DlkTextBox("Input", elm);
                                txtInput.SetTextOnly(Value);
                                Thread.Sleep(3000);
                                var searchValue = "./descendant::li[contains(@class,'search-result')]//div[normalize-space(text()) = '" + Value + "']";
                                var searchPartialValues = "./descendant::li[contains(@class,'search-result')]//*[contains(text(),'" + Value + "')]";
                                itm = list.mElement.FindWebElementCoalesce(By.XPath(searchValue), By.XPath(searchPartialValues));
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

        [Keyword("VerifyListItem", new String[]
         {
             "1|text|SearchedItem|Posted",
             "2|text|Items|1/8/2014~Posted~Posted",
             "3|text|ImageExists|TRUE"
         })]
        public void VerifyListItem(String SearchedItem, String Items, String ImageExists)
        {
            try
            {
                Initialize();
                IWebElement mInput = this.mElement.FindElement(By.XPath(".//descendant::input[contains(@class,'multiselect')]"));
                DlkTextBox txtInput = new DlkTextBox("Input", mInput);
                txtInput.SetTextOnly(SearchedItem);
                Thread.Sleep(3000);

                DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "result-items");
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[@class='tap-target']"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                }

                list.FindElement();
                DlkAssert.AssertEqual("VerifyListItem", Items, list.GetValue().Replace("\r\n", "~"));
     
                try
                {
                    IWebElement mImage = list.mElement.FindElement(By.TagName("img"));
                    DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists), mImage.Displayed);
                }
                catch
                {
                    DlkAssert.AssertEqual("VerifyImageDisplayed", Convert.ToBoolean(ImageExists), false);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyListItem() failed: " + ex.Message);
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

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "result-items");
                if (!list.Exists(1))
                    DisplayUndisplayDropDown();

                list.FindElement();
                if (list.mElement.FindElements(By.XPath(".//li")).Count > 0)
                {
                    //verify result-items
                    foreach (IWebElement elm in list.mElement.FindElements(By.XPath(".//li")).Where(elm => elm.Displayed))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        actual += DlkString.RemoveCarriageReturn(ctl.GetValue().Trim()) + "~";
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

                //close list
                if (list.Exists(1))
                    mElement.Click();
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
                //filter operational fields in selected multiselect controls
                foreach (IWebElement elm in mElement.FindElements(By.XPath(".//input[not(contains(@class,'operator'))]")))
                {
                    if (elm.Displayed)
                    {
                        DlkTextBox txtInput = new DlkTextBox("Input", elm);
                        txtInput.SetTextOnly(SearchedValue);  
                    }                       
                }                   
  
                DlkBaseControl list = new DlkBaseControl("Multiselect", "CLASS_DISPLAY", "results");

                list.FindElement();
                if (list.mElement.FindElements(By.XPath(".//*[contains(@class,'search-name')]")).Count > 0)
                {
                    //verify result-items
                    foreach (IWebElement elm in list.mElement.FindElements(By.XPath(".//*[contains(@class,'search-name')]")))
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

        [Keyword("VerifySearchListNoResults", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySearchListNoResults(String SearchText, String TrueOrFalse)
        {
            try
            {
                Boolean expected = false;
                String noResultsExpectedString = "no results were found";
                // guard clause
                if (!Boolean.TryParse(TrueOrFalse, out expected)) throw new ArgumentException("TrueOrFalse must be a Boolean value");
                Initialize();

                //set text in multiselect
                string multiselect_xpath = ".//descendant::input[contains(@class,'multiselect')]";
                string textbox_xpath = ".//descendant::input";

                if (mElement.FindElements(By.XPath(multiselect_xpath)).Count() > 0)
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath(multiselect_xpath)).Where(x => x.Displayed))
                    {
                            DlkTextBox txtInput = new DlkTextBox("Input", elm);
                            txtInput.SetTextOnly(SearchText);
                    }
                }
                else if (mElement.FindElements(By.XPath(textbox_xpath)).Count > 0)
                {
                    IWebElement txtbox = mElement.FindElement(By.XPath(textbox_xpath));
                    SetValue(txtbox, SearchText);
                }

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                if (!list.Exists(1))
                {
                    IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//span[@class='tap-target']"));
                    DlkBaseControl ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                    ctlArrowDown.Click();
                    Thread.Sleep(5000);
                }

                list.FindElement();
                var noResultElement = list.mElement.FindElement(By.XPath(".//div[@class='no-results']"));
                // check if displayed
                DlkAssert.AssertEqual("Check if no results were found", expected, noResultElement.Displayed & noResultElement.Text.ToLower().Trim().Equals(noResultsExpectedString));
            }
            catch (NoSuchElementException ex)
            {
                throw new Exception(String.Format("VerifySearchListNoResults() failed. Unable to find no results class. See error message:\n{0} ", ex.Message));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("VerifySearchListNoResults() failed. {0}", ex.Message));
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

                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "result-items");
                if (!list.Exists(1))
                    DisplayUndisplayDropDown();
               
                list.FindElement();
                if (list.mElement.FindElements(By.XPath(".//li")).Count > 0)
                {
                    foreach (IWebElement elm in list.mElement.FindElements(By.XPath(".//li")))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        if (DlkString.RemoveCarriageReturn(ctl.GetValue().Trim()) == Item)
                        {
                            icon = elm.FindElement(By.TagName("a")).GetAttribute("style");
                            if (icon != "") actual = true;
                            break;
                        }                  
                    }
                }
                DlkAssert.AssertEqual("VerifyItemImageExists()", bool.Parse(TrueOrFalse), actual);
                DlkLogger.LogInfo("VerifyItemImageExists() successfully executed.");

                //close list
                if (list.Exists(1))
                    mElement.Click();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemImageExists() failed : " + e.Message, e);
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
                            if (DlkString.RemoveCarriageReturn(ctl.GetValue().Trim()) == Item)
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

        [Keyword("ClickMultiselectButton", new String[] { "1|text|Caption|Edit" })]
        public void ClickMultiselectButton(string Caption = null)
        {
            try
            {
                Initialize();
                IWebElement mBttn = null;

                switch (Caption.ToLower())
                {
                    case "clear":
                        mBttn = mElement.FindElement(By.XPath(".//span[contains(@class,'clear-icon')]"));
                        break;
                    case "search":
                        mBttn = mElement.FindElement(By.XPath(".//span[contains(@class,'tap-target')]"));
                        break;
                    case "":
                        mBttn = mElement.FindElement(By.XPath(".//*[@class='tap-target']"));
                        break;
                    default:
                        mBttn = mElement.FindElement(By.XPath(".//*[@title='" + Caption + "']"));
                        break;
                }
               
                mBttn.Click();
                DlkLogger.LogInfo("ClickMultiselectButton() successfully executed.");

            }
            catch( Exception e)
            {
                throw new Exception("ClickMultiselectButton() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySpecialSelectionsExist", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySpecialSelectionsExist(String TrueOrFalse)
        {
            try
            {

                Initialize();
                bool expected = Convert.ToBoolean(TrueOrFalse);

                //open list
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                if (!list.Exists(1))
                    DisplayUndisplayDropDown();

                list.FindElement();
                var specialSelectionElement = list.mElement.FindElement(By.XPath(spSelections));
                bool actual = specialSelectionElement.Displayed && specialSelectionElement != null;
                
                DlkAssert.AssertEqual("VerifySpecialSelectionsExists()", expected, actual);
                DlkLogger.LogInfo("VerifySpecialSelectionsExists() passed");

                //close list
                if (list.Exists(1))
                    mElement.Click();
            }
            catch (Exception e)
            {
                throw new Exception("VerifySpecialSelectionsExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySpecialSelectionsList", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifySpecialSelectionsList(String Items)
        {
            try
            {
                Initialize();
                string actual = "";

                //display dropdownlist if not existing
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                if (!list.Exists(1))
                {
                    DisplayUndisplayDropDown();
                }
                list.FindElement();

                //click special selections option
                ClickSpecialOptions(list.mElement.FindElement(By.XPath(spSelections)));

                //get special selector items and compare with expected items
                List<IWebElement> specialOperators = list.mElement.FindElements(By.XPath(spOperators)).ToList();
                foreach (IWebElement elm in specialOperators)
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    actual += ctl.GetValue() + "~";
                }

                DlkAssert.AssertEqual("VerifySpecialSelectionsList()", Items, actual.Trim('~'));
                DlkLogger.LogInfo("VerifySpecialSelectionsList() passed");

                //close list
                if (list.Exists(1))
                    mElement.Click();
            }
            catch (Exception e)
            {
                throw new Exception("VerifySpecialSelectionsList() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectSpecialSelectionsItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectSpecialSelectionsItem(String Item)
        {
            try
            {
                Initialize();
                bool bFound = false;

                //display dropdownlist if not existing
                DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "results");
                if (!list.Exists(1))
                {
                    DisplayUndisplayDropDown();
                }
                list.FindElement();

                //click special selections option
                ClickSpecialOptions(list.mElement.FindElement(By.XPath(spSelections)));

                //get special selector items and click desired item if exists
                List<IWebElement> specialOperators = list.mElement.FindElements(By.XPath(spOperators)).ToList();
                foreach (IWebElement elm in specialOperators)
                {
                    DlkBaseControl ctl = new DlkBaseControl("element", elm);
                    if (ctl.GetValue() == Item)
                    {
                        bFound = true;
                        ctl.Click();
                        DlkLogger.LogInfo("SelectSpecialSelectionsItem() passed");
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception(Item + " not found in the Special Selections List.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectSpecialSelectionsItem() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyStartsWithContains", new String[] { "1|text|Caption|Edit" })]
        public void VerifyStartsWithContains(string Items)
        {
            try
            {
                Initialize();
                string actual = "";
                List<IWebElement> tagList = mElement.FindElements(By.XPath(".//div[contains(@class,'tagstext')]")).ToList();
                foreach (IWebElement elm in tagList)
                {
                    var className = elm.GetAttribute("class");
                    if (className.Contains("starts-with"))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        actual += "STARTS WITH " + ctl.GetValue().Trim() + "~";
                    }
                    else if (className.Contains("contains"))
                    {
                        DlkBaseControl ctl = new DlkBaseControl("element", elm);
                        actual += "CONTAINS " + ctl.GetValue().Trim() + "~";
                    }
                }
                DlkAssert.AssertEqual("VerifyStartsWithContains()", Items, actual.Trim('~'));
                DlkLogger.LogInfo("VerifyStartsWithContains() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyStartsWithContains() failed : " + e.Message, e);
            }
        }
        

        #endregion
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
