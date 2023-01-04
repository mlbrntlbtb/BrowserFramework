using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium.Interactions;
using CommonLib.DlkUtility;
using StormWebLib.System;

namespace StormWebLib.DlkControls
{
    [ControlType("Timeline")]
    public class DlkTimeline : DlkBaseControl
    {
        #region CONSTRUCTORS
        public DlkTimeline(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTimeline(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTimeline(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkTimeline(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region CONSTANTS
        private const String strTimelineXpath = ".//*[@class='timeline-milestone-container']";
        private const String strTimelineMarkerXPATH = ".//*[@class='timeline-marker']";

        #endregion


        #region PRIVATE MEMBERS
        IList<IWebElement> mMilestones;
        
        #endregion


        #region PRIVATE METHODS

        private void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();

            FindElement();
            this.ScrollIntoViewUsingJavaScript();
            GetMilestones();
        }

        private void GetMilestones()
        {
            mMilestones = this.mElement.FindElements(By.XPath(strTimelineXpath)).Where(e => e.Displayed).ToList();
            if (! (mMilestones.Count > 0))
            {
                throw new Exception("No milestones displayed.");
            }
            DlkLogger.LogInfo("Milestones displayed [" + mMilestones.Count + "]");
        }

        private String FormatStrings(String Input)
        {
            string val = Input;
            val = DlkString.RemoveCarriageReturn(val);
            val = DlkString.ReplaceElipsesWithThreeDots(val);
            return val;
        }

        #endregion

        #region KEYWORDS
        [Keyword("ClickMilestone", new String[] { "1|text|Expected Tick Values|ExpectedValues" })]
        public void ClickMilestone(String Item)
        {
            try
            {
                // guard clause
                if (String.IsNullOrWhiteSpace(Item)) throw new ArgumentException("Parameter must not be empty.");

                bool bFound = false;
                Initialize();
                foreach (IWebElement mItem in mMilestones)
                {
                    DlkBaseControl milestone = new DlkBaseControl("Item", mItem);
                    string val = FormatStrings(milestone.GetValue());
                    if (val.Trim() == Item.Trim())
                    {
                        IWebElement mButton = mItem.FindElement(By.XPath(strTimelineMarkerXPATH));
                        new DlkBaseControl("Button", mButton).ClickUsingJavaScript();
                        bFound = true;
                        DlkLogger.LogInfo("ClickMilestone() passed");
                        break;
                    }
                }
                if (!bFound)
                {
                    throw new Exception("Milestone ['" + Item + "'] not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("ClickMilestone() failed: " + e.Message);
            }
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
        
        [Keyword("VerifyMilestoneExists", new String[] { "1|text|Expected Tick Values|ExpectedValues" })]
        public void VerifyMilestoneExists(String ExpectedValue, String TrueOrFalse)
        {
            try
            {
                Boolean expected = false;
                // guard clause
                if (String.IsNullOrWhiteSpace(ExpectedValue)) throw new ArgumentException("Parameter must not be empty.");
                if (!Boolean.TryParse(TrueOrFalse, out expected)) throw new ArgumentException("TrueOrFalse must be a Boolean value");

                bool bFound = false;
                Initialize();
                foreach (IWebElement mItem in mMilestones)
                {
                    DlkBaseControl milestone = new DlkBaseControl("Item", mItem);
                    string val = FormatStrings(milestone.GetValue());
                    if (val.Trim() == ExpectedValue.Trim())
                    {
                        bFound = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyMilestoneExists()", expected, bFound);                
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMilestoneExists() failed: " + e.Message);
            }
        }        

        [Keyword("VerifyMilestoneList", new String[] { "1|text|Expected Tick Values|ExpectedValues" })]
        public void VerifyMilestoneList(String Items)
        {
            try
            {
                // guard clause
                if (String.IsNullOrWhiteSpace(Items)) throw new ArgumentException("Parameter must not be empty.");

                string actual = "";
                Initialize();
                foreach (IWebElement mItem in mMilestones)
                {
                    DlkBaseControl milestone = new DlkBaseControl("Item", mItem);
                    string val = FormatStrings(milestone.GetValue());
                    if (val.Trim() != "")
                    {
                        actual += val.Trim() + "~";
                    }
                }
                DlkAssert.AssertEqual("VerifyMilestoneList()", Items, actual.Trim('~'));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyMilestoneList() failed: " + e.Message);
            }
        }
        #endregion
    }
}