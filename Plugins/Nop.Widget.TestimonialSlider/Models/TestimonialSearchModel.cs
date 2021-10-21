using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Models
{
    public record TestimonialSearchModel : BaseSearchModel
    {
        public string UserName { get; set; }

        public string Country { get; set; }
    }
}
