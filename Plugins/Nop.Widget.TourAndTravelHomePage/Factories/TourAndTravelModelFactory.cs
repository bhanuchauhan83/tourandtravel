using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
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
        private readonly IBestDealService _bestDealService;
        private readonly IRecentJourneyService _recentJourneyService;


        public TourAndTravelModelFactory(IStoreContext storeContext, ISettingService settingService,
            IPictureService pictureService, IBestDealService bestDealService,
            IRecentJourneyService recentJourneyService)
        {
            _settingService = settingService;
            _storeContext = storeContext;
            _pictureService = pictureService;
            _bestDealService = bestDealService;
            _recentJourneyService = recentJourneyService;
        }

        public async Task<ConfigurationModel> PrepareConfigurationModelAsync()
        {
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var bestDealSetting = await _settingService.LoadSettingAsync<BestDealsSettings>(storeId);
            var recentJourneySetting = await _settingService.LoadSettingAsync<RecentJourneySettings>(storeId);
            var testimonialSetting = await _settingService.LoadSettingAsync<TestimonialSettings>(storeId);
            var blogPostSetting = await _settingService.LoadSettingAsync<BlogPostSettings>(storeId);
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
                    Title1 = bestDealSetting.Title1,
                    Title2 = bestDealSetting.Title2,
                    Package = bestDealSetting.Package
                },
                RecentJourney = new RecentJourneyModel()
                {
                    Description = recentJourneySetting.Description,
                    LargeTitle = recentJourneySetting.LargeTitle,
                    SmallTitle = recentJourneySetting.SmallTitle,
                    BackgroundImageId = recentJourneySetting.BackgroundImageId
                },
                
                Testimonial = new TestimonialsModel()
                {
                    BackgroundImageId = testimonialSetting.BackgroundImageId,
                    Description = testimonialSetting.Description,
                    LargeTitle = testimonialSetting.LargeTitle,
                    SmallTitle = testimonialSetting.SmallTitle
                },
                BlogPost = new BlogPostModel()
                {
                    CouponBoxDescription = blogPostSetting.CouponBoxDescription,
                    CouponBoxTitle = blogPostSetting.CouponBoxTitle,
                    DiscountBanner = blogPostSetting.DiscountBanner,
                    LargeTitle = blogPostSetting.LargeTitle,
                    SmallTitle = blogPostSetting.SmallTitle
                },
                SearchModel = new RecentJourneySearchModel()

            };
            return model;
        }

        public async Task<HomePageSections> PrepareHomePageSectionsModelAsync()
        {
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var bestDealSetting = await _settingService.LoadSettingAsync<BestDealsSettings>(storeId);
            var recentJourneySetting = await _settingService.LoadSettingAsync<RecentJourneySettings>(storeId);
            var testimonialSetting = await _settingService.LoadSettingAsync<TestimonialSettings>(storeId);
            var blogPostSetting = await _settingService.LoadSettingAsync<BlogPostSettings>(storeId);
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
                    Title1 = bestDealSetting.Title1,
                    Title2 = bestDealSetting.Title2,
                    Package = bestDealSetting.Package,
                    IsAnyBestDealExist = _bestDealService.GetBestDealCatgoriesExistorNot()
                },
                Testimonials = new TestimonialHomePageSectionModel()
                {
                    BackgroundImageURL = testimonialSetting.BackgroundImageId > 0 ? await _pictureService.GetPictureUrlAsync(testimonialSetting.BackgroundImageId) : string.Empty,
                    Description = testimonialSetting.Description,
                    LargeTitle = testimonialSetting.LargeTitle,
                    SmallTitle = testimonialSetting.SmallTitle
                },
                RecentJourney = new RecentJourney()
                {
                    Description = recentJourneySetting.Description,
                    LargeTitle = recentJourneySetting.LargeTitle,
                    SmallTitle = recentJourneySetting.SmallTitle,
                    BackgroundImageURL = recentJourneySetting.BackgroundImageId > 0 ? await _pictureService.GetPictureUrlAsync(recentJourneySetting.BackgroundImageId) : string.Empty,
                    RecentJourneyVideos = _recentJourneyService.GetAllRecentJourneyVideos().Result.Select(x => new RecentJourneyVideosModel()
                    {
                        Id = x.Id,
                        Published = x.Published,
                        VideoURL = x.VideoURL
                    }).Where(x=>x.Published).ToList()
                },
                BlogPost = new BlogPost()
                {
                    CouponBoxDescription = blogPostSetting.CouponBoxDescription,
                    SmallTitle = blogPostSetting.SmallTitle,
                    LargeTitle = blogPostSetting.LargeTitle,
                    DiscountBannerURL = blogPostSetting.DiscountBanner > 0 ? await _pictureService.GetPictureUrlAsync(blogPostSetting.DiscountBanner) : string.Empty,
                    CouponBoxTitle = blogPostSetting.CouponBoxTitle
                }

            };
            return model;
        }
    }
}
