using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Factories
{
    public interface IRecentVideoModelFactory
    {
        Task<RecentJourneyVideosModelList> PrepareRecentJourneyVideosModelAsync(RecentJourneySearchModel searchModel);
    }
}
