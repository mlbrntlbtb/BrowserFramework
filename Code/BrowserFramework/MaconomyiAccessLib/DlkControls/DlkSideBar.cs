using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("SideBar")]
    public class DlkSideBar : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private List<DlkBaseControl> mSideBarTabList;
        private List<IWebElement> mSideBarButtons;

        #endregion

        #region CONSTRUCTORS
        public DlkSideBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            FindSidebarItems();
            ExpandSidebarButtons();
        }
        #endregion

        #region KEYWORDS

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
            try
            {
                Initialize();

                Boolean sFound = false;

                foreach (DlkBaseControl sideBarItem in mSideBarTabList)
                {
                    string ActualValue = DlkString.ReplaceCarriageReturn(sideBarItem.GetValue(), "").Trim();
                    if (ActualValue.ToLower() == Value.ToLower().Trim())
                    {
                        sFound = true;
                        sideBarItem.ScrollIntoViewUsingJavaScript();
                        sideBarItem.Click();
                        DlkLogger.LogInfo("Select() passed.");
                        break;
                    }
                }

                if (!sFound)
                    throw new Exception("Sidebar tab not found.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains", new String[] { "1|text|Tab|Opportunity" })]
        public void SelectContains(String Value)
        {
            try
            {
                Initialize();

                Boolean sFound = false;

                foreach (DlkBaseControl sideBarItem in mSideBarTabList)
                {
                    string ActualValue = DlkString.ReplaceCarriageReturn(sideBarItem.GetValue(), "").Trim();
                    if (ActualValue.ToLower().Contains(Value.ToLower().Trim()))
                    {
                        sFound = true;
                        sideBarItem.ScrollIntoViewUsingJavaScript();
                        sideBarItem.Click();
                        DlkLogger.LogInfo("SelectContains() passed.");
                        break;
                    }
                }

                if (!sFound)
                    throw new Exception("Sidebar tab not found.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

        #region PRIVATE METHODS
        private void FindSidebarItems()
        {
            mSideBarTabList = mElement.FindElements(By.TagName("a"))
                .Select(x => new DlkBaseControl("Tab", x))
                .Where(x => (String.IsNullOrEmpty(x.GetValue())) == false).ToList();
        }

        private void ExpandSidebarButtons()
        {
            string buttonCollapse_XPath = "//i[contains(@class,'chevron-down')]";
            if(DlkEnvironment.AutoDriver.FindElements(By.XPath(buttonCollapse_XPath)).Count > 0)
            {
                DlkLogger.LogInfo("Expanding all sidebar menu buttons... ");
                mSideBarButtons = DlkEnvironment.AutoDriver.FindElements(By.XPath(buttonCollapse_XPath)).Where(x => x.Displayed).ToList();
                foreach(var sidebarButton in mSideBarButtons)
                {
                    sidebarButton.Click();
                    Thread.Sleep(1000);
                }
            }
        }
        #endregion
    }
}
