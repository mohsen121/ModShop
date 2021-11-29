using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : AllQuery<IQueryable<Product>, Product>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IQueryable<Product>>
    {
        private IAppDb _appDb;

        public GetAllProductsQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public Task<IQueryable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_appDb.Products.AsQueryable());
        }
    }
}
