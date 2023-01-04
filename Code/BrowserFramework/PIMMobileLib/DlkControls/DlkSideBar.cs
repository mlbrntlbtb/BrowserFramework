using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMMobileLib.DlkControls
{
    [ControlType("SideBar")]
    public class DlkSideBar : DlkBaseControl
    {
        #region Declarations
        
        private List<IWebElement> mItems;

        #endregion

        #region CONSTRUCTORS
        public DlkSideBar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSideBar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSideBar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
            GetMenus();
        }

        #endregion

        #region KEYWORDS

        [Keyword("Select", new String[] { "Home" })]
        public void Select(String Name)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement aMenu in mItems)
                {
                    IWebElement item = aMenu.FindElement(By.Id("menuText"));
                    DlkBaseControl sideBarItem = new DlkBaseControl("SideBarItem", item);
                    if (sideBarItem.GetValue().Trim().ToLower() == Name.Trim().ToLower())
                    {
                        sideBarItem.Tap();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("SideBar item '" + Name + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion

        #region METHODS

        private void GetMenus()
        {
            try
            {
                var mitemList = this.mElement.FindElements(By.XPath(".//*[contains(@resource-id,'menuText')]/ancestor::*[@class='android.widget.LinearLayout']"));
                List<IWebElement> visibleMenus = new List<IWebElement>();
                foreach (IWebElement item in mitemList)
                {
                    if (item.Displayed)
                    {
                        visibleMenus.Add(item);
                    }
                }
                mItems = visibleMenus;
            }
            catch
            {
                //do nothing
            }
        }

        #endregion
    }
}
