using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("RadioButtonGroup")]
    public class DlkRadioButtonGroup : DlkBaseControl
    {
        private List<DlkRadioButton> mRadioButtons = new List<DlkRadioButton>();
        private IList<IWebElement> mElements;

        public DlkRadioButtonGroup(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkRadioButtonGroup(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            this.FindElements();
        }

        private void FindElements()
        {
            FindElements(iFindElementDefaultSearchMax);
        }

        private void FindElements(int iSecToWait)
        {
            DlkRadioButton aRadioButton;
            DlkBaseControl aRadioLabel;
            String css;
            int iFindElementElapsedTime = -1;

            mElements = null;
            for (int i = 1; i <= iSecToWait; i++)
            {
                try
                {
                    switch (mSearchType.ToLower())
                    {
                        case "id":
                            mElements = DlkEnvironment.AutoDriver.FindElements(By.Id(mSearchValues[0]));
                            break;
                        case "name":
                            mElements = DlkEnvironment.AutoDriver.FindElements(By.Name(mSearchValues[0]));
                            break;
                        case "css":
                            mElements = DlkEnvironment.AutoDriver.FindElements(By.CssSelector(mSearchValues[0]));
                            break;
                        case "xpath":
                            mElements = DlkEnvironment.AutoDriver.FindElements(By.XPath(mSearchValues[0]));
                            break;
                        case "classname":
                            mElements = DlkEnvironment.AutoDriver.FindElements(By.ClassName(mSearchValues[0]));
                            break;
                        default:
                            break;
                    }
                    DlkLogger.LogInfo("count:" + Convert.ToString(mElements.Count));
                    if (mElements != null) // we found and defined the control
                    {
                        DlkLogger.LogInfo("Successfully identified control: " + mControlName);
                        foreach (IWebElement element in mElements)
                        {
                            //css = "span[id*='" + element.GetAttribute("id") + "'][style*='top :" +
                            //    element.GetCssValue("top") + "']";
                            css = "span[id*='" + element.GetAttribute("id") + "']";
                            IList<IWebElement> labelElements = DlkEnvironment.AutoDriver.FindElements(By.CssSelector(css));
                            if (labelElements != null && labelElements.Count > 0)
                            {
                                foreach (IWebElement labelElement in labelElements)
                                {
                                    if (labelElement.GetCssValue("top") == element.GetCssValue("top"))
                                    {
                                        aRadioLabel = new DlkBaseControl("RadioLabel", labelElement);
                                        aRadioButton = new DlkRadioButton(aRadioLabel.GetValue(), element);
                                        mRadioButtons.Add(aRadioButton);
                                        break;
                                    }
                                }
                            }


                        }
                        iFindElementElapsedTime = i;
                        break;
                    }
                }
                catch (Exception e)
                {
                    DlkLogger.LogInfo("Error: " + e.Message);
                    DlkLogger.LogInfo(e.StackTrace);
                }
                //catch
                //{
                //    if (i == DlkEnvironment.ControlSearchTimeoutSec)
                //    {
                //        throw;
                //    }
                //    else
                //    {
                //        DlkLogger.LogWarning("Couldn't find control [" + mControlName + "] . Retrying...");
                //    }
                //}
                Thread.Sleep(1000);
            }
        }

        [Keyword("Select", new String[] { "1|text|Value|Value1" })]
        public void Select(String Value)
        {
            Boolean bFound = false;

            Initialize();
            DlkLogger.LogInfo("count: " + Convert.ToString(mRadioButtons.Count));
            foreach (DlkRadioButton aRadioButton in mRadioButtons)
            {
                if (aRadioButton.mControlName.ToLower() == Value.ToLower())
                {
                    //aRadioButton.Select();
                    bFound = true;
                    break;
                }
            }
            if (bFound)
            {
                DlkLogger.LogInfo("");
            }
            else
            {
                throw new Exception("");
            }

        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|ExampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {

        }
    }
}
