using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestRunner.Designer.Model
{
    public static class Enumerations
    {
        public enum QueryOperator
        {
            And,
            Or,
            Sum,
            Difference,
            Product,
            Quotient
        }

        public enum QueryType
        {
            Test,
            Suite,
            Invalid
        }

        public enum QueryRowColor
        {
            Gray,
            Yellow,
            Orange
        }

        public enum QueryTagType
        {
            SubQuery,
            Column
        }

        public enum AddQueryMode
        {
            AddNewQuery,
            EditQuery,
            AddQueryModifier,
            EditQueryModifier,
        }

        public enum TestViewStatus
        {
            NewSuiteAdded,
            SelectedSuiteEdited
        }

        public enum FinderViewStatus
        {
            NewTestAdded,
            SelectedTestEdited
        }

        public enum BatchReplaceType
        {
            Control,
            Parameter
        }

        public static string ConvertToString(object EnumObject)
        {
            return Enum.GetName(EnumObject.GetType(), EnumObject);
        }
    }
}
