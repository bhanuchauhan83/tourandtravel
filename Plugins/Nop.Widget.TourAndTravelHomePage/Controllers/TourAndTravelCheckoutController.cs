using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Http.Extensions;
using Newtonsoft.Json;
using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Web.Extensions;
using Nop.Web.Factories;
using Nop.Web.Framework.Controllers;
using Nop.Web.Models.Checkout;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Widget.TourAndTravelHomePage.Services;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Nop.Services.Messages;

namespace Nop.Widget.TourAndTravelHomePage.Controllers
{
    public class TourAndTravelCheckoutController : BasePluginController
    {
        #region Properties
        private readonly OrderSettings _orderSettings;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly CustomerSettings _customerSettings;
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IPaymentPluginManager _paymentPluginManager;
        private readonly IStoreContext _storeContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICheckoutModelFactory _checkoutModelFactory;
        private readonly ILocalizationService _localizationService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressService _addressService;
        private readonly AddressSettings _addressSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly ICountryService _countryService;
        private readonly ILogger _logger;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly PaymentSettings _paymentSettings;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly IPackageService _packageService;
        private readonly INotificationService _notificationService;
        #endregion

        #region Constructor
        public TourAndTravelCheckoutController(OrderSettings orderSettings,
            IShoppingCartService shoppingCartService, CustomerSettings customerSettings,
            IWorkContext workContext, IProductService productService,
            ICustomerService customerService, IPaymentPluginManager paymentPluginManager,
            IStoreContext storeContext, IGenericAttributeService genericAttributeService,
            ICheckoutModelFactory checkoutModelFactory, ILocalizationService localizationService,
            IAddressAttributeParser addressAttributeParser, IAddressService addressService,
            AddressSettings addressSettings, ShippingSettings shippingSettings,
            ICountryService countryService, ILogger logger,
            IOrderProcessingService orderProcessingService, PaymentSettings paymentSettings,
            RewardPointsSettings rewardPointsSettings,
            IPackageService packageService,
            INotificationService notificationService)
        {
            _orderSettings = orderSettings;
            _shoppingCartService = shoppingCartService;
            _customerSettings = customerSettings;
            _workContext = workContext;
            _productService = productService;
            _customerService = customerService;
            _paymentPluginManager = paymentPluginManager;
            _storeContext = storeContext;
            _genericAttributeService = genericAttributeService;
            _checkoutModelFactory = checkoutModelFactory;
            _localizationService = localizationService;
            _addressAttributeParser = addressAttributeParser;
            _addressService = addressService;
            _addressSettings = addressSettings;
            _shippingSettings = shippingSettings;
            _countryService = countryService;
            _logger = logger;
            _orderProcessingService = orderProcessingService;
            _paymentSettings = paymentSettings;
            _rewardPointsSettings = rewardPointsSettings;
            _packageService = packageService;
            _notificationService = notificationService;
        }
        #endregion

        #region Methods

        #region Common (Methods)
        public async Task<IActionResult> Index()
        {
            //validation
            if (_orderSettings.CheckoutDisabled)
                return RedirectToRoute("ShoppingCart");

            var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            var cartProductIds = cart.Select(ci => ci.ProductId).ToArray();
            var downloadableProductsRequireRegistration =
                _customerSettings.RequireRegistrationForDownloadableProducts && await _productService.HasAnyDownloadableProductAsync(cartProductIds);

            if (await _customerService.IsGuestAsync(await _workContext.GetCurrentCustomerAsync()) && (!_orderSettings.AnonymousCheckoutAllowed || downloadableProductsRequireRegistration))
                return Challenge();

            //if we have only "button" payment methods available (displayed on the shopping cart page, not during checkout),
            //then we should allow standard checkout
            //all payment methods (do not filter by country here as it could be not specified yet)
            var paymentMethods = await (await _paymentPluginManager
                .LoadActivePluginsAsyncAsync(await _workContext.GetCurrentCustomerAsync(), (await _storeContext.GetCurrentStoreAsync()).Id))
                .WhereAwait(async pm => !await pm.HidePaymentMethodAsync(cart)).ToListAsync();
            //payment methods displayed during checkout (not with "Button" type)
            var nonButtonPaymentMethods = paymentMethods
                .Where(pm => pm.PaymentMethodType != PaymentMethodType.Button)
                .ToList();
            //"button" payment methods(*displayed on the shopping cart page)
            var buttonPaymentMethods = paymentMethods
                .Where(pm => pm.PaymentMethodType == PaymentMethodType.Button)
                .ToList();
            if (!nonButtonPaymentMethods.Any() && buttonPaymentMethods.Any())
                return RedirectToRoute("ShoppingCart");

            //reset checkout data
            await _customerService.ResetCheckoutDataAsync(await _workContext.GetCurrentCustomerAsync(), (await _storeContext.GetCurrentStoreAsync()).Id);

            //validation (cart)
            var checkoutAttributesXml = await _genericAttributeService.GetAttributeAsync<string>(await _workContext.GetCurrentCustomerAsync(),
                NopCustomerDefaults.CheckoutAttributes, (await _storeContext.GetCurrentStoreAsync()).Id);
            var scWarnings = await _shoppingCartService.GetShoppingCartWarningsAsync(cart, checkoutAttributesXml, true);
            if (scWarnings.Any())
                return RedirectToRoute("ShoppingCart");
            //validation (each shopping cart item)
            foreach (var sci in cart)
            {
                var product = await _productService.GetProductByIdAsync(sci.ProductId);

                var sciWarnings = await _shoppingCartService.GetShoppingCartItemWarningsAsync(await _workContext.GetCurrentCustomerAsync(),
                    sci.ShoppingCartType,
                    product,
                    sci.StoreId,
                    sci.AttributesXml,
                    sci.CustomerEnteredPrice,
                    sci.RentalStartDateUtc,
                    sci.RentalEndDateUtc,
                    sci.Quantity,
                    false,
                    sci.Id);
                if (sciWarnings.Any())
                    return RedirectToRoute("ShoppingCart");
            }

            if (_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute(TourAndTravelDefault.TourAndTravelOnePageCheckout_Route);

            return RedirectToRoute("CheckoutBillingAddress");
        }

        #endregion

        #region OnePageCheckout
        public async Task<IActionResult> OnePageCheckout()
        {
            //validation
            if (_orderSettings.CheckoutDisabled)
                return RedirectToRoute("ShoppingCart");

            var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            if (!_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute("Checkout");

            if (await _customerService.IsGuestAsync(await _workContext.GetCurrentCustomerAsync()) && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            if(!await AllDetailsProvided())
            {
                _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("TravellerInfoInvalid"));
                return RedirectToRoute("ShoppingCart");
            }
          

            var onePageCheckoutModel = await _checkoutModelFactory.PrepareOnePageCheckoutModelAsync(cart);
            TourAndTravelOnePageCheckoutModel model = new TourAndTravelOnePageCheckoutModel()
            {
                OnePageCheckoutModel = onePageCheckoutModel
            };
            return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Checkout/OnePageCheckout.cshtml", model);
        }


        public async Task<IActionResult> OpcLoadPersonInfoHtml(IFormCollection form)
        {
            var model = await GetTravellerInfo();
            return Json(new
            {
                update_section = new UpdateSectionJsonModel
                {
                    name = "traveller-personal-info",
                    html = await RenderViewComponentToStringAsync("PersonalInfo", new { personsDetail = model })
                },
                goto_section = "traveller_personal_info"
            });
        }

        private async Task<Dictionary<string, int>> GetTravellerInfo()
        {
            var TravellerInfo = await _packageService.GetCurrentCustomerPackage(await _workContext.GetCurrentCustomerAsync());

            var model = new Dictionary<string, int>();
            if (TravellerInfo != null)
            {
                model.Add("Adult", TravellerInfo.Adults);
                model.Add("Children", TravellerInfo.Children);
            }
            return model;
        }

        private async Task<bool> AllDetailsProvided()
        {
            var TravellerInfo = await _packageService.GetCurrentCustomerPackage(await _workContext.GetCurrentCustomerAsync());
            return !(string.IsNullOrEmpty(TravellerInfo.LocationFrom) && string.IsNullOrEmpty(TravellerInfo.LocationTo));
        }

        public async Task<IActionResult> OpcSavePersonalInfo(IFormCollection form)
        {

            var currentCustomer = await _workContext.GetCurrentCustomerAsync();

            var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, currentCustomer.Id);
            //var travellers = HttpContext.Session.GetString("NumberOfPersons_" + currentCustomer.Id + "");

            //if (!string.IsNullOrEmpty(travellers))
            //{
            var TravellerInfo = await _packageService.GetCurrentCustomerPackage(await _workContext.GetCurrentCustomerAsync());

            List<PersonalInfo> adultList = new List<PersonalInfo>();
            for (int i = 1; i <= TravellerInfo.Adults; i++)
            {
                var firstName = form["Adult_FirstName_" + i + ""].ToString();
                var lastName = form["Adult_LastName_" + i + ""].ToString();
                var age = form["Adult_Age_" + i + ""].ToString();
                adultList.Add(new PersonalInfo()
                {
                    Age = Convert.ToInt32(age),
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                });
            }
            List<PersonalInfo> childList = new List<PersonalInfo>();
            for (int i = 1; i <= TravellerInfo.Children; i++)
            {
                var firstName = form["Child_FirstName_" + i + ""].ToString();
                var lastName = form["Child_LastName_" + i + ""].ToString();
                var age = form["Child_Age_" + i + ""].ToString();
                childList.Add(new PersonalInfo()
                {
                    Age = Convert.ToInt32(age),
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                });
            }

            if (TravellerInfo != null)
            {
                var serializedTravllerInfo = new TravellerInfo();
                serializedTravllerInfo.ChildrenInfo = childList;
                serializedTravllerInfo.AdultsInfo = adultList;
                serializedTravllerInfo.Adults = TravellerInfo.Adults;
                serializedTravllerInfo.CartId = TravellerInfo.CartId;
                serializedTravllerInfo.Children = TravellerInfo.Children;
                serializedTravllerInfo.CustomerId = TravellerInfo.CustomerID;
                serializedTravllerInfo.TravellingDate = TravellerInfo.DateOfTravelling;
                serializedTravllerInfo.TravellingFrom = TravellerInfo.LocationFrom;
                serializedTravllerInfo.TravellingTo = TravellerInfo.LocationTo;
                serializedTravllerInfo.PersonsInfoXML = ToXml(serializedTravllerInfo);
                await _packageService.SaveOrUpdateCustomerPackageMapping(serializedTravllerInfo);
            }

            if (!cart.Any())
                return RedirectToRoute("ShoppingCart");

            if (!_orderSettings.OnePageCheckoutEnabled)
                return RedirectToRoute("Checkout");

            if (await _customerService.IsGuestAsync(await _workContext.GetCurrentCustomerAsync()) && !_orderSettings.AnonymousCheckoutAllowed)
                return Challenge();

            var onePageCheckoutModel = await _checkoutModelFactory.PrepareOnePageCheckoutModelAsync(cart);

            return Json(new
            {
                update_section = new UpdateSectionJsonModel
                {
                    name = "contact-info",
                    html = await RenderPartialViewToStringAsync("~/Plugins/Widget.TourAndTravelHomePage/Views/Checkout/BillingAddress.cshtml", onePageCheckoutModel.BillingAddress)
                },
                goto_section = "contact_info"
            });
            //}

        }

        public async Task<IActionResult> OpcSaveContactInfo(CheckoutBillingAddressModel model, IFormCollection form)
        {
            try
            {
                //validation
                if (_orderSettings.CheckoutDisabled)
                    throw new Exception(await _localizationService.GetResourceAsync("Checkout.Disabled"));

                var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);

                if (!cart.Any())
                    throw new Exception("Your cart is empty");

                if (!_orderSettings.OnePageCheckoutEnabled)
                    throw new Exception("One page checkout is disabled");

                if (await _customerService.IsGuestAsync(await _workContext.GetCurrentCustomerAsync()) && !_orderSettings.AnonymousCheckoutAllowed)
                    throw new Exception("Anonymous checkout is not allowed");

                int.TryParse(form["billing_address_id"], out var billingAddressId);

                if (billingAddressId > 0)
                {
                    //existing address
                    var address = await _customerService.GetCustomerAddressAsync((await _workContext.GetCurrentCustomerAsync()).Id, billingAddressId)
                        ?? throw new Exception(await _localizationService.GetResourceAsync("Checkout.Address.NotFound"));

                    (await _workContext.GetCurrentCustomerAsync()).BillingAddressId = address.Id;
                    await _customerService.UpdateCustomerAsync(await _workContext.GetCurrentCustomerAsync());
                }
                else
                {
                    //new address
                    var newAddress = model.BillingNewAddress;

                    //custom address attributes
                    var customAttributes = await _addressAttributeParser.ParseCustomAddressAttributesAsync(form);
                    var customAttributeWarnings = await _addressAttributeParser.GetAttributeWarningsAsync(customAttributes);
                    foreach (var error in customAttributeWarnings)
                    {
                        ModelState.AddModelError("", error);
                    }

                    //validate model
                    if (!ModelState.IsValid)
                    {
                        //model is not valid. redisplay the form with errors
                        var billingAddressModel = await _checkoutModelFactory.PrepareBillingAddressModelAsync(cart,
                            selectedCountryId: newAddress.CountryId,
                            overrideAttributesXml: customAttributes);
                        billingAddressModel.NewAddressPreselected = true;
                        return Json(new
                        {
                            update_section = new UpdateSectionJsonModel
                            {
                                name = "billing",
                                html = await RenderPartialViewToStringAsync("~/Plugins/Widget.TourAndTravelHomePage/Views/Checkout/BillingAddress.cshtml", billingAddressModel)
                            },
                            wrong_billing_address = true,
                        });
                    }

                    //try to find an address with the same values (don't duplicate records)
                    var address = _addressService.FindAddress((await _customerService.GetAddressesByCustomerIdAsync((await _workContext.GetCurrentCustomerAsync()).Id)).ToList(),
                        newAddress.FirstName, newAddress.LastName, newAddress.PhoneNumber,
                        newAddress.Email, newAddress.FaxNumber, newAddress.Company,
                        newAddress.Address1, newAddress.Address2, newAddress.City,
                        newAddress.County, newAddress.StateProvinceId, newAddress.ZipPostalCode,
                        newAddress.CountryId, customAttributes);

                    if (address == null)
                    {
                        //address is not found. let's create a new one
                        address = newAddress.ToEntity();
                        address.CustomAttributes = customAttributes;
                        address.CreatedOnUtc = DateTime.UtcNow;

                        //some validation
                        if (address.CountryId == 0)
                            address.CountryId = null;

                        if (address.StateProvinceId == 0)
                            address.StateProvinceId = null;

                        await _addressService.InsertAddressAsync(address);

                        await _customerService.InsertCustomerAddressAsync(await _workContext.GetCurrentCustomerAsync(), address);
                    }

                    (await _workContext.GetCurrentCustomerAsync()).BillingAddressId = address.Id;

                    await _customerService.UpdateCustomerAsync(await _workContext.GetCurrentCustomerAsync());
                }

                if (await _shoppingCartService.ShoppingCartRequiresShippingAsync(cart))
                {
                    //shipping is required
                    var address = await _customerService.GetCustomerBillingAddressAsync(await _workContext.GetCurrentCustomerAsync());

                    //by default Shipping is available if the country is not specified
                    var shippingAllowed = !_addressSettings.CountryEnabled || ((await _countryService.GetCountryByAddressAsync(address))?.AllowsShipping ?? false);
                    //if (_shippingSettings.ShipToSameAddress && model.ShipToSameAddress && shippingAllowed)
                    //{
                    //ship to the same address
                    (await _workContext.GetCurrentCustomerAsync()).ShippingAddressId = address.Id;
                    await _customerService.UpdateCustomerAsync(await _workContext.GetCurrentCustomerAsync());
                    //reset selected shipping method (in case if "pick up in store" was selected)
                    await _genericAttributeService.SaveAttributeAsync<ShippingOption>(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.SelectedShippingOptionAttribute, null, (await _storeContext.GetCurrentStoreAsync()).Id);
                    await _genericAttributeService.SaveAttributeAsync<PickupPoint>(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.SelectedPickupPointAttribute, null, (await _storeContext.GetCurrentStoreAsync()).Id);
                    //limitation - "Ship to the same address" doesn't properly work in "pick up in store only" case (when no shipping plugins are available) 
                    //return await OpcLoadStepAfterShippingAddress(cart);
                    //}

                }

                //shipping is not required
                await _genericAttributeService.SaveAttributeAsync<ShippingOption>(await _workContext.GetCurrentCustomerAsync(), NopCustomerDefaults.SelectedShippingOptionAttribute, null, (await _storeContext.GetCurrentStoreAsync()).Id);
                //load next step
                return await OpcLoadStepAfterContactInfo(cart);
            }
            catch (Exception exc)
            {
                await _logger.WarningAsync(exc.Message, exc, await _workContext.GetCurrentCustomerAsync());
                return Json(new { error = 1, message = exc.Message });
            }
        }


        //OpcTourAndTravelSavePaymentMethod

        [IgnoreAntiforgeryToken]
        public virtual async Task<IActionResult> OpcTourAndTravelSavePaymentMethod(string paymentmethod, CheckoutPaymentMethodModel model)
        {
            try
            {
                //validation
                if (_orderSettings.CheckoutDisabled)
                    throw new Exception(await _localizationService.GetResourceAsync("Checkout.Disabled"));

                var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);

                if (!cart.Any())
                    throw new Exception("Your cart is empty");

                if (!_orderSettings.OnePageCheckoutEnabled)
                    throw new Exception("One page checkout is disabled");

                if (await _customerService.IsGuestAsync(await _workContext.GetCurrentCustomerAsync()) && !_orderSettings.AnonymousCheckoutAllowed)
                    throw new Exception("Anonymous checkout is not allowed");

                //payment method 
                if (string.IsNullOrEmpty(paymentmethod))
                    throw new Exception("Selected payment method can't be parsed");

                //reward points
                if (_rewardPointsSettings.Enabled)
                {
                    await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(),
                        NopCustomerDefaults.UseRewardPointsDuringCheckoutAttribute, model.UseRewardPoints,
                        (await _storeContext.GetCurrentStoreAsync()).Id);
                }

                //Check whether payment workflow is required
                var isPaymentWorkflowRequired = await _orderProcessingService.IsPaymentWorkflowRequiredAsync(cart);
                if (!isPaymentWorkflowRequired)
                {
                    //payment is not required
                    await _genericAttributeService.SaveAttributeAsync<string>(await _workContext.GetCurrentCustomerAsync(),
                        NopCustomerDefaults.SelectedPaymentMethodAttribute, null, (await _storeContext.GetCurrentStoreAsync()).Id);

                    var confirmOrderModel = await _checkoutModelFactory.PrepareConfirmOrderModelAsync(cart);
                    return Json(new
                    {
                        update_section = new UpdateSectionJsonModel
                        {
                            name = "confirm-order",
                            html = await RenderPartialViewToStringAsync("OpcConfirmOrder", confirmOrderModel)
                        },
                        goto_section = "confirm_order"
                    });
                }

                var paymentMethodInst = await _paymentPluginManager
                    .LoadPluginBySystemNameAsync(paymentmethod, await _workContext.GetCurrentCustomerAsync(), (await _storeContext.GetCurrentStoreAsync()).Id);
                if (!_paymentPluginManager.IsPluginActive(paymentMethodInst))
                    throw new Exception("Selected payment method can't be parsed");

                //save
                await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(),
                    NopCustomerDefaults.SelectedPaymentMethodAttribute, paymentmethod, (await _storeContext.GetCurrentStoreAsync()).Id);

                return await OpcLoadStepAfterPaymentMethod(paymentMethodInst, cart);
            }
            catch (Exception exc)
            {
                await _logger.WarningAsync(exc.Message, exc, await _workContext.GetCurrentCustomerAsync());
                return Json(new { error = 1, message = exc.Message });
            }
        }
        #endregion

        #region Utilities

        private string ToXml<T>(T value)
        {
            using var writer = new StringWriter();
            using var xmlWriter = new XmlTextWriter(writer) { Formatting = System.Xml.Formatting.Indented };
            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(xmlWriter, value);
            return writer.ToString();
        }
        protected virtual async Task<JsonResult> OpcLoadStepAfterContactInfo(IList<ShoppingCartItem> cart)
        {
            //Check whether payment workflow is required
            //we ignore reward points during cart total calculation
            var isPaymentWorkflowRequired = await _orderProcessingService.IsPaymentWorkflowRequiredAsync(cart, false);
            var confirmOrderModel = await _checkoutModelFactory.PrepareConfirmOrderModelAsync(cart);
            if (isPaymentWorkflowRequired)
            {
                //filter by country
                var filterByCountryId = 0;
                if (_addressSettings.CountryEnabled)
                {
                    filterByCountryId = (await _customerService.GetCustomerBillingAddressAsync(await _workContext.GetCurrentCustomerAsync()))?.CountryId ?? 0;
                }

                //payment is required
                var paymentMethodModel = await _checkoutModelFactory.PreparePaymentMethodModelAsync(cart, filterByCountryId);

                if (_paymentSettings.BypassPaymentMethodSelectionIfOnlyOne &&
                    paymentMethodModel.PaymentMethods.Count == 1 && !paymentMethodModel.DisplayRewardPoints)
                {
                    //if we have only one payment method and reward points are disabled or the current customer doesn't have any reward points
                    //so customer doesn't have to choose a payment method

                    var selectedPaymentMethodSystemName = paymentMethodModel.PaymentMethods[0].PaymentMethodSystemName;
                    await _genericAttributeService.SaveAttributeAsync(await _workContext.GetCurrentCustomerAsync(),
                        NopCustomerDefaults.SelectedPaymentMethodAttribute,
                        selectedPaymentMethodSystemName, (await _storeContext.GetCurrentStoreAsync()).Id);

                    var paymentMethodInst = await _paymentPluginManager
                        .LoadPluginBySystemNameAsync(selectedPaymentMethodSystemName, await _workContext.GetCurrentCustomerAsync(), (await _storeContext.GetCurrentStoreAsync()).Id);
                    if (!_paymentPluginManager.IsPluginActive(paymentMethodInst))
                        throw new Exception("Selected payment method can't be parsed");

                    return Json(new
                    {
                        update_section = new UpdateSectionJsonModel
                        {
                            name = "confirm-order",
                            html = await RenderPartialViewToStringAsync("OpcConfirmOrder", confirmOrderModel)
                        },
                        goto_section = "confirm_order"
                    });
                }

                //customer have to choose a payment method
                return Json(new
                {
                    update_section = new UpdateSectionJsonModel
                    {
                        name = "payment-method",
                        html = await RenderPartialViewToStringAsync("~/Plugins/Widget.TourAndTravelHomePage/Views/Checkout/OpcPaymentMethods.cshtml", paymentMethodModel)
                    },
                    goto_section = "payment_method"
                });
            }

            //payment is not required
            await _genericAttributeService.SaveAttributeAsync<string>(await _workContext.GetCurrentCustomerAsync(),
                NopCustomerDefaults.SelectedPaymentMethodAttribute, null, (await _storeContext.GetCurrentStoreAsync()).Id);


            return Json(new
            {
                update_section = new UpdateSectionJsonModel
                {
                    name = "confirm-order",
                    html = await RenderPartialViewToStringAsync("OpcConfirmOrder", confirmOrderModel)
                },
                goto_section = "confirm_order"
            });
        }

        protected virtual async Task<JsonResult> OpcLoadStepAfterPaymentMethod(IPaymentMethod paymentMethod, IList<ShoppingCartItem> cart)
        {
            if (paymentMethod.SkipPaymentInfo ||
                (paymentMethod.PaymentMethodType == PaymentMethodType.Redirection && _paymentSettings.SkipPaymentInfoStepForRedirectionPaymentMethods))
            {
                //skip payment info page
                var paymentInfo = new ProcessPaymentRequest();

                //session save
                HttpContext.Session.Set("OrderPaymentInfo", paymentInfo);

                var confirmOrderModel = await _checkoutModelFactory.PrepareConfirmOrderModelAsync(cart);
                return Json(new
                {
                    update_section = new UpdateSectionJsonModel
                    {
                        name = "confirm-order",
                        html = await RenderPartialViewToStringAsync("OpcConfirmOrder", confirmOrderModel)
                    },
                    goto_section = "confirm_order"
                });
            }

            //return payment info page
            var paymenInfoModel = await _checkoutModelFactory.PreparePaymentInfoModelAsync(paymentMethod);
            return Json(new
            {
                update_section = new UpdateSectionJsonModel
                {
                    name = "payment-info",
                    html = await RenderPartialViewToStringAsync("~/Plugins/Widget.TourAndTravelHomePage/Views/Checkout/OpcPaymentInfo.cshtml", paymenInfoModel)
                },
                goto_section = "payment_info"
            });
        }
        #endregion

        #endregion
    }
}
