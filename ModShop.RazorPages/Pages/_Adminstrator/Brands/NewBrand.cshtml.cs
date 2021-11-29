using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Brands.Commands.CreateBrand;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Brands
{
    public class NewBrandModel : BaseCreatePage<CreateBrandCommand, Brand>
    {
        [BindProperty]
        public override CreateBrandCommand Command { get; set; }
    }
}
