using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sizes.Queries.GetPagedSize;

namespace ModShop.RazorPages.Pages._Adminstrator.Sizes
{
    public class SearchSizeModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnGet(string q)
        {
            var query = new GetPagedSizeQuery { Search = q, SearchExpression = "Title.Contains(@0) || Code.Contains(@0)", Length = int.MaxValue };

            var items = await Mediator.Send(query);

            return new JsonResult(items.Item1.Select(x => new
            {
                Id = x.Id.ToString(),
                Text = x.Title
            }));
        }
    }
}
