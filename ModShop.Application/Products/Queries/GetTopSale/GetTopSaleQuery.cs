using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Products.Queries.GetTopSale
{
    public class GetTopSaleQuery : IRequest<List<Product>>
    {
        public int Count { get; set; } = 4;
    }

    public class GetTopSaleQueryHandler : IRequestHandler<GetTopSaleQuery, List<Product>>
    {
        private IAppDb _appDb;

        public GetTopSaleQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<List<Product>> Handle(GetTopSaleQuery request, CancellationToken cancellationToken)
        {
            //var ordersCount = await _appDb.OrderItems
            //    .Include(x => x.Order)
            //    .Include(x => x.Product)
            //    .AsNoTracking()
            //    .Where(x => x.Order.IsPaid)
            //    .GroupBy(x => x.ProductId, (k, g) => new
            //    {
            //        Item = g.FirstOrDefault(),
            //        Sale = g.Sum(a => a.Quantity),
            //    })
            //    .Select(z => z.Item.Product)
            //    .ToListAsync();
            //;


            var query = await
   (from p in _appDb.Products
    join bp in _appDb.Products on p.BaseProductId equals bp.Id
    let totalQuantity = (from op in _appDb.OrderItems
                         join o in _appDb.Orders on op.OrderId equals o.Id
                         where op.ProductId == p.Id && o.IsPaid
                         select op.Quantity).Sum()
    where totalQuantity > 0 && p.BaseProductId != null && p.Quantity > 0
    orderby totalQuantity descending
    select bp).ToListAsync(cancellationToken);
            //var products = from p in _appDb.Products.Where(x => x.BaseProductId == null && x.Quantity > 0).AsNoTracking()
            //               join o in ordersCount on p.Id equals o.ProductId into Orders
            //               from po in Orders.DefaultIfEmpty()
            //               orderby po.Sale, p.Created descending
            //               select p
            //    ;

            return query.Take(request.Count).ToList();
        }
    }
}
