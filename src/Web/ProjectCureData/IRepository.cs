﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCureData.Models;

namespace ProjectCureData
{
	public interface IRepository
	{
		bool IsValidUser(string userName, string password);
		User GetUserByUserName(string userName);
		void UpdatePassword(User user);
		void SaveUser(User user);
	}
}
