using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkHandlers;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace CPTouchLib.DlkControls
{
    [ControlType("TimesheetLinesList")]
    public class DlkTimesheetLinesList : DlkMobileControl
    {
        private List<DlkMobileControl> mLines = new List<DlkMobileControl>();

        public DlkTimesheetLinesList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTimesheetLinesList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTimesheetLinesList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        [Keyword("SelectLine", new String[] { "1|text|Value|SampleValue" })]
        public void SelectLine(string ChargeCode, string Description, string UDTs)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs);
                if (target == null)
                {
                    throw new Exception("Cannot locate line");
                }
                target.Tap();
                DlkLogger.LogInfo("SelectLine() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectLine() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLineWithHours", new String[] { "1|text|Value|SampleValue" })]
        public void SelectLineWithHours(string ChargeCode, string Description, string UDTs, string Hours)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs, Hours);
                if (target == null)
                {
                    throw new Exception("Cannot locate line");
                }
                target.Tap();
                DlkLogger.LogInfo("SelectLineWithHours() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectLineWithHours() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLineByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SelectLineByIndex(String OneBasedIndex)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mLines.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }

                DlkMobileControl mTarget = mLines[index - 1];
                mTarget.Tap();
                DlkLogger.LogInfo("SelectLineByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectLineByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SetLineHours", new String[] { "1|text|Value|SampleValue" })]
        public void SetLineHours(string ChargeCode, string Description, string UDTs, string HoursToSet)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs);
                if (target == null)
                {
                    throw new Exception("Cannot locate line");
                }
                target.SetHours(HoursToSet);
                DlkLogger.LogInfo("SetLineHours() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetLineHours() failed : " + e.Message, e);
            }
        }

        [Keyword("TapLineHours", new String[] { "1|text|Value|SampleValue" })]
        public void TapLineHours(string ChargeCode, string Description, string UDTs)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs);
                if (target == null)
                {
                    throw new Exception("Cannot locate line");
                }
                target.TapHours();
                DlkLogger.LogInfo("TapLineHours() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("TapLineHours() failed : " + e.Message, e);
            }
        }

        [Keyword("SetLineHoursByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SetLineHoursByIndex(string OneBasedIndex, string HoursToSet)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mLines.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
                var target = new TimesheetLineDetail(mLines[index - 1], index);
                target.SetHours(HoursToSet);
                DlkLogger.LogInfo("SetLineHoursByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetLineHoursByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("TapLineHoursByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void TapLineHoursByIndex(string OneBasedIndex)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mLines.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
                var target = new TimesheetLineDetail(mLines[index - 1], index);
                target.TapHours();
                DlkLogger.LogInfo("TapLineHoursByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("TapLineHoursByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineHours", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyLineHours(string ChargeCode, string Description, string UDTs, string HoursToVerify)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs);
                if (target == null)
                {
                    throw new Exception("Cannot locate line");
                }
                DlkAssert.AssertEqual("VerifyLineHoursByIndex", HoursToVerify, target.Hours);
                DlkLogger.LogInfo("VerifyLineHours() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineHours() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyLineHoursByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyLineHoursByIndex(string OneBasedIndex, string HoursToVerify)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mLines.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }
                var target = new TimesheetLineDetail(mLines[index - 1], index);
                DlkAssert.AssertEqual("VerifyLineHoursByIndex", HoursToVerify, target.Hours);
                DlkLogger.LogInfo("VerifyLineHoursByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineHoursByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLineIndex", new String[] { "1|text|Value|SampleValue" })]
        public void GetLineIndex(string ChargeCode, string Description, string UDTs, string OneBasedIndex, string Variable)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs, null, index);
                if (target == null)
                {
                    throw new Exception("Cannot locate line");
                }

                DlkVariable.SetVariable(Variable, target.ListIndex.ToString());
                DlkLogger.LogInfo("GetLineIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetLineIndex() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyLineCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineCount(string ExpectedCount)
        {
            try
            {
                Initialize();
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                DlkAssert.AssertEqual("VerifyLineCount", expected, mLines.Count);
                DlkLogger.LogInfo("VerifyLineCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyLineCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(string TrueOrFalse)
        {
            try
            {
                VerifyExists(Convert.ToBoolean(TrueOrFalse));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        private void Initialize()
        {
            FindElement();
            mLines = GetLines();
        }

        private List<DlkMobileControl> GetLines()
        {
            List<DlkMobileControl> ret = new List<DlkMobileControl>();
            var items = mElement.FindElements(By.XPath(".//*[contains(@resource-id, 'simplelistitem')]"));
            if (items.Any())
            {
                items.ToList().ForEach(x => ret.Add(new DlkMobileControl("item", x)));
            }
            return ret;
        }
    }

    public class TimesheetLineDetail
    {
        public string ChargeCode { get; set; }
        public string Description { get; set; }
        public string UDTs { get; set; }
        public string Hours { get; set; }
        public int ListIndex { get; set; }

        private DlkMobileControl mParent = null;
        private IWebElement mHoursEditNode = null;
        private IWebElement mInfoContainer = null;

        public TimesheetLineDetail(DlkMobileControl ParentItem, int Index)
        {
            mParent = ParentItem;
            ListIndex = Index;
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                mHoursEditNode = mParent.mElement.FindElements(By.XPath("(.//*[contains(@resource-id,'tsLine')])")).FirstOrDefault();
                mInfoContainer = mParent.mElement.FindElements(By.XPath(".//*[contains(@resource-id,'tsLine')]/../..")).FirstOrDefault();

                if (mInfoContainer != null)
                {
                    if (mHoursEditNode != null)
                    {
                        Hours = mHoursEditNode.Text;
                    }
                    var ccodeNode = mInfoContainer.FindElements(By.XPath("(./*/*)[1]")).FirstOrDefault();
                    if (ccodeNode != null)
                    {
                        ChargeCode = ccodeNode.Text.Trim();
                    }
                    var descNode = mInfoContainer.FindElements(By.XPath("(./*/*)[3]")).FirstOrDefault();
                    if (descNode != null)
                    {
                        Description = descNode.Text;
                    }
                    var udtsNode = mInfoContainer.FindElements(By.XPath("(./*/*)[5]")).FirstOrDefault();
                    if (udtsNode != null)
                    {
                        UDTs = udtsNode.Text;
                    }
                }
            }
            catch
            {

            }
        }

        public void SetHours(string HoursToSet)
        {
            if (mHoursEditNode != null)
            {
                var editControl = new DlkMobileControl("Edit", mHoursEditNode);
                editControl.Tap();

                /* hours picker */
                var hoursPickerOSRecord = DlkDynamicObjectStoreHandler.Instance.GetControlRecord("Main", "HoursPicker");
                var hoursPicker = new DlkHoursPicker(hoursPickerOSRecord.mKey, hoursPickerOSRecord.mSearchMethod,
                    hoursPickerOSRecord.mSearchParameters);
                var hu = new HoursUnit(HoursToSet);
                hoursPicker.Set(hu.PlusMinus, hu.Hours, hu.Fractional);
            }
            else
            {

            }
        }

        public void TapHours()
        {
            if (mHoursEditNode != null)
            {
                var editControl = new DlkMobileControl("Edit", mHoursEditNode);
                editControl.Tap();

            }
            else
            {

            }
        }

        public void Tap()
        {
            mParent.Tap();
        }


        public static TimesheetLineDetail GetLineDetail(List<DlkMobileControl> Lines, string ChargeCode, string Description,
            string UDTs, string Hours = "", int index = 1)
        {
            TimesheetLineDetail ret = null;
            index = index > Lines.Count ? 1 : index;
            List<TimesheetLineDetail> hits = new List<TimesheetLineDetail>();

            for (int i = 0; i < Lines.Count; i++)
            {
                var temp = new TimesheetLineDetail(Lines[i], i + 1);
                bool match = ChargeCode == temp.ChargeCode
                    && Description == temp.Description
                    && UDTs == temp.UDTs
                    && (string.IsNullOrEmpty(Hours) ? true : Hours == temp.Hours);
                if (match)
                {
                    if (index == 1)
                    {
                        ret = temp;
                        return ret;
                    }
                    else
                    {
                        hits.Add(temp);
                    }
                }
            }

            if (hits.Any())
            {
                index = index > hits.Count ? 1 : index - 1;
                if (index > hits.Count)
                {
                    index = 1;
                    DlkLogger.LogWarning("Input index out of range of timesheet line matches... Setting index to 1");
                }
                else
                {
                    index = index - 1;
                }
                return hits[index];
            }
            return ret;
        }
    }
}
