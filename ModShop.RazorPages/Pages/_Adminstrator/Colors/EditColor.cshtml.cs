using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Colors.Commands.EditColor;
using ModShop.Domain.Entities;
using ModShop.Application.Colors.Queries.GetColor;

namespace ModShop.RazorPages.Pages._Adminstrator.Colors
{
    public class EditColorModel : BaseEditPage<GetColorQuery, EditColorCommand, Color, Guid>
    {
        [BindProperty]
        public override EditColorCommand Command { get; set; }

        public EditColorModel() : base(redirectRoute: "/Adminstrator/Colors")
        {

        }
    }
}
