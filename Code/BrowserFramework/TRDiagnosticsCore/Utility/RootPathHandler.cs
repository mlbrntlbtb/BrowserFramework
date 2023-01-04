using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TRDiagnosticsCore.Utility
{
    public static class RootPathHandler
    {
        /// <summary>
        /// Retrieves browser framework path
        /// </summary>
        public static List <string> GetBrowserFrameworkPath(string selectedDrive, bool isConsole = false)
        {
            //Retrieve all main directories first
            if (isConsole)
            {
                ConsolePrinter.Print("Searching for available Test Runner directories. This may take a few minutes. Please wait... ");
            }
            
            List<string> mainDirectories = new List<string>();
            List<string> browserFrameworkPaths = new List<string>();
            mainDirectories.AddRange(Directory.GetDirectories(selectedDrive.ToString(), "*.*", SearchOption.TopDirectoryOnly)
                .Where(x=>!x.Contains("Windows") && !x.Contains("Program Files")));
            foreach (var directory in mainDirectories)
            {
                browserFrameworkPaths.AddRange(GetSubDirectories(directory, "*.*"));
            }
            //Filter all directories with browser framework main directories
            browserFrameworkPaths = browserFrameworkPaths.Where(x => x.Contains(@"BrowserFramework\Tools\TestRunner"))
                .Where(x => !x.Contains(@"TestRunner\")).ToList();

            //Filter test runner path with test runner application only
            List<string> testRunnerPathsWithExe = new List<string>();
            foreach (string trpath in browserFrameworkPaths)
            {
                if (File.Exists(Path.Combine(trpath, "TestRunner.exe")))
                    testRunnerPathsWithExe.Add(trpath);
            }

            if (isConsole)
            {
                ConsolePrinter.PrintLine();
            }
            return testRunnerPathsWithExe;
        }

        /// <summary>
        /// Retrieves sub directories under specified directory
        /// </summary>
        private static List<string> GetSubDirectories(string path, string pattern)
        {
            //Recursion for getting subdirectories with BrowserFramework and/or Tools pattern
            List<string> mainDirectories = new List<string>();
            List<string> testRunnerPaths = new List<string>();
            try
            {
                mainDirectories.AddRange(Directory.GetDirectories(path, pattern, SearchOption.TopDirectoryOnly));
                testRunnerPaths.AddRange(Directory.GetDirectories(path, pattern, SearchOption.TopDirectoryOnly).Where(t => t.Contains("BrowserFramework")));
                foreach (var directory in mainDirectories)
                {
                    testRunnerPaths.AddRange(GetSubDirectories(directory, pattern));
                }
            }
            catch
            {
                //Ignore unauthorized folders
            }
            return testRunnerPaths;
        }
    }
}
