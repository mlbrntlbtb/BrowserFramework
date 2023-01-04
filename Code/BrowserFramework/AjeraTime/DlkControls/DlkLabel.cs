using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AjeraTimeLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace AjeraTimeLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkAjeraTimeBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit = false;
        #endregion

        #region CONSTRUCTORS
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }


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
        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                FindElement();
                String ActualResult = "";

                ActualResult = mElement.Text.Trim();
                if (ActualResult.Contains("\r\n"))
                {
                    ActualResult = ActualResult.Replace("\r\n", "\n");
                }
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, ActualResult);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }

        }

        #endregion
    }
}
