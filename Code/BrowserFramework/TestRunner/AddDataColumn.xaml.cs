using System;
using System.Linq;
using System.Windows;
using CommonLib.DlkRecords;
using TestRunner.Common;
using CommonLib.DlkSystem;
using System.Text.RegularExpressions;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddHost.xaml
    /// </summary>
    public partial class AddDataColumn : Window
    {
        private DlkData m_Data;
        private string m_colToRename = "";
        private int insertIndex=-1;
        public AddDataColumn(DlkData Data, string ColumnToRename="", int InsertIndex=-1)
        {
            InitializeComponent();
            m_colToRename = ColumnToRename;
            insertIndex = InsertIndex;
            m_Data = Data;
        }


        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    if (!IsColumnExist(txtName.Text))
                    {
                        if (insertIndex >= 0)
                        {
                            Insert(insertIndex);
                        }
                        else if (string.IsNullOrEmpty(m_colToRename))
                        {
                            Add();
                        }
                        else
                        {
                            Rename();
                        }
                        //SaveHostsToFile(mHosts);
                        //this.Close();
                        this.DialogResult = true;
                    }
                    else
                    {
                        DlkUserMessages.ShowError(DlkUserMessages.ERR_COLUMNNAME_EXISTS_NEWCOLUMN_FAILED);
                    }

                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_COLUMNNAME_INVALID);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private bool IsColumnExist(string ColumnName)
        {
            bool ret = false;
            foreach (DlkDataRecord rec in m_Data.Records)
            {
                if (rec.Name == ColumnName)
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        private bool Validate()
        {
            return (!string.IsNullOrEmpty(txtName.Text) && (!txtName.Text.StartsWith("_") && new Regex("^[A-Za-z0-9]*(?:_[A-Za-z0-9]+)*$").IsMatch(txtName.Text)));
        }

        private void Add()
        {
             DlkDataRecord rec = new DlkDataRecord();
             rec.Name = txtName.Text;
             if (m_Data.Records.Count > 0 && m_Data.Records.First().Values.Count > 0)
             {
                 for (int i=0; i < m_Data.Records.First().Values.Count; i++)
                 {
                     rec.Values.Add(string.Empty);
                 }
             }
             m_Data.Records.Add(rec);
        }
        private void Insert(int insertindex)
        {
             DlkDataRecord rec = new DlkDataRecord();
             rec.Name = txtName.Text;
             if (m_Data.Records.Count > 0 && m_Data.Records.First().Values.Count > 0)
             {
                 for (int i = 0; i < m_Data.Records.First().Values.Count; i++)
                 {
                     rec.Values.Add(string.Empty);
                 }
             }
             m_Data.Records.Insert(insertindex, rec);
        }
        private void Rename()
        {
             foreach (DlkDataRecord rec in m_Data.Records)
             {
                 if (m_colToRename == rec.Name)
                 {
                     rec.Name = txtName.Text;
                     break;
                 }
             }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
                //this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtName_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //renaming a column - set current column name in text box and highlight
                if (!string.IsNullOrEmpty(m_colToRename))
                {
                    txtName.Text = m_colToRename;
                    txtName.SelectAll();
                }

                txtName.Focus();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}
