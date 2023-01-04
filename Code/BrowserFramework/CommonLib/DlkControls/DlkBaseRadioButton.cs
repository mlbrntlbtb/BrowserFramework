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


namespace CommonLib.DlkControls
{
    /// <summary>
    /// Base class for radiobuttons
    /// </summary>
    public class DlkBaseRadioButton : DlkBaseControl
    {

        public DlkBaseRadioButton(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkBaseRadioButton(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            if (mElement == null)
            {
                FindElement();
            }
        }

    }
}

