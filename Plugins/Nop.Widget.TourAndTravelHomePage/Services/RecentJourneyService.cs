using Nop.Core;
using Nop.Data;
using Nop.Widget.TourAndTravelHomePage.Domain;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public class RecentJourneyService : IRecentJourneyService
    {
        #region Properties
        private readonly IRepository<RecentJourneyVideos> _recentJourneyVideosrepository;
        #endregion

        #region Constructor
        public RecentJourneyService(IRepository<RecentJourneyVideos> recentJourneyVideosrepository)
        {
            _recentJourneyVideosrepository = recentJourneyVideosrepository;
        }
        #endregion

        public async Task Delete(int Id)
        {
            var recentJourneyRecord = await GetJourneyVideoById(Id);
            if (recentJourneyRecord != null)
                await _recentJourneyVideosrepository.DeleteAsync(recentJourneyRecord);
        }

        public async Task<IPagedList<RecentJourneyVideos>> GetAllRecentJourneyVideos(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var list = await _recentJourneyVideosrepository.Table.ToListAsync();
            return new PagedList<RecentJourneyVideos>(list, pageIndex, pageSize);
        }

        public async Task InsertOrUpdateRecentJourneyVideos(RecentJourneyVideosModel model)
        {
            if (model.Id > 0)
            {
                var existing = await GetJourneyVideoById(model.Id);
                existing.VideoURL = model.VideoURL;
                existing.Published = model.Published;
                await _recentJourneyVideosrepository.UpdateAsync(existing);
            }
            else
            {
                await _recentJourneyVideosrepository.InsertAsync(new RecentJourneyVideos()
                {
                    Published = model.Published,
                    VideoURL = model.VideoURL
                });
            }
        }

        public async Task<RecentJourneyVideos> GetJourneyVideoById(int id)
        {
            return await _recentJourneyVideosrepository.Table.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
