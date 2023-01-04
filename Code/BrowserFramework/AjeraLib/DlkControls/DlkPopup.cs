using AjeraLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjeraLib.DlkControls
{
    [ControlType("PopUp")]
    public class DlkPopup:DlkAjeraBaseControl
    {
        #region DECLARATIONS
        private Boolean IsInit = false;
        #endregion

        #region CONSTRUCTOR
        public DlkPopup(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkPopup(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkPopup(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkPopup(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            if (!IsInit)
            {
                FindElement();
                IsInit = true;
            }
            else
            {
                if (IsElementStale())
                {
                    FindElement();
                }
            }
        }

        public void InitializeRow(string RowNumber)
        {
            InitializeSelectedElement(RowNumber);
        }

        #endregion

        #region KEYWORDS

        [Keyword("Close")]
        public void Close()
        {
            try
            {
                Initialize();
                var closeButton = mElement.FindElement(By.XPath(".//*[contains(@class,'buttons')]//img[contains(@src,'close2.svg')]/ancestor::span[1]"));

                if (closeButton.Displayed)
                {
                    closeButton.Click();
                }
                else
                {
                    throw new Exception("Close() failed : Close Button not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Close() failed : " + e.Message, e);
            }

        }

        [Keyword("Pin")]
        public void Pin()
        {
            try
            {
                Initialize();
                var pinButton = mElement.FindElement(By.XPath(".//*[contains(@class,'buttons')]//img[contains(@src,'pushpin.svg')]/ancestor::span[1]"));

                if (pinButton.Displayed)
                {
                    pinButton.Click();
                }
                else
                {
                    throw new Exception("Pin() failed : Pin Button not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Pin() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyText")]
        public void VerifyText(String expectedValue)
        {
            try
            {
                Initialize();
                string popUpString = mElement.Text;
                DlkAssert.AssertEqual("VerifyText() : "+ mControlName, expectedValue.ToLower().Trim(), popUpString.ToLower().Trim());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists")]
        public void VerifyExists(String IsTrueOrFalse)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(IsTrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }
        #endregion

    }

}
