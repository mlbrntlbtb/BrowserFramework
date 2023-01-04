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
    [ControlType("Pagination")]
    public class DlkPagination : DlkBaseControl
    {
        #region Declarations
        #endregion

        #region Constructors

        public DlkPagination(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            Initialize();
        }

        public DlkPagination(String ControlName, IWebElement ExistingWebElement) :
            base(ControlName, ExistingWebElement)
        {
            Initialize();
        }

        #endregion

        #region Keywords

        [Keyword("Next")]
        public bool Next()
        {
            bool isNextAvailable = true;

            try
            {
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//li/a/i[@class='fa fa-angle-right']"));
                if (elements.Count > 0)
                {
                    DlkLink nextPage = new DlkLink("Next Page", elements[0].FindElement(By.XPath(@"..")));
                    elements[0].Click();
                    DlkLogger.LogInfo("Next( ) successfully executed.");
                }
                else
                {
                    isNextAvailable = false;
                    DlkLogger.LogInfo("Next( ) is not available");
                }

            }
            catch(Exception ex)
            {
                throw new Exception("Next( ) failed " + ex.Message, ex);
            }

            return isNextAvailable;
        }

        [Keyword("Previous")]
        public void Previous()
        {
            try
            {
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//li/a/i[@class='fa fa-angle-left']"));
                if (elements.Count > 0)
                {
                    elements[0].Click();
                    DlkLogger.LogInfo("Previous( ) successfully executed.");
                }
                else
                {
                    DlkLogger.LogInfo("Previous( ) is not available");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Previous( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("LastPage")]
        public void LastPage()
        {
            try
            {
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//li/a/i[@class='fa fa-angle-double-right']"));
                if (elements.Count > 0)
                {
                    elements[0].Click();
                    DlkLogger.LogInfo("LastPage( ) successfully executed.");
                }
                else
                {
                    DlkLogger.LogInfo("LastPage( ) is not available");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("LastPage( ) failed " + ex.Message, ex);
            }
        }

        [Keyword("FirstPage")]
        public void FirstPage()
        {
            try
            {
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//li/a/i[@class='fa fa-angle-double-left']"));
                if (elements.Count > 0)
                {
                    elements[0].Click();
                    DlkLogger.LogInfo("FirstPage( ) successfully executed.");
                }
                else
                {
                    DlkLogger.LogInfo("FirstPage( ) is not available");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("FirstPage( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("SelectByText")]
        public bool SelectByText(string Title)
        {
            bool isNextAvailable = true;

            try
            {
                bool found = false;
                List<string> searchQueries = new List<string>()
                {
                    @"../../following-sibling::div[1]/ul[@class='pagination_per_page pagination pagination-sm hidden-xs']/li/a[@title='" + Title + "']",
                    @"../../following-sibling::div[1]/ul[@class='pagination_per_page pagination pagination-sm hidden-xs']/li/a[text()='" + Title + "']",
                    @"./li/a[text()='" + Title + "']"
                };

                for (int i = 0; i < searchQueries.Count; i++)
                {
                    IList<IWebElement> elements = mElement.FindElements(By.XPath(searchQueries[i]));
                    if (elements.Count > 0)
                    {
                        DlkBaseControl paginationControl = new DlkBaseControl("Pagination_Control", elements[0]);
                        paginationControl.ClickUsingJavaScript();
                        DlkLogger.LogInfo("SelectByText( ) successfully executed.");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    DlkLogger.LogError(new Exception("Clickable Link : " + Title + " not found"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SelectByText( ) execution failed. " + ex.Message, ex);
            }

            return isNextAvailable;
        }

        [Keyword("VerifyDisplayLabelText")]
        public void VerifyDisplayLabelText(string Text)
        {
            try
            {
                IWebElement labelElement = mElement.FindElement(By.XPath(@"../../../div/span[@class='pagination_displaying displaycount']"));
                DlkLabel labelControl = new DlkLabel("Pagination Label", labelElement);
                labelControl.VerifyText(Text);
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyDisplayLabelText() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Methods

        public void Initialize()
        {
            FindElement();
        }

        public string GetCurrentPageNumber()
        {
            string pageNumber = string.Empty;
            DlkBaseControl currentPageControl = new DlkBaseControl("Current_Page", getActivePageNumberElement());
            pageNumber = currentPageControl.GetValue();

            return pageNumber;
        }

        public string GetCurrentDisplayPerPage()
        {
            string numberOfItemsPerPage = string.Empty;
            DlkBaseControl currentPageControl = new DlkBaseControl("Current_Display_PerPage", getActiveNumberOfPageElement());
            numberOfItemsPerPage = currentPageControl.GetValue();
            return numberOfItemsPerPage;
        }

        public bool IsCurrentDisplayPerPageEqual(string valueToCompare)
        {
            DlkBaseControl currentPageControl = new DlkBaseControl("Current_Display_PerPage", getActiveNumberOfPageElement());
            if (currentPageControl.GetValue().Equals(valueToCompare))
            {
                return true;
            }

            return false;
        }

        public bool IsCurrentPageEqual(string valueToCompare)
        {
            DlkBaseControl currentPageControl = new DlkBaseControl("Current_Page", getActivePageNumberElement());
            if (currentPageControl.GetValue().Equals(valueToCompare))
            {
                return true;
            }
            return false;
        }

        private IWebElement getActivePageNumberElement()
        {
            return mElement.FindElement(By.XPath(@"./li[@class='active']"));
        }

        private IWebElement getActiveNumberOfPageElement()
        {
            string perPageXpath = @"../../following-sibling::div[1]/ul[@class='pagination_per_page pagination pagination-sm hidden-xs']/li[@class='active']";
            return mElement.FindElement(By.XPath(perPageXpath));
        }

        #endregion
    }
}
