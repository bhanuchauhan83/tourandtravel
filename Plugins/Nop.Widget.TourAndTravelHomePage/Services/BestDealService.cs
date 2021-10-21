using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Widget.TourAndTravelHomePage.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public class BestDealService : IBestDealService
    {

        #region Properties
        private readonly IRepository<BestDealCategory> _bestDealCategoryrepository;
        private readonly ICategoryService _categoryService;
        #endregion

        #region Constructor
        public BestDealService(IRepository<BestDealCategory> bestDealCategoryrepository,
            ICategoryService categoryService)
        {
            _bestDealCategoryrepository = bestDealCategoryrepository;
            _categoryService = categoryService;
        }
        #endregion
        public async Task<List<Category>> GetAllBestDealCategories()
        {
            var listOfBestDealCategoryIds = _bestDealCategoryrepository.Table.Where(c => c.IsBestDeal).ToList();
            List<Category> retVal = new List<Category>();
            foreach (var ids in listOfBestDealCategoryIds)
            {
                retVal.Add(await _categoryService.GetCategoryByIdAsync(ids.CategoryId));
            }
            return retVal;
        }

        public bool GetBestDealCatgoriesExistorNot()
        {
            return (_bestDealCategoryrepository.Table.Count(c => c.IsBestDeal) > 0);
        }

        public async Task InsertOrUpdate(int categoryId, bool isBestDeal)
        {
            var exists = await GetBestDealCategoryByIdAsync(categoryId);
            if (exists != null)
            {
                exists.IsBestDeal = isBestDeal;
                await _bestDealCategoryrepository.UpdateAsync(exists);
            }
            else
            {
                await _bestDealCategoryrepository.InsertAsync(new BestDealCategory()
                {
                    IsBestDeal = isBestDeal,
                    CategoryId = categoryId
                });
            }
        }

        public async Task<BestDealCategory> GetBestDealCategoryByIdAsync(int categoryId)
        {
            return await _bestDealCategoryrepository.Table.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        }


    }
}
