using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using CommonLib.DlkRecords;
using TestRunner.Common;
using CommonLib.DlkSystem;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for EmailSettingsDialog.xaml
    /// </summary>
    public partial class EmailSettingsDialog : Window
    {
        private string filePath = string.Empty;
        private string _mySuite;
        private DlkScheduleRecord mCurrentSched;

        private string _mEmails;

        //public List<DlkScheduleRecord> mSchedRecs;
        public string mEmails
        {
            get
            {
                return _mEmails;
            }
            set
            {
                _mEmails = value;
            }
        }

        public EmailSettingsDialog(string ScheduleFilePath, string SuitePath, DlkScheduleRecord CurrentSched)
        {
            InitializeComponent();
            filePath = ScheduleFilePath;
            _mySuite = SuitePath;
            //mSchedRecs = SchedRecs;
            mCurrentSched = CurrentSched;
         }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtMailAddresses.Text = mEmails;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateEmails(txtMailAddresses.Text))
                {
                    if (SaveEmail(filePath))
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_UNEXPECTED_ERROR, "Save failed");
                    }

                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Checks if input string is valid email address
        /// </summary>
        /// <param name="inputEmail">Input email string</param>
        /// <returns>True if valid, otherwise False</returns>
        private static bool IsEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private static bool ValidateEmails( string input)
        {
            String eMails = input;
            string[] eMailAdds = eMails.Split(';');

            List<string> validEmail = new List<string>();
            foreach (string m in eMailAdds)
            {
                if (string.IsNullOrEmpty(m))
                {
                    continue;
                }
                else if (IsEmail(m) == true)
                {
                    validEmail.Add(m);
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_EMAIL_INVALID);
                    return false;
                }
            }

            return true;
        }

        private bool SaveEmail(string filepath)
        {
            try
            {
                ///<summary
                ///old code to save directly to file
                ///</summary
                //XDocument xDoc = XDocument.Load(filepath);
                //var node = xDoc.Descendants("schedrec").Where(e => e.Attribute("suite").Value == _mySuite).FirstOrDefault();
                //if (node != null)
                //{
                //    node.Attribute("email").SetValue(txtMailAddresses.Text);
                //}
                //xDoc.Save(filepath);
                mCurrentSched.Email = txtMailAddresses.Text;
                //mSchedRecs.FirstOrDefault(x => x.TestSuitePath == _mySuite).Email = txtMailAddresses.Text;
                return true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
                return false;
            }
            
        }

    }

}
