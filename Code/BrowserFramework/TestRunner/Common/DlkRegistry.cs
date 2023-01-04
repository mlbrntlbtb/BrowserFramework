using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace TestRunner.Common
{
    public static class DlkRegistry
    {
        #region CONST
        private const string REGKEY_SETTINGS = @"Software\Deltek\SeleniumExcelAddIn\Settings\";
        private const string REGKEY_PRODUCTS = @"Software\Deltek\SeleniumExcelAddIn\Products\";
        public const string REGVAL_AUTOROOTFOLDER = "CfgAutomationRootFolder";
        public const string REGVAL_TESTSCRIPTROOT = "TestScriptRoot";
        public const string REGVAL_DATAROOT = "DataRoot";
        public const string REGVAL_FRAMEWORKRELROOT = "FrameworkRelativeRoot";
        public const string REGVAL_PATH = "Path";
        public const string REGVAL_NAME = "Name";
        public const string REGVAL_TESTSCRIPT_TFSPATH = "TestScriptTFSPath";
        public const string REGVAL_DATA_TFSPATH = "DataTFSPath";
        public const string REGVAL_SUPPORTEDBROWSERS = "SupportedBrowsers";

        public const string FLDR_REFERENCE = @"Reference\";
        public const string FLDR_FRAMEWORK = @"Framework\";
        public const string FLDR_LIBRARY = @"Library\";

        public const string FILE_REF_USER = "User.xlsx";
        //public const string FILE_REF_ENVIRONMENT = "Environment.xlsx";
        public const string FILE_PRODUCT_RAW = "Products.ini";
        public const string FILE_PRODUCT_TRANSFORMED = "Products.xml";
        public const string FILE_LIB_COMPONENTS_RAW = "Components.ini";
        public const string FILE_LIB_COMPONENTS_TRANSFORMED = "Components.xml";
        public const string FILE_LIB_KEYWORDS_RAW = "KeywordLists.ini";
        public const string FILE_LIB_KEYWORDS_TRANSFORMED = "KeywordLists.xml";
        #endregion

        private static string mstrAutomationRootFolder = string.Empty;
        private static string mstrDataRoot = string.Empty;
        private static string mstrTestScriptRoot = string.Empty;
        //private static List<DlkProduct> mlstProducts = null;
        private static RegistryKey mregSettings = null;
        //private static List<XmlProduct> mXmlProducts = null;
        private static RegistryKey mregProducts = null;

    }
}
