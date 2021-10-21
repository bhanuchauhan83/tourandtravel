using Nop.Core;
using Nop.Widget.TourAndTravelHomePage.Domain;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public interface IRecentJourneyService
    {
        Task<IPagedList<RecentJourneyVideos>> GetAllRecentJourneyVideos(int pageIndex = 0, int pageSize = int.MaxValue);

        Task InsertOrUpdateRecentJourneyVideos(RecentJourneyVideosModel model);

        Task Delete(int Id);

        Task<RecentJourneyVideos> GetJourneyVideoById(int id);
    }
}
