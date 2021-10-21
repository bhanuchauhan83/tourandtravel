using Nop.Widget.TourAndTravelHomePage.Domain;
using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Services
{
    public interface ITravelPackageService
    {
        Task<TravelPackage> GetTravelPackageByIdAsync(int Id);
        Task<TravelPackage> GetTravelPackageByProductIdAsync(int productId);

        Task InsertOrUpdatePackgeInfoAsync(TravelPackageModel travelPackage);
    }
}
