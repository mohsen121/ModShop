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
    public class MyOrdersModel : BaseUserPage
    {
        private IAppDb _appDb;
        private ICurrentUserService _currentUserService;

        [BindProperty]
        public List<Order> Orders { get; set; }

        public MyOrdersModel(IAppDb appDb, ICurrentUserService currentUserService)
        {
            _appDb = appDb;
            _currentUserService = currentUserService;
        }
        public async Task OnGet()
        {
            Orders = await _appDb.Orders
                .Where(x => x.IsPaid && x.UserId == _currentUserService.UserId)
                .ToListAsync();
        }
    }
}
