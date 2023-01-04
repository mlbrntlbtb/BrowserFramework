using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using CommonLib.DlkSystem;
using System.Xml.Linq;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using CPExtensibility.Utility;

namespace CPExtensibility.Model
{
    /// <summary>
    /// POCO class
    /// </summary>
    public class Screen : INotifyPropertyChanged
    {
        #region PRIVATE FIELDS
        private ObservableCollection<Control> _existingControls = null;
        private ObservableCollection<Control> _newlyAddedControls = null;
        private string _name;
        private string _filePath;
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private Control _selectedControl;
        private ObservableCollection<Control> _selectedControls;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// This constructor is never used
        /// </summary>
        public Screen()
        {
            Initialize();
        }

        /// <summary>
        /// This constructor is always used
        /// </summary>
        /// <param name="screenName"></param>
        public Screen(string screenName)
        {
            Initialize(screenName);
        }
        #endregion

        #region PUBLIC PROPERTIES        
        public ObservableCollection<Control> ExistingControls
        {
            get
            {
                return _existingControls;
            }
            set
            {
                _existingControls = value;
                OnPropertyChanged("ExistingControls");
            }
        }

        public ObservableCollection<Control> NewlyAddedControls
        {
            get
            {
                return _newlyAddedControls;
            }
            set
            {
                _newlyAddedControls = value;
                OnPropertyChanged("NewlyAddedControls");
            }
        }

        public string ScreenName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("ScreenName");
            }
        }

        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                // no need for onpropertychanged since we are not rendering it on the UI/view
            }
        }

        /// <summary>
        /// This property will be binded to the SelectedItem of the datagrid in order to perform operations on the selected control such as highlight, delete, edit.
        /// </summary>
        public Control SelectedControl
        {
            get
            {
                return _selectedControl;
            }
            set
            {
                _selectedControl = value;
                OnPropertyChanged("SelectedControl");
            }
        }

        /// <summary>
        /// Same as above but for multi-select
        /// </summary>
        public ObservableCollection<Control> SelectedControls
        {
            get
            {
                return _selectedControls;
            }
            set
            {
                _selectedControls = value;
                OnPropertyChanged("SelectedControls");
            }
        }
        #endregion

        #region PRIVATE METHODS
        private void Initialize(string screenName = "")
        {
            this.ExistingControls = new ObservableCollection<Control>();
            this.NewlyAddedControls = new ObservableCollection<Control>();
            if (!string.IsNullOrEmpty(screenName))
            {
                string fi = Directory.GetFiles(DlkEnvironment.mDirObjectStore, "*.xml", SearchOption.AllDirectories).ToList().Find(x => x.Contains("OS_" + screenName + ".xml"));
                if (fi == null) 
                {
                    CreateScreen(screenName);
                    this.ExistingControls = new ObservableCollection<Control>();
                }
                else
                {
                    LoadControls(fi);
                }
                this.FilePath = fi;
            }
            this.ScreenName = screenName;
        }

        /// <summary>
        /// Create a new object store in the form of an xml file
        /// </summary>
        /// <param name="screenName"></param>
        private void CreateScreen(string screenName)
        {
            var newScreenPath = DlkEnvironment.mDirObjectStore + "\\OS_" + screenName + ".xml";
            List<DlkObjectStoreFileControlRecord> ctls = new List<DlkObjectStoreFileControlRecord>();
            DlkObjectStoreFileRecord os = new DlkObjectStoreFileRecord(newScreenPath, screenName, ctls);
            DlkObjectStoreHandler.SaveObjectStoreFile(screenName, newScreenPath, os);
        }

        /// <summary>
        /// Created a new method instead to remove the complexities that are coupled with the code being used in other parts of test runner
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadControls(string filePath)
        {
            var screen = XDocument.Load(filePath);
            var screenControls = screen.Descendants("control").ToList();
            foreach (var control in screenControls)
            {
                Control existingCtrl = new Control();
                existingCtrl.ControlName = control.Attribute("key").Value;
                existingCtrl.ControlType = control.Element("controltype").Value;
                existingCtrl.SearchMethod = control.Element("searchmethod").Value;
                existingCtrl.SearchParameter = control.Element("searchparameters").Value;

                this.ExistingControls.Add(existingCtrl);
            }
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Saves the screen to the Extensibility folder in the objectstore folder.
        /// </summary>
        /// <param name="screenName"></param>
        public void SaveScreen(string screenName)
        {
            if (! string.IsNullOrEmpty(screenName))
            {
                var newScreenPath = DlkEnvironment.mDirObjectStore + "\\OS_" + screenName + ".xml";
                List<DlkObjectStoreFileControlRecord> ctls = new List<DlkObjectStoreFileControlRecord>();
                foreach (var item in ExistingControls)
                {
                    ctls.Add(new DlkObjectStoreFileControlRecord()
                    {
                        mKey = item.ControlName,
                        mControlType = item.ControlType,
                        mSearchMethod = item.SearchMethod,
                        mSearchParameters = item.SearchParameter,
                    });
                }
                DlkObjectStoreFileRecord os = new DlkObjectStoreFileRecord(newScreenPath, screenName, ctls);
                DlkObjectStoreHandler.SaveObjectStoreFile(screenName, newScreenPath, os);
            }
        }

        public void MergeNewControlsToExistingControls()
        {
            this.ExistingControls.Union(this.NewlyAddedControls);
        }

        /// <summary>
        /// Deletes the selected controls in the MainFormWindow
        /// </summary>
        public void RemoveSelectedItems()
        {
            // remove each selected item. no need to update ui because ExistingControls is an observable collection. it will do that on its own.
            foreach (var item in SelectedControls)
            {
                ExistingControls.Remove(item);
            }
        }
        #endregion

        #region STATIC METHODS
        /// <summary>
        /// Load screens under ...\Costpoint_711\Framework\ObjectStore\Extensibility\ folder in memory
        /// </summary>
        public static void LoadScreens()
        {
            DlkEnvironment.mDirObjectStore = DlkEnvironment.mDirObjectStore + "\\Extensibility";
            DlkDynamicObjectStoreHandler.Instance.Initialize(true);
        }
        #endregion

        /// <summary>
        /// Adds a control to the list if it is not yet mapped.
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
        public bool AddControl(DescendantControl dc)
        {
            bool ret = false;
            if (!ExistingControls.Any(control => control.SearchParameter.Equals(dc.SearchParameter))) // if already mapped
            {
                this.ExistingControls.Add(dc);
                this.NewlyAddedControls.Add(dc); // reference controls that are newly added, for future purposes. if you want to, like, show all added controls since mapping started, etc.
                ret = true;
            }
            else
            {
                // if existing(already mapped) , select the mapped control to show the user.
                this.SelectedControl = ExistingControls.Where(control => control.SearchParameter.Equals(dc.SearchParameter)).ToObservableCollection().FirstOrDefault();
            }
            return ret;
        }

        /// <summary>
        /// Edits the selected controls in the MainFormWindow with the data provided in the EditControlWindow
        /// </summary>
        /// <param name="obj"></param>
        public void EditExistingControls(Messages.ControlsEdited obj)
        {
            foreach (var editedControl in obj.ControlsToEdit)
            {
                foreach (var mappedControl in ExistingControls)
                {
                    if (String.IsNullOrEmpty(obj.ContainerName))
                    {
                        if (mappedControl == editedControl)
                        {
                            mappedControl.ControlName = editedControl.ControlName;
                            break;
                        }
                    }
                    else
                    {
                        if (mappedControl == editedControl)
                        {
                            mappedControl.ControlName = obj.ContainerName + "_" + editedControl.ControlName;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if all of the controls are unique
        /// </summary>
        /// <returns></returns>
        public bool AreControlNamesUnique()
        {
            bool isUnique = ExistingControls.GroupBy(x => x.ControlName).All(g => g.Count() == 1);
            return isUnique;
        }
    }
}
