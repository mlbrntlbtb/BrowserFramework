using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CommonLib.DlkControls;
using CommonLib.DlkUtility;
using CommonLib.DlkSystem;
using KonaLib.DlkControls;


namespace KonaLib.DlkUtility
{
    public class DlkKonaControlHelper : DlkControlHelper
    {

        public override string DetectControlType(DlkBaseControl control)
        {
            DlkButton mButton;
            DlkTextBox mTextBox;

            mButton = new DlkButton("Button", control.mElement);
            if (mButton.VerifyControlType())
                return "Button";
            mTextBox = new DlkTextBox("TextBox", control.mElement);
            if (mTextBox.VerifyControlType())
                return "TextBox";            


            return "";

        }

        public override void AutoCorrectSearchMethod(DlkBaseControl control, string controlType, ref string SearchType, ref string SearchValue)
        {
            //DlkButton mButton;
            //DlkCheckBox mCheckBox;
            //DlkComboBox mComboBox;
            //DlkLabel mLabel;
            //DlkLink mLink;
            //DlkQuickEdit mQuickEdit;
            //DlkSideBar mSideBar;
            //DlkTab mTab;
            //DlkTable mTable;
            //DlkTextArea mTextArea;
            //DlkTextBox mTextBox;
            //DlkToolbar mToolbar;
            ////DlkUIDialog mUIDialog;

            //switch (controlType.ToLower())
            //{
            //    case "combobox":
            //        mComboBox = new DlkComboBox("ComboBox", control.mElement);
            //        mComboBox.AutoCorrectSearchMethod(ref SearchType, ref SearchValue);
            //        break;


            //}
        }

        public override string[] GetControlTypes()
        {
            Assembly assy = Assembly.GetExecutingAssembly();
            List<string> ret = new List<string>();
            foreach (Type aType in assy.GetTypes())
            {
                foreach (Attribute attribControlType in aType.GetCustomAttributes(typeof(ControlType), true))
                {
                    string controlType = ((ControlType)attribControlType).controltype;
                    ret.Add(controlType);
                }
            }
            ret.Sort();
            return ret.ToArray();
        }
    }
}
