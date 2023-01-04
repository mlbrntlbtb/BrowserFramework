using System;
using System.IO;

namespace CommonLib.DlkHandlers
{
    public class DlkSettingsConfigHandler
    {
        #region PROPERTIES
        public String MConfigFile { get; set; }
        public String MErrorLogLevel { get; set; }
        #endregion

        #region CONSTRUCTOR

        public DlkSettingsConfigHandler()
        {
            MConfigFile = DlkConfigHandler.MainConfig;

            if (!File.Exists(MConfigFile) || !DlkConfigHandler.ConfigExists("errorloglevel"))
            {
                DlkConfigHandler.UpdateConfigValue("errorloglevel", "default");
            }

            MErrorLogLevel = DlkConfigHandler.GetConfigValue("errorloglevel");
        }

        #endregion


    }
}