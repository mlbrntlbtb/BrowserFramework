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
using TestRunner.Common;
using CommonLib.DlkSystem;
namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for AddContainsTag.xaml
    /// </summary>
    public partial class AddContainsTag : Window
    {

        private List<string> existingContainsTags;
        private string editString;

        public AddContainsTag(List<string> ExistingContainsTags, string EditString="")
        {
            InitializeComponent();
            existingContainsTags = ExistingContainsTags;
            editString = EditString;
            Initialize();
        }

        private void Initialize()
        {
            if (editString != "")
            {
                this.Title = "Edit Contains Tag";
                btnAdd.Content = "Save";
                txtName.Text = editString;
            }
        }

        private bool Validate()
        {
            return (!string.IsNullOrEmpty(txtName.Text) && (!txtName.Text.Any(ch => !(Char.IsLetterOrDigit(ch) || Char.IsWhiteSpace(ch) || ch == '_'))));
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    if ((existingContainsTags.Any(x => x == txtName.Text)))
                    {
                        DlkUserMessages.ShowError("Contains tag already exists. Please enter a different partial match string");
                    }
                    else
                    {
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
