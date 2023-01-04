using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Drawing;
using System.Threading;

namespace TestRunner.Common
{
    public static class DlkUserMessages
    {
        /* Error message constants */
        public const string ERR_INVALID_EMAIL_LIST_FORMAT = "Invalid email list format.";
        public const string ERR_URLBLACKLIST_SETTING = "Environment '{0}' contains a blacklisted URL. Please enter a different URL.";
        public const string ERR_URL_BLACKLIST = "Current schedule contains a blacklisted URL in Environment '{0}'. Please update the URL in Edit > Settings > Environment or choose a different environment.";
        public const string ERR_TEST_HTML_EXPORT = "Error encountered while exporting test as HTML. Please see log files.";
        public const string ERR_SETTINGS_NO_ENVIRONMENT_ID = "Environment ID cannot be blank.";
        public const string ERR_SETTINGS_NO_REMOTE_ID = "Remote ID cannot be blank.";
        public const string ERR_SETTINGS_NO_DEVICE_ID = "Device ID cannot be blank.";
        public const string ERR_SETTINGS_NO_DEVICE_NAME = "Device Name cannot be blank.";
        public const string ERR_SETTINGS_NO_MOBILE_TYPE = "Please select Mobile Type.";
        public const string ERR_SETTINGS_NO_DEVICE_VERSION = "Please select Device Version.";
        public const string ERR_SETTINGS_NO_MOBILE_BROWSER = "Please select Mobile Browser.";
        public const string ERR_SETTINGS_DUPLICATE_REMOTE_ID = "Remote Machine ID '{0}' already exists. Please use a different ID.";
        public const string ERR_SETTINGS_DUPLICATE_DEVICE_ID = "Device ID '{0}' already exists. Please use a different ID.";
        public const string ERR_SETTINGS_LONG_DEVICE_NAME = "Device Name exceeds the maximum length of 100 characters.";
        public const string ERR_UNEXPECTED_ERROR = "Test Runner encountered an unexpected error. See program logs for details.";
        public const string ERR_INITIALIZE_ERROR = "Error initializing Test Runner. Please make sure that all files and folders are in their right locations.";
        public const string ERR_GENERIC_STARTUP_ERROR = "There was a problem starting Test Runner. Please contact your system administrator for assistance.";
        public const string ERR_EMPTY_PRODUCT_FOLDER = "The Products folder is either empty or has no valid product to test. Please make sure that all files and folders are in their right locations.";
        public const string ERR_EMPTY_APPLICATION_DROPDOWN = "Please choose a valid product.";
        public const string ERR_NO_TEST_TO_RUN = "Please add tests to the queue.";
        public const string ERR_NO_TEST_TO_ORDER = "Please add tests to the queue before trying to order them.";
        public const string ERR_NO_TEST_TO_REMOVE = "Please add tests to the queue before trying to remove them.";
        public const string ERR_NO_TEST_TO_SAVE = "Please add tests to the queue before trying to save a suite.";
        public const string ERR_NO_TEST_IN_QUEUE = "Please select a test in the queue.";
        public const string ERR_NO_TEST_IN_QUEUE_FOLDER = "Selected folder does not contain any test.";
        public const string ERR_NO_SELECTED_TEST_IN_QUEUE = "Please select a test from the Test Execution Queue.";
        public const string ERR_NO_OPEN_SUITE = "Please open a test suite.";
        public const string ERR_NO_IMAGE_TO_DISPLAY = "No error image to display.";
        public const string ERR_NO_TEST_TO_QUEUE = "Please select a test.";
        public const string ERR_TEST_XML_INVALID = "Please use a valid test XML.";
        public const string ERR_COLUMNNAME_EXISTS_NEWCOLUMN_FAILED = "Column name already exists. Please use a unique name.";
        public const string ERR_MACHINENAME_INVALID = "Invalid machine name.";
        public const string ERR_COLUMNNAME_INVALID = "Invalid column name.";
        public const string ERR_FOLDERNAME_INVALID = "Invalid folder name.";
        public const string ERR_EMAIL_INVALID = "Invalid email address.\nMake sure that the email addresses are valid emails.";
        public const string ERR_IMPORT_FILE_INVALID = "File for import is invalid: ";
        public const string ERR_FILE_NOT_FOUND = "Error loading file. File not found: ";
        public const string ERR_NO_EXPORT_DATA = "No data to export.";
        public const string ERR_NO_SEARCH_TYPE = "Please select a Search Type.";
        public const string ERR_NO_SEARCH_VALUE = "Please enter a Search Value.";
        public const string ERR_NO_VM_SELECTED = "Please select VMs.";
        public const string ERR_NO_EXECUTION_DATE = "Please select an execution date, or choose Cancel.";
        public const string ERR_CONTROL_NOT_FOUND = "Control not found.";
        public const string ERR_SAVE_FAILED = "Unable to save changes.";
        public const string ERR_SAVE_ERROR = "Error in saving.\nMessage:\n";
        public const string ERR_NO_FILE_TO_OPEN = "Please select a file to open.";
        public const string ERR_CANNOT_SAVE_READ_ONLY = "The file is read-only. Save will not proceed.";
        public const string ERR_FILENAME_INVALID = "Please use a valid filename.";
        public const string ERR_NO_FILE_TO_OVERWRITE = "Please select a file to overwrite.";
        public const string ERR_SCHEDULE_EXISTS = "Schedule for this suite exists. Choose another suite";
        public const string ERR_SET_SCHEDULE_FAILED = "Unable to set schedule.";
        public const string ERR_UNEXPECTED_REG_UPDATE_ERROR = "Test Runner encountered an unexpected error during modification of registry settings. See program logs for details.";
        public const string ERR_INCORRECT_TIME_FORMAT = "Incorrect time format.";
        public const string ERR_NO_ENVIRONMENT = "Select an environment before opening the browser.";
        public const string ERR_AUTOLOGIN_FAIL = "Automatic login failed. Please login manually.";
        public const string ERR_ENVIRONMENT_INVALID = "Please select a valid environment.";
        public const string ERR_IMPORT_INVALID = "Test Runner encountered an unexpected error during import. See program logs for details.";
        public const string ERR_CSV_EMPTY = "CSV file is empty.";
        public const string ERR_CSV_FORMAT_INVALID = "Selected CSV file is in an invalid format.";
        public const string ERR_CSV_HEADERS_INVALID = "The number of data field entries in the CSV file must not exceed the number of columns.";
        public const string ERR_CSV_DUPLICATE_HEADERS = "Every column header in the CSV file must be unique.";
        public const string ERR_CSV_HEADER_EMPTY = "One or more column headers in the CSV file are empty. Empty headers are not allowed.";
        public const string ERR_CSV_EXCEL_WRONG_FORMAT = "Please import CSV or Excel files only.";
        public const string ERR_CSV_EXCEL_WRONG_EXTENSION = "Unsupported file format for export. Save as CSV or Excel files only.";
        public const string ERR_EXCEL_EMPTY = "Excel file is empty.";
        public const string ERR_EXCEL_DUPLICATE_HEADERS = "Every column header in the Excel file must be unique.";
        public const string ERR_EXCEL_HEADERS_INVALID = "The number of data field entries in the Excel file must not exceed the number of columns.";
        public static string ERR_EXCEL_FILE_IN_USE = "The Excel file is in use. Please save and close your file to reflect all changes.";
        public static string ERR_NO_FILE_SELECTED = "Please select a file/folder to delete.";
        public const string ERR_NONEXISTENT_PATH = "File or directory does not exist: ";
        public const string ERR_NO_SCRIPT_PATH = "The path of the script must not be empty.";
        public const string ERR_SELECT_ACTIVATOR_TEST = "Please select an activator test in the list to continue.";
        public const string ERR_DBCONN_FAIL = "Database connection failed. Please make sure that provided values are correct.";
        public const string ERR_DATA_XML_INVALID = "Please use a valid data (.trd) file.";
        public const string ERR_DELETE_USED_AGENT = "These agents are in use and cannot be deleted right now.";
        public const string ERR_DISABLE_USED_AGENT = "These agents are in use and cannot be disabled right now.";
        public const string ERR_DELETE_USED_AGENT_GROUP = "Agent group is in use and cannot be deleted right now.";
        public const string ERR_LINKS_PATH_INVALID = "Please provide a valid link path.";
        public const string ERR_LINKS_NAME_INVALID = "Please provide a valid link display name.";
        public const string ERR_NO_INFO_SELECTED_TO_EXPORT = "Please select information to export.";
        public const string ERR_TAG_NAME_BLANK = "Tag name cannot be blank";
        public const string ERR_TAG_NAME_EXISTS = "Tag Name '{0}' already exists. Please use a different name.";
        public const string ERR_TEST_CAPTURE_ONGOING_RECORDING = "Running a test while there is an ongoing Test Capture session is not allowed. Please stop your recording first to run your test.";
        public const string ERR_NO_VALID_RECIPIENTS = "There are no valid recipients.";
        public const string ERR_FOLDER_NAME_ALREADY_EXISTS = "Folder name already exists.";
        public const string ERR_ROWEDITOR_SAVE_FAIL = "One or more items cannot be edited at this time. Saving Failed!";
        public const string ERR_CONNECTION_TEST_FAILED = "The connection test failed.\n\n You may want to check the following:\n\n- The application URL is correct and currently accessible\n- The login information provided is correct";
        public const string ERR_CONNECTION_URL_BLACKLIST = "The connection test cannot proceed because you are currently using a blacklisted URL.\n\nPlease update the environment URL if you wish to continue.";
        public const string ERR_FILE_NAME_EXCEEDS_MAX = "The file name exceeds the maximum length of 256 characters.";
        public const string ERR_TEST_CAPTURE_REQUIRES_FF_OR_CHROME = "Please install any of the following web browsers to use Test Capture:\n\n(1) Mozilla Firefox\n(2) Google Chrome";
        public const string ERR_VALUE_NOT_EMPTY = "Instance value cannot be empty";
        public const string ERR_INVALID_INSTANCE_VALUE = "Value entered is invalid";
        public const string ERR_INVALID_ENVIRONMENT_VALUE = "Environment does not exist. Please enter a valid environment.";
        public const string ERR_INVALID_ENVIRONMENT_VALUE_SAVE = "Environment '{0}' does not exist. Please enter a valid environment before saving.";
        public const string ERR_INVALID_ENVIRONMENT_VALUE_RUN = "Environment '{0}' does not exist. Please enter a valid environment before running.";
        public const string ERR_BLANK_ENVIRONMENT_CHANGE_DEFAULT = "Environment cannot be blank. Please provide a valid environment.";
        public const string ERR_INVALID_EXECUTE_VALUE = "Execute value is invalid. Please enter a valid execute value.";
        public const string ERR_BLANK_EXECUTE_CHANGE_DEFAULT = "Execute value cannot be blank. Please provide a valid execute value.";
        public const string ERR_INVALID_RECURRENCE_VALUE = "Recurrence value is invalid. Please enter a valid recurrence value.";
        public const string ERR_BLANK_RECURRENCE_CHANGE_DEFAULT = "Recurrence value cannot be blank. Please provide a valid recurrence value.";
        public const string ERR_TEST_DIR_PATH_NOT_EXIST = "Test Directory path '{0}' is invalid.\n\nPlease ensure that the directory exists and you have sufficient access permission.";
        public const string ERR_SUITE_DIR_PATH_NOT_EXIST = "Suite Directory path '{0}' is invalid.\n\nPlease ensure that the directory exists and you have sufficient access permission.";
        public const string ERR_TEST_DIR_PATH_EMPTY = "Test Directory path is empty.\n\nPlease enter a valid directory and ensure that you have sufficient access permissions for that directory.";
        public const string ERR_SUITE_DIR_PATH_EMPTY = "Suite Directory path is empty.\n\nPlease enter a valid directory and ensure that you have sufficient access permissions for that directory.";
        public const string ERR_GLOBAL_VAR_EMPTY_VARIABLE = "One of the variable names for this file is empty. Empty variables are not allowed.";
        public const string ERR_GLOBAL_VAR_DUPLICATE_VARIABLE = "Two or more variables in this file have the same name. Duplicate variables are not allowed.";
        public const string ERR_UNSUPPORTED_TEST_CAPTURE_APPLICATION = "Test Capture does not support current selected product.";
        public const string ERR_OUTDATED_OBJECTSTORE = "Test Runner detected object store files in outdated format. Please contact your system administrator for assistance.\n\nTest Runner will now exit.";
        public const string ERR_OUTDATED_TEST_LIBRARY_OBJECTSTORE = "Test Library detected object store files in outdated format. Please contact your system administrator for assistance.\n\nTest Library will now exit.";
        public const string ERR_SCHEDULING_AGENT_RUNNING = "Another instance of SchedulingAgent is currently running.";
        public const string ERR_SCHEDULING_AGENT_MISSING = @"SchedulingAgent cannot be found in Tools\TestRunner folder. Please get the latest SchedulingAgent.exe file from TFS.";
        public const string ERR_SCHEDULING_AGENT_MISSING_EXTERNAL = @"SchedulingAgent cannot be found in Tools\TestRunner folder. Please reinstall TestRunner by using the provided installer.";
        public const string ERR_IMPORT_ROOTTEST = "The folder you wish to import is already inside the Tests directory. Please choose a different folder.";
        public const string ERR_IMPORT_ROOTSUITE = "The folder you wish to import is already inside the Suites directory. Please choose a different folder.";
        public const string ERR_MAX_EDITOR_INVALID = "Test Runner is unable to launch Test Editor due to invalid configuration. Please contact your system administrator for assistance.";
        public const string ERR_DIAGNOSTIC_HTML_REPORT_NOTFOUND = "Test Runner Diagnostics HTML report not found.";
        public const string ERR_TESTRUNNER_RUNNING = "Another instance of Test Runner is currently running.";
        public const string ERR_SCHEDULER_LOADER_CONFIG_CORRUPT = "Corrupted login config file detected. Scheduler will now exit.";
        public const string ERR_UNSUPPORTED_DOTNETFRAMEWORK = "Test Runner is unable to start due to unsupported .NET Framework version. Please install .NET Framework 4.5 Runtime version to run Test Runner.";
        public const string ERR_RESULTSFILE_NOT_FOUND = "Test results file not found: {0}  \nThe file may have been deleted, renamed or moved to a different location.";
        public const string ERR_DDT_SPECIAL_CHARACTERS = "Column headers must not contain special characters.";
        public const string ERR_FILE_ACCESS_ANOTHER_PROCESS = "File: '{0}' is currently open in another program and cannot be accessed. \n\nPlease close the file and try again.";
        public const string ERR_SCHEDULER_UNEXPECTED_ERROR = "Test Scheduler encountered an unexpected error.";
        public const string ERR_SCHEDULER_DUPLICATE_AGENT_NAME = "Invalid machine name. '{0}' already exists.";
        public const string ERR_SCHEDULER_CONFLICT_AGENT_NAME = "Invalid machine name. '{0}' will conflict with another agent/group name.";
        public const string ERR_SCHEDULER_DUPLICATE_AGENT_GROUP_NAME = "Invalid group name. '{0}' already exists.";
        public const string ERR_SCHEDULER_CONFLICT_AGENT_GROUP_NAME = "Invalid group name. '{0}' will conflict with another agent name.";
        public const string ERR_SCHEDULER_EMPTY_AGENT_NAME = "Agent name cannot be empty.";
        public const string ERR_SCHEDULER_EMPTY_AGENT_GROUP_NAME = "Agent group name cannot be empty.";
        public const string ERR_SCHEDULER_INVALID_AGENT_NAME = "Agent names are required to have at least 1 alphanumeric character, and only periods (.) and hyphens (-) are allowed special characters.";
        public const string ERR_SCHEDULER_INVALID_AGENT_GROUP_NAME = "Agent group names are required to have at least 1 alphanumeric character, and only periods (.) and hyphens (-) are allowed special characters.";
        public const string ERR_SETTINGS_INVALID_URL_SCHEME = "Invalid URL. URL should start with http:// or https://";
        public const string ERR_SETTINGS_INVALID_URL_FORMAT = "Invalid URL. URL is in invalid format";
        public const string ERR_SCHEDULER_FILTER_DATE_INVALID_FORMAT = "Invalid date format. Please enter the date in the format m/dd/yyyy.";
        public const string ERR_SCHEDULER_FILTER_DATE_INVALID = "Invalid date.";

        /* Question message constants */
        public const string ASK_ENV_USED_IN_SCHEDULER = "Environment '{0}' is used in Scheduler. Do you want to continue?"; 
        public const string ASK_CLEAR_ALL_SCHED = "Are you sure you want to delete all scheduled test?"; 
        public const string ASK_OVERWRITE_EXISTING_SCHED = "Saving the new scheduled suites after importing a file will overwrite the existing schedule. Continue?";
        public const string ASK_UNSAVED_FILE_EXPORT = "Don't forget to save your file before exporting data. Would you like to save it now?";
        public const string ASK_UNSAVED_TEST_RUN = "The test has been modified. Only the saved test will be used for running. Do you want to save changes before running?";
        public const string ASK_UNSAVED_SUITE_RUN = "The test suite has been modified. The execution results will not be saved. Do you want to save changes before running?";
        public const string ASK_UNSAVED_CHANGES = "The test has been modified. Do you want to save changes?";
        public const string ASK_UNSAVED_CHANGES_CONVERTDATA = "Discard pending changes?";
        public const string ASK_UNSAVED_SCHEDULE_CHANGES = "Schedule has been modified. Do you want to save changes?";
        public const string ASK_UNSAVED_SCHEDULE_CHANGES_RUNNOW = "Schedule has been modified. Do you want to save changes and run the suites now?";
        public const string ASK_REMOVE_SUITE_SCHEDULE = "Are you sure you want to remove the schedule for this suite?";
        public const string ASK_OVERWRITE_SUITE_SCHEDULE = "Pasting a schedule will overwrite all schedules for this day. Are you sure you want to continue?";
        public const string ASK_DELETE_COLUMN = "Are you sure you want to delete '";
        public const string ASK_DELETE_ROW = "Are you sure you want to delete row '";
        public const string ASK_DELETE_ROWS = "Are you sure you want to delete multiple rows?";
        public const string ASK_DELETE_ENVIRONMENT = "Are you sure you want to delete this environment: ";
        public const string ASK_DELETE_REMOTE_BROWSER = "Are you sure you want to delete this remote browser: ";
        public const string ASK_DELETE_MOBILE_DEVICE = "Are you sure you want to delete this mobile device: ";
        public const string ASK_FILE_EXISTS_IMPORT_FAILED = "File '{0}' already exists. Do you want to continue import?";
        public const string ASK_FOLDER_EXISTS_IMPORT_FAILED = "Folder already exists. Do you want to continue import?";
        public const string ASK_SAVE_SOURCE_CONTROL_FILE = "This file is under source control. Saving will check-in changes.\n\nProceed to save and check-in?";
        public const string ASK_PUT_FILE_TO_SOURCE_CONTROL = "Would you like to put file under source control?";
        public const string ASK_OVERWRITE_READ_ONLY = "The file is read-only. Overwrite anyway?";
        public const string ASK_OVERWRITE_READ_ONLY_FILES = "One or more files are read-only. Overwrite anyway?";
        public const string ASK_CHECK_IN_FILE_AFTER_SAVING = "This file is under source control. Would you like to check-in file after saving?";
        public const string ASK_CHECK_IN_SOURCE_CONTROL_FILE_AFTER_SAVING = "Would you like to check-in file after saving?";
        public const string ASK_CHECK_IN_SOURCE_CONTROL_FOLDER_AFTER_SAVING = "Would you like to check-in folder after saving?";
        public const string ASK_TEST_IMPORT_PLAYBACK = "Would you like to run imported test?";        
        public const string ASK_TEST_RESUME_PLAYBACK_WITH_UNSAVED = "There are unsaved steps in the grid. You can choose to run the steps or you can clear the grid and start from scratch. What would you like to do?";
        public const string ASK_TEST_RESUME_PLAYBACK = "There are steps loaded in the grid. You can choose to run the steps or you can clear the grid and start from scratch. What would you like to do?";
        public const string ASK_START_RECORD_WITH_EXISTING_SCRIPT = "The recording has been stopped. Your existing script will be cleared. Do you want to continue and start a new recording?";
        public const string ASK_IMPORT_WITH_EXISTING_SCRIPT = "Do you want to continue import and overwrite existing test?";
        public const string ASK_CLEAR_TEST = "All test steps will be cleared. Do you want to proceed?";
        public const string ASK_DELETE_STEP = "Do you want to delete step ";
        public const string ASK_DELETE_STEPS = "Do you want to delete steps ";
        public const string ASK_DELETE_SELECTED_STEPS = "Do you want to delete the selected steps?";
        public const string ASK_TEST_CAPTURE_SAVE_BEFORE_EXIT = "Do you want to stop recording and save your changes?";
        public const string ASK_TEST_CAPTURE_SAVE_SCRIPT_BEFORE_EXIT = "There are unsaved steps in the grid. Do you want to save your script before exit?";
        public const string ASK_TEST_DELETE = "Are you sure you want to delete the test ";
        public const string ASK_SUITE_DELETE = "Are you sure you want to delete the suite ";
        public const string ASK_FOLDER_DELETE = "Are you sure you want to delete the folder ";
        public const string ASK_FOLDER_ALLFILES = "? Warning, all files inside will be deleted as well!";
        public const string ASK_DELETE_SOURCE_CONTROL = "Do you want to delete the files from source control as well?";
        public const string ASK_FILE_READ_ONLY = "Note that the file is marked as read-only.";
        public const string ASK_FOLDER_READ_ONLY = "Note that there are files within the folder which are marked as read-only.";
        public const string ASK_MASS_DELETE_READ_ONLY = "Note that if there are read-only files in your current selection, they will also be deleted. Proceed?";
        public const string ASK_APPLICATION_QUIT_TEST_EDITOR = "Test Editor is open. All unsaved work will be lost.\n\nAre you sure you want to exit Test Runner?";
        public const string ASK_APPLICATION_QUIT_SUITE_EDITOR = "Suite has been edited. All unsaved work will be lost.\n\nAre you sure you still want to exit Suite Editor?";
        public const string ASK_DELETE_EXTERNAL_SCRIPT = "Do you want to delete external script? ";
        public const string ASK_DELETE_ALL_CURRENT_EXEC_CONDITIONS = "Applying selected Execute value to all tests will remove all existing execution conditions. Proceed?";
        public const string ASK_REMOVE_CURRENT_TEST_DEPENDENCY = "Changing execution order invalidates the execution condition of selected test.\n\nThe condition will be removed. Proceed?";
        public const string ASK_REMOVE_TEST_DEPENDENCY = "Changing execution order invalidates the execution condition of the following test:";
        public const string ASK_ADVANCED_SCHEDULER_EXIT = "Exiting will stop the scheduling process.\n\nAre you sure you want to exit the Scheduler?";
        public const string ASK_ADVANCED_SCHEDULER_RELOAD = "Target application was changed. Do you want to reload the scheduler?";
        public const string ASK_ADVANCED_SCHEDULER_EXIT_WARNING = "There are currently {0} ongoing runs: \n\n{1} \n\nAre you sure you want to exit the Scheduler?";
        public const string ASK_ADVANCED_SCHEDULER_OVERRIDE_READONLY = "Do you want to allow Scheduler to overwrite changes to the schedule file?\n\n Selecting 'No' will disable editing, but test suites will still execute as scheduled.";
        public const string ASK_SAVE_SCRIPT_BEFORE_CREATING_NEW = "There are unsaved steps in the grid. Do you want to save your script before creating a new one?";
        public const string ASK_REMOVE_AGENT = "Are you sure you want to remove selected agents?";
        public const string ASK_REMOVE_AGENT_GROUP = "Are you sure you want to remove selected agent groups?";
        public const string ASK_DISABLE_AGENT = "Some agents are disabled, do you want to enable them? \nSelecting 'Yes' will enable them. \nSelecting 'No' will disable selected agents.";
        public const string ASK_DELETE_TEST_LINK = "Are you sure you want to delete the selected link?";
        public const string ASK_SCHEDULER_REMOVE_LINEUP = "Are you sure you want to remove {0} from the lineup?";
        public const string ASK_SCHEDULER_ADD_NEW_GROUP = "Selected lineup/s are already part of a group. Do you want to delete existing group/s and proceed creating a new group?";
        public const string ASK_SCHEDULER_DELETE_GROUP = "Do you want to delete existing group for this lineup?";
        public const string ASK_SCHEDULER_DELETE_FAVORITES = "Are you sure you want to delete the selected item/s from favorites?";
        public const string ASK_SCHEDULER_QUIT_TEST_EDITOR = "Test Editor is open. All unsaved work will be lost.\n\nAre you sure you want to exit Suite Editor?";
        public const string ASK_SCHEDULER_QUIT_VIEW_LOGS = "View Logs is open. All displayed logs will be closed.\n\nAre you sure you want to exit Scheduler?";
        public const string ASK_SCHEDULER_QUIT_RESULT_DETAILS = "Result details is open. All displayed results will be closed.\n\nAre you sure you want to exit Scheduler?";
        public const string ASK_CLEAR_FIND_RESULTS = "Changing this setting will clear the current find results. Proceed?";
        public const string ASK_LOSE_CHANGES = "All changes will be lost. Proceed?";
        public const string ASK_CLEAR_EXSTEPS = "Are you sure you want to delete all excluded steps?";
        public const string ASK_EDIT_EMAIL_MESSAGE = "Would you like to edit the email message before sending?\n\nSelecting 'YES' will launch your default email client.\nSelecting 'NO' will send your email and close this form.";
        public const string ASK_DELETE_TAG_APPEND_TAGNAME = "Are you sure you want to delete tag '";
        public const string ASK_CONTINUE_WITH_LOGIN_ATTEMPT = "A connection test will perform the following:\n\n- Launch Microsoft Internet Explorer\n- Check if the application login page is reached\n- Attempt a user login with the provided login information\n\nThe connection test may take 30s to finish.\n\nWould you like to proceed with the test?";
        public const string ASK_DELETE_HISTORY_ITEM = "Are you sure you want to delete selected item/s?";
        public const string ASK_OVERWRITE_TEST_TREE = "File already exists in this location. Do you want to overwrite?";
        public const string ASK_OVERWRITE_TEST_SCRIPT = "File {0} already exists. Do you wish to replace it?";
        public const string ASK_MODIFY_SOURCE_CONTROL = "Do you want to commit changes to source control?";
        public const string ASK_ALL_SCHEDULER_AGENTS_ARE_BUSY = "There are no agents ready for this schedule. \nWould you like to pend run until an agent becomes available?";
        public const string ASK_COPY_FILES_TO_NEW_LOCATION = "Test/Suite directories were changed. Would you like to import your files to the new location?";
        public const string ASK_FOLDER_EXISTS_SUITE_IMPORT_FAILED = "Folder already exists. Do you want to overwrite its contents?";
        public const string ASK_CANCEL_TEST_RUN_CLOSE_TEST_EDITOR = "Closing Test Editor will cancel ongoing test run. \n\nDo you want to proceed?";
        public const string ASK_CLOSE_TEST_EDITOR_WINDOW = "Test Editor is open. Test Editor must close to apply the newly saved preferences.\n\nDo you want to continue saving?";
        public const string ASK_IMPORT_NESTEDFOLDERS = "The folder you wish to import contains multiple nested folders. Do you wish to continue?";
        public const string ASK_FOLDER_QUEUE = "Queue all tests inside folder '{0}'?";
        public const string ASK_FOLDER_INSERT = "Insert all tests inside folder '{0}'?";
        public const string ASK_CONTINUE_ON_MISSING_ENV_CREDETIALS = "Environment '{0}' has missing credentials. Automatic login will be disabled. If this is a Single Sign-On (SSO) environment, you may click OK. Otherwise, do you want to continue and login manually?";
        public const string ASK_PASTE_OVERWRITE = "Step(s) contents will be overwritten. Do you want to continue?";
        public const string ASK_CLEAR_DATAVIEW_ROWS = "Are you sure you want to clear all rows?";
        public const string ASK_CLEAR_DATAVIEW_ALL = "Are you sure you want to clear all columns and rows?";
        public const string ASK_TEST_SUITE_MISSING_SCRIPTS = "All test scripts from suite are missing. No tests scripts will be loaded. \n\nDo you want to continue?";
        public const string ASK_CHANGE_DEFAULT_BROWSER = "{0} will now replace {1} as the default browser. Do you wish to continue?";
        public const string ASK_REPLACE_COLUMN_HEADERS = "Test Runner will update header names to remove the spaces.\n\nDo you wish to continue?";
        /* Information message constants */
        public const string INF_SCHEDULE_SET_TO_DEFAULT = "The following test suite/s are changed to default environment as '{0}' no longer exist. \n\n{1}";
        public const string INF_TEST_HTML_EXPORT = "Test has been successfully exported as HTML.";
        public const string INF_TEST_HTML_EXPORT_EMAIL = "Test has been successfully exported as HTML and sent to mail";
        public const string INF_FEEDBACK_MESSAGE = "Thank you for sharing your feedback!\n\nYour default e-mail client will appear shortly. Please feel free to type in your feedback and suggestions!";
        public const string INF_DUPLICATE_CONTROLS = "Duplicate controls were detected:\n\n";
        public const string INF_FIELDS_INCORRECT_VALUES = "The following fields have incorrect values: ";
        public const string INF_FIELDS_REPLACE_INCORRECT_VALUES = "Replace the fields with valid values to proceed with the update.";
        public const string INF_SAVE_SUCCESSFUL = "Saved: ";
        public const string INF_SAVED_SUITE = "Saved Suite: ";
        public const string INF_NO_ERROR_LOGS = "There are no error logs to be sent";
        public const string INF_EXPORT_COMPLETED = "Export completed: ";
        public const string INF_IMPORT_COMPLETED = "Import completed: ";
        public const string INF_SAVE_TO_PRESERVE = "\n\nPlease save your test to preserve data changes.";
        public const string INF_INSPECT_CONTROL = "Start inspecting a control by clicking the 'Inspect' button.";
        public const string INF_SPLASH_FINISHED_LOAD_OS_FILES = "Finished loading object store files...";
        public const string INF_SPLASH_STARTING_APP = "Starting application...";
        public const string INF_HIGHLIGHT_CONTROL = "Please click the Highlight button again to highlight the selected control.";
        public const string INF_LAST_TEST_IN_SUITE_WILL_CLOSE_BROWSER = "If you wish to keep the browser open after all tests have executed, manually check the 'Keep Open' setting of the last test in the test suite.";
        public const string INF_PLAYBACK_FINISHED = "Test playback is successful. Your recording will now start.";
        public const string INF_PLAYBACK_FAILED = "Test playback has encountered an error and failed at ";
        public const string INF_PLAYBACK_PAUSED = "The recording will be put in [Paused] state.";
        public const string INF_DELETE_SUCCESSFUL = "Successfully deleted ";
        public const string INF_DELETE_SUCCESSFUL_SELECTED = "Successfully deleted selected item/s.";
        public const string INF_DELETE_FOLDER = ", as well as files inside the folder.";
        public const string INF_TEST_SUITE_MISSING_SCRIPTS = "The test suite contains the following deleted or missing test scripts: \n\n";
        public const string INF_MASS_DELETE_SUCCESSFUL = "Files successfully deleted.";
        public const string INF_MASS_DELETE_FILES = " files and ";
        public const string INF_MASS_DELETE_FOLDERS = " folders successfully deleted.";
        public const string INF_SUITE_COPY_SUCCESSFUL = "Successfully copied schedule for ";
        public const string INF_SUITE_COPY_NOT_SUCCESSFUL = "There is no schedule for this day. No schedule was copied.";
        public const string INF_DBCONN_SUCCESS = "Database connection succeeded.";
        public const string INF_RUN_NOW_REMOTE = "Test execution initiated at ";
        public const string INF_SCHEDULER_EXPORT_COMPLETED = "Export completed successfully.";
        public const string INF_SCHEDULER_ENTER_AGENT_NAME = "Enter the machine name:";
        public const string INF_SCHEDULER_NEW_AGENT_GROUP_NAME = "New agent group name:";
        public const string INF_SCHEDULER_UPDATE_AGENT_GROUP = "Agent group changes successfully saved.";
        public const string INF_SCHEDULER_ADD_FAVORITES_FOLDER_EMPTY = "Failed to add folder with empty name.";
        public const string INF_TEST_CANNOT_GROUP = "At least one of the suites that you are trying to group is still {0}. Please try again later.";
        public const string INF_PROCESS_RUNNING_BACKGROUND = "Process is still running in the background.";
        public const string INF_SCHEDULER_LINEUP_OPTIONS_UPDATED = "Test Schedule/s options updated.";
        public const string INF_NO_MATCHES_FOUND = "0 matches found.";
        public const string INF_FILES_UPDATED = " file/s updated.";
        public const string INF_FILES_NOT_SAVED_READONLY = " file/s not saved. Ensure that test files are not readonly.";
        public const string INF_ALL_SCHEDULER_AGENTS_ARE_BUSY = "There are no agents ready for this schedule. Failed to run the suite now.";
        public const string INF_SEE_ATTACHED_FILE = "See attached file...";
        public const string INF_RESULTS_EMAIL_SENT = "Results email sent!";
        public const string INF_FOLDER_ALREADY_EXIST = "Folder/file name already exists!";
        public const string INF_FOLDER_ALREADY_EXIST_SPECIFIED = "Folder/file name '{0}' already exists!";
        public const string INF_OVERLAP_DATE = "Date FROM should not be greater than date TO"; 
        public const string INF_FOLDERNAME_NOT_EMPTY = "Folder name cannot be empty!";
        public const string INF_CONNECTION_TEST_SUCCESSFUL = "The connection test passed!\n\n- The application login page was reached\n- User login was successful";
        public const string INF_DRAG_DROP_SOURCE_CONTROL_SUCCESSFUL = "Source control changes committed successfully.";
        public const string INF_GLOBAL_VAR_SELECT_SUCCESSFUL_PREFIX = "Global variable file \"";
        public const string INF_GLOBAL_VAR_SELECT_SUCCESSFUL_SUFFIX = "\" successfully saved to this suite.";
        public const string INF_TOOLTIP_SHOW_APP_NAME = "This is the application name. To view the application \nID instead, go to Edit > Settings > Preferences\nthen uncheck the checkbox under Object Store.";
        public const string INF_TOOLTIP_SHOW_APP_ID = "This is the application ID. To view the application \nname instead, go to Edit > Settings > Preferences\nthen check the checkbox under Object Store.";
        public const string INF_EMPTY_TEST = "A test must have at least one step. An empty step will be created.";
        public const string INF_PRODUCTKEY_APPLIED = "Product Key has been applied successfully.";
        public const string INF_SCHEDULER_LOADER_CORRUPT_SUITE = "Suite {0} is corrupted. Skipping...";
        public const string INF_SCHEDULER_LOADER_NO_DEFAULT_ENVIRONMENT_IN_SUITES = "No suites have default environments. Skipping check...";
        public const string INF_SCHEDULER_LOADER_SUITE_TEST_COUNT_FAILURE = "Suite {0} failed to be read during test count. Skipping...";
        public const string INF_SCHEDULER_LOADER_SUITE_TEST_COUNT_CALCULATING = "Calculating number of default Environment tests for {0} / {1} suites...";
        public const string INF_SCHEDULER_LOADER_SUITE_COUNT_FINISHED = "{0} / {1} suites checked. Starting blacklist check...";
        public const string INF_SCHEDULER_LOADER_TEST_BLACKLIST_CHECK = "Checking {0} / {1} tests of {2} / {3} suites in schedule...";
        public const string INF_SCHEDULER_LOADER_BLACKLISTED_URL_FOUND = "Blacklisted environment {0} found in suite {1} / {2} in schedule.";
        public const string INF_TESTRESULT_EXPORT_EMAIL_SENT = "{0} has been successfully sent to mail.";

        /* Warning message constants */
        public const string WRN_DUPLICATE_DISPLAYNAME = "Display name '{0}' already exists. Please use a different name.";
        public const string WRN_VALUE_OUT_OF_RANGE = "Value entered is out of range";
        public const string WRN_EMULATOR_ANDROID_ONLY = "This feature only supports Android emulators at the meantime. Please use the AVD name as the device name.";
        public const string WRN_GLOBAL_VAR_FILE_NOT_FOUND_PREFIX = "Global variable file name \"";
        public const string WRN_GLOBAL_VAR_FILE_NOT_FOUND_SUFFIX = "\" not found in ";
        public const string WRN_NO_GLOBAL_VAR_FILE = "No global variable file is present in ";
        public const string WRN_NO_GLOBAL_VAR_FILE_SUFFIX = ". A new default global variable file will be created in this location.";
        public const string WRN_NO_ENVIRONMENT_SETTINGS = "No Environment record(s) found.";
        public const string WRN_ADD_NEW_ENVIRONMENT = "Add a new Environment in Settings to continue with this action.";
        public const string WRN_DUPLICATE_BLACKLIST_URL = "URL '{0}' already exists. Please enter a different URL.";
        public const string WRN_DUPLICATE_BLACKLIST_URL_DOMAIN = "URL '{0}' already has a similar domain with URL '{1}'. Please enter a different URL.";
        public const string WRN_DUPLICATE_BLACKLIST_DELIMITER = "URL Blacklist has detected multiple delimiters(;) entered in succession.";
        public const string WRN_BLACKLIST_URL_INCORRECT_FORMAT = "URL Blacklist '{0}' should be in a correct format.";
        public const string WRN_TEST_CONNECTION_SKIPLOGIN = "The connection test passed on certain conditions:\n\n- The application login page was reached\n- No user login performed due to missing Username and Password information.";
        public const string WRN_MAX_EDITOR_WINDOWS = "{1} has reached maximum number of {0} active Test Editor Windows. Please close inactive Test Editor Windows to continue.";
        public const string WRN_DIAGNOSTIC_TEST_CLOSING = "Diagnostic test is currently in progress.\nPlease wait until it completes, or click Cancel to stop the diagnostic test.";
        public const string WRN_NO_ROWS_TO_CLEAR = "No row(s) to clear.";
        public const string WRN_TEST_CAPTURE_IS_OPEN = "An instance of Test Capture is currently running. Make sure Test Capture is closed before switching to another application.";
        public const string WRN_DATE_GREATER_THAN_CURRENT_DATE = "Selected date must not be greater than the current date.";
        public const string WRN_SUITE_RUN_ALL_FALSE = "All tests in queue are set to False. No test will run.";
        public const string WRN_SUITE_SAVE_ALL_FALSE = "All tests in queue are set to False. No test will run in this suite.\n\nAre you sure you want to continue?";

        /* MessageBox types */
        public static System.Windows.MessageBoxResult ShowError(string ErrorMessage, string TitleCaption = "Error")
        {
            return System.Windows.MessageBox.Show(ErrorMessage, TitleCaption, System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK);
        }

        public static System.Windows.MessageBoxResult ShowInfo(string Message, string TitleCaption = "Information")
        {
            return System.Windows.MessageBox.Show(Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information, System.Windows.MessageBoxResult.OK);
        }

        public static System.Windows.MessageBoxResult ShowWarning(string Message, string TitleCaption = "Warning")
        {
            return System.Windows.MessageBox.Show(Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.OK);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNo(string Question, string TitleCaption = "Question")
        {
            return System.Windows.MessageBox.Show(Question, TitleCaption, System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.No);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNoWarning(string Question, string TitleCaption = "Warning")
        {
            return System.Windows.MessageBox.Show(Question, TitleCaption, System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.No);
        }

        public static System.Windows.MessageBoxResult ShowQuestionOkCancel(string Question, string TitleCaption = "Question")
        {
            return System.Windows.MessageBox.Show(Question, TitleCaption, System.Windows.MessageBoxButton.OKCancel,
                System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Cancel);
        }

        public static System.Windows.MessageBoxResult ShowQuestionOkCancelWarning(string Question, string TitleCaption = "Warning")
        {
            return System.Windows.MessageBox.Show(Question, TitleCaption, System.Windows.MessageBoxButton.OKCancel,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.Cancel);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNoCancel(string Question, string TitleCaption = "Question")
        {
            return System.Windows.MessageBox.Show(Question, TitleCaption, System.Windows.MessageBoxButton.YesNoCancel,
                System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Cancel);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNoCancelWarning(string Question, string TitleCaption = "Warning")
        {
            return System.Windows.MessageBox.Show(Question, TitleCaption, System.Windows.MessageBoxButton.YesNoCancel,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.Cancel);
        }

        /* modal Messageboxes */
        public static System.Windows.MessageBoxResult ShowError(System.Windows.Window Window, string ErrorMessage, string TitleCaption = "Error")
        {
            return MessageBoxEx.Show(Window, ErrorMessage, TitleCaption, System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK);
        }

        public static System.Windows.MessageBoxResult ShowInfo(System.Windows.Window Window, string Message, string TitleCaption = "Information")
        {
            return MessageBoxEx.Show(Window, Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Information, System.Windows.MessageBoxResult.OK);
        }

        public static System.Windows.MessageBoxResult ShowWarning(System.Windows.Window Window, string Message, string TitleCaption = "Warning")
        {
            return MessageBoxEx.Show(Window, Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.OK);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNo(System.Windows.Window Window, string Question, string TitleCaption = "Question")
        {
            return MessageBoxEx.Show(Window, Question, TitleCaption, System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.No);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNoWarning(System.Windows.Window Window, string Question, string TitleCaption = "Warning")
        {
            return MessageBoxEx.Show(Window, Question, TitleCaption, System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.No);
        }

        public static System.Windows.MessageBoxResult ShowQuestionOkCancel(System.Windows.Window Window, string Question, string TitleCaption = "Question")
        {
            return MessageBoxEx.Show(Window, Question, TitleCaption, System.Windows.MessageBoxButton.OKCancel,
                System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Cancel);
        }

        public static System.Windows.MessageBoxResult ShowQuestionOkCancelWarning(System.Windows.Window Window, string Question, string TitleCaption = "Warning")
        {
            return MessageBoxEx.Show(Window, Question, TitleCaption, System.Windows.MessageBoxButton.OKCancel,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.Cancel);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNoCancel(System.Windows.Window Window, string Question, string TitleCaption = "Question")
        {
            return MessageBoxEx.Show( Window, Question, TitleCaption, System.Windows.MessageBoxButton.YesNoCancel,
                System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.Cancel);
        }

        public static System.Windows.MessageBoxResult ShowQuestionYesNoCancelWarning(System.Windows.Window Window, string Question, string TitleCaption = "Warning")
        {
            return MessageBoxEx.Show(Window, Question, TitleCaption, System.Windows.MessageBoxButton.YesNoCancel,
                System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.Cancel);
        }

        public static System.Windows.MessageBoxResult ShowWarning(System.Drawing.Point Position, string Message, string TitleCaption = "Warning")
        {
            MessageBoxResult res;
            try
            {
                System.Windows.Window newForm = new System.Windows.Window();
                newForm.Height = 10;
                newForm.Width = 10;
                newForm.AllowsTransparency = true;
                newForm.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                newForm.WindowStyle = System.Windows.WindowStyle.None;
                newForm.Topmost = true;
                newForm.ShowInTaskbar = false;

                newForm.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                newForm.Left = Position.X - newForm.Width / 2;
                newForm.Top = Position.Y - newForm.Height / 2;
                newForm.Show();

                res = MessageBoxEx.Show(newForm, Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.OK);
                newForm.Close();
            }
            catch
            {
                res = MessageBox.Show(Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning, System.Windows.MessageBoxResult.OK);
            }
            return res;
        }

        public static System.Windows.MessageBoxResult ShowInfo(System.Drawing.Point Position, string Message, string TitleCaption = "Info")
        {
            MessageBoxResult res;
            try
            {
                System.Windows.Window newForm = new System.Windows.Window();
                newForm.Height = 10;
                newForm.Width = 10;
                newForm.AllowsTransparency = true;
                newForm.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                newForm.WindowStyle = System.Windows.WindowStyle.None;
                newForm.Topmost = true;
                newForm.ShowInTaskbar = false;

                newForm.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                newForm.Left = Position.X - newForm.Width / 2;
                newForm.Top = Position.Y - newForm.Height / 2;
                newForm.Show();

                res = MessageBoxEx.Show(newForm, Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information, System.Windows.MessageBoxResult.OK);
                newForm.Close();
            }
            catch
            {
                res = MessageBox.Show(Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Information, System.Windows.MessageBoxResult.OK);
            }
            return res;
        }

        public static System.Windows.MessageBoxResult ShowError(System.Drawing.Point Position, string Message, string TitleCaption = "Error")
        {
            MessageBoxResult res;
            try
            {
                System.Windows.Window newForm = new System.Windows.Window();
                newForm.Height = 10;
                newForm.Width = 10;
                newForm.AllowsTransparency = true;
                newForm.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
                newForm.WindowStyle = System.Windows.WindowStyle.None;
                newForm.Topmost = true;
                newForm.ShowInTaskbar = false;

                newForm.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                newForm.Left = Position.X - newForm.Width / 2;
                newForm.Top = Position.Y - newForm.Height / 2;
                newForm.Show();

                res = MessageBoxEx.Show(newForm, Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK);
                newForm.Close();

            }
            catch
            {
                res = MessageBox.Show(Message, TitleCaption, System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Error, System.Windows.MessageBoxResult.OK);
            }
            return res;
        }
    }

    public class MessageBoxEx
    {
        private static IntPtr _owner;
        private static HookProc _hookProc;
        private static IntPtr _hHook;

        public static MessageBoxResult Show(string text)
        {
            Initialize();
            return MessageBox.Show(text);
        }

        public static MessageBoxResult Show(string text, string caption)
        {
            Initialize();
            return MessageBox.Show(text, caption);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon, defResult);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult, MessageBoxOptions options)
        {
            Initialize();
            return MessageBox.Show(text, caption, buttons, icon, defResult, options);
        }

        public static MessageBoxResult Show(Window owner, string text)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons, MessageBoxImage icon)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon, defResult);
        }

        public static MessageBoxResult Show(Window owner, string text, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defResult, MessageBoxOptions options)
        {
            _owner = new WindowInteropHelper(owner).Handle;
            Initialize();
            return MessageBox.Show(owner, text, caption, buttons, icon,
                                    defResult, options);
        }

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);

        public const int WH_CALLWNDPROCRET = 12;

        public enum CbtHookAction : int
        {
            HCBT_MOVESIZE = 0,
            HCBT_MINMAX = 1,
            HCBT_QS = 2,
            HCBT_CREATEWND = 3,
            HCBT_DESTROYWND = 4,
            HCBT_ACTIVATE = 5,
            HCBT_CLICKSKIPPED = 6,
            HCBT_KEYSKIPPED = 7,
            HCBT_SYSCOMMAND = 8,
            HCBT_SETFOCUS = 9
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("User32.dll")]
        public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

        [DllImport("user32.dll")]
        public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        } ;

        static MessageBoxEx()
        {
            _hookProc = new HookProc(MessageBoxHookProc);
            _hHook = IntPtr.Zero;
        }

        private static void Initialize()
        {
            if (_hHook != IntPtr.Zero)
            {
                throw new NotSupportedException("multiple calls are not supported");
            }

            if (_owner != null)
            {
                _hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, _hookProc, IntPtr.Zero, Thread.CurrentThread.ManagedThreadId);
            }
        }

        private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(_hHook, nCode, wParam, lParam);
            }

            CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            IntPtr hook = _hHook;

            if (msg.message == (int)CbtHookAction.HCBT_ACTIVATE)
            {
                try
                {
                    CenterWindow(msg.hwnd);
                }
                finally
                {
                    UnhookWindowsHookEx(_hHook);
                    _hHook = IntPtr.Zero;
                }
            }

            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        private static void CenterWindow(IntPtr hChildWnd)
        {
            Rectangle recChild = new Rectangle(0, 0, 0, 0);
            bool success = GetWindowRect(hChildWnd, ref recChild);

            int width = recChild.Width - recChild.X;
            int height = recChild.Height - recChild.Y;

            Rectangle recParent = new Rectangle(0, 0, 0, 0);
            success = GetWindowRect(_owner, ref recParent);

            System.Drawing.Point ptCenter = new System.Drawing.Point(0, 0);
            ptCenter.X = recParent.X + ((recParent.Width - recParent.X) / 2);
            ptCenter.Y = recParent.Y + ((recParent.Height - recParent.Y) / 2);


            System.Drawing.Point ptStart = new System.Drawing.Point(0, 0);
            ptStart.X = (ptCenter.X - (width / 2));
            ptStart.Y = (ptCenter.Y - (height / 2));

            //ptStart.X = (ptStart.X < 0) ? 0 : ptStart.X;
            //ptStart.Y = (ptStart.Y < 0) ? 0 : ptStart.Y;

            int result = MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width,
                                    height, false);
        }
    }
}
