using System;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace VisionTimeLib.DlkControls
{
    [ControlType("TimePicker")]
    public class DlkTimePicker : DlkBaseControl
    {
        public DlkTimePicker(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTimePicker(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTimePicker(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        //private string mItemsXPath = ".//div[contains(@class, 'x-dataview-item')]";
        //private string mSelectedItemXPath = ".//div[contains(@class,'x-item-selected')]/div[contains(@class,'x-picker-item')]";
        private string mFirstPickerXPathLeft = ".//div[contains(@class,'x-picker-left')][1]";
        private string mSecondPickerXPathLeft = ".//div[contains(@class,'x-picker-left')][2]";
        private string mThirdPickerXPathLeft = ".//div[contains(@class,'x-picker-left')][3]";

        private string mFirstPickerXPathCenter = ".//div[contains(@class,'x-picker-center')][1]";
        private string mSecondPickerXPathCenter = ".//div[contains(@class,'x-picker-center')][2]";
        private string mThirdPickerXPathCenter = ".//div[contains(@class,'x-picker-center')][3]";

        private string mFirstPickerXPathRight = ".//div[contains(@class,'x-picker-right')][1]";
        private string mSecondPickerXPathRight = ".//div[contains(@class,'x-picker-right')][2]";
        private string mThirdPickerXPathRight = ".//div[contains(@class,'x-picker-right')][3]";
        //private List<IWebElement> mItems;

        private DlkPicker mFirstPickerColumn;
        private DlkPicker mSecondPickerColumn;
        private DlkPicker mThirdPickerColumn;

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
            InitializePickerColumns();
            //mItems = mElement.FindElements(By.XPath(mItemsXPath)).ToList();
        }

        public void InitializePickerColumns()
        {
            /* Check if which xpath search string will work */
            if (mElement.FindElements(By.XPath(mFirstPickerXPathLeft)).Count() > 0) /* Use 'Left' justified */
            {
                mFirstPickerColumn = new DlkPicker("FirstPicker", mElement.FindElement(By.XPath(mFirstPickerXPathLeft)));
                mSecondPickerColumn = new DlkPicker("SecondPicker", mElement.FindElement(By.XPath(mSecondPickerXPathLeft)));
                mThirdPickerColumn = new DlkPicker("ThirdPicker", mElement.FindElement(By.XPath(mThirdPickerXPathLeft)));
            }
            else if (mElement.FindElements(By.XPath(mFirstPickerXPathRight)).Count() > 0) /* Use 'Right' justified */
            {
                mFirstPickerColumn = new DlkPicker("FirstPicker", mElement.FindElement(By.XPath(mFirstPickerXPathRight)));
                mSecondPickerColumn = new DlkPicker("SecondPicker", mElement.FindElement(By.XPath(mSecondPickerXPathRight)));
                mThirdPickerColumn = new DlkPicker("ThirdPicker", mElement.FindElement(By.XPath(mThirdPickerXPathRight)));
            }
            else /* Use 'Center' justified */
            {
                if (mElement.FindElements(By.XPath(mFirstPickerXPathCenter)).Count() == 0)
                {
                    throw new Exception("Cannot find any picker controls.");
                }
                mFirstPickerColumn = new DlkPicker("FirstPicker", mElement.FindElement(By.XPath(mFirstPickerXPathCenter)));
                mSecondPickerColumn = new DlkPicker("SecondPicker", mElement.FindElement(By.XPath(mSecondPickerXPathCenter)));
                mThirdPickerColumn = new DlkPicker("ThirdPicker", mElement.FindElement(By.XPath(mThirdPickerXPathCenter)));
            }
        }

        [Keyword("SetTime", new String[] { "1|text|Value|SampleValue" })]
        public void SetTime(String FirstPickerValue, String SecondPickerValue, String ThirdPickerValue)
        {
            try
            {
                Initialize();

                mFirstPickerColumn.Select(FirstPickerValue);
                mSecondPickerColumn.Select(SecondPickerValue);
                mThirdPickerColumn.Select(ThirdPickerValue);

                //Refresh the selected item control
                Initialize();

                //this.VerifyValues(Month, Day,Year);
                DlkLogger.LogInfo("SetTime() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetTime() failed : " + e.Message, e);
            }

        }

        [Keyword("VerifyValues", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyValues(String FirstPickerValue, String SecondPickerValue, String ThirdPickerValue)
        {
            try
            {
                Initialize();
                mFirstPickerColumn.VerifyValue(FirstPickerValue);
                mSecondPickerColumn.VerifyValue(SecondPickerValue);
                mThirdPickerColumn.VerifyValue(ThirdPickerValue);

                DlkLogger.LogInfo("VerifyValues() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValues() failed : " + e.Message, e);
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



    }
}
