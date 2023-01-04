using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkKeywordParameterRecord
    {
        public string mParameterName { get; set; }
        public string mValue { get; set; }
        public int mIndex { get; set; }
        public bool mContains { get; set; }
        public bool mParamConversionSetting { get; set; }


        public DlkKeywordParameterRecord(string name, string value, int index)
        {
            mParameterName = name;
            mValue = value;
            mIndex = index;
        }

        public DlkKeywordParameterRecord(string name, string value, int index, bool conversionSetting)
        {
            mParameterName = name;
            mValue = value;
            mIndex = index;
            mParamConversionSetting = conversionSetting;
        }
    }
}
