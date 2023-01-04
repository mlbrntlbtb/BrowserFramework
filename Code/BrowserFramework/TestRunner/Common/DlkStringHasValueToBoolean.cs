using System;
using System.Windows.Data;
using System.Globalization;
using System.Xml;

namespace TestRunner.Common
{
    public class DlkStringHasValueToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(XmlAttribute))
                {
                    return !string.IsNullOrEmpty(((XmlAttribute)value).Value.ToString());
                }
                else if (value.GetType() == typeof(string))
                {
                    return !string.IsNullOrEmpty(value.ToString());
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
