using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._User.Profile
{
    public class ProfileModel : BaseUserPage
    {

        private UserManager<User> _userManager;

        public ProfileModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public User ProfileUser { get; set; }

        public async Task OnGet()
        {
            ProfileUser = await _userManager.GetUserAsync(User);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.GetUserAsync(User);
            user.Name = ProfileUser.Name;
            user.Family = ProfileUser.Family;
            user.Province = ProfileUser.Province;
            user.City = ProfileUser.City;
            user.Address = ProfileUser.Address;
            user.LastModified = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return Redirect("/User/Profile");
        }
    }
}
