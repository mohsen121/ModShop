using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents.Common
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public HeaderViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new HeaderViewModel
            {
                User = user
            };
            return await Task.FromResult((IViewComponentResult)View("~/Pages/_Adminstrator/ViewComponents/Common/_Header.cshtml", model));
        }
    }
}
