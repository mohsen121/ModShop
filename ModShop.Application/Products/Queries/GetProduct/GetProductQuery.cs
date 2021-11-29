using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Queries.GetBaseEntity;

namespace ModShop.Application.Products.Queries.GetProduct
{
    public class GetProductQuery : IGetBaseEntityQuery<Product, Guid>
    {
        public Guid Id { get; set; }
        public string[] Includes { get; set; }
    }

    public class GetProductQueryHandler : GetBaseEntityQueryHandler<Product, Guid, GetProductQuery>
    {
        private IAppDb _appDb;

        public GetProductQueryHandler(IAppDb appDb) : base(appDb)
        {
            _appDb = appDb;
        }
    }
}
