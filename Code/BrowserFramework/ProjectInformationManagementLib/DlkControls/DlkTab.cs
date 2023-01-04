using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using ProjectInformationManagementLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("Tab")]
    public class DlkTab : DlkProjectInformationManagementBaseControl
    {
        #region DECLARATIONS
        private List<IWebElement> tabItems;
        private bool _iframeSearchType = false;
        #endregion

        #region CONSTRUCTOR
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
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            }

            if (mSearchType.ToLower().Equals("iframe_xpath"))
            {
                _iframeSearchType = true;
                FindElement();
                DlkEnvironment.mSwitchediFrame = true;
                GetItems();
            }
            else
            {
                _iframeSearchType = false;
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
                GetItems();
            }
        }

        public void Terminate()
        {
            if (_iframeSearchType)
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                DlkEnvironment.mSwitchediFrame = false;
            }
        }

        public void InitializeRow(string RowNumber)
        {
            InitializeSelectedElement(RowNumber);
        }

        #endregion

        #region KEYWORDS
        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement tab in tabItems)
                {
                    DlkBaseControl tabItem = new DlkBaseControl("MenuItem", tab);
                    if (tabItem.GetValue().Trim().ToLower() == Item.Trim().ToLower())
                    {
                        tabItem.Click(4.5);
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Tab - '" + Item + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
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
            finally
            {
                Terminate();
            }
        }
        #endregion

        #region METHODS
        private List<IWebElement> GetItems()
        {
            try
            {
                var tabs = this.mElement.FindElements(By.XPath(".//ul[@id='lbMainTabs']/li"));
                List<IWebElement> tabList = new List<IWebElement>();
                foreach (IWebElement item in tabs)
                {
                    if (item.Displayed)
                    {
                        tabList.Add(item);
                    }
                }
                tabItems = tabList;
            }
            catch
            {
                //do nothing
            }
            return tabItems;
        }
        #endregion


    }
}
