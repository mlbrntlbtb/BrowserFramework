using System;
using System.Linq;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using System.Reflection;

namespace GovWinLib.DlkFunctions
{
    [Component("WebPageStore")]
    public class DlkWebPageStore
    {

        public DlkWebPageStore(String control, String keyword, String[] parameters)
        {
            switch (keyword.ToLower())
            {
                case "verifycontrolcontainstext":
                    this.VerifyControlContainsText(parameters[0], parameters[1]);
                    break;
                case "verifycontrolcontainspartialtext":
                    this.VerifyControlContainsPartialText(parameters[0], parameters[1], parameters[2]);
                    break;
                default:
                    {
                        throw new Exception("Unknown function. Screen: DlkWebPageStore, Function:" + keyword);
                    }

            }
        }

        [Keyword("VerifyControlContainsText", new String[]{ "1|text|Control Name|Name",
                                                            "2|text|Expected (TRUE or FALSE)|TRUE" })]
        public void VerifyControlContainsText(String controlName, String expectedResult)
        {
            
            bool actualResult = false;
            string fieldVal = "";
            var fieldInfos = this.GetType().GetFields();

            foreach (FieldInfo field in fieldInfos)
            {
                var controlattr = field.GetCustomAttributes(typeof(Control), true).First() as Control;

                if (controlattr != null)
                {
                    if (controlattr.controlname == controlName)
                    {
                        if (field.FieldType.BaseType.Name == typeof(DlkBaseButton).ToString().Split('.').Last())
                        {
                            DlkBaseButton dlkCtrl = field.GetValue(this) as DlkBaseButton;

                            if (dlkCtrl != null)
                            {
                                DlkLogger.LogInfo(string.Format("VerifyControlContainsText: {0} control found.", controlName));
                                fieldVal = dlkCtrl.GetValue();
                                if (fieldVal != "")
                                {
                                    DlkLogger.LogInfo(string.Format("VerifyControlContainsText: {0} contains \"{1}\".", controlName, fieldVal));
                                    actualResult = true;
                                }
                                else
                                {
                                    DlkLogger.LogInfo(string.Format("VerifyControlContainsText: {0} is empty.", controlName));
                                }
                            }
                        }
                    }
                }
            }

            DlkAssert.AssertEqual("VerifyControlContainsText ", Convert.ToBoolean(expectedResult), actualResult);
        }


        [Keyword("VerifyControlContainsPartialText", new String[]{ "1|text|Control Name|Name",
                                                            "2|text|Search Value|Some Text",
                                                            "3|text|Expected (TRUE or FALSE)|TRUE" })]
        public void VerifyControlContainsPartialText(String controlName, String partialText, String expectedResult)
        {
            bool actualResult = false;
            string fieldVal = "";
            var fieldInfos = this.GetType().GetFields();

            foreach (FieldInfo field in fieldInfos)
            {
                var controlattr = field.GetCustomAttributes(typeof(Control), true).First() as Control;

                if (controlattr != null)
                {
                    if (controlattr.controlname == controlName)
                    {
                        if (field.FieldType.BaseType.Name == typeof(DlkBaseButton).ToString().Split('.').Last())
                        {
                            DlkBaseButton dlkCtrl = field.GetValue(this) as DlkBaseButton;

                            if (dlkCtrl != null)
                            {
                                DlkLogger.LogInfo(string.Format("VerifyControlContainsPartialText: {0} control found.", controlName));
                                fieldVal = dlkCtrl.GetValue();
                                if (fieldVal.Contains(partialText))
                                {
                                    DlkLogger.LogInfo(string.Format("VerifyControlContainsPartialText: {0} contains \"{1}\".", fieldVal, partialText));
                                    actualResult = true;
                                }
                                else
                                {
                                    DlkLogger.LogInfo(string.Format("VerifyControlContainsPartialText: {0} is not found in {1}.", partialText, fieldVal));
                                }
                            }
                        }
                    }
                }
            }

            DlkAssert.AssertEqual("VerifyControlContainsText ", Convert.ToBoolean(expectedResult), actualResult);
        }
        
    }
}
