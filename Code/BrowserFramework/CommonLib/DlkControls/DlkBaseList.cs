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
    /// Base class for list controls
    /// </summary>
    public class DlkBaseList : DlkBaseControl
    {

        public DlkBaseList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        /// <summary>
        /// finds the control
        /// </summary>
        public void Initialize()
        {
            if (mElement == null)
            {
                FindElement();
            }
        }

        /// <summary>
        /// selects the option from the list
        /// </summary>
        /// <param name="mOptionText"></param>
        public void SelectOption(String mOptionText)
        {
            Initialize();
            Thread.Sleep(200);
            IList<IWebElement> mElms = mElement.FindElements(By.TagName("li"));
            foreach (IWebElement mElm in mElms)
            {
                DlkBaseControl mCtrl = new DlkBaseControl("List Options", mElm);
                if (mCtrl.GetValue() == mOptionText)
                {
                    mElm.Click();
                    break;
                }
            }
        }

        /// <summary>
        /// verifies the option exists/not exists in the list
        /// </summary>
        /// <param name="bExpResult"></param>
        /// <param name="mOptionText"></param>
        public void VerifyOptionExists(Boolean bExpResult, String mOptionText)
        {
            Boolean bExists = DoesOptionExist(mOptionText);
            DlkAssert.AssertEqual("Verify Exists: " + mOptionText, bExpResult, bExists);
        }

        /// <summary>
        /// returns option existance
        /// </summary>
        /// <param name="mOptionText"></param>
        /// <returns></returns>
        public Boolean DoesOptionExist(String mOptionText)
        {
            Boolean bExists = false;
            Initialize();
            IList<IWebElement> mElms = mElement.FindElements(By.TagName("li"));
            foreach (IWebElement mElm in mElms)
            {
                DlkBaseControl mCtrl = new DlkBaseControl("List Options", mElm);
                if (mCtrl.GetValue() == mOptionText)
                {
                    bExists = true;
                    break;
                }
            }
            return bExists;
        }

        /// <summary>
        /// Verify the options in a list
        /// </summary>
        /// <param name="mExpOptions"></param>
        public void VerifyOptions(List<String> mExpOptions)
        {
            Initialize();
            Thread.Sleep(200);
            IList<IWebElement> mElmListOptions = new List<IWebElement>();
            IList<IWebElement> mElms = mElement.FindElements(By.TagName("li"));
            foreach (IWebElement mElm in mElms)
            {  
                DlkBaseControl mOption = new DlkBaseControl("All Options", mElm);
                if (!mElm.Text.Contains("\r\n") && (mElm.Text!=""))
                {
                    mElmListOptions.Add(mElm);  
                }
                
            }
            
            DlkAssert.AssertEqual("Verify Options Count", mExpOptions.Count(), mElmListOptions.Count());
            for (int i = 0; i < mElmListOptions.Count(); i++)
            {
                IWebElement mElmOption = mElmListOptions[i];
                String sOptionName = mElmOption.Text;
                DlkAssert.AssertEqual("Verify Options List", mExpOptions[i], sOptionName);
            } 
        }

    }
}

