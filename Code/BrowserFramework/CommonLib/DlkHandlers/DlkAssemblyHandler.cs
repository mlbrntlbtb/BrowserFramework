using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;

namespace CommonLib.DlkHandlers
{
    /// <summary>
    /// The object store handler reads object store files and stores the control data in memory
    /// It is possible to divide a screen into several object store files
    /// </summary>
    public static class DlkAssemblyHandler
    {
        // Add allowed invokable methods here
        // method invoke is handled in Invoke method, take note of parameter needed for each method
        public enum TestMethods
        {
            ExecuteTest, // This method accepts no parameter, BUT user should pass parameter of DlkTestExecute constructor
            AutoCorrectSearchMethod,
            DetectControlType,
            GetControlTypes
        };

        /// <summary>
        /// Invoke assembly method. Note that we are limiting invokable methods by using TestMethods enum as input param
        /// </summary>
        public static object Invoke(string AssemblyFullPath, TestMethods Method, bool IsAdHocRun = false, params object[] Parameters)
        {
            Assembly assy = Assembly.LoadFrom(AssemblyFullPath);
            Type type = null;
            Object instance = null;
            MethodInfo func = null;
            string method = "";
            object[] funcParams = null;

            object ret = null;

            switch (Method)
            {
                case TestMethods.ExecuteTest: // input params will be used as params for instance contructor
                    type = GetType(AssemblyFullPath, typeof(DlkTestExecute));
                    if (type == null)
                    {
                        throw new Exception("Type is null");
                    }
                    instance = GetInstance(type, Parameters);
                    PropertyInfo propertyInfo = instance.GetType().GetProperty("IsAdHocRun", BindingFlags.Public | BindingFlags.Instance);
                    propertyInfo.SetValue(instance, Convert.ChangeType(IsAdHocRun, propertyInfo.PropertyType), null);
                    method = "ExecuteTest";
                    funcParams = null;
                    break;
                case TestMethods.AutoCorrectSearchMethod:
                    type = GetType(AssemblyFullPath, typeof(DlkControlHelper));
                    if (type == null)
                    {
                        throw new Exception("Type is null");
                    }
                    instance = GetInstance(type, null);
                    method = "AutoCorrectSearchMethod";
                    funcParams = Parameters;
                    break;
                case TestMethods.DetectControlType:
                    type = GetType(AssemblyFullPath, typeof(DlkControlHelper));
                    if (type == null)
                    {
                        throw new Exception("Type is null");
                    }
                    instance = GetInstance(type, null);
                    method = "DetectControlType";
                    funcParams = Parameters;
                    break;
                case TestMethods.GetControlTypes:
                    type = GetType(AssemblyFullPath, typeof(DlkControlHelper));
                    if (type == null)
                    {
                        throw new Exception("Type is null");
                    }
                    instance = GetInstance(type, null);
                    method = "GetControlTypes";
                    funcParams = Parameters;
                    break;
                default:
                    break;
            }
            if (instance == null)
            {
                throw new Exception("Instance is null");
            }
            func = type.GetMethod(method);
            ret = func.Invoke(instance, funcParams); // will change when new methods will be added, some methods might need parameters
            return ret;
        }


        /// <summary>
        /// Gets Type object gven parent Type in an assembly
        /// </summary>
        /// <param name="FullAssemblyPath"></param>
        /// <param name="BaseType"></param>
        /// <returns></returns>
        private static Type GetType(string FullAssemblyPath, Type BaseType)
        {
            Type ret = null;
            Assembly assy = Assembly.LoadFrom(FullAssemblyPath);

            foreach (Type typ in assy.GetTypes())
            {
                if (typ.BaseType == BaseType)
                {
                    ret = typ;
                    break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Gets object instance given type and parameters
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        private static object GetInstance(Type type, params object[] Params)
        {
            object ret = null;

            ret = Activator.CreateInstance(type, Params);
            return ret;
        }

        public static string GetVersion(string AssemblyFullPath)
        {
            Assembly assy = Assembly.LoadFrom(AssemblyFullPath);
            return assy.GetName().Version.ToString();
        }

    }
}