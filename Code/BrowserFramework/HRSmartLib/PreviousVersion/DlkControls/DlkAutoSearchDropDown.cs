using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
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
            try
            {
                initialize();
                IWebElement inputBox = mElement.FindElement(By.XPath(@".//div[@class='input-group']//input"));
                inputBox.SendKeys(Value);
                DlkLogger.LogInfo("Set() execution passed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Set() execution failed. :" + ex.Message, ex);
            }
        }

        [Keyword("Select")]
        public void Select(string Value)
        {
            try
            {
                initialize();
                bool foundMatching = false;
                IList<IWebElement> contentList = mElement.FindElements(By.XPath(@".//div[@class='content']/div"));

                foreach(IWebElement element in contentList)
                {
                    DlkBaseControl content = new DlkBaseControl("Content", element);
                    if (content.GetValue().Trim().Equals(Value))
                    {
                        content.mElement.Click();
                        foundMatching = true;
                        break;
                    }
                }

                if (!foundMatching)
                {
                    DlkLogger.LogError(new Exception(Value + " Value was not found."));
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Select() execution failed. :" + ex.Message, ex);
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
