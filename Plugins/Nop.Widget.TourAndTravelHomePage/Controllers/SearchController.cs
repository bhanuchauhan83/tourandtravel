using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Controllers
{
    public class SearchController:BasePluginController
    {
        #region Properties
        private readonly CatalogSettings _catalogSettings;
        private readonly ISearchService _searchService;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor
        public SearchController(CatalogSettings catalogSettings,
            ISearchService searchService, IStoreContext storeContext,
            IWorkContext workContext)
        {
            _catalogSettings = catalogSettings;
            _searchService = searchService;
            _storeContext = storeContext;
            _workContext = workContext;
        }

        #endregion


        #region Methods

        #endregion


        public async Task<IActionResult> SearchTermAutoComplete(string term)
        {
            term = term.Trim();
            if (string.IsNullOrWhiteSpace(term) || term.Length < _catalogSettings.ProductSearchTermMinimumLength)
                return Content("");

            var products = await _searchService.GetSearchedCityOrCountryByKeywordAsync(term);

            return Json(products);
        }
    }
}
