using System;
using System.Configuration;

namespace ProjectCureData.Models.Migrations
{
    internal static class MigrationSettings
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        static MigrationSettings()
        {
            var enableDataMigrations = ConfigurationManager.AppSettings["EnableDataMigrations"];
            var enableMigrationDataLoss = ConfigurationManager.AppSettings["EnableMigrationDataLoss"];
            var enableMigrationDataSeed = ConfigurationManager.AppSettings["EnableMigrationDataSeed"];

            MigrationSettings.MigrationsEnabled = !String.IsNullOrWhiteSpace(enableDataMigrations) ? Convert.ToBoolean(enableDataMigrations) : false;
            MigrationSettings.MigrationDataLossAllowed = !String.IsNullOrWhiteSpace(enableMigrationDataLoss) ? Convert.ToBoolean(enableMigrationDataLoss) : false;
            MigrationSettings.MigrationDataSeed = !String.IsNullOrWhiteSpace(enableMigrationDataSeed) ? Convert.ToBoolean(enableMigrationDataSeed) : false;
        }

        #endregion

        #region Member Variables
        #endregion

        #region Properties

        /// <summary>
        /// Determines of Migrations is Enabled
        /// </summary>
        public static bool MigrationsEnabled { get; private set; }

        /// <summary>
        /// Detemines is dataloss enabled in automatic migrations...(THIS SHOULD ONLY BE AVAILABLE IN DEVELOPMENT)
        /// </summary>
        public static bool MigrationDataLossAllowed { get; private set; }

        /// <summary>
        /// Detemines is dataloss enabled in automatic migrations...(THIS SHOULD ONLY BE AVAILABLE IN DEVELOPMENT)
        /// </summary>
        public static bool MigrationDataSeed { get; private set; }

        #endregion

        #region Methods
        #endregion

        #region Events
        #endregion
    }
}
