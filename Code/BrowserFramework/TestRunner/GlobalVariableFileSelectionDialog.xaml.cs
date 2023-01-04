using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data;
using System.IO;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.Common;
using CommonLib.DlkHandlers;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for GlobalVariableFileSelectionDialog.xaml
    /// </summary>
    public partial class GlobalVariableFileSelectionDialog : Window
    {
        #region CONSTRUCTOR
        public GlobalVariableFileSelectionDialog(MainWindow Owner, string SuiteGlobalVarName)
        {
            InitializeComponent();
            this.Owner = Owner;
            SuiteGlobalVarFileName = SuiteGlobalVarName;
            Initialize();
        }
        #endregion

        #region PUBLIC MEMBERS
        public string SelectedGlobalVarFile = "";
        #endregion

        #region DECLARATIONS
        List<string> listFiles = new List<string>();
        List<string> listFileNames = new List<string>();
        DataTable varData;
        string SuiteGlobalVarFileName = String.Empty;
        string globalVarFolder = String.Empty;
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Initializes form and its members
        /// </summary>
        private void Initialize()
        {
            globalVarFolder = Path.Combine(DlkEnvironment.mDirProduct, @"UserTestData\Data\");
            listFiles = Directory.GetFiles(globalVarFolder, "*.csv").ToList();
            listFiles.RemoveAll(x => !Path.GetFileName(x).ToLower().StartsWith("global"));

            if (!listFiles.Any())
            {
                DlkUserMessages.ShowWarning(DlkUserMessages.WRN_NO_GLOBAL_VAR_FILE + globalVarFolder.TrimEnd('\\') + DlkUserMessages.WRN_NO_GLOBAL_VAR_FILE_SUFFIX);
                string filePath = globalVarFolder + "GlobalVar.csv";
                StringBuilder defaultContent = new StringBuilder();
                defaultContent.AppendLine("VarName,Value");
                defaultContent.AppendLine("Test,Test Value");
                File.WriteAllText(filePath, defaultContent.ToString());
                listFiles = Directory.GetFiles(globalVarFolder, "*.csv").ToList();
                listFiles.RemoveAll(x => !Path.GetFileName(x).ToLower().StartsWith("global"));
            }

            listFiles.Sort();

            foreach (string filePath in listFiles)
            {
                listFileNames.Add(Path.GetFileNameWithoutExtension(filePath));
            }

            cboGlobalVarFile.DataContext = listFileNames;
            cboGlobalVarFile.SelectedIndex = 0;

            if (SuiteGlobalVarFileName == String.Empty)
            {
                return;
            }

            if (listFileNames.Contains(SuiteGlobalVarFileName))
            {
                cboGlobalVarFile.SelectedIndex = listFileNames.IndexOf(SuiteGlobalVarFileName);
            }
            else
            {
                DlkUserMessages.ShowWarning(DlkUserMessages.WRN_GLOBAL_VAR_FILE_NOT_FOUND_PREFIX + SuiteGlobalVarFileName + DlkUserMessages.WRN_GLOBAL_VAR_FILE_NOT_FOUND_SUFFIX + globalVarFolder.TrimEnd('\\'));
            }
        }

        /// <summary>
        /// Changes grid content based on selected global variable file
        /// </summary>
        /// <param name="fileName">Global variable file name</param>
        private void RefreshVariableGrid(string fileName = "")
        {
            varData = new DataTable();
            int fileIndex = listFileNames.IndexOf(fileName);
            try
            {
                varData = DlkCSVHelper.CSVParse(listFiles[fileIndex]);
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("Variable assignment failed:" + e);
            }

            if (IsInvalidGlobalVarFileFormat(varData))
            {
                varData.Clear();
            }

            dgGlobalVariables.DataContext = varData.DefaultView;
        }

        /// <summary>
        /// Checks whether or not the CSV file format loaded is invalid
        /// </summary>
        /// <param name="tableToCheck">DataTable of CSV file</param>
        private bool IsInvalidGlobalVarFileFormat(DataTable tableToCheck)
        {
            return tableToCheck.Columns.Count != 2 || tableToCheck.Columns[0].ColumnName != "VarName" || tableToCheck.Columns[1].ColumnName != "Value";
            
        }

        /// <summary>
        /// Checks whether or not the CSV file's variables are invalid
        /// </summary>
        /// <param name="tableToCheck">DataTable of CSV file</param>
        private bool HasInvalidVariableName(DataTable tableToCheck)
        {
            List<string> variableNames = new List<string>();
            foreach (DataRow dataRow in tableToCheck.Rows)
            {
                string variableName = dataRow[0].ToString();
                if (variableName == String.Empty)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_GLOBAL_VAR_EMPTY_VARIABLE);
                    return true;
                }
                else if (variableNames.Contains(variableName))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_GLOBAL_VAR_DUPLICATE_VARIABLE);
                    return true;
                }
                variableNames.Add(variableName);
            }
            return false;
        }
        #endregion

        #region EVENT HANDLERS
        /// <summary>
        /// handles combobox selection event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboGlobalVarFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem as string;
            RefreshVariableGrid(text);
        }

        /// <summary>
        /// handles Cancel button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handles OK button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsInvalidGlobalVarFileFormat(varData))
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_CSV_FORMAT_INVALID);
                    return;
                }
                else if (varData.Rows.Count == 0)
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_CSV_EMPTY);
                    return;
                }
                else if (HasInvalidVariableName(varData))
                {
                    return;
                }

                SelectedGlobalVarFile = cboGlobalVarFile.Text;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
