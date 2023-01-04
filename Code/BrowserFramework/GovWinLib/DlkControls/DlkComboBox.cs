using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;

namespace GovWinLib.DlkControls
{
    [ControlType("ComboBox")]
    public class DlkComboBox : DlkBaseControl
    {
        public DlkComboBox(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkComboBox(String ControlName, DlkBaseControl ParentControl, String SearchType, String SearchValue)
            : base(ControlName, ParentControl, SearchType, SearchValue) { }
        public DlkComboBox(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            FindElement();
        }

        [Keyword("Select", new String[] { "1|text|Value|TRUE" })]
        public void Select(String Value)
        {
            Thread.Sleep(1000);
            //Click first on the combobox to bring up the combobox list
            Initialize();

            if (Value.Contains("\n") || Value.Contains("\r"))
            {
                Value = Value.Replace("\n", "");
                Value = Value.Replace("\r", "");
            }

            SelectElement comboBoxElement = new SelectElement(mElement);
            comboBoxElement.SelectByText(Value);

            //VerifyValue(Value);
            DlkLogger.LogInfo("Successfully executed Select(). Control : " + mControlName + " : " + Value);
        }

        [RetryKeyword("GetValue", new string[] {"1|text|VariableName|MyValue"})]
        public void GetValue(String VariableName)
        {
            this.PerformAction(() =>
            {
                Initialize();
                
                SelectElement comboBoxElement = new SelectElement(mElement);
                DlkBaseControl selectedOption = new DlkBaseControl("Selected Option", comboBoxElement.SelectedOption);
                String sActualValue = selectedOption.GetValue();

                DlkVariable.SetVariable(VariableName, sActualValue);
                
            });
        }

        [RetryKeyword("GetCount", new string[] { "1|text|VariableName|MyCount" })]
        public void GetCount(String VariableName)
        {
            this.PerformAction(() =>
            {
                Initialize();                

                SelectElement comboBoxElement = new SelectElement(mElement);
                String sActualValue = comboBoxElement.Options.Count.ToString();

                DlkVariable.SetVariable(VariableName, sActualValue);

            });
        }

        #region Verify methods
        [RetryKeyword("VerifyValue", new String[] { "1|text|Expected Value|ExampleValue" })]
        public void VerifyValue(String ExpectedValue)
        {
            String expectedValue = ExpectedValue;

            this.PerformAction(() =>
                {
                    Initialize();

                    SelectElement comboBoxElement = new SelectElement(mElement);
                    DlkBaseControl selectedOption = new DlkBaseControl("Selected Option", comboBoxElement.SelectedOption);
                    String sActualValue = selectedOption.GetValue();

                    DlkAssert.AssertEqual("VerifyValue()", expectedValue, sActualValue);
                }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyList", new String[] { "1|text|Expected Values|Value1~Value2~Value3" })]
        public void VerifyList(String[] ExpectedValues)
        {
            String expectedValues = ExpectedValues[0];
            this.PerformAction(() =>
                {
                    String sActualValues = "";
                    Boolean bFirst = true;
                    Initialize();

                    SelectElement comboBoxElement = new SelectElement(mElement);
                    foreach (IWebElement option in comboBoxElement.Options)
                    {
                        if (!bFirst)
                        {
                            sActualValues = sActualValues + "~";
                        }
                        sActualValues = sActualValues + option.Text;
                        bFirst = false;
                    }

                    DlkAssert.AssertEqual("VerifyList()", expectedValues, sActualValues);
                }, new String[] { "retry" });
        }

        [RetryKeyword("VerifyListContains", new String[] { "1|text|Expected Text Value|ABC", "2|text|Expected Value|TRUE" })]
        public void VerifyListContains(String ExpectedValue, String TrueOrFalse)
        {
            String expectedTextValue = ExpectedValue;
            Boolean expectedValue = Convert.ToBoolean(TrueOrFalse);

            Boolean actualValue = false;

            this.PerformAction(() =>
            {  
                Initialize();

                SelectElement comboBoxElement = new SelectElement(mElement);
                foreach (IWebElement option in comboBoxElement.Options)
                {
                    if (option.Text == expectedTextValue)
                    {
                        actualValue = true;
                    }
                }            

                DlkAssert.AssertEqual("VerifyListContains()", expectedValue, actualValue);

            }, new String[] { "retry" });
        }

        public bool ifListContains(String ExpectedTextValue, String TrueOrFalse)
        {
            String expectedTextValue = ExpectedTextValue;
            Boolean expectedValue = Convert.ToBoolean(TrueOrFalse);
            Boolean actualValue = false;
                
                Initialize();

                SelectElement comboBoxElement = new SelectElement(mElement);
                foreach (IWebElement option in comboBoxElement.Options)
                {
                    if (option.Text.ToLower().Trim() == expectedTextValue.ToLower().Trim())
                    {
                        actualValue = true;
                    }
                }                

            return actualValue;
        }

        [RetryKeyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String TrueOrFalse)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {
                    //Boolean bExists = base.Exists();
                    //DlkLogger.LogAssertion("VerifyExists()", expectedValue, Convert.ToString(bExists));
                    base.VerifyExists(Convert.ToBoolean(TrueOrFalse));
                    
                }, new String[] {"retry"});
        }

        [RetryKeyword("GetIfExists", new String[] { "1|text|Expected Value|TRUE",
                                                            "2|text|VariableName|ifExist"})]
        public new void GetIfExists(String VariableName)
        {
            this.PerformAction(() =>
            {

                Boolean bExists = base.Exists();
                DlkVariable.SetVariable(VariableName, Convert.ToString(bExists));

            }, new String[] { "retry" });
        }

        [RetryKeyword("GetVerifyExists", new String[] { "1|text|Expected Value|TRUE",
                                                        "2|text|VariableName|IfExists"})]
        public void GetVerifyExists(String TrueOrFalse, String VariableName)
        {
            String expectedValue = TrueOrFalse;

            this.PerformAction(() =>
                {                    
                    Boolean bExists = base.Exists();
                    DlkVariable.SetVariable(VariableName, Convert.ToString(bExists == Convert.ToBoolean(expectedValue)));

                }, new String[] { "retry" });
        }
        #endregion
    }
}

