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
    [ControlType("Widget")]
    public class DlkWidget : DlkBaseControl
    {
        #region Declarations
        string widgetAreaXpath = "./ancestor::div[@id='dashboard_index_widget_area']";
        string cardHolderXpath = ".//h3[@class='card-title']/parent::div";
        string widgetCardTitle = "";
        #endregion

        #region Constructors

        public DlkWidget(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {

        }

        #endregion

        #region Keywords

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

        [Keyword("DragAndDrop")]
        public void DragAndDrop(string OrderNumber)
        {
            try
            {
                if (!int.TryParse(OrderNumber, out int result))
                {
                    throw new Exception("OrderNumber must be a number.");
                }

                initialize();
                IWebElement widgetArea = mElement.FindElement(By.XPath(widgetAreaXpath));
                List<IWebElement> widgets = widgetArea.FindElements(By.XPath(cardHolderXpath)).ToList();
                IWebElement fromElement = mElement.FindElement(By.XPath(cardHolderXpath));
                IWebElement toElement = widgets[result - 1];

                DlkCommon.DlkCommonFunction.DragAndDrop(fromElement, toElement, -5, 0);

                DlkLogger.LogInfo("DragAndDrop() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("DragAndDrop() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPosition", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyPosition(string OrderNumber)
        {
            try
            {
                if (!int.TryParse(OrderNumber, out int result))
                {
                    throw new Exception("OrderNumber must be a number.");
                }

                initialize();
                int actual = 0;
                IWebElement widgetArea = mElement.FindElement(By.XPath(widgetAreaXpath));
                List<IWebElement> widgets = widgetArea.FindElements(By.XPath(cardHolderXpath)).ToList();

                for (int i = 0; i < widgets.Count; i++)
                {
                    if (widgets[i].FindElement(By.XPath(".//h3")).Text == widgetCardTitle)
                    {
                        actual = i + 1;
                        break;
                    }
                }

                DlkAssert.AssertEqual("VerifyPosition()", OrderNumber, actual.ToString());
                DlkLogger.LogInfo("VerifyPosition() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPosition() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();

            widgetCardTitle = mElement.FindElement(By.XPath(".//h3")).Text;
        }

        #endregion
    }
}
