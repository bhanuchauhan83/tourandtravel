using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Orders;
using Nop.Web.Factories;
using Nop.Web.Models.ShoppingCart;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;

namespace Nop.Widget.TourAndTravelHomePage.Component
{
    [ViewComponent(Name = TourAndTravelDefault.TourAndTravelOrderSummaryComponent)]
    public class TourAndTravelOrderSummaryViewComponent : NopViewComponent
    {
        private readonly ITourAndTravelModelFactory _tourAndTravelModelFactory;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkContext _workContext;
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;
        private readonly IStoreContext _storeContext;

        public TourAndTravelOrderSummaryViewComponent(ITourAndTravelModelFactory tourAndTravelModelFactory,
            IShoppingCartService shoppingCartService,
            IWorkContext workContext, IShoppingCartModelFactory shoppingCartModelFactory,
             IStoreContext storeContext)
        {
            _tourAndTravelModelFactory = tourAndTravelModelFactory;
            _shoppingCartService = shoppingCartService;
            _workContext = workContext;
            _shoppingCartModelFactory = shoppingCartModelFactory;
            _storeContext = storeContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(bool? prepareAndDisplayOrderReviewData, ShoppingCartModel overriddenModel)
        {
            //use already prepared (shared) model
            if (overriddenModel != null)
                return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/TourAndTravelOrderSummary/Default.cshtml", overriddenModel);

            //if not passed, then create a new model
            var cart = await _shoppingCartService.GetShoppingCartAsync(await _workContext.GetCurrentCustomerAsync(), ShoppingCartType.ShoppingCart, (await _storeContext.GetCurrentStoreAsync()).Id);

            var model = new ShoppingCartModel();
            model = await _shoppingCartModelFactory.PrepareShoppingCartModelAsync(model, cart,
                isEditable: false,
                prepareAndDisplayOrderReviewData: prepareAndDisplayOrderReviewData.GetValueOrDefault());
            return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/TourAndTravelOrderSummary/Default.cshtml", model);
        }

    }
}
