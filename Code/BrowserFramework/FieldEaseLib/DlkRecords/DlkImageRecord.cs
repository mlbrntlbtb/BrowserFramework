using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldEaseLib.DlkRecords
{
    public static class DlkImageRecord
    {
        public static string ImageSourceBefore { get; private set; }
        public static string ImageSourceAfter { get; private set; }

        public static void SetImageSource(string img, bool before = true)
        {
            if (before)
                ImageSourceBefore = img;
            else
                ImageSourceAfter = img;
        }
    }
}
