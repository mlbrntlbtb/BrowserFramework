using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        private String mstrLastURL = "";
        private String mstrTabItemsCSS = "div>ul>li>a";
        private String mstrTabItemsXPATH = ".//ul/li/a";
        private List<DlkBaseControl> mlstTabs;


        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public void Initialize()
        {
            if (mstrLastURL != DlkEnvironment.AutoDriver.Url)
            {
                mstrLastURL = DlkEnvironment.AutoDriver.Url;
            }

            mlstTabs = new List<DlkBaseControl>();
            FindElement();
            FindTabs();

        }

        private void FindTabs()
        {
            IList<IWebElement> lstTabElements;
            //lstTabElements = mElement.FindElements(By.CssSelector(mstrTabItemsCSS));
            lstTabElements = mElement.FindElements(By.XPath(mstrTabItemsXPATH));
            foreach (IWebElement tabElement in lstTabElements)
            {
                mlstTabs.Add(new DlkBaseControl("Tab", tabElement));
            }
        }

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1"})]
        public void Select(String TabCaption)
        {
            bool bFound = false;
            String strActualTabs = "";

            Initialize();
            foreach(DlkBaseControl tab in mlstTabs)
            {
                strActualTabs = strActualTabs + tab.GetValue().Trim() + " ";
                if (tab.GetValue().ToLower().Trim().Contains(TabCaption.ToLower()))
                {
                    //String hRef = tab.GetAttributeValue("href");
                    //if (hRef.Contains("javascript"))
                    //{
                    //    DlkEnvironment.CaptureUrl();
                    //}
                    tab.Click(5,5);

                    //if (hRef.Contains("javascript"))
                    //{
                    //    DlkEnvironment.WaitUrlUpdate();
                    //}
                    //if (tab.Exists())
                    //{
                    //    //tab.Click();
                    //    OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                    //    mAction.MoveToElement(tab.mElement).MoveByOffset(5, 5).ClickAndHold().Perform();
                    //    Thread.Sleep(500);
                    //    mAction.Release().Perform();
                    //}
                    bFound = true;
                    break;
                }
            }
            if (bFound)
            {
                DlkLogger.LogInfo("Successfully executed Select(). Control : " + mControlName + " : " + TabCaption);
            }
            else
            {
                throw new Exception("Select() failed. Control : " + mControlName + " : '" + TabCaption + 
                                        "' tab not found. : Actual Tabs = " + strActualTabs);
            }
        }

        private void FindVisibleTabs()
        {
            IList<IWebElement> lstTabElements;
            lstTabElements = mElement.FindElements(By.CssSelector(mstrTabItemsCSS));
            foreach (IWebElement tabElement in lstTabElements)
            {
                if (string.IsNullOrEmpty(tabElement.Text) == false)
                {
                    mlstTabs.Add(new DlkBaseControl("Tab", tabElement));
                }
            }
        }

        #region Verify methods
        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                /*
                Boolean bExists = Exists();

                if (bExists == Convert.ToBoolean(expectedValue))
                {
                    DlkLogger.LogInfo("VerifyExists() passed : Actual = " + Convert.ToString(bExists) + " : Expected = " + expectedValue);
                }
                else
                {
                    throw new Exception("VerifyExists() failed : Actual = " + Convert.ToString(bExists) + " : Expected = " + expectedValue));
                }*/

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

        [RetryKeyword("VerifyTabs", new String[] { "1|text|Tab Names|Header~Other Information~Accouting Defaults" })]
        public void VerifyTabs(String TabNames)
        {
            String tabNames = TabNames;
            Boolean actual = false;
            
            this.PerformAction(() =>
            {
                mlstTabs = new List<DlkBaseControl>();
                FindElement();
                FindVisibleTabs();

                String strTabList = "";
                int itemIdx = 0;
                int elemIdx = mlstTabs.Count - 1;

                foreach (DlkBaseControl tab in mlstTabs)
                {
                    String tabText = "";
                    if (tab.GetAttributeValue("innerHTML").Contains("<span>"))
                    {
                        tabText = tab.GetAttributeValue("innerHTML").Split(new String[] { "<span>" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                    }
                    else
                    {
                        tabText = tab.GetValue().Trim();
                    }
                    itemIdx = mlstTabs.IndexOf(tab);
                    if (itemIdx == elemIdx)
                    {
                        strTabList = strTabList + tabText;
                    }
                    else
                    {
                        strTabList = strTabList + tabText + "~";
                    }

                }

                if (!TabNames.Contains("~"))
                {
                    if (strTabList.Contains(tabNames))
                        actual = true;
                }
                else
                {
                    if (strTabList == tabNames)
                        actual = true;
                }

                DlkAssert.AssertEqual("VerifyTabs():", true, actual);
                
            }, new String[]{"retry"});
        }
        #endregion
    }
}

