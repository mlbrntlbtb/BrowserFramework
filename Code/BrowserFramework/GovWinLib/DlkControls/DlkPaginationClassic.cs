using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{

    //DlkPaginationClassic is a table element that has pagination functionalities
    //the root element is expected to be the table also
    [ControlType("PaginationClassic")]
    public class DlkPaginationClassic : DlkBaseControl
    {
        private const string PageLinkParentXPath = @".//*[@class='MAININPUT'][1]/b";
        public DlkPaginationClassic(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPaginationClassic(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPaginationClassic(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkPaginationClassic(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        
        public void Refresh()
        {
            FindElement();
        }

        [Keyword("ClickPageButton", new String[] { "1|text|Page Button Caption|Next >>" })]
        public void ClickPageButton(String PageButtonCaption)
        {
            Refresh();
            DlkLink pageButton = new DlkLink("PageButton '" + PageButtonCaption + "'", this, "XPATH", PageLinkParentXPath + "//a[contains(text(),'" + PageButtonCaption + "')]");
            pageButton.Click();

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
                    DlkLink pageButton = new DlkLink("PageButton '" + pageButtonCaption + "'", this, "XPATH", PageLinkParentXPath + "//a[contains(text(),'" + pageButtonCaption + "')]");
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

        [RetryKeyword("VerifyPageCount", new String[] { "1|text|Expected Page Count|5" })]
        public void VerifyPageCount(String ExpectedPageCount)
        {
            String expectedPageCount = ExpectedPageCount;

            this.PerformAction(() =>
            {
                Refresh();
                DlkLink pageButton = new DlkLink("Page Button", this, "XPATH", PageLinkParentXPath + "/*[last()]");
                DlkAssert.AssertEqual("VerifyPageCount()", expectedPageCount, pageButton.GetValue());
            }, new String[]{"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }
        #endregion
    }
}

