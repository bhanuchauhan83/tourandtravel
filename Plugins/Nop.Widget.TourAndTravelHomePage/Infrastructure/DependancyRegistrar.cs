using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.Orders;
using Nop.Web.Controllers;
using Nop.Web.Factories;
using Nop.Widget.TourAndTravelHomePage.Controllers;
using Nop.Widget.TourAndTravelHomePage.Factories;
using Nop.Widget.TourAndTravelHomePage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Widget.TourAndTravelHomePage.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependancyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="appSettings">App settings</param>
        public virtual void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            services.AddScoped<ITourAndTravelModelFactory, TourAndTravelModelFactory>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<ICatalogModelFactory, CatalogModelFactoryOverride>();
            services.AddScoped<ITravelPackageModelFactory, TravelPackageModelFactory>();
            services.AddScoped<ITravelPackageService, TravelPackageService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<ProductController, ProductOverrideController>();
            services.AddScoped<ShoppingCartController, ShoppingCartOverrideController>();
            services.AddScoped<IShoppingCartService, ShoppingCartOverrideService>();
            services.AddScoped<IOrderProcessingService, OrderProcessingOverrideService>();
            services.AddScoped<IBestDealService, BestDealService>();
            services.AddScoped<IRecentVideoModelFactory, RecentVideoModelFactory>();
            services.AddScoped<IRecentJourneyService, RecentJourneyService>();
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order => 100;
    }
}
