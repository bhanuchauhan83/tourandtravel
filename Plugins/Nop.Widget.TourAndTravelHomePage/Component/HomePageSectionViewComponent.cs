using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Factories;
using Nop.Widget.TourAndTravelHomePage.Models;
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
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/HomePageSection/Testimonial.cshtml", model.Testimonials);
            }
            if (widgetZone == TourAndTravelDefault.BestDealSection)
            {
                if (!string.IsNullOrEmpty(model.BestDeals.SmallTitle))
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/HomePageSection/BestDeals.cshtml", model.BestDeals);
            }
            if (widgetZone == TourAndTravelDefault.RecentJourneySection)
            {
                if (!string.IsNullOrEmpty(model.RecentJourney.SmallTitle))
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/HomePageSection/RecentJourney.cshtml", model.RecentJourney);
            }
            if (widgetZone == TourAndTravelDefault.BlogPostSection)
            {
                if (!string.IsNullOrEmpty(model.BlogPost.SmallTitle))
                    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/HomePageSection/BlogPost.cshtml", model.BlogPost);
            }
            //if (widgetZone == TourAndTravelDefault.SearchForTripSection)
            //{
            //    var searchTripModel = new SearchForTripModel();
            //    return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Component/HomePageSection/SearchForTrip.cshtml", searchTripModel);
            //}
            return Content(string.Empty);
        }

    }
}
