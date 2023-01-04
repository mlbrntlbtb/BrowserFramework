using System; 
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Collections.Generic;
using System.Linq;

namespace SBCLib.DlkControls
{
    [ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        #region Constructors
        public DlkMenu(String ControlName, String SearchType, String SearchValue)
           : base(ControlName, SearchType, SearchValue) { }
        public DlkMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region Declarations
        private IList<IWebElement> lstMenuItems = null;
        private IList<IWebElement> lstSubMenuItems = null;
        private const string mTopMenuXpath = ".//li[contains(@class,'top_menu')]/a";
        private const string mChildXpath = ".//following-sibling::ul[contains(@class,'child_menu')]";
        private const string mBreadNavLevel1 = ".//*[contains(@class,'mp-level')][@data-level='1']";
        private const string mBreadNavLevel2 = ".//*[contains(@class,'mp-level')][@data-level='2']";

        #endregion
        public void Initialize()
        {
            FindElement();
            FindMenuItems();
        }

        #region Keywords
        
        /// <summary>
        /// Verifies if control exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
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

        /// <summary>
        /// Verifies if control is readonly. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="ExpectedValue"></param>
        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                DlkAssert.AssertEqual("VerifyReadOnly() : ", TrueOrFalse.ToLower(), base.IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Selects a menu item given the menu path.
        /// </summary>
        /// <param name="MenuPath"></param>
        [Keyword("SelectMenuItem", new String[] { "1|text|Expected Value|TRUE" })]
        public void SelectMenuItem(String MenuPath)
        {
            try
            {
                Initialize();
                IWebElement menuItem = GetMenuItem(MenuPath);
                new DlkBaseControl("MenuItem", menuItem).Click();
                DlkLogger.LogInfo("SelectMenuItem() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SelectMenuItem() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if control is readonly. Requires MenuPath and TrueOrFalse - can either be True or False.  
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyItemReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemReadOnly(String MenuPath, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement menuItem = GetMenuItem(MenuPath);
                DlkAssert.AssertEqual("VerifyItemReadOnly() : ", TrueOrFalse.ToLower(), new DlkBaseControl("Item", menuItem).IsReadOnly().ToLower());
                DlkLogger.LogInfo("VerifyItemReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemReadOnly() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Verifies if control exists. Requires MenuPath and TrueOrFalse - can either be True or False.  
        /// </summary>
        /// <param name="TrueOrFalse"></param>
        [Keyword("VerifyItemExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemExists(String MenuPath, String TrueOrFalse)
        {
            try
            {
                Initialize();
                IWebElement menuItem = GetMenuItem(MenuPath, true);
                new DlkBaseControl("Item", menuItem).VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyItemExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private void FindMenuItems()
        {
            lstMenuItems = mElement.FindElements(By.XPath(mTopMenuXpath)).Count > 0 ?
                mElement.FindElements(By.XPath(mTopMenuXpath)).ToList() :
                mElement.FindElements(By.XPath(mBreadNavLevel1)).First().FindElements(By.TagName("li")).ToList();
        }

        private void FindChildMenuItems(IWebElement menuItem)
        {
            lstSubMenuItems = menuItem.FindElements(By.XPath(mChildXpath)).Count > 0 ?
                menuItem.FindElement(By.XPath(mChildXpath)).FindElements(By.TagName("li")).ToList() :
                menuItem.FindElements(By.XPath(mBreadNavLevel2)).First().FindElements(By.TagName("li")).ToList();
        }

        private IWebElement GetMenuItem(string MenuItemPath, Boolean IsVerifyExists = false)
        {
            string[] mItems = MenuItemPath.Split('~');
            IWebElement mainItem = null;
            IWebElement menuItem = null;
            if (mItems.Count() == 0)
            {
                throw new Exception("Blank input.");
            }

            for (int i = 0; i < mItems.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        mainItem = mItems[i].ToLower().Equals("back") ? mElement.FindElements(By.ClassName("mp-back")).Where(x => x.Displayed).FirstOrDefault() : 
                                    lstMenuItems.Where(x => x.Text.Equals(mItems[i])).FirstOrDefault();
                        menuItem = mainItem;
                        break;
                    case 1:
                        FindChildMenuItems(mainItem);
                        IWebElement mSubItem = mItems[i].ToLower().Equals("back") ? mElement.FindElements(By.ClassName("mp-back")).Where(x => x.Displayed).FirstOrDefault() :
                                                lstSubMenuItems.Where(x => x.Text.Equals(mItems[i])).FirstOrDefault();
                        menuItem = mSubItem;
                        break;
                    default:
                        throw new Exception("Menu level currently unsupported.");
                }
                //Check if item was found. Throw an exception if this isn't verifyexist
                if (menuItem == null)
                {
                    string message = $"MenuItem [{ mItems[i] }] not found.";
                    if (IsVerifyExists)
                    {
                        DlkLogger.LogInfo(message);
                    }
                    else
                    {
                        throw new Exception(message);
                    }
                }
                //Attempt click if menu has a submenu
                if ( i != (mItems.Length - 1))
                {
                    new DlkBaseControl("MenuItem", menuItem).Click();
                    DlkLogger.LogInfo(mItems[i] + " clicked");
                }
            }
            return menuItem;
        }
        #endregion
    }
 }

