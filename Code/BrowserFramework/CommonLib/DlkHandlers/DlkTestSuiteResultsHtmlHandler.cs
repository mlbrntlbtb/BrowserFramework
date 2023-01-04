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
    public static class DlkTestSuiteResultsHtmlHandler
    {
        private const string STR_HTML_STYLE_TITLE = "border:1px dotted black;border-collapse:collapse; font-family:arial; font-style: bold; display: inline";
        private const string STR_HTML_STYLE_TABLE = "border-collapse:collapse; font-family:arial; font-size:13";
        private const string STR_HTML_STYLE_CANCELLED_TEXT = "color:red; padding:5px";
        private const string STR_HTML_STYLE_SUMMARY_HEADER = "border:1px dotted black; text-align:left";
        private const string STR_HTML_STYLE_SUMMARY_HEADER_INDENT = "border:1px dotted black; text-align:left; padding-left:12px";
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


        private const string STR_HTML_TITLE = "Browser Framework Test Execution Report";
        private const string STR_HTML_TOTAL_TEST_SCRIPTS = "Total Test Scripts";
        private const string STR_HTML_PASSED_TEST_SCRIPTS = "Passed Test Scripts";
        private const string STR_HTML_FAILED_TEST_SCRIPTS = "Failed Test Scripts";
        private const string STR_HTML_NOTRUN_TEST_SCRIPTS = "Not Run Test Scripts";

        private const string STR_HTML_OWNER = "Owner";
        private const string STR_HTML_RUN_NUMBER = "Run #";
        private const string STR_HTML_PASS_RATE = "Pass Rate";
        private const string STR_HTML_COMPLETION_RATE = "Completion Rate";
        private const string STR_HTML_EXECUTION_TIME_FORMAT = "Execution Time (hh:mm:ss.ms)";

        private const string STR_HTML_MACHINE_NAME = "Machine Name";
        private const string STR_HTML_USER_NAME = "User Name";
        private const string STR_HTML_OPERATING_SYS = "Operating System";

        private const string STR_HTML_TEST_RESULT = "Result";
        private const string STR_HTML_FILE_PATH = "Filename";
        private const string STR_HTML_TEST = "Test";
        private const string STR_HTML_INSTANCE = "Instance";
        private const string STR_HTML_BROWSER = "Browser";
        private const string STR_HTML_ENVIRONMENT = "Environment";
        private const string STR_HTML_EXECUTED_STEPS = "Executed steps";
        private const string STR_HTML_ERROR_MESSAGE = "Error Message";
        private const string STR_HTML_ERROR_IMG = "Link to Error Image";
        private const string STR_HTML_EXECUTION_TIME = "Execution Time";

        public static string CreateHTMLReportBody(string inputXML, bool isCancelled)
        {
            string ret = "";
            try
            {
                XDocument input = XDocument.Load(inputXML);
                XElement title = new XElement("h3", new XAttribute("style", STR_HTML_STYLE_TITLE), STR_HTML_TITLE);
                //XElement suitename = GetSuiteName(inputXML);
                XElement suiteNameTable = GetSuiteNameTable(inputXML);
                XElement summaryTable = GetSummaryTable(inputXML);
                XElement customInfoTable = GetCustomInfoTable(inputXML);
                XElement suiteInfoTable = GetSuiteDetailsTable(inputXML);
                XElement blankHeader = new XElement("br");
                XElement detailsTable = GetDetailsTable(inputXML);
                XElement cancelledTable = GetCancelledTable(isCancelled);
                XElement body = new XElement("body", title, blankHeader, suiteNameTable, cancelledTable, suiteInfoTable, blankHeader, summaryTable, blankHeader, customInfoTable, blankHeader, detailsTable);

                // create html node
                XElement root = new XElement("html", body);
                XDocument output = new XDocument(root);
                ret = Path.Combine(Path.GetDirectoryName(inputXML), "summary.html");
                output.Save(ret);
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        public static string CreateHTMLConsolidatedEmailBody(IList<string> inputXMLList)
        {
            string ret = "";
            try
            {
                XElement title = new XElement("h3", new XAttribute("style", STR_HTML_STYLE_TITLE), STR_HTML_TITLE);
                XElement blankHeader = new XElement("br");
                XElement suiteNameTable = GetSuiteNameTable(inputXMLList.FirstOrDefault());
                XElement suiteInfoTable = GetSuiteDetailsTable(inputXMLList.FirstOrDefault());
                XElement consolidatedTable = GetConsolidatedTable(inputXMLList);

                var compiledResults = new List<XElement>();
                foreach (var inputXML in inputXMLList.Reverse())
                {
                    XElement summaryTable = GetSummaryTable(inputXML);
                    XElement detailsTable = GetDetailsTable(inputXML);

                    compiledResults.Add(new XElement("div", blankHeader, summaryTable, blankHeader, detailsTable));
                }
                
                XElement body = new XElement("body", title, blankHeader, suiteNameTable, blankHeader, suiteInfoTable, blankHeader, consolidatedTable, compiledResults);

                // create html node
                XElement root = new XElement("html", body);
                XDocument output = new XDocument(root);
                ret = Path.Combine(Path.GetDirectoryName(inputXMLList.FirstOrDefault()), "consolidatedSummary.html");
                output.Save(ret);
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        private static XElement GetSummaryTable(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);

            var summary = from itm in input.Descendants("summary")
                          select new
                          {
                              total = itm.Attribute("total").Value,
                              notrun = itm.Attribute("notrun").Value,
                              passed = itm.Attribute("passed").Value,
                              failed = itm.Attribute("failed").Value,
                              elapsed = itm.Attribute("elapsed").Value,
                              runnumber = itm.Attribute("runnumber").Value,
                              numberofruns = itm.Attribute("numberofruns").Value
                          };
            
            // run number
            XElement runNumber = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_RUN_NUMBER),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), string.Format("{0}/{1}", summary.First().runnumber, summary.First().numberofruns)));
           
            // total test scripts
            XElement totalTestScripts = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_TOTAL_TEST_SCRIPTS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().total));
       
            // passed test scripts
            XElement passedTestScripts = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER_INDENT), STR_HTML_PASSED_TEST_SCRIPTS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().passed));
           
            // failed test scripts
            XElement failedTestScripts = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER_INDENT), STR_HTML_FAILED_TEST_SCRIPTS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().failed));
            
            // not run test scripts
            XElement notrunTestScripts = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER_INDENT), STR_HTML_NOTRUN_TEST_SCRIPTS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().notrun));
            
            // pass rate
            double rate = summary.First().total ==  summary.First().notrun ? 0 : 
                int.Parse(summary.First().passed) * 100 / (int.Parse(summary.First().total) - int.Parse(summary.First().notrun));
            XElement passRate = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_PASS_RATE),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), rate.ToString() + "%"));

            // completion rate
            double completion = (int.Parse(summary.First().passed) + int.Parse(summary.First().failed)) * 100 / (int.Parse(summary.First().total));
            XElement completionRate = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_COMPLETION_RATE),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), completion.ToString() + "%"));

            // execution time
            XElement executionTime = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_EXECUTION_TIME_FORMAT),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().elapsed));

            // table
            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE),
                runNumber, totalTestScripts, passedTestScripts, failedTestScripts, notrunTestScripts, passRate, completionRate, executionTime);

            return table;
        }

        private static XElement GetCustomInfoTable(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);
            var customInfo = input.Descendants("custominfo");
            List<XElement> AllElements = new List<XElement>();

            foreach (var item in customInfo.Elements())
            {
                string titleName = item.Attribute("display").Value.ToString();
                string elementValue = item.Value.ToString();

                //Add all elements from custom info to the table
                XElement customElement = new XElement("tr",
                    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), titleName),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), elementValue));
                AllElements.Add(customElement);
            }

            // Create table for custom info
            XElement customTable = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), AllElements);
            return customTable;
        }

        private static XElement GetSuiteName(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);

            var title = from itm in input.Descendants("summary")
                        select new
                        {
                            name = itm.Attribute("name").Value
                        };
            return new XElement("h4", title.First().name);
        }

        private static XElement GetCancelledTable(bool isCancelled)
        {
            XElement element;

            if(isCancelled)
            {
                element = new XElement("div",
                    new XAttribute("style", STR_HTML_STYLE_CANCELLED_TEXT),
                    "Cancelled",
                    new XElement("br"));
            }
            else
            {
                element = new XElement("br");
            }

            return element;
        }

        private static XElement GetDetailsTable(string inputXML)
        {
            // create table header row
            XElement header = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), "#"),
                //new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_FILE_PATH),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_INSTANCE),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_BROWSER),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_ENVIRONMENT),                  
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_EXECUTED_STEPS),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_EXECUTION_TIME),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST_RESULT),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_ERROR_MESSAGE),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_ERROR_IMG));
             
            // create table rows
            XDocument input = XDocument.Load(inputXML);
            var test = from itm in input.Descendants("test")
                       select new
                       {
                           file = itm.Element("filepath").Value,
                           name = itm.Element("name").Value,
                           instance = itm.Element("instance").Value,
                           browser = itm.Element("browser").Value,
                           environment = itm.Element("environment").Value,
                           elapsed = itm.Element("elapsed").Value,
                           executed = itm.Element("executedsteps").Value + "/" + itm.Element("totalsteps").Value,
                           stepdesc = string.IsNullOrEmpty(itm.Element("errormessage").Value) ? "" : itm.Element("step_desc").Value,
                           stepinfo = string.IsNullOrEmpty(itm.Element("errormessage").Value) ? "" : itm.Element("step_info").Value,
                           errormessage = string.IsNullOrEmpty(itm.Element("errormessage").Value) ? "-" : itm.Element("errormessage").Value,
                           outputfile = string.IsNullOrEmpty(itm.Element("erroroutputfile").Value) ? "" : itm.Element("erroroutputfile").Value,
                           errorscreenshot = string.IsNullOrEmpty(itm.Element("errorscreenshot").Value) ? "-" : itm.Element("errorscreenshot").Value,
                           status = itm.Element("status").Value
                       };

            List<XElement> rows = new List<XElement>();
            int count = 0;

            foreach (var data in test)
            {
                string statusStyle = STR_HTML_STYLE_DETAILS_STATUS_NOTRUN;
                if (data.status.ToLower() == "passed")
                {
                    statusStyle = STR_HTML_STYLE_DETAILS_STATUS_PASS;
                }
                else if (data.status.ToLower() == "failed")
                {
                    statusStyle = STR_HTML_STYLE_DETAILS_STATUS_FAIL;
                }
                XElement row = new XElement("tr",
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), ++count),
                    //new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), Path.GetFileName(data.file)),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), data.name),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.instance),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.browser),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.environment),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), data.executed),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_RIGHT), data.elapsed),
                    new XElement("td", new XAttribute("style", statusStyle), data.status),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT),
                        new XElement("div", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT_NO_BORDER), data.stepdesc),
                        new XElement("div", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT_NO_BORDER), data.stepinfo),
                        new XElement("div", new XAttribute("style", data.errormessage == "-" ? STR_HTML_STYLE_DETAILS_CENTER_NO_BORDER : STR_HTML_STYLE_DETAILS_LEFT_NO_BORDER), data.errormessage),
                        new XElement("div", new XAttribute("style", data.outputfile == "-" ? STR_HTML_STYLE_DETAILS_CENTER_NO_BORDER : STR_HTML_STYLE_DETAILS_LEFT_NO_BORDER), GetLinkOutputFile(data.outputfile))),
                    new XElement("td",new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), GetLinkImage(data.errorscreenshot)));
                rows.Add(row);
            }

            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE),
                new XElement("thead", header), new XElement("tbody", rows));
            return table;
        }

        private static XElement GetConsolidatedTable(IList<string> inputXMLList)
        {
            if (inputXMLList != null && inputXMLList.Count > 0)
            {
                int count = 1;
                var testResults = new List<IEnumerable<ConsolidatedSummaryModel>>();

                // create table header row
                XElement header = new XElement("tr", new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), "#"),
                                                     new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_TEST));
                foreach (var inputXML in inputXMLList.Reverse())
                {
                    header.Add(new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_RUN_NUMBER + " " + count++));

                    //extract execution results
                    XDocument input = XDocument.Load(inputXML);
                    var test = from itm in input.Descendants("test")
                               select new ConsolidatedSummaryModel()
                               {
                                   Name = itm.Element("name").Value,
                                   ExecutedSteps = itm.Element("executedsteps").Value + "/" + itm.Element("totalsteps").Value,
                                   Status = itm.Element("status").Value
                               };
                    testResults.Add(test);
                }

                // create table rows
                var run1 = testResults.FirstOrDefault();
                var rows = new List<XElement>();
                for (int i = 0; i < run1.Count(); i++)
                {
                    XElement row = new XElement("tr", new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), i + 1),
                                                      new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), run1.ElementAt(i).Name));
                    foreach (var run in testResults)
                    {
                        var test = run.ElementAt(i);
                        string statusStyle = GetStatusStyle(test.Status);
                        var element1 = new XElement("span", new XAttribute("style", statusStyle), test.Status);
                        var element2 = new XElement("span", string.Format("({0})", test.ExecutedSteps));
                        row.Add(new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_CENTER), element1, element2));
                    }
                    rows.Add(row);
                }

                XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), 
                    new XElement("thead", header), new XElement("tbody", rows));
                return table;
            }

            return null;
        }

        private static string GetStatusStyle(string style)
        {
            string statusStyle;
            switch (style.ToLower())
            {
                case "passed":
                    statusStyle = "color:green;";
                    break;
                case "failed":
                    statusStyle = "color:red;";
                    break;
                default:
                    statusStyle = "color:orange;";
                    break;
            }

            return statusStyle;
        }

        /// <summary>
        /// Transform image location to link
        /// </summary>
        /// <param name="imageText">complete location of the screenshot/image</param>
        /// <returns></returns>
        private static String TransformLinkImage(String imageText){
            if (!imageText.Equals("-"))
            {
                int substr = 7 + DlkEnvironment.mDirProduct.Length;
                imageText = imageText.Substring(substr, imageText.Length - substr);
                imageText = "file:///\\" + Environment.MachineName + "\\BrowserFramework" + @"\" + DlkEnvironment.mProductFolder + @"\"+ imageText;
            }
            return imageText;
        }

        /// <summary>
        ///  Returns an XML Element representing the screenshot/image
        /// </summary>
        /// <param name="imageText">complete location of the screenshot/image</param>
        /// <returns></returns>
        private static XElement GetLinkImage(String imageText)
        {
            XElement linkImage;
            if (!imageText.Equals("-"))
            {
                linkImage = new XElement("a", new XAttribute("href", TransformLinkImage(imageText)), "Link");
            }
            else
            {
                linkImage = new XElement("p", imageText);
            }

            return linkImage;
        }

        /// <summary>
        /// Transform output file location to link
        /// </summary>
        /// <param name="outputFileLoc">complete location of the output file</param>
        /// <returns></returns>
        private static String TransformOutputFileLink(String outputFileLoc)
        {
            if (!String.IsNullOrEmpty(outputFileLoc) && !outputFileLoc.Equals("-") && outputFileLoc.Contains("[OUTPUTFILE]"))
            {
                var fileLoc = outputFileLoc.Substring(outputFileLoc.LastIndexOf("[OUTPUTFILE]") + 13);
                outputFileLoc = "file:///\\" + Environment.MachineName + @"\BrowserFramework\" + DlkEnvironment.mProductFolder 
                    + @"\UserTestData\DocDiff\OutputFile\" + Path.GetFileName(fileLoc);
            }
            return outputFileLoc;
        }

        /// <summary>
        ///  Returns an XML Element representing the output file
        /// </summary>
        /// <param name="outputFileLoc">complete location of the output file</param>
        /// <returns></returns>
        private static XElement GetLinkOutputFile(String outputFileLoc)
        {
            XElement linkOutputFile;
            if (!String.IsNullOrEmpty(outputFileLoc) && !outputFileLoc.Equals("-"))
            {
                linkOutputFile = new XElement("a", new XAttribute("href", TransformOutputFileLink(outputFileLoc)), "OutputFile");
            }
            else
            {
                linkOutputFile = new XElement("p", outputFileLoc);
            }

            return linkOutputFile;
        }

        private static XElement GetSuiteDetailsTable(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);

            var suiteDetails = from itm in input.Descendants("summary")
                          select new
                          {
                              machinename = itm.Attribute("machinename").Value,
                              username = itm.Attribute("username").Value,
                              os = itm.Attribute("operatingsystem").Value,
                          };

            // Machine Name
            XElement machinename = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_MACHINE_NAME),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), suiteDetails.First().machinename));

            // User Name (Logged in user)
            XElement username = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_USER_NAME),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), suiteDetails.First().username));

            // Operation System
            XElement os = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_OPERATING_SYS),
                new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), suiteDetails.First().os));

            // Table
            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE),
                machinename, username, os);

            return table;
        }

        private static XElement GetSuiteNameTable(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);

            var suiteMain = from itm in input.Descendants("summary")
                        select new
                        {
                            name = itm.Attribute("name").Value,
                            description = itm.Attribute("description").Value,
                            owner = itm.Attribute("owner").Value
                        };

            // Test Suite Name
            XElement testSuiteName = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), suiteMain.First().name),
                string.IsNullOrEmpty(suiteMain.First().description) ? null : new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), suiteMain.First().description));

            XElement table;

            // Test Suite Owner
            if (!String.IsNullOrWhiteSpace(suiteMain.First().owner))
            {
                XElement testSuiteOwner = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_OWNER),
                string.IsNullOrEmpty(suiteMain.First().owner) ? null : new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), suiteMain.First().owner));

                table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE),
                testSuiteName, testSuiteOwner);
            }
            else
            {
                table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE),
                testSuiteName);
            }

            return table;
        }

        private class ConsolidatedSummaryModel
        {
            public string Name {get; set;}
            public string ExecutedSteps { get; set; }
            public string Status { get; set; }
        }
    }
}
