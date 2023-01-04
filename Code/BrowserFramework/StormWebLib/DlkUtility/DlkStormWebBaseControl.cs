using CommonLib.DlkControls;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CommonLib.DlkSystem;
using System.IO;
using System.Text.RegularExpressions;
using CommonLib.DlkUtility;


namespace StormWebLib.DlkControls
{
    // additional layer for localized strings support
    // all StormWeb controls should now derive from this control instead of all of them deriving from DlkBaseControl
    public class DlkStormWebBaseControl : DlkBaseControl, ILanguageParser
    {
        /*
         This class should only implement the extended functionality from interfaces (like language translation). Every other method that derived classes will use should be from DlkBaseControl.
         We will just change specific controls in StormWeb that derive from DlkBaseControl to DlkStormWebBaseControl. 
         Localized string parsing should only work on VerifyText since I think that the strings in localization files should only work for text,not HTML attributes
         */
        #region PRIVATE MEMBERS"
        private Dictionary<string, string> englishDictionary = null;
        private Dictionary<string, string> foreignDictionary = null;
        #endregion

        #region CONSTRUCTORS
        public DlkStormWebBaseControl(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkStormWebBaseControl(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkStormWebBaseControl(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkStormWebBaseControl(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Initialize the Dictonaries with data from the foreign and english files. 
        /// The english json file's path should be fixed but the foreign language's path should change depending on the user's setting
        /// </summary>
        public void InitializeLanguageParser()
        {
            // for testing purposes only. TODO: add switch case to initialize dictionary depending on language.
            englishDictionary = ReadEnglishJsonFileToDictionary(DlkEnvironment.mDirUserData+"/english.json");
            foreignDictionary = ReadForeignFileToKeyValuePairs(DlkEnvironment.mDirUserData+"/spanish.json");
        }

        /// <summary>
        /// Value is whatever text that we are reading from the browser
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public String GetForeignDictionaryKeyByValue(String Value)
        {
            String foreignDictionaryKey = "";
            try
            {
                if (foreignDictionary.Values.Contains(Value))
                {
                    // get the key of the first item that matches the supplied value
                     foreignDictionaryKey = foreignDictionary.FirstOrDefault(foreignDictionaryEntries => foreignDictionaryEntries.Value.Equals(Value)).Key;
                }
               
                return foreignDictionaryKey;   
            }
            catch (Exception ex)
            {
                throw new Exception("GetForeignDictionaryKeyByValue() failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Give me the dictionary key of the foreign string and I'll give you the equivalent english string
        /// </summary>
        /// <param name="foreignDictionaryKey"></param>
        /// <returns></returns>
        public String ParseToEnglishByDictionaryKey(String foreignDictionaryKey)
        {
            String englishEquivalent = "";
            try
            {
                if (foreignDictionary.Keys.Contains(foreignDictionaryKey))
                {
                    // get the value of the first item that matches the key that is return from the foreign dictionary.
                    englishEquivalent = englishDictionary.FirstOrDefault(englishDictionaryEntries => englishDictionaryEntries.Key.Equals(foreignDictionaryKey)).Value ?? "";
                }
                return englishEquivalent;
            }
            catch (Exception ex)
            {
               throw new Exception("ParseToEnglishByDictionaryKey() failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Give me the path of the English JSON file and I'll read it as a Dictionary<string,string> for you
        /// </summary>
        /// <param name="englishJsonFileAbsolutePath"></param>
        /// <returns></returns>
        public Dictionary<string, string> ReadEnglishJsonFileToDictionary(string englishJsonFileAbsolutePath)
        {
            try
            {
                String englishFileContent = "";
                using (StreamReader sr = new StreamReader(englishJsonFileAbsolutePath))
                {
                    // Read the JSON file to a string
                    englishFileContent = sr.ReadToEnd();
                }
                // Deserialize natin to Dictionary since Key - Value pairs
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(englishFileContent);
            }
            catch (Exception ex)
            {
                throw new Exception("ReadEnglishJsonFileToDictionary() failed: " + ex.Message);
            }
        }

        /// <summary>
        /// Give me the path of the Foreign (ex.: spanish, french) JSON file and I'll read it as a Dictionary<string,string> for you
        /// </summary>
        /// <param name="foreignJsonFileAbsolutePath"></param>
        /// <returns></returns>
        public Dictionary<string, string> ReadForeignFileToKeyValuePairs(string foreignJsonFileAbsolutePath)
        {
            try
            {
                String foreignFileContent = "";
                using (StreamReader sr = new StreamReader(foreignJsonFileAbsolutePath))
                {
                    // Read the JSON file to a string
                    foreignFileContent = sr.ReadToEnd();
                }
                // Deserialize natin to Dictionary since Key - Value pairs
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(foreignFileContent);
            }
            catch (Exception ex)
            {
                throw new Exception("ReadForeignFileToKeyValuePairs() failed: " + ex.Message);
            }
        }

        /// <summary>
        /// This should find the value being held in the placeholder {0} of the string.
        /// out parameter is the best match found in the dictionary using Levenshtein Distance
        /// </summary>
        /// <returns></returns>
        public Tuple<object[], object[]> FindPlaceholderValue(String FormattedString, out string outVal)
        {

            Tuple<object[], object[]> plcehldrValue = null;
            var foreignDictionaryKey = GetKeyOfBestMatch(FormattedString);
            var foreignDictionaryWord = foreignDictionary[foreignDictionaryKey];
            // find the placeholder's values by comparing StringWithPlaceholder to foreignDictionaryWord
            // TODO: IMPROVE OVER THE WEEKEND
            plcehldrValue = ReverseStringFormat(foreignDictionaryWord, FormattedString);
            outVal = foreignDictionaryWord;
            return plcehldrValue;
        }

        /// <summary>
        /// This method attempts to find the values of the placeholders in a template given the actual String
        /// </summary>
        /// <param name="template"></param>
        /// <param name="foreign"></param>
        /// <returns></returns>
        public Tuple<object[], object[]> ReverseStringFormat(String template, String foreign)
        {
            // similar logic for code below can be found at:
            // http://stackoverflow.com/questions/5346158/parse-string-using-format-template

            // identify the placeholders
            // compare the difference with the actual string from the web element
            // identify the values of the placeholders and store it in a String array

            try
            {
                return foreign.Unformat(template); //to be used in another method to substitute values for placeholders inside string.Format()
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// This should inject the placeholder values to the english unformatted string
        /// </summary>
        /// <returns></returns>
        public string ReplacePlaceholderValue(String StringWithPlaceholder, object[] PlaceholderValue)
        {
            //this method's return value should be the one to be compared to the parameter in TR
            return String.Format(StringWithPlaceholder, PlaceholderValue);
        }

        /// <summary>
        /// This should inject the placeholder values to an english unformatted string with custom placeholders
        /// </summary>
        /// <param name="CustomString"></param>
        /// <param name="PlaceholderValue"></param>
        /// <returns></returns>
        public string ReplaceCustomPlaceholderValue(string CustomString, object[] PlaceholderValue)
        {
            var replaceRegExPattern = new Regex(@"\[.*?\]");
            int ctr = 0;
            CustomString = replaceRegExPattern.Replace(CustomString, s => { return @"{" + ctr++ + @"}"; }
                );
            return string.Format(CustomString, PlaceholderValue);
        }

        /// <summary>
        /// Additional code in the implementation of GetValue() 
        /// </summary>
        /// <returns></returns>
        new public String GetValue()
        {
            String elementText = base.GetValue();
            String english = "";
            try
            {
                if (true) // replace 'true' with condition that checks if parsing is allowed in settings or in some file
                {
                    // TODO: ADD A SWITCH-CASE for language setting, initialize dictionary according to chosen language
                    InitializeLanguageParser();
                    // try to parse immediately, maybe the localized string has no placeholder. there is no need to perform the operation below if the foreign language string is a straightforward translation.
                    english = ParseToEnglishByDictionaryKey(GetForeignDictionaryKeyByValue(elementText));
                    if (String.IsNullOrWhiteSpace(english))
                    {
                        // variable that will store the unformatted foreign language BEST MATCH string.
                        String foreignStringFromJson = "";

                        // unformats the string being used in string.Format.
                        // FindPlaceholderValue() will return a tuple with two items: the values used in place of standard placeholders {1} and custom placeholders [wbs1].
                        var placeholderValue = FindPlaceholderValue(elementText, out foreignStringFromJson);

                        // this array contains the values for the standard placeholders such as {0}, {1}, {2}
                        var standard = placeholderValue.Item1;

                        // while this array contains the values for the custom placeholders such as [WBS1], [WBS2]
                        var wbs = placeholderValue.Item2;

                        // get the unformatted english string
                        english = ParseToEnglishByDictionaryKey(GetForeignDictionaryKeyByValue(foreignStringFromJson));
                        if (standard.Count() != 0)
                        {
                            // "flatten" the unformatted string using the placeholder values. we just use String.Format inside.
                            var substituted = ReplacePlaceholderValue(english, standard);
                            // return foreign language string if not applicable
                            english = String.IsNullOrWhiteSpace(substituted) ? elementText : substituted;
                        }
                        if (wbs.Count() != 0)
                        {
                            // "flatten" the unformatted string using the placeholder values. we just use String.Format inside.
                            var substituted = ReplaceCustomPlaceholderValue(english, wbs);
                            english = String.IsNullOrWhiteSpace(substituted) ? elementText : substituted;
                        }

                    }
                    return english;
                }
                //else return elementText; - modify when the if condition can now see if language translation has been triggered
            }
            catch (Exception ex)
            {
                throw new Exception("Localization parsing failed.: "+ ex.Message);
            }
        }

        #endregion

        #region PRIVATE METHODS
   

        /// <summary>
        /// Uses the Levenshtein algo in the method above.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string GetKeyOfBestMatch(string inputString)
        {
            string keyToGet = "";
            int minValue = int.MaxValue;
            foreach (var templateString in foreignDictionary)
            {
                int val = DlkString.LevenshteinDistance(templateString.Value, inputString);
                if (val < minValue)
                {
                    minValue = val;
                    keyToGet = templateString.Key;
                }
            }
            return keyToGet;
        }
        #endregion


    }

    /// <summary>
    /// Contains the methods that will be implemented for language parsing attempt
    /// </summary>
    public interface ILanguageParser
    {
        void InitializeLanguageParser();

        String ParseToEnglishByDictionaryKey(String dictionaryKey);

        Dictionary<string, string> ReadEnglishJsonFileToDictionary(string englishJsonFileAbsolutePath);

        Dictionary<string, string> ReadForeignFileToKeyValuePairs(string foreignJsonFileAbsolutePath);

        Tuple<object[], object[]> FindPlaceholderValue(String StringWithPlaceholder, out string outVal);

        String ReplacePlaceholderValue(String StringWithPlaceholder, object[] PlaceholderValue);

        String ReplaceCustomPlaceholderValue(String CustomString, object[] PlaceholderValue);

        Tuple<object[], object[]> ReverseStringFormat(String template, String foreign);
    }

    /// <summary>
    /// Custom extension method for String to find placeholder values
    /// </summary>
    public static class StringExtensions
    {
        #region Regex Members

        private static object _initLock = new object();

        private static Regex _escapeRegEx;
        private static Regex EscapeRegEx
        {
            get
            {
                if (_escapeRegEx == null)
                    lock (_initLock)
                        if (_escapeRegEx == null)
                            InitExpressions();

                return _escapeRegEx;
            }
        }

        private static Regex _standardPlaceholderSelectorRegEx;
        private static Regex StandardPlaceholderSelectorRegEx
        {
            get
            {
                if (_standardPlaceholderSelectorRegEx == null)
                    lock (_initLock)
                        if (_standardPlaceholderSelectorRegEx == null)
                            InitExpressions();

                return _standardPlaceholderSelectorRegEx;
            }
        }

        private static Regex _customPlaceholderSelectorRegEx;
        private static Regex CustomPlaceholderSelectorRegEx
        {
            get
            {
                if (_customPlaceholderSelectorRegEx == null)
                    lock (_initLock)
                        if (_customPlaceholderSelectorRegEx == null)
                            InitExpressions();

                return _customPlaceholderSelectorRegEx;
            }
        }

        /// <summary>
        /// Initializes our Regex Patterns backing fields.
        /// </summary>
        private static void InitExpressions()
        {
            // https://regex101.com/ will explain what this regex does. 
            // These will only be used for replacement purposes, then to loop over the new values once replaced.
            _escapeRegEx = new Regex(@"([\^\$\.\|\?\*\+\(\)])", RegexOptions.Compiled);
            _standardPlaceholderSelectorRegEx = new Regex(@"\{([0-9]+)\}");
            _customPlaceholderSelectorRegEx = new Regex(@"\[.*?\]");
        }

        #endregion

        /// <summary>
        /// I tested this on different scenarios of strings found in the JSON file, it works so far.
        /// Returns object arrays to be used as arguments on string.Format when parsing languages
        /// </summary>
        /// <param name="input"></param>
        /// <param name="formatString"></param>
        /// <returns></returns>
        public static Tuple<object[], object[]> Unformat(this String input, string formatString)
        {
            try
            {
                // Escape special regex characters defined above, this just adds a backslash to it
                string interim = EscapeRegEx.Replace(formatString, @"\$1");

                // Replace format style {0} into regex style (?<C0>.+), (?<C1>.+) and so on
                int ctr = 0;
                interim = StandardPlaceholderSelectorRegEx.Replace(interim, @"(?<C$1>.+)");
                // Replace format style [WBS] or other placeholders inside square brackets into (?<D0>.+)
                interim = CustomPlaceholderSelectorRegEx.Replace(interim, s =>
                {
                    return @"(?<D" + ctr++ + ">.+)";
                });

                // add start and end markers for regex match
                interim = String.Format(@"^{0}$", interim);

                // perform the match, finds all placeholder values and stores them into match.Groups
                Regex regex = new Regex(interim);
                Match match = regex.Match(input);

                List<object> standard = new List<object>();
                List<object> custom = new List<object>();

                // loop to check the standard placeholder values found using the Regex.Match
                int loop = 0;
                while (true)
                {
                    // build a capture group name and check for it
                    string captureName = String.Format("C{0}", loop++);
                    Group capture = match.Groups[captureName];

                    //  see if this capture was found
                    if (capture == null || !capture.Success)
                    {
                        break;
                    }

                    // add it to the output list
                    standard.Add(capture.Value);
                }
                // loop to check the custom placeholder values found using the Regex.Match
                loop = 0;
                while (true)
                {
                    // build a capture group name and check for it
                    string captureName = String.Format("D{0}", loop++);
                    Group capture = match.Groups[captureName];

                    //  see if this capture was found
                    if (capture == null || !capture.Success)
                    {
                        //try again
                        captureName = String.Format("D{0}", loop++);
                        capture = match.Groups[captureName];
                        if (capture == null || !capture.Success)
                        {
                            break;
                        }
                    }

                    // add it to the output list
                    custom.Add(capture.Value);
                }
                return Tuple.Create(standard.ToArray(), custom.ToArray());

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
