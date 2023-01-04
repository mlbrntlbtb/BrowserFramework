using System;
using System.Collections.Generic;
using System.IO;
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
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;
using TestRunner.Designer.View;


namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for AddEditTestLink.xaml
    /// </summary>
    public partial class AddEditTag : Window
    {
        #region PRIVATE_MEMBERS
        private const string TITLE_ADD = "Add";
        private const string TITLE_EDIT = "Edit";
        private DlkTag mTag= null;
        private List<DlkTag> mAllTags;
        #endregion

        #region PUBLIC_MEMBERS
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="Link"></param>
        public AddEditTag(DlkTag TagToAddEdit, List<DlkTag> AllTags)
        {
            InitializeComponent();
            mTag = TagToAddEdit;
            mAllTags = AllTags;
            Initialize();
        }
        #endregion

        #region PRIVATE_MEMBERS
        /// <summary>
        /// Initialize form resources
        /// </summary>
        private void Initialize()
        {
            txtName.DataContext = mTag;
            txtDesc.DataContext = mTag;
        }

        /// <summary>
        /// Validate form contents on submit
        /// </summary>
        /// <param name="output">Error string, empty if no error</param>
        /// <returns>TRUE if valid, FALSE otherwise</returns>
        private bool Validate(out string output)
        {
            output = string.Empty;

            if (string.IsNullOrEmpty(txtName.Text))
            {
                // erro
                output = DlkUserMessages.ERR_TAG_NAME_BLANK;
                return false;
            }

            // if tag is using an existng name
            if (mAllTags.Any(y => y.Name == txtName.Text))
            {
                if (mAllTags.FindAll(x => x.Name == mTag.Name).Count > 0)
                {
                    // if the hit is not itself
                    if (mAllTags.Find(x => x.Name == mTag.Name).Id != mTag.Id || mAllTags.Find(x => x.Name == txtName.Text).Id != mTag.Id)
                    {
                        output = DlkUserMessages.ERR_TAG_NAME_EXISTS;
                        return false;
                    }
                }
                else
                {
                    output = DlkUserMessages.ERR_TAG_NAME_EXISTS;
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region EVENT_HANDLERS
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
                mTag.Name = txtName.Text;
                mTag.Description = txtDesc.Text;
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

        /// <summary>
        /// Handler for window loaded
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Title = string.IsNullOrEmpty(mTag.Name) ? TITLE_ADD : TITLE_EDIT;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion

    }
}
