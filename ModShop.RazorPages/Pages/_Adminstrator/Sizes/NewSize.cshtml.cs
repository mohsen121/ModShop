using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sizes.Commands.CreateSize;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Sizes
{
    public class NewSizeModel : BaseCreatePage<CreateSizeCommand, Size>
    {
        [BindProperty]
        public override CreateSizeCommand Command { get; set; }
    }
}
