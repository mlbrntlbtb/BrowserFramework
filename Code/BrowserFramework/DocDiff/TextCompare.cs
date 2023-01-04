using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DocDiff
{
    public class TextCompare
    {
        private String mExpected = "";
        private String mActual = "";
        private Boolean mErrorsOnly = true; // we used to allow this to be toggled... but too many results

        public TextCompare(String ExpectedFile, String ActualFile)
        {
            mExpected = ExpectedFile;
            mActual = ActualFile;
        }
        public List<DocDiffCompareDiffRecord> CompareFiles()
        {
            List<DocDiffCompareDiffRecord> AllResults = new List<DocDiffCompareDiffRecord>();
            int iType = 1;
            String sResult = "";
            String sTest = "";

            String[] fExpected = File.ReadAllLines(mExpected);
            String[] fActual = File.ReadAllLines(mActual);

            // check the # of Lines

            sTest = "FilesLinesCheck";
            iType = 1;
            if (fExpected.Length == fActual.Length)
            {
                if (!mErrorsOnly)
                {
                    iType = 0;
                    sResult = "Files have the same number of lines.";
                    DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, sTest, sResult, fExpected.Length.ToString(), fActual.Length.ToString());
                    AllResults.Add(cdr);
                }
            }
            else
            {
                sResult = "Files have different number of lines.";
                DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, sTest, sResult, fExpected.Length.ToString(), fActual.Length.ToString());
                AllResults.Add(cdr);
            }

            sTest = "FileContentCheck";
            for (int i = 0; i < fExpected.Length; i++)
            {
                iType = 1;
                if (i > fActual.Length)
                {
                    sResult = "Additonal line found only in Expected. Line Number: " + (i + 1).ToString();
                    DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, sTest, sResult, fExpected[i], "");
                    AllResults.Add(cdr);
                }
                else
                {
                    if (fExpected[i] == fActual[i])
                    {
                        if (!mErrorsOnly)
                        {
                            iType = 0;
                            sResult = "Lines match. Line Number: " + (i + 1).ToString();
                            DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, sTest, sResult, fExpected[i], fActual[i]);
                            AllResults.Add(cdr);

                        }
                    }
                    else
                    {
                        sResult = "Lines do not match. Line Number: " + (i + 1).ToString();
                        DocDiffCompareDiffRecord cdr = new DocDiffCompareDiffRecord(iType, sTest, sResult, fExpected[i], fActual[i]);
                        AllResults.Add(cdr);
                    }
                }
            }

            return AllResults;
        }
    }
}
