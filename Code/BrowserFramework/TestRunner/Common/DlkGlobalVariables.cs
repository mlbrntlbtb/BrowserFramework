using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Common
{
    public static class DlkGlobalVariables
    {
        public static string mOldTestDirPath;
        public static string mOldSuiteDirPath;

        public static bool mIsTestDirChanged = false;
        public static bool mIsSuiteDirChanged = false;
        public static bool mIsApplicationChanged = false;
    }
}
