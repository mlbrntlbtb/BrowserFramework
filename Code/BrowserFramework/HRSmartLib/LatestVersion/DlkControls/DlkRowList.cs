using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("RowList")]
    public class DlkRowList : DlkBaseControl
    {
        #region Declarations

        private const string ROW = "div[@role='tabpanel']";
        private const string ROW_CONTENT = "div[@class='row employee-row hua_ribbon performance_ribbon']";
        private const string ROW_TOGGLE_SECTION = "div[@class='col-sm-12 tabbed_interface_page_body']";
        private const string ROW_TOGGLE = "div[@class='col-sm-12 text-right text-center-xs tabbed_interface_toggle_sections']";

        #endregion

        #region Constructors

        public DlkRowList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            initialize();
        }

        #endregion

        #region Properties
        #endregion

        #region Keywords

        [Keyword("SelectRowActionByTitle")]
        public void SelectRowActionByTitle(string Row, string Title)
        {
            try
            {
                int row = Convert.ToInt32(Row) - 1;
                IList<IWebElement> rowList = mElement.FindElements(By.XPath("./" + ROW));

                if (row < rowList.Count)
                {
                    IWebElement rowAction = rowList[row].FindElement(By.XPath("./div/" + ROW_CONTENT + "//div[@class='dropdown']/a"));
                    DlkBaseControl rowActionControl = new DlkBaseControl("Row_Action_Control : " + Row, rowAction);
                    rowActionControl.Click();
                    handleDropDownAction(rowAction, Title);
                    DlkLogger.LogInfo("SelectRowActionByTitle( ) successfully executed.");
                }
                else
                {
                    throw new Exception("Row : " + Row + " is missing.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectRowActionByTitle( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyRowText")]
        public void VerifyRowText(string Row, string ExpectedResult)
        {
            try
            {
                IWebElement row = mElement.FindElement(By.XPath(@"./div[" + Row + "]"));
                DlkBaseControl rowControl = new DlkBaseControl("Row : " + row.Text, row);
                DlkAssert.AssertEqual("Verify Row : " + Row, ExpectedResult, rowControl.GetValue());
                DlkLogger.LogInfo("VerifyRowText( ) successfuly executed.");
            }
            catch ( Exception ex)
            {
                throw new Exception("VerifyRowText( ) execution failed. " + ex.Message, ex);
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

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private void handleDropDownAction(IWebElement actionElement, string title)
        {
            IList<IWebElement> dropDownMenuOptions = actionElement.FindElements(By.XPath(@"../ul[@role='menu']/li/a"));
            foreach (IWebElement option in dropDownMenuOptions)
            {
                DlkBaseControl optionControl = new DlkBaseControl("Option", option);
                if (optionControl.GetValue().Trim().Equals(title))
                {
                    optionControl.Click();
                    break;
                }
            }
        }

        #endregion

    }
}
 