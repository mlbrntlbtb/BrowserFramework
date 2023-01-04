using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using CommonLib.DlkUtility;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;

namespace CommonLib.DlkHandlers
{
    /// <summary>
    /// This test suite contents handler reads the test suite xml file and retrieves the test scripts within it.
    /// It then arranges the information in a html file for sending through email once the suite execution starts.
    /// This email will be sent if the field in the Scheduler Settings Dialog is checked.
    /// </summary>
    public static class DlkTestSuiteContentsHtmlHandler
    {
        private const string STR_HTML_STYLE_TABLE = "border:1px dotted black;border-collapse:collapse; font-size:13";
        private const string STR_HTML_STYLE_DETAILS_HEADER = "border:1px dotted black";
        private const string STR_HTML_STYLE_DETAILS_LEFT = "border:1px dotted black; text-align:left; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_RIGHT = "border:1px dotted black; text-align:right; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_CENTER = "border:1px dotted black; text-align:center; padding:5px";
        private const string STR_HTML_STYLE_SUMMARY_HEADER = "border:1px dotted black; text-align:left";
        private const string STR_HTML_STYLE_SUMMARY_CELL = "border:1px dotted black; text-align:right";

        private const string STR_HTML_TESTSUITE_NAME = "Name";
        private const string STR_HTML_MACHINE_NAME = "Machine Name";
        private const string STR_HTML_START_TIME = "Time Started";
        private const string STR_HTML_TEST = "Test";
        private const string STR_HTML_INSTANCE = "Instance";
        private const string STR_HTML_BROWSER = "Browser";
        private const string STR_HTML_ENVIRONMENT = "Environment";

        public static string CreateHTMLEmailNotificationBody(string inputXML)
        {
            string ret = "";
            try
            {
                XDocument input = XDocument.Load(inputXML);
                XElement suiteNameTable = GetSuiteNameTable(inputXML);
                XElement detailsTable = GetDetailsTable(inputXML);
                XElement blankHeader = new XElement("br");
                XElement body = new XElement("body", suiteNameTable, blankHeader, detailsTable);

                // create html node
                XElement root = new XElement("html", body);
                XDocument output = new XDocument(root);
                ret = Path.Combine(Path.GetDirectoryName(inputXML), "TestSuiteNotification.html");
                output.Save(ret);
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        /// <summary>
        /// This creates the test suite details table.
        /// </summary>
        /// <param name="inputXML"></param>
        /// <returns></returns>
        private static XElement GetSuiteNameTable(string inputXML)
        {
            string testSuiteName = Path.GetFileNameWithoutExtension(inputXML);

            // Test Suite Name
            XElement testsuitename = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_TESTSUITE_NAME),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), testSuiteName));

            // Machine Name
            XElement machinename = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_MACHINE_NAME),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), Environment.MachineName));

            // Time started
            XElement starttime = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_START_TIME),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), DateTime.Now.ToShortTimeString()));
            // Table
            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE),
                testsuitename, machinename, starttime);

            return table;
        }

        /// <summary>
        /// This creates the main details table containing the information for each test script.
        /// </summary>
        /// <param name="inputXML"></param>
        /// <returns></returns>
        private static XElement GetDetailsTable(string inputXML)
        {
            // create table header row
            XElement header = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), "#"),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_INSTANCE),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_BROWSER),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_ENVIRONMENT));
             
            // create table rows
            XDocument input = XDocument.Load(inputXML);
            var test = from itm in input.Descendants("test")
                       select new
                       {
                           test = itm.Attribute("file").Value,
                           instance = itm.Attribute("testid").Value,
                           browser = itm.Attribute("browser").Value,
                           environment = itm.Attribute("environment").Value
                       };

            List<XElement> rows = new List<XElement>();
            int count = 0;

            foreach (var data in test)
            {
                XElement row = new XElement("tr",
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), ++count),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), data.test),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.instance),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.browser),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.environment));
                rows.Add(row);
            }

            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), header, rows);
            return table;
        }
    }
}
