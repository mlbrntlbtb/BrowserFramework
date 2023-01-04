//#define OS_TEMPFIX
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Reflection;
using OpenQA.Selenium;
using TestRunner.Common;
using CommonLib.DlkControls;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for OSRecorder.xaml
    /// </summary>
    public partial class OSRecorder : Window
    {
        private DlkBaseControl mSelectedControl;
        private DlkObjectStoreFileRecord mCurrentObjectStoreRec;
        private String openedFile;
        public ObservableCollection<DlkObjectStoreFileControlRecord> mControls = new ObservableCollection<DlkObjectStoreFileControlRecord>();
        public bool isMControlsFiltered = false;

        public DlkOSRecorderData mOSRecorderData;
        public DlkAddControlData AddControlData;

        public DlkDynamicObjectStoreHandler DlkDynamicObjectStoreHandler
        {
            get { return DlkDynamicObjectStoreHandler.Instance; }
        }

        public OSRecorder()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RefreshDOM();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void RefreshDOM()
        {
            String dom = DlkEnvironment.AutoDriver.PageSource;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(dom);
            DOMViewer.xmlDocument = doc;
        }

        private void RefreshDOM(IWebElement element)
        {
            IWebElement parentElement = element.FindElement(By.XPath(".."));
            String dom = parentElement.GetAttribute("outerHTML");
            XmlDocument doc = new XmlDocument();
            
            Sgml.SgmlReader reader = new Sgml.SgmlReader();
            reader.DocType = string.Empty;
            reader.InputStream = new StringReader(dom);
            StringWriter output = new StringWriter();
            XmlTextWriter writer = new XmlTextWriter(output);
            reader.Read();
            while (!reader.EOF)
            {
                writer.WriteNode(reader, true);
            }
            writer.Close();
            string xmlcode = output.ToString();

            xmlcode = xmlcode.Replace("&amp;", "&");
            xmlcode = xmlcode.Replace("&nbsp;", "&#160;");


            doc.LoadXml(xmlcode);
            DOMViewer.xmlDocument = doc;
        }

        private void btnInspect_Click(object sender, RoutedEventArgs e)
        {
                try
                {
                    DlkEnvironment.AutoDriver.SwitchTo().DefaultContent();
                }
                catch
                {
                    StartBrowserDialog dlgStartBrowser = new StartBrowserDialog();
                    dlgStartBrowser.ShowDialog();
                    if (dlgStartBrowser.DialogResult == true)
                        DlkUserMessages.ShowInfo(DlkUserMessages.INF_INSPECT_CONTROL);
                    return;
                }


            StartInspecting();
        }

        private void StartInspecting()
        {
            if (mSelectedControl != null && mSelectedControl.Exists(1))
            {
                mSelectedControl.ClearHightlight();
                mSelectedControl.SetAttribute("dlkselected", "false");
            }
            DlkEnvironment.AutoDriver.Manage().Timeouts().AsynchronousJavaScript = new TimeSpan(0, 0, 0, 15);
            Object node = (Object)((IJavaScriptExecutor)DlkEnvironment.AutoDriver).ExecuteAsyncScript(
                "var callback =  arguments[arguments.length - 1];" +
                "function dispatchClickEvent() {" +
                    "document.removeEventListener(\"click\",nodeClicked,true);" +
                "}" +
                "function nodeClicked(event) {" +
                    "event.stopPropagation();" +
                    "event.preventDefault();" +
                    "callback(event.target);" +
                    "dispatchClickEvent();" +
                    "return;" +                    
                "}"+
                "document.addEventListener(\"click\", nodeClicked, true);");
            mSelectedControl = new DlkBaseControl("Node", (IWebElement)node);
            mSelectedControl.Highlight(false);
            mSelectedControl.SetAttribute("dlkselected", "true");

            mOSRecorderData.CSSPath = mSelectedControl.GetPath();

            this.Focus();

            RefreshDOM(mSelectedControl.mElement);
            DOMViewer.SelectNode();

            AutoPopulateControlProperties();
            //DOMViewer.SelectNode(mSelectedControl.GetAttributeValue("outerHTML").Split(new String[] { "><" }, StringSplitOptions.RemoveEmptyEntries)[0]);
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DlkDynamicObjectStoreHandler.Initialize(true);
                mOSRecorderData = new DlkOSRecorderData();
                dgControls.DataContext = mControls;
                mControls.CollectionChanged += mControls_CollectionChanged;
                //dgControls.DataContext = xmlNodes;                
                txtScreen.DataContext = mOSRecorderData;

                AddControlData = new DlkAddControlData();
                cboControlType.ItemsSource = LoadControlTypes();
                cboControlType.DataContext = AddControlData;
                txtControlName.DataContext = AddControlData;
                cboSearchType.ItemsSource = LoadSearchType();
                cboSearchType.DataContext = AddControlData;
                txtSearchValue.DataContext = AddControlData;
                EnableControlProperties(false);                
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mControls_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //refresh filtered list if currently filtering
            if (isMControlsFiltered)
            {
                dgControls.DataContext = mControls.Where(x => x.mKey.Contains(txtFind.Text));
            }
        }

        private void NewObjectStore()
        {
            this.Title = "New -Object Store Recorder";
            ClearControls();
            ClearOSRecorderData();
            ClearControlProperties();
        }

        private void ClearControls()
        {
            while (true)
            {
                if (mControls.Count > 0)
                {
                    mControls.RemoveAt(0);
                    continue;
                }
                break;
            }
        }

        private void ClearOSRecorderData()
        {
            mOSRecorderData.Screen = "";
            mOSRecorderData.CSSPath = "";
        }

        private void ClearControlProperties()
        {
            AddControlData.New = false;
            AddControlData.ControlType = "";
            AddControlData.ControlName = "";
            AddControlData.SearchType = "";
            AddControlData.SearchValue = "";
            EnableControlProperties(false);
        }

        private void AutoPopulateControlProperties()
        {
            if(AddControlData.New)
                AddControlData.ControlName = "";

            //Detect control type
            string detectedControlType = DetectControlType(mSelectedControl);
            AddControlData.ControlType = detectedControlType;


            String mId = mSelectedControl.GetAttributeValue("id");
            String mName = mSelectedControl.GetAttributeValue("name");
            String mClass = mSelectedControl.GetAttributeValue("class");
            String mSearchType = "";
            String mSearchValue = "";

            if (mId != null && mId != "")
            {
                mSearchType = "ID";
                mSearchValue = mId;
            }
            else if (mName != null && mName != "")
            {
                mSearchType = "NAME";
                mSearchValue = mName;
            }
            else if (mClass != null && mClass != "")
            {
                mSearchType = "CLASSNAME";
                mSearchValue = mClass.Split(' ').First();
            }
            else
            {
                mSearchType = "XPATH";
                mSearchValue = mSelectedControl.FindXPath();
            }

            //Auto correct the search method
            AutoCorrectSearchMethod(mSelectedControl, detectedControlType, ref mSearchType, ref mSearchValue);

            AddControlData.SearchType = mSearchType;
            AddControlData.SearchValue = mSearchValue;
        }

        private void NewControl()
        {
            AddControlData.New = true;
            AddControlData.ControlName = "";
            AddControlData.ControlType = "";
            AddControlData.SearchType = "";
            AddControlData.SearchValue = "";
            EnableControlProperties(true);
        }

        private void EnableControlProperties(bool IsEnabled)
        {
            cboControlType.IsEnabled = IsEnabled;
            txtControlName.IsEnabled = IsEnabled;
            cboSearchType.IsEnabled = IsEnabled;
            txtSearchValue.IsEnabled = IsEnabled;
            btnInspect.IsEnabled = IsEnabled;
            btnSaveControl.IsEnabled = IsEnabled;
            btnCancel.IsEnabled = IsEnabled;
        }

        private void ReplaceControl(String Key, DlkAddControlData NewControl)
        {
            for (int mIndex = 0; mIndex < mControls.Count; mIndex++)
            {
                if (mControls[mIndex].mKey == Key)
                {
                    mControls.RemoveAt(mIndex);
                    mControls.Insert(mIndex, new DlkObjectStoreFileControlRecord(NewControl.ControlName, NewControl.ControlType, NewControl.SearchType, NewControl.SearchValue));
                    break;
                }
            }
        }

        private void DeleteControl(String Key)
        {
            for(int mIndex = 0; mIndex < mControls.Count; mIndex++)
            {
                if(mControls[mIndex].mKey == Key)
                {
                    mControls.RemoveAt(mIndex);
                    break;
                }
            }
        }

        private void DOMViewer_SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                String mXPath = DOMViewer.mSelectedItemXPath;
                mXPath.Replace("/html[1]", "/");
                SelectControl(mXPath);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void SelectControl(String XPath)
        {
            if (mSelectedControl != null && mSelectedControl.Exists(1))
            {
                mSelectedControl.ClearHightlight();
                mSelectedControl.SetAttribute("dlkselected", "false");
            }
            mSelectedControl = new DlkBaseControl("Node", "XPath", XPath);
            mSelectedControl.Highlight(true);
            mSelectedControl.SetAttribute("dlkselected", "true");

            mOSRecorderData.CSSPath = mSelectedControl.GetPath();

        }

        private string DetectControlType(DlkBaseControl control)
        {
            //switch(DlkEnvironment.mProduct.ToLower())
            //{
            //    case "ngcrm":
            //        return DlkNgCRMControlHelper.DetectControlType(control);
            //}

            string ret = DlkAssemblyHandler.Invoke(DlkEnvironment.mLibrary, DlkAssemblyHandler.TestMethods.DetectControlType,
                Parameters: control).ToString();
            return ret;
            //return "";
        }

        private void AutoCorrectSearchMethod(DlkBaseControl control, string controlType, ref string SearchType, ref string SearchValue)
        {
            //switch (DlkEnvironment.mProduct.ToLower())
            //{
            //    case "ngcrm":
            //        DlkNgCRMControlHelper.AutoCorrectSearchMethod(control, controlType, ref SearchType, ref SearchValue);
            //        break;
            //}
            object[] paramArray = new object[] {control, controlType, SearchType, SearchValue};
            DlkAssemblyHandler.Invoke(DlkEnvironment.mLibrary, DlkAssemblyHandler.TestMethods.AutoCorrectSearchMethod,
                Parameters : paramArray);
            SearchType = paramArray[2].ToString();
            SearchValue = paramArray[3].ToString();
        }

        private void btnHighlight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DlkEnvironment.AutoDriver == null)
                {
                    StartBrowserDialog dlgStartBrowser = new StartBrowserDialog();
                    dlgStartBrowser.ShowDialog();
                    DlkUserMessages.ShowInfo(DlkUserMessages.INF_HIGHLIGHT_CONTROL);
                    return;
                }

                if (AddControlData.SearchType == "")
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_SEARCH_TYPE);
                    return;
                }

                if (AddControlData.SearchValue == "")
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_SEARCH_VALUE);
                    return;
                }

                try
                {
                    DlkBaseControl currentControl = new DlkBaseControl("Control", AddControlData.SearchType, AddControlData.SearchValue);
                    currentControl.Highlight(false);
                }
                catch
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_CONTROL_NOT_FOUND);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnSaveControl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddControlData.New)
                {
                    mControls.Add(new DlkObjectStoreFileControlRecord(AddControlData.ControlName, AddControlData.ControlType, AddControlData.SearchType, AddControlData.SearchValue));
                    AddControlData.New = false;
                }
                else
                {
                    DlkObjectStoreFileControlRecord selectedControl = (DlkObjectStoreFileControlRecord)dgControls.SelectedItem;
                    ReplaceControl(selectedControl.mKey, AddControlData);
                }
                EnableControlProperties(false);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private String[] LoadControlTypes()
        {
            String[] ControlTypes = {"Button", "TextBox", "ComboBox", "CheckBox", "Label", "Link", "QuickEdit", "SideBar", "Tab", "TabPage", "Table",
                            "TextArea", "Toolbar", "UIDialog", "Image", "ToolTip", "List"};

            string assyPath;
            switch (DlkEnvironment.mProductFolder.ToLower())
            {
                case "ajera":
                case "ngcrm":
                case "costpoint":
                case "govwin":
                case "kona":
                case "maconomynavigator":
                case "maconomytouch":
                case "navigator":
                case "visioncrm":
                case "visiontime":
                case "stormtouchtimeexpense":
                case "stormtouchcrm":
                    assyPath = Path.Combine(DlkEnvironment.mDirTools, "TestRunner", DlkEnvironment.mLibrary);
                    break;
                default:
                    return ControlTypes;
            }
            ControlTypes = (string[])DlkAssemblyHandler.Invoke(assyPath, DlkAssemblyHandler.TestMethods.GetControlTypes, Parameters: null);
            return ControlTypes;

        }

        private String[] LoadSearchType()
        {
            String[] SearchTypes = {"ID", "CSS", "XPATH", "IFRAME_XPATH", "NAME", "CLASSNAME", "PARTIALLINKTEXT", "TAGNAME_TEXT", "TAGNAME_ATTRIBUTE", "IMG_SRC",
                                    "PARENTID_CHILDCLASS", "PARENT_CHILDCSS", "PARENT_CHILDTAG", "CLASS_DISPLAY"};
            return SearchTypes;
        }

        private void dgControls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgControls.SelectedItem != null)
                {
                    DlkObjectStoreFileControlRecord selectedControl = (DlkObjectStoreFileControlRecord)dgControls.SelectedItem;
                    AddControlData.New = false;
                    AddControlData.ControlType = selectedControl.mControlType;
                    AddControlData.ControlName = selectedControl.mKey;
                    AddControlData.SearchType = selectedControl.mSearchMethod;
                    AddControlData.SearchValue = selectedControl.mSearchParameters;
                    EnableControlProperties(false);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mnuNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewObjectStore();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mnuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xml";
                saveDialog.InitialDirectory = DlkEnvironment.mDirObjectStore;
                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    mCurrentObjectStoreRec = new DlkObjectStoreFileRecord(saveDialog.FileName, mOSRecorderData.Screen, mControls.ToList());
                    try
                    {
                        DlkDynamicObjectStoreHandler.SaveObjectStoreFile(mOSRecorderData.Screen, saveDialog.FileName, mCurrentObjectStoreRec);
                        System.Windows.MessageBox.Show("Saved: " + saveDialog.FileName);
                        DlkUserMessages.ShowInfo(DlkUserMessages.INF_SAVE_SUCCESSFUL + saveDialog.FileName);
                        mnuSave.IsEnabled = true;
                        openedFile = saveDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        DlkUserMessages.ShowInfo(DlkUserMessages.ERR_SAVE_ERROR + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mnuOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = "xml";
                openFileDialog.InitialDirectory = DlkEnvironment.mDirObjectStore;
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ClearControls();
                    ClearOSRecorderData();
                    String mScreen = DlkDynamicObjectStoreHandler.GetScreen(openFileDialog.FileName);
                    openedFile = openFileDialog.FileName;
                    List<DlkObjectStoreFileControlRecord> lstControls = DlkDynamicObjectStoreHandler.GetControlRecords(mScreen);
                    foreach (DlkObjectStoreFileControlRecord ctrl in lstControls)
                    {
                        mControls.Add(ctrl);
                    }
                    mOSRecorderData.Screen = mScreen;
                    Title = openFileDialog.FileName;
                    mnuSave.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void mnuSave_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                mCurrentObjectStoreRec = new DlkObjectStoreFileRecord(openedFile, mOSRecorderData.Screen, mControls.ToList());
                DlkDynamicObjectStoreHandler.SaveObjectStoreFile(mOSRecorderData.Screen, openedFile, mCurrentObjectStoreRec);
                System.Windows.MessageBox.Show("Saved: " + openedFile);
                DlkUserMessages.ShowInfo(DlkUserMessages.INF_SAVE_SUCCESSFUL + openedFile);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Access to the path"))
                    DlkUserMessages.ShowInfo(DlkUserMessages.ERR_SAVE_ERROR + ex.Message + " Check if Read-only.");
                else
                    DlkUserMessages.ShowInfo(DlkUserMessages.ERR_SAVE_ERROR + ex.Message);
            }
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewControl();
                btnSaveControl.Content = "Add";
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgControls.SelectedItem != null)
                {
                    EnableControlProperties(true);
                    btnSaveControl.Content = "Update";
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteControl(((DlkObjectStoreFileControlRecord)dgControls.SelectedItem).mKey);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddControlData.New = false;
                if (dgControls.SelectedItem != null)
                {
                    DlkObjectStoreFileControlRecord selectedControl = (DlkObjectStoreFileControlRecord)dgControls.SelectedItem;
                    AddControlData.ControlType = selectedControl.mControlType;
                    AddControlData.ControlName = selectedControl.mKey;
                    AddControlData.SearchType = selectedControl.mSearchMethod;
                    AddControlData.SearchValue = selectedControl.mSearchParameters;

                }
                else
                {
                    AddControlData.ControlType = "";
                    AddControlData.ControlName = "";
                    AddControlData.SearchType = "";
                    AddControlData.SearchValue = "";
                }
                EnableControlProperties(false);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnFindNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isMControlsFiltered = true;
                dgControls.DataContext = mControls.Where(x => x.mKey.Contains(txtFind.Text));
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtFind.Clear();
                isMControlsFiltered = false;
                dgControls.DataContext = mControls;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
    }
}

