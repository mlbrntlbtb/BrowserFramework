using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace AjeraLib.DlkControls
{
    [ControlType("SearchBox")]
    public class DlkSearchBox : DlkBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit;

        #endregion
        
        #region CONSTRUCTOR
        public DlkSearchBox(string ControlName, string SearchType, string SearchValue) 
            : base(ControlName, SearchType, SearchValue){}

        public DlkSearchBox(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues){}

        public DlkSearchBox(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}

        public DlkSearchBox(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkSearchBox(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector){}

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        #endregion

        #region KEYWORDS
        [Keyword("SetByValue", new String[] { "1|text|Value|SampleValue" })]
        public void SetByValue(String Value)
        {
            try
            {
                Initialize();

                mElement.Clear();
                if (!string.IsNullOrEmpty(Value))
                {
                    mElement.SendKeys(Value);

                    if (mElement.GetAttribute("value").ToLower() != Value.ToLower())
                    {
                        mElement.Clear();
                        mElement.SendKeys(Value);
                    }
                }
                DlkLogger.LogInfo("Successfully executed SetByValue()");
            }
            catch (Exception e)
            {
                throw new Exception("SetByValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetBySuggestion", new String[] { "A|ADP" })]
        public void SetBySuggestion(String Suggestion, String Value)
        {
            try
            {
                Initialize();
                SetByValue(Suggestion);
                Thread.Sleep(1000);
                mElement.SendKeys(Keys.ArrowDown);
                var searchBoxList = mElement.FindElements(By.XPath("//ul[contains(@class,'autocomplete')]/li"));

                bool bFound = false;
                string actualItems = string.Empty;

                foreach (var item in searchBoxList)
                {
                    actualItems = actualItems + " " + item.Text;
                    if (item.Text.ToLower().Equals(Value.ToLower()))
                    {
                        bFound = true;
                        item.Click();
                        DlkLogger.LogInfo("Successfully executed SetBySuggestion()");
                    }
                }
                if (!bFound)
                {
                    throw new Exception("SetBySuggestion() failed : " + Suggestion + "not found in the list - " + actualItems);
                }

                
            }
            catch (Exception e)
            {
                throw new Exception("SetBySuggestion() failed : " + e.Message, e);
            }
        }


        [Keyword("ClickSearch")]
        public void ClickSearch()
        {
            try
            {
                Initialize();
                var searchButton = mElement.FindElement(
                    By.XPath("./ancestor::span[1]/following-sibling::span[contains(@class,'imagebutton')]"));

                if (searchButton.Displayed)
                {
                    searchButton.Click();
                }
                else
                {
                    throw new Exception("ClickSearch() failed : Search Button not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickSearch() failed : " + e.Message, e);
            }

        }

        #endregion

        #region METHODS
        #endregion
    }
}
