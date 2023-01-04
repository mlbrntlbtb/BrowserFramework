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
    [ControlType("DropDown")]
    public class DlkNavDropDown : DlkBaseControl   
    {

        public DlkNavDropDown(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkNavDropDown(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkNavDropDown(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

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
        [Keyword("SelectOption", new String[] { "1|text|Value|TRUE" })]
        public void SelectOption(String mOptionText)
        {
            Initialize();

            IList<IWebElement> mElms = mElement.FindElements(By.TagName("a"));
            foreach (IWebElement mElm in mElms)
            {
                DlkBaseControl mCtrl = new DlkBaseControl("Dropdown Options", mElm);
                //Select option
                if (mCtrl.GetValue() == mOptionText)
                {
                    mElm.Click();
                    break;
                }
            }
        }

        /// <summary>
        /// verifies an option is checked or unchecked. Returns true or false
        /// </summary>
        [Keyword("DoesOptionChecked")]
        public Boolean DoesOptionChecked()
        {
            Boolean bChecked = false;

            IList<IWebElement> mElms = mElement.FindElements(By.TagName("li"));
            foreach (IWebElement mElm in mElms)
            {
                DlkBaseControl mCtrl = new DlkBaseControl("Dropdown Options", mElm);
                if (mCtrl.GetAttributeValue("data-key") == "GroupStatus")
                {
                    if (mElm.GetAttribute("innerHTML").Contains("checked"))
                        bChecked = true;
                    break;
                }
            }
            return bChecked;
        }

        /// <summary>
        /// Selects an option that is not selected
        /// </summary>
        /// <param name="mOptionText"></param>
        [Keyword("SelectOptionNonSelected", new String[] { "1|text|Value|TRUE" })]
        public void SelectOptionNonSelected(String mOptionText)
        {
            Initialize();
            Thread.Sleep(200);

            IList<IWebElement> mElms = mElement.FindElements(By.TagName("a"));
            IList<IWebElement> mCheckElms = mElement.FindElements(By.TagName("div"));

            foreach (IWebElement mElm in mElms)
            {
                DlkBaseControl mCtrl = new DlkBaseControl("Dropdown Options", mElm);

                if (mCtrl.GetValue() == mOptionText)
                {
                    foreach (IWebElement mCheckElm in mCheckElms)
                    {
                        if (mCheckElm.GetAttribute("class") == "")
                        {
                            mElm.Click();
                        }
                    }
                    break;

                }
            }
        }

        /// <summary>
        /// Verifies the list of options in the dropdown
        /// </summary>
        /// <param name= mExpOptions"></param>
        private IList<IWebElement> mElmListOptions;
        [Keyword("VerifyOptions", new String[] { "1|text|Expected Value|TRUE" })]
        
        public void VerifyOptions(String mExpOptions)
        {
            Initialize();
            Thread.Sleep(200);
            List<String> lsExpOptions = mExpOptions.Split('~').ToList();
            mElmListOptions = new List<IWebElement>();
            IList<IWebElement> mElms = mElement.FindElements(By.TagName("li"));
            foreach (IWebElement mElm in mElms)
            {
                DlkBaseControl mOption = new DlkBaseControl("All Options", mElm);
                if (!mElm.Text.Contains("\r\n") && (mElm.Text != ""))
                {
                    mElmListOptions.Add(mElm);
                }

            }

            DlkAssert.AssertEqual("Verify Options Count", lsExpOptions.Count(), mElmListOptions.Count());
            for (int i = 0; i < mElmListOptions.Count(); i++)
            {
                IWebElement mElmOption = mElmListOptions[i];
                String sOptionName = mElmOption.Text;
                DlkAssert.AssertEqual("Verify Options List", lsExpOptions[i], sOptionName);
            }
        }

        /// <summary>
        /// Verifies if the option is enabled or not
        /// </summary>
        /// <param name="bEnabled"></param>
        /// /// <param name="strMenuOption"></param>
         [Keyword("IsOptionEnabled", new String[] { "1|text|Expected Value|TRUE",
                                                         "2|text|Menu Options|Sample Text"})]
        public void IsOptionEnabled(String Enabled, String strMenuOption)
        {
            Initialize();
            Boolean bEnabled = Convert.ToBoolean(Enabled);
            IList<IWebElement> mElms = mElement.FindElements(By.TagName("a"));
            foreach (IWebElement mElm in mElms)
            {
                DlkBaseControl mCtrl = new DlkBaseControl("Menu Option", mElm);
                if (mCtrl.GetValue() == strMenuOption)
                {
                    DlkAssert.AssertEqual("Verify Options Count", bEnabled, mCtrl.GetAttributeValue("class").Contains("disabled"));
                    break;
                }
            }

        }



    }
}

