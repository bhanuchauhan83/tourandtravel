using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Widget.TourAndTravelHomePage.Models;
using Nop.Widget.TourAndTravelHomePage.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class TripPackageManagementController : BasePluginController
    {
        #region Properties
        private readonly ITravelPackageService _travelPackageService;
        #endregion

        #region Constructor
        public TripPackageManagementController(ITravelPackageService travelPackageService)
        {
            _travelPackageService = travelPackageService;
        }
        #endregion

        public async Task<IActionResult> Save(IFormCollection form, TravelPackageModel travelPackageModelmodel)
        {
            var productId = form["Id"].ToString();          
            if (!string.IsNullOrEmpty(productId) && Convert.ToInt32(productId) > 0)
            {
                travelPackageModelmodel.ProductId = Convert.ToInt32(productId);
                await _travelPackageService.InsertOrUpdatePackgeInfoAsync(travelPackageModelmodel);
            }
            return new NullJsonResult();
        }

        //private async Task<TravelPackageModel> GenerateModelUsingFormAsync(IFormCollection form)
        //{
        //    var retVal = new TravelPackageModel();

        //    var includeFlights = form["Packages.IncludeFlights"].ToString();
        //    var includeHotel = form["Packages.IncludeHotel"].ToString();
        //    var includeBus = form["Packages.IncludeBus"].ToString();
        //    var includeTrain = form["Packages.IncludeTrain"].ToString();
        //    var includeCabs = form["Packages.IncludeCabs"].ToString();
        //    var includeCityTour = form["Packages.IncludeCityTour"].ToString();
        //    var hotel_name = form["Packages.HotelDetails.Name"].ToString();
        //    var hotel_address1 = form["Packages.HotelDetails.Address1"].ToString();
        //    var hotel_address2 = form["Packages.HotelDetails.Address2"].ToString();
        //    var hotel_contact = form["Packages.HotelDetails.ContactNumber"].ToString();
        //    var hotel_roomtype = form["Packages.HotelDetails.RoomType"].ToString();
        //    var hotel_serviceDescription = form["Packages.HotelDetails.ServiceDescription"].ToString();
        //    if (!string.IsNullOrEmpty(productId) && Convert.ToInt32(productId) > 0)
        //    {
        //        retVal.ProductId = Convert.ToInt32(productId);
        //        retVal.Packages = new Package()
        //        {
        //            IncludeBus = Convert.ToBoolean(includeBus.Split(',')[0]),
        //            IncludeCabs = Convert.ToBoolean(includeCabs.Split(',')[0]),
        //            IncludeCityTour = Convert.ToBoolean(includeCityTour.Split(',')[0]),
        //            IncludeFlights = Convert.ToBoolean(includeFlights.Split(',')[0]),
        //            IncludeHotel = Convert.ToBoolean(includeHotel.Split(',')[0]),
        //            IncludeTrain = Convert.ToBoolean(includeTrain.Split(',')[0]),
        //            HotelDetails = new HotelDetails()
        //            {
        //                Address1 = hotel_address1,
        //                Address2 = hotel_address2,
        //                ContactNumber = hotel_contact,
        //                Name = hotel_name,
        //                RoomType = hotel_roomtype,
        //                ServiceDescription = hotel_serviceDescription
        //            }
        //        };
        //    }
        //    return retVal;
        //}
    }
}
