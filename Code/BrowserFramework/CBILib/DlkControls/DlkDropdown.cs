using CBILib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("Dropdown")]
    public class DlkDropdown : DlkBaseControl
    {
        private DropDownType mDropdownType = DropDownType.NoSupported;
        private IList<IWebElement> mOptions;

        #region Constructors
        public DlkDropdown(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkDropdown(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkDropdown(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        private void Initialize()
        {
            DlkCERCommon.WaitForPromptSpinner();
            FindElement();
        }

        #region Keywords
        /// <summary>
        ///  Verifies if Dropdown exists. Requires strExpectedValue - can either be True or False
        /// </summary>
        /// <param name="TrueOrFalse"></param>
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
        /// <summary>
        /// Selects item in radio button
        /// </summary>
        /// <param name="Value"></param>
        [Keyword("Select", new String[] { "1|text|Text To Verify|Sample Dropdown Text" })]
        public void Select(String Value)
        {
            try
            {
                Initialize();
                new DlkBaseControl("Dropdown", mElement).ClickByObjectCoordinates();
                GetOptions();
                bool found = false;

                foreach (var opt in mOptions)
                {
                    if (opt.Text == Value)
                    {
                        new DlkBaseControl("Option", opt).ClickByObjectCoordinates();
                        DlkLogger.LogInfo("Select() passed");
                        
                        if(mDropdownType == DropDownType.Reporting)
                            mElement.SendKeys(Keys.Escape);

                        System.Threading.Thread.Sleep(2000);
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new Exception($"Dropdown item '{Value}' not found.");
                else
                    DlkLogger.LogInfo("Select() successfully executed");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }
        #endregion

        private void GetOptions()
        {
            mOptions = new List<IWebElement>();

            /*reporting*/
            mOptions = mElement.FindElements(By.XPath(".//option")).ToList();

            if (mOptions.Count == 0)
            {
                /*dashboard dropdown filter*/
                mOptions = DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'commonFilterControlContainer')]//div[contains(@data-tid,'list-dropdown')]")).ToList();

                if (mOptions.Count > 0)
                {
                    mDropdownType = DropDownType.DashboardFilter;
                }
                else
                {
                    mOptions = DlkEnvironment.AutoDriver.FindElements(By.XPath("//ul[@class='commonMenuItems']//li"));
                    mDropdownType = mOptions.Count > 0 ? DropDownType.AppMenu : DropDownType.NoSupported;
                }
            }
            else
            {
                mDropdownType = DropDownType.Reporting;
            }

            if (mDropdownType == DropDownType.NoSupported)
                throw new Exception("No dropdown options found");
        }
    }

    enum DropDownType
    { 
        Reporting,
        DashboardFilter,
        AppMenu,
        NoSupported
    }
}
