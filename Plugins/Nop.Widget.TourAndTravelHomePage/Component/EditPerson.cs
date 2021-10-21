using Microsoft.AspNetCore.Mvc;
using Nop.Core;
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
    public class EditPersonViewComponent : NopViewComponent
    {
        private readonly IPackageService _packageService;
        private readonly IWorkContext _workContext;

        public EditPersonViewComponent(IPackageService packageService,
            IWorkContext workContext)
        {
            _packageService = packageService;
            _workContext = workContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var package =await _packageService.GetCurrentCustomerPackage(await _workContext.GetCurrentCustomerAsync());
            var model = new TravellerInfo();
            if (package != null)
            {

                model.Adults = package.Adults;
                model.Children = package.Children;
                model.TravellingDate = package.DateOfTravelling;
                model.TravellingFrom = package.LocationFrom;
                model.TravellingTo = package.LocationTo;
                                
                ViewBag.TextBoxValue = string.Format("{0} Adults, {1} Children", package.Adults, package.Children);
            }           
            return View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/EditPerson/Default.cshtml", model);            

        }

    }
}
