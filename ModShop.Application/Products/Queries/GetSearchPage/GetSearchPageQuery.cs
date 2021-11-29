using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Products.Queries.GetSearchPage
{
    public class GetSearchPageQuery : IRequest<Tuple<List<Product>, int>>
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public Guid[] CategoryIds { get; set; }
        public Guid[] BrandIds { get; set; }
        public Guid[] ColorIds { get; set; }
        public Guid[] SizeIds { get; set; }
        public int PageNumber { get; set; }
        public string Query { get; set; }
    }

    public class GetSearchPageQueryHandler : IRequestHandler<GetSearchPageQuery, Tuple<List<Product>, int>>
    {
        private IAppDb _appDb;

        public GetSearchPageQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<Tuple<List<Product>, int>> Handle(GetSearchPageQuery request, CancellationToken cancellationToken)
        {
            var query = _appDb.Products
                .AsNoTracking()
                .Where(x => x.BaseProductId != null && x.Quantity > 0)
                ;

            if (!string.IsNullOrEmpty(request.Query))
                query = query.Where(x => x.Title.Contains(request.Query) || x.Description.Contains(request.Query));

            if (request.MinPrice == request.MaxPrice)
                request.MaxPrice = double.MaxValue;

            query = query.Where(x => x.Price >= request.MinPrice && x.Price <= request.MaxPrice);
            if (request.CategoryIds != null && request.CategoryIds.Any())
            {
                var childCats = await _appDb.Categories.Where(x => x.ParentId != null && request.CategoryIds.Any(a => a == x.ParentId.Value)).Select(x => x.Id).ToArrayAsync(cancellationToken);
                if (childCats != null && childCats.Any())
                {
                    var newCats = request.CategoryIds.ToList();
                    newCats.AddRange(childCats);

                    request.CategoryIds = newCats.ToArray();
                }

                query = query.Where(x => request.CategoryIds.Any(a => a == x.CategoryId));
            }


            if (request.BrandIds != null && request.BrandIds.Any())
                query = query.Where(x => request.BrandIds.Any(a => a == x.BrandId));

            if (request.ColorIds != null && request.ColorIds.Any())
                query = query.Where(x => x.ColorId != null && request.ColorIds.Any(a => a == x.ColorId.Value));

            if (request.SizeIds != null && request.SizeIds.Any())
                query = query.Where(x => x.SizeId != null && request.SizeIds.Any(a => a == x.SizeId.Value));

            var list = await query
                .OrderByDescending(x => x.Created)
                .Skip((request.PageNumber - 1) * 10)
                .Take(10)
                .ToListAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken);

            return new Tuple<List<Product>, int>(list, count);
        }
    }
}
