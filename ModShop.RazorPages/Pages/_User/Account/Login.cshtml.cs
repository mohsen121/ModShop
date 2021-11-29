using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._User.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginModel(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public string ReturnUrl { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage = "{0} اجباری است")]
            [Display(Name = "ایمیل")]
            [EmailAddress]
            public string Email { get; set; }

            //[Required(ErrorMessage = "{0} اجباری است")]
            //[Display(Name = "نام کاربری")]
            //public string UserName { get; set; }

            [Required(ErrorMessage = "{0} اجباری است")]
            [DataType(DataType.Password)]
            [Display(Name = "رمز عبور")]
            public string Password { get; set; }

            [Display(Name = "مرا به خاطر بسپار")]
            public bool RememberMe { get; set; }
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
                return LocalRedirect(Url.Content("~/"));

            returnUrl ??= Url.Content("~/");
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "مشخصات شما در سیستم یافت نشد");
                    return Page();
                }
                //if (!await _userManager.IsInRoleAsync(user, "Admin"))
                //{
                //    ModelState.AddModelError(string.Empty, "شما دسترسی به این قسمت را ندارید");
                //    return Page();
                //}
                var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, true);
                if (result.Succeeded)
                {

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {

                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "تلاش نامعتبر برای ورود به سیستم");
                    return Page();
                }
            }

            return Page();
        }
    }
}
