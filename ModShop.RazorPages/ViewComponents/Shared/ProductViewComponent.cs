using Microsoft.AspNetCore.Mvc;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class ProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {

            return await Task.FromResult((IViewComponentResult)View("~/ViewComponents/Shared/_Product.cshtml", product));
        }
    }
}
