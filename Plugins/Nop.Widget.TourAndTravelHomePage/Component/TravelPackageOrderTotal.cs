using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Services.Orders;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Component
{
    public class TravelPackageOrderTotalViewComponent : NopViewComponent
    {
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly IPackageService _packageService;

        public TravelPackageOrderTotalViewComponent(IShoppingCartModelFactory shoppingCartModelFactory,
            IShoppingCartService shoppingCartService,
            IStoreContext storeContext,
            IWorkContext workContext, IPackageService packageService)
        {
            _shoppingCartModelFactory = shoppingCartModelFactory;
            _shoppingCartService = shoppingCartService;
            _storeContext = storeContext;
            _packageService = packageService;
            _workContext = workContext;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync(bool isEditable)
        {
            var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);

            var model = await _shoppingCartModelFactory.PrepareOrderTotalsModelAsync(cart, isEditable);
            var package =await _packageService.GetCurrentCustomerPackage(await _workContext.GetCurrentCustomerAsync());
            ViewBag.Quantity = cart.FirstOrDefault().Quantity;
            if (package != null)
                ViewBag.Children = package.Children;
            return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/TravelPackageOrderTotal/Default.cshtml", model);
        }
    }
}
