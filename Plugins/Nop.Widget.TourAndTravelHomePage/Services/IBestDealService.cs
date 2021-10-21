using Nop.Core.Domain.Catalog;
using Nop.Widget.TourAndTravelHomePage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public interface IBestDealService
    {
        Task<List<Category>> GetAllBestDealCategories();

        Task InsertOrUpdate(int categoryId, bool isBestDeal);

        Task<BestDealCategory> GetBestDealCategoryByIdAsync(int categoryId);

        bool GetBestDealCatgoriesExistorNot();
    }
}
