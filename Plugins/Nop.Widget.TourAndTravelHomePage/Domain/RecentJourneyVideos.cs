using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Domain
{
    public class RecentJourneyVideos : BaseEntity
    {
        public string VideoURL { get; set; }
        public bool Published { get; set; }
    }
}
