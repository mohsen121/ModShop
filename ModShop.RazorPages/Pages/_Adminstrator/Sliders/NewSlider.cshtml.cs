using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sliders.Commands.CreateSlider;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Sliders
{
    public class NewSliderModel : BaseCreatePage<CreateSliderCommand, Slider>
    {
        private IWebHostEnvironment _env;

        [BindProperty]
        public override CreateSliderCommand Command { get; set; }

        [BindProperty]
        [Required]
        public IFormFile Image { get; set; }

        public NewSliderModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public override async Task<IActionResult> OnPost()
        {
            if (Image != null && Image.Length > 0)
            {
                var extension = Path.GetExtension(Image.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var basePath = Path.Combine(_env.WebRootPath, "uploads/images/sliders");
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                var file = Path.Combine(basePath, newFileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }
                Command.Entity.Image = "/uploads/images/sliders/" + newFileName;
            }
            else
            {
                ModelState.AddModelError("Image", "تصویر اسلایدر الزامی است.");
            }

            try
            {
                var response = await base.OnPost();
                return response;
            }
            catch (ModShop.Application.Common.Exceptions.ValidationException ex)
            {
                foreach (var e in ex.Failures)
                {
                    ModelState.AddModelError(e.Key, string.Join('\n', e.Value));
                }
                return Page();
            }
        }
    }
}
