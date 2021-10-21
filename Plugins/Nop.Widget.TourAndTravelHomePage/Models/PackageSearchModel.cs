using Nop.Web.Framework.Models;
using Nop.Web.Models.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{
    public record PackageSearchModel : BaseNopModel
    {
        public PackageSearchModel()
        {
            CatalogProductsModel = new CatalogProductsModel();
        }
       
        public CatalogProductsModel CatalogProductsModel { get; set; }
    }

}
