using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Colors.Queries.GetAllColorsCount;
using ModShop.Application.Colors.Queries.GetPagedColor;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Colors
{
    public class ColorsListModel : BaseListPage<Color, ColorPagedListVm, GetPagedColorsQuery, GetAllColorsCountQuery>
    {
        [BindProperty]
        public override DatatableDto Dto { get; set; }

        public ColorsListModel() : base("id", "desc")
        {

        }
    }
}
