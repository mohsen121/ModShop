using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Orders.Queries.GetCurrentCart
{
    public class GetCurrentCartQuery : IRequest<Order>
    {
    }

    public class GetCurrentCartQueryHandler : IRequestHandler<GetCurrentCartQuery, Order>
    {
        private IAppDb _appDb;
        private ICurrentUserService _currentUserService;

        public GetCurrentCartQueryHandler(IAppDb appDb, ICurrentUserService currentUserService)
        {
            _appDb = appDb;
            _currentUserService = currentUserService;
        }
        public async Task<Order> Handle(GetCurrentCartQuery request, CancellationToken cancellationToken)
        {
            var order = await _appDb.Orders
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Color)
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Brand)
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.Size)
                .Where(x => x.UserId == _currentUserService.UserId && !x.IsPaid)
                .FirstOrDefaultAsync(cancellationToken);

            return order;
        }
    }
}
