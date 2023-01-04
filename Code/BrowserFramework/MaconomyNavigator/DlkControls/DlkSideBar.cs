using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace MaconomyNavigatorLib.DlkControls
{
    [ControlType("SideBar")]
    public class DlkSideBar : DlkBaseControl
    {
        public DlkSideBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {

            FindElement();
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

        [Keyword("Select", new String[] { "1|text|Tab|Opportunity" })]
        public void Select(String Value)
        {
            Boolean bFound = false;
            try
            {
                Initialize();
                IWebElement targetTab = mElement;

                //old method
                //IList<IWebElement> tabs = this.mElement.FindElements(By.CssSelector(mTabItemsCSS));
                //for (int i = 0; i < tabs.Count; i++)
                //{
                //    if (tabs[i].GetAttribute("title").ToLower() == Value.ToLower())
                //    {
                //        DlkBaseControl ctlTab = new DlkBaseControl("TabItem", tabs[i]);
                //        ctlTab.Click();
                //        bFound = true;
                //        DlkLogger.LogInfo("Select() passed.");
                //        break;
                //    }
                //}
                if (Value.ToLower().Contains("time sheet"))
                    {
                        
                        if (Value.ToLower().Contains("daily"))
                        {
                            targetTab = this.mElement.FindElement(By.XPath("./descendant::a[contains(@class,'icon-uniE604')]"));
                            bFound = true;
                        }else{
                            targetTab = this.mElement.FindElement(By.XPath("./descendant::a[contains(@class,'icon-uniE007')]"));
                            bFound = true;
                        }
                    }
                if (Value.ToLower().Contains("expense"))
                {
                    targetTab = this.mElement.FindElement(By.XPath("./descendant::a[contains(@title,'Expense')]"));
                    bFound = true;
                }
                if (Value.ToLower().Contains("mileage"))
                {
                    targetTab = this.mElement.FindElement(By.XPath("./descendant::a[contains(@title,'Mileage')]"));
                    bFound = true;
                }
                if (Value.ToLower().Contains("favorite"))
                {
                    targetTab = this.mElement.FindElement(By.XPath("./descendant::a[contains(@title,'Favorite')]"));
                    bFound = true;
                }
                if (Value.ToLower().Contains("absence"))
                {
                    targetTab = this.mElement.FindElement(By.XPath("./descendant::a[contains(@title,'Absence')]"));
                    bFound = true;
                }
                if (bFound)
                {
                    DlkBaseControl ctlTab = new DlkBaseControl("TabItem", targetTab);
                    ctlTab.Click();
                    DlkLogger.LogInfo("Select() passed.");
                }
                else
                {
                    throw new Exception("Select() failed. '" + Value + "' not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }




    }
}
