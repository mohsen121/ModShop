using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator._Identity.Pages.Account
{
    public class EditProfileModel : BaseAdminstratorPage
    {
        private ICurrentUserService _currentUserService;
        private UserManager<User> _userManager;

        public EditProfileModel(ICurrentUserService currentUserService, UserManager<User> userManager)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
        }
        [BindProperty]
        public User Profile { get; set; }

        [BindProperty]
        public PasswordVm Password { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Profile = await _userManager.GetUserAsync(User);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);

            user.Name = Profile.Name;
            user.Family = Profile.Family;
            user.LastModified = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return Redirect("/Adminstrator");
        }

        public async Task<IActionResult> OnPostChangePassword()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.GetUserAsync(User);

            var result = await _userManager.ChangePasswordAsync(user, Password.CurrentPassword, Password.Password);

            if (result.Succeeded)
                return Redirect("/Adminstrator");

            foreach (var e in result.Errors.Select(x => x.Description))
                ModelState.AddModelError("", e);

            return Page();
        }
    }

    public class PasswordVm
    {
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
