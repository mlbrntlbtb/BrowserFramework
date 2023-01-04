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
using System.Drawing;

using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("Image")]
    public class DlkImage : DlkBaseControl
    {
        private Boolean IsInit = false;
        #region Constructors
        public DlkImage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkImage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

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


        }

        #region Verify methods
        private void VerifyImageNotBroken()
        {
            Initialize();
            String mSrc = mElement.GetAttribute("src");
            String mNaturalHeight = mElement.GetAttribute("naturalHeight");
            String mNaturalWidth = mElement.GetAttribute("naturalWidth");
            DlkLogger.LogInfo("Image [" + mControlName + "] src: " + mSrc);
            DlkLogger.LogInfo("Image [" + mControlName + "] naturalHeight: " + mNaturalHeight);
            DlkLogger.LogInfo("Image [" + mControlName + "] naturalWidth: " + mNaturalWidth);

            if ((mSrc == "") || (mSrc == null))
            {
                throw new Exception("Image src is empty. Control: " + mControlName);
            }
            if ((mNaturalHeight == "") || (mNaturalHeight == null))
            {
                throw new Exception("Image naturalHeight is empty. Control: " + mControlName);
            }
            else
            {
                if ((Convert.ToInt32(mNaturalHeight) < 5))
                {
                    throw new Exception("Image naturalHeight is less than 5. Control: " + mControlName);
                }
            }
            if ((mNaturalWidth == "") || (mNaturalWidth == null))
            {
                throw new Exception("Image naturalWidth is empty. Control: " + mControlName);
            }
            else
            {
                if ((Convert.ToInt32(mNaturalWidth) < 5))
                {

                    throw new Exception("Image naturalWidth is less than 5. Control: " + mControlName);
                }
            }
            DlkLogger.LogInfo("Successfully verified image is valid for control: " + mControlName);
        }

        [RetryKeyword("VerifyPartialSource", new String[] { "1|text|ExpectedPartialSource|img.gif" })]
        public void VerifyPartialSource(String ExpectedPartialSource)
        {
            String expectedPartialSource = ExpectedPartialSource;

            this.PerformAction(() =>
                {
                    Initialize();

                    String sActualSource = mElement.GetAttribute("src");
                    Boolean bActual = sActualSource.Contains(expectedPartialSource);
                    DlkAssert.AssertEqual("VerifyPartialSource()", true, bActual);
                }, new String[] {"retry"});
        }

        [RetryKeyword("VerifyIfImageLoaded", new String[] { "1|text|Expected|TRUE" })]
        public void VerifyIfImageLoaded(String TrueOrFalse)
        {
            bool expectedResult = Convert.ToBoolean(TrueOrFalse);

            this.PerformAction(() =>
            {
                Initialize();

                String sActualSource = mElement.GetAttribute("src");
                Boolean bActual = !sActualSource.Contains("loading");
                DlkAssert.AssertEqual("VerifyIfImageLoaded()", expectedResult, bActual);
            }, new String[] { "retry" });
        }
        #endregion

    }
}

