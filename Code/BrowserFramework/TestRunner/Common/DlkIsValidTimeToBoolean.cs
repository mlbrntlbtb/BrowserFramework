using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestRunner.Common
{
    class DlkIsValidTimeToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string value2 = value.ToString();
            if (!string.IsNullOrEmpty(value2))
            {
                if (DlkString.IsValidTime(value2))
                {
                    return "True";
                }
                else
                {
                    value2 = value2.Trim('~');
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
