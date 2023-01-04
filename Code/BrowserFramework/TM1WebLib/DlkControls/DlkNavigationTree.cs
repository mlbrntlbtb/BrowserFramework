using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TM1WebLib.DlkControls
{
    [ControlType("NavigationTree")]
    public class DlkNavigationTree : DlkBaseControl
    {

        public DlkNavigationTree(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavigationTree(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavigationTree(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("ClickNode", new String[] { "1|text|Node|Applications" })]
        public void ClickNode(String sNode)
        {

            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::*[text()='" + sNode + "']"));
                item.Click();
                DlkLogger.LogInfo("Successfully executed ClickNode()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickNode() failed : " + e.Message, e);
            }
        }

        [Keyword("ExpandNode", new String[] { "1|text|Node|Applications" })]
        public void ExpandNode(String sNode)
        {

            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::span[contains(text(), '" + sNode + "')]"));
                IWebElement expandButton = item.FindElement(By.XPath("../preceding-sibling::img"));
                expandButton.Click();
                DlkLogger.LogInfo("Successfully executed ExpandNode()");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandNode() failed : " + e.Message, e);
            }
        }

        [Keyword("CollapseNode", new String[] { "1|text|Node|Applications" })]
        public void CollapseNode(String sNode)
        {

            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::span[contains(text(), '" + sNode + "')]"));
                IWebElement collapseButton = item.FindElement(By.XPath("../preceding-sibling::img"));
                collapseButton.Click();
                DlkLogger.LogInfo("Successfully executed CollapseNode()");
            }
            catch (Exception e)
            {
                throw new Exception("CollapseNode() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyNodeExists", new String[] { "1|text|Applications|TRUE" })]
        public void VerifyNodeExists(String sNodeText, String strExpectedValue)
        {
            try
            {
                Initialize();
                try
                {
                    IWebElement item = mElement.FindElement(By.XPath("/descendant::span[contains(text(), '" + sNodeText + "')]"));
                    if (!Convert.ToBoolean(strExpectedValue))
                    {
                        throw new Exception(sNodeText + " is an existing node.");
                    }
                    DlkLogger.LogInfo("Successfully executed VerifyNodeExists()");
                }
                catch (NoSuchElementException)
                {
                    if (Convert.ToBoolean(strExpectedValue))
                    {
                        throw;
                    }
                    else if (!Convert.ToBoolean(strExpectedValue))
                    {
                        DlkLogger.LogInfo("Successfully executed VerifyNodeExists()");
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyNodeExists() failed : " + e.Message, e);
            }
        }

    }
}
