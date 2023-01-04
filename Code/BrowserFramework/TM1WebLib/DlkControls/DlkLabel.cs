using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TM1WebLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {

        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public void Initialize()
        {
                FindElement();
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String TextToVerify)
        {
            Initialize();
            String ActualResult = "";

            // Below style does not work on IE
            //ActualResult = mElement.GetAttribute("textContent").Trim();

            ActualResult = mElement.Text.Trim();
            DlkAssert.AssertEqual("VerifyText() : " + mControlName, TextToVerify, ActualResult);
        }

        [Keyword("Set", new String[] { "1|text|Value|SampleValue" })]
        public void Set(String TextToEnter)
        {
            try
            {
                Initialize();
                ScrollIntoViewUsingJavaScript();
                OpenQA.Selenium.Interactions.Actions mCAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver).DoubleClick(mElement);
                if (!string.IsNullOrEmpty(TextToEnter))
                {
                    mCAction.Perform();
                    OpenQA.Selenium.Interactions.Actions mSAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver).SendKeys(TextToEnter + Keys.Enter);
                    mSAction.Perform();
                    Thread.Sleep(3000);

                    if (mElement.Text.ToLower() != TextToEnter.ToLower())
                    {
                        mCAction.Perform();
                        mSAction.Perform();
                        Thread.Sleep(3000);
                    }
                }
                DlkLogger.LogInfo("Successfully executed Set()");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

    }
}
