using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using SFTLib.DlkSystem;
using SFTLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFTLib.DlkControls
{
    [ControlType("CheckBox")]
    public class DlkCheckBox : DlkBaseControl
    {
        public DlkCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkCheckBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

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
            DlkSFTCommon.WaitForScreenToLoad();
            DlkSFTCommon.WaitForSpinner();
            Terminate();
            FindElement();
        }

        [Keyword("Set", new String[] { "1|text|Value|TRUE" })]
        public void Set(String IsChecked)
        {
            try
            {
                int retryCount = 0;
                Initialize();
                Boolean bIsChecked = Convert.ToBoolean(IsChecked);
                Boolean bCurrentValue = GetCheckedState();
                while (++retryCount <= 3 && bCurrentValue != bIsChecked)
                {
                    ScrollIntoViewUsingJavaScript();
                    Click(4.3);
                    if (!DlkAlert.DoesAlertExist())  // If dialog is present, exit retry
                    {
                        bCurrentValue = GetCheckedState();
                        if (bCurrentValue == bIsChecked)
                        {
                            break;
                        }
                        else
                        {
                            DlkLogger.LogInfo("Set() failed. Retrying...");
                        }
                    }
                    else
                    {
                        break;
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
            if (mElement.GetAttribute("class").Contains("x-table-form-item"))
            {
                if (mElement.GetAttribute("class").Contains("checked"))
                    return true;
                else
                    return false;
            }
            else if (mElement.GetAttribute("class").Contains("x-form-checkbox"))
            {
                if (mElement.FindElement(By.XPath("./ancestor::table[1]")).GetAttribute("class").Contains("checked"))
                    return true;
                else
                    return false;

            }
            else
            {
                Boolean bCurrentVal = Convert.ToBoolean(this.GetAttributeValue("checked"));
                return bCurrentVal;
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String IsChecked)
        {
            try
            {
                Initialize();
                Boolean bIsChecked = Convert.ToBoolean(IsChecked);
                Boolean bCurrentValue = GetCheckedState();
                DlkAssert.AssertEqual("VerifyValue()", bIsChecked, bCurrentValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                DlkSFTCommon.ScrollToElement(this);
                DlkEnvironment.mSwitchediFrame = false;
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String ExpectedValue)
        {
            try
            {
                String ActValue = IsReadOnly();
                DlkAssert.AssertEqual("VerifyReadOnly()", Convert.ToBoolean(ExpectedValue), Convert.ToBoolean(ActValue));
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("SetPortlet")]
        public void SetPortlet(String CheckBoxList, String IsChecked) {
            try {
                Initialize();
                Boolean isChecked = Convert.ToBoolean(IsChecked);
                List<String> checkboxToToggle = CheckBoxList.Trim().Split('~').ToList<String>();
                mElement.ClickUsingJS();
                Thread.Sleep(1000);
                foreach(var checkbox in GetCheckBoxList()) {
                    if (checkboxToToggle.Where(c => c == checkbox.Text).Count() > 0 && (checkbox.GetAttribute("class").Contains("x-menu-item-checked") != isChecked) ) {
                        checkbox.ClickUsingJS();
                    }
                }
                DlkLogger.LogInfo("SetPortlet() passed");
            } catch (Exception e) {
                throw new Exception("SetPortlet() failed : " + e.Message, e);
            }
        }

        private void Terminate()
        {
            DlkEnvironment.mSwitchediFrame = false;
        }
        private List<IWebElement> GetCheckBoxList() {
            var elements = mElement.FindElements(By.XPath("//div[contains(@class,'x-panel x-layer') and not(contains(@style,'visibility: hidden'))][last()]//div[contains(@class,'x-box-item')]")).ToList();
            if (elements.Count() > 0) return elements;
            else throw new Exception("No CheckBox found!");
        }
    }
}
