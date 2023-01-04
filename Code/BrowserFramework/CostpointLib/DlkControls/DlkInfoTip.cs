using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;

namespace CostpointLib.DlkControls
{
    [ControlType("InfoTip")]
    public class DlkInfoTip : DlkBaseControl
    {

        public DlkInfoTip(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkInfoTip(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkInfoTip(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }

        public void Initialize()
        {
                FindElement();
        }

        [Keyword("VerifyText", new String[] { "1|text|Text To Verify|Info Text" })]
        public void VerifyText(String TextToVerify)
        {
            try
            {
                Initialize();

                //Hover on the infotip in order to display the info tip text
                DlkBaseControl infoControl = new DlkBaseControl("infoTip", mElement);
                infoControl.MouseOver();

                //Find info tip text elem as it is not a child or sibling of the infoTip elem/control
                IWebElement mInfoTextElem = mElement.FindElement(By.XPath("//div[@id='infoTipDiv']/div[@id='infoTipText'and not(contains(@style,'none'))]"));

                //Perform verify
                DlkAssert.AssertEqual("VerifyText() : " + mControlName, TextToVerify, mInfoTextElem.Text.Trim());
            }
            catch (Exception e)
            {
                throw new Exception("VerifyText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                Initialize();
                Boolean ActValue;

                //Find info tip text elem 
                IWebElement mInfoTextElem = mElement.FindElement(By.XPath("//div[@id='infoTipDiv']"));
                string elemStyle = mInfoTextElem.GetAttribute("style");

                if (elemStyle.Contains("block"))
                {
                    ActValue = true;
                    DlkAssert.AssertEqual("VerifyExists() : " + mControlName, Convert.ToBoolean(ExpectedValue), ActValue);
                }
                else
                {
                    ActValue = false;
                    DlkAssert.AssertEqual("VerifyExists() : " + mControlName, Convert.ToBoolean(ExpectedValue), ActValue);
                }
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

    }
}
