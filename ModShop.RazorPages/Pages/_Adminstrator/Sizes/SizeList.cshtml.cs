using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sizes.Queries.GetAllSizesCount;
using ModShop.Application.Sizes.Queries.GetPagedSize;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Sizes
{
    public class SizeListModel : BaseListPage<Size, SizePagedListVm, GetPagedSizeQuery, GetAllSizesCountQuery>
    {
        public SizeListModel() : base("created", "desc")
        {
        }

        [BindProperty]
        public override DatatableDto Dto { get; set; }
    }
}
