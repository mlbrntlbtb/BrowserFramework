using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("MessageModal")]
    public class DlkMessageModal : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkMessageModal(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMessageModal(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMessageModal(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) 
        {
            DlkEnvironment.AutoDriver.SwitchTo().Frame(ExistingWebElement);
            DlkEnvironment.mSwitchediFrame = true;
            mElement = DlkEnvironment.AutoDriver.FindElement(By.XPath("html/body"));
        }

        #endregion

        #region Methods

        public void initialize()
        {
            DlkEnvironment.AutoDriver.SwitchTo().Frame(DlkEnvironment.AutoDriver.FindElement(By.XPath("//iframe[@class='cboxIframe'] | //iframe")));
            DlkEnvironment.mSwitchediFrame = true;
            FindElement();
        }

        #endregion

        #region Keywords

        [Keyword("HasPartialTextContent")]
        public void HasPartialTextContent(string Text)
        {
            try
            {
                initialize();
                string[] expectedResults = Text.Split('~');
                string actualResult = string.Empty;

                foreach (string expectedResult in expectedResults)
                {
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(expectedResult, mElement, true);
                    foreach (IWebElement element in elements)
                    {
                        DlkBaseControl textControl = new DlkBaseControl("Text_Control", element);
                        actualResult = textControl.GetValue().Trim();
                        if (actualResult.Trim().Contains(expectedResult))
                        {
                            break;
                        }
                    }

                    DlkAssert.AssertEqual("HasPartialTextContent : ", expectedResult, actualResult, true);
                }

                DlkLogger.LogInfo("HasPartialTextContent() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("HasPartialTextContent() execution failed. : " + Text + " : " + ex.Message, ex);
            }
            finally
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        #endregion
    }
}
