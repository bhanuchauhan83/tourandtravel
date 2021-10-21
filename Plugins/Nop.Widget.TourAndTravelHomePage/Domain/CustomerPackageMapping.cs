using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Domain
{
    public class CustomerPackageMapping : BaseEntity
    {
        public int? OrderId { get; set; }
        public int? CartId { get; set; }
        public int CustomerID { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public DateTime DateOfTravelling { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }

        public string PersonsInfo { get; set; }
    }
}
