using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using CommonLib.DlkRecords;

namespace CommonLib.DlkHandlers
{
    public class DlkInitiFileHandler
    {
        public String mLibrary { get; set; }
        public String mProductName { get; set; }
        public String mInitFile { get; set; }

        private XDocument mXml { get; set; }

        public DlkInitiFileHandler(String InitFilePath)
        {
            mLibrary = "";
            mProductName = "";
            mInitFile = InitFilePath;

            mXml = XDocument.Load(InitFilePath);

            var data = from doc in mXml.Descendants("Init")
                        select new
                        {
                            lib = (String)doc.Element("library").Value,
                            prod = (String)doc.Element("product").Value
                        };
            foreach (var val in data)
            {
                mLibrary = val.lib;
                mProductName = val.prod;
            }
        }
    }
}