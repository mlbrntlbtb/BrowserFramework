using CommonLib.DlkSystem;
using System;
using System.Windows;
using TestRunner.Common;

namespace Recorder
{
    /// <summary>
    /// Interaction logic for CustomModalDialog.xaml
    /// </summary>
    public partial class TestPlaybackDialog : Window
    {
        public string mChoice = "";

        private const string CONST_SAVE_AND_RUN = "Save and run the test before recording.";
        private const string CONST_RUN = "Run the test before recording.";

        public TestPlaybackDialog(bool IsTopMost=false, bool IsChanged=false)
        {
            InitializeComponent();
            this.Topmost = IsTopMost; 
            txtInstructions.Text = IsChanged ? DlkUserMessages.ASK_TEST_RESUME_PLAYBACK_WITH_UNSAVED : DlkUserMessages.ASK_TEST_RESUME_PLAYBACK;
            rbPlayback.Content = IsChanged ? CONST_SAVE_AND_RUN : CONST_RUN;
            mChoice = "playback"; //default
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

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

        private void rbPlayback_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mChoice = "playback";
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void rbManual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mChoice = "manual";
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void rbClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mChoice = "clear";
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
