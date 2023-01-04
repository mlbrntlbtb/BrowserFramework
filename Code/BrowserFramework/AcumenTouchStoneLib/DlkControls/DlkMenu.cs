using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        private ReadOnlyCollection<IWebElement> mItems;

        public DlkMenu(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        /// <summary>
        /// Always call this for every keyword
        /// </summary>
        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            GetItems();
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement aButton in mItems)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", aButton);
                    if (menuItem.GetValue().Trim().ToLower() == Item.Trim().ToLower())
                    {
                        if (this.mElement.GetAttribute("class") == "MenuBarBkGnd")
                        {
                            menuItem.Click();
                        }
                        else
                        {
                            menuItem.ClickUsingJavaScript();
                        }
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Menu item '" + Item + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectContains", new String[] { "1|text|Value|TRUE" })]
        public void SelectContains(String Item)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement aButton in mItems)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", aButton);
                    if (menuItem.GetValue().Trim().ToLower().Contains(Item.Trim().ToLower()))
                    {
                        menuItem.ClickUsingJavaScript();
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Menu item containing '" + Item + "' not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception("SelectContains() failed : " + e.Message, e);
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
        }

        [Keyword("VerifyMenuItemReadOnly", new String[] { "1|text|MenuItem|SampleItem", "2|text|ExpectedValue|TRUE" })]
        public void VerifyMenuItemReadOnly(String Item, String TrueOrFalse)
        {
            try
            {
                bool bFound = false;
                Initialize();

                foreach (IWebElement aButton in mItems)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", aButton);
                    if (menuItem.GetValue().ToLower() == Item.ToLower())
                    {
                        DlkAssert.AssertEqual("VerifyMenuItemReadOnly()", TrueOrFalse.ToLower(), menuItem.IsReadOnly().ToLower());
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Menu item '" + Item + "' not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenuItemReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInMenuContains", new String[] { "1|text|MenuItem|Sample item",
                                                                 "2|text|Expected Value|TRUE"})]
        public void VerifyAvailableInMenuContains(String Item, String TrueOrFalse)
        {
            try
            {
                bool bFound = false;
                Initialize();
                foreach (IWebElement aButton in mItems)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", aButton);
                    if (menuItem.GetValue().ToLower().Contains(Item.ToLower()))
                    {
                        bFound = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyAvailableInMenuContains()", TrueOrFalse.ToLower(), bFound.ToString().ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInMenuContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyAvailableInMenu", new String[] { "1|text|MenuItem|Sample item",
                                                         "2|text|Expected Value|TRUE"})]
        public void VerifyAvailableInMenu(String Item, String TrueOrFalse)
        {
            try
            {
                bool bFound = false;
                Initialize();
                foreach (IWebElement aButton in mItems)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", aButton);
                    if (menuItem.GetValue().ToLower() == Item.ToLower())
                    {
                        bFound = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyAvailableInMenu()", TrueOrFalse.ToLower(), bFound.ToString().ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyAvailableInMenu() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyMenu", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void VerifyMenu(String Items)
        {
            try
            {
                Initialize();
                string[] expected = Items.Split('~');
                //if (expected.Count() != mItems.Count)
                //{
                //    throw new Exception("Actual item count " + mItems.Count.ToString() +
                //        " not equal to expected item count " + expected.Count().ToString());
                //}
                for (int idx = 0; idx < mItems.Count; idx++)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", mItems[idx]);
                    if ((menuItem.GetValue() != "") && (menuItem.GetValue() != "\r\n        "))
                    {
                        if (menuItem.GetValue().ToLower() != expected[idx].ToLower())
                        {
                            throw new Exception("Item comparison failed at index " + idx.ToString() + ". Actual: "
                                + menuItem.GetValue().ToLower() + " , Expected: " + expected[idx].ToLower());
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenu() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickMenuItemButton", new String[] { "1|text|Expected Values|-Select-~All~Range" })]
        public void ClickMenuItemButton(String Item, String Button)
        {
            try
            {
                bool bFound = false;
                string buttonClass;
                switch (Button.ToLower().Trim())
                {
                    case "delete":
                        buttonClass = "icon-trash";
                        break;
                    default:
                        throw new Exception("ClickMenuItemButton () : Invalid input for Button. The following are the acceptable values: [delete]");
                }

                Initialize();

                foreach (IWebElement aButton in mItems)
                {
                    DlkBaseControl menuItem = new DlkBaseControl("MenuItem", aButton);
                    if (menuItem.GetValue().Trim().ToLower() == Item.Trim().ToLower())
                    {
                        DlkBaseControl menuItemButton = new DlkBaseControl("MenuItemButton", aButton.FindElement(By.XPath(".//*[contains(@class,'" + buttonClass + "')]")));
                        menuItemButton.ClickUsingJavaScript();
                        DlkLogger.LogInfo(String.Format("ClickMenuItemButton() : Successfully clicked button [{0}] of menu item [{1}].", Button, Item));
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                {
                    throw new Exception("Menu item '" + Item + "' not found");
                }

            }
            catch (Exception e)
            {
                throw new Exception("ClickMenuItemButton() failed : " + e.Message, e);
            }
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        private void GetItems()
        {
            try
            {
                mItems = this.mElement.GetAttribute("class") == "MenuBarBkGnd" ? this.mElement.FindElements(By.XPath(".//div")) : this.mElement.FindElements(By.XPath(".//a[not(contains(@style,'none'))]"));

                if (mItems.Count == 0)
                {
                    DlkBaseControl ctlArrowDown = null;
                    DlkBaseControl list = new DlkBaseControl("List", "CLASS_DISPLAY", "popupmenu");
                    if (!list.Exists(1))
                    {
                        IWebElement mArrowDown = this.mElement.FindElement(By.XPath(".//div[contains(@class,'actionsMenuIcon')]"));
                        ctlArrowDown = new DlkBaseControl("ArrowDown", mArrowDown);
                        ctlArrowDown.ClickUsingJavaScript();
                    }
                    list.FindElement();
                    mItems = list.mElement.FindElements(By.XPath("./li[contains(@class,'popupmenuItem')]"));
                    ctlArrowDown.ClickUsingJavaScript(); //close list
                }
            }
            catch
            {
                mItems = this.mElement.FindElements(By.TagName("span"));
            }
        }
    }
}
