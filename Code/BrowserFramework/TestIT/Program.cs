using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestIT.Sys;

namespace TestIT
{
    class Program
    {
        private const string RUN = "Run";
        private const string HEADER = "==================================================";
        private const string TITLE = "Test Investigation Tool";
        private static string LOGS_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs");
        private const string LOG_FILE_HEADER = "log_";
        private static List<String> tests = new List<string>();

        public enum UnitTestType
        {
            TEST_CODE,
            TEST_CODE_ALL,
            //specific test script path
            TEST_SCRIPT,
            //specific folder path containing all existing tests
            TEST_FOLDER
        }
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
            int ret = -1;
            try
            {
                UnitTestType testType = UnitTestType.TEST_CODE_ALL;
                string testName = string.Empty;
                string emailList = string.Empty;

                if (args.Length <= 0)
                {
                    Console.WriteLine("Bye!");
                    return 0;
                }
                else
                {
                    testType = (UnitTestType)Enum.Parse(typeof(UnitTestType), args[0]);

                    testName = args[1];
                    if (args.Length > 1)
                        emailList = args[2];
                }
                DisplayPreamble();

                Type type = null;
                switch (testType)
                {
                    case UnitTestType.TEST_CODE:
                        type = GetType(testName);
                        if (type != null)
                        {
                            Object instance = GetInstance(type, "", emailList);
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
                        break;
                    case UnitTestType.TEST_SCRIPT:
                        if (File.Exists(testName))
                        {
                            tests.Clear();
                            tests.Add(testName);
                            RunTest(tests, emailList);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: No such Test File exists.");
                        }
                        break;
                    case UnitTestType.TEST_FOLDER:
                        if (Directory.Exists(testName))
                        {
                            tests.Clear();

                            foreach (string file in Directory.EnumerateFiles(testName, "*.xml"))
                            {
                                tests.Add(file);
                            }

                            RunTest(tests, emailList);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: No such Test Folder exists.");
                        }
                        break;
                    case UnitTestType.TEST_CODE_ALL:
                    default:
                        //code to run all coded test
                        if (Directory.Exists(testName))
                        {
                            tests.Clear();

                            foreach (string file in Directory.EnumerateFiles(testName, "*.cs"))
                            {
                                tests.Add(testName);
                            }

                            RunTest(tests, emailList);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: No such Test Folder exists.");
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            return ret;
        }

        private static int RunTest(List<String> testName, string emailList)
        {
            int ret = -1;
            try
            {
                Type type = GetType("GlobalProductRun");
                if (type != null)
                {
                    Object instance = GetInstance(type, testName, emailList);
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
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);

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
