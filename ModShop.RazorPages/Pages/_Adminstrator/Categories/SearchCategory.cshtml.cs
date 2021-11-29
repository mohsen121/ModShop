using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Categories.Queries.GetPagedCategories;
using ModShop.Application.Categories.Queries.SearchCategory;

namespace ModShop.RazorPages.Pages._Adminstrator.Categories
{
    public class SearchCategoryModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnGet(string q)
        {
            var query = new GetPagedCategoriesQuery { Search = q, SearchExpression = "Title.Contains(@0) || Description.Contains(@0)", Length = int.MaxValue };

            var items = await Mediator.Send(query);

            return new JsonResult(items.Item1.Select(x => new
            {
                Id = x.Id.ToString(),
                Text = x.Title
            }));
        }
    }
}
