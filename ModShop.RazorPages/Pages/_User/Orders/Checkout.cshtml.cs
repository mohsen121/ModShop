using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Orders.Commands.PayOrder;
using ModShop.Application.Orders.Queries.GetCurrentCart;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._User.Orders
{
    public class CheckoutModel : BaseUserPage
    {
        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public Guid OrderId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var currentQuery = new GetCurrentCartQuery { };
            Order = await Mediator.Send(currentQuery);

            if (Order == null)
            {
                return Redirect("/");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostPayOrder()
        {
            var command = new PayOrderCommand
            {

            };

            // باید هدایت بشه به بانک

            var payment = await Mediator.Send(command);
            if (payment != null)
            {
                return Redirect("/PaymentCallback/" + payment.Id.ToString());
            }
            return Redirect("/");
        }
    }
}
