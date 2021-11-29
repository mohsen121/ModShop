using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Orders.Queries.GetOrder;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Orders
{
    public class OrderDetailsModel : BaseAdminstratorPage
    {
        [BindProperty]
        public Order Order { get; set; }


        public async Task<IActionResult> OnGet(Guid id)
        {
            var query = new GetOrderQuery
            {
                Id = id,
                Includes = new string[] { "Items.Product", "User" }
            };
            var order = await Mediator.Send(query);

            if (order == null)
                return Redirect("/Adminstrator/Orders");

            Order = order;

            return Page();
        }
    }
}
