using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Categories.Queries.GetAllCategoriesCount;
using ModShop.Application.Categories.Queries.GetPagedCategories;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Categories
{
    public class CategoryListModel : BaseListPage<Category, CategoryPagedListVm, GetPagedCategoriesQuery, GetAllCategoriesCountQuery>
    {
        [BindProperty]
        public override DatatableDto Dto { get; set; }

        public CategoryListModel() : base("id", "desc")
        {
        }
    }
}
