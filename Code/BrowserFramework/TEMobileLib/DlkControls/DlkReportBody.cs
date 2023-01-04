using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using CommonLib.DlkSystem;
using CommonLib.DlkControls;

namespace TEMobileLib.DlkControls
{
    [ControlType("ReportBody")]
    public class DlkReportBody : DlkBaseControl
    {
        public DlkReportBody(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkReportBody(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String ExpectedValue)
        {
            try
            {
                //Initialize();
                base.VerifyExists(Convert.ToBoolean(ExpectedValue));
                DlkLogger.LogInfo("VerifyExists passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTitle", new String[] { "1|text|Expected Header|Sample report header" })]
        public void VerifyTitle(String ExpectedTitle)
        {
            try
            {
                Initialize();
                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody/tr[1]")).Count < 1)
                {
                    throw new Exception("Unable to find report title");
                }
                DlkBaseControl header = new DlkBaseControl("ReportHeader", mElement.FindElement(By.XPath(
                    "./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody/tr[1]")));

                // innertext attrib is used by IE
                String innerTextValue = header.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = header.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualHeader = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualHeader))
                {
                    actualHeader = actualHeader.Trim();
                }
                DlkAssert.AssertEqual("VerifyTitle()", ExpectedTitle, actualHeader);
                DlkLogger.LogInfo("VerifyTitle passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifySubTitle", new String[] { "1|text|Expected SubTitle|Sample report subheader" })]
        public void VerifySubTitle(String ExpectedSubTitle)
        {
            try
            {
                Initialize();
                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody/tr[2]")).Count < 1)
                {
                    throw new Exception("Unable to find report subtitle");
                }
                DlkBaseControl subHeader = new DlkBaseControl("ReportSubTitle", mElement.FindElement(By.XPath(
                    "./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody/tr[2]")));

                // innertext attrib is used by IE
                String innerTextValue = subHeader.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = subHeader.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualSubHeader = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualSubHeader))
                {
                    actualSubHeader = actualSubHeader.Trim();
                }
                DlkAssert.AssertEqual("VerifySubTitle()", ExpectedSubTitle, actualSubHeader);
                DlkLogger.LogInfo("VerifySubTitle passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifySubTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyFooterLabel", new String[] { "1|text|Expected footer label|Sample footer label" })]
        public void VerifyFooterLabel(String ExpectedFooterLabel)
        {
            try
            {
                Initialize();
                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[3]/descendant::tbody/tr[2]")).Count < 1)
                {
                    throw new Exception("Unable to find report footer label");
                }
                DlkBaseControl footerLabel = new DlkBaseControl("ReportFooterLabel", mElement.FindElement(By.XPath("./div/table/tbody/tr[3]/descendant::tbody/tr[2]")));

                // innertext attrib is used by IE
                String innerTextValue = footerLabel.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = footerLabel.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualFooterLabel = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualFooterLabel))
                {
                    actualFooterLabel = actualFooterLabel.Trim();
                }
                DlkAssert.AssertEqual("VerifyFooterLabel()", ExpectedFooterLabel, actualFooterLabel);
                DlkLogger.LogInfo("VerifyFooterLabel passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFooterLabel() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyFooterText", new String[] { "1|text|Expected footer text|Sample footer text" })]
        public void VerifyFooterText(String ExpectedFooterText)
        {
            try
            {
                Initialize();
                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[3]/descendant::tbody/tr[2]")).Count < 1)
                {
                    throw new Exception("Unable to find report footer text");
                }
                DlkBaseControl footerText = new DlkBaseControl("ReportFooterText", mElement.FindElement(By.XPath("./div/table/tbody/tr[3]/descendant::tbody/tr[3]")));

                // innertext attrib is used by IE
                String innerTextValue = footerText.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = footerText.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualFooterText = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualFooterText))
                {
                    actualFooterText = actualFooterText.Trim();
                }
                DlkAssert.AssertEqual("VerifyFooterText()", ExpectedFooterText, actualFooterText);
                DlkLogger.LogInfo("VerifyFooterText passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyFooterText() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReportingCompany", new String[] { "1|text|Expected reporting company|Company A" })]
        public void VerifyReportingCompany(String ExpectedReportingCompany)
        {
            try
            {
                Initialize();
                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[1]/descendant::tbody/tr[1]")).Count < 1)
                {
                    throw new Exception("Unable to find reporting company");
                }
                DlkBaseControl reportingCompany = new DlkBaseControl("ReportingCompany", mElement.FindElement(By.XPath(
                    "./div/table/tbody/tr[1]/descendant::tbody/tr[1]")));

                // innertext attrib is used by IE
                String innerTextValue = reportingCompany.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = reportingCompany.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualReportingCompany = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualReportingCompany))
                {
                    actualReportingCompany = actualReportingCompany.Trim();
                }
                DlkAssert.AssertEqual("VerifyReportingCompany", ExpectedReportingCompany, actualReportingCompany);
                DlkLogger.LogInfo("VerifyReportingCompany passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReportingCompany() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTotalPageCount", new String[] { "1|text|Expected page count|1" })]
        public void VerifyTotalPageCount(String ExpectedPageCount)
        {
            try
            {
                Initialize();
                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[1]")).Count < 1)
                {
                    throw new Exception("Unable to find page count");
                }
                DlkBaseControl pageCount = new DlkBaseControl("PageCount", mElement.FindElement(By.XPath(
                    "./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[1]")));

                // innertext attrib is used by IE
                String innerTextValue = pageCount.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = pageCount.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualPageCount = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualPageCount))
                {
                    actualPageCount = actualPageCount.Trim();
                    actualPageCount = actualPageCount.Split(' ').Last();
                }
                DlkAssert.AssertEqual("VerifyPageCount()", ExpectedPageCount, actualPageCount);
                DlkLogger.LogInfo("VerifyPageCount passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyPageCount() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyCurrentPageNumber", new String[] { "1|text|Expected current page number|1" })]
        public void VerifyCurrentPageNumber(String ExpectedCurrentPageNumber)
        {
            try
            {
                Initialize();
                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[1]")).Count < 1)
                {
                    throw new Exception("Unable to find current page number");
                }
                DlkBaseControl currentPage = new DlkBaseControl("CurrentPage", mElement.FindElement(By.XPath(
                    "./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[1]")));

                // innertext attrib is used by IE
                String innerTextValue = currentPage.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = currentPage.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualCurrentPage = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualCurrentPage))
                {
                    actualCurrentPage = actualCurrentPage.Trim();
                    actualCurrentPage = actualCurrentPage.Split(' ')[1];
                }
                DlkAssert.AssertEqual("VerifyCurrentPageNumber()", ExpectedCurrentPageNumber, actualCurrentPage);
                DlkLogger.LogInfo("VerifyCurrentPageNumber passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyCurrentPageNumber() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyDate", new String[] { "1|text|Expected date|1/1/2014", "1|text|Format|MM/DD/YYYY" })]
        public void VerifyDate(String ExpectedDate, String Format)
        {
            try
            {
                Initialize();

                // Format 'Format' :p
                Format = Format.Replace("m", "M").Replace("D", "d").Replace("Y", "y");

                //string expectedShortDate = DateTime.Parse(ExpectedDate).ToShortDateString();
                string expectedShortDate = DateTime.ParseExact(ExpectedDate, Format, null).ToShortDateString();

                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[2]")).Count < 1)
                {
                    throw new Exception("Unable to find date");
                }
                DlkBaseControl date = new DlkBaseControl("ReportDate", mElement.FindElement(By.XPath(
                    "./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[2]")));

                // innertext attrib is used by IE
                String innerTextValue = date.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = date.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualDate = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualDate))
                {
                    actualDate = actualDate.Trim();
                    //actualDate = DateTime.Parse(actualDate).ToShortDateString();
                    actualDate = DateTime.ParseExact(actualDate, Format, null).ToShortDateString();
                }
                DlkAssert.AssertEqual("VerifyDate()", expectedShortDate, actualDate);
                DlkLogger.LogInfo("VerifyDate passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyDate() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTime", new String[] { "1|text|Expected time|6:30 AM" })]
        public void VerifyTime(String ExpectedTime)
        {
            try
            {
                Initialize();

                string expectedShortTime = DateTime.Parse(ExpectedTime).ToShortTimeString();

                // Check if header element is finadable
                if (mElement.FindElements(By.XPath("./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[3]")).Count < 1)
                {
                    throw new Exception("Unable to find time");
                }
                DlkBaseControl time = new DlkBaseControl("ReportDate", mElement.FindElement(By.XPath(
                    "./div/table/tbody/tr[1]/descendant::tbody/tr[2]/descendant::tbody/descendant::tbody[2]/tr[3]")));

                // innertext attrib is used by IE
                String innerTextValue = time.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = time.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String actualTime = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(actualTime))
                {
                    actualTime = actualTime.Trim();
                    actualTime = DateTime.Parse(actualTime).ToShortTimeString();
                }
                DlkAssert.AssertEqual("VerifyTime()", expectedShortTime, actualTime);
                DlkLogger.LogInfo("VerifyTime passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTime() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyColumnCount", new String[] { "1|text|Expected column count|5" })]
        public void VerifyColumnCount(string ExpectedColumnCount)
        {
            try
            {
                Initialize();

                int actualColumnCount = mElement.FindElements(By.XPath("./div/table/tbody/tr[2]/descendant::th/descendant::tr[1]/td")).Count;
                int expectedColumnCount = int.Parse(ExpectedColumnCount);
                DlkLogger.LogInfo("Expected column count = " + expectedColumnCount + " : Actual column count = " + actualColumnCount);

                DlkAssert.AssertEqual("VerifyColumnCount", expectedColumnCount, actualColumnCount);
                DlkLogger.LogInfo("VerifyColumnCount passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyColumnCount() failed : " + e.Message, e);
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

        public void VerirfyColumnHeaders(string ExpectedColumnHeaders)
        {
            string[] expectedHeaders = ExpectedColumnHeaders.Split('~');
            string actualColumnHeaders = string.Empty;

            int actualColumnCount = mElement.FindElements(By.XPath("./div/table/tbody/tr[2]/descendant::tbody/tr[1]/descendant::tbody/tr[1]/td")).Count;
            foreach (IWebElement elm in mElement.FindElements(By.XPath("./div/table/tbody/tr[2]/descendant::tbody/tr[1]/descendant::tbody/tr[1]/td")))
            {
                DlkBaseControl currentColHeader = new DlkBaseControl("CurrentColumnHeader", elm);
                // innertext attrib is used by IE
                String innerTextValue = currentColHeader.GetAttributeValue("innerText");
                // textContent attrib is used by Firefox (what about chrome?)
                String textContent = currentColHeader.GetAttributeValue("textContent");
                // if innerText available use it, otherwise use textContent
                String currentColHeaderText = string.IsNullOrEmpty(innerTextValue) ? textContent : innerTextValue;
                if (!string.IsNullOrEmpty(currentColHeaderText))
                {
                    currentColHeaderText = currentColHeaderText.Trim();
                    actualColumnHeaders += currentColHeaderText + "~";
                }
            }
            actualColumnHeaders.Trim('~');

        }

        public void VerifyRowExistsWithColumnValue(string ColumnHeader, string Value, string ExpectedValue)
        {

        }

        public void VerifyRowExistsWithMultipleColumnValues(string ColumnHeaders, string Values, string ExpectedValue)
        {

        }
    }
}
