using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application.Products.Commands.DeleteProduct;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Categories.Commands.DeleteCategory;
using ModShop.Application.Categories.Queries.GetCategory;

namespace ModShop.RazorPages.Pages._Adminstrator.Categories
{
    public class DeleteCategoryModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnPost([FromForm] Guid id)
        {
            var categoryQuery = new GetCategoryQuery
            {
                Id = id,
            };
            var category = await Mediator.Send(categoryQuery);

            var command = new DeleteCategoryCommand
            {
                Entity = category
            };

            await Mediator.Send(command);

            return new JsonResult(new { });
        }
    }
}
