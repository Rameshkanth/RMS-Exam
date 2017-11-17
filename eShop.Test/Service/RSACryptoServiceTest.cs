using eShop.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace eShop.Test.Service
{
    [TestClass]
    public class RSACryptoServiceTest
    {
        [TestMethod]
        public void RsaEncryptDecryptTest()
        {
            // Setup
            const string orignalString = "RSA is very cool";
            const string privateKeyContainerId = "testContainer";

            // Act
            var encryptedString = RSACryptoService.Instance.Encrypt(orignalString, privateKeyContainerId);
            var decryptedString = RSACryptoService.Instance.Decrypt(encryptedString, privateKeyContainerId);

            // Assert
            Assert.AreEqual(orignalString, decryptedString);
        }

    }
}
