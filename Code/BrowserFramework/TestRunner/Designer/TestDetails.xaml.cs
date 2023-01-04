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
using TestRunner.Designer.Model;
using TestRunner.Designer.Presenter;
using TestRunner.Designer.View;
using CommonLib.DlkHandlers;
using System.IO;
using CommonLib.DlkSystem;
using TestRunner.Common;

namespace TestRunner.Designer
{
    /// <summary>
    /// Interaction logic for TestViewer.xaml
    /// </summary>
    public partial class TestDetails : Window
    {
        #region PRIVATE MEMBERS
        private Match mTestMatchInfo;
        private DlkTest mTest;
        #endregion

        #region PRIVATE METHODS
        private void dgStepResults_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            SolidColorBrush mNotFoundBrush = new SolidColorBrush(Colors.White);
            SolidColorBrush mFoundBrush = new SolidColorBrush(Colors.LightGreen);
            if (e.Row.Item is DlkTestStepRecord)
            {
                DlkTestStepRecord mRowRec = (DlkTestStepRecord)e.Row.DataContext;
                e.Row.Background = mTestMatchInfo.MatchedRows.Exists(x => x.mStepNumber == mRowRec.mStepNumber && x.mScreen == mRowRec.mScreen 
                                    && x.mKeyword == mRowRec.mKeyword && x.mControl == mRowRec.mControl) 
                    ? mFoundBrush : mNotFoundBrush;
                return;
            }
        }


        /// <summary>
        /// Helper function to return visual children of input control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void PopulateTestSteps()
        {
            if (File.Exists(mTestMatchInfo.Path))
                mTest = new DlkTest(mTestMatchInfo.Path);
            dgStepResults.DataContext = mTest.mTestSteps;
            /* refresh UI */
            dgStepResults.Items.Refresh();
        }
        #endregion

        #region CONSTRUCTOR
        public TestDetails(Match Test)
        {
            InitializeComponent();
            mTestMatchInfo = Test;
            lblTestName.Content = Test.TestName;
            tbxFolderPath.Text = Test.Path;
            lblMatchCount.Content = Test.MatchCount + " / " + Test.StepCount;
            PopulateTestSteps();
        }
        #endregion

    }
}
