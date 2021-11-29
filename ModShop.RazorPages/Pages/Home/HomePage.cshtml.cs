using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Products.Queries.GetTopSale;
using ModShop.Application.Sliders.Queries.GetPagedSliders;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages.Home
{
    public class HomePageModel : BasePageModel
    {
        [BindProperty]
        public List<Slider> Sliders { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Sliders == null || !Sliders.Any())
            {
                var query = new GetPagedSlidersQuery { Start = 0, Length = int.MaxValue, OrderColumn = "Order", OrderDir = "asc" };
                var response = await Mediator.Send(query);
                Sliders = response.Item1;
            }
            return Page();
        }
    }
}
