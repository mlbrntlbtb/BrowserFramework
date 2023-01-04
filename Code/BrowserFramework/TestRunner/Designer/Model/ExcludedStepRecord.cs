using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Designer.Model
{
    public class ExcludedStepRecord: INotifyPropertyChanged
    {
        private String _screen;
        private String _control;
        private String _keyword;
        private bool _includeparam;
        public String mScreen
        {
            get
            {
                return _screen;
            }
            set
            {
                _screen = value;
                OnPropertyChanged("mScreen");
            }
        }
        public String mControl
        {
            get
            {
                return _control;
            }
            set
            {
                _control = value;
                OnPropertyChanged("mControl");
            }
        }
        public String mKeyword
        {
            get
            {
                return _keyword;
            }
            set
            {
                _keyword = value;
                OnPropertyChanged("mKeyword");
            }
        }
        public bool includeParameter
        {
            get
            {
                return _includeparam;
            }
            set
            {
                _includeparam = value;
                OnPropertyChanged("includeParameter");
            }
        }
        public List<String> Screens
        {
            get
            {
                return _Screens;
            }
            set
            {
                _Screens = value;
                OnPropertyChanged("Screens");
            }
        }
        private List<String> _Screens;
        public List<String> Controls
        {
            get
            {
                return _Controls;
            }
            set
            {
                _Controls = value;
                OnPropertyChanged("Controls");
            }
        }
        private List<String> _Controls;

        public List<String> Keywords
        {
            get
            {
                return _Keywords;
            }
            set
            {
                _Keywords = value;
                OnPropertyChanged("Keywords");
            }
        }
        private List<String> _Keywords;

        public String Parameters { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises an event each time a property is changed
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Excluded Step Record Constructor
        /// </summary>
        public ExcludedStepRecord(List<String> _Screens)
        {
            Screens = _Screens;
        }
    }
}
