using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.DlkUtility;
using CommonLib.DlkRecords;
using NavigatorLib.DlkSystem;
using ngCRMLib.System;

namespace TestExecute
{
    /// <summary>
    /// This TestExecute console application provides a simple way to execute a test without using any of our other tools (e.g. TestRunner, etc) 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            String mProduct = "", mRelativeTestPath = "", mLoginConfig = "", mBrowser = "", mTestInstance = "", mResultsFile = "", mKeepBrowserOpen = "";

            mProduct = DlkArgs.GetArg("-product");           // e.g. "Navigator"
            mLoginConfig = DlkArgs.GetArg("-loginconfig");   // each product will have a Framework/Configs/LoginConfig.xml containing login information; use "skip" to "skip"
            mRelativeTestPath = DlkArgs.GetArg("-test");     // e.g. RelativeFolderPath/TestName.xml --> UnitTests/Controls/Button/ButtonTest1.xml
            mTestInstance = DlkArgs.GetArg("-testinstance"); // e.g. 1 --> all tests are data driven; the instance tells us which version of the test to execute
            mBrowser = DlkArgs.GetArg("-browser");           // e.g. one of these: internetexplorer, firefox, chrome
            mKeepBrowserOpen = DlkArgs.GetArg("-keepbrowseropen"); 

            /// Based on the above info, we select a libary to execute the tests
            /// For example; NavigatorLib will execute Navigator tests. We'd create new libs as needed to support other products
            /// This provides us with easy code sharing, inheritance of common objects, standard results as well as custom development
            /// This does mean folks need to be attentive to situations where they have the main solution checked out

            /// Note: the output of this program is a results file. Regardless of error, we should always create a results file.
            switch (mProduct.ToLower())
            {
                case "ngcrm":
                    DlkNgCRMTestExecute mNgCRMExecute = new DlkNgCRMTestExecute(mProduct, mLoginConfig, mRelativeTestPath, mTestInstance, mBrowser, mKeepBrowserOpen);
                    mResultsFile = mNgCRMExecute.ExecuteTest();
                    break;
                case "navigator":
                    DlkNavigatorTestExecute mNavExecute = new DlkNavigatorTestExecute(mProduct, mLoginConfig, mRelativeTestPath, mTestInstance, mBrowser, mKeepBrowserOpen);
                    mResultsFile = mNavExecute.ExecuteTest();
                    break;
                default:
                    throw new Exception("Unsupported product: " + mProduct); // not going to spend time on this... likely never happen as we will control via front end;
            }
            System.Console.WriteLine(mResultsFile);
        }
    }
}