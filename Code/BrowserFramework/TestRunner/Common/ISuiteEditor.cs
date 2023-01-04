using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TestRunner.Recorder;

namespace TestRunner.Common
{
    public interface ISuiteEditor
    {
        string TestRoot { get; }
        ListCollectionView AllBrowsers { get; }
        ObservableCollection<string> EnvironmentIDs { get; }
        TestCapture TestCaptureForm { get; }
        void RefreshInMemoryTestTree(string testPath);
        void RefreshTestQueue(string editedTestPath);
    }
}
