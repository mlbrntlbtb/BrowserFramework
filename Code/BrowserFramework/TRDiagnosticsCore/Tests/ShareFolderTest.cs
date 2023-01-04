using System;
using System.Collections.Generic;
using System.Management;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Linq;
using System.Diagnostics;

namespace TRDiagnosticsCore.Tests
{
    public class ShareFolderTest : DiagnosticTest
    {
        private const string SHARED_FOLDER_NAME = "BrowserFramework";
        private SharedFolder trSharedFolder;

        public ShareFolderTest() : base() { }

        protected override void DefineTestName()
        {
            TotalTestCount = 3;
            TestName = "Shared Folder And Permissions Check";
        }

        protected override void PerformCheck(out string ErrorMessage)
        {
            try
            {
                /* PERFORM CHECKS HERE */
                ErrorMessage = string.Empty;
                DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for BrowserFramework shared folder");
                DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for BrowserFramework shared folder...");

                List<SharedFolder> sharedFolders = GetSharedFolders();
                List<SharedFolder> foundSharedFolders = sharedFolders.Where(file => file.ShareName == SHARED_FOLDER_NAME
                                        || file.ShareName.Contains(SHARED_FOLDER_NAME)
                                        || file.DirectoryPath.Contains(SHARED_FOLDER_NAME)).ToList();

                trSharedFolder = sharedFolders.FirstOrDefault(folder => TestRunnerPath.Contains(folder.DirectoryPath.Replace("Products", "")));

                if (trSharedFolder == null)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "BrowserFramework is not shared. Links from scheduled suite email notifications are not accessible.");
                    DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for user admin rights");

                    //check if user can share this folder
                    if (!IsCurrentUserAdmin())
                    {
                        DiagnosticLogger.LogResult(Logger.MessageType.ERROR, "Current user has no admin rights to share this folder.");
                    }
                }
                else
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "BrowserFramework shared folder found.");
                }
                
                if (foundSharedFolders.Count > 1)
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.WARNING, "Multiple BrowserFramework shared folders found.");
                }

                //check folder permission if shared folder found
                if (trSharedFolder != null) 
                {
                    DiagnosticLogger.LogResult(Logger.MessageType.COUNTER, "Checking for BrowserFramework shared folder permissions");
                    DiagnosticLogger.LogResult(Logger.MessageType.INFO, "Checking for BrowserFramework shared folder permissions...");

                    List<FolderPermission> permissions = GetFolderPermissions(trSharedFolder.DirectoryPath);

                    if (permissions.FirstOrDefault(permerission => permerission.Identity.ToLower().Contains("everyone")) == null)
                        DiagnosticLogger.LogResult(Logger.MessageType.WARNING, "Insufficient permission for BrowserFramework folder. Some user(s) cannot access this folder.");
                    else
                        DiagnosticLogger.LogResult(Logger.MessageType.SUCCESS, "BrowserFramework shared folder permission passed.");
                }
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

            /* IDENTIFY RECOMMENDATION HERE */
            switch (Record.MessageType)
            {
                case Logger.MessageType.ERROR:
                    if (Record.MessageDetails.Contains("is not shared"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "Make sure File and Printer sharing is enabled and run SharedFolderFull.bat in BrowserFramework/Tools/TestRunner folder.");
                    }
                    else if (Record.MessageDetails.Contains("no admin rights"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "Grant current user access as Administrator. If you don't have access rights to perform this, contact your System Administrator.");
                    }
                    break;
                case Logger.MessageType.WARNING:
                    if (Record.MessageDetails.Contains("Multiple"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, "Remove duplicate BrowserFramework shared folders to avoid error on accessing links to error screenshots in scheduled suite email notifications.");
                    }
                    else if (Record.MessageDetails.Contains("Insufficient permission"))
                    {
                        ret = new LogRecord(Logger.MessageType.RECOMMENDATION, $"To allow access for everyone, follow these instructions: Go to {trSharedFolder.DirectoryPath} > Right click the Products folder > Click Properties > Go to Sharing Tab and click Share > In the Network Access dialog that appears, add Everyone and click Share.");
                    }
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Get list of shared folders on current machine
        /// </summary>
        /// <returns>List of SharedFolder class</returns>
        private List<SharedFolder> GetSharedFolders()
        {
            try
            {
                List<SharedFolder> sharedFolders = new List<SharedFolder>();
                ManagementScope scope = new ManagementScope(@"\\.\root\cimv2");
                scope.Connect();
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_LogicalShareSecuritySetting");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();

                foreach (ManagementObject sharedFolder in queryCollection)
                {
                    string shareName = (string)sharedFolder["Name"];
                    string localPath = string.Empty;
                    ManagementObjectSearcher win32Share = new ManagementObjectSearcher("SELECT Path FROM Win32_share WHERE Name = '" + shareName + "'");
                    foreach (ManagementObject shareData in win32Share.Get())
                    {
                        localPath = (string)shareData["Path"];
                    }
                    sharedFolders.Add(new SharedFolder(shareName, localPath));
                }
                return sharedFolders;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get list of permissions for the specific folder
        /// </summary>
        /// <param name="path">shared folder full path</param>
        /// <returns>List of FolderPermission class</returns>
        private List<FolderPermission> GetFolderPermissions(string path)
        {
            try
            {
                List<FolderPermission> permissions = new List<FolderPermission>();
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                DirectorySecurity dirSec = dirInfo.GetAccessControl();

                foreach (FileSystemAccessRule rule in dirSec.GetAccessRules(true, true, typeof(NTAccount)))
                {
                    permissions.Add(new FolderPermission(rule.IdentityReference.ToString(), rule.FileSystemRights.ToString()));
                }
                return permissions;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if current user has admin privilege to share a folder in current machine
        /// </summary>
        /// <returns>true if admin;false if not</returns>
        private static bool IsCurrentUserAdmin()
        {
            if (IsWorkGroupAdmin() || !IsStandardUser())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if user is a member of WORKGROUP Administrators
        /// </summary>
        /// <returns>true if admin;else false</returns>
        private static bool IsWorkGroupAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// Get list of users in standardusers group
        /// </summary>
        /// <param name="domainUsername">current user domain</param>
        /// <returns>true if current user is a member of standard users group</returns>
        private static bool IsStandardUser()
        {
            string domainUsername = WindowsIdentity.GetCurrent().Name.ToLower();
            string[] groups = new string[] { "users", "guests", "remote desktop users" };

            foreach (string group in groups)
            {
                List<string> members = GetLocalGroupMembers(group);
                if (members.Contains(domainUsername))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get localgroup members
        /// </summary>
        /// <returns>list of users in a localgroup</returns>
        private static List<string> GetLocalGroupMembers(string group)
        {
            List<string> admins = new List<string>();
            bool readUser = false;
            foreach (string lineResult in ExecuteCommand($"localgroup {group}"))
            {
                if (lineResult.Contains("-------------"))
                {
                    readUser = true;
                    continue;
                }

                if (readUser && !string.IsNullOrEmpty(lineResult))
                    admins.Add(lineResult.ToLower());
            }

            return admins;
        }

        /// <summary>
        /// Execute and retrieve result from command prompt
        /// </summary>
        /// <param name="arguments">command argument</param>
        /// <returns>enumerable command result</returns>
        private static IEnumerable<string> ExecuteCommand(string arguments)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "net",
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                yield return proc.StandardOutput.ReadLine();
            }
        }
    }

    /// <summary>
    /// Class for shared folder record
    /// </summary>
    public class SharedFolder
    {
        public SharedFolder(string shareName, string path)
        {
            ShareName = shareName;
            DirectoryPath = path;
        }

        public string ShareName { get; private set; }

        public string DirectoryPath { get; private set; }
    }

    /// <summary>
    /// Class use for folder permission
    /// </summary>
    public class FolderPermission
    {
        public FolderPermission(string identity, string rights)
        {
            Identity = identity;
            FileSystemRights = rights;
        }

        public string Identity { get; private set; }

        public string FileSystemRights { get; private set; }
    }
}
