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

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ScheduleOptions.xaml
    /// </summary>
    public partial class ScheduleOptions : Window
    {
        private string mSuitePath;
        private string mScheduleFilePath;
        private List<DlkScheduleRecord> mSchedRecs;

        public ScheduleOptions(string ScheduleFilePath, string SuitePath, List<DlkScheduleRecord> SchedRecs)
        {
            InitializeComponent();
            mScheduleFilePath = ScheduleFilePath;
            mSuitePath = SuitePath;
            mSchedRecs = SchedRecs;
        }
    }
}
