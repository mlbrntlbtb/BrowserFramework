using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestRunner.Common
{
    class DlkVariableParameterChecker : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string value2 = value.ToString();
            if (!String.IsNullOrEmpty(value2))
            {
                if (value2.Contains("D{"))
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            else
            {
                return "False";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
