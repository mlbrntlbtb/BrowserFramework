using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using METestHarness.Sys;
using METestHarness.Tests.Samples;

namespace METestHarness
{
    class Program
    {
        private const string RUN = "Run";
        private const string HEADER = "==================================================";
        private const string TITLE = "Test Harness (testh.exe)";
        private static string LOGS_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs");
        private const string LOG_FILE_HEADER = "log_";
        static void Run(string TestRunClassName)
        {

        }

        static Type GetType(string TestRunClassName)
        {
            Type ret = null;

            foreach (Type typ in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typ.Name.ToLower() == TestRunClassName.ToLower())
                {
                    ret = typ;
                    break;
                }
            }
            return ret;
        }

        static object GetInstance(Type type, params object[] Params)
        {
            object ret = null;
            ret = Activator.CreateInstance(type, Params);
            return ret;
        }

        static int Main(string[] args)
        {
            string testRunName = string.Empty;
            string emailList = string.Empty;

            if (args.Length <= 0)
            {
                Console.WriteLine("Bye!");
                return 0;
            }
            else
            {
                testRunName = args[0];
                if (args.Length > 1)
                    emailList = args[1];
            }

            DisplayPreamble();

            int ret = -1;
            Type type = GetType(testRunName);
            if (type != null)
            {
                Object instance = GetInstance(type, emailList);
                if (instance != null)
                {
                    MethodInfo func = type.GetMethod(RUN);
                    ret = (int)(func.Invoke(instance, null));
                    PrepareLogs();
                }
                else
                {
                    Console.WriteLine("ERROR: Instance of TestRun cannot be created.");
                }
            }
            else
            {
                Console.WriteLine("ERROR: No such TestRun exists.");
            }
            return ret;
        }

        static void DisplayHelp()
        {

        }

        static void PrepareLogs()
        {
            Directory.CreateDirectory(LOGS_PATH);
            File.WriteAllLines(Path.Combine(LOGS_PATH, LOG_FILE_HEADER + GetDateTimeString(DateTime.Now) + ".txt"), 
                Driver.SessionLogger.Logs);
        }


        static string GetDateTimeString(DateTime inputString)
        {
            return string.Format("{0:yyyyMMddHHmmss}", inputString);
        }

        static void DisplayPreamble()
        {
            Console.WriteLine(HEADER);
            Console.WriteLine(TITLE);
            Console.WriteLine("Version\t: " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Console.WriteLine("Author\t: MakatiAutomation");
            Console.WriteLine(HEADER);
            Console.WriteLine();
        }
    }
}
