using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace AjeraLib.DlkControls
{
    [ControlType("Indicator")]
    class DlkIndicator:DlkBaseControl
    {

         #region DECLARATIONS
        private Boolean IsInit;
/*
        private int retryLimit = 3;
*/
        #endregion

        #region CONSTRUCTOR

        public DlkIndicator(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) {}

        public DlkIndicator(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues){}

        public DlkIndicator(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement){}

        public DlkIndicator(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkIndicator(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector){}

        public void Initialize()
        {
            if (!IsInit)
            {                
                FindElement();
                IsInit = true;
            }
        }
        #endregion

        #region KEYWORDS

        [Keyword("VerifyExistsByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyExistsByIndex(String ItemIndex, string IsTrueOrFalse)
        {
            try
            {
                Initialize();
                int iTargetItemPos = Convert.ToInt32(ItemIndex);

                string mIndicatorPath = "(" + mSearchValues[0] + ")[position()=" + iTargetItemPos + "]";
                DlkIndicator indicator = new DlkIndicator("Indicator", "XPATH", mIndicatorPath);
                string ActValue =indicator.GetAttributeValue("style").Contains("display: inline;").ToString();
                DlkAssert.AssertEqual("VerifyExistsByIndex()", IsTrueOrFalse.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExistsByIndex() failed : " + e.Message, e);
            }
        }
        #endregion
    }
}
