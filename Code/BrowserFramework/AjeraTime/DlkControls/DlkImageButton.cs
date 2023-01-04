using System;
using System.Collections.Generic;
using System.Threading;
using AjeraTimeLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;

namespace AjeraTimeLib.DlkControls
{
    [ControlType("ImageButton")]
    class DlkImageButton: DlkAjeraTimeBaseControl
    {
        
        #region DECLARATIONS
        private Boolean IsInit;
        #endregion

        #region CONSTRUCTOR

        public DlkImageButton(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) {}

        public DlkImageButton(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues){}

        public DlkImageButton(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement){}

        public DlkImageButton(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkImageButton(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
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

        public void InitializeRow(string RowNumber)
        {
            InitializeSelectedElement(RowNumber);
        }


        #endregion

        #region KEYWORDS
        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                MouseOver();

                if (mElement.GetAttribute("class") == "popupBtn")
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(4.5);
                    }
                }
                else
                {                
                    Click(4.5);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToLower().Contains("the http request to the remote webdriver server for url"))
                    throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST

        #endregion

        #region METHODS
      
        #endregion

    }
}
