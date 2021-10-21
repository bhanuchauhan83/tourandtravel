using Nop.Core;
using Nop.Data;
using Nop.Widget.TestimonialSlider.Domain;
using Nop.Widget.TestimonialSlider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Service
{
    public class TestimonialService : ITestimonialService
    {
        #region Properties
        private readonly IRepository<Testimonials> _testimonialsRepository;
        #endregion

        #region Constructor
        public TestimonialService(IRepository<Testimonials> testimonialsRepository)
        {
            _testimonialsRepository = testimonialsRepository;
        }
        #endregion

        public async Task DeleteTesimonialAsync(int id)
        {
            var testimonial = await GetTestimonialsByIdAsync(id);
            if (testimonial != null)
                await _testimonialsRepository.DeleteAsync(testimonial);
        }

        public async Task<IPagedList<Testimonials>> GetAllTestimonialsAsync(string UserName, string Country, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var list = await _testimonialsRepository.Table.ToListAsync();
            if (!string.IsNullOrEmpty(UserName))
                list = list.Where(x => x.UserName == UserName).ToList();
            if (!string.IsNullOrEmpty(Country))
                list = list.Where(x => x.Country == Country).ToList();
            return new PagedList<Testimonials>(list, pageIndex, pageSize);
        }

        public async Task InsertOrUpdateTestimonialsAsync(TestimonailModel model)
        {
            var testimonial = await GetTestimonialsByIdAsync(model.Id);
            if (testimonial != null)
            {
                testimonial.Country = model.Country;
                testimonial.Description = model.Description;
                testimonial.PictureId = model.PictureId;
                testimonial.UserName = model.UserName;
                await _testimonialsRepository.UpdateAsync(testimonial);
            }
            else
            {
                await _testimonialsRepository.InsertAsync(new Testimonials()
                {
                    Country = model.Country,
                    Description = model.Description,
                    PictureId = model.PictureId,
                    UserName = model.UserName
                });
            }
        }

        public async Task<Testimonials> GetTestimonialsByIdAsync(int id)
        {
            return await _testimonialsRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Testimonials>> GetAllTestimonialsAsync()
        {
            return await _testimonialsRepository.Table.ToListAsync();
        }
    }
}
