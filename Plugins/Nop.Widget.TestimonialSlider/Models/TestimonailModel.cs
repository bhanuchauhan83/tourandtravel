using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Models
{
    public partial record TestimonailModel: BaseNopEntityModel
    {
        [NopResourceDisplayName("Nop.Widget.TestimonialSlider.PictureId")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        public string PictureURL { get; set; }

        [NopResourceDisplayName("Nop.Widget.TestimonialSlider.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Nop.Widget.TestimonialSlider.UserName")]
        public string UserName { get; set; }

        [NopResourceDisplayName("Nop.Widget.TestimonialSlider.Country")]
        public string Country { get; set; }
    }

    public partial record TestimonailModelList : BasePagedListModel<TestimonailModel>
    {

    }
}
