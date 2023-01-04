using System;
using System.Windows;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddControlDialog.xaml
    /// </summary>
    public partial class AddControlDialog : Window
    {
        private DlkAddControlData mAddControlData;
        public DlkAddControlData AddControlData
        {
            get
            {
                return mAddControlData;
            }
            set
            {
                mAddControlData = value;
            }
        }

        public AddControlDialog()
        {
            InitializeComponent();
            mAddControlData = new DlkAddControlData();
        }

        public AddControlDialog(DlkAddControlData controlData, Boolean IsAdd)
        {
            InitializeComponent();
            mAddControlData = controlData;
            if (IsAdd)
            {
                this.Title = "Add Control";
                btnAdd.Content = "Add";
            }
            else
            {
                this.Title = "Edit Control";
                btnAdd.Content = "Edit";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cboControlType.ItemsSource = LoadControlTypes();
                cboControlType.DataContext = mAddControlData;
                txtControlName.DataContext = mAddControlData;
                cboSearchType.ItemsSource = LoadSearchType();
                cboSearchType.DataContext = mAddControlData;
                txtSearchValue.DataContext = mAddControlData;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private String[] LoadControlTypes()
        {
            switch (DlkEnvironment.mProductFolder.ToLower())
            {
                case "ngcrm":
                    break;
                default:
                    break;
            }

            String[] ControlTypes = {"Button", "TextBox", "ComboBox", "CheckBox", "Label", "Link", "QuickEdit", "SideBar", "Tab", "TabPage", "Table",
                                      "TextArea", "Toolbar", "UIDialog"};
            return ControlTypes;

        }

        private String[] LoadSearchType()
        {
            String[] SearchTypes = {"ID", "CSS", "XPATH", "NAME", "CLASSNAME", "PARTIALLINKTEXT", "TAGNAME_TEXT", "TAGNAME_ATTRIBUTE", "IMG_SRC",
                                    "PARENTID_CHILDCLASS", "PARENT_CHILDCSS", "PARENT_CHILDTAG", "CLASS_DISPLAY"};
            return SearchTypes;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
                this.Close();
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
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }    
        }

        private void btnHighlight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mAddControlData.SearchType == "")
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_SEARCH_TYPE);
                    return;
                }
                if (mAddControlData.SearchValue == "")
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_SEARCH_VALUE);
                    return;
                }
                try
                {
                    DlkBaseControl currentControl = new DlkBaseControl("Control", mAddControlData.SearchType, mAddControlData.SearchValue);
                    currentControl.Highlight(false);
                }
                catch
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_CONTROL_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
