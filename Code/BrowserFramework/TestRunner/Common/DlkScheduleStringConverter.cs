using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace TestRunner.Common
{
    public class DlkScheduleStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string value2 = value.ToString();
            if (!string.IsNullOrEmpty(value2))
            {
                if (IsValidTime(value2))
                {
                    return (DateTime.Parse(value2)).ToString("HH:mm");
                }
                else
                {
                    value2 = value2.Trim('~');
                    //return "Queued after " + System.IO.Path.GetFileName(value2).ToString();
                    return " ";

                }
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        CultureInfo culture)
        {
            return value;
        }

        private bool IsValidTime(object timeValue)
        {
            try
            {
                String ts = timeValue.ToString();
                DateTime dt = System.Convert.ToDateTime(ts);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
