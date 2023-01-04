using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkSuiteLinkRecord
    {
        public string Id { get; set; }
        public string LinkPath { get; set; }
        public string DisplayName { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        public DlkSuiteLinkRecord(string Name, string Link)
        {
            this.Id = Guid.NewGuid().ToString();
            this.LinkPath = Link;
            this.DisplayName = Name;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        public DlkSuiteLinkRecord(string LinkID, string Name, string Link)
        {
            this.Id = LinkID;
            this.LinkPath = Link;
            this.DisplayName = Name;
        }

        /// <summary>
        /// Return copy of current object
        /// </summary>
        /// <returns></returns>
        public DlkSuiteLinkRecord Clone()
        {
            return (DlkSuiteLinkRecord)this.MemberwiseClone();
        }
    }
}
