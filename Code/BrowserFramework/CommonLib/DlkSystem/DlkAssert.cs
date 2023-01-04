using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;


namespace CommonLib.DlkSystem
{
    /// <summary>
    /// Our Assert class. An assertion is a test... comparison of 2 values - an expected and an actual result.
    /// If not equal an exception is raised
    /// </summary>
    public static class DlkAssert
    {

        /// <summary>
        /// Verifies the supplied value is within a range
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="lowrange"></param>
        /// <param name="hirange"></param>
        /// <param name="actualvalue"></param>
        public static void AssertWithinRange(String TestDesc, double lowrange, double hirange, double actualvalue)
        {
            DlkLogger.LogAssertion(TestDesc, lowrange.ToString() + ":" + hirange.ToString(), actualvalue.ToString());
            if (actualvalue < lowrange)
            {
                throw new Exception("Actual value [" + actualvalue.ToString() + "] below Low Threshold [" + lowrange.ToString() + "].");
            }
            if (actualvalue > hirange)
            {
                throw new Exception("Actual value [" + actualvalue.ToString() + "] above High Threshold [" + hirange.ToString() + "].");
            }
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqual(String TestDesc, String ExpectedResult, String ActualResult)
        {
            DlkLogger.LogAssertion(TestDesc, ExpectedResult, ActualResult);
            AreEqual(ExpectedResult, ActualResult);
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqual(String TestDesc, String [] ExpectedResult, String [] ActualResult)
        {
            DlkAssert.AssertEqual(TestDesc + " List Count", ExpectedResult.Length, ActualResult.Length);
            DlkLogger.LogAssertion(TestDesc, String.Join("~", ExpectedResult), String.Join("~", ActualResult));
            for (int i = 0; i < ExpectedResult.Length; i++)
            {
                AreEqual(ExpectedResult[i], ActualResult[i]);
            }
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        /// <param name="PartialMatch"></param>
        public static void AssertEqual(String TestDesc, String ExpectedResult, String ActualResult, Boolean PartialMatch)
        {
            DlkLogger.LogAssertionMatch(TestDesc, ExpectedResult, ActualResult);
            if (PartialMatch)
            {
                AreEqual(true, ActualResult.Contains(ExpectedResult));
            }
            else
            {
                AreEqual(ExpectedResult, ActualResult);
            }
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqual(String TestDesc, Object ExpectedResult, Object ActualResult)
        {
            DlkLogger.LogAssertion(TestDesc, Convert.ToString(ExpectedResult), Convert.ToString(ActualResult));
            AreEqual(ExpectedResult, ActualResult);
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqual(String TestDesc, Boolean ExpectedResult, Boolean ActualResult)
        {
            DlkLogger.LogAssertion(TestDesc, Convert.ToString(ExpectedResult), Convert.ToString(ActualResult));
            AreEqual(ExpectedResult, ActualResult);
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqual(String TestDesc, float ExpectedResult, float ActualResult)
        {
            DlkLogger.LogAssertion(TestDesc, Convert.ToString(ExpectedResult), Convert.ToString(ActualResult));
            AreEqual(ExpectedResult, ActualResult);
        }
       
        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqual(String TestDesc, double ExpectedResult, double ActualResult)
        {
            DlkLogger.LogAssertion(TestDesc, Convert.ToString(ExpectedResult), Convert.ToString(ActualResult));
            AreEqual(ExpectedResult, ActualResult);
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// Display masked text parameters in log
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqualPassword(String TestDesc, String ExpectedResult, String ActualResult)
        {
            DlkLogger.LogAssertion(TestDesc, GetMaskedText(ExpectedResult), GetMaskedText(ActualResult));
            AreEqualPassword(ExpectedResult, ActualResult);
        }

        /// <summary>
        /// Compares and expected and actual result. An error is thrown if not equal.
        /// </summary>
        /// <param name="TestDesc"></param>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        public static void AssertEqual(String TestDesc, int ExpectedResult, int ActualResult)
        {
            DlkLogger.LogAssertion(TestDesc, Convert.ToString(ExpectedResult), Convert.ToString(ActualResult));
            AreEqual(ExpectedResult, ActualResult);
        }

        /// <summary>
        /// Works like typical assert methods... does nothing if equal... raises an error if not equal
        /// </summary>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        private static void AreEqual(String ExpectedResult, String ActualResult)
        {
            if (ExpectedResult != ActualResult)
            {
                throw new Exception("Expected Result [" + ExpectedResult + "] not equal to Actual result [" + ActualResult + "].");
            }
        }

        /// <summary>
        /// Works like typical assert methods... does nothing if equal... raises an error if not equal
        /// Masked parameters if not matched..
        /// </summary>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        private static void AreEqualPassword(String ExpectedResult, String ActualResult)
        {
            if (ExpectedResult != ActualResult)
            {
                throw new Exception("Expected Result [" + GetMaskedText(ExpectedResult) + "] not equal to Actual result [" + GetMaskedText(ActualResult) + "].");
            }
        }

        /// <summary>
        /// Works like typical assert methods... does nothing if equal... raises an error if not equal
        /// </summary>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        private static void AreEqual(Boolean ExpectedResult, Boolean ActualResult)
        {
            if (ExpectedResult != ActualResult)
            {
                throw new Exception("Expected Result [" + ExpectedResult.ToString() + "] not equal to Actual result [" + ActualResult.ToString() + "].");
            }
        }
        /// <summary>
        /// Works like typical assert methods... does nothing if equal... raises an error if not equal
        /// </summary>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        private static void AreEqual(Object ExpectedResult, Object ActualResult)
        {
            if (ExpectedResult != ActualResult)
            {
                throw new Exception("Expected Result [" + ExpectedResult.ToString() + "] not equal to Actual result [" + ActualResult.ToString() + "].");
            }
        }
        /// <summary>
        /// Works like typical assert methods... does nothing if equal... raises an error if not equal
        /// </summary>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        private static void AreEqual(int ExpectedResult, int ActualResult)
        {
            if (ExpectedResult != ActualResult)
            {
                throw new Exception("Expected Result [" + ExpectedResult.ToString() + "] not equal to Actual result [" + ActualResult.ToString() + "].");
            }
        }
        /// <summary>
        /// Works like typical assert methods... does nothing if equal... raises an error if not equal
        /// </summary>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        private static void AreEqual(float ExpectedResult, float ActualResult)
        {
            if (ExpectedResult != ActualResult)
            {
                throw new Exception("Expected Result [" + ExpectedResult.ToString() + "] not equal to Actual result [" + ActualResult.ToString() + "].");
            }
        }
        /// <summary>
        /// Works like typical assert methods... does nothing if equal... raises an error if not equal
        /// </summary>
        /// <param name="ExpectedResult"></param>
        /// <param name="ActualResult"></param>
        private static void AreEqual(double ExpectedResult, double ActualResult)
        {
            if (ExpectedResult != ActualResult)
            {
                throw new Exception("Expected Result [" + ExpectedResult.ToString() + "] not equal to Actual result [" + ActualResult.ToString() + "].");
            }
        }

        /// <summary>
        /// Masked text parameter
        /// </summary>
        /// <param name="value">parameter value</param>
        /// <returns></returns>
        private static string GetMaskedText(string value)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            foreach (char _ in value.ToCharArray())
            {
                result.Append(DlkRecords.DlkPasswordMaskedRecord.PasswordChar);
            }
            if (String.IsNullOrWhiteSpace(result.ToString())) result.Append(DlkRecords.DlkPasswordMaskedRecord.DEFAULT_BLANK_MASKED_VALUE);
            return result.ToString();
        }
    }
}
