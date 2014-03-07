using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCureData.Models
{
	public partial class ProjectCureContext : DbContext
	{
		public ProjectCureContext(string connectionString)
			: base(connectionString)
		{
		}
	}
}
