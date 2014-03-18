using System.Data.Entity.Migrations;

namespace ProjectCureData.Models.Migrations
{
    /// <summary>
    /// Database Migrations strategy configuration
    /// </summary>
    public class ProjectCureDbMigrationsConfiguration : DbMigrationsConfiguration<ProjectCureContext>
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public ProjectCureDbMigrationsConfiguration()
        {
            this.AutomaticMigrationsEnabled = MigrationSettings.MigrationsEnabled;
            this.AutomaticMigrationDataLossAllowed = MigrationSettings.MigrationDataLossAllowed;            
        }

        #endregion

        #region Member Variables
        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ProjectCureContext context)
        {
            //  This method will be called after migrating to the latest version.
            if (MigrationSettings.MigrationDataSeed)
                Seeder.Seed(context);

            base.Seed(context);
        }

        #endregion

        #region Events
        #endregion
    }
}
