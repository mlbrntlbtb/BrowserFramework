using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using AjeraLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace AjeraLib.DlkControls
{
    [ControlType("ColorPalette")]
    class DlkColorPalette : DlkAjeraBaseControl
    {
        #region DECLARATIONS
        
        private Boolean IsInit;

        #endregion

        #region CONSTRUCTORS

        public DlkColorPalette(string ControlName, string SearchType, string SearchValue) 
            : base(ControlName, SearchType, SearchValue){}

        public DlkColorPalette(string ControlName, string SearchType, string[] SearchValues) 
            : base(ControlName, SearchType, SearchValues){}

        public DlkColorPalette(string ControlName, IWebElement ExistingWebElement) 
            : base(ControlName, ExistingWebElement){}

        public DlkColorPalette(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue) 
            : base(ControlName, ParentControl, SearchType, SearchValue){}

        public DlkColorPalette(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector) 
            : base(ControlName, ExistingParentWebElement, CSSSelector){}


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

        [Keyword("SetColor", new String[] { "1|text|Color Code|#ffffff" })]
        public void SetColor(String ColorCode)
        {
            try
            {
                Initialize();
                mElement.Click();
                SelectColor(ColorCode);
                DlkLogger.LogInfo("Successfully executed SetColor()");
            }
            catch (Exception e)
            {
                DlkLogger.LogError(e);
                throw new Exception("SetColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColor", new String[] { "1|text|Expected|#ffffff" })]
        public void VerifyColor(String ColorCode)
        {
            try
            {
                Initialize();
                string convertedColor = "background-color: " + ConvertHexToRGB(ColorCode) + ";";
                DlkAssert.AssertEqual("VerifyColor", convertedColor.ToLower(), GetColor().ToLower());
                DlkLogger.LogInfo("Successfully executed VerifyColor()");
            }
            catch (Exception e)
            {
                DlkLogger.LogError(e);
                throw new Exception("VerifyColor() failed : " + e.Message, e);
            }
        }

        //[RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        //public void VerifyExists(String TrueOrFalse)
        //{
        //    String strExpectedValue = TrueOrFalse;

        //    try
        //    {
        //        base.VerifyExists(Convert.ToBoolean(strExpectedValue));
        //        DlkLogger.LogInfo("VerifyExists() passed");
        //    }
        //    catch (Exception e)
        //    {
        //        DlkLogger.LogError(e);
        //        throw new Exception("VerifyExists() failed : " + e.Message, e);
        //    }
        //}

        //[RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
        //                                                    "2|text|VariableName|ifExist"})]
        //public new void GetIfExists(String VariableName)
        //{
        //    Boolean bExists = base.Exists();
        //    DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));
        //}

        #endregion

        #region KEYWORDS_FOR_CONTROLS_IN_LIST

        [Keyword("SetColorByRow", new String[] { "1|text|Value|SampleValue" })]
        public void SetColorByRow(String RowNumber, String ColorCode)
        {
            try
            {
                InitializeRow(RowNumber);
                mElement.Click();
                SelectColor(ColorCode);
                DlkLogger.LogInfo("Successfully executed SetColorByRow()");
            }
            catch (Exception e)
            {
                DlkLogger.LogError(e);
                throw new Exception("SetColorByRow() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColorByRow", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyColorByRow(String RowNumber, String ColorCode)
        {
            try
            {
                InitializeRow(RowNumber);
                if (mElement.Displayed)
                {
                    string convertedColor = "background-color: " + ConvertHexToRGB(ColorCode) + ";";
                    DlkAssert.AssertEqual("VerifyColorByRow", convertedColor.ToLower(), GetColor().ToLower());
                    DlkLogger.LogInfo("Successfully executed VerifyColorByRow()");
                }
                else
                {
                    throw new Exception("VerifyColorByRow() failed. Control : " + mControlName + " [ " + RowNumber + " ]:" +
                                        "' cannot be found.");
                }

            }
            catch (Exception e)
            {
                DlkLogger.LogError(e);
                throw new Exception("VerifyColorByRow() failed : " + e.Message, e);
            }
        }


        #endregion

        #region METHODS

        public string ConvertHexToRGB(string ColorCode)
        {
            int argb = Int32.Parse(ColorCode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb(argb);
            return "rgb(" + clr.R + ", " + clr.G + ", " + clr.B + ")";
        }

        public bool VerifyIfColorExistsinPalette(string ColorCode)
        {
            try
            {
                mElement.FindElements(By.XPath("//div[@class='sp-palette-container']//span[contains(@style,'" + ConvertHexToRGB(ColorCode) + "')]")).Where(x => x.Displayed).First();
                return true;
            }
            catch
            {
                return false;

            }
        }

        public void SelectColor(string ColorCode)
        {
            //using color palette
            if (VerifyIfColorExistsinPalette(ColorCode))
            {
                IWebElement txtColorCode =
                    mElement.FindElements(
                        By.XPath("//div[@class='sp-palette-container']//span[contains(@style,'" +
                                 ConvertHexToRGB(ColorCode) + "')]")).Where(x => x.Displayed).First();

                txtColorCode.Click();
                DlkLogger.LogInfo("Successfully executed SelectColor()");
            }
            else
            {
                throw new Exception("Color is not available in the Color Palette.");
            }

            //using color picker
            //IWebElement txtColorCode = mElement.FindElements(By.XPath("//div[@class='sp-picker-container']//input")).Where(x => x.Displayed).First();
            //txtColorCode.Click();
            //txtColorCode.Clear();
            //txtColorCode.SendKeys(ColorCode);
            //txtColorCode.SendKeys(Keys.Enter
        }

        public string GetColor()
        {
            IWebElement txtColorCode = mElement.FindElement(By.ClassName("sp-preview-inner"));
            return txtColorCode.GetAttribute("style");
        }

        #endregion
    }
}
