using CommonLib.DlkSystem;
using CommonLib.DlkUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMLib.DlkUtility
{
    public static class DlkBPMGenericMethods
    {
        static string globalVarFolder = Path.Combine(DlkEnvironment.mDirProduct, @"UserTestData\Data\");

        public static string GetPhaseValue()
        {
            string filePath = globalVarFolder + "GlobalConfig.csv";
            string phaseVal = "";
            DataTable csvData;

            try
            {
                csvData = new DataTable();
                csvData = DlkCSVHelper.CSVParse(filePath);

                phaseVal = csvData.Rows[0][1].ToString();
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("Getting Phase value failed:" + e);
            }

            return phaseVal;
        }

        public static string GetRunTypeValue()
        {
            string filePath = globalVarFolder + "GlobalConfig.csv";
            string runTypeVal = "";
            DataTable csvData;

            try
            {
                csvData = new DataTable();
                csvData = DlkCSVHelper.CSVParse(filePath);

                runTypeVal = csvData.Rows[1][1].ToString();
            }
            catch (Exception e)
            {
                DlkLogger.LogInfo("Getting Phase value failed:" + e);
            }

            return runTypeVal;
        }
    }
}
