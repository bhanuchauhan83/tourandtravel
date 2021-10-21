using Nop.Core;
using Nop.Widget.TestimonialSlider.Domain;
using Nop.Widget.TestimonialSlider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Service
{
    public interface ITestimonialService
    {
        Task<IPagedList<Testimonials>> GetAllTestimonialsAsync(string UserName,string Country, int pageIndex = 0, int pageSize = int.MaxValue);

        Task<IList<Testimonials>> GetAllTestimonialsAsync();

        Task InsertOrUpdateTestimonialsAsync(TestimonailModel model);

        Task DeleteTesimonialAsync(int id);

        Task<Testimonials> GetTestimonialsByIdAsync(int id);
    }
}
