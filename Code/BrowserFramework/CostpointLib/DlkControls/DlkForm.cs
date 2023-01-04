using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using System.Text.RegularExpressions;

namespace CostpointLib.DlkControls
{
    [ControlType("Form")]
    public class DlkForm : DlkBaseControl
    {
        private String m_strHeaderSiblingXpath = "./descendant::div[substring(@class,string-length(@class)-5)='Header']";
        private String m_strLabelXpath = "div[substring(@class,string-length(@class)-4)='Label']";
        private String m_strFormDragBarXpath = ".//span[@class='rsDragger']";
        private String m_strVisibleFormParentsXpath = "//div/form//div[@id='tbHdrDiv']/../..";

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
            String strActualTitle = null;

            try
            {

                Initialize();

                if (mElement.FindElements(By.XPath(m_strHeaderSiblingXpath)).Count > 0)
                {
                    foreach (IWebElement hdr in mElement.FindElements(By.XPath(m_strHeaderSiblingXpath)))
                    {
                        strActualTitle = GetFormHeaderLabelText(hdr);
                        if (strActualTitle != null)
                        {
                            break;
                        }
                    }
                }

                // For report form
                else if (mElement.FindElements(By.Id("rptHdr")).Count > 0)
                {
                    strActualTitle = mElement.FindElements(By.Id("rptHdr")).First().Text;
                }

                else
                {
                    throw new Exception("Form label control not found");
                }
                if (DlkEnvironment.mBrowser.ToLower() == "edge")
                {
                    ExpectedTitle = Regex.Replace(ExpectedTitle, @"\u00A0", " ");
                    strActualTitle = Regex.Replace(strActualTitle, @"\u00A0", " ");
                }
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
                Initialize();
                DlkBaseControl ctlButton = null;
                DlkBaseControl ctlDropdownButton = null;
                // To click on a MainForm button w/ no dropdown. (i.e. New, Delete) 
                if (!ButtonName.Contains("~")) 
                {
                    if (mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']")).Count <= 0)
                    {
                        if (mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]")).Count <= 0)
                        {
                            throw new Exception("Button control = '" + ButtonName + "' not found");
                        }
                        else
                        {
                            ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]"))[0]);
                        }
                    }
                    else
                    {
                        ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']"))[0]);
                    }

                    if (ButtonName == "Query" && new string[] { "firefox","chrome","edge" }.Contains(DlkEnvironment.mBrowser.ToLower()))
                        ctlButton.ScrollIntoViewUsingJavaScript();
                    else
                        ctlButton.MouseOver();

                    ctlButton.Click();
                    DlkLogger.LogInfo("Successfully executed ClickButton()");
                }
                else // To support instances when the dropdown button is clicked and an item from the list needs to be selected. (i.e. Copy~Copy Record)
                {
                    arrInputString = ButtonName.Split('~');
                    // Find MainForm button (i.e. Copy). Same as code in if condition above except for clicking on the button once found.
                    if (mElement.FindElements(By.CssSelector("*[title*='" + arrInputString[0] + "']")).Count <= 0) 
                    {
                        if (mElement.FindElements(By.XPath(".//div[contains(text(),'" + arrInputString[0] + "')]")).Count <= 0)
                        {
                            throw new Exception("Button control = '" + arrInputString[0] + "' not found");
                        }
                        else
                        {
                            ctlButton = new DlkBaseControl(arrInputString[0], 
                                mElement.FindElements(By.XPath(".//div[contains(text(),'" + arrInputString[0] + "')]"))[0]);
                        }
                    }
                    else
                    {
                        ctlButton = new DlkBaseControl(arrInputString[0], mElement.FindElements(By.CssSelector("*[title*='" + arrInputString[0] + "']"))[0]);
                        // Find whether MainForm button has a dropdown control (i.e. Copy button dropdown)
                        if (ctlButton.mElement.FindElements(By.XPath("./following-sibling::div/span[@class='tbBtnSplitImg']")).Count <= 0) 
                        {
                            throw new Exception("Button dropdown control = '" + arrInputString[0] + "' not found");
                        }
                        else
                        {
                            ctlDropdownButton = new DlkBaseControl(arrInputString[0],
                                ctlButton.mElement.FindElements(By.XPath("./following-sibling::div[contains(@id,'Mnu')]"))[0]);
                            // Click on the dropdown button first
                            ctlDropdownButton.MouseOver();
                            ctlDropdownButton.Click();
                            DlkLogger.LogInfo("Successfully clicked the button dropdown control.");
                        }
                    }
                    // Retrieve all the items from the dropdown list after dropdown button was clicked
                    DlkBaseControl ctlDropdownList = null;
                    DlkBaseControl ctlItem = null;
                    ctlDropdownList = new DlkBaseControl("List",
                        mElement.FindElement(By.XPath("//div[@class='tlbrDDMenuDiv' and contains(@style, 'display: block')]")));

                    foreach (IWebElement ctl in ctlDropdownList.mElement.FindElements(By.XPath("//div[@class='tlbrDDActionDiv']")))
                    {
                        ctlItem = new DlkBaseControl("DdItemList", ctl);
                        if (ctlItem.GetAttributeValue("textContent") == arrInputString[1])
                        {
                            ctlItem.Click();  // Click on item from list (i.e. Copy Record)
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

        [Keyword("ClickExactButtonIfExists", new String[] { "1|text|Button Name|New" })]
        public void ClickExactButtonIfExists(string ButtonName)
        {
            try
            {
                Initialize();
                DlkBaseControl ctlButton = null;
                IWebElement button = mElement.FindElements(By.XPath($"//div[string()='{ButtonName}']")).FirstOrDefault();

                if (button != null)
                {
                    ctlButton = new DlkBaseControl(ButtonName, button);
                    ctlButton.MouseOver();
                    ctlButton.Click();
                    DlkLogger.LogInfo("Successfully executed ClickExactButtonIfExists()");
                }
                else
                {
                    DlkLogger.LogInfo("ClickExactButtonIfExists() : " + ButtonName + " does not exist.");
                    DlkLogger.LogInfo("Successfully executed ClickExactButtonIfExists()");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickExactButtonIfExists() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickButtonIfExists", new String[] { "1|text|Button Name|New" })]
        public void ClickButtonIfExists(string ButtonName)
        {
            try
            {
                Initialize();
                DlkBaseControl ctlButton = null;
                if (mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']")).Count <= 0)
                {
                    if (mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]")).Count <= 0)
                    {
                        //throw new DlkException("Button control = '" + ButtonName + "' not found");
                        DlkLogger.LogInfo("ClickButtonIfExists() : " + ButtonName + " does not exist.");
                        DlkLogger.LogInfo("Successfully executed ClickButtonIfExists()");
                        return;
                    }
                    else
                    {
                        ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]"))[0]);
                    }
                }
                else
                {
                    ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']"))[0]);
                }
                ctlButton.MouseOver();
                ctlButton.Click();
                DlkLogger.LogInfo("Successfully executed ClickButtonIfExists()");
            }
            catch (Exception e)
            {
                throw new Exception("ClickButton() failed : " + e.Message, e);
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
                    if (DlkEnvironment.mBrowser.ToLower().Equals("ie"))
                    {
                        ctlCheckBox.mElement.FindElement(By.XPath("..")).Click();
                        ctlCheckBox.mElement.SendKeys(OpenQA.Selenium.Keys.Space);
                        // wait before state check
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        ctlCheckBox.Click(1);
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
                if (DlkEnvironment.mBrowser.ToLower() == "ie")
                {
                    ctlCloseButton.mElement.Click();
                }
                else
                {
                    ctlCloseButton.Click();
                }
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
                        // Click on the dropdown button first to display list
                        ctlDropdownButton.MouseOver();
                        ctlDropdownButton.Click();
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

        [Keyword("SwitchFormPosition", new String[] { "1|text|Target Index|1" })]
        public void SwitchFormPosition(String TargetIndex)
        {
            try
            {
                Initialize();
                int index = Convert.ToInt32(TargetIndex);
                new DlkBaseControl("Form", mElement).ScrollIntoViewUsingJavaScript();
                IList <IWebElement> displayedForms = DlkEnvironment.AutoDriver.FindElements(By.XPath(m_strVisibleFormParentsXpath)).ToList();
                displayedForms.RemoveAt(0);
                if (index <= 0 || index > displayedForms.Count)
                {
                    throw new Exception("SwitchFormPosition() failed : Index " + TargetIndex + " out of bounds of the number of displayed forms");
                }
                displayedForms = ReorderFormsBasedOnCSSStyle(displayedForms);
                IWebElement targetFormParent = displayedForms[index-1];
                IWebElement formParent = mElement.FindElement(By.XPath(".."));
                if (formParent.Location == targetFormParent.Location)
                {
                    DlkLogger.LogInfo("No drag action performed - form is at the target index already.");
                    return;
                }
                IWebElement formDragBar = formParent.FindElement(By.XPath(m_strFormDragBarXpath));
                IWebElement targetDragBar = targetFormParent.FindElement(By.XPath(m_strFormDragBarXpath));
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
                mAction.MoveToElement(formDragBar, 0, 0).ClickAndHold().MoveToElement(targetDragBar).Release().Build().Perform();
                new DlkBaseControl("Form", mElement).ScrollIntoViewUsingJavaScript();
                DlkLogger.LogInfo("Successfully executed SwitchFormPosition() : Form moved to index " + TargetIndex);
            }
            catch (Exception e)
            {
                throw new Exception("SwitchFormPosition() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyButtonColor", new string[] { "1|text|ButtonName|Form", "2|text|ExpectedColor|Blue" })]
        public void VerifyButtonColor(string ButtonName, string ExpectedColor)
        {
            try
            {
                Initialize();
                IWebElement button = mElement.FindElements(By.XPath($"//div[string()='{ButtonName}']")).FirstOrDefault();

                DlkButton btn = new DlkButton(ButtonName, button);
                btn.VerifyColor(ExpectedColor);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyButtonColor() failed : " + e.Message, e);
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

        /// <summary>
        /// Reorder forms based on style:top CSS values
        /// </summary>
        /// <param name="forms">list of forms to reorder</param>
        /// <returns>reordered list of forms</returns>
        private IList<IWebElement> ReorderFormsBasedOnCSSStyle(IList<IWebElement> forms)
        {
            IList<IWebElement> lstOriginalForms = forms;
            List<IWebElement> lstReorderedForms = new List<IWebElement>();
            List<int> topCSSValues = new List<int>();
            for (int i = 0; i < lstOriginalForms.Count; i++)
            {
                string styleText = lstOriginalForms[i].GetCssValue("top").Replace("px", "");
                topCSSValues.Add(Convert.ToInt32(styleText));
            }
            List<int> reorderedCSSValues = topCSSValues;
            reorderedCSSValues.Sort();
            for (int i = 0; i < lstOriginalForms.Count; i++)
            {
                lstReorderedForms.Add(lstOriginalForms.Where(j => j.GetCssValue("top") == topCSSValues[i].ToString() + "px").First());
            }
            return lstReorderedForms;
        }
    }
}
