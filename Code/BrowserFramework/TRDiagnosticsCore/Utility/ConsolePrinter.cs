using System;
using System.Collections.Generic;
using System.Linq;

namespace TRDiagnosticsCore.Utility
{
    public static class ConsolePrinter
    {
        private const string HEADER = "==================================================";
        private const string TITLE = "Test Runner Diagnostics Tool";
        private const string AUTHOR = "Author: MakatiAutomation";

        /// <summary>
        /// Print Test header to Console
        /// </summary>
        public static void PrintTestHeader(String TestName)
        {
            Console.WriteLine(HEADER);
            Console.WriteLine(TestName.ToUpper());
            Console.WriteLine(HEADER);
        }

        /// <summary>
        /// Print summary results to Console
        /// </summary>
        public static void PrintSummary(int SummarySteps, List<LogRecord> SummaryLogs, List<RecommendedItem> SummaryRecommendations, String SummaryType)
        {
            if (SummarySteps > 0)
            {
                Console.WriteLine(HEADER);
                Console.WriteLine($"{SummaryType}s found: {SummarySteps}");
                Console.WriteLine(HEADER);
                foreach (LogRecord log in SummaryLogs)
                {
                    log.PrintToConsole();
                    LogRecord recLog = SummaryRecommendations.Where(r => r.Issue.Equals(log)).First().Recommendation;
                    if (recLog.MessageType != Logger.MessageType.NULL) { recLog.PrintToConsole(); }
                }
                Console.WriteLine(HEADER);
            }
        }

        /// <summary>
        /// Print specified values to Console
        /// </summary>
        public static void PrintLine(params string[] Values)
        {
            if(Values.Count() > 0)
            {
                foreach (string value in Values)
                {
                    Console.WriteLine(value);
                }
            }
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Print Header only to Console
        /// </summary>
        public static void PrintHeader()
        {
            Console.WriteLine(HEADER);
        }

        /// <summary>
        /// Print Diagnostic header to Console
        /// </summary>
        public static void PrintDiagnosticHeader()
        {
            Console.WriteLine(HEADER);
            Console.WriteLine(TITLE);
            Console.WriteLine(AUTHOR);
            Console.WriteLine(HEADER);
        }

        /// <summary>
        /// Print value without new line to Console
        /// </summary>
        public static void Print(string Value)
        {
            Console.Write(Value);
        }
    }
}
