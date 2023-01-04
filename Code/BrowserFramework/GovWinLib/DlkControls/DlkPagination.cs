using System;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{

    [ControlType("Pagination")]
    public class DlkPagination : DlkBaseControl
    {
        protected DlkProcessing mProcessing = null;

        public DlkPagination(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPagination(String ControlName, String SearchType, String SearchValue, DlkProcessing processing)
            : base(ControlName, SearchType, SearchValue) { mProcessing = processing; }
        public DlkPagination(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPagination(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkPagination(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        
        public void Refresh()
        {
            FindElement();
        }

        [Keyword("ClickPageButton", new String[] { "1|text|Page Button Caption|Next >>" })]
        public void ClickPageButton(String PageButtonCaption)
        {
            Refresh();
            
            
            Thread.Sleep(10000);
            
            IWebElement pgeB = mElement.FindElement(By.XPath(".//a[contains(text(),'" + PageButtonCaption + "')]"));
            while(pgeB == null)
                pgeB = mElement.FindElement(By.XPath(".//a[contains(text(),'" + PageButtonCaption + "')]"));
            pgeB.Click();

            

            //handle ajax
            if (mProcessing != null)
                mProcessing.WaitProcessing();

        }

        #region Verify methods
        [RetryKeyword("VerifyPageButtonExists", new String[] { "1|text|Page Button Caption|Next >>", 
                                                                "2|text|Expected Value (TRUE or FALSE)|TRUE"})]
        public void VerifyPageButtonExists(String PageButtonCaption, String TrueOrFalse)
        {
            String pageButtonCaption = PageButtonCaption;
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    Refresh();
                    DlkLink pageButton = new DlkLink("PageButton '" + pageButtonCaption + "'", this, "XPATH", ".//a[contains(text(),'" + pageButtonCaption + "')]");
                    pageButton.VerifyExists(Convert.ToBoolean(expectedValue));
                }, new String[]{"retry"});
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                VerifyExists(Convert.ToBoolean(expectedValue));
            }, new String[]{"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {
                //Refresh();
                Thread.Sleep(2000);
                Boolean bExists = base.Exists();

                //Boolean bVisible = mElement.Displayed && mElement.Enabled;

                Boolean bActual = bExists;

                DlkVariable.SetVariable(VariableName, Convert.ToString(bActual));

            }, new String[] { "retry" });
        }


        [RetryKeyword("VerifyPageCount", new String[] { "1|text|Expected Page Count|5" })]
        public void VerifyPageCount(String ExpectedPageCount)
        {
            String expectedPageCount = ExpectedPageCount;

            this.PerformAction(() =>
            {
                Refresh();
                DlkLink pageButton = new DlkLink("Page Button", this, "XPATH", ".//a[@class='paginate_active' or @class='paginate_button'][last()]");
                DlkAssert.AssertEqual("VerifyPageCount()", expectedPageCount, pageButton.GetValue());
            }, new String[]{"retry"});

        }
        #endregion
    }
}

