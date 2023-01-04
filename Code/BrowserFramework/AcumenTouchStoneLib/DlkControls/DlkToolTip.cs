using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using AcumenTouchStoneLib.DlkSystem;

namespace AcumenTouchStoneLib.DlkControls
{
    /// <summary>
    /// Navigator class for Buttons
    /// </summary>
    [ControlType("ToolTip")]
    public class DlkToolTip : DlkBaseControl
    {
        public DlkToolTip(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkToolTip(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkToolTip(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }

        private void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        private void CloseToolTip()
        {
            IWebElement closeButton = mElement.FindElement(By.XPath("./descendant::img[contains(@class, 'tip-close-button')]"));
            closeButton.Click();
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

        [Keyword("VerifyText", new String[] { "1|text|Expected Value|Sample tool tip text" })]
        public void VerifyText(String ExpectedValue)
        {
            //Mew line was represented by "\n" when the data was entered from Test Runner.
            //When the data was derived from the website, new line was represented by "\r\n".
            //Replacing "\r\n" with "\n" on both the expected and actual value will eliminate errors caused by
            //inconsistent representations of the new line coming from Test Runner (manual data entry
            //and data driven) and from the website especially on data with multiple lines.
            string toolTipValue = GetValue().Replace("\r\n", "\n");
            string expectedValue = ExpectedValue.Replace("\r\n", "\n");
            DlkAssert.AssertEqual("VerifyText", expectedValue.Trim(), toolTipValue.Trim());
        }

        [Keyword("VerifyTextAndClose", new String[] { "1|text|Expected Value|Sample tool tip text" })]
        public void VerifyTextAndClose(String ExpectedValue)
        {
            string toolTipValue = GetValue().Replace("\r\n", "\n");
            string expectedValue = ExpectedValue.Replace("\r\n", "\n");
            DlkAssert.AssertEqual("VerifyText", expectedValue.Trim(), toolTipValue.Trim());
            CloseToolTip();
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|Sample tool tip text" })]
        public void VerifyTextContains(String PartialValue)
        {
            try
            {
                Initialize();
                string actualValue = GetValue();
                DlkLogger.LogInfo("Actual value [" + actualValue + "]");
                DlkLogger.LogInfo("Expected partial value [" + PartialValue + "]");
                DlkAssert.AssertEqual("VerifyTextContains", true, DlkString.NormalizeNonBreakingSpace(
                    DlkString.ReplaceCarriageReturn(actualValue, "")).Contains(PartialValue));
                DlkLogger.LogInfo("VerifyTextContains() passed");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextWithIcon", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextWithIcon(String ExpectedValue, String Icons)
        {
            FindElement();
            String ActualResult = "";
            const int tagLength = 7;

            //Verify Text
            if (mElement.GetAttribute("class").Equals("right edit"))
            {
                ActualResult = new DlkBaseControl("Text", mElement.FindElement(By.XPath("./input"))).GetValue();
            }
            else
            {
                ExpectedValue = ExpectedValue.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                ExpectedValue = ExpectedValue.Replace("<p>", "").Replace("</p>", "").Replace("<li>", "").Replace("</li>", "").Replace("<b>", "").Replace("</b>", "").Replace("<ul>", "").Replace("</ul>", "").Replace("<ol>", "").Replace("</ol>", "");

                string content = mElement.GetAttribute("innerHTML").Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Trim();
                List<string> labelContent = new List<string>();
                List<string> labelButtons = new List<string>();
                int spanIndex = content.IndexOf("<span");

                while (spanIndex != -1)
                {
                    labelContent.Add(content.Substring(0, spanIndex));
                    content = content.Remove(0, spanIndex);

                    int spanCloseTag = content.IndexOf("</span>") + tagLength;
                    labelContent.Add(content.Substring(0, spanCloseTag));
                    content = content.Remove(0, spanCloseTag);
                    spanIndex = content.IndexOf("<span");
                }

                if (!String.IsNullOrEmpty(content))
                    labelContent.Add(content);

                foreach (string lContent in labelContent)
                {
                    if (!lContent.Contains("<span"))
                    {
                        ActualResult += lContent;
                    }
                }
            }

            //Verify Icons
            string[] icons = Icons.Split('~');
            bool mCorrectIcons = true;

            IList<IWebElement> buttons = mElement.FindElements(By.XPath("./descendant::span"));

            if (icons.Length == buttons.Count)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (!(buttons[i].GetAttribute("class")).ToLower().Contains(icons[i].ToLower()))
                    {
                        mCorrectIcons = false;
                    }
                }
            }
            else
            {
                mCorrectIcons = false;
            }
            ActualResult = ActualResult.Replace("<p>", "").Replace("</p>", "").Replace("<li>", "").Replace("</li>", "").Replace("<b>", "").Replace("</b>", "").Replace("<ul>", "").Replace("</ul>", "").Replace("<ol>", "").Replace("</ol>", "");
            DlkAssert.AssertEqual("VerifyTextWithIcon() : " + mControlName, ExpectedValue, ActualResult);
            DlkAssert.AssertEqual("VerifyTextWithIcon() : " + mControlName, true, mCorrectIcons);
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

