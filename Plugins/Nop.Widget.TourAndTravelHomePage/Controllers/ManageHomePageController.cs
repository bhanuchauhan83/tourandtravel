using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Widget.TourAndTravelHomePage.Factories;
using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class ManageHomePageController : BasePluginController
    {
        #region Properties
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly ITourAndTravelModelFactory _tourAndTravelModelFactory;
        private readonly IRecentVideoModelFactory _recentVideoModelFactory;
        private readonly IRecentJourneyService _recentJourneyService;
        #endregion

        #region Constructor
        public ManageHomePageController(IStoreContext storeContext,
            ISettingService settingService, INotificationService notificationService,
            ILocalizationService localizationService,
            ITourAndTravelModelFactory tourAndTravelModelFactory,
            IRecentVideoModelFactory recentVideoModelFactory,
            IRecentJourneyService recentJourneyService)
        {
            _storeContext = storeContext;
            _settingService = settingService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _tourAndTravelModelFactory = tourAndTravelModelFactory;
            _recentVideoModelFactory = recentVideoModelFactory;
            _recentJourneyService = recentJourneyService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Configure()
        {
            var model = await _tourAndTravelModelFactory.PrepareConfigurationModelAsync();
            return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return await Configure();
            await SaveSettings(model);
            return await Configure();
        }

        [HttpPost]
        public async Task<IActionResult> RecentJourneyVideos(RecentJourneySearchModel searchModel)
        {
            var videoUrls = await _recentVideoModelFactory.PrepareRecentJourneyVideosModelAsync(searchModel);
            return Json(videoUrls);
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateRecentJourneyVideos(RecentJourneyVideosModel videosModel)
        {
            await _recentJourneyService.InsertOrUpdateRecentJourneyVideos(videosModel);
            return new NullJsonResult();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRecentJourneyVideo(int Id)
        {
            await _recentJourneyService.Delete(Id);
            return new NullJsonResult();
        }
        #endregion

        #region Utilities
        private async Task SaveSettings(ConfigurationModel model)
        {
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var bestDealSettings = await _settingService.LoadSettingAsync<BestDealsSettings>(storeId);
            var testimonialSettings = await _settingService.LoadSettingAsync<TestimonialSettings>(storeId);
            var recentJourneySetting = await _settingService.LoadSettingAsync<RecentJourneySettings>(storeId);
            var blogPostSetting = await _settingService.LoadSettingAsync<BlogPostSettings>(storeId);

            #region Set Settings
            //best deals
            #region Best Deals
            bestDealSettings.DiscountBannerId = model.BestDeals.DiscountBannerId;
            bestDealSettings.Description = model.BestDeals.Description;
            bestDealSettings.DiscountBannerDescription = model.BestDeals.DiscountBannerDescription;
            bestDealSettings.Title1 = model.BestDeals.Title1;
            bestDealSettings.Title2 = model.BestDeals.Title2;
            bestDealSettings.Package = model.BestDeals.Package;
            bestDealSettings.KnowMoreLink = model.BestDeals.KnowMoreLink;
            bestDealSettings.LargeTitle = model.BestDeals.LargeTitle;
            bestDealSettings.SmallTitle = model.BestDeals.SmallTitle;          
            #endregion

            //testimonials
            #region Testimonials
            testimonialSettings.BackgroundImageId = model.Testimonial.BackgroundImageId;
            testimonialSettings.Description = model.Testimonial.Description;
            testimonialSettings.LargeTitle = model.Testimonial.LargeTitle;
            testimonialSettings.SmallTitle = model.Testimonial.SmallTitle;
            #endregion

            //recent journey
            #region Recent Journey
            recentJourneySetting.Description = model.RecentJourney.Description;
            recentJourneySetting.LargeTitle = model.RecentJourney.LargeTitle;
            recentJourneySetting.SmallTitle = model.RecentJourney.SmallTitle;
            recentJourneySetting.BackgroundImageId = model.RecentJourney.BackgroundImageId; 
            #endregion

            //blog post
            #region Blog Post
            blogPostSetting.LargeTitle = model.BlogPost.LargeTitle;
            blogPostSetting.SmallTitle = model.BlogPost.SmallTitle;
            blogPostSetting.CouponBoxTitle = model.BlogPost.CouponBoxTitle;
            blogPostSetting.CouponBoxDescription = model.BlogPost.CouponBoxDescription;
            blogPostSetting.DiscountBanner = model.BlogPost.DiscountBanner;
            #endregion

            #endregion

            #region Save Settings
            //best deals -save settings
            #region Best Deal Settings Save
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.DiscountBannerId, model.BestDeals.DiscountBannerId_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Description, model.BestDeals.Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.DiscountBannerDescription, model.BestDeals.DiscountBannerDescription_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.KnowMoreLink, model.BestDeals.KnowMoreLink_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.LargeTitle, model.BestDeals.LargeTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.SmallTitle, model.BestDeals.SmallTitle_OverrideForStore, storeId, false);
           
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Title1, model.BestDeals.Title1_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Title2, model.BestDeals.Title2_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Package, model.BestDeals.Package_OverrideForStore, storeId, false);
            #endregion

            //testimonials -save settings
            #region Testimonial Settings Save
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.BackgroundImageId, model.Testimonial.BackgroundImageId_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Description, model.Testimonial.Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.LargeTitle, model.Testimonial.LargeTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.SmallTitle, model.Testimonial.SmallTitle_OverrideForStore, storeId, false);
            
            #endregion

            //recent journey -save settings
            #region Recent Journey Save
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.Description, model.RecentJourney.Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.LargeTitle, model.RecentJourney.LargeTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.SmallTitle, model.RecentJourney.SmallTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.BackgroundImageId, model.RecentJourney.BackgroundImageId_OverrideForStore, storeId, false);
          
            #endregion

            //blog post -save settings
            #region Blog Post Save
            await _settingService.SaveSettingOverridablePerStoreAsync(blogPostSetting, setting => blogPostSetting.LargeTitle, model.BlogPost.LargeTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(blogPostSetting, setting => blogPostSetting.SmallTitle, model.BlogPost.SmallTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(blogPostSetting, setting => blogPostSetting.CouponBoxDescription, model.BlogPost.CouponBoxDescription_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(blogPostSetting, setting => blogPostSetting.CouponBoxTitle, model.BlogPost.CouponBoxTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(blogPostSetting, setting => blogPostSetting.DiscountBanner, model.BlogPost.DiscountBanner_OverrideForStore, storeId, false);
            #endregion
            #endregion
            await _settingService.ClearCacheAsync();
            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
        }
        #endregion
    }
}
