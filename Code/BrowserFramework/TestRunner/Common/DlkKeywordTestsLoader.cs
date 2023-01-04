using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace TestRunner.Common
{
    public class KwDirItem : INotifyPropertyChanged
    {
        #region DECLARATIONS
        private bool mIsSelected;
        private string mPath = string.Empty;
        private string mAuthor = string.Empty;
        private List<DlkTag> mTags = new List<DlkTag>();        
        public String Name { get; set; }
        public bool IsExpanded { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region ENUMS
        public enum SearchType
        {
            TestsSuitesOnly,
            TagsOnly,
            TestsSuitesAndTags,
            Owner,
            Author
        }
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Path of KwDirItem
        /// </summary>
        public String Path
        {
            get
            {
                return mPath;
            }
            set
            {
                mPath = value;
                OnPropertyChanged("Path");
            }
        }

        /// <summary>
        /// Author of test
        /// </summary>
        public String Author
        {
            get
            {
                return mAuthor;
            }
            set
            {
                mAuthor = value;
                OnPropertyChanged("Author");
            }
        }

        /// <summary>
        /// IsSelected state of KwDirItem
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return mIsSelected;
            }
            set
            {
                mIsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Tags of KwDirItem
        /// </summary>
        public List<DlkTag> Tags
        {
            get
            {
                return mTags;
            }
            set
            {
                mTags = value;
            }
        }
        #endregion

        /// <summary>
        /// Property changed notifyer
        /// </summary>
        /// <param name="Name">Name of property</param>
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
    }

    public class KwInstance : KwDirItem
    {
        public string Icon
        {
            get
            {
                return "pack://siteoforigin:,,,/Resources/instance.png";
            }
        }
    }

    public class KwFile : KwDirItem
    {
        public List<KwDirItem> DirItems { get; set; }

        public KwFile()
        {
            DirItems = new List<KwDirItem>();
        }
        public string Icon
        {
            get
            {
                return "pack://siteoforigin:,,,/Resources/file.png";
            }
        }
    }

    public class KwFolder : KwDirItem
    {
        public List<KwDirItem> DirItems { get; set; }
        public KwFolder()
        {
            DirItems = new List<KwDirItem>();
        }
        public  string Icon 
        {
            get
            {
                return "pack://siteoforigin:,,,/Resources/folder.png";
            }
        }
    }

    public class DlkKeywordTestsLoader : INotifyPropertyChanged
    {
        #region PUBLIC MEMBERS
        public List<KwDirItem> TestsLoaded = new List<KwDirItem>();
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region PRIVATE MEMBERS
        private static int mTotalFileCount = Directory.GetFiles(DlkEnvironment.mDirTests, "*.xml", SearchOption.AllDirectories).Count();
        private Boolean _StillLoading;
        private int mFileLoadProgress;
        /// <summary>
        /// 
        /// </summary>
        private string mCurrentItemProcessing;
        /// <summary>
        /// 
        /// </summary>
        private int mRunningFileCount = 0;
        private BackgroundWorker mBgw = new BackgroundWorker();
        private readonly object yale = new object();
        #endregion

        #region PROPERTIES
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
        #endregion
        
        #region PUBLIC METHODS

        /// <summary>
        /// Loading screen background worker
        /// </summary>
        public void Initialize()
        {
            TestsLoaded.Clear();
            //mTotalFileCount = 0;
            mTotalFileCount = Directory.GetFiles(DlkEnvironment.mDirTests, "*.xml", SearchOption.AllDirectories).Count();
            mRunningFileCount = 0;
            mBgw = new BackgroundWorker();
            mBgw.DoWork += new DoWorkEventHandler(GetKeywordDirectories);
            mBgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GetKeywordDirectoriesFilesCompleted);
            mBgw.RunWorkerAsync();
        }


        /// <summary>
        /// Recursive, kind of slow
        /// TO DO: speed up somehow
        /// </summary>
        /// <param name="root"></param>
        /// <param name="Filter"></param>
        /// <param name="FilteredItemContainer"></param>
        /// <param name="ExpandedDirectories"></param>
        /// <returns></returns>
        public List<KwDirItem> GetKeywordDirectories(String root,  List<KwDirItem> ExpandedDirectories = null)
        {
            try
            {
                
                List<KwDirItem> dirs = new List<KwDirItem>();
                DirectoryInfo di = new DirectoryInfo(root);
                List<DlkTag> allTags = DlkTag.LoadAllTags();

                var folders = di.EnumerateDirectories().ToList();

                foreach(var folder in folders)
                {
                    KwFolder dirItem = new KwFolder
                    {
                        Name = folder.Name,
                        Path = folder.FullName,
                        DirItems = GetKeywordDirectories(folder.FullName)//recursion slow?
                    };
                    dirs.Add(dirItem);
                }
                //this might help
                var files = di.EnumerateFiles()
                            .Where(s => s.Extension == ".xml").ToList();
                foreach (var file in files)
                {
                    if (file != null)
                    {
                        mRunningFileCount++; // increment file counter, important since this func was called recursively
                        FileLoadProgress = Convert.ToInt32(Convert.ToDouble((Convert.ToDouble(mRunningFileCount) / Convert.ToDouble(mTotalFileCount == 0 ? 1 : mTotalFileCount))) * 100);
                        CurrentItemProcessing = file.Name;
                        KwFile dirItem = new KwFile
                        {
                            Name = file.Name,
                            Path = file.FullName,
                            Author = GetTestAuthor(file.FullName),
                            Tags = GetTestTags(file.FullName, allTags),
                            DirItems = GetTestInstances(file.FullName)
                        };
                        dirs.Add(dirItem);
                    }
                }
                if (ExpandedDirectories != null)
                {
                    dirs = ExpandDirectories(dirs, ExpandedDirectories);
                }
                //store new list by passing dirs to the constructor
                SearchForTests._testsFolderDirectory = new List<KwDirItem>(dirs);
                //add the tests only
                SearchForTests._globalCollectionOfTests.AddRange(dirs);
                return dirs;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        /// <summary>
        /// Update Specific Directory. This works for file updates only.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="path"></param>
        /// <param name="expandedDirectories"></param>
        public void UpdateSpecificMemoryDirectory(String root, string path, List<KwDirItem> expandedDirectories = null)
        {
            string fileName = Path.GetFileName(path);
            string parentPath = Directory.GetParent(path).FullName;
            List<DlkTag> allTags = DlkTag.LoadAllTags();

            //get the directory level of the saved file
            var directory = SearchForTests._testsFolderDirectory;
            List<KwDirItem> directoryLevel = directory;
            var folderPath = parentPath.Trim('\\').Replace(root.Trim('\\'), string.Empty).Split('\\').Where(x => !string.IsNullOrEmpty(x));
            foreach (var folderName in folderPath)
            {
                var folder = directoryLevel.FirstOrDefault(x => x.Name == folderName);

                //check for inconsistencies - if this ever passes, break from method
                if (folder == null || folder.GetType() != typeof(KwFolder))
                    return;

                directoryLevel = ((KwFolder)folder).DirItems;
            }

            //get the updated/saved file
            var file = new DirectoryInfo(parentPath).EnumerateFiles().FirstOrDefault(s => s.Name == fileName);
            KwFile savedFile = new KwFile
            {
                Name = file.Name,
                Path = file.FullName,
                Author = GetTestAuthor(file.FullName),
                Tags = GetTestTags(file.FullName, allTags),
                DirItems = GetTestInstances(file.FullName)
            };

            //update our list with the newly saved file
            if (directoryLevel.Any(x => x.Name == fileName))
            {
                directoryLevel[directoryLevel.FindIndex(x => x.Name == fileName)] = savedFile;
                SearchForTests._globalCollectionOfTests[SearchForTests._globalCollectionOfTests.FindIndex(x => x.Path == savedFile.Path)] = savedFile;
            }
            else
            {
                int i = 0;
                while (i < directoryLevel.Count)
                {
                    var dir = directoryLevel[i];
                    if (dir.GetType() == typeof(KwFile) && dir.Name.CompareTo(savedFile.Name) == 1)
                    {
                        directoryLevel.Insert(i, savedFile);
                        break;
                    }
                    i++;
                }
                //item was not inserted - last item on list
                if (i == directoryLevel.Count)
                    directoryLevel.Add(savedFile);
                
                SearchForTests._globalCollectionOfTests.Add(savedFile);
            }

            //make sure expanded directories are updated
            if (expandedDirectories != null)
                directory = ExpandDirectories(directory, expandedDirectories);

            //refresh the list
            SearchForTests._testsFolderDirectory = new List<KwDirItem>(directory);
        }

        #endregion

        #region PRIVATE METHODS
        private void GetKeywordDirectories(object sender, DoWorkEventArgs e)
        {
            try
            {
                StillLoading = true;
                TestsLoaded = GetKeywordDirectories(DlkEnvironment.mDirTests);
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void GetKeywordDirectoriesFilesCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                StillLoading = false;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        /// <summary>
        /// List of items to expand - includes processing
        /// </summary>
        private List<KwDirItem> ExpandDirectories(List<KwDirItem> DirsToExpand, List<KwDirItem> ExpandedDirs)
        {
            for (int num = 0; num < ExpandedDirs.Count; num++ )
            {
                int index = -1;
                index = DirsToExpand.FindIndex(x => x.Path == ExpandedDirs[num].Path);
                if (index >= 0) //if item is found
                {
                    DirsToExpand[index].IsExpanded = ExpandedDirs[num].IsExpanded; //outside KwFolder condition for expanding KwFiles/Instances
                    if (ExpandedDirs[num] is KwFolder)
                    {
                        KwFolder Dirs = (KwFolder)DirsToExpand[index];
                        if (Dirs.DirItems.Count > 0)
                        {
                            Dirs.DirItems = ExpandDirectories(Dirs.DirItems, ExpandedDirs);
                            DirsToExpand[index] = Dirs;
                        }
                    }

                }

            }
            return DirsToExpand;
        }

        private List<KwDirItem> GetTestInstances(string FilePath)
        {
            List<KwDirItem> ret = new List<KwDirItem>();
            try
            {
                var counter = GetInstanceCount(FilePath);

                if (counter <= 1) 
                    return ret;

                for (int idx = 0; idx < counter; idx++)
                {
                    ret.Add(new KwInstance
                    {
                        Name = "Test : " + string.Format("{0:00000}", idx + 1),
                        Path = FilePath + "|" + (idx + 1).ToString()
                    });
                }
            }
            catch
            {
                // do nothing
            }
            return ret;
        }

        private int GetInstanceCount(String testFilePath)
        {
            // load the XML
            string trd = testFilePath.Replace(".xml", ".trd");

            if (!File.Exists(trd))
            {
                return 1;
            }


            var mXml = XDocument.Load(trd);

            // add test steps
            var datastep = from doc in mXml.Descendants("datarecord")
                       select new
                       {
                           dataval = doc.Elements("datavalue"),
                       };

            
            var datarec = datastep.FirstOrDefault();

            return datarec == null ? 0 : datarec.dataval.Count();
        }

        private static String GetTestAuthor(string testFilePath)
        {
            string author = "";
            try
            {
                // load the XML
                XDocument mXml = XDocument.Load(testFilePath);
                List<XElement> elements = mXml.Descendants().ToList();

                if (elements.Any(x => x.Name.ToString() == "author"))
                {
                    return mXml.Element("test").Element("author").Value;
                }
            }
            catch (Exception)
            {
                author = "";
            }

            return author;
        }

        private List<DlkTag> GetTestTags(String testFilePath, List<DlkTag> allTags)
        {
            List<DlkTag> ret = new List<DlkTag>();
            try
            {
                // load the XML
                XDocument mXml = XDocument.Load(testFilePath);
                var tags = from doc in mXml.Descendants("tag")
                           select new
                           {
                               id = doc.Attribute("id").Value,
                               name = doc.Attribute("name") != null ? doc.Attribute("name").Value : "",
                           };
                foreach (var val in tags)
                {
                    if (allTags.Any(x => x.Id == val.id))
                    {
                        ret.Add(new DlkTag(val.id, allTags.Find(x => x.Id == val.id).Name, "", ""));
                    }
                }
            }
            catch
            {
                //do nothing
            }
            return ret;
        }

        #endregion

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    /// <summary>
    /// Static class containing the list of all tests that can be filtered
    /// </summary>
    public static class SearchForTests
    {
        //list to return when filter is cleared: (this list contains the whole structure of the Tests folder)
        public static List<KwDirItem> _testsFolderDirectory = new List<KwDirItem>();
        //list to query against: (this list contains only the tests, which you will query from)
        public static List<KwDirItem> _globalCollectionOfTests = new List<KwDirItem>();
        //list containing the filtered results using the filter string: (this list contains the results after querying _globalCollectionOfTests)
        public static List<KwDirItem> filteredTestsResults = new List<KwDirItem>();
        //previous filter string (for backspace purposes)
        public static string previousFilterString = "";

        /// <summary>
        /// Filters tests in tree
        /// </summary>
        /// <param name="type">Search type</param>
        /// <param name="filterString">Text to search</param>
        public static List<KwDirItem> FilterTests(KwDirItem.SearchType type, String filterString)
        {
            try
            {
                    List<DlkTag> allTags = DlkTag.LoadAllTags();
                    filterString = filterString.ToLower();
                    var previousList = filteredTestsResults;
                    //if first time to filter (example: if first letter was typed) , filter the entire collection of xml files that 
                    //satisfies filter
                    if (filteredTestsResults.Count == 0)
                    {
                    filteredTestsResults = FilterTreeItems(type, filterString, _globalCollectionOfTests, allTags);
                    }
                    //if not first time to filter
                    else if (filteredTestsResults.Count > 0 && !string.IsNullOrEmpty(filterString))
                    {
                        /*
                         * backspace handler
                         */

                        //handler for backspace
                        if (previousFilterString.Contains(filterString) && previousFilterString.Length > filterString.Length)
                        {
                        //example in cp711: if you typed: "aop" then -> backspace was pressed = ao, ao will contain a bigger list due to ao having more hits than aop 
                        filteredTestsResults = FilterTreeItems(type, filterString, _globalCollectionOfTests, allTags);
                        //if same results
                        if (filteredTestsResults.Count == previousList.Count)
                            {
                                //just return the previous list
                                filteredTestsResults = previousList;
                            }
                        }
                        //handler for typing
                        else if (filterString.Length > previousFilterString.Length)
                        {
                        filteredTestsResults = FilterTreeItems(type, filterString, _globalCollectionOfTests, allTags);
                        //if user typed at the beginning of string
                        if (filterString.EndsWith(previousFilterString))
                            {
                            filteredTestsResults = FilterTreeItems(type, filterString, _globalCollectionOfTests, allTags);
                        }
                        }
                        //if user did not backspace at both ends of filter string, or not typed 2nd letter onwards
                        else
                        {
                        filteredTestsResults = FilterTreeItems(type, filterString, _globalCollectionOfTests, allTags);
                    }
                        previousFilterString = filterString;
                    }
                    else if (string.IsNullOrEmpty(filterString))
                    {
                        //clear filtered test results here if no filter string.
                        //this will return a list containing 0 items, making the treeView.datacontext use _testsFolderDirectory
                        filteredTestsResults.Clear();
                        previousFilterString = "";
                    }
                
            }
            catch
            {
                //ignore
            }
            return filteredTestsResults.OrderBy(item => item.Name).ToList();//ignore deferred execution of IEnumerable and return a list
        }

        /// <summary>
        /// Changes search results based on search type
        /// </summary>
        /// <param name="type">Search type</param>
        /// <param name="filterString">Text to search</param>
        /// <param name="globalTestList">Global list of tests</param>
        /// <param name="globalTagList">Global list of tags</param>
        private static List<KwDirItem> FilterTreeItems(KwDirItem.SearchType type, string filterString, List<KwDirItem> globalTestList, List<DlkTag> globalTagList)
        {
            List<KwDirItem> mFilteredItems = new List<KwDirItem>();
            switch (type)
            {
                case KwDirItem.SearchType.TestsSuitesOnly:
                    mFilteredItems = globalTestList.FindAll(s => s.Name.ToLower().Contains(filterString)).AsParallel().ToList();
                    break;
                case KwDirItem.SearchType.TagsOnly:
                    mFilteredItems = globalTestList.FindAll(s => s.Tags.Any(x => x.Name.ToLower().Contains(filterString) && globalTagList.Any(y => y.Name == x.Name)));
                    break;
                case KwDirItem.SearchType.TestsSuitesAndTags:
                    mFilteredItems = globalTestList.FindAll(s => s.Tags.Any(x => x.Name.ToLower().Contains(filterString) && globalTagList.Any(y => y.Name == x.Name)) || s.Name.ToLower().Contains(filterString)).AsParallel().ToList();
                    break;
                case KwDirItem.SearchType.Author:
                    mFilteredItems = globalTestList.FindAll(s => s.Author.ToLower().Contains(filterString)).AsParallel().ToList();
                    break;
                default:
                    break;
            }
            return mFilteredItems;
        }
    }

    /// <summary>
    /// Static class containing the list of all suites that can be filtered
    /// </summary>
    public static class SearchForSuites
    {
        public static List<KwDirItem> GlobalCollectionOfSuites = new List<KwDirItem>();
        public static List<KwDirItem> GlobalCollectionOfFavorites = new List<KwDirItem>();
    }
}
