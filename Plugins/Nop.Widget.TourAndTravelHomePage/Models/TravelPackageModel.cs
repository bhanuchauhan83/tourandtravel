using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{
    public record TravelPackageModel : BaseNopEntityModel
    {
        public TravelPackageModel()
        {
            Packages = new Package();
        }
        public int ProductId { get; set; }

        public Package Packages { get; set; }
    }
    public record Package
    {
        public Package()
        {
            HotelDetails = new HotelDetails();            
        }

        public int Days { get; set; }

        public int Nights { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeFlights")]
        public bool IncludeFlights { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeHotel")]
        public bool IncludeHotel { get; set; }

        public HotelDetails HotelDetails { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeBus")]
        public bool IncludeBus { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeTrain")]
        public bool IncludeTrain { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeCabs")]
        public bool IncludeCabs { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.IncludeCityTour")]
        public bool IncludeCityTour { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.PackageDetail")]
        public string PackageDetail { get; set; }
    }

    public class HotelDetails
    {

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.Address1")]
        public string Address1 { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.Address2")]
        public string Address2 { get; set; }


        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.ContactNumber")]
        public string ContactNumber { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.RoomType")]
        public string RoomType { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.StringResourcePrefix + ".Fields.ServiceDescription")]
        public string ServiceDescription { get; set; }
    }

    public class FlightDetails
    {

    }


}
