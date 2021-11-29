using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class SearchViewComponent : ViewComponent
    {
        private IAppDb _appDb;
        private SearchComponentVm _model;

        public SearchViewComponent(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            _model ??= new SearchComponentVm
            {
                Items = await _appDb.Categories.AsNoTracking()
                .Select(x => new SearchCategoryItem { Id = x.Id, Title = x.Title }).ToListAsync()
            };
            return await Task.FromResult((IViewComponentResult)View("~/ViewComponents/Shared/_Search.cshtml", _model));
        }
    }
}
