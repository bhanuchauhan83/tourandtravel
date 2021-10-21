using Nop.Data;
using Nop.Widget.TourAndTravelHomePage.Domain;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public class TravelPackageService : ITravelPackageService
    {
        #region Properties
        private readonly IRepository<TravelPackage> _travelPackagerepository;
        #endregion

        #region Constructor
        public TravelPackageService(IRepository<TravelPackage> travelPackagerepository)
        {
            _travelPackagerepository = travelPackagerepository;
        }
        #endregion

        #region Methods
        public async Task<TravelPackage> GetTravelPackageByIdAsync(int Id)
        {
            return await _travelPackagerepository.Table.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<TravelPackage> GetTravelPackageByProductIdAsync(int productId)
        {
            return await _travelPackagerepository.Table.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task InsertOrUpdatePackgeInfoAsync(TravelPackageModel travelPackage)
        {
            if (travelPackage.ProductId > 0)
            {
                var package = await GetTravelPackageByProductIdAsync(travelPackage.ProductId);
                if (package != null)
                {
                    package.PackagesIncludeXML = ToXml(travelPackage.Packages);
                    await _travelPackagerepository.UpdateAsync(package);
                }
                else
                {
                    await _travelPackagerepository.InsertAsync(new TravelPackage()
                    {
                        PackagesIncludeXML = ToXml(travelPackage.Packages),
                        ProductId = travelPackage.ProductId
                    });
                }
            }            
            await Task.CompletedTask;
        }

        private string ToXml<T>(T value)
        {
            using var writer = new StringWriter();
            using var xmlWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented };
            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(xmlWriter, value);
            return writer.ToString();
        }
        #endregion
    }
}
