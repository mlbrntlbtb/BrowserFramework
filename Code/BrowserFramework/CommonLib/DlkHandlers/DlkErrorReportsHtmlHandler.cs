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
    public static class DlkErrorReportsHtmlHandler
    {
        private const string STR_HTML_STYLE_TITLE = "border:1px dotted black;border-collapse:collapse; font-style: bold; display: inline";
        private const string STR_HTML_STYLE_TABLE = "border:1px dotted black;border-collapse:collapse; font-size:13";
        private const string STR_HTML_STYLE_SUMMARY_HEADER = "border:1px dotted black; text-align:left";
        private const string STR_HTML_STYLE_SUMMARY_CELL = "border:1px dotted black; text-align:left";
        private const string STR_HTML_STYLE_DETAILS_HEADER = "border:1px dotted black";
        private const string STR_HTML_STYLE_NO_BORDER = "border:none; margin:0px,5px";
        private const string STR_HTML_STYLE_DETAILS_LEFT = "border:1px dotted black; text-align:left; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_RIGHT = "border:1px dotted black; text-align:right; padding:5px";
        private const string STR_HTML_STYLE_DETAILS_CENTER = "border:1px dotted black; text-align:center; padding:5px";

        private const string STR_HTML_TITLE = "Test Runner Error Report";
        private const string STR_HTML_ERROR_TIMESTAMP = "Timestamp";
        private const string STR_HTML_ERRORLOG = "Error Information";


        public static string CreateHTMLReportBody(string inputXML)
        {
            string ret = "";
            try
            {
                XDocument input = XDocument.Load(inputXML);
                XElement title = new XElement("h3", new XAttribute("style", STR_HTML_STYLE_TITLE), STR_HTML_TITLE);
                XElement summaryTable = GetInfoTable(inputXML);
                XElement blankHeader = new XElement("br");
                XElement body = new XElement("body", title, blankHeader, summaryTable);

                // create html node
                XElement root = new XElement("html", body);
                XDocument output = new XDocument(root);
                ret = Path.Combine(Path.GetDirectoryName(inputXML), "errorreport.html");
                output.Save(ret);
            }
            catch
            {
                // do nothing
            }
            return ret;
        }


        private static XElement GetInfoTable(string inputXML)
        {
            XDocument input = XDocument.Load(inputXML);
            List<XElement> rows = new List<XElement>();

            var summary = from itm in input.Descendants("errorlog")
                          select new
                          {
                              name = itm.Attribute("instance").Value,
                              errorlog = itm.Attribute("errorInfo").Value
                          };

            foreach (var data in summary)
            {
                XElement inst = new XElement("tr",
                    new XElement("th", new XAttribute("style", STR_HTML_STYLE_DETAILS_HEADER), STR_HTML_ERROR_TIMESTAMP),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_DETAILS_LEFT), data.name));
                rows.Add(inst);
                inst = new XElement("tr",
                    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_ERRORLOG),
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), data.errorlog));
                rows.Add(inst);
                inst = new XElement("tr",
                    new XElement("td", new XAttribute("style", STR_HTML_STYLE_NO_BORDER), " "));
                rows.Add(inst);
            }

            // execution time
            //XElement logInfo = new XElement("tr",
            //    new XElement("th", new XAttribute("style", STR_HTML_STYLE_SUMMARY_HEADER), STR_HTML_ERRORLOG),
            //    new XElement("td", new XAttribute("style", STR_HTML_STYLE_SUMMARY_CELL), summary.First().errorlog));

            // table
            //XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), logInfo);
            XElement table = new XElement("table", new XAttribute("style", STR_HTML_STYLE_TABLE), rows);

            return table;
        }


    }
}
