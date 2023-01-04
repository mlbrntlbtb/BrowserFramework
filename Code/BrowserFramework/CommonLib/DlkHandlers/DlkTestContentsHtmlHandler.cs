using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Castle.Core.Internal;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    /// <summary>
    /// This test suite contents handler reads the test suite xml file and retrieves the test scripts within it.
    /// It then arranges the information in a html file for sending through email once the suite execution starts.
    /// This email will be sent if the field in the Scheduler Settings Dialog is checked.
    /// </summary>
    public static class DlkTestContentsHtmlHandler
    {
        private const string STR_HTML_STYLE_BODY_CONTENT = "font-size:20px; font-weight:bold; color:blue";
        private const string STR_HTML_STYLE_TABLE = "border:1px dotted black;border-collapse:collapse; font-size:13";

        private const string STR_HTML_STYLE_SUMMARY_TABLE = "width:50%; border:1px dotted black;border-collapse:collapse; font-size:13; color:black;";
        private const string STR_HTML_STYLE_SUMMARY_HEADER = "border:1px dotted black; text-align:left; background-color:lightyellow; color:black;";
        private const string STR_HTML_STYLE_SUMMARY_CELL = "border:1px dotted black; text-align:justify; white-space:pre; color:black;";

        private const string STR_HTML_STYLE_TEST_STEP_HEADER = "border:1px dotted black; text-align:left; background-color:blue; color:white;";
        private const string STR_HTML_STYLE_TEST_DATA_HEADER = "border:1px dotted black; text-align:left; background-color:green; color:white;";
        
        private const string STR_HTML_STYLE_DETAILS_LEFT = "border:1px dotted black; text-align:left; padding:3px; color:black;";
        
        private const string STR_HTML_STYLE_DETAILS_COMMENT = "border:1px dotted black; text-align:left; padding:3px; color:black; background-color:orange";
        private const string STR_HTML_STYLE_DETAILS_COMMENT_FALSE = "border:1px dotted black; text-align:left; padding:3px; color:gray; background-color:orange; font-style:italic";
        private const string STR_HTML_STYLE_DETAILS_HINT = "border:1px dotted black; text-align:left; padding:3px; color:black; background-color:lightgreen";
        private const string STR_HTML_STYLE_DETAILS_HINT_FALSE = "border:1px dotted black; text-align:left; padding:3px; color:gray; background-color:lightgreen; font-style:italic";
        private const string STR_HTML_STYLE_DETAILS_NOTE = "border:1px dotted black; text-align:left; padding:3px; color:black; background-color:yellow";
        private const string STR_HTML_STYLE_DETAILS_NOTE_FALSE = "border:1px dotted black; text-align:left; padding:3px; color:gray; background-color:yellow; font-style:italic";
        private const string STR_HTML_STYLE_DETAILS_STEP = "border:1px dotted black; text-align:left; padding:3px; color:black; background-color:pink";
        private const string STR_HTML_STYLE_DETAILS_STEP_FALSE = "border:1px dotted black; text-align:left; padding:3px; color:gray; background-color:pink; font-style:italic";
        private const string STR_HTML_STYLE_DETAILS_FALSE_EXECUTION = "border:1px dotted black; text-align:left; padding:3px; color:gray; font-style:italic;";
        
        private const string BLOCK_STYLE_DETAILS_DATADRIVEN = "text-align:left; color:green; font-weight:bold;";
        private const string BLOCK_STYLE_DETAILS_OUTPUT = "text-align:left; color:blue; font-weight:bold;";
        private const string BLOCK_STYLE_DETAILS_LEFT = "text-align:left; color:black;";
       
        private const string STR_HTML_TEST_NAME = "Name";
        private const string STR_HTML_DESCRIPTION = "Description";
        private const string STR_HTML_AUTHOR = "Author";
        private const string STR_HTML_NUMBER_OF_STEPS = "No. of Steps";
        private const string STR_HTML_NUMBER_OF_DATA = "No. of Data";

        private const string STR_HTML_STEP = "Step";
        private const string STR_HTML_EXECUTE= "Execute";
        private const string STR_HTML_SCREEN = "Screen";
        private const string STR_HTML_CONTROL = "Control";
        private const string STR_HTML_KEYWORD = "Keyword";
        private const string STR_HTML_PARAMETERS = "Parameters";
        private const string STR_HTML_DELAY = "Delay";

        private const string STR_FILTER_PARTIAL_PREFIX = " out of ";
        private const string STR_FILTER_PARTIAL_SUFFIX = " steps displayed";

        static XElement blankHeader = new XElement("br");
        public static XElement TestName { get; set; }
        public static XElement TestDesc { get; set; }
        public static XElement TestAuthor { get; set; }
        public static int TestStepCount { get; set; }
        public static int InstanceCount { get; set; }
        public static int DataStepCount { get; set; }

        public static string CreateSummaryBody(string inputXML, List<DlkTestStepRecord> filteredSteps = null)
        {
            string body = "";
            try
            {
                string finalTestStepCount = filteredSteps != null ? filteredSteps.Count.ToString() + STR_FILTER_PARTIAL_PREFIX + TestStepCount + STR_FILTER_PARTIAL_SUFFIX : TestStepCount.ToString();
                body = "Test: " + inputXML + "\r\n\r\n" +
                              "Name: " + TestName.Value + "\r\n" +
                              "Description: " + TestDesc.Value + "\r\n" +
                              "Author: " + TestAuthor.Value + "\r\n" +
                              "Number of Steps: " + finalTestStepCount + "\r\n" +
                              "Number of Data: " + DataStepCount + " Data Column; " + InstanceCount + " Data Instance";
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile("Test Runner encountered an unexpected error. See program logs for details.", ex);
            }
            return body;
        }
        

        public static string CreateHTMLReportBody(string inputXML, List<DlkTestStepRecord> filteredSteps = null)
        {
            string testName = Path.GetFileNameWithoutExtension(inputXML);
            string ret = "";
            try
            {
                string fileName = "Test: " + inputXML;
                XElement testNameTable = GetTestDetailsTable(inputXML, filteredSteps);

                XElement testStepTable = GetTestStepTable(inputXML, filteredSteps);
                var stepName = testStepTable.Descendants("tr").Any() ? "Steps" : "";

                XElement testDataTable = GetTestDataTable(inputXML);
                var dataName = testDataTable.Descendants("tr").Any() ? "Data" : "";

                XElement body = new XElement("body", new XAttribute("style", STR_HTML_STYLE_BODY_CONTENT),
                    fileName, blankHeader, blankHeader, testNameTable, blankHeader,
                    stepName, blankHeader, testStepTable, blankHeader,
                    dataName, blankHeader, testDataTable);

                // create html node
                XElement root = new XElement("html", body);
                XDocument output = new XDocument(root);
                ret = Path.Combine(Path.GetDirectoryName(inputXML), testName + ".html");
                output.Save(ret);
            }
            catch(Exception ex)
            {
                DlkLogger.LogToFile("Test Runner encountered an unexpected error. See program logs for details.", ex);
            }
            return ret;
        }
        
        /// <summary>
        /// This creates the test details table.
        /// </summary>
        /// <param name="inputXML"></param>
        /// <returns></returns>
        private static XElement GetTestDetailsTable(string inputXML, List<DlkTestStepRecord> filteredSteps)
        {
            XDocument input = XDocument.Load(inputXML);
            var step = (from itm in input.Descendants("step")
                select new
                {
                    stepid = itm.Attribute("id").Value,
                }).ToList();

            string trdFileName = inputXML.Replace(".xml", ".trd");
            XDocument trdFile = XDocument.Load(trdFileName);
            var datastep = (from doc in trdFile.Descendants("datarecord")
                select new
                {
                    dataval = doc.Elements("datavalue"),
                }).ToList();

            var datarec = datastep.FirstOrDefault();

            // create table rows
            XElement testname = null;
            XElement description = null;
            XElement author = null;
            // Test Name
            TestName = input.Root.Element("name");
            if (TestName != null)
            {
                testname = new XElement("tr",
                    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_TEST_NAME),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), TestName.Value));
            }

            // Description
            TestDesc = input.Root.Element("description");
            if (TestDesc != null)
            {
                 description = new XElement("tr",
                    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_DESCRIPTION),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), TestDesc.Value));
            }
            // Author
            TestAuthor = input.Root.Element("author");
            if (TestAuthor != null)
            {
                author = new XElement("tr",
                    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_AUTHOR),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), TestAuthor.Value));
            }
            // No. of Steps
            TestStepCount = step.Count;
            string FilteredTestStepCount = filteredSteps != null ? filteredSteps.Count.ToString() + STR_FILTER_PARTIAL_PREFIX + TestStepCount + STR_FILTER_PARTIAL_SUFFIX : String.Empty;
            XElement stepCount = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_NUMBER_OF_STEPS),
                filteredSteps != null ? new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), FilteredTestStepCount)
                                        : new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), TestStepCount));

                //No. of Data
            InstanceCount = 0;
            DataStepCount = datastep.Count;
            if (datarec != null)
            {
                InstanceCount = datarec.dataval.Count();
            }

            XElement dataCount = new XElement("tr",
                    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_NUMBER_OF_DATA),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), DataStepCount + " Data Column", blankHeader, InstanceCount + " Data Instance"));
  
                // Table
            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_SUMMARY_TABLE),
                    testname, description, author, stepCount, dataCount);

            return table;
        }

        /// <summary>
        /// This creates the table containing the test step for each test script.
        /// </summary>
        /// <param name="inputXML"></param>
        /// <returns></returns>
        private static XElement GetTestStepTable(string inputXML, List<DlkTestStepRecord> filteredSteps)
        {
            // create table header row
            XElement header = new XElement("tr",
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_STEP_HEADER), STR_HTML_STEP),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_STEP_HEADER), STR_HTML_EXECUTE),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_STEP_HEADER), STR_HTML_SCREEN),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_STEP_HEADER), STR_HTML_CONTROL),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_STEP_HEADER), STR_HTML_KEYWORD),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_STEP_HEADER), STR_HTML_PARAMETERS),
                new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_STEP_HEADER), STR_HTML_DELAY));

            // create table rows
                XDocument input = XDocument.Load(inputXML);
                var step = filteredSteps != null ? from DlkTestStepRecord record in filteredSteps
                                                   select new
                                                   {
                                                       stepid = record.mStepNumber.ToString(),
                                                       execute = record.mExecute,
                                                       screen = record.mScreen,
                                                       control = record.mControl,
                                                       keyword = record.mKeyword,
                                                       parameters = record.mParameterString,
                                                       delay = record.mStepDelay.ToString()
                                                   }
                                                   : from doc in input.Descendants("step")
                                                   select new
                                                   {
                                                       stepid = doc.Attribute("id").Value,
                                                       execute = doc.Element("execute").Value,
                                                       screen = doc.Element("screen").Value,
                                                       control = doc.Element("control").Value,
                                                       keyword = doc.Element("keyword").Value,
                                                       parameters = doc.Element("parameters").Descendants().FirstOrDefault().Value,
                                                       delay = doc.Element("delay").Value
                                                   };

            //format parameters and rows

            List<XElement> rows = new List<XElement>();

            foreach (var data in step)
            {
                var stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT);

                List<XElement> paramElem = new List<XElement>();
                if (data.execute.Equals("True"))
                {
                    if (data.parameters.Contains("|"))
                    {
                        //multiple parameters
                        var paramList = data.parameters.Split(Convert.ToChar("|"));
                        int count = 0;
                        foreach (var par in paramList)
                        {
                            count++;
                            var parameter = count == paramList.Length ? par : par + "|";

                            if (par.Contains("D{"))
                                paramElem.Add(new XElement("block",
                                    new XAttribute("style", BLOCK_STYLE_DETAILS_DATADRIVEN), parameter));
                            else if (par.Contains("O{"))
                                paramElem.Add(new XElement("block", new XAttribute("style", BLOCK_STYLE_DETAILS_OUTPUT),
                                    parameter));
                            else
                                paramElem.Add(new XElement("block", new XAttribute("style", BLOCK_STYLE_DETAILS_LEFT),
                                    parameter));
                        }
                    }
                    else
                    {
                        //single parameter
                        if (data.parameters.Contains("D{"))
                        {
                            paramElem.Add(new XElement("block", new XAttribute("style", BLOCK_STYLE_DETAILS_DATADRIVEN),
                                data.parameters));
                        }
                        else if (data.parameters.Contains("O{"))
                        {
                            paramElem.Add(new XElement("block", new XAttribute("style", BLOCK_STYLE_DETAILS_OUTPUT),
                                data.parameters));
                        }
                        else
                        {
                            paramElem.Add(new XElement("block", new XAttribute("style", BLOCK_STYLE_DETAILS_LEFT),
                                data.parameters));
                        }
                    }

                    if (data.keyword.Equals("LogComment"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_COMMENT);
                    }
                    else if (data.keyword.Equals("Hint"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_HINT);
                    }
                    else if (data.keyword.Equals("Note"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_NOTE);
                    }
                    else if (data.keyword.Equals("ExpectedResult") || data.keyword.Equals("Step"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_STEP);
                    }
                }
                else
                {
                    stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_FALSE_EXECUTION);
                    paramElem.Add(new XElement("block", data.parameters));

                    if (data.keyword.Equals("LogComment"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_COMMENT_FALSE);
                    }
                    else if (data.keyword.Equals("Hint"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_HINT_FALSE);
                    }
                    else if (data.keyword.Equals("Note"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_NOTE_FALSE);
                    }
                    else if (data.keyword.Equals("ExpectedResult") || data.keyword.Equals("Step"))
                    {
                        stepStyle = new XAttribute("style", STR_HTML_STYLE_DETAILS_STEP_FALSE);
                    }
                }
     
                XElement row = new XElement("tr",
                    new XElement("td", stepStyle, data.stepid),
                    new XElement("td", stepStyle, data.execute),
                    new XElement("td", stepStyle, data.screen),
                    new XElement("td", stepStyle, data.control),
                    new XElement("td", stepStyle, data.keyword),
                    new XElement("td", stepStyle, paramElem),
                    new XElement("td", stepStyle, data.delay));
                rows.Add(row);
            }

            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), header, rows);
            return table;
        }


        /// <summary>
        /// This creates the table containing the data driven for each test script.
        /// </summary>
        /// <param name="inputXML"></param>
        /// <returns></returns>
        private static XElement GetTestDataTable(string inputXML)
        {
            string trdFileName = inputXML.Replace(".xml", ".trd");
            XDocument trdFile = XDocument.Load(trdFileName);

            //data driven columns                     
            var dataStep = from dataRec in trdFile.Descendants("datarecord")
                select new
                {
                    name = dataRec.Attribute("name").Value,
                    values = dataRec.Elements("datavalue")
                };

            XElement dataHeader = null;
            List<XElement> rows = new List<XElement>();
            if (dataStep.Any())
            {
                //data header
                List<XElement> headers = new List<XElement>();
                foreach (var dataCol in dataStep)
                {
                    XElement header = new XElement("th", new XAttribute("style", STR_HTML_STYLE_TEST_DATA_HEADER),
                        dataCol.name);
                    headers.Add(header);

                }
                dataHeader = new XElement("tr", headers);

                var datarecord = dataStep.ToList();
                for (int i = 0; i < datarecord.First().values.Count(); i++)
                {
                    rows.Add(new XElement("tr"));
                    foreach (var t in datarecord)
                    {
                        var values = t.values.ToList();
                        rows.Add(new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT),
                            values[i].Value));
                    }
                }
            }
            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), dataHeader, rows);
            return table;
        }

    }
}
