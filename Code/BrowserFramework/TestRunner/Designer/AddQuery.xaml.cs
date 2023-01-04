using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using TestRunner.Common;
using TestRunner.Designer.View;
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;
using CommonLib.DlkRecords;
using CommonLib.DlkSystem;


namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for AddQuery.xaml
    /// </summary>
    public partial class AddQuery : Window, IAddQueryView
    {
        private List<QueryTag> mQryTags = new List<QueryTag>();
        private List<QueryCol> mAllColumns = new List<QueryCol>();
        private List<QueryRow> mAllRows = new List<QueryRow>();
        private List<QueryTag> mContainsTags = new List<QueryTag>();
        private string originalCol = "";
        private string originalRow = "";
        private bool isNewRow = false;
        private bool isNewCol = false;
        public AddQuery(Enumerations.AddQueryMode AddQueryMode, List<DlkTag> AllTags, QueryRow QRow, QueryCol QCol, List<QueryCol> AllColumns, List<QueryRow> AllRows = null)
        {
            InitializeComponent();

            mMode = AddQueryMode;
            mTags = AllTags;
            mRow = QRow;
            mCol = QCol;
            mQryTags = mCol.QTags.Select(x => x.Clone()).ToList();
            mAllColumns = AllColumns;
            if (AllRows != null)
            {
                mAllRows = AllRows;
            }
            Initialize();
        }

        private void Initialize()
        {
            // load datacontexts
            cboSubQAOpr.ItemsSource = Operators;
            cboSubQAOpr.DataContext = Col;
            cboSubQAOpr.Text = "And";

            cboColOperators.ItemsSource = ColumnOperators;
            cboColumn1.ItemsSource = AllColumns;
            cboColumn2.ItemsSource = AllColumns;
            cboDecimalPlaces.ItemsSource = DecimalPlaces;

            switch (Mode)
            {
                case Enumerations.AddQueryMode.AddNewQuery:
                    txtName.DataContext = Row;
                    lblTargetName.Text = "Row Name";
                    this.Title = "Add Row";
                    stpColumnType.Visibility = System.Windows.Visibility.Collapsed;
                    isNewRow = true;
                    break;
                case Enumerations.AddQueryMode.EditQuery:
                    txtName.DataContext = Row;
                    lblTargetName.Text = "Row Name";
                    this.Title = "Edit Row";
                    stpColumnType.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                case Enumerations.AddQueryMode.AddQueryModifier:
                    txtName.DataContext = Col;
                    lblTargetName.Text = "Column Name";
                    this.Title = "Add Column";
                    stpColumnType.Visibility = System.Windows.Visibility.Visible;
                    isNewCol = true;
                    break;
                case Enumerations.AddQueryMode.EditQueryModifier:
                    txtName.DataContext = Col;
                    lblTargetName.Text = "Column Name";
                    this.Title = "Add Column";
                    stpColumnType.Visibility = System.Windows.Visibility.Collapsed;
                    break;
            }
            dgSubQA.ItemsSource = mQryTags;
            IsTaggedAsList = new List<string>(new string[] {"tagged as", "NOT tagged as"});
            originalCol = mCol.Name;
            originalRow = mRow.Name;
        }


        private Enumerations.AddQueryMode mMode;
        public Enumerations.AddQueryMode Mode
        {
            get
            {
                return mMode;
            }
            set
            {
                mMode = value;
                switch (mMode)
                {
                    case Enumerations.AddQueryMode.AddQueryModifier:
                        break;
                    case Enumerations.AddQueryMode.EditQuery:
                        break;
                    case Enumerations.AddQueryMode.EditQueryModifier:
                        break;
                    case Enumerations.AddQueryMode.AddNewQuery:
                    default:
                        break;

                }
            }
        }

        private List<string> mOperators;
        public List<string> Operators
        {
            get
            {
                return new List<string>(new string[] {Enumerations.ConvertToString(Enumerations.QueryOperator.And), 
                    Enumerations.ConvertToString(Enumerations.QueryOperator.Or)});
            }
            set
            {
                mOperators = value;
            }
        }

        //private List<string> mColumnOperators;
        public List<string> ColumnOperators
        {
            get
            {
                return new List<string>(new string[] {"/ [DIVIDE]", "x [MULTIPLY]", "+ [ADD]", "- [SUBTRACT]"}); 
            }
        }


        public List<string> AllColumns
        {
            get
            {
                List<string> ret = new List<string>(mAllColumns.ConvertAll(x => (x.Index + 2).ToString() + " - " + x.Name));
                ret.Insert(0, "1 - TOTAL");
                return ret;
            }
        }

        public List<int> DecimalPlaces
        {
            get
            {
                return new List<int>(new int[]{0, 1, 2});
            }
        }

        private Enumerations.QueryOperator mSubQueryAOperator;
        public Enumerations.QueryOperator SubQueryAOperator
        {
            get
            {
                return mSubQueryAOperator;
            }
            set
            {
                mSubQueryAOperator = value;
            }
        }

        private Enumerations.QueryOperator mSubQueryBOperator;
        public Enumerations.QueryOperator SubQueryBOperator
        {
            get
            {
                return mSubQueryBOperator;
            }
            set
            {
                mSubQueryBOperator = value;
            }
        }

        private List<QueryTag> mSubQueryA;
        public List<QueryTag> SubQueryA
        {
            get
            {
                return mSubQueryA;
            }
            set
            {
                mSubQueryA = value;
            }
        }

        private List<QueryTag> mSubQueryB;
        public List<QueryTag> SubQueryB
        {
            get
            {
                return mSubQueryB;
            }
            set
            {
                mSubQueryB = value;
            }
        }

        private List<DlkTag> mTags;
        public List<DlkTag> Tags
        {
            get
            {
                return mTags;
            }
            set
            {
                mTags = value;
            }
        }

        private QueryRow mRow;
        public QueryRow Row
        {
            get
            {
                return mRow;
            }
            set
            {
                mRow = value;
            }

        }

        private QueryCol mCol;
        public QueryCol Col
        {
            get
            {
                return mCol;
            }
            set
            {
                mCol = value;
            }
        }

        private void btnSubQryAManageTags_Click(object sender, RoutedEventArgs e)
        {
            //List<DlkTag> myList = new List<DlkTag>(mQryTags.Select(x => new DlkTag(Tags.Find(y => y.Id == x.TagId).Id,
            //    Tags.Find(y => y.Id == x.TagId).Name, Tags.Find(y => y.Id == x.TagId).Description)).ToArray());

            List<DlkTag> myList = new List<DlkTag>();
            mContainsTags.Clear();
            foreach (QueryTag tg in mQryTags)
            {
                if (mTags.Any(x => x.Id == tg.TagId))
                {
                    myList.Add(mTags.Find(x => x.Id == tg.TagId));
                }
                else if (tg.TagId == "0")
                {
                    DlkTag newTag = new DlkTag(tg.Contains, "");
                    newTag.Id = "0";
                    myList.Add(newTag);
                }
            }

            ManageTags mt = new ManageTags(mTags, myList);
            mt.Owner = this;
            if ((bool)mt.ShowDialog())
            {
                foreach (DlkTag tg in myList)
                {
                    QueryTag qt = null;
                    if (tg.Id == "0") // contains tag
                    {
                        if (!mQryTags.Any(x => x.Contains == tg.Name && x.TagId == "0")) // newly added contains tag
                        {
                            qt = new QueryTag(tg.Id, null, -1, tg.Name);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!mQryTags.Any(x => x.TagId == tg.Id))
                        {
                            qt = new QueryTag(tg.Id, true, -1, null);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    mQryTags.Add(qt);
                    qt.ListPosition = mQryTags.Count;   
                }
                for (int num = mQryTags.Count-1; num >= 0; num--) // for removed tags
                {
                    if (mQryTags[num].Is != null) // is a regular tag
                    {
                        if (!myList.Any(x => x.Id == mQryTags[num].TagId)) // recently removed
                        {
                            mQryTags.RemoveAt(num);
                            continue;
                        }
                    }
                    else
                    {
                        if (!myList.Any(x => x.Name == mQryTags[num].Contains && x.Id == "0"))
                        {
                            mQryTags.RemoveAt(num);
                            continue;
                        }
                    }
                    mQryTags[num].ListPosition = num;
                }
                RefreshTagGrid();
            }
        }

        public List<string> IsTaggedAsList
        {
            get;
            set;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            // Assemble qtags
            if (isNewRow && mAllRows.FindAll(x => x.Name == txtName.Text).Count > 0) // Newly added row
            {
                DlkUserMessages.ShowError("Row with matching name already exists. Please change the row name.");
            }
            else if (mAllRows.Count > 0 && mAllRows.FindAll(x => x.Name == txtName.Text && x.Name != originalRow).Count > 1) // Edited row
            {
                DlkUserMessages.ShowError("Row with matching name already exists. Please change the row name.");
            }
            else if (isNewCol && mAllColumns.FindAll(x => x.Name == txtName.Text).Count > 0) // Newly added col
            {
                DlkUserMessages.ShowError("Column with matching name already exists. Please change the column name.");
            } 
            else if (mAllRows.Count == 0 && mAllColumns.FindAll(x => x.Name == txtName.Text && x.Name != originalCol).Count > 0) // Edited col
            {
                DlkUserMessages.ShowError("Column with matching name already exists. Please change the column name.");
            }            
            else
            {
                // Updated qcol
                if ((bool)rdoSubQuery.IsChecked)
                {
                    mCol.Operator = (Enumerations.QueryOperator)Enum.Parse(typeof(Enumerations.QueryOperator), cboSubQAOpr.Text);
                    mCol.QTags = mQryTags;
                }
                else
                {
                    switch (cboColOperators.SelectedIndex)
                    {
                        case 0:
                            mCol.Operator = Enumerations.QueryOperator.Quotient;
                            break;
                        case 1:
                            mCol.Operator = Enumerations.QueryOperator.Product;
                            break;
                        case 2:
                            mCol.Operator = Enumerations.QueryOperator.Sum;
                            break;
                        case 3:
                            mCol.Operator = Enumerations.QueryOperator.Difference;
                            break;
                    }
                }
                if (Mode == Enumerations.AddQueryMode.AddNewQuery || Mode == Enumerations.AddQueryMode.EditQuery)
                {
                    mRow.Name = txtName.Text;
                }
                else if (Mode == Enumerations.AddQueryMode.AddQueryModifier)
                {
                    mCol.Name = txtName.Text;
                }
                else if (Mode == Enumerations.AddQueryMode.EditQueryModifier)
                {
                    mAllColumns[mCol.Index] = mCol;
                }

                // Upda
                DialogResult = true;
            }
        }

        private void cboIs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBox cbx = sender as ComboBox;

            //QueryTag qt = dgSubQA.SelectedItem as QueryTag;
            //if (qt != null)
            //{
            //    qt.Is = cbx.Text == "tagged as" ? true : false;
            //}
        }

        private void cboDecimalPlaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double num = 75;
            string decimalplace = cboDecimalPlaces.SelectedItem.ToString();
            string format = num.ToString("F" + decimalplace);
            if ((bool)chkPercentage.IsChecked)
            {
                format += "%";
            }
            txtFormatPreview.Text = format;
        }

        private void chkPercentage_Changed(object sender, RoutedEventArgs e)
        {
            string format = txtFormatPreview.ToString();
            if ((bool)chkPercentage.IsChecked)
            {
                txtFormatPreview.Text += "%";
            }
            else
            {
                txtFormatPreview.Text = txtFormatPreview.Text.TrimEnd(new char[] { '%' });
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void rdoSubQuery_Checked(object sender, RoutedEventArgs e)
        {
            if (grpSubQuery == null || grpColOperations == null)
            {
                return;
            }
            grpSubQuery.Visibility = System.Windows.Visibility.Visible;
            grpColOperations.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void rdoColumnOperation_Checked(object sender, RoutedEventArgs e)
        {
            if (grpSubQuery == null || grpColOperations == null)
            {
                return;
            }
            grpSubQuery.Visibility = System.Windows.Visibility.Collapsed;
            grpColOperations.Visibility = System.Windows.Visibility.Visible;
        }

        private void RefreshTagGrid()
        {
            if (mContainsTags.Count > 0)
            {
                mQryTags.AddRange(mContainsTags);
                mContainsTags.Clear();
            }
            mQryTags = mQryTags.OrderBy(x => x.ListPosition).ToList();
            dgSubQA.ItemsSource = mQryTags;
            dgSubQA.Items.Refresh();
        }
    }

    public class TagIdConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string tagid = values[0].ToString();
            string tagcontains = values[1] != null ? values[1].ToString() : String.Empty;
            if (tagid == "0")
            {
                return tagcontains;
            }
            else
            {
                string mTagsFile = System.IO.Path.Combine(DlkEnvironment.mDirFramework, @"Library\Tags\tags.xml");
                XDocument doc = XDocument.Load(mTagsFile);

                var tags = from tag in doc.Descendants("tag")
                           select new
                           {
                               id = tag.Attribute("id").Value.ToString(),
                               name = tag.Attribute("name").Value.ToString()
                           };

                var tagname = tags.ToList().FindAll(x => x.id == tagid).Any() ? tags.ToList().FindAll(x => x.id == tagid).First().name : string.Empty;
                return tagname;
            }
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class TagOperatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enumerations.ConvertToString((Enumerations.QueryOperator)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Enumerations.QueryOperator)Enum.Parse(typeof(Enumerations.QueryOperator), value.ToString());
        }
    }

    public class TagIsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? isValue = null;
            if (value == null)
            {
                return "contains";
            }
            else
            {
                isValue = bool.Parse(value.ToString());
                if ((bool)isValue)
                {
                    return "tagged as";
                }
                else
                {
                    return "NOT tagged as";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == "tagged as")
            {
                return true;
            }
            else if (value.ToString() == "NOT tagged as")
            {
                return false;
            }
            else
            {
                return null;
            }
        }
    }

    public class TagContainsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value != null ? Visibility.Visible : Visibility.Hidden);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
