using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Widget.TourAndTravelHomePage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Data
{

    [SkipMigrationOnUpdate]
    [NopMigration("2021/07/06 01:39:17:6455442", "Plugin.Widget.TripMangement base schema")]
    public class TripManagementSchemaMigration : AutoReversingMigration
    {
        #region Fields

        protected IMigrationManager _migrationManager;

        #endregion

        #region Ctor

        public TripManagementSchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            _migrationManager.BuildTable<TravelPackage>(Create);
            _migrationManager.BuildTable<CustomerPackageMapping>(Create);
            _migrationManager.BuildTable<BestDealCategory>(Create);
            _migrationManager.BuildTable<RecentJourneyVideos>(Create);
        }

        #endregion
    }
}
