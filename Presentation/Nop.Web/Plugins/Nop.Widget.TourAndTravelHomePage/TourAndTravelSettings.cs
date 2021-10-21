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
    }

    public class RecentJourneySettings : BaseTourAndTravelSettings
    {
        public int BackgroundImageId { get; set; }
        public string Video1URL { get; set; }
        public string Video2URL { get; set; }
        public string Video3URL { get; set; }
    }

    public class TestimonialSettings: BaseTourAndTravelSettings
    {
        public int BackgroundImageId { get; set; } 
        public string Testimonial1_Description { get; set; }  
        public string Testimonial1_UserName { get; set; }
        public string Testimonial1_UserCountry { get; set; }
        public int Testimonial1_UserImg { get; set; }
        public string Testimonial2_Description { get; set; }
        public string Testimonial2_UserName { get; set; }
        public string Testimonial2_UserCountry { get; set; }
        public int Testimonial2_UserImg { get; set; }
        public string Testimonial3_Description { get; set; }
        public string Testimonial3_UserName { get; set; }
        public string Testimonial3_UserCountry { get; set; }
        public int Testimonial3_UserImg { get; set; }
    }

    public class BestDealsSettings: BaseTourAndTravelSettings
    {        
        public string KnowMoreLink { get; set; }
        public int DiscountBannerId { get; set; }     
        public string DiscountBannerDescription { get; set; }     
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Package { get; set; }
        public string BestDeal1_Name { get; set; }  
        public decimal BestDeal1_Price { get; set; }
        public int BestDeal1_Image { get; set; }
        public string BestDeal2_Name { get; set; }     
        public decimal BestDeal2_Price { get; set; }
        public int BestDeal2_Image { get; set; }
        public string BestDeal3_Name { get; set; }  
        public decimal BestDeal3_Price { get; set; }
        public int BestDeal3_Image { get; set; }
        public string BestDeal4_Name { get; set; }
        public decimal BestDeal4_Price { get; set; }
        public int BestDeal4_Image { get; set; }
    }

    public class BaseTourAndTravelSettings : ISettings
    {
        public string SmallTitle { get; set; }
        public string LargeTitle { get; set; }
        public string Description { get; set; }
    }

}
