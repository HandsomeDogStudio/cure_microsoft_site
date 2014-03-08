using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectCureData;

namespace ProjectCure.IntegrationTests
{
    [TestClass]
    public class SHA256Test
    {
        [TestMethod]
        public void TestSHA256()
        {
            Console.Out.WriteLine(SHA256Encryption.ComputeSHA256Hash("test"));
        }
    }
}
