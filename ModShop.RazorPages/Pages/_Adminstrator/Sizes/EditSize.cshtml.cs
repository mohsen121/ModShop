using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sizes.Commands.EditSize;
using ModShop.Application.Sizes.Queries.GetSize;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Sizes
{
    public class EditSizeModel : BaseEditPage<GetSizeQuery, EditSizeCommand, Size, Guid>
    {
        [BindProperty]
        public override EditSizeCommand Command { get; set; }

    }
}
