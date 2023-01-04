using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        #region Constructors
        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private string tabType = null;
        private void Initialize()
        {
            FindElement();
            tabType = GetTabType();
        }
        #region Keywords

        /// <summary>
        /// Selects a tab based on tab caption
        /// </summary>
        /// <param name="TabCaption"></param>
        [Keyword("Select", new String[] { "1|text|Expected Value|Tab1" })]
        public void Select(String TabCaption)
        {
            try
            {
                Initialize();
                IWebElement tab;
                switch (tabType)
                {
                    case "tab":
                        tab = mElement.FindElement(By.XPath(".//a[@role='tab']/span[text()='" + TabCaption + "']"));
                        break;
                    case "mui-Grid":
                        tab = mElement.FindElement(By.XPath(".//*[text()='" + TabCaption + "']"));
                        break;
                    case "fsp-tab": //Tab in file upload inside CompanyPage edit mode
                        tab = mElement.FindElement(By.XPath(".//div[contains(@title,'" + TabCaption + "')]"));
                        break;
                    case "tab-list": //Tab in file upload inside CompanyPage edit mode
                        tab = mElement.FindElement(By.XPath(".//span[contains(@class,'MuiTab-wrapper') and text()='" + TabCaption + "']"));
                        break;
                    default:
                        tab = null;
                        break;
                }
                if (tab == null) throw new Exception("Cannot select Tab: " + TabCaption);
                tab.Click();
                DlkLogger.LogInfo("Select() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }
        #endregion

        private string GetTabType()
        {
            string type = null;
            if (mElement.GetAttribute("class").Contains("fsp-modal__sidebar"))
                type = "fsp-tab";
            else if (mElement.FindElements(By.XPath(".//a[@role='tab']")).FirstOrDefault() != null)
                type = "tab";
            else if (mElement.GetAttribute("role") != null && mElement.GetAttribute("role").Contains("tablist"))
                type = "tab-list";
            else
                type = "mui-Grid";
            return type;
        }
    }
}
