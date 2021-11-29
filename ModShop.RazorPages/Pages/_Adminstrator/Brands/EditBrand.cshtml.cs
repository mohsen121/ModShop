using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Brands.Commands.EditBrand;
using ModShop.Application.Brands.Queries.GetBrand;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Brands
{
    public class EditBrandModel : BaseEditPage<GetBrandQuery, EditBrandCommand, Brand, Guid>
    {
        [BindProperty]
        public override EditBrandCommand Command { get; set; }
    }
}
