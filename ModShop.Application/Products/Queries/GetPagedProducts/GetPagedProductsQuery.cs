using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Queries.GetPagedBaseEntities;
using System.Linq.Expressions;

namespace ModShop.Application.Products.Queries.GetPagedProducts
{
    public class GetPagedProductsQuery : IGetPagedBaseEntitiesQuery<Product>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDir { get; set; }
        public string Search { get; set; }
        public string SearchExpression { get; set; }
        public string[] Includes { get; set; }
        public Dictionary<string, object[]> Filters { get; set; }
        public Expression<Func<Product, bool>> SearchFunc { get; set; }
    }

    public class GetPagedProductsQueryHandler : GetPagedBaseEntitiesQueryHandler<Product, GetPagedProductsQuery>
    {

        public GetPagedProductsQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
        //public Task<List<Product>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        //{
        //    var query = _appDb.Products.AsNoTracking();

        //    if (!string.IsNullOrEmpty(request.Search))
        //        query = query.Where(x => x.Title.Contains(request.Search));

        //    if (string.IsNullOrEmpty(request.OrderDir))
        //    {
        //        if (!string.IsNullOrEmpty(request.OrderColumn))
        //            query = query.OrderBy(request.OrderColumn, "asc");
        //        else
        //        {
        //            query = query.OrderByDescending(x => x.Id);
        //        }
        //    }
        //    else
        //    {
        //        query = query.OrderBy(request.OrderColumn, request.OrderDir);
        //    }

        //    if (request.Filters != null && request.Filters.Any())
        //    {
        //        foreach (var filter in request.Filters)
        //            query = query.Where(filter.Key, filter.Value);
        //    }
        //    request.Start = request.Start == 0 ? request.Start + 1 : request.Start;

        //    return request.Length == int.MaxValue ? query.ToListAsync(cancellationToken) : query.Skip((request.Start - 1) * request.Length)
        //        .Take(request.Length)
        //        .ToListAsync(cancellationToken);
        //}
    }
}
