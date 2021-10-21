using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage
{
    public class HomePagePlugin : BasePlugin, IWidgetPlugin
    {
        #region Properties
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        #endregion

        #region Constructor
        public HomePagePlugin(ILocalizationService localizationService,
            IWebHelper webHelper)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
        }
        #endregion

        #region Methods

        #region WidgetSection
        public bool HideInWidgetList => true;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return TourAndTravelDefault.HomePageSectionComponent;
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
            return Task.FromResult<IList<string>>(TourAndTravelDefault.WidgetList());
        }
        #endregion       

        #region Install/Uninstall
        public async override Task InstallAsync()
        {
            await _localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
            {
                [TourAndTravelDefault.DefaultStringResource + ".Field.KnowMoreLink"] = "Know More Link",
                [TourAndTravelDefault.DefaultStringResource + ".Field.KnowMoreLink.Hint"] = "On click on know more customer will redirect to this URL",
                [TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBannerId"] = "Banner Backgorund Image",
                [TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBannerId.Hint"] = "This image will show on the backgorund of the section",
                [TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBannerDescription"] = "Discount Banner Discription",
                [TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBannerDescription.Hint"] = "This Description will show on the banner at right corner",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BackgroundImageId"] = "Backgound Image",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BackgroundImageId.Hint"] = "This image will show on the backgorund of the section",
                [TourAndTravelDefault.DefaultStringResource + ".Field.SmallTitle"] = "Small Title",
                [TourAndTravelDefault.DefaultStringResource + ".Field.SmallTitle.Hint"] = "This title will show in green text above the Large Title.",
                
                [TourAndTravelDefault.DefaultStringResource + ".Field.LargeTitle"] = "Large Title",
                [TourAndTravelDefault.DefaultStringResource + ".Field.LargeTitle.Hint"] = "This title will show in black text below the Small Title.",  
                [TourAndTravelDefault.DefaultStringResource + ".Field.Description"] = "Discription",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Description.Hint"] = "This is show in the body of the section below Large Title",

                [TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL"]="Video URL",
                [TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL.Hint"]="Video will be listed into the homepage section",
                


                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name"] = "Deal Name",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name.Hint"] = "Deal name will be shown in the section",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price"] = "Price",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price.Hint"] = "Price with show with deal",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Image"] = "Deal Image",


                [TourAndTravelDefault.DefaultStringResource + ".RecentJourneyBlock"] = "Recent Journey",
                [TourAndTravelDefault.DefaultStringResource + ".BestDealsBlock"] = "Best Deal",
                [TourAndTravelDefault.DefaultStringResource + ".Testimonials"] = "Testimonials",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description"] = "Discription",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description.Hint"] = "Testimonial commneted by user",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName"] = "User Name",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName.Hint"] = "User who write this testimonial.",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry"] = "User Country",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry.Hint"] = "User's Country",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg"] = "User Image",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg.Hint"] = "User's Image",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Title1"] = "Title 1",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Title1.Hint"] = "Title will show over discount banner",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Package"] = "Package Text",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Package.Hint"] = "Title will show over discount banner after description",
            });

            await base.InstallAsync();
        }

        public async override Task UninstallAsync()
        {
            await _localizationService.DeleteLocaleResourceAsync(TourAndTravelDefault.DefaultStringResource);
            await base.UninstallAsync();
        }
        #endregion

        #region Configure
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/ManageHomePage/Configure";
        }
        #endregion
        #endregion
    }
}
