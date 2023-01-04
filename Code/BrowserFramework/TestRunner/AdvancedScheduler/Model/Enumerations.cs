using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestRunner.AdvancedScheduler.Model
{
    public static class Enumerations
    {
        public enum RecurrenceType
        {
            Once,
            Daily,
            Weekly,
            Weekdays,
            Monthly
        }

        public enum TestStatus
        {
            Ready,
            Pending,
            Running,
            Warning,
            Passed, 
            Failed,
            Error,
            Cancelling,
            Cancelled,
            Disconnected,
            Skipped,
            None
        }

        public enum AgentStatus
        {
            Offline,
            Disabled,
            Updating,
            Ready,
            Busy,
            Error,
            Warning,
            Reserved,
            Null
        }

        public enum ExetrnalScriptType
        {
            [Description("Pre-execution")]
            PreExecution,
            [Description("Post-execution")]
            PostExecution
        }

        /// <summary>
        /// used specifically to track execution status in relation to its agent
        /// </summary>
        public enum AgentExecutionStatus
        {
            Started,
            Error,
            Waiting,
            Rerun,
            NextInGroup,
            ToRemove,
            Disconnected
        }

        public static string ConvertToString(object EnumObject)
        {
            return Enum.GetName(EnumObject.GetType(), EnumObject);
        }

        /// <summary>
        /// Used to get the ENUM value from a given description
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new Exception("Description not found.");
        }

        /// <summary>
        /// Gets the description with the given enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }

    public class EnumerationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Enumerations.ConvertToString(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
