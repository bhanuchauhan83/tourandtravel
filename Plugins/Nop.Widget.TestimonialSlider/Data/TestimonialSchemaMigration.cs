using FluentMigrator;
using Nop.Data.Migrations;
using Nop.Widget.TestimonialSlider.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Data
{
    [SkipMigrationOnUpdate]
    [NopMigration("2021/07/16 01:39:17:6455442", "Widget.TestimonialSlider base schema")]

    public class TestimonialSchemaMigration : AutoReversingMigration
    {
        #region Fields

        protected IMigrationManager _migrationManager;

        #endregion

        #region Ctor

        public TestimonialSchemaMigration(IMigrationManager migrationManager)
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
            _migrationManager.BuildTable<Testimonials>(Create);         
        }

        #endregion
    }
}
