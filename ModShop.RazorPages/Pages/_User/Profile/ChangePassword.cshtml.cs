using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._User.Profile
{
    public class ChangePasswordModel : BaseUserPage
    {
        private UserManager<User> _userManager;

        public ChangePasswordModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public PasswordInput Input { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.Password);

            if (result.Succeeded)
                return Redirect("/User/Profile");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return Page();
        }
    }

    public class PasswordInput
    {
        [Required(ErrorMessage = "رمز عبور قدیمی الزامی است")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "رمز عبور جدید الزامی است.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "تایید رمز عبور جدید الزامی است.")]
        [Compare("Password", ErrorMessage = "با رمز عبور همخوانی ندارد")]
        public string ConfirmPassword { get; set; }
    }
}
