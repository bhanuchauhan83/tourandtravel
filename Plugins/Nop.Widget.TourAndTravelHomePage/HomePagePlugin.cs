using Nop.Core;
using Nop.Services.Catalog;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage
{
    public class HomePagePlugin : BasePlugin, IWidgetPlugin
    {
        #region Properties
        private readonly ILocalizationService _localizationService;
        private readonly IWebHelper _webHelper;
        private readonly IProductTemplateService _productTemplateService;
        #endregion

        #region Constructor
        public HomePagePlugin(ILocalizationService localizationService,
            IWebHelper webHelper, IProductTemplateService productTemplateService)
        {
            _localizationService = localizationService;
            _webHelper = webHelper;
            _productTemplateService = productTemplateService;
        }
        #endregion

        #region Methods

        #region WidgetSection
        public bool HideInWidgetList => true;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            if (widgetZone == AdminWidgetZones.ProductDetailsBlock)
                return TourAndTravelDefault.VIEW_COMPONENT_TRIP_PACKAGES_MANGEMENT;
            else if (widgetZone == AdminWidgetZones.CategoryDetailsBlock)
                return TourAndTravelDefault.VIEW_COMPONENT_BESTDEAL; 
            else if (widgetZone == AdminWidgetZones.OrderDetailsBlock)
                return TourAndTravelDefault.VIEW_COMPONENT_TRAVELLERDETAILS;
            else
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

                [TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL"] = "Video URL",
                [TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL.Hint"] = "Video will be listed into the homepage section",



                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name"] = "Deal Name",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Name.Hint"] = "Deal name will be shown in the section",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price"] = "Price",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Price.Hint"] = "Price with show with deal",
                [TourAndTravelDefault.DefaultStringResource + ".Field.BestDeal_Image"] = "Deal Image",


                [TourAndTravelDefault.DefaultStringResource + ".RecentJourneyBlock"] = "Recent Journey",
                [TourAndTravelDefault.DefaultStringResource + ".BestDealsBlock"] = "Best Deal",
                [TourAndTravelDefault.DefaultStringResource + ".Testimonials"] = "Testimonials",
                [TourAndTravelDefault.DefaultStringResource + ".BlogPosts"] = "Blog Posts",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description"] = "Discription",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_Description.Hint"] = "Testimonial commneted by user",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName"] = "User Name",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserName.Hint"] = "User who write this testimonial.",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry"] = "User Country",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserCountry.Hint"] = "User's Country",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg"] = "User Image",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Testimonial_UserImg.Hint"] = "User's Image",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Title1"] = "Title 1",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Title2"] = "Title 2",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Title2.Hint"] = "Title 2 will show over discount banner after title 1",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Title1.Hint"] = "Title will show over discount banner",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Package"] = "Package Text",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Package.Hint"] = "Title will show over discount banner after description",



                [TourAndTravelDefault.DefaultStringResource + ".Field.CouponBoxTitle"] = "Discount Title",
                [TourAndTravelDefault.DefaultStringResource + ".Field.CouponBoxTitle.Hint"] = "This will show as a tile in coupon box at blog section",
                [TourAndTravelDefault.DefaultStringResource + ".Field.CouponBoxDescription"] = "Discount Description",
                [TourAndTravelDefault.DefaultStringResource + ".Field.CouponBoxDescription.Hint"] = "This will show as a discount description in coupon box at blog section ",
                [TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBanner"] = "Discount Banner",
                [TourAndTravelDefault.DefaultStringResource + ".Field.DiscountBanner.Hint"] = "This image will show as discount banner in blog section",


                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeFlights"] = "Include Flight",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeFlights.Hint"] = "Include flight in this package ?",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeHotel"] = "Include Hotel",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeHotel.Hint"] = "Include hotel in this package",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeBus"] = "Include Bus",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeBus.Hint"] = "Include bus in this package",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeTrain"] = "Include Train",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeTrain.Hint"] = "Include train in this package",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeCabs"] = "Include Cab",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeCabs.Hint"] = "Include cab in this package",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeCityTour"] = "Include City Tour",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeCityTour.Hint"] = "Include city tour in this package",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.Name"] = "Name",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.Name.Hint"] = "Hotel name",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.Address1"] = "Address1",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.Address1.Hint"] = "Address1 for hotel",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.Address2"] = "Address2",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.Address2.Hint"] = "Address2 for hotel",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.ContactNumber"] = "Contact",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.ContactNumber.Hint"] = "Contact for hotel",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.RoomType"] = "Room Type",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.RoomType.Hint"] = "Room type that will offer to customer in this hotel",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.ServiceDescription"] = "Service Description",
                [TourAndTravelDefault.StringResourcePrefix + ".Fields.ServiceDescription.Hint"] = "Service Description",
                [TourAndTravelDefault.PackageManagementTabName] = "Package",
                [TourAndTravelDefault.DefaultStringResource + ".Field.NumberOfPersons"] = "Total Travellers",
                [TourAndTravelDefault.DefaultStringResource + ".Field.FirstName"] = "First Name",
                [TourAndTravelDefault.DefaultStringResource + ".Field.LastName"] = "Last Name",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Age"] = "Age",
                [TourAndTravelDefault.DefaultStringResource + ".Field.IsBestDeal"] = "Is Best Deal",
                [TourAndTravelDefault.DefaultStringResource + ".Field.IsBestDeal.Hint"] = "If true then will show in best deal section",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Published"] = "Published",
                [TourAndTravelDefault.DefaultStringResource + ".Field.Published.Hint"] = "If Published then will show on front-end",
                [TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL"] = "VideoURL",
                [TourAndTravelDefault.DefaultStringResource + ".Field.VideoURL.Hint"] = "Youtube embaded link will work here",

                ["checkout.numberofpersons"] = "Total Travellers",
                ["checkout.personsinfo"] = "Traveller Info",
                ["checkout.contactinfo"] = "Contact Info",
                ["PackageAddedIntoCartNotAvaiable"] = "Package added in cart is not avaiable for this location, Please select another location or add another product.",
                ["TravellerInfoInvalid"] = "Travelling data is not valid, Please provide valid data before checkout",
                [TourAndTravelDefault.DefaultStringResource + ".SaveBeforeEdit"] = "Save product before mapping package",



            });
            var productTemplate = await _productTemplateService.GetAllProductTemplatesAsync();
            if (!productTemplate.Any(x => x.Name == TourAndTravelDefault.PackageTemplate))
            {
                await _productTemplateService.InsertProductTemplateAsync(new Core.Domain.Catalog.ProductTemplate()
                {
                    DisplayOrder = 1,
                    IgnoredProductTypes = null,
                    Name = TourAndTravelDefault.PackageTemplate,
                    ViewPath = "ProductTemplate.Package.cshtml"
                });
            }
            
            await base.InstallAsync();
        }

        public async override Task UninstallAsync()
        {
            await _localizationService.DeleteLocaleResourceAsync(TourAndTravelDefault.DefaultStringResource);
            await _localizationService.DeleteLocaleResourceAsync(TourAndTravelDefault.StringResourcePrefix);
            var productTemplate = await _productTemplateService.GetAllProductTemplatesAsync();
            if (!productTemplate.Any(x => x.Name == TourAndTravelDefault.PackageTemplate))
            {
                await _productTemplateService.DeleteProductTemplateAsync(productTemplate.FirstOrDefault(x => x.Name == TourAndTravelDefault.PackageTemplate));
            }
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
