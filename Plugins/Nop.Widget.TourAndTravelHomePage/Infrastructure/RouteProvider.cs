using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Widget.TourAndTravelHomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.TourAndTravelHomePage.Infrastructure
{ 
    
    /// <summary>
    /// Represents the plugin route provider
    /// </summary>
    public class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="endpointRouteBuilder">Route builder</param>
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(TourAndTravelDefault.TourAndTravelCheckout_Route,
                "checkout",
                new { controller = "TourAndTravelCheckout", action = "Index" });
            endpointRouteBuilder.MapControllerRoute(TourAndTravelDefault.TourAndTravelOnePageCheckout_Route,
                "onepagecheckout",
                new { controller = "TourAndTravelCheckout", action = "OnePageCheckout" }); 
            
            endpointRouteBuilder.MapControllerRoute("SavePersonInfo",
                "checkout/OpcSavePersonInfo",
                new { controller = "TourAndTravelCheckout", action = "OpcSavePersonInfo" }); 
            
            endpointRouteBuilder.MapControllerRoute("SavePersonalInfo",
                "checkout/OpcSavePersonalInfo",
                new { controller = "TourAndTravelCheckout", action = "OpcSavePersonalInfo" });
            
            endpointRouteBuilder.MapControllerRoute("SaveContactInfo",
                "checkout/OpcSaveContactInfo",
                new { controller = "TourAndTravelCheckout", action = "OpcSaveContactInfo" }); 
            
            endpointRouteBuilder.MapControllerRoute("SavePaymentMethod",
                "checkout/OpcTourAndTravelSavePaymentMethod",
                new { controller = "TourAndTravelCheckout", action = "OpcTourAndTravelSavePaymentMethod" });
            
            endpointRouteBuilder.MapControllerRoute("PlaceSearchAutoComplete",
                "SearchTermAutoComplete",
                new { controller = "Search", action = "SearchTermAutoComplete" });
            
            endpointRouteBuilder.MapControllerRoute("LoadHtml",
                "checkout/OpcLoadPersonInfoHtml/",
                new { controller = "TourAndTravelCheckout", action = "OpcLoadPersonInfoHtml" });
            
            endpointRouteBuilder.MapControllerRoute("UpdateTravellerInfo",
                "checkout/UpdateTravellerInfo/",
                new { controller = "TourAndTravelCheckout", action = "UpdateTravellerInfo" });
            
            endpointRouteBuilder.MapControllerRoute("UpdateCartQuantity",
                "cart/UpdateCartQuantity/",
                new { controller = "ShoppingCartOverride", action = "UpdateCartQuantity" });
        }

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority => 100;
    }
}
