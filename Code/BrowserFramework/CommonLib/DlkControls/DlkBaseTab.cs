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
    /// Base class for tabs
    /// </summary>
    public class DlkBaseTab : DlkBaseControl
    {
        public DlkBaseTab(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseTab(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        /// <summary>
        /// Selects a tab
        /// </summary>
        /// <param name="TabName"></param>
        public void Select(String TabName)
        {
            FindElement(); // find the tab control
            IWebElement mTabLink = mElement.FindElement(By.PartialLinkText(TabName));
            
            mTabLink.Click();
            DlkLogger.LogInfo("Successfully executed Select() : " + mControlName + ": " + TabName);
        }

        /// <summary>
        /// Verifies the tab name exists
        /// </summary>
        /// <param name="bExpToExist"></param>
        /// <param name="TabName"></param>
        public void VerifyTabNameExists(Boolean bExpToExist, String TabName)
        {
            FindElement(); // find the tab control
            IWebElement mTabLink = mElement.FindElement(By.PartialLinkText(TabName));
            DlkAssert.AssertEqual("Verify Tab Name Exists: " + TabName, bExpToExist, mTabLink.Displayed);
        }

    }
}

