using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ProjectCureData.Models.Mapping;

namespace ProjectCureData.Models
{
    public partial class ProjectCureContext : DbContext
    {
        static ProjectCureContext()
        {
            Database.SetInitializer<ProjectCureContext>(null);
        }

        public ProjectCureContext()
            : base("Name=ProjectCureContext")
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new TemplateMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
