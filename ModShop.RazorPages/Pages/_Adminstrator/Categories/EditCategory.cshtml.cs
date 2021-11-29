using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Categories.Commands.EditCategory;
using ModShop.Application.Categories.Queries.GetCategory;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Categories
{
    public class EditCategoryModel : BaseEditPage<GetCategoryQuery, EditCategoryCommand, Category, Guid>
    {
        [BindProperty]
        public override EditCategoryCommand Command { get; set; }

        public EditCategoryModel() : base(new[] { "Parent" }) { }
    }
}
