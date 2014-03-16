using System.Data.Entity;

namespace ProjectCureData.Models.Migrations
{
    /// <summary>
    /// Default strategy if not
    /// </summary>
    public class ProjectCureCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<ProjectCureContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ProjectCureContext context)
        {
            //  This method will be called after migrating to the latest version.
            Seeder.Seed(context);

            base.Seed(context); 
        }
    }
}
