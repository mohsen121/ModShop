using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents.KDatatable
{
    public class KDatatableViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(DatatableDto dto)
        {
            return await Task.FromResult((IViewComponentResult)View("~/Pages/_Adminstrator/ViewComponents/KDatatable/KDatatableView.cshtml", dto));
        }
    }
}
