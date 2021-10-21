using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Widget.TourAndTravelHomePage.Factories;
using Nop.Widget.TourAndTravelHomePage.Models;
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
        #endregion

        #region Constructor
        public ManageHomePageController(IStoreContext storeContext,
            ISettingService settingService, INotificationService notificationService,
            ILocalizationService localizationService,
            ITourAndTravelModelFactory tourAndTravelModelFactory)
        {
            _storeContext = storeContext;
            _settingService = settingService;
            _localizationService = localizationService;
            _notificationService = notificationService;
            _tourAndTravelModelFactory = tourAndTravelModelFactory;
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
        #endregion

        #region Utilities
        private async Task SaveSettings(ConfigurationModel model)
        {
            var storeId = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var bestDealSettings = await _settingService.LoadSettingAsync<BestDealsSettings>(storeId);
            var testimonialSettings = await _settingService.LoadSettingAsync<TestimonialSettings>(storeId);
            var recentJourneySetting = await _settingService.LoadSettingAsync<RecentJourneySettings>(storeId);

            //best deals
            bestDealSettings.DiscountBannerId = model.BestDeals.DiscountBannerId;
            bestDealSettings.Description = model.BestDeals.Description;
            bestDealSettings.DiscountBannerDescription = model.BestDeals.DiscountBannerDescription;
            bestDealSettings.Title1 = model.BestDeals.Title1;
            bestDealSettings.Title2 = model.BestDeals.Title2;
            bestDealSettings.Package = model.BestDeals.Package;
            bestDealSettings.KnowMoreLink = model.BestDeals.KnowMoreLink;
            bestDealSettings.LargeTitle = model.BestDeals.LargeTitle;
            bestDealSettings.SmallTitle = model.BestDeals.SmallTitle;
            bestDealSettings.BestDeal1_Image = model.BestDeals.BestDeal1_Image;
            bestDealSettings.BestDeal1_Name = model.BestDeals.BestDeal1_Name;
            bestDealSettings.BestDeal1_Price = model.BestDeals.BestDeal1_Price;
            bestDealSettings.BestDeal2_Image = model.BestDeals.BestDeal2_Image;
            bestDealSettings.BestDeal2_Name = model.BestDeals.BestDeal2_Name;
            bestDealSettings.BestDeal2_Price = model.BestDeals.BestDeal2_Price;
            bestDealSettings.BestDeal3_Image = model.BestDeals.BestDeal3_Image;
            bestDealSettings.BestDeal3_Name = model.BestDeals.BestDeal3_Name;
            bestDealSettings.BestDeal3_Price = model.BestDeals.BestDeal3_Price;
            bestDealSettings.BestDeal4_Image = model.BestDeals.BestDeal4_Image;
            bestDealSettings.BestDeal4_Name = model.BestDeals.BestDeal4_Name;
            bestDealSettings.BestDeal4_Price = model.BestDeals.BestDeal4_Price;


            //testimonials
            testimonialSettings.BackgroundImageId = model.Testimonial.BackgroundImageId;
            testimonialSettings.Description = model.Testimonial.Description;
            testimonialSettings.LargeTitle = model.Testimonial.LargeTitle;
            testimonialSettings.SmallTitle = model.Testimonial.SmallTitle;
            testimonialSettings.Testimonial1_Description = model.Testimonial.Testimonial1_Description;
            testimonialSettings.Testimonial1_UserCountry = model.Testimonial.Testimonial1_UserCountry;
            testimonialSettings.Testimonial1_UserImg = model.Testimonial.Testimonial1_UserImg;
            testimonialSettings.Testimonial1_UserName = model.Testimonial.Testimonial1_UserName;
            testimonialSettings.Testimonial2_Description = model.Testimonial.Testimonial2_Description;
            testimonialSettings.Testimonial2_UserCountry = model.Testimonial.Testimonial2_UserCountry;
            testimonialSettings.Testimonial2_UserImg = model.Testimonial.Testimonial2_UserImg;
            testimonialSettings.Testimonial2_UserName = model.Testimonial.Testimonial2_UserName;
            testimonialSettings.Testimonial3_Description = model.Testimonial.Testimonial3_Description;
            testimonialSettings.Testimonial3_UserCountry = model.Testimonial.Testimonial3_UserCountry;
            testimonialSettings.Testimonial3_UserImg = model.Testimonial.Testimonial3_UserImg;
            testimonialSettings.Testimonial3_UserName = model.Testimonial.Testimonial3_UserName;


            //recent journey
            recentJourneySetting.Description = model.RecentJourney.Description;
            recentJourneySetting.LargeTitle = model.RecentJourney.LargeTitle;
            recentJourneySetting.SmallTitle = model.RecentJourney.SmallTitle;
            recentJourneySetting.BackgroundImageId = model.RecentJourney.BackgroundImageId;
            recentJourneySetting.Video1URL = model.RecentJourney.Video1URL;
            recentJourneySetting.Video2URL = model.RecentJourney.Video2URL;
            recentJourneySetting.Video3URL = model.RecentJourney.Video3URL;



            //best deals
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.DiscountBannerId, model.BestDeals.DiscountBannerId_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Description, model.BestDeals.Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.DiscountBannerDescription, model.BestDeals.DiscountBannerDescription_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.KnowMoreLink, model.BestDeals.KnowMoreLink_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.LargeTitle, model.BestDeals.LargeTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.SmallTitle, model.BestDeals.SmallTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal1_Image, model.BestDeals.BestDeal1_Image_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal1_Name, model.BestDeals.BestDeal1_Name_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal1_Price, model.BestDeals.BestDeal1_Price_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal2_Image, model.BestDeals.BestDeal2_Image_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal2_Name, model.BestDeals.BestDeal2_Name_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal2_Price, model.BestDeals.BestDeal2_Price_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal3_Image, model.BestDeals.BestDeal3_Image_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal3_Name, model.BestDeals.BestDeal3_Name_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal3_Price, model.BestDeals.BestDeal3_Price_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal4_Image, model.BestDeals.BestDeal4_Image_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal4_Name, model.BestDeals.BestDeal4_Name_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.BestDeal4_Price, model.BestDeals.BestDeal4_Price_OverrideForStore, storeId, false);

            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Title1, model.BestDeals.Title1_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Title2, model.BestDeals.Title2_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(bestDealSettings, setting => bestDealSettings.Package, model.BestDeals.Package_OverrideForStore, storeId, false);



            //testimonials
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.BackgroundImageId, model.Testimonial.BackgroundImageId_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Description, model.Testimonial.Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.LargeTitle, model.Testimonial.LargeTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.SmallTitle, model.Testimonial.SmallTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial1_Description, model.Testimonial.Testimonial1_Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial1_UserCountry, model.Testimonial.Testimonial1_UserCountry_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial1_UserImg, model.Testimonial.Testimonial1_UserImg_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial1_UserName, model.Testimonial.Testimonial1_UserName_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial2_Description, model.Testimonial.Testimonial2_Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial2_UserCountry, model.Testimonial.Testimonial2_UserCountry_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial2_UserImg, model.Testimonial.Testimonial2_UserImg_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial2_UserName, model.Testimonial.Testimonial2_UserName_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial3_Description, model.Testimonial.Testimonial3_Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial3_UserCountry, model.Testimonial.Testimonial3_UserCountry_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial3_UserImg, model.Testimonial.Testimonial3_UserImg_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(testimonialSettings, setting => testimonialSettings.Testimonial3_UserName, model.Testimonial.Testimonial3_UserName_OverrideForStore, storeId, false);


            //recent journey
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.Description, model.RecentJourney.Description_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.LargeTitle, model.RecentJourney.LargeTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.SmallTitle, model.RecentJourney.SmallTitle_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.BackgroundImageId, model.RecentJourney.BackgroundImageId_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.Video1URL, model.RecentJourney.Video1URL_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.Video2URL, model.RecentJourney.Video2URL_OverrideForStore, storeId, false);
            await _settingService.SaveSettingOverridablePerStoreAsync(recentJourneySetting, setting => recentJourneySetting.Video3URL, model.RecentJourney.Video3URL_OverrideForStore, storeId, false);


            await _settingService.ClearCacheAsync();
            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));
        }
        #endregion
    }
}
