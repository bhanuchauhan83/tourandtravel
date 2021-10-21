using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Nop.Widget.TourAndTravelHomePage.Factories
{
    public class TravelPackageModelFactory : ITravelPackageModelFactory
    {
        #region Properties
        private readonly ITravelPackageService _travelPackageService;
        #endregion


        #region Constructor
        public TravelPackageModelFactory(ITravelPackageService travelPackageService)
        {
            _travelPackageService = travelPackageService;
        }
        #endregion

        public async Task<TravelPackageModel> PrepareTravelPackageModelAsync(int productId)
        {
            var retVal = new TravelPackageModel();
            retVal.ProductId = productId;
            var package = await _travelPackageService.GetTravelPackageByProductIdAsync(productId);
            if (package != null)
            {
                retVal.Id = package.Id;
                retVal.ProductId = package.ProductId;
                retVal.Packages = ParseXML<Package>(package.PackagesIncludeXML);
                //retVal.Packages = ToXml<Package>(package.PackagesIncludeXML);
            }
            return retVal;

        }

        #region Utitlites
        private T ParseXML<T>(string xml)
        {
            using var tr = new StringReader(xml);
            return (T)new XmlSerializer(typeof(T)).Deserialize(tr);
        }
        #endregion

    }
}
