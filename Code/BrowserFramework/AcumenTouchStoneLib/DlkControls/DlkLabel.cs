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
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        private String mCloseButtonXPath = ".//div[@id='close']";

        public void Initialize()
        {
            DlkAcumenTouchStoneFunctionHandler.WaitScreenGetsReady();
            
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
                ClickBanner();
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            try
            {
                Initialize();
                String ActualResult = "";
                if (mElement.GetAttribute("class").Equals("right edit"))
                {
                    ActualResult = new DlkBaseControl("Text", mElement.FindElement(By.XPath("./input"))).GetValue();
                }
                else
                {
                    ActualResult = mElement.Text.Trim();
                    if (ActualResult.Contains("\r\n"))
                    {
                        ActualResult = ActualResult.Replace("\r\n", "<br>");
                    }
                }
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, ActualResult);
                ClickBanner();
            }
            catch (Exception e)
            {
                if (DlkAcumenTouchStoneFunctionHandler.errorMessageContent != String.Empty)
                {
                    string ActualResult = "";
                    if (DlkAcumenTouchStoneFunctionHandler.errorMessageContent != "")
                    {
                        ActualResult = DlkAcumenTouchStoneFunctionHandler.errorMessageContent;
                    }
                    else
                    {
                        throw new Exception("VerifyText() failed : " + e.Message, e);
                    }
                    DlkAssert.AssertEqual("VerifyText() : " + mControlName, ExpectedValue, ActualResult);
                    ClickBanner();
                }
                else
                {
                    throw new Exception("VerifyText() failed : " + e.Message, e);
                }

            }
        }

        [Keyword("VerifyTextColor", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextColor(String ExpectedValue)
        {
            Initialize();
            if (mElement.GetAttribute("class").Equals("right edit"))
            {
                mElement = mElement.FindElement(By.XPath("./input"));
            }
            string fontColor = mElement.GetCssValue("color");
            if (DlkEnvironment.mBrowser.ToLower() == "firefox")
            {
                fontColor = fontColor.Replace(")", ", " + mElement.GetCssValue("opacity") + ")").Replace("rgb", "rgba");
            }
            DlkAssert.AssertEqual("VerifyTextColor() : " + mControlName, ExpectedValue, fontColor);
            ClickBanner();
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            Initialize();
            String ActualResult = "";

            // Below style does not work on IE
            //ActualResult = mElement.GetAttribute("textContent").Trim();

            ActualResult = new DlkBaseControl("Label", mElement).GetValue();
            if (ActualResult.Contains("\r\n"))
            {
                ActualResult = ActualResult.Replace("\r\n", "<br>");
            }
            if (ExpectedValue.Contains("\n"))
            {
                ExpectedValue = ExpectedValue.Replace("\n", "<br>");
            }
            DlkAssert.AssertEqual("VerifyTextContains() : " + mControlName, true, ActualResult.Contains(ExpectedValue));
            ClickBanner();
        }

        [Keyword("Hover")]
        public void Hover()
        {
            Initialize();
            try
            {
                MouseOver();
                DlkLogger.LogInfo("Hover() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("Hover() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyQuickEditExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyQuickEditExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool actualResult = false;
                Hover();
                Thread.Sleep(500);
                if (mElement.FindElements(By.XPath(".//*[contains(@class,'icon-edit')]")).Count > 0)
                {
                    IWebElement editIcon = mElement.FindElements(By.XPath(".//*[contains(@class,'icon-edit')]")).First();
                    //string text = editIcon.GetCssValue("display");
                    if (editIcon.GetCssValue("display") != "none")
                    {
                        actualResult = true;
                    }
                }
                DlkAssert.AssertEqual("VerifyQuickEditExists() : " + mControlName, Convert.ToBoolean(TrueOrFalse), actualResult);
                ClickBanner();
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyQuickEditExists(): failed : " + ex.Message, ex);
            }
        }

        [Keyword("Close")]
        public void Close()
        {
            try
            {
                Initialize();
                IWebElement buttonElement;
                if (mElement.FindElements(By.XPath(mCloseButtonXPath)).Any())
                {
                    buttonElement = mElement.FindElements(By.XPath(mCloseButtonXPath)).First();
                }
                else
                {
                    buttonElement = mElement.FindElements(By.XPath(".//div[contains(@class, 'icon-close')]")).First();
                }
                DlkButton btnClose = new DlkButton("Close", buttonElement);
                btnClose.Click();
            }
            catch (Exception e)
            {

                throw new Exception("Close() failed : " + e.Message, e);
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

        /// <summary>
        /// Click on banner to refresh logout timer
        /// </summary>
        public void ClickBanner()
        {
            /*Removed this functionality after adjusting timeout recurrence to 30mins*/

            //try
            //{
            //    DlkLogger.LogInfo("Performing Click on Banner to refresh logout timer");
            //    DlkBaseControl bannerCtrl = new DlkBaseControl("Banner", DlkEnvironment.AutoDriver.FindElement(By.XPath("//div[@class='banner']")));
            //    bannerCtrl.Click();
            //}
            //catch
            //{
            //    //Do nothing -- there might be instances that setting a text or value would display a dialog message
            //    //Placing a log instead for tracking
            //    DlkLogger.LogInfo("Problem performing Click on Banner. Proceeding...");
            //}
        }
    }
}
