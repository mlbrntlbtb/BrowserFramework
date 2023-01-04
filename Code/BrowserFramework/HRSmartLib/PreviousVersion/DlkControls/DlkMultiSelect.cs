using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System.Threading;

namespace HRSmartLib.PreviousVersion.DlkControls
{
    [ControlType("MultiSelect")]
    public class DlkMultiSelect : DlkBaseControl
    {
        #region Declarations



        #endregion

        #region Constructors

        public DlkMultiSelect(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues)
        {
            initialize();
        }

        public DlkMultiSelect(String ControlName, IWebElement existingWebElement)
            : base(ControlName, existingWebElement)
        {
            initialize();
        }

        #endregion

        #region Keywords

        [Keyword("Select")]
        public void Select(string Values)
        {
            try
            {
                string[] valueList = Values.Split('~');
                IList<IWebElement> selectOptionList = mElement.FindElements(By.XPath(@".//option"));

                for (int i = 0; i < valueList.Length; i++)
                {
                    bool foundValue = false;
                    string textValue = valueList[i];

                    foreach (IWebElement element in selectOptionList)
                    {
                        DlkBaseControl control = new DlkBaseControl("Option", element);
                        if (control.GetValue().Equals(textValue))
                        {
                            foundValue = true;
                            control.Click();
                            DlkLogger.LogInfo(textValue + "selected.");
                            break;
                        }
                    }

                    if (!foundValue)
                    {
                        DlkLogger.LogInfo(textValue + " not found.");
                    }
                }

                DlkLogger.LogInfo("Select( ) successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("Select( ) execution failed : " + ex.Message, ex);
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

        [Keyword("VerifyState")]
        public void VerifyState(String TrueOrFalse)
        {
            try
            {
                bool actualResult = false;
                bool expectedResult = Convert.ToBoolean(TrueOrFalse);
                string disabled = mElement.GetAttribute("disabled").Trim();
                if (disabled != null &&
                    disabled == "true")
                {
                    actualResult = false;
                }
                else
                {
                    actualResult = true;
                }

                DlkAssert.AssertEqual("VerifyState : ", expectedResult, actualResult);
                DlkLogger.LogInfo("VerifyState() successfully executed.");
            }
            catch(Exception ex)
            {
                throw new Exception("VerifyState() execution failed. " + ex.Message, ex);
            }
        }
        #endregion

        #region Methods

        private void initialize()
        {
            FindElement();
        }

        #endregion
    }
}
