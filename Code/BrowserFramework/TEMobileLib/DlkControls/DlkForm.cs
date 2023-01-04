using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using TEMobileLib.DlkUtility;

namespace TEMobileLib.DlkControls
{
    [ControlType("Form")]
    public class DlkForm : DlkBaseControl
    {
        private String m_strHeaderSiblingXpath = "./descendant::div[substring(@class,string-length(@class)-5)='Header']";
        private String m_strHeaderSiblingXpath_Lookup = "./descendant::div[substring(@class,string-length(@class)-5)='Header']//following-sibling::*[contains(@class, 'flbl')]";
        private String m_strLabelXpath = "div[substring(@class,string-length(@class)-4)='Label']";

        const String ANDROID = "android";
        const String CHROME = "chrome";
        const String IOS = "ios";
        const String SAFARI = "safari";

        public DlkForm(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkForm(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkForm(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
                FindElement();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Expected Title|Sample form title" })]
        public void VerifyTitle(String ExpectedTitle)
        {
            try
            {

                Initialize();
                String strActualTitle = GetTitle();

                DlkAssert.AssertEqual("VerifyTitle() : " + mControlName, ExpectedTitle.Trim(), strActualTitle.Trim());
                DlkLogger.LogInfo("VerifyTitle() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickButton", new String[] { "1|text|Button Name|New or Copy~Copy Record" })]
        public void ClickButton(string ButtonName)
        {
            String[] arrInputString = null;
            bool bFound = false;

            try
            {
                DlkTEMobileCommon.WaitForScreenToLoad();
                Initialize();
                DlkBaseControl ctlButton = null;
                DlkBaseControl ctlDropdownButton = null;

                string CSS_Title;
                string CSS_Name;
                string CSS_ID;
                string XPath_Text;
                IWebElement Button = null;

                var browser = DlkEnvironment.mBrowser.ToLower();
                browser = browser.Contains(IOS) ? IOS
                    : browser.Contains(ANDROID) ? ANDROID
                    : browser;

                // To click on a MainForm button w/ no dropdown. (i.e. New, Delete) 
                if (!ButtonName.Contains("~")) 
                {
                    CSS_Title = "*[title*='" + ButtonName.Trim() + "']";
                    CSS_Name = "*[name*='" + ButtonName.Trim() + "']";
                    CSS_ID = "*[id*='" + ButtonName.Trim() + "']";
                    XPath_Text = ".//div[contains(text(),'" + ButtonName.Trim() + "')]";

                    Button = mElement.FindWebElementCoalesce(By.CssSelector(CSS_Title), By.CssSelector(CSS_Name), By.CssSelector(CSS_ID), By.XPath(XPath_Text));

                    if (Button != null)
                        ctlButton = new DlkBaseControl(ButtonName, Button);
                    else
                        throw new Exception("Button control = '" + ButtonName + "' not found");

                    switch (browser)
                    {
                        case IOS:
                        case SAFARI:
                            DlkTEMobileCommon.WaitForScreenToLoad();
                            DlkTEMobileCommon.WaitForElementToLoad(ctlButton);
                            ctlButton.MouseOverUsingJavaScript();
                            ctlButton.ClickUsingJavaScript(false);
                            break;

                        case ANDROID:
                        case CHROME:
                            ctlButton.MouseOver();
                            ctlButton.Click();
                            break;
                        default:
                            throw new Exception($"Browser [{browser}] is not yet supported.");
                    }

                    DlkLogger.LogInfo("Successfully executed ClickButton()");
                }
                else if (ButtonName.Contains("Dropdown")) // for direct dropdown interaction with no adjacent controls.
                {
                    string XPath_ButtonDropdownImg = ".//div[@id='nwRwMnuBttn']/span[@class='tbBtnSplitImg']";
                    string XPath_ButtonDropDown = ".//div[contains(@id,'Mnu')]";
                    IWebElement ButtonDropDown = mElement.FindWebElementCoalesce(By.XPath(XPath_ButtonDropDown), By.XPath(XPath_ButtonDropdownImg));
                    if (ButtonDropDown != null)
                    {
                        arrInputString = ButtonName.Split('~');

                        // Find button dropdown and execute click action
                        ctlDropdownButton = new DlkBaseControl("Dropdown", ButtonDropDown);

                        switch (browser)
                        {
                            case IOS:
                            case SAFARI:
                                DlkTEMobileCommon.WaitForScreenToLoad();
                                DlkTEMobileCommon.WaitForElementToLoad(ctlDropdownButton);
                                ctlDropdownButton.MouseOverUsingJavaScript();
                                ctlDropdownButton.ClickUsingJavaScript(false);
                                break;

                            case ANDROID:
                            case CHROME:
                                ctlDropdownButton.MouseOver();
                                ctlDropdownButton.Click();
                                break;
                            default:
                                throw new Exception($"Browser [{browser}] is not yet supported.");
                        }

                        DlkLogger.LogInfo("Successfully clicked the button dropdown control.");
                    }
                    else
                    {
                        throw new Exception("Button dropdown control not found");
                    }

                    // Retrieve all the items from the dropdown list after dropdown button was clicked
                    DlkBaseControl ctlDropdownList = null;
                    DlkBaseControl ctlItem = null;
                    string XPath_List = "//div[@class='tlbrDDMenuDiv' and contains(@style, 'display: block')]";
                    string XPath_ListItem = "//div[@class='tlbrDDActionDiv']";
                    ctlDropdownList = new DlkBaseControl("List", mElement.FindElement(By.XPath(XPath_List)));

                    foreach (IWebElement ctl in ctlDropdownList.mElement.FindElements(By.XPath(XPath_ListItem)))
                    {
                        ctlItem = new DlkBaseControl("DdItemList", ctl);
                        if (ctlItem.GetAttributeValue("textContent") == arrInputString[1].Trim())
                        {
                            switch (browser)
                            {
                                case IOS:
                                case SAFARI:
                                    ctlItem.ClickUsingJavaScript(false);
                                    break;

                                case ANDROID:
                                case CHROME:
                                    ctlItem.Click();
                                    break;
                                default:
                                    throw new Exception($"Browser [{browser}] is not yet supported.");
                            }
                            
                            bFound = true;
                            break;
                        }
                    }

                    if (!bFound)
                    {
                        throw new Exception(arrInputString[1] + " not found in the dropdown list.");
                    }
                }
                else // To support instances when the dropdown button is clicked and an item from the list needs to be selected. (i.e. Copy~Copy Record)
                {
                    arrInputString = ButtonName.Split('~');
                    CSS_Title = "*[title*='" + arrInputString[0].Trim() + "']";
                    CSS_Name = "*[name*='" + arrInputString[0].Trim() + "']";
                    CSS_ID = "*[id*='" + arrInputString[0].Trim() + "']";
                    XPath_Text = ".//div[contains(text(),'" + arrInputString[0].Trim() + "')]";
                    string XPath_ButtonDropdownImg = "./following-sibling::div/span[@class='tbBtnSplitImg']";
                    string XPath_ButtonDropDown = "./following-sibling::div[contains(@id,'Mnu')]";

                    Button = mElement.FindWebElementCoalesce(By.CssSelector(CSS_Title), By.CssSelector(CSS_Name), By.CssSelector(CSS_ID), By.XPath(XPath_Text));
                    
                    // Find MainForm button (i.e. Copy). Same as code in if condition above except for clicking on the button once found.
                    if (Button != null)
                    {
                        ctlButton = new DlkBaseControl(arrInputString[0], Button);
                        // Find whether MainForm button has a dropdown control (i.e. Copy button dropdown)
                        IWebElement ButtonDropDown = ctlButton.mElement.FindWebElementCoalesce(By.XPath(XPath_ButtonDropDown), By.XPath(XPath_ButtonDropdownImg));
                        if (ButtonDropDown != null)
                        {
                            // Find button dropdown and execute click action
                            ctlDropdownButton = new DlkBaseControl(arrInputString[0], ButtonDropDown);

                            switch (browser)
                            {
                                case IOS:
                                case SAFARI:
                                    DlkTEMobileCommon.WaitForScreenToLoad();
                                    DlkTEMobileCommon.WaitForElementToLoad(ctlDropdownButton);
                                    ctlDropdownButton.MouseOverUsingJavaScript();
                                    ctlDropdownButton.ClickUsingJavaScript(false);
                                    break;

                                case ANDROID:
                                case CHROME:
                                    ctlDropdownButton.MouseOver();
                                    ctlDropdownButton.Click();
                                    break;
                                default:
                                    throw new Exception($"Browser [{browser}] is not yet supported.");
                            }

                            DlkLogger.LogInfo("Successfully clicked the button dropdown control.");
                        }
                        else
                        {
                            throw new Exception("Button dropdown control = '" + arrInputString[0] + "' not found");
                        }
                    }
                    else
                    {
                        throw new Exception("Button control = '" + arrInputString[0] + "' not found");
                    }

                    // Retrieve all the items from the dropdown list after dropdown button was clicked
                    DlkBaseControl ctlDropdownList = null;
                    DlkBaseControl ctlItem = null;
                    string XPath_List = "//div[@class='tlbrDDMenuDiv' and contains(@style, 'display: block')]";
                    string XPath_ListItem = "//div[@class='tlbrDDActionDiv']";
                    ctlDropdownList = new DlkBaseControl("List", mElement.FindElement(By.XPath(XPath_List)));

                    foreach (IWebElement ctl in ctlDropdownList.mElement.FindElements(By.XPath(XPath_ListItem)))
                    {
                        ctlItem = new DlkBaseControl("DdItemList", ctl);
                        if (ctlItem.GetAttributeValue("textContent") == arrInputString[1].Trim())
                        {
                            switch (browser)
                            {
                                case IOS:
                                case SAFARI:
                                    ctlItem.ClickUsingJavaScript(false);
                                    break;

                                case ANDROID:
                                case CHROME:
                                    ctlItem.Click();
                                    break;
                                default:
                                    throw new Exception($"Browser [{browser}] is not yet supported.");
                            }

                            bFound = true;
                            break;
                        }
                    }

                    if (!bFound)
                    {
                        throw new Exception(arrInputString[1] + " not found in the dropdown list.");
                    }

                    DlkLogger.LogInfo("Successfully executed ClickButton()");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickButton() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickButtonIfExists", new String[] { "1|text|Button Name|New" })]
        public void ClickButtonIfExists(string ButtonName)
        {
            try
            {
                DlkTEMobileCommon.WaitForScreenToLoad();
                Initialize();
                DlkBaseControl ctlButton = null;
                if (mElement.FindElements(By.CssSelector("*[title*='" + ButtonName.Trim() + "']")).Count <= 0)
                {
                    if (mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName.Trim() + "')]")).Count <= 0)
                    {
                        //throw new DlkException("Button control = '" + ButtonName + "' not found");
                        DlkLogger.LogInfo("ClickButtonIfExists() : " + ButtonName + " does not exist.");
                        DlkLogger.LogInfo("Successfully executed ClickButtonIfExists()");
                        return;
                    }
                    else
                    {
                        ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName.Trim() + "')]"))[0]);
                    }
                }
                else
                {
                    ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + ButtonName.Trim() + "']"))[0]);
                }

                var browser = DlkEnvironment.mBrowser.ToLower();
                browser = browser.Contains(IOS) ? IOS
                    : browser.Contains(ANDROID) ? ANDROID
                    : browser;

                switch (browser)
                {
                    case IOS:
                    case SAFARI:
                        DlkTEMobileCommon.WaitForScreenToLoad();
                        DlkTEMobileCommon.WaitForElementToLoad(ctlButton);
                        ctlButton.ClickUsingJavaScript(false);
                        break;

                    case ANDROID:
                    case CHROME:
                        ctlButton.MouseOver();
                        ctlButton.Click();
                        break;
                    default:
                        throw new Exception($"Browser [{browser}] is not yet supported.");
                }

                DlkLogger.LogInfo("Successfully executed ClickButtonIfExists()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickButton() failed : " + e.Message, e);
            }
        }


        [Keyword("ClickSection", new String[] { "1|text|Section Name|Value" })]
        public void ClickSection(string SectionName)
        {
            try
            {
                Initialize();
                var section = new DlkSection("section", mElement);
                section.Click(SectionName.Trim());
                DlkLogger.LogInfo("Successfully executed ClickSection()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickSection() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickSectionByInstanceNumber", new String[] { "1|text|Section Name|Value" })]
        public void ClickSectionByInstanceNumber(string SectionName, string InstanceNumber)
        {
            try
            {
                int index;
                if (!Int32.TryParse(InstanceNumber, out index) && index < 1) throw new Exception($"Invalid index {InstanceNumber}.");

                Initialize();

                var sections = mElement.FindElements(By.XPath("//div[@id='0']/form[1]//*[(contains(@id, 'OBJ') or contains(@id, 'PIB') or contains(@id, 'GB')) and contains(@class, 'gBx')]")).Where(s => s.Displayed).ToList();

                if (sections.Count() < 1) throw new Exception("no section found.");

                sections = sections.Select(s => {
                    var sctn = s.FindElements(By.XPath("./following-sibling::span[(contains(@id, 'GB') or contains(@id, 'OBJ')) and contains(@class, 'glbl')] | ./*[contains(@class, 'Label')]")).FirstOrDefault();
                    if (sctn == null) throw new Exception("sctn not found");
                    return sctn;
                }).Where(s => s.Text.ToLower().Trim() == SectionName.ToLower().Trim()).ToList();

                if (sections.Count() < 1) throw new Exception("no section found.");
                if (index > sections.Count()) throw new Exception($"Instance Count [{sections.Count()}] is greater than InstanceNumber [{InstanceNumber}]");

                var section = sections.ElementAt(index-1);

                if (section == null) throw new Exception("no section found.");

                new DlkBaseControl("section", section).ClickUsingJavaScript(false);

                DlkLogger.LogInfo("Successfully executed ClickSectionByInstanceNumber()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickSectionByInstanceNumber() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickFormHeaderByName", new String[] { "1|text|Section Name|Value" })]
        public void ClickFormHeaderByName(string FormHeaderName)
        {
            try
            {
                DlkTEMobileCommon.WaitForScreenToLoad();
                var formHeaders = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[contains(@class, 'gBx') and contains(@class, 'Label')]")).Where(fh => fh.Displayed).ToList();

                if (formHeaders.Count() < 1) throw new Exception("No Form Headers not found.");

                var formheader = formHeaders.FirstOrDefault(fh => fh.Text.Trim() == FormHeaderName.Trim());

                if (formheader == null) throw new Exception($"{FormHeaderName} Form Header not found.");

                var browser = DlkEnvironment.mBrowser.ToLower();
                browser = browser.Contains(IOS) ? IOS
                    : browser.Contains(ANDROID) ? ANDROID
                    : browser;

                switch (browser)
                {
                    case IOS:
                    case SAFARI:
                        var headerToClick = new DlkBaseControl("test", formheader);
                        headerToClick.Click();

                        //some form headers do not expand on the first click.
                        //retry click if the link underline is still visible on the form header
                        if (headerToClick.mElement.GetCssValue("text-decoration") == "underline")
                        {
                            ClickHeaderUsingJavascript(headerToClick);
                        }
                        break;

                    case ANDROID:
                    case CHROME:
                        formheader.Click();
                        var androidHeader = new DlkBaseControl("FormHeader", formheader);
                        bool isHeaderUnderlined = androidHeader.mElement.GetCssValue("text-decoration").Contains("underline");
                        bool isHeaderTextSame = formheader.Text == FormHeaderName.Trim();
                        int clickRetryCount = 0;
                        while ((isHeaderUnderlined || isHeaderTextSame) && clickRetryCount < 3)
                        {
                            ClickHeaderUsingJavascript(androidHeader);
                            Thread.Sleep(300);
                            clickRetryCount++;
                            if (isHeaderTextSame != (formheader.Text == FormHeaderName.Trim()))
                            {
                                break;
                            }
                            if (isHeaderUnderlined != (androidHeader.mElement.GetCssValue("text-decoration").Contains("underline")))
                            {
                                break;
                            }
                        }
                        DlkLogger.LogInfo(string.Format("Header clicked {0} time/s", (clickRetryCount + 1).ToString()));
                        break;
                    default:
                        throw new Exception($"Browser [{browser}] is not yet supported.");
                }

                DlkLogger.LogInfo("Successfully executed ClickFormHeaderByName()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickFormHeaderByName() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickFormHeaderByIndex", new String[] { "1|text|Section Name|Value" })]
        public void ClickFormHeaderByIndex(string FormHeaderIndex)
        {
            try
            {
                int fhIndex;
                if(!Int32.TryParse(FormHeaderIndex, out fhIndex) && fhIndex < 1) throw new Exception($"Invalid index {FormHeaderIndex}.");

                var formHeaders = DlkEnvironment.AutoDriver
                    .FindElements(By.XPath("//*[contains(@class, 'gBx') and contains(@class, 'Label')]"))
                    .Where(fh => fh.Displayed).Where(fh => {
                        var form = fh.FindElements(By.XPath(".//ancestor::form//*[(@id='tblvw' or @id='frmvw') and not(contains(@style, 'display: none;'))] | .//preceding-sibling::form//*[(@id='tblvw' or @id='frmvw') and not(contains(@style, 'display: none;'))]")).FirstOrDefault();
                        if (form == null) return true;
                        return !form.Displayed;
                    }).ToList();

                if (formHeaders.Count() < 1) throw new Exception("No Form Headers not found.");

                var formheader = formHeaders.ElementAt(fhIndex-1);

                if (formheader == null) throw new Exception($"Form Header at {FormHeaderIndex} not found.");
                try { formheader.Click(); }
                catch { new DlkBaseControl("formHeader", formheader).ClickUsingJavaScript(false); }
                

                DlkLogger.LogInfo("Successfully executed ClickFormHeaderByIndex()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickFormHeaderByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("CLoseChildForm")]
        public  void CLoseChildForm()
        {
            try
            {
                var formheader = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[@id='0']/*[contains(@class, 'rsHeader')]//following-sibling::*[contains(@class, 'Label')]")).FirstOrDefault();
                if (formheader == null) throw new Exception("FormHeader nout found.");

                formheader.Click();
                DlkLogger.LogInfo("Successfully executed CLoseChildForm()");
            }
            catch (Exception e)
            {
                throw new Exception("CLoseChildForm() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifySectionIfExpanded", new String[] { "1|text|IsExpanded|TRUE",
                                                    "2|text|Section Name|Sample Value" })]
        public void VerifySectionIfExpanded(String isExpanded, String SectionName)
        {
            try
            {
                Initialize();
                var section = new DlkSection("section", mElement);
                section.VerifyIfExpanded(isExpanded, SectionName);
                DlkLogger.LogInfo("Successfully executed VerifySectionIfExpanded()");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySectionIfExpanded() failed : " + e.Message, e);
            }
        }

        [Keyword("SetCheckBoxValue", new String[] { "1|text|CheckBox Name|Sample name", "2|text|Value|TRUE" })]
        public void SetCheckBoxValue(String CheckBoxName, String IsChecked)
        {
            try
            {
                Initialize();

                DlkBaseControl ctlCheckBox = new DlkBaseControl("CheckBox", mElement.FindElement(By.CssSelector("input[title*='" + CheckBoxName + "']")));

                if (!ctlCheckBox.Exists())
                {
                    throw new Exception("Checkbox = '" + CheckBoxName + "' not found");
                }

                Boolean bIsChecked = Convert.ToBoolean(IsChecked);
                Boolean bCurrentState = Convert.ToBoolean(ctlCheckBox.GetAttributeValue("checked"));

                if (bCurrentState != bIsChecked)
                {
                    // Click on parent node first to solve issue on IE, then send space key instead of click 
                    if (DlkEnvironment.mBrowser.Equals("ie"))
                    {
                        ctlCheckBox.mElement.FindElement(By.XPath("..")).Click();
                        ctlCheckBox.mElement.SendKeys(OpenQA.Selenium.Keys.Space);
                        // wait before state check
                        Thread.Sleep(4300);
                    }
                    else
                    {
                        ctlCheckBox.Click(4.3);
                    }

                    // Verify if IsChecked was set
                    bCurrentState = Convert.ToBoolean(ctlCheckBox.GetAttributeValue("checked"));
                    DlkAssert.AssertEqual("SetCheckBoxValue()", bIsChecked, bCurrentState);
                }

                // UI BUG: when any click is invoked on any un-focused control after this checkbox is clicked, this checkbox is also clicked
                DlkLogger.LogInfo("Successfully executed SetCheckBoxValue() : " + CheckBoxName);
            }
            catch (Exception e)
            {
                throw new Exception("SetCheckBoxValue() failed : " + e.Message, e);
            }
        }

        [Keyword("Close")]
        public void Close()
        {
            try
            {
                Initialize();

                String CloseButton = "Close";
                //CloseButton = Costpoint.TranslateParameter(CloseButton);

                DlkButton ctlCloseButton = null;
                if (mElement.FindElements(By.CssSelector("*[title*='" + CloseButton + "']")).Count == 1)
                {
                    ctlCloseButton = new DlkButton("CloseButton", mElement.FindElements(By.CssSelector("*[title*='" + CloseButton + "']"))[0]);
                }
                else
                {
                    // handling for 'tblcls' and 'frmcls'
                    foreach (IWebElement ctl in mElement.FindElements(By.CssSelector("*[title*='" + CloseButton + "']")))
                    {
                        ctlCloseButton = new DlkButton("CloseButton", ctl);
                        if (ctlCloseButton.Exists())
                        {
                            break;
                        }

                    }
                    if (ctlCloseButton == null || !ctlCloseButton.Exists())
                    {
                        throw new Exception("Close button not found");
                    }
                }

                ctlCloseButton.Initialize();
                ctlCloseButton.ScrollIntoViewUsingJavaScript();
                ctlCloseButton.Click();
                DlkLogger.LogInfo("Successfully executed Close()");
            }
            catch (Exception e)
            {
                throw new Exception("Close() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyButtonReadOnly", new String[] { "1|text|Button Name|Delete",
                                                     "2|text|Expected Value|TRUE" })]
        public void VerifyButtonReadOnly(String ButtonName, String ExpectedValue)
        {
            try
            {
                Initialize();
                DlkBaseControl ctlButton = null;
                String ActValue = "";
                String[] arrInputString = null;

                // To verify a MainForm button w/ no dropdown. (i.e. New, Delete) 
                if (!ButtonName.Contains("~"))
                {
                    if (mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']")).Count <= 0)
                    {
                        throw new Exception("Button control = '" + ButtonName + "' not found.");
                    }
                    ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']"))[0]);
                    ActValue = CheckReadOnlyState(ctlButton);                 
                }
                else
                {
                    DlkBaseControl ctlDropdownButton = null;
                    DlkBaseControl ctlDropdownList = null;
                    DlkBaseControl ctlItem = null;

                    arrInputString = ButtonName.Split('~');
                    if (mElement.FindElements(By.CssSelector("*[title*='" + arrInputString[0] + "']")).Count <= 0)
                    {
                        throw new Exception("Button control = '" + arrInputString[0] + "' not found.");
                    }
                    ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + arrInputString[0] + "']"))[0]);

                    // Find whether MainForm button has a dropdown control (i.e. Copy button dropdown)
                    if (ctlButton.mElement.FindElements(By.XPath("./following-sibling::div/span[@class='tbBtnSplitImg']")).Count <= 0)
                    {
                        throw new Exception("Button dropdown control = '" + arrInputString[0] + "' not found");
                    }
                    else
                    {
                        ctlDropdownButton = new DlkBaseControl(arrInputString[0],
                            ctlButton.mElement.FindElements(By.XPath("./following-sibling::div[contains(@id,'Mnu')]"))[0]);

                        var browser = DlkEnvironment.mBrowser.ToLower();
                        browser = browser.Contains(IOS) ? IOS
                            : browser.Contains(ANDROID) ? ANDROID
                            : browser;

                        switch (browser)
                        {
                            case IOS:
                            case SAFARI:
                                DlkTEMobileCommon.WaitForScreenToLoad();
                                DlkTEMobileCommon.WaitForElementToLoad(ctlDropdownButton);
                                ctlDropdownButton.ClickUsingJavaScript(false);
                                break;

                            case ANDROID:
                            case CHROME:
                                ctlDropdownButton.MouseOver();
                                // Click on the dropdown button first to display list
                                ctlDropdownButton.Click();
                                break;
                            default:
                                throw new Exception($"Browser [{browser}] is not yet supported.");
                        }

                        DlkLogger.LogInfo("Successfully clicked the button dropdown control.");
                    }

                    // Retrieve all the items from the dropdown list after dropdown button was clicked
                    ctlDropdownList = new DlkBaseControl("List",
                        mElement.FindElement(By.XPath("//div[@class='tlbrDDMenuDiv' and contains(@style, 'display: block')]")));

                    foreach (IWebElement ctl in ctlDropdownList.mElement.FindElements(By.XPath("//div[@class='tlbrDDActionDiv']")))
                    {
                        ctlItem = new DlkBaseControl("DdItemList", ctl);
                        if (ctlItem.GetAttributeValue("textContent") == arrInputString[1])
                        {
                            ActValue = CheckReadOnlyState(ctlItem); // Verify if item in list is read only (i.e. Paste Data)
                            break;
                        }
                    }
                }
           
                DlkAssert.AssertEqual("VerifyAttribute()", ExpectedValue.ToLower(), ActValue.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyButtonReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyButtonExists", new String[] { "1|text|Button Name|New", 
                                                        "2|text|Expected Value|TRUE" })]
        public void VerifyButtonExists(string ButtonName, string ExpectedValue)
        {
            try
            {
                Initialize();
                DlkBaseControl ctlButton = null;
                String ActValue = "false";

                if (mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']")).Count > 0)
                {
                    ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']"))[0]);
                    ActValue = CheckVisibility(ctlButton);
                }
                else
                {
                    if (mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]")).Count > 0)
                    {
                        ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]"))[0]);
                        ActValue = CheckVisibility(ctlButton);
                    }
                    else
                    {
                        DlkLogger.LogInfo("VerifyButtonExists() : " + ButtonName + " does not exist.");
                        ActValue = "false";
                    }
                }

                DlkAssert.AssertEqual("VerifyButtonExists()", ExpectedValue.ToLower(), ActValue.ToLower());
                DlkLogger.LogInfo("Successfully executed VerifyButtonExists() for " + ButtonName + " button.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyButtonExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCheckBoxValue", new String[] { "1|text|CheckBox Name|Sample name", "2|text|Expected Value|TRUE" })]
        public void VerifyCheckBoxValue(String CheckBoxName, String ExpectedValue)
        {
            try
            {
                Initialize();

                DlkBaseControl ctlCheckBox = new DlkBaseControl("CheckBox", mElement.FindElement(By.CssSelector("input[title*='" + CheckBoxName + "']")));

                if (!ctlCheckBox.Exists())
                {
                    throw new Exception("Checkbox = '" + CheckBoxName + "' not found");
                }

                Boolean bIsChecked = Convert.ToBoolean(ExpectedValue);
                Boolean bCurrentState = Convert.ToBoolean(ctlCheckBox.GetAttributeValue("checked"));

                DlkAssert.AssertEqual("VerifyCheckBoxValue()", bIsChecked, bCurrentState);
                DlkLogger.LogInfo("Successfully executed VerifyCheckBoxValue() : " + CheckBoxName);           
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCheckBoxValue() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        private String CheckVisibility(DlkBaseControl btnControl)
        {
            String strVisible = "false";

            if (btnControl.mElement.Displayed)

            {
                strVisible = "true";
            }
            else
            {
                strVisible = "false";
            }

            return strVisible;
        }

        private String GetFormHeaderLabelText(IWebElement Header)
        {
            String strRet = null;
            DlkBaseControl ctlHeader = null;

            ctlHeader = new DlkBaseControl("Header", Header);
            // If multiple hits, check w/c one is displayed using the Exist() base routine
            if (ctlHeader.Exists())
            {
                // If found visible Header, look for Label control immediately following the visible Header node
                if (ctlHeader.mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath)).Count == 1)
                {
                    strRet = ctlHeader.mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath))[0].Text;
                }
                /* Check if code below is reacheable - jpv */
                // look for label at descendant nodes
                else if (mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath)).Count == 1)
                {
                    strRet = mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath))[0].Text;
                }
            }
            else if (mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath)).Count == 1) //visibility is hidden even if form is shown
            {
                strRet = mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath))[0].Text;
            }
            return strRet;
        }

        /// <summary>
        /// Check the readonly state of a button
        /// </summary>
        /// <param name="btnControl">button control to be checked</param>
        /// <returns>string value (True if it is readonly and False if not)</returns>
        private String CheckReadOnlyState(DlkBaseControl btnControl)
        {
            String strReadOnly = "";
            String strClass = "";

            try
            {
                //new way of checking disabled buttons
                strClass = btnControl.GetAttributeValue("dis");
                if (strClass.Contains("1"))
                {
                    strReadOnly = "TRUE";
                }
                else
                {
                    strReadOnly = "FALSE";
                }
            }
            catch
            {
                strClass = btnControl.GetAttributeValue("class");
                if (!string.IsNullOrEmpty(strClass) && strClass.Contains("Disabled"))
                {
                    strReadOnly = "TRUE";
                }
                else
                {
                    strReadOnly = "FALSE";
                }

                //additional check for disabled inputs
                strClass = btnControl.GetAttributeValue("disabled");
                if (!string.IsNullOrEmpty(strClass) && strClass.Contains("true"))
                {
                    strReadOnly = "TRUE";
                }

                //check for special case in Copy Dropdown menu. i.e. Copy~Paste Data
                strClass = btnControl.GetAttributeValue("mnudisabled");
                if (!string.IsNullOrEmpty(strClass) && strClass.Contains("Y"))
                {
                    strReadOnly = "TRUE";
                }
            }

            return strReadOnly;
        }

        private string GetTitle()
        {
            string strActualTitle = string.Empty;

            var titles = mElement.FindElements(By.XPath(m_strHeaderSiblingXpath));

            if (titles.Count() > 0)
            {
                foreach (IWebElement hdr in titles)
                {
                    strActualTitle = GetFormHeaderLabelText(hdr);
                    if (strActualTitle != null)
                    {
                        break;
                    }
                }
            }

            if(!string.IsNullOrEmpty(strActualTitle)) return strActualTitle;

            var title = mElement.FindElements(By.Id("rptHdr")).FirstOrDefault();

            // For report form
            if (title != null) strActualTitle = title.Text;

            if (!string.IsNullOrEmpty(strActualTitle)) return strActualTitle;

            title = mElement.FindElements(By.XPath(m_strHeaderSiblingXpath_Lookup)).FirstOrDefault();

            //for lookup
            if (title != null) strActualTitle = title.Text;

            title = DlkEnvironment.AutoDriver.FindElements(By.XPath("//*[@id='wMnuBar']//*[contains(@id, 'TitleSpan')]")).FirstOrDefault(elem => elem.Displayed);

            if (title != null) strActualTitle = title.Text.Trim();

            if (!string.IsNullOrEmpty(strActualTitle)) return strActualTitle;

            throw new Exception("Form label control not found");
        }

        private void ClickHeaderUsingJavascript(DlkBaseControl HeaderToClick)
        {
            if (!DlkTEMobileCommon.IsElementStale(HeaderToClick))
            {
                HeaderToClick.ClickUsingJavaScript(false);
            }
        }

    }
}
