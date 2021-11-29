using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Products.Queries.GetPagedProducts;

namespace ModShop.RazorPages.Pages._Adminstrator.Products
{
    public class SearchProductModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnGet(string q)
        {
            var query = new GetPagedProductsQuery { Search = q, SearchExpression = "Title.Contains(@0) || Description.Contains(@0)", Length = int.MaxValue };

            var items = await Mediator.Send(query);

            return new JsonResult(items.Item1.Select(x => new
            {
                Id = x.Id.ToString(),
                Text = x.Title
            }));
        }
    }
}
