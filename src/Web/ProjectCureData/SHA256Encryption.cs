using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCureData
{
	public class SHA256Encryption
	{
		public static string ComputeSHA256Hash(string input)
		{
			SHA256 hash = SHA256.Create();
			hash.ComputeHash(Encoding.ASCII.GetBytes(input));
			return string.Concat(hash.Hash.Select(b => b.ToString("x2")));
		}
	}
}
