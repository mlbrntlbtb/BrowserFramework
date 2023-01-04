#define NATIVE_MAPPING

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using CommonLib.DlkHandlers;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace CPTouchLib.DlkControls
{
    [ControlType("ApproveTimesheetsList")]
    public class DlkApproveTimesheetsList : DlkMobileControl
    {
        private const string STR_XPATH_LIST_ITEM_WEBVIEW = ".//*[contains(@id,'ext-simplelistitem')]";
        private const string STR_XPATH_LIST_ITEM = ".//*[contains(@resource-id,'ext-simplelistitem')]";
        private const string STR_XPATH_BADGE_WEBVIEW = "//*[@class='taskDetailHeader']//*[contains(@class,'badge')]";
        private const string STR_XPATH_TITLE_WEBVIEW = "//*[@class='taskDetailHeader']//*[@class='taskDetailTitle']";

        private List<DlkMobileControl> mItems = new List<DlkMobileControl>();

        public DlkApproveTimesheetsList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkApproveTimesheetsList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkApproveTimesheetsList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        [Keyword("Select", new String[] { "1|text|Value|SampleValue" })]
        public void Select(string ChargePeriodLine, string EmployeeLine)
        {
            try
            {
                Initialize();
                var toFind = new TimesheetForApproval(ChargePeriodLine, EmployeeLine);
                foreach (var ln in mItems)
                {
                    if (TimesheetForApproval.AreEqual(new TimesheetForApproval(ln), toFind))
                    {
                        ln.Tap();
                        DlkLogger.LogInfo("Select() successfully executed.");
                        return;
                    }
                }
                throw new Exception("Cannot find target item");
            }
            catch (Exception e)
            {
                throw new Exception("Select() failed : " + e.Message, e);
            }
        }

        [Keyword("SelectByIndex", new String[] { "1|text|Value|SampleValue" })]
        public void SelectByIndex(String OneBasedIndex)
        {
            try
            {
                Initialize();
                int index;
                if (!int.TryParse(OneBasedIndex, out index))
                {
                    throw new Exception("Invalid index: '" + OneBasedIndex + "'");
                }
                if (index < 1 || index > mItems.Count)
                {
                    throw new Exception("Index out of item range: '" + OneBasedIndex + "'");
                }

                DlkMobileControl mTarget = mItems[index - 1];
                mTarget.Tap();
                DlkLogger.LogInfo("SelectByIndex() successfully executed.");
            }
            catch (Exception e)
            {
                throw new Exception("SelectByIndex() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyTitle(string ExpectedTitle)
        {

            try
            {
#if NATIVE_MAPPING
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
#endif
                var actual = DlkEnvironment.AutoDriver.FindElements(By.XPath(STR_XPATH_TITLE_WEBVIEW)).FirstOrDefault();
                if (actual == null)
                {
                    throw new Exception("Cannot find title");
                }
                DlkAssert.AssertEqual("VerifyTitle", ExpectedTitle, actual.Text.Trim());
                DlkLogger.LogInfo("VerifyTitle() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
        }

        [Keyword("VerifyBadge", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyBadge(string ExpectedValue)
        {

            try
            {
#if NATIVE_MAPPING
                DlkEnvironment.mLockContext = false;
                DlkEnvironment.SetContext("WEBVIEW");
#endif
                var actual = DlkEnvironment.AutoDriver.FindElements(By.XPath(STR_XPATH_BADGE_WEBVIEW)).FirstOrDefault();
                if (actual == null)
                {
                    throw new Exception("Cannot find badge");
                }
                DlkAssert.AssertEqual("VerifyBadge", ExpectedValue, actual.Text);
                DlkLogger.LogInfo("VerifyBadge() passed.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyBadge() failed : " + e.Message, e);
            }
#if NATIVE_MAPPING
            finally
            {
                DlkEnvironment.SetContext("NATIVE", true);
            }
#endif
         }

        [Keyword("VerifyItemExists", new String[] { "1|text|Value|SampleValue" })]
        public void VerifyItemExists(string ChargePeriodLine, string EmployeeLine, string TrueOrFalse)
        {
            try
            {
                Initialize();
                bool bExpected;
                bool bActual = false;
                if (!bool.TryParse(TrueOrFalse, out bExpected))
                {
                    throw new Exception("Invalid boolean value: '" + TrueOrFalse + "'");
                }
                var toFind = new TimesheetForApproval(ChargePeriodLine, EmployeeLine);
                foreach (var ln in mItems)
                {
                    if (TimesheetForApproval.AreEqual(new TimesheetForApproval(ln), toFind))
                    {
                        bActual = true;
                        break;
                    }
                }
                DlkAssert.AssertEqual("VerifyItemExists", bExpected, bActual);
                DlkLogger.LogInfo("VerifyItemExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemExists() failed : " + e.Message, e);
            }
        }

        [Keyword("GetItemIndex", new String[] { "1|text|Value|SampleValue" })]
        public void GetItemIndex(string ChargePeriodLine, string EmployeeLine, string Variable)
        {
            try
            {
                Initialize();
                var toFind = new TimesheetForApproval(ChargePeriodLine, EmployeeLine);
                for (int i = 0; i < mItems.Count; i++)
                {
                    if (TimesheetForApproval.AreEqual(new TimesheetForApproval(mItems[i]), toFind))
                    {
                        DlkVariable.SetVariable(Variable, (i + 1).ToString());
                        DlkLogger.LogInfo("GetItemIndex() successfully executed.");
                        return;
                    }
                }
                throw new Exception("Cannot find target item");
            }
            catch (Exception e)
            {
                throw new Exception("GetItemIndex() failed : " + e.Message, e);
            }
        }


        [Keyword("VerifyItemCount", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyItemCount(string ExpectedCount)
        {
            try
            {
                Initialize();
                int expected;
                if (!int.TryParse(ExpectedCount, out expected))
                {
                    throw new Exception("Invalid ExpectedCount: '" + ExpectedCount + "'");
                }
                DlkAssert.AssertEqual("VerifyItemCount", expected, mItems.Count);
                DlkLogger.LogInfo("VerifyItemCount() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyItemCount() failed : " + e.Message, e);
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
            mItems = GetItems();
        }

        private List<DlkMobileControl> GetItems()
        {
            List<DlkMobileControl> ret = new List<DlkMobileControl>();
#if NATIVE_MAPPING
            var items = mElement.FindElements(By.XPath(STR_XPATH_LIST_ITEM));
#else
            var items = mElement.FindElements(By.XPath(STR_XPATH_LIST_ITEM_WEBVIEW));
#endif
            if (items.Any())
            {
                items.ToList().ForEach(x => ret.Add(new DlkMobileControl("item", x)));
            }
            return ret;
        }
    }

    public class TimesheetForApproval
    {
        private DlkMobileControl mListItem = null;
        private List<IWebElement> mLines = new List<IWebElement>();
        public string ChargePeriodLine { get; set; }
        public string EmployeeLine { get; set; }

        public TimesheetForApproval(DlkMobileControl container)
        {
            mListItem = container;
#if NATIVE_MAPPING
            mLines = container.mElement.FindElements(By.XPath(".//*[string-length(@resource-id)=0]")).ToList();
#else
            mLines = container.mElement.FindElements(By.XPath(".//span")).ToList();
#endif
            ChargePeriodLine = GetChargePeriodLine();
            EmployeeLine = GetEmployeeLine();
        }

        public TimesheetForApproval(string chargePeriodLine, string employeeLine)
        {
            ChargePeriodLine = chargePeriodLine;
            EmployeeLine = employeeLine;
        }

        public static bool AreEqual(TimesheetForApproval item1, TimesheetForApproval item2)
        {
            return item1.ChargePeriodLine.Equals(item2.ChargePeriodLine) 
                && item1.EmployeeLine.Equals(item2.EmployeeLine);
        }

        private string GetChargePeriodLine()
        {
            string ret = string.Empty;
            if (mLines.Any())
            {
                ret = mLines.First().Text;
            }
            return ret;
        }

        private string GetEmployeeLine()
        {
            string ret = string.Empty;
            if (mLines.Any())
            {
                ret = mLines.Last().Text;
            }
            return ret;
        }
    }
}
