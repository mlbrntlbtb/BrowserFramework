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


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Base class for Buttons
    /// </summary>
    public class DlkBaseButton : DlkBaseControl
    {
        public DlkBaseButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkBaseButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement)
        {
            mElement = ExistingWebElement;
            Initialize();
        }

        /// <summary>
        /// Find the element
        /// </summary>
        public void Initialize()
        {
            if (mElement == null)
            {
                FindElement();
            }
        }

        /// <summary>
        /// Gets the text of the button
        /// </summary>
        /// <returns></returns>
        public String GetText()
        {
            String mText = "";
            mText = GetValue();
            return mText;
        }

        /// <summary>
        /// Verifies the text of the button 
        /// </summary>
        /// <param name="ExpectedText"></param>
        public void VerifyText(String ExpectedText)
        {
            String ActText = GetText();
            DlkAssert.AssertEqual("Verify text for button: " + mControlName, ExpectedText, ActText);
        }
    }
}

