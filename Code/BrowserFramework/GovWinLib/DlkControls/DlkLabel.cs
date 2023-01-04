using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        private Boolean IsInit = false;

        #region Constructors
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        #endregion

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

            if (mControlName.Equals("lblRelatedContent_EditRelatedDocument_DocumentSource_ValidationMessage"))
            {
                var x = mControlName;
            }

        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                Initialize();

                Click(5, 5);

                DlkLogger.LogInfo("Successfully executed Click().");
            }
            catch (InvalidOperationException invalid)
            {
                //InvalidOperationCan be due to alert dialog from WaitUrlUpdate call above
                if (invalid.Message.Contains("Modal dialog present"))
                {
                    if (DlkAlert.DoesAlertExist(3))
                    {
                        DlkAlert.Accept();
                        Click();
                    }
                }
                else
                {
                    throw new Exception(string.Format("Exception of type {0} caught in button Click() method.", invalid.GetType()));
                }
            }
            catch (Exception e)
            {

                throw new Exception("Click() failed : " + e.Message);

            }

        }

        [Keyword("GetText", new String[] {"1|text|VariableName|MyText"})]
        public virtual void GetText(String sVariableName)
        {            
            Initialize();
            DlkVariable.SetVariable(sVariableName, mElement.Text);
        }

        [Keyword("VerifyIsCurrentDate")]
        public void verifyIsCurrentDate()
        {
            bool actualResult = false;
            Initialize();

            String lastUpdateDate = mElement.Text;

            DateTimeOffset newTime = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time"));
            String currentDate = newTime.ToString("MM/dd/yyyy");

            if (lastUpdateDate.Trim().Equals(currentDate))
                actualResult = true;

            DlkAssert.AssertEqual(string.Format("VerifyIsCurrentDate() for {0} Expected: true, Actual {1}", mControlName, actualResult), true, actualResult);
            
        }

        [Keyword("VerifyIsRequired", new String[] { "1|Text|ExpectedResult (TRUE/FALSE)|TRUE" })]
        public void VerifyIsRequired(String TrueOrFalse)
        {
            bool actualResult = Convert.ToBoolean(TrueOrFalse) ? false : true;

            Initialize();
            String classField = mElement.GetAttribute("class");
            if (classField.Equals("required"))
            {
                actualResult = true;
            }else{
                actualResult = false;
            }

            DlkAssert.AssertEqual(string.Format("VerifyIsRequired() for {0}: Expected: {1} Actual:{2}", mControlName, Convert.ToBoolean(TrueOrFalse), actualResult), Convert.ToBoolean(TrueOrFalse), actualResult);

        }        

        [Keyword("WaitForTextChanges", new String[] { "1|text|Wait Time|10" })]
        public void WaitForTextChanges(String sWaitTime)
        {
            Boolean bExceptionOnce = false;
            String sOrigText = "";

            GetOrigValue: 
            FindElement(1);

            try
            {
                sOrigText = GetValue();
            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("element not found in the cache ") && bExceptionOnce)
                {
                    bExceptionOnce = true;
                    goto GetOrigValue;
                }
                else
                {
                    throw;
                }
            }

            Boolean bChanged = false;
            for (int i = 0; i < Convert.ToInt32(sWaitTime); i++)
            {
                String sCurrentText = "";

                GetCurrentValue:
                FindElement(1);
                try
                {
                    sCurrentText = GetValue();
                }
                catch (Exception e)
                {
                    if (e.Message.ToLower().Contains("element not found in the cache ") && bExceptionOnce)
                    {
                        bExceptionOnce = true;
                        goto GetCurrentValue;
                    }
                    else
                    {
                        throw;
                    }
                }

                if (sOrigText != sCurrentText)
                {
                    DlkLogger.LogInfo("Successfully executed WaitForTextChanges().");
                    bChanged = true;
                    break;
                }
                Thread.Sleep(1000);
            }
            if (!bChanged)
            {
                throw new Exception("WaitForTextChanges() has timed out.");
            }
            
            
        }

        private String TrimValue(String sValue)
        {
            sValue = sValue.Replace("\n", " ");
            sValue = sValue.Replace("\r", " ");
            while (sValue.Contains("  "))
            {
                sValue = sValue.Replace("  ", " ");
            }
            return sValue.Trim();
        }

        #region Verify methods
        [RetryKeyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String Text)
        {
            String textToVerify = Text;

            this.PerformAction(() =>
            {
                FindElement();
                String ActualResult = "";

                // Below style does not work on IE
                //ActualResult = mElement.GetAttribute("textContent").Trim();
                switch (GetAttributeValue("tagName").ToLower())
                {
                    case "h1":
                        ActualResult = TrimValue(GetValue());
                        IList<IWebElement> aElements = mElement.FindElements(By.CssSelector("a"));
                        foreach (IWebElement aElement in aElements)
                        {
                            DlkBaseControl linkControl = new DlkBaseControl("Link", aElement);
                            ActualResult = ActualResult.Replace(linkControl.GetValue(), "").Replace("  "," ");
                        }
                        ActualResult = ActualResult.Trim();
                        //String[] actualResults = GetAttributeValue("innerHTML").Split(new String[] { "</a>" }, StringSplitOptions.RemoveEmptyEntries);
                        //if (actualResults.Count() > 1)
                        //{
                        //    ActualResult = actualResults[1].Trim();
                        //}
                        //else
                        //{
                        //    ActualResult = mElement.Text.Trim();
                        //}
                        break;
                    default:
                        ActualResult = mElement.Text.Trim();
                        break;
                }

                DlkAssert.AssertEqual("VerifyText() : " + mControlName, textToVerify, ActualResult.Trim());
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTextChange", new String[] { "1|text|Text To Verify|Sample Label Text",
                                                         "2|text|Expected Value (TRUE or FALSE)|TRUE" })]
        public void VerifyTextChange(String Text, String TrueOrFalse)
        {
            String textToVerify = Text;
            String sActualResults = "false";
            String sExpectedResults = TrueOrFalse.ToLower();

            this.PerformAction(() =>
            {
                FindElement();
                String ActualResult = "";

                switch (GetAttributeValue("tagName").ToLower())
                {
                    case "h1":
                        ActualResult = TrimValue(GetValue());
                        IList<IWebElement> aElements = mElement.FindElements(By.CssSelector("a"));
                        foreach (IWebElement aElement in aElements)
                        {
                            DlkBaseControl linkControl = new DlkBaseControl("Link", aElement);
                            ActualResult = ActualResult.Replace(linkControl.GetValue(), "");
                        }
                        ActualResult = ActualResult.Trim();
                        break;
                    default:
                        ActualResult = mElement.Text.Trim();
                        break;
                }

                if (ActualResult.Equals(textToVerify))
                    sActualResults = "false";
                else sActualResults = "true";

                DlkAssert.AssertEqual("VerifyTextChange()", sExpectedResults, sActualResults);

            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTextContains", new String[] { "1|text|ExpectedText|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedText)
        {
            String expectedText = TrimValue(ExpectedText);

            this.PerformAction(() =>
            {
                FindElement();
                String ActualText = "";

                // Below style does not work on IE
                //ActualResult = mElement.GetAttribute("textContent").Trim();


                switch (mElement.GetAttribute("tagName").Trim().ToLower())
                {
                    case "h1":
                        ActualText = TrimValue(GetValue()).ToLower();
                        break;
                    default:
                        ActualText = TrimValue(mElement.Text.Trim()).ToLower();
                        break;
                }
                Boolean ActualResult = ActualText.Contains(expectedText.ToLower());
                DlkAssert.AssertEqual("VerifyTextContains()", true, ActualResult);
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyTextContainsContent", new String[] { "1|text|ExpectedText|Sample Label Text" })]
        public void VerifyTextContainsContent(String ExpectedText)
        {
            String expectedText = ExpectedText;

            this.PerformAction(() =>
            {
                FindElement();
                String ActualText = "";

                // Below style does not work on IE
                //ActualResult = mElement.GetAttribute("textContent").Trim();
                switch (GetAttributeValue("tagName").ToLower())
                {
                    case "h1":
                        ActualText = TrimValue(GetValue());
                        break;
                    default:
                        ActualText = mElement.Text.Trim();
                        break;
                }
                Boolean ActualResult = (TrimValue(ActualText)).Replace(" ", "").Contains(TrimValue(expectedText).Replace(" ", ""));
                DlkAssert.AssertEqual("VerifyTextContainsContent()", true, ActualResult);
            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
            {
                Initialize();
                base.VerifyExists(Convert.ToBoolean(expectedValue));
            }, new String[] {"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyChartViewTableTitle", new String[] { "1|text|Table To Verify|Sample Label Text",
                                                                  "2|text|Expected Value (TRUE or FALSE)|TRUE" })]
        public void VerifyChartViewTableTitle(String TableToVerify, String TrueOrFalse)
        {
            String verifyText = TableToVerify;
            String expectedValue = TrueOrFalse;
            String withMatch = "false";
            
            FindElement();
            IList<IWebElement> tableTitles = mElement.FindElements(By.XPath(".//div[@class='chartDisplay']/div[@class='chartTitle']"));
            
            for (int i = 0; i < tableTitles.Count; i++)
            {
                if (tableTitles[i].Text == verifyText)
                {
                    withMatch = "true";
                }
            }
            
            this.PerformAction(() =>
            {
                Initialize();
                DlkAssert.AssertEqual("VerifyChartViewTableTitle()", expectedValue.ToLower(), withMatch.ToLower());
            }, new String[] { "retry" });
        }
        #endregion
    }
}

