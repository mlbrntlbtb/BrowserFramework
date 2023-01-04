using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace UpdateRegistrySettings
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdateReg_Click(object sender, EventArgs e)
        {
            UpdateEnableProtectedMode();
            SetZoomLevel();

            MessageBox.Show("Registry successfully updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateEnableProtectedMode()
        {
            try
            {
                const string userRoot = "HKEY_CURRENT_USER";
                const string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones";

                string machineOS = Environment.OSVersion.ToString();

                if (!machineOS.Contains("Microsoft Windows XP"))
                {
                    //Local Intranet setting
                    const string keyName1 = userRoot + "\\" + subKey + "\\" + "1\\";
                    Registry.SetValue(keyName1, "2500", 0);

                    //Trusted sites setting
                    const string keyName2 = userRoot + "\\" + subKey + "\\" + "2\\";
                    Registry.SetValue(keyName2, "2500", 0);

                    //Internet setting
                    const string keyName3 = userRoot + "\\" + subKey + "\\" + "3\\";
                    Registry.SetValue(keyName3, "2500", 0);

                    //Restricted sites setting
                    const string keyName4 = userRoot + "\\" + subKey + "\\" + "4\\";
                    Registry.SetValue(keyName4, "2500", 0);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void SetZoomLevel()
        {
            try
            {
                const string userRoot = "HKEY_CURRENT_USER";
                const string subKey = "Software\\Microsoft\\Internet Explorer\\Zoom";

                var ieVersion = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer").GetValue("Version");

                if (ieVersion.ToString().StartsWith("11"))
                {
                    const string zoomLevelkeyName = userRoot + "\\" + subKey;
                    Registry.SetValue(zoomLevelkeyName, "ZoomFactor", 0x000186a0, RegistryValueKind.DWord);
                }
                else
                {
                    const string zoomLevelkeyName = userRoot + "\\" + subKey;
                    Registry.SetValue(zoomLevelkeyName, "ZoomFactor", 100000, RegistryValueKind.DWord);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
