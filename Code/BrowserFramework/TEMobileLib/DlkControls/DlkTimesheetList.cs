using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMobileLib.DlkControls
{
    [ControlType("TimesheetLinesList")]
    public class DlkTimesheetList : DlkBaseControl
    {
        private List<DlkBaseControl> mLines = new List<DlkBaseControl>();

        public DlkTimesheetList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkTimesheetList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkTimesheetList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLine", new String[] {"1|text|Charge Code|SampleValue",
                                                "2|text|Description|SampleValue",
                                                "3|text|UDTs|SampleValue"})]
        public void SelectLine(String ChargeCode, String Description, String UDTs)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs);
                if (target == null)
                {
                    throw new Exception("Cannot locate Subtask");
                }
                target.Click();
                DlkLogger.LogInfo("SelectSubtask() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectSubtask() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLineWithHours", new String[] {"1|text|Charge Code|SampleValue",
                                                  "2|text|Description|SampleValue",
                                                  "3|text|UDTs|SampleValue",
                                                  "4|text|HoursToSet|SampleValue"})]
        public void SelectLineWithHours(String ChargeCode, String Description, String UDTs, String Hours)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs, Hours);
                if (target == null)
                {
                    throw new Exception("Cannot locate Subtask");
                }
                target.Click();
                DlkLogger.LogInfo("SelectSubtaskWithHours() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectSubtaskWithHours() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectLineByIndex", new String[] { "1|text|OneBasedIndex|SampleValue" })]
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

                DlkBaseControl mTarget = mLines[index - 1];
                mTarget.Click();
                DlkLogger.LogInfo("SelectSubtaskByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectSubtaskByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("SetLineHours", new String[] {"1|text|Charge Code|SampleValue",
                                                  "2|text|Description|SampleValue",
                                                  "3|text|UDTs|SampleValue",
                                                  "4|text|HoursToSet|SampleValue"})]
        public void SetLineHours(String ChargeCode, String Description, String UDTs, String HoursToSet)
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

        [Keyword("SetLineHoursByIndex", new String[] {"1|text|Charge Code|SampleValue",
                                                  "2|text|Description|SampleValue"})]
        public void SetLineHoursByIndex(String OneBasedIndex, string HoursToSet)
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
                DlkLogger.LogInfo("SetSubtaskHoursByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SetSubtaskHoursByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("GetLineIndex", new String[] {"1|text|Charge Code|SampleValue",
                                                  "2|text|Description|SampleValue",
                                                  "3|text|UDTs|SampleValue",
                                                  "4|text|Variable|SampleValue"})]
        public void GetLineIndex(String ChargeCode, String Description, String UDTs, string Variable)
        {
            try
            {
                Initialize();
                var target = TimesheetLineDetail.GetLineDetail(mLines, ChargeCode, Description, UDTs);
                if (target == null)
                {
                    throw new Exception("Cannot locate Subtask");
                }

                DlkVariable.SetVariable(Variable, target.ListIndex.ToString());
                DlkLogger.LogInfo("GetSubtaskIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("GetSubtaskIndex() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyLineCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyLineCount(String ExpectedCount)
        {
            try
            {
                Initialize();
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                DlkAssert.AssertEqual("VerifySubtaskCount", expected, mLines.Count);
                DlkLogger.LogInfo("VerifySubtaskCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySubtaskCount() failed : " + e.Message, e);
            }
        }

        [Keyword("GetExists", new String[] { "1|text|VariableName|MyVar" })]
        public void GetExists(string sVariableName)
        {
            try
            {
                string sControlExists = Exists(3).ToString();
                DlkVariable.SetVariable(sVariableName, sControlExists);
                DlkLogger.LogInfo("Successfully executed GetExists(). Value : " + sControlExists);
            }
            catch (Exception e)
            {
                throw new Exception("GetExists() failed : " + e.Message, e);
            }
        }

        private void Initialize()
        {
            FindElement();
            mLines = GetLines();
        }

        private List<DlkBaseControl> GetLines()
        {
            List<DlkBaseControl> ret = new List<DlkBaseControl>();
            var items = mElement.FindElements(By.XPath(".//*[@class='tsRow']"));
            if (items.Any())
            {
                items.ToList().ForEach(x => ret.Add(new DlkBaseControl("item", x)));
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

        private DlkBaseControl mParent = null;
        private IWebElement mHoursEditNode = null;
        private IWebElement mInfoContainer = null;

        public TimesheetLineDetail(DlkBaseControl ParentItem, int Index)
        {
            mParent = ParentItem;
            ListIndex = Index;
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                mHoursEditNode = mParent.mElement.FindElements(By.XPath("(.//*[contains(@class,'tsRowHours')])")).FirstOrDefault();
                mInfoContainer = mParent.mElement.FindElements(By.XPath(".//*[@class='tsRowInfo']")).FirstOrDefault();

                if (mInfoContainer != null)
                {
                    if (mHoursEditNode != null)
                    {
                        Hours = mHoursEditNode.Text;
                    }
                    var ccodeNode = mInfoContainer.FindElements(By.XPath(".//*[@class='tsRowLine1']")).FirstOrDefault();
                    if (ccodeNode != null)
                    {
                        ChargeCode = ccodeNode.Text.Trim();
                    }
                    var descNode = mInfoContainer.FindElements(By.XPath(".//*[@class='tsRowLine2']")).FirstOrDefault();
                    if (descNode != null)
                    {
                        Description = descNode.Text;
                    }
                    var udtsNode = mInfoContainer.FindElements(By.XPath(".//*[@class='tsRowLine3']")).FirstOrDefault();
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

        public static TimesheetLineDetail GetLineDetail(List<DlkBaseControl> Lines, string ChargeCode, string Description,
            string UDTs, string Hours = "", int index = 1)
        {
           TimesheetLineDetail ret = null;
            index = index > Lines.Count ? 1 : index;
            List<TimesheetLineDetail> hits = new List<TimesheetLineDetail>();

            if (Lines.Count == 0) throw new Exception("No timesheet lines were found.");

            for (int i = 0; i < Lines.Count; i++)
            {
                var temp = new TimesheetLineDetail(Lines[i], i + 1);
                bool match = ChargeCode == temp.ChargeCode
                    && Description == temp.Description
                    && (string.IsNullOrEmpty(UDTs) ? true : UDTs == temp.UDTs)
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

        public void SetHours(string HoursToSet)
        {
            if (mHoursEditNode != null)
            {
                var test = new DlkTextBox("Subtask hours", mHoursEditNode);
                test.Set(HoursToSet);
            }
            else
            {
                throw new Exception("Unable to set hours on timesheet line.");
            }
        }

        public void Click()
        {
            mParent.Click();
        }
    }
}
