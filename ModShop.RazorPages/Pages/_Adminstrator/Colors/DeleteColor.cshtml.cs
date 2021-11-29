using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Colors.Commands.DeleteColor;
using ModShop.Application.Colors.Queries.GetColor;

namespace ModShop.RazorPages.Pages._Adminstrator.Colors
{
    public class DeleteColorModel : BaseAdminstratorPage
    {
        public async Task<IActionResult> OnPost([FromForm] Guid id)
        {
            var colorQuery = new GetColorQuery
            {
                Id = id,
            };
            var color = await Mediator.Send(colorQuery);

            var command = new DeleteColorCommand
            {
                Entity = color
            };

            await Mediator.Send(command);

            return new JsonResult(new { });
        }
    }
}
