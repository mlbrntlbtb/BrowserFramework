using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TestRunner.Common
{
    public class DlkStartBrowserData : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DlkStartBrowserData(String DefaultEnvID)
        {
            EnvID = DefaultEnvID;
        }

    }
}
