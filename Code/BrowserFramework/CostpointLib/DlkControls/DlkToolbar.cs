using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using CommonLib.DlkHandlers;

namespace CostpointLib.DlkControls
{
    [ControlType("Toolbar")]
    public class DlkToolbar : DlkBaseControl
    {
        private ReadOnlyCollection<IWebElement> mButtons;
        private String mStrToolbarButtons = "./*[(@class='tbBtnContainer' or contains(@class, 'tbBtnHRpt')) and not(contains(@style,'display: none'))]";
        private String mToolbarMenu = "//*[@class='tlbrDDMenuDiv']";

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
        public void ClickToolbarButton(String pStrButtonCaption)
        {
            bool bFound = false;
            String[] arrInputString = pStrButtonCaption.Split('~');
            int inputCount = arrInputString.Count();

            try
            {
                Initialize();

                bool prodVer701 = DlkTestRunnerSettingsHandler.ApplicationUnderTest.Version.Contains("7.0.1") ? true : false;
                string firstSearchKey = prodVer701 ? arrInputString.First() : DlkString.NormalizeNonBreakingSpace(arrInputString.First());
                mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));

                /* Iterate thru all toolbar button containers in toolbar */
                foreach (IWebElement aButton in mButtons)
                {
                    /* Click on first item if just 1 item to click */
                    if (inputCount == 1)
                    {
                        IWebElement mainButton = null;
                        if (aButton.FindElements(By.XPath("./div[contains(@title,'" + firstSearchKey + "')]")).Any())
                        {
                            mainButton = aButton.FindElements(By.XPath("./div[contains(@title,'" + firstSearchKey + "')]")).First();
                        }
                        else if (aButton.GetAttribute("title") == firstSearchKey)
                        {
                            mainButton = aButton;
                        }
                        if (mainButton != null)
                        {
                            DlkBaseControl btnMainButton = new DlkBaseControl("MainButton", mainButton);
                            if (DlkEnvironment.mBrowser.ToLower() == "ie")
                            {
                                mElement = mainButton;
                                ClickUsingJavaScript(false);
                            }
                            else
                            {
                                btnMainButton.Click();
                            }
                            bFound = true;
                            break;
                        }
                    }

                    /* Click on dropdown always if more than 1 item to click EX: Refresh~Refresh All */
                    else
                    {
                        /* Check if first button exists from current toolbar button container */
                        if (aButton.FindElements(By.XPath("./div[contains(@title,'" + firstSearchKey + "')]")).Any())
                        {
                            bFound = true;
                            string secondSearchKey = arrInputString[1];

                            /* Look for descendant drop down button */
                            ReadOnlyCollection<IWebElement> splitButtonRight = aButton.FindElements(By.XPath("./descendant::*[contains(@class,'tbBtnSplitRight')]"));
                            if (splitButtonRight.Any())
                            {
                                DlkBaseControl dropDown = new DlkBaseControl("DropDownButton", splitButtonRight.First());
                                dropDown.Click();

                                /* Menu items visible at this point */
                                /* Check if user wants to click 'Batch Mode' menu item */
                                bool getLastHit = false;
                                if (inputCount >= 3 && arrInputString[1].ToLower().Contains("batch mode"))
                                {
                                    if (secondSearchKey.ToLower().Contains("batch mode"))
                                    {
                                        secondSearchKey = arrInputString[2];
                                        getLastHit = true;
                                    }
                                    else
                                    {
                                        throw new Exception("Clicking of more than 2 menu items is not allowed at this point.");
                                    }
                                }
                                ReadOnlyCollection<IWebElement> items = mElement.FindElements(By.XPath("//*[@class = 'tlbrDDItem' and contains(text(),'" + secondSearchKey + "')]"));
                                
                                /* Check if target menu item exists */
                                if (!items.Any())
                                {
                                    throw new Exception("Toolbar menu item '" + secondSearchKey + "' for '" + firstSearchKey + "' button not found");
                                }
                                DlkBaseControl targetItem = new DlkBaseControl("MenuItem", getLastHit ? items.Last() : items.First());
                                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                                {
                                    mElement = targetItem.mElement;
                                    ClickUsingJavaScript(false);
                                }
                                else
                                {
                                    new DlkBaseControl("ItemToClick", targetItem.GetParent()).Click();
                                }
                                break;
                            }
                            else
                            {
                                throw new Exception("Toolbar menu list cannot be displayed.");
                            }
                        }
                    }            
                }

                /* Raise error if first button was not found */
                if (!bFound)
                {
                    throw new Exception("Toolbar button '" + firstSearchKey + "' not found");
                }
                DlkLogger.LogInfo("Successfully executed ClickToolbarButton()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickToolbarButton() failed : " + e.Message, e);
            }
        }

        public void VerifyToolbarButton(String pStrButtonCaption, String pStrProperty, String pStrExpectedValue)
        {
        }


        [Keyword("VerifyToolbarButtonExist", new String[] {"1|text|Button Caption|Save", 
                                                              "2|text|Expected Value|True " })]
        public void VerifyToolbarButtonExist(String ButtonCaption, String ExpectedValue)
        {
            bool bFound = false;
            String[] arrInputString = ButtonCaption.Split('~');

            try
            {
                Initialize();

                mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).Count > 0)
                    {
                        IWebElement mainButton = aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).First();
                        DlkBaseControl btnMainButton = new DlkBaseControl("MainButton", mainButton);
                        if (arrInputString.Count() > 1)
                        {
                            btnMainButton.Click();
                        }
                        bFound = true;
                        break;
                    }
                }

                if (arrInputString.Count() == 1 || !bFound)
                {
                    DlkAssert.AssertEqual("VerifyToolbarButtonExist()", Convert.ToBoolean(ExpectedValue), bFound);
                }
                else
                {
                    bFound = false;

                    if (mElement.FindElements(By.XPath(mToolbarMenu)).Count > 0)
                    {
                        foreach (IWebElement itm in mElement.FindElements(By.XPath(mToolbarMenu)))
                        {
                            if (itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
                                + arrInputString[1] + "')]")).Count > 0)
                            {
                                bFound = true;
                                break;
                            }
                        }
                        DlkAssert.AssertEqual("VerifyToolbarButtonExist()", Convert.ToBoolean(ExpectedValue), bFound);
                    }
                    else
                    {
                        throw new Exception("Toolbar menu for '" + arrInputString.First() + "' button not found");
                    }

                }
                DlkLogger.LogInfo("VerifyToolbarButtonExist() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolbarButtonExist() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToolbarButtonReadOnly", new String[] {"1|text|Button Caption|Save", 
                                                              "2|text|Expected Value|True (if button is expected to be read only)" })]
        public void VerifyToolbarButtonReadOnly(String ButtonCaption, String ExpectedValue)
        {
            bool bFound = false;
            bool bReadOnly = false;
            String[] arrInputString = ButtonCaption.Split('~');

            try
            {
                Initialize();

                mButtons = mElement.FindElements(By.XPath(mStrToolbarButtons));
                foreach (IWebElement aButton in mButtons)
                {
                    if (aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).Count > 0)
                    {
                        IWebElement mainButton = aButton.FindElements(By.XPath("./div[contains(@title,'" + arrInputString.First() + "')]")).First();
                        bReadOnly = mainButton.GetCssValue("opacity") != "1";
                        DlkBaseControl btnMainButton = new DlkBaseControl("MainButton", mainButton);
                        if (arrInputString.Count() > 1 && !bReadOnly)
                        {
                            btnMainButton.Click();
                        }
                        else if (arrInputString.Count() == 1)
                        {
                            DlkAssert.AssertEqual("VerifyToolbarButtonReadOnly()", Convert.ToBoolean(ExpectedValue), bReadOnly);
                        }
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Toolbar button '" + arrInputString.First() + "' not found");
                }

                bFound = false;

                if (arrInputString.Count() == 2)
                {
                    if (mElement.FindElements(By.XPath(mToolbarMenu)).Count > 0)
                    {
                        foreach (IWebElement itm in mElement.FindElements(By.XPath(mToolbarMenu)))
                        {
                            if (itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
                                + arrInputString[1] + "')]")).Count > 0)
                            {
                                IWebElement itmTbrMenuItem = itm.FindElements(By.XPath("./descendant::*[@class='tlbrDDItem' and contains(text(),'"
                                + arrInputString[1] + "')]")).First();
                                DlkBaseControl ctlMenuItem = new DlkBaseControl("ToolbarMenuItem", itmTbrMenuItem);
                                bReadOnly = ctlMenuItem.GetParent().GetAttribute("mnudisabled") == "Y";
                                DlkAssert.AssertEqual("VerifyToolbarButtonReadOnly()", Convert.ToBoolean(ExpectedValue), bReadOnly);
                                bFound = true;
                                break;
                            }
                        }
                        if (!bFound)
                        {
                            throw new Exception("Toolbar menu item '" + arrInputString[1] + "' for '" + arrInputString.First() + "' button not found");
                        }
                    }
                    else
                    {
                        throw new Exception("Toolbar menu for '" + arrInputString.First() + "' button not found");
                    }
                }

                DlkLogger.LogInfo("VerifyToolbarButtonReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolbarButtonReadOnly() failed : " + e.Message, e);
            }
        }
    }
}
