using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace KnowledgePointLib.DlkControls
{
    [ControlType("Map")]
    public class DlkMap : DlkBaseControl
    {
        #region Constructors
        public DlkMap(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkMap(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkMap(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion
        private void Initialize()
        {
            FindElement();
        }

        #region Keywords
        [Keyword("AssignProjectCountByStateToVariable", new String[] { "1|text|Expected Value|TRUE" })]
        public void AssignProjectCountByStateToVariable(String State, String VariableName)
        {
            try
            {
                string projectCount = GetProjectCountByState(State).ToString();
                DlkVariable.SetVariable(VariableName, projectCount);
                DlkLogger.LogInfo("AssignProjectCountByStateToVariable()", mControlName, "Variable:[" + VariableName + "], Value:[" + projectCount + "].");
            }
            catch (Exception e)
            {
                throw new Exception("AssignProjectCountByStateToVariable() failed : " + e.Message, e);
            }
        }

        /// <summary>
        ///  Verifies if Map exists. Requires TrueOrFalse - can either be True or False
        /// </summary>
        /// <param name="strExpectedValue"></param>
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


        /// <summary>
        ///  Verifies widget content list.
        /// </summary>
        /// <param name="strExpectedValue"></param>
        [Keyword("VerifyProjectCountByState", new String[] { "1|text|Expected contents|List1~List2~List3" })]
        public void VerifyProjectCountByState(String State, String ProjectCount)
        {
            try
            {
                int projectCount = GetProjectCountByState(State);
                DlkAssert.AssertEqual("VerifyProjectCount() : " + mControlName, projectCount, Convert.ToInt32(ProjectCount));
            }
            catch (Exception e)
            {
                throw new Exception("VerifyProjectCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLegendExists", new String[] { "1|text|Expected contents|List1~List2~List3" })]
        public void VerifyLegendExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                bool legendExists = mElement.FindElements(By.XPath(".//preceding-sibling::*[@id='legendContainer']")).Count != 0; // exists
                DlkAssert.AssertEqual("VerifyLegendExists(): " + "Map Legends", Convert.ToBoolean(TrueOrFalse), legendExists);
                DlkLogger.LogInfo("VerifyLegendExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLegendExists() failed : " + e.Message, e);
            }
        }
        #endregion

        #region Private Methods
        private int GetProjectCountByState(string state)
        {
            Initialize();
            // Hover on selected state first
            DlkBaseControl stateElement = new DlkBaseControl("Widget", mElement.FindElement(By.XPath(".//*[@name='" + state + "']")));
            stateElement.MouseOver();
            DlkLogger.LogInfo("Hovering on selected state..");
            // Tooltip will then be visible
            string toolTipValue = mElement.FindElement(By.XPath("//span[@id='TooltipCount']")).Text; // contains "627 projects"
            int toolTipCount = Convert.ToInt32(toolTipValue.Split(' ')[0]); // get 627 from original "627 projects"
            return toolTipCount;
        }

        #endregion
    }
}
