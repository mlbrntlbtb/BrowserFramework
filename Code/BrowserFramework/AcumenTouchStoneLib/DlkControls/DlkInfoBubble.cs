using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;
using System.Linq;
using System.Collections.Generic;
using CommonLib.DlkUtility;
using System.Threading;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("InfoBubble")]
    public class DlkInfoBubble : DlkBaseControl
    {
        public DlkInfoBubble(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkInfoBubble(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkInfoBubble(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkInfoBubble(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private string mstrDescriptionText = "Description:";
        private string mstrRemarksText = "Remarks:";
        private string mstrInclusionsText = "Inclusions:";
        private string mstrTypesText = "Types:";
        private string mstrFormulaText = "Formula:";

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTitle(String ExpectedValue)
        {
            try
            {
                Initialize();
                String sTitle = String.Empty;
                IWebElement mTitle = mElement.FindElements(By.XPath("//h2")).First();
                sTitle = mTitle.Text;
                DlkAssert.AssertEqual("Verify title: " + mControlName, ExpectedValue, sTitle);
                DlkLogger.LogInfo("VerifyTitle() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDescription", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyDescription(String ExpectedValue)
        {
            try
            {
                Initialize();
                String sDesc = String.Empty;
                IWebElement mDesc = mElement.FindElements(By.XPath("//span[contains(text(), '" + mstrDescriptionText + "')]/..")).First();
                sDesc = mDesc.Text.Replace(mstrDescriptionText, "").Trim();
                DlkAssert.AssertEqual("Verify description: " + mControlName, ExpectedValue, sDesc);
                DlkLogger.LogInfo("VerifyDescription() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDescription() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyRemarks", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyRemarks(String ExpectedValue)
        {
            try
            {
                Initialize();
                String sRemarks = String.Empty;
                IWebElement mRemarks = mElement.FindElements(By.XPath("//span[contains(text(), '" + mstrRemarksText + "')]/..")).First();
                sRemarks = mRemarks.Text.Replace(mstrRemarksText, "").Trim();
                DlkAssert.AssertEqual("Verify remarks: " + mControlName, ExpectedValue, sRemarks);
                DlkLogger.LogInfo("VerifyRemarks() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyRemarks() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyInclusions", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyInclusions(String ExpectedValue)
        {
            try
            {
                Initialize();
                String sInclusions = String.Empty;
                IWebElement mInclusions = mElement.FindElements(By.XPath("//span[contains(text(), '" + mstrInclusionsText + "')]/following-sibling::span//br[1]")).First();
                IList<IWebElement> lstInclusions = mInclusions.FindElements(By.XPath("./preceding-sibling::li")).ToList();
                foreach (IWebElement listElement in lstInclusions)
                {
                    sInclusions += listElement.Text + "~";
                }
                sInclusions = sInclusions.TrimEnd('~');
                DlkAssert.AssertEqual("Verify inclusions: " + mControlName, ExpectedValue, sInclusions);
                DlkLogger.LogInfo("VerifyInclusions() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyInclusions() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTypes", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyTypes(String ExpectedValue)
        {
            try
            {
                Initialize();
                String sTypes = String.Empty;
                IWebElement mTypes = mElement.FindElements(By.XPath("//span[contains(text(), '" + mstrTypesText + "')]/following-sibling::span//br[1]")).First();
                IList<IWebElement> lstTypes = mTypes.FindElements(By.XPath("./preceding-sibling::li")).ToList();
                foreach (IWebElement listElement in lstTypes)
                {
                    sTypes += listElement.Text + "~";
                }
                sTypes = sTypes.TrimEnd('~');
                DlkAssert.AssertEqual("Verify types: " + mControlName, ExpectedValue, sTypes);
                DlkLogger.LogInfo("VerifyTypes() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTypes() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyFormula", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyFormula(String ExpectedValue)
        {
            try
            {
                Initialize();
                String sFormula = String.Empty;
                IWebElement mFormula = mElement.FindElements(By.XPath("//span[contains(text(), '" + mstrFormulaText + "')]/..")).First();
                sFormula = mFormula.Text.Replace(mstrFormulaText, "").Trim();
                DlkAssert.AssertEqual("Verify formula: " + mControlName, ExpectedValue, sFormula);
                DlkLogger.LogInfo("VerifyFormula() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFormula() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyContent", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyContent(String ExpectedValue)
        {
            try
            {
                Initialize();
                String sContent = String.Empty;
                IWebElement mContent;
                if (mElement.FindElements(By.XPath("//h2")).Count > 0)
                {
                    IWebElement mTitle = mElement.FindElements(By.XPath("//h2")).First();
                    mContent = mTitle.FindElements(By.XPath("./following-sibling::*")).First();
                }
                else
                {
                    mContent = mElement.FindElements(By.XPath("//div[@class='content']//span")).First();
                }
                sContent = mContent.Text;
                DlkAssert.AssertEqual("Verify content: " + mControlName, ExpectedValue, sContent);
                DlkLogger.LogInfo("VerifyContent() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyContent() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "True" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
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
    }
}
