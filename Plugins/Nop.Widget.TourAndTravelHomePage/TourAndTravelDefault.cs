using Nop.Web.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage
{
    public static class TourAndTravelDefault
    {
        public const string HomePageSectionComponent = DefaultStringResource + ".HomePageSectionComponent";
        public const string TourAndTravelOrderSummaryComponent = "TourAndTravelOrderSummary";


        public const string VIEW_COMPONENT_TRIP_PACKAGES_MANGEMENT = "TripManagementPackages";
        public const string VIEW_COMPONENT_TRAVELLERDETAILS = "TravellerDetails";
        public const string VIEW_COMPONENT_BESTDEAL = "BestDeal";

        public const string StringResourcePrefix = "Nop.Plugin.Widget.TripMangement";

        public static string PackageManagementTabName = StringResourcePrefix + ".PackageManagement";

        //Widgets
        public static string TestimonialsSection = "TestimonialsSection";
        public static string BestDealSection = "BestDealSection";
        public static string RecentJourneySection = "RecentJourneySection";
        public static string BlogPostSection = "BlogPostSection";
        public static string SearchForTripSection = "SearchForTripSection";
        public static string PackageTemplate = "Package Template";


        public const string DefaultStringResource = "Nop.Widget.TourAndTravelHomePage";

        public static string TourAndTravelCheckout_Route = "Nop.Plugin.TourAndTravelCheckout.CheckoutPageRoute";
        public static string TourAndTravelOnePageCheckout_Route = "Nop.Plugin.TourAndTravelCheckout.OnePageCheckoutRoute";
        public static List<string> WidgetList()
        {
            var list = new List<string>();
            list.Add(TestimonialsSection);
            list.Add(BestDealSection);
            list.Add(RecentJourneySection);
            list.Add(BlogPostSection);
            list.Add(AdminWidgetZones.ProductDetailsBlock);
            list.Add(AdminWidgetZones.OrderDetailsBlock);
            list.Add(AdminWidgetZones.CategoryDetailsBlock);
            //list.Add(SearchForTripSection);
            return list;
        }
    }
}
