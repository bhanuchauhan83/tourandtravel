using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{

    public class HomePageSections
    {
        public HomePageSections()
        {
            Testimonials = new Testimonials();
            BestDeals = new BestDeals();
            RecentJourney = new RecentJourney();
        }
        public Testimonials Testimonials { get; set; }
        public BestDeals BestDeals { get; set; }
        public RecentJourney RecentJourney { get; set; }
    }

    public class Testimonials: BaseHomePageSection
    {      
        public string BackgroundImageURL { get; set; }       
        public string Testimonial1_Description { get; set; }       
        public string Testimonial1_UserName { get; set; }      
        public string Testimonial1_UserCountry { get; set; }       
        public string Testimonial1_UserImgURL { get; set; }     
        public string Testimonial2_Description { get; set; }       
        public string Testimonial2_UserName { get; set; }        
        public string Testimonial2_UserCountry { get; set; }        
        public string Testimonial2_UserImgURL { get; set; }     
        public string Testimonial3_Description { get; set; }
        public string Testimonial3_UserName { get; set; }        
        public string Testimonial3_UserCountry { get; set; }       
        public string Testimonial3_UserImgURL { get; set; }
    }

    public class BestDeals: BaseHomePageSection
    {        
        public string KnowMoreLink { get; set; }        
        public string DiscountBannerImageURL { get; set; }        
        public string DiscountBannerDescription { get; set; }        
        public string BestDeal1_Name { get; set; }        
        public decimal BestDeal1_Price { get; set; }       
        public string BestDeal1_ImageURL { get; set; }      
        public string BestDeal2_Name { get; set; }        
        public decimal BestDeal2_Price { get; set; }        
        public string BestDeal2_ImageURL { get; set; }       
        public string BestDeal3_Name { get; set; }       
        public decimal BestDeal3_Price { get; set; }        
        public string BestDeal3_ImageURL { get; set; }       
        public string BestDeal4_Name { get; set; }       
        public decimal BestDeal4_Price { get; set; }       
        public string BestDeal4_ImageURL { get; set; }
        public string Title1 { get; set; }
        public string Title2 { get; set; }
        public string Package { get; set; }
    }

    public class RecentJourney: BaseHomePageSection
    {        
        public string BackgroundImageURL { get; set; }       
        public string Video1URL { get; set; }       
        public string Video2URL { get; set; }     
        public string Video3URL { get; set; }      
    }

    public class BaseHomePageSection
    {        
        public string SmallTitle { get; set; }     
        public string LargeTitle { get; set; }        
        public string Description { get; set; }        
    }
}
