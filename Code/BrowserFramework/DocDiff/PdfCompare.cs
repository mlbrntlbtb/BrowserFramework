using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.Content.Objects;
using System.Text;

namespace DocDiff
{
    public class PdfCompare
    {
        private String mExpected = "";
        private String mActual = "";
        //private Boolean mErrorsOnly = true; // we used to allow this to be toggled... but too many results

        public PdfCompare(String ExpectedFile, String ActualFile)
        {
            mExpected = ExpectedFile;
            mActual = ActualFile;
        }
        public List<DocDiffCompareDiffRecord> CompareFiles()
        {
            List<DocDiffCompareDiffRecord> AllResults = new List<DocDiffCompareDiffRecord>();
            int iType = 1;
            String sResult = "";

            String[] fExpected = File.ReadAllLines(mExpected);
            String[] fActual = File.ReadAllLines(mActual);
            PdfDocument pdfExpected = PdfReader.Open(mExpected, PdfDocumentOpenMode.ReadOnly);
            PdfDocument pdfActual = PdfReader.Open(mActual, PdfDocumentOpenMode.ReadOnly);

            DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord();

            string strExpected = GetStringContent(pdfExpected);
            string strActual = GetStringContent(pdfActual);

            if (strExpected == strActual)
            {
                iType = 0;
                sResult = $"File have the same word content.";
            }
            else
            {
                sResult = $"File have different word content.";
            }

            cdr = new DocDiffCompareDiffRecord(iType, "FileContentCheck", sResult, strExpected, strActual);
            AllResults.Add(cdr);

            //close the docs
            pdfExpected.Close();
            pdfActual.Close();
            return AllResults;
        }

        public static String GetStringContent(PdfDocument Document)
        {
            StringBuilder pdfContent = new StringBuilder();
            for (int i = 0; i < Document.Pages.Count; i++)
            {
                PdfPage page = Document.Pages[i];
                CObject content = ContentReader.ReadContent(page);
                var text = ExtractText(content);
                foreach (string s in text)
                {
                    pdfContent.Append(s);
                }
            }
            return pdfContent.ToString();
        }

        private static IEnumerable<string> ExtractText(CObject cObject)
        {
            var textList = new List<string>();
            if (cObject is COperator)
            {
                var cOperator = cObject as COperator;
                if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
                    cOperator.OpCode.Name == OpCodeName.TJ.ToString())
                {
                    foreach (var cOperand in cOperator.Operands)
                    {
                        textList.AddRange(ExtractText(cOperand));
                    }
                }
            }
            else if (cObject is CSequence)
            {
                var cSequence = cObject as CSequence;
                foreach (var element in cSequence)
                {
                    textList.AddRange(ExtractText(element));
                }
            }
            else if (cObject is CString)
            {
                var cString = cObject as CString;
                textList.Add(cString.Value);
            }
            return textList;
        }
    }
}
