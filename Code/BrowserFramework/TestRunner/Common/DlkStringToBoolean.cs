using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Xml;

namespace TestRunner.Common
{
    public class DlkStringToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if(value.GetType() == typeof(XmlAttribute))
                {
                    switch (((XmlAttribute)value).Value.ToString().ToLower())
                    {
                        case "true":
                            return true;
                        case "false":
                            return false;
                    }
                }
                else if (value.GetType() == typeof(string))
                {
                    return bool.Parse(value.ToString());
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
            {
                if ((bool)value == true)
                    return "true";
                else
                    return "false";
            }

            return "false";
        }
    }

    public class DlkStringToBooleanInverted : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.GetType() == typeof(XmlAttribute))
                {
                    switch (((XmlAttribute)value).Value.ToString().ToLower())
                    {
                        case "true":
                            return false;
                        case "false":
                            return true;
                    }
                }
                else if (value.GetType() == typeof(string))
                {
                    return !bool.Parse(value.ToString());
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "false";
                else
                    return "true";
            }

            return "false";
        }
    }

}
