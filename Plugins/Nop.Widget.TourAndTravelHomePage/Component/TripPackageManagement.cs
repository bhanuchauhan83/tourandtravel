using Microsoft.AspNetCore.Mvc;
using Nop.Widget.TourAndTravelHomePage.Factories;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Components
{

    [ViewComponent(Name = TourAndTravelDefault.VIEW_COMPONENT_TRIP_PACKAGES_MANGEMENT)]
    public class TripPackageManagementViewComponent: NopViewComponent
    {

        #region Fields

        private readonly ITravelPackageModelFactory _travelPackageModelFactory;
        #endregion

        #region Ctor

        public TripPackageManagementViewComponent(ITravelPackageModelFactory travelPackageModelFactory)
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
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, ProductModel additionalData)
        {
           // if (additionalData.Id > 0)
            //{
                var model = await _travelPackageModelFactory.PrepareTravelPackageModelAsync(additionalData.Id);
                return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/TripPackageManagement/Default.cshtml", model);
            //}
         //   return Content(string.Empty);
        }

        #endregion
    }
}
