using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using TM1WebLib.DlkUtility;
using TM1WebLib.DlkControls;

namespace TM1WebLib.DlkControls
{
    [ControlType("Toolbar")]
    public class DlkToolbar : DlkBaseControl
    {

        public DlkToolbar(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToolbar(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToolbar(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
                FindElement();
        }

        [Keyword("ClickToolbarButton", new String[] { "1|text|Button Caption|Save" })]
        public void ClickToolbarButton(String sButtonTooltip)
        {

            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::span[@title = '" + sButtonTooltip + "']"));
                if (item.GetAttribute("aria-disabled") == "true")
                {
                    throw new Exception("ClickToolbarButton() failed : button is disabled");
                }
                item.Click();
                DlkLogger.LogInfo("Successfully executed ClickToolbarButton()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarButton() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickToolbarActionDropdownItem", new String[] { "1|text|Menu Item|Save Data Changes" })]
        public void ClickToolbarActionDropdownItem(String sMenuItem)
        {

            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::span[@title = 'Actions Menu']"));
                if (item.GetAttribute("aria-disabled") == "true")
                {
                    throw new Exception("ClickToolbarActionDropdownItem() failed : Actions Menu button is disabled");
                }
                item.Click();
                Thread.Sleep(3000);
                IWebElement dropdownButton = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='dijitPopup dijitMenuPopup']/descendant::td[text() = '" + sMenuItem + "']"));
                dropdownButton.Click();
                Thread.Sleep(3000);
                DlkLogger.LogInfo("Successfully executed ClickToolbarActionDropdownItem()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarActionDropdownItem() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickToolbarSandBoxDropdownItem", new String[] { "1|text|Menu Item|Delete Sandbox" })]
        public void ClickToolbarSandBoxDropdownItem(String sMenuItem)
        {

            try
            {
                Initialize();
                IWebElement item = mElement.FindElement(By.XPath("/descendant::span[@title = 'Sandbox']"));
                if (item.GetAttribute("aria-disabled") == "true")
                {
                    throw new Exception("ClickToolbarSandBoxDropdownItem() failed : Sandbox button is disabled");
                }
                item.Click();
                IWebElement dropdownButton = DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='dijitPopup dijitMenuPopup']/descendant::td[text() = '" + sMenuItem + "']"));
                dropdownButton.Click();
                DlkLogger.LogInfo("Successfully executed ClickToolbarSandBoxDropdownItem()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarSandBoxDropdownItem() failed : " + e.Message, e);
            }
        }

        //[Keyword("VerifyToolbarButtonExist", new String[] {"1|text|Button Caption|Save", 
        //                                                      "2|text|Expected Value|True " })]
        //public void VerifyToolbarButtonExist(String ButtonCaption, String ExpectedValue)
        //{
        //    bool bFound = false;
        //    String[] arrInputString = ButtonCaption.Split('~');

        //    try
        //    {
        //        Initialize();

        //        mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));
        //        foreach (IWebElement aButton in mButtons)
        //        {
        //            if (aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).Count > 0)
        //            {
        //                IWebElement mainButton = aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).First();
        //                DlkBaseControl btnMainButton = new DlkBaseControl("MainButton", mainButton);
        //                if (arrInputString.Count() > 1)
        //                {
        //                    btnMainButton.Click();
        //                }
        //                bFound = true;
        //                break;
        //            }
        //        }

        //        if (arrInputString.Count() == 1 || !bFound)
        //        {
        //            DlkAssert.AssertEqual("VerifyToolbarButtonExist()", Convert.ToBoolean(ExpectedValue), bFound);
        //        }
        //        else
        //        {
        //            bFound = false;

        //            if (mElement.FindElements(By.XPath(mToolbarMenu)).Count > 0)
        //            {
        //                foreach (IWebElement itm in mElement.FindElements(By.XPath(mToolbarMenu)))
        //                {
        //                    if (itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
        //                        + arrInputString[1] + "')]")).Count > 0)
        //                    {
        //                        bFound = true;
        //                        break;
        //                    }
        //                }
        //                DlkAssert.AssertEqual("VerifyToolbarButtonExist()", Convert.ToBoolean(ExpectedValue), bFound);
        //            }
        //            else
        //            {
        //                throw new Exception("Toolbar menu for '" + arrInputString.First() + "' button not found");
        //            }

        //        }
        //        DlkLogger.LogInfo("VerifyToolbarButtonExist() passed");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyToolbarButtonExist() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("VerifyToolbarButtonReadOnly", new String[] {"1|text|Button Caption|Save", 
        //                                                      "2|text|Expected Value|True (if button is expected to be read only)" })]
        //public void VerifyToolbarButtonReadOnly(String ButtonCaption, String ExpectedValue)
        //{
        //    bool bFound = false;
        //    bool bReadOnly = false;
        //    String[] arrInputString = ButtonCaption.Split('~');

        //    try
        //    {
        //        Initialize();

        //        mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));
        //        foreach (IWebElement aButton in mButtons)
        //        {
        //            if (aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).Count > 0)
        //            {
        //                IWebElement mainButton = aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).First();
        //                bReadOnly = mainButton.GetCssValue("opacity") != "1";
        //                DlkBaseControl btnMainButton = new DlkBaseControl("MainButton", mainButton);
        //                if (arrInputString.Count() > 1 && !bReadOnly)
        //                {
        //                    btnMainButton.Click();
        //                }
        //                else if (arrInputString.Count() == 1)
        //                {
        //                    DlkAssert.AssertEqual("VerifyToolbarButtonReadOnly()", Convert.ToBoolean(ExpectedValue), bReadOnly);
        //                }
        //                bFound = true;
        //                break;
        //            }
        //        }

        //        if (!bFound)
        //        {
        //            throw new Exception("Toolbar button '" + arrInputString.First() + "' not found");
        //        }

        //        bFound = false;

        //        if (arrInputString.Count() == 2)
        //        {
        //            if (mElement.FindElements(By.XPath(mToolbarMenu)).Count > 0)
        //            {
        //                foreach (IWebElement itm in mElement.FindElements(By.XPath(mToolbarMenu)))
        //                {
        //                    if (itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
        //                        + arrInputString[1] + "')]")).Count > 0)
        //                    {
        //                        IWebElement itmTbrMenuItem = itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
        //                        + arrInputString[1] + "')]")).First();
        //                        DlkBaseControl ctlMenuItem = new DlkBaseControl("ToolbarMenuItem", itmTbrMenuItem);
        //                        bReadOnly = ctlMenuItem.GetParent().GetAttribute("mnudisabled") == "Y";
        //                        DlkAssert.AssertEqual("VerifyToolbarButtonReadOnly()", Convert.ToBoolean(ExpectedValue), bReadOnly);
        //                        bFound = true;
        //                        break;
        //                    }
        //                }
        //                if (!bFound)
        //                {
        //                    throw new Exception("Toolbar menu item '" + arrInputString[1] + "' for '" + arrInputString.First() + "' button not found");
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("Toolbar menu for '" + arrInputString.First() + "' button not found");
        //            }
        //        }

        //        DlkLogger.LogInfo("VerifyToolbarButtonReadOnly() passed");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyToolbarButtonReadOnly() failed : " + e.Message, e);
        //    }
        //}
    }
}
