using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CommonLib.DlkRecords
{
    public class DlkErrorRecord
    {
        private string m_Logs = "";
        private string m_Instance = string.Empty;

        public string Instance 
        { 
            get
            {
                return m_Instance;
            }
            set
            {
                m_Instance = value;
            }
        }


        public string Logs 
        { 
            get
            {
                return m_Logs;
            }
            set
            {
                m_Logs = value;
            }
        }


        public DlkErrorRecord()
        {

        }
    }
}