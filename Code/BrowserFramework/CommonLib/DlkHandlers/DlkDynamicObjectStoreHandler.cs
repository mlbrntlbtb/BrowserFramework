using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkUtility;
using HtmlAgilityPack;

namespace CommonLib.DlkHandlers
{
    /// <summary>
    /// The object store handler reads object store files and stores the control data in memory
    /// It is possible to divide a screen into several object store files
    /// </summary>
    public class DlkDynamicObjectStoreHandler : INotifyPropertyChanged
    {
        #region PRIVATE MEMBERS
        private string mLastObjectStoreDirLoaded = Guid.NewGuid().ToString(); // some random string, unique initialization
        private Boolean _StillLoading;
        private int mFileLoadProgress;
        private string mCurrentItemProcessing;
        private List<String> _Screens;
        private List<string> _Alias;
        private BackgroundWorker bwLoadObjectStireFileRecords;
        private static DlkDynamicObjectStoreHandler instance;

        /// <summary>
        /// these are the currently loaded object store records in memory
        /// </summary>
        private List<DlkObjectStoreFileRecord> mLoadedObjectStoreFileRecords;
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Private constructor (singleton)
        /// </summary>
        private DlkDynamicObjectStoreHandler()
        {
            mLoadedObjectStoreFileRecords = new List<DlkObjectStoreFileRecord>();

            _Screens = new List<String>();
            _Alias = new List<string>();
        }

        /// <summary>
        /// Get the object store file records from memory for a given screen
        /// </summary>
        /// <param name="Screen">target screen</param>
        /// <returns>List of object records from file</returns>
        private List<DlkObjectStoreFileRecord> GetObjectStoreFileRecords(String Screen)
        {
            List<DlkObjectStoreFileRecord> mRecs = new List<DlkObjectStoreFileRecord>();
            foreach (DlkObjectStoreFileRecord osfr in mLoadedObjectStoreFileRecords)
            {
                if (osfr.mScreen == Screen)
                {
                    mRecs.Add(osfr);
                    break;
                }
            }
            return mRecs;
        }

        /// <summary>
        /// Reads the object store file data into a record
        /// </summary>
        /// <param name="mFilePath">path of objectstore file</param>
        /// <returns>Object store record of file</returns>
        private DlkObjectStoreFileRecord GetObjectStoreFileRecord(String mFilePath)
        {
            FileInfo mFile = new FileInfo(mFilePath);
            DlkObjectStoreFileRecord mOsfr = new DlkObjectStoreFileRecord();
            mOsfr.mFile = mFile.FullName;

            if (DlkEnvironment.mProductFolder == "GovWin")
            {
                HtmlDocument mXmlDoc = new HtmlDocument();
                mXmlDoc.Load(mFile.FullName);

                var data = from doc in mXmlDoc.DocumentNode.Descendants("objectstore")
                           select new
                           {
                               screen = doc.Attributes["screen"].Value
                           };

                foreach (var val in data)
                {
                    mOsfr.mScreen = val.screen;

                    Boolean bFound = false;
                    foreach (String mTmp in _Screens)
                    {
                        if (mTmp == val.screen)
                        {
                            bFound = true;
                        }
                    }
                    if (!bFound)
                    {
                        _Screens.Add(val.screen);
                    }
                }

                List<DlkObjectStoreFileControlRecord> mCntrls = new List<DlkObjectStoreFileControlRecord>();

                var dataCtrl = from doc in mXmlDoc.DocumentNode.Descendants("control")
                               select new
                               {
                                   key = doc.Attributes["key"].Value,
                                   type = doc.SelectSingleNode(".//controltype").InnerText,
                                   searchmethod = doc.SelectSingleNode(".//searchmethod").InnerText,
                                   searchparameters = doc.SelectSingleNode(".//searchparameters").InnerText
                               };

                foreach (var val in dataCtrl)
                {
                    DlkObjectStoreFileControlRecord cr = new DlkObjectStoreFileControlRecord();
                    cr.mKey = val.key;
                    cr.mControlType = val.type;
                    cr.mSearchMethod = val.searchmethod;
                    cr.mSearchParameters = val.searchparameters;
                    mCntrls.Add(cr);
                }

                mOsfr.mControls = mCntrls;
            }
            else
            {
                XDocument mXmlDoc = XDocument.Load(mFile.FullName);

                // set basic props
                var data = from doc in mXmlDoc.Descendants("objectstore")
                           select new
                           {
                               screen = doc.Attribute("screen").Value,
                               alias = doc.Attribute("alias")?.Value
                           };
                foreach (var val in data)
                {
                    mOsfr.mScreen = val.screen;

                    Boolean bFound = false;
                    foreach (String mTmp in _Screens)
                    {
                        if (mTmp == val.screen)
                        {
                            bFound = true;
                        }
                    }
                    if (!bFound)
                    {
                        _Screens.Add(val.screen);

                        if (DlkEnvironment.IsShowAppNameProduct)
                        {
                            if (val.alias == null)
                            {
                                OutDatedOS = true;
                                return null;
                            }
                            else
                            {
                                mOsfr.mAlias = val.alias;
                                _Alias.Add(val.alias);
                            }
                        }
                    }
                }

                // set control props
                List<DlkObjectStoreFileControlRecord> mCntrls = new List<DlkObjectStoreFileControlRecord>();
                var dataCtrls = from doc in mXmlDoc.Descendants("control")
                                select new
                                {
                                    key = doc.Attribute("key").Value,
                                    type = doc.Element("controltype").Value,
                                    searchmethod = doc.Element("searchmethod").Value,
                                    searchparams = doc.Element("searchparameters").Value
                                };
                foreach (var val in dataCtrls)
                {
                    DlkObjectStoreFileControlRecord cr = new DlkObjectStoreFileControlRecord();
                    cr.mKey = val.key;
                    cr.mControlType = val.type;
                    cr.mSearchMethod = val.searchmethod;
                    cr.mSearchParameters = val.searchparams;
                    mCntrls.Add(cr);
                }
                mOsfr.mControls = mCntrls.OrderBy(o => o.mKey).ToList();
            }
            return mOsfr;
        }

        /// <summary>
        /// Load object store files
        /// </summary>
        private void LoadObjectStoreFiles()
        {
            Screens.Insert(0, "Function");
            Screens.Insert(1, "Database");

            if (DlkEnvironment.IsShowAppNameProduct)
            {
                Alias.Insert(0, "Function");
                Alias.Insert(1, "Database");
            }

            if (DlkEnvironment.mProductFolder == "GovWin")
            {
                //Screens.Insert(1, "Login");
                Screens.Insert(2, "Dialog");
                Screens.Insert(3, "Browser");
                Screens.Insert(4, "Page");
            }

            DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirObjectStore);
            FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);
            //foreach (FileInfo mFile in mFiles)
            for (int idx = 0; idx < mFiles.Count(); idx++)
            {
                try // ignore any erroneous file
                {
                    DlkObjectStoreFileRecord mOsfr = GetObjectStoreFileRecord(mFiles[idx].FullName);

                    if (mOsfr == null)
                        break;

                    FileLoadProgress = Convert.ToInt32(Convert.ToDouble((Convert.ToDouble(idx + 1) / mFiles.Count())) * 100);
                    CurrentItemProcessing = mOsfr.mScreen;
                    if (mLoadedObjectStoreFileRecords.Any(x => x.mScreen == CurrentItemProcessing)) 
                    {
                        DlkObjectStoreFileRecord originalRecord = mLoadedObjectStoreFileRecords.First(x => x.mScreen == CurrentItemProcessing);
                        originalRecord.mControls.AddRange(mOsfr.mControls);
                        originalRecord.mControls.Sort((x, y) => x.mKey.CompareTo(y.mKey));
                        continue;
                    }
                    mLoadedObjectStoreFileRecords.Add(mOsfr);
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// reads every object store file into memory
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LoadObjectStoreFiles(object sender, DoWorkEventArgs e)
        {
            StillLoading = true;
            LoadObjectStoreFiles();       
        }

        /// <summary>
        /// Completion method of object store file loading
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void LoadObjectStoreFilesCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StillLoading = false;
        }


        /// <summary>
        /// reads a specific object store file into memory
        /// </summary>
        /// <param name="sOneFile">File path of object store to load</param>
        private void LoadObjectStoreFiles(String sOneFile)
        {
            DlkObjectStoreFileRecord mOsfr = GetObjectStoreFileRecord(sOneFile);
            mLoadedObjectStoreFileRecords.Add(mOsfr);
        }

        /// <summary>
        /// validates the object store by throwing an error if duplicate controls are found for the same screen
        /// </summary>
        private void ValidStore()
        {
            mDuplicateControls.Clear();
            foreach (String mScreen in _Screens)
            {
                List<DlkObjectStoreFileControlRecord> Controls = GetControlRecords(mScreen);
                List<String> mKeys = new List<String>();
                for (int i = 0; i < Controls.Count; i++)
                {
                    foreach (String mKey in mKeys)
                    {
                        if (Controls[i].mKey == mKey)
                        {
                            string duplicate = mScreen + ";" + mKey;
                            if (!mDuplicateControls.Contains(mKey))
                            {
                                mDuplicateControls.Add(duplicate);
                            }
                            continue;
                            //throw new Exception("Duplicate control records found for Component: " + mScreen + ", Key:" + mKey);
                        }
                    }
                    mKeys.Add(Controls[i].mKey);
                }
            }
        }
        #endregion

        #region PROTECTED METHODS
        /// <summary>
        /// Notify property changed method
        /// </summary>
        /// <param name="propertyName">Property that changed</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region PUBLIC MEMBERS
        public List<string> mDuplicateControls = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public List<String> Screens
        {
            get
            {
                return _Screens;
            }
        }

        /// <summary>
        /// Screen name alias (for costpoint products only)
        /// </summary>
        public List<string> Alias
        {
            get 
            {
                return _Alias;
            }
        }

        public Boolean StillLoading
        {
            get
            {
                return _StillLoading;
            }

            set
            {
                _StillLoading = value;
                OnPropertyChanged("StillLoading");
            }
        }

        public bool OutDatedOS { get; set; }

        public int FileLoadProgress
        {
            get
            {
                return mFileLoadProgress;
            }
            set
            {
                mFileLoadProgress = value;
                OnPropertyChanged("FileLoadProgress");
            }
        }

        public string CurrentItemProcessing
        {
            get
            {
                return mCurrentItemProcessing;
            }
            set
            {
                mCurrentItemProcessing = value;
                OnPropertyChanged("CurrentItemProcessing");
            }
        }

        public static DlkDynamicObjectStoreHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DlkDynamicObjectStoreHandler();
                }
                return instance;
            }
        }


        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Load object store files, if same Object Store is previosuly loaded, will do nothing
        /// </summary>
        /// <param name="synchronous">True if func will wait until fully loaded, False if load in the background</param>
        public void Initialize(bool synchronous = false)
        {
            if (mLastObjectStoreDirLoaded == DlkEnvironment.mDirObjectStore)
            {
                return;
            }
            mLoadedObjectStoreFileRecords.Clear();
            Screens.Clear();
            Alias.Clear();
            if (synchronous)
            {
                LoadObjectStoreFiles();
            }
            else
            {
                bwLoadObjectStireFileRecords = new BackgroundWorker();
                bwLoadObjectStireFileRecords.DoWork += new DoWorkEventHandler(LoadObjectStoreFiles);
                bwLoadObjectStireFileRecords.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadObjectStoreFilesCompleted);
                bwLoadObjectStireFileRecords.RunWorkerAsync();
            }
            mLastObjectStoreDirLoaded = DlkEnvironment.mDirObjectStore;
        }

        /// <summary>
        /// This initializes one file of the object store only
        /// </summary>
        /// <param name="LoadOnlyThisFile">File to initialize</param>
        public void Initialize(String LoadOnlyThisFile)
        {
            mLoadedObjectStoreFileRecords.Clear();
            Screens.Clear();
            Alias.Clear();
            LoadObjectStoreFiles(LoadOnlyThisFile);
            ValidStore();
            _Screens.Insert(0, "Function");
            
            if(DlkEnvironment.IsShowAppNameProduct)
                _Alias.Insert(0, "Function");
        }

        /// <summary>
        /// looks in memory and returns the screen name given a file path
        /// </summary>
        /// <param name="File">the path of the object store file to look at</param>
        /// <returns>Target screen of file</returns>
        public String GetScreen(String File)
        {
            String mRes = "";
            foreach (DlkObjectStoreFileRecord osfr in mLoadedObjectStoreFileRecords)
            {
                if (osfr.mFile == File)
                {
                    mRes = osfr.mScreen;
                    break;
                }
            }
            return mRes;
        }

        /// <summary>
        /// looks in memory and returns the control type (button, textbox, etc)
        /// </summary>
        /// <param name="Screen">the screen our control is on</param>
        /// <param name="Key">the assigned key of the control</param>
        /// <returns>Control type of control with input key</returns>
        public String GetControlType(String Screen, String Key)
        {
            String mType = "";
            List<DlkObjectStoreFileControlRecord> mCtrls = GetControlRecords(Screen);
            foreach (DlkObjectStoreFileControlRecord cr in mCtrls)
            {
                if (cr.mKey == Key)
                {
                    mType = cr.mControlType;
                    break;
                }
            }
            return mType;
        }

        /// <summary>
        /// Get control record from Screen with input control key
        /// </summary>
        /// <param name="Screen">Target screen</param>
        /// <param name="Key">Control key</param>
        /// <returns>Object record with input key</returns>
        public DlkObjectStoreFileControlRecord GetControlRecord(String Screen, String Key)
        {
            List<DlkObjectStoreFileControlRecord> mCtrls = GetControlRecords(Screen);
            foreach (DlkObjectStoreFileControlRecord cr in mCtrls)
            {
                if (cr.mKey == Key)
                {
                    return cr;
                }
            }
            throw new Exception("Control record not found.");
        }

        /// <summary>
        /// looks in memory and gets a list of keys for the screen
        /// </summary>
        /// <param name="Screen">the assigned screen name the controls reside on</param>
        /// <returns>a list of keys</returns>
        public List<String> GetControlKeys(String Screen)
        {
            List<String> mResults = new List<string>();
            List<DlkObjectStoreFileControlRecord> mCtrls = GetControlRecords(Screen);
            foreach (DlkObjectStoreFileControlRecord cr in mCtrls)
            {
                mResults.Add(cr.mKey);
            }
            mResults.Sort();
            return mResults;
        }

        /// <summary>
        /// Looks in memory and returns the control records given a screen
        /// </summary>
        /// <param name="Screen"></param>
        /// <returns></returns>
        public List<DlkObjectStoreFileControlRecord> GetControlRecords(String Screen)
        {
            List<DlkObjectStoreFileControlRecord> mRecs = new List<DlkObjectStoreFileControlRecord>();
            List<DlkObjectStoreFileRecord> mOsfrs = GetObjectStoreFileRecords(Screen);
            foreach (DlkObjectStoreFileRecord osfr in mOsfrs)
            {
                mRecs.AddRange(osfr.mControls);
            }      
            return mRecs;
        }

        /// <summary>
        /// Save object store file
        /// </summary>
        /// <param name="Screen">Target screen</param>
        /// <param name="File">File path</param>
        /// <param name="NewObjectStoreFileRecord">Object store file record</param>
        public void SaveObjectStoreFile(String Screen, String File, DlkObjectStoreFileRecord NewObjectStoreFileRecord)
        {
            List<XElement> controls = new List<XElement>();

            HashSet<String> controlKeys = new HashSet<String>();
            for (int i = 0; i < NewObjectStoreFileRecord.mControls.Count; i++)
            {
                if (!controlKeys.Add(NewObjectStoreFileRecord.mControls[i].mKey))
                    throw new Exception("Duplicate control key '" + NewObjectStoreFileRecord.mControls[i].mKey + "'");
            }

            foreach (DlkObjectStoreFileControlRecord controlRec in NewObjectStoreFileRecord.mControls)
            {
                controls.Add(new XElement("control",
                    new XAttribute("key", controlRec.mKey),
                    new XElement("controltype", controlRec.mControlType),
                    new XElement("searchmethod", controlRec.mSearchMethod),
                    new XElement("searchparameters", controlRec.mSearchParameters)
                    )
                    );
            }

            XElement objectstore = new XElement("objectstore",
                new XAttribute("screen", Screen),
                controls
                );

            XDocument xDoc = new XDocument(objectstore);
            xDoc.Save(File);


        }
        #endregion
    }
}