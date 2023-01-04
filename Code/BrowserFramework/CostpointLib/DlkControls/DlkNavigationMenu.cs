using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("NavigationMenu")]
    public class DlkNavigationMenu : DlkBaseControl
    {
        private DlkNavigationArea mNavArea;
        private String mStrBusNavAreaDesc = "//div[@id='busArea']";
        private String mStrBusMemberClass = "//div[@id='busArea']/div[@class='busItem']";
        //private String mStrBusMemberClass = "//div[@class='busItem']";
        private String mStrDeptNavAreaDesc = "//div[@id='deptArea']";
        private String mStrDeptMemberClass = "//div[@class='deptItem']";
        private String mStrWorkNavAreaDesc = "//div[@id='workArea']";
        private String mStrWorkMemberClass = "//div[@id='workArea']/div[@class='navItem']";
        private String mStrActivityNavAreaDesc = "//div[@id='activityArea']";
        private String mStrActivityMemberClass = "//div[@id='activityArea']/div[@class='navItem']";

        public DlkNavigationMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavigationMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavigationMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkButton btnBrowse = new DlkButton("Browse", "ID", "goToLbl");
            int countLimit = 7; // Exists() call is default at 2000 ms, loop has max runtime of approx 15000 ms
         
            for (int i = 0; i < countLimit; i++)
            {
                if (Exists())
                {
                    DlkLogger.LogInfo("Navigation Menu found.");
                    return;
                }
                DlkLogger.LogInfo("Navigation Menu not found. Clicking Browse Applications...");
                btnBrowse.Click();
            }
            throw new Exception("Navigation Menu not found.");
        }

        [Keyword("SelectMenu", new String[] { "1|text|MenuPath|Accounting~General Ledger~Accounts~Print Accounts List" })]
        public void SelectMenu(String sMenuPath)
        {
            String[] menuItems = sMenuPath.Split('~');
            try
            {
                if (menuItems.Length <= 4)
                {
                    Initialize();
                    for (int i = 0; i < menuItems.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                mNavArea = new DlkNavigationArea("BusinessArea", "xpath", mStrBusNavAreaDesc);
                                mNavArea.MemberItemClass = mStrBusMemberClass;
                                break;
                            case 1:
                                mNavArea = new DlkNavigationArea("DepartmentArea", "xpath", mStrDeptNavAreaDesc);
                                mNavArea.MemberItemClass = mStrDeptMemberClass;
                                break;
                            case 2:
                                mNavArea = new DlkNavigationArea("WorkArea", "xpath", mStrWorkNavAreaDesc);
                                mNavArea.MemberItemClass = mStrWorkMemberClass;
                                break;
                            case 3:
                                mNavArea = new DlkNavigationArea("ActivityArea", "xpath", mStrActivityNavAreaDesc);
                                mNavArea.MemberItemClass = mStrActivityMemberClass;
                                break;
                        }
                        mNavArea.Select(menuItems[i]);
                    }
                }
                else
                {
                    throw new Exception("Menu path count is invalid : Menu Path = '" + sMenuPath + "'");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectMenu() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyMenu", new String[] { "1|text|Menu Path|Accounting~General Ledger~Accounts~Print Accounts List",
                                              "2|text|Expected Value|TRUE" })]
        public void VerifyMenu(String MenuPath, String ExpectedValue)
        {
            String[] lstMenuItems = MenuPath.Split('~');
            bool bIsFound = false;

            try
            {
                if (lstMenuItems.Length < 1 || lstMenuItems.Length > 4)
                {
                    throw new Exception("Menu path count is invalid : Menu Path = '" + MenuPath + "'");
                }
                else
                {
                    Initialize();
                    for (int i = 0; i < lstMenuItems.Length; i++)
                    {
                        bool bSelectItem = true;
                        switch (i)
                        {
                            case 0:
                                mNavArea = new DlkNavigationArea("BusinessArea", "xpath", mStrBusNavAreaDesc);
                                mNavArea.MemberItemClass = mStrBusMemberClass;
                                break;
                            case 1:
                                mNavArea = new DlkNavigationArea("DepartmentArea", "xpath", mStrDeptNavAreaDesc);
                                mNavArea.MemberItemClass = mStrDeptMemberClass;
                                break;
                            case 2:
                                mNavArea = new DlkNavigationArea("WorkArea", "xpath", mStrWorkNavAreaDesc);
                                mNavArea.MemberItemClass = mStrWorkMemberClass;
                                break;
                            case 3: // do not select last nav area item to retain current view
                                mNavArea = new DlkNavigationArea("ActivityArea", "xpath", mStrActivityNavAreaDesc);
                                mNavArea.MemberItemClass = mStrActivityMemberClass;
                                bSelectItem = false;
                                break;
                        }
                        if (mNavArea.VerifyNode(lstMenuItems[i]))
                        {
                            bIsFound = true;
                            if (bSelectItem)
                            {
                                mNavArea.Select(lstMenuItems[i]);
                            }
                        }
                        else
                        {
                            DlkLogger.LogInfo("VerifyMenu(): Menu item = '" + lstMenuItems[i] + "' not found.");
                            bIsFound = false;
                            break;
                        }
                    }
                    DlkLogger.LogInfo("VerifyMenu(): Menu Path = '" + MenuPath + "' found.");
                    DlkAssert.AssertEqual("VerifyMenu()", Convert.ToBoolean(ExpectedValue), bIsFound);
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenu() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

    }
}
