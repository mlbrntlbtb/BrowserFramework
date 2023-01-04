using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSmartLib.PreviousVersion.DlkControls
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
                int incrementalValue = 1;
                int targetValue = Convert.ToInt32(Value);
                int currentValue = getCurrentValue();
                int tempCurrentValue = currentValue;
                bool stopIncrement = false;
                bool toIncrease = currentValue < targetValue;
                Actions actions = new Actions(DlkEnvironment.AutoDriver);
                moveSlider(actions, incrementalValue, 0);

                while (currentValue != targetValue)
                {
                    moveSlider(actions, incrementalValue, 0);
                    currentValue = getCurrentValue();

                    if (tempCurrentValue != currentValue)
                    {
                        //We already found the amount to change slider value so lets stop incrementing/decrementing.
                        stopIncrement = true;
                        DlkLogger.LogInfo("Moving from " + tempCurrentValue + " to " + currentValue);
                    }

                    if (!stopIncrement)
                    {
                        if (currentValue < targetValue &&
                            toIncrease)
                        {
                            incrementalValue++;
                        }
                        else if (!toIncrease)
                        {
                            incrementalValue--;
                        }
                    }
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
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
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

        private void moveSlider(Actions actions, int x, int y)
        {
            IAction action = actions.ClickAndHold(mElement).MoveByOffset(x, y).Release().Build();
            action.Perform();
        }

        #endregion
    }
}
