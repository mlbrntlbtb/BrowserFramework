using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace CostpointLib.DlkControls
{
    [ControlType("Image")]
    public class DlkImage : DlkBaseControl
    {

        public DlkImage(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkImage(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
                FindElement();
        }

        public void VerifyImageNotBroken()
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
                throw new Exception ("Image src is empty. Control: " + mControlName);
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

    }
}
