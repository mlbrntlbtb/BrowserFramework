using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ConvertToDataModificationDialog.xaml
    /// </summary>
    public partial class ConvertToDataModificationDialog : Window
    {
        #region DECLARATIONS

        private ModifyConvertDataOptions mModifyConvertDataChoice;
        private ModifyConvertDataOptions selectedOption;
        bool isSaving = false;

        #endregion

        #region ENUMS

        public enum ModifyConvertDataOptions
        {
            ModifyAndConvert,
            ConvertOnly,
            AbortSave
        }

        #endregion

        #region CONSTRUCTORS

        public ConvertToDataModificationDialog()
        {
            InitializeComponent();
            selectedOption = ModifyConvertDataOptions.ConvertOnly; //default
        }

        #endregion

        #region PROPERTIES

        public ModifyConvertDataOptions ModifyConvertDataChoice
        {
            get { return mModifyConvertDataChoice; }
            set { mModifyConvertDataChoice = value; }
        }

        #endregion

        #region EVENTS
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
                ModifyConvertDataChoice = selectedOption;
                isSaving = true;
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
                //Abort Save
                this.DialogResult = false;
                ModifyConvertDataChoice = ModifyConvertDataOptions.AbortSave;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void rbModifyAndConvert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = ModifyConvertDataOptions.ModifyAndConvert;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }  
        }

        private void rbConvertAll_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedOption = ModifyConvertDataOptions.ConvertOnly;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isSaving)
            {
                e.Cancel = false;
                ModifyConvertDataChoice = ModifyConvertDataOptions.AbortSave;
            }

        }

        #endregion
    }
}
