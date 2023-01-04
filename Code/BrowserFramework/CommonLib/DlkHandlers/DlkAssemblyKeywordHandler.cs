using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    public static class DlkAssemblyKeywordHandler
    {
        private static Dictionary<string, Dictionary<string, List<string>>> mKeywordManifest 
            = new Dictionary<string, Dictionary<string, List<string>>>();
        private static Assembly mAssembly;
        private static Assembly mCommonAsembly;

        private static Dictionary<string, Dictionary<string, List<DlkKeywordParameterRecord>>> mKeywordDictionary
            = new Dictionary<string, Dictionary<string, List<DlkKeywordParameterRecord>>>();
        
        public static void Initialize(string LibraryPath)
        {
            if (mAssembly?.Location == LibraryPath)
            {
                return;
            }

            mAssembly = Assembly.LoadFrom(LibraryPath);
            mCommonAsembly = Assembly.GetExecutingAssembly();
            mKeywordManifest = new Dictionary<string, Dictionary<string, List<string>>>();
            mKeywordDictionary = new Dictionary<string, Dictionary<string, List<DlkKeywordParameterRecord>>>();

            // load common first
            foreach (Type aType in mCommonAsembly.GetTypes())
            {
                foreach (Attribute attribControlType in aType.GetCustomAttributes(typeof(ControlType), true))
                {
                    string controlType = ((ControlType)attribControlType).controltype;
                    Dictionary<string, List<string>> keywords = GetControlKeywords(aType);
                    mKeywordManifest.Add(controlType, keywords);

                    Dictionary<string, List<DlkKeywordParameterRecord>> keywordEntries = GetControlKeywordInformation(aType);
                    mKeywordDictionary.Add(controlType, keywordEntries);
                }
            }

            foreach (Type aType in mAssembly.GetTypes())
            {
                //foreach (Attribute attribComponent in aType.GetCustomAttribute(typeof(Component), true))
                //{
                //    string componentName = ((Component)attribComponent).name;
                //    Dictionary<string, List<string>> keywords = GetComponentKeywords(aType);
                //    mKeywordManifest.Add(componentName, keywords);
                //}
                foreach (Attribute attribComponent in aType.GetCustomAttributes(typeof(Component), true))
                {
                    string componentName = ((Component)attribComponent).name;
                    Dictionary<string, List<string>> keywords = GetComponentKeywords(aType);
                    mKeywordManifest.Add(componentName, keywords);

                    Dictionary<string, List<DlkKeywordParameterRecord>> keywordEntries = GetComponentKeywordInformation(aType);
                    mKeywordDictionary.Add(componentName, keywordEntries);
                }
                foreach (Attribute attribControlType in aType.GetCustomAttributes(typeof(ControlType), true))
                {
                    string controlType = ((ControlType)attribControlType).controltype;
                    Dictionary<string, List<string>> keywords = GetControlKeywords(aType);
                    // keyword control type exists, just add the additional keywords
                    if (mKeywordManifest.Keys.Contains(controlType))
                    {
                        foreach (KeyValuePair<string, List<string>> kvp in keywords)
                        {
                            if (!mKeywordManifest[controlType].ContainsKey(kvp.Key))
                            {
                                mKeywordManifest[controlType].Add(kvp.Key, kvp.Value);
                            }
                        }
                    }
                    else
                    {
                        mKeywordManifest.Add(controlType, keywords);
                    }
                    Dictionary<string, List<DlkKeywordParameterRecord>> keywordEntries = GetControlKeywordInformation(aType);
                    // keyword control type exists, just add the additional keywords
                    if (mKeywordDictionary.Keys.Contains(controlType))
                    {
                        foreach (KeyValuePair<string, List<DlkKeywordParameterRecord>> kvp in keywordEntries)
                        {
                            if (!mKeywordManifest[controlType].ContainsKey(kvp.Key))
                            {
                                mKeywordDictionary[controlType].Add(kvp.Key, kvp.Value);
                            }
                        }
                    }
                    else
                    {
                        mKeywordDictionary.Add(controlType, keywordEntries);
                    }
                }
            }
        }

        private static Dictionary<string, List<string>> GetControlKeywords(Type controlType)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            foreach(MethodInfo mi in controlType.GetMethods())
            {
                foreach(Attribute attribute in mi.GetCustomAttributes(typeof(Keyword), true))
                {
                    string kw = ((Keyword)attribute).keyword;
                    if (ret.Keys.Contains(kw))
                    {
                        continue;
                    }
                    ret.Add(kw, GetParameters(mi.GetParameters()));
                }
            }
            return ret;
        }

        private static Dictionary<string, List<string>> GetComponentKeywords(Type componentType)
        {
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();
            foreach (MethodInfo mi in componentType.GetMethods())
            {
                foreach (Attribute attribute in mi.GetCustomAttributes(typeof(Keyword), true))
                {
                    string kw = ((Keyword)attribute).keyword;
                    if (ret.Keys.Contains(kw))
                    {
                        continue;
                    }
                    ret.Add(kw, GetParameters(mi.GetParameters()));
                }
            }
            return ret;
        }


        private static Dictionary<string, List<DlkKeywordParameterRecord>> GetControlKeywordInformation(Type controlType)
        {
            Dictionary<string, List<DlkKeywordParameterRecord>> ret = new Dictionary<string, List<DlkKeywordParameterRecord>>();
            foreach (MethodInfo mi in controlType.GetMethods())
            {
                foreach (Attribute attribute in mi.GetCustomAttributes(typeof(Keyword), true))
                {
                    string kw = ((Keyword)attribute).keyword;
                    if (ret.Keys.Contains(kw))
                    {
                        continue;
                    }
                    List<DlkKeywordParameterRecord> parms = new List<DlkKeywordParameterRecord>();
                    for(int i=0; i < ((Keyword)attribute).Parameters.Count; i++)
                    {
                        Keyword.Parameter aParm = ((Keyword)attribute).Parameters[i];
                        parms.Add(new DlkKeywordParameterRecord(aParm.sName, aParm.sDefaultValue, i));
                    }
                    ret.Add(kw, parms);
                }
            }
            return ret;
        }

        private static Dictionary<string, List<DlkKeywordParameterRecord>> GetComponentKeywordInformation(Type componentType)
        {
            Dictionary<string, List<DlkKeywordParameterRecord>> ret = new Dictionary<string, List<DlkKeywordParameterRecord>>();
            foreach (MethodInfo mi in componentType.GetMethods())
            {
                foreach (Attribute attribute in mi.GetCustomAttributes(typeof(Keyword), true))
                {
                    string kw = ((Keyword)attribute).keyword;
                    if (ret.Keys.Contains(kw))
                    {
                        continue;
                    }
                    List<DlkKeywordParameterRecord> parms = new List<DlkKeywordParameterRecord>();
                    for (int i = 0; i < ((Keyword)attribute).Parameters.Count; i++)
                    {
                        Keyword.Parameter aParm = ((Keyword)attribute).Parameters[i];
                        parms.Add(new DlkKeywordParameterRecord(aParm.sName, aParm.sDefaultValue, i));
                    }
                    ret.Add(kw, parms);
                }
            }
            return ret;
        }

        private static List<string> GetParameters(ParameterInfo[] parameterInfoArr)
        {
            List<string> ret = new List<string>();
            foreach (ParameterInfo pi in parameterInfoArr)
            {
                ret.Add(pi.Name);
            }
            return ret;
        }

        public static List<string> GetControlKeywords(string ControlName)
        {
            List<string> ret = new List<string>();

            if (mKeywordManifest.ContainsKey(ControlName))
            { 
                Dictionary<string, List<string>> keywords = mKeywordManifest[ControlName];

                foreach (string kwdName in keywords.Keys)
                {
                    ret.Add(kwdName);
                }
                ret.Sort();
            }
            return ret;
        }

        //public static List<string> GetComponentKeywords(string ComponentName)
        //{
        //    List<string> ret = new List<string>();
        //    if (mKeywordManifest.ContainsKey(ComponentName))
        //    {
        //        Dictionary<string, List<string>> keywords = mKeywordManifest[ComponentName];

        //        foreach (string kwdName in keywords.Keys)
        //        {
        //            ret.Add(kwdName);
        //        }
        //        ret.Sort();
        //    }
        //    return ret;
        //}

        public static List<string> GetControlKeywordParameters(string ControlName, string KeywordName)
        {
            if (mKeywordManifest.Count == 0)
                Initialize(DlkEnvironment.mLibrary);

            List<string> ret = new List<string>();

            if (mKeywordManifest.ContainsKey(ControlName))
            {
                Dictionary<string, List<string>> keywords = mKeywordManifest[ControlName];

                if (keywords.ContainsKey(KeywordName))
                {
                    ret = keywords[KeywordName];
                }
            }
            return ret;
        }

        public static List<DlkKeywordParameterRecord> GetControlKeywordParameterRecords(string ControlName, string KeywordName)
        {
            List<DlkKeywordParameterRecord> lstParameters = new List<DlkKeywordParameterRecord>();
            if(mKeywordDictionary.ContainsKey(ControlName))
            {
                Dictionary<string, List<DlkKeywordParameterRecord>> keywords = mKeywordDictionary[ControlName];
                if(keywords.ContainsKey(KeywordName))
                {
                    lstParameters = keywords[KeywordName];
                }
            }
            return lstParameters;
        }
    }
}
