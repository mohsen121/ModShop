using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModShop.Common.Models;

namespace ModShop.Common.Util
{
    public static class PaginationModelTools
    {
        public static Task<PaginationModel<TModel>> ToPaginationModel<TModel>(this IQueryable<TModel> items, int page = 1, int count = 10) where TModel : class
        {
            return Task.FromResult(new PaginationModel<TModel>
            {
                Items = items
                .Skip((page - 1) * count)
                .Take(count)
                .ToList(),
                Page = page,
                Count = count,
                TotalCount = items.Count()
            });
        }
    }
}
