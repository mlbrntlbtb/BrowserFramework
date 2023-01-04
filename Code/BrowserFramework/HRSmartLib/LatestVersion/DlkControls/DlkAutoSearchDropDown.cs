using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("AutoSearchDropDown")]
    public class DlkAutoSearchDropDown : DlkBaseControl
    {
        #region Declarations

        private IWebElement _next = null;
        private IWebElement _previous = null;
        private IWebElement _firstPage = null;
        private IWebElement _lastPage = null;

        #endregion

        #region Constructors

        public DlkAutoSearchDropDown(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                initialize();
                IWebElement dropDownElement = mElement.FindElement(By.XPath(@".//div[@class='input-group']//a"));
                dropDownElement.Click();
                DlkLogger.LogInfo("Click execution passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Click execution failed. :" + ex.Message, ex);
            }
        }

        [Keyword("NextPage")]
        public void NextPage()
        {
            try
            {
                initialize();
                if (_next == null)
                {
                    DlkLogger.LogInfo("NextPage() not available.");
                }
                else
                {
                    _next.Click();
                    DlkLogger.LogInfo("NextPage() execution passed.");
                }
            }
            catch(Exception ex)
            {
                throw new Exception("NextPage() execution failed. :" + ex.Message, ex);
            }
        }

        [Keyword("LastPage")]
        public void LastPage()
        {
            try
            {
                initialize(); 
                if (_lastPage == null)
                {
                    DlkLogger.LogInfo("LastPage() not available.");
                }
                else
                {
                    _lastPage.Click();
                    DlkLogger.LogInfo("LastPage() execution passed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LastPage() execution failed. :" + ex.Message, ex);
            }
        }

        [Keyword("PreviousPage")]
        public void PreviousPage()
        {
            try
            {
                initialize(); 
                if (_previous == null)
                {
                    DlkLogger.LogInfo("PreviousPage() not available.");
                }
                else
                {
                    _previous.Click();
                    DlkLogger.LogInfo("PreviousPage() execution passed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PreviousPage() execution failed. :" + ex.Message, ex);
            }
        }

        [Keyword("FirstPage")]
        public void FirstPage()
        {
            try
            {
                initialize();
                if (_firstPage == null)
                {
                    DlkLogger.LogInfo("FirstPage() not available.");
                }
                else
                {
                    _firstPage.Click();
                    DlkLogger.LogInfo("FirstPage() execution passed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("FirstPage() execution failed. :" + ex.Message, ex);
            }
        }

        [Keyword("Set")]
        public void Set(string Value)
        {
            if (Value.Equals(DlkCommon.DlkCommonFunction.SKIP_TEXTBOX_SET))
            {
                DlkLogger.LogInfo("Skipping set since the data parameter is blank.");
            }
            else
            {
                try
                {
                    initialize();
                    IWebElement inputBox = null;
                    List<IWebElement> inputBoxResults = mElement.FindElements(By.XPath(@".//div[@class='input-group']//input | ./div/input[@ng-model]")).ToList();

                    inputBox = inputBoxResults.Count == 0 ? mElement : inputBoxResults[0];
                    inputBox.Clear();
                    inputBox.SendKeys(Value);
                    DlkLogger.LogInfo("Set() execution passed.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Set() execution failed. :" + ex.Message, ex);
                }
            }
        }

        [Keyword("Select")]
        public void Select(string Value)
        {
            if (Value.Equals(string.Empty))
            {
                DlkLogger.LogInfo("Skipping select since the data parameter is blank.");
            }
            else
            {
                DlkBaseControl content = null;
                try
                {
                    initialize();
                    bool foundMatching = false;
                    IList<IWebElement> contentList = mElement.FindElements(By.XPath(@".//div[@class='content']/div | .//div[@class='combo_box_select_input_div']/select/option | ..//div[@class='list-group']/span/span[@ng-click] | ..//span[@id]/span[@ng-click]"));
                    string val = string.Empty;
                    foreach (IWebElement element in contentList)
                    {
                        content = new DlkBaseControl("Content", element);
                        val = content.GetValue().TrimEnd();
                        if (content.GetValue().TrimEnd().Equals(Value))
                        {
                            content.mElement.Click();
                            foundMatching = true;
                            break;
                        }
                    }

                    if (!foundMatching)
                    {
                        DlkLogger.LogError(new Exception(val + " Value was not found."));
                    }

                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("is not clickable"))
                    {
                        DlkCommon.DlkCommonFunction.ScrollIntoView(content.mElement);
                        content.mElement.Click();
                    }
                    else
                    {
                        throw new Exception("Select() execution failed. :" + ex.Message, ex);
                    }
                }
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySelectedItem", new String[] { "1|text|Expected Value|SampleText" })]
        public void VerifySelectedItem(string ItemText)
        {
            try
            {
                initialize();
                string actual = "";

                DlkBaseControl currTextBox = new DlkBaseControl("Textbox", mElement.FindElement(By.XPath(".//input[@type='text']")));
                actual = currTextBox.GetValue();

                DlkAssert.AssertEqual("VerifySelectedItem", ItemText, actual);
                DlkLogger.LogInfo("VerifySelectedItem() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedItem() execution failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemExists")]
        public void VerifyItemExists(string ItemText, string TrueOrFalse)
        {
            try
            {
                bool actualResult = false;
                initialize();
                IWebElement inputBox = mElement.FindElement(By.XPath(@".//div[@class='input-group']//input"));
                inputBox.Clear();
                inputBox.SendKeys(ItemText);
                Thread.Sleep(1500);
                IList<IWebElement> contentList = mElement.FindElements(By.XPath(@".//div[@class='content']/div | .//div[@class='combo_box_select_input_div']/select/option"));

                foreach (IWebElement element in contentList)
                {
                    DlkBaseControl content = new DlkBaseControl("Content", element);

                    if (element.FindElements(By.XPath("./span[@class='ffb-match']")).Count > 0)
                    {
                        content = new DlkBaseControl("Content", element.FindElement(By.XPath("./span[@class='ffb-match']")));
                    }

                    if (content.GetValue().Trim().Equals(ItemText))
                    {
                        actualResult = true;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyItemExists", Convert.ToBoolean(TrueOrFalse), actualResult);
                DlkLogger.LogInfo("VerifyItemExists() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() execution failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyMatchingCount")]
        public void VerifyMatchingCount(string ItemText, string MatchingCount)
        {
            try
            {
                initialize();
                IWebElement inputBox = mElement.FindElement(By.XPath(@".//div[@class='input-group']//input"));
                inputBox.SendKeys(ItemText);
                Thread.Sleep(1500);
                IList<IWebElement> contentList = mElement.FindElements(By.XPath(@".//div[@class='content']/div | .//div[@class='combo_box_select_input_div']/select/option"));
                
                DlkAssert.AssertEqual("VerifyMatchingCount", Convert.ToInt32(MatchingCount), contentList.Count);
                DlkLogger.LogInfo("VerifyMatchingCount() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMatchingCount() execution failed : " + e.Message, e);
            }
        }
        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
            //Sample check-in
            IList<IWebElement> paginationControls = mElement.FindElements(By.XPath(@".//div[@class='paging']/a"));
            foreach (IWebElement element in paginationControls)
            {
                DlkBaseControl control = new DlkBaseControl("Control", element);
                switch (control.GetValue().Trim())
                {
                    case ">" :
                    {
                        _next = element;
                        break;
                    }
                    case ">>" :
                    {
                        _lastPage = element;
                        break;
                    }
                    case "<" :
                    {
                        _previous = element;
                        break;
                    }
                    case "<<" :
                    {
                        _firstPage = element;
                        break;
                    }
                }
            }
        }

        #endregion
    }
}
