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
    /// <summary>
    /// Base textbox class
    /// </summary>
    public class DlkBaseTextBox : DlkBaseControl
    {
        public DlkBaseTextBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseTextBox(String ControlName, String SearchType, String [] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkBaseTextBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }

        /// <summary>
        /// find the textbox
        /// </summary>
        public void Initialize()
        {
            if (mElement == null)
            {
                FindElement();
            }
        }

        /// <summary>
        /// clear and then set the text
        /// </summary>
        /// <param name="TextToEnter"></param>
        public void Set(String TextToEnter)
        {
            Initialize();
            mElement.Clear();
            mElement.SendKeys(Keys.Delete);
            mElement.Click();
            mElement.SendKeys(TextToEnter);
            DlkLogger.LogInfo("Successfully executed Set() : " + mControlName + ": " + TextToEnter);
        }

        /// <summary>
        /// set text and press enter
        /// </summary>
        /// <param name="TextToEnter"></param>
        /// <param name="KeyToPress"></param>
        public void SetAndPressEnter(String TextToEnter)
        {
            Set(TextToEnter);
            mElement.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// set text and press tab
        /// </summary>
        /// <param name="TextToEnter"></param>
        /// <param name="KeyToPress"></param>
        public void SetAndPressTab(String TextToEnter)
        {
            Set(TextToEnter);
            mElement.SendKeys(Keys.Tab);
        }

        /// <summary>
        /// verifies the text
        /// </summary>
        /// <param name="TextToVerify"></param>
        public void VerifyText(String TextToVerify)
        {
            VerifyAttribute("value", TextToVerify);
        }

        /// <summary>
        /// gets the text using the value attribute
        /// </summary>
        /// <returns></returns>
        public String GetText()
        {
            return GetAttributeValue("value");
        }
    }
}

