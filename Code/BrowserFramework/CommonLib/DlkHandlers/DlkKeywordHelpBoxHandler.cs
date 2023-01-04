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
using System.Windows.Forms;
using CommonLib.DlkSystem;
namespace CommonLib.DlkHandlers
{
    public class DlkKeywordHelpBoxHandler
    {
        private List<DlkKeywordHelpBoxControlParameters> mControls;
        private String mHelpFile { get; set; }
        public ObservableCollection<DlkKeywordHelpBoxRecord> mKeywordHelpBoxRecords { get; set; }

        public DlkKeywordHelpBoxHandler(bool Override = false)
        {
            mHelpFile = !File.Exists(DlkEnvironment.mHelpConfigFile) | Override ? DlkEnvironment.mCommonHelpConfigFile : DlkEnvironment.mHelpConfigFile;
            mKeywordHelpBoxRecords = new ObservableCollection<DlkKeywordHelpBoxRecord>();
            if (File.Exists(mHelpFile))
            {
                InitializeHelp();
            }
        }

        private void InitializeHelp()
        {
            string mKeyword = "";
            string mInformation = "";
            string mControlList = "";
            string mParameters = "";
            DlkKeywordHelpBoxControlParameters mControl;

            using (XmlReader reader = XmlReader.Create(mHelpFile))
            {
                while (reader.ReadToFollowing("help"))
                {
                    mControls = new List<DlkKeywordHelpBoxControlParameters>();
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        XmlReader keywordTree = reader.ReadSubtree();
                        while (keywordTree.Read())
                        {
                            if (keywordTree.NodeType == XmlNodeType.Element)
                            {
                                switch (keywordTree.Name)
                                {
                                    case "keyword":
                                        mKeyword = keywordTree.ReadInnerXml();
                                        break;
                                    case "information":
                                        mInformation = keywordTree.ReadInnerXml();
                                        break;
                                    case "control":
                                        mControl = new DlkKeywordHelpBoxControlParameters { Control = keywordTree.GetAttribute("key") };
                                        keywordTree.MoveToElement();
                                        XmlReader parameterTree = keywordTree.ReadSubtree();
                                        while (parameterTree.ReadToFollowing("parameters"))
                                        {
                                            if(parameterTree.NodeType == XmlNodeType.Element)
                                                mControl.Parameters = parameterTree.ReadInnerXml();
                                        }
                                        mControls.Add(mControl);                                        
                                        break;
                                    case "controls":
                                        mControlList = keywordTree.ReadInnerXml();
                                        break;
                                    case "parameters":
                                        mParameters = keywordTree.ReadInnerXml();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                    if(mControls.Count > 0)
                    {
                        foreach (DlkKeywordHelpBoxControlParameters val in mControls)
                        {
                            foreach (string _control in val.Control.Split(','))
                            {
                                DlkKeywordHelpBoxRecord rec = new DlkKeywordHelpBoxRecord(mKeyword, mInformation, val.Parameters, mControlList, mHelpFile, _control);
                                mKeywordHelpBoxRecords.Add(rec);
                            }
                        }
                    }
                    else
                    {
                        DlkKeywordHelpBoxRecord rec = new DlkKeywordHelpBoxRecord(mKeyword, mInformation, mParameters, mControlList, mHelpFile, "");
                        mKeywordHelpBoxRecords.Add(rec);
                    }
                }
            }
        }         
    }

    sealed class DlkKeywordHelpBoxControlParameters
    {
        public string Control { get; set; } = "";
        public string Parameters { get; set; } = "";
    }
}
