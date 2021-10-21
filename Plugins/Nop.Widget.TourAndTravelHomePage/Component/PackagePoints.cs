using Microsoft.AspNetCore.Mvc;
using Nop.Widget.TourAndTravelHomePage.Factories;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Components
{
    public class PackagePointsViewComponent : NopViewComponent
    {

        #region Fields

        private readonly ITravelPackageModelFactory _travelPackageModelFactory;
        #endregion

        #region Ctor

        public PackagePointsViewComponent(ITravelPackageModelFactory travelPackageModelFactory)
        {
            _travelPackageModelFactory = travelPackageModelFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoke the widget view component
        /// </summary>
        /// <param name="widgetZone">Widget zone</param>
        /// <param name="additionalData">Additional parameters</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the view component result
        /// </returns>
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, int additionalData)
        {
            if (additionalData > 0)
            {
                var model = await _travelPackageModelFactory.PrepareTravelPackageModelAsync(additionalData);

                // if any of one is true then only show the package icons
                if (model.Packages.IncludeBus || model.Packages.IncludeCabs || model.Packages.IncludeCityTour || model.Packages.IncludeFlights
                    || model.Packages.IncludeHotel || model.Packages.IncludeTrain)
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/PackagePoints/Default.cshtml", model);
            }
            return Content(string.Empty);
        }

        #endregion
    }
}
