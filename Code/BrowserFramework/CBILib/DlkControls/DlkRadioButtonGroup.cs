using CBILib.DlkUtility;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBILib.DlkControls
{
    [ControlType("RadioButtonGroup")]
    public class DlkRadioButtonGroup : DlkBaseControl
    {
        private List<IWebElement> mRadioButtons = new List<IWebElement>();

        #region constructors
        public DlkRadioButtonGroup(string ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public DlkRadioButtonGroup(string ControlName, string SearchType, string SearchValue)
            : base(ControlName, SearchType, SearchValue) { }

        public DlkRadioButtonGroup(string ControlName, string SearchType, string[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public DlkRadioButtonGroup(string ControlName, IWebElement ExistingParentWebElement, string CSSSelector)
            : base(ControlName, ExistingParentWebElement, CSSSelector) { }

        public DlkRadioButtonGroup(string ControlName, DlkBaseControl ParentControl, string SearchType, string SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        #endregion

        private void Initialize()
        {
            DlkCERCommon.WaitForPromptSpinner();
            FindElement();
            GetRadioButtons();
        }

        [Keyword("Select", new String[] { "1|text|Item|Account" })]
        public void Select(string Item)
        {
            try
            {
                Initialize();

                bool found = false;
                //use foreach instead of xpath to avoid issue with nbsp tag
                foreach (IWebElement item in mRadioButtons)
                {
                    string radioButtonText = DlkString.RemoveCarriageReturn(item.Text);
                    if (radioButtonText == Item)
                    {
                        item.FindElement(By.XPath(".//input")).Click();
                        found = true;
                        break;
                    }                    
                }

                if (found)
                {
                    DlkCERCommon.WaitForPromptSpinner();
                    DlkLogger.LogInfo("Select() Passed.");
                }
                else
                {
                    throw new Exception($"Radiobutton item '{Item}' not found.");
                }                
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        private void GetRadioButtons()
        {
            var items = mElement.FindElements(By.XPath(".//div[contains(@class,'clsCheckBoxRow')]"));

            if (items.Count > 0)
            {
                mRadioButtons.AddRange(items);
            }
            else
            {
                throw new Exception("No radiobutton items found.");
            }
        }
    }
}
