using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TestRunner.Common
{
    public class DlkAddControlData : INotifyPropertyChanged
    {
        private String _ControlType;
        public String ControlType
        {
            get { return _ControlType; }
            set
            {
                if (value == _ControlType)
                    return;
                _ControlType = value;
                OnPropertyChanged("ControlType");
            }
        }

        private String _ControlName;
        public String ControlName
        {
            get { return _ControlName; }
            set
            {
                if (value == _ControlName)
                    return;
                _ControlName = value;
                OnPropertyChanged("ControlName");
            }
        }

        private String _SearchType;
        public String SearchType
        {
            get { return _SearchType; }
            set
            {
                if (value == _SearchType)
                    return;
                _SearchType = value;
                OnPropertyChanged("SearchType");
            }
        }

        private String _SearchValue;
        public String SearchValue
        {
            get { return _SearchValue; }
            set
            {
                if (value == _SearchValue)
                    return;
                _SearchValue = value;
                OnPropertyChanged("SearchValue");
            }
        }

        private bool _New;
        public bool New
        {
            get { return _New; }
            set
            {
                if (value == _New)
                    return;
                _New = value;
                OnPropertyChanged("New");
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

        public DlkAddControlData()
        {
            ControlType = "";
            ControlName = "";
            SearchType = "";
            SearchValue = "";
            New = true;
        }

        public DlkAddControlData(String ControlType, String ControlName, String SearchType, String SearchValue)
        {
            this.ControlType = ControlType;
            this.ControlName = ControlName;
            this.SearchType = SearchType;
            this.SearchValue = SearchValue;
        }
    }
}
