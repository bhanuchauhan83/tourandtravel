using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Widget.TestimonialSlider.Factories;
using Nop.Widget.TestimonialSlider.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Components
{
    public class TestimonialSliderViewComponent : NopViewComponent
    {
        #region Properties
        private readonly ITestimonialModelFactory _testimonialModelFactory;
        #endregion

        #region Construcotr
        public TestimonialSliderViewComponent(ITestimonialModelFactory testimonialModelFactory)
        {
            _testimonialModelFactory = testimonialModelFactory;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _testimonialModelFactory.PrepareTestimonailSliderModelAsync();
            return View("~/Plugins/TestimonialSlider/Views/Shared/Components/TestimonialSlider/Default.cshtml", model);
        }
    }
}
