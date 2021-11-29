using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._User.Orders
{
    public class OrderDetailsModel : BaseUserPage
    {
        private IAppDb _appDb;
        private ICurrentUserService _currentUserService;

        public OrderDetailsModel(IAppDb appDb, ICurrentUserService currentUserService)
        {
            _appDb = appDb;
            _currentUserService = currentUserService;
        }

        [BindProperty]
        public Order Order { get; set; }
        public async Task<IActionResult> OnGet(Guid id)
        {
            var order = await _appDb.Orders
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .Include(x => x.User)
                .AsNoTracking()
                .Where(x => x.Id == id && x.UserId == _currentUserService.UserId)
                .FirstOrDefaultAsync();

            if (order == null)
                return Redirect("/User/MyOrders");

            Order = order;

            return Page();
        }
    }
}
