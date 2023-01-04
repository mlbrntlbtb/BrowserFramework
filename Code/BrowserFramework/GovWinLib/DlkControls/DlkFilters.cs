using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Filters")]
    public class DlkFilters : DlkBaseControl
    {
        private String mstrFilterType = "";

        public DlkFilters(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkFilters(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkFilters(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        
        public void Initialize()
        {

            FindElement();
            mstrFilterType = GetAttributeValue("class");

        }

        [Keyword("RemoveFilter", new String[] { "1|text|Filter Path|Contractor~HARRIS CORPORATION"})]
        public void RemoveFilter(String FilterPath)
        {
            Initialize();
            switch (mstrFilterType)
            {
                case "selectedBucketContainer":
                    RemoveFilter_SelectedBucketContainer(FilterPath);
                    break;
                default:
                    throw new Exception("RemoveFilter() failed. Keyword does not support Filter control of type '" + mstrFilterType + "'");
            }
        }

        private void RemoveFilter_SelectedBucketContainer(String pstrFilterPath)
        {
            String[] arrFilters = pstrFilterPath.Split('~');
            if (arrFilters.Count() > 2)
            {
                throw new Exception("RemoveFilter() failed. Invalid filter path. Syntax: <Parent Filter>~<Child Filter>. Example: Contractor~HARRIS CORPORATION");
            }

            DlkBaseControl bucketGroup = new DlkBaseControl("Bucket Group", "XPATH", "//div[@class='selectedBucketGroup']/div[@class='filtersHeaderTitle clearfix']/span[contains(.,'" + arrFilters[0] + "')]/../..");
            if (bucketGroup.Exists(1))
            {
                if (arrFilters.Count() > 1)
                {
                    Boolean bFound = false;
                    IList<IWebElement> filterItems = bucketGroup.mElement.FindElements(By.XPath("./div[@class='selectedBucketItem clearfix']"));
                    foreach (IWebElement filterItem in filterItems)
                    {
                        DlkBaseControl filterText = new DlkBaseControl("Filter Item", filterItem, "span");
                        if (filterText.GetValue() == arrFilters[1])
                        {
                            DlkBaseControl removeFilter = new DlkBaseControl("RemoveFilter", filterItem, "img");
                            DlkEnvironment.CaptureUrl();
                            removeFilter.Click();
                            DlkEnvironment.WaitUrlUpdate();
                            bFound = true;
                        }
                    }
                    if (!bFound)
                    {
                        throw new Exception("RemoveFilter() failed. Filter '" + pstrFilterPath + "' not found in the filter list.");
                    }
                }
                else
                {
                    DlkBaseControl removeFilter = new DlkBaseControl("RemoveFilter", bucketGroup, "XPATH", "./div[@class='filtersHeaderTitle clearfix']/img");
                    DlkEnvironment.CaptureUrl();
                    removeFilter.Click();
                    DlkEnvironment.WaitUrlUpdate();
                }

            }
            else
            {
                throw new Exception("RemoveFilter() failed. Filter '" + pstrFilterPath + "' not found in the filter list.");
            }
        }

        #region Verify methods
        [RetryKeyword("VerifyFilter", new String[] { "1|text|Filter Path|Basic Information~Department",
                                                       "2|text|Expected Value|AGENCY FOR INTERNATIONAL DEVELOPMENT" })]
        public void VerifyFilter(String FilterPath, String ExpectedValue)
        {
            String filterPath = FilterPath;
            String expectedValue = ExpectedValue;

            this.PerformAction(() =>
            {
                //try
                //{
                Initialize();
                switch (mstrFilterType)
                {
                    case "filtersContainer clearfix":
                        VerifyFilter_FiltersContainer(filterPath, expectedValue);
                        break;
                    case "selectedBucketContainer":
                        VerifyFilter_SelectedBucketContainer(filterPath, expectedValue);
                        break;
                    default:
                        throw new Exception("VerifyFilter() failed. Filter List of type '" + mstrFilterType + "' is not supported.");
                }
                //}
                //catch (Exception e)
                //{
                //    DlkLogger.LogInfo("here");
                //    if (!(e is DlkException))
                //    {
                //        DlkLogger.LogInfo(e.Message + " " + e.StackTrace + "\n" + e.InnerException.Message + " " + e.InnerException.StackTrace);

                //    }
                //}
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyFilterExists", new String[] { "1|text|Filter Path|Basic Information~Department~DEPT1",
                                                            "2|text|Expected Value|TRUE" })]
        public void VerifyFilterExists(String FilterPath, String TrueOrFalse)
        {
            String filterPath = FilterPath;
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                Initialize();
                switch (mstrFilterType)
                {
                    case "filtersContainer clearfix":
                        VerifyFilterExists_FiltersContainer(filterPath, expectedValue);
                        break;
                    case "selectedBucketContainer":
                        VerifyFilterExists_SelectedBucketContainer(filterPath, expectedValue);
                        break;
                    default:
                        throw new Exception("VerifyFilterExists() failed. Filter List of type '" + mstrFilterType + "' is not supported.");
                }
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value(TRUE or FALSE)|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    VerifyExists(Convert.ToBoolean(expectedValue));
                }, new String[] { "retry" });

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

        [RetryKeyword("VerifyFilterCount", new String[] { "1|text|Expected Count|2" })]
        public void VerifyFilterCount(String ExpectedCount)
        {
            String expectedValue = ExpectedCount;

            this.PerformAction(() =>
                {
                    Initialize();
                    double dblExpectedCount = Convert.ToDouble(expectedValue);
                    switch (mstrFilterType)
                    {
                        case "filtersContainer clearfix":
                            VerifyCount_FiltersContainer(dblExpectedCount);
                            break;
                        case "selectedBucketContainer":
                            VerifyCount_SelectedBucketContainer(dblExpectedCount);
                            break;
                        default:
                            throw new Exception("VerifyFilterCount() failed. Keyword does not support Filter control of type '" + mstrFilterType + "'");
                    }
                }, new String[] { "retry" });
        }

        private void VerifyCount_FiltersContainer(double dblExpectedCount)
        {
            double dblActualCount = Convert.ToDouble(mElement.FindElements(By.CssSelector("li.selection")).Count);
            DlkAssert.AssertEqual("VerifyCount_FiltersContainer", dblExpectedCount, dblActualCount);
        }

        private void VerifyCount_SelectedBucketContainer(double dblExpectedCount)
        {
            double dblActualCount = Convert.ToDouble(mElement.FindElements(By.XPath(".//div[@class='selectedBucketItem clearfix']")).Count);
            DlkAssert.AssertEqual("VerifyCount_SelectedBucketContainer", dblExpectedCount, dblActualCount);
        }

        private void VerifyFilter_FiltersContainer(String pstrFilterPath, String pstrExpectedValue)
        {
            String[] arrFilters = pstrFilterPath.Split('~');
            if (arrFilters.Count() != 2)
            {
                throw new Exception("VerifyFilter() failed. Invalid filter path. Syntax: <Parent Filter>~<Child Filter>. Example: Basic Information~Department.");
            }
            DlkLabel filterDetail = new DlkLabel("Filter Item", "XPATH", "//span/div/h4[contains(text(), '"+arrFilters[0]+"')]/../..//li[contains(., '"+arrFilters[1]+"')]");
            if (filterDetail.Exists(1))
            {
                String sActualText = "";

                if (filterDetail.GetAttributeValue("innerHTML").Contains("<br>"))
                {
                    String[] arrActualTexts = filterDetail.GetAttributeValue("innerHTML").Split(new String[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    for (int i = 1; i < arrActualTexts.Count(); i++)
                    {
                        if (i > 1)
                        {
                            sActualText = sActualText + "~";
                        }
                        sActualText = sActualText + arrActualTexts[i];
                    }
                }
                else if (filterDetail.GetAttributeValue("innerHTML").Contains("</b>"))
                {
                    String[] arrActualTexts = filterDetail.GetAttributeValue("innerHTML").Split(new String[] { "</b>" }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 1; i < arrActualTexts.Count(); i++)
                    {
                        if (i > 1)
                        {
                            sActualText = sActualText + "~";
                        }
                        sActualText = sActualText + arrActualTexts[i];
                    }
                }
                
                DlkAssert.AssertEqual("VerifyFilter_FiltersContainer", pstrExpectedValue, sActualText);
            }
            else
            {
                throw new Exception("VerifyFilter() failed. Filter '" + pstrFilterPath + "' not found in the filter list.");
            }
        }

        private void VerifyFilter_SelectedBucketContainer(String pstrFilterPath, String pstrExpectedValue)
        {

            DlkBaseControl bucketGroup = new DlkBaseControl("Bucket Group", "XPATH", "//div[@class='selectedBucketGroup']/div[@class='filtersHeaderTitle clearfix']/span[contains(.,'" + pstrFilterPath + "')]/../..");
            if (bucketGroup.Exists(1))
            {
                IList<IWebElement> filterItems = bucketGroup.mElement.FindElements(By.XPath("./div[@class='selectedBucketItem clearfix']"));
                String sActualText = "";
                foreach (IWebElement filterItem in filterItems)
                {
                    if (sActualText != "")
                    {
                        sActualText = sActualText + "~";
                    }
                    DlkBaseControl filterText = new DlkBaseControl("Filter Item", filterItem, "span");
                    sActualText = sActualText + filterText.GetValue();
                }

                DlkAssert.AssertEqual("VerifyFilter_SelectedBucketContainer", pstrExpectedValue, sActualText);
            }
            else
            {
                throw new Exception("VerifyFilter() failed. Filter '" + pstrFilterPath + "' not found in the filter list.");
            }
        }

        private void VerifyFilterExists_FiltersContainer(String pstrFilterPath, String pstrExpectedValue)
        {
            Boolean bFound = false;
            String[] arrFilters = pstrFilterPath.Split('~');
            if (arrFilters.Count() < 2)
            {
                throw new Exception("VerifyFilterExists() failed. Invalid filter path. Syntax: <Parent Filter>~<Child Filter>. Example: Basic Information~Department.");
            }
            DlkLabel filterDetail = new DlkLabel("Filter Item", "XPATH", "//span/div/h4[contains(text(), '" + arrFilters[0] + "')]/../..//li[contains(., '" + arrFilters[1] + "')]");
            if (filterDetail.Exists(1))
            {
                if (arrFilters.Count() > 2)
                {
                    bFound = filterDetail.GetAttributeValue("innerHTML").Contains(arrFilters[2]);
                }
                else
                {
                    bFound = true;
                }

            }
            DlkAssert.AssertEqual("VerifyFilterExists_FiltersContainer", Convert.ToBoolean(pstrExpectedValue), bFound);
        }

        private void VerifyFilterExists_SelectedBucketContainer(String pstrFilterPath, String pstrExpectedValue)
        {
            Boolean bFound = false;
            String[] arrFilters = pstrFilterPath.Split('~');
            DlkBaseControl bucketGroup = new DlkBaseControl("Bucket Group", "XPATH", "//div[@class='selectedBucketGroup']/div[@class='filtersHeaderTitle clearfix']/span[contains(.,'" + arrFilters[0] + "')]/../..");
            if (bucketGroup.Exists(1))
            {

                if (arrFilters.Count() > 1)
                {
                    IList<IWebElement> filterItems = bucketGroup.mElement.FindElements(By.XPath("./div[@class='selectedBucketItem clearfix']"));
                    foreach (IWebElement filterItem in filterItems)
                    {
                        DlkBaseControl filterText = new DlkBaseControl("Filter Item", filterItem, "span");
                        if (filterText.GetValue() == arrFilters[1])
                        {
                            bFound = true;
                            break;
                        }
                    }

                }
                else
                {
                    bFound = true;
                }
            }

            DlkAssert.AssertEqual("VerifyFilterExists_SelectedBucketContainer", Convert.ToBoolean(pstrExpectedValue), bFound);

        }
        #endregion
    }
}

