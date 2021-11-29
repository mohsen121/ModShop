using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModShop.Application.Orders.Queries.GetCurrentCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class CartViewComponent : ViewComponent
    {
        private IMediator _mediator;

        public CartViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new CartVm();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var current = await _mediator.Send(new GetCurrentCartQuery { });
                if (current != null && current.Items != null)
                {
                    model = new CartVm
                    {
                        BadgeCount = current.Items.Sum(x => x.Quantity),
                        TotalPrice = current.Items.Sum(x => (x.Price * x.Quantity)),
                        Products = current.Items.ToList()
                    };
                }
            }

            return await Task.FromResult((IViewComponentResult)View("~/ViewComponents/Shared/_Cart.cshtml", model));
        }
    }
}
