using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sliders.Commands.EditSlider;
using ModShop.Application.Sliders.Queries.GetSlider;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Sliders
{
    public class EditSliderModel : BaseEditPage<GetSliderQuery, EditSliderCommand, Slider, Guid>
    {
        private IWebHostEnvironment _environment;

        public EditSliderModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public override EditSliderCommand Command { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public string OldImage { get; set; }

        public override async Task<IActionResult> OnPost(Guid id)
        {
            if (Image != null && Image.Length > 0)
            {
                var extension = Path.GetExtension(Image.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var basePath = Path.Combine(_environment.WebRootPath, "uploads/images/sliders");
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
                Command.Entity.Image = OldImage;
            }
            return await base.OnPost(id);
        }
    }
}
