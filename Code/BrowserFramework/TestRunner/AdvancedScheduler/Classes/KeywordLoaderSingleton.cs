using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Common;

namespace TestRunner.AdvancedScheduler.Classes
{
    public class KeywordLoaderSingleton
    {
        private static KeywordLoaderSingleton instance;

        private KeywordLoaderSingleton() { }
        public static KeywordLoaderSingleton Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new KeywordLoaderSingleton();
                }
                return instance;
            }
        }

        private Object LoadKeywordLock = new Object();
        private string testDirPath = string.Empty;
        private List<KwDirItem> keywordDirectories = null;

        public List<KwDirItem> GetKeywordDirectories(string testDir, List<KwDirItem> expandedDir)
        {
            lock (LoadKeywordLock)
            {
                if (keywordDirectories == null)
                {
                    keywordDirectories = new DlkKeywordTestsLoader().GetKeywordDirectories(testDir, expandedDir);
                    testDirPath = testDir;
                }
            }

            return keywordDirectories;
        }

        public string GetTestDirPath()
        {
            return testDirPath;
        }

        public bool IsKeywordDirectoriesNull()
        {
            return (keywordDirectories == null);
        }

        public void SetKeywordDirectories(List<KwDirItem> newKeywordDirectories)
        {
            keywordDirectories = newKeywordDirectories;
        }
    }
}
