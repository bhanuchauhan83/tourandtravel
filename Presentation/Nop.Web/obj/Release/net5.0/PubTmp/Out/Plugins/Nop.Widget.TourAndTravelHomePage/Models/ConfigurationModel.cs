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
            Testimonial = new TestimonialModel();
            BestDeals = new BestDealsModel();
            RecentJourney = new RecentJourneyModel();
        }
        public TestimonialModel Testimonial { get; set; }
        public BestDealsModel BestDeals { get; set; }
        public RecentJourneyModel RecentJourney { get; set; }
    }

    #region Refrence Models

    public class RecentJourneyModel : BaseHomePageSectionModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BackgroundImageId")]
        [UIHint("Picture")]
        public int BackgroundImageId { get; set; }
        public bool BackgroundImageId_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL")]
        public string Video1URL { get; set; }
        public bool Video1URL_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL")]
        public string Video2URL { get; set; }
        public bool Video2URL_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL")]
        public string Video3URL { get; set; }
        public bool Video3URL_OverrideForStore { get; set; }
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


        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name")]
        [Required]
        public string BestDeal1_Name { get; set; }
        public bool BestDeal1_Name_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price")]
        [Required]
        public decimal BestDeal1_Price { get; set; }
        public bool BestDeal1_Price_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Image")]
        [UIHint("Picture")]
        public int BestDeal1_Image { get; set; }
        public bool BestDeal1_Image_OverrideForStore { get; set; }


        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name")]
        [Required]
        public string BestDeal2_Name { get; set; }
        public bool BestDeal2_Name_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price")]
        [Required]
        public decimal BestDeal2_Price { get; set; }
        public bool BestDeal2_Price_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Image")]
        [UIHint("Picture")]
        public int BestDeal2_Image { get; set; }
        public bool BestDeal2_Image_OverrideForStore { get; set; }


        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name")]
        [Required]
        public string BestDeal3_Name { get; set; }
        public bool BestDeal3_Name_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price")]
        [Required]
        public decimal BestDeal3_Price { get; set; }
        public bool BestDeal3_Price_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Image")]
        [UIHint("Picture")]
        public int BestDeal3_Image { get; set; }
        public bool BestDeal3_Image_OverrideForStore { get; set; }


        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name")]
        [Required]
        public string BestDeal4_Name { get; set; }
        public bool BestDeal4_Name_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price")]
        [Required]
        public decimal BestDeal4_Price { get; set; }
        public bool BestDeal4_Price_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Image")]
        [UIHint("Picture")]
        public int BestDeal4_Image { get; set; }
        public bool BestDeal4_Image_OverrideForStore { get; set; }
    }

    public class TestimonialModel : BaseHomePageSectionModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.BackgroundImageId")]
        [UIHint("Picture")]  
        public int BackgroundImageId { get; set; }
        public bool BackgroundImageId_OverrideForStore { get; set; }


        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description")]
        [Required]
        public string Testimonial1_Description { get; set; }
        public bool Testimonial1_Description_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName")]
        [Required]
        public string Testimonial1_UserName { get; set; }
        public bool Testimonial1_UserName_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry")]
        [Required]
        public string Testimonial1_UserCountry { get; set; }
        public bool Testimonial1_UserCountry_OverrideForStore { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg")]
        public int Testimonial1_UserImg { get; set; }
        public bool Testimonial1_UserImg_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description")]
        [Required]
        public string Testimonial2_Description { get; set; }
        public bool Testimonial2_Description_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName")]
        [Required]
        public string Testimonial2_UserName { get; set; }
        public bool Testimonial2_UserName_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry")]
        [Required]
        public string Testimonial2_UserCountry { get; set; }
        public bool Testimonial2_UserCountry_OverrideForStore { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg")]
        public int Testimonial2_UserImg { get; set; }
        public bool Testimonial2_UserImg_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description")]
        [Required]
        public string Testimonial3_Description { get; set; }
        public bool Testimonial3_Description_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName")]
        [Required]
        public string Testimonial3_UserName { get; set; }
        public bool Testimonial3_UserName_OverrideForStore { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry")]
        [Required]
        public string Testimonial3_UserCountry { get; set; }
        public bool Testimonial3_UserCountry_OverrideForStore { get; set; }

        [UIHint("Picture")]
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg")]
        public int Testimonial3_UserImg { get; set; }
        public bool Testimonial3_UserImg_OverrideForStore { get; set; }
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
}
