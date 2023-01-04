using CommonLib.DlkSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DlkRecords
{
    public class DlkMobileRecord
    {
        public string MobileId { get; set; }
        public string MobileUrl { get; set; }
        public string MobileType { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceName { get; set; }
        public string Application { get; set; }
        public string Path { get; set; }
        private int _cmdTimeout = DlkEnvironment.DefaultAppiumCommandTimeout;//set to 60 by default
        public bool IsEmulator { get; set; }

        public int CommandTimeout
        {
            get 
            {
                if (_cmdTimeout < DlkEnvironment.DefaultAppiumCommandTimeout)
                {
                    return DlkEnvironment.DefaultAppiumCommandTimeout;
                }
                return _cmdTimeout; 
            }
            set 
            {
                if (value >= DlkEnvironment.DefaultAppiumCommandTimeout)
                {
                    _cmdTimeout = value; //only change when greater than 60
                }
            }
        }
        
        public DlkMobileRecord(string pId, string pUrl, string pType, string pDeviceName, string pDeviceVersion, string pApplication, string pPath, int pCommandTimeout, bool pIsEmulator)
        {
            MobileId = pId;
            MobileUrl = pUrl;
            MobileType = pType;
            DeviceName = pDeviceName;
            DeviceVersion = pDeviceVersion;
            Application = pApplication;
            Path = pPath;
            CommandTimeout = pCommandTimeout;
            IsEmulator = pIsEmulator;
        }
    }
}
