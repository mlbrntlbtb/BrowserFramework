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
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    public class DlkBaseLabel : DlkBaseControl
    {
        /// <summary>
        /// determines if class has been initialized
        /// </summary>
        private Boolean IsInit = false;

        public DlkBaseLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkBaseLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }

        /// <summary>
        /// finds the element
        /// </summary>
        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }

        }

        /// <summary>
        /// Verifies the text of the label
        /// </summary>
        /// <param name="TextToVerify"></param>
        public void VerifyText(String TextToVerify)
        {
            Initialize();
            String ActualResult = "";
            ActualResult = mElement.GetAttribute("textContent").Trim();
            DlkAssert.AssertEqual("VerifyText() : " + mControlName, TextToVerify, ActualResult);
        }

        /// <summary>
        /// verifies part of the text of the label
        /// </summary>
        /// <param name="ExpectedResult"></param>
        public void VerifyPartialText(String ExpectedResult)
        {
            Initialize();
            String ActualResult = "";
            ActualResult = mElement.GetAttribute("textContent").Trim();
            Boolean PartOfText = ActualResult.Contains(ExpectedResult);
            if (PartOfText)
            {
                ActualResult = ExpectedResult;
            }

            DlkAssert.AssertEqual("VerifyPartialText(): " + mControlName, ExpectedResult, ActualResult);
        }

    }
}

