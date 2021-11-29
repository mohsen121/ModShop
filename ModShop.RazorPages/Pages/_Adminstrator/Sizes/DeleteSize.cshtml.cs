using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sizes.Commands.DeleteSize;
using ModShop.Application.Sizes.Queries.GetSize;

namespace ModShop.RazorPages.Pages._Adminstrator.Sizes
{
    public class DeleteSizeModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnPost([FromForm] Guid id)
        {
            var sizeQuery = new GetSizeQuery
            {
                Id = id,
            };
            var size = await Mediator.Send(sizeQuery);

            var command = new DeleteSizeCommand
            {
                Entity = size
            };

            await Mediator.Send(command);

            return new JsonResult(new { });
        }

    }
}
