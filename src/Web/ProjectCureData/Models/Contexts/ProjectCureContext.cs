using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ProjectCureData.Models.Mapping;

namespace ProjectCureData.Models
{
    public partial class ProjectCureContext : DbContext
    {
        public ProjectCureContext()
            : base("Name=ProjectCureContext")
        {}

        public ProjectCureContext(string connectionString)
            : base(connectionString)
        {}

        public DbSet<Event> Events { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(ProjectCureContext).Assembly);
        }
    }
}
