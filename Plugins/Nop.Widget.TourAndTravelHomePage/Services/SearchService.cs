using Nop.Core.Domain.Directory;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public class SearchService : ISearchService
    {
        #region Properties
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        #endregion

        #region Constructor
        public SearchService(IRepository<Country> countryRepository,
            IRepository<StateProvince> stateProvinceRepository)
        {
            _countryRepository = countryRepository;
            _stateProvinceRepository = stateProvinceRepository;

        }
        #endregion
        public async Task<List<string>> GetSearchedCityOrCountryByKeywordAsync(string term)
        {
            var countryList = await _countryRepository.Table.ToListAsync();
            var stateList = await _stateProvinceRepository.Table.ToListAsync();
            var searchedCountry = await countryList.Where(x => x.Name.ToLower().Contains(term.ToLower().Trim()) || x.TwoLetterIsoCode.ToLower() == term.ToLower().Trim() || x.ThreeLetterIsoCode.ToLower() == term.ToLower().Trim()).Select(x => x.Name).Distinct().ToListAsync();
            var searchedState = await stateList.Where(x => x.Name.ToLower().Contains(term.ToLower().Trim()) || x.Abbreviation.ToLower() == term.ToLower().Trim()).Select(x => new { name = x.Name, abbreviation = x.Abbreviation }).Distinct().ToListAsync();
            var retVal = new List<string>();
            foreach (var country in searchedCountry)
            {
                retVal.Add(country);
            }
            foreach (var state in searchedState)
            {
                var stateinfo = stateList.FirstOrDefault(x => x.Abbreviation == state.abbreviation);
                var country = countryList.FirstOrDefault(x => x.Id == stateinfo.CountryId);
                retVal.Add(string.Format("{0},{1}", stateinfo.Name, country.Name));
            }
            return retVal;
        }
    }
}
