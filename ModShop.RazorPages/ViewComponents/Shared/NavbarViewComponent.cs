using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class NavbarViewComponent : ViewComponent
    {
        private IAppDb _appDb;
        private IMemoryCache _memoryCache;

        public NavbarViewComponent(IAppDb appDb, IMemoryCache memoryCache)
        {
            _appDb = appDb;
            _memoryCache = memoryCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var trees = new List<CategoryItem>();
            trees = await GetCatTree();

            //var tree = await _appDb.Categories.FromSqlRaw("WITH x AS( " +
            //    "SELECT *, [level] = 0" +
            //    "FROM Categories WHERE ParentId IS NULL " +
            //    "UNION ALL " +
            //    "SELECT t.*, [level] = x.[level] + 1 " +
            //    "FROM x INNER JOIN Categories AS t " +
            //    "ON t.ParentId = x.Id " +
            //    ")" +
            //    "SELECT Id, Title, Description, Created, ParentId, LastModified, [level] FROM x " +
            //    "ORDER BY[level] " +
            //    "OPTION(MAXRECURSION 32)").ToListAsync();

            var model = new CategoriesSiteVm { Categories = trees };
            return await Task.FromResult((IViewComponentResult)View("~/ViewComponents/Shared/_Navbar.cshtml", model));
        }

        private async Task<List<CategoryItem>> GetCatTree()
        {
            var trees = new List<CategoryItem>();
            var cats = await _appDb.Categories
                .AsNoTracking()
                .Where(x => x.ParentId == null)
                .Select(x => new CategoryItem
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToListAsync();

            for (var i = 0; i < cats.Count(); i++)
            {
                trees.Add(
                    new CategoryItem
                    {
                        Id = cats[i].Id,
                        Title = cats[i].Title,
                        Children = GetChildCats(cats[i].Id)
                    });
            }

            return trees;
        }

        private List<CategoryItem> GetChildCats(Guid catId)
        {
            var childs = _appDb.Categories
                .AsNoTracking()
                .Where(x => x.ParentId == catId)
                .Include(x => x.Parent).ToList();

            var result = childs.Select(cat => new CategoryItem
            {
                Id = cat.Id,
                Title = cat.Title,
                Parent = new CategoryItem { Id = cat.Parent.Id, Title = cat.Parent.Title },
            }).ToList();

            var chResults = new List<CategoryItem>();
            foreach (var r in result)
            {
                chResults.AddRange(GetChildCats(r.Id));
            }

            result.AddRange(chResults);
            return result;
        }
    }
}
