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


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Base class for tab pages
    /// </summary>
    public class DlkBaseTabPage : DlkBaseControl
    {
        /// <summary>
        /// determines if the object has been initialized
        /// </summary>
        private Boolean IsInit = false;

        public DlkBaseTabPage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseTabPage(String ControlName, String SearchType, String [] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkBaseTabPage(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) {}

        /// <summary>
        /// finds the link
        /// </summary>
        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }

        }

    }
}

