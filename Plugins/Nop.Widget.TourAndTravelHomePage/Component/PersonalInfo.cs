using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Component
{
    public class PersonalInfoViewComponent : NopViewComponent
    {

        #region Methods
        public Task<IViewComponentResult> InvokeAsync(Dictionary<string, int> personsDetail)
        {         
            return Task.FromResult<IViewComponentResult>(View("~/Plugins/Widget.TourAndTravelHomePage/Views/Shared/Component/PersonalInfo/Default.cshtml", personsDetail));
        }

        #endregion
    }
}
