using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace CommonLib.DlkUtility
{
    /// <summary>
    /// This class holds string utility functions
    /// </summary>
    public static class DlkString
    {
        private const string STR_NONBREAKING_SPACE = "&nbsp;";
        private const string STR_NORMAL_SPACE = " ";

        public static String RemoveCarriageReturn(String InputString)
        {
            String sResult = "";
            sResult = InputString.Replace("\n\r"," ");
            sResult = sResult.Replace("\r\n", " ");
            sResult = sResult.Replace("\n"," ");
            sResult = sResult.Replace("\r"," ");
            return sResult;
        }

        public static String ReplaceCarriageReturn(String InputString, String ReplacementString)
        {
            String sResult = "";
            sResult = InputString.Replace("\n\r", ReplacementString);
            sResult = sResult.Replace("\r\n", ReplacementString);
            sResult = sResult.Replace("\n", ReplacementString);
            sResult = sResult.Replace("\r", ReplacementString);
            return sResult;
        }

        public static String GetStringBeforeBreakingSpace(String InputString)
        {
            String sResult = "";
            sResult = InputString.Substring(0, InputString.IndexOf("\r\n"));
            return sResult;
        }

        public static String GetDateAsText(DateTime InputDate, String FormatType)
        {
            String sDate = "";
            switch (FormatType.ToLower())
            {
                case @"mm/dd/yyyy":
                    sDate = string.Format("{0:MM/dd/yyyy}", InputDate);
                    break;
                case @"m/d/yyyy":
                    sDate = string.Format("{0:M/d/yyyy}", InputDate);
                    break;
                case @"mm/dd/yy":
                    sDate = string.Format("{0:MM/dd/yy}", InputDate);
                    break;
                case @"dd/mm/yyyy":
                    sDate = string.Format("{0:dd/MM/yyyy}", InputDate);
                    break;
                case @"d/m/yyyy":
                    sDate = string.Format("{0:d/M/yyyy}", InputDate);
                    break;
                case @"dd/mm/yy":
                    sDate = string.Format("{0:dd/MM/yy}", InputDate);
                    break;
                case "yyyymmdd":
                    sDate = string.Format("{0:yyyyMMdd}", InputDate);
                    break;
                case "yyyy-mm-dd":
                    sDate = string.Format("{0:yyyy-MM-dd}", InputDate);
                    break;
                case "yymmdd":
                    sDate = string.Format("{0:yyMMdd}", InputDate);
                    break;
                case "yy-mm-dd":
                    sDate = string.Format("{0:yy-MM-dd}", InputDate);
                    break;
                case "yymmddhhmmss":
                    sDate = string.Format("{0:yyMMddHHmmss}", InputDate);
                    break;
                case "yy-mm-dd hh:mm:ss":
                    sDate = string.Format("{0:yy-MM-dd HH:mm:ss}", InputDate);
                    break;
                case "yyyymmddhhmmss":
                case "file":
                    sDate = string.Format("{0:yyyyMMddHHmmss}", InputDate);
                    break;
                case "yyyy-mm-dd hh:mm:ss":
                    sDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", InputDate);
                    break;
                case "yyyymmddhhmmsstt":
                    sDate = string.Format("{0:yyyyMMddhhmmsstt}", InputDate);
                    break;
                case "yyyy-mm-dd hh:mm:ss tt":
                case "long":
                    sDate = string.Format("{0:yyyy-MM-dd hh:mm:ss tt}", InputDate);
                    break;
                case "monthyear":
                    sDate = string.Format("{0:MMM yy}", InputDate);
                    break;
                case "startofmonth":
                    sDate = string.Format("{0:1-MMM-yy}", InputDate);
                    break;
                case "endofmonth":
                    DateTime mDate = InputDate;
                    int i = 0;
                    switch (mDate.Month)
                    {
                        case 2:
                            i = 28;
                            int iLeapYr = 2400;
                            while (iLeapYr > 1900)
                            {
                                if (mDate.Year == iLeapYr)
                                {
                                    i = 29;
                                    break;
                                }
                                iLeapYr = iLeapYr - 4;
                            }
                            break;
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            i = 30;
                            break;
                        default:
                            i = 31;
                            break;
                    }
                    sDate = i.ToString() + "-" + string.Format("{0:MMM-yy}", mDate);
                    break;
                case "mmddyy":
                    sDate = string.Format("{0:MMddyy}", InputDate);
                    break;
                case "ddmmyy":
                    sDate = string.Format("{0:ddMMyy}", InputDate);
                    break;
                case "dddd, mmmm dd, yyyy":
                    sDate = string.Format("{0:dddd, MMMM dd, yyyy}", InputDate);
                    break;
                case @"m/dd":
                    sDate = string.Format("{0:M/dd}", InputDate);
                    break;
                case "dd-mmm":
                    sDate = string.Format("{0:dd-MMM}", InputDate);
                    break;
                case "mmm dd":
                    sDate = string.Format("{0:MMM dd}", InputDate);
                    break;
                case "dd-mmm-yy":
                    sDate = string.Format("{0:dd-MMM-yy}", InputDate);
                    break;
                case "mmm-dd":
                    sDate = string.Format("{0:MMM-dd}", InputDate);
                    break;
                case "mmm dd, yyyy":
                    sDate = string.Format("{0:MMM dd, yyyy}", InputDate);
                    break;
                case "dd-mmm-yyyy":
                    sDate = string.Format("{0:dd-MMM-yyyy}", InputDate);
                    break;
                case "dd-mm-yyyy":
                    sDate = string.Format("{0:dd-MM-yyyy}", InputDate);
                    break;
                case "mm-dd-yyyy":
                    sDate = string.Format("{0:MM-dd-yyyy}", InputDate);
                    break;
                case "mm-dd-yy":
                    sDate = string.Format("{0:MM-dd-yy}", InputDate);
                    break;
                case @"m/dd/yy h:mm tt":
                    sDate = string.Format("{0:M/dd/yy h:mm tt}", InputDate);
                    break;
                case @"m/dd/yy hh:mm tt":
                    sDate = string.Format("{0:M/dd/yy hh:mm tt}", InputDate);
                    break;
                case @"m/dd/yy h:mm":
                    sDate = string.Format("{0:M/dd/yy H:mm}", InputDate);
                    break;
                case @"mm/dd/yyyy hh:mm":
                    sDate = string.Format("{0:MM/dd/yyyy hh:mm}", InputDate);
                    break;
                case "dddd":
                    sDate = string.Format("{0:dddd}", InputDate);
                    break;
                case "monthonly":
                    sDate = string.Format("{0:MMMM}", InputDate);
                    break;
                case "d mmmm yyyy":
                    sDate = string.Format("{0:d MMMM yyyy}", InputDate);
                    break;
                case "d mmm yyyy":
                    sDate = string.Format("{0:d MMM yyyy}", InputDate);
                    break;
                case "dd mmm yyyy":
                    sDate = string.Format("{0:dd MMM yyyy}", InputDate);
                    break;
                case "mmmm d, yyyy":
                    sDate = string.Format("{0:MMMM d, yyyy}", InputDate);
                    break;
                case "m/d/yy":
                    sDate = string.Format("{0:M/d/yy}", InputDate);
                    break;
                case "d/m/yy":
                    sDate = string.Format("{0:d/M/yy}", InputDate);
                    break;
                case "dd-mm-yy":
                    sDate = string.Format("{0:dd-MM-yy}", InputDate);
                    break;
                case "dd.mm.y":
                    sDate = string.Format("{0:dd.MM.yyyy}", InputDate);
                    break;
                case "dd.mm.yy":
                    sDate = string.Format("{0:dd.MM.yy}", InputDate);
                    break;
                case "dd/mm/y":
                    sDate = string.Format("{0:dd/MM/yyyy}", InputDate);
                    break;
                case "y-mm-dd":
                    sDate = string.Format("{0:yyyy-MM-dd}", InputDate);
                    break;
                case "yyyy.mm.dd":
                    sDate = string.Format("{0:yyyy.MM.dd}", InputDate);
                    break;
                case "ddd mmm d":
                    sDate = string.Format("{0:ddd MMM d}", InputDate);
                    break;
                case "day":
                case "d":
                    sDate = InputDate.Day.ToString();
                    break;
                case "year":
                case "yyyy":
                    sDate = InputDate.Year.ToString();
                    break;
                default:
                    sDate = string.Format("{0:yyyy-MM-dd hh:mm:ss tt}", InputDate);
                    break;
            }
            return sDate;
        }

        public static String GetDateAsText(String FormatType)
        {
            String sDate = "";
            switch (FormatType.ToLower())
            {
                case @"mm/dd/yyyy":
                    sDate = string.Format("{0:MM/dd/yyyy}", DateTime.Now);
                    break;
                case @"m/d/yyyy":
                    sDate = string.Format("{0:M/d/yyyy}", DateTime.Now);
                    break;
                case @"mm/dd/yy":
                    sDate = string.Format("{0:MM/dd/yy}", DateTime.Now);
                    break;
                case @"dd/mm/yyyy":
                    sDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                    break;
                case @"d/m/yyyy":
                    sDate = string.Format("{0:d/M/yyyy}", DateTime.Now);
                    break;
                case @"dd/mm/yy":
                    sDate = string.Format("{0:dd/MM/yy}", DateTime.Now);
                    break;
                case "yyyymmdd":
                    sDate = string.Format("{0:yyyyMMdd}", DateTime.Now);
                    break;
                case "yyyy-mm-dd":
                    sDate = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    break;
                case "yymmdd":
                    sDate = string.Format("{0:yyMMdd}", DateTime.Now);
                    break;
                case "yy-mm-dd":
                    sDate = string.Format("{0:yy-MM-dd}", DateTime.Now);
                    break;
                case "yymmddhhmmss":
                    sDate = string.Format("{0:yyMMddHHmmss}", DateTime.Now);
                    break;
                case "yy-mm-dd hh:mm:ss":
                    sDate = string.Format("{0:yy-MM-dd HH:mm:ss}", DateTime.Now);
                    break;
                case "yyyymmddhhmmss": case "file":
                    sDate = string.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
                    break;
                case "yyyy-mm-dd hh:mm:ss":
                    sDate = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                    break;
                case "yyyymmddhhmmsstt":
                    sDate = string.Format("{0:yyyyMMddhhmmsstt}", DateTime.Now);
                    break;
                case "yyyy-mm-dd hh:mm:ss tt":
                case "long":
                    sDate = string.Format("{0:yyyy-MM-dd hh:mm:ss tt}", DateTime.Now);
                    break;
                case "monthyear":
                    sDate = string.Format("{0:MMM yy}", DateTime.Now);
                    break;
                case "startofmonth":
                    sDate = string.Format("{0:1-MMM-yy}", DateTime.Now);
                    break;
                case "endofmonth":
                    DateTime mDate = DateTime.Now;
                    int i=0;
                    switch (mDate.Month)
                    {
                        case 2:
                            i = 28;
                            int iLeapYr = 2400;
                            while (iLeapYr > 1900)
                            {
                                if (mDate.Year == iLeapYr)
                                {
                                    i = 29;
                                    break;
                                }
                                iLeapYr = iLeapYr - 4;
                            }
                            break;
                        case 4:
                        case 6:
                        case 9:
                        case 11:
                            i = 30;
                            break;
                        default:
                            i = 31;
                            break;
                    }
                    sDate = i.ToString() + "-" + string.Format("{0:MMM-yy}", mDate);
                    break;
                case "mmddyy":
                    sDate = string.Format("{0:MMddyy}", DateTime.Now);
                    break;
                case "ddmmyy":
                    sDate = string.Format("{0:ddMMyy}", DateTime.Now);
                    break;
                case "dddd, mmmm dd, yyyy":
                    sDate = string.Format("{0:dddd, MMMM dd, yyyy}", DateTime.Now);
                    break;
                case @"m/dd":
                    sDate = string.Format("{0:M/dd}", DateTime.Now);
                    break;
                case "dd-mmm":
                    sDate = string.Format("{0:dd-MMM}", DateTime.Now);
                    break;
                case "mmm dd":
                    sDate = string.Format("{0:MMM dd}", DateTime.Now);
                    break;
                case "dd-mmm-yy":
                    sDate = string.Format("{0:dd-MMM-yy}", DateTime.Now);
                    break;
                case "mmm-dd":
                    sDate = string.Format("{0:MMM-dd}", DateTime.Now);
                    break;
                case "mmm dd, yyyy":
                    sDate = string.Format("{0:MMM dd, yyyy}", DateTime.Now);
                    break;
                case "dd-mmm-yyyy":
                    sDate = string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
                    break;
                case "dd-mm-yyyy":
                    sDate = string.Format("{0:dd-MM-yyyy}", DateTime.Now);
                    break;
                case "ddd mmm d":
                    sDate = string.Format("{0:ddd MMM d}", DateTime.Now);
                    break;
                case @"m/dd/yy h:mm tt":
                    sDate = string.Format("{0:M/dd/yy h:mm tt}", DateTime.Now);
                    break;
                case @"m/dd/yy h:mm":
                    sDate = string.Format("{0:M/dd/yy H:mm}", DateTime.Now);
                    break;
                case "dddd":
                    sDate = string.Format("{0:dddd}", DateTime.Now);
                    break;
                case "monthonly":
                    sDate = string.Format("{0:MMMM}", DateTime.Now);
                    break;
                case "d mmmm yyyy":
                    sDate = string.Format("{0:d MMMM yyyy}", DateTime.Now);
                    break;
                case "mmmm d, yyyy":
                    sDate = string.Format("{0:MMMM d, yyyy}", DateTime.Now);
                    break;
                default:
                    sDate = string.Format("{0:yyyy-MM-dd hh:mm:ss tt}", DateTime.Now);
                    break;
            }
            return sDate;
        }

        public static String ToUpperIndex(String Input, int Index)
        {
            string ret = "";
            for (int idx = 0; idx < Input.ToCharArray().Count(); idx++)
            {
                if (idx == Index)
                {
                    ret += char.ToUpper(Input[idx]).ToString();
                }
                else
                {
                    ret += Input[idx].ToString();
                }
            }
            return ret;
        }

        public static bool IsNumeric(string Text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(Text);
        }

        /// <summary>
        /// Check whether input string contains a numeric character
        /// </summary>
        /// <param name="Text">Input text</param>
        /// <returns>TRUE if Text contains numeric char, FALSE otherwise</returns>
        public static bool HasNumericChar(string Text)
        {
            return Text.Any(x => char.IsDigit(x));
        }

        /// <summary>
        /// Check whether input string contains an alphanumeric character
        /// </summary>
        /// <param name="Text">Input text</param>
        /// <returns>TRUE if Text contains alphanumeric char, FALSE otherwise</returns>
        public static bool HasAlphanumericChar(string Text)
        {
            return Text.Any(x => char.IsLetterOrDigit(x));
        }

        public static String GetMatch(String ReferenceString, String Pattern)
        {
            String sMatch = "";
            Regex rx = new Regex(Pattern, RegexOptions.Singleline);
            Match res = rx.Match(ReferenceString);
            if (res.Success)
            {
                sMatch = res.Value;
            }
            return sMatch;
        }

        public static MatchCollection GetAllMatches(String ReferenceString, String Pattern)
        {
            Regex rx = new Regex(Pattern, RegexOptions.Singleline);
            MatchCollection res = rx.Matches(ReferenceString);

            return res;
        }

        public static String NormalizeNonBreakingSpace(String InputString)
        {
            if (InputString.Contains(STR_NONBREAKING_SPACE))
            {
                InputString = InputString.Replace(STR_NONBREAKING_SPACE, STR_NORMAL_SPACE);
            }
            return InputString.Replace(Convert.ToChar(160), Convert.ToChar(32));
        }

        public static String UnescapeXML(String InputString)
        {
            return InputString.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'");
        }

        public static bool IsValidTime(object timeValue)
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

        public static bool IsDayOfWeek(string DayOfWeekString)
        {
            bool ret = false;
            switch(DayOfWeekString.ToLower())
            {
                case "monday":
                case "tuesday":
                case "wednesday":
                case "thursday":
                case "friday":
                case "saturday":
                case "sunday":
                    ret = true;
                    break;
                default:
                    break;
            }
            return ret;
        }

        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int Place = Source.LastIndexOf(Find);
            string result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            return result;
        }
        public static String GetCaseAsText(String InputString, String FormatType)
        {
            String sCase = "";

            switch(FormatType)
            {
                case "Uppercase":
                    sCase = InputString.ToUpper();
                    break;
                case "Lowercase":
                    sCase = InputString.ToLower();
                    break;

                default:
                    sCase = InputString;
                    break;
            }


            return sCase;
        }

        public static int LevenshteinDistance(string templateString, string inputString)
        {
            int n = templateString.Length;
            int m = inputString.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }
            for (int i = 0; i <= n; d[i, 0] = i++)
                ;
            for (int j = 0; j <= m; d[0, j] = j++)
                ;
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (inputString[j - 1] == templateString[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        public static String ReplaceElipsesWithThreeDots(String InputString)
        {
            return InputString.Replace("\u2026", "...");
        }

        /// <summary>
        /// Returns the object as DateTime in 12 hour format.
        /// </summary>
        /// <param name="timeValue">Value to be converted</param>
        /// <returns></returns>
        public static DateTime GetTimeIn12Hour(object timeValue)
        {
            String ts = timeValue.ToString();
            DateTime dt = System.Convert.ToDateTime(ts);
            return dt;
        }

        /// <summary>
        /// Check if input string is valid absolute HTTP/S Uri string
        /// </summary>
        /// <param name="Input">Input string to check for validity</param>
        /// <returns>TRUE if valid absolute URI, FALSE otherwise</returns>
        public static bool IsValidUriString(string Input)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(Input, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        /// <summary>
        /// Checks if input string is valid email address
        /// </summary>
        /// <param name="inputEmail">Input email string</param>
        /// <returns>True if valid, otherwise False</returns>
        public static bool IsValidEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        /// <summary>
        /// Checks if the search value does not contain invalid characters
        /// </summary>
        /// <param name="searchValue">Value to be checked</param>
        /// <returns>True if valid, otherwise False</returns>
        public static bool IsSearchValid(string searchValue)
        {
            string invalidChars = "\\/:*?\"<>|";
            return searchValue.IndexOfAny(invalidChars.ToCharArray()) == -1;
        }

        /// <summary>
        /// Checks if the agent name does not contain invalid characters
        /// </summary>
        /// <param name=agentNameValue">Value to be checked</param>
        /// <returns>True if valid, otherwise False</returns>
        public static bool IsAgentNameValid(string agentNameValue)
        {
            string regexPattern = @"^[a-zA-Z0-9.-]*$";
            Regex regex = new Regex(regexPattern);
            return regex.IsMatch(agentNameValue) && HasAlphanumericChar(agentNameValue);
        }

        public static bool IsValidEmailListFormat(string emailList)
        {
            if (emailList.StartsWith(";")) return false;

            var reg = new Regex(@";+");
            var matches = reg.Matches(emailList);

            foreach (var match in matches)
                if (match.ToString().Count() > 1) return false;

            return true;
        }

        /// <summary>
        /// Replace unicode characters in string with the specified value
        /// </summary>
        /// <param name="textInput">text input</param>
        /// <param name="replacement">replacement value</param>
        /// <param name="regexOptions">SingleLine = single use; Compiled = iteration</param>
        /// <returns>new value for textInput</returns>
        public static string ReplaceUnicode(string textInput, string replacement, RegexOptions regexOptions = RegexOptions.Singleline)
        {
            //Regex pattern definition: negate space to char code 32 to 126, ',', \r, \n, and \t
            Regex unicodeRegex = new Regex("[^\u0020-\u007E,\t\r\n]", regexOptions);

            return unicodeRegex.IsMatch(textInput) ? unicodeRegex.Replace(textInput, replacement) : textInput;
        }

        /// <summary>
        /// Check is input value is valid for configuration files
        /// </summary>
        /// <param name="Value">text input</param>
        /// <returns></returns>
        public static bool IsValidConfigValue(string Value)
        {
            Regex rg = new Regex(@"^[A-Z0-9,!\[\];:-]+$", RegexOptions.IgnoreCase);
            return rg.IsMatch(Value);
        }

        /// <summary>
        /// Checks whether a string value is a file or a directory
        /// </summary>
        /// <param name="FilePath">The string to be checked if it is a file or directory</param>
        /// <returns>True if a file is valid, false if otherwise</returns>
        public static bool IsFile(string FilePath)
        {
            try
            {
                FileAttributes attr = File.GetAttributes(FilePath);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                //Not a file. Return false
                return false;
            }
        }
    }
}
