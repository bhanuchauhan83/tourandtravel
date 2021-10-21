using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Tax;
using Nop.Web.Controllers;
using Nop.Web.Factories;
using Nop.Web.Framework.Mvc;
using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.Widget.TourAndTravelHomePage.Controllers
{
    public class ShoppingCartOverrideController : ShoppingCartController
    {
        #region Fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly ICheckoutAttributeParser _checkoutAttributeParser;
        private readonly ICheckoutAttributeService _checkoutAttributeService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerService _customerService;
        private readonly IDiscountService _discountService;
        private readonly IDownloadService _downloadService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IGiftCardService _giftCardService;
        private readonly ILocalizationService _localizationService;
        private readonly INopFileProvider _fileProvider;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductService _productService;
        private readonly IShippingService _shippingService;
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreContext _storeContext;
        private readonly ITaxService _taxService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IWebHelper _webHelper;
        private readonly IWorkContext _workContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly MediaSettings _mediaSettings;
        private readonly OrderSettings _orderSettings;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly IPackageService _packageService;
        private readonly ISearchService _searchService;

        #endregion

        #region Ctor

        public ShoppingCartOverrideController(CaptchaSettings captchaSettings,
            CustomerSettings customerSettings,
            ICheckoutAttributeParser checkoutAttributeParser,
            ICheckoutAttributeService checkoutAttributeService,
            ICurrencyService currencyService,
            ICustomerActivityService customerActivityService,
            ICustomerService customerService,
            IDiscountService discountService,
            IDownloadService downloadService,
            IGenericAttributeService genericAttributeService,
            IGiftCardService giftCardService,
            ILocalizationService localizationService,
            INopFileProvider fileProvider,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IPriceFormatter priceFormatter,
            IProductAttributeParser productAttributeParser,
            IProductAttributeService productAttributeService,
            IProductService productService,
            IShippingService shippingService,
            IShoppingCartModelFactory shoppingCartModelFactory,
            IShoppingCartService shoppingCartService,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            ITaxService taxService,
            IUrlRecordService urlRecordService,
            IWebHelper webHelper,
            IWorkContext workContext,
            IWorkflowMessageService workflowMessageService,
            MediaSettings mediaSettings,
            OrderSettings orderSettings,
            ShoppingCartSettings shoppingCartSettings,
            ShippingSettings shippingSettings,
            IPackageService packageService,
            ISearchService searchService) : base(captchaSettings,
            customerSettings, checkoutAttributeParser,
            checkoutAttributeService, currencyService,
            customerActivityService, customerService,
            discountService, downloadService,
            genericAttributeService, giftCardService,
            localizationService, fileProvider,
            notificationService, permissionService,
            pictureService, priceFormatter,
            productAttributeParser, productAttributeService,
            productService, shippingService,
            shoppingCartModelFactory, shoppingCartService,
            staticCacheManager, storeContext,
            taxService, urlRecordService,
            webHelper, workContext,
            workflowMessageService, mediaSettings,
            orderSettings, shoppingCartSettings,
            shippingSettings)
        {
            _captchaSettings = captchaSettings;
            _customerSettings = customerSettings;
            _checkoutAttributeParser = checkoutAttributeParser;
            _checkoutAttributeService = checkoutAttributeService;
            _currencyService = currencyService;
            _customerActivityService = customerActivityService;
            _customerService = customerService;
            _discountService = discountService;
            _downloadService = downloadService;
            _genericAttributeService = genericAttributeService;
            _giftCardService = giftCardService;
            _localizationService = localizationService;
            _fileProvider = fileProvider;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _priceFormatter = priceFormatter;
            _productAttributeParser = productAttributeParser;
            _productAttributeService = productAttributeService;
            _productService = productService;
            _shippingService = shippingService;
            _shoppingCartModelFactory = shoppingCartModelFactory;
            _shoppingCartService = shoppingCartService;
            _staticCacheManager = staticCacheManager;
            _storeContext = storeContext;
            _taxService = taxService;
            _urlRecordService = urlRecordService;
            _webHelper = webHelper;
            _workContext = workContext;
            _workflowMessageService = workflowMessageService;
            _mediaSettings = mediaSettings;
            _orderSettings = orderSettings;
            _shoppingCartSettings = shoppingCartSettings;
            _shippingSettings = shippingSettings;
            _packageService = packageService;
            _searchService = searchService;
        }

        #endregion

        #region Methods

        public override async Task<IActionResult> AddProductToCart_Catalog(int productId, int shoppingCartTypeId, int quantity, bool forceredirection = false)
        {
            var cart = _shoppingCartService.GetShoppingCartAsync(_workContext.GetCurrentCustomerAsync().Result, (ShoppingCartType)shoppingCartTypeId, (_storeContext.GetCurrentStoreAsync().Result).Id).Result;
            foreach (var item in cart)
            {
                await _shoppingCartService.DeleteShoppingCartItemAsync(item.Id);
            }
            var querystring = GetQuerryStringValues();
            var persons = querystring.FirstOrDefault(x => x.Key == "p").Value;
            var travellerInfo = new TravellerInfo();
            if (!string.IsNullOrEmpty(persons))
            {
                persons = System.Web.HttpUtility.UrlDecode(persons);
                var value = persons.Split(',');
                var adult = value[0].ToString().Split(" ")[0];
                var children = value[1].ToString().Trim().Split(" ")[0];
                quantity = Convert.ToInt32(adult);
                travellerInfo.Adults = Convert.ToInt32(adult);
                travellerInfo.Children = Convert.ToInt32(children);
                travellerInfo.TravellingFrom = System.Web.HttpUtility.UrlDecode(querystring.FirstOrDefault(x => x.Key == "f").Value);
                travellerInfo.TravellingTo = System.Web.HttpUtility.UrlDecode(querystring.FirstOrDefault(x => x.Key == "q").Value);
                var date = System.Web.HttpUtility.UrlDecode(querystring.FirstOrDefault(x => x.Key == "d").Value);
                var month = Convert.ToInt32(date.Split('/')[0]);
                var day = Convert.ToInt32(date.Split('/')[1]);
                var year = Convert.ToInt32(date.Split('/')[2]);
                var dateTime = new DateTime(year, month, day);
                travellerInfo.TravellingDate = dateTime;

            }
            var result = await base.AddProductToCart_Catalog(productId, shoppingCartTypeId, quantity, forceredirection);

            cart = _shoppingCartService.GetShoppingCartAsync(_workContext.GetCurrentCustomerAsync().Result, (ShoppingCartType)shoppingCartTypeId, (_storeContext.GetCurrentStoreAsync().Result).Id).Result;
            if (cart.Count > 0)
            {
                travellerInfo.CartId = cart.FirstOrDefault().Id;
                travellerInfo.CustomerId = (await _workContext.GetCurrentCustomerAsync()).Id;
                var _productTagService = EngineContext.Current.Resolve<IProductTagService>();
                if (string.IsNullOrEmpty(persons))
                {
                    var productTags = await _productTagService.GetAllProductTagsByProductIdAsync(productId);
                    travellerInfo.TravellingDate = DateTime.UtcNow;
                    travellerInfo.Adults = 1;
                    travellerInfo.TravellingFrom = "USA"; //static entry
                    if (productTags.Count > 0)
                        travellerInfo.TravellingTo = productTags.FirstOrDefault().Name;
                }

                await _packageService.SaveOrUpdateCustomerPackageMapping(travellerInfo);


            }

            return result;
        }

        public override async Task<IActionResult> AddProductToCart_Details(int productId, int shoppingCartTypeId, IFormCollection form)
        {
            var cart = _shoppingCartService.GetShoppingCartAsync(_workContext.GetCurrentCustomerAsync().Result, (ShoppingCartType)shoppingCartTypeId, (_storeContext.GetCurrentStoreAsync().Result).Id).Result;
            var _productTagService = EngineContext.Current.Resolve<IProductTagService>();
            foreach (var item in cart)
            {
                await _shoppingCartService.DeleteShoppingCartItemAsync(item.Id);
            }
            var querystring = GetQuerryStringValues();
            var persons = querystring.FirstOrDefault(x => x.Key == "p").Value;
            var travellerInfo = new TravellerInfo();
            var product = await _productService.GetProductByIdAsync(productId);
            if (!string.IsNullOrEmpty(persons))
            {

                persons = System.Web.HttpUtility.UrlDecode(persons);
                var value = persons.Split(',');
                var adult = value[0].ToString().Split(" ")[0];
                var children = value[1].ToString().Trim().Split(" ")[0];
                var quantity = _productAttributeParser.ParseEnteredQuantity(product, form);
                quantity = Convert.ToInt32(adult);
                travellerInfo.Adults = Convert.ToInt32(adult);
                travellerInfo.Children = Convert.ToInt32(children);
                travellerInfo.TravellingFrom = System.Web.HttpUtility.UrlDecode(querystring.FirstOrDefault(x => x.Key == "f").Value);
                travellerInfo.TravellingTo = System.Web.HttpUtility.UrlDecode(querystring.FirstOrDefault(x => x.Key == "q").Value);
                var date = System.Web.HttpUtility.UrlDecode(querystring.FirstOrDefault(x => x.Key == "d").Value);
                var month = Convert.ToInt32(date.Split('/')[0]);
                var day = Convert.ToInt32(date.Split('/')[1]);
                var year = Convert.ToInt32(date.Split('/')[2]);
                var dateTime = new DateTime(year, month, day);
                travellerInfo.TravellingDate = dateTime;

            }
            var result = await base.AddProductToCart_Details(productId, shoppingCartTypeId, form);

            cart = _shoppingCartService.GetShoppingCartAsync(_workContext.GetCurrentCustomerAsync().Result, (ShoppingCartType)shoppingCartTypeId, (_storeContext.GetCurrentStoreAsync().Result).Id).Result;
            if (cart.Count > 0)
            {
                travellerInfo.CartId = cart.FirstOrDefault().Id;
                travellerInfo.CustomerId = (await _workContext.GetCurrentCustomerAsync()).Id;
                var productTags = await _productTagService.GetAllProductTagsByProductIdAsync(productId);
                if (string.IsNullOrEmpty(persons))
                {
                    travellerInfo.TravellingDate = DateTime.UtcNow;
                    travellerInfo.Adults = 1;
                    travellerInfo.TravellingFrom = "USA"; //static entry
                    if (productTags.Count > 0)
                        travellerInfo.TravellingTo = productTags.FirstOrDefault().Name;
                }
               

                await _packageService.SaveOrUpdateCustomerPackageMapping(travellerInfo);


            }

            return result;
        }


        private Dictionary<string, string> GetQuerryStringValues()
        {

            var keyValuePair = new Dictionary<string, string>();
            if (Request.Headers.Keys.Contains("Referer"))
            {
                var querryStrings = string.Empty;
                try
                {
                    querryStrings = Request.Headers["Referer"].ToString().Split('?')[1];
                }
                catch
                {

                }
                if (!String.IsNullOrEmpty(querryStrings))
                {
                    var values = querryStrings.Split('&');
                    foreach (var val in values)
                    {
                        var keyValue = val.Split('=');
                        keyValuePair.Add(keyValue[0], keyValue[1]);
                    }
                }

            }
            return keyValuePair;
        }

        public async Task<IActionResult> UpdateCartQuantity(IFormCollection form)
        {
            var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);
            var guestInfo = form["guestInfo"].ToString();
            var from = form["f"].ToString();
            var to = form["q"].ToString();
            if (await CheckIfAddedProductIsValidForThisUpdatedLocation(to, cart.FirstOrDefault()))
            {
                var date = form["d"].ToString();
                var package = await _packageService.GetCurrentCustomerPackage(await _workContext.GetCurrentCustomerAsync());
                if (!string.IsNullOrEmpty(guestInfo))
                {
                    var adultInfo = guestInfo.Split(',')[0].ToString();
                    if (!string.IsNullOrEmpty(adultInfo))
                    {
                        var adultCount = adultInfo.Split(' ')[0].ToString();
                        if (!string.IsNullOrEmpty(adultCount))
                        {
                            var quantity = Convert.ToInt32(adultCount);
                            await _shoppingCartService.UpdateShoppingCartItemAsync(await _workContext.GetCurrentCustomerAsync(), cart.FirstOrDefault().Id, string.Empty, 0, quantity: quantity);

                            if (package != null)
                            {
                                package.Adults = quantity;
                                var childCount = guestInfo.Split(',')[1].ToString().Trim().Split(' ')[0].ToString();
                                package.Children = Convert.ToInt32(childCount);

                            }
                        }
                    }
                }
                package.LocationFrom = from;
                package.LocationTo = to;
                if (date.Contains('/'))
                {
                    var month = Convert.ToInt32(date.Split('/')[0]);
                    var day = Convert.ToInt32(date.Split('/')[1]);
                    var year = Convert.ToInt32(date.Split('/')[2]);
                    var dateTime = new DateTime(year, month, day);
                    package.DateOfTravelling = dateTime;
                }
                else
                {
                    var month = Convert.ToInt32(date.Split('-')[0]);
                    var day = Convert.ToInt32(date.Split('-')[1]);
                    var year = Convert.ToInt32(date.Split('-')[2]);
                    var dateTime = new DateTime(year, month, day);
                    package.DateOfTravelling = dateTime;
                }

                await _packageService.UpdatePackage(package);
            }
            else
            {
                _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("PackageAddedIntoCartNotAvaiable"));

            }

            return new NullJsonResult();
        }

        private async Task<bool> CheckIfAddedProductIsValidForThisUpdatedLocation(string location, ShoppingCartItem item)
        {
            var packages = await _packageService.SearchPackages(location, 0, int.MaxValue);
            if (packages.FirstOrDefault(x => x.Id == item.ProductId) != null)
                return true;
            return false;
        }
        #endregion
    }
}
