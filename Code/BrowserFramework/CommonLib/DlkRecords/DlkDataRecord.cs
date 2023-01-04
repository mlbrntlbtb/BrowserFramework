using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommonLib.DlkRecords
{
    [Serializable]
    public class DlkDataRecord
    {
        private List<string> m_Values = new List<string>();
        private string m_Name = string.Empty;

        public string Name 
        { 
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }


        public List<string> Values 
        { 
            get
            {
                return m_Values;
            }
            set
            {
                m_Values = value;
            }
        }

        public DlkDataRecord(string DataName)
        {
            m_Name = DataName;
        }

        public DlkDataRecord()
        {

        }
    }
}