using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Pagination")]
    public class DlkPagination : DlkBaseControl
    {
        #region Declarations
        string loadIconXpath = "//div[contains(@class,'loader')]";
        #endregion

        #region Constructors

        public DlkPagination(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            // Initialize();
        }

        public DlkPagination(String ControlName, IWebElement ExistingWebElement) :
            base(ControlName, ExistingWebElement)
        {
            // Initialize();
        }

        #endregion

        #region Keywords

        [Keyword("Next")]
        public bool Next()
        {
            bool isNextAvailable = true;

            try
            {
                Initialize();
                waitForLoadIcon();
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//li/a/i[@class='fa fa-angle-right']"));
                if (elements.Count > 0)
                {
                    DlkLink nextPage = new DlkLink("Next Page", elements[0].FindElement(By.XPath(@"..")));
                    DlkCommon.DlkCommonFunction.ScrollIntoElement(mElement);
                    waitForLoadIcon();
                    elements[0].Click();
                    waitForLoadIcon();
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
                Initialize();
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
                Initialize();
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//li/a/i[contains(@class,'fa-angle-double-right')]"));
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
                Initialize();
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@".//li[not(contains(@class,'disabled'))]/a/i[contains(@class,'fa-angle-double-left')]"));
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
                Initialize();
                bool found = false;
                List<string> searchQueries = new List<string>()
                {
                    @"../../following-sibling::div[1]/ul[contains(@class,'pagination')]/li/a[@title='" + Title + "']",
                    @"../../following-sibling::div[1]/ul[contains(@class,'pagination')]/li/a[text()='" + Title + "']",
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
                Initialize();
                IWebElement labelElement = mElement.FindElement(By.XPath(@"../../../div/span[@class='pagination_displaying displaycount']"));
                DlkLabel labelControl = new DlkLabel("Pagination Label", labelElement);
                string actualResult = labelControl.GetValue();
                DlkAssert.AssertEqual("Pagination Display : ", Text, actualResult);
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyDisplayLabelText() execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyPerPage")]
        public void VerifyPerPage(string Page)
        {
            try
            {
                Initialize();
                IWebElement perPageElement = mElement.FindElement(By.XPath("./ancestor::div[contains(@class,'containerPagination')]//div[contains(@class,'columnRightPagination')]"));
                IWebElement perPageItemElement = DlkCommon.DlkCommonFunction.GetElementWithText(Page, perPageElement)[0];
                DlkBaseControl perPageItemControl = new DlkBaseControl("Pagination", perPageItemElement);
                DlkAssert.AssertEqual("VerifyPerPage", Page, perPageItemControl.GetValue());
                DlkLogger.LogInfo("VerifyPerPage( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyPerPage( ) execution failed. " + ex.Message, ex);
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
            Initialize();
            return mElement.FindElement(By.XPath(@"./li[@class='active' or @class='page-item disabled']"));
        }

        private IWebElement getActiveNumberOfPageElement()
        {
            Initialize();
            string perPageXpath = @"../../following-sibling::div[1]/ul[contains(@class,'pagination_per_page')]/li[@class='active'] |
                                    ../../following-sibling::div[1]/nav/ul[contains(@class,'pagination_per_page')]/li[@class='page-item disabled']";
            return mElement.FindElement(By.XPath(perPageXpath));
        }

        private void waitForLoadIcon()
        {
            IList<IWebElement> loadIcons = DlkEnvironment.AutoDriver.FindElements(By.XPath(loadIconXpath)).Where(x => x.Displayed).ToList();

            while (loadIcons.Count != 0)
            {
                Thread.Sleep(500);
                loadIcons = DlkEnvironment.AutoDriver.FindElements(By.XPath(loadIconXpath)).Where(x => x.Displayed).ToList();
            }
        }
        #endregion
    }
}
