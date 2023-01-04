using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestRunner.Common
{
    public class DlkReportTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            var report = item as DlkReportContainer;
            
            if (report == null)
                return null;

            if (report.Report is DlkSummaryReport)
            {
                return element.FindResource("ExecutionSummaryChart") as DataTemplate;
            }
            
            return null;
        }
    }
}
