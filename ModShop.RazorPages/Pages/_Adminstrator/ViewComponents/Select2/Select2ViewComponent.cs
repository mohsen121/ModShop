using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents.Select2
{
    public class Select2ViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Select2Dto dto)
        {
            return await Task.FromResult((IViewComponentResult)View("~/Pages/_Adminstrator/ViewComponents/Select2/Select2View.cshtml", dto));
        }
    }
}
