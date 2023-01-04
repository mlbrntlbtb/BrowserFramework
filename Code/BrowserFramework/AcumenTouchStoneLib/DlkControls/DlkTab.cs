using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;
using System.Collections.Generic;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        private String mstrTabItemsXPath = ".//li/a";
        private List<DlkBaseControl> mlstTabs;

        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            mlstTabs = mElement.FindElements(By.XPath(mstrTabItemsXPath))
                .Select(y => new DlkBaseControl("Tab", y))
                .Where(y => (String.IsNullOrEmpty(y.GetValue())) == false).ToList();
        }

        [Keyword("Select", new String[] { "1|text|Tab Caption|Tab1" })]
        public void Select(String Item)
        {
            bool bFound = false;
            String strActualTabs = "";
            try
            {
                Initialize();
                foreach (DlkBaseControl tab in mlstTabs)
                {
                    DlkLogger.LogInfo(tab.GetValue());
                    strActualTabs = strActualTabs + tab.GetValue().Trim() + " ";
                    if (tab.GetValue().Trim().ToLower() == Item.ToLower())
                    {
                        if (!tab.Exists(1))
                        {
                            tab.ScrollIntoViewUsingJavaScript();
                        }
                        tab.Click();
                        DlkLogger.LogInfo("Successfully executed Select() : " + mControlName + " = " + Item);
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception(mControlName + " = '" + Item + "' tab not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTabItemExists", new String[] { "1|text|Tab Caption|Tab1" })]
        public void VerifyTabItemExists(String Item, String ExpectedValue)
        {
            bool ActualValue = false;
            try
            {
                Initialize();
                foreach (DlkBaseControl tab in mlstTabs)
                {
                    if (tab.GetValue().Trim().ToLower() == Item.ToLower())
                    {
                        if (tab.mElement.Displayed)
                        {
                            ActualValue = true;
                        }
                        break;
                    }
                }
                DlkAssert.AssertEqual("TabItem", Convert.ToBoolean(ExpectedValue), ActualValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabItemExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySelectedTab", new String[] { "1|text|Tab Names|Header~Other Information~Accouting Defaults" })]
        public void VerifySelectedTab(String StringToVerify)
        {
            try
            {
                Initialize();

                mlstTabs = mlstTabs.Where((tab) => !tab.GetAttributeValue("style").Replace(" ", "").Contains("display:none")).ToList();
                DlkBaseControl mTab = mlstTabs.Where((tab) => tab.mElement.Text == StringToVerify).First();
                IWebElement tabActiveElement = mTab.GetParent();
                string tabActive = tabActiveElement.GetAttribute("class");
                if (tabActive == "active")
                {
                    DlkLogger.LogInfo("VerifySelectedTab() passed - " + StringToVerify + " is currently selected");
                    return;
                }
                throw new Exception("VerifySelectedTab() failed - " + StringToVerify + " not currently selected");

            }
            catch (Exception e)
            {
                throw new Exception("VerifySelectedTab() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "True" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
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
    }
}
