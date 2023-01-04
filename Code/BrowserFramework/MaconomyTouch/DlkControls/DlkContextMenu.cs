using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace MaconomyTouchLib.DlkControls
{
    [ControlType("ContextMenu")]
    public class DlkContextMenu : DlkBaseControl
    {
        private const int INT_UNDISPLAY_OFFSET = 50;

        public DlkContextMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkContextMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkContextMenu(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        
        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(String ExactText)
        {
            try
            {
                Initialize();
                IWebElement mSelected = null;
                if (DlkEnvironment.mBrowser.ToLower() == "ios")
                {
                    mSelected = mElement.FindElement(By.XPath("./descendant::div[@class='x-innerhtml' and text()='" + ExactText + "']"));
                }
                else
                {
                    mSelected = mElement.FindElement(By.XPath("./descendant::span[text()='" + ExactText + "']"));
                }
                DlkBaseControl mSelectedItem = new DlkBaseControl("Selected", mSelected);
                mSelectedItem.Click();
                DlkLogger.LogInfo("Select() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("Undisplay")]
        public void Undisplay()
        {
            try
            {
                //if (DlkEnvironment.mBrowser.ToLower() == "ios")
                //{
                    Initialize();
                    IWebElement mask = mElement.FindElement(By.XPath("./following::div[contains(@class,'x-mask')]"));
                    DlkBaseControl ctlMask = new DlkBaseControl("Selected", mask);
                    ctlMask.Click();
                    DlkLogger.LogInfo("Undisplay() successfully executed.");
                //}
                //else
                //{
                //   throw new Exception("Undisplay() keyword is only supported in iOS.");
                //}
            }
            catch (Exception e)
            {
                throw new Exception("Undisplay() failed : " + e.Message, e);
            }
        }

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

        [Keyword("VerifyMenu", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyMenu(String MenuItems)
        {
            try
            {
                Initialize();
                List<string> actualItemsText = new List<string>();

                
                foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::div[@class='x-innerhtml']")))
                {
                    actualItemsText.Add(new DlkBaseControl("element", elm).GetValue());
                }

                if (!actualItemsText.Any())
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::span[contains(@class,'x-button-label')]")))
                    {
                        actualItemsText.Add(new DlkBaseControl("element", elm).GetValue());
                    }
                }

                DlkAssert.AssertEqual("VerifyMenu()", MenuItems.Split('~'), actualItemsText.ToArray());
                DlkLogger.LogInfo("VerifyMenu() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenu() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyItemInMenu", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyItemInMenu(String MenuItem, String TrueOrFalse)
        {
            try
            {
                Initialize();
                List<string> actual = new List<string>();

                if (DlkEnvironment.mBrowser.ToLower() == "ios")
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::div[@class='x-innerhtml']")))
                    {
                        actual.Add(new DlkBaseControl("element", elm).GetValue());
                    }
                }
                else
                {
                    foreach (IWebElement elm in mElement.FindElements(By.XPath("./descendant::span[contains(@class,'x-button-label')]")))
                    {
                        actual.Add(new DlkBaseControl("element", elm).GetValue());
                    }
                }

                DlkAssert.AssertEqual("VerifyItemInMenu()", Convert.ToBoolean(TrueOrFalse), actual.Contains(MenuItem));
                DlkLogger.LogInfo("VerifyItemInMenu() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemInMenu() failed : " + e.Message, e);
            }
        }
    }
}
