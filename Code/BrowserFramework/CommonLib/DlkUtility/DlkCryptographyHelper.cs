using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommonLib.DlkUtility
{ 
    /// <summary>
    /// Provides methods to encrypt strings or files into a byte array ( byte[] ) using the RijndaelManaged Class.
    /// This uses the RijndaelManaged class of System.Security.Cryptography
    /// </summary>
    public class DlkEncryptionHelper
    {
        //change if really needed, but this will affect decryption
        private const string password = "myKey123"; 

        /// <summary>
        /// Encrypts the product key and stores the encrypted product key in a file.
        /// </summary>
        /// <param name="licenseKey"></param>
        /// <param name="outputfile"></param>
        public void EncryptFile(string licenseKey, string outputfile)
        {
            try
            {
                UnicodeEncoding ue = new UnicodeEncoding();
                byte[] key = ue.GetBytes(password);

                string cryptFile = outputfile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged rmCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    rmCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                byte[] data = Encoding.ASCII.GetBytes(licenseKey);
                cs.Write(data, 0, data.Length);

                cs.Close();
                fsCrypt.Close();

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To be used for encryption of words (username, password). Returns an array of bytes that can be converted 
        /// to a (usually) gibberish string literal by using Convert.ToBase64String(pass returned byte array here).
        /// </summary>
        public byte[] EncryptStringToByteArray (string word)
        {
            byte[] encrypted;
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] key = ue.GetBytes(password);
            try
            {
                using (RijndaelManaged rijndaelInstance = new RijndaelManaged())
                {
                    rijndaelInstance.Padding = PaddingMode.PKCS7;
                    rijndaelInstance.Key = key;
                    rijndaelInstance.IV = key;

                    ICryptoTransform encryptor = rijndaelInstance.CreateEncryptor(key, key);

                    //create a data s'tream for encryption
                    using (MemoryStream encryptionStream = new MemoryStream())
                    {   
                        //transform encryptor to cryptostream
                        using (CryptoStream cryptoStream = new CryptoStream(encryptionStream, encryptor, CryptoStreamMode.Write))
                        {
                            //stream the cryptostream to be able to read the original string literal
                            using (StreamWriter writer = new StreamWriter(cryptoStream))
                            {
                                writer.Write(word);
                            }
                            //memorystream to byte[]
                            encrypted = encryptionStream.ToArray();
                        }
                    }
                    //streams to be garbage collected out of existence.
                }
                return encrypted.ToArray();
                //use Convert.ToBase64String(encrypted) to convert the byte array to a string literal that you can convert back using Convert.FromBase64String
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if a string is encryptable or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsEncryptable(string value)
        {
            try
            {
                Convert.ToBase64String(EncryptStringToByteArray(value));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Provides methods to decrypt strings or files that were encrypted using the DlkEncryptionHelper class. The password should always be the same between the two classes.
    /// This uses the RijndaelManaged class of System.Security.Cryptography
    /// </summary>
    public class DlkDecryptionHelper
    {
        //change if really needed ,but changing this
        //will affect decryption of the previously encrypted values
        private const string password = "myKey123";

        /// <summary>
        /// Returns the decrypted string from a file.
        /// </summary>
        /// <param name="inputfile"></param>
        /// <returns></returns>
        public string DecryptFile(string inputfile)
        {
            string deKey = string.Empty;

            try
            {

                UnicodeEncoding ue = new UnicodeEncoding();
                byte[] key = ue.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputfile, FileMode.Open);

                RijndaelManaged rmCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    rmCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                StreamReader reader = new StreamReader(cs);
                deKey = reader.ReadToEnd();
                deKey.Replace(Environment.NewLine, "");

                cs.Close();
                fsCrypt.Close();

            }
            catch
            {
                throw;
            }

            return deKey;

        }

        /// <summary>
        /// Accepts a byte array to be decrypted using our Key and Initialization Vector (IV). The Key and IV is the converted byte array from the password used in both encryption and decryption.
        /// This returns the original string after decrypting the byte array returned by the EncryptStringToByteArray method.
        /// Accepts a byte array argument which you can get by using the return type of Convert.FromBase64String(pass the string literal converted from EncryptStringToByteArray)
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public string DecryptByteArrayToString(byte[] encryptedString)
        {
            string originalString = "";
            UnicodeEncoding ue = new UnicodeEncoding();
            byte[] key = ue.GetBytes(password);
            try
            {
                using (RijndaelManaged rijndaelInstance = new RijndaelManaged())
                {
                    rijndaelInstance.Padding = PaddingMode.PKCS7;
                    rijndaelInstance.Key = key;
                    rijndaelInstance.IV = key;

                    ICryptoTransform decryptor = rijndaelInstance.CreateDecryptor(rijndaelInstance.Key, rijndaelInstance.IV);

                    //difference with encryption is passing the encrypted string to the ctor
                    using (MemoryStream decryptionStream = new MemoryStream(encryptedString))
                    {
                        //and settings the stream mode from "Write" to "Read"
                        using (CryptoStream cryptoStream = new CryptoStream(decryptionStream, decryptor, CryptoStreamMode.Read))
                        {
                            //and using streamreader instead of streamwriter
                            using (StreamReader reader = new StreamReader(cryptoStream))
                            {
                                //decrypt original string from the byte array
                                originalString = reader.ReadToEnd();
                            }
                        }
                    }
                }
                //streams to be garbage collected out of existence.
                return originalString;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Checks if a string is decryptable or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsDecrpytable(string value)
        {
            try
            {
                DecryptByteArrayToString(Convert.FromBase64String(value));
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
