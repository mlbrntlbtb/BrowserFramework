using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using System.Reflection;

namespace TestIT.Sys
{
    /// <summary>
    /// The object store handler reads object store files and stores the control data in memory
    /// It is possible to divide a screen into several object store files
    /// </summary>
    public class ObjectStoreHandler : INotifyPropertyChanged
    {
        public List<string> mDuplicateControls = new List<string>();

        public String ProductName { get; set; }

        /// <summary>
        /// these are the list of screens (which contols are mapped to)
        /// </summary>
        public List<String> Screens
        {
            get
            {
                return _Screens;
            }
        }
        private List<String> _Screens;

        private Boolean _StillLoading;
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

        private int mFileLoadProgress;
        /// <summary>
        /// 
        /// </summary>
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

        private string mCurrentItemProcessing;
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// these are the currently loaded object store records in memory
        /// </summary>
        private List<DlkObjectStoreFileRecord> mLoadedObjectStoreFileRecords;

        private BackgroundWorker bwLoadObjectStireFileRecords;

        private static ObjectStoreHandler instance;

        public ObjectStoreHandler()
        {
            mLoadedObjectStoreFileRecords = new List<DlkObjectStoreFileRecord>();

            _Screens = new List<String>();
        }

        public static ObjectStoreHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ObjectStoreHandler();
                }
                return instance;
            }
        }

        public void Initialize(bool synchronous)
        {
            if (synchronous)
            {
                mLoadedObjectStoreFileRecords.Clear();
                Screens.Clear();
                LoadObjectStoreFiles();
            }
            else
                Initialize();

        }

        /// <summary>
        /// Initialize the object store. If it has already been done, this does nothing.
        /// </summary>
        public void Initialize()
        {
            mLoadedObjectStoreFileRecords.Clear();
            Screens.Clear();
            bwLoadObjectStireFileRecords = new BackgroundWorker();
            bwLoadObjectStireFileRecords.DoWork += new DoWorkEventHandler(LoadObjectStoreFilesForUnitTest);
            bwLoadObjectStireFileRecords.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadObjectStoreFilesCompleted);
            bwLoadObjectStireFileRecords.RunWorkerAsync();
            //ValidStore();
        }

        /// <summary>
        /// Initialize the object store. If it has already been done, this does nothing.
        /// </summary>
        public void InitializeForUnitTest()
        {
            mLoadedObjectStoreFileRecords.Clear();
            Screens.Clear();
            bwLoadObjectStireFileRecords = new BackgroundWorker();
            bwLoadObjectStireFileRecords.DoWork += new DoWorkEventHandler(LoadObjectStoreFilesForUnitTest);
            bwLoadObjectStireFileRecords.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LoadObjectStoreFilesCompleted);
            bwLoadObjectStireFileRecords.RunWorkerAsync();
            //ValidStore();
        }

        /// <summary>
        /// This initializes one file of the object store only
        /// </summary>
        /// <param name="LoadOnlyThisFile"></param>
        public void Initialize(String LoadOnlyThisFile)
        {
            mLoadedObjectStoreFileRecords.Clear();
            Screens.Clear();
            LoadObjectStoreFiles(LoadOnlyThisFile);
            ValidStore();
            _Screens.Insert(0, "Function");
        }

        /// <summary>
        /// looks in memory and returns the screen name given a file path
        /// </summary>
        /// <param name="File">the path of the object store file to look at</param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// get the object store file records from memory for a given screen
        /// </summary>
        /// <param name="Screen"></param>
        /// <returns></returns>
        private List<DlkObjectStoreFileRecord> GetObjectStoreFileRecords(String Screen)
        {
            List<DlkObjectStoreFileRecord> mRecs = new List<DlkObjectStoreFileRecord>();
            foreach (DlkObjectStoreFileRecord osfr in mLoadedObjectStoreFileRecords)
            {
                if (osfr.mScreen == Screen)
                {
                    mRecs.Add(osfr);
                }
            }
            return mRecs;
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
        /// Reads the object store file data into a record
        /// </summary>
        /// <param name="mFilePath"></param>
        /// <returns></returns>
        private DlkObjectStoreFileRecord GetObjectStoreFileRecord(String mFilePath)
        {
            FileInfo mFile = new FileInfo(mFilePath);
            DlkObjectStoreFileRecord mOsfr = new DlkObjectStoreFileRecord();
            mOsfr.mFile = mFile.FullName;

            XDocument mXmlDoc = XDocument.Load(mFile.FullName);

            // set basic props
            var data = from doc in mXmlDoc.Descendants("objectstore")
                       select new
                       {
                           screen = doc.Attribute("screen").Value
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
            mOsfr.mControls = mCntrls;
            return mOsfr;
        }

        private void LoadObjectStoreFiles()
        {
            Screens.Insert(0, "Function");
            Screens.Insert(1, "Database");
            if (DlkEnvironment.mProductFolder == "GovWin")
            {
                //Screens.Insert(1, "Login");
                Screens.Insert(2, "Dialog");
                Screens.Insert(3, "Browser");
                Screens.Insert(4, "Page");
            }

            DirectoryInfo di = new DirectoryInfo(@"C:\TFS\BrowserFramework\Products\MaconomyTouch\Framework\ObjectStore");
            FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);
            //foreach (FileInfo mFile in mFiles)
            for (int idx = 0; idx < mFiles.Count(); idx++)
            {
                try // ignore any erroneous file
                {
                    DlkObjectStoreFileRecord mOsfr = GetObjectStoreFileRecord(mFiles[idx].FullName);
                    FileLoadProgress = Convert.ToInt32(Convert.ToDouble((Convert.ToDouble(idx + 1) / mFiles.Count())) * 100);
                    CurrentItemProcessing = mOsfr.mScreen;
                    mLoadedObjectStoreFileRecords.Add(mOsfr);
                }
                catch
                {
                    continue;
                }
            }
        }

        private void LoadObjectStoreFilesForUnitTest()
        {
            string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mRootPath = Directory.GetParent(binDir).FullName;
            while (new DirectoryInfo(mRootPath).GetDirectories()
                .Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                mRootPath = Directory.GetParent(mRootPath).FullName;
            }
            String mDirFramework = Path.Combine(mRootPath, @"Products\" + ProductName + @"\Framework");

            Screens.Insert(0, "Function");
            Screens.Insert(1, "Database");
            if (DlkEnvironment.mProductFolder == "GovWin")
            {
                //Screens.Insert(1, "Login");
                Screens.Insert(2, "Dialog");
                Screens.Insert(3, "Browser");
                Screens.Insert(4, "Page");
            }

            var mDirObjectStoreFile = Path.Combine(mDirFramework, "ObjectStore");
            DlkEnvironment.mDirObjectStore = mDirObjectStoreFile;
            DirectoryInfo di = new DirectoryInfo(mDirObjectStoreFile);
            FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);
            //foreach (FileInfo mFile in mFiles)
            for (int idx = 0; idx < mFiles.Count(); idx++)
            {
                try // ignore any erroneous file
                {
                    DlkObjectStoreFileRecord mOsfr = GetObjectStoreFileRecord(mFiles[idx].FullName);
                    FileLoadProgress = Convert.ToInt32(Convert.ToDouble((Convert.ToDouble(idx + 1) / mFiles.Count())) * 100);
                    CurrentItemProcessing = mOsfr.mScreen;
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
        /// <param name="ObjectStoreDir"></param>
        private void LoadObjectStoreFiles(object sender, DoWorkEventArgs e)
        {
            StillLoading = true;
            LoadObjectStoreFiles();
        }
		
		/// <summary>
        /// reads every object store file into memory
        /// </summary>
        /// <param name="ObjectStoreDir"></param>
        private void LoadObjectStoreFilesForUnitTest(object sender, DoWorkEventArgs e)
        {
            StillLoading = true;
            LoadObjectStoreFilesForUnitTest();
        }

        private void LoadObjectStoreFilesCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StillLoading = false;
        }


        /// <summary>
        /// reads a specific object store file into memory
        /// </summary>
        /// <param name="sOneFile"></param>
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}