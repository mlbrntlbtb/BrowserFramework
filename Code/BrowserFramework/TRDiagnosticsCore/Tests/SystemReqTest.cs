using Microsoft.VisualBasic.Devices;
using System;
using Microsoft.Win32;
using System.Security.Permissions;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;


namespace TRDiagnosticsCore.Tests
{
    public class SystemReqTest : DiagnosticTest
    {
        

        const int MIN_OS_VERSION = 7;
        const int MIN_DISK_SPACE = 100; // MB      
        const int MIN_RAM = 2; // GB
        const int RECOMMENDED_RAM = 4; // GB

        const double GB = 1_073_741_824; // 1 GB = 1,073,741,824 BYTES
        const int MB = 1_048_576; // 1 MB = 1,048,576 Bytes  
        const int MB_TO_GB = 1_024; // 1 GB = 1,024 MB

        public SystemReqTest() : base() => TotalTestCount = 4;

        protected override void DefineTestName() => TestName = "System Requirements Check";

        protected override void PerformCheck(out string ErrorMessage)
        {
            try
            {
                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for Operating System information");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for Operating System information...");
                CheckIfOSSuported();

                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for RAM information");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for RAM information...");
                CheckifRamSuffice();

                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for Available Disk information");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for Available Disk information...");
                CheckifDiskSpaceSuffice();

                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for installed Microsoft .NET Framework version");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for installed Microsoft .NET Framework version...");
                CheckDotnetVersion();

                ErrorMessage = string.Empty;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                throw new Exception(e.Message);
            }
        }
        protected override LogRecord IdentifyRecommendation(LogRecord Record)
        {
            LogRecord ret = new LogRecord(Logger.MessageType.NULL, "No recommendation available.");

            switch (Record.MessageType)
            {
                case Logger.MessageType.WARNING:
                    if (Record.MessageDetails.Contains("RAM"))
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, $"It is recommended to upgrade your RAM to at least {RECOMMENDED_RAM} GB for better performance.");

                    if (Record.MessageDetails.Contains("Disk Space"))
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, $"Make sure to have at least {MIN_DISK_SPACE} MB of remaining Disk Space in your machine.");

                    break;

                case Logger.MessageType.ERROR:
                    if (Record.MessageDetails.Contains("Operating System"))
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, $"Make sure to have Microsoft Windows {MIN_OS_VERSION} or later installed in your machine.");

                    if (Record.MessageDetails.Contains(".NET"))
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, $"Make sure to have Microsoft .NET Framework 4.5 or later version installed in your machine.");

                    if (Record.MessageDetails.Contains("RAM"))
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, $"Make sure to upgrade your RAM to at least {RECOMMENDED_RAM} GB for better performance.");

                    break;
            }

            return ret;
        }

        /// <summary>
        /// Checks if the installed Operating Sytem is Microsoft Windows 7 or later
        /// </summary>
        private void CheckIfOSSuported()
        {
            try
            {
                var os = new ComputerInfo().OSFullName;
                var match = Regex.Match(os, @"[+-]?\d+(\.\d+)?");
                if(double.TryParse(match.Value, out double version))
                {
                    if (version >= MIN_OS_VERSION)
                        DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, $"Operating System {os} is installed.");
                    else
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, $"Operating System Microsoft Windows {MIN_OS_VERSION} or later is not detected.");
                }
                else throw new Exception("Failed to extract Operating System information.");
            }
            catch
            {
                throw new Exception("Failed to extract Operating System information.");
            }
        }

        /// <summary>
        /// Checks if available Disk Space meets the minimum requirement
        /// </summary>
        private void CheckifDiskSpaceSuffice()
        {
            try
            {
                var trDrive = TestRunnerPath.Substring(0, 1);
                DriveInfo cDrive = new DriveInfo(trDrive);
                var availSpace = Convert.ToDouble(cDrive.AvailableFreeSpace);

                DiagnosticLogger.LogResult(Logger.MessageType.INFO, $"Available Disk Space: {Math.Round(availSpace / GB,1)} GB");

                if (availSpace >= MIN_DISK_SPACE * MB)
                    DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, $"Available Disk Space meets the minimum requirement of {MIN_DISK_SPACE} MB.");
                else
                    DiagnosticLogger.LogResult(Logger.MessageType.WARNING, $"Available Disk Space does not meet the minimum requirement of {MIN_DISK_SPACE} MB.");
            }
            catch
            {
                throw new Exception("Failed to extract Disk Space information.");
            }

        }

        /// <summary>
        /// Checks if RAM meets the minimum requirement
        /// </summary>
        private void CheckifRamSuffice()
        {
            try
            {
                double totalRAMNative = new ComputerInfo().TotalPhysicalMemory;
                var totalMemory = Math.Round(totalRAMNative / Math.Pow(1024, (int)2), 2);

                string message = string.Empty;

                if (totalMemory < MIN_RAM * MB_TO_GB)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, $"RAM: {Math.Round(totalMemory / MB_TO_GB)} GB");
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, $"Available RAM does not meet the minimum requirement of {MIN_RAM} GB.");
                }
                else if (totalMemory >= MIN_RAM * MB_TO_GB && totalMemory <= RECOMMENDED_RAM * MB_TO_GB)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, $"RAM: {Math.Round(totalMemory / MB_TO_GB)} GB");
                    DiagnosticLogger.LogResult(Logger.MessageType.WARNING, $"Available RAM meets the minimum requirement of {MIN_RAM} GB. However, it is below the recommended size of {RECOMMENDED_RAM} GB.");
                }
                else if (totalMemory > RECOMMENDED_RAM * MB_TO_GB)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, $"RAM: {Math.Round(totalMemory / MB_TO_GB)} GB");
                    DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, $"Available RAM meets the recommended requirement of {RECOMMENDED_RAM} GB.");
                }
            }
            catch
            {
                throw new Exception("Failed to extract RAM information.");
            }

        }

        /// <summary>
        /// Checks if Microsoft DotNet Framework 4.5 or later versions is installed.
        /// </summary>
        private void CheckDotnetVersion()
        {
            try
            {
                const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

                using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
                {
                    if (ndpKey != null && ndpKey.GetValue("Release") != null)
                    {
                        var version = CheckForDotnetVersion((int)ndpKey.GetValue("Release"));
                        if (version == null)
                            DiagnosticLogger.LogResult(Logger.MessageType.ERROR, $"Microsoft .NET Framework version 4.7 or later is not detected.");
                        else
                            DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, $"Microsoft .NET Framework version {version} is installed.");
                        
                    }
                    else DiagnosticLogger.LogResult(Logger.MessageType.ERROR, $"Microsoft .NET Framework version 4.7 or later is not detected.");
                }

                string CheckForDotnetVersion(int releaseKey)
                {
                    if (releaseKey >= 528040)
                        return "4.8";
                    if (releaseKey >= 460798)
                        return "4.7";

                    // less than 4.7
                    return null;
                }
            }
            catch
            {
                throw new Exception("Failed to extract Microsoft .NET version.");
            }
        }
    }
}
