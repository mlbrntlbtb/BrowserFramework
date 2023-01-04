using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Data;
using Microsoft.VisualBasic.FileIO;
namespace CommonLib.DlkUtility
{
    public class DlkCSVHelper
    {
        public static DataTable CSVParse(string path)
        {
            long lineNumber = 0;
            DataTable dt = new DataTable();
            string[] fields = null;
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.Delimiters = new string[] { "," };
                parser.HasFieldsEnclosedInQuotes = true; // for commas inside fields
                while (!parser.EndOfData)
                {
                    try
                    {
                        lineNumber = parser.LineNumber;
                        fields = parser.ReadFields();
                        if (lineNumber > 1) // if data field
                        {
                            dt.Rows.Add(fields);
                        }
                        else if (lineNumber == 1) // if first line (header)
                        {
                            foreach (string header in fields)
                            {
                                dt.Columns.Add(header);
                            }
                        }
                    }
                    catch (MalformedLineException) /* for unescaped/trailing double quotes (only happens when user erases escape-string intended double-quotes outside of Test Runner, which breaks CSV standards) */
                    {
                        string error = "There is a problem parsing the CSV file on line " + lineNumber.ToString() + ".\n\nPlease ensure that double-quotes inside data fields are preceded by another double-quote.\n\nRe-exporting the data using Test Editor can also fix this.";
                        MessageBox.Show(error, "Import Data", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
                        parser.Close();
                        parser.Dispose();
                        return null;
                    }
                    catch // other exceptions handled by main function
                    {
                        parser.Close();
                        parser.Dispose();
                        throw;
                    }
                }
                parser.Close();
                parser.Dispose();
                return dt;
            }
        }
    }
}
