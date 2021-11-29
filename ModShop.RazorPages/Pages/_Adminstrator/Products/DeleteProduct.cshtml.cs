using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application.Products.Commands.DeleteProduct;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ModShop.RazorPages.Pages._Adminstrator.Products
{
    public class DeleteProductModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnPost([FromForm] Guid id)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };

            await Mediator.Send(command);

            return new JsonResult(new { });
        }
    }
}
