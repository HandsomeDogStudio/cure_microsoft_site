using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCure.Web.Models
{
	public class PasswordGenerator
	{
		public string GeneratePassword(int length)
		{
			var buffer = new char[length];
			for (var i = 0; i < length; i++)
			{
				buffer[i] = _chars[_rng.Next(_chars.Length)];
			}
			return new string(buffer);
		}

		private readonly Random _rng = new Random();
		private const string _chars = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
	}
}