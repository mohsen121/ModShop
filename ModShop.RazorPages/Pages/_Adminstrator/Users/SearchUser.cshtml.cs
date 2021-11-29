using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Persistence;

namespace ModShop.RazorPages.Pages._Adminstrator.Users
{
    public class SearchUserModel : BaseAdminstratorPage
    {
        private AppDb _appDb;

        public SearchUserModel(AppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<IActionResult> OnGet(string q)
        {
            var items = await _appDb.Users
                .Where(x => x.Name.Contains(q) || x.Family.Contains(q))
                .ToListAsync();

            return new JsonResult(items.Select(x => new
            {
                Id = x.Id.ToString(),
                Text = $"{x.Name} {x.Family}"
            }));
        }
    }
}
