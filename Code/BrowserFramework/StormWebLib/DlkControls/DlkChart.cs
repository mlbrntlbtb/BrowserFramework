using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using StormWebLib.System;


namespace StormWebLib.DlkControls
{
    [ControlType("Chart")]
    public class DlkChart : DlkBaseControl
    {
        private String mHeaderXPath = ".//div[@class='dashpartHdr']";
        private String mChartImageXPath = ".//div[contains(@class,'dashpartContent')]//canvas";
        private String mLegendXPath = ".//div[contains(@class,'dashpartContent')]//table[contains(@class,'table-legend')]";
        private String mYAxisXPath = ".//div[contains(@class,'dashpartContent')]//div[contains(@class,'jqplot-yaxis')]";
        private String mXAxisXPath = ".//div[contains(@class,'dashpartContent')]//div[contains(@class,'jqplot-xaxis')]";

        public DlkChart(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkChart(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkChart(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();            

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

        [Keyword("VerifyHeaderCaption", new String[] { "1|text|Expected Value|Sample header caption" })]
        public void VerifyHeaderCaption(String ExpectedValue)
        {
            try
            {
                Initialize();
                IWebElement headerElem = mElement.FindElement(By.XPath(mHeaderXPath));
                IWebElement hdrCaption = headerElem.FindElement(By.XPath(".//span[contains(@class,'hdrCaption')]"));
                DlkLabel lblTitle = new DlkLabel("Title", hdrCaption);
                lblTitle.VerifyText(ExpectedValue);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeaderCaption() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyHeaderIsLink", new String[] { "1|text|TrueOrFalse|True" })]
        public void VerifyHeaderIsLink(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bIsLink = false;
                IWebElement headerElem = mElement.FindElement(By.XPath(mHeaderXPath));
                if (headerElem.FindElements(By.XPath(".//span[contains(@class,'hdrCaptionLink')]")).Count > 0)
                {
                    bIsLink = true;
                }
                DlkAssert.AssertEqual("VerifyHeaderIsLink(): ", Convert.ToBoolean(TrueOrFalse), bIsLink);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyHeaderIsLink() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyChartImageExists", new String[] { "1|text|TrueOrFalse|True" })]
        public void VerifyChartImageExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bExists = false;
                if (mElement.FindElements(By.XPath(mChartImageXPath)).Count > 0)
                {
                    bExists = true;
                }
                DlkAssert.AssertEqual("VerifyChartImageExists(): ", Convert.ToBoolean(TrueOrFalse), bExists);
            }
            catch (Exception e)
            {
                throw new Exception("VerifyChartImageExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLegendItems", new String[] { "1|text|Expected Items|Item 1~Item 2~Item 3" })]
        public void VerifyLegendItems(String ExpectedItems)
        {
            try
            {
                Initialize();
                String ActualItems = "";
                IWebElement legendTable = mElement.FindElement(By.XPath(mLegendXPath));
                foreach (IWebElement elm in legendTable.FindElements(By.XPath(".//td[contains(@class,'legend-label')]")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    ActualItems += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                }
                ActualItems = ActualItems.Trim('~');
                DlkAssert.AssertEqual("VerifyLegendItems()", ExpectedItems, ActualItems);
                DlkLogger.LogInfo("VerifyLegendItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLegendItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyYAxisItems", new String[] { "1|text|Expected Items|Item 1~Item 2~Item 3" })]
        public void VerifyYAxisItems(String ExpectedItems)
        {
            try
            {
                Initialize();
                String ActualItems = "";
                IWebElement listYItems = mElement.FindElement(By.XPath(mYAxisXPath));
                foreach (IWebElement elm in listYItems.FindElements(By.XPath(".//div[contains(@class,'yaxis-tick')]")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    ActualItems += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "") + "~";
                }
                ActualItems = ActualItems.Trim('~');
                DlkAssert.AssertEqual("VerifyYAxisItems()", ExpectedItems, ActualItems);
                DlkLogger.LogInfo("VerifyYAxisItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyYAxisItems() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyXAxisItems", new String[] { "1|text|Expected Items|Item 1~Item 2~Item 3" })]
        public void VerifyXAxisItems(String ExpectedItems)
        {
            try
            {
                Initialize();
                String ActualItems = "";
                IWebElement listXItems = mElement.FindElement(By.XPath(mXAxisXPath));
                foreach (IWebElement elm in listXItems.FindElements(By.XPath(".//div[contains(@class,'xaxis-tick')]")))
                {
                    DlkBaseControl ctl = new DlkBaseControl("Item", elm);
                    ActualItems += DlkString.ReplaceCarriageReturn(ctl.GetValue(), "");
                }
                ActualItems = ActualItems.Trim('~');
                DlkAssert.AssertEqual("VerifyXAxisItems()", ExpectedItems, ActualItems);
                DlkLogger.LogInfo("VerifyXAxisItems() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyXAxisItems() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickHeaderButton", new String[] { "1|text|Button Caption|Sample Button" })]
        public void ClickHeaderButton(String ButtonCaption)
        {
            try
            {
                Initialize();
                IWebElement hdrElem = mElement.FindElement(By.XPath(mHeaderXPath));
                IWebElement button;
                switch (ButtonCaption.ToLower()){
                    case "select":
                        button = hdrElem.FindElement(By.XPath(".//div[contains(@class,'dashpart')/img[contains(@class,'select')]"));
                        break;
                    case "maximize":
                        button = hdrElem.FindElement(By.XPath(".//div[contains(@class,'dashpart')/img[contains(@class,'maximize')]"));
                        break;
                    case "restore":
                        button = hdrElem.FindElement(By.XPath(".//div[contains(@class,'dashpart')/img[contains(@class,'restore')]"));
                        break;
                    case "tooltip":
                        button = hdrElem.FindElement(By.XPath(".//div[contains(@class,'dashpart')/img[contains(@class,'toolTipButton')]"));
                        break;
                    default:
                        throw new Exception("ClickHeaderButton() failed : " + ButtonCaption + " is an unrecognized button name");
                }
                new DlkButton("Button", button).Click();
            }
            catch (Exception e)
            {
                throw new Exception("ClickHeaderButton() failed : " + e.Message, e);
            }
        }
    }
}
