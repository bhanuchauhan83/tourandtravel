using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Domain
{
    public class BestDealCategory : BaseEntity
    {
        public int CategoryId { get; set; }
        public bool IsBestDeal { get; set; }
    }
}
