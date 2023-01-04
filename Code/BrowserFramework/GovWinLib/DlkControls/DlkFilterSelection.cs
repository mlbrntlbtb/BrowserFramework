using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("FilterSelection")]
    public class DlkFilterSelection : DlkBaseControl
    {
        public DlkFilterSelection(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkFilterSelection(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkFilterSelection(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        
        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Select", new String[] { "1|text|Filter Path|Contractor~HARRIS CORPORATION" })]
        public void Select(String FilterPath)
        {
            String[] filters = FilterPath.Split('~');
            if (filters.Count() != 2)
            {
                throw new Exception("Select() failed. Invalid filter path. Syntax: <Filter Group>~<Filter Value>. Example: Contractor~HARRIS CORPORATION");
            }

            DlkBaseControl filterGroup = new DlkBaseControl("Filter Group", "XPATH", ".//div[@class='filtersContainer']//span[@class='CesFacetHeaderLabel'][contains(text(),'" + filters[0] + "')]");
            if (filterGroup.Exists(1))
            {
                IList<IWebElement> filterItems = filterGroup.mElement.FindElements(By.XPath(".//ancestor::div[(contains(@class,'CesFacetBackground'))]//div[(contains(@class,'CesFacetNormalLine CesFacetNormalLine_'))]"));
                Boolean bFound = false;
                foreach (IWebElement filterItem in filterItems)
                {
                    DlkBaseControl filterText = new DlkBaseControl("Filter Item", filterItem, "span");
                    if (filterText.GetValue().Contains(filters[1]))
                    {
                        DlkEnvironment.CaptureUrl();
                        DlkBaseControl filter = new DlkBaseControl("Filter '" + filters[1] + "'", filterItem);
                        filter.Click();
                        bFound = true;
                        DlkEnvironment.WaitUrlUpdate();
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Select() failed. Filter Item '" + filters[1] + "' not found in the filter selection control.");                    
                }
            }
            else
            {
                throw new Exception("Select() failed. Filter Group'" + filters[0] + "' not found in the filter selection control.");
            }
        }

        #region Verify methods
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

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyFilterGroupExists", new String[] { "1|text|Filter Group|Filter Group",
                                                                 "2|text|Expected Value (TRUE or FALSE)|TRUE" })]
        public void VerifyFilterGroupExists(String FilterGroup, String TrueOrFalse)
        {
            this.PerformAction(() =>
            {
                String sActualResults = "false";
                String sExpectedResults = TrueOrFalse.ToLower();
                DlkBaseControl filterGroup = new DlkBaseControl("Filter Group", "XPATH", ".//div[@class='filtersContainer']//span[@class='CesFacetHeaderLabel'][contains(text(),'" + FilterGroup + "')]");

                if (filterGroup.Exists(1))
                    sActualResults = "true";

                DlkAssert.AssertEqual("VerifyFilterGroupExists()", sExpectedResults, sActualResults);
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyFilterItemExists", new String[] { "1|text|Filter Group|Filter Group",
                                                                 "2|text|Filter Item|Filter Item",
                                                                 "3|text|Expected Value (TRUE or FALSE)|TRUE" })]
        public void VerifyFilterItemExists(String FilterGroup, String FilterItem, String TrueOrFalse)
        {
            String sActualResults = "false";
            String sExpectedResults = TrueOrFalse.ToLower();
            //DlkBaseControl filterGroup = new DlkBaseControl("Filter Group", "XPATH", ".//div[@class='filtersContainer']//span[@class='CesFacetHeaderLabel'][contains(text(),'" + FilterGroup + "')]");
            DlkBaseControl filterGroup = new DlkBaseControl("Filter Group", "XPATH", ".//div[@class='filtersContainer']//span[contains(text(),'" + FilterGroup + "')]//ancestor::span");
            this.PerformAction(() =>
                {
                    if (filterGroup.Exists(1))
                    {
                        /*
                        IList<IWebElement> filterItems = filterGroup.mElement.FindElements(By.XPath(".//ancestor::div[(contains(@class,'CesFacetBackground'))]//div[(contains(@class,'CesFacetNormalLine CesFacetNormalLine_'))]"));

                        foreach (IWebElement filterItem in filterItems)
                        {
                            DlkBaseControl filterText = new DlkBaseControl("Filter Item", filterItem, "span");
                            if (filterText.GetValue().Contains(FilterItem))
                            {
                                sActualResults = "true";
                                break;
                            }
                        }
                         */
                        String actualFilterItem = filterGroup.mElement.FindElement(By.XPath("./label")).Text;
                        if(actualFilterItem.Contains(FilterItem))
                        {
                            sActualResults = "true";                            
                        }
                    }

                    DlkAssert.AssertEqual("VerifyFilterItemExists()", sExpectedResults, sActualResults);
                }, new String[] { "retry" });
        }
        #endregion
    }
}

