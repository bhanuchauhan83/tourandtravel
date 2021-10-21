using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Component
{
    [ViewComponent(Name = TourAndTravelDefault.HomePageSectionComponent)]
    public class HomePageSectionViewComponent : NopViewComponent
    {
        private readonly ITourAndTravelModelFactory _tourAndTravelModelFactory;

        public HomePageSectionViewComponent(ITourAndTravelModelFactory tourAndTravelModelFactory)
        {
            _tourAndTravelModelFactory = tourAndTravelModelFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var model = await _tourAndTravelModelFactory.PrepareHomePageSectionsModelAsync();
            
            if (widgetZone == TourAndTravelDefault.TestimonialsSection)
            {
                if (!string.IsNullOrEmpty(model.Testimonials.SmallTitle))
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Component/HomePageSection/Testimonial.cshtml", model.Testimonials);
            }
            if (widgetZone == TourAndTravelDefault.BestDealSection)
            {
               if (!string.IsNullOrEmpty(model.BestDeals.SmallTitle))
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Component/HomePageSection/BestDeals.cshtml", model.BestDeals);
            }
            if (widgetZone == TourAndTravelDefault.RecentJourneySection)
            {
                if (!string.IsNullOrEmpty(model.RecentJourney.SmallTitle))
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Component/HomePageSection/RecentJourney.cshtml", model.RecentJourney);
            }
            return Content(string.Empty);
        }

    }
}
