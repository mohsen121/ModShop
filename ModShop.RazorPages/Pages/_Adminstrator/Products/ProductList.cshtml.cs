using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application.Products.Queries.GetAllProductsCount;
using ModShop.Application.Products.Queries.GetPagedProducts;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents.KDatatable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ModShop.RazorPages.Pages._Adminstrator.Products
{
    public class ProductListModel : BaseListPage<Product, ProductPageListVm, GetPagedProductsQuery, GetAllProductsCountQuery>
    {
        [BindProperty]
        public override DatatableDto Dto { get; set; }

        public ProductListModel() : base("created", "desc")
        {

        }

    }
}
