using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;


using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Suggestions")]
    public class DlkSuggestions : DlkBaseControl
    {
        private Boolean IsInit = false;

        public DlkSuggestions(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSuggestions(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSuggestions(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        
        public void Initialize()
        {
            
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
        }

        
        [Keyword("Select", new String[] { "1|text|Caption|Tab1"})]
        public void Select(String Caption)
        {
            Initialize();
            DlkBaseControl SuggestItem = new DlkBaseControl("Suggested Item", mElement.FindElement(By.XPath("./div[contains(.,'" + Caption + "')]")));
            SuggestItem.Click();
        }

        
    }
}

