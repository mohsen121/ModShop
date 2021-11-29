using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Categories.Commands.CreateCategory;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Categories
{
    public class NewCategoryModel : BaseCreatePage<CreateCategoryCommand, Category>
    {
        [BindProperty]
        public override CreateCategoryCommand Command { get; set; }
    }
}
