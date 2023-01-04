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
using System.Windows.Shapes;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddEditSuiteLink.xaml
    /// </summary>
    public partial class AddEditSuiteLink : Window
    {
        #region PRIVATE_MEMBERS
        private bool isEdit = false;
        private string origName = "";
        private const string TITLE_ADD = "Add";
        private const string TITLE_EDIT = "Edit";
        private DlkSuiteLinkRecord mlinkRec = null;
        private List<DlkSuiteLinkRecord> mLinkRecs = null;
        #endregion

        #region PUBLIC_MEMBERS
        /// <summary>
        /// Class constractor
        /// </summary>
        /// <param name="Link"></param>
        public AddEditSuiteLink(DlkSuiteLinkRecord Link, List<DlkSuiteLinkRecord> Links)
        {
            InitializeComponent();
            mlinkRec = Link;
            mLinkRecs = Links;
            Initialize();
        }
        #endregion

        #region PRIVATE_MEMBERS
        /// <summary>
        /// Initialize form resources
        /// </summary>
        private void Initialize()
        {
            txtName.DataContext = mlinkRec;
            txtPath.DataContext = mlinkRec;
            origName = mlinkRec.DisplayName;
            isEdit = string.IsNullOrEmpty(mlinkRec.DisplayName) ? false : true;
        }

        /// <summary>
        /// Validate form contents on submit
        /// </summary>
        /// <param name="output">Error string, empty if no error</param>
        /// <returns>TRUE if valid, FALSE otherwise</returns>
        private bool Validate(out string output)
        {
            bool ret = true;
            output = string.Empty;

            /* Check if name is not blank. Any other name is valid */
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                output = DlkUserMessages.ERR_LINKS_NAME_INVALID;
                return false;
            }

            bool isNameDuplicate = mLinkRecs.Any(x => x.DisplayName.Trim().ToLower().Equals(txtName.Text.Trim().ToLower())
                && x.Id != mlinkRec.Id);

            if (isEdit)
            {
                if (!origName.ToLower().Equals(txtName.Text.ToLower().Trim()) && isNameDuplicate)
                {
                    output = string.Format(DlkUserMessages.WRN_DUPLICATE_DISPLAYNAME, txtName.Text.Trim());
                    return false;
                }
            }
            else
            {
                if (isNameDuplicate)
                {
                    output = string.Format(DlkUserMessages.WRN_DUPLICATE_DISPLAYNAME, txtName.Text.Trim());
                    return false;
                }
            }

            if (!System.IO.Path.IsPathRooted(txtPath.Text) &&
                !DlkString.IsValidUriString(txtPath.Text))
            { 
                output = DlkUserMessages.ERR_LINKS_PATH_INVALID;
                return false;
            }
            return ret;
        }
        #endregion

        #region EVENT_HANDLERS
        /// <summary>
        /// Handler for window loaded
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Title = string.IsNullOrEmpty(mlinkRec.DisplayName) ? TITLE_ADD : TITLE_EDIT;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for OK click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string err;

                if (!Validate(out err))
                {
                    DlkUserMessages.ShowError(err);
                    return;
                }
                mlinkRec.DisplayName = txtName.Text.Trim();
                mlinkRec.LinkPath = txtPath.Text.Trim();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// Handler for Cancel click
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}
