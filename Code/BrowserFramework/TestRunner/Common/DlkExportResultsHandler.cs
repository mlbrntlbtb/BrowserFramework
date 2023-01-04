using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System.IO;
using HTMLBuilder;

namespace TestRunner.Common
{
    public static class DlkExportResultsHandler
    {
        #region  DECLARATIONS
        const string STR_TEST_QUEUE_SUMMARY_HEADER = "Test Queue Summary"; 
        const string STR_TEST_RESULT_HEADER = "Test Result: ";

        const string CSS_DARK = "table-dark";
        const string CSS_SUCCESS = "table-success";
        const string CSS_DANGER = "table-danger";
        const string CSS_WHITE = "table-white";
        const string CSS_GRAY = "table-active";
        const string CSS_TABLE = "align-middle table-bordered";
        const string STYLE_BREAKWORD_MAXWIDTH = "white-space:pre-wrap; word-wrap: break-word; max-width: 25rem !important;";
        const string STYLE_BREAKWORD = "white-space:pre-wrap; word-wrap: break-word;";


        public enum OutputOption
        {
            HTML,
            Excel
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Generates an output file based on the Test Queue Summary
        /// </summary>
        /// <param name="records">List of Execution Queue Records to export</param>
        /// <param name="filepath">Full file path</param>
        /// <param name="outputOption">Output Type: HTML and Excel are the available options so far</param>
        public static void ExportTestQueueSummaryToFile(List<DlkExecutionQueueRecord> records, string filepath, OutputOption outputOption, string fileDisplayType)
        {
            try
            {
                var content = CreateTestQueueSummary(records, fileDisplayType);
                switch (outputOption)
                {
                    case OutputOption.HTML:
                        var html = new HtmlBuilder();
                        html.Body(content);
                        html.Save(filepath, false);
                        break;
                    case OutputOption.Excel:
                        DlkExcelHelper.ExportHTMLTableToExcel(content, filepath, STR_TEST_QUEUE_SUMMARY_HEADER, true, true);
                        break;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                throw ex;
            }
        }



        /// <summary>
        /// Generates an output file based on the Test Result
        /// </summary>
        /// <param name="test"> The test result to export</param>
        /// <param name="filepath">Full file path</param>
        /// /// <param name="outputOption">Output Type: HTML and Excel are the available options so far</param>
        public static void ExportTestResultToFile(DlkTest test, string filepath, OutputOption outputOption)
        {
            try
            {
                var content = CreateTestResult(test);
                switch (outputOption)
                {
                    case OutputOption.HTML:
                        var html = new HtmlBuilder();
                        html.Body(content);
                        html.Save(filepath, false);
                        break;
                    case OutputOption.Excel:
                        DlkExcelHelper.ExportHTMLTableToExcel(content, filepath, $"{STR_TEST_RESULT_HEADER}{test.mTestName}", true, true);
                        break;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                throw ex;
            }            
        }

        /// <summary>
        /// Send Test Queue Summary to email
        /// </summary>
        /// <param name="records">List of Execution Queue Records to email</param>
        /// <param name="recipients">Email address where report to be send</param>
        /// <param name="subject">Email subject</param>
        public static bool ExportTestQueueSummaryToEmail(List<DlkExecutionQueueRecord> records, string recipients, string subject, string fileDisplayType)
        {
            try
            {
                var content = CreateTestQueueSummary(records, fileDisplayType);
                var html = new HtmlBuilder();
                html.Body(content);
                EmailHTMLResult(html.ToEmailMessageBody(html.ConvertToString()), recipients, subject);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Send test result to email
        /// </summary>
        /// <param name="test">Test result to export</param>
        /// <param name="recipients">Email address where report to be send</param>
        /// <param name="subject">Email subject</param>
        public static bool ExportTestResultToEmail(DlkTest test, string recipients, string subject)
        {
            try
            {
                var content = CreateTestResult(test);
                var html = new HtmlBuilder();
                html.Body(content);
                EmailHTMLResult(html.ToEmailMessageBody(html.ConvertToString()), recipients, subject);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Creates an XElement for the Test Queue Summary
        /// </summary>
        /// <param name="records">List of Execution Queue Records to export</param>
        /// <returns></returns>
        private static XElement CreateTestQueueSummary(List<DlkExecutionQueueRecord> records, string fileDisplayType)
        {
            List<XElement> eqRecs = new List<XElement>();
            int i = 1;
            foreach (DlkExecutionQueueRecord eqr in records)
            {
                
                eqRecs.Add(Row(i++, eqr.execute, FileNameBasedOnDisplay(eqr,fileDisplayType), eqr.instance, eqr.description, eqr.environment, eqr.Browser.Name,
                    eqr.keepopen, eqr.executedsteps, eqr.duration, eqr.teststatus));
            }
            XElement  content = HTML.Container(
                HTML.H4(HTML.Class(UC.MB_0), STR_TEST_QUEUE_SUMMARY_HEADER),
                HTML.Table(HTML.Class(CSS_TABLE),
                            HTML.THead(
                                HTML.Class(CSS_DARK),
                                HTML.TR(
                                    HTML.TH(HTML.Class(UC.TEXT_CENTER), "#"),
                                    HTML.TH("Execute"),
                                    HTML.TH(fileDisplayType),
                                    HTML.TH("Instance"),
                                    HTML.TH("Description"),
                                    HTML.TH("Environment"),
                                    HTML.TH("Browser"),
                                    HTML.TH("Keep Open?"),
                                    HTML.TH("Completed"),
                                    HTML.TH("Duration")
                                )
                            ),
                            HTML.TBody(eqRecs))
                );

            return content;
            XElement Row(int number, string execute, string filename, string instance, string desc, string env, string browser, string keepopen, string completed, string duration, string status)
            {
                string rowClass;
                switch (status.ToLower())
                {
                    case "passed":
                        rowClass = CSS_SUCCESS;
                        break;
                    case "failed":
                        rowClass = CSS_DANGER;
                        break;
                    default:
                        rowClass = CSS_WHITE;
                        break;
                }
                return HTML.TR(HTML.Class(rowClass), HTML.TD(number.ToString()),
                    HTML.TD(execute),
                    HTML.TD(HTML.Style(STYLE_BREAKWORD_MAXWIDTH), filename),
                    HTML.TD(instance),
                    HTML.TD(HTML.Style(STYLE_BREAKWORD_MAXWIDTH), desc),
                    HTML.TD(env),
                    HTML.TD(browser),
                    HTML.TD(keepopen),
                    HTML.TD(completed),
                    HTML.TD(duration));
            }

            string FileNameBasedOnDisplay(DlkExecutionQueueRecord record, string displaytype)
            {
                switch (displaytype)
                {
                    case "Full Path":
                        return record.fullpath;
                    case "Test Name":
                        return record.name;
                    default:
                        return record.file;
                }
            }
        }

        /// <summary>
        /// Creates an XElement for the Test Result
        /// </summary>
        /// <param name="test">The test result to export</param>
        /// <returns></returns>
        private static XElement CreateTestResult(DlkTest test)
        {
            List<XElement> teststeps = new List<XElement>();

            //Remove existing setup step for uniformity of output result
            if(test.mTestSteps.Any(x=> x.mStepNumber == 0))
            {
                test.mTestSteps.Remove(test.mTestSteps.Find(x=>x.mStepNumber == 0));   
            }
            string setupPassed = test.mTestSetupLogMessages.FindAll(x => x.mMessageType.ToLower().Contains("exception")).Count > 0 ? "Failed" : "Passed";
            teststeps.Add(Row(0, "SETUP", "", "", "", "", setupPassed));
            
            //Add the rest of test steps
            foreach (DlkTestStepRecord tsr in test.mTestSteps)
            {
                teststeps.Add(Row(tsr.mStepNumber, tsr.mExecute, tsr.mScreen, tsr.mControl, tsr.mKeyword, tsr.mParameterString, tsr.mStepStatus));
            }
            XElement content = HTML.Container(
                HTML.H4(HTML.Class(UC.MB_0), HTML.Style(STYLE_BREAKWORD), $"{STR_TEST_RESULT_HEADER}{test.mTestName}"),
                HTML.Table(HTML.Class(CSS_TABLE),
                                    HTML.THead(
                                        HTML.Class(CSS_DARK),
                                        HTML.TR(
                                            HTML.TH(HTML.Class(UC.TEXT_CENTER), "Step"),
                                            HTML.TH("Execute"),
                                            HTML.TH("Screen"),
                                            HTML.TH("Control"),
                                            HTML.TH("Keyword"),
                                            HTML.TH("Parameters"),
                                            HTML.TH("Status")
                                        )
                                    ),
                                    HTML.TBody(teststeps))
                );
            return content;

            XElement Row(int number, string execute, string screen, string control, string keyword, string parameters, string status)
            {
                string rowClass;
                switch (status.ToLower())
                {
                    case "passed":
                        rowClass = execute == "SETUP" ? CSS_GRAY : CSS_SUCCESS;
                        break;
                    case "failed":
                        rowClass = CSS_DANGER;
                        break;
                    default:
                        rowClass = CSS_WHITE;
                        break;
                }
                return HTML.TR(HTML.Class(rowClass), HTML.TD(number.ToString()),
                    HTML.TD(execute),
                    HTML.TD(screen),
                    HTML.TD(control),
                    HTML.TD(keyword),
                    HTML.TD(HTML.Style(STYLE_BREAKWORD_MAXWIDTH), parameters),
                    HTML.TD(status));
            }
        }

        private static void EmailHTMLResult(string msgBody, string recipient, string subject)
        {
            try
            {
                List<string> _recipient = new List<string>(recipient.Split(';').ToList());
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("AutomationBatch@deltek.com");
                mail.Subject = subject;

                foreach (string addr in _recipient)
                {
                    if (!mail.To.Any(x => x.Address.Equals(addr)))
                    {
                        mail.To.Add(new MailAddress(addr));
                    }
                }

                mail.IsBodyHtml = true;
                mail.Body = msgBody;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = int.Parse(DlkConfigHandler.GetConfigValue("smtpport"));
                smtp.Host = DlkConfigHandler.GetConfigValue("smtphost");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
