using CommonLib.DlkUtility;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TestRunnerCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool displayHelp = true;

                if (args.Count() > 0)
                {
                    var filePath = args[0];
                    //validations - fail if suite do not exist
                    if (!File.Exists(filePath))
                    {
                        throw new ArgumentException("Suite path not found.");
                    }
                    var xml = XDocument.Load(filePath);
                    var type = xml.Root.Name.LocalName.ToLower();

                    /* Allow parallel execution */
                    DlkTestRunnerCmdLib.DoNotKillDriversOnTearDown = true;

                    //Run suite or scheduler file
                    if (type == "suite")
                    {
                        var arguments = new DlkTestRunnerCmdLibExecutionArgs(args);
                        displayHelp = DlkTestRunnerCmdLib.Run(arguments);
                    }
                    else if (type == "scheduler")
                    {
                        displayHelp = DlkTestRunnerCmdLib.RunLegacySchedulerFile(args);
                    }
                }

                if (displayHelp)
                    DisplayHelp();
            }
            catch (Exception e)
            {
                DlkTestRunnerCmdLib.LogException(e);
                Console.WriteLine("Test Runner Cmd encountered an unexpected error. Message:" + e.Message + " See program logs for more details.");
                Environment.Exit(0); 
            }
        }

        /// <summary>
        /// Serves as the help output to the users
        /// </summary>
        private static void DisplayHelp()
        {
            Console.WriteLine();
            Console.WriteLine("===================================");
            Console.WriteLine("Test Runner Cmd by MakatiAutomation");
            Console.WriteLine();
            Console.WriteLine("TESTRUNNERCMD [path] /args [args]");
            Console.WriteLine("   path              Absolute path of the suite or scheduler file.");
            Console.WriteLine();
            Console.WriteLine("For Suites:");
            Console.WriteLine("TESTRUNNERCMD [path] /b [browser] /en [environment] /l [library] /p [product] /e [email]");
            Console.WriteLine("   /l                Name of the product library to use.");
            Console.WriteLine("   /p                Name of the product.");
            Console.WriteLine("   /b                Target browser to use.");
            Console.WriteLine("   /en               Target environment to use.");
            Console.WriteLine("   /e                Email addresses of recipients.");
            Console.WriteLine("For Schedulers:");
            Console.WriteLine("TESTRUNNERCMD [path] /d [dayofweek]");
            Console.WriteLine("   /d                Day of the week whose schedules will be executed.");
            Console.WriteLine();
            Console.WriteLine("EXAMPLES");
            Console.WriteLine("   TestRunnerCmd C:\\QEAutomation\\Selenium\\BrowserFramework\\Products\\Costpoint_711\\Suites\\suite.xml /b Chrome /en default_environment /l CostpointLib.dll /p Costpoint /e user@deltek.com");
            Console.WriteLine("   TestRunnerCmd C:\\QEAutomation\\Selenium\\BrowserFramework\\Products\\Common\\Scheduler\\scheduler.xml /d Tuesday");
            Console.WriteLine("===================================");
        }
    }

}
