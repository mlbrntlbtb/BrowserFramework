using CommonLib.DlkHandlers;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Controls;
using System.Xml.Linq;
using TestRunner.AdvancedScheduler.Model;
using TestRunner.AdvancedScheduler.View;
using TestRunner.Common;

namespace TestRunner.AdvancedScheduler.Presenter
{
    public class ExportPresenter
    {
        private IExportView mView = null;

        private DataSet mExportDataSet = new DataSet();
        private List<string> mWorksheetNames = new List<string>();
        private List<string> mTestSuiteHistoryNames = new List<string>();
        private List<string> mScheduleTestLineupList;
        private List<string> mResultsHistoryList;
        private List<string> mAgentsInformationList;
        private List<string> mCompleteResultsHistoryList;

        public ExportPresenter(IExportView View)
        {
            mView = View;
        } 

        /// <summary>
        /// Converts the Test Lineup data to a data table before export is performed.
        /// </summary>
        /// <param name="testLineup">collection of TestLineupRecord objects</param>      
        public void CreateTestLineupDataTable(ObservableCollection<TestLineupRecord> testLineup)
        {
            DataTable dt = new DataTable();
            dt.TableName = "ScheduleTestLineup";

            try
            {
                if (testLineup.Count > 0)
                {
                    // Iterate through each header in the list.
                    foreach (string header in mScheduleTestLineupList)
                    {
                        dt.Columns.Add(header);
                    }

                    // Iterate through each object in the list and add a new row in the data table.
                    // Once all items in the current object have been assigned, add the row to the data table.
                    foreach (var item in testLineup)
                    {
                        var row = dt.NewRow();

                        row[dt.Columns[0]] = item.TestSuiteToRun.Name;
                        row[dt.Columns[1]] = item.Enabled;
                        row[dt.Columns[2]] = item.Status;
                        row[dt.Columns[3]] = item.RunningAgent.Name;
                        row[dt.Columns[4]] = item.Environment;
                        row[dt.Columns[5]] = item.Browser.Name;
                        row[dt.Columns[6]] = item.StartTime.ToShortTimeString();
                        row[dt.Columns[7]] = item.Recurrence;
                        if (item.Recurrence == Enumerations.RecurrenceType.Daily || item.Recurrence == Enumerations.RecurrenceType.Weekdays)
                        {
                            row[dt.Columns[8]] = "";
                        }
                        else if (item.Recurrence == Enumerations.RecurrenceType.Monthly)
                        {
                            row[dt.Columns[8]] = "Day " + ConvertDateToDay(item.Schedule);
                        }
                        else
                        {
                            row[dt.Columns[8]] = item.Schedule.ToString().Split(' ')[0];
                        }

                        // add the row
                        dt.Rows.Add(row);
                    }

                    mExportDataSet.Tables.Add(dt);
                    mWorksheetNames.Add("Test Lineup");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Converts the Results History data to a data table before export is performed.
        /// </summary>
        /// <param name="testLineup">collection of TestLineupRecord objects</param> 
        public void CreateResultsHistoryDataTable(ObservableCollection<TestLineupRecord> testLineup)
        {
            DataTable dt = new DataTable();
            dt.TableName = "ResultsHistory";

            try
            {
                if (testLineup.Count > 0)
                {
                    // Iterate through each header in the list.
                    foreach (string header in mResultsHistoryList)
                    {
                        dt.Columns.Add(header);
                    }

                    // Iterate through each object in the list and add a new row in the data table.
                    // Once all items in the current object have been assigned, add the row to the data table.
                    foreach (var item in testLineup)
                    {
                        var row = dt.NewRow();

                        row[dt.Columns[0]] = item.TestSuiteToRun.Name;

                        if (item.LastRunResult != null)
                        {
                            row[dt.Columns[1]] = item.LastRunResult.ExecutionDate;
                            row[dt.Columns[2]] = item.LastRunResult.PassRate;
                            row[dt.Columns[3]] = item.LastRunResult.CompletionRate;
                            row[dt.Columns[4]] = item.LastRunResult.PassedTests;
                            row[dt.Columns[5]] = item.LastRunResult.FailedTests;
                            row[dt.Columns[6]] = item.LastRunResult.NotRunTests;
                            row[dt.Columns[7]] = item.LastRunResult.TotalTests;
                            row[dt.Columns[8]] = item.LastRunResult.Environment;
                            row[dt.Columns[9]] = item.LastRunResult.Browser;
                            row[dt.Columns[10]] = item.LastRunResult.RunNumber;
                            row[dt.Columns[11]] = item.LastRunResult.StartTime;
                            row[dt.Columns[12]] = item.LastRunResult.EndTime;
                            row[dt.Columns[13]] = item.LastRunResult.Duration.ToString();
                            row[dt.Columns[14]] = item.LastRunResult.RunningAgent;
                            row[dt.Columns[15]] = item.LastRunResult.UserName;
                            row[dt.Columns[16]] = item.LastRunResult.OperatingSystem;
                        }

                        // add the row
                        dt.Rows.Add(row);
                    }
                       
                    mExportDataSet.Tables.Add(dt);
                    mWorksheetNames.Add("Test Results");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Converts the Results History data to a data table before export is performed.
        /// </summary>
        /// <param name="testLineup">collection of all History objects (complete History)</param> 
        public void CreateCompleteResultsHistoryDataTable(ObservableCollection<ExecutionHistory> execHistory)
        {
            DataTable dt = new DataTable();
            dt.TableName = "ResultsHistory";
            int idx = 0;

            try
            {
                if (execHistory.Count > 0)
                {
                    // Iterate through each header in the list.
                    foreach (string header in mCompleteResultsHistoryList)
                    {
                        dt.Columns.Add(header);
                    }

                    // Iterate through each object in the list and add a new row in the data table.
                    // Once all items in the current object have been assigned, add the row to the data table.
                    foreach (var item in execHistory)
                    {
                        var row = dt.NewRow();

                        row[dt.Columns[0]] = mTestSuiteHistoryNames[idx];
                        row[dt.Columns[1]] = item.ExecutionDate;
                        row[dt.Columns[2]] = item.PassRate;
                        row[dt.Columns[3]] = item.CompletionRate;
                        row[dt.Columns[4]] = item.PassedTests;
                        row[dt.Columns[5]] = item.FailedTests;
                        row[dt.Columns[6]] = item.NotRunTests;
                        row[dt.Columns[7]] = item.TotalTests;
                        row[dt.Columns[8]] = item.Environment;
                        row[dt.Columns[9]] = item.Browser;
                        row[dt.Columns[10]] = item.RunNumber;
                        row[dt.Columns[11]] = item.StartTime;
                        row[dt.Columns[12]] = item.EndTime;
                        row[dt.Columns[13]] = item.Duration;
                        row[dt.Columns[14]] = item.RunningAgent;
                        row[dt.Columns[15]] = item.UserName;
                        row[dt.Columns[16]] = item.OperatingSystem;

                        // add the row
                        dt.Rows.Add(row);
                        idx++;
                    }

                    mExportDataSet.Tables.Add(dt);
                    mWorksheetNames.Add("Test Results");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Converts the Agents Info data to a data table before export is performed.
        /// </summary>
        /// <param name="agentsLineup">collection of Agent objects</param>
        /// <param name="agentsHistory">collection of Execution History objects per agent</param>
        public void CreateAgentInfoDataTable(ObservableCollection<Agent> agentsLineup, ObservableCollection<ExecutionHistory> agentsHistory)
        {
            DataTable dt = new DataTable();
            dt.TableName = "AgentsInformation";

            try
            {
                if (agentsLineup.Count > 0)
                {
                    // Iterate through each header in the list.
                    foreach (string header in mAgentsInformationList)
                    {
                        dt.Columns.Add(header);
                    }

                    // Iterate through each object in the list and add a new row in the data table.
                    // Once all items in the current object have been assigned, add the row to the data table.
                    foreach (var item in agentsLineup.OrderBy(n => n.Name))
                    {
                        var row = dt.NewRow();

                        row[dt.Columns[0]] = item.Name;
                        row[dt.Columns[1]] = item.Status;
                        row[dt.Columns[2]] = item.Description;
                        row[dt.Columns[3]] = item.Runtime;
                        row[dt.Columns[4]] = item.AgentStats.AvailableDiskSpace;
                        row[dt.Columns[5]] = item.AgentStats.PeakMemoryUsage;
                        row[dt.Columns[6]] = item.AgentStats.SystemMemory;
                        row[dt.Columns[7]] = item.AgentStats.OperatingSystem;

                        // Iterate through each ExecutionHistory object & cross check with Agent
                        foreach (var agentItem in agentsHistory)
                        {
                            if (agentItem.RunningAgent == item.Name)
                            {
                                if (row.RowState == DataRowState.Added)
                                {
                                   row = dt.NewRow();
                                }

                                row[dt.Columns[0]] = item.Name;
                                row[dt.Columns[1]] = item.Status;
                                row[dt.Columns[2]] = item.Description;
                                row[dt.Columns[3]] = item.Runtime;
                                row[dt.Columns[4]] = item.AgentStats.AvailableDiskSpace;
                                row[dt.Columns[5]] = item.AgentStats.PeakMemoryUsage;
                                row[dt.Columns[6]] = item.AgentStats.SystemMemory;
                                row[dt.Columns[7]] = item.AgentStats.OperatingSystem;

                                row[dt.Columns[8]] = Path.GetFileName(agentItem.SuiteFullPath);
                                row[dt.Columns[9]] = agentItem.ExecutionDate;
                                row[dt.Columns[10]] = agentItem.PassRate;
                                row[dt.Columns[11]] = agentItem.PassedTests;
                                row[dt.Columns[12]] = agentItem.FailedTests;
                                row[dt.Columns[13]] = agentItem.NotRunTests;
                                row[dt.Columns[14]] = agentItem.TotalTests;
                                row[dt.Columns[15]] = agentItem.RunNumber;
                                row[dt.Columns[16]] = agentItem.StartTime;
                                row[dt.Columns[17]] = agentItem.EndTime;
                                row[dt.Columns[18]] = agentItem.Duration;
                                row[dt.Columns[19]] = agentItem.UserName;

                                // add the row
                                dt.Rows.Add(row);
                            }
                        }
                        // add the row for an agent without any execution history
                        if (row.RowState != DataRowState.Added) 
                        {
                            dt.Rows.Add(row);
                        }
                    }

                    mExportDataSet.Tables.Add(dt);
                    mWorksheetNames.Add("Agents Information");
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Perform the actual export and saving by calling the method in the DlkExcelHelper class.
        /// </summary>
        /// <param name="fileName">Excel file name</param>
        public void SaveExcelFile(string fileName)
        {
            try
            {
                DlkExcelHelper.IsColumnHeaderStyled = true;
                DlkExcelHelper.ExportAndSaveToExcel(mExportDataSet, fileName, mWorksheetNames);

                //Clear data set and sheetnames list
                mWorksheetNames.Clear();
                mExportDataSet.Tables.Clear();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Populate the header list.
        /// </summary>
        public void GetDataColumnHeaders()
        {
            // Schedule Test Line up
            mScheduleTestLineupList = new List<string>();
            mScheduleTestLineupList.Add("Test Suite");
            mScheduleTestLineupList.Add("Enabled");
            mScheduleTestLineupList.Add("Status");
            mScheduleTestLineupList.Add("Agent");
            mScheduleTestLineupList.Add("Environment");
            mScheduleTestLineupList.Add("Browser");
            mScheduleTestLineupList.Add("Start Time");
            mScheduleTestLineupList.Add("Recurrence");
            mScheduleTestLineupList.Add("When?");

            // Results History
            mResultsHistoryList = new List<string>();
            mResultsHistoryList.Add("Test Suite");
            mResultsHistoryList.Add("Execution Date");
            mResultsHistoryList.Add("Pass Rate (%)");
            mResultsHistoryList.Add("Completion (%)");
            mResultsHistoryList.Add("Passed");
            mResultsHistoryList.Add("Failed");
            mResultsHistoryList.Add("Not Run");
            mResultsHistoryList.Add("Total");
            mResultsHistoryList.Add("Environment");
            mResultsHistoryList.Add("Browser");
            mResultsHistoryList.Add("Run No.");
            mResultsHistoryList.Add("Start Time");
            mResultsHistoryList.Add("End Time");
            mResultsHistoryList.Add("Duration");
            mResultsHistoryList.Add("Machine Name");
            mResultsHistoryList.Add("User Name");
            mResultsHistoryList.Add("Operating System");

            // Complete Results History
            mCompleteResultsHistoryList = new List<string>();
            mCompleteResultsHistoryList.Add("Test Suite");
            mCompleteResultsHistoryList.Add("Execution Date");
            mCompleteResultsHistoryList.Add("Pass Rate (%)");
            mCompleteResultsHistoryList.Add("Completion (%)");
            mCompleteResultsHistoryList.Add("Passed");
            mCompleteResultsHistoryList.Add("Failed");
            mCompleteResultsHistoryList.Add("Not Run");
            mCompleteResultsHistoryList.Add("Total");
            mCompleteResultsHistoryList.Add("Environment");
            mCompleteResultsHistoryList.Add("Browser");
            mCompleteResultsHistoryList.Add("Run No.");
            mCompleteResultsHistoryList.Add("Start Time");
            mCompleteResultsHistoryList.Add("End Time");
            mCompleteResultsHistoryList.Add("Duration");
            mCompleteResultsHistoryList.Add("Machine Name");
            mCompleteResultsHistoryList.Add("User Name");
            mCompleteResultsHistoryList.Add("Operating System");

            // Agents Information
            mAgentsInformationList = new List<string>();
            mAgentsInformationList.Add("Agent");
            mAgentsInformationList.Add("Status");
            mAgentsInformationList.Add("Description");
            mAgentsInformationList.Add("Agent Runtime");
            mAgentsInformationList.Add("Disk Space (GB)");
            mAgentsInformationList.Add("Peak Memory Usage (MB)");
            mAgentsInformationList.Add("Total RAM (MB)");
            mAgentsInformationList.Add("Operating System");
            // Execution history per agent
            mAgentsInformationList.Add("Test Suite");
            mAgentsInformationList.Add("Execution Date");
            mAgentsInformationList.Add("Pass Rate (%)");
            mAgentsInformationList.Add("Passed");
            mAgentsInformationList.Add("Failed");
            mAgentsInformationList.Add("Not Run");
            mAgentsInformationList.Add("Total");
            mAgentsInformationList.Add("Run No.");
            mAgentsInformationList.Add("Start Time");
            mAgentsInformationList.Add("End Time");
            mAgentsInformationList.Add("Duration");
            mAgentsInformationList.Add("User Name");
        }

        /// <summary>
        /// Retrieve history data for all suites in the main grid
        /// </summary>
        /// <param name="grid">Agent Lineup grid</param>
        /// <returns>list of ExecutionHistory objects for all agents</returns>       
        public List<ExecutionHistory> RetrieveAgentHistoryGridData(DataGrid grid)
        {
            List<ExecutionHistory> gridData = new List<ExecutionHistory>();
            var itemsSource = grid.ItemsSource as IEnumerable;

            foreach (var item in itemsSource)
            {
                Agent agentItem = (Agent)(item);
                DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirSuiteResults);
                foreach (FileInfo fi in new List<FileInfo>(di.GetFiles("*.dat", SearchOption.AllDirectories)).FindAll(x => x.CreationTime >= DateTime.Now.AddDays(-30d)))
                {
                    Dictionary<string, string> attribs = DlkTestSuiteResultsFileHandler.GetSummaryAttributeValues(fi.FullName);
                    if (attribs[DlkTestSuiteInfoAttributes.MACHINENAME] == agentItem.Name)
                    {
                        string suitePath = attribs[DlkTestSuiteInfoAttributes.PATH];
                        if (string.IsNullOrEmpty(suitePath))
                        {
                            suitePath = attribs[DlkTestSuiteInfoAttributes.NAME] + ".xml";
                        }
                        gridData.Add(AgentUtility.GenerateExecutionHistoryFromSuiteInstance(suitePath, fi.FullName, attribs, agentItem));
                    }
                }
            }
            return gridData;
        }

        /// <summary>
        /// Retrieve history data for all suites in the main grid
        /// </summary>
        /// <param name="grid">Test Schedule grid</param>
        /// <returns>list of ExecutionHistory objects for all suites</returns>       
        public List<ExecutionHistory> RetrieveScheduleHistoryGridData(DataGrid grid)
        {
            List<ExecutionHistory> gridData = new List<ExecutionHistory>();
            var itemsSource = grid.ItemsSource as IEnumerable;

            foreach (var item in itemsSource)
            {
                TestLineupRecord suite = (TestLineupRecord)(item);
                var suiteNameWithoutExtension = Path.GetFileNameWithoutExtension(suite.TestSuiteToRun.Name);
                var productFolderName = DlkTestRunnerCmdLib.GetProductFolder(suite.TestSuiteToRun.Path);
                var suiteResultsDirectory = suite.TestSuiteToRun.Path.Substring(0, suite.TestSuiteToRun.Path.IndexOf(productFolderName) + productFolderName.Length) + "\\Framework\\SuiteResults\\";
                var mSuiteResultsFolder = suiteResultsDirectory + suiteNameWithoutExtension;
                if (Directory.Exists(mSuiteResultsFolder))
                {
                    var instances = new DirectoryInfo(mSuiteResultsFolder).GetDirectories();
                    for (int i = 0; i < instances.Count(); i++)
                    {
                        var instance = instances[i];
                        var suiteResult = GenerateExecutionHistoryFromSuiteInstance(suite.TestSuiteToRun.Path, mSuiteResultsFolder, instance.Name, suite);

                        if (suiteResult != null)
                        {
                           mTestSuiteHistoryNames.Add(suite.TestSuiteToRun.Name);
                           gridData.Add(suiteResult);
                        }
                    }
                }         
            }
            return gridData;
        }

        /// <summary>
        /// Generates a suite result/ExecutionHistory
        /// </summary>
        /// <param name="mSuiteName">Name of suite</param>
        /// <param name="mSuiteResultsFolder">Full path of all results folder</param>
        /// <param name="instance">Target results folder</param>
        /// <param name="Owner">Owner object of history</param>
        /// <returns>ExecutionHistory results for a suite</returns>
        private ExecutionHistory GenerateExecutionHistoryFromSuiteInstance(String suitePath, String mSuiteResultsFolder, String instance, object Owner)
        {
            ExecutionHistory historyEntry = null;
            var resultsPath = mSuiteResultsFolder + @"\" + instance;
            var summaryPath = resultsPath + @"\summary.dat";
            if (File.Exists(summaryPath))
            {
                Dictionary<string, string> summaryDict = DlkTestSuiteResultsFileHandler.GetSummaryAttributeValues(summaryPath);
                var executionDate = DateTime.ParseExact(instance.Substring(0, 14), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                var duration = TimeSpan.Parse(summaryDict[DlkTestSuiteInfoAttributes.ELAPSED]);
                historyEntry = new ExecutionHistory()
                {
                    SuiteFullPath = suitePath,
                    ResultsFolderFullPath = resultsPath,
                    ExecutionDate = executionDate.Date.ToString("MM/dd/yyyy"),
                    Duration = duration.ToString("hh\\:mm\\:ss\\.ff"),
                    StartTime = executionDate.Subtract(duration).ToString("hh:mm:ss.ff tt"),
                    EndTime = executionDate.ToString("hh:mm:ss.ff tt"),
                    TotalTests = summaryDict[DlkTestSuiteInfoAttributes.TOTAL],
                    PassedTests = summaryDict[DlkTestSuiteInfoAttributes.PASSED],
                    FailedTests = summaryDict[DlkTestSuiteInfoAttributes.FAILED],
                    NotRunTests = summaryDict[DlkTestSuiteInfoAttributes.NOTRUN],
                    Id = Guid.NewGuid().ToString(),
                    RunningAgent = summaryDict[DlkTestSuiteInfoAttributes.MACHINENAME],
                    OperatingSystem = summaryDict[DlkTestSuiteInfoAttributes.OPERATINGSYSTEM],
                    UserName = summaryDict[DlkTestSuiteInfoAttributes.USERNAME],
                    RunNumber = summaryDict[DlkTestSuiteInfoAttributes.RUNNUMBER],
                    Environment = summaryDict[DlkTestSuiteInfoAttributes.ENVIRONMENT],
                    Browser = summaryDict[DlkTestSuiteInfoAttributes.BROWSER],
                    Parent = Owner
                };
            }

            return historyEntry;
        }

        /// <summary>
        /// Convert the schedule object to get the day string
        /// </summary>
        /// <param name="date">schedule obj</param>
        /// <returns>Day in string format</returns>
        private string ConvertDateToDay(object date)
        {
            string day = string.Empty;

            DateTime dateTime;
            if (DateTime.TryParse(date.ToString(), out dateTime))
            {
                day = dateTime.Day.ToString();
            }

            return day;
        }

    }
}
