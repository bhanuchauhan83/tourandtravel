using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage
{
    public class TourAndTravelSettings
    {
        public TourAndTravelSettings()
        {
            Testimonial = new TestimonialSettings();
            RecentJourney = new RecentJourneySettings();
            BestDeals = new BestDealsSettings();
        }
        public TestimonialSettings Testimonial { get; set; }
        public RecentJourneySettings RecentJourney { get; set; }
        public BestDealsSettings BestDeals { get; set; }

        public BlogPostSettings BlogPost { get; set; }
    }

    public class RecentJourneySettings : BaseTourAndTravelSettings
    {
        public int BackgroundImageId { get; set; }       
    }

    public class TestimonialSettings: BaseTourAndTravelSettings
    {
        public int BackgroundImageId { get; set; }        
    }

    public class BestDealsSettings: BaseTourAndTravelSettings
    {        
        public string KnowMoreLink { get; set; }
        public int DiscountBannerId { get; set; }     
        public string DiscountBannerDescription { get; set; }     
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Package { get; set; }       
    }

    public class BlogPostSettings : ISettings
    {
        public string SmallTitle { get; set; }
        public string LargeTitle { get; set; }
        public string CouponBoxTitle { get; set; }        
        public string CouponBoxDescription { get; set; }
        public int DiscountBanner { get; set; }
    }

    public class BaseTourAndTravelSettings :ISettings
    {
        public string SmallTitle { get; set; }
        public string LargeTitle { get; set; }
        public string Description { get; set; }
    }

}
