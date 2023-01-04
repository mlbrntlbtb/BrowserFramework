using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TestRunner.Common
{
    public class DlkOSRecorderData : INotifyPropertyChanged
    {
        private String _Screen;
        public String Screen
        {
            get { return _Screen; }
            set
            {
                if (value == _Screen)
                    return;
                _Screen = value;
                OnPropertyChanged("Screen");
            }
        }

        private String _CSSPath;
        public String CSSPath
        {
            get { return _CSSPath; }
            set
            {
                if (value == _CSSPath)
                    return;
                _CSSPath = value;
                OnPropertyChanged("CSSPath");
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


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DlkOSRecorderData()
        {
            Screen = "";
            CSSPath = "";
        }

        public DlkOSRecorderData(String Screen, String CSSPath)
        {
            this.Screen = Screen;
            this.CSSPath = CSSPath;
        }
    }
}
