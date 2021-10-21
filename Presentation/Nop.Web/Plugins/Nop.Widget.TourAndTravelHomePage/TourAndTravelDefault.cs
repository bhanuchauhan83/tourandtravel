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

        //Widgets
        public static string TestimonialsSection = "TestimonialsSection";
        public static string BestDealSection = "BestDealSection";
        public static string RecentJourneySection = "RecentJourneySection";

        public const string DefaultStringResource = "Nop.Widget.TourAndTravelHomePage";


        public static List<string> WidgetList()
        {
            var list = new List<string>();
            list.Add(TestimonialsSection);
            list.Add(BestDealSection);
            list.Add(RecentJourneySection);
            return list;
        }
    }
}
