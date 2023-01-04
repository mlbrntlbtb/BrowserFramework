using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using CommonLib.DlkUtility;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("SelectionList")]
    public class DlkSelectionList : DlkBaseControl
    {
        #region DECLARATIONS
        private bool _iframeSearchType = false;
        #endregion

        #region CONSTRUCTOR
        public DlkSelectionList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSelectionList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSelectionList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        public void Initialize()
        {
            //support for multiple windows
            if (DlkEnvironment.AutoDriver.WindowHandles.Count > 1)
            {
                DlkEnvironment.AutoDriver.SwitchTo().Window(DlkEnvironment.AutoDriver.WindowHandles[DlkEnvironment.AutoDriver.WindowHandles.Count - 1]);
            }
            else
            //{
            //    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            //}

            if (mSearchType.ToLower().Equals("iframe_xpath"))
            {
                _iframeSearchType = true;
                FindElement();
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
                FindElement();
                this.ScrollIntoViewUsingJavaScript();
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

        #endregion

        #region KEYWORDS
        [Keyword("ActivitySelect", new String[] { "1|text|item|item" })]
        public void ActivitySelect(string Item)
        {
            try
            {
                string xpath = "//div[text()='{0}']";
                Initialize();
                //FindElement();
                xpath = string.Format(xpath, Item); 
                mElement.FindElement(By.XPath(xpath));

                if (mElement != null)
                {
                    mElement.Click();
                    DlkLogger.LogInfo("ActivitySelect(): click successful");
                }
                else
                {
                    throw new Exception("ActivitySelect() element can't be found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ActivitySelect() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        [Keyword("SelectFromList", new String[] { "Organisations|"})]
        public void SelectFromList(String Header, String Item)
        {
            try
            {
                bool bFound = false;
                Initialize();
                List<IWebElement> headerItems = GetItems(Header);

                foreach (IWebElement item in headerItems)
                {
                    DlkBaseControl listItem = new DlkBaseControl("ListItem", item);
                    if (listItem.GetValue().Trim().ToLower() == Item.Trim().ToLower())
                    {
                        listItem.Click();
                        bFound = true;
                        Thread.Sleep(5000);
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("List item '" + Item + "' under " + Header + " not found");
                }
                else
                {
                    DlkLogger.LogInfo("SelectFromList() - " + "List item '" + Item + "' under " + Header + " passed");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectFromList() failed : " + e.Message, e);
            }
            finally
            {
                Terminate();
            }
        }

        #endregion

        #region METHODS
        private List<IWebElement> GetItems(String Header)
        {
            List<IWebElement> headerItems = new List<IWebElement>();
            try
            {
                var header = this.mElement.FindElement(By.XPath(".//dt[.='" + Header + "']"));
                if (header != null)
                {
                    var mitemList = header.FindElements(By.XPath("./ancestor::dl/dd"));
                    List<IWebElement> items = new List<IWebElement>();
                    foreach (IWebElement item in mitemList.Where(x => x.Displayed))
                    {
                        headerItems.Add(item);
                    }
                }
                else
                {
                    throw new Exception("Header - " + Header + " not found.");
                }
            }
            catch(Exception ex)
            {
                var err = ex.ToString();
                //do nothing
            }
            return headerItems;
        }
        #endregion


    }
}
