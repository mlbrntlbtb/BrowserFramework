using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("NineBox")]
    public class DlkNineBox : DlkBaseControl
    {
        #region Declarations

        private const string HEADING_TOOLTIP = @"./div[@class='panel-heading']/i";
        private const string EMPLOYEE_LIST = @".//div[@class='potential_matrix_cell_body']/ul";
        private const string EDIT_THIS_BOX = @".//div[@class='potential_matrix_cell_body']/a";
        private const string FOOTER = @".//div[@class='panel-footer']";
        private const string POPOVER = @".//div[contains(@id,'popover')]";

        #endregion

        #region Constructors

        public DlkNineBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            //Do Nothing.
        }

        public DlkNineBox(String ControlName, IWebElement existingWebElement)
            : base(ControlName, existingWebElement)
        {
            //Do Nothing.
        }
        
        #endregion

        #region Keywords

        [Keyword("HasTextContent")]
        public void HasTextContent(string Text)
        {
            try
            {
                verifyHasTextContent(Text, false);
                DlkLogger.LogInfo("HasTextContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HasTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("HasPartialTextContent")]
        public void HasPartialTextContent(string Text)
        {

            try
            {
                verifyHasTextContent(Text, true);
                DlkLogger.LogInfo("HasPartialTextContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HasPartialTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
        }

        [Keyword("ClickByRowIndex")]
        public void ClickByRowIndex(string Row, string Index)
        {
            try
            {
                initialize();
                IWebElement employeeList = mElement.FindElement(By.XPath(EMPLOYEE_LIST));
                DlkList employeeListControl = new DlkList("Employee List Control", employeeList);
                employeeListControl.ClickByRowIndex(Row, Index);
            }
            catch(Exception ex)
            {
                throw new Exception("ClickByRowIndex() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickByTitleAndIndex")]
        public void ClickByTitleAndIndex(string Title, string Index)
        {
            try
            {
                int index = Convert.ToInt32(Index) - 1;
                initialize();
                DlkCommon.DlkCommonFunction.ClickElementByTitleAndIndex(mElement, Title, index);
            }
            catch (Exception ex)
            {
                throw new Exception("ClickByTitleAndIndex() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickDetailView")]
        public void ClickDetailView()
        {
            try
            {
                initialize();
                IWebElement footerElement = mElement.FindElement(By.XPath(FOOTER));
                IWebElement detailView = footerElement.FindElement(By.XPath("./a"));
                DlkBaseControl detailViewControl = new DlkBaseControl("Detail View Control", detailView);
                detailViewControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ClickDetailView() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickDetailView() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("ClickEditThisBox")]
        public void ClickEditThisBox()
        {
            try
            {
                initialize();
                IWebElement editThisBox = mElement.FindElement(By.XPath(EDIT_THIS_BOX));
                DlkBaseControl editThisBoxControl = new DlkBaseControl("Edit This Box Control", editThisBox);
                editThisBoxControl.ClickUsingJavaScript();
                DlkLogger.LogInfo("ClickEditThisBox() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("ClickEditThisBox() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyTooltip")]
        public void VerifyTooltip(string Caption)
        {
            try
            {
                initialize();
                IWebElement tooltip = mElement.FindElement(By.XPath(@".//i[@class='glyphicon glyphicon-info-sign pull-right flip']"));
                string actualValue = tooltip.GetAttribute("data-original-title") == null ?
                    tooltip.GetAttribute("title") :
                    tooltip.GetAttribute("data-original-title");

                if (actualValue == string.Empty && tooltip.GetAttribute("data-content") != null)
                {
                    actualValue = tooltip.GetAttribute("data-content");
                }

                DlkAssert.AssertEqual("Action Column", Caption, actualValue);
                DlkLogger.LogInfo("VerifyTooltip() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyTooltip() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyRowToolTipCaption")]
        public void VerifyRowToolTipCaption(string Row, string Caption)
        {
            try
            {
                initialize();
                IWebElement employeeList = mElement.FindElement(By.XPath(EMPLOYEE_LIST));
                DlkList employeeListControl = new DlkList("Employee List Control", employeeList);
                IWebElement rowElement = employeeListControl.GetRow(Row);
                base.ScrollIntoViewUsingJavaScript();
                Actions mBuilder = new Actions(DlkEnvironment.AutoDriver);
                IAction mAction = mBuilder.MoveToElement(rowElement).Build();
                mAction.Perform();
                Thread.Sleep(1000);
                IWebElement popOverElement = rowElement.FindElement(By.XPath(POPOVER));
                DlkBaseControl popOverControl = new DlkBaseControl("Popover Control", popOverElement);
                string actualResult = popOverControl.GetValue().Replace("\r\n"," ");
                DlkAssert.AssertEqual("VerifyRowToolTipCaption", Caption, actualResult, true);
                DlkLogger.LogInfo("VerifyRowToolTipCaption( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyRowToolTipCaption( ) execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private void verifyHasTextContent(string text, bool partialMatch)
        {
            initialize();
            string[] expectedResults = text.Split('~');

            foreach (string expectedResult in expectedResults)
            {
                string actualResult = string.Empty;
                IWebElement element = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, mElement, partialMatch)[0];
                DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                actualResult = textControl.GetValue().Trim();
                DlkAssert.AssertEqual("HasTextContent : ", expectedResult, actualResult);
            }
        }

        #endregion
    }
}
