using Microsoft.AspNetCore.Mvc;
using Nop.Widget.TourAndTravelHomePage.Factories;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NopWidget.TourAndTravelHomePage.Components
{
    public class DayNightViewComponent : NopViewComponent
    {

        #region Fields

        private readonly ITravelPackageModelFactory _travelPackageModelFactory;
        #endregion

        #region Ctor

        public DayNightViewComponent(ITravelPackageModelFactory travelPackageModelFactory)
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
                if (model.Packages.Days > 0 || model.Packages.Nights > 0)
                {
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/DayNight/Default.cshtml", model.Packages);
                }
               
            }
            return Content(string.Empty);
        }

        #endregion
    }
}
