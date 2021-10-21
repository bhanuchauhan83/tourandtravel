using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{
    public class ConfigurationModel
    {
        public ConfigurationModel()
        {
            Testimonial = new TestimonialsModel();
            BestDeals = new BestDealsModel();
            RecentJourney = new RecentJourneyModel();
            BlogPost = new BlogPostModel();
        }
        public TestimonialsModel Testimonial { get; set; }
        public BestDealsModel BestDeals { get; set; }
        public RecentJourneyModel RecentJourney { get; set; }
        public BlogPostModel BlogPost { get; set; }
        public RecentJourneySearchModel SearchModel { get; set; }

    }

    #region Refrence Models

    public record RecentJourneySearchModel : BaseSearchModel
    {

    }

    public class RecentJourneyModel : BaseHomePageSectionModel
    {
        public RecentJourneyModel()
        {
            JourneyVideosModel = new RecentJourneyVideosModel();
        }
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BackgroundImageId")]
        [UIHint("Picture")]
        public int BackgroundImageId { get; set; }
        public bool BackgroundImageId_OverrideForStore { get; set; }   
        
        public RecentJourneyVideosModel JourneyVideosModel { get; set; }
    }

    public class BestDealsModel : BaseHomePageSectionModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.KnowMoreLink")]
        [Required]
        public string KnowMoreLink { get; set; }
        public bool KnowMoreLink_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBannerId")]
        [UIHint("Picture")]
        public int DiscountBannerId { get; set; }
        public bool DiscountBannerId_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBannerDescription")]
        [Required]
        public string DiscountBannerDescription { get; set; }
        public bool DiscountBannerDescription_OverrideForStore { get; set; }


        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Title1")]
        [Required]
        public string Title1 { get; set; }
        public bool Title1_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Title2")]
        [Required]
        public string Title2 { get; set; }
        public bool Title2_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Package")]
        [Required]
        public string Package { get; set; }
        public bool Package_OverrideForStore { get; set; }

    }

    //public class TestimonialModel : BaseHomePageSectionModel
    //{
    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BackgroundImageId")]
    //    [UIHint("Picture")]  
    //    public int BackgroundImageId { get; set; }
    //    public bool BackgroundImageId_OverrideForStore { get; set; }


    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description")]
    //    [Required]
    //    public string Testimonial1_Description { get; set; }
    //    public bool Testimonial1_Description_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName")]
    //    [Required]
    //    public string Testimonial1_UserName { get; set; }
    //    public bool Testimonial1_UserName_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry")]
    //    [Required]
    //    public string Testimonial1_UserCountry { get; set; }
    //    public bool Testimonial1_UserCountry_OverrideForStore { get; set; }

    //    [UIHint("Picture")]
    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg")]
    //    public int Testimonial1_UserImg { get; set; }
    //    public bool Testimonial1_UserImg_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description")]
    //    [Required]
    //    public string Testimonial2_Description { get; set; }
    //    public bool Testimonial2_Description_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName")]
    //    [Required]
    //    public string Testimonial2_UserName { get; set; }
    //    public bool Testimonial2_UserName_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry")]
    //    [Required]
    //    public string Testimonial2_UserCountry { get; set; }
    //    public bool Testimonial2_UserCountry_OverrideForStore { get; set; }

    //    [UIHint("Picture")]
    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg")]
    //    public int Testimonial2_UserImg { get; set; }
    //    public bool Testimonial2_UserImg_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description")]
    //    [Required]
    //    public string Testimonial3_Description { get; set; }
    //    public bool Testimonial3_Description_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName")]
    //    [Required]
    //    public string Testimonial3_UserName { get; set; }
    //    public bool Testimonial3_UserName_OverrideForStore { get; set; }

    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry")]
    //    [Required]
    //    public string Testimonial3_UserCountry { get; set; }
    //    public bool Testimonial3_UserCountry_OverrideForStore { get; set; }

    //    [UIHint("Picture")]
    //    [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg")]
    //    public int Testimonial3_UserImg { get; set; }
    //    public bool Testimonial3_UserImg_OverrideForStore { get; set; }
    //}

    public class BlogPostModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.SmallTitle")]
        [Required]
        public string SmallTitle { get; set; }
        public bool SmallTitle_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.LargeTitle")]
        [Required]
        public string LargeTitle { get; set; }
        public bool LargeTitle_OverrideForStore { get; set; }


        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.CouponBoxTitle")]
        [Required]
        public string CouponBoxTitle { get; set; }
        public bool CouponBoxTitle_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.CouponBoxDescription")]
        [Required]
        public string CouponBoxDescription { get; set; }
        public bool CouponBoxDescription_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBanner")]
        [UIHint("Picture")]
        public int DiscountBanner { get; set; }
        public bool DiscountBanner_OverrideForStore { get; set; }

        public int ActiveStoreScopeConfiguration { get; set; }
    }

    public class BaseHomePageSectionModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.SmallTitle")]
        [Required]
        public string SmallTitle { get; set; }
        public bool SmallTitle_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.LargeTitle")]
        [Required]
        public string LargeTitle { get; set; }
        public bool LargeTitle_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Description")]
        [Required]
        public string Description { get; set; }
        public bool Description_OverrideForStore { get; set; }

        public int ActiveStoreScopeConfiguration { get; set; }
    }
    #endregion


    public class TestimonialsModel : BaseHomePageSectionModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BackgroundImageId")]
        public int BackgroundImageId { get; set; }
        public bool BackgroundImageId_OverrideForStore { get; set; }

    }

    public record RecentJourneyVideosModel: BaseNopEntityModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL")]
       
        public string VideoURL { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Published")]
        public bool Published { get; set; }      
    }

    public partial record RecentJourneyVideosModelList : BasePagedListModel<RecentJourneyVideosModel>
    {

    }
}
