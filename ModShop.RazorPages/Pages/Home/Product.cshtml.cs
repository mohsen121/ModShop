using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages.Home
{
    public class ProductModel : BasePageModel
    {
        [BindProperty]
        public ProductPageVm Dto { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        private IAppDb _appDb;

        public ProductModel(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<IActionResult> OnGet(Guid id, [FromQuery] InputModel model)
        {
            var product = await _appDb.Products
                .AsNoTracking()
                .Include(x => x.Color)
                .Include(x => x.Children)
                .ThenInclude(x => x.Size)
                .Include(x => x.Children)
                .ThenInclude(x => x.Color)
                .Include(x => x.Children)
                .ThenInclude(x => x.Category)
                .Include(x => x.Children)
                .ThenInclude(x => x.Brand)

                .Include(x => x.Brand)
                .Include(x => x.Size)
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id && x.BaseProductId == null);
            if (product == null)
                return Redirect("/");

            var selected = new Product();
            if (Input != null)
            {
                if (Input.SelectedColorId != null && Input.SelectedColorId != Guid.Empty)
                    selected = product.Children.Where(x => x.ColorId == Input.SelectedColorId.Value).FirstOrDefault();
                else if (Input.SelectedSizeId != null && Input.SelectedSizeId != Guid.Empty)
                    selected = product.Children.Where(x => x.SizeId == Input.SelectedSizeId.Value).FirstOrDefault();
            }
            else
                selected = product.Children.FirstOrDefault(x => x.Quantity > 0);

            if (selected == null)
                return Redirect("/");

            var sizes = product.Children.Where(x => x.Quantity > 0).Select(z => z.Size).ToList();
            var sizeIds = sizes.Select(a => a.Id).ToArray();
            var colors = product.Children.Where(x => x.Quantity > 0 && sizeIds.Any(a => a == x.SizeId)).Select(x => x.Color).ToList();

            Dto = new ProductPageVm
            {
                Parent = product,
                Selected = selected,
                IsInStock = selected.Quantity > 0,
                Children = product.Children.ToList(),
                ExistingSizes = sizes,
                ExistingColors = colors
            };
            return Page();
        }
    }

    public class InputModel
    {
        public Guid? SelectedSizeId { get; set; }
        public Guid? SelectedColorId { get; set; }
    }
}
