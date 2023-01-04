using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using CommonLib.DlkSystem;
using CommonLib.DlkRecords;
using CommonLib.DlkHandlers;

namespace TestRunner.Common
{
    public class VM : INotifyPropertyChanged
    {
        private String _Name;
        public String Name
        {
            get { return _Name; }
            set 
            {
                if (value == _Name)
                    return;
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        private String _DataRoot;
        public String DataRoot
        {
            get { return _DataRoot; }
            set 
            {
                if (value == _DataRoot)
                    return;
                _DataRoot = value;
                OnPropertyChanged("DataRoot");
            }
        }

        public VM(String Name, String DataRoot)
        {
            this.Name = Name;
            this.DataRoot = DataRoot;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public static class DlkVMs
    {
        private static ObservableCollection<VM> _VMs;
        public static ObservableCollection<VM> mVMs
        {
            get
            {
                if (_VMs == null)
                {
                    Initialize();
                }
                return _VMs;
            }
        }

        private static void Initialize()
        {
            _VMs = new ObservableCollection<VM>();
            String mFile = DlkEnvironment.DirReference + @"VMs.xml";
            if (File.Exists(mFile))
            {
                XDocument DlkXml = XDocument.Load(mFile);

                var data = from doc in DlkXml.Descendants("vm")
                           select new
                           {
                               vmname = doc.Attribute("name").Value,
                               dataroot = doc.Attribute("data") != null ? doc.Attribute("data").Value : ""
                           };

                foreach (var val in data)
                {
                    _VMs.Add(new VM(val.vmname, val.dataroot));
                }
            }
        }

        private static void SaveVMs()
        {
            List<XElement> vms = new List<XElement>();

            for (int i = 0; i < _VMs.Count; i++)
            {
                vms.Add(new XElement("vm",
                    new XAttribute("name", _VMs[i].Name),
                    new XAttribute("data", _VMs[i].DataRoot)
                    )
                    );
            }

            XElement root = new XElement("vms", vms);
            XDocument xDoc = new XDocument(root);
            xDoc.Save(DlkEnvironment.DirReference + @"VMs.xml");

        }

        public static void AddEditVM(String Name, String DataRoot)
        {
            Boolean bFound = false;
            for (int i = 0; i < _VMs.Count; i++)
            {
                if (_VMs[i].Name == Name)
                {
                    _VMs[i].DataRoot = DataRoot.Last() == '\\'?DataRoot.Substring(0, DataRoot.Length - 1):DataRoot;
                    bFound = true;
                    break;
                }
            }
            if (!bFound)
            {
                _VMs.Add(new VM(Name, DataRoot));
            }

            SaveVMs();
        }

        public static void DeleteVM(String Name)
        {
            foreach(VM aVM in _VMs)
            {
                if (aVM.Name == Name)
                {
                    _VMs.Remove(aVM);
                    break;
                }
            }
            SaveVMs();
        }

    }
}
