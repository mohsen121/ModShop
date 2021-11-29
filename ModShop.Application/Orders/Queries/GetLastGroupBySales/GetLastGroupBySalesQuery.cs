using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Application.Common.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Orders.Queries.GetLastGroupBySales
{
    public class GetLastGroupBySalesQuery : IRequest<List<LastSaleItem>>
    {
    }

    public class GetLastGroupBySalesQueryHandler : IRequestHandler<GetLastGroupBySalesQuery, List<LastSaleItem>>
    {
        private IAppDb _appDb;

        public GetLastGroupBySalesQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public Task<List<LastSaleItem>> Handle(GetLastGroupBySalesQuery request, CancellationToken cancellationToken)
        {
            return _appDb.Orders
                .Where(x => x.IsPaid && x.Created >= DateTime.Now.AddDays(-7))
                .Select(x => new
                {
                    x.Created
                })
                .GroupBy(x => x.Created)
                .Select(x => new LastSaleItem { Date = x.Key, SaleCount = x.Count() })
                .ToListAsync();
        }
    }
}
