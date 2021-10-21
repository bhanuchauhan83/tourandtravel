using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Widget.TestimonialSlider
{
    public class SliderPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ILocalizationService _localizationService;

        public SliderPlugin(IWebHelper webHelper,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _localizationService = localizationService;
        }
        public bool HideInWidgetList => false;

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return TestimonialSliderDefault.TESTIMONIAL_SLIDER_VIEWCOMPONENT;
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/Testimonial/List";
        }

        public Task<IList<string>> GetWidgetZonesAsync()
        {
           return Task.FromResult<IList<string>>(new List<string>()
            {
                "TestimonialSliderSection"
           });
        }

        public async override Task InstallAsync()
        {
            await _localizationService.AddLocaleResourceAsync(new Dictionary<string, string>
            {
                ["Nop.Widget.TestimonialSlider.PictureId"] = "User Image",
                ["Nop.Widget.TestimonialSlider.Description"] = "Description",
                ["Nop.Widget.TestimonialSlider.UserName"] = "User Name",
                ["Nop.Widget.TestimonialSlider.Country"] = "Country",  
                
                ["Nop.Widget.TestimonialSlider.PictureId.Hint"] = "User's Image",
                ["Nop.Widget.TestimonialSlider.Description.Hint"] = "Testimonial description",
                ["Nop.Widget.TestimonialSlider.UserName.Hint"] = "User's Name",
                ["Nop.Widget.TestimonialSlider.Country.Hint"] = "User's country name"
            });
             await base.InstallAsync();
        }

    }
}
