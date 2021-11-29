using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class ProfileLinksViewComponent : ViewComponent
    {
        private UserManager<User> _userManager;
        private User _user = null;

        public ProfileLinksViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            _user = _user ?? await _userManager.GetUserAsync(HttpContext.User);
            return await Task.FromResult((IViewComponentResult)View("~/ViewComponents/Shared/_Profile.cshtml", _user));
        }
    }
}
