using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Threading;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("DropDownMenu")]
    public class DlkDropDownMenu : DlkBaseControl
    {
        #region Constructors

        public DlkDropDownMenu(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) 
        {
            //Do Nothing.
        }

        #endregion

        #region Keywords
        
        /// <summary>
        /// Will click the selected (Value parameter) drop down menu item.
        /// </summary>
        /// <param name="Value">will search using XPath contains text function.</param>
        [Keyword("Select")]
        public void Select(string Value)
        {
            try
            {
                //Click the menu drop down.
                Click();
                selectSubMenu(Value);

                DlkLogger.LogInfo("Select( ) passed.");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("no such element"))
                {
                    DlkLogger.LogInfo("Retrying...");
                    selectSubMenu(Value);
                }
                else
                {
                    throw new Exception("Select( ) failed " + ex.Message, ex);
                }
            }
        }

        private void selectSubMenu( string Value)
        {
            IWebElement selectedElement = null;

            if (mElement.TagName.Equals("button"))
            {
                selectedElement = FindElementInControls(@"//*[@id='search-select']/li/a", Value);
            }
            else
            {
                selectedElement = FindElementInControls(@"//li[contains(@class,'open')]//a[@role='menuitem'] | ./following-sibling::ul[contains(@style,'display')]//a
                | //ul[contains(@class,'dropdown-menu show')]//a[@role='menuitem']", Value);
            }

            if (selectedElement == null)
            {
                selectedElement = FindElementInControls(@"./../../ul//a", Value);
                if (selectedElement == null || !selectedElement.Displayed)
                {
                    IList<IWebElement> dropDownElem = mElement.FindElements(By.XPath(@"../div[@id and contains(@style,'none')]"));
                    if (dropDownElem.Count > 0)
                    {
                        Click(1);
                    }
                    string[] param = Value.Split('~');
                    List<string> searchVal = param.ToList();
                    IWebElement parentElement = null;
                    if (searchVal.Count > 1)
                    {
                        for (int i = 0; i < searchVal.Count - 1; i++)
                        {
                            parentElement = DlkCommon.DlkCommonFunction.GetElementWithText(searchVal[i], mElement.FindElement(By.XPath(@"./..//div[@id]")))[0];
                            IList<IWebElement> childElements = parentElement.FindElements(By.XPath(@"../div[@role and contains(@style,'none')]"));
                            if (childElements.Count > 0)
                            {
                                parentElement.Click();
                            }
                        }
                    }
                    string txtToSearch = searchVal[searchVal.Count - 1];
                    IList<IWebElement> submenus = mElement.FindElements(By.XPath(@"./..//div[@id]"));
                    IWebElement targetMenu = submenus.Count > 0 ? submenus[0] : mElement.FindElement(By.XPath(@"./..//ul[contains(@role,'menu')]"));
                    IList<IWebElement> selectedElements = DlkCommon.DlkCommonFunction.GetElementWithText(txtToSearch, targetMenu);

                    // retry
                    if (selectedElements.Count == 0)
                    {
                        IWebElement container = DlkEnvironment.AutoDriver.FindElement(By.XPath("//ul[@class='dropdown-menu' and contains(@style,'display: block')]"));
                        selectedElement = DlkCommon.DlkCommonFunction.GetElementWithText(Value, container)[0];
                    }
                    else
                    {
                        foreach (IWebElement element in selectedElements)
                        {
                            DlkBaseControl elem = new DlkBaseControl("Core_Sub_Menu", element);
                            string classAttr = elem.GetAttributeValue("class") == null ? string.Empty : elem.GetAttributeValue("class");
                            if (element.Text.Equals(txtToSearch) && !classAttr.Contains("-active"))
                            {
                                selectedElement = element;
                                break;
                            }
                        }
                    }
                }
            }
            //Click the selected item in the drop down menu.
            DlkLink link = new DlkLink("SelectedMenuItem", selectedElement);
            //Simple IWebElement.Click() not working on chrome.
            link.ClickUsingJavaScript();

            if (DlkEnvironment.mBrowser.ToLower().Equals("ie"))
            {
                //IE not responding well in using the control after clicking we need to delay.
                Thread.Sleep(500);
            }
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();
                DlkButton currentButton = new DlkButton("Button", mElement);
                currentButton.Click();

                DlkLogger.LogInfo("Click( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("Click( ) failed " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCaption")]
        public void VerifyCaption(string Caption)
        {
            try
            {
                Initialize();
                IWebElement listElement = mElement.FindElement(By.XPath(".//ancestor::li[@class]"));
                DlkBaseControl listControl = new DlkBaseControl("DropDownMenuItem", listElement);
                DlkAssert.AssertEqual("VerifyCaption", Caption, listControl.GetValue());
                DlkLogger.LogInfo("VerifyCaption( ) successfuly executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyCaption( ) execution failed. " + ex.Message, ex);
            }
        }

        [Keyword("VerifyItemCaption")]
        public void VerifyItemCaption(string ItemCaption)
        {
            try
            {
                Initialize();
                mElement.Click();
                IWebElement selectedElement = null;

                if (mElement.TagName.Equals("button"))
                {
                    selectedElement = FindElementInControls(@"//*[@id='search-select']/li/a", ItemCaption);
                }
                else
                {
                    selectedElement = FindElementInControls(@"//li[contains(@class,'open')]//a[@role='menuitem']", ItemCaption);
                }

                if (selectedElement == null)
                {
                    selectedElement = FindElementInControls(@"./../../ul//a", ItemCaption);
                }

                DlkLink link = new DlkLink("SelectedMenuItem", selectedElement);
                string actualResult = selectedElement == null ? string.Empty : link.GetValue();
                DlkAssert.AssertEqual("VerifyCaption", ItemCaption, link.GetValue());
                DlkLogger.LogInfo("VerifyItemCaption( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyItemCaption( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                mElement.Click();
            }
        }

        [Keyword("VerifyExistItemCaption")]
        public void VerifyExistItemCaption(string ItemCaption, string TrueOrFalse)
        {
            try
            {
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                Initialize();
                mElement.Click();
                IWebElement selectedElement = null;

                if (mElement.TagName.Equals("button"))
                {
                    selectedElement = FindElementInControls(@"//*[@id='search-select']/li/a", ItemCaption);
                }
                else
                {
                    selectedElement = FindElementInControls(@"//li[contains(@class,'open')]//a[@role='menuitem']", ItemCaption);
                }

                if (selectedElement == null)
                {
                    selectedElement = FindElementInControls(@"./../../ul//a", ItemCaption);
                }

                if (selectedElement == null)
                {
                    IList<IWebElement> elements = DlkCommon.DlkCommonFunction.GetElementWithText(ItemCaption, mElement.FindElement(By.XPath("./..")));
                    if (elements.Count > 0)
                    {
                        selectedElement = elements[0];
                    }
                }

                DlkLink link = new DlkLink("SelectedMenuItem", selectedElement);
                string actualCaption = selectedElement == null ? string.Empty : link.GetValue();
                bool actualResult = false;
                if (actualCaption.Equals(ItemCaption))
                {
                    actualResult = true;
                }
                DlkAssert.AssertEqual("VerifyCaption", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyExistItemCaption( ) successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyExistItemCaption( ) execution failed. " + ex.Message, ex);
            }
            finally
            {
                mElement.Click();
            }
        }

        [Keyword("AssignExistStatusToVariable")]
        public void AssignExistStatusToVariable(string Variable)
        {
            try
            {
                //Fail safe code for checking crashed site.
                DlkEnvironment.AutoDriver.FindElement(By.CssSelector("h1"));
                base.GetIfExists(Variable);
                DlkLogger.LogInfo("AssignExistStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("AssignExistItemStatusToVariable")]
        public void AssignExistItemStatusToVariable(string Value, string Variable)
        {
            try
            {
                Click(3);
                IWebElement selectedElement = null;

                if (mElement.TagName.Equals("button"))
                {
                    selectedElement = FindElementInControls(@"//*[@id='search-select']/li/a", Value);
                }
                else
                {
                    selectedElement = FindElementInControls(@"//li[contains(@class,'open')]//a[@role='menuitem']", Value);
                }

                DlkVariable.SetVariable(Variable, DlkCommon.DlkCommonFunction.VerifyElementExists(selectedElement).ToString());
                mElement.Click();
                DlkLogger.LogInfo("AssignExistItemStatusToVariable() successfully executed.");
            }
            catch (Exception ex)
            {
                throw new Exception("AssignExistItemStatusToVariable() execution failed. : " + ex.Message, ex);
            }
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            FindElement();
        }

        private IWebElement FindElementInControls(string xpathSearchKey, string searchKey)
        {
            IList<IWebElement> controls = mElement.FindElements(By.XPath(xpathSearchKey));

            foreach (IWebElement element in controls)
            {
                if (element.Text.Contains(searchKey))
                {
                    return element;
                }
            }

            return null;
        }

        #endregion
    }
}
