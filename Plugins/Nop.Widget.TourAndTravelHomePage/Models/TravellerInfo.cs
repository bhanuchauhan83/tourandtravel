using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TourAndTravelHomePage.Models
{
    public class TravellerInfo
    {
        public TravellerInfo()
        {
            AdultsInfo = new List<PersonalInfo>();
            ChildrenInfo = new List<PersonalInfo>();
        }
        public int? CartId { get; set; }
        public DateTime TravellingDate { get; set; }
        public string TravellingFrom { get; set; }
        public string TravellingTo { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }

        public int CustomerId { get; set; }

        public string PersonsInfoXML { get; set; }

        public List<PersonalInfo> AdultsInfo { get; set; }
        public List<PersonalInfo> ChildrenInfo { get; set; }
    }
}
