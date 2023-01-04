using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;

namespace ProjectInformationManagementLib.DlkControls
{
    [ControlType("TreeView")]
    public class DlkTreeView : DlkBaseControl
    {
        private bool _iframeSearchType = false;
        private readonly String mXPathElementExpander = ".//div/div[contains(text(), '{0}')]/preceding::button[@class='treeExpander']";
        private readonly String mXPathElementToSelect = ".//div/div[contains(text(), '{0}')]/../preceding-sibling::button";
        private readonly String mXPathElementToExpand = ".//div/div[contains(text(), '{0}')]/ancestor::li[1]";

        public DlkTreeView(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTreeView(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTreeView(String ControlName, IWebElement ExistingWebElement)
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

            if (mSearchType.ToLower().Contains("iframe"))
            {
                _iframeSearchType = true;
                DlkEnvironment.mSwitchediFrame = true;
            }
            else
            {
                _iframeSearchType = false;
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

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
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

        [Keyword("Select", new String[] { "1|text|Path|Path1~Path2~Path3" })]
        public void Select(String Path)
        {
            try
            {
                Initialize();
                FindElement();
                String[] arrPath = Path.Split('~');
                string tXPath = string.Empty;

                for (int i = 0; i < arrPath.Count(); i++)
                {
                    if (i == arrPath.Count() - 1)
                    {
                        tXPath = string.Format(mXPathElementToSelect, arrPath[i]);
                        DlkBaseControl currentNode = new DlkBaseControl("CurrentNode", "xpath", tXPath);
                        currentNode.Click();
                    }
                    else
                    {
                        tXPath = string.Format(mXPathElementToExpand, arrPath[i]);
                        DlkBaseControl currentNode = new DlkBaseControl("CurrentNode", "xpath", tXPath);

                        if(!currentNode.GetAttributeValue("class").Contains("open"))
                        {
                            string _path = string.Format(mXPathElementExpander, arrPath[i]);
                            DlkBaseControl elemExpander = new DlkBaseControl("Expander", "xpath", _path);
                            elemExpander.Click();
                        }
                    }
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
    }
}
