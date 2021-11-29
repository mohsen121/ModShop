using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application.Products.Commands.NewProduct;
using ModShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ModShop.RazorPages.Pages._Adminstrator.Products
{
    public class NewProductModel : BaseCreatePage<NewProductCommand, Product>
    {
        private IWebHostEnvironment _environment;

        public NewProductModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [BindProperty]
        public override NewProductCommand Command { get; set; }

        [BindProperty]
        [Required]
        public IFormFile Image { get; set; }

        public override async Task<IActionResult> OnPost()
        {
            if (Image != null && Image.Length > 0)
            {
                var extension = Path.GetExtension(Image.FileName);
                var newFileName = Guid.NewGuid() + extension;
                var basePath = Path.Combine(_environment.WebRootPath, "uploads/images/products");
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                var file = Path.Combine(basePath, newFileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Image.CopyToAsync(fileStream);
                }
                Command.Entity.Image = "/uploads/images/products/" + newFileName;
            }
            else
            {
                ModelState.AddModelError("Image", "تصویر محصول الزامی است.");
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
