using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("MessageList")]
    public class DlkMessageList : DlkBaseControl
    {
        #region Declarations

        private const string MESSAGE_ROW = @".//div[@class='align_right fullwidth']/parent::div";
        private const string ACTIONS = @"./div[@class='align_right fullwidth']/a";
        #endregion

        #region Constructors

        public DlkMessageList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMessageList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        #endregion

        #region Keywords

        [Keyword("PerformRowActionColumn")]
        public void PerformRowActionColumn(string Row, string Column)
        {
            try
            {
                FindElement();
                int columnIndex = Convert.ToInt32(Column) - 1;
                IWebElement row = getMessageRow(Convert.ToInt32(Row));
                IList<IWebElement> rowActions = row.FindElements(By.XPath(ACTIONS));
                DlkBaseControl actionControl = new DlkBaseControl("Action", rowActions[columnIndex]);
                actionControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("PerformRowActionColumn() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("PerformRowActionColumn() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private IWebElement getMessageRow(int row)
        {
            IList<IWebElement> messageRows = mElement.FindElements(By.XPath(MESSAGE_ROW));
            return messageRows[row - 1];
        }

        #endregion
    }
}
