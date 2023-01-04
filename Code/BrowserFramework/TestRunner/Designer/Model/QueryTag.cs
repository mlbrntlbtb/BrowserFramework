using CommonLib.DlkRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Designer.Model
{
    public class QueryTag
    {
        public static readonly int QRY_TAG_SUBQRY_TYPE_DEF_IDX = -1;
        public QueryTag(string QueryTagId, bool? QueryIs, int ColIndex, string QueryContains)
        {
            TagId = QueryTagId;
            Is = QueryIs;
            TargetColumnIndex = ColIndex;
            Contains = QueryContains;
            //TagType = ColIndex == QRY_TAG_SUBQRY_TYPE_DEF_IDX ? Enumerations.QueryTagType.SubQuery : Enumerations.QueryTagType.Column;
        }

        public string TagId { get; set; }
        public bool? Is { get; set; }
        public int TargetColumnIndex { get; set; }
        public string Contains { get; set; }
        //public Enumerations.QueryTagType TagType { get; set; }
        public int ListPosition { get; set; }

        /// <summary>
        /// Return copy of current object
        /// </summary>
        /// <returns></returns>
        public QueryTag Clone()
        {
            return (QueryTag)this.MemberwiseClone();
        }
    }
}
