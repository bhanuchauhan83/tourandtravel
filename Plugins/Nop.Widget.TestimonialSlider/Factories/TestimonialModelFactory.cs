using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;
using Nop.Widget.TestimonialSlider.Models;
using Nop.Widget.TestimonialSlider.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Factories
{
    public class TestimonialModelFactory : ITestimonialModelFactory
    {
        #region Properties
        private readonly ITestimonialService _testimonialService;
        private readonly IPictureService _pictureService;
        #endregion

        #region Constructor
        public TestimonialModelFactory(ITestimonialService testimonialService,
            IPictureService pictureService)
        {
            _testimonialService = testimonialService;
            _pictureService = pictureService;
        }
        #endregion

        public async Task<TestimonailModelList> PrepareTestimonailListModelAsync(TestimonialSearchModel searchModel)
        {
            var list = await _testimonialService.GetAllTestimonialsAsync(searchModel.UserName, searchModel.Country, searchModel.Page - 1, searchModel.PageSize);
            var model = await new TestimonailModelList().PrepareToGridAsync(searchModel, list, () =>
            {
                return list.SelectAwait(async x =>
                {
                    //fill in model values from the entity
                    var productPictureModel = new TestimonailModel()
                    {
                        Country = x.Country,
                        Description = x.Description,
                        UserName = x.UserName,
                        Id = x.Id,
                        PictureId = x.PictureId,
                        PictureURL = await _pictureService.GetPictureUrlAsync(x.PictureId, 75)
                    };
                    return productPictureModel;
                });
            });

            return model;
        }

        public async Task<List<TestimonailModel>> PrepareTestimonailSliderModelAsync()
        {
            var testimonial = await _testimonialService.GetAllTestimonialsAsync();
            return testimonial.Select(x => new TestimonailModel()
            {
                Country = x.Country,
                Description = x.Description,
                Id = x.Id,
                PictureId = x.PictureId,
                PictureURL = _pictureService.GetPictureUrlAsync(x.PictureId, 75).Result,
                UserName = x.UserName
            }).ToList();
        }

        public async Task<TestimonailModel> PrepareTestimonialModelAsnc(int id)
        {
            var testimonial = await _testimonialService.GetTestimonialsByIdAsync(id);
            var retVal = new TestimonailModel();
            if (testimonial != null)
            {
                retVal.Description = testimonial.Description;
                retVal.Country = testimonial.Country;
                retVal.Id = testimonial.Id;
                retVal.PictureId = testimonial.PictureId;
                retVal.UserName = testimonial.UserName;
            }
            return retVal;
        }
    }
}
