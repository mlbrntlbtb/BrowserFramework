using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recorder.View;

namespace Recorder.Model
{
    public class Variable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static List<Variable> AllVariables = new List<Variable>();
        private IMainView mView;

        public Variable(string name, IMainView view, string currentValue, string startIndex, string length)
        {
            mView = view;
            Name = name;
            StartIndex = startIndex;
            Length = length;
            CurrentValue = currentValue;
            Variable.AllVariables.Add(this);
            this.InUse = true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private bool mInUse;
        public bool InUse 
        { 
            get
            {
                return mInUse;
            } 
            set
            {
                mInUse = value;
                foreach (Variable vrb in Variable.AllVariables)
                {
                    if (!vrb.Equals(this))
                    {
                        vrb.mInUse = false;
                    }
                }
                OnPropertyChanged("InUse");
                mView.VariablesUpdated();
            }
        }

        private string mCurrentValue = string.Empty;
        public string CurrentValue
        {
            get
            {
                return mCurrentValue;
            }
            set
            {
                mCurrentValue = value;
                OnPropertyChanged("CurrentValue");
                mView.VariablesUpdated();
            }
        }
        public string Name { get; set; }
        public string Length { get; set; }
        public string StartIndex { get; set; }
    }
}
