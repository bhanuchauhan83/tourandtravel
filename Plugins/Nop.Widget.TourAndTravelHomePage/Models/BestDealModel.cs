using Nop.Web.Areas.Admin.Models.Catalog;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{
    public record BestDealModel : BaseNopEntityModel
    {
        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.IsBestDeal")]
        public bool IsBestDeal { get; set; }
    }
}
