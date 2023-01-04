using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text.RegularExpressions;
using TestRunner.Common;
using System.Globalization;
using System.Reflection;
using CommonLib.DlkUtility;
using CommonLib.DlkSystem;
using Microsoft.Win32;
using System.Linq;
using System.Security.AccessControl;

namespace TestRunner
{
    public partial class EnterProductKey
    {
        #region DECLARATIONS

        //ERROR MESSAGES
        const string mainKey = "TRTC";
        readonly string INVALID_PRODUCT_KEY = "The Product Key is invalid, please try a new one!";
        readonly string EXPIRED_PRODUCT_KEY = "The Product Key has expired, please renew now!";
        private bool _mContinue;
        private bool mUpdateLicense = false;

        //PRODUCT KEY FILE NAMES
        static string encryptedFile = "enKey.txt";

        //FULL PATH OF PRODUCT KEY FILES
        private readonly string _enFullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), encryptedFile);

        public static DateTime expiryDate;        

        #endregion

        #region PROPERTIES

        public string EnteredProductKey { get; set; }

        public LicenseStatusType LicenseStatus { get; set; }

        public string EnFullPath
        {
            get { return _enFullPath; }
        }

        #endregion

        #region CONSTRUCTOR

        public EnterProductKey(bool IsUpdate = false)
        {
            InitializeComponent();

            /* Set Root Path based from executing binary */
            string binDir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string mRootPath = Directory.GetParent(binDir).FullName;
            while (new DirectoryInfo(mRootPath).GetDirectories().Where(x => x.FullName.Contains("Products")).Count() == 0)
            {
                mRootPath = Directory.GetParent(mRootPath).FullName;
            }

            DlkEnvironment.mDirTools = Path.Combine(mRootPath, "Tools") + @"\";
            if (!Directory.Exists(DlkEnvironment.mDirTools))
            {
                throw new Exception("Required Tools directory does not exist: " + DlkEnvironment.mDirTools);
            }
            mUpdateLicense = IsUpdate;
        }

        #endregion

        #region METHODS

        public LicenseStatusType GetLicenseKeyStatus(string key = "")
        {
            var licenseStatus = LicenseStatusType.Invalid;
            
            /* if input is empty, check in registry */
            key = string.IsNullOrEmpty(key) ? GetLicenseKeyFromRegistry() : key;

            if (!String.IsNullOrEmpty(key))
            {
                /* check if decryptable, if not ASSUME it is already decrypted */
                string line = new DlkDecryptionHelper().IsDecrpytable(key) ? DecryptProductKey(key) : key;
                if (!string.IsNullOrEmpty(line))
                {
                    var productKey = GetResultsWithOutHyphen(line);
                    var decryptedLicenseKey = CreateLicenseKey(productKey);
                    var prefixFound = ValidateLicenseKey(decryptedLicenseKey);
                    licenseStatus = prefixFound
                        ? ValidateExpirationDate(decryptedLicenseKey)
                        : LicenseStatusType.Invalid;
                }
            }

            return licenseStatus;
        }

        private LicenseStatusType ValidateExpirationDate(string key)
        {
            try
            {
                //Split key into four parts separated by "-"
                string[] separateKeys = key.Split('-');
                string date = string.Empty;
                LicenseStatusType status;

                // get date in format of MMDDYYYY
                date += separateKeys[1] += separateKeys[2];
                date = date.Insert(2, "/");
                date = date.Insert(5, "/");

                
                DateTime currentDate = DateTime.Now.Date;
                if (DateTime.TryParseExact(date, "MM/dd/yyyy",  CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate))
                {
                    status = currentDate > expiryDate.Date ? LicenseStatusType.Expired : LicenseStatusType.Valid;
                }
                else
                {
                    status = LicenseStatusType.Invalid;
                }

                return status;
            }
            catch
            {
                return LicenseStatusType.Invalid;
            }
        }

        public void CheckLicenseStatus()
        {
            string key = GetLicenseKeyFromRegistry();

            if (!String.IsNullOrEmpty(key))
            {
                //Have previously entered product key
                string line = DecryptProductKey(key);

                /* if key in reg does not match EnteredProductKey, user entered new key */
                bool bNewKey = line != EnteredProductKey && !string.IsNullOrEmpty(EnteredProductKey);
                line = bNewKey ? EnteredProductKey : string.Empty;

                LicenseStatus = GetLicenseKeyStatus(line);

                switch (LicenseStatus)
                {
                    case LicenseStatusType.Valid:
                        if (bNewKey) // write only if there is new key
                        {
                            WriteLicenseKeyToRegistry(EnteredProductKey);
                        }
                        _mContinue = true;
                        if (mUpdateLicense)
                        {
                            DlkUserMessages.ShowInfo(DlkUserMessages.INF_PRODUCTKEY_APPLIED);
                        }
                        Close();
                        break;
                    case LicenseStatusType.Expired:
                        Show();
                        ToggleFields(true);
                        DisplayErrorMessage(EXPIRED_PRODUCT_KEY);
                        _mContinue = false;
                        break;
                    case LicenseStatusType.Invalid:
                        Show();
                        ToggleFields(true);
                        DisplayErrorMessage(INVALID_PRODUCT_KEY);
                        _mContinue = false;
                        break;
                }
            }
            else //First time entering product key
            {
                LicenseStatus = GetLicenseKeyStatus(EnteredProductKey);

                switch (LicenseStatus)
                {
                    case LicenseStatusType.Valid:
                        WriteLicenseKeyToRegistry(EnteredProductKey);
                        _mContinue = true;
                        Close();
                        break;
                    case LicenseStatusType.Expired:
                        DisplayErrorMessage(EXPIRED_PRODUCT_KEY);
                        ToggleFields(true);
                        _mContinue = false;
                        break;
                    case LicenseStatusType.Invalid:
                        DisplayErrorMessage(INVALID_PRODUCT_KEY);
                        ToggleFields(true);
                        _mContinue = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Returns the decrypted product key.
        /// </summary>
        /// <param name="key">the encrypyted key from the registry</param>
        /// <returns>the decrypted key</returns>
        private string DecryptProductKey(string key)
        {
            DlkDecryptionHelper oDecrypt = new DlkDecryptionHelper();
            return oDecrypt.DecryptByteArrayToString(Convert.FromBase64String(key));
        }

        /// <summary>
        /// Encrypts the product key.
        /// </summary>
        /// <param name="key">the key entered by the user</param>
        private string EncryptProductKey(string key)
        {
            DlkEncryptionHelper oEncrypt = new DlkEncryptionHelper();
            return Convert.ToBase64String(oEncrypt.EncryptStringToByteArray(key));
        }

        /// <summary>
        /// Returns the decrypted string from the key file.
        /// </summary>
        /// <param name="key">the full path of the encrypted key file</param>
        /// <returns>the decrypted key</returns>
        private string DecryptProductKeyFile(string filePath)
        {
            DlkDecryptionHelper oDecrypt = new DlkDecryptionHelper();
            return oDecrypt.DecryptFile(filePath);
        }

        private void DisplayErrorMessage(string error)
        {
            ErrorMessage.Content = string.Empty;
            ErrorMessage.Visibility = Visibility.Visible;
            ErrorMessage.Content = error;
        }

        private string GetResultsWithOutHyphen(string inputString)
        {
            return inputString.Replace("-", "");
        }

        private string CreateLicenseKey(string productKey)
        {
            string key = string.Empty;
            key += productKey[15];
            key += productKey[14];
            key += productKey[8];
            key += productKey[9];
            key += "-";
            key += productKey[6];
            key += productKey[11];
            key += productKey[4];
            key += productKey[12];
            key += "-";
            key += productKey[2];
            key += productKey[3];
            key += productKey[13];
            key += productKey[5];
            key += "-";
            key += productKey[7];
            key += productKey[10];
            key += productKey[1];
            key += productKey[0];

            return key;

        }

        private bool ValidateLicenseKey(string key)
        {
            string[] separateKeys = key.Split('-');
            bool isLegit = false;

            if (separateKeys.Length > 0)
            {
                isLegit = separateKeys[0] == mainKey;
            }

            return isLegit;
        }

        private void EnableNext()
        {
            BtnNext.IsEnabled = TxtProductKeyValue.Text.Length == TxtProductKeyValue.MaxLength;
        }

        private void ToggleFields(bool visible)
        {
            var controlVisibility = visible ? Visibility.Visible : Visibility.Hidden;
            
            LblWelcome.Visibility = controlVisibility;
            LblTestRunner.Visibility = controlVisibility;
            TxtProductKey.Visibility = controlVisibility;

            if (mUpdateLicense)
            {
                LblWelcome.Visibility = Visibility.Hidden;
                LblTestRunner.Visibility = Visibility.Hidden;
            }
        }

        private string GetResultsWithHyphen(string input)
        {

            // append hyphen after 4 characters logic goes here
            string output = "";
            int start = 0;
            while (start < input.Length)
            {
                output += input.Substring(start, Math.Min(4, input.Length - start)) + "-";
                start += 4;
            }
            // remove the trailing dash
            return output.Trim('-');
        }
        
        /// <summary>
        /// Encrypts the product key first and writes the encrypted key to registry.
        /// </summary>
        /// <param name="key">the license key entered by the user</param>
        private void WriteLicenseKeyToRegistry(string key)
        {
            RegistryKey registryKeyNode;
            string encryptedKey;

            try
            {
                encryptedKey = EncryptProductKey(key);
                registryKeyNode = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DELTEK\TestRunner");

                if (registryKeyNode != null)
                {
                    Registry.CurrentUser.DeleteSubKey(@"SOFTWARE\DELTEK\TestRunner");
                }

                // Create a subkey named TestRunner under HKEY_CURRENT_USER.
                registryKeyNode = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\DELTEK\TestRunner");
                registryKeyNode.SetValue("LicenseKey", encryptedKey);
            }
            catch (Exception e)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_REG_UPDATE_ERROR, e, true);
            }

        }

        /// <summary>
        /// Checks if the TestRunner registry node exists or not. Retrieves the encrypted key value.
        /// </summary>
        private string GetLicenseKeyFromRegistry()
        {
            string keyValue = "";
            RegistryKey regKey;

            try
            {
                regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DELTEK\TestRunner");

                if (regKey == null)
                {
                    keyValue = "";
                }
                else
                {
                    keyValue = (string)regKey.GetValue("LicenseKey");
                }
                return keyValue;
            }
            catch
            {
                return keyValue;
            }         
        }

        #endregion

        #region EVENTS

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Check if key was retrieved from Registry
                string key = GetLicenseKeyFromRegistry();
                if (!String.IsNullOrEmpty(key) && !mUpdateLicense)
                {
                    CheckLicenseStatus();
                }
                else
                {
                    ToggleFields(true);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EnteredProductKey = TxtProductKeyValue.Text;
                CheckLicenseStatus();
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex, true);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                DialogResult = _mContinue;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mUpdateLicense)
                {
                    DialogResult = _mContinue;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }

        private void txtProductKeyValue_OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            try
            {
                TxtProductKeyValue.Text = Regex.Replace(TxtProductKeyValue.Text, @"[^a-zA-Z0-9-]", "");
                var text = GetResultsWithOutHyphen(TxtProductKeyValue.Text);
                TxtProductKeyValue.Text = GetResultsWithHyphen(text);
                EnableNext();
                TxtProductKeyValue.CaretIndex = TxtProductKeyValue.Text.Length;
            }
            catch (Exception ex)
            {
                DlkLogger.LogToFile(DlkUserMessages.ERR_UNEXPECTED_ERROR, ex);
            }
        }
        #endregion
    }
}

public enum LicenseStatusType
{
    Valid,
    Invalid,
    Expired
}

