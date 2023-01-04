using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium.Interactions;

namespace TM1WebLib.DlkControls
{
    [ControlType("DropdownTree")]
    public class DlkDropdownTree : DlkBaseControl
    {
        
        public DlkDropdownTree(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropdownTree(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropdownTree(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("ClickNode", new String[] { "1|text|MenuItem|All Projects and Opportunities" })]
        public void ClickNode(String sMenuItem)
        {
            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::*[text()='" + sMenuItem + "']"));
                item.Click();
                Thread.Sleep(5000);
                DlkLogger.LogInfo("Successfully executed ClickNode()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickNode() failed : " + e.Message, e);
            }
        }

        [Keyword("MoveSliderLeft")]
        public void MoveSliderLeft()
        {
            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::*[@class='dijitSliderDecrementIconH']"));
                item.Click();
                Thread.Sleep(5000);
                DlkLogger.LogInfo("Successfully executed MoveSliderLeft()");
            }
            catch (Exception e)
            {
                throw new Exception("MoveSliderLeft() failed : " + e.Message, e);
            }
        }

        [Keyword("MoveSliderRight")]
        public void MoveSliderRight()
        {
            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::*[@class='dijitSliderIncrementIconH']"));
                item.Click();
                Thread.Sleep(5000);
                DlkLogger.LogInfo("Successfully executed MoveSliderRight()");
            }
            catch (Exception e)
            {
                throw new Exception("MoveSliderRight() failed : " + e.Message, e);
            }
        }

        [Keyword("ExpandNode", new String[] { "1|text|MenuItem|All Projects and Opportunities" })]
        public void ExpandNode(String sMenuItem)
        {
            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::*[text()='" + sMenuItem + "']"));
                IWebElement expandButton = item.FindElement(By.XPath("../preceding-sibling::img"));
                expandButton.Click();
                DlkLogger.LogInfo("Successfully executed ExpandNode()");
            }
            catch (Exception e)
            {
                throw new Exception("ExpandNode() failed : " + e.Message, e);
            }
        }

        [Keyword("CollapseNode", new String[] { "1|text|MenuItem|All Projects and Opportunities" })]
        public void CollapseNode(String sMenuItem)
        {
            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::*[text()='" + sMenuItem + "']"));
                IWebElement expandButton = item.FindElement(By.XPath("../preceding-sibling::img"));
                expandButton.Click();
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

        // not working - search box cannot be interacted with
        //[Keyword("SearchDropdownTree", new String[] { "1|text|MenuItem|All Projects and Opportunities" })]
        //public void SearchDropdownTree(String sMenuItem)
        //{
        //    try
        //    {
        //        Initialize();
        //        IWebElement item = mElement.FindElement(By.XPath("/descendant::input"));
        //        item.Clear();
        //        if (!string.IsNullOrEmpty(sMenuItem))
        //        {
        //            item.SendKeys(sMenuItem);

        //            if (item.GetAttribute("value").ToLower() != sMenuItem.ToLower())
        //            {
        //                item.Clear();
        //                item.SendKeys(sMenuItem);
        //            }
        //        }
        //        item.SendKeys(Keys.Shift + Keys.Tab);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("SelectMenu() failed : " + e.Message, e);
        //    }
        //}

    }
}
