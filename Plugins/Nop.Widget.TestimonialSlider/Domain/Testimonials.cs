using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Domain
{
    public class Testimonials : BaseEntity
    {
        public int PictureId { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
    }
}
