using Nop.Widget.TestimonialSlider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Factories
{
    public interface ITestimonialModelFactory
    {
        Task<TestimonailModelList> PrepareTestimonailListModelAsync(TestimonialSearchModel searchModel);
        Task<TestimonailModel> PrepareTestimonialModelAsnc(int id);
        Task<List<TestimonailModel>> PrepareTestimonailSliderModelAsync();
    }
}
