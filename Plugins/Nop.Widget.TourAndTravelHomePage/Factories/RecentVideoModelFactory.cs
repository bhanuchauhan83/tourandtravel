using Nop.Web.Framework.Models.Extensions;
using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Factories
{
    public class RecentVideoModelFactory : IRecentVideoModelFactory
    {
        #region Properties
        private readonly IRecentJourneyService _recentJourneyService;
        #endregion

        #region Constructor
        public RecentVideoModelFactory(IRecentJourneyService recentJourneyService)
        {
            _recentJourneyService = recentJourneyService;
        }
        #endregion
        public async Task<RecentJourneyVideosModelList> PrepareRecentJourneyVideosModelAsync(RecentJourneySearchModel searchModel)
        {
            var list = await _recentJourneyService.GetAllRecentJourneyVideos(searchModel.Page - 1, searchModel.PageSize);
            var model = new RecentJourneyVideosModelList().PrepareToGrid(searchModel, list, () =>
            {
                return list.Select(x =>
                {
                    //fill in model values from the entity
                    var productPictureModel = new RecentJourneyVideosModel()
                    {
                        Published = x.Published,
                        VideoURL = x.VideoURL,
                        Id = x.Id
                    };
                    return productPictureModel;
                });
            });

            return model;
        }
    }
}
