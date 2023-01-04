using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Xml;
using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using TestRunner.Common;


namespace TestRunner
{
    /// <summary>
    /// Interaction logic for NewFeaturesDialog.xaml
    /// </summary>
    public partial class NewFeaturesDialog : Window
    {
        List<String> mNewItems = new List<String>();
        List<String> mNewItemsDesc = new List<String>();
        string mFinalList = "";
        string mVersion = "";

        public NewFeaturesDialog(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
            Initialize();
        }

        private void Initialize()
        {
            ReadXML();
            SetVersion();
            FormatList();
        }

        private void SetVersion()
        {
            try
            {
                About abt = new About();
                mVersion = abt.AssemblyVersion;
                //lblVersion.Content = lblVersion.Content + mVersion;
                lblVersion.Content = abt.ProductName + " - v" + abt.AssemblyVersion;
            }
            catch
            {
                //do nothing
            }
        }

        private void ReadXML()
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();

                FileStream fs = new FileStream(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ReleaseNews.xml", FileMode.Open, FileAccess.Read);
                xmldoc.Load(fs);
                var xmlNodeList = xmldoc.SelectNodes("/testrunner/feature");
                foreach (XmlNode node in xmlNodeList)
                {
                    mNewItems.Add(node.Attributes["name"].Value);
                    mNewItemsDesc.Add(node.Attributes["description"].Value);
                }
            }
            catch
            {
                //do nothing
            }
        }

        private void FormatList()
        {
            int ctr = 1;

            try
            {
                foreach (string item in mNewItems)
                {
                    int idx = mNewItems.IndexOf(item);

                    if (ctr == 1)
                    {
                        mFinalList = mFinalList + "\u25C9 " + item + "\n" + "    " + "- " + mNewItemsDesc[idx] + "\n";
                        ctr = ctr + 1;
                    }
                    else
                    {
                        mFinalList = mFinalList + Environment.NewLine + "\u25C9 " + item + "\n" + "    " + "- " + mNewItemsDesc[idx] + "\n";
                        ctr = ctr + 1;
                    }
                }
                txtItems.Text = mFinalList;
            }
            catch
            {
                //do nothing
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dir = Directory.GetParent(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName;
                string fileName = System.IO.Path.Combine(dir, "CHANGELOG.txt");
                DlkProcess.RunProcess(fileName, "", dir, false, -1);
            }
            catch
            {
                // swallow
            }
        }
    }
}
