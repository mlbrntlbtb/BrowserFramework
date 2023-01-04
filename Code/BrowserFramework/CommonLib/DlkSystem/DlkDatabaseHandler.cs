using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace CommonLib.DlkSystem
{
    [ControlType("Database")]
    public static class DlkDatabaseHandler
    { 
        #region DECLARATIONS
        
        private static String mDBType = "";
        private static String mDBServer = "";
        private static String mDBPort = "";
        private static String mDBUserID = "";
        private static String mDBPassword = "";
        private static String mDBId = "";
        private static DataSet dsResult;
        private static String mConnectionString = "";

        #endregion

        #region PROPERTIES

        public static String DBType
        {
          get { return mDBType; }
          set { mDBType = value; }
        }
        
        public static String DBServer
        {
          get { return mDBServer; }
          set { mDBServer = value; }
        }
        
        public static String DBPort
        {
          get { return mDBPort; }
          set { mDBPort = value; }
        }
        
        public static String DBUserID
        {
          get { return mDBUserID; }
          set { mDBUserID = value; }
        }
        
        public static String DBPassword
        {
          get { return mDBPassword; }
          set { mDBPassword = value; }
        }

        public static String DBId
        {
          get { return mDBId; }
          set { mDBId = value; }
        }

        public static DataSet DSResult
        {
            get { return dsResult; }
            set { dsResult = value; }
        }

        public static String ConnectionString
        {
            get { return mConnectionString; }
            set { mConnectionString = value; }
        }
        #endregion

        #region CONSTRUCTOR

        private static void InitializeConnection(String type, String server, String port, String userID, String password, String databaseID)
        {
            DBType = type;
            DBServer = server;
            DBPort = port;
            DBUserID = userID;
            DBPassword = password;
            DBId = databaseID;
        }

        #endregion

        #region KEYWORDS
        [Keyword("DeleteDB")]
        public static void DeleteDB(String ServerName, String DBName)
        {
            try
            {

                String databaseFolder = Path.Combine(DlkEnvironment.mDirProduct, @"UserTestData\Database\");
                Process deleteProc = new Process();
                deleteProc.StartInfo.FileName = Path.Combine(databaseFolder, "DropDatabase.bat");
                deleteProc.StartInfo.Arguments = String.Format("{0} {1}",ServerName,DBName);
                deleteProc.Start();
                deleteProc.WaitForExit();
                deleteProc.Close();
                DlkLogger.LogInfo("Successfully executed DeleteDB().");
            }
            catch (Exception e)
            {
                throw new Exception("DeleteDB failed : " + e.Message, e);
            }
        }

        [Keyword("RestoreDB")]
        public static void RestoreDB(String DBName, String bakPath,String machineName)
        {
            String databaseFolder = Path.Combine(DlkEnvironment.mDirProduct, @"UserTestData\Database\");
            Process restoreProc = new Process();
            //restoreProc.StartInfo.FileName = Path.Combine(databaseFolder, "RestoreDB.bat");
            restoreProc.StartInfo.FileName = Path.Combine(databaseFolder, "OptimizedRestoreDB.bat");
            restoreProc.StartInfo.Arguments = String.Format(DBName+" "+bakPath+" \""+databaseFolder+"\" "+machineName);
            restoreProc.Start();
            restoreProc.WaitForExit();
            restoreProc.Close();
            String lastLine = File.ReadLines(Path.Combine(databaseFolder, @"RestoreLogs\" + DBName + "_Restore.log")).Last();

            //if (!lastLine.Contains("SQLTools finished unsuccessfully"))
            if (lastLine.Contains("RESTORE DATABASE successfully"))
            {
                DlkLogger.LogInfo("Successfully executed ExecuteQuery().");
            }
            else
            {
                throw new Exception("Restore failed. "+ lastLine);
            }
        }

        [Keyword("ExecuteQuery")]
        public static void ExecuteQuery(String DBType, String DBServer, String DBPort, String DBUserID, String DBPassword, String DBId, String SQL, String OutputFile)
        {
            ExecuteDbQuery(DBType, DBServer, DBPort, DBUserID, DBPassword, DBId, SQL, OutputFile, false);
        }
        
        [Keyword("ExecuteQueryAssignToVariable")]
        public static void ExecuteQueryAssignToVariable(String DBType, String DBServer, String DBPort, String DBUserID, String DBPassword, String DBId, String SQL, String VariableName)
        {
            ExecuteDbQuery(DBType, DBServer, DBPort, DBUserID, DBPassword, DBId, SQL, VariableName, true);
        }

        [Keyword("GetRowWithFieldValue", new String[] { "1|text|Field Name|PROJ_ID", "2|text|Field Value|0100"})]
        public static void GetRowWithFieldValue(String FieldName, String Value, String VariableName)
        {
            Boolean bFound = false;

            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsResult.Tables[0].Rows[i];
                if (row[FieldName].ToString() == Value)
                {
                    DlkFunctionHandler.AssignToVariable(VariableName, (i + 1).ToString());
                    DlkLogger.LogInfo("Successfully executed GetRowWithFieldValue()");
                    bFound = true;
                    break;
                }
            }

            if (!bFound)
            {
                throw new Exception("GetRowWithFieldValue() failed. Row with Field '" + FieldName + "' and value '" + Value + "' not found.");
            }
        }

        [Keyword("VerifyRecordExistsWithFieldValue", new String[] { "1|text|Field Name|PROJ_ID", "2|text|Field Value|0100", "3|text|Expected Result|TRUE"})]
        public static void VerifyRecordExistsWithFieldValue(String FieldName, String Value, String IsTrueOrFalse)
        {
            Boolean bFound = false;

            for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
            {
                DataRow row = dsResult.Tables[0].Rows[i];
                if (row[FieldName].ToString() == Value)
                {
                    bFound = true;
                    break;
                }
            }

            DlkAssert.AssertEqual("VerifyRecordExistsWithFieldValue()", Convert.ToBoolean(IsTrueOrFalse), bFound);
        }

        [Keyword("VerifyRecordCount", new String[] { "1|text|ExpectedRecordCount|1" })]
        public static void VerifyRecordCount(String ExpectedCount)
        {
            int iActualRecordCount = 0;
            if (dsResult.Tables.Count > 0)
            {
                iActualRecordCount = dsResult.Tables[0].Rows.Count;
            }
            DlkAssert.AssertEqual("VerifyRecordCount", Convert.ToInt32(ExpectedCount), iActualRecordCount);
        }
        
        [Keyword("GetRecordFieldValue", new String[] { "1|text|Row Index|1", "2|text|Field Name|PROJ_ID"})]
        public static void GetRecordFieldValue(String Row, String FieldName, String VariableName)
        {
            try
            {
                int iRow = Convert.ToInt32(Row) - 1;
                String sFieldValue = dsResult.Tables[0].Rows[iRow][FieldName].ToString();
                DlkFunctionHandler.AssignToVariable(VariableName, sFieldValue);
                DlkLogger.LogInfo("Successfully executed GetRecordFieldValue()");
            }
            catch (Exception e)
            {
                throw new Exception("GetRecordFieldValue() failed: " + e.Message);
            }
        }

        [Keyword("VerifyRecordFieldValue", new String[] { "1|text|Row Index|1", "2|text|Field Name|PROJ_ID", "3|text|Expected Value|0100"})]
        public static void VerifyRecordFieldValue(String Row, String FieldName, String ExpectedValue)
        {
            int iRow = Convert.ToInt32(Row) - 1;
            String sActualValue = "";
            if (dsResult.Tables[0].Rows[iRow][FieldName] == DBNull.Value)
            {
                sActualValue = "NULL";
            }
            else
            {
                sActualValue = dsResult.Tables[0].Rows[iRow][FieldName].ToString();
            }
            DlkAssert.AssertEqual("VerifyRecordFieldValue", ExpectedValue, sActualValue);
        }

        [Keyword("VerifyRecordFieldDateValue", new String[] { "1|text|Row Index|1", "2|text|Field Name|TS_DT", "3|text|Expected Date Value (M/D/YYYY)|1/31/2013"})]
        public static void VerifyRecordFieldDateValue(String Row, String FieldName, String ExpectedDateValue)
        {
            int iRow = Convert.ToInt32(Row) - 1;
            String sActualValue = "";
            String sActual = "";
            String sExpected = "";
            DateTime dtActualValue;
            DateTime dtExpectedValue;
            Boolean bValidFormat = true;

            if (dsResult.Tables[0].Rows[iRow][FieldName] == DBNull.Value)
            {
                sActualValue = "NULL";
            }
            else
            {
                sActualValue = dsResult.Tables[0].Rows[iRow][FieldName].ToString();
            }

            if (bValidFormat = DateTime.TryParse(ExpectedDateValue, out dtExpectedValue))
            {
                dtActualValue = DateTime.Parse(sActualValue);
                dtExpectedValue = DateTime.Parse(ExpectedDateValue);

                sActual = dtActualValue.ToShortDateString();
                sExpected = dtExpectedValue.ToShortDateString();
            }
            else
            {
                throw new Exception("VerifyRecordFieldDateValue() failed. Invalid date format of the expected value parameter: " + ExpectedDateValue );
            }

            DlkAssert.AssertEqual("VerifyRecordFieldDateValue", sExpected, sActual);
        }

        #endregion

        #region METHDODS

        public static void ExecuteDatabase(String Keyword, String[] Parameters)
        {
            switch (Keyword.ToLower())
            {
                case "executequery":
                    ExecuteQuery(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4], Parameters[5], Parameters[6], Parameters[7]);
                    break;
                case "executequeryassigntovariable":
                    ExecuteQueryAssignToVariable(Parameters[0], Parameters[1], Parameters[2], Parameters[3], Parameters[4], Parameters[5], Parameters[6], Parameters[7]);
                    break;
                case "getrowwithfieldvalue":
                    GetRowWithFieldValue(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "getrecordfieldvalue":
                    GetRecordFieldValue(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "verifyrecordfieldvalue":
                    VerifyRecordFieldValue(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "verifyrecordfielddatevalue":
                    VerifyRecordFieldDateValue(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "verifyrecordcount":
                    VerifyRecordCount(Parameters[0]);
                    break;
                case "verifyrecordexistswithfieldvalue":
                    VerifyRecordExistsWithFieldValue(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "restoredb":
                    RestoreDB(Parameters[0], Parameters[1], Parameters[2]);
                    break;
                case "deletedb":
                    DeleteDB(Parameters[0], Parameters[1]);
                    break;
                default:
                    throw new Exception("Unknown function. Function:" + Keyword);
            }

        }

        /// <summary>
        /// Common method used for Db Query execute
        /// </summary>
        /// <param name="DBType"></param>
        /// <param name="DBServer"></param>
        /// <param name="DBPort"></param>
        /// <param name="DBUserID"></param>
        /// <param name="DBPassword"></param>
        /// <param name="DBId"></param>
        /// <param name="SQL"></param>
        /// <param name="OutputName"></param>
        /// <param name="AssignToVariable">if true assign to variable, if false save as outputfile</param>
        private static void ExecuteDbQuery(String DBType, String DBServer, String DBPort, String DBUserID, String DBPassword, String DBId, String SQL, String OutputName, bool AssignToVariable)
        {
            InitializeConnection(DBType, DBServer, DBPort, DBUserID, DBPassword, DBId);
            switch (DBType.ToLower())
            {
                case "mss":
                    MSSQuery(SQL, OutputName, AssignToVariable);
                    break;
                case "ora":
                    ORAQuery(SQL, OutputName, AssignToVariable);
                    break;
                default:
                    throw new Exception("ExecuteQuery() failed. DB Type '" + DBType + "' not supported.");
            }
            DlkLogger.LogInfo("Successfully executed ExecuteQuery().");
        }

        /// <summary>
        /// MS Sql Server
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="sOutputFile"></param>
        /// <param name="assignToVariable"></param>
        private static void MSSQuery(String sSQL, String sOutputName, bool assignToVariable)
        {
            try
            {

                var mServerWithPort = DBServer;
                if (!String.IsNullOrEmpty(DBPort))
                {
                    mServerWithPort = mServerWithPort + "," + DBPort;
                }

                if (DBUserID == "" || DBPassword == "")
                {
                    //windows authentication
                    ConnectionString = string.Format(@"Server={0};Database={1};Trusted_Connection=yes", mServerWithPort, DBId);

                }
                else
                {
                    //sql server authentication
                    ConnectionString = string.Format(@"Server={0};Database={1};User Id={2};Password={3}", mServerWithPort, DBId, DBUserID, DBPassword);
                }

                var conn = new SqlConnection(ConnectionString);
                conn.Open();
                var com = new SqlCommand(sSQL, conn);
                com.CommandTimeout = 0;
                var da = new SqlDataAdapter(com);
                dsResult = new DataSet("queryresults");
                da.Fill(dsResult, "result");

                if (assignToVariable)
                    WriteDataSetToVariable(dsResult, sOutputName);
                else if (!assignToVariable && Path.GetExtension(sOutputName) == ".csv")
                    WriteDataSetToCSV(dsResult, sOutputName);
                else
                    WriteDataSetToXml(dsResult, sOutputName);

                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Execute Query - MSS() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// Oracle 11g/12c
        /// </summary>
        /// <param name="sSQL"></param>
        /// <param name="sOutputFile"></param>
        /// <param name="assignToVariable"></param>
        private static void ORAQuery(String sSQL, String sOutputName, bool assignToVariable)
        {
            try
            {
                
                //set pooling = true for a bit of performance gain
                string oradb = string.Format("User Id={0};Password={1}; Data Source={2}:{3}/{4}; Pooling=true;", DBUserID, DBPassword, DBServer, DBPort, DBId);
                OracleConnection conn = new OracleConnection(oradb);
                conn.Open();
                OracleCommand cmd = new OracleCommand(sSQL, conn);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                dsResult = new DataSet("queryresults");
                da.Fill(dsResult, "result");

                int rowsCount = 0;
                int tableCount = dsResult.Tables.Count;
                if (tableCount > 0)
                    rowsCount = dsResult.Tables[0].Rows.Count;

                DlkLogger.LogInfo($"Execute Query - ORA(): {rowsCount} records retrieved.");

                if (assignToVariable)
                    WriteDataSetToVariable(dsResult, sOutputName);
                else
                {
                    if (tableCount > 0)
                    {
                        if (!assignToVariable && Path.GetExtension(sOutputName) == ".csv")
                            WriteDataSetToCSV(dsResult, sOutputName);
                        else
                            WriteDataSetToXml(dsResult, sOutputName);
                    }
                }

                conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Execute Query - ORA() failed : " + e.Message, e);
            }
        }

        private static void WriteDataSetToXml(DataSet dataSet, string outputFile)
        {
            bool isXMLFile = Regex.IsMatch(outputFile, ".xml", RegexOptions.IgnoreCase);
            DataTable dt;
            dt = dataSet.Tables["result"];

            //uncomment if we want to see columnnames as attribute mapping type
            //foreach (DataColumn dc in dt.Columns)
            //{
            //    dc.ColumnMapping = MappingType.Attribute;
            //}

            if(!string.IsNullOrEmpty(outputFile) && isXMLFile) 
            {
                dataSet.WriteXml(Path.Combine(DlkEnvironment.mDirData + outputFile));
            }
            else
            {
                throw new Exception("OutputFile is invalid.");
                //dataSet.WriteXml(Path.Combine(DlkEnvironment.mDirData + "queryresults.xml"));
            }
        }

        private static void WriteDataSetToVariable(DataSet dataSet, string variableName)
        {
            StringWriter sw = new StringWriter();
            dsResult.WriteXml(sw);
            DlkFunctionHandler.AssignToVariable(variableName, sw.ToString());
        }

        /// <summary>
        /// Writes dataset results from either MSSQuery or ORAQuery to CSV file 
        /// </summary>
        /// <param name="dataSet"> Set of data that lists all results from query execution </param>
        /// <param name="outputFile"> Output file where the CSV will be written </param>
        private static void WriteDataSetToCSV(DataSet dataSet, string outputFile)
        {
            StringBuilder content = new StringBuilder();

            if (dataSet.Tables.Count >= 1)
            {
                DataTable table = dataSet.Tables[0];

                if (table.Rows.Count > 0)
                {
                    DataRow dr1 = (DataRow)table.Rows[0];
                    int intColumnCount = dr1.Table.Columns.Count;
                    int index = 1;

                    //add column names
                    foreach (DataColumn item in dr1.Table.Columns)
                    {
                        content.Append(String.Format("\"{0}\"", item.ColumnName));
                        if (index < intColumnCount)
                            content.Append(",");
                        else
                            content.Append("\r\n");
                        index++;
                    }

                    //add column data
                    foreach (DataRow currentRow in table.Rows)
                    {
                        string strRow = string.Empty;
                        for (int y = 0; y <= intColumnCount - 1; y++)
                        {
                            strRow += "\"" + currentRow[y].ToString().Trim() + "\"";

                            if (y < intColumnCount - 1 && y >= 0)
                                strRow += ",";
                        }
                        content.Append(strRow + "\r\n");
                    }
                }
            }
            string filePath = Path.Combine(DlkEnvironment.mDirData + outputFile);
            System.IO.File.WriteAllText(filePath, content.ToString(), Encoding.UTF8);
        }

        #endregion
    }
}
