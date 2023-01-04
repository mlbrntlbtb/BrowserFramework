using System;
using System.Windows.Data;
using System.Globalization;
using System.Xml;

namespace TestRunner.Common
{
    public class DlkTruncateString : IValueConverter 
    {
         public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int maxChars = 100;
            string valueToConvert = string.Empty;
            if (value != null)
            {
                if (value.GetType() == typeof(XmlAttribute))
                {
                    valueToConvert = ((XmlAttribute)value).Value.ToString().Trim();
                }
                else if (value.GetType() == typeof(string))
                {
                    valueToConvert = value.ToString().Trim();
                }

                return valueToConvert.Length <= maxChars ? valueToConvert : valueToConvert.Substring(0, maxChars) + "...";
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
