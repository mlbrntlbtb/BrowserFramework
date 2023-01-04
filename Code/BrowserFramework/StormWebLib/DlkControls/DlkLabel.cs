using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Collections.Generic;
using StormWebLib.System;

namespace StormWebLib.DlkControls
{
    [ControlType("Label")]
    public class DlkLabel : DlkBaseControl
    {
        #region Constructors
        public DlkLabel(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkLabel(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkLabel(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        public void Initialize()
        {
            FindElement();
            this.ScrollIntoViewUsingJavaScript();            

        }

        public new bool VerifyControlType()
        {
            FindElement();
            if (mElement.TagName == "div" || mElement.TagName == "span")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Keyword("AssignValueToVariable", new String[] { "1|text|Index|1",
                                            "2|text|VariableName|Sample"})]
        new public void AssignValueToVariable(String VariableName)
        {
            try
            {
                Initialize();
                DlkStormWebFunctionHandler.WaitScreenGetsReady();

                String mValue = this.GetValue().TrimEnd();
                DlkVariable.SetVariable(VariableName, mValue);
                DlkLogger.LogInfo("AssignValueToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + mValue + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignValueToVariable() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyText(String ExpectedValue)
        {
            FindElement();
            String ActualResult = "";

            // Below style does not work on IE
            //ActualResult = mElement.GetAttribute("textContent").Trim();

            if(mElement.GetAttribute("class").Equals("right edit"))
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
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextContains(String ExpectedValue)
        {
            FindElement();
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
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            try
            {
                Boolean bFound = false;
                int i = 1, retryLimit = 5;

                //Used a retry counter than changing the parameter for Exists() since I believe it is more controlled.
                while (i++ <= retryLimit)
                {
                    if (base.Exists(1))
                    {
                        if (!String.IsNullOrWhiteSpace(new DlkBaseControl("Label", mElement).GetValue())) //need to check some labels have empty text but still appear as existing
                        {
                            bFound = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        DlkLogger.LogInfo("Label not found. Retry [" + i + "]");
                    }
                }
                           
                DlkAssert.AssertEqual("VerifyExists():", Convert.ToBoolean(TrueOrFalse), bFound);
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyStickyHeader", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyStickyHeader(String TrueOrFalse)
        {
            try
            {
                FindElement();
                Boolean bSticky = false;
                string mClass = mElement.GetAttribute("class");
                if (!mClass.Contains("section-header")) //sticky header applies only to section-header labels
                {
                    throw new Exception("Element is not a sticky header type.");
                }              
                bSticky = mClass.Contains("sticky") ? true : false;                
                DlkAssert.AssertEqual("VerifyStickyHeader():", Convert.ToBoolean(TrueOrFalse), bSticky);
                DlkLogger.LogInfo("VerifyStickyHeader() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyStickyHeader() failed : " + e.Message, e);
            }
        }

        [Keyword("FieldExists", new String[] { "1|text|VariableName|TrueFalse" })]
        public void FieldExists(String VariableName)
        {
           // Initialize();
            if (this.Exists(1))
            {
                DlkVariable.SetVariable(VariableName, true.ToString());
            }
            else
            {
                DlkVariable.SetVariable(VariableName, false.ToString());
            }
        }

        [Keyword("VerifyRequired", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyRequired(String TrueOrFalse)
        {
            try
            {
                bool actualResult = GetAttributeValue("class").ToLower().Contains("require");
                //in case the definition of a required field is not located on the core-label level, check the core-component level
                if (!actualResult && mElement.FindElements(By.XPath("./ancestor::*[contains(@class,'core-component')]")).Count>0)
                {
                    IWebElement coreComponent = mElement.FindElement(By.XPath("./ancestor::*[contains(@class,'core-component')]"));
                    actualResult = coreComponent.GetAttribute("class").ToLower().Contains("required");
                }
                DlkAssert.AssertEqual("VerifyRequired() : " + mControlName, Convert.ToBoolean(TrueOrFalse), actualResult);
            }
            catch (Exception ex)
            {
                throw new Exception("VerifyRequired(): failed : " + ex.Message, ex);
            }
        }

        [Keyword("Click")]
        public new void Click()
        {
            try
            {
                base.Click();
            }
            catch
            {
                //when the label is obstructed by another control/container
                Initialize();
                base.ClickUsingJavaScript();
            }
        }

        [Keyword("ControlClick")]
        public void ControlClick()
        {
            try
            {
                Initialize();
                OpenQA.Selenium.Interactions.Actions mAction = new OpenQA.Selenium.Interactions.Actions(DlkEnvironment.AutoDriver);
              //  mAction.SendKeys(Keys.Control).MoveToElement(mElement).Click().KeyUp(Keys.Control).Perform();
                mAction.KeyDown(Keys.Control).MoveToElement(mElement).Click().KeyUp(Keys.Control).Perform();
               
                DlkLogger.LogInfo("ControlClick() successfully executed.");

            }
            catch (Exception e)
            {
                throw new Exception("ControlClick() failed : " + e.Message, e);
            }
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

        //This keyword for removing the Multiselect field in Custom Search dialog
        [Keyword("RemoveField", new String[] { "1|text|Value|ItemToDelete" })]
        public void RemoveField()
        {
            Initialize();
            try
            {
                MouseOver();
                IWebElement mField = this.mElement.FindElement(By.ClassName("deleteable"));
                DlkBaseControl ctl = new DlkBaseControl("element", mField);
                //ctl.Click();
                ctl.ClickUsingJavaScript();
                DlkLogger.LogInfo("RemoveField() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("RemoveField() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextWithIcon", new String[] { "1|text|Text To Verify|Sample Label Text" })]
        public void VerifyTextWithIcon(String ExpectedValue, String Icons)
        {
            FindElement();
            String ActualResult = "";
            const int tagLength = 7;

            //Verify Text
            if (mElement.GetAttribute("class").Equals("right edit"))
            {
                ActualResult = new DlkBaseControl("Text", mElement.FindElement(By.XPath("./input"))).GetValue();
            }
            else if (mElement.GetAttribute("data-bubble-type") == "activity")
            {
                ActualResult = mElement.Text.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Trim();
            }
            else
            {
                ExpectedValue = ExpectedValue.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                string content = mElement.GetAttribute("innerHTML").Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Trim();
                List<string> labelContent = new List<string>();
                List<string> labelButtons = new List<string>();
                int spanIndex = content.IndexOf("<span");

                while (spanIndex != -1)
                {
                    labelContent.Add(content.Substring(0, spanIndex));
                    content = content.Remove(0, spanIndex);

                    int spanCloseTag = content.IndexOf("</span>") + tagLength;
                    labelContent.Add(content.Substring(0, spanCloseTag));
                    content = content.Remove(0, spanCloseTag);
                    spanIndex = content.IndexOf("<span");
                }

                if (!String.IsNullOrEmpty(content))
                    labelContent.Add(content);

                foreach (string lContent in labelContent)
                {
                    if (!lContent.Contains("<span"))
                    {
                        ActualResult += lContent;
                    }
                }
            }

            //Verify Icons
            string[] icons = Icons.Split('~');
            bool mCorrectIcons = true;

            IList<IWebElement> buttons = mElement.FindElements(By.XPath("./descendant::span"));
            
            if (icons.Length == buttons.Count)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (!(buttons[i].GetAttribute("class")).ToLower().Contains(icons[i].ToLower()))
                    {
                        mCorrectIcons = false;
                    }
                }
            }
            else
            {
                mCorrectIcons = false;
            }
            DlkAssert.AssertEqual("VerifyTextWithIcon() : " + mControlName, ExpectedValue, ActualResult);
            DlkAssert.AssertEqual("VerifyTextWithIcon() : " + mControlName, true, mCorrectIcons);
        }

    }
}
