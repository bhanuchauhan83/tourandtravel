using Microsoft.AspNetCore.Mvc;
using Nop.Web.Areas.Admin.Models.Orders;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Nop.Widget.TourAndTravelHomePage.Component
{
    public class TravellerDetailsViewComponent : NopViewComponent
    {
        #region Properties
        private readonly IPackageService _packageService;
        #endregion

        #region Constructor
        public TravellerDetailsViewComponent(IPackageService packageService)
        {
            _packageService = packageService;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, OrderModel additionalData)
        {
            var travellerDetails = await _packageService.GetTrvellerDetailsByOrderIdAsync(additionalData.Id);

            if (travellerDetails != null)
            {
                using var tr = new StringReader(travellerDetails.PersonsInfo);
                var model = (TravellerInfo)new XmlSerializer(typeof(TravellerInfo)).Deserialize(tr);
                return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/TravellerDetails/Default.cshtml", model);
            }
            return Content(string.Empty);
        }
    }
}
