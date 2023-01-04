using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.AdvancedScheduler.Model
{
    public class AgentGroup : INotifyPropertyChanged, IAgentList
    {
        public const string ALL_GROUP_NAME = "ALL";
        public const string AGENT_TYPE_GROUPS = "Groups";

        public event PropertyChangedEventHandler PropertyChanged;

        private string mName = string.Empty;

        public string Name 
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
                NotifyPropertyChanged("Name");
            }
        }

        public List<string> Members { get; set; }
        public string AgentType { get; set; }

        public AgentGroup(string name)
        {
            Name = name;
            Members = new List<string>();
            AgentType = AGENT_TYPE_GROUPS;
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
