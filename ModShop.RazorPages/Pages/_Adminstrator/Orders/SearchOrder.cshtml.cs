using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ModShop.Application.Common.Interfaces;
using ModShop.Persistence;

namespace ModShop.RazorPages.Pages._Adminstrator.Orders
{
    public class SearchOrderModel : BaseAdminstratorPage
    {
        private IAppDb _appDb;

        public SearchOrderModel(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<IActionResult> OnGet(string q)
        {
            var items = await _appDb.Orders
                .Include(x => x.User)
                .Where(x => AppDb.ConvertToString(x.OrderNumber).Contains(q) || x.User.Name.Contains(q) || x.User.Family.Contains(q))
                .ToListAsync();

            return new JsonResult(items.Select(x => new
            {
                Id = x.Id.ToString(),
                Text = $"شماره سفارش: {x.OrderNumber} - کاربر: {x.User.Name} {x.User.Family}"
            }));
        }
    }
}
