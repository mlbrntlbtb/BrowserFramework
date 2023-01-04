using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLib.DlkSystem
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class Component : System.Attribute
    {
        public String name;

        public Component(String name)
        {
            this.name = name;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class Control : System.Attribute
    {
        public String controlname;

        public Control(String controlname)
        {
            this.controlname = controlname;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ControlType : System.Attribute
    {
        public String controltype;

        public ControlType(String controltype)
        {
            this.controltype = controltype;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class Translate : System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class Keyword : System.Attribute
    {
        public struct Parameter
        {
            public String sID;
            public String sType;
            public String sName;
            public String sDefaultValue;
            public String sValues;

            public Parameter(String[] parameterMembers)
            {
                sID = "";
                sType = "";
                sName = "";
                sDefaultValue = "";
                sValues = "";

                for (int i = 0; i < parameterMembers.Count(); i++)
                {
                    switch (i)
                    {
                        case 0:
                            sID = parameterMembers[i];
                            break;
                        case 1:
                            sType = parameterMembers[i];
                            break;
                        case 2:
                            sName = parameterMembers[i];
                            break;
                        case 3:
                            sDefaultValue = parameterMembers[i];
                            break;
                        case 4:
                            sValues = parameterMembers[i];
                            break;
                    }
                }
            }
        }

        public String keyword;
        public List<Parameter> Parameters;

        public Keyword(String keyword)
        {
            this.keyword = keyword;
            Parameters = new List<Parameter>();
        }

        public Keyword(String keyword, String[] parameters)
        {
            String[] parmMembers;
            Parameter newParm;

            this.keyword = keyword;
            Parameters = new List<Parameter>();
            for (int i = 0; i < parameters.Count(); i++)
            {
                parmMembers = parameters[i].Split('|');
                newParm = new Parameter(parmMembers);
                Parameters.Add(newParm);
                
            }
        }
        
    }

    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class RetryKeyword : Keyword
    {
        public RetryKeyword(String keyword) : base(keyword) { }

        public RetryKeyword(String keyword, String[] parameters)
            : base(keyword, parameters)
        {
            //add default verify delay and retry

            int paramCount = Parameters.Count;
            var additional = new String[] { string.Format("{0}|text|Delay (msecs)|500", paramCount++),
                                            string.Format("{0}|text|Retries|100", paramCount++)};

            String[] addParmMembers;
            Parameter addNewParm;

            for (int i = 0; i < additional.Count(); i++)
            {
                addParmMembers = additional[i].Split('|');
                addNewParm = new Parameter(addParmMembers);
                Parameters.Add(addNewParm);
            }

        }

    }

    [System.AttributeUsage(System.AttributeTargets.Assembly)]
    public class ObjectStoreExtensions : System.Attribute
    {
        public string[] assemblies;
        public ObjectStoreExtensions(string[] AdditionalOSAssemblies)
        {
            assemblies = AdditionalOSAssemblies;
        }
    }
}
