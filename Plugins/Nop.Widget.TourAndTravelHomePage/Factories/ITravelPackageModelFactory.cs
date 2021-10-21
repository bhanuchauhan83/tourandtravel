using Nop.Widget.TourAndTravelHomePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Factories
{
    public interface ITravelPackageModelFactory
    {
        Task<TravelPackageModel> PrepareTravelPackageModelAsync(int productId);
    }
}
