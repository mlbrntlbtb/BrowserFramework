using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using CommonLib.DlkUtility;
using DiffMatchPatch;
using System.Text.RegularExpressions;

namespace DocDiff
{
    public class WordCompare
    {
        private String mExpected = "";
        private String mActual = "";
        private string mOutputFile = "";
        private List<Diff> mDiffs = null;
        private List<String> resultHTML = null;

        private List<Dictionary<String, Object>> ActualListStyleFormat;
        private List<Dictionary<String, Object>> ExpectedListStyleFormat;

        private const String HEADER_TEST_TYPE = "HEADER";
        private const String BODY_TEST_TYPE = "BODY";
        private const String FOOTER_TEST_TYPE = "FOOTER";

        private const String EXPECTED = "EXPECTED";
        private const String ACTUAL = "ACTUAL";

        private const String VALUE = "VALUE";
        private const String TYPE = "TYPE";
        private const String FORMAT = "FORMAT";
        private const String LEVEL = "LEVEL";
        private const String TEXT = "TEXT";
        private const String ITERATION = "ITERATION";
        private const String IND = "IND";
        private const String HANG = "HANG";

        private const int TWIPS = 120;

        //private Boolean mErrorsOnly = true; // we used to allow this to be toggled... but too many results

        public WordCompare(String ExpectedFile, String ActualFile, String OutputFile)
        {
            mExpected = ExpectedFile;
            mActual = ActualFile;
            mOutputFile = OutputFile;
            mDiffs = new List<Diff>();
            resultHTML = new List<String>();
        }

        public WordCompare(String ExpectedFile, String OutputFile)
        {
            mExpected = ExpectedFile;
            mOutputFile = OutputFile;
            mDiffs = new List<Diff>();
            resultHTML = new List<String>();
        }

        public List<DocDiffCompareDiffRecord> CompareFiles()
        {
            List<DocDiffCompareDiffRecord> AllResults = new List<DocDiffCompareDiffRecord>();

            WordprocessingDocument docExpected = WordprocessingDocument.Open(mExpected,true);
            WordprocessingDocument docActual = WordprocessingDocument.Open(mActual, true);



            //Add or remove checks here as needed
            AllResults.Add(PerformComparison(HEADER_TEST_TYPE, docExpected, docActual));
            AllResults.Add(PerformComparison(BODY_TEST_TYPE, docExpected, docActual));
            AllResults.Add(PerformComparison(FOOTER_TEST_TYPE, docExpected, docActual));

            //close the docs
            docExpected.Close();
            docActual.Close();        
            WriteResultToHTML(resultHTML);
            return AllResults;
        }

        public List<DocDiffCompareDiffRecord> CompareFileToString(List<String> StringToCompare)
        {
            List<DocDiffCompareDiffRecord> AllResults = new List<DocDiffCompareDiffRecord>();
            WordprocessingDocument docExpected = WordprocessingDocument.Open(mExpected, true);
            //Add or remove checks here as needed
            AllResults.Add(PerformComparison("Body", docExpected, StringToCompare));
            //close the docs
            docExpected.Close();
            WriteResultToHTML(resultHTML);
            return AllResults;
        }

        private String GetStringContent(WordprocessingDocument Document, String TestType)
        {
            StringBuilder sbContent = new StringBuilder();
            switch (TestType.ToLower())
            {
                case "header":
                    HeaderPart header = Document.MainDocumentPart.HeaderParts.FirstOrDefault();
                    if (header.Header.InnerText != null) sbContent.Append(header.Header.InnerText);
                    break;
                case "footer":
                    FooterPart footer = Document.MainDocumentPart.FooterParts.FirstOrDefault();
                    if (footer.Footer.InnerText != null) sbContent.Append(footer.Footer.InnerText);
                    break;
                case "body":
                    Body body = Document.MainDocumentPart.Document.Body;
                    if (body.InnerText != null) sbContent.Append(body.InnerText);
                    break;
            }
            return sbContent.ToString();
        }

        private List<Dictionary<String, String>> GetStringListContent(WordprocessingDocument Document, String TestType, String ActualOrExpected)
        {
            List<Dictionary<String, String>> ret = new List<Dictionary<String, String>>();
            List<OpenXmlElement> contents = new List<OpenXmlElement>();
            switch (TestType)
            {
                case HEADER_TEST_TYPE:
                    HeaderPart header = Document.MainDocumentPart.HeaderParts.FirstOrDefault();
                    if (header.Header.InnerText != null) contents = header.Header.Descendants().ToList();
                    break;
                case FOOTER_TEST_TYPE:
                    FooterPart footer = Document.MainDocumentPart.FooterParts.FirstOrDefault();
                    if (footer.Footer.InnerText != null) contents = footer.Footer.Descendants().ToList();
                    break;
                case BODY_TEST_TYPE:
                    Body body = Document.MainDocumentPart.Document.Body;
                    if (ActualOrExpected == ACTUAL)
                        ActualListStyleFormat = ExtracNumberStyleFormat(Document);
                    else if (ActualOrExpected == EXPECTED)
                        ExpectedListStyleFormat = ExtracNumberStyleFormat(Document);

                    if (body.InnerText != null) contents = body.ChildElements.ToList();
                    break;
            }

            if (TestType == BODY_TEST_TYPE) 
                ret = GetBodyListInfo(contents);
            else
                foreach (OpenXmlElement elm in contents)
                {
                    //localname p = paragraph. add it to list
                    if (elm.LocalName.Equals("p"))
                    {
                        if (elm.InnerText.ToLower().Contains("hyperlink"))
                        {
                            var textNoLink = elm.ChildElements.Where(x => !String.IsNullOrWhiteSpace(x.InnerText) && !x.InnerText.ToLower().Contains("hyperlink")).First();
                            if (textNoLink != null) ret.Add(new Dictionary<string, string>() { { VALUE, textNoLink.InnerText } });
                        }
                        else
                        {
                            ret.Add(new Dictionary<string, string>() { { VALUE, elm.InnerText } });
                        }
                    }
                }

            return ret;
        }


        private DocDiffCompareDiffRecord PerformComparison(String TestType, WordprocessingDocument Expected, WordprocessingDocument Actual)
        {
            int iType = 1;
            String sResult = "";
            DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord();

            string strTest = $"File{TestType}ContentCheck";
            List<Dictionary<String, String>> strExpected = GetStringListContent(Expected, TestType, EXPECTED);
            List<Dictionary<String, String>> strActual = GetStringListContent(Actual, TestType, ACTUAL);

            CompareDiffs(strExpected,strActual);
            if (mDiffs.Any(x=>x.Operation.IsInsert) || mDiffs.Any(x=>x.Operation.IsDelete))
            {
                sResult = $"File {TestType.ToLower()} have different word content. For details, please view HTML file results: {Path.GetFileNameWithoutExtension(mOutputFile)}.html";
            }
            else
            {
                iType = 0;
                sResult = $"File {TestType.ToLower()} have the same word content.";
            }

            cdr = new DocDiffCompareDiffRecord(iType, strTest, sResult, mExpected, mActual);
            return cdr;
        }

        private DocDiffCompareDiffRecord PerformComparison(String TestType, WordprocessingDocument Expected, List<String> ActualString)
        {
            int iType = 1;
            String sResult = "";
            DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord();

            string strTest = $"File{TestType}ContentCheck";
            List<Dictionary<String, String>> strExpected = GetStringListContent(Expected, TestType, EXPECTED);
            //TODO: ActualString must be in List form
            string ACTUAL_SOURCE = "Web Preview";

            CompareDiffs(strExpected, ActualString);
            if (mDiffs.Any(x => x.Operation.IsInsert) || mDiffs.Any(x => x.Operation.IsDelete))
            {
                sResult = $"File {TestType.ToLower()} have different word content. For details, please view HTML file results: {Path.GetFileNameWithoutExtension(mOutputFile)}.html";
            }
            else
            {
                iType = 0;
                sResult = $"File {TestType.ToLower()} have the same word content.";
            }

            cdr = new DocDiffCompareDiffRecord(iType, strTest, sResult, mExpected, ACTUAL_SOURCE);
            return cdr;
        }


        private String CleanString(String TextToClean)
        {
            string ret = string.Empty;

            ret = DlkString.ReplaceCarriageReturn(TextToClean,"");
            ret = DlkString.NormalizeNonBreakingSpace(ret);
            ret = DlkString.UnescapeXML(ret);
            return ret;
        }
        
        /// <summary>
        /// Use DiffMatchPatch library to compare differences
        /// </summary>
        /// <param name="Expected"></param>
        /// <param name="Actual"></param>
        private void CompareDiffs(List<Dictionary<String, String>> Expected, List<Dictionary<String, String>> Actual)
        {
            bool isElementHasTypeKey = Expected[0].ContainsKey(TYPE) && Actual[0].ContainsKey(TYPE);

            if (isElementHasTypeKey)
                CompareAndGenerateHtmlWithTypes(Expected, Actual);
            else
            {
                var dmp = DiffMatchPatchModule.Default;
                for (int i = 0; i < Expected.Count; i++)
                {
                    if (i >= Actual.Count) break;
                    int j = i >= Actual.Count ? Actual.Count - 1 : i;

                    var expected = Expected[i];
                    var actual = Actual[j];

                    var diffsRecord = dmp.DiffMain(expected[VALUE], actual[VALUE]);
                    dmp.DiffCleanupEfficiency(diffsRecord);
                    mDiffs.AddRange(diffsRecord);
                    var html = dmp.DiffPrettyHtml(diffsRecord);
                    resultHTML.Add($"<br>{html}</br>");
                }
            }
                
        }

        private void CompareDiffs(List<Dictionary<String, String>> Expected, List<String> Actual)
        {
            var actual = Actual.Select(a => new Dictionary<string, string>() 
            {
                {VALUE,  a}
            }).ToList();

            CompareDiffs(Expected, actual);
        }

        /// <summary>
        /// This method will generate and display the result of the document comparison on the browser with additional styling.
        /// </summary>
        /// <param name="results"></param>
        private void WriteResultToHTML(List<String> results)
        {
            string htmlFile = Path.GetFileNameWithoutExtension(mOutputFile) + ".html";
            using (FileStream fs = new FileStream(Path.Combine(Path.GetDirectoryName(mOutputFile),htmlFile), FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<div style='margin: 100px 100px; padding: 96px 120px; border: .5px solid black; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);'>");
                    foreach (string line in results)
                    {
                        w.WriteLine(line);
                    }
                    w.WriteLine("</div>");
                }
            }
        }

        /// <summary>
        /// This method will extract and return Number List Information from the given contents.
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        private List<Dictionary<String, String>> GetBodyListInfo(List<OpenXmlElement> contents)
        {
            List<Dictionary<String, String>> ret = new List<Dictionary<String, String>>();
            
            foreach (OpenXmlElement item in contents)
            {
                string listType = "";

                var ItemWithListType = item.Descendants().Where(dec => dec.LocalName.ToLower().Contains("style")).FirstOrDefault();
                if (ItemWithListType != null && !ItemWithListType.OuterXml.Contains(":val=\"NUM\""))
                {
                    listType = GetValueUsingRegex(ItemWithListType.OuterXml);
                    
                    var element = new Dictionary<string, string>()
                    {
                        { VALUE, item.InnerText.Trim() },
                        { TYPE, listType }
                    };
                    ret.Add(element);
                }
            }

            return ret;
        }

        /// <summary>
        /// This Method will conduct Comparison and genarate HTML with styling based on the result.
        /// </summary>
        /// <param name="Expected"></param>
        /// <param name="Actual"></param>
        private void CompareAndGenerateHtmlWithTypes(List<Dictionary<String, String>> Expected, List<Dictionary<String, String>> Actual)
        {
            var dmp = DiffMatchPatchModule.Default;
            for (int i = 0; i < Expected.Count; i++)
            {
                if (i >= Actual.Count) break;
                int j = i >= Actual.Count ? Actual.Count - 1 : i;

                var expected = Expected[i];
                var actual = Actual[j];

                var ActualText = GenerateLineTextWithFormatAndStyle(actual, ACTUAL);
                var ExpectedText = GenerateLineTextWithFormatAndStyle(expected, EXPECTED);

                var diffsRecord = dmp.DiffMain(ExpectedText, ActualText);
                dmp.DiffCleanupEfficiency(diffsRecord);
                mDiffs.AddRange(diffsRecord);
                var html = dmp.DiffPrettyHtml(diffsRecord);
                string HTML;

                if (expected[TYPE] == "CMT")
                    HTML = $"<br><span style='color: #0000FF;'>{html}</span></br>";
                else
                    HTML = $"<br>{html}</br>";

                resultHTML.Add(HTML);
            }
        }

        /// <summary>
        /// This method will extract NumberStyle Formating Definition fron the given Document
        /// </summary>
        /// <param name="Document"></param>
        /// <returns></returns>
        private List<Dictionary<String, Object>> ExtracNumberStyleFormat(WordprocessingDocument Document)
        {
            List<Dictionary<String, Object>> ListStyleFormat = new List<Dictionary<string, Object>>();

            var numberings = Document.MainDocumentPart.NumberingDefinitionsPart.Numbering.Descendants();
            var formats = numberings.Where(an => an.LocalName == "lvl").Where(lvl => lvl.InnerXml.Contains("<w:pStyle w:val="));
            foreach (var format in formats)
            {
                var lvlValue = GetValueUsingRegex(format.OuterXml, "ilvl");

                var type = format.ChildElements.Where(f => f.LocalName.ToLower().Contains("style")).FirstOrDefault();
                var frmt = format.ChildElements.Where(f => f.LocalName.ToLower().Contains("numfmt")).FirstOrDefault();
                var text = format.ChildElements.Where(f => f.LocalName.ToLower().Contains("text")).FirstOrDefault();

                if (type == null || frmt == null || text == null) throw new Exception("GetStringListContent() failed.");

                var typeVal = GetValueUsingRegex(type.OuterXml);
                var formatValue = GetValueUsingRegex(frmt.OuterXml);
                var textValue = GetValueUsingRegex(text.OuterXml);

                var ind = format.ChildElements.Where(f => f.LocalName.ToLower().Contains("ppr")).FirstOrDefault();
                var indValue = 0;
                var hangValue = 0;

                if (ind != null)
                {
                    indValue = Convert.ToInt32(GetValueUsingRegex(ind.OuterXml, "left"));
                    var hang = GetValueUsingRegex(ind.OuterXml, "hanging");
                    hangValue = string.IsNullOrEmpty(hang) ? 0 : Convert.ToInt32(hang);
                }

                ListStyleFormat.Add(new Dictionary<string, Object>()
                        {
                            {LEVEL, lvlValue},
                            {TYPE, typeVal},
                            {FORMAT, formatValue},
                            {TEXT, textValue},
                            {ITERATION, 0},
                            {IND, indValue},
                            {HANG, hangValue}
                        });
            }

            return ListStyleFormat;
        }
        
        /// <summary>
        /// This method will extract and return val (by default or any other given attr) from the given xml string
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private String GetValueUsingRegex(String txt, String type = "val")
        {
            var match = Regex.Match(txt, $@"w:{type}="".*? """);
            return match.Value.Replace($"w:{type}=", "").Replace("\"", "");
        }

        /// <summary>
        /// This method will retrun a converted indentation based on the given param.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private string Indent(int count)
        {
            return "".PadLeft(count);
        }

        /// <summary>
        /// This method will return formated text with number style by level that is ready to be compared.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ActualOrExpected"></param>
        /// <returns></returns>
        private String GenerateLineTextWithFormatAndStyle(Dictionary<string, string> item, String ActualOrExpected)
        {
            //const string DECIMAL = "decimal";
            const string DECIMAL_ZERO = "decimalZero";
            const string UPPER_LETTER = "upperLetter";
            const string LOWER_LETTER = "lowerLetter";


            var styleFormats = ActualOrExpected == EXPECTED ? ExpectedListStyleFormat : ActualListStyleFormat;


            var styleFormat = styleFormats.Where(sf => sf[TYPE].ToString() == item[TYPE]).FirstOrDefault();

            if (styleFormat != null)
            {
                var level = Convert.ToInt32(styleFormat[LEVEL]);
                var type = (String)styleFormat[TYPE];
                var format = (String)styleFormat[FORMAT];
                var text = (String)styleFormat[TEXT];
                var ind = Convert.ToDouble(styleFormat[IND]);
                var hang = Convert.ToDouble(styleFormat[HANG]);


                // Increment current item level iteration and reset all lower level 
                styleFormat[ITERATION] = (int)styleFormat[ITERATION] + 1;
                foreach (var sf in styleFormats)
                    if (Convert.ToInt32(sf[LEVEL]) > level) sf[ITERATION] = 0;

                var matches = Regex.Matches(text, @"%[0-9]+");

                var prevLvls = styleFormats.GetRange(0, styleFormats.IndexOf(styleFormat) + 1);

                foreach (Match match in matches)
                {
                    var lvl = Convert.ToInt32(match.Value.Replace("%", ""));
                    var val = prevLvls.Where(pl => 
                    Convert.ToInt32(pl[LEVEL]) + 1 == lvl && Convert.ToInt32(pl[ITERATION]) > 0).FirstOrDefault();

                    if (val == null) 
                        throw new Exception("GenerateLineTextWithFormatAndStyle() failed.");

                    string valStr = string.Empty;
                    int itr = Convert.ToInt32(val[ITERATION]);
                    int valLvl = Convert.ToInt32(val[LEVEL]);

                    if (val[FORMAT].ToString() == DECIMAL_ZERO && valLvl > 0)
                        valStr = itr > 9 ? $"{itr}" : $"0{itr}";
                    else if (val[FORMAT].ToString() == UPPER_LETTER)
                        valStr = $"{(char)(64 + itr)}";
                    else if (val[FORMAT].ToString() == LOWER_LETTER)
                        valStr = $"{(char)(96 + itr)}";
                    else
                        valStr = $"{itr}";

                    text = text.Replace(match.Value, valStr);
                }

                //120 twips == 1 char
                int indent = (int)Math.Round((ind / TWIPS));
                int hanging = (int)Math.Round((hang / TWIPS));


                return $"{Indent(indent)}{text}{Indent(hanging)}{item[VALUE]}";
            }

            

            return item[VALUE];
        }

    }
}
