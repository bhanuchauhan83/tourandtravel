using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Factories
{
    public class TourAndTravelModelFactory : ITourAndTravelModelFactory
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;

        public TourAndTravelModelFactory(IStoreContext storeContext, ISettingService settingService,
            IPictureService pictureService)
        {
            _settingService = settingService;
            _storeContext = storeContext;
            _pictureService = pictureService;
        }

        public async Task<ConfigurationModel> PrepareConfigurationModelAsync()
        {
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var bestDealSetting = await _settingService.LoadSettingAsync<BestDealsSettings>(storeId);
            var recentJourneySetting = await _settingService.LoadSettingAsync<RecentJourneySettings>(storeId);
            var testimonialSetting = await _settingService.LoadSettingAsync<TestimonialSettings>(storeId);
            var model = new ConfigurationModel()
            {
                BestDeals = new BestDealsModel()
                {
                    Description = bestDealSetting.Description,
                    DiscountBannerDescription = bestDealSetting.DiscountBannerDescription,
                    DiscountBannerId = bestDealSetting.DiscountBannerId,
                    KnowMoreLink = bestDealSetting.KnowMoreLink,
                    LargeTitle = bestDealSetting.LargeTitle,
                    SmallTitle = bestDealSetting.SmallTitle,
                    BestDeal1_Image = bestDealSetting.BestDeal1_Image,
                    BestDeal1_Name = bestDealSetting.BestDeal1_Name,
                    BestDeal1_Price = bestDealSetting.BestDeal1_Price,
                    BestDeal2_Image = bestDealSetting.BestDeal2_Image,
                    BestDeal2_Name = bestDealSetting.BestDeal2_Name,
                    BestDeal2_Price = bestDealSetting.BestDeal2_Price,
                    BestDeal3_Image = bestDealSetting.BestDeal3_Image,
                    BestDeal3_Name = bestDealSetting.BestDeal3_Name,
                    BestDeal3_Price = bestDealSetting.BestDeal3_Price,
                    BestDeal4_Image = bestDealSetting.BestDeal4_Image,
                    BestDeal4_Name = bestDealSetting.BestDeal4_Name,
                    BestDeal4_Price = bestDealSetting.BestDeal4_Price,
                    Title1 = bestDealSetting.Title1,
                    Title2 = bestDealSetting.Title2,
                    Package = bestDealSetting.Package
                },
                RecentJourney = new RecentJourneyModel()
                {
                    Description = recentJourneySetting.Description,
                    LargeTitle = recentJourneySetting.LargeTitle,
                    SmallTitle = recentJourneySetting.SmallTitle,
                    BackgroundImageId = recentJourneySetting.BackgroundImageId,
                    Video1URL = recentJourneySetting.Video1URL,
                    Video2URL = recentJourneySetting.Video2URL,
                    Video3URL = recentJourneySetting.Video3URL
                },
                Testimonial = new TestimonialModel()
                {
                    BackgroundImageId = testimonialSetting.BackgroundImageId,
                    Description = testimonialSetting.Description,
                    LargeTitle = testimonialSetting.LargeTitle,
                    SmallTitle = testimonialSetting.SmallTitle,
                    Testimonial1_Description = testimonialSetting.Testimonial1_Description,
                    Testimonial1_UserCountry = testimonialSetting.Testimonial1_UserCountry,
                    Testimonial1_UserImg = testimonialSetting.Testimonial1_UserImg,
                    Testimonial1_UserName = testimonialSetting.Testimonial1_UserName,
                    Testimonial2_Description = testimonialSetting.Testimonial2_Description,
                    Testimonial2_UserCountry = testimonialSetting.Testimonial2_UserCountry,
                    Testimonial2_UserImg = testimonialSetting.Testimonial2_UserImg,
                    Testimonial2_UserName = testimonialSetting.Testimonial2_UserName,
                    Testimonial3_Description = testimonialSetting.Testimonial3_Description,
                    Testimonial3_UserCountry = testimonialSetting.Testimonial3_UserCountry,
                    Testimonial3_UserImg = testimonialSetting.Testimonial3_UserImg,
                    Testimonial3_UserName = testimonialSetting.Testimonial3_UserName
                }

            };
            return model;
        }

        public async Task<HomePageSections> PrepareHomePageSectionsModelAsync()
        {
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var bestDealSetting = await _settingService.LoadSettingAsync<BestDealsSettings>(storeId);
            var recentJourneySetting = await _settingService.LoadSettingAsync<RecentJourneySettings>(storeId);
            var testimonialSetting = await _settingService.LoadSettingAsync<TestimonialSettings>(storeId);
            var model = new HomePageSections()
            {
                BestDeals = new BestDeals()
                {
                    Description = bestDealSetting.Description,
                    DiscountBannerDescription = bestDealSetting.DiscountBannerDescription,
                    DiscountBannerImageURL = bestDealSetting.DiscountBannerId > 0 ? await _pictureService.GetPictureUrlAsync(bestDealSetting.DiscountBannerId) : string.Empty,
                    KnowMoreLink = bestDealSetting.KnowMoreLink,
                    LargeTitle = bestDealSetting.LargeTitle,
                    SmallTitle = bestDealSetting.SmallTitle,
                    BestDeal1_ImageURL = bestDealSetting.BestDeal1_Image > 0 ? await _pictureService.GetPictureUrlAsync(bestDealSetting.BestDeal1_Image) : string.Empty,
                    BestDeal1_Name = bestDealSetting.BestDeal1_Name,
                    BestDeal1_Price = bestDealSetting.BestDeal1_Price,
                    BestDeal2_ImageURL = bestDealSetting.BestDeal2_Image > 0 ? await _pictureService.GetPictureUrlAsync(bestDealSetting.BestDeal2_Image) : string.Empty,
                    BestDeal2_Name = bestDealSetting.BestDeal2_Name,
                    BestDeal2_Price = bestDealSetting.BestDeal2_Price,
                    BestDeal3_ImageURL = bestDealSetting.BestDeal3_Image > 0 ? await _pictureService.GetPictureUrlAsync(bestDealSetting.BestDeal3_Image) : string.Empty,
                    BestDeal3_Name = bestDealSetting.BestDeal3_Name,
                    BestDeal3_Price = bestDealSetting.BestDeal3_Price,
                    BestDeal4_ImageURL = bestDealSetting.BestDeal4_Image > 0 ? await _pictureService.GetPictureUrlAsync(bestDealSetting.BestDeal4_Image) : string.Empty,
                    BestDeal4_Name = bestDealSetting.BestDeal4_Name,
                    BestDeal4_Price = bestDealSetting.BestDeal4_Price,
                    Title1 = bestDealSetting.Title1,
                    Title2 = bestDealSetting.Title2,
                    Package = bestDealSetting.Package
                },
                RecentJourney = new RecentJourney()
                {
                    Description = recentJourneySetting.Description,
                    LargeTitle = recentJourneySetting.LargeTitle,
                    SmallTitle = recentJourneySetting.SmallTitle,
                    BackgroundImageURL = recentJourneySetting.BackgroundImageId > 0 ? await _pictureService.GetPictureUrlAsync(recentJourneySetting.BackgroundImageId) : string.Empty,
                    Video1URL = recentJourneySetting.Video1URL,
                    Video2URL = recentJourneySetting.Video2URL,
                    Video3URL = recentJourneySetting.Video3URL
                },
                Testimonials = new Testimonials()
                {
                    BackgroundImageURL = testimonialSetting.BackgroundImageId > 0 ? await _pictureService.GetPictureUrlAsync(testimonialSetting.BackgroundImageId) : string.Empty,
                    Description = testimonialSetting.Description,
                    LargeTitle = testimonialSetting.LargeTitle,
                    SmallTitle = testimonialSetting.SmallTitle,
                    Testimonial1_Description = testimonialSetting.Testimonial1_Description,
                    Testimonial1_UserCountry = testimonialSetting.Testimonial1_UserCountry,
                    Testimonial1_UserImgURL = testimonialSetting.Testimonial1_UserImg > 0 ? await _pictureService.GetPictureUrlAsync(testimonialSetting.Testimonial1_UserImg) : string.Empty,
                    Testimonial1_UserName = testimonialSetting.Testimonial1_UserName,
                    Testimonial2_Description = testimonialSetting.Testimonial2_Description,
                    Testimonial2_UserCountry = testimonialSetting.Testimonial2_UserCountry,
                    Testimonial2_UserImgURL = testimonialSetting.Testimonial2_UserImg > 0 ? await _pictureService.GetPictureUrlAsync(testimonialSetting.Testimonial2_UserImg) : string.Empty,
                    Testimonial2_UserName = testimonialSetting.Testimonial2_UserName,
                    Testimonial3_Description = testimonialSetting.Testimonial3_Description,
                    Testimonial3_UserCountry = testimonialSetting.Testimonial3_UserCountry,
                    Testimonial3_UserImgURL = testimonialSetting.Testimonial3_UserImg > 0 ? await _pictureService.GetPictureUrlAsync(testimonialSetting.Testimonial3_UserImg) : string.Empty,
                    Testimonial3_UserName = testimonialSetting.Testimonial3_UserName
                }

            };
            return model;
        }
    }
}
