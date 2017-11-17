using eShop.Web.Helpers;
using System;
using System.Security.Cryptography;
using System.Text;

namespace eShop.Web.Services
{
    public class RSACryptoService
    {
        private UnicodeEncoding ByteConverter;
        private RSACryptoServiceProvider RSA;
        private byte[] plaintext;
        private byte[] encryptedtext;
        private static readonly Lazy<RSACryptoService> instance = new Lazy<RSACryptoService>(() => new RSACryptoService());

        private RSACryptoService()
        {
            ByteConverter = new UnicodeEncoding();
            RSA = new RSACryptoServiceProvider();
        }

        public static RSACryptoService Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public string Encrypt(string textToEncrypted, string privateKeyContainerId)
        {
            var privateKeyContainerIdHash = CryptographyHelper.GetMd5Hash(privateKeyContainerId);
            plaintext = ByteConverter.GetBytes(textToEncrypted);
            encryptedtext = Encryption(plaintext, true, privateKeyContainerIdHash);
            return Convert.ToBase64String(encryptedtext);
        }

        public string Decrypt(string textToBeDecrypted, string privateKeyContainerId)
        {
            var privateKeyContainerIdHash = CryptographyHelper.GetMd5Hash(privateKeyContainerId);
            encryptedtext = Convert.FromBase64String(textToBeDecrypted);
            byte[] decryptedtex = Decryption(encryptedtext, true, privateKeyContainerIdHash);
            return ByteConverter.GetString(decryptedtex);
        }

        private byte[] Encryption(byte[] data, bool doOAEPPadding, string privateKeyContainerId)
        {
            try
            {
                byte[] encryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(new CspParameters()
                {
                    KeyContainerName = privateKeyContainerId,
                    Flags = CspProviderFlags.UseMachineKeyStore, 
                }))
                {
                    encryptedData = RSA.Encrypt(data, doOAEPPadding);
                }

                return encryptedData;
            }
            catch (CryptographicException e)
            {
                return null;
            }

        }

        private byte[] Decryption(byte[] data, bool doOAEPPadding, string privateKeyContainerId)
        {
            try
            {
                byte[] decryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(new CspParameters()
                {
                    KeyContainerName = privateKeyContainerId,
                    Flags = CspProviderFlags.UseMachineKeyStore,
                }))
                {
                    decryptedData = RSA.Decrypt(data, doOAEPPadding);
                }

                return decryptedData;
            }
            catch (CryptographicException e)
            {
                return null;
            }
        }
    }
}

