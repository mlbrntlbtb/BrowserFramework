using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Support.UI;

namespace MaconomyiAccessLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkBaseControl
    {
        #region PRIVATE VARIABLES
        private Boolean IsInit = false;
        private string mTabClass;
        private List<DlkBaseControl> mTabList;
        private int mTabMatch;
        private const String DXTC_CLASS = "dxtc";
        #endregion

        #region CONSTANTS

        private const string NotifCountXpath = ".//parent::a//span[contains(@class,'badge-danger')]";

        #endregion

        #region CONSTRUCTORS
        public DlkTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTab(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                mTabClass = GetTabClass();
                FindTabItems();
                IsInit = true;
            }
        }
        #endregion

        #region KEYWORDS
        [Keyword("VerifyNotificationCount", new String[] { "1|text|Count|1" })]
        public void VerifyNotificationCount(String PartialText, String Count)
        {
            try
            {
                int count;
                if (!int.TryParse(Count, out count))
                    throw new Exception("VerifyNotificationCount() : Parameter supplied [Count] is not a valid input number");

                Initialize();
                CountMatchTabs(PartialText);

                if (mTabMatch == 1)
                {
                    
                    foreach (DlkBaseControl tab in mTabList)
                    {
                        if (tab.GetValue().Trim().ToLower().Contains(PartialText.ToLower()))
                        {
                            tab.ScrollIntoViewUsingJavaScript();
                            DlkBaseControl notif = new DlkBaseControl("Notification", tab, "XPATH", NotifCountXpath);
                            if (notif.Exists(1))
                                DlkAssert.AssertEqual("VerifyNotificationCount(): ", Count, notif.GetValue().Trim());
                            else
                                DlkAssert.AssertEqual("VerifyNotificationCount(): ", Count, "0");
                            break;
                        }
                    }
                }
                else if (mTabMatch > 1)
                    throw new Exception(PartialText + " text item returns multiple results.");
                else
                    throw new Exception(PartialText + " tab not found.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyNotificationCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetNotificationCount", new String[] { "1|text|Count|1" })]
        public void GetNotificationCount(String PartialText, String VariableName)
        {
            try
            {
                Initialize();
                CountMatchTabs(PartialText);

                if (mTabMatch == 1)
                {
                    foreach (DlkBaseControl tab in mTabList)
                    {
                        if (tab.GetValue().Trim().ToLower().Contains(PartialText.ToLower()))
                        {
                            tab.ScrollIntoViewUsingJavaScript();
                            DlkBaseControl notif = new DlkBaseControl("Notification", tab, "XPATH", NotifCountXpath);
                            if (notif.Exists(1))
                            {
                                string val = notif.GetValue().Trim();
                                DlkVariable.SetVariable(VariableName, val);
                                DlkLogger.LogInfo("Notification Value: [" + val + "] stored to Variable: [" + VariableName + "]");
                                DlkLogger.LogInfo("GetNotificationCount() passed");
                                break;
                            }
                            else
                                throw new Exception(PartialText + " tab has no notification value.");
                        }
                    }
                }
                else if (mTabMatch > 1)
                    throw new Exception(PartialText + " text item returns multiple results.");
                else
                    throw new Exception(PartialText + " tab not found.");
            }
            catch (Exception e)
            {
                throw new Exception("GetNotificationCount() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectPartialTextInList", new String[] { "1|text|Item|text" })]
        public void SelectPartialTextInList(String PartialText)
        {
            try
            {
                Initialize();
                CountMatchTabs(PartialText);

                if (mTabMatch == 1)
                {
                    foreach (DlkBaseControl tab in mTabList)
                    {
                        if (tab.GetValue().Trim().ToLower().Contains(PartialText.ToLower()))
                        {
                            switch (mTabClass)
                            {
                                case DXTC_CLASS: //Modify if element is under DXTC TAB CLASS only
                                    tab.mElement.Click();
                                    break;
                                default:
                                    if (!tab.Exists(1))
                                        tab.ScrollIntoViewUsingJavaScript();
                                    tab.Click();
                                    break;
                            }
                           
                            DlkLogger.LogInfo("Successfully executed SelectPartialTextInList() : " + mControlName + " = " + PartialText);
                            break;
                        }
                    }
                }
                else if (mTabMatch > 1)
                    throw new Exception("[" + PartialText + "] text item returns multiple results.");
                else
                    throw new Exception("[" + PartialText + "] tab not found.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectPartialTextInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTabs", new String[] { "1|text|Items|text~text~text" })]
        public void VerifyTabs(String TabItems)
        {
            try
            {
                Initialize();
                string ActualTabsValue = "";
                int itemId = 0;
                int elemId = mTabList.Count - 1;

                foreach(DlkBaseControl tab in mTabList)
                {
                    itemId = mTabList.IndexOf(tab);
                    if (itemId == elemId)
                        ActualTabsValue = ActualTabsValue + tab.GetValue().Trim();
                    else
                        ActualTabsValue = ActualTabsValue + tab.GetValue() + "~";
                }

                DlkAssert.AssertEqual("VerifyTabs()", TabItems.ToLower(), ActualTabsValue.ToLower());
                DlkLogger.LogInfo("VerifyTabs() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTabs() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyPartialTextInList", new String[] { "1|text|Item|text" })]
        public void VerifyPartialTextInList(String PartialText, String TrueOrFalse)
        {
            try
            {
                Initialize();

                bool trueOrfalse;
                if (!Boolean.TryParse(TrueOrFalse, out trueOrfalse))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                CountMatchTabs(PartialText, false);
                bool ActualResult = false;

                if (mTabMatch == 1)
                    ActualResult = true;
                else if (mTabMatch > 1)
                    DlkLogger.LogInfo("[" + PartialText + "] text item returns multiple results.");
                else
                    DlkLogger.LogInfo("[" + PartialText + "] tab not found.");

                DlkAssert.AssertEqual("VerifyPartialTextInList(): ", Convert.ToBoolean(TrueOrFalse), ActualResult);
                DlkLogger.LogInfo("Successfully executed VerifyPartialTextInList() : " + mControlName + " has " + PartialText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPartialTextInList() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactTabs", new String[] { "1|text|Items|text~text~text" })]
        public void VerifyExactTabs(String TabItems)
        {
            try
            {
                Initialize();
                string ActualTabsValue = "";
                int itemId = 0;
                int elemId = mTabList.Count - 1;

                foreach (DlkBaseControl tab in mTabList)
                {
                    itemId = mTabList.IndexOf(tab);
                    if (itemId == elemId)
                        ActualTabsValue = ActualTabsValue + tab.GetValue().Trim();
                    else
                        ActualTabsValue = ActualTabsValue + tab.GetValue() + "~";
                }

                DlkAssert.AssertEqual("VerifyExactTabs()", TabItems, ActualTabsValue);
                DlkLogger.LogInfo("VerifyExactTabs() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactTabs() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExactPartialTextInList", new String[] { "1|text|Item|text" })]
        public void VerifyExactPartialTextInList(String PartialText, String TrueOrFalse)
        {
            try
            {
                Initialize();

                bool trueOrfalse;
                if (!Boolean.TryParse(TrueOrFalse, out trueOrfalse))
                    throw new Exception("[" + TrueOrFalse + "] is not a valid input for parameter TrueOrFalse.");

                CountMatchTabs(PartialText, true);
                bool ActualResult = false;

                if (mTabMatch == 1)
                    ActualResult = true;
                else if (mTabMatch > 1)
                    DlkLogger.LogInfo("[" + PartialText + "] text item returns multiple results.");
                else
                    DlkLogger.LogInfo("[" + PartialText + "] tab not found.");

                DlkAssert.AssertEqual("VerifyExactPartialTextInList(): ", Convert.ToBoolean(TrueOrFalse), ActualResult);
                DlkLogger.LogInfo("Successfully executed VerifyExactPartialTextInList() : " + mControlName + " has " + PartialText);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExactPartialTextInList() failed : " + e.Message, e);
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

        private string GetTabClass()
        {
            string tabClass = mElement.GetAttribute("class") != null ?
                mElement.GetAttribute("class").ToString().ToLower() : string.Empty;

            //Identify table class
            if (tabClass.Contains(DXTC_CLASS))
                tabClass = DXTC_CLASS;

            return tabClass;
        }

        private void FindTabItems()
        {
            string tabItemsXPath;

            switch (mTabClass)
            {
                case DXTC_CLASS:
                    tabItemsXPath = ".//li[contains(@class,'dxtc-activeTab')]";
                    break;
                default:
                    tabItemsXPath = ".//li/a";
                    break;
            }

            mTabList = mElement.FindElements(By.XPath(tabItemsXPath))
                   .Select(x => new DlkBaseControl("Tab", x))
                   .Where(x => (String.IsNullOrEmpty(x.GetValue())) == false).ToList();
        }

        private void CountMatchTabs(String Item, Boolean CaseSensitive = false)
        {
            mTabMatch = 0;
            foreach (DlkBaseControl tab in mTabList)
            {
                string tabText = tab.GetValue();
                DlkLogger.LogInfo(tabText);

                if (CaseSensitive)
                {
                    if (tabText.Trim().Contains(Item))
                        mTabMatch++;
                }
                else
                {
                    if (tabText.Trim().ToLower().Contains(Item.ToLower()))
                        mTabMatch++;
                }
            }
        }
        #endregion
    }
}
