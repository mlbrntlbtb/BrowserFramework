using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    /// <summary>
    /// The object store handler reads object store files and stores the control data in memory
    /// It is possible to divide a screen into several object store files
    /// </summary>
    public static class DlkObjectStoreHandler
    {
        public static List<string> mDuplicateControls = new List<string>();

        /// <summary>
        /// these are the currently loaded object store records in memory
        /// </summary>
        private static List<DlkObjectStoreFileRecord> mLoadedObjectStoreFileRecords;

        /// <summary>
        /// this flag indicates if the object store has been initialized
        /// </summary>
        private static Boolean bIsInit = false;

        /// <summary>
        /// these are the list of screens (which contols are mapped to)
        /// </summary>
        public static List<String> Screens
        {
            get
            {
                return _Screens;
            }
        }
        private static List<String> _Screens;

        /// <summary>
        /// This initializes the object store
        /// </summary>
        /// <param name="ForceReInitialize">true/false to force reinitialization</param>
        public static void Initialize(Boolean ForceReInitialize)
        {
            bIsInit = false;
            Initialize();
        }

        /// <summary>
        /// Initialize the object store. If it has already been done, this does nothing.
        /// </summary>
        public static void Initialize()
        {
            if (!bIsInit)
            {
                _Screens = new List<String>();
                LoadObjectStoreFiles();
                _Screens.Sort();
                _Screens.Insert(0, "Function");
                ValidStore();
                bIsInit = true;
            }
        }

        /// <summary>
        /// This initializes one file of the object store only
        /// </summary>
        /// <param name="LoadOnlyThisFile"></param>
        public static void Initialize(String LoadOnlyThisFile)
        {
            _Screens = new List<String>();
            LoadObjectStoreFiles(LoadOnlyThisFile);
            ValidStore();
            _Screens.Insert(0, "Function");
            bIsInit = true;
        }

        /// <summary>
        /// looks in memory and returns the screen name given a file path
        /// </summary>
        /// <param name="File">the path of the object store file to look at</param>
        /// <returns></returns>
        public static String GetScreen(String File)
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
        public static String GetControlType(String Screen, String Key)
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

        public static DlkObjectStoreFileControlRecord GetControlRecord(String Screen, String Key)
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
        public static List<String> GetControlKeys(String Screen)
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
        private static List<DlkObjectStoreFileRecord> GetObjectStoreFileRecords(String Screen)
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
        public static List<DlkObjectStoreFileControlRecord> GetControlRecords(String Screen)
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
        private static DlkObjectStoreFileRecord GetObjectStoreFileRecord(String mFilePath)
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

        /// <summary>
        /// reads every object store file into memory
        /// </summary>
        /// <param name="ObjectStoreDir"></param>
        private static void LoadObjectStoreFiles()
        {
            List<DlkObjectStoreFileRecord> mOsfrs = new List<DlkObjectStoreFileRecord>();
            DirectoryInfo di = new DirectoryInfo(DlkEnvironment.mDirObjectStore);
            FileInfo[] mFiles = di.GetFiles("*.xml", SearchOption.AllDirectories);
            foreach (FileInfo mFile in mFiles)
            {
                try // ignore any erroneous file
                {
                    DlkObjectStoreFileRecord mOsfr = GetObjectStoreFileRecord(mFile.FullName);
                    mOsfrs.Add(mOsfr);
                }
                catch
                {
                    continue;
                }
            }
            mLoadedObjectStoreFileRecords = mOsfrs;
        }

        /// <summary>
        /// reads a specific object store file into memory
        /// </summary>
        /// <param name="sOneFile"></param>
        private static void LoadObjectStoreFiles(String sOneFile)
        {
            List<DlkObjectStoreFileRecord> mOsfrs = new List<DlkObjectStoreFileRecord>();
            DlkObjectStoreFileRecord mOsfr = GetObjectStoreFileRecord(sOneFile);
            mOsfrs.Add(mOsfr);
            mLoadedObjectStoreFileRecords = mOsfrs;
        }

        /// <summary>
        /// validates the object store by throwing an error if duplicate controls are found for the same screen
        /// </summary>
        private static void ValidStore()
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

        public static void SaveObjectStoreFile(String Screen, String File, DlkObjectStoreFileRecord NewObjectStoreFileRecord)
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

    }
}