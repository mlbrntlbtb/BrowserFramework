using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestRunner.Common;
using CommonLib.DlkSystem;
using CommonLib.DlkHandlers;
using CommonLib.DlkRecords;
using TestRunner.Common;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for ImportResultsDialog.xaml
    /// </summary>
    public partial class ImportResultsDialog : Window
    {
        public ImportResultsDialog()
        {
            InitializeComponent();
            lboxVMs.DataContext = DlkVMs.mVMs;
        }

        BackgroundWorker bwImp;
        List<VM> mSelectedVMs;
        private void btnImportResults_Click(object sender, RoutedEventArgs e)
        {
            mSelectedVMs = null;

            // check if we have tests to run
            if (lboxVMs.SelectedItems.Count < 1)
            {
                DlkUserMessages.ShowError(DlkUserMessages.ERR_NO_VM_SELECTED);
                return;
            }

            mSelectedVMs = new List<VM>();
            for (int i = 0; i < lboxVMs.SelectedItems.Count; i++)
            {
                mSelectedVMs.Add((VM)lboxVMs.SelectedItems[i]);
            }

            // disable buttons
            lboxVMs.IsEnabled = false;
            btnImportResults.IsEnabled = false;

            // show progress bar
            lblImportStatus.Content = "Importing results...";
            lblImportStatus.Visibility = System.Windows.Visibility.Visible;
            pbImport.Visibility = System.Windows.Visibility.Visible;
            UpdateProgressBar(1, 0);


            // do work
            bwImp = new BackgroundWorker();
            bwImp.DoWork += new DoWorkEventHandler(bwImp_DoWork);
            bwImp.ProgressChanged += new ProgressChangedEventHandler(bwImp_ProgressChanged);
            bwImp.WorkerReportsProgress = true;
            bwImp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwImp_RunWorkerCompleted);

            bwImp.RunWorkerAsync();

        }

        private void bwImp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lboxVMs.IsEnabled = true;
            btnImportResults.IsEnabled = true;
            lblImportStatus.Content = "Results imported successfully.";
        }

        private void bwImp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int iCreep = 10;
            if (e.ProgressPercentage > 95)
            {
                iCreep = 2;
            }
            UpdateProgressBar(iCreep, e.ProgressPercentage);
        }

        private void bwImp_DoWork(object sender, DoWorkEventArgs e)
        {

            // Report progress... just to get the meter running :)
            bwImp.ReportProgress(10);

            for (int i = 0; i < mSelectedVMs.Count; i++)
            {

                String mVmInProgress = mSelectedVMs[i].Name;

                // construct path for source
                //String mSource = @"\\" + mVmInProgress + @"\c$" + DlkEnvironment.DirResultsArchive.Substring(2);
                String mSource = mSelectedVMs[i].DataRoot;

                if (File.Exists(mSource + @"\ResultsManifest.xml"))
                {
                    // construct path for target
                    String mTarget = DlkEnvironment.DirResultsArchive;

                    // copy the directories
                    foreach (DirectoryInfo di in new DirectoryInfo(mSource).GetDirectories())
                    {
                        String mDest = mTarget + di.Name;
                        if (Directory.Exists(mDest))
                        {
                            DlkEnvironment.EmptyFolder(mDest);
                        }
                        else
                        {
                            Directory.CreateDirectory(mDest);
                        }

                        // copy the root files
                        foreach (FileInfo fi in di.GetFiles())
                        {
                            fi.CopyTo(mDest + @"\" + fi.Name);
                        }

                        // copy child dirs
                        String mSourceChild = mSource + @"\" + di.Name;
                        foreach (DirectoryInfo diChild in new DirectoryInfo(mSourceChild).GetDirectories())
                        {
                            String mDestChild = mDest + @"\" + diChild.Name;
                            if (Directory.Exists(mDestChild))
                            {
                                DlkEnvironment.EmptyFolder(mDestChild);
                            }
                            else
                            {
                                Directory.CreateDirectory(mDestChild);
                            }

                            // copy the root files
                            foreach (FileInfo fi in diChild.GetFiles())
                            {
                                fi.CopyTo(mDestChild + @"\" + fi.Name);
                            }
                        }
                    }

                    // update the 
                    DlkTestSuiteResultsFileHandler.MergeImportedResults(mSource + @"\ResultsManifest.xml");
                }

                

                // Report progress
                double iPercentComplete = ((double)(i + 1) / (double)mSelectedVMs.Count) * 100;
                int iToReport = Convert.ToInt32(iPercentComplete);
                if (iToReport > 10) // we report 10% at the beginning
                {
                    bwImp.ReportProgress(iToReport);
                }

            }
        }

        private void UpdateProgressBar(int iDurationSecondsToPerformAnimation, int iPercentToMoveTo)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(iDurationSecondsToPerformAnimation));
            DoubleAnimation doubleanimation = new DoubleAnimation(iPercentToMoveTo, duration);
            pbImport.Value = iPercentToMoveTo;
            pbImport.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        }

    }
}
