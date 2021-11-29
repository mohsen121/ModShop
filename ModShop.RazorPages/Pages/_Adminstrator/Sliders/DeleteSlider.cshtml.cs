using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sliders.Commands.DeleteSlider;
using ModShop.Application.Sliders.Queries.GetSlider;

namespace ModShop.RazorPages.Pages._Adminstrator.Sliders
{
    public class DeleteSliderModel : BaseAdminstratorPage
    {
        private IWebHostEnvironment _env;

        public DeleteSliderModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<IActionResult> OnPost([FromForm] Guid id)
        {
            var query = new GetSliderQuery { Id = id };
            var slider = await Mediator.Send(query);
            var command = new DeleteSliderCommand
            {
                Entity = slider
            };

            await Mediator.Send(command);

            try
            {
                var imgPath = Path.Combine(_env.WebRootPath, slider.Image);
                System.IO.File.Delete(imgPath);
            }
            catch { }


            return new JsonResult(new { });
        }
    }
}
