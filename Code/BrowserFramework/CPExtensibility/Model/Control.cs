using CommonLib.DlkControls;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPExtensibility.Model
{
    /// <summary>
    /// Just a POCO class, nothing special with this business object.
    /// Contains method needed in generating xpath, control type, etc for mapping purposes
    /// </summary>
    public class Control : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PRIVATE FIELDS
        private string controlName;
        private string controlType;
        private string searchMethod;
        private string searchParam;
        private const string METADATA = "metadata";
        private const string ID = "id";
        private static Dictionary<Recorder.Model.Enumerations.ControlType, int> UnnamedControlCount = new Dictionary<Recorder.Model.Enumerations.ControlType, int>();
        #endregion

        #region CONSTRUCTOR
        public Control()
        {

        }
        #endregion

        #region PUBLIC PROPERTIES
        public string ControlName 
        {
            get { return controlName;}
            set { controlName = value; OnPropertyChanged("ControlName"); }
        }
        public string ControlType
        {
            get { return controlType; }
            set { controlType = value; }
        }
        public string SearchMethod
        {
            get { return searchMethod; }
            set { searchMethod = value; }
        }
        public string SearchParameter
        {
            get { return searchParam; }
            set { searchParam = value; }
        }

        #endregion

        #region METHODS
        /// <summary>
        /// Constructs the xpath of the element that the user is interacting with during runtime.
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public static string ConstructXPath(Recorder.Model.Action act)
        {
            try
            {
                int formIndex = act.Target.AncestorFormIndex;

                if (act.Target.Type == Recorder.Model.Enumerations.ControlType.Link)
                {
                    string LinkDescriptor = act.Target.Descriptor.Value;
                    int firstIndex = LinkDescriptor.IndexOf("_");
                    int lastIndex = LinkDescriptor.LastIndexOf("_");
                    string linkIndex = LinkDescriptor.Substring(firstIndex + 1, lastIndex - (firstIndex + 1));
                    formIndex = Convert.ToInt32(LinkDescriptor.Substring(lastIndex + 1, (LinkDescriptor.Length - 1) - lastIndex));
                }
                else if (formIndex < 0)
                {
                    string id = act.Target.Id;
                    // attempt to map using id. control flow will go here whenever you interact with elements that disappear on click such as the ok button.
                    return string.Format("//*[@id='{0}']", id);
                }
                string ParentXpath = DlkInspect.GetTargetFormXPath(formIndex);
                DlkBaseControl Control = act.Target.Base;
                string strRet = string.Empty;
                string ctlId = act.Target.Id;
                string ctlClass = act.Target.Class;

                switch (act.Target.Type)
                {
                    case Recorder.Model.Enumerations.ControlType.TextBox:
                    case Recorder.Model.Enumerations.ControlType.CheckBox:
                    case Recorder.Model.Enumerations.ControlType.ComboBox:
                    case Recorder.Model.Enumerations.ControlType.TextArea:
                        strRet = ParentXpath + @"/descendant::*[@id='" + ctlId + @"']";
                        break;
                    case Recorder.Model.Enumerations.ControlType.Table:
                        strRet = ParentXpath + @"/descendant::*[@id='dTbl']";
                        break;
                    case Recorder.Model.Enumerations.ControlType.Tab:
                        strRet = ParentXpath + @"/descendant::*[@id='tbTbl']";
                        break;
                    case Recorder.Model.Enumerations.ControlType.RadioButton:
                        string value = Control.GetValue();
                        strRet = ParentXpath + @"/descendant::*[@id='" + ctlId + @"' and @value='" + value + @"']";
                        break;
                    case Recorder.Model.Enumerations.ControlType.Button:
                        switch (ctlClass)
                        {
                            case "actBtn":
                                ctlId = ctlId.Replace("___T", "");
                                strRet = ParentXpath + @"/descendant::*[contains(@id,'" + ctlId + @"') and contains(@style,'visible')]";
                                break;
                            case "fCalBtn":
                            case "fCalBtn20":
                            case "fExpandoBtn":
                            case "fBrowseBtn":
                            case "fLaunchBtn":
                                DlkBaseControl ctl = DlkInspect.GetControlViaXPath(@"./preceding-sibling::*[1]", Control);
                                string precSiblingId = ctl.GetAttributeValue("id");
                                strRet = ParentXpath + @"/descendant::*[@id='" + precSiblingId + @"']/following-sibling::*[@class='" + ctlClass + @"']";
                                break;
                            default:
                                break;
                        }
                        break;
                    case Recorder.Model.Enumerations.ControlType.Link:
                        string strClickH = Control.GetAttributeValue("clickh");
                        string strLinkIndex = strClickH.Substring(strClickH.IndexOf(",") + 1,
                            strClickH.Length - strClickH.Substring(0, strClickH.IndexOf(",") + 1).Length);
                        strRet = ParentXpath + @"/following-sibling::div[1]/descendant::*[substring-after(@clickh,',')='" + strLinkIndex + @"']";
                        break;
                    case Recorder.Model.Enumerations.ControlType.Form:
                        strRet = ParentXpath;
                        break;
                    default:
                        break;
                }

                return strRet;
            }
            catch 
            {
                string id = act.Target.Id;
                return string.Format("//*[@id='{0}']", id);
            }
            
        }

        /// <summary>
        /// Gets the control name of the interacted control
        /// </summary>
        /// <param name="OriginalName"></param>
        /// <param name="act"></param>
        /// <returns></returns>
        public static string GetDescendantControlName(string OriginalName, Recorder.Model.Action act)
        {
            string ret = OriginalName;

            try
            {
                DlkBaseControl ctl = act.Target.Base; ;

                // get the Control Name based on the Control type
                switch (act.Target.Type)
                {
                    case Recorder.Model.Enumerations.ControlType.TextBox:
                    case Recorder.Model.Enumerations.ControlType.ComboBox:
                    case Recorder.Model.Enumerations.ControlType.CheckBox:
                    case Recorder.Model.Enumerations.ControlType.TextArea:
                        DlkBaseControl myCtl = DlkInspect.GetControlViaXPath(@"./preceding-sibling::span[1]", ctl);
                        if (myCtl != null)
                        {
                            string val = myCtl.GetValue();

                            if (string.IsNullOrEmpty(val)) // control has no label
                            {
                                // Do nothing
                            }
                            else if (myCtl.GetAttributeValue(ID).ToLower().Contains(METADATA)) // dynamic label
                            {
                                ret = TrimName(val);
                            }
                        }
                        break;
                    case Recorder.Model.Enumerations.ControlType.RadioButton:
                        // dont support control names for radio buttons
                        break;
                    case Recorder.Model.Enumerations.ControlType.Button:
                        if (act.Target.Class == "fCalBtn" || act.Target.Class == "fExpandoBtn")
                        {
                            DlkBaseControl dateboxCtl = DlkInspect.GetControlViaXPath(@"./preceding-sibling::*[1]", ctl);
                            // for Button control types, get the label beside the Button as the Control Name
                            DlkBaseControl dCtlLabel = GetDescendantControlLabel(Recorder.Model.Enumerations.ControlType.TextBox, dateboxCtl);
                            if (dCtlLabel == null)
                            {
                                break;
                            }
                            string nameEnd = act.Target.Class == "fCalBtn" ? "ShowCalendar" : "ShowTextEntryPopUp";
                            ret = TrimName(dCtlLabel.GetValue()) + nameEnd;
                        }
                        else
                        {
                            ret = TrimName(act.Target.Value);
                        }
                        break;
                    case Recorder.Model.Enumerations.ControlType.Link:
                        ret = TrimName(act.Target.Value) + "Link";
                        break;
                    case Recorder.Model.Enumerations.ControlType.Table:
                    case Recorder.Model.Enumerations.ControlType.Tab:
                    default:
                        if (UnnamedControlCount.ContainsKey(act.Target.Type))
                        {
                            UnnamedControlCount[act.Target.Type]++;
                        }
                        else
                        {
                            UnnamedControlCount.Add(act.Target.Type, 1);
                        }
                        string trailer = UnnamedControlCount[act.Target.Type].ToString();
                        ret = OriginalName + trailer;
                        break;
                }
                return ret;
            }
            catch 
            {
                return ret;
            }
        }

        private static string TrimName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Name;
            }

            string filterList = @"/\~!@#$%^&*()-=+?.,:;[]{}~";
            string ret = Name;

            foreach (char c in filterList)
            {
                ret = ret.Replace(c.ToString(), string.Empty);
            }
            ret = System.Text.RegularExpressions.Regex.Replace(ret, @"\s", "|");
            string[] split = ret.Split('|');
            ret = string.Empty;
            foreach (string str in split)
            {
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                string strRem = str.Length > 1 ? str.Substring(1) : string.Empty;
                ret += str[0].ToString().ToUpper() + strRem;
            }

            return ret;
        }

        /// <summary>
        /// Gets the label beside the specific control
        /// </summary>
        /// <param name="CType"></param>
        /// <param name="Ctl"></param>
        /// <returns></returns>
        public static DlkBaseControl GetDescendantControlLabel(Recorder.Model.Enumerations.ControlType CType, DlkBaseControl Ctl)
        {
            DlkBaseControl ret = null;
            try
            {
                DlkBaseControl lblCtl = null;

                switch (CType)
                {
                    case Recorder.Model.Enumerations.ControlType.TextBox:
                    case Recorder.Model.Enumerations.ControlType.ComboBox:
                    case Recorder.Model.Enumerations.ControlType.CheckBox:
                        lblCtl = DlkInspect.GetControlViaXPath(@"./preceding-sibling::span[1]", Ctl);
                        if (lblCtl != null)
                        {
                            if (string.IsNullOrEmpty(lblCtl.GetValue())) // control has no label
                            {
                                // Do nothing
                            }
                            else if (lblCtl.GetAttributeValue(ID).ToLower().Contains(METADATA)) // dynamic label
                            {
                                ret = lblCtl;
                            }
                        }
                        break;
                    case Recorder.Model.Enumerations.ControlType.RadioButton:
                        break;
                    default:
                        break;
                }

                return ret;
            }
            catch
            {
                return ret;
                // swallow
            }        
        }

        public static string GetControlType(Recorder.Model.Enumerations.ControlType controlType)
        {
            return Enum.GetName(typeof(Recorder.Model.Enumerations.ControlType), controlType);
        }

#endregion
    }

    /// <summary>
    /// Needed for ATOM implementation
    /// </summary>
    public class DescendantControl : Control
    {
        public string XPath = string.Empty;
        public string Type = string.Empty;
        public DlkObjectStoreFileControlRecord mControl = null;
        public string OriginalName = string.Empty;
        public string Name = string.Empty;
        public string NameFormatCode = "m";

        public DescendantControl(DlkObjectStoreFileControlRecord control)
        {
            mControl = control;
            OriginalName = control.mKey;
        }
    }
}
