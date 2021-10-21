using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Component
{
    public class BestDealViewComponent : NopViewComponent
    {
        #region Properties
        private readonly ITravelPackageService _travelPackageService;
        private readonly IBestDealService _bestDealService;
        #endregion

        #region Ctor
        public BestDealViewComponent(ITravelPackageService travelPackageService,
            IBestDealService bestDealService)
        {
            _travelPackageService = travelPackageService;
            _bestDealService = bestDealService;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, CategoryModel additionalData)
        {
            var isBestDeal =await _bestDealService.GetBestDealCategoryByIdAsync(additionalData.Id);
            BestDealModel model = new BestDealModel();
           if (isBestDeal != null)
            {
                model.IsBestDeal = isBestDeal.IsBestDeal;
                model.Id = isBestDeal.Id;
            }
            return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/BestDeal/Default.cshtml", model);
        }
    }
}
