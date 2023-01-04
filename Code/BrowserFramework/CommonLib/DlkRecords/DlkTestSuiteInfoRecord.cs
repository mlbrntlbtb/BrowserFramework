using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace CommonLib.DlkRecords
{
    public class DlkTestSuiteInfoRecord : INotifyPropertyChanged
    {
        private String _Browser;
        public String Browser
        {
            get { return _Browser; }
            set
            {
                if (value == _Browser)
                    return;
                _Browser = value;
                OnPropertyChanged("Browser");
            }
        }
        private String _EnvID;
        public String EnvID 
        {
            get { return _EnvID; }
            set
            {
                if (value == _EnvID)
                    return;
                _EnvID = value;
                OnPropertyChanged("EnvID");
            }
        }
        private String _Language;
        public String Language 
        {
            get { return _Language; }
            set
            {
                if (value == _Language)
                    return;
                _Language = value;
                OnPropertyChanged("Language");
            }
        }
        private List<DlkTag> _Tags = new List<DlkTag>();
        public List<DlkTag> Tags 
        {
            get { return _Tags; }
            set
            {
                _Tags = value;
            }
        }

        private String _Email;
        public String Email
        {
            get { return _Email; }
            set
            {
                if (value == _Email)
                    return;
                _Email = value;
                OnPropertyChanged("Email");
            }
        }

        private String _Description;
        public String Description
        {
            get { return _Description; }
            set
            {
                if (value == _Description)
                    return;
                _Description = value;
                OnPropertyChanged("Description");
            }
        }

        private String _Owner;
        public String Owner
        {
            get { return _Owner; }
            set
            {
                if (value == _Owner)
                    return;
                _Owner = value;
                OnPropertyChanged("Owner");
            }
        }

        private List<DlkSuiteLinkRecord> _links = new List<DlkSuiteLinkRecord>();
        public List<DlkSuiteLinkRecord> mLinks
        {
            get
            {
                return _links;
            }
            set 
            {
                _links = value;
            }
        }

        private String _GlobalVar;
        public String GlobalVar
        {
            get
            {
                if (_GlobalVar == null)
                {
                    _GlobalVar = String.Empty;
                }
                return _GlobalVar;
            }
            set
            {
                _GlobalVar = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DlkTestSuiteInfoRecord(String sBrowser, String sEnvID, String sLanguage)
        {
            Browser = sBrowser;
            EnvID = sEnvID;
            Language = sLanguage;
        }

        public DlkTestSuiteInfoRecord(String sBrowser, String sEnvID, String sLanguage, String sEmail)
        {
            Browser = sBrowser;
            EnvID = sEnvID;
            Language = sLanguage;
            Email = sEmail;
        }

        public DlkTestSuiteInfoRecord(String sBrowser, String sEnvID, String sLanguage, List<DlkTag> sTags, String sEmail, String sDescription, List<DlkSuiteLinkRecord> sLinks, String sOwner, String sGlobalVar)
        {
            Browser = sBrowser;
            EnvID = sEnvID;
            Language = sLanguage;
            Tags = sTags;
            Email = sEmail;
            Description = sDescription;
            mLinks = sLinks;
            Owner = sOwner;
            GlobalVar = sGlobalVar;
        }

        public DlkTestSuiteInfoRecord()
        {
            Browser = "Firefox";
            EnvID = "default";
            Language = "EN";
            Email = "";
            Description = "";
        }

        public void Update(String sBrowser, String sEnvID, String sLanguage)
        {
            Browser = sBrowser;
            EnvID = sEnvID;
            Language = sLanguage;
        }

        public void Update(String sBrowser, String sEnvID, String sLanguage, String sEmail)
        {
            Browser = sBrowser;
            EnvID = sEnvID;
            Language = sLanguage;
            Email = sEmail;
        }

        public void Update(String sBrowser, String sEnvID, String sLanguage, List<DlkTag> sTags, String sEmail, String sDescription, List<DlkSuiteLinkRecord> sLinks, String sOwner, String sGlobalVar)
        {
            Browser = sBrowser;
            EnvID = sEnvID;
            Language = sLanguage;
            Tags = sTags;
            Email = sEmail;
            Description = sDescription;
            mLinks = sLinks;
            Owner = sOwner;
            GlobalVar = sGlobalVar;
        }
    }
}
