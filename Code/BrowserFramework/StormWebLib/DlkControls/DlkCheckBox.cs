using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;
using System.Linq;
using System.Collections.Generic;

namespace StormWebLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        private const string GridCheckboxClass = "gridCheckbox";
        private const string CellCheckBoxClass = "checkbox";
        private const string CheckBoxIcon = "checkbox-icon";
        private const string TreeCheckBox = "tree-checkbox";
        private const string CoreField = "core-field";
        private const string CheckBoxIconXPATH = ".//*[contains(@class,'checkbox-icon')]";

        private string mClass = "";

        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        //public DlkCheckBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
        //    : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public new bool VerifyControlType()
        {
            FindElement();
                        
            if (GetAttributeValue("type") == "checkbox")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();

            string sClass = GetAttributeValue("class");
            if (sClass.Contains(GridCheckboxClass))
            {
                mClass = GridCheckboxClass;
            }
            else if (sClass.Contains(CheckBoxIcon)) // Some core-field checkbox uses different class from CheckboxIcon
            {
                mClass = CheckBoxIcon;
            }
            else if (sClass.Contains(TreeCheckBox))
            {
                mClass = TreeCheckBox;
            }
            else if (sClass.Contains(CellCheckBoxClass) && (mElement.TagName == "span" || mElement.TagName == "div")) // TagName is used for checkboxes in grids/tables
            {
                mClass = CellCheckBoxClass;
            }
            else if (sClass.Contains(CoreField))
            {
                mClass = CoreField;                              
            }
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                base.Click();
                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("Set", new String[] { "1|text|Value|TRUE" })]
        public void Set(String TrueOrFalse)
        {
            try
            {
                Initialize();
                Boolean bIsChecked = Convert.ToBoolean(TrueOrFalse);
                if (bIsChecked != GetCheckedState())
                {
                    DlkStormWebFunctionHandler.WaitScreenGetsReady();
                    Click(4.3);
                    if (bIsChecked != GetCheckedState()) //Perform another check if the checkbox is checked, if not, try clickusingjavascript
                    {
                        ClickUsingJavaScript();
                    }
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        public Boolean GetCheckedState()
        {
            Initialize();
            Boolean bCurrentVal;

            switch (mClass)
            {
                case GridCheckboxClass:
                    string sClass = GetAttributeValue("class");
                    bCurrentVal = (sClass.Contains("checked")) ? true : false;
                    break;
                case CellCheckBoxClass:
                    sClass = GetAttributeValue("class");
                    bCurrentVal = (sClass.Contains("checked")) ? true : false;
                    break;
                case CheckBoxIcon:
                    sClass = GetAttributeValue("class");
                    bCurrentVal = (sClass.Contains("selected")) ? true : false;
                    break;
                case TreeCheckBox:
                    sClass = this.mElement.FindElement(By.XPath("..")).GetAttribute("class");
                    bCurrentVal = (sClass.Contains("checked")) ? true : false;
                    break;
                case CoreField:
                    if (this.mElement.FindElements(By.XPath(CheckBoxIconXPATH)).Count > 0) //if core-field contains a checkbox-icon, assign mElement to the checkbox child
                    {
                        mElement = this.mElement.FindElement(By.XPath(CheckBoxIconXPATH));
                        sClass = mElement.GetAttribute("class");
                        bCurrentVal = (sClass.Contains("selected")) ? true : false;
                    }
                    else //else, use default
                    {
                        bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
                    }
                    break;
                default:
                    bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
                    break;
            }
            
            return bCurrentVal;
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String TrueOrFalse)
        {
            try
            {
                Boolean bIsChecked = Convert.ToBoolean(TrueOrFalse);
                Boolean bCurrentValue = GetCheckedState();
                DlkAssert.AssertEqual("VerifyValue()", bIsChecked, bCurrentValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyErrorMessage", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyErrorMessage(String ErrorMessage)
        {
            try
            {
                Initialize();
                IWebElement errorElm = null;
                //If class doesn't contain core-component, try searching for parent that contains core-component... then search for core-error
                errorElm = !mElement.GetAttribute("class").Contains("core-component") ?
                    errorElm = mElement.FindElement(By.XPath("./ancestor::div[contains(@class,'core-component')][1]//*[@class='core-error']")) :
                    errorElm = mElement.FindElement(By.XPath(".//*[@class='core-error']"));

                if (errorElm == null || !errorElm.Displayed)
                {
                    throw new Exception("Error message not found.");
                }
                DlkAssert.AssertEqual("VerifyErrorMessage() : " + mControlName, ErrorMessage, errorElm.Text.Trim());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyErrorMessage() failed : " + e.Message, e);
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

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String TrueOrFalse)
        {
            try
            {
                String ActValue = IsReadOnly();
                if (!Convert.ToBoolean(ActValue))
                {

                    if (!mElement.GetAttribute("class").Contains("core-component"))
                    {
                        if (mElement.FindElements(By.XPath("./ancestor::*[contains(@class,'core-component')][contains(@class,'checkbox')]")).Count > 0)
                        {
                            DlkBaseControl cbCore = new DlkBaseControl("Core Checkbox", new DlkBaseControl("Checkbox", mElement), "XPATH", "./ancestor::*[contains(@class,'core-component')][contains(@class,'checkbox')]");
                            cbCore.FindElement();
                            ActValue = cbCore.IsReadOnly();

                            if (!Convert.ToBoolean(ActValue))
                            {
                                ActValue = cbCore.GetAttributeValue("class").Contains("locked").ToString();
                            }
                        }
                        // If checkbox does not lie under "core-component", use "grid-selection-check-all" to verify "Check/Uncheck All" type of checkbox
                        else if (mElement.FindElements(By.XPath("./ancestor::*[contains(@class,'grid-selection-check-all')]//*[contains(@class,'checkbox')]")).Count > 0)
                        {
                            ActValue = mElement.GetAttribute("class").Contains("locked").ToString();
                        }
                    }
                   
                }
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(TrueOrFalse), Convert.ToBoolean(ActValue));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToolTip", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyToolTip(String ExpectedValue)
        {
            Initialize();
            String ActToolTip = mElement.GetAttribute("title");
            //[AJM 1/15/2018] For special case of Select All controls found in Search dialog
            //wherein a different tooltip from the child node appears when checkbox is disabled.
            if (mElement.FindElements(By.XPath(".//span[contains(@class,'locked')]")).Count > 0)
            {
                DlkLogger.LogInfo("Checking child nodes for possible tooltip...");
                ActToolTip = mElement.FindElement(By.XPath(".//span[contains(@class,'locked')]")).GetAttribute("title");
            }
            DlkAssert.AssertEqual("Verify tooltip for button: " + mControlName, ExpectedValue, ActToolTip);
            DlkLogger.LogInfo("VerifyToolTip() passed");
        }     
        
    }
}
