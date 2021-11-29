using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Domain.Entities;
using ModShop.Application.Colors.Commands.CreateColor;

namespace ModShop.RazorPages.Pages._Adminstrator.Colors
{
    public class NewColorModel : BaseCreatePage<CreateColorCommand, Color>
    {
        [BindProperty]
        public override CreateColorCommand Command { get; set; }

        public NewColorModel() : base("/Adminstrator/Colors")
        {

        }
    }
}
