using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{
    public record SearchForTripModel : BaseNopEntityModel
    {
        public string City { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public decimal Budget { get; set; }

        public string BackgroundURL { get; set; }

        public string SearchTitle { get; set; }
    }
}
