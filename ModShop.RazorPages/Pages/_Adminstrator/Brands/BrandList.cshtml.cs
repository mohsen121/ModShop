using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Brands.Queries.GetAllBrandsCount;
using ModShop.Application.Brands.Queries.GetPagedBrands;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Brands
{
    public class BrandListModel : BaseListPage<Brand, BrandPagedListVm, GetPagedBrandsQuery, GetAllBrandsCountQuery>
    {
        [BindProperty]
        public override DatatableDto Dto { get; set; }

        public BrandListModel() : base("id", "asc")
        {

        }
    }
}
