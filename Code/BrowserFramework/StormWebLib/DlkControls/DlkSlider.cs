using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium.Interactions;
using StormWebLib.System;

namespace StormWebLib.DlkControls
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
        Dictionary<HandleIdentity,Handle> mHandles;
        HandleIdentity selectedHandleIdentity;
        //HandleIdentity unselectedHandleIdentity;
        
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
                string tooltipId = handle.GetAttribute("aria-describedby");
                hTooltip = handle.FindElement(By.XPath("./following-sibling::*[@id='" + tooltipId + "']"));
                hValue = double.Parse(hTooltip.GetAttribute("textContent").TrimEnd(new char[] { '%' })); ;
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
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            CreateHandles();
            CreateTicks();
        }

        private void CreateHandles()
        {
            mHandles = new Dictionary<HandleIdentity, Handle>();
            IList<IWebElement> handles = mElement.FindElements(By.XPath(".//*[contains(@class,'ui-slider-handle')]"));
            
            switch(handles.Count)
            {
                case 0: throw new Exception("No handles found in this slider.");
                case 1: mHandles.Add(HandleIdentity.single,new Handle(handles[0]));
                    break;
                case 2: mHandles.Add(HandleIdentity.left,new Handle(handles[0]));
                    mHandles.Add(HandleIdentity.right,new Handle(handles[1]));
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
            return (HandleIdentity) Enum.Parse(typeof(HandleIdentity), handleName, true);
        }

        private void InitializeHandleVariables(String handleName)
        {
            switch(handleName.ToLower())
            {
                case "single" : selectedHandleIdentity = HandleIdentity.single;
                    break;
                case "left" :
                    selectedHandleIdentity = HandleIdentity.left;
                    //unselectedHandleIdentity = HandleIdentity.right;
                    break;
                case "right":
                    selectedHandleIdentity = HandleIdentity.right;
                    //unselectedHandleIdentity = HandleIdentity.left;
                    break;
                default:
                    throw new Exception("Unsupported handle name [" + handleName + "]");
            }
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
        public void VerifyValue(String HandleName, String ExpectedValue)
        {
            try
            {
                // fail fast if we know that it will fail in InitializeHandleVariables
                if (String.IsNullOrWhiteSpace(HandleName)) throw new Exception("HandleName must not be empty.");
                if (!(HandleName.ToLower().Equals("single") || HandleName.ToLower().Equals("left") || HandleName.ToLower().Equals("right"))) throw new Exception("HandleName must be 'single', 'left', or 'right'");
                // initialize variables
                Initialize();
                InitializeHandleVariables(HandleName);
                // determine which handle to get value then assert
                DlkAssert.AssertEqual("Value comparison", ExpectedValue.ToLower().Trim(new char[] {'%',' '}), mHandles[selectedHandleIdentity].hValue.ToString().ToLower().Trim());

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
            InitializeHandleVariables(HandleName);


            IWebElement selectedHandle = mHandles[selectedHandleIdentity].hHandle;
            selectedHandle.Click();
            DlkLogger.LogInfo("SetValue() : [" + HandleName + "] " + " Clicked handle to prepare set action.");

            //Value validation - check if the value to be set is beyond the allowable value to be selected considering the current value of the other handle
            //Only applicable if handle is left or right
            switch (selectedHandleIdentity)
            {
                case HandleIdentity.left:
                    if (GetDoubleEquivalent(Value) > mHandles[HandleIdentity.right].hValue)
                        throw new Exception("Invalid Value [" + HandleName + "]" + "cannot be set to value [" + Value + "]");
                    break;
                case HandleIdentity.right:
                    if (GetDoubleEquivalent(Value) < mHandles[HandleIdentity.left].hValue)
                        throw new Exception("Invalid Value [" + HandleName + "]" + "cannot be set to value [" + Value + "]");
                    break;
            }

            //Get the corresponding web element of the tick to be selected
            var tickVal = mTicks.Where(x => x.tValue == Value);
            IWebElement destinationTick = tickVal.First().tMark; 
            Actions builder = new Actions(DlkEnvironment.AutoDriver);
            builder.DragAndDrop(selectedHandle, destinationTick).Build().Perform();
            DlkLogger.LogInfo("SetValue() : [" + HandleName + "] " + " Successfully set value [" + Value + "]");
        }

        [Keyword("VerifyTicks", new String[] { "1|text|Expected Tick Values|ExpectedValues" })]
        public void VerifyTicks(String ExpectedTicks)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(ExpectedTicks)) throw new Exception("ExpectedTicks must not be empty");
                Initialize();
                String ActualTicks = "";
                foreach (var tick in mTicks)
                {
                    if (!String.IsNullOrWhiteSpace(ActualTicks)) ActualTicks += "~";
                    ActualTicks += tick.tValue;
                }
                DlkAssert.AssertEqual("Compare tick values", ExpectedTicks, ActualTicks);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTicks() failed: " + e.Message);
            }
        }

        #endregion
    }
}