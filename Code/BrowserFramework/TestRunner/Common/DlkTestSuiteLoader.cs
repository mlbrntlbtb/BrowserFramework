using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CommonLib.DlkHandlers;


namespace TestRunner.Common
{
    //public class KwDirItem
    //{
    //    public String Name { get; set; }
    //    public String Path { get; set; }
    //}

    //public class KwInstance : KwDirItem
    //{
    //    public string Icon
    //    {
    //        get
    //        {
    //            return "pack://siteoforigin:,,,/Resources/instance.png";
    //        }
    //    }
    //}

    //public class KwFile : KwDirItem
    //{
    //    public List<KwDirItem> DirItems { get; set; }

    //    public KwFile()
    //    {
    //        DirItems = new List<KwDirItem>();
    //    }
    //    public string Icon
    //    {
    //        get
    //        {
    //            return "pack://siteoforigin:,,,/Resources/file.png";
    //        }
    //    }
    //}

    //public class KwFolder : KwDirItem
    //{
    //    public List<KwDirItem> DirItems { get; set; }
    //    public KwFolder()
    //    {
    //        DirItems = new List<KwDirItem>();
    //    }
    //    public  string Icon 
    //    {
    //        get
    //        {
    //            return "pack://siteoforigin:,,,/Resources/folder.png";
    //        }
    //    }
    //}

    public class DlkTestSuiteLoader
    {
        public List<KwDirItem> GetSuiteDirectories(String root, string filePath = "")
        {
            List<KwDirItem> dirs = new List<KwDirItem>();
            DirectoryInfo di = new DirectoryInfo(root);

            //if (includeRoot)
            //{
            //    SuiteFolder rootFolder = new SuiteFolder
            //    {
            //        Name = di.Name,
            //        Path = di.FullName,
            //        DirItems = GetSuiteDirectories(di.FullName, false)
            //    };
            //    dirs.Add(rootFolder);
            //}

            foreach (DirectoryInfo folder in di.GetDirectories())
            {
                BFFolder dirItem = new BFFolder
                {
                    Name = folder.Name,
                    Path = folder.FullName,
                    DirItems = GetSuiteDirectories(folder.FullName, filePath),
                    IsSelected = string.IsNullOrEmpty(filePath) ? false : (folder.FullName == Path.GetDirectoryName(filePath)),
                    IsExpanded = string.IsNullOrEmpty(filePath) ? false : filePath.Contains(folder.FullName)
                };
                dirs.Add(dirItem);
            }
            //foreach (FileInfo file in di.GetFiles())
            //{
            //    if (file.Extension.ToLower() == ".xml")
            //    {
            //        KwFile dirItem = new KwFile
            //        {
            //            Name = file.Name,
            //            Path = file.FullName,
            //            DirItems = GetTestInstances(file.FullName)
            //        };
            //        dirs.Add(dirItem);
            //    }
            //}

            return dirs;
        }

        public List<KwDirItem> GetSuiteDirectories2(String root, String Filter = "", List<KwDirItem> FilteredItemContainer = null)
        {
            List<KwDirItem> dirs = new List<KwDirItem>();
            DirectoryInfo di = new DirectoryInfo(root);
            foreach (DirectoryInfo folder in di.GetDirectories())
            {
                BFFolder dirItem = new BFFolder
                {
                    Name = folder.Name,
                    Path = folder.FullName,
                    DirItems = GetSuiteDirectories2(folder.FullName, Filter, FilteredItemContainer),
                };
                if (string.IsNullOrEmpty(Filter))
                {
                    dirs.Add(dirItem);
                }
            }
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Extension.ToLower() == ".xml")
                {
                    BFFile dirItem = new BFFile
                    {
                        Name = file.Name,
                        Path = file.FullName,
                        //DirItems = GetTestInstances(file.FullName)
                    };
                    if (string.IsNullOrEmpty(Filter))
                    {
                        dirs.Add(dirItem);
                        SearchForSuites.GlobalCollectionOfSuites.Add(dirItem);
                    }
                    else if (FilteredItemContainer != null)
                    {
                        if (dirItem.Name.ToLower().Contains(Filter.ToLower()))
                        {
                            FilteredItemContainer.Add(dirItem);
                        }
                    }
                    //dirs.Add(dirItem);
                }
            }

            if (!string.IsNullOrEmpty(Filter) && FilteredItemContainer != null)
            {
                dirs = FilteredItemContainer;
            }
            return dirs;
        }
        //public List<BFFolder> GetTestInstances(string FilePath)
        //{
        //    List<BFFolder> ret = new List<BFFolder>();

        //    DlkTest test = new DlkTest(FilePath);

        //    for (int idx = 0; idx < test.mInstanceCount; idx++)
        //    {
        //        BFInstance instance = new BFInstance
        //        {
        //            Name = "Test : " + string.Format("{0:00000}", idx + 1),
        //            Path = FilePath + "|" + (idx + 1).ToString()
        //        };
        //        ret.Add(instance);
        //    }
        //    if (ret.Count == 1)
        //    {
        //        ret.Clear();
        //    }
        //    return ret;
        //}
    }

    public class BFFolder : KwDirItem
    {
        public List<KwDirItem> DirItems { get; set; }
        public BFFolder()
        {
            DirItems = new List<KwDirItem>();
        }
        public string Icon
        {
            get
            {
                return "pack://siteoforigin:,,,/Resources/folder.png";
            }
        }
    }

    public class BFFile : KwDirItem
    {
        public List<BFFile> DirItems { get; set; }

        public BFFile()
        {
            DirItems = new List<BFFile>();
        }
        public string Icon
        {
            get
            {
                return "pack://siteoforigin:,,,/Resources/text.png";
            }
        }
    }

    public class BFInstance : KwDirItem
    {
        public string Icon
        {
            get
            {
                return "pack://siteoforigin:,,,/Resources/instance.png";
            }
        }
    }
}
