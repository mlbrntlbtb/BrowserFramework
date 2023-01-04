using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIT.Sys
{
    class MobileConfig
    {
        public static MobileRecord[] MobileRecords = new MobileRecord[]
        {
                new MobileRecord()
                {
                    MobileId = "AJ_310_EMU",
                    MobileUrl = "http://127.0.0.1:4723/wd/hub",
                    MobileType = "Android",
                    DeviceVersion = "8.1.0",
                    DeviceName = "Pixel_2_API_27",
                    Application = "com.deltek.maconomy.time",
                    Path = @"C:\TFS\BrowserFramework\Products\MaconomyTouch\UserTestData\deltekmaconomytime-debug.apk",
                    IsEmulator = true,
                    APKSource = @"\\hq1apprd63vs\MaconomyTouch\300"
                }

                ,new MobileRecord()
                {
                    MobileId = "AJ_310",
                    MobileUrl = "http://127.0.0.1:4723/wd/hub",
                    MobileType = "Android",
                    DeviceVersion = "8.1.0",
                    DeviceName = "Pixel C",
                    Application = "com.deltek.maconomy.time",
                    Path = @"C:\TFS\BrowserFramework\Products\MaconomyTouch\UserTestData\deltekmaconomytime-debug.apk",
                    IsEmulator = false,
                    APKSource = @"\\hq1apprd63vs\MaconomyTouch\300"
                }

                ,new MobileRecord()
                {
                    MobileId = "AJ_231",
                    MobileUrl = "http://127.0.0.1:4723/wd/hub",
                    MobileType = "Android",
                    DeviceVersion = "8.1.0",
                    DeviceName = "Pixel C",
                    Application = "com.deltek.maconomy.time",
                    Path = @"C:\TFS\BrowserFramework\Products\MaconomyTouch_231\UserTestData\deltekmaconomytime-debug.apk",
                    IsEmulator = false,
                    APKSource = @"\\hq1apprd63vs\MaconomyTouch\231"
                }

                 ,new MobileRecord()
                {
                    MobileId = "Nexus6PEmulatorNougat7",
                    MobileUrl = "http://127.0.0.1:4723/wd/hub",
                    MobileType = "Android",
                    DeviceVersion = "7.0",
                    DeviceName = "Nexus6PEmulatorNougat7",
                    Application = "com.deltek.maconomy.time",
                    Path = @"C:\BrowserFramework\Products\MaconomyTouch\UserTestData\deltekmaconomytime-debug.apk",
                    IsEmulator = true,
                    APKSource = @"\\hq1apprd63vs\MaconomyTouch\300"
                }

                 ,new MobileRecord()
                {
                    MobileId = "TESTIT_Nexus6PEmulator_Nougat7",
                    MobileUrl = "http://127.0.0.1:4723/wd/hub",
                    MobileType = "Android",
                    DeviceVersion = "7.1.1",
                    DeviceName = "TESTIT_Nexus6PEmulator_Nougat7",
                    Application = "com.deltek.maconomy.time",
                    Path = @"C:\BrowserFramework\Products\MaconomyTouch\UserTestData\deltekmaconomytime-debug.apk",
                    IsEmulator = true,
                    APKSource = @"\\hq1apprd63vs\MaconomyTouch\300"
                }

                 ,new MobileRecord()
                {
                    MobileId = "NEXUS",
                    MobileUrl = "http://127.0.0.1:4723/wd/hub",
                    MobileType = "Android",
                    DeviceVersion = "6.0.1",
                    DeviceName = "Nexus 6P",
                    Application = "com.deltek.maconomy.time",
                    Path = @"C:\TFS\BrowserFramework\Products\MaconomyTouch\UserTestData\deltekmaconomytime-debug.apk",
                    IsEmulator = false,
                    APKSource = @"\\hq1apprd63vs\MaconomyTouch\300"
                }

                // Copy and uncomment below to add new record. Do not delete this block, just copy.
                
                //,new MobileRecord()
                //{
                //    MobileId = "default",
                //    MobileUrl = "http://127.0.0.1:4723/wd/hub",
                //    MobileType = "",
                //    DeviceVersion = "",
                //    DeviceName = "",
                //    Application = "",
                //    Path = "",
                //    IsEmulator = false,
                //    APKSource = @"\\hq1apprd63vs\MaconomyTouch\300"
                //}
        };
    }

    public class MobileRecord
    {
        public string MobileId { get; set; }
        public string MobileUrl { get; set; }
        public string MobileType { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceName { get; set; }
        public string Application { get; set; }
        public string Path { get; set; }
        public bool IsEmulator { get; set; }
        public string APKSource { get; set; }
    }
}
