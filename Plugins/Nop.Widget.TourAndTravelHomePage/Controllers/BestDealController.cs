using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Widget.TourAndTravelHomePage.Services;
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
    public class BestDealController : BasePluginController
    {
        #region Properties
        private readonly IBestDealService _bestDealService;
        #endregion

        #region Constructor
        public BestDealController(IBestDealService bestDealService)
        {
            _bestDealService = bestDealService;
        }
        #endregion

        public async Task<IActionResult> Save(IFormCollection form)
        {
            var IsBestDeal = form["IsBestDeal"].ToString();
            var id = form["Id"].ToString();
            await _bestDealService.InsertOrUpdate(Convert.ToInt32(id), Convert.ToBoolean(IsBestDeal.Split(',')[0]));
            return new NullJsonResult();

        }
    }
}
