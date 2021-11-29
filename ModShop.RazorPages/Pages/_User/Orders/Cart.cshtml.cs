using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Orders.Commands.AddToCart;
using ModShop.Application.Orders.Commands.DeleteFromCart;
using ModShop.RazorPages.Common.CartModels;

namespace ModShop.RazorPages.Pages._User.Orders
{
    public class CartModel : BaseUserPage
    {
        [BindProperty]
        public CartDto Dto { get; set; }
        public async Task<IActionResult> OnPostAdd()
        {
            if (ModelState.IsValid)
            {
                var command = new AddToCartCommand
                {
                    ProductId = Dto.ProductId,
                    Count = Dto.Count
                };

                await Mediator.Send(command);

                return new OkResult();
            }
            return BadRequest();
        }


        public async Task<IActionResult> OnPostRemove()
        {
            if (ModelState.IsValid)
            {
                var command = new DeleteFromCartCommand
                {
                    ProductId = Dto.ProductId
                };

                await Mediator.Send(command);
                return new OkResult();
            }

            return BadRequest();
        }
    }
}
