using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using AcumenTouchStoneLib.DlkSystem;
using System.Linq;
using CommonLib.DlkUtility;
using System.Threading;

namespace AcumenTouchStoneLib.DlkControls
{
    [ControlType("Button")]
    public class DlkButton : DlkBaseControl
    {
        public DlkButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkButton(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkButton(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();

                string sClass = mElement.GetAttribute("class");
                if (sClass == "popupBtn" || sClass.Contains("quick-edit") || sClass.Contains("icon-edit") || sClass.Contains("gridImage")
                    || sClass.Contains("icon-filter") || sClass.Contains("design") || sClass.Contains("help-icon"))
                {
                    if (DlkEnvironment.mBrowser.ToLower() == "ie")
                    {
                        Click(5, 5);
                    }
                    else
                    {
                        Click(4.5);
                    }
                }
                else
                {
                    bool dialogExist = false;
                    var dialogList = mElement.FindElements(By.XPath("//*[@role='dialog']")).ToList();
                    if (dialogList != null && dialogList.Where(x => x.Displayed).Count() > 0)
                    {
                        dialogExist = true;
                    }
                    if (((DlkEnvironment.mBrowser.ToLower().Contains("chrome")) || (DlkEnvironment.mBrowser.ToLower() == "ie"))
                        && (mElement.TagName != "button")
                        && (!dialogExist || (mControlName == "SortingOptions"))
                        && (!mElement.GetAttribute("class").Contains("overlay"))
                        && (!mElement.GetAttribute("style").Contains("inline-block"))
                        && (!mElement.GetAttribute("class").Contains("page-mode"))
                        && (!mElement.GetAttribute("class").Contains("action-text"))
                        && (!mElement.GetAttribute("class").Contains("toolTipButton"))
                       )
                    {
                        ClickUsingJavaScript();
                    }
                    else
                    {
                        ScrollIntoViewUsingJavaScript(true);
                        mElement.Click();
                    }
                }
                DlkLogger.LogInfo("Click() passed");
                DlkAcumenTouchStoneFunctionHandler.SaveSuccessMessage();
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyToolTip", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyToolTip(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActToolTip = String.Empty;
                String sClass = String.Empty;
                sClass = mElement.GetAttribute("class");
                DlkBaseControl ctlButton = new DlkBaseControl("Button", mElement);
                IWebElement mSub = ctlButton.GetParent();
                if ((sClass.Contains("statusIndicator")) || (sClass.Contains("ui-dialog-titlebar")))
                {
                    ActToolTip = mElement.GetAttribute("title");
                }
                else if (sClass.Contains("icon-listview") || sClass.Contains("icon-formview"))
                {
                    ActToolTip = mSub.GetAttribute("title");
                }
                else if (sClass.Contains("toolTipButton"))
                {
                    MouseOver();
                    if (DlkEnvironment.AutoDriver.FindElements(By.XPath("//div[contains(@class,'tpd-content')]")).Count > 0)
                    {
                        ActToolTip = DlkString.RemoveCarriageReturn(new DlkButton("Tooltip", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[contains(@class,'tpd-content')]"))).GetValue());
                    }
                    else
                    {
                        ActToolTip = mElement.GetAttribute("data-original-title");
                    }
                }
                else
                {
                    DlkBaseControl ctlSub = new DlkBaseControl("Parent", mSub);
                    IWebElement mMain = ctlSub.GetParent();
                    ActToolTip = mMain.GetAttribute("title");
                    if (ActToolTip == "")
                    {
                        ActToolTip = mElement.GetAttribute("title");
                    }
                }
                DlkAssert.AssertEqual("Verify tooltip for button: " + mControlName, ExpectedValue, ActToolTip);
                DlkLogger.LogInfo("VerifyToolTip() passed");
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyToolTip() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyButtonState", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyButtonState(String IsEnabled)
        {
            try
            {
                Initialize();
                bool buttonState = mElement.GetAttribute("class").Contains("disabled") ? false : true;
                DlkAssert.AssertEqual("Verify tooltip for button: " + mControlName, IsEnabled.ToLower(), buttonState.ToString().ToLower());
                DlkLogger.LogInfo("VerifyButtonState() passed");
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyButtonState() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "True" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
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

        public void ClickBanner()
        {
            /*Removed this functionality after adjusting timeout recurrence to 30mins*/

            //try
            //{
            //    DlkLogger.LogInfo("Performing Click on Banner to avoid timeout.");
            //    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='banner']")));
            //    bannerCtrl.Click();
            //}
            //catch
            //{
            //    //Do nothing -- there might be instances that setting a text or value would display a dialog message
            //    //Placing a log instead for tracking
            //    DlkLogger.LogInfo("Problem performing Click on Banner. Trying dialog click...");
            //    try
            //    {
            //        DlkBaseControl dialogCtrl = new DlkBaseControl("Dialog", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@role='dialog'][not(contains(@style,'display: none'))][not(contains(@style,'display:none'))][not(contains(@class,'hidden'))]")));
            //        dialogCtrl.Click();
            //    }
            //    catch
            //    {
            //        DlkLogger.LogInfo("Problem performing Click on Dialog. Proceeding...");
            //    }
            //}
        }
    }
}
