using System;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
{
    [ControlType("Template")]
    public class DlkTemplate : DlkBaseControl
    {
        public DlkTemplate(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTemplate(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTemplate(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
                FindElement();
        }

        //[Keyword("Click")]
        //public new void Click()
        //{
        //    try
        //    {
        //        Initialize();

        //        if (mElement.GetAttribute("class") == "popupBtn")
        //        {
        //            if (DlkEnvironment.mBrowser.ToLower() == "ie")
        //            {
        //                Click(5, 5);
        //            }
        //            else
        //            {
        //                Click(4.5);
        //            }
        //        }
        //        else
        //        {
        //            Click(4.5);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Click() failed : " + e.Message, e);
        //    }

        //}



    }
}
