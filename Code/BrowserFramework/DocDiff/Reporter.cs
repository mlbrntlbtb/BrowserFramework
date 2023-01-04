using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml.Linq;
using System.IO;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using CommonLib.DlkHandlers;
using System.Reflection;
using HTMLBuilder;

namespace DocDiff
{
    public class Reporter
    {
        private DocDiffCompareInfoRecord mCompareInfo;
        private List<DocDiffCompareDiffRecord> mCompareResults;
        private String mExecutionTime;
        private string mDirTools;
        private string mDirProductsRoot;

        public Reporter(DocDiffCompareInfoRecord CompareInfo, List<DocDiffCompareDiffRecord> CompareResults, TimeSpan ExecutionTime)
        {
            InitializeEnvironment();
            mCompareInfo = CompareInfo;
            mCompareResults = CompareResults;
            mExecutionTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ExecutionTime.Hours, ExecutionTime.Minutes, ExecutionTime.Seconds, ExecutionTime.Milliseconds / 10);
        }

        private void InitializeEnvironment()
        {
            string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mRootPath = Directory.GetParent(binDir).FullName;
            while (new DirectoryInfo(mRootPath).GetDirectories()
                .Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                mRootPath = Directory.GetParent(mRootPath).FullName;
            }

            mDirProductsRoot = Path.Combine(mRootPath, "Products") + @"\";
            mDirTools = Path.Combine(mRootPath, "Tools") + @"\";
            if (!Directory.Exists(mDirTools))
            {
                throw new Exception("Required Tools directory does not exist: " + mDirTools);
            }
        }
        public void CreateReport(String FileOutput)
        {
            int ErrCount =0;

            // create CompareResults
            List<XElement> ElmsResults = new List<XElement>();
            
         
            //Environment.CurrentDirectory; 

            int iNum = 0;
            foreach (DocDiffCompareDiffRecord cdr in mCompareResults)
            {
                iNum++;
                String messagetype = "info";
                if (cdr.messagetype != 0)
                {
                    messagetype = "error";
                    ErrCount++;
                }
                XElement resultrow = new XElement("result",
                    new XAttribute("id", iNum.ToString()),
                    new XElement("messagetype", messagetype),
                    new XElement("testtype", cdr.testtype),
                    new XElement("testdetails", cdr.testdetails),
                    new XElement("expectedfiledata", cdr.expectedfiledata),
                    new XElement("actualfiledata", cdr.actualfiledata)
                    );
                ElmsResults.Add(resultrow);
            }
            XElement ElmResultsParent= new XElement("results", ElmsResults);

            // create CompareInfo
            XElement ElmInfo = new XElement("info",
                new XElement("errorcount", ErrCount),
                new XElement("configfile", this.mCompareInfo.configfile),
                new XElement("expectedfile", this.mCompareInfo.expectedfile),
                new XElement("actualfile", this.mCompareInfo.actualfile),
                new XElement("comparetype", this.mCompareInfo.comparetype),
                new XElement("executiontime", this.mExecutionTime)
                );

            XElement ElmRoot = new XElement("DocDiff", ElmInfo, ElmResultsParent);

            XDocument xDoc = new XDocument(ElmRoot);
            // add reference to StyleSheet
            string xslPath = Path.Combine(mDirProductsRoot, @"Common\StyleSheet\DocDiff.xsl");
            xDoc.AddFirst(new XProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"" + xslPath + "\"")); 
            
            if (File.Exists(FileOutput))
            {
                File.Delete(FileOutput);
            }
            xDoc.Save(FileOutput);

            System.Console.WriteLine("Report created: " + FileOutput);
        }

        public void CreateReportHTML(String FileOutput)
        {
            List<XElement> rows = new List<XElement>();

            int iNum = 0;
            int ErrCount = 0;
            int SheetNameCompCount = 0;
            foreach (DocDiffCompareDiffRecord cdr in mCompareResults)
            {
                iNum++;
                String messagetype = "info";
                if (cdr.messagetype != 0)
                {
                    messagetype = "error";
                    ErrCount++;
                }
                if (cdr.testtype == "SheetNameComparison") SheetNameCompCount++;

                 XElement row = HTML.TR(
                        HTML.ID(iNum.ToString()),
                        HTML.Attr("message-type", messagetype),
                        HTML.TD(cdr.testtype),
                        HTML.TD(cdr.testdetails),
                        HTML.TD(cdr.expectedfiledata),
                        HTML.TD(cdr.actualfiledata)
                    );

                rows.Add(row);
            }

            var docdiff = HTML.Container(
                    HTML.CustomCard("DocDiff General Information",
                        HTML.Class(UC.MY_5),
                        HTML.Div(
                            HTML.Class(UC.D_FLEX, UC.FLEX_COLUMN, UC.FLEX_MD_ROW),
                            HTML.Div(
                                HTML.Class(UC.FLEX_FILL, UC.MB_5, UC.MR_MD_4),
                                HTML.InfoCard($"Error{plural(ErrCount)} found", ErrCount.ToString(), UC.BG_DANGER)
                            ),
                            HTML.Div(
                                HTML.Class(UC.FLEX_FILL, UC.MB_5),
                                HTML.InfoCard("Execution Time", this.mExecutionTime, UC.BG_PRIMARY)
                            )
                        ),
                        HTML.Div(
                            HTML.Info("Compare Type", this.mCompareInfo.comparetype),
                            HTML.Info("Config File", this.mCompareInfo.configfile, "config"),
                            HTML.Info("Expected File", this.mCompareInfo.expectedfile, "expectedFile"),
                            HTML.Info("Actual File", this.mCompareInfo.actualfile, "actualFile")
                        )
                    ),
                    HTML.CustomCard("DocDiff Comparison Details",
                        HTML.Class(UC.MB_5),
                        HTML.Table(
                            HTML.Class("table-hover align-middle table-bordered"),
                            HTML.THead(
                                HTML.TR(
                                    HTML.TH("Test Type"),
                                    HTML.TH("Test Details"),
                                    HTML.TH("ExpectedFile"),
                                    HTML.TH("ActualFile")
                                )
                            ),
                            rows.Count > 0
                                    ? HTML.TBody(rows)
                                    : HTML.TBody(
                                            HTML.TR(
                                                HTML.TD(
                                                    HTML.Class(UC.TEXT_CENTER),
                                                    HTML.Attr("colspan", "4"),
                                                    HTML.H3("No records to display")
                                                )
                                            )
                                    )
                        )
                    ),
                    HTML.P(
                        HTML.Class(UC.TEXT_CENTER, UC.MY_5),
                        HTML.Style("color: #8F8F8F"),
                        "* Full details of the report are not available for Internet Explorer. It is recommended to view this report in the latest version of Google Chrome, Mozilla Firefox or Microsoft Edge (Chromium) *"
                    )
                );

            var data = HTML.Tag("data", HTML.Class(UC.D_NONE),
                    HTML.Tag("errorcount", ErrCount.ToString()),
                    HTML.Tag("sheetnamecompcount", SheetNameCompCount.ToString())
                );

            var html = new HtmlBuilder();
            html.Body(docdiff);
            html.Data(data);
            html.Save(FileOutput, false);

            string plural(int count) => count == 1 ? "" : "s";
        }
    }
}
