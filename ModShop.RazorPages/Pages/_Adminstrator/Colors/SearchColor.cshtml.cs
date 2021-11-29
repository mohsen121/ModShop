using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Colors.Queries.GetPagedColor;

namespace ModShop.RazorPages.Pages._Adminstrator.Colors
{
    public class SearchColorModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnGet(string q)
        {
            var query = new GetPagedColorsQuery { Search = q, SearchExpression = "Title.Contains(@0)", Length = int.MaxValue };


            var items = await Mediator.Send(query);

            return new JsonResult(items.Item1.Select(x => new
            {
                Id = x.Id.ToString(),
                Text = x.Title
            }));
        }
    }
}
