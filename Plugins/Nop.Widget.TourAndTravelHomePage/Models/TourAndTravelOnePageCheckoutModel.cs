using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Models.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{
    public class TourAndTravelOnePageCheckoutModel
    {
        public TourAndTravelOnePageCheckoutModel()
        {
            OnePageCheckoutModel = new OnePageCheckoutModel();
        }
        public OnePageCheckoutModel OnePageCheckoutModel { get; set; }
    }



    public class PersonalInfo
    {

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.FirstName")]
        public string FirstName { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.LastName")]

        public string LastName { get; set; }

        [NopResourceDisplayName(TourAndTravelDefault.DefaultStringResource + ".Field.Age")]

        public int Age { get; set; }
    }
}
