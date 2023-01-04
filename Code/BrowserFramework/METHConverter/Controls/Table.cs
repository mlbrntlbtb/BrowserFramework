using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace METHConverter.Controls
{
    public class Table
    {
        public string GetTableRowWithColumnValue(string Control, string ColumnHeader, string Value, string VariableName)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Table mTable = new Table(sDriver,").Append(Control).AppendLine(");");
            action.Append(VariableName).Append(" = mTable.GetTableRowWithColumnValue(\"").Append(ColumnHeader).Append("\",\"").Append(Value).AppendLine("\");");
            return action.ToString();
        }

        public string ClickTableCellByRowColumn(string Control, string Row, string ColumnHeader)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Table mTable = new Table(sDriver,").Append(Control).AppendLine(");");
            action.Append("mTable.ClickTableCellByRowColumn(").Append(Row).Append(",\"").Append(ColumnHeader).AppendLine("\");");
            return action.ToString();
        }

        public string VerifyTableCellValue(string Control, string Row, string ColumnHeader, string ExpectedValue)
        {
            StringBuilder action = new StringBuilder();
            action.Append("Table mTable = new Table(sDriver,").Append(Control).AppendLine(");");
            action.Append("Function.AssertEqual(\"").Append(ExpectedValue.ToLower()).Append("\", ");
            action.Append("mTable.GetTableCellValue(").Append(Row).Append(",\"").Append(ColumnHeader).AppendLine("\"));");
            return action.ToString();
        }
    }
}
