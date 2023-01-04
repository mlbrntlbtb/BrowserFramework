using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using DeltekProjectsToolLib.DlkSystem;
using System.Linq;

namespace DeltekProjectsToolLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {

        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkDeltekProjectsToolFunctionHandler.WaitScreenGetsReady(DlkDeltekProjectsToolFunctionHandler.DEFAULT_WAIT_TIME);
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Item)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Item)) throw new Exception("Item must not be empty");
                Item = Item.Trim().Replace(" ", "");
                Initialize();
                mElement.Click();
                var lstItems = mElement.FindElements(By.XPath(".//option")).Where(item => item.Displayed && item.Text.Replace(" ", "") == Item).ToList();
                if (lstItems.Count == 0)
                {
                    throw new Exception("Select() failed : item not found in list");
                }
                else
                {
                    lstItems.First().Click();
                    DlkDeltekProjectsToolFunctionHandler.WaitScreenGetsReady(DlkDeltekProjectsToolFunctionHandler.DEFAULT_WAIT_TIME);
                    Initialize();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }
    }
}
