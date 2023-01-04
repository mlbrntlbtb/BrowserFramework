using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using CommonLib.DlkHandlers;
using TestRunner.Common;
using CommonLib.DlkSystem;
using System;

namespace TestRunner
{
    /// <summary>
    /// Interaction logic for AddHost.xaml
    /// </summary>
    public partial class AddHost : Window
    {
        private List<Host> mHosts;
        public AddHost()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            mHosts = GetHostsFromFile();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Validate())
                {
                    if (!IsHostExist(txtName.Text))
                    {
                        Add();
                        SaveHostsToFile(mHosts);
                    }
                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    DlkUserMessages.ShowError(DlkUserMessages.ERR_MACHINENAME_INVALID);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private bool IsHostExist(string HostName)
        {
            bool ret = false;
            foreach (Host hst in mHosts)
            {
                if (hst.Name == HostName)
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }

        private bool Validate()
        {
            return !string.IsNullOrEmpty(txtName.Text);
        }

        private void Add()
        {
            mHosts.Add(new Host(txtName.Text, HostType.NETWORK, "placeholder"));
        }

        private List<Host> GetHostsFromFile()
        {
            if (!System.IO.File.Exists(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "Hosts.xml")))
            {
                SaveHostsToFile(new List<Host>());
            }
            List<Host> ret = new List<Host>();

            //mTestSuiteRecs = new List<DlkExecutionQueueRecord>();
            XDocument DlkXml = XDocument.Load(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "Hosts.xml"));

            var data = from doc in DlkXml.Descendants("host")
                       select new
                       {
                           name = doc.Attribute("name").Value,
                           type = doc.Attribute("type").Value == "local" ? HostType.LOCAL : HostType.NETWORK,
                           status = doc.Attribute("status").Value,
                       };
            foreach (var val in data)
            {
                Host hst = new Host(val.name, val.type, val.status);
                ret.Add(hst);
            }

            return ret;
        }

        private void SaveHostsToFile(List<Host> Hosts)
        {
             List<XElement> hosts = new List<XElement>();
             foreach (Host hst in Hosts)
                {
                   hosts.Add(new XElement("host",
                      new XAttribute("name", hst.Name),
                      new XAttribute("type", hst.Type == HostType.LOCAL ? "local" : "network"),
                      new XAttribute("status", hst.Status))
                      );
                }

             XElement hostsRoot = new XElement("hosts", hosts);
             XDocument xDoc = new XDocument(hostsRoot);
             xDoc.Save(System.IO.Path.Combine(DlkConfigHandler.GetConfigRoot("Scheduler"), "Hosts.xml"));
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
                this.Close();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }


    }
}
