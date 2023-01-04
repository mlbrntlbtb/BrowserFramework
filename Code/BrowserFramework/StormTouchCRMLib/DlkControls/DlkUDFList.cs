using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;

namespace StormTouchCRMLib.DlkControls
{
    [ControlType("UDFList")]
    public class DlkUDFList : DlkBaseControl
    {
        #region DECLARATIONS
        //constants
        private const String TEXTFLD = "x-field-text";
        private const String NUMBERFLD = "x-field-number";
        private const String TOGGLEFLD = "x-toggle-field";
        private const String TEXTEDITORFLD = "x-field-textarea";
        private const String SELECTFLD = "chevron_fields";
        private const String FIELDITEM_XPATH = ".//div[contains(@class,'x-container')][contains(@class,'x-field')]";
        private const String FIELDLABEL_XPATH = ".//div[contains(@class,'x-form-label')]";
        private const String FIELDINPUT_XPATH = ".//input";
        private const String FIELDTOGGLE_XPATH = ".//div[contains(@class,'x-toggle')]";
        private const String FIELDTEXTEDITOR_XPATH = ".//textarea";

        private IWebElement mField = null;
        #endregion

        public DlkUDFList(String ControlName, String SearchType, String SearchValue)
            : base(ControlName, SearchType, SearchValue) { }
        public DlkUDFList(String ControlName, String SearchType, String[] SearchValues)
            : base(ControlName, SearchType, SearchValues) { }
        public DlkUDFList(String ControlName, IWebElement ExistingWebElement)
            : base(ControlName, ExistingWebElement) { }

        public void Initialize()
        {
            DlkEnvironment.SetContext("WEBVIEW");
            FindElement();
        }

        #region KEYWORDS

        [Keyword("Click", new String[] { "1|text|Expected Value|TRUE" })]
        public void Click(String FieldLabel)
        {
            try
            {
                Initialize();
                GetFieldByFieldLabel(FieldLabel);

                var control = new DlkBaseControl("Control", mField.FindElement(By.XPath("./div[@class='x-component-outer']")));
                control.ScrollIntoViewUsingJavaScript();
                control.Click();

                DlkLogger.LogInfo("Click() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        /// <summary>
        /// This keyword look at the UDF List control, then looks if the supplied field label exists. If yes then try to set the text using Value.
        /// </summary>
        /// <param name="FieldLabel"></param>
        /// <param name="Value"></param>
        [Keyword("Set", new String[] { "1|text|Expected Value|TRUE" })]
        public void Set(String FieldLabel, String Value)
        {
            try
            {
                Initialize();
                GetFieldByFieldLabel(FieldLabel);

                string mClass = mField.GetAttribute("class");
                if (mClass.Contains(SELECTFLD) || mClass.Contains(TEXTEDITORFLD)) //list,textarea,pickers and other fields that will set value on different screen go here
                {
                    throw new Exception("Set() failed: Set cannot be performed in this type of field.");
                }
                else if (mClass.Contains(TEXTFLD) || mClass.Contains(NUMBERFLD)) // number field and text field are both textboxes
                {
                    TextFieldSet(Value);
                }
                else if (mClass.Contains(TOGGLEFLD))
                {
                    ToggleFieldSet(Value);
                }
                else
                {
                    throw new Exception("Initialize() error. Unknown field type '" + mClass + "'");
                }
                DlkLogger.LogInfo("Set() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Set() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyExists", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyExists(String UDFLabel, String TrueOrFalse)
        {
            try
            {
                Initialize();
                VerifyUDFComponentExists(UDFLabel, TrueOrFalse);
                DlkLogger.LogInfo("VerifyExists() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyExists() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyValue", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyValue(String UDFLabel, String ExpectedValue)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(UDFLabel)) throw new ArgumentException("UDFLabel must not be empty.");
                Initialize();
                // finds the label,
                // gets the udf control beside it,
                // gets the value of the control using the GetValue() method of the DlkBaseControl class,
                // compare
                DlkLogger.LogInfo("Verifying UDF component value...");
                VerifyUDFComponentValue(UDFLabel, ExpectedValue);
                DlkLogger.LogInfo("VerifyValue() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyValue() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyReadOnly", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyReadOnly(String UDFLabel, String ExpectedValue)
        {
            try
            {
                bool expected = false;
                if (String.IsNullOrWhiteSpace(UDFLabel)) throw new Exception("UDFLabel must not be empty");
                if (String.IsNullOrWhiteSpace(ExpectedValue)) throw new Exception("ExpectedValue must not be empty");
                if (!Boolean.TryParse(ExpectedValue, out expected)) throw new Exception("ExpectedValue must be a Boolean value");
                Initialize();
                VerifyUDFComponentReadOnly(UDFLabel, expected);
                DlkLogger.LogInfo("VerifyReadOnly() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyReadOnly() failed : " + e.Message, e);
            }
        }

        [Keyword("SetAndEnter", new String[] { "1|text|Expected Value|TRUE" })]
        public void SetAndEnter(String UDFLabel, String Value)
        {
            try
            {
                Initialize();
                GetFieldByFieldLabel(UDFLabel);

                string mClass = mField.GetAttribute("class");
                if (mClass.Contains(SELECTFLD) || mClass.Contains(TEXTEDITORFLD) || mClass.Contains(TOGGLEFLD)) //list,textarea,pickers and other fields that will set value on different screen go here
                {
                    throw new Exception("SetAndEnter() failed: SetAndEnter cannot be performed in this type of field.");
                }
                else if (mClass.Contains(TEXTFLD) || mClass.Contains(NUMBERFLD)) // number field and text field are both textboxes
                {
                    TextFieldSetAndEnter(Value);
                }
                else
                {
                    throw new Exception("Initialize() error. Unknown field type '" + mClass + "'");
                }
                DlkLogger.LogInfo("SetAndEnter() passed");
            }
            catch (Exception e)
            {
                throw new Exception("SetAndEnter() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyBlank", new String[] { "1|text|Expected Value|SampleValue" })]
        public void VerifyBlank(String UDFLabel, String TrueOrFalse)
        {
            try
            {
                // perform checks before performing keyword logic
                bool expected = false;
                if (!Boolean.TryParse(TrueOrFalse, out expected))
                {
                    throw new ArgumentException("Parameter must be either true or false.");
                }
                GetFieldByFieldLabel(UDFLabel);

                Initialize();
                bool isBlank = false;

                DlkLogger.LogInfo("Looking at the UDF field text...");

                IWebElement mInput = mField.FindElement(By.XPath(FIELDINPUT_XPATH));
                isBlank = mInput.Text.Equals(String.Empty);

                DlkAssert.AssertEqual("Verify if text is blank/empty for button: " + mControlName, expected, isBlank);
                DlkLogger.LogInfo("VerifyBlank() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyBlank() failed : " + e.Message, e);
            }
        }

        [Keyword("ClickTextboxButton", new String[] { "1|text|Expected Value|TRUE" })]
        public void ClickTextboxButton(String UDFLabel)
        {
            try
            {
                Initialize();
                var labelXpath = string.Format("./descendant::*[text()='{0}']", UDFLabel);
                var udfOfLabelXpath = "./parent::*/following-sibling::*[1]/div/*[1]";
                //elements with matching labels
                var matchingLabels = mElement.FindElements(By.XPath(labelXpath));
                //no labels found with matching text to the parameter.
                if (matchingLabels.Count == 0)
                {
                    throw new Exception("VerifyExists() failed : No label found with supplied text " + UDFLabel);
                }
                foreach (var item in matchingLabels)
                {
                    var udfCtrl = item.FindElement(By.XPath(udfOfLabelXpath));
                    var textBoxButton = udfCtrl.FindElement(By.XPath("./following-sibling::div[contains(@class,'icon')]"));
                    new DlkBaseControl("txtbxBtn", textBoxButton).Tap();
                }

                DlkLogger.LogInfo("ClickTextboxButton() passed");
            }
            catch (Exception e)
            {
                throw new Exception("Click() failed : " + e.Message, e);
            }
        }

        [Keyword("VerifyTextContains", new String[] { "1|text|Expected Value|TRUE" })]
        public void VerifyTextContains(String UDFLabel, String ExpectedValue)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(UDFLabel)) throw new ArgumentException("UDFLabel must not be empty.");
                Initialize();
                // finds the label,
                // gets the udf control beside it,
                // gets the value of the control using the GetValue() method of the DlkBaseControl class,
                // compare
                DlkLogger.LogInfo("Verifying if UDF component value contains the expected value...");
                VerifyUDFComponentValueContains(UDFLabel, ExpectedValue);
                DlkLogger.LogInfo("VerifyTextContains() passed");
            }
            catch (Exception e)
            {
                throw new Exception("VerifyTextContains() failed : " + e.Message, e);
            }
        }

        #endregion

        #region PRIVATE METHODS
        private void GetFieldByFieldLabel(String FieldLabel)
        {
            mField = null;
            //Get all UDF list items
            var labelXpath = string.Format("/descendant::*[1]");
            IList<IWebElement> mFields = mElement.FindElements(By.XPath(FIELDITEM_XPATH));
            foreach (IWebElement itm in mFields)
            {
                //Search for matching field label
                string itmLabel = DlkString.NormalizeNonBreakingSpace(itm.FindElement(By.XPath(FIELDLABEL_XPATH + labelXpath)).GetAttribute("innerText"));
                if (itmLabel.Equals(FieldLabel))
                {
                    mField = itm;
                    break;
                }
            }
            if (mField == null)
            {
                throw new Exception("Field label [" + FieldLabel + "] was not found.");
            }


            DlkBaseControl ctrlField = new DlkBaseControl("Selected", mField);
            ctrlField.ScrollIntoViewUsingJavaScript();

            //check the Y coordinates of the item to be selected. if the scrollintoview does not work use the swipe action
            Point selectedNativeCoord = ctrlField.GetNativeViewCoordinates();
            Point listNativeStartCoord = GetNativeViewCoordinates();
            Point listNativeEndCoord = ConvertToNativeViewCoordinates(mElement.Location.X + mElement.Size.Width, mElement.Location.Y + mElement.Size.Height);
            if (selectedNativeCoord.Y < listNativeStartCoord.Y || selectedNativeCoord.Y > listNativeEndCoord.Y || selectedNativeCoord.Y > DlkEnvironment.mDeviceHeight)
            {
                Point listNativeCenterCoord = GetNativeViewCenterCoordinates();
                double dYTranslation = Convert.ToDouble(selectedNativeCoord.Y) - Convert.ToDouble(listNativeCenterCoord.Y);
                if (dYTranslation < 0)
                {
                    Swipe(SwipeDirection.Down, Convert.ToInt32(Math.Abs(dYTranslation)));
                }
                else
                {
                    Swipe(SwipeDirection.Up, Convert.ToInt32(dYTranslation));
                }

            }
        }

        private void TextFieldSet(String Value)
        {
            IWebElement mInput = mField.FindElement(By.XPath(FIELDINPUT_XPATH));
            new DlkTextBox("TextField", mInput).Set(Value);
        }

        private void TextFieldSetAndEnter(String Value)
        {
            IWebElement mInput = mField.FindElement(By.XPath(FIELDINPUT_XPATH));
            new DlkTextBox("TextField", mInput).SetAndEnter(Value);
        }

        private void ToggleFieldSet(String Value)
        {
            IWebElement mToggle = mField.FindElement(By.XPath(FIELDTOGGLE_XPATH));
            new DlkToggle("ToggleField", mToggle).Set(Value);
        }

        /// <summary>
        /// Checks if there is a UDF element beside the supplied label
        /// </summary>
        /// <param name="UDFLabel"></param>
        /// <param name="TrueOrFalse"></param>
        private void VerifyUDFComponentExists(String UDFLabel, String TrueOrFalse)
        {
            var labelXpath = string.Format("./descendant::*[text()='{0}']", UDFLabel);
            var udfXpath = "./parent::*/following-sibling::*[1]/div/*[1]";
            //elements with matching labels
            var label = mElement.FindElements(By.XPath(labelXpath));
            //no labels found with matching text to the parameter.
            if (label.Count == 0)
            {
                throw new Exception("VerifyExists() failed : No label found with supplied text " + UDFLabel);
            }
            foreach (var item in label)
            {
                //gets the corresponding control of a label
                var udfCtrl = item.FindElement(By.XPath(udfXpath)); // get the udf control beside the label
                bool bExistingControl = new DlkBaseControl("label", udfCtrl).Exists();
                if (bExistingControl)
                {
                    DlkLogger.LogInfo("UDF control exists");
                    var strExists = bExistingControl.ToString().ToLower();
                    TrueOrFalse = TrueOrFalse.ToLower();
                    DlkAssert.AssertEqual("VerifyExists:", TrueOrFalse, strExists);
                }
            }

        }


        /// <summary>
        /// Gets the value of the UDF element beside the supplied label and compares the value if it is equal to the supplied expected value
        /// </summary>
        /// <param name="UDFLabel"></param>
        /// <param name="ExpectedValue"></param>
        private void VerifyUDFComponentValue(String UDFLabel, String ExpectedValue)
        {
            var labelXpath = string.Format("./descendant::*[text()='{0}']", UDFLabel);
            var udfOfLabelXpath = "./parent::*/following-sibling::*[1]/div/*[1]";
            //elements with matching labels
            var matchingLabels = mElement.FindElements(By.XPath(labelXpath));
            //no labels found with matching text to the parameter.
            if (matchingLabels.Count == 0)
            {
                throw new Exception("VerifyExists() failed : No label found with supplied text " + UDFLabel);
            }
            foreach (var item in matchingLabels)
            {
                DlkBaseControl udf = new DlkBaseControl("UDF", item);
                udf.ScrollIntoViewUsingJavaScript();
                var udfCtrl = item.FindElements(By.XPath(udfOfLabelXpath)).OrderByDescending(x => x.Displayed).FirstOrDefault();//first one should be the displayed = true 
                var container = udfCtrl.FindElement(By.XPath("./parent::*"));
                String ActText = String.Empty;
                // check if the control beside the label is a TOGGLE, there might be scenarios where there is a new control type.
                if (container.GetAttribute("class").ToLower().Contains("deltektoggle"))
                {
                    if (container.GetAttribute("class").Contains("x-deltektoggle-off"))
                    {
                        ActText = "off";
                    }
                    else if (container.GetAttribute("class").Contains("x-deltektoggle-on"))
                    {
                        ActText = "on";
                    }
                    DlkAssert.AssertEqual("VerifyUDFComponentValue()", ExpectedValue.ToLower(), ActText);
                }
                // if not a TOGGLE, this else block handles other controls, TEXTBOX, BUTTONS. there might be scenarios where there is a new control type.
                else
                {
                    ActText = new DlkBaseControl("udfValue", udfCtrl).GetValue();
                    if (!string.IsNullOrWhiteSpace(ActText))
                    {
                        DlkAssert.AssertEqual("VerifyUDFComponentValue()", ExpectedValue.ToLower(), ActText.ToLower());
                    }
                }

            }
        }

        /// <summary>
        /// Gets the value of the UDF element beside the supplied label and compares the value if it contains the supplied expected value
        /// </summary>
        /// <param name="UDFLabel"></param>
        /// <param name="ExpectedValue"></param>
        private void VerifyUDFComponentValueContains(String UDFLabel, String ExpectedValue)
        {
            var bPass = false;
            var labelXpath = string.Format("./descendant::*[text()='{0}']", UDFLabel);
            var udfOfLabelXpath = "./parent::*/following-sibling::*[1]/div/*[1]";
            //elements with matching labels
            var matchingLabels = mElement.FindElements(By.XPath(labelXpath));
            String ActText = String.Empty;

            //no labels found with matching text to the parameter.
            if (matchingLabels.Count == 0)
            {
                throw new Exception("VerifyTextContains() failed : No label found with supplied text " + UDFLabel);
            }
            foreach (var item in matchingLabels)
            {
                DlkBaseControl udf = new DlkBaseControl("UDF", item);
                udf.ScrollIntoViewUsingJavaScript();
                var udfCtrl = item.FindElements(By.XPath(udfOfLabelXpath)).OrderByDescending(x => x.Displayed).FirstOrDefault();//first one should be the displayed = true 
                var container = udfCtrl.FindElement(By.XPath("./parent::*"));
                // set ActText value
                ActText = new DlkBaseControl("udfValue", udfCtrl).GetValue().Trim();
                if (!ActText.ToLower().Contains(ExpectedValue.ToLower()))
                {
                    continue;
                }
                else
                {
                    bPass = true;
                }
            }
            if (bPass)
            {
                DlkLogger.LogInfo(String.Format("{0} contains {1}", ActText, ExpectedValue));
            }
            else
            {
                throw new Exception(String.Format("Actual Value:[{0}] does not contain expected value: [{1}].", ActText, ExpectedValue));
            }
        }

        /// <summary>
        /// Looks at the UDF control beside the supplied label and checks for the 'readonly' and 'disabled' attribute.
        /// </summary>
        /// <param name="UDFLabel"></param>
        /// <param name="TrueOrFalse"></param>
        private void VerifyUDFComponentReadOnly(String UDFLabel, Boolean TrueOrFalse)
        {
            var labelXpath = string.Format("./descendant::*[text()='{0}']", UDFLabel);
            var udfOfLabelXpath = "./parent::*/following-sibling::*[1]/div/*[1]";
            //elements with matching labels
            var label = mElement.FindElements(By.XPath(labelXpath));
            //no labels found with matching text to the parameter.
            if (label.Count == 0)
            {
                throw new Exception("VerifyReadOnly() failed : No label found with supplied text " + UDFLabel);
            }
            foreach (var item in label)
            {
                var controlType = item.GetAttribute("class").Contains("delteknumberfld") ? "textbox" : (item.GetAttribute("class").Contains("deltektextfld") ? "button" : (item.GetAttribute("class").Contains("toggle") ? "toggle" : "unsupported"));
                //gets the corresponding control of a label beside the Udf list
                var udfCtrl = item.FindElement(By.XPath(udfOfLabelXpath));
                var bReadonly = false;
                // adding handler depending on control type - DlkBaseControl.IsReadOnly fails on buttons with readonly tags that are not disabled.
                switch (controlType)
                {
                    case "textbox":
                        Boolean.TryParse(new DlkBaseControl("UDF Control", udfCtrl).IsReadOnly(), out bReadonly);
                        DlkLogger.LogInfo(String.Format("{0} is a textbox, checking if readonly: {1}", UDFLabel, bReadonly));
                        break;
                    case "button":
                    case "toggle":
                        bReadonly = udfCtrl.Enabled;
                        DlkLogger.LogInfo(String.Format("{0} is a button/toggle, checking if readonly: {1}", UDFLabel, bReadonly));
                        break;
                    default:
                        break;
                }
                DlkAssert.AssertEqual("Check to see if UDF Control is readonly or disabled", TrueOrFalse, bReadonly);
            }
        }

        #endregion
    }
}
