using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator._Identity.Pages.Account
{
    public class LogoutModel : BaseAdminstratorPage
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutModel(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return LocalRedirect(Url.Content("~/Adminstrator/Account/Login"));

            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return LocalRedirect(Url.Content("~/Adminstrator/Account/Login"));
            }
        }
    }
}
