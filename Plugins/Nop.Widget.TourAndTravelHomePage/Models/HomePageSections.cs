using Nop.Web.Areas.Admin.Models.Catalog;
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
            Testimonials = new TestimonialHomePageSectionModel();
            BestDeals = new BestDeals();
            RecentJourney = new RecentJourney();
            BlogPost = new BlogPost();
        }
        public TestimonialHomePageSectionModel Testimonials { get; set; }
        public BestDeals BestDeals { get; set; }
        public RecentJourney RecentJourney { get; set; }
        public BlogPost BlogPost { get; set; }
    }

    public class TestimonialHomePageSectionModel : BaseHomePageSection
    {
        public string BackgroundImageURL { get; set; }
     
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

        public bool IsAnyBestDealExist { get; set; }
    }

    public class RecentJourney: BaseHomePageSection
    {
        public RecentJourney()
        {
            RecentJourneyVideos= new List<RecentJourneyVideosModel>();
        }
        public string BackgroundImageURL { get; set; } 
        
        public List<RecentJourneyVideosModel> RecentJourneyVideos { get; set; }
    }

    public class BlogPost: BaseHomePageSection
    {
        public string CouponBoxTitle { get; set; }
        public string CouponBoxDescription { get; set; }
        public string DiscountBannerURL { get; set; }
    }

    public class BaseHomePageSection
    {        
        public string SmallTitle { get; set; }     
        public string LargeTitle { get; set; }        
        public string Description { get; set; }        
    }
}
