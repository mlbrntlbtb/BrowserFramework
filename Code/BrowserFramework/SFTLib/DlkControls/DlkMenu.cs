using System;
using System.Linq;
using System.Threading;
using SFTLib.DlkSystem;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using SFTLib.DlkUtility;
using System.Windows.Forms;
using System.Diagnostics;

namespace SFTLib.DlkControls
{
    [CommonLib.DlkSystem.ControlType("Menu")]
    public class DlkMenu : DlkBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit;
        #endregion
        #region CONSTRUCTORS
        public DlkMenu(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkMenu(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public DlkMenu(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkMenu(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public DlkMenu(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }


        public void Initialize()
        {
            DlkEnvironment.mSwitchediFrame = mSearchValues == null;

            //When passing an element via DlkMenu constructor, these methods may cause a stale element exception.
            //Only execute these methods when menu control came from OS.
            if (mSearchValues != null)
            {
                DlkSFTCommon.WaitForScreenToLoad(3);
                DlkSFTCommon.WaitForSpinner();


                if (!IsInit)
                {
                    FindElement();
                    IsInit = true;
                }
                else
                {
                    if (IsElementStale())
                    {
                        FindElement();
                    }
                }
            }
        }
        #endregion

        #region KEYWORDS
        [Keyword("Select")]
        public void Select(string Parameters)
        {
            try
            {
                Initialize();
                Navigate(Parameters, isLookupMenu: (mSearchValues == null ? false : (mSearchValues[0].Contains("mainMenuToolbar") ? false : true)));
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }
        [Keyword("VerifySubMenu")]
        public void VerifySubMenu(string Parameters)
        {
            try
            {
                Initialize();
                Navigate(Parameters, false);
                mElement.Click();
                DlkSFTCommon.WaitForScreenToLoad();
                DlkSFTCommon.WaitForSpinner();
                DlkLogger.LogInfo("VerifyMenu() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenu() failed : " + e.Message, e);
            }
        }
        [Keyword("VerifySubMenuList")]
        public void VerifySubMenuList(string SelectMenuParameters, string Items)
        {
            try
            {
                Thread.Sleep(2000);

                Initialize();
                if (!String.IsNullOrEmpty(SelectMenuParameters))
                    Navigate(SelectMenuParameters, false);

                Thread.Sleep(100);
                var lastSubMenuItemsList = GetCurrentMenuList().Select(menu => menu.Text.Trim()).ToList();
                lastSubMenuItemsList.CompareToOtherList(Items.ConvertTildeDelimitedStringToList());
                mElement.Click();

                DlkSFTCommon.WaitForScreenToLoad();
                DlkSFTCommon.WaitForSpinner();
                DlkLogger.LogInfo("VerifyMenuList() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenuList() failed : " + e.Message, e);
            }
        }
        [Keyword("VerifyMenuPath", new String[] { "" })]
        public void VerifyMenuPath(String strExpectedValue)
        {
            try
            {
                Initialize();
                var list = mElement.FindElements(By.XPath("//*[@id='mainMenuToolbar']//*[contains(@class,'x-unselectable')][position() > 1]"))
                   .Where(x => !String.IsNullOrEmpty(x.Text.Trim()))
                  .Select(x => x.Text.Trim()).ToList();
                string menuPath = String.Join("~", list);
                DlkAssert.AssertEqual("VerifyMenuPath()", strExpectedValue.Trim(), menuPath.Trim());
                DlkLogger.LogInfo("VerifyMenuPath() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMenuPath() failed : " + e.Message, e);
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
        }
        #endregion

        #region Methods
        private void Navigate(string Parameters, bool clickLastItem = true, bool isLookupMenu = false)
        {
            Thread.Sleep(1200);
            bool subMenuFound = false;
            string[] versionLookUpMenu = { "form:but", "form_edit" };
            string idAttribute = mElement.GetAttribute("id").ToLower();
            bool isElemLookup = versionLookUpMenu.Any(idAttribute.Contains); //verify version 1 lookup menu buttons

            if (DlkEnvironment.mBrowser == "IE" && !isElemLookup)
            {
                if (!(GetCurrentMenuList().Count > 0)) new DlkBaseControl("Menu", mElement).ClickUsingJavaScript(false);
            }
            else if(DlkEnvironment.mBrowser == "IE" && mElement.GetAttribute("id").ToLower().Contains("form_edit"))
            {
                mElement.SendKeys("");
                ClickUsingJavaScript(false);
            }
            else
                mElement.Click();
            
            Thread.Sleep(1500);
            var parametersArray = Parameters.Split('~').ToArray();

            //IE has known issues with hovering/clicking menus/dropdowns
            //Use sendkeys/javascript commands as a workaround
            if (DlkEnvironment.mBrowser.ToLower() == "ie" && !isElemLookup)
            {
                foreach (var param in parametersArray)
                {
                    subMenuFound = false; //reset to false for multiple params

                    var subMenuList = GetCurrentMenuList();
                    if (subMenuList.Count > 0)
                    {
                        foreach (var menuItem in subMenuList)
                        {
                            try
                            {
                                menuItem.SendKeys(OpenQA.Selenium.Keys.ArrowDown);//highlights the item as active
                            }
                            catch (ElementNotInteractableException e)
                            {
                                if (e.Message == "Element cannot be interacted with via the keyboard because it is not focusable")
                                {
                                    ClickMenuItemTextElements(parametersArray);
                                    DlkSFTCommon.WaitForScreenToLoad(2);
                                    DlkSFTCommon.WaitForSpinner();
                                    return;
                                }
                            }
                            var highlightedMenu = GetSelectedMenuItem();
                            if (highlightedMenu.Text.Trim() == param)
                            {
                                DlkLogger.LogInfo("Navigating to " + param + "...");
                                highlightedMenu.SendKeys(OpenQA.Selenium.Keys.ArrowRight);//selects the submenu of the highlighted item
                                subMenuFound = true;
                                break;
                            }
                        }
                    }
                    else throw new Exception("No menu for screens found.");

                    if (!subMenuFound)
                    {
                        throw new Exception(String.Format("{0} not found.", param));
                    }
                }

                if(clickLastItem) GetSelectedMenuItem().SendKeys(OpenQA.Selenium.Keys.Enter);//will select the highlighted item
            }
            else NavigateUsingClick(parametersArray, clickLastItem, isLookupMenu, subMenuFound);

            if (clickLastItem)
            {
                DlkSFTCommon.WaitForScreenToLoad(2);
                DlkSFTCommon.WaitForSpinner();
            }
        }

        /// <summary>
        /// Navigates through the menu by clicking on the menu item to select.
        /// Used for all browsers except IE.
        /// </summary>
        /// <param name="parametersArray"></param>
        /// <param name="clickLastItem"></param>
        /// <param name="isLookupMenu"></param>
        private void NavigateUsingClick(string[] parametersArray, bool clickLastItem, bool isLookupMenu, bool subMenuFound)
        {
            DlkSFTCommon.WaitForScreenToLoad(2);
            int numberOfFoundItems = 0;

            foreach (var item in parametersArray)
            {
                subMenuFound = false;
                var subMenuList = GetCurrentMenuList();
                foreach (var submenu in subMenuList)
                {
                    if (item == submenu.Text.Trim())
                    {
                        subMenuFound = true;
                        if ((parametersArray.Count() - 1) == numberOfFoundItems++ && !clickLastItem)//Last Item
                            break;

                        try
                        {
                            if (isLookupMenu)
                                submenu.Click();
                            else if (DlkEnvironment.mBrowser.ToLower() == "edge")
                            {
                                new DlkBaseControl(item, submenu).Click();
                                DlkSFTCommon.WaitForScreenToLoad();
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                new DlkBaseControl("subMenu", submenu).ClickByObjectCoordinates();
                                Thread.Sleep(100);
                                //prompt click on FF when the clicked item isn't highlighted yet
                                if (!submenu.GetAttribute("class").Contains("active") && DlkEnvironment.mBrowser.ToLower() == "firefox")
                                    new DlkBaseControl("subMenu", submenu).Click(2, 2);
                            }
                        }
                        catch (StaleElementReferenceException)
                        {

                        }
                        Thread.Sleep(1500);
                        break;
                    }
                }
                if (!subMenuFound)
                    throw new Exception(String.Format("{0} not found.", item));
            }
        }

        /// <summary>
        /// Looks for the menu item's text element for more accurate clicking - for IE
        /// </summary>
        private void ClickMenuItemTextElements(string[] parameters)
        {
            foreach (var param in parameters)
            {
                var subMenuList = GetCurrentMenuList();
                if (subMenuList.Count > 0)
                {
                    if (!subMenuList.Any(x => x.FindElement(By.XPath(".//*[contains(@id, 'textEl')]")).Text.Trim() == param))
                    {
                        //try to scroll down current item list
                        IWebElement lastItemElement = subMenuList.Last().FindElement(By.XPath(".//*[contains(@id, 'textEl')]"));
                        string lastItemText = lastItemElement.Text.Trim();
                        if (mElement.FindElements(By.XPath("//div[contains(@class,'x-box-scroller x-menu-scroll-bottom')]")).Any(x => x.Displayed))
                        {
                            int lastItemRepeatCount = 0;
                            bool isFound = false;
                            IWebElement arrowDown = mElement.FindElements(By.XPath("//div[contains(@class,'x-box-scroller x-menu-scroll-bottom')]")).Where(x => x.Displayed).First();
                            while (lastItemRepeatCount < 3)
                            {
                                // arrow down button doesn't always change last item - check for same item 3 times before assuming end of list
                                DlkLogger.LogInfo("Clicking arrow down button...");
                                arrowDown.Click();
                                subMenuList = GetCurrentMenuList();
                                lastItemElement = subMenuList.Last(x => x.Text != "").FindElement(By.XPath(".//*[contains(@id, 'textEl')]"));
                                if (lastItemText == lastItemElement.Text.Trim())
                                {
                                    lastItemRepeatCount++;
                                    if (lastItemRepeatCount == 3)
                                    {
                                        DlkLogger.LogInfo("Reached the end of the item list.");
                                    }
                                }
                                else
                                {
                                    lastItemRepeatCount = 0;
                                }
                                lastItemText = lastItemElement.Text.Trim();
                                if (lastItemText == param)
                                {
                                    DlkLogger.LogInfo("Navigating to " + param + "...");
                                    arrowDown.Click();
                                    lastItemElement.Click();
                                    Thread.Sleep(500);
                                    isFound = true;
                                    break;
                                }
                            }
                            if (!isFound)
                            {
                                throw new Exception("Parameter " + param + " not found in menu list");
                            }
                        }
                        else throw new Exception("Parameter " + param + " not found in menu list");
                    }
                    else
                    {
                        IWebElement itemText = subMenuList.Where(x => x.FindElement(By.XPath(".//*[contains(@id, 'textEl')]")).Text.Trim() == param).First();
                        DlkLogger.LogInfo("Navigating to " + param + "...");
                        itemText.Click();
                        Thread.Sleep(500);
                    }
                }
                else throw new Exception("No menu for screens found.");
            }
        }

        /// <summary>
        /// Gets the current active/selected item in the menu list
        /// </summary>
        /// <returns>IWebElement Menu Item</returns>
        private IWebElement GetSelectedMenuItem()
        {
            var highlightedMenuItems = mElement.FindElements(By.XPath("//div[contains(@class,'x-panel x-layer') and not(contains(@style,'visibility: hidden'))][last()]//div[contains(@class,'x-box-item') and contains(@class, 'active')]")).Where(obj => obj.Displayed).ToList();

            if (highlightedMenuItems.Count > 0) return highlightedMenuItems.FirstOrDefault();
            else throw new Exception("Unable to select from the screen menu.");
        }
        
        private List<IWebElement> GetCurrentMenuList()
        {
            return mElement.FindElements(By.XPath("//div[contains(@class,'x-panel x-layer') and not(contains(@style,'visibility: hidden'))][last()]//div[contains(@class,'x-box-item') and not(contains(@class, 'disabled')) and not(contains(@id, 'separator'))]  | //*[contains(@class, 'rich-menu-item-enabled')] | //select/following-sibling::div[contains(@id, 'form')]//*[contains(@class, 'menu-list-border') and not(contains(@style, 'none;'))]//*[@onclick]")).Where(x => x.Displayed).ToList();
        }
        #endregion
        #region AutoMapper
        private List<IWebElement> GetMenuItemWithNoArrow()
        {
            return mElement.FindElements(By.XPath("//div[contains(@class,'x-panel x-layer') and not(contains(@style,'visibility: hidden'))][last()]//img[not(contains(@class,'x-menu-item-arrow'))]/ancestor::div[1]")).ToList();
        }
        private List<IWebElement> GetMenuItemWithArrow()
        {
            return mElement.FindElements(By.XPath("//div[contains(@class,'x-panel x-layer') and not(contains(@style,'visibility: hidden'))][last()]//img[contains(@class,'x-menu-item-arrow')]/ancestor::div[1]")).ToList();
        }
        private string GetCurrentMenuTitle()
        {
            return mElement.FindElement(By.XPath("//*[@id='mainMenuToolbar']//*[contains(@class,'x-btn x-unselectable')][last()]")).Text;
        }
        private string GetObjectStorePath()
        {
            return @"C:\Users\robinlavares\Workspace 2019\Products\SFT\Framework\ObjectStore";
        }
        private string GetOSScreenName()
        {
            var list = mElement.FindElements(By.XPath("//*[@id='mainMenuToolbar']//*[contains(@class,'x-btn x-unselectable')]"))
                   .Where(x => !String.IsNullOrEmpty(x.Text.Trim()))
                   .Select(x => x.Text.Trim()).ToList();
            var osName = "OS_" + String.Join("_", list).Replace(" ", "") + ".xml";
            return osName;
        }
        private List<FormControl> GetFormControls(out string formTitle, string xPathParentForControls = "", string parentTab = "", bool isDialogBox = false)
        {
            List<FormControl> list = new List<FormControl>();
            try
            {
                formTitle = GetCurrentMenuTitle();
            }
            catch
            {
                formTitle = "";
            }
            string formTitleString = formTitle;
            DlkBaseControl formTitleAndFormSubtitle = GetFormTitleAndFormSubTitle();

            string parentName = (!String.IsNullOrEmpty(parentTab) ? parentTab + "_" : "");

            if (!isDialogBox && formTitleAndFormSubtitle.mElms != null)
            {
                list.AddRange(formTitleAndFormSubtitle.mElms
                        .Where(x => !String.IsNullOrEmpty(x.Text.Trim()))
                        .Select(x => new FormControl()
                        {
                            IsFormTitle = (formTitleString == x.Text.Trim() ? true : false),
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "Label",
                            ControlName = (formTitleString == x.Text.Trim() ? "FormTitle" : "SubFormTitle"),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }
            else
            {
                DlkBaseControl dialogBoxTitle = GetFormControl(ControlType.DialogBoxTitle, xPathParentForControls, true);

                if (dialogBoxTitle != null && dialogBoxTitle.mElms != null)
                {
                    list.AddRange(dialogBoxTitle.mElms
                            .Select(x => new FormControl()
                            {
                                XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                                ControlType = "Label",
                                ControlName = "DialogBoxTitle",
                                SearchMethod = "IFRAME_XPATH"
                            }).ToList());
                }

            }

            DlkBaseControl formButtons = GetFormControl(ControlType.Button, xPathParentForControls);

            if (formButtons != null && formButtons.mElms != null)
            {
                list.AddRange(formButtons.mElms
                        .Select(x => new FormControl()
                        {
                            IsFormTitle = (formTitleString == x.Text.Trim() ? true : false),
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']{(x.Text.Trim() == "Filter" ? "/parent::*" : "")}",
                            ControlType = (x.Text.Trim() == "Filter" ? "Toggle" : "Button"),
                            ControlName = parentName + x.Text.Trim().Replace(" ", ""),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formCombBoxes = GetFormControl(ControlType.ComboBox, xPathParentForControls, true);

            if (formCombBoxes != null && formCombBoxes.mElms != null)
            {
                list.AddRange(formCombBoxes.mElms
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = GetFormControlLabel($"//*[@id='{x.GetAttribute("id")}']", isDialogBox).Contains("Date") ? "Calendar" : "ComboBox",
                            ControlName = parentName + GetFormControlLabel($"//*[@id='{x.GetAttribute("id")}']", isDialogBox).Replace(" ", ""),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formTextBoxes = GetFormControl(ControlType.TextBox, xPathParentForControls, true);

            if (formTextBoxes != null && formTextBoxes.mElms != null)
            {
                list.AddRange(formTextBoxes.mElms
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "TextBox",
                            ControlName = parentName + GetFormControlLabel($"//*[@id='{x.GetAttribute("id")}']", isDialogBox).Replace(" ", ""),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formCheckBoxes = GetFormControl(ControlType.CheckBox, xPathParentForControls, true);

            if (formCheckBoxes != null && formCheckBoxes.mElms != null)
            {
                list.AddRange(formCheckBoxes.mElms
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "CheckBox",
                            ControlName = parentName + GetFormControlLabel($"//*[@id='{x.GetAttribute("id")}']", isDialogBox).Replace(" ", ""),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formNumerics = GetFormControl(ControlType.Numeric, xPathParentForControls, true);

            if (formNumerics != null && formNumerics.mElms != null)
            {
                list.AddRange(formNumerics.mElms
                        .Select(x => new FormControl()
                        {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "TextBox",
                            ControlName = parentName + GetFormControlLabel($"//*[@id='{x.GetAttribute("id")}']", isDialogBox).Replace(" ", ""),
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }


            DlkBaseControl formTabPages = GetFormControl(ControlType.TabPage, xPathParentForControls);

            if (formTabPages != null && formTabPages.mElms != null)
            {
                list.AddRange(formTabPages.mElms
                        .Select(x => new FormControl()
                        {
                            IsFormTitle = (formTitleString == x.Text.Trim() ? true : false),
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']{(x.Text.Trim() == "Filter" ? "/parent::*" : "")}",
                            ControlType = "Tab",
                            ControlName = parentName + x.Text.Trim().Replace(" ", "") + "Tab",
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            DlkBaseControl formGrids = GetFormControl(ControlType.Grid, xPathParentForControls);

            if (formGrids != null && formGrids.mElms != null) {
                list.AddRange(formGrids.mElms
                        .Select(x => new FormControl() {
                            XPath = $"contentFrame_//*[@id='{x.GetAttribute("id")}']",
                            ControlType = "Grid",
                            ControlName = parentName + "Grid",
                            SearchMethod = "IFRAME_XPATH"
                        }).ToList());
            }

            return list;
        }
        private DlkBaseControl GetFormControl(ControlType controlType, string parentXPATH, bool acceptEmptyString = false)
        {
            string controlBaseXPath = GetControlBaseXPath(controlType);
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkLabel label = new DlkLabel("Control", "IFRAME_XPATH_MULTIPLE_RETURN", "contentFrame_" + parentXPATH + controlBaseXPath);
            try { label.FindElement(3); } catch { }
            if (label != null && label.mElms != null)
            {
                label.mElms = label.mElms.Where(row =>
                {
                    return (acceptEmptyString ? true : !String.IsNullOrEmpty(row.Text.Trim())) && row.Displayed;
                }).ToList();

            }
            return label;
        }
        private string GetFormControlLabel(string controlXPath, bool isDialogBox = false)
        {
            string ancestorXPath = "";
            if (isDialogBox)
                ancestorXPath = "/ancestor::td[last()]/preceding-sibling::td[1]";//"/ancestor::tr[2]//*[contains(@class,'x-form-display-field')]";
            else
                ancestorXPath = "/ancestor::table[1]//label";

            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkLabel label = new DlkLabel("Control", "IFRAME_XPATH", "contentFrame_" + controlXPath + ancestorXPath);
            try
            {
                label.FindElement(3);
            }
            catch
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                label = new DlkLabel("Control", "IFRAME_XPATH", "contentFrame_" + controlXPath + "/ancestor::table[2]//label");
                label.FindElement(3);
            }
            if (label != null && label.mElement != null)
            {
                return label.mElement.Text;
            }
            return "";
        }
        private string GetControlBaseXPath(ControlType control)
        {
            switch (control)
            {
                case ControlType.Button:
                    return "//*[contains(@class,'x-btn-wrap')]";
                case ControlType.ComboBox:
                    return "//input/parent::*/following-sibling::td[contains(@class,'x-trigger-cell x-unselectable')]/ancestor::table[1]";
                case ControlType.TextBox:
                    return "//input/ancestor::table[1][not(contains(@class,'x-form-trigger-wrap'))]//input[not(contains(@class,'checkbox'))]";
                case ControlType.CheckBox:
                    return "//input/ancestor::table[1][not(contains(@class,'x-form-trigger-wrap'))]//input[contains(@class,'checkbox')]";
                case ControlType.Numeric:
                    return "//input/parent::*/following-sibling::td[@class='x-trigger-cell']/ancestor::table[1]//input";
                case ControlType.TabPage:
                    return "//div[contains(@class,'x-tab x-unselectable')]";
                case ControlType.Grid:
                    return "//*[substring(@id,string-length(@id) -string-length('_grid') +1) = '_grid']";
                case ControlType.DialogBoxTitle:
                    return "//*[@class='x-header-text x-window-header-text']";
                default:
                    return "";
            }
        }
        private static DlkBaseControl GetFormTitleAndFormSubTitle()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkLabel label = new DlkLabel("Control", "IFRAME_XPATH_MULTIPLE_RETURN", "contentFrame_//*[contains(@class,'x-panel-header-text-container')]");
            try { label.Initialize(); } catch { }

            return label;
        }
       [Keyword("AutoMapper")]
        public void AutoMapper(string ObjectStorePath, string XPathParent, string ParentTabName, string ScreenFileName, string IsDialogBox)
        {

            DialogResult dialogResult = MessageBox.Show($"Auto Map? {ParentTabName}", "Auto Mapper", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult != DialogResult.Yes)
                return;

            XDocument document;
            XElement root;
            string formTitle;
            string osName = "";
            IsInit = false;
            bool isDialogBox = bool.Parse(IsDialogBox);
            ScreenFileName += ".xml";

            if (String.IsNullOrEmpty(ScreenFileName))
            {
                DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                mSearchValues[0] = "//*[@id='mainMenuToolbar']//*[contains(@class,'x-unselectable')][position() =1]";
                Initialize();

                osName = GetOSScreenName();
            }

            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            var formControls = GetFormControls(out formTitle, XPathParent, ParentTabName, isDialogBox);



            string filePath = $@"{ObjectStorePath}\OS_{((!String.IsNullOrEmpty(ScreenFileName)) ? ScreenFileName : osName)}";


            if (File.Exists(filePath))
            {
                document = XDocument.Load(filePath);
                root = document.Element("objectstore");
            }
            else
            {
                root = new XElement("root", new XAttribute("screen", ((!String.IsNullOrEmpty(ScreenFileName)) ? ScreenFileName : osName).Replace("OS_", "").Replace(".xml", "")));
                root.Name = "objectstore";
                root.Descendants("control");
                document = new XDocument(root);
            }


            foreach (var formControl in formControls)
            {
                if (!document.ToString().Contains(formControl.ControlName))
                {
                    var control = new XElement("control", new XAttribute("key", formControl.ControlName));
                    var controlType = new XElement("controltype");
                    controlType.Value = formControl.ControlType;
                    var searchmethod = new XElement("searchmethod");
                    searchmethod.Value = formControl.SearchMethod;
                    var searchparameters = new XElement("searchparameters");
                    searchparameters.Value = formControl.XPath;
                    control.Add(controlType);
                    control.Add(searchmethod);
                    control.Add(searchparameters);
                    root.Add(control);
                }
            }
            Thread.Sleep(1000);
            document.Save(filePath);
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(filePath);
            process.Start();
        }
        private class Menuitem
        {
            public string ParentMenu { get; set; }
            public string Menu { get; set; }
            public bool IsParent { get; set; }
            public bool IsExplored { get; set; }
        }
        public class FormControl
        {
            public bool IsFormTitle { get; set; }
            public string XPath { get; set; }
            public string ControlType { get; set; }
            public string ControlName { get; set; }
            public string SearchMethod { get; set; }
        }
        private enum ControlType
        {
            TextBox,
            ComboBox,
            Button,
            CheckBox,
            Numeric,
            TabPage,
            Grid,
            DialogBoxTitle
        }
        #endregion
    }
}
