using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProjectCure.IntegrationTests
{
    [TestClass]
    public class SHA256Test
    {
        [TestMethod]
        public void TestSHA256()
        {
            Console.Out.WriteLine(ComputeSHA256Hash("hackathon"));
        }

        private static string ComputeSHA256Hash(string input)
        {
            SHA256 hash = SHA256.Create();
            hash.ComputeHash(Encoding.ASCII.GetBytes(input));
            return string.Concat(hash.Hash.Select(b => b.ToString("x2")));
        }
    }
}
