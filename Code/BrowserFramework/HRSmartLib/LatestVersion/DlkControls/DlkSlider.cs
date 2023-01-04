using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.LatestVersion.DlkControls
{
    [ControlType("Slider")]
    public class DlkSlider : DlkBaseControl
    {
        #region Declarations



        #endregion

        #region Constructors

        public DlkSlider(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            initialize();
        }

        public DlkSlider(String ControlName, IWebElement existingElement)
            : base(ControlName, existingElement)
        {
            //Do Nothing.
        }

        #endregion

        #region Properties
        #endregion

        #region Keywords

        [Keyword("Set")]
        public void Set(string Value)
        {
            try
            {
                //Scroll Into Element.
                base.ScrollIntoViewUsingJavaScript();
                int targetValue = Convert.ToInt32(Value);
                int val = int.Parse(Value);
                int currentValue = getCurrentValue();
                if (currentValue != targetValue)
                {
                    do
                    {

                        if (currentValue < targetValue)
                        {
                            moveSlider(true);
                        }
                        else
                        {
                            moveSlider(false);
                        }
                        currentValue = getCurrentValue();
                    } while (currentValue != targetValue);
                }

                DlkLogger.LogInfo("Set( ) execution passed.");
                    
            }
            catch (Exception ex)
            {
                throw new Exception("Set( ) execution failed. : " + ex.Message, ex);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                if (!TrueOrFalse.Equals(string.Empty))
                {
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    DlkLogger.LogInfo("VerifyExists() passed");
                }
                else
                {
                    DlkLogger.LogInfo("Verification skipped");
                }
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String Value)
        {
            try
            {
                initialize();
                string actualResult = "No Value";
                IList<IWebElement> elements = mElement.FindElements(By.XPath(@"./following::span"));
                if (elements.Count > 0)
                {
                    actualResult = elements[0].Text.Trim();
                }
                DlkAssert.AssertEqual("VerifyValue", Value, actualResult);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        private int getCurrentValuePercentage()
        {
            string styleAttr = mElement.GetAttribute("style");
            string[] splitedStyleAttr = styleAttr.Split(':');
            int currentValue = Convert.ToInt32(splitedStyleAttr[1].Substring(0, splitedStyleAttr[1].IndexOf("%")));

            return currentValue;
        }

        private int getCurrentValue()
        {
            IWebElement sliderInput = mElement.FindElement(By.XPath(@"./ancestor::div[3]/input[@class='form-control slider'] | ./ancestor::div/input[contains(@class,'form-control slider')]"));
            return Convert.ToInt32(sliderInput.GetAttribute("value"));
        }

        private void moveSlider(bool increase)
        {
            IWebElement elem = mElement.FindElement(By.XPath(".//*[contains(@class,'slider-handle')]"));
            if (increase)
            {
                elem.SendKeys(Keys.ArrowRight);
            }
            else
            {
                elem.SendKeys(Keys.ArrowLeft);
            }
        }

        #endregion
    }
}
