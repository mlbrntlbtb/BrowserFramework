using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TestIT.Sys
{
    public class ReportBuilder
    {

        // Styling

        private const string STR_HTML_STYLE_TITLE = "border:1px dotted black;border-collapse:collapse; font-style: bold; display: inline";
        private const string STR_HTML_STYLE_TABLE = "border:1px dotted black;border-collapse:collapse; font-size:13";
        private const string STR_HTML_STYLE_CANCELLED_TEXT = "color:red; padding:5px";
        private const string STR_HTML_STYLE_SUMMARY_HEADER = "border:1px dotted black; text-align:left";
        private const string STR_HTML_STYLE_SUMMARY_CELL = "border:1px dotted black; text-align:right";
        private const string STR_HTML_STYLE_DETAILS_HEADER = "border:1px dotted black";
        private const string STR_HTML_STYLE_DETAILS_STATUS_PASS = "border:1px dotted black; text-align:center; color:green; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_STATUS_FAIL = "border:1px dotted black; text-align:center; color:red; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_STATUS_NOTRUN = "border:1px dotted black; text-align:center; color:orange; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_LEFT = "border:1px dotted black; text-align:left; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_LEFT_NO_BORDER = "text-align:left; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_RIGHT = "border:1px dotted black; text-align:right; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_CENTER = "border:1px dotted black; text-align:center; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_CENTER_NO_BORDER = "text-align:center; padding:5px";

        //Run Summary Table Headers

        private const string STR_HTML_TITLE = "TestIT Results Summary";
        private const string STR_HTML_TESTRUN = "Test Run";
        private const string STR_HTML_PASSED_TESTS = "Passed Tests";
        private const string STR_HTML_FAILED_TESTS = "Failed Tests";
        private const string STR_HTML_TOTAL_TESTS = "Total Tests";
        private const string STR_HTML_FAILED_BY_CONTROL = "Tests failed by controls";
        private const string STR_HTML_FAILED_BY_KEYWORD = "Tests failed by keywords";
        private const string STR_HTML_SUCCESS_RATE = "Success Rate";
        private const string STR_HTML_ELAPSED = "Elapsed (hh:mm:ss)";

        //Run Details Table Headers

        private const string STR_HTML_TEST = "Test";
        private const string STR_HTML_EXECUTION_TIME = "Execution Time";
        private const string STR_HTML_TEST_RESULT = "Result";
        private const string STR_HTML_STEP_NUMBER = "Step";
        private const string STR_HTML_STEP_SCREEN = "Screen";
        private const string STR_HTML_STEP_CONTROL = "Control";
        private const string STR_HTML_STEP_KEYWORD = "Keyword";
        private const string STR_HTML_INSTANCE = "Instance";
        private const string STR_HTML_ERROR_MESSAGE = "Error Message";
        private const string STR_HTML_ERROR_IMG = "Link to Error Image";
        
        public String inputXML(String Product, String FileName)
        {
            String xml = "";
            try
            {

            }
            catch
            {
                //do nothing
            }

            return xml;
        }

        public string CreateHTMLReportBody(TestRun Run, string FileName)
        {
            string ret = "";
            string product = DlkEnvironment.mDirProduct.Split('\\').ElementAt(4);
            try
            {
                XElement title = new XElement("h3", new XAttribute("style", STR_HTML_STYLE_TITLE), STR_HTML_TITLE + ": " + product);
                XElement blankHeader = new XElement("br");
                XElement suiteInfoTable = GetSummaryDetailsTable(Run);
                XElement detailsTable = GetRunDetailsTable(Run);
                XElement body = new XElement("body", title, blankHeader, suiteInfoTable, blankHeader, blankHeader, detailsTable);

                // create html node
                XElement root = new XElement("html", body);
                XDocument output = new XDocument(root);
                
                ret = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);
                output.Save(ret);
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        private static XElement GetRunDetailsTable(TestRun run)
        {
            // create table header row
            XElement header = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), "#"),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_EXECUTION_TIME),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST_RESULT),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_STEP_NUMBER),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_STEP_SCREEN),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_STEP_CONTROL),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_STEP_KEYWORD),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_ERROR_MESSAGE));

            List<XElement> rows = new List<XElement>();
            int count = 0;

            foreach (KeyValuePair<int, TestScript> kvp in run.TestManifest)
            {
                string statusStyle = STR_HTML_STYLE_DETAILS_STATUS_NOTRUN;
                string result = string.Empty;
                if (kvp.Value.Result)
                {
                    statusStyle = STR_HTML_STYLE_DETAILS_STATUS_PASS;
                    result = "Passed";
                }
                else
                {
                    statusStyle = STR_HTML_STYLE_DETAILS_STATUS_FAIL;
                    result = "Failed";
                }
                string strElapsed = kvp.Value.Elapsed.ToString().Substring(0, kvp.Value.Elapsed.ToString().LastIndexOf('.'));

                XElement row = new XElement("tr",
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), ++count),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), kvp.Value.Name),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_RIGHT), strElapsed),
                    new XElement("td", new XAttribute("style", statusStyle), result),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), kvp.Value.StepNumber),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), kvp.Value.Screen),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), kvp.Value.Control),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), kvp.Value.Keyword),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT),
                    new XElement("div", new XAttribute("style", kvp.Value.Error == "-" ? STR_HTML_STYLE_DETAILS_CENTER_NO_BORDER : STR_HTML_STYLE_DETAILS_LEFT_NO_BORDER), kvp.Value.Error)));
                rows.Add(row);
            }

            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), header, rows);
            return table;
        }

        private static XElement GetSummaryDetailsTable(TestRun run)
        {
            // Run Name
            //XElement runname = new XElement("tr",
            //    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_TESTRUN),
            //    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), run.Name));

            // Passed
            XElement passedtests = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_PASSED_TESTS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), run.Test_Passed.ToString()));

            // Failed
            XElement failedtests = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_FAILED_TESTS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), run.Test_Failed.ToString()));


            // Total
            XElement total = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_TOTAL_TESTS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), run.Test_Total.ToString()));

            // Tests failed by controls
            double ctrFailRate = run.Test_Failed_Control * 100 / run.Test_Failed;
            XElement failedbycontrols = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_FAILED_BY_CONTROL),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), run.Test_Failed_Control.ToString() + " (" + ctrFailRate.ToString() + "%)"));

            //Tests failed by keywords
            double kwFailRate = run.Test_Failed_Keyword * 100 / run.Test_Failed;
            XElement failedbykeywords = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_FAILED_BY_KEYWORD),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), run.Test_Failed_Keyword.ToString() + " (" + kwFailRate.ToString() + "%)"));

            // Success Rate
            double rate = run.Test_Passed * 100 / run.Test_Total;
            XElement successRate = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_SUCCESS_RATE),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), rate.ToString() + "%"));

            // Elapsed
            string strElapsed = run.Elapsed.ToString().Substring(0, run.Elapsed.ToString().LastIndexOf('.'));
            XElement elapsed = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_ELAPSED),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), strElapsed));

            // Table
            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), passedtests,
                failedtests, total, failedbycontrols, failedbykeywords, successRate, elapsed);

            return table;
        }
    }
}
