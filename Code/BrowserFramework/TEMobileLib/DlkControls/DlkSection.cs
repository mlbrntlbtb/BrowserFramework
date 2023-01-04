using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
{
    [ControlType("Section")]
    public class DlkSection : DlkBaseControl
    {
        #region Constructors
        public DlkSection(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSection(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSection(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        
        #region Keywords
        [Keyword("Click", new String[] { "1|text|Section Name|Sample Value" })]
        public void Click(string SectionName)
        {
            try
            {
                Initialize();
                var section = new DlkBaseControl($"Section '{SectionName}'", GetSectionControl(SectionName));
                section.ScrollIntoViewUsingJavaScript();
                section.Click();
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String strExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(strExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyIfExpanded", new String[] { "1|text|Expected Value|TRUE",
                                                    "2|text|Section Name|Sample Value" })]
        public void VerifyIfExpanded(String ExpectedValue, String SectionName)
        {
            try
            {
                Initialize();//refreshes the DOM                
                var sectionIcon = new DlkBaseControl("Section", GetSectionControl(SectionName).FindElement(By.CssSelector(".gBxPrtImg"))).GetValue();
                
                DlkAssert.AssertEqual($"VerifyIfExpanded(): Section '{SectionName}'", Convert.ToBoolean(ExpectedValue), sectionIcon == "<<");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyIfExpanded() failed : " + e.Message, e);
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
        #endregion

        #region Private methods

        private void Initialize()
        {
            if(mElement == null)
                FindElement();
        }

        private IWebElement GetSectionControl(string LabelValue)
        {
            IWebElement sectionLabel;

            if (mElement.Text == LabelValue) sectionLabel = mElement;
            else //check for section controls
            {
                sectionLabel = mElement.FindElements(By.XPath("./..//*[@class='glbl']")).FirstOrDefault(elem => elem.Text.ToLower() == LabelValue.ToLower() && elem.Displayed && elem.FindElements(By.XPath("./preceding-sibling::span//span[text()='>>' or text()='<<']")).Any(x => x.Displayed));
            }
            
            if (sectionLabel == null) 
                sectionLabel = mElement.FindElements(By.XPath($"./following-sibling::*[contains(@class, 'gBx')]")).FirstOrDefault(elem => elem.Text.ToLower() == LabelValue.ToLower());

            if (sectionLabel == null)
                sectionLabel = DlkEnvironment.AutoDriver.FindElements(By.XPath($"//*[contains(@class, 'gBx')]")).FirstOrDefault(elem => elem.Text.ToLower() == LabelValue.ToLower() && elem.Displayed);

            if (sectionLabel == null)
                sectionLabel = DlkEnvironment.AutoDriver.FindElements(By.XPath($"//*[contains(@class, 'gBx') and text()='{LabelValue}']")).FirstOrDefault(elem => elem.Displayed);

            if (sectionLabel == null) throw new Exception($"Unable to find section: '{LabelValue}'");

            //var sectionToClick = sectionLabel.FindElements(By.XPath("./preceding-sibling::span[contains(@id, 'GB') and @class='gBx']")).LastOrDefault();//selects the section divider that corresponds to the label

            //if (sectionToClick != null) sectionLabel = sectionToClick;

            return sectionLabel;
        }
        #endregion
    }
}
