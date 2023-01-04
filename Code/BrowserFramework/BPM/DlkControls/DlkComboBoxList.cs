using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;
using BPMLib.DlkControls;
using BPMLib.DlkUtility;

namespace BPMLib.DlkControls
{
    public class DlkComboBoxList : DlkBaseControl
    {
        private IList<IWebElement> mItemElements = null;
        private string mStrListItemDesc = "";

        public DlkComboBoxList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBoxList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBoxList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
            DlkEnvironment.mSwitchediFrame = true;
        }

        private void Terminate()
        {
            DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
            DlkEnvironment.mSwitchediFrame = false;
        }

        public void Select(String Value)
        {
            Initialize();
            Boolean bFound = false;
            String actualItems = "";
            DlkBaseControl dlkComboItem;

            mItemElements = mElement.FindElements(By.CssSelector(mStrListItemDesc));
            foreach (IWebElement aListItem in mItemElements)
            {
                dlkComboItem = new DlkBaseControl("Combo Item", aListItem);
                actualItems = actualItems + dlkComboItem.GetValue() + " ";
                if (dlkComboItem.GetValue().ToLower() == Value.ToLower())
                {
                    dlkComboItem.Click();

                    bFound = true;
                    break;
                }
            }
            if (!bFound)
            {
                throw new Exception("Select() failed. Control : " + mControlName + " : '" + Value +
                                        "' not found in list. : Actual List = " + actualItems);
            }
        }
    }
}
