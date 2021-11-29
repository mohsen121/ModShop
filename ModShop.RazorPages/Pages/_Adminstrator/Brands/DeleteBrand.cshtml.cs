using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Brands.Commands.DeleteBrand;
using ModShop.Application.Brands.Queries.GetBrand;

namespace ModShop.RazorPages.Pages._Adminstrator.Brands
{
    public class DeleteBrandModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnPost([FromForm] Guid id)
        {
            var brandQuery = new GetBrandQuery
            {
                Id = id,
            };
            var brand = await Mediator.Send(brandQuery);

            var command = new DeleteBrandCommand
            {
                Entity = brand
            };

            await Mediator.Send(command);

            return new JsonResult(new { });
        }
    }
}
