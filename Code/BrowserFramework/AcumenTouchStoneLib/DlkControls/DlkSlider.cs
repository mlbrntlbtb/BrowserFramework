using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium.Interactions;
using AcumenTouchStoneLib.DlkSystem;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("Slider")]
    public class DlkSlider : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkSlider(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkSlider(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkSlider(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkSlider(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PRIVATE MEMBERS
        IList<Tick> mTicks;
        Dictionary<HandleIdentity, Handle> mHandles;
        private const int mSliderFloorValue = -10;
        private const int mSliderStyleValue = 5;
        private const int mSliderUnitValue = 1;

        #endregion

        #region PRIVATE CLASSES
        private enum HandleIdentity
        {
            left,
            right,
            single
        }

        private class Handle
        {
            public IWebElement hHandle { get; set; }
            public IWebElement hTooltip { get; set; }
            public double hValue { get; set; }

            public Handle(IWebElement handle)
            {
                hHandle = handle;
            }
        }

        private class Tick
        {
            public string tValue { get; set; }
            public IWebElement tTick { get; set; }
            public IWebElement tMark { get; set; }

            public Tick(IWebElement tick)
            {
                tTick = tick;
                tValue = tick.FindElement(By.ClassName("ui-slider-tick-value")).GetAttribute("textContent");
                tMark = tick.FindElement(By.ClassName("ui-slider-tick-mark"));
            }
        }
        #endregion

        #region PRIVATE METHODS

        private void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            //CreateHandles();
        }

        private void CreateHandles()
        {
            mHandles = new Dictionary<HandleIdentity, Handle>();
            IList<IWebElement> handles = mElement.FindElements(By.XPath(".//*[contains(@class,'ui-slider-handle')]"));

            switch (handles.Count)
            {
                case 0: throw new Exception("No handles found in this slider.");
                case 1:
                    mHandles.Add(HandleIdentity.single, new Handle(handles[0]));
                    break;
                case 2:
                    mHandles.Add(HandleIdentity.left, new Handle(handles[0]));
                    mHandles.Add(HandleIdentity.right, new Handle(handles[1]));
                    break;
                default:
                    throw new Exception("Number of handles not supported : [" + handles.Count + " handles]");
            }
            DlkLogger.LogInfo("Handles of the slider defined [" + handles.Count + "]");
        }

        private void CreateTicks()
        {
            mTicks = new List<Tick>();

            //ui-slider-tick = StormWeb1.1
            //ui-slider-tick-on = StormWeb2.0
            List<IWebElement> elmTicks = mElement.FindElements(By.XPath(".//span[@class='ui-slider-tick']")).Count > 0 ? mElement.FindElements(By.XPath(".//span[@class='ui-slider-tick']")).ToList()
                : mElement.FindElements(By.XPath(".//span[contains(@class,'ui-slider-tick-on')]")).ToList();

            foreach (IWebElement tick in elmTicks)
            {
                mTicks.Add(new Tick(tick));
            }
            DlkLogger.LogInfo("Ticks found [" + mTicks.Count + "]");
        }

        private double GetDoubleEquivalent(String HandleValue)
        {
            return double.Parse(HandleValue.TrimEnd(new char[] { '%' }));
        }

        private HandleIdentity GetHandleIdentity(String handleName)
        {
            return (HandleIdentity)Enum.Parse(typeof(HandleIdentity), handleName, true);
        }

        #endregion

        #region KEYWORDS
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

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement handle = mElement.FindElements(By.XPath(".//*[contains(@class,'ui-slider-handle')]")).First();
                string style = handle.GetAttribute("style");
                int leftSize = Convert.ToInt32(style.Replace("%;", "").Replace("left: ", ""));
                int baseNumber = 0;
                int sliderValue = mSliderFloorValue;
                while (baseNumber != leftSize)
                {
                    baseNumber += mSliderStyleValue;
                    sliderValue += mSliderUnitValue;
                }
                DlkAssert.AssertEqual("Value comparison", Convert.ToInt32(ExpectedValue), sliderValue);

                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("SetValue", new String[] { "1|text|Handle|Value" })]
        public void SetValue(String HandleName, String Value)
        {
           Initialize();
            IWebElement handle = mElement.FindElements(By.XPath(".//*[contains(@class,'ui-slider-handle')]")).First();
            Actions builder = new Actions(DlkEnvironment.AutoDriver);
            builder.DoubleClick(handle).Build().Perform();
            builder = new Actions(DlkEnvironment.AutoDriver);
            if (HandleName.ToLower() == "left")
            {
                builder = builder.SendKeys(Keys.ArrowLeft);
            }
            else if (HandleName.ToLower() == "right")
            {
                builder = builder.SendKeys(Keys.ArrowRight);
            }
            else
            {
                throw new Exception("Direction can only be left or right.");
            }
            int numTicks = Convert.ToInt32(Value);
            for (int i = 1; i <= numTicks; i++)
            {
                builder.Build().Perform();
                System.Threading.Thread.Sleep(100);
            }
            DlkLogger.LogInfo("SetValue() : Slider moved successfully");
        }

        [Keyword("GetVerifyExists", new String[] { "SampleVar|1" })]
        public void GetVerifyExists(String VariableName, String SecondsToWait)
        {
            try
            {
                int wait = 0;
                if (!int.TryParse(SecondsToWait, out wait) || wait == 0)
                    throw new Exception("[" + SecondsToWait + "] is not a valid input for parameter SecondsToWait.");

                bool isExist = Exists(wait);
                string ActualValue = isExist.ToString();
                DlkVariable.SetVariable(VariableName, ActualValue);
                DlkLogger.LogInfo("[" + ActualValue + "] value set to Variable: [" + VariableName + "]");
                DlkLogger.LogInfo("GetVerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("GetVerifyExists() failed : " + e.Message, e);
            }
        }

        #endregion
    }
}