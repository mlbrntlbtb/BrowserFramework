using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLib.DlkRecords
{
    /// <summary>
    /// Class for browser capabilities
    /// </summary>
    public class DlkBrowserCapability
    {
        #region Constants
        /// <summary>
        /// Chrome argument capability
        /// </summary>
        public const string CHROME_ARGUMENT = "argument";

        /// <summary>
        /// Chrome excluded argument capability
        /// </summary>
        public const string CHROME_EXCLUDED_ARGUMENT = "excludedargument";

        /// <summary>
        /// Chrome additional capability
        /// </summary>
        public const string CHROME_ADDITIONAL_CAPABILITY = "additionalcapability";

        /// <summary>
        /// Chrome user profile preference
        /// </summary>
        public  const string CHROME_USER_PROFILE_PREFERENCE = "userprofilepreference";
        #endregion

        #region Public Properties
        /// <summary>
        /// Browser name
        /// </summary>
        public string Browser { get; }

        /// <summary>
        /// Capability name
        /// </summary>
        public string CapabilityName { get; }

        /// <summary>
        /// Capability parameter
        /// </summary>
        public string Parameter { get; }

        /// <summary>
        /// Chrome capability argument
        /// </summary>
        public string Argument { get; }

        /// <summary>
        /// Chrome capability type
        /// </summary>
        public string CapabilityType { get; }

        /// <summary>
        /// Checks if the argument is for disable notification
        /// </summary>
        public bool DisableNotificationArgument => Argument == "--disable-notifications";

        /// <summary>
        /// Checks if the argument is for browser window resizing
        /// </summary>
        public bool WindowSizeArgument => Argument.Contains("window-size");
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for argument and excludedargument capability
        /// </summary>
        /// <param name="browser">browser name</param>
        /// <param name="argument">capability argument</param>
        /// <param name="capabilityType">capability type</param>
        public DlkBrowserCapability(string browser, string argument, string capabilityType)
        {
            Browser = browser;
            Argument = argument;
            CapabilityType = capabilityType;
        }

        /// <summary>
        /// Constructor for additional capability and user profile preference capability
        /// </summary>
        /// <param name="browser">browser name</param>
        /// <param name="capabilityName">capability name</param>
        /// <param name="parameter">capability parameter</param>
        /// <param name="capabilityType">capability type</param>
        public DlkBrowserCapability(string browser, string capabilityName, string parameter, string capabilityType)
        {
            Browser = browser;
            CapabilityName = capabilityName;
            Parameter = parameter;
            CapabilityType = capabilityType;
        }
        #endregion
    }
}
