using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Common.Exceptions;
using ModShop.Application.Users.Commands.CreateAdmin;

namespace ModShop.RazorPages.Pages._Adminstrator.Users
{
    public class NewAdminModel : BaseAdminstratorPage
    {
        [BindProperty]
        public CreateAdminCommand Command { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                Command.Entity.Created = DateTime.Now;
                Command.Entity.UserName = Command.Entity.Email;
                await Mediator.Send(Command);
            }
            catch (ValidationException ex)
            {
                foreach (var e in ex.Failures)
                {
                    ModelState.AddModelError(e.Key, string.Join('\n', e.Value));
                }
                return Page();
            }

            return Redirect("/Adminstrator/Admins");
        }
    }
}
