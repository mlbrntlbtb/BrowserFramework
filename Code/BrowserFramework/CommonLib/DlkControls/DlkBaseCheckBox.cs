using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using CommonLib.DlkSystem;


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Base class for checkboxes 
    /// </summary>
    public class DlkBaseCheckBox : DlkBaseControl
    {

        public DlkBaseCheckBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseCheckBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        /// <summary>
        /// finds the element
        /// </summary>
        public void Initialize()
        {
            if (mElement == null)
            {
                FindElement();
            }
        }

        /// <summary>
        /// sets the checkbox; on=true, off=false
        /// </summary>
        /// <param name="bIsChecked"></param>
        public void Set(Boolean bIsChecked)
        {
            Initialize();
            Boolean bCurrentValue = GetCheckedState();
            if (bCurrentValue != bIsChecked)
            {
                this.mElement.Click();
                Thread.Sleep(200);
            }
            VerifyChecked(bIsChecked);
            DlkLogger.LogInfo("Successfully executed Set(): " + mControlName);
        }

        /// <summary>
        /// gets the state of the checkbox; on=true, off=false
        /// </summary>
        /// <returns></returns>
        public Boolean GetCheckedState()
        {
            Initialize();
            Boolean bCurrentVal = false;
            String strCurrentVal = this.GetAttributeValue("checked");
            if (strCurrentVal != null)
            {
                bCurrentVal = true;
            }
               
            return bCurrentVal;
        }

        /// <summary>
        /// Verifies the state of the checkbox; on=true, off=false
        /// </summary>
        /// <param name="bIsChecked"></param>
        public void VerifyChecked(Boolean bIsChecked)
        {
            Boolean bCurrentValue = GetCheckedState();
            DlkAssert.AssertEqual("VerifyChecked() : " + bIsChecked.ToString() + " : " + mControlName, bIsChecked, bCurrentValue);
        }
    }
}

