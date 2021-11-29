using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount;

namespace ModShop.Application.Products.Queries.GetAllProductsCount
{
    public class GetAllProductsCountQuery : IGetAllBaseEntitiesCountQuery<Product>
    {
    }

    public class GetAllProductsCountQueryHandler : GetAllBaseEntitiesCountQueryHandler<Product, GetAllProductsCountQuery>
    {
        private IAppDb _appDb;

        public GetAllProductsCountQueryHandler(IAppDb appDb) : base(appDb)
        {
            _appDb = appDb;
        }

    }
}
