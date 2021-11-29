using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application.Products.Commands.EditProduct;
using ModShop.Application.Products.Queries.GetProduct;
using ModShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ModShop.RazorPages.Pages._Adminstrator.Products
{
    public class EditProductModel : BaseEditPage<GetProductQuery, EditProductCommand, Product, Guid>
    {
        private IWebHostEnvironment _environment;

        public EditProductModel(IWebHostEnvironment environment) : base(includes: new[] { "Color", "Size", "Brand", "Category", "BaseProduct" }, redirectRoute: "/Adminstrator/Products")
        {
            _environment = environment;
        }

        [BindProperty]
        public override EditProductCommand Command { get; set; }

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
                Command.Entity.Image = OldImage;
            }
            return await base.OnPost(id);
        }
    }
}
