using System;
using System.Linq;
using System.Threading;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("TextBox")]
    public class DlkTextBox : DlkBaseControl
    {

        #region CONSTRUCTORS

        public DlkTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        //public DlkTextBox(String ControlName, DlkControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        #endregion

        #region PRIVATE METHODS

        private void SetText(String sTextToEnter)
        {
            switch (DlkEnvironment.mBrowser.ToLower())
            {
                case "safari":
                    mElement.Clear();
                    mElement.SendKeys(sTextToEnter);
                    break;
                default:
                    mElement.Clear();
                    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    mAction.SendKeys(sTextToEnter).Build().Perform();
                    break;
            }
        }

        private void InputConcurrentKey(string specialKey, string concurrentKey, IWebElement input)
        {
            switch (specialKey.ToLower())
            {
                case "ctrl":
                    if (concurrentKey.ToLower().Equals("tab"))
                        input.SendKeys(Keys.Control + Keys.Tab);
                    else
                        input.SendKeys(Keys.Control + concurrentKey.ToLower());
                    break;
                case "shift":
                    if (concurrentKey.ToLower().Equals("tab"))
                        input.SendKeys(Keys.Shift + Keys.Tab);
                    else
                        input.SendKeys(Keys.Shift + concurrentKey.ToLower());
                    break;
                case "alt":
                    if (concurrentKey.ToLower().Equals("tab"))
                        input.SendKeys(Keys.Alt + Keys.Tab);
                    else
                        input.SendKeys(Keys.Alt + concurrentKey.ToLower());
                    break;
                default:
                    throw new Exception("Special key not supported");
            }
        }

        #endregion

        #region KEYWORDS

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();

                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndEnter", new String[] { "1|text|Value|SampleValue" })]
        public void SetAndEnter(String TextToEnter)
        {
            try
            {
                Initialize();

                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                mElement.SendKeys(Keys.Return);
                DlkLogger.LogInfo("Successfully executed SetAndEnter()");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyTextContains(String TextToEnter)
        {
            try
            {
                String ActValue = GetAttributeValue("value");
                DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, ActValue.ToLower().Contains(TextToEnter.ToLower()));

            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("SetThenSelectFromList", new String[] { "1|text|Value|SampleValue" })]
        public void SetThenSelectFromList(String TextToEnter)
        {
            try
            {
                Initialize();

                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[@class='row']")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }
                    bool bFound = false;
                    foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        if (actualItem == DlkString.ReplaceCarriageReturn(TextToEnter, ""))
                        {
                            new DlkBaseControl("Item", elm).Click();
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        throw new Exception("Item " + TextToEnter + " not found on list");
                    }
                }
                DlkLogger.LogInfo("Successfully executed SetThenSelectFromList()");
            }
            catch (Exception e)
            {
                throw new Exception("SetThenSelectFromList() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectFromList", new String[] { "1|text|Value|SampleValue" })]
        public void SelectFromList(String TextToEnter)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[@class='row']")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }
                    bool bFound = false;
                    foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        if (actualItem == DlkString.ReplaceCarriageReturn(TextToEnter, ""))
                        {
                            new DlkBaseControl("Item", elm).Click();
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        throw new Exception("Item " + TextToEnter + " not found on list");
                    }

                }
                DlkLogger.LogInfo("Successfully executed SelectFromList()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectFromList() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectFromListContains", new String[] { "1|text|Value|SampleValue" })]
        public void SelectFromListContains(String TextToEnter)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }
                    bool bFound = false;
                    foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        if (actualItem.Contains(DlkString.ReplaceCarriageReturn(TextToEnter, "")))
                        {
                            new DlkBaseControl("Item", elm).Click();
                            bFound = true;
                            break;
                        }
                    }
                    if (!bFound)
                    {
                        throw new Exception("Item " + TextToEnter + " not found on list");
                    }

                }
                DlkLogger.LogInfo("Successfully executed SelectFromListContains()");
            }
            catch (Exception e)
            {
                throw new Exception("SelectFromListContains() failed : " + e.Message, e);
            }
        }

        [Keyword("SetThenSelectListItem", new String[] { "1|text|Value|SampleValue" })]
        public void SetThenSelectListItem(String TextToEnter, String LineIndex)
        {
            try
            {
                Initialize();

                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[@class='row']")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }

                    IWebElement item = resultList.FindElement(By.XPath("./*[@class='result']/a[" + LineIndex + "]/div[1]"));
                    new DlkBaseControl("Item", item).Click();
                }
                DlkLogger.LogInfo("Successfully executed SetThenSelectListItem()");
            }
            catch (Exception e)
            {
                throw new Exception("SetThenSelectListItem() failed : " + e.Message, e);
            }
        }

        [Keyword("SetThenVerifyAvailableInList", new String[] { "1|text|Value|SampleValue" })]
        public void SetThenVerifyAvailableInList(String TextToEnter, String TrueOrFalse)
        {
            try
            {
                Initialize();

                mElement.Clear();
                mElement.SendKeys(TextToEnter);
                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;

                    while (resultList.FindElements(By.XPath("./descendant::p[1]")).Count == 0 && count < 10)
                    {
                        Thread.Sleep(1000);
                        DlkLogger.LogInfo("Waiting for result list... " + ++count + "s");
                    }

                    DlkAssert.AssertEqual("SetThenVerifyAvailableInList()", bool.Parse(TrueOrFalse), resultList.FindElements(By.XPath("./descendant::p[1]")).Count > 0);

                }
                DlkLogger.LogInfo("Successfully executed SetThenVerifyAvailableInList()");
            }
            catch (Exception e)
            {
                throw new Exception("SetThenVerifyAvailableInList() failed : " + e.Message, e);
            }
            finally
            {
                // try to close dropdown
                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    resultList.Click();
                }
            }
        }

        [Keyword("VerifyAvailableInList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyAvailableInList(String TextToEnter, String TrueOrFalse)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;
                bool expected = bool.Parse(TrueOrFalse);
                bool actual = false;
                bool IsOpenedHere = false;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                    IsOpenedHere = true;
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }
                    foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        if (actualItem.ToLower() == DlkString.ReplaceCarriageReturn(TextToEnter.ToLower(), ""))
                        {
                            new DlkBaseControl("Item", elm).Click();
                            actual = true;
                            break;
                        }
                    }
                    if (IsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing list");
                        DropDownButton.Click();
                    }
                    DlkAssert.AssertEqual("VerifyAvailableInList()", expected, actual);
                    DlkLogger.LogInfo("VerifyAvailableInList() passed");
                }
                DlkLogger.LogInfo("Successfully executed VerifyAvailableInList()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartiallyAvailableInList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyPartiallyAvailableInList(String TextToEnter, String TrueOrFalse)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;
                bool expected = bool.Parse(TrueOrFalse);
                bool actual = false;
                bool IsOpenedHere = false;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                    IsOpenedHere = true;
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }
                    foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        if (actualItem.ToLower().Contains(DlkString.ReplaceCarriageReturn(TextToEnter.ToLower(), "")))
                        {
                            new DlkBaseControl("Item", elm).Click();
                            actual = true;
                            break;
                        }
                    }
                    if (IsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing list");
                        DropDownButton.Click();
                    }
                    DlkAssert.AssertEqual("VerifyPartiallyAvailableInList()", expected, actual);
                    DlkLogger.LogInfo("VerifyPartiallyAvailableInList() passed");
                }
                DlkLogger.LogInfo("Successfully executed VerifyPartiallyAvailableInList()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyList(String Items)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;
                bool IsOpenedHere = false;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                    IsOpenedHere = true;
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    string[] expected = Items.Split('~');
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    ReadOnlyCollection<IWebElement> actual = resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]"));

                    int count = 0;
                    while (actual.Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }

                    int expectedCount = expected.Count();
                    int actualCount = actual.Count;
                    // check for count equality
                    if (actualCount != expectedCount)
                    {
                        throw new Exception("Actual count: " + actualCount + " did not match Expected count: " + expectedCount);
                    }

                    for (int i = 0; i < expected.Count(); i++)
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in actual[i].FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        DlkBaseControl ctl = new DlkBaseControl("ActualControl", actual[i]);
                        DlkAssert.AssertEqual("VerifyList()", DlkString.ReplaceCarriageReturn(expected[i].ToLower(), ""), actualItem.ToLower());
                    }
                    if (IsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing list");
                        DropDownButton.Click();
                    }
                    DlkLogger.LogInfo("VerifyList() passed");
                }
                DlkLogger.LogInfo("Successfully executed VerifyList()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyText(String ExpectedValue)
        {
            String ActValue = GetAttributeValue("value");
            DlkAssert.AssertEqual("VerifyText()", ExpectedValue.ToLower(), ActValue.ToLower());            
        }

        /// <summary>
        /// This keyword will get the value of the placeholder attribute of a TextBox control.
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyPlaceholder", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyPlaceholder(String ExpectedValue)
        {
            //i considered this but it might have unintended results
            //if (ActValue=="")
            //{
            //    ActValue = base.GetValue();
            //}
            String ActValue = GetAttributeValue("placeholder");
            DlkAssert.AssertEqual("VerifyPlaceholder()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        //break pt here
        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            String ActValue = IsReadOnly();
            DlkAssert.AssertEqual("VerifyReadOnly()", ExpectedValue.ToLower(), ActValue.ToLower());
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {            
            base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
            DlkLogger.LogInfo("VerifyExists() passed");            
        }

        [Keyword("VerifySelected", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifySelected(String ExpectedValue)
        {
            Initialize();
            //check if current element is the focused element
            var actualValue = mElement.Equals(DlkEnvironment.AutoDriver.SwitchTo().ActiveElement());
            DlkAssert.AssertEqual("VerifyText()", bool.Parse(ExpectedValue), actualValue);
            //switch back to original focus
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
        }

        [Keyword("ClickTextboxButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickTextboxButton(String CalendarOrFind)
        {
            try
            {
                Initialize();
                IWebElement mButton;
                if(CalendarOrFind.Equals("Calendar",StringComparison.OrdinalIgnoreCase)){
                    mButton = mElement.FindElement(By.XPath("./preceding-sibling::a[contains(@class,'fa-calendar')]"));
                }
                else if (CalendarOrFind.Equals("Find", StringComparison.OrdinalIgnoreCase))
                {
                    mButton = mElement.FindElement(By.XPath("./preceding::label[1]/following::*[@class='icon-lookup']"));
                }
                else
                {
                    throw new Exception("ClickTextboxButton() failed : Search parameter");
                }
                mButton.Click();
                DlkLogger.LogInfo("ClickTextboxButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("ClickTextboxButton() failed : " + e.Message, e);
            }
            
        }

        [Keyword("EnterSimultaneousKeys")]
        public void EnterSimultaneousKeys(String CtrlShiftAlt, String ConcurrentKey)
        {
            try
            {
                Initialize();
                InputConcurrentKey(CtrlShiftAlt, ConcurrentKey, mElement);
                DlkLogger.LogInfo("EnterSimultaneousKeys() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("EnterSimultaneousKeys() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactTextContains", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExactTextContains(String TextToEnter)
        {
            try
            {
                String ActValue = GetAttributeValue("value");
                DlkAssert.AssertEqual("VerifyExactTextContains() : " + mControlName, true, ActValue.Contains(TextToEnter));

            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactAvailableInList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExactAvailableInList(String TextToEnter, String TrueOrFalse)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;
                bool expected = bool.Parse(TrueOrFalse);
                bool actual = false;
                bool IsOpenedHere = false;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                    IsOpenedHere = true;
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }
                    foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        if (actualItem == DlkString.ReplaceCarriageReturn(TextToEnter, ""))
                        {
                            new DlkBaseControl("Item", elm).Click();
                            actual = true;
                            break;
                        }
                    }
                    if (IsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing list");
                        DropDownButton.Click();
                    }
                    DlkAssert.AssertEqual("VerifyExactAvailableInList()", expected, actual);
                    DlkLogger.LogInfo("VerifyExactAvailableInList() passed");
                }
                DlkLogger.LogInfo("Successfully executed VerifyExactAvailableInList()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactPartiallyAvailableInList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExactPartiallyAvailableInList(String TextToEnter, String TrueOrFalse)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;
                bool expected = bool.Parse(TrueOrFalse);
                bool actual = false;
                bool IsOpenedHere = false;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                    IsOpenedHere = true;
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    int count = 0;
                    while (resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")).Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }
                    foreach (IWebElement elm in resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]")))
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in elm.FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        if (actualItem.Contains(DlkString.ReplaceCarriageReturn(TextToEnter, "")))
                        {
                            new DlkBaseControl("Item", elm).Click();
                            actual = true;
                            break;
                        }
                    }
                    if (IsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing list");
                        DropDownButton.Click();
                    }
                    DlkAssert.AssertEqual("VerifyExactPartiallyAvailableInList()", expected, actual);
                    DlkLogger.LogInfo("VerifyExactPartiallyAvailableInList() passed");
                }
                DlkLogger.LogInfo("Successfully executed VerifyExactPartiallyAvailableInList()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactPartiallyAvailableInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactList", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExactList(String Items)
        {
            try
            {
                Initialize();

                IWebElement DropDownButton = mElement.FindElement(By.XPath("./preceding::i[@class = 'caret']/.."));

                int attempts = 0;
                bool IsOpenedHere = false;

                while ((mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count <= 0
                    || !mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']"))[0].Displayed)
                    && attempts < 10)
                {
                    DlkLogger.LogInfo("Attempting to display list attempt " + ++attempts + " ...");
                    DropDownButton.Click();
                    IsOpenedHere = true;
                }

                if (mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).Count > 0)
                {
                    string[] expected = Items.Split('~');
                    DlkLogger.LogInfo("Result list detected");

                    IWebElement resultList = mElement.FindElements(By.XPath("./following::*[@class='result-wrapper']")).First();
                    ReadOnlyCollection<IWebElement> actual = resultList.FindElements(By.XPath("./*[@class='result']/a/div[contains(@class,'row')]"));

                    int count = 0;
                    while (actual.Count == 0 && count < 10)
                    {
                        DlkLogger.LogInfo("Waiting for items... " + ++count + "s elapsed");
                        Thread.Sleep(1000);
                    }

                    int expectedCount = expected.Count();
                    int actualCount = actual.Count;
                    // check for count equality
                    if (actualCount != expectedCount)
                    {
                        throw new Exception("Actual count: " + actualCount + " did not match Expected count: " + expectedCount);
                    }

                    for (int i = 0; i < expected.Count(); i++)
                    {
                        string actualItem = "";
                        foreach (IWebElement sub in actual[i].FindElements(By.TagName("p")))
                        {
                            actualItem += DlkString.ReplaceCarriageReturn(new DlkBaseControl("sub", sub).GetValue(), "");
                        }
                        DlkBaseControl ctl = new DlkBaseControl("ActualControl", actual[i]);
                        DlkAssert.AssertEqual("VerifyExactList()", DlkString.ReplaceCarriageReturn(expected[i], ""), actualItem);
                    }
                    if (IsOpenedHere)
                    {
                        DlkLogger.LogInfo("Closing list");
                        DropDownButton.Click();
                    }
                    DlkLogger.LogInfo("VerifyExactList() passed");
                }
                DlkLogger.LogInfo("Successfully executed VerifyExactList()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactText", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactText(String ExpectedValue)
        {
            String ActValue = GetAttributeValue("value");
            DlkAssert.AssertEqual("VerifyExactText()", ExpectedValue, ActValue);
        }

        [Keyword("VerifyExactPlaceholder", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyExactPlaceholder(String ExpectedValue)
        {
            //i considered this but it might have unintended results
            //if (ActValue=="")
            //{
            //    ActValue = base.GetValue();
            //}
            String ActValue = GetAttributeValue("placeholder");
            DlkAssert.AssertEqual("VerifyExactPlaceholder()", ExpectedValue, ActValue);
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

    }
}
