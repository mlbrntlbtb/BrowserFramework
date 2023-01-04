using System;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using StormWebLib.System;
using System.Linq;
using CommonLib.DlkUtility;

namespace StormWebLib.DlkControls
{
    [ControlType("ReportViewer")]
    public class DlkReportViewer : DlkBaseControl
    {

    #region Constructors
        public DlkReportViewer(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkReportViewer(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkReportViewer(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkReportViewer(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
    #endregion

    #region Constants

        private const String ReportContainer = "//*[contains(@id,'sqlrsReportViewer_ReportViewer')]";
        private const String ReportTableCont = ".//*[contains(@id,'VisibleReportContent')]";
        private const String ReportTable = ".//*[contains(@id,'VisibleReportContent')]//table";
        private const String ReportError = ".//*[contains(@id,'NonReportContent')]";
        private const String ReportToolbar = ".//*[contains(@class,'ToolBarButtonsCell')]";
        private const String ReportFindText = "//input[contains(@title,'Find Text')]";
        private const String ReportFindLink = "//a[@title='Find']";

    #endregion

    #region Private Variables

        private String reportTitle = "//tr[2]//td[2]//*[contains(@aria-label,'Report text')]";
        private String reportDate = "//tr[2]//td[4]//*[contains(@aria-label,'Report text')]";
        private String reportPage = "//tr[contains(@valign,'top')]//td//*[contains(text(),'Page')]";
        private String reportHighlight = "//*[@class='searchHighlighting']";
        private String reportHeader = "//table//table//table//table//table//tr[2]";
        private String reportHeaderStyle = "//style[@id='sqlrsReportViewer_ctl14_ReportControl_styles']";

    #endregion

    #region Public Methods

        public void Initialize()
        {
            DlkStormWebFunctionHandler.WaitScreenGetsReady();
            FindElement();
            this.ScrollIntoViewUsingJavaScript();
        }

    #endregion

    #region Keywords

        [Keyword("VerifyTableExists")]
        public void VerifyTableExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                Boolean ExpectedResult = Convert.ToBoolean(TrueOrFalse);
                Boolean ActualResult = false;

                if (mElement.FindElements(By.XPath(ReportTable)).Count > 0 &&
                    !mElement.FindElement(By.XPath(ReportTable)).GetAttribute("style").Contains("none"))
                {
                    ActualResult = true;
                }

                DlkAssert.AssertEqual("VerifyTableExists() : " + mControlName, ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyTableExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTableExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyErrorExists")]
        public void VerifyErrorExists(String TrueOrFalse)
        {
            try
            {
                Initialize();
                Boolean ExpectedResult = Convert.ToBoolean(TrueOrFalse);
                Boolean ActualResult = false;

                if (mElement.FindElements(By.XPath(ReportError)).Count > 0 &&
                    !mElement.FindElement(By.XPath(ReportError)).GetAttribute("style").Contains("none"))
                {
                    ActualResult = true;
                }

                DlkAssert.AssertEqual("VerifyErrorExists() : " + mControlName, ExpectedResult, ActualResult);
                DlkLogger.LogInfo("VerifyErrorExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyErrorExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReportTitle")]
        public void VerifyReportTitle(String ExpectedTitle)
        {
            try
            {
                String ActualTitle = "";
                Initialize();
                if (mElement.FindElements(By.XPath(ReportTable + reportTitle)).Count > 0)
                {
                    ActualTitle = mElement.FindElement(By.XPath(ReportTable + reportTitle)).Text;
                }
                else
                {
                    throw new Exception("Report title not found.");
                }

                DlkAssert.AssertEqual("VerifyReportTitle() : ", ExpectedTitle.ToLower(), ActualTitle.ToLower());
                DlkLogger.LogInfo("VerifyReportTitle() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReportTitle() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReportDate")]
        public void VerifyReportDate(String ExpectedDate)
        {
            try
            {
                String ActualDate = "";
                Initialize();
                if (mElement.FindElements(By.XPath(ReportTable + reportDate)).Count > 0)
                {
                    ActualDate = mElement.FindElement(By.XPath(ReportTable + reportDate)).Text;
                }
                else
                {
                    throw new Exception("Report date not found.");
                }

                DlkAssert.AssertEqual("VerifyReportDate() : ", ExpectedDate.ToLower(), ActualDate.ToLower());
                DlkLogger.LogInfo("VerifyReportDate() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReportDate() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReportPage")]
        public void VerifyReportPage(String ExpectedPage)
        {
            try
            {
                int expectedPage = 0;
                int ActualPage = 0;
                if (!int.TryParse(ExpectedPage, out expectedPage)) throw new Exception("Page must be a valid number.");

                Initialize();
                if (mElement.FindElements(By.XPath(ReportTable + reportPage)).Count > 0)
                {
                    String [] PageText = mElement.FindElement(By.XPath(ReportTable + reportPage)).Text.Split(' ');
                    ActualPage = Convert.ToInt32(PageText[1]);
                }
                else
                {
                    throw new Exception("Report page not found.");
                }

                DlkAssert.AssertEqual("VerifyReportPage() : ", ActualPage, expectedPage);
                DlkLogger.LogInfo("VerifyReportPage() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReportPage() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReportTextFound")]
        public void VerifyReportTextFound(String TrueOrFalse)
        {
            try
            {
                Boolean ExpectedResult;
                Boolean ActualResult = false;

                if (!Boolean.TryParse(TrueOrFalse, out ExpectedResult)) throw new Exception("Parameter must be true or false.");
                Initialize();

                var findLink = mElement.FindElement(By.XPath(ReportFindLink));
                Boolean isTextExist = findLink.GetAttribute("class").Contains("DisabledLink") ? false : true;

                if (isTextExist)
                    ActualResult = mElement.FindElements(By.XPath(ReportTable + reportHighlight)).Count > 0 ? true : false;
                else
                    throw new Exception("No text found on Find Text box.");

                DlkAssert.AssertEqual("VerifyReportTextFound() : ", ActualResult, ExpectedResult);
                DlkLogger.LogInfo("VerifyReportTextFound() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReportTextFound() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReportRowColor")]
        public void VerifyReportRowColor(String HexRowColor)
        {
            try
            {
                String noColorRow = "ffffff";
                String RowXPath;

                if (HexRowColor.ToLower() != "none" && !String.IsNullOrEmpty(HexRowColor))
                    RowXPath = "//tr//td[contains(@style,'background-color:#" + HexRowColor.ToUpper() + "')]";
                else
                    RowXPath = "//tr//td[contains(@style,'background-color:#" + noColorRow.ToUpper() + "')]";

                Initialize();
                Boolean rowColorExist = mElement.FindElements(By.XPath(ReportTable + RowXPath)).Count > 0 ? true : false;

                if (rowColorExist)
                    DlkLogger.LogInfo("VerifyReportRowColor() passed");
                else
                    throw new Exception("Row color does not match.");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReportRowColor() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReportHeaderColor")]
        public void VerifyReportHeaderColor(String HexHeaderColor)
        {
            try
            {
                Boolean headColorExist = false;
                String noColorHeader = "ffffff";

                Initialize();

                if (mElement.FindElements(By.XPath(ReportTableCont + reportHeader)).Count > 0)
                {
                    var header = mElement.FindElement(By.XPath(reportHeaderStyle));
                    
                    if (HexHeaderColor.ToLower() != "none" && !String.IsNullOrEmpty(HexHeaderColor))
                        headColorExist = header.GetAttribute("textContent").ToLower().Contains(HexHeaderColor.ToLower()) ? true : false;
                    else
                        headColorExist = header.GetAttribute("textContent").ToLower().Contains(noColorHeader) ? true : false;
                }
                else
                    throw new Exception("Row header not found.");

                if (headColorExist)
                    DlkLogger.LogInfo("VerifyReportHeaderColor() passed");
                else
                    throw new Exception("Header color does not match.");

            }
            catch (Exception e)
            {
                throw new Exception("VerifyReportHeaderColor() failed : " + e.Message, e);
            }
        }
    #endregion

    }
}
