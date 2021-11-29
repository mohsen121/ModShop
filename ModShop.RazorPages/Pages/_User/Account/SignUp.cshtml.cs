using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._User.Account
{
    [AllowAnonymous]
    public class SignUpModel : PageModel
    {
        private UserManager<User> _userManager;

        public SignUpModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user != null)
            {
                ModelState.AddModelError("", "این ایمیل تکراری است");
                return Page();
            }

            user = new User
            {
                Name = Input.Name,
                Family = Input.Family,
                Address = Input.Address,
                Province = Input.Province,
                City = Input.City,
                Email = Input.Email,
                UserName = Input.Email,
                Created = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError("", e.Description);

                return Page();
            }

            return Redirect("/Account/Login");
        }
    }

    public class InputModel
    {
        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string Email { get; set; }
        [Required(ErrorMessage = "نام الزامی است")]
        public string Name { get; set; }
        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        public string Family { get; set; }
        [Required(ErrorMessage = "استان الزامی است")]
        public string Province { get; set; }
        [Required(ErrorMessage = "شهر الزامی است")]
        public string City { get; set; }
        [Required(ErrorMessage = "آدرس الزامی است")]
        public string Address { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [StringLength(255, ErrorMessage = "طول رمز باید بین 5 و 255 کاراکتر باشد", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تایید رمز عبور الزامی است")]
        [StringLength(255, ErrorMessage = "طول رمز باید بین 5 و 255 کاراکتر باشد", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
