using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Widget.TestimonialSlider.Factories;
using Nop.Widget.TestimonialSlider.Models;
using Nop.Widget.TestimonialSlider.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class TestimonialController : BasePluginController
    {
        #region Properties
        private readonly ITestimonialModelFactory _testimonialModelFactory;
        private readonly ITestimonialService _testimonialService;
        #endregion

        #region Constructor
        public TestimonialController(ITestimonialModelFactory testimonialModelFactory,
            ITestimonialService testimonialService)
        {
            _testimonialModelFactory = testimonialModelFactory;
            _testimonialService = testimonialService;
        }
        #endregion

        #region Methods

        public IActionResult List()
        {
            return View("~/Plugins/TestimonialSlider/Views/List.cshtml",new TestimonialSearchModel());
        }
       

        public async Task<IActionResult> AllTestimonials(TestimonialSearchModel searchModel)
        {
            var model = await _testimonialModelFactory.PrepareTestimonailListModelAsync(searchModel);
            return Json(model);
        }

        [ActionName("Create")]
        public IActionResult CreateTestimonials()
        {
            return View("~/Plugins/TestimonialSlider/Views/Create.cshtml", new TestimonailModel());
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreateTestimonials(TestimonailModel model)
        {
            await _testimonialService.InsertOrUpdateTestimonialsAsync(model);
            return RedirectToAction("List");
        }


        [ActionName("Edit")]
        public async Task<IActionResult> EditTestimonials(int Id)
        {
            var model = await _testimonialModelFactory.PrepareTestimonialModelAsnc(Id);
            return View("~/Plugins/TestimonialSlider/Views/Edit.cshtml", model);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditTestimonials(TestimonailModel model)
        {
            await _testimonialService.InsertOrUpdateTestimonialsAsync(model);
            return new NullJsonResult();
        }  
        
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteTestimonials(int Id)
        {
            await _testimonialService.DeleteTesimonialAsync(Id);
            return RedirectToAction("List");
        }  
        
        
        #endregion
    }
}
