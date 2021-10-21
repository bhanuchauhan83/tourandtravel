using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Domain
{
    public class TravelPackage : BaseEntity
    {
        public int ProductId { get; set; }

        public string PackagesIncludeXML { get; set; }

    }
}
