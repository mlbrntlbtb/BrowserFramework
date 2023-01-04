using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace DCOLib.DlkControls
{
    [ControlType("Form")]
    public class DlkForm : DlkBaseControl
    {
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

        //[Keyword("Close")]
        //public void Close()
        //{
        //    try
        //    {
        //        Initialize();

        //        String CloseButton = "Close";
        //        CloseButton = Costpoint.TranslateParameter(CloseButton);

        //        DlkButton ctlCloseButton = null;
        //        if (mElement.FindElements(By.CssSelector("*[title*='" + CloseButton + "']")).Count == 1)
        //        {
        //            ctlCloseButton = new DlkButton("CloseButton", mElement.FindElements(By.CssSelector("*[title*='" + CloseButton + "']"))[0]);
        //        }
        //        else
        //        {
        //            handling for 'tblcls' and 'frmcls'
        //            foreach (IWebElement ctl in mElement.FindElements(By.CssSelector("*[title*='" + CloseButton + "']")))
        //                {
        //                    ctlCloseButton = new DlkButton("CloseButton", ctl);
        //                    if (ctlCloseButton.Exists())
        //                    {
        //                        break;
        //                    }

        //                }
        //            if (ctlCloseButton == null || !ctlCloseButton.Exists())
        //            {
        //                throw new Exception("Close button not found");
        //            }
        //        }

        //        ctlCloseButton.Initialize();
        //        ctlCloseButton.ScrollIntoViewUsingJavaScript();
        //        ctlCloseButton.Click();
        //        DlkLogger.LogInfo("Successfully executed Close()");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Close() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("VerifyButtonReadOnly", new String[] { "1|text|Button Name|Delete",
        //                                             "2|text|Expected Value|TRUE" })]
        //public void VerifyButtonReadOnly(String ButtonName, String ExpectedValue)
        //{
        //    try
        //    {
        //        Initialize();
        //        DlkBaseControl ctlButton = null;
        //        String ActValue = "";
        //        String[] arrInputString = null;

        //        To verify a MainForm button w/ no dropdown. (i.e.New, Delete)
        //        if (!ButtonName.Contains("~"))
        //        {
        //            if (mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']")).Count <= 0)
        //            {
        //                throw new Exception("Button control = '" + ButtonName + "' not found.");
        //            }
        //            ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']"))[0]);
        //            ActValue = CheckReadOnlyState(ctlButton);
        //        }
        //        else
        //        {
        //            DlkBaseControl ctlDropdownButton = null;
        //            DlkBaseControl ctlDropdownList = null;
        //            DlkBaseControl ctlItem = null;

        //            arrInputString = ButtonName.Split('~');
        //            if (mElement.FindElements(By.CssSelector("*[title*='" + arrInputString[0] + "']")).Count <= 0)
        //            {
        //                throw new Exception("Button control = '" + arrInputString[0] + "' not found.");
        //            }
        //            ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + arrInputString[0] + "']"))[0]);

        //            Find whether MainForm button has a dropdown control(i.e.Copy button dropdown)
        //            if (ctlButton.mElement.FindElements(By.XPath("./following-sibling::div/span[@class='tbBtnSplitImg']")).Count <= 0)
        //            {
        //                throw new Exception("Button dropdown control = '" + arrInputString[0] + "' not found");
        //            }
        //            else
        //            {
        //                ctlDropdownButton = new DlkBaseControl(arrInputString[0],
        //                    ctlButton.mElement.FindElements(By.XPath("./following-sibling::div[contains(@id,'Mnu')]"))[0]);
        //                Click on the dropdown button first to display list
        //                ctlDropdownButton.MouseOver();
        //                ctlDropdownButton.Click();
        //                DlkLogger.LogInfo("Successfully clicked the button dropdown control.");
        //            }

        //            Retrieve all the items from the dropdown list after dropdown button was clicked
        //           ctlDropdownList = new DlkBaseControl("List",
        //               mElement.FindElement(By.XPath("//div[@class='tlbrDDMenuDiv' and contains(@style, 'display: block')]")));

        //            foreach (IWebElement ctl in ctlDropdownList.mElement.FindElements(By.XPath("//div[@class='tlbrDDActionDiv']")))
        //            {
        //                ctlItem = new DlkBaseControl("DdItemList", ctl);
        //                if (ctlItem.GetAttributeValue("textContent") == arrInputString[1])
        //                {
        //                    ActValue = CheckReadOnlyState(ctlItem); // Verify if item in list is read only (i.e. Paste Data)
        //                    break;
        //                }
        //            }
        //        }

        //        DlkAssert.AssertEqual("VerifyAttribute()", ExpectedValue.ToLower(), ActValue.ToLower());
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyButtonReadOnly() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("VerifyButtonExists", new String[] { "1|text|Button Name|New",
        //                                                "2|text|Expected Value|TRUE" })]
        //public void VerifyButtonExists(string ButtonName, string ExpectedValue)
        //{
        //    try
        //    {
        //        Initialize();
        //        DlkBaseControl ctlButton = null;
        //        String ActValue = "false";

        //        if (mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']")).Count > 0)
        //        {
        //            ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.CssSelector("*[title*='" + ButtonName + "']"))[0]);
        //            ActValue = CheckVisibility(ctlButton);
        //        }
        //        else
        //        {
        //            if (mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]")).Count > 0)
        //            {
        //                ctlButton = new DlkBaseControl(ButtonName, mElement.FindElements(By.XPath(".//div[contains(text(),'" + ButtonName + "')]"))[0]);
        //                ActValue = CheckVisibility(ctlButton);
        //            }
        //            else
        //            {
        //                DlkLogger.LogInfo("VerifyButtonExists() : " + ButtonName + " does not exist.");
        //                ActValue = "false";
        //            }
        //        }

        //        DlkAssert.AssertEqual("VerifyButtonExists()", ExpectedValue.ToLower(), ActValue.ToLower());
        //        DlkLogger.LogInfo("Successfully executed VerifyButtonExists() for " + ButtonName + " button.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyButtonExists() failed : " + e.Message, e);
        //    }
        //}

        //[Keyword("VerifyCheckBoxValue", new String[] { "1|text|CheckBox Name|Sample name", "2|text|Expected Value|TRUE" })]
        //public void VerifyCheckBoxValue(String CheckBoxName, String ExpectedValue)
        //{
        //    try
        //    {
        //        Initialize();

        //        DlkBaseControl ctlCheckBox = new DlkBaseControl("CheckBox", mElement.FindElement(By.CssSelector("input[title*='" + CheckBoxName + "']")));

        //        if (!ctlCheckBox.Exists())
        //        {
        //            throw new Exception("Checkbox = '" + CheckBoxName + "' not found");
        //        }

        //        Boolean bIsChecked = Convert.ToBoolean(ExpectedValue);
        //        Boolean bCurrentState = Convert.ToBoolean(ctlCheckBox.GetAttributeValue("checked"));

        //        DlkAssert.AssertEqual("VerifyCheckBoxValue()", bIsChecked, bCurrentState);
        //        DlkLogger.LogInfo("Successfully executed VerifyCheckBoxValue() : " + CheckBoxName);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("VerifyCheckBoxValue() failed : " + e.Message, e);
        //    }
        //}

        //private String CheckVisibility(DlkBaseControl btnControl)
        //{
        //    String strVisible = "false";

        //    if (btnControl.mElement.Displayed)

        //    {
        //        strVisible = "true";
        //    }
        //    else
        //    {
        //        strVisible = "false";
        //    }

        //    return strVisible;
        //}

        //private String GetFormHeaderLabelText(IWebElement Header)
        //{
        //    String strRet = null;
        //    DlkBaseControl ctlHeader = null;

        //    ctlHeader = new DlkBaseControl("Header", Header);
        //    If multiple hits, check w/ c one is displayed using the Exist() base routine
        //    if (ctlHeader.Exists())
        //    {
        //        If found visible Header, look for Label control immediately following the visible Header node
        //        if (ctlHeader.mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath)).Count == 1)
        //            {
        //                strRet = ctlHeader.mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath))[0].Text;
        //            }
        //        /* Check if code below is reacheable - jpv */
        //        look for label at descendant nodes
        //        else if (mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath)).Count == 1)
        //                {
        //                    strRet = mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath))[0].Text;
        //                }
        //    }
        //    else if (mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath)).Count == 1) //visibility is hidden even if form is shown
        //    {
        //        strRet = mElement.FindElements(By.XPath("./following-sibling::" + m_strLabelXpath))[0].Text;
        //    }
        //    return strRet;
        //}

        /// <summary>
        /// Check the readonly state of a button
        /// </summary>
        /// <param name = "btnControl" > button control to be checked</param>
        /// <returns>string value(True if it is readonly and False if not)</returns>
        //private String CheckReadOnlyState(DlkBaseControl btnControl)
        //{
        //    String strReadOnly = "";
        //    String strClass = "";

        //    try
        //    {
        //        new way of checking disabled buttons
        //        strClass = btnControl.GetAttributeValue("dis");
        //        if (strClass.Contains("1"))
        //        {
        //            strReadOnly = "TRUE";
        //        }
        //        else
        //        {
        //            strReadOnly = "FALSE";
        //        }
        //    }
        //    catch
        //    {
        //        strClass = btnControl.GetAttributeValue("class");
        //        if (!string.IsNullOrEmpty(strClass) && strClass.Contains("Disabled"))
        //        {
        //            strReadOnly = "TRUE";
        //        }
        //        else
        //        {
        //            strReadOnly = "FALSE";
        //        }

        //        additional check for disabled inputs
        //        strClass = btnControl.GetAttributeValue("disabled");
        //        if (!string.IsNullOrEmpty(strClass) && strClass.Contains("true"))
        //            {
        //                strReadOnly = "TRUE";
        //            }

        //        check for special case in Copy Dropdown menu.i.e.Copy~Paste Data
        //        strClass = btnControl.GetAttributeValue("mnudisabled");
        //        if (!string.IsNullOrEmpty(strClass) && strClass.Contains("Y"))
        //            {
        //                strReadOnly = "TRUE";
        //            }
        //    }

        //    return strReadOnly;
        //}

    }
}
