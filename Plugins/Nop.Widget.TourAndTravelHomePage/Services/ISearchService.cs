using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public interface ISearchService
    {
        Task<List<string>> GetSearchedCityOrCountryByKeywordAsync(string term);
    }
}
