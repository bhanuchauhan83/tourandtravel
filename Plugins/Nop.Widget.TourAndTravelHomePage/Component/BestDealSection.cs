using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Component
{
    public class BestDealSectionViewComponent : NopViewComponent
    {
        #region Properties
        private readonly IBestDealService _bestDealService;
        private readonly ICategoryModelFactory _categoryModelFactory;
        #endregion

        #region Constructor
        public BestDealSectionViewComponent(IBestDealService bestDealService,
            ICategoryModelFactory categoryModelFactory)
        {
            _bestDealService = bestDealService;
            _categoryModelFactory = categoryModelFactory;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryList = await _bestDealService.GetAllBestDealCategories();
            var model = new List<CategoryModel>();
            foreach(var category in categoryList)
            {
                model.Add(await _categoryModelFactory.PrepareCategoryModelAsync(null, category));
            }          
            return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/BestDealSection/Default.cshtml", model);
        }
    }
}
